namespace Funtom.Collections

open System.ComponentModel

[<Struct; System.Serializable;>]
type DictionaryEntry =
  val mutable private _key : obj
  val mutable private _value : obj
  new(key': obj, value': obj) = { _key = key'; _value = value'; }

  member __.key with get() = __._key and set(v) = __._key <- v
  member __.value with get() = __._value and set(v) = __._value <- v
  [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
  member __.deconstruct(key': outref<obj>, value': outref<obj>) =
    key' <- __.key
    value' <- __.value
