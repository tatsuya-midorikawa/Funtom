module Tests

open System
open System.Collections.Generic
open Xunit
open Xunit
open Xunit.Abstractions
open Funtom.span

type ``SpanExtension Test`` (Console: ITestOutputHelper) =
  [<Fact>]
  let ``ReadOnlySpan Test`` () =
    let xs = ReadOnlySpan<int> [| 0..10 |]
    let ys = xs[0..5]
    let actual = ys.ToArray() :> IEnumerable<int>
    Assert.Equal([| 0..4 |], actual)

  [<Fact>]
  let ``ReadOnlySpan.map Test`` () =
    let xs = ReadOnlySpan<int> [| 0..10 |]
    xs
      .map(fun x -> x * 2)
      .iter(fun x -> Console.WriteLine($"{x}"))

    Assert.True

  [<Fact>]
  let ``ReadOnlySpan.filter Test`` () =
    ReadOnlySpan<int>([| 0..10 |])
      .filter(fun x -> x % 2 = 0)
      .map(fun x -> x * 2)
      .iter(fun x -> Console.WriteLine($"{x}"))

    Assert.True
