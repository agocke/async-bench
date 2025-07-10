// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias SerdeSync;
extern alias SerdeCompasync;
extern alias SerdeTask;
extern alias SerdeValueTask;

using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    [GenericTypeArguments(typeof(Location))]
    public class DeserializeFromString<T>
        where T : SerdeSync::Serde.IDeserializeProvider<T>,
            SerdeTask::Serde.IDeserializeProvider<T>,
            SerdeValueTask::Serde.IDeserializeProvider<T>,
            SerdeCompasync::Serde.IDeserializeProvider<T>
    {
        private JsonSerializerOptions _options = null!;
        private string value = null!;

        [GlobalSetup]
        public void Setup()
        {
            _options = new JsonSerializerOptions()
            {
                IncludeFields = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            value = DataGenerator.GenerateDeserialize<T>();
        }

        [Benchmark]
        public T SerdeSync() => SerdeSync::Serde.Json.JsonSerializer.Deserialize<T>(value);

        [Benchmark]
        public T SerdeTask()
            => SerdeTask::Serde.Json.JsonSerializer.Deserialize<T>(value);

        [Benchmark]
        public T SerdeCompasync()
            => SerdeCompasync::Serde.Json.JsonSerializer.Deserialize<T>(value);

        //[Benchmark]
        //public T SerdeValueTask()
        //    => SerdeValueTask::Serde.Json.JsonSerializer.Deserialize<T>(value);
    }
}