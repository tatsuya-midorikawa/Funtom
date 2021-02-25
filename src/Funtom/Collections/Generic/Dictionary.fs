namespace Funtom.Collections.Generic

type internal InsertionBehavior =
  | none = 0uy
  | overwriteExisting = 1uy
  | throwOnExisting = 2uy

module Dictionary =
  [<Struct;>]
  type Entry<'Key, 'Value> =
    val mutable internal hashCode : uint
    val mutable internal next : int
    val mutable internal key : 'Key
    val mutable internal value : 'Value
    new(hashCode', next', key', value') =
      { hashCode = hashCode'; next = next'; key = key'; value = value'; }

  module Enumerator = 
    [<Literal>]
    let DictionaryEntry = 1
    [<Literal>]
    let KeyValuePair = 2

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


  val private dictionary : Dictionary<'Key, 'Value>
  val private version : int
  val mutable private index : int
  val mutable private current : KeyValuePair<'Key, 'Value>
  val private getEnumeratorRetType : int

  new (dictionary': Dictionary<'Key, 'Value>, getEnumeratorRetType') =
    { dictionary = dictionary'
      index = 0
      version = dictionary'.version
      current = Unchecked.defaultof<KeyValuePair<'Key, 'Value>>
      getEnumeratorRetType = getEnumeratorRetType' }

  member __.moveNext() =
    if __.version <> __.dictionary.version then
      System.InvalidOperationException("") |> raise
    
    let mutable break' = false
    while __.index < __.dictionary.count && not break' do
      let entry = ref __.dictionary.entries.[__.index]
      __.index <- __.index + 1
      if -1 <= (!entry).next then
        __.current <- KeyValuePair<'Key, 'Value>((!entry).key, (!entry).value)
        break' <- true

    __.index <- __.dictionary.count + 1
    __.current <- Unchecked.defaultof<KeyValuePair<'Key, 'Value>>
    false
    
  member __.dispose() = ()
