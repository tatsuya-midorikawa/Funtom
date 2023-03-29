namespace Funtom.collections.internals

#nowarn "9"

open Microsoft.FSharp.NativeInterop

module core =
  let inline throw_empty() = raise (System.InvalidOperationException "The source is empty.")
  let inline debug_writel(str) = System.Diagnostics.Debug.WriteLine(str)

  type vec128 = System.Runtime.Intrinsics.Vector128
  type vec128<'T when 'T: unmanaged and 'T: struct and 'T: comparison and 'T: (new: unit -> 'T) and 'T:> System.ValueType> = System.Runtime.Intrinsics.Vector128<'T>
  type vec256 = System.Runtime.Intrinsics.Vector256
  type vec256<'T when 'T: unmanaged and 'T: struct and 'T: comparison and 'T: (new: unit -> 'T) and 'T:> System.ValueType> = System.Runtime.Intrinsics.Vector256<'T>
  
  let inline defaultof<'T> = Unchecked.defaultof<'T>