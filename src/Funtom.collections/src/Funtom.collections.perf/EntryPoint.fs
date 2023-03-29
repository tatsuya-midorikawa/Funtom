open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open Bogus
open System.Linq

let fake = Faker()

[<PlainExporter; MemoryDiagnoser>]
type Benchmark () =

  let mutable xs = Array.empty

  [<GlobalSetup>]
  member __.Setup() =
    xs <- [| for _ in 1..100_000_000 do fake.Random.Int(System.Int32.MinValue, System.Int32.MaxValue) |]

  [<Benchmark>]
  member __.Linq_max() = xs.Max()

  [<Benchmark>]
  member __.Array_max() = Array.max xs
    
  [<Benchmark>]
  member __.Funtom_Array_max_v2() = Funtom.collections.Array.max xs
  
#if BENCHMARK
[<EntryPoint>]
let main args =
  BenchmarkRunner.Run<Benchmark>() |> ignore
  0
#else
#if RELEASE
[<EntryPoint>]
let main args =
  let xs = [|for _ in 0..10 do fake.Random.Int(0, 100)|]
  let xs = [| 999 |] |> Array.append xs
  xs |> printfn "%A"
  
  xs
  |> Array.max
  |> printfn "FSharp Array.max= %d"
  xs
  |> Funtom.collections.Array.max
  |> printfn "Funtom Array.max= %d"
  xs
  |> Funtom.collections.Array.max_v2
  |> printfn "Funtom Array.max_v2= %d"
  
  0
#else
#nowarn "9"
type X = { mutable v: int }
[<EntryPoint>]
let main args =
  let xs = [| for _ in 1..100_000_000 do fake.Random.Int(System.Int32.MinValue, System.Int32.MaxValue) |]
  
  xs.Max()
  |> printfn "System.Linq.Max= %d"
  xs
  |> Array.max
  |> printfn "FSharp Array.max= %d"
  xs
  |> Funtom.collections.Array.max
  |> printfn "Funtom Array.max= %d"
  
  xs.Min()
  |> printfn "System.Linq.Min= %d"
  xs
  |> Array.min
  |> printfn "FSharp Array.min= %d"
  xs
  |> Funtom.collections.Array.min
  |> printfn "Funtom Array.min= %d"

  0
#endif
#endif