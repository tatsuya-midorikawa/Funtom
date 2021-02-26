namespace Funtom.Collections

type IEnumerable =
  inherit System.Collections.IEnumerable
  abstract member getEnumerator : unit -> IEnumerator
