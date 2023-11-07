
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Serde
{
    /// <summary>
    /// The driving interface for deserializing a given type. This interface should be implemented
    /// for any type that wants to be deserialized by the Serde framework. The implementation should
    /// be independent of the format the type is being deserialized from.
    /// </summary>
    public interface IDeserialize<T>
    {
        abstract static ValueTask<T> Deserialize(IDeserializer deserializer);
    }

    /// <summary>
    /// Thrown from implementations of <see cref="IDeserializer" />. Indicates that an unexpected
    /// value was seen in the input which cannot be converted to the target type.
    /// </summary>
    public sealed class InvalidDeserializeValueException : Exception
    {
        public InvalidDeserializeValueException(string msg)
        : base(msg)
        { }
    }

    public interface IDeserializeVisitor<T>
    {
        string ExpectedTypeName { get; }
        T VisitBool(bool b) => throw new InvalidDeserializeValueException("Expected type " + ExpectedTypeName);
        T VisitChar(char c) => VisitString(c.ToString());
        T VisitByte(byte b) => VisitU64(b);
        T VisitU16(ushort u16) => VisitU64(u16);
        T VisitU32(uint u32) => VisitU64(u32);
        T VisitU64(ulong u64) => throw new InvalidDeserializeValueException("Expected type " + ExpectedTypeName);
        T VisitSByte(sbyte b) => VisitI64(b);
        T VisitI16(short i16) => VisitI64(i16);
        T VisitI32(int i32) => VisitI64(i32);
        T VisitI64(long i64) => throw new InvalidDeserializeValueException("Expected type " + ExpectedTypeName);
        T VisitFloat(float f) => VisitDouble(f);
        T VisitDouble(double d) => throw new InvalidDeserializeValueException("Expected type " + ExpectedTypeName);
        T VisitDecimal(decimal d) => throw new InvalidDeserializeValueException("Expected type " + ExpectedTypeName);
        T VisitString(string s) => throw new InvalidDeserializeValueException("Expected type " + ExpectedTypeName);
        T VisitUtf8Span(ReadOnlySpan<byte> s) => throw new InvalidDeserializeValueException("Expected type " + ExpectedTypeName);
        ValueTask<T> VisitEnumerable(IDeserializeEnumerable d)
            => throw new InvalidDeserializeValueException("Expected type " + ExpectedTypeName);
        ValueTask<T> VisitDictionary(IDeserializeDictionary d)
            => throw new InvalidDeserializeValueException("Expected type " + ExpectedTypeName);
        T VisitNull() => throw new InvalidOperationException("Expected type " + ExpectedTypeName);
        ValueTask<T> VisitNotNull(IDeserializer d) => throw new InvalidOperationException("Expected type " + ExpectedTypeName);
    }

    public interface IDeserializeEnumerable
    {
        ValueTask<(bool HasNext, T Next)> TryGetNext<T, D>() where D : IDeserialize<T>;
        int? SizeOpt { get; }
    }

    public interface IDeserializeDictionary
    {
        ValueTask<(bool HasNext, K Key)> TryGetNextKey<K, D>()
            where D : IDeserialize<K>;
        ValueTask<V> GetNextValue<V, D>() where D : IDeserialize<V>;
        ValueTask<(bool HasNext, (K, V) Entry)> TryGetNextEntry<K, DK, V, DV>()
            where DK : IDeserialize<K>
            where DV : IDeserialize<V>;
        int? SizeOpt { get; }
    }

    public interface IDeserializer
    {
        ValueTask<T> DeserializeAny<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeBool<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeChar<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeByte<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeU16<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeU32<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeU64<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeSByte<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeI16<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeI32<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeI64<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeFloat<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeDouble<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeDecimal<T>(IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeString<T>(IDeserializeVisitor<T>  v);
        ValueTask<T> DeserializeIdentifier<T>(IDeserializeVisitor<T>  v);
        ValueTask<T> DeserializeType<T>(string typeName, ReadOnlySpan<string> fieldNames, IDeserializeVisitor<T> v);
        ValueTask<T> DeserializeEnumerable<T>(IDeserializeVisitor<T>  v);
        ValueTask<T> DeserializeDictionary<T>(IDeserializeVisitor<T>  v);
        ValueTask<T> DeserializeNullableRef<T>(IDeserializeVisitor<T>  v);
    }
}