
using System;
using System.Collections.Immutable;
using System.Text;
using System.Threading.Tasks;

namespace Serde.Json
{
    partial record JsonValue : IDeserialize<JsonValue>
    {
        static ValueTask<JsonValue> IDeserialize<JsonValue>.Deserialize(IDeserializer deserializer)
        {
            return deserializer.DeserializeAny(Visitor.Instance);
        }

        private sealed class Visitor : IDeserializeVisitor<JsonValue>
        {
            public static readonly Visitor Instance = new Visitor();
            private Visitor() { }

            public string ExpectedTypeName => nameof(JsonValue);

            public async ValueTask<JsonValue> VisitEnumerable<D>(IDeserializeEnumerable d)
            {
                var builder = ImmutableArray.CreateBuilder<JsonValue>(d.SizeOpt ?? 3);
                while (true)
                {
                    var (hasNext, next) = await d.TryGetNext<JsonValue, JsonValue>();
                    if (!hasNext)
                    {
                        break;
                    }
                    builder.Add(next);
                }
                return new Array(builder.ToImmutable());
            }

            public async ValueTask<JsonValue> VisitDictionary<D>(IDeserializeDictionary d)
            {
                var builder = ImmutableDictionary.CreateBuilder<string, JsonValue>();
                while (true)
                {
                    var (hasNext, next) = await d.TryGetNextEntry<string, StringWrap, JsonValue, JsonValue>();
                    if (!hasNext)
                    {
                        break;
                    }
                    builder.Add(next.Item1, next.Item2);
                }
                return new Object(builder.ToImmutable());
            }

            public JsonValue VisitBool(bool b) => new Bool(b);
            public JsonValue VisitDouble(double d) => new Number(d);
            public JsonValue VisitString(string s) => new String(s);
            public JsonValue VisitUtf8Span(ReadOnlySpan<byte> s) => VisitString(Encoding.UTF8.GetString(s));
        }
    }
}