namespace Funtom.Collections.Generic

type ICollection<'T> =
  inherit System.Collections.Generic.ICollection<'T>
  inherit Funtom.Collections.Generic.IEnumerable<'T>
  abstract member count : int with get
  abstract member isReadonly : bool with get
  abstract member add : 'T -> unit
  abstract member clear : unit -> unit
  abstract member contains : 'T -> bool
  abstract member copyTo : ('T[] * int) -> unit
  abstract member remove : 'T -> bool
