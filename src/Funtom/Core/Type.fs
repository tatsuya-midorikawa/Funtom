namespace Funtom

open System.ComponentModel
open System.Runtime.CompilerServices

type Type(t: System.Type) as __ =
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  member _.GetType() = t.GetType() |> Type
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.ToString() = t.ToString()
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.Equals(obj: obj) = t.Equals(obj)
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.GetHashCode() = t.GetHashCode()

  member _.raw with get() = t
  
  member _.equals(obj: obj) : bool = t.Equals(obj)
  member _.equals(type': Type) : bool = t.Equals(type'.raw)
  member _.equals(type': System.Type) : bool = t.Equals(type')
  member _.findInterfaces(filter: System.Reflection.TypeFilter, filterCriteria: obj) : Type[] = 
    t.FindInterfaces(filter, filterCriteria) |> Array.map Type
  member _.findMembers
    ( memberType: System.Reflection.MemberTypes, 
      bindingAttr: System.Reflection.BindingFlags, 
      filter: System.Reflection.MemberFilter, 
      filterCriteria: obj ) : System.Reflection.MemberInfo[] = 
    t.FindMembers(memberType, bindingAttr, filter, filterCriteria)
  member _.getArrayRank() : int = t.GetArrayRank()
  member _.getConstructor(types: System.Type[]) : System.Reflection.ConstructorInfo = t.GetConstructor(types)
  member _.getConstructor(types: Type[]) : System.Reflection.ConstructorInfo = t.GetConstructor(types |> Array.map (fun t -> t.raw))


  member _.getType() : Type = t.GetType() |> Type
  member _.toString() : string = t.ToString()
  member _.getHashCode() : int = t.GetHashCode()

