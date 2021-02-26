namespace Funtom.Collections

type IDictionary =
  inherit System.Collections.IDictionary
  inherit Funtom.Collections.ICollection
  abstract member Item : obj -> obj with get, set
  abstract member keys : ICollection with get
  abstract member values : ICollection with get
  abstract member contains : obj -> bool
  abstract member add : (obj * obj) -> unit
  abstract member clear : unit -> unit
  abstract member isReadOnly : bool with get
  abstract member isFixedSize : bool with get
  abstract member getEnumerator : unit -> IDictionaryEnumerator
  abstract member remove : obj -> unit
