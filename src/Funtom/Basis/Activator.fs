namespace Funtom

open System
open System.Reflection
open System.Threading

[<AbstractClass; Sealed>]
type Activator private() =
  static member public createInstance (type', binding', binder', args', culture', activation':obj array) = 
    if type' = typeof<System.Reflection.Emit.TypeBuilder> then
      raise(NotSupportedException(""))

    let lookupMask = 0x000000FF
    let mutable binding = binding'
    if (binding' &&& lookupMask) = 0 then
      binding <- binding' ||| lookupMask

    if (0 < activation'.Length) then
      raise(PlatformNotSupportedException(""))

    if (type'.UnderlyingSystemType = typeof<Type>) then
      ()

    ()
