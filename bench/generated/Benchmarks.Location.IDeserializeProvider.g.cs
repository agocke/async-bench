extern alias SerdeSync;

namespace Benchmarks;

partial record Location : SerdeSync::Serde.IDeserializeProvider<Benchmarks.Location>
{
    static SerdeSync::Serde.IDeserialize<Benchmarks.Location> SerdeSync::Serde.IDeserializeProvider<Benchmarks.Location>.Instance { get; }
        = new Benchmarks.Location._DeObj();
}
