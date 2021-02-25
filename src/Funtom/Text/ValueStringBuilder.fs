namespace Funtom.Text

open System
open System.Buffers
open System.Diagnostics
open System.Runtime.CompilerServices
open System.Runtime.InteropServices


[<Struct; IsByRefLike>]
type ValueStringBuilder =
  val mutable private disposed : bool
  val mutable private pool : char[]
  val mutable private chars : Span<char>
  val mutable private pos : int

  new(capacity') =
    let pool' = ArrayPool<char>.Shared.Rent(capacity')
    { pool = pool'; chars = pool'.AsSpan(); pos = 0; disposed = false; }
    
  new(buffer') =
    { pool = ArrayPool<char>.Shared.Rent(0); chars = buffer'; pos = 0; disposed = false; }
    
  static member inline (!>) (x:^a) : ^b = ((^a or ^b) : (static member op_Implicit : ^a -> ^b) x) 

  member public __.length
    with get() = __.pos
    and set(value) =
      Debug.Assert(0 <= value)
      Debug.Assert(value <= __.chars.Length)
      __.pos <- value

  member public __.capacity = __.chars.Length
  
  member public __.grow(beyond) =
    Debug.Assert(0 < beyond)
    Debug.Assert(__.chars.Length - beyond < __.pos, "Grow called incorrectly, no resize is needed.")
    let mutable pool = ArrayPool<char>.Shared.Rent(Math.Max(__.pos + beyond, __.chars.Length * 2))
    __.chars.Slice(0, __.pos).CopyTo(pool.AsSpan())
    let mutable r = __.pool
    __.pool <- pool
    __.chars <- __.pool.AsSpan()
    if r <> null then
      ArrayPool<char>.Shared.Return(r)

  member public __.ensureCapacity(capacity) =
    if __.chars.Length < capacity then
      __.grow(capacity - __.pos)

  member public __.getPinnableReference() =
    &MemoryMarshal.GetReference(__.chars)
    
  member public __.getPinnableReference(terminate) =
    if terminate then
      __.ensureCapacity(__.length + 1)
      __.chars.[__.length] <- '\000'
    &MemoryMarshal.GetReference(__.chars)

  member public __.Item with get(index) =
    Debug.Assert(index < __.pos)
    &__.chars.[index]
    
  member public __.rawChars = __.chars

  member inline public __.asSpan(terminate) : ReadOnlySpan<char> =
    if terminate then
      __.ensureCapacity(__.length + 1)
      __.chars.[__.length] <- '\000'
    Span<char>.op_Implicit(__.chars.Slice(0, __.pos))

  member inline public __.asSpan() : ReadOnlySpan<char> =  Span<char>.op_Implicit(__.chars.Slice(0, __.pos))
  member inline public __.asSpan(start) : ReadOnlySpan<char> =  Span<char>.op_Implicit(__.chars.Slice(start, __.pos - start))
  member inline public __.asSpan(start, length) : ReadOnlySpan<char> =  Span<char>.op_Implicit(__.chars.Slice(start, length))
  
  member public __.tryCopyTo(destination:Span<char>, written:outref<int>) =
    if __.chars.Slice(0, __.pos).TryCopyTo(destination) then
      written <- __.pos
      __.dispose(true)
      true
    else
      written <- 0
      __.dispose(true)
      false

  member public __.insert(index, value, count) =
    if __.chars.Length - count < __.pos then
      __.grow(count)
      
    let remaining = __.pos - index
    __.chars.Slice(index, remaining).CopyTo(__.chars.Slice(index + count))
    __.chars.Slice(index, count).Fill(value)
    __.pos <- __.pos + count
    
  member public __.insert(index, str:string) =
    if str = null then
      ()
    else
      let count = str.Length
      if __.chars.Length - count < __.pos then
        __.grow(count)
      let remaining = __.pos - index
      __.chars.Slice(index, remaining).CopyTo(__.chars.Slice(index + count))
      str.AsSpan().CopyTo(__.chars.Slice(index))
      __.pos <- __.pos + count

  member inline public __.append(c) =
    let pos = __.pos
    if pos < __.chars.Length then
      __.chars.[pos] <- c
      __.pos <- pos + 1
    else
      __.growAndAppend(c)
      
  member inline public __.append(str:string) =
    if str = null then
      ()
    else
      let pos = __.pos
      if str.Length = 1 && pos < __.chars.Length then
        __.chars.[pos] <- str.[0]
        __.pos <- pos + 1
      else
        __.appendSlow(str)

  member public __.append(c, count) =
    if __.chars.Length - count < __.pos then
      __.grow(count)
    let mutable dst = __.chars.Slice(__.pos, count)
    for i = 0 to dst.Length - 1 do
      dst.[i] <- c
    __.pos <- __.pos + count
    
  member public __.append(value: ReadOnlySpan<char>) =
    let pos = __.pos
    if __.chars.Length - value.Length < pos then
      __.grow(value.Length)
    value.CopyTo(__.chars.Slice(__.pos))
    __.pos <- __.pos + value.Length

  member inline public __.appendSpan(length) =
    let pos = __.pos
    if __.chars.Length - length < pos then
      __.grow(length)
    __.pos <- pos + length
    __.chars.Slice(pos, length)

  member private __.appendSlow(str:string) =
    let pos = __.pos
    if __.chars.Length - str.Length < pos then
      __.grow(str.Length)

    str.AsSpan().CopyTo(__.chars.Slice(pos))
    __.pos <- __.pos + str.Length

  member inline private __.growAndAppend(c) =
    __.grow(1)
    __.append(c)

  member inline __.toString() =
    let str = __.chars.Slice(0, __.pos).ToString()
    __.dispose(true)
    str

  override __.ToString() = __.toString()

  member inline __.dispose(disposing:bool) =
    if __.disposed then
      ()
    else
      if disposing then
        if __.pool <> null then
          ArrayPool<char>.Shared.Return(__.pool)
      __.disposed <- true
      
  interface IDisposable with
    member __.Dispose() = __.dispose(true)
