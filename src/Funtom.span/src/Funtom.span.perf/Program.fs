open BenchmarkDotNet
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Jobs
open BenchmarkDotNet.Running

open Funtom.span.perf

[<EntryPoint>]
let main args =
  let config =
    let c = ManualConfig()
    let rough = AccuracyMode(MaxRelativeError = 0.1)
    let quickRoughJob = Job("QuickRough", rough, RunMode.Short)
    c.AddJob(quickRoughJob) |> ignore

    ManualConfig.Union(DefaultConfig.Instance, c)

#if QUICK
  BenchmarkRunner.Run<Benchmark>(config) |> ignore
#else
  BenchmarkRunner.Run<Benchmark>() |> ignore
#endif

  0