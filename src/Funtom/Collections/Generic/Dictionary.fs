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



/// <summary>
///
/// </summary>
[<System.Serializable; Sealed;>]
type Dictionary<'Key, 'Value when 'Key: equality and 'Value: equality> =
  [<Literal>] 
  let VersionName = "Version"
  [<Literal>] 
  let HashSizeName = "HashSize"
  [<Literal>] 
  let KeyValuPairName = "KeyValuePairs"
  [<Literal>] 
  let ComparerName = "Comparer"

  let mutable backets : int[] = defaultof<int[]>
  #if BIT64
  let mutable fastModMultiplier : uint64 = 0UL
  #endif
  let mutable freeList : int = 0
  let mutable freeCount : int = 0
  val mutable comparer : System.Collections.Generic.IEqualityComparer<'Key>
  let mutable keys : KeyCollection<'Key, 'Value> = defaultof<KeyCollection<'Key, 'Value>>
  let mutable values : ValueCollection<'Key, 'Value> = defaultof<ValueCollection<'Key, 'Value>>
  
  member val internal count : int = 0 with get, set
  member val internal entries : Dictionary.Entry<'Key, 'Value>[] = defaultof<Dictionary.Entry<'Key, 'Value>[]> with get, set
  member val internal version : int = 0

  new(capacity': int, comparer': System.Collections.Generic.IEqualityComparer<'Key>) =
    if capacity' < 0 then
      System.ArgumentOutOfRangeException("") |> raise

    
    if comparer' <> defaultof<System.Collections.Generic.IEqualityComparer<'Key>> then
      { comparer = comparer' }
    else if typeof<'Key> = typeof<string> then
      { comparer = System.Collections.Generic.NonRandomizedStringEqualityComparer }
    else
      { comparer = null }



/// <summary>
///
/// </summary>
and KeyCollection<'Key, 'Value when 'Key: equality and 'Value: equality>(dictionary': Dictionary<'Key, 'Value>) =
  /// <summary></summary>
  member inline public __.getEnumerator() = new KeyCollectionEnumerator<'Key, 'Value>(dictionary')
  /// <summary></summary>
  member inline public __.copyTo(array': 'Key[], index': int) =
    if array' = null then
      System.ArgumentNullException("") |> raise
    if index' < 0 || array'.Length < index' then
      System.ArgumentOutOfRangeException("") |> raise
    if array'.Length - index' < dictionary'.count then
      System.ArgumentException("") |> raise
    
    let mutable index = index'
    for i = 0 to dictionary'.count - 1 do
      if -1 <= dictionary'.entries.[i].next then
        array'.[index] <- dictionary'.entries.[i].key
        index <- index + 1
  /// <summary></summary>
  member inline public __.count = dictionary'.count
  /// <summary></summary>
  member inline public __.syncroot = (dictionary' :> Funtom.Collections.ICollection).syncroot
  /// <summary></summary>
  member inline public __.contains(item: 'Key) = dictionary'.containsKey(item)
  /// <summary></summary>
  member inline public __.isReadOnly = true
  /// <summary></summary>
  member inline public __.remove() = System.NotSupportedException("") |> raise

  interface System.Collections.IEnumerable with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.GetEnumerator() = __.getEnumerator() :> System.Collections.IEnumerator
    
  interface Funtom.Collections.IEnumerable with
    override __.getEnumerator() = __.getEnumerator() :> Funtom.Collections.IEnumerator
  
  interface System.Collections.Generic.IEnumerable<'Key> with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.GetEnumerator() = __.getEnumerator() :> System.Collections.Generic.IEnumerator<'Key>
    
  interface Funtom.Collections.Generic.IEnumerable<'Key> with
    override __.getEnumerator() = __.getEnumerator() :> Funtom.Collections.Generic.IEnumerator<'Key>

  interface System.Collections.ICollection with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.CopyTo(array': System.Array, index': int) =
      if array' = null then
        System.ArgumentNullException("") |> raise
      if array'.Rank <> 1 then
        System.ArgumentException("") |> raise
      if array'.GetLowerBound(0) <> 0 then
        System.ArgumentException("") |> raise
      if uint array'.Length < uint index' then
        System.IndexOutOfRangeException("") |> raise
      if array'.Length - index' < dictionary'.count then
        System.ArgumentException("") |> raise
      
      match array' with
      | :? ('Key[]) as keys -> __.copyTo(keys, index')
      | _ -> 
        let mutable objects = array' :?> obj[]
        if objects = null then
          System.ArgumentException("") |> raise
        
        let mutable index = index'
        let count = dictionary'.count
        let entries = dictionary'.entries
        for i = 0 to count - 1 do
          if -1 <= entries.[i].next then
            objects.[index] <- entries.[i].key :> obj
            index <- index + 1

    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Count = __.count
  
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.SyncRoot = __.syncroot
    
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.IsSynchronized = false

  interface Funtom.Collections.ICollection with
    override __.copyTo(array': System.Array, index': int) = (__ :> System.Collections.ICollection).CopyTo(array', index')
    override __.count = __.count
    override __.syncroot = __.syncroot
    override __.isSynchronized = false

  interface System.Collections.Generic.ICollection<'Key> with
    override __.Add(item: 'Key) = System.NotSupportedException("") |> raise
    override __.Clear() = System.NotSupportedException("") |> raise
    override __.Contains(item: 'Key) = __.contains(item)
    override __.CopyTo(array': 'Key[], index': int) = __.copyTo(array', index')
    override __.Count = __.count
    override __.IsReadOnly = __.isReadOnly
    override __.Remove(item: 'Key) = __.remove()

  interface Funtom.Collections.Generic.ICollection<'Key> with
    override __.add(item: 'Key) = System.NotSupportedException("") |> raise
    override __.clear() = System.NotSupportedException("") |> raise
    override __.contains(item: 'Key) = __.contains(item)
    override __.copyTo(array': 'Key [], index': int) = __.copyTo(array', index')
    override __.count = __.count
    override __.isReadOnly = __.isReadOnly
    override __.remove(item: 'Key) = __.remove()

  interface System.Collections.Generic.IReadOnlyCollection<'Key> with
    override __.Count = __.count

  interface Funtom.Collections.Generic.IReadOnlyCollection<'Key> with
    override __.count = __.count
    


/// <summary>
///
/// </summary>
and ValueCollection<'Key, 'Value when 'Key: equality and 'Value: equality>(dictionary': Dictionary<'Key, 'Value>) =
  /// <summary></summary>
  member inline public __.getEnumerator() = new ValueCollectionEnumerator<'Key, 'Value>(dictionary')
  /// <summary></summary>
  member inline public __.copyTo(array': 'Value[], index': int) =
    if array' = null then
      System.ArgumentNullException("") |> raise
    if index' < 0 || array'.Length < index' then
      System.ArgumentOutOfRangeException("") |> raise
    if array'.Length - index' < dictionary'.count then
      System.ArgumentException("") |> raise
    
    let mutable index = index'
    for i = 0 to dictionary'.count - 1 do
      if -1 <= dictionary'.entries.[i].next then
        array'.[index] <- dictionary'.entries.[i].value
        index <- index + 1
  /// <summary></summary>
  member inline public __.count = dictionary'.count
  /// <summary></summary>
  member inline public __.syncroot = (dictionary' :> Funtom.Collections.ICollection).syncroot
  /// <summary></summary>
  member inline public __.contains(item: 'Value) = dictionary'.containsKey(item)
  /// <summary></summary>
  member inline public __.isReadOnly = true
  /// <summary></summary>
  member inline public __.remove() = System.NotSupportedException("") |> raise

  interface System.Collections.IEnumerable with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.GetEnumerator() = __.getEnumerator() :> System.Collections.IEnumerator
    
  interface Funtom.Collections.IEnumerable with
    override __.getEnumerator() = __.getEnumerator() :> Funtom.Collections.IEnumerator
  
  interface System.Collections.Generic.IEnumerable<'Value> with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.GetEnumerator() = __.getEnumerator() :> System.Collections.Generic.IEnumerator<'Value>
    
  interface Funtom.Collections.Generic.IEnumerable<'Value> with
    override __.getEnumerator() = __.getEnumerator() :> Funtom.Collections.Generic.IEnumerator<'Value>

  interface System.Collections.ICollection with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.CopyTo(array': System.Array, index': int) =
      if array' = null then
        System.ArgumentNullException("") |> raise
      if array'.Rank <> 1 then
        System.ArgumentException("") |> raise
      if array'.GetLowerBound(0) <> 0 then
        System.ArgumentException("") |> raise
      if uint array'.Length < uint index' then
        System.IndexOutOfRangeException("") |> raise
      if array'.Length - index' < dictionary'.count then
        System.ArgumentException("") |> raise
      
      match array' with
      | :? ('Value[]) as values -> __.copyTo(values, index')
      | _ -> 
        let mutable objects = array' :?> obj[]
        if objects = null then
          System.ArgumentException("") |> raise
        
        let mutable index = index'
        let count = dictionary'.count
        let entries = dictionary'.entries
        for i = 0 to count - 1 do
          if -1 <= entries.[i].next then
            objects.[index] <- entries.[i].value :> obj
            index <- index + 1

    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Count = __.count
  
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.SyncRoot = __.syncroot
    
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.IsSynchronized = false

  interface Funtom.Collections.ICollection with
    override __.copyTo(array': System.Array, index': int) = (__ :> System.Collections.ICollection).CopyTo(array', index')
    override __.count = __.count
    override __.syncroot = __.syncroot
    override __.isSynchronized = false

  interface System.Collections.Generic.ICollection<'Value> with
    override __.Add(item: 'Value) = System.NotSupportedException("") |> raise
    override __.Clear() = System.NotSupportedException("") |> raise
    override __.Contains(item: 'Value) = __.contains(item)
    override __.CopyTo(array': 'Value[], index': int) = __.copyTo(array', index')
    override __.Count = __.count
    override __.IsReadOnly = __.isReadOnly
    override __.Remove(item: 'Value) = __.remove()

  interface Funtom.Collections.Generic.ICollection<'Value> with
    override __.add(item: 'Value) = System.NotSupportedException("") |> raise
    override __.clear() = System.NotSupportedException("") |> raise
    override __.contains(item: 'Value) = __.contains(item)
    override __.copyTo(array': 'Value[], index': int) = __.copyTo(array', index')
    override __.count = __.count
    override __.isReadOnly = __.isReadOnly
    override __.remove(item: 'Value) = __.remove()

  interface System.Collections.Generic.IReadOnlyCollection<'Value> with
    override __.Count = __.count

  interface Funtom.Collections.Generic.IReadOnlyCollection<'Value> with
    override __.count = __.count



/// <summary>
///
/// </summary>
and [<Struct;>] KeyCollectionEnumerator<'Key, 'Value when 'Key: equality and 'Value: equality> =
  val private dictionary : Dictionary<'Key, 'Value>
  val private version : int
  val mutable private index : int
  val mutable private currentKey : 'Key
  
  new(dictionary': Dictionary<'Key, 'Value>) = 
    { dictionary = dictionary'
      version = dictionary'.version
      index = 0
      currentKey = defaultof<'Key> }
      
  /// <summary></summary>
  member inline public __.current =
    if __.index = 0 || __.index = __.dictionary.count + 1 then
      System.InvalidOperationException("") |> raise
    __.currentKey
  /// <summary></summary>
  member inline public __.moveNext() =
    if __.version <> __.dictionary.version then
      System.InvalidOperationException("") |> raise
    
    let mutable break' = not(uint __.index < uint __.dictionary.count)
    let mutable result = false
    while not break' do
      break' <- not(uint __.index < uint __.dictionary.count)
      let entry = ref __.dictionary.entries.[__.index]
      __.index <- __.index + 1
      if -1 <= (!entry).next then
        __.currentKey <- (!entry).key
        break' <- true
        result <- true

    __.index <- __.dictionary.count + 1
    __.currentKey <- defaultof<'Key>
    result
  /// <summary></summary>
  member inline public __.reset() =
    if __.index = 0 || __.index = __.dictionary.count + 1 then
      System.InvalidOperationException("") |> raise
    __.index <- 0
    __.currentKey <- defaultof<'Key>
  /// <summary></summary>
  member inline public __.current = __.currentKey
  /// <summary></summary>
  member inline public __.dispose() = ()

  interface System.IDisposable with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Dispose() = __.dispose()

  interface Funtom.IDisposable with
    override __.dispose() = __.dispose()
  
  interface System.Collections.IEnumerator with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Current = __.current :> obj
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.MoveNext() = __.moveNext()
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Reset() = __.reset()

  interface Funtom.Collections.IEnumerator with
    override __.current = __.current :> obj |> function null -> Option<obj>.None | x -> Option<obj>.Some x
    override __.moveNext() = __.moveNext()
    override __.reset() = __.reset()

  interface System.Collections.Generic.IEnumerator<'Key> with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Current = __.current

  interface Funtom.Collections.Generic.IEnumerator<'Key> with
    override __.current = __.current
    
    

/// <summary>
///
/// </summary>
and [<Struct;>] ValueCollectionEnumerator<'Key, 'Value when 'Key: equality and 'Value: equality> =
  val private dictionary : Dictionary<'Key, 'Value>
  val private version : int
  val mutable private index : int
  val mutable private currentValue : 'Value
  
  new(dictionary': Dictionary<'Key, 'Value>) = 
    { dictionary = dictionary'
      version = dictionary'.version
      index = 0
      currentValue = defaultof<'Value> }
  
  /// <summary></summary>
  member inline public __.moveNext() =
    if __.version <> __.dictionary.version then
      System.InvalidOperationException("") |> raise
    
    let mutable break' = not(uint __.index < uint __.dictionary.count)
    let mutable result = false
    while not break' do
      break' <- not(uint __.index < uint __.dictionary.count)
      let entry = ref __.dictionary.entries.[__.index]
      __.index <- __.index + 1
      if -1 <= (!entry).next then
        __.currentValue <- (!entry).value
        break' <- true
        result <- true

    __.index <- __.dictionary.count + 1
    __.currentValue <- defaultof<'Value>
    result
  /// <summary></summary>
  member inline public __.current = 
    if __.index = 0 || __.index = __.dictionary.count + 1 then
      System.InvalidOperationException("") |> raise
    __.currentValue
  /// <summary></summary>
  member inline public __.reset() =
    if __.index = 0 || __.index = __.dictionary.count + 1 then
      System.InvalidOperationException("") |> raise
    __.index <- 0
    __.currentValue <- defaultof<'Value>
  /// <summary></summary>
  member inline public __.dispose() = ()

  interface System.IDisposable with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Dispose() = __.dispose()

  interface Funtom.IDisposable with
    override __.dispose() = __.dispose()
  
  interface System.Collections.IEnumerator with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Current = __.current :> obj
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.MoveNext() = __.moveNext()
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Reset() = __.reset()

  interface Funtom.Collections.IEnumerator with
    override __.current = __.current :> obj |> function null -> Option<obj>.None | x -> Option<obj>.Some x
    override __.moveNext() = __.moveNext()
    override __.reset() = __.reset()

  interface System.Collections.Generic.IEnumerator<'Value> with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Current = __.current

  interface Funtom.Collections.Generic.IEnumerator<'Value> with
    override __.current = __.current


/// <summary>
///
/// </summary>
and [<Struct;>] Enumerator<'Key, 'Value when 'Key: equality and 'Value: equality> =
  val private dictionary : Dictionary<'Key, 'Value>
  val private version : int
  val mutable private index : int
  val mutable private currentPair : KeyValuePair<'Key, 'Value>
  val private getEnumeratorRetType : int

  new (dictionary': Dictionary<'Key, 'Value>, getEnumeratorRetType') =
    { dictionary = dictionary'
      index = 0
      version = dictionary'.version
      currentPair = defaultof<KeyValuePair<'Key, 'Value>>
      getEnumeratorRetType = getEnumeratorRetType' }
      
  /// <summary></summary>
  member inline public __.dispose() = ()
  /// <summary></summary>
  member inline public __.current = 
    if __.index = 0 || __.index = __.dictionary.count + 1 then 
      System.InvalidOperationException("") |> raise
    if __.getEnumeratorRetType = Dictionary.Enumerator.DictionaryEntry then
      DictionaryEntry(__.currentPair.key, __.currentPair.value) :> obj
    else 
      KeyValuePair<'Key, 'Value>(__.currentPair.key, __.currentPair.value) :> obj
  /// <summary></summary>
  member inline public __.moveNext() =
    if __.version <> __.dictionary.version then
      System.InvalidOperationException("") |> raise
        
    let mutable break' = not(uint __.index < uint __.dictionary.count)
    let mutable result = false
    while not break' do
      break' <- not(uint __.index < uint __.dictionary.count)
      let entry = ref __.dictionary.entries.[__.index]
      __.index <- __.index + 1
      if -1 <= (!entry).next then
        __.currentPair <- KeyValuePair<'Key, 'Value>((!entry).key, (!entry).value)
        break' <- true
        result <- true

    __.index <- __.dictionary.count + 1
    __.currentPair <- defaultof<KeyValuePair<'Key, 'Value>>
    result
  /// <summary></summary>
  member inline public __.reset() = 
    if __.version <> __.dictionary.version then
      System.InvalidOperationException("") |> raise
    __.index <- 0
    __.currentPair <- defaultof<KeyValuePair<'Key, 'Value>>
  /// <summary></summary>
  member inline public __.key =
    if __.index = 0 || __.index = __.dictionary.count + 1 then
      System.InvalidOperationException("") |> raise
    __.currentPair.key :> obj
  /// <summary></summary>
  member inline public __.value =
    if __.index = 0 || __.index = __.dictionary.count + 1 then
      System.InvalidOperationException("") |> raise
    __.currentPair.value :> obj
  /// <summary></summary>
  member inline public __.entry = 
    if __.index = 0 || __.index = __.dictionary.count + 1 then
      System.InvalidOperationException("") |> raise
    DictionaryEntry(__.currentPair.key, __.currentPair.value)

  interface System.IDisposable with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Dispose() = __.dispose()

  interface Funtom.IDisposable with
    override __.dispose() = __.dispose()
  
  interface System.Collections.IEnumerator with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Current = __.current
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.MoveNext() = __.moveNext()
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never); >]
    override __.Reset() = __.reset()

  interface Funtom.Collections.IEnumerator with
    override __.current = __.current |> function null -> Option<obj>.None | x -> Option<obj>.Some x
    override __.reset() = __.reset()
    override __.moveNext() = __.moveNext()

  interface System.Collections.Generic.IEnumerator<KeyValuePair<'Key, 'Value>> with
    override __.Current = __.currentPair

  interface Funtom.Collections.Generic.IEnumerator<KeyValuePair<'Key, 'Value>> with
    override __.current = __.currentPair
    
  interface System.Collections.IDictionaryEnumerator with
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Key = __.key
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Value = __.value
    [<Browsable(false); EditorBrowsable(EditorBrowsableState.Never);>]
    override __.Entry = 
      if __.index = 0 || __.index = __.dictionary.count + 1 then
        System.InvalidOperationException("") |> raise
      System.Collections.DictionaryEntry(__.currentPair.key, __.currentPair.value)

  interface Funtom.Collections.IDictionaryEnumerator with
    override __.key = __.key
    override __.value = __.value
    override __.entry = __.entry
