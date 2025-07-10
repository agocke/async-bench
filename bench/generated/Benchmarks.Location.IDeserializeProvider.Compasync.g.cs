extern alias SerdeCompasync;
using SerdeCompasync::Serde;
namespace Benchmarks;

partial record Location : SerdeCompasync::Serde.IDeserializeProvider<Benchmarks.Location>
{
    static SerdeCompasync::Serde.IDeserialize<Benchmarks.Location> SerdeCompasync::Serde.IDeserializeProvider<Benchmarks.Location>.Instance { get; }
        = new Benchmarks.Location._DeObj();
}
