extern alias SerdeCompasync;
#nullable enable
using SerdeCompasync::Serde;

namespace Benchmarks;

partial record Location
{
    private static SerdeCompasync::Serde.ISerdeInfo s_serdeInfo = SerdeCompasync::Serde.SerdeInfo.MakeCustom(
        "Location",
    typeof(Benchmarks.Location).GetCustomAttributesData(),
    new (string, SerdeCompasync::Serde.ISerdeInfo, System.Reflection.MemberInfo?)[] {
        ("id", SerdeCompasync::Serde.SerdeInfoProvider.GetSerializeInfo<int, SerdeCompasync::Serde.I32Proxy>(), typeof(Benchmarks.Location).GetProperty("Id")),
        ("address1", SerdeCompasync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeCompasync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address1")),
        ("address2", SerdeCompasync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeCompasync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address2")),
        ("city", SerdeCompasync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeCompasync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("City")),
        ("state", SerdeCompasync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeCompasync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("State")),
        ("postalCode", SerdeCompasync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeCompasync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PostalCode")),
        ("name", SerdeCompasync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeCompasync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Name")),
        ("phoneNumber", SerdeCompasync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeCompasync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PhoneNumber")),
        ("country", SerdeCompasync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeCompasync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Country"))
    }
    );
}
