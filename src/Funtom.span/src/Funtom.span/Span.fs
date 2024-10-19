namespace Funtom.span

open System

[<AutoOpen>]
module SpanExtensions =
  type ReadOnlySpan<'T> with
    member inline __.GetSlice(start, finish) =
      let s = match start with None -> 0 | Some s -> max 0 s
      match finish with None -> __.Slice s | Some e -> __.Slice (s, min __.Length (e - s))

  type Span<'T> with
    member inline __.GetSlice(start, finish) =
      let s = match start with None -> 0 | Some s -> max 0 s
      match finish with None -> __.Slice s | Some e -> __.Slice (s, min __.Length (e - s))