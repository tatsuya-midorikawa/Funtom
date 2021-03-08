namespace Funtom.Collections.Generic

open System.Runtime.CompilerServices
open System.ComponentModel

[<AbstractClass>]
type EqualityComparer<'T>() =
  abstract member equals: x: 'T * y: 'T -> bool
  abstract member getHashCode: obj: 'T -> int
  
  member public __.equals(x: obj, y: obj) =
    if x = y then true
    else if x = null || y = null then false
    else if x.GetType() = typeof<'T> &&  y.GetType() = typeof<'T> then __.equals(x :?> 'T, y:?> 'T)
    else System.ArgumentException("") |> raise

  member public __.getHashCode(obj: obj) =
    if obj = null then 0
    else if obj.GetType() = typeof<'T> then __.getHashCode(obj :?> 'T)
    else System.ArgumentException("") |> raise
      
  interface System.Collections.IEqualityComparer with
    override __.Equals(x, y) = __.equals(x, y)
    override __.GetHashCode(obj) = __.getHashCode(obj)

  interface Funtom.Collections.IEqualityComparer with
    override __.equals(x, y) = __.equals(x, y)
    override __.getHashCode(obj) = __.getHashCode(obj)

  interface System.Collections.Generic.IEqualityComparer<'T> with
    override __.Equals(x, y) = __.equals(x, y)
    override __.GetHashCode(obj) = __.getHashCode(obj)
    
  interface Funtom.Collections.Generic.IEqualityComparer<'T> with
    override __.equals(x, y) = __.equals(x, y)
    override __.getHashCode(obj) = __.getHashCode(obj)


[<Sealed;System.Serializable;>]
[<TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")>]
type GenericEqualityComparer<'T when 'T :> System.IEquatable<'T> and 'T : null and 'T : equality>() =
  inherit EqualityComparer<'T>()

  member __.equals(obj: obj) =
    if obj = null then false
    else obj.GetType() = typeof<GenericEqualityComparer<'T>>
    
  override __.Equals(obj: obj) = __.equals(obj)

  override __.equals(x, y) = 
    if x <> null then
      if y <> null then x.Equals(y) else false
    else
      if y <> null then false else true

  override __.getHashCode(obj) =
    if obj <> null then obj.GetHashCode()
    else 0
    
  [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
  override __.GetHashCode() =
    typeof<GenericEqualityComparer<'T>>.GetHashCode()
    
  interface System.IEquatable<'T> with
    override __.Equals(other) = __.equals(other)

  interface Funtom.IEquatable<'T> with
    override __.equals(other) = __.equals(other)
