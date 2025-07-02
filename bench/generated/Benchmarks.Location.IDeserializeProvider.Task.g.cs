extern alias SerdeTask;

namespace Benchmarks;

partial record Location : SerdeTask::Serde.IDeserializeProvider<Benchmarks.Location>
{
    static SerdeTask::Serde.IDeserialize<Benchmarks.Location> SerdeTask::Serde.IDeserializeProvider<Benchmarks.Location>.Instance { get; }
        = new Benchmarks.Location._DeObj();
}
