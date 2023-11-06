// Contains implementations of data interfaces for core types

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Serde
{
    public static class EnumerableHelpers
    {
        public static void SerializeSpan<T, TWrap>(string typeName, ReadOnlySpan<T> arr, ISerializer serializer)
            where TWrap : struct, ISerializeWrap<T, TWrap>, ISerialize
        {
            var enumerable = serializer.SerializeEnumerable(typeName, arr.Length);
            foreach (var item in arr)
            {
                enumerable.SerializeElement(TWrap.Create(item));
            }
            enumerable.End();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SerializeList<T, TWrap>(string typeName, List<T> list, ISerializer serializer)
            where TWrap : struct, ISerializeWrap<T, TWrap>, ISerialize
        {
            var enumerable = serializer.SerializeEnumerable(typeName, list.Count);
            foreach (var item in list)
            {
                enumerable.SerializeElement(TWrap.Create(item));
            }
            enumerable.End();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SerializeIList<T, TWrap>(string typeName, IList<T> list, ISerializer serializer)
            where TWrap : struct, ISerializeWrap<T, TWrap>, ISerialize
        {
            var enumerable = serializer.SerializeEnumerable(typeName, list.Count);
            foreach (var item in list)
            {
                enumerable.SerializeElement(TWrap.Create(item));
            }
            enumerable.End();
        }
    }

    public readonly record struct IdWrap<T>(T Value) : ISerialize, ISerializeWrap<T, IdWrap<T>>
        where T : ISerialize
    {
        public static IdWrap<T> Create(T t) => new IdWrap<T>(t);

        void ISerialize.Serialize(ISerializer serializer) => Value.Serialize(serializer);
    }

    public static class ArrayWrap
    {
        public readonly record struct SerializeImpl<T, TWrap>(T[] Value)
            : ISerialize, ISerializeWrap<T[], SerializeImpl<T, TWrap>>
           where TWrap : struct, ISerializeWrap<T, TWrap>, ISerialize
        {
            public static SerializeImpl<T, TWrap> Create(T[] t) => new SerializeImpl<T, TWrap>(t);

            void ISerialize.Serialize(ISerializer serializer)
                => EnumerableHelpers.SerializeSpan<T, TWrap>(typeof(T[]).ToString(), Value, serializer);
        }

        public readonly struct DeserializeImpl<T, TWrap> : IDeserialize<T[]>
            where TWrap : IDeserialize<T>
        {
            static ValueTask<T[]> IDeserialize<T[]>.Deserialize(IDeserializer deserializer)
            {
                return deserializer.DeserializeEnumerable(new SerdeVisitor());
            }
            private struct SerdeVisitor : IDeserializeVisitor<T[]>
            {
                string IDeserializeVisitor<T[]>.ExpectedTypeName => typeof(T[]).ToString();

                async ValueTask<T[]> IDeserializeVisitor<T[]>.VisitEnumerable(IDeserializeEnumerable d)
                {
                    if (d.SizeOpt is int size)
                    {
                        var array = new T[size];
                        for (int i = 0; i < size; i++)
                        {
                            var (hasNext, next) = await d.TryGetNext<T, TWrap>();
                            if (!hasNext)
                            {
                                throw new InvalidDeserializeValueException($"Expected enumerable of size {size}, but only received {i} items");
                            }
                            array[i] = next;
                        }
                        return array;
                    }
                    else
                    {
                        var list = new List<T>();
                        while (true)
                        {
                            var (hasNext, next) = await d.TryGetNext<T, TWrap>();
                            if (!hasNext)
                            {
                                break;
                            }
                            list.Add(next);
                        }
                        return list.ToArray();
                    }
                }
            }
        }
    }

    public static class ListWrap
    {
        public readonly record struct SerializeImpl<T, TWrap>(List<T> Value)
            : ISerialize, ISerializeWrap<List<T>, SerializeImpl<T, TWrap>>
            where TWrap : struct, ISerializeWrap<T, TWrap>, ISerialize
        {
            public static SerializeImpl<T, TWrap> Create(List<T> t) => new SerializeImpl<T, TWrap>(t);

            void ISerialize.Serialize(ISerializer serializer)
                => EnumerableHelpers.SerializeList<T, TWrap>(typeof(List<T>).ToString(), Value, serializer);
        }

        public readonly struct DeserializeImpl<T, TWrap> : IDeserialize<List<T>>
            where TWrap : IDeserialize<T>
        {
            static ValueTask<List<T>> IDeserialize<List<T>>.Deserialize(IDeserializer deserializer)
            {
                return deserializer.DeserializeEnumerable(new SerdeVisitor());
            }
            private struct SerdeVisitor : IDeserializeVisitor<List<T>>
            {
                string IDeserializeVisitor<List<T>>.ExpectedTypeName => typeof(T[]).ToString();

                async ValueTask<List<T>> IDeserializeVisitor<List<T>>.VisitEnumerable(IDeserializeEnumerable d)
                {
                    List<T> list;
                    if (d.SizeOpt is int size)
                    {
                        list = new List<T>(size);
                    }
                    else
                    {
                        size = -1; // Set initial size to unknown
                        list = new List<T>();
                    }

                    while (true)
                    {
                        var (hasNext, next) = await d.TryGetNext<T, TWrap>();
                        if (!hasNext)
                        {
                            break;
                        }
                        list.Add(next);
                    }
                    if (size >= 0 && list.Count != size)
                    {
                        throw new InvalidDeserializeValueException($"Expected enumerable of size {size}, but only received {list.Count} items");
                    }
                    return list;
                }
            }
        }
    }

    public static class ImmutableArrayWrap
    {
        public readonly record struct SerializeImpl<T, TWrap>(ImmutableArray<T> Value)
            : ISerialize, ISerializeWrap<ImmutableArray<T>, SerializeImpl<T, TWrap>>
            where TWrap : struct, ISerializeWrap<T, TWrap>, ISerialize
        {
            public static SerializeImpl<T, TWrap> Create(ImmutableArray<T> t) => new SerializeImpl<T, TWrap>(t);

            void ISerialize.Serialize(ISerializer serializer)
                => EnumerableHelpers.SerializeSpan<T, TWrap>(typeof(ImmutableArray<T>).ToString(), Value.AsSpan(), serializer);
        }

        public readonly struct DeserializeImpl<T, TWrap> : IDeserialize<ImmutableArray<T>>
            where TWrap : IDeserialize<T>
        {
            static ValueTask<ImmutableArray<T>> IDeserialize<ImmutableArray<T>>.Deserialize(IDeserializer deserializer)
            {
                return deserializer.DeserializeEnumerable(new Visitor());
            }

            private struct Visitor : IDeserializeVisitor<ImmutableArray<T>>
            {
                public string ExpectedTypeName => typeof(ImmutableArray<T>).ToString();
                async ValueTask<ImmutableArray<T>> IDeserializeVisitor<ImmutableArray<T>>.VisitEnumerable(IDeserializeEnumerable d)
                {
                    ImmutableArray<T>.Builder builder;
                    if (d.SizeOpt is int size)
                    {
                        builder = ImmutableArray.CreateBuilder<T>(size);
                    }
                    else
                    {
                        size = -1; // Set initial size to unknown
                        builder = ImmutableArray.CreateBuilder<T>();
                    }

                    while (true)
                    {
                        var (hasNext, next) = await d.TryGetNext<T, TWrap>();
                        if (!hasNext)
                        {
                            break;
                        }
                        builder.Add(next);
                    }
                    if (size >= 0 && builder.Count != size)
                    {
                        throw new InvalidDeserializeValueException($"Expected {size} items, found {builder.Count}");
                    }
                    return builder.ToImmutable();
                }
            }
        }
    }
}