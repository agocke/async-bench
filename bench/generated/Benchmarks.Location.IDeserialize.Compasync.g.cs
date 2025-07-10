extern alias SerdeCompasync;
#nullable enable

using System;
using System.Runtime.CompilerServices;
using SerdeCompasync::Serde;

namespace Benchmarks;

partial record Location
{
    sealed partial class _DeObj : SerdeCompasync::Serde.IDeserialize<Benchmarks.Location>
    {
        SerdeCompasync::Serde.ISerdeInfo SerdeCompasync::Serde.ISerdeInfoProvider.SerdeInfo => Benchmarks.Location.s_serdeInfo;

        [RuntimeAsyncMethodGenerationAttribute(false)]
        async global::System.Threading.Tasks.Task<Benchmarks.Location> SerdeCompasync::Serde.IDeserialize<Benchmarks.Location>.Deserialize(IDeserializer deserializer)
        {
            int _l_id = default!;
            string _l_address1 = default!;
            string _l_address2 = default!;
            string _l_city = default!;
            string _l_state = default!;
            string _l_postalcode = default!;
            string _l_name = default!;
            string _l_phonenumber = default!;
            string _l_country = default!;

            ushort _r_assignedValid = 0;

            var _l_serdeInfo = SerdeCompasync::Serde.SerdeInfoProvider.GetInfo(this);
            var typeDeserialize = deserializer.ReadType(_l_serdeInfo);
            while (true)
            {
                var (_l_index_, _) = await typeDeserialize.TryReadIndexWithName(_l_serdeInfo);
                if (_l_index_ == SerdeCompasync::Serde.ITypeDeserializer.EndOfType)
                {
                    break;
                }

                switch (_l_index_)
                {
                    case 0:
                        _l_id = await typeDeserialize.ReadI32(_l_serdeInfo, _l_index_);
                        _r_assignedValid |= ((ushort)1) << 0;
                        break;
                    case 1:
                        _l_address1 = await typeDeserialize.ReadString(_l_serdeInfo, _l_index_);
                        _r_assignedValid |= ((ushort)1) << 1;
                        break;
                    case 2:
                        _l_address2 = await typeDeserialize.ReadString(_l_serdeInfo, _l_index_);
                        _r_assignedValid |= ((ushort)1) << 2;
                        break;
                    case 3:
                        _l_city = await typeDeserialize.ReadString(_l_serdeInfo, _l_index_);
                        _r_assignedValid |= ((ushort)1) << 3;
                        break;
                    case 4:
                        _l_state = await typeDeserialize.ReadString(_l_serdeInfo, _l_index_);
                        _r_assignedValid |= ((ushort)1) << 4;
                        break;
                    case 5:
                        _l_postalcode = await typeDeserialize.ReadString(_l_serdeInfo, _l_index_);
                        _r_assignedValid |= ((ushort)1) << 5;
                        break;
                    case 6:
                        _l_name = await typeDeserialize.ReadString(_l_serdeInfo, _l_index_);
                        _r_assignedValid |= ((ushort)1) << 6;
                        break;
                    case 7:
                        _l_phonenumber = await typeDeserialize.ReadString(_l_serdeInfo, _l_index_);
                        _r_assignedValid |= ((ushort)1) << 7;
                        break;
                    case 8:
                        _l_country = await typeDeserialize.ReadString(_l_serdeInfo, _l_index_);
                        _r_assignedValid |= ((ushort)1) << 8;
                        break;
                    case SerdeCompasync::Serde.ITypeDeserializer.IndexNotFound:
                        await typeDeserialize.SkipValue(_l_serdeInfo, _l_index_);
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected index: " + _l_index_);
                }
            }
            if ((_r_assignedValid & 0b111111111) != 0b111111111)
            {
                throw SerdeCompasync::Serde.DeserializeException.UnassignedMember();
            }
            var newType = new Benchmarks.Location() {
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
