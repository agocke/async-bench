
using System;
using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace Serde.Json
{
    internal partial struct JsonDeserializer : IDeserializer
    {
        // Need to use a class so it can be referenced from the Enumerable and
        // Dictionary implementations
        private sealed class DeserializerState
        {
            public Utf8JsonReader Reader;
        }
        private readonly DeserializerState _state;

        public static JsonDeserializer FromString(string s)
        {
            return new JsonDeserializer(Encoding.UTF8.GetBytes(s));
        }

        private JsonDeserializer(byte[] bytes)
        {
            _state = new DeserializerState
            {
                Reader = new Utf8JsonReader(bytes, default)
            };
        }

        private void SaveState(in Utf8JsonReader reader)
        {
            _state.Reader = reader;
        }

        private ref Utf8JsonReader GetReader()
        {
            return ref _state.Reader;
        }

        public async ValueTask<T> DeserializeAny<T>(IDeserializeVisitor<T> v)
        {
            var reader = GetReader();
            await reader.ReadOrThrow();
            T result;
            switch (reader.TokenType)
            {
                case JsonTokenType.StartArray:
                    result = await DeserializeEnumerable<T>(v);
                    break;

                case JsonTokenType.Number:
                    result = await DeserializeDouble<T>(v);
                    break;

                case JsonTokenType.StartObject:
                    result = await DeserializeDictionary<T>(v);
                    break;

                case JsonTokenType.String:
                    result = await DeserializeString<T>(v);
                    break;

                case JsonTokenType.True:
                case JsonTokenType.False:
                    result = DeserializeBool<T>(v);
                    break;

                default:
                    throw new InvalidDeserializeValueException($"Could not deserialize '{reader.TokenType}");
            }
            return result;
        }

        public async ValueTask<T> DeserializeBool<T>(IDeserializeVisitor<T> v)
        {
            await GetReader().ReadOrThrow();
            bool b = GetReader().GetBoolean();
            return v.VisitBool(b);
        }

        public async ValueTask<T> DeserializeDictionary<T>(IDeserializeVisitor<T> v)
        {
            await GetReader().ReadOrThrow();

            if (GetReader().TokenType != JsonTokenType.StartObject)
            {
                throw new InvalidDeserializeValueException("Expected object start");
            }

            var map = new DeDictionary(this);
            return await v.VisitDictionary(map);
        }

        public ValueTask<T> DeserializeFloat<T>(IDeserializeVisitor<T> v)
            => DeserializeDouble<T>(v);

        public async ValueTask<T> DeserializeDouble<T>(IDeserializeVisitor<T> v)
        {
            await GetReader().ReadOrThrow();
            var d = GetReader().GetDouble();
            return v.VisitDouble(d);
        }

        public async ValueTask<T> DeserializeDecimal<T>(IDeserializeVisitor<T> v)
        {
            await GetReader().ReadOrThrow();
            var d = GetReader().GetDecimal();
            return v.VisitDecimal(d);
        }

        public async ValueTask<T> DeserializeEnumerable<T>(IDeserializeVisitor<T> v)
        {
            await GetReader().ReadOrThrow();

            if (GetReader().TokenType != JsonTokenType.StartArray)
            {
                throw new InvalidDeserializeValueException("Expected array start");
            }

            var enumerable = new DeEnumerable(this);
            return v.VisitEnumerable(ref enumerable);
        }

        private struct DeEnumerable : IDeserializeEnumerable
        {
            private JsonDeserializer _deserializer;
            public DeEnumerable(JsonDeserializer de)
            {
                _deserializer = de;
            }
            public int? SizeOpt => null;

            public bool TryGetNext<T, D>([MaybeNullWhen(false)] out T next)
                where D : IDeserialize<T>
            {
                var reader = _deserializer.GetReader();
                // Check if the next token is the end of the array, but don't advance the stream if not
                reader.ReadOrThrow();
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    _deserializer.SaveState(reader);
                    next = default;
                    return false;
                }
                // Don't save state
                next = D.Deserialize(ref _deserializer);
                return true;
            }
        }

        private struct DeDictionary : IDeserializeDictionary
        {
            private JsonDeserializer _deserializer;
            public DeDictionary(JsonDeserializer de)
            {
                _deserializer = de;
            }

            public int? SizeOpt => null;

            public async ValueTask<(bool HasNext, (K, V) Entry)> TryGetNextEntry<K, DK, V, DV>()
                where DK : IDeserialize<K>
                where DV : IDeserialize<V>
            {
                // Don't save state
                var (hasNext, key) = await TryGetNextKey<K, DK>();
                if (!hasNext)
                {
                    return (false, default);
                }
                var nextValue = GetNextValue<V, DV>();
                return (true, (key, nextValue));
            }

            public async ValueTask<(bool HasNext, K Key)> TryGetNextKey<K, D>() where D : IDeserialize<K>
            {
                while (true)
                {
                    var reader = _deserializer.GetReader();
                    await reader.ReadOrThrow();
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.EndObject:
                            // Check if the next token is the end of the object, but don't advance the stream if not
                            _deserializer.SaveState(reader);
                            return (false, default!);
                        case JsonTokenType.PropertyName:
                            return (true, D.Deserialize(ref _deserializer));
                        default:
                            // If we aren't at a property name, we must be at a value and intending to skip it
                            // Call Skip in case we are starting a new array or object. Doesn't do
                            // anything for bare tokens, but we've already read one token forward above,
                            // so we can simply save the state and continue
                            reader.Skip();
                            _deserializer.SaveState(reader);
                            break;
                    }
                }
            }

            public V GetNextValue<V, D>() where D : IDeserialize<V>
            {
                return D.Deserialize(ref _deserializer);
            }
        }

        public ValueTask<T> DeserializeSByte<T>(IDeserializeVisitor<T> v)
            => DeserializeI64<T>(v);

        public ValueTask<T> DeserializeI16<T>(IDeserializeVisitor<T> v)
            => DeserializeI64<T>(v);


        public ValueTask<T> DeserializeI32<T>(IDeserializeVisitor<T> v)
            => DeserializeI64<T>(v);

        public async ValueTask<T> DeserializeI64<T>(IDeserializeVisitor<T> v)
        {
            await GetReader().ReadOrThrow();
            var i64 = GetReader().GetInt64();
            return v.VisitI64(i64);
        }

        public async ValueTask<T> DeserializeString<T>(IDeserializeVisitor<T> v)
        {
            await GetReader().ReadOrThrow();
            if (GetReader().HasValueSequence || GetReader().ValueIsEscaped)
            {
                var s = GetReader().GetString()!;
                return v.VisitString(s);
            }
            else
            {
                var result = v.VisitUtf8Span(GetReader().ValueSpan);
                return result;
            }
        }

        public ValueTask<T> DeserializeIdentifier<T>(IDeserializeVisitor<T> v)
            => DeserializeString<T>(v);

        public ValueTask<T> DeserializeType<T>(string typeName, ReadOnlySpan<string> fieldNames, IDeserializeVisitor<T> v)
        {
            // Types are identical to dictionaries
            return DeserializeDictionary<T>(v);
        }

        public ValueTask<T> DeserializeByte<T>(IDeserializeVisitor<T> v)
            => DeserializeU64<T>(v);

        public ValueTask<T> DeserializeU16<T>(IDeserializeVisitor<T> v)
            => DeserializeU64<T>(v);

        public ValueTask<T> DeserializeU32<T>(IDeserializeVisitor<T> v)
            => DeserializeU64<T>(v);

        public async ValueTask<T> DeserializeU64<T>(IDeserializeVisitor<T> v)
        {
            await GetReader().ReadOrThrow();
            var u64 = GetReader().GetUInt64();
            return v.VisitU64(u64);
        }

        public ValueTask<T> DeserializeChar<T>(IDeserializeVisitor<T> v)
            => DeserializeString<T>(v);

        public async ValueTask<T> DeserializeNullableRef<T>(IDeserializeVisitor<T> v)
        {
            // Grab copy of reader
            var reader = GetReader();
            await reader.ReadOrThrow();
            if (reader.TokenType == JsonTokenType.Null)
            {
                return v.VisitNull();
            }
            else
            {
                var deserializer = this;
                return v.VisitNotNull(ref deserializer);
            }
        }
    }

    internal static class Utf8JsonReaderExtensions
    {
        public static ValueTask ReadOrThrow(ref this Utf8JsonReader reader)
        {
            if (!reader.Read())
            {
                throw new InvalidDeserializeValueException("Unexpected end of stream");
            }
            return ValueTask.CompletedTask;
        }
    }
}