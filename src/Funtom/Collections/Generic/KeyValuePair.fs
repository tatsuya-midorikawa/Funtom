namespace Funtom.Collections.Generic

open System
open System.ComponentModel
open System.Runtime.CompilerServices
open FSharp.NativeInterop
open Funtom.Text

#nowarn "9"
module KeyValuePair =
  let public toString(key:obj, value:obj) =
    let sb = new ValueStringBuilder(System.Span<char>(NativePtr.stackalloc<char>(64)|> NativePtr.toVoidPtr, 64))
    sb.append('[')
    if key <> null then
      sb.append(key.ToString())
    sb.append(',')
    if value <> null then
      sb.append(value.ToString())
    sb.append(']')
    sb.toString()
    
    
[<Struct; Serializable; IsReadOnly;>]
type KeyValuePair<'Key, 'Value>(key': 'Key, value': 'Value) =
  static member public create(key, value) = KeyValuePair(key, value)
  
  member public __.key = key'
  member public __.value  = value'
  
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  member public __.deconstruct(key: outref<'Key>, value: outref<'Value>) =
    key <- key'
    value <- value'

  override __.ToString() =
    KeyValuePair.toString(__.key, __.value)  
