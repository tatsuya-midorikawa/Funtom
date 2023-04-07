open BenchmarkDotNet
open BenchmarkDotNet.Extensions
open BenchmarkDotNet.Exporters
open BenchmarkDotNet.Diagnosers
open BenchmarkDotNet.Configs
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open Bogus
open System.Linq
open System.Runtime.Intrinsics

let fake = Faker()

//[<PlainExporter; MemoryDiagnoser;>]
//[<DisassemblyDiagnoser(maxDepth= 9, syntax= DisassemblySyntax.Intel, printSource= true, printInstructionAddresses= true, exportGithubMarkdown= true, exportHtml= true, exportCombinedDisassemblyReport= true, exportDiff= true)>]
type Benchmark () =

  let mutable xs = Array.empty
  let mutable ys = Array.empty
  let mutable zs = Array.empty

  [<GlobalSetup>]
  member __.Setup() =
    xs <- [| for _ in 1..100_000_000 do fake.Random.Int(System.Int32.MinValue, System.Int32.MaxValue) |]
    ys <- [| for _ in 1..100_000_000 do fake.Random.Int(-20, 20) |]
    zs <- [| for _ in 1..100_000_000 do fake.Random.Double(-20, 20) |]

  [<Benchmark>]
  member __.Linq_max() = xs.Max()

  [<Benchmark>]
  member __.Fsharp_Array_max() = Array.max xs
    
  [<Benchmark>]
  member __.Funtom_Array_max() = Funtom.collections.Array.max xs

  // ---------------------------------

  [<Benchmark>]
  member __.Linq_min() = xs.Min()

  [<Benchmark>]
  member __.Fsharp_Array_min() = Array.min xs
    
  [<Benchmark>]
  member __.Funtom_Array_min() = Funtom.collections.Array.min xs
  
  // ---------------------------------

  [<Benchmark>]
  member __.Linq_sum() = ys.Max()

  [<Benchmark>]
  member __.Fsharp_Array_sum() = Array.max ys
    
  [<Benchmark>]
  member __.Funtom_Array_sum() = Funtom.collections.Array.max ys
  
  // ---------------------------------

  [<Benchmark>]
  member __.Linq_average() = zs.Average()

  [<Benchmark>]
  member __.Fsharp_Array_average() = Array.average zs
    
  [<Benchmark>]
  member __.Funtom_Array_average() = Funtom.collections.Array.average zs

  // ---------------------------------

  [<Benchmark>]
  member __.Linq_contains() = xs.Contains(255)

  [<Benchmark>]
  member __.Fsharp_Array_contains() = xs |> Array.contains 255
  
  [<Benchmark>]
  member __.Funtom_Array_contains() = xs |> Funtom.collections.Array.contains 255
  
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
  |> Funtom.collections.Array.max
  |> printfn "Funtom Array.max_v2= %d"
  
  0
#else
#nowarn "9"
open Microsoft.FSharp.NativeInterop
type Unsafe = System.Runtime.CompilerServices.Unsafe
[<EntryPoint>]
let main args =
  let xs = [| for _ in 1..100_000_000 do fake.Random.Int(System.Int32.MinValue, System.Int32.MaxValue) |]
  let ys = [| for _ in 1..100_000_000 do fake.Random.Double(0.0, System.Double.MaxValue) |]
  
  //let xs' = [| for _ in 1..96 do fake.Random.Int(0, 100) |]
  //let ys' = [| for _ in 1..96 do fake.Random.Double(0.0, 100.) |]
  let xs' = [| for _ in 1..100 do fake.Random.Int(0, 100) |]
  let ys' = [| for _ in 1..100 do fake.Random.Double(0.0, 100.) |]
  
  let xs'' = [| for _ in 1..1_000_000 do fake.Random.Int(0, System.Int32.MaxValue) |]

  //let size = sizeof<int>
  //size |> printfn "size<int>= %d"
  //let count = System.Runtime.Intrinsics.Vector256<int>.Count
  //count |> printfn "Vector256<int>.Count= %d"

  //let size = sizeof<double>
  //size |> printfn "size<double>= %d"
  //let count = System.Runtime.Intrinsics.Vector256<double>.Count
  //count |> printfn "Vector256<double>.Count= %d"

  //xs.Max()
  //|> printfn "System.Linq.Max= %d"
  //xs
  //|> Array.max
  //|> printfn "FSharp Array.max= %d"
  //xs
  //|> Funtom.collections.Array.max
  //|> printfn "Funtom Array.max= %d"
  
  //printfn "----------"

  //ys.Max()
  //|> printfn "System.Linq.Max    = %f"
  //ys
  //|> Array.max
  //|> printfn "FSharp Array.max   = %f"
  //ys
  //|> Funtom.collections.Array.max
  //|> printfn "Funtom Array.max   = %f"

  //printfn "----------"

  //xs.Min()
  //|> printfn "System.Linq.Min= %d"
  //xs
  //|> Array.min
  //|> printfn "FSharp Array.min= %d"
  //xs
  //|> Funtom.collections.Array.min
  //|> printfn "Funtom Array.min= %d"
  
  //printfn "----------"

  //ys.Min()
  //|> printfn "System.Linq.Min = %f"
  //ys
  //|> Array.min
  //|> printfn "FSharp Array.min= %f"
  //ys
  //|> Funtom.collections.Array.min
  //|> printfn "Funtom Array.min= %f"
  
  //printfn "----------"
  
  //xs'.Sum()
  //|> printfn "System.Linq.Sum= %d"
  //xs'
  //|> Array.sum
  //|> printfn "FSharp Array.sum= %d"
  //xs'
  //|> Funtom.collections.Array.sum
  //|> printfn "Funtom Array.sum= %d"
  
  //printfn "----------"

  //ys'.Sum()
  //|> printfn "System.Linq.Sum= %f"
  //ys'
  //|> Array.sum
  //|> printfn "FSharp Array.sum= %f"
  //ys'
  //|> Funtom.collections.Array.sum
  //|> printfn "Funtom Array.sum= %f"
  
  //printfn "----------"
  
  ////xs''.Sum()
  ////|> printfn "System.Linq.Sum= %d"
  //xs''
  //|> Array.sum
  //|> printfn "FSharp Array.sum= %d"
  //xs''
  //|> Funtom.collections.Array.sum
  //|> printfn "Funtom Array.sum= %d"
  
  //printfn "----------"
  
  //xs'.Average()
  //|> printfn "System.Linq.Average= %f"
  ////xs'
  ////|> Array.average
  ////|> printfn "FSharp Array.sum= %f"
  //xs'
  //|> Funtom.collections.Array.average
  //|> printfn "Funtom Array.average= %f"
  
  //printfn "----------"

  //ys'.Average()
  //|> printfn "System.Linq.Average= %f"
  //ys'
  //|> Array.average
  //|> printfn "FSharp Array.average= %f"
  //ys'
  //|> Funtom.collections.Array.average
  //|> printfn "Funtom Array.average= %f"
  
  //printfn "----------"

  [| 0..101 |].Contains(102) |> printfn "System.Linq.Contains= %b"
  [| 0..101 |] |> Array.contains(102) |> printfn "FSharp Array.contains= %b"
  [| 0..101 |] |> Funtom.collections.Array.contains(102) |> printfn "Funtom Array.contains= %b"

  printfn "----------"

  [| 0.0..0.2..100 |] 
  |> Array.iter (printf "%f, ")
  
  printfn "----------"

  [| 0.0..0.2..100.0 |].Contains(99.800000) |> printfn "System.Linq.Contains (99.800000)= %b"
  [| 0.0..0.2..100.0 |] |> Array.contains(99.800000) |> printfn "FSharp Array.contains (99.800000)= %b"
  [| 0.0..0.2..100.0 |] |> Funtom.collections.Array.contains(99.800000) |> printfn "Funtom Array.contains (99.800000)= %b"

  [| 0.0..0.2..100.0 |].Contains(100) |> printfn "System.Linq.Contains (100)= %b"
  [| 0.0..0.2..100.0 |] |> Array.contains(100) |> printfn "FSharp Array.contains (100)= %b"
  [| 0.0..0.2..100.0 |] |> Funtom.collections.Array.contains(100) |> printfn "Funtom Array.contains (100)= %b"

  0

#endif
#endif
