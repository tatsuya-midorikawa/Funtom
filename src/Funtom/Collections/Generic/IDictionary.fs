namespace Funtom.Collections.Generic

type IDictionary<'Key, 'Value> =
  inherit System.Collections.Generic.IDictionary<'Key, 'Value>
  inherit Funtom.Collections.Generic.ICollection<KeyValuePair<'Key, 'Value>>
  abstract member Item : 'Key -> 'Value with get, set
  abstract member keys : ICollection<'Key> with get
  abstract member values : ICollection<'Value> with get
  abstract member containsKey : 'Key -> bool
  abstract member add : ('Key * 'Value) -> unit
  abstract member remove : 'Key -> bool
  abstract member tryGetValue : 'Key -> 'Value option
