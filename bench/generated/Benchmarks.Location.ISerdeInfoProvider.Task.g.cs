extern alias SerdeTask;

#nullable enable

namespace Benchmarks;

partial record Location
{
    private static SerdeTask::Serde.ISerdeInfo s_taskSerdeInfo = SerdeTask::Serde.SerdeInfo.MakeCustom(
        "Location",
    typeof(Benchmarks.Location).GetCustomAttributesData(),
    new (string, SerdeTask::Serde.ISerdeInfo, System.Reflection.MemberInfo?)[] {
        ("id", SerdeTask::Serde.SerdeInfoProvider.GetSerializeInfo<int, SerdeTask::Serde.I32Proxy>(), typeof(Benchmarks.Location).GetProperty("Id")),
        ("address1", SerdeTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address1")),
        ("address2", SerdeTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Address2")),
        ("city", SerdeTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("City")),
        ("state", SerdeTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("State")),
        ("postalCode", SerdeTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PostalCode")),
        ("name", SerdeTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Name")),
        ("phoneNumber", SerdeTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("PhoneNumber")),
        ("country", SerdeTask::Serde.SerdeInfoProvider.GetSerializeInfo<string, SerdeTask::Serde.StringProxy>(), typeof(Benchmarks.Location).GetProperty("Country"))
    }
    );
}
