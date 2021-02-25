namespace Funtom.Collections.Generic

type internal InsertionBehavior =
  | none = 0uy
  | overwriteExisting = 1uy
  | throwOnExisting = 2uy

module rec Dictionary =
  [<Struct;>]
  type Entry<'Key, 'Value> =
    val mutable hashCode : uint
    val mutable next : int
    val mutable key : 'Key
    val mutable value : 'Value
    new(hashCode', next', key', value') =
      { hashCode = hashCode'; next = next'; key = key'; value = value'; }

  [<Struct;>]
  type Enumerator<'Key, 'Value> =
    val mutable private entries : Dictionary.Entry<'Key, 'Value>[]
    val mutable private dictionary : IDictionary<'Key, 'Value>
    val mutable private index : int
    val mutable private version : int
    val mutable private current : 'Value option

    new (dictionary': IDictionary<'Key, 'Value>, entries: Dictionary.Entry<'Key, 'Value>[], version': int) =
      { entries = entries; dictionary = dictionary'; index = 0; version = version'; current = Option<'Value>.None }

    member __.dispose() = ()
    member __.moveNext() =
      if __.version <> __.dictionary.version then
        System.InvalidOperationException("") |> raise
      
      while __.index < __.dictionary.count do
        for entry in __.entries do
          
      __.index <- __.dictionary.count + 1
      __.current <- Option<'Value>.None
      false

  type KeyCollection<'Key, 'Value>(dictionary': IDictionary<'Key, 'Value>) =
    member public __.getEnumerator() =
      Enumerator(dictionary')
    

[<System.Serializable; Sealed;>]
type Dictionary<'Key, 'Value when 'Key: equality>() =
  [<Literal>] 
  let VersionName = "Version"
  [<Literal>] 
  let HashSizeName = "HashSize"
  [<Literal>] 
  let KeyValuPairName = "KeyValuePairs"
  [<Literal>] 
  let ComparerName = "Comparer"

  [<DefaultValue>] val mutable private backets : int[]
  [<DefaultValue>] val mutable private entries : Dictionary.Entry<'Key, 'Value>[]
  #if BIT64
  [<DefaultValue>] val mutable private fastModMultiplier : uint64
  #endif
  [<DefaultValue>] val mutable private count : int
  [<DefaultValue>] val mutable private freeList : int
  [<DefaultValue>] val mutable private freeCount : int
  [<DefaultValue>] val mutable private version : int
  [<DefaultValue>] val mutable private comaperer : System.Collections.Generic.IEqualityComparer<'Key>
  [<DefaultValue>] val mutable private keys : Dictionary.KeyCollection<'Key, 'Value>
  [<DefaultValue>] val mutable private values : ValueCollection
