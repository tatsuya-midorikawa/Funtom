namespace Funtom.Collections.Generic

open System.ComponentModel
open Funtom
open Funtom.Collections

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
    KeyCollectionEnumerator(dictionary')

and [<Struct;>] KeyCollectionEnumerator<'Key, 'Value when 'Key: equality> =
  val private dictionary : Dictionary<'Key, 'Value>
  val private version : int
  val mutable private index : int
  val mutable private currentKey : 'Key
  
  new(dictionary': Dictionary<'Key, 'Value>) = 
    { dictionary = dictionary'
      version = dictionary'.version
      index = 0
      currentKey = defaultof<'Key> }
      
  interface System.IDisposable with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Dispose() = ()

  interface Funtom.IDisposable with
    override __.dispose() = (__ :> System.IDisposable).Dispose()
  
  interface System.Collections.IEnumerator with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Current =
      if __.index = 0 || __.index = __.dictionary.count + 1 then
        System.InvalidOperationException("") |> raise
      __.currentKey :> obj

    override __.MoveNext() =
      if __.version <> __.dictionary.version then
        System.InvalidOperationException("") |> raise
      // TODO: MIDORIKAWA


  interface Funtom.Collections.Generic.IEnumerator with

  interface System.Collections.Generic.IEnumerator<'Key> with

  interface IEnumerator<'Key> with
    

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
      current = defaultof<KeyValuePair<'Key, 'Value>>
      getEnumeratorRetType = getEnumeratorRetType' }
      
  interface IDisposable with
    override __.dispose() = (__ :> IDisposable).Dispose()
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Dispose() = ()

  interface IEnumerator with
    override __.current = (__ :> IEnumerator).Current |> function null -> Option<obj>.None | x -> Option<obj>.Some x
    override __.reset() = (__ :> IEnumerator).reset()
    override __.moveNext() = (__ :> IEnumerator>).moveNext()
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Current = 
      if __.index = 0 || __.index = __.dictionary.count + 1 then 
        System.InvalidOperationException("") |> raise
      if __.getEnumeratorRetType = Dictionary.Enumerator.DictionaryEntry then
        (DictionaryEntry(__.current.key, __.current.value) :> obj)
      else 
        KeyValuePair<'Key, 'Value>(__.current.key, __.current.value) :> obj

    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.MoveNext() =
      if __.version <> __.dictionary.version then
        System.InvalidOperationException("") |> raise
          
      let mutable break' = not(__.index < __.dictionary.count)
      let mutable result = false
      while not break' do
        break' <- not(__.index < __.dictionary.count)
        let entry = ref __.dictionary.entries.[__.index]
        __.index <- __.index + 1
        if -1 <= (!entry).next then
          __.current <- KeyValuePair<'Key, 'Value>((!entry).key, (!entry).value)
          break' <- true
          result <- true

      __.index <- __.dictionary.count + 1
      __.current <- defaultof<KeyValuePair<'Key, 'Value>>
      result

    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Reset() = 
      if __.version <> __.dictionary.version then
        System.InvalidOperationException("") |> raise
      __.index <- 0
      __.current <- defaultof<KeyValuePair<'Key, 'Value>>

  interface IEnumerator<KeyValuePair<'Key, 'Value>> with
    override __.current = __.current

  interface IDictionaryEnumerator with
    override __.key = 
      if __.index = 0 || __.index = __.dictionary.count + 1 then
        System.InvalidOperationException("") |> raise
      __.current.key :> obj

    override __.value = 
      if __.index = 0 || __.index = __.dictionary.count + 1 then
        System.InvalidOperationException("") |> raise
      __.current.value :> obj

    override __.entry = 
      if __.index = 0 || __.index = __.dictionary.count + 1 then
        System.InvalidOperationException("") |> raise
      DictionaryEntry(__.current.key, __.current.value)

    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Key = 
      if __.index = 0 || __.index = __.dictionary.count + 1 then
        System.InvalidOperationException("") |> raise
      __.current.key :> obj
      
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Value = 
      if __.index = 0 || __.index = __.dictionary.count + 1 then
        System.InvalidOperationException("") |> raise
      __.current.value :> obj
      
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Entry = 
      if __.index = 0 || __.index = __.dictionary.count + 1 then
        System.InvalidOperationException("") |> raise
      System.Collections.DictionaryEntry(__.current.key, __.current.value)

