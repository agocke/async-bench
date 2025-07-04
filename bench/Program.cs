﻿extern alias SerdeSync;
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

Console.WriteLine(loc1 == loc2);

#else

//BenchmarkRunner.Run<DeserializeFromString<Location>>();
var config = DefaultConfig.Instance.AddDiagnoser(MemoryDiagnoser.Default);
BenchmarkRunner.Run<DeserializeFromString<Location>>(config);
//var summary = BenchmarkSwitcher.FromAssembly(typeof(DeserializeFromString<>).Assembly)
//    .Run(args, config);

#endif