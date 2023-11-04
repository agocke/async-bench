
#nullable enable
extern alias SerdeSync;
using System;
using SerdeSync::Serde;
using Serde = SerdeSync::Serde;

namespace Benchmarks
{
    partial record Location : SerdeSync::Serde.IDeserialize<Benchmarks.Location>
    {
        static Benchmarks.Location SerdeSync::Serde.IDeserialize<Benchmarks.Location>.Deserialize<D>(ref D deserializer)
        {
            var visitor = new SerdeVisitor();
            var fieldNames = new[]
            {
                "Id",
                "Address1",
                "Address2",
                "City",
                "State",
                "PostalCode",
                "Name",
                "PhoneNumber",
                "Country"
            };
            return deserializer.DeserializeType<Benchmarks.Location, SerdeVisitor>("Location", fieldNames, visitor);
        }

        private sealed class SerdeVisitor : IDeserializeVisitor<Benchmarks.Location>
        {
            public string ExpectedTypeName => "Benchmarks.Location";

            private struct FieldNameVisitor : IDeserialize<byte>, IDeserializeVisitor<byte>
            {
                public static byte Deserialize<D>(ref D deserializer)
                    where D : IDeserializer => deserializer.DeserializeString<byte, FieldNameVisitor>(new FieldNameVisitor());
                public string ExpectedTypeName => "string";

                byte IDeserializeVisitor<byte>.VisitString(string s) => VisitUtf8Span(System.Text.Encoding.UTF8.GetBytes(s));
                public byte VisitUtf8Span(System.ReadOnlySpan<byte> s)
                {
                    switch (s[0])
                    {
                        case (byte)'i'when s.SequenceEqual("id"u8):
                            return 1;
                        case (byte)'a'when s.SequenceEqual("address1"u8):
                            return 2;
                        case (byte)'a'when s.SequenceEqual("address2"u8):
                            return 3;
                        case (byte)'c'when s.SequenceEqual("city"u8):
                            return 4;
                        case (byte)'s'when s.SequenceEqual("state"u8):
                            return 5;
                        case (byte)'p'when s.SequenceEqual("postalCode"u8):
                            return 6;
                        case (byte)'n'when s.SequenceEqual("name"u8):
                            return 7;
                        case (byte)'p'when s.SequenceEqual("phoneNumber"u8):
                            return 8;
                        case (byte)'c'when s.SequenceEqual("country"u8):
                            return 9;
                        default:
                            return 0;
                    }
                }
            }

            Benchmarks.Location SerdeSync::Serde.IDeserializeVisitor<Benchmarks.Location>.VisitDictionary<D>(ref D d)
            {
                int _l_id = default !;
                string _l_address1 = default !;
                string _l_address2 = default !;
                string _l_city = default !;
                string _l_state = default !;
                string _l_postalcode = default !;
                string _l_name = default !;
                string _l_phonenumber = default !;
                string _l_country = default !;
                ushort _r_assignedValid = 0b0;
                while (d.TryGetNextKey<byte, FieldNameVisitor>(out byte key))
                {
                    switch (key)
                    {
                        case 1:
                            _l_id = d.GetNextValue<int, Int32Wrap>();
                            _r_assignedValid |= ((ushort)1) << 0;
                            break;
                        case 2:
                            _l_address1 = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((ushort)1) << 1;
                            break;
                        case 3:
                            _l_address2 = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((ushort)1) << 2;
                            break;
                        case 4:
                            _l_city = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((ushort)1) << 3;
                            break;
                        case 5:
                            _l_state = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((ushort)1) << 4;
                            break;
                        case 6:
                            _l_postalcode = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((ushort)1) << 5;
                            break;
                        case 7:
                            _l_name = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((ushort)1) << 6;
                            break;
                        case 8:
                            _l_phonenumber = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((ushort)1) << 7;
                            break;
                        case 9:
                            _l_country = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((ushort)1) << 8;
                            break;
                    }
                }

                if (_r_assignedValid != 0b111111111)
                {
                    throw new SerdeSync::Serde.InvalidDeserializeValueException("Not all members were assigned");
                }

                var newType = new Benchmarks.Location()
                {
                    Id = _l_id,
                    Address1 = _l_address1,
                    Address2 = _l_address2,
                    City = _l_city,
                    State = _l_state,
                    PostalCode = _l_postalcode,
                    Name = _l_name,
                    PhoneNumber = _l_phonenumber,
                    Country = _l_country,
                };
                return newType;
            }
        }
    }
}