
#nullable enable

using System;
using Serde;

namespace Serde.Test;

partial class JsonDeserializeTests
{
    partial record ThrowMissingFalse
    {
        sealed partial class _DeObj : Serde.IDeserialize<Serde.Test.JsonDeserializeTests.ThrowMissingFalse>
        {
            global::Serde.ISerdeInfo global::Serde.ISerdeInfoProvider.SerdeInfo => Serde.Test.JsonDeserializeTests.ThrowMissingFalse.s_serdeInfo;

            async global::System.Threading.Tasks.ValueTask<Serde.Test.JsonDeserializeTests.ThrowMissingFalse> Serde.IDeserialize<Serde.Test.JsonDeserializeTests.ThrowMissingFalse>.Deserialize(IDeserializer deserializer)
            {
                string _l_present = default!;
                bool _l_missing = default!;

                byte _r_assignedValid = 0;

                var _l_serdeInfo = global::Serde.SerdeInfoProvider.GetInfo(this);
                var typeDeserialize = deserializer.ReadType(_l_serdeInfo);
                int _l_index_;
                while ((_l_index_ = await typeDeserialize.TryReadIndex(_l_serdeInfo, out _)) != ITypeDeserializer.EndOfType)
                {
                    switch (_l_index_)
                    {
                        case 0:
                            _l_present = await typeDeserialize.ReadString(_l_serdeInfo, _l_index_);
                            _r_assignedValid |= ((byte)1) << 0;
                            break;
                        case 1:
                            _l_missing = await typeDeserialize.ReadBool(_l_serdeInfo, _l_index_);
                            _r_assignedValid |= ((byte)1) << 1;
                            break;
                        case Serde.ITypeDeserializer.IndexNotFound:
                            await typeDeserialize.SkipValue(_l_serdeInfo, _l_index_);
                            break;
                        default:
                            throw new InvalidOperationException("Unexpected index: " + _l_index_);
                    }
                }
                if ((_r_assignedValid & 0b1) != 0b1)
                {
                    throw Serde.DeserializeException.UnassignedMember();
                }
                var newType = new Serde.Test.JsonDeserializeTests.ThrowMissingFalse() {
                    Present = _l_present,
                    Missing = _l_missing,
                };

                return newType;
            }
        }
    }
}
