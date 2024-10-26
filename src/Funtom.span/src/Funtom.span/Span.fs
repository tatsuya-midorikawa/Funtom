namespace Funtom.span

open System
open System.Runtime.CompilerServices

[<Struct; IsReadOnly; IsByRefLike>]
type VolatileArray<'T> = { raw: array<'T>; length: int }
with
  static member inline create (size: int) =
    { raw = System.Buffers.ArrayPool<'T>.Shared.Rent size; length = size }
  override __.Finalize () =
    System.Buffers.ArrayPool<'T>.Shared.Return __.raw
  member inline __.asSpan() = __.raw.AsSpan(0, __.length)
  member inline __.asSpan(start: int, length: int) = __.raw.AsSpan(start, length)
  member inline __.asReadOnlySpan() = ReadOnlySpan<'T> (__.raw, 0, __.length)
  member inline __.asReadOnlySpan(start: int, length: int) = ReadOnlySpan<'T>(__.raw, start, length)

[<AutoOpen>]
module SpanExtensions =
  type ReadOnlySpan<'T> with
    member inline __.GetSlice(start, finish) =
      let s = match start with None -> 0 | Some s -> max 0 s
      match finish with None -> __.Slice s | Some e -> __.Slice (s, min __.Length (e - s))
      
    member inline __.iter ([<InlineIfLambdaAttribute>] fn: 'T -> unit) =
      for i = 0 to __.Length - 1 do
        fn __.[i]

    member inline __.map ([<InlineIfLambdaAttribute>] fn: 'T -> 'U) =
      let acc = VolatileArray<'U>.create __.Length
      for i = 0 to __.Length - 1 do
        acc.raw[i] <- fn __.[i]
      acc.asReadOnlySpan()

    member inline __.filter ([<InlineIfLambdaAttribute>] fn: 'T -> bool) =
      let acc = VolatileArray<'T>.create __.Length
      let mutable j = 0
      for i = 0 to __.Length - 1 do
        if fn __.[i] then
          acc.raw.[j] <- __.[i]
          j <- j + 1
      acc.asReadOnlySpan(0, j)

    member inline __.toArray () = __.ToArray()

  type Span<'T> with
    member inline __.GetSlice(start, finish) =
      let s = match start with None -> 0 | Some s -> max 0 s
      match finish with None -> __.Slice s | Some e -> __.Slice (s, min __.Length (e - s))

    member inline __.iter ([<InlineIfLambdaAttribute>] fn: 'T -> unit) =
      for i = 0 to __.Length - 1 do
        fn __.[i]

    member inline __.map ([<InlineIfLambdaAttribute>] fn: 'T -> 'U) =
      let acc = VolatileArray<'U>.create __.Length
      for i = 0 to __.Length - 1 do
        acc.raw[i] <- fn __.[i]
      acc.asSpan()

    member inline __.filter ([<InlineIfLambdaAttribute>] fn: 'T -> bool) =
      let acc = VolatileArray<'T>.create __.Length
      let mutable j = 0
      for i = 0 to __.Length - 1 do
        if fn __.[i] then
          acc.raw.[j] <- __.[i]
          j <- j + 1
      acc.asSpan(0, j)
