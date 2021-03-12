namespace Funtom.Collections.Generic

open System.Runtime.Serialization

type NonRandomizedStringEqualityComparer() =
  inherit EqualityComparer<string>()

  member __.getObjectData(info: SerializationInfo, context: StreamingContext) = 
    info.SetType(typeof<GenericEqualityComparer<string>>)

  interface System.Runtime.Serialization.ISerializable with
    override __.GetObjectData(info: SerializationInfo, context: StreamingContext) = __.getObjectData(info, context)

  interface Funtom.Runtime.Serialization.ISerializable with
    override __.getObjectData(info: SerializationInfo, context: StreamingContext) = __.getObjectData(info, context)
