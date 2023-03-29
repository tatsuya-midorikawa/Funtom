namespace Funtom.collections

#nowarn "9"

module Array =
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
            let mutable best = vec128.Load p
            let mutable last = vec128.Load(NativePtr.add p (src.Length - vec128<^T>.Count))
            for i = 1 to src.Length / vec128<^T>.Count - 1 do
              best <- vec128.Max<^T>(best, NativePtr.add p (i * vec128<^T>.Count) |> vec128.Load)
            best <- vec128.Max<^T>(best, last)
            let mutable max = best[0]
            let mutable i = 1
            while i < vec128<^T>.Count do
              if max < best[i] then max <- best[i]
              i <- i + 1
            max
          // SIMD : 256bit
          else
            use p = fixed &src[0]
            let mutable best = vec256.Load p
            let mutable last = vec256.Load(NativePtr.add p (src.Length - vec256<^T>.Count))
            for i = 1 to src.Length / vec256<^T>.Count - 1 do
              best <- vec256.Max<^T>(best, NativePtr.add p (i * vec256<^T>.Count) |> vec256.Load)
            best <- vec256.Max<^T>(best, last)
            let mutable max = best[0]
            let mutable i = 1
            while i < vec256<^T>.Count do
              if max < best[i] then max <- best[i]
              i <- i + 1
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
            let mutable worst = vec128.Load p
            let mutable last = vec128.Load(NativePtr.add p (src.Length - vec128<^T>.Count))
            for i = 1 to src.Length / vec128<^T>.Count - 1 do
              worst <- vec128.Min<^T>(worst, NativePtr.add p (i * vec128<^T>.Count) |> vec128.Load)
            worst <- vec128.Min<^T>(worst, last)
            let mutable min' = worst[0]
            let mutable i = 1
            while i < vec128<^T>.Count do
              if worst[i] < min' then min' <- worst[i]
              i <- i + 1
            min'
          // SIMD : 256bit
          else
            use p = fixed &src[0]
            let mutable worst = vec256.Load p
            let mutable last = vec256.Load(NativePtr.add p (src.Length - vec256<^T>.Count))
            for i = 1 to src.Length / vec256<^T>.Count - 1 do
              worst <- vec256.Min<^T>(worst, NativePtr.add p (i * vec256<^T>.Count) |> vec256.Load)
            worst <- vec256.Min<^T>(worst, last)
            let mutable min' = worst[0]
            let mutable i = 1
            while i < vec256<^T>.Count do
              if worst[i] < min' then min' <- worst[i]
              i <- i + 1
            min'
