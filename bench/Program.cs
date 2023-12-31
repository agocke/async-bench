﻿extern alias SerdeSync;
extern alias SerdeAsync;
using System;
using System.Diagnostics;
using System.Text.Json;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using Benchmarks;

#if DEBUG

const string LocationSample = DataGenerator.LocationSample;
var options = new JsonSerializerOptions()
{
    IncludeFields = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};
var json1 = System.Text.Json.JsonSerializer.Serialize(DataGenerator.CreateLocation(), options);
var json2 = SerdeSync::Serde.Json.JsonSerializer.Serialize(DataGenerator.CreateLocation());
var loc1 = System.Text.Json.JsonSerializer.Deserialize<Location>(LocationSample, options);
var loc2 = SerdeSync::Serde.Json.JsonSerializer.Deserialize<Location>(LocationSample);
var locAsync = await SerdeAsync::Serde.Json.JsonSerializer.DeserializeAsync<Location>(LocationSample);

Console.WriteLine(loc1);
Console.WriteLine(locAsync);
Console.WriteLine(loc1 == loc2);
Console.WriteLine(loc1 == locAsync);

#else

BenchmarkRunner.Run<DeserializeFromString<Location>>();
//var config = DefaultConfig.Instance.AddDiagnoser(MemoryDiagnoser.Default);
//var summary = BenchmarkSwitcher.FromAssembly(typeof(DeserializeFromString<>).Assembly)
//    .Run(args, config);

#endif