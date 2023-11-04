// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias SerdeSync;
extern alias SerdeAsync;

using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace Benchmarks
{
    //[GenericTypeArguments(typeof(LoginViewModel))]
    [GenericTypeArguments(typeof(Location))]
    public class DeserializeFromString<T>
        where T : SerdeSync::Serde.IDeserialize<T>, SerdeAsync::Serde.IDeserialize<T>
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
        public T? SystemText()
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(value, _options);
        }

        [Benchmark]
        public T SerdeJson() => SerdeSync::Serde.Json.JsonSerializer.Deserialize<T>(value);

        [Benchmark]
        public T SerdeJsonAsync()
            => SerdeAsync::Serde.Json.JsonSerializer.DeserializeAsync<T>(value).Result;
    }
}