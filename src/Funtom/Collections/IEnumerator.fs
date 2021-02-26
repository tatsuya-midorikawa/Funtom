namespace Funtom.Collections

type IEnumerator =
  inherit System.Collections.IEnumerator
  abstract member moveNext : unit -> bool
  abstract member current : obj option with get
  abstract member reset : unit -> unit
