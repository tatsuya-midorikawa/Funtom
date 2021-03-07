namespace Funtom.Collections

type ICollection =
  inherit System.Collections.ICollection
  inherit Funtom.Collections.IEnumerable
  abstract member copyTo : array: System.Array * index: int -> unit
  abstract member count : int with get
  abstract member syncroot : obj with get
  abstract member isSynchronized : bool with get
