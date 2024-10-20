namespace Funtom.span.perf

open Funtom.span
open BenchmarkDotNet.Attributes

type Benchmark() =
  let xs = [| 0..100_0000 |]
  
  [<Benchmark>]
  member __.quick_check_array () =
    let mutable i = 0
    for _ in xs do
      i <- i + 1
    i

  [<Benchmark>]
  member __.quick_check_forin () =
    let span =  System.Span<int> xs
    let mutable acc = 0
    for _ in span do
      acc <- acc + 1
    acc
  
  [<Benchmark>]
  member __.quick_check_forto () =
    let span =  System.Span<int> xs
    let mutable acc = 0
    for i = 0 to span.Length - 1 do
      acc <- acc + 1
    acc
  
  [<Benchmark>]
  member __.quick_check_span_iter() =
    let span =  System.Span<int> xs
    let mutable acc = 0
    span.iter (fun _ -> acc <- acc + 1)
    acc
  
  [<Benchmark>]
  member __.quick_check_span_map() =
    let span =  System.Span<int> xs
    let r = span.map (fun i -> i % 2)
    System.Diagnostics.Debug.WriteLine(r)
    r
    
