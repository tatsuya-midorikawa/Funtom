namespace Funtom.experiment.provider

open System
open System.Reflection
open ProviderImplementation.ProvidedTypes
open FSharp.Core.CompilerServices
open FSharp.Quotations

// This type defines the type provider. When compiled to a DLL, it can be added
// as a reference to an F# command-line compilation, script, or project.
[<TypeProvider>]
type public SampleTypeProvider(config: TypeProviderConfig) as this =

  // Inheriting from this type provides implementations of ITypeProvider
  // in terms of the provided types below.
  inherit TypeProviderForNamespaces(config)

  let namespaceName = "Funtom.experiment.provider"
  let thisAssembly = Assembly.GetExecutingAssembly()

  // Make one provided type, called TypeN.
  let makeOneProvidedType (n:int) =
    // This is the provided type. It is an erased provided type and, in compiled code,
    // will appear as type 'obj'.
    let t = ProvidedTypeDefinition(thisAssembly, namespaceName,
                                  "Type" + string n,
                                  baseType = Some typeof<obj>)
    t.AddXmlDocDelayed (fun () -> $"""This provided type {"Type" + string n}""")
    let staticProp = ProvidedProperty(propertyName = "StaticProperty",
                                  propertyType = typeof<string>,
                                  isStatic = true,
                                  getterCode = (fun args -> <@@ "Hello!" @@>))
    staticProp.AddXmlDocDelayed(fun () -> "This is a static property")
    t.AddMember staticProp
    let ctor = ProvidedConstructor(parameters = [ ],
                               invokeCode = (fun args -> <@@ "The object data" :> obj @@>))
    ctor.AddXmlDocDelayed(fun () -> "This is a constructor")
    t.AddMember ctor
    let instanceProp =
      ProvidedProperty(propertyName = "InstanceProperty",
                      propertyType = typeof<int>,
                      getterCode= (fun args ->
                          <@@ ((%%(args[0]) : obj) :?> string).Length @@>))
    instanceProp.AddXmlDocDelayed(fun () -> "This is an instance property")
    t.AddMember instanceProp
    let instanceMeth =
      ProvidedMethod(methodName = "InstanceMethod",
                    parameters = [ProvidedParameter("x",typeof<int>)],
                    returnType = typeof<char>,
                    invokeCode = (fun args ->
                        <@@ ((%%(args[0]) : obj) :?> string).Chars(%%(args[1]) : int) @@>))

    instanceMeth.AddXmlDocDelayed(fun () -> "This is an instance method")
    // Add the instance method to the type.
    t.AddMember instanceMeth
    t.AddMembersDelayed(fun () ->
      let nestedType = ProvidedTypeDefinition("NestedType", Some typeof<obj>)

      nestedType.AddMembersDelayed (fun () ->
        let staticPropsInNestedType =
          [
            for i in 1 .. 100 ->
              let valueOfTheProperty = "I am string "  + string i

              let p =
                ProvidedProperty(propertyName = "StaticProperty" + string i,
                  propertyType = typeof<string>,
                  isStatic = true,
                  getterCode= (fun args -> <@@ valueOfTheProperty @@>))

              p.AddXmlDocDelayed(fun () ->
                $"This is StaticProperty{i} on NestedType")

              p
          ]

        staticPropsInNestedType)

      [nestedType])
    t

  // Now generate 100 types
  let types = [ for i in 1 .. 100 -> makeOneProvidedType i ]

  // And add them to the namespace
  do this.AddNamespace(namespaceName, types)

[<assembly:CompilerServices.TypeProviderAssembly()>]
do()