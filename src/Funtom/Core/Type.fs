namespace Funtom

open System.ComponentModel
open System.Reflection
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
  member _.findInterfaces(filter: TypeFilter, filterCriteria: obj) : Type[] = 
    t.FindInterfaces(filter, filterCriteria) |> Array.map Type
  member _.findMembers(memberType: MemberTypes, bindingAttr: BindingFlags, filter: MemberFilter, filterCriteria: obj) : MemberInfo[] = 
    t.FindMembers(memberType, bindingAttr, filter, filterCriteria)
  member _.getArrayRank() : int = t.GetArrayRank()
  member _.getConstructor(types: System.Type[]) : ConstructorInfo = t.GetConstructor(types)
  member _.getConstructor(types: Type[]) : ConstructorInfo = t.GetConstructor(types |> Array.map (fun t -> t.raw))
  member _.getConstructor(bindingAttr: BindingFlags, binder: Binder, types: System.Type[], modifiers: ParameterModifier[]) : ConstructorInfo = 
    t.GetConstructor(bindingAttr, binder, types, modifiers)
  member _.getConstructor(bindingAttr: BindingFlags, binder: Binder, types: Type[], modifiers: ParameterModifier[]) : ConstructorInfo = 
    t.GetConstructor(bindingAttr, binder, types |> Array.map (fun t -> t.raw), modifiers)
  member _.getConstructor(bindingAttr: BindingFlags, binder: Binder, callConvention: CallingConventions, types: System.Type[], modifiers: ParameterModifier[]) : ConstructorInfo = 
    t.GetConstructor(bindingAttr, binder, callConvention, types, modifiers)
  member _.getConstructor(bindingAttr: BindingFlags, binder: Binder, callConvention: CallingConventions, types: Type[], modifiers: ParameterModifier[]) : ConstructorInfo = 
    t.GetConstructor(bindingAttr, binder, callConvention, types |> Array.map (fun t -> t.raw), modifiers)
  member _.getConstructors() : ConstructorInfo[] = t.GetConstructors()
  member _.getConstructors(bindingAttr: BindingFlags) : ConstructorInfo[] = t.GetConstructors(bindingAttr)
  member _.getDefaultMembers() : MemberInfo[] = t.GetDefaultMembers()
  member _.getElementType() : Type = t.GetElementType() |> Type
  member _.getEnumName(value: obj) : string = t.GetEnumName(value)
  member _.getEnumNames() : string[] = t.GetEnumNames()
  member _.getEnumUnderlyingType() : Type = t.GetEnumUnderlyingType() |> Type
  member _.getEnumValues() : System.Array = t.GetEnumValues()
  member _.getEvent(name: string) : EventInfo = t.GetEvent(name)
  member _.getEvent(name: string, bindingAttr: BindingFlags) : EventInfo = t.GetEvent(name, bindingAttr)
  member _.getEvents() : EventInfo[] = t.GetEvents()
  member _.getEvents(bindingAttr: BindingFlags) : EventInfo[] = t.GetEvents(bindingAttr)
  member _.getField(name: string) : FieldInfo = t.GetField(name)
  member _.getField(name: string, bindingAttr: BindingFlags) : FieldInfo = t.GetField(name, bindingAttr)
  member _.getFields() : FieldInfo[] = t.GetFields()
  member _.getFields(bindingAttr: BindingFlags) : FieldInfo[] = t.GetFields(bindingAttr)
  member _.getGenericArguments() : Type[] = t.GetGenericArguments() |> Array.map Type
  member _.getGenericParameterConstraints() : Type[] = t.GetGenericParameterConstraints() |> Array.map Type
  member _.getGenericTypeDefinition() : Type = t.GetGenericTypeDefinition() |> Type
  member _.getHashCode() : int = t.GetHashCode()
  member _.getInterface(name: string) : Type = t.GetInterface(name) |> Type
  member _.getInterface(name: string, ignoreCase: bool) : Type = t.GetInterface(name, ignoreCase) |> Type
  member _.getInterfaceMap(interfaceType: System.Type) : InterfaceMapping = t.GetInterfaceMap(interfaceType)
  member _.getInterfaceMap(interfaceType: Type) : InterfaceMapping = t.GetInterfaceMap(interfaceType.raw)
  member _.getInterfaces() : Type[] = t.GetInterfaces() |> Array.map Type
  member _.getMember(name: string) : MemberInfo[] = t.GetMember(name)
  member _.getMember(name: string, bindingAttr: BindingFlags) : MemberInfo[] = t.GetMember(name, bindingAttr)
  member _.getMember(name: string, type': MemberTypes, bindingAttr: BindingFlags) : MemberInfo[] = t.GetMember(name, type', bindingAttr)
  member _.getMembers() : MemberInfo[] = t.GetMembers()
  member _.getMembers(bindingAttr: BindingFlags) : MemberInfo[] = t.GetMembers(bindingAttr)
  member _.getMethod(name: string) : MethodInfo = t.GetMethod(name)
  member _.getMethod(name: string, bindingAttr: BindingFlags) : MethodInfo = t.GetMethod(name, bindingAttr)
  member _.getMethod(name: string, types: System.Type[]) : MethodInfo = t.GetMethod(name, types)
  member _.getMethod(name: string, types: Type[]) : MethodInfo = t.GetMethod(name, types |> Array.map (fun t -> t.raw))
  member _.getMethod(name: string, types: System.Type[], modifiers: ParameterModifier[]) : MethodInfo = t.GetMethod(name, types, modifiers)
  member _.getMethod(name: string, types: Type[], modifiers: ParameterModifier[]) : MethodInfo = t.GetMethod(name, types |> Array.map (fun t -> t.raw), modifiers)
  member _.getMethod(name: string, bindingAttr: BindingFlags, binder: Binder, types: System.Type[], modifiers: ParameterModifier[]) : MethodInfo = 
    t.GetMethod(name, bindingAttr, binder, types, modifiers)
  member _.getMethod(name: string, bindingAttr: BindingFlags, binder: Binder, types: Type[], modifiers: ParameterModifier[]) : MethodInfo = 
    t.GetMethod(name, bindingAttr, binder, types |> Array.map (fun t -> t.raw), modifiers)
  member _.getMethods() : MethodInfo[] = t.GetMethods()
  member _.getMethods(bindingAttr: BindingFlags) : MethodInfo[] = t.GetMethods(bindingAttr)



  member _.f () = t.GetElementType



  member _.getType() : Type = t.GetType() |> Type
  member _.toString() : string = t.ToString()

