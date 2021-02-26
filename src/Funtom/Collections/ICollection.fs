namespace Funtom.Collections

type ICollection =
  inherit System.Collections.ICollection
  inherit Funtom.Collections.IEnumerable
  abstract member copyTo : (System.Array * int) -> IEnumerator
  abstract member count : int with get
  abstract member syncroot : obj with get
  abstract member isSynchronized : bool with get
