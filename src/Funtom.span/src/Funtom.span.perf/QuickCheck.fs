namespace Funtom.span.perf

open System
open System.Linq
open Funtom.span
open BenchmarkDotNet.Attributes

type Benchmark() =
  let xs = [| 0..100_0000 |]
  
  [<Benchmark>]
  member __.quick_check_linq () =
    let ys = xs
                      .Where(fun x -> x % 2 = 0)
                      .Select(fun x -> x * 2)
    let mutable count = 0
    for _ in ys do
      count <- count + 1
    count
  
  [<Benchmark>]
  member __.quick_check_funtom () =
    let mutable count = 0
    ReadOnlySpan<_>(xs)
      .filter(fun x -> x % 2 = 0)
      .map(fun x -> x * 2)
      .iter(fun _ -> count <- count + 1)
    count
  
  //[<Benchmark>]
  //member __.quick_check_array () =
  //  let mutable i = 0
  //  for _ in xs do
  //    i <- i + 1
  //  i

  //[<Benchmark>]
  //member __.quick_check_forin () =
  //  let span =  System.Span<int> xs
  //  let mutable acc = 0
  //  for _ in span do
  //    acc <- acc + 1
  //  acc
  
  //[<Benchmark>]
  //member __.quick_check_forto () =
  //  let span =  System.Span<int> xs
  //  let mutable acc = 0
  //  for i = 0 to span.Length - 1 do
  //    acc <- acc + 1
  //  acc
  
  //[<Benchmark>]
  //member __.quick_check_span_iter() =
  //  let span =  System.Span<int> xs
  //  let mutable acc = 0
  //  span.iter (fun _ -> acc <- acc + 1)
  //  acc
  
  //[<Benchmark>]
  //member __.quick_check_span_map() =
  //  let span =  System.Span<int> xs
  //  let r = span.map (fun i -> i % 2)
  //  r
    
