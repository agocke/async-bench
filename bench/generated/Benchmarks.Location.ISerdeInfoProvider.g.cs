
#nullable enable

namespace Benchmarks;

partial record Location
{
    private static global::Serde.ISerdeInfo s_serdeInfo = Serde.SerdeInfo.MakeCustom(
        "Location",
    typeof(Benchmarks.Location).GetCustomAttributesData(),
    new (string, global::Serde.ISerdeInfo, System.Reflection.MemberInfo?)[] {
        ("id", global::Serde.SerdeInfoProvider.GetSerializeInfo<int, global::Serde.I32Proxy>(), typeof(Benchmarks.Location).GetProperty("Id")),
        ("address1", global::Serde.SerdeInfoProvider.GetSerializeInfo<string, global::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address1")),
        ("address2", global::Serde.SerdeInfoProvider.GetSerializeInfo<string, global::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address2")),
        ("city", global::Serde.SerdeInfoProvider.GetSerializeInfo<string, global::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("City")),
        ("state", global::Serde.SerdeInfoProvider.GetSerializeInfo<string, global::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("State")),
        ("postalCode", global::Serde.SerdeInfoProvider.GetSerializeInfo<string, global::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PostalCode")),
        ("name", global::Serde.SerdeInfoProvider.GetSerializeInfo<string, global::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Name")),
        ("phoneNumber", global::Serde.SerdeInfoProvider.GetSerializeInfo<string, global::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PhoneNumber")),
        ("country", global::Serde.SerdeInfoProvider.GetSerializeInfo<string, global::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Country"))
    }
    );
}
