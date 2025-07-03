extern alias SerdeSync;

#nullable enable

namespace Benchmarks;

partial record Location
{
    private static SerdeSync::Serde.ISerdeInfo s_syncSerdeInfo = SerdeSync::Serde.SerdeInfo.MakeCustom(
        "Location",
    typeof(Benchmarks.Location).GetCustomAttributesData(),
    new (string, SerdeSync::Serde.ISerdeInfo, System.Reflection.MemberInfo?)[] {
        ("id", SerdeSync::Serde.SerdeInfoProvider.GetSerializeInfo<int, SerdeSync::Serde.I32Proxy>(), typeof(Benchmarks.Location).GetProperty("Id")),
        ("address1", SerdeSync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeSync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address1")),
        ("address2", SerdeSync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeSync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address2")),
        ("city", SerdeSync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeSync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("City")),
        ("state", SerdeSync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeSync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("State")),
        ("postalCode", SerdeSync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeSync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PostalCode")),
        ("name", SerdeSync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeSync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Name")),
        ("phoneNumber", SerdeSync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeSync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PhoneNumber")),
        ("country", SerdeSync::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeSync::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Country"))
    }
    );
}
