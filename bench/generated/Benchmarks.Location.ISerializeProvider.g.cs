
namespace Benchmarks;

partial record Location : Serde.ISerializeProvider<Benchmarks.Location>
{
    static global::Serde.ISerialize<Benchmarks.Location> global::Serde.ISerializeProvider<Benchmarks.Location>.Instance { get; }
        = new Benchmarks.Location._SerObj();
}
