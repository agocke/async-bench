
namespace Benchmarks;

partial record Location : Serde.IDeserializeProvider<Benchmarks.Location>
{
    static global::Serde.IDeserialize<Benchmarks.Location> global::Serde.IDeserializeProvider<Benchmarks.Location>.Instance { get; }
        = new Benchmarks.Location._DeObj();
}
