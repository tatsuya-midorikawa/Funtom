namespace Funtom

open System.ComponentModel
open System.Runtime.CompilerServices

type Object() as this =
  inherit System.Object()
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.Finalize() = base.Finalize()
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  member _.GetType() = base.GetType()
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  member _.MemberwiseClone() = base.MemberwiseClone()
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.ToString() = base.ToString()
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.Equals(obj: obj) = base.Equals(obj)
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.GetHashCode() = base.GetHashCode()
  
  static member equals(a: obj, b: obj) =
    if a = b then true
    elif a = null || b = null then false
    else a.Equals(b)
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  static member Equals(a, b) = Object.equals(a, b)

  static member referenceEquals(a: obj, b: obj) = a = b
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  static member ReferenceEquals(a, b) = Object.referenceEquals(a, b)

  member _.getType() = base.GetType()
  member _.memberwiseClone() = base.MemberwiseClone()
  member _.toString() = this.getType().ToString()
  member _.equals(obj: obj) = RuntimeHelpers.Equals(this, obj)
  member _.getHashCode() = RuntimeHelpers.GetHashCode(this)

