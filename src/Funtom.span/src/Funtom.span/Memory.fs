namespace Funtom.span

#nowarn "9"

open System
open System.Buffers
open System.Runtime.InteropServices
open FSharp.NativeInterop

module Memory =
  let mutable threshold = 128

  let inline malloc<'a when 'a: unmanaged> (size: int) : nativeptr<'a> =
    let size = if 0 < size then size else 0
    NativePtr.stackalloc<'a> size

  let inline stackalloc<'a when 'a: unmanaged> (size: int) : Span<'a> =
    let size = if 0 < size then size else 0
    let p = NativePtr.stackalloc<'a> size |> NativePtr.toVoidPtr
    Span<'a>(p, size)

  // If the threshold is not exceeded, memory is allocated from the stack.
  // If the threshold is exceeded, memory is allocated from the heap.
  let inline alloc<'a when 'a: unmanaged> (size: int) = 
    let size = if 0 < size then size else 0
    if size <= threshold then stackalloc size else Array.zeroCreate<'a>(size).AsSpan()
    
  let inline rent<'a when 'a: unmanaged> (size: int) =
    let size = if 0 < size then size else 0
    ArrayPool<'a>.Shared.Rent size
    
  let inline returns<'a when 'a: unmanaged> (ary: array<'a>) =
    ArrayPool<'a>.Shared.Return ary

  let inline ref (span: Span<'a>) = &(MemoryMarshal.GetReference span)
