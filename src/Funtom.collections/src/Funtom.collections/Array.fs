namespace Funtom.collections

#nowarn "9"

module Array =
  open System.Runtime.Intrinsics
  open Microsoft.FSharp.NativeInterop
  open Funtom.collections.internals.core

  let inline max<^T when ^T: unmanaged and ^T: struct and ^T: comparison and ^T: (new: unit -> ^T) and ^T:> System.ValueType>
    (src: array<^T>) =
      if src = defaultof<_> || src.Length = 0
        then throw_empty()

      if not vec128.IsHardwareAccelerated || src.Length < vec128<^T>.Count
        // Not SIMD
        then
          let mutable max = src[0]
          for n in src do if max < n then max <- n
          max
        elif not vec256.IsHardwareAccelerated || src.Length < vec256<^T>.Count
          // SIMD : 128bit
          then            
            use p = fixed &src[0]
            let mutable current = NativePtr.toNativeInt p
            // let padding = 16n

            let mutable best = Vector128.Load p
            let lastp = current + nativeint ((src.Length - Vector128<^T>.Count) * sizeof<^T>) 
            let last = Vector128.Load (lastp |> NativePtr.ofNativeInt<^T>)
              
            while current < lastp do
              current <- current + 16n
              best <- Vector128.Max<^T>(best, Vector128.Load (current |> NativePtr.ofNativeInt<^T>))
            
            let best = Vector128.Max<^T>(best, last)
            let best = Vector64.Max<^T>(best.GetLower(), best.GetUpper())
            let mutable max = best[0]
            for i = 1 to Vector64<^T>.Count - 1 do
              if max < best[i] then max <- best[i]
            max
          // SIMD : 256bit
          else
            use p = fixed &src[0]
            let mutable current = NativePtr.toNativeInt p
            // let padding = 32n

            let mutable best = Vector256.Load p
            let lastp = current + nativeint ((src.Length - Vector256<^T>.Count) * sizeof<^T>) 
            let last = Vector256.Load (lastp |> NativePtr.ofNativeInt<^T>)
              
            while current < lastp do
              current <- current + 32n
              best <- Vector256.Max<^T>(best, Vector256.Load (current |> NativePtr.ofNativeInt<^T>))
            
            let best = Vector256.Max<^T>(best, last)
            let best = Vector128.Max<^T>(best.GetLower(), best.GetUpper())
            let best = Vector64.Max<^T>(best.GetLower(), best.GetUpper())
            let mutable max = best[0]
            for i = 1 to Vector64<^T>.Count - 1 do
              if max < best[i] then max <- best[i]
            max

  let inline min<^T when ^T: unmanaged and ^T: struct and ^T: comparison and ^T: (new: unit -> ^T) and ^T:> System.ValueType>
    (src: array<^T>) =
      if src = defaultof<_> || src.Length = 0
        then throw_empty()

      if not vec128.IsHardwareAccelerated || src.Length < vec128<^T>.Count
        // Not SIMD
        then
          let mutable min = src[0]
          for n in src do if n < min then min <- n
          min
        elif not vec256.IsHardwareAccelerated || src.Length < vec256<^T>.Count
          // SIMD : 128bit
          then
            use p = fixed &src[0]
            let mutable current = NativePtr.toNativeInt p
            // let padding = 16n

            let mutable worst = Vector128.Load p
            let lastp = current + nativeint ((src.Length - Vector128<^T>.Count) * sizeof<^T>) 
            let last = Vector128.Load (lastp |> NativePtr.ofNativeInt<^T>)
              
            while current < lastp do
              current <- current + 16n
              worst <- Vector128.Min<^T>(worst, Vector128.Load (current |> NativePtr.ofNativeInt<^T>))
            
            let worst = Vector128.Min<^T>(worst, last)
            let worst = Vector64.Min<^T>(worst.GetLower(), worst.GetUpper())
            let mutable min = worst[0]
            for i = 1 to Vector64<^T>.Count - 1 do
              if worst[i] < min then min <- worst[i]
            min
          // SIMD : 256bit
          else            
            use p = fixed &src[0]
            let mutable current = NativePtr.toNativeInt p
            // let padding = 32n

            let mutable worst = Vector256.Load p
            let lastp = current + nativeint ((src.Length - Vector256<^T>.Count) * sizeof<^T>) 
            let last = Vector256.Load (lastp |> NativePtr.ofNativeInt<^T>)
              
            while current < lastp do
              current <- current + 32n
              worst <- Vector256.Min<^T>(worst, Vector256.Load (current |> NativePtr.ofNativeInt<^T>))
            
            let worst = Vector256.Min<^T>(worst, last)
            let worst = Vector128.Min<^T>(worst.GetLower(), worst.GetUpper())
            let worst = Vector64.Min<^T>(worst.GetLower(), worst.GetUpper())
            let mutable min = worst[0]
            for i = 1 to Vector64<^T>.Count - 1 do
              if worst[i] < min then min <- worst[i]
            min

  let inline sum<^T when ^T: unmanaged and ^T: struct and ^T: comparison and ^T: (new: unit -> ^T) and ^T:> System.ValueType and ^T:> System.Numerics.INumber<^T>>
    (src: array<^T>) =
      if src = defaultof<_>
        then throw_empty()
      
      let sum = 'T.Zero
      // // 256bit SIMD supported
      // ref var begin = ref MemoryMarshal.GetReference(source);
      let p = fixed &src[0]
      let mutable current = NativePtr.toNativeInt p

      // ref var last = ref Unsafe.Add(ref begin, source.Length);
      let endp = current + nativeint (src.Length * sizeof<^T>)      
      let lastp = current + nativeint ((src.Length - Vector256<^T>.Count) * sizeof<^T>) 

      // ref var current = ref begin;
      // var vectorSum = Vector256<T>.Zero;
      let mutable vsum = Vector256<^T>.Zero

      // ref var to = ref Unsafe.Add(ref begin, source.Length - Vector256<T>.Count);
      // while (Unsafe.IsAddressLessThan(ref current, ref to))
      // {
      //     vectorSum += Vector256.LoadUnsafe(ref current);
      //     current = ref Unsafe.Add(ref current, Vector256<T>.Count);
      // }
      while current < endp do
        vsum <- vsum + Vector256.Load(current |> NativePtr.ofNativeInt<^T>)
        current <- current + 32n

      // while (Unsafe.IsAddressLessThan(ref current, ref last))
      // {
      //     unchecked // SIMD operation is unchecked so keep same behaviour
      //     {
      //         sum += current;
      //     }
      //     current = ref Unsafe.Add(ref current, 1);
      // }
      while current < lastp do
        // Microsoft.FSharp.Core.Operators.(+) sum current
        ()


      // sum += Vector256.Sum(vectorSum);

      sum


      // if not vec128.IsHardwareAccelerated || src.Length < vec128<^T>.Count
      //   // Not SIMD
      //   then
      //     let mutable max = src[0]
      //     for n in src do if max < n then max <- n
      //     max
      //   elif not vec256.IsHardwareAccelerated || src.Length < vec256<^T>.Count
      //     // SIMD : 128bit
      //     then            
      //       use p = fixed &src[0]
      //       let mutable current = NativePtr.toNativeInt p
      //       // let padding = 16n

      //       let mutable best = Vector128.Load p
      //       let lastp = current + nativeint ((src.Length - Vector128<^T>.Count) * sizeof<^T>) 
      //       let last = Vector128.Load (lastp |> NativePtr.ofNativeInt<^T>)
              
      //       while current < lastp do
      //         current <- current + 16n
      //         best <- Vector128.Max<^T>(best, Vector128.Load (current |> NativePtr.ofNativeInt<^T>))
            
      //       let best = Vector128.Max<^T>(best, last)
      //       let best = Vector64.Max<^T>(best.GetLower(), best.GetUpper())
      //       let mutable max = best[0]
      //       for i = 1 to Vector64<^T>.Count - 1 do
      //         if max < best[i] then max <- best[i]
      //       max
      //     // SIMD : 256bit
      //     else
      //       use p = fixed &src[0]
      //       let mutable current = NativePtr.toNativeInt p
      //       // let padding = 32n

      //       let mutable best = Vector256.Load p
      //       let lastp = current + nativeint ((src.Length - Vector256<^T>.Count) * sizeof<^T>) 
      //       let last = Vector256.Load (lastp |> NativePtr.ofNativeInt<^T>)
              
      //       while current < lastp do
      //         current <- current + 32n
      //         best <- Vector256.Max<^T>(best, Vector256.Load (current |> NativePtr.ofNativeInt<^T>))
            
      //       let best = Vector256.Max<^T>(best, last)
      //       let best = Vector128.Max<^T>(best.GetLower(), best.GetUpper())
      //       let best = Vector64.Max<^T>(best.GetLower(), best.GetUpper())
      //       let mutable max = best[0]
      //       for i = 1 to Vector64<^T>.Count - 1 do
      //         if max < best[i] then max <- best[i]
      //       max