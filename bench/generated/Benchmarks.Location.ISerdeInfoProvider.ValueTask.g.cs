extern alias SerdeValueTask;

#nullable enable

namespace Benchmarks;

partial record Location
{
    private static SerdeValueTask::Serde.ISerdeInfo s_valueTaskSerdeInfo = SerdeValueTask::Serde.SerdeInfo.MakeCustom(
        "Location",
    typeof(Benchmarks.Location).GetCustomAttributesData(),
    new (string, SerdeValueTask::Serde.ISerdeInfo, System.Reflection.MemberInfo?)[] {
        ("id", SerdeValueTask::Serde.SerdeInfoProvider.GetSerializeInfo<int, SerdeValueTask::Serde.I32Proxy>(), typeof(Benchmarks.Location).GetProperty("Id")),
        ("address1", SerdeValueTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeValueTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address1")),
        ("address2", SerdeValueTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeValueTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address2")),
        ("city", SerdeValueTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeValueTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("City")),
        ("state", SerdeValueTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeValueTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("State")),
        ("postalCode", SerdeValueTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeValueTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PostalCode")),
        ("name", SerdeValueTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeValueTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Name")),
        ("phoneNumber", SerdeValueTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeValueTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PhoneNumber")),
        ("country", SerdeValueTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeValueTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Country"))
    }
    );
}
