
#nullable enable
extern alias SerdeSync;
extern alias SerdeAsync;
using System;
using StringWrap = SerdeSync::Serde.StringWrap;
using BoolWrap = SerdeSync::Serde.BoolWrap;

namespace Benchmarks
{
    partial class LoginViewModel : SerdeSync::Serde.ISerialize
    {
        void SerdeSync::Serde.ISerialize.Serialize(SerdeSync::Serde.ISerializer serializer)
        {
            var type = serializer.SerializeType("LoginViewModel", 3);
            type.SerializeField("email"u8, new StringWrap(this.Email));
            type.SerializeField("password"u8, new StringWrap(this.Password));
            type.SerializeField("rememberMe"u8, new BoolWrap(this.RememberMe));
            type.End();
        }
    }
}