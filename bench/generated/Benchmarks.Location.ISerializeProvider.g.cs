extern alias SerdeSync;

namespace Benchmarks;

partial record Location : SerdeSync::Serde.ISerializeProvider<Benchmarks.Location>
{
    static SerdeSync::Serde.ISerialize<Benchmarks.Location> SerdeSync::Serde.ISerializeProvider<Benchmarks.Location>.Instance { get; }
        = new Benchmarks.Location._SerObj();
}
