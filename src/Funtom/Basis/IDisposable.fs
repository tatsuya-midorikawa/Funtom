namespace Funtom

type public IDisposable =
  inherit System.IDisposable
  abstract member dispose : unit -> unit
