
#nullable enable
extern alias SerdeSync;
using System;
using SerdeSync::Serde;

namespace Benchmarks
{
    partial record Location : ISerialize
    {
        void ISerialize.Serialize(ISerializer serializer)
        {
            var type = serializer.SerializeType("Location", 9);
            type.SerializeField("id"u8, new Int32Wrap(this.Id));
            type.SerializeField("address1"u8, new StringWrap(this.Address1));
            type.SerializeField("address2"u8, new StringWrap(this.Address2));
            type.SerializeField("city"u8, new StringWrap(this.City));
            type.SerializeField("state"u8, new StringWrap(this.State));
            type.SerializeField("postalCode"u8, new StringWrap(this.PostalCode));
            type.SerializeField("name"u8, new StringWrap(this.Name));
            type.SerializeField("phoneNumber"u8, new StringWrap(this.PhoneNumber));
            type.SerializeField("country"u8, new StringWrap(this.Country));
            type.End();
        }
    }
}