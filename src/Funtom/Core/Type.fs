namespace Funtom

open System.ComponentModel
open System.Reflection
open System.Runtime.CompilerServices

type Type(t: System.Type) =
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  member _.GetType() = t.GetType() |> Type
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.ToString() = t.ToString()
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.Equals(obj: obj) = t.Equals(obj)
  [<EditorBrowsable(EditorBrowsableState.Never)>]
  override _.GetHashCode() = t.GetHashCode()

  static member Delimiter : char = System.Type.Delimiter
  static member EmptyTypes : Type[] = System.Type.EmptyTypes |> Array.map Type
  static member DefaultBinder : Binder = System.Type.DefaultBinder

  member _.raw = t
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
  member _.getNestedType(name: string) : Type = t.GetNestedType(name) |> Type
  member _.getNestedType(name: string, bindingAttr: BindingFlags) : Type = t.GetNestedType(name, bindingAttr) |> Type
  member _.getNestedTypes() : Type[] = t.GetNestedTypes() |> Array.map Type
  member _.getNestedTypes(bindingAttr: BindingFlags) : Type[] = t.GetNestedTypes(bindingAttr) |> Array.map Type
  member _.getProperties() : PropertyInfo[] = t.GetProperties()
  member _.getProperties(bindingAttr: BindingFlags) : PropertyInfo[] = t.GetProperties(bindingAttr)
  member _.getProperty(name: string) : PropertyInfo = t.GetProperty(name)
  member _.getProperty(name: string, bindingAttr: BindingFlags) : PropertyInfo = t.GetProperty(name, bindingAttr)
  member _.getProperty(name: string, returnType: System.Type) : PropertyInfo = t.GetProperty(name, returnType)
  member _.getProperty(name: string, returnType: Type) : PropertyInfo = t.GetProperty(name, returnType.raw)
  member _.getProperty(name: string, types: System.Type[]) : PropertyInfo = t.GetProperty(name, types)
  member _.getProperty(name: string, types: Type[]) : PropertyInfo = t.GetProperty(name, types |> Array.map (fun t -> t.raw))
  member _.getProperty(name: string, returnType: System.Type, types: System.Type[]) : PropertyInfo = t.GetProperty(name, returnType, types)
  member _.getProperty(name: string, returnType: Type, types: Type[]) : PropertyInfo = t.GetProperty(name, returnType.raw, types |> Array.map (fun t -> t.raw))
  member _.getProperty(name: string, bindingAttr: BindingFlags, binder: Binder, returnType: System.Type, types: System.Type[], modifiers: ParameterModifier[]) : PropertyInfo = 
    t.GetProperty(name, bindingAttr, binder, returnType, types, modifiers)
  member _.getProperty(name: string, bindingAttr: BindingFlags, binder: Binder, returnType: Type, types: Type[], modifiers: ParameterModifier[]) : PropertyInfo = 
    t.GetProperty(name, bindingAttr, binder, returnType.raw, types |> Array.map (fun t -> t.raw), modifiers)
  member _.getType() : Type = t.GetType() |> Type
  member _.invokeMember(name: string, invokeAttr: BindingFlags, binder: Binder, target: obj, args: obj[]) : obj =
    t.InvokeMember(name, invokeAttr, binder, target, args)
  member _.invokeMember(name: string, invokeAttr: BindingFlags, binder: Binder, target: obj, args: obj[], culture: System.Globalization.CultureInfo) : obj =
    t.InvokeMember(name, invokeAttr, binder, target, args, culture)
  member _.invokeMember(name: string, invokeAttr: BindingFlags, binder: Binder, target: obj, args: obj[], modifiers: ParameterModifier[], culture: System.Globalization.CultureInfo, namedParameters: string[]) : obj =
    t.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters)
  member _.isAssignableFrom(type': System.Type) : bool = t.IsAssignableFrom(type')
  member _.isAssignableFrom(type': Type) : bool = t.IsAssignableFrom(type'.raw)
  member _.isEnumDefined(value: obj) : bool = t.IsEnumDefined(value)
  member _.isEquivalentTo(other: System.Type) : bool = t.IsEquivalentTo(other)
  member _.isEquivalentTo(other: Type) : bool = t.IsEquivalentTo(other.raw)
  member _.isInstanceOfType(o: obj) : bool = t.IsInstanceOfType(o)
  member _.isSubclassOf(c: System.Type) : bool = t.IsSubclassOf(c)
  member _.isSubclassOf(c: Type) : bool = t.IsSubclassOf(c.raw)
  member _.makeArrayType() : Type = t.MakeArrayType() |> Type
  member _.makeArrayType(rank: int) : Type = t.MakeArrayType(rank) |> Type
  member _.makeByRefType() : Type = t.MakeByRefType() |> Type
  member _.makeGenericType([<System.ParamArray>] typeArguments: System.Type[]) : Type = t.MakeGenericType(typeArguments) |> Type
  member _.makeGenericType([<System.ParamArray>] typeArguments: Type[]) : Type = t.MakeGenericType(typeArguments |> Array.map (fun t -> t.raw)) |> Type
  member _.makePointerType() : Type = t.MakePointerType() |> Type
  member _.toString() : string = t.ToString()

  member _.Assembly : Assembly = t.Assembly
  member _.AssemblyQualifiedName : string = t.AssemblyQualifiedName
  member _.Attributes : TypeAttributes = t.Attributes
  member _.BaseType : Type = t.BaseType |> Type
  member _.ContainsGenericParameters : bool = t.ContainsGenericParameters
  member _.DeclaringMethod : MethodBase = t.DeclaringMethod
  member _.DeclaringType : Type = t.DeclaringType |> Type
  member _.FullName : string = t.FullName
  member _.GUID : System.Guid = t.GUID
  member _.GenericParameterAttributes : GenericParameterAttributes = t.GenericParameterAttributes
  member _.GenericParameterPosition : int = t.GenericParameterPosition
  member _.GenericTypeArguments : Type[] = t.GenericTypeArguments |> Array.map Type
  member _.HasElementType : bool = t.HasElementType
  member _.IsAbstract : bool = t.IsAbstract
  member _.IsAnsiClass : bool = t.IsAnsiClass
  member _.IsArray : bool = t.IsArray
  member _.IsAutoClass : bool = t.IsAutoClass
  member _.IsAutoLayout : bool = t.IsAutoLayout
  member _.IsByRef : bool = t.IsByRef
  member _.IsCOMObject : bool = t.IsCOMObject
  member _.IsClass : bool = t.IsClass
  member _.IsConstructedGenericType : bool = t.IsConstructedGenericType
  member _.IsContextful : bool = t.IsContextful
  member _.IsEnum : bool = t.IsEnum
  member _.IsExplicitLayout : bool = t.IsExplicitLayout
  member _.IsGenericParameter : bool = t.IsGenericParameter
  member _.IsGenericType : bool = t.IsGenericType
  member _.IsGenericTypeDefinition : bool = t.IsGenericTypeDefinition
  member _.IsImport : bool = t.IsImport
  member _.IsInterface : bool = t.IsInterface
  member _.IsLayoutSequential : bool = t.IsLayoutSequential
  member _.IsMarshalByRef : bool = t.IsMarshalByRef
  member _.IsNested : bool = t.IsNested
  member _.IsNestedAssembly : bool = t.IsNestedAssembly
  member _.IsNestedFamANDAssem : bool = t.IsNestedFamANDAssem
  member _.IsNestedFamORAssem : bool = t.IsNestedFamORAssem
  member _.IsNestedFamily : bool = t.IsNestedFamily
  member _.IsNestedPrivate : bool = t.IsNestedPrivate
  member _.IsNestedPublic : bool = t.IsNestedPublic
  member _.IsNotPublic : bool = t.IsNotPublic
  member _.IsPointer : bool = t.IsPointer
  member _.IsPrimitive : bool = t.IsPrimitive
  member _.IsPublic : bool = t.IsPublic
  member _.IsSealed : bool = t.IsSealed
  member _.IsSecurityCritical : bool = t.IsSecurityCritical
  member _.IsSecuritySafeCritical : bool = t.IsSecuritySafeCritical
  member _.IsSerializable : bool = t.IsSerializable
  member _.IsSpecialName : bool = t.IsSpecialName
  member _.IsUnicodeClass : bool = t.IsUnicodeClass
  member _.IsValueType : bool = t.IsValueType
  member _.IsVisible : bool = t.IsVisible
  member _.MemberType : MemberTypes = t.MemberType
  member _.Module : Module = t.Module
  member _.Namespace : string = t.Namespace
  member _.ReflectedType : Type = t.ReflectedType |> Type
  member _.StructLayoutAttribute : System.Runtime.InteropServices.StructLayoutAttribute = t.StructLayoutAttribute
  member _.TypeHandle : System.RuntimeTypeHandle = t.TypeHandle
  member _.TypeInitializer : ConstructorInfo = t.TypeInitializer
  member _.UnderlyingSystemType : Type = t.UnderlyingSystemType |> Type
