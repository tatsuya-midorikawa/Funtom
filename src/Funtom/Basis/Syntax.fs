namespace Funtom

[<AutoOpen>]
module public Syntax =
  let inline public defaultof<'T> = Unchecked.defaultof<'T>
