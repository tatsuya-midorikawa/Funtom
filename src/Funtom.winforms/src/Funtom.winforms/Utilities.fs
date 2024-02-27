namespace Funtom.winforms

[<Measure>] type px

module Debug =
  let inline log msg = System.Diagnostics.Debug.WriteLine $"{msg}"

[<RequireQualifiedAccess>]
type evt =
  | click of (System.EventArgs -> unit)

module Utilities =
  let binder (o: 't) = if o = null then None else Some o
