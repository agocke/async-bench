extern alias SerdeValueTask;

namespace Benchmarks;

partial record Location : SerdeValueTask::Serde.IDeserializeProvider<Benchmarks.Location>
{
    static SerdeValueTask::Serde.IDeserialize<Benchmarks.Location> SerdeValueTask::Serde.IDeserializeProvider<Benchmarks.Location>.Instance { get; }
        = new Benchmarks.Location._DeObj();
}
