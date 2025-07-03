extern alias SerdeSync;
#nullable enable

using System;
using SerdeSync::Serde;

namespace Benchmarks;

partial record Location
{
    sealed partial class _SerObj : SerdeSync::Serde.ISerialize<Benchmarks.Location>
    {
        SerdeSync::Serde.ISerdeInfo SerdeSync::Serde.ISerdeInfoProvider.SerdeInfo => Benchmarks.Location.s_syncSerdeInfo;

        void SerdeSync::Serde.ISerialize<Benchmarks.Location>.Serialize(Benchmarks.Location value, SerdeSync::Serde.ISerializer serializer)
        {
            var _l_info = SerdeSync::Serde.SerdeInfoProvider.GetInfo(this);
            var _l_type = serializer.WriteType(_l_info);
            _l_type.WriteI32(_l_info, 0, value.Id);
            _l_type.WriteString(_l_info, 1, value.Address1);
            _l_type.WriteString(_l_info, 2, value.Address2);
            _l_type.WriteString(_l_info, 3, value.City);
            _l_type.WriteString(_l_info, 4, value.State);
            _l_type.WriteString(_l_info, 5, value.PostalCode);
            _l_type.WriteString(_l_info, 6, value.Name);
            _l_type.WriteString(_l_info, 7, value.PhoneNumber);
            _l_type.WriteString(_l_info, 8, value.Country);
            _l_type.End(_l_info);
        }

    }
}
