namespace Funtom.winforms

[<Measure>] type px

module Utilities =
  let binder (o: 't) = if o = null then None else Some o
