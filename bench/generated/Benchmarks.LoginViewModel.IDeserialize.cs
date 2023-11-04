
#nullable enable
extern alias SerdeSync;
using System;
using SerdeSync::Serde;
using Serde = SerdeSync::Serde;

namespace Benchmarks
{
    partial class LoginViewModel : IDeserialize<Benchmarks.LoginViewModel>
    {
        static Benchmarks.LoginViewModel IDeserialize<Benchmarks.LoginViewModel>.Deserialize<D>(ref D deserializer)
        {
            var visitor = new SerdeVisitor();
            var fieldNames = new[]
            {
                "Email",
                "Password",
                "RememberMe"
            };
            return deserializer.DeserializeType<Benchmarks.LoginViewModel, SerdeVisitor>("LoginViewModel", fieldNames, visitor);
        }

        private sealed class SerdeVisitor : IDeserializeVisitor<Benchmarks.LoginViewModel>
        {
            public string ExpectedTypeName => "Benchmarks.LoginViewModel";

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
                        case (byte)'e'when s.SequenceEqual("email"u8):
                            return 1;
                        case (byte)'p'when s.SequenceEqual("password"u8):
                            return 2;
                        case (byte)'r'when s.SequenceEqual("rememberMe"u8):
                            return 3;
                        default:
                            return 0;
                    }
                }
            }

            Benchmarks.LoginViewModel IDeserializeVisitor<Benchmarks.LoginViewModel>.VisitDictionary<D>(ref D d)
            {
                string _l_email = default !;
                string _l_password = default !;
                bool _l_rememberme = default !;
                byte _r_assignedValid = 0b0;
                while (d.TryGetNextKey<byte, FieldNameVisitor>(out byte key))
                {
                    switch (key)
                    {
                        case 1:
                            _l_email = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((byte)1) << 0;
                            break;
                        case 2:
                            _l_password = d.GetNextValue<string, StringWrap>();
                            _r_assignedValid |= ((byte)1) << 1;
                            break;
                        case 3:
                            _l_rememberme = d.GetNextValue<bool, BoolWrap>();
                            _r_assignedValid |= ((byte)1) << 2;
                            break;
                    }
                }

                if (_r_assignedValid != 0b111)
                {
                    throw new Serde.InvalidDeserializeValueException("Not all members were assigned");
                }

                var newType = new Benchmarks.LoginViewModel()
                {
                    Email = _l_email,
                    Password = _l_password,
                    RememberMe = _l_rememberme,
                };
                return newType;
            }
        }
    }
}