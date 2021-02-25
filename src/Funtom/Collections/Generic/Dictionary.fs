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
  [<DefaultValue>] val mutable internal entries : Dictionary.Entry<'Key, 'Value>[]
  #if BIT64
  [<DefaultValue>] val mutable private fastModMultiplier : uint64
  #endif
  [<DefaultValue>] val mutable internal count : int
  [<DefaultValue>] val mutable private freeList : int
  [<DefaultValue>] val mutable private freeCount : int
  [<DefaultValue>] val mutable internal version : int
  [<DefaultValue>] val mutable private comaperer : System.Collections.Generic.IEqualityComparer<'Key>
  [<DefaultValue>] val mutable private keys : KeyCollection<'Key, 'Value>
  [<DefaultValue>] val mutable private values : ValueCollection
  
and KeyCollection<'Key, 'Value when 'Key: equality>(dictionary': Dictionary<'Key, 'Value>) =
  member public __.getEnumerator() =
    Enumerator(dictionary')


and [<Struct;>] Enumerator<'Key, 'Value when 'Key: equality> =
  val mutable private dictionary : Dictionary<'Key, 'Value>
  val mutable private index : int
  val mutable private version : int
  val mutable private current : 'Value option

  new (dictionary': Dictionary<'Key, 'Value>) =
    { dictionary = dictionary'; index = 0; version = dictionary'.version; current = Option<'Value>.None }

  member __.dispose() = ()
  member __.moveNext() =
    if __.version <> __.dictionary.version then
      System.InvalidOperationException("") |> raise
    
    while __.index < __.dictionary.count do
      for entry in __.entries do
        
    __.index <- __.dictionary.count + 1
    __.current <- Option<'Value>.None
    false
