namespace Funtom.span

open System

[<AutoOpen>]
module SpanExtensions =
  type ReadOnlySpan<'T> with
    member inline __.GetSlice(start, finish) =
      let s = match start with None -> 0 | Some s -> max 0 s
      match finish with None -> __.Slice s | Some e -> __.Slice (s, min __.Length (e - s))
      
    member inline __.iter ([<InlineIfLambdaAttribute>] fn: 'T -> unit) =
      for i = 0 to __.Length - 1 do
        fn __.[i]

  type Span<'T> with
    member inline __.GetSlice(start, finish) =
      let s = match start with None -> 0 | Some s -> max 0 s
      match finish with None -> __.Slice s | Some e -> __.Slice (s, min __.Length (e - s))

    member inline __.iter ([<InlineIfLambdaAttribute>] fn: 'T -> unit) =
      for i = 0 to __.Length - 1 do
        fn __.[i]
