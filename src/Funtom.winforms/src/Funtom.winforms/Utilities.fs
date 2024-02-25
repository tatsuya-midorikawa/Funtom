namespace Funtom.winforms

[<Measure>] type px

[<RequireQualifiedAccess>]
type evt =
  | click of (System.EventArgs -> unit)

module Utilities =
  let binder (o: 't) = if o = null then None else Some o
