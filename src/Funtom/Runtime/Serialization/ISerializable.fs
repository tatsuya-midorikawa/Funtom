namespace Funtom.Runtime.Serialization

open System.Runtime.Serialization

type ISerializable =
  inherit System.Runtime.Serialization.ISerializable
  abstract member getObjectData : info: SerializationInfo * context: StreamingContext -> unit
