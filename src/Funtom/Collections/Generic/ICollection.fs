namespace Funtom.Collections.Generic

type ICollection<'T> =
  inherit System.Collections.Generic.ICollection<'T>
  inherit Funtom.Collections.Generic.IEnumerable<'T>
  abstract member count : int with get
  abstract member isReadOnly : bool with get
  abstract member add : item: 'T -> unit
  abstract member clear : unit -> unit
  abstract member contains : item: 'T -> bool
  abstract member copyTo : array: 'T[] * index: int -> unit
  abstract member remove : item: 'T -> bool
