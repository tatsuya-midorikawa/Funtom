// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

(*

The xxHash32 implementation is based on the code published by Yann Collet:
https://raw.githubusercontent.com/Cyan4973/xxHash/5c174cfa4e45a42f94082dc0d4539b39696afea1/xxhash.c

  xxHash - Fast Hash algorithm
  Copyright (C) 2012-2016, Yann Collet

  BSD 2-Clause License (http://www.opensource.org/licenses/bsd-license.php)

  Redistribution and use in source and binary forms, with or without
  modification, are permitted provided that the following conditions are
  met:

  * Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer.
  * Redistributions in binary form must reproduce the above
  copyright notice, this list of conditions and the following disclaimer
  in the documentation and/or other materials provided with the
  distribution.

  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
  OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
  LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
  OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

  You can contact the author at :
  - xxHash homepage: http://www.xxhash.com
  - xxHash source repository : https://github.com/Cyan4973/xxHash

*)
namespace Funtom

open System
open System.Security.Cryptography
open System.Collections.Generic
open Funtom.Numerics.BitOperations

[<Struct>]
type HashCode =
  val mutable private v1 : uint
  val mutable private v2 : uint
  val mutable private v3 : uint
  val mutable private v4 : uint
  val mutable private q1 : uint
  val mutable private q2 : uint
  val mutable private q3 : uint
  val mutable private length : uint

  member public __.add(value) =
    __.add(value.GetHashCode())
    
  member public __.add(value, comparer:IEqualityComparer<'T>) =
    __.add(comparer.GetHashCode(value))

  member private __.add(value:int) =
    let value = uint value
    let previousLength = __.length + 1u
    let position = previousLength % 4u

    match position with
    | 0u -> __.q1 <- value
    | 1u -> __.q2 <- value
    | 2u -> __.q3 <- value
    | _ -> 
      if previousLength = 3u then
        HashCode.initialize(&__.v1, &__.v2, &__.v3, &__.v4)
      __.v1 <- HashCode.round(__.v1, int __.q1)
      __.v2 <- HashCode.round(__.v2, int __.q2)
      __.v3 <- HashCode.round(__.v3, int __.q3)
      __.v4 <- HashCode.round(__.v4, int value)

    ()

  member public __.toHashCode() =
    let length = __.length
    let position = length % 4u
    let mutable hash = if length < 4u then HashCode.mixEmptyState() else HashCode.mixState(__.v1, __.v2, __.v3, __.v4)
    hash <- hash + length * 4u
    if 0u < position then
      hash <- HashCode.queueRound(hash, int __.q1)
      if 1u < position then
        hash <- HashCode.queueRound(hash, int __.q2)
        if 2u < position then
          hash <- HashCode.queueRound(hash, int __.q3)
    hash <- HashCode.mixFinal(hash)
    int hash

  static member private seed = HashCode.generateGlobalSeed()
  static member private prime1 = 2654435761u
  static member private prime2 = 2246822519u
  static member private prime3 = 3266489917u
  static member private prime4 = 668265263u
  static member private prime5 = 374761393u

  static member private generateGlobalSeed() =
    use rng = new RNGCryptoServiceProvider()
    let buffer = Array.zeroCreate<byte> sizeof<int>
    rng.GetBytes(buffer)
    BitConverter.ToUInt32(buffer,  0)

  static member private mixState(v1:uint, v2:uint, v3:uint, v4:uint) =
    rotateLeft(v1, 1) + rotateLeft(v2, 7) + rotateLeft(v3, 12) + rotateLeft(v4, 18)

  static member private mixFinal(hash:uint) =
    let mutable h = hash
    h <- h ^^^ (h >>> 15)
    h <- h * HashCode.prime2
    h <- h ^^^ (h >>> 13)
    h <- h * HashCode.prime3
    h <- h ^^^ (h >>> 16)
    h

  static member private mixEmptyState() =
    HashCode.seed + HashCode.prime5
    
  static member private round(hash: uint, value: int) =
    rotateLeft(hash + (uint value) * HashCode.prime2, 13) * HashCode.prime1

  static member private queueRound(hash: uint, value: int) =
    rotateLeft(hash + (uint value) * HashCode.prime3, 17) * HashCode.prime4

  static member private initialize(v1:outref<uint>, v2:outref<uint>, v3:outref<uint>, v4:outref<uint>) =
    v1 <- HashCode.seed + HashCode.prime1 + HashCode.prime2
    v2 <- HashCode.seed + HashCode.prime2
    v3 <- HashCode.seed
    v4 <- HashCode.seed - HashCode.prime1

  static member public combine(v1) =
    let hc1 = v1.GetHashCode()

    let mutable hash = HashCode.mixEmptyState()
    hash <- hash + 4u
    hash <- HashCode.queueRound(hash, hc1)
    hash <- HashCode.mixFinal(hash)
    int hash
    
  static member public combine(v1, v2) =
    let hc1 = v1.GetHashCode()
    let hc2 = v2.GetHashCode()

    let mutable hash = HashCode.mixEmptyState()
    hash <- hash + 8u
    hash <- HashCode.queueRound(hash, hc1)
    hash <- HashCode.queueRound(hash, hc2)
    hash <- HashCode.mixFinal(hash)
    int hash
    
  static member public combine(v1, v2, v3) =
    let hc1 = v1.GetHashCode()
    let hc2 = v2.GetHashCode()
    let hc3 = v3.GetHashCode()

    let mutable hash = HashCode.mixEmptyState()
    hash <- hash + 12u
    hash <- HashCode.queueRound(hash, hc1)
    hash <- HashCode.queueRound(hash, hc2)
    hash <- HashCode.queueRound(hash, hc3)
    hash <- HashCode.mixFinal(hash)
    int hash
    
  static member public combine(v1, v2, v3, v4) =
    let mutable v1 = v1
    let mutable v2 = v2
    let mutable v3 = v3
    let mutable v4 = v4

    let hc1 = v1.GetHashCode()
    let hc2 = v2.GetHashCode()
    let hc3 = v3.GetHashCode()
    let hc4 = v4.GetHashCode()

    HashCode.initialize(&v1, &v2, &v3, &v4)

    v1 <- HashCode.round(v1, hc1)
    v2 <- HashCode.round(v2, hc2)
    v3 <- HashCode.round(v3, hc3)
    v4 <- HashCode.round(v4, hc4)

    let mutable hash = HashCode.mixState(v1, v2, v3, v4)
    hash <- hash + 16u

    hash <- HashCode.mixFinal(hash)
    int hash
    
  static member public combine(v1, v2, v3, v4, v5) =
    let mutable v1 = v1
    let mutable v2 = v2
    let mutable v3 = v3
    let mutable v4 = v4

    let hc1 = v1.GetHashCode()
    let hc2 = v2.GetHashCode()
    let hc3 = v3.GetHashCode()
    let hc4 = v4.GetHashCode()
    let hc5 = v5.GetHashCode()

    HashCode.initialize(&v1, &v2, &v3, &v4)

    v1 <- HashCode.round(v1, hc1)
    v2 <- HashCode.round(v2, hc2)
    v3 <- HashCode.round(v3, hc3)
    v4 <- HashCode.round(v4, hc4)

    let mutable hash = HashCode.mixState(v1, v2, v3, v4)
    hash <- hash + 20u

    hash <- HashCode.queueRound(hash, hc5)
    hash <- HashCode.mixFinal(hash)
    int hash
    
  static member public combine(v1, v2, v3, v4, v5, v6) =
    let mutable v1 = v1
    let mutable v2 = v2
    let mutable v3 = v3
    let mutable v4 = v4

    let hc1 = v1.GetHashCode()
    let hc2 = v2.GetHashCode()
    let hc3 = v3.GetHashCode()
    let hc4 = v4.GetHashCode()
    let hc5 = v5.GetHashCode()
    let hc6 = v6.GetHashCode()

    HashCode.initialize(&v1, &v2, &v3, &v4)

    v1 <- HashCode.round(v1, hc1)
    v2 <- HashCode.round(v2, hc2)
    v3 <- HashCode.round(v3, hc3)
    v4 <- HashCode.round(v4, hc4)

    let mutable hash = HashCode.mixState(v1, v2, v3, v4)
    hash <- hash + 24u

    hash <- HashCode.queueRound(hash, hc5)
    hash <- HashCode.queueRound(hash, hc6)
    hash <- HashCode.mixFinal(hash)
    int hash
    
  static member public combine(v1, v2, v3, v4, v5, v6, v7) =
    let mutable v1 = v1
    let mutable v2 = v2
    let mutable v3 = v3
    let mutable v4 = v4

    let hc1 = v1.GetHashCode()
    let hc2 = v2.GetHashCode()
    let hc3 = v3.GetHashCode()
    let hc4 = v4.GetHashCode()
    let hc5 = v5.GetHashCode()
    let hc6 = v6.GetHashCode()
    let hc7 = v7.GetHashCode()

    HashCode.initialize(&v1, &v2, &v3, &v4)

    v1 <- HashCode.round(v1, hc1)
    v2 <- HashCode.round(v2, hc2)
    v3 <- HashCode.round(v3, hc3)
    v4 <- HashCode.round(v4, hc4)

    let mutable hash = HashCode.mixState(v1, v2, v3, v4)
    hash <- hash + 28u

    hash <- HashCode.queueRound(hash, hc5)
    hash <- HashCode.queueRound(hash, hc6)
    hash <- HashCode.queueRound(hash, hc7)
    hash <- HashCode.mixFinal(hash)
    int hash
    
  static member public combine(v1, v2, v3, v4, v5, v6, v7, v8) =
    let mutable v1 = v1
    let mutable v2 = v2
    let mutable v3 = v3
    let mutable v4 = v4

    let hc1 = v1.GetHashCode()
    let hc2 = v2.GetHashCode()
    let hc3 = v3.GetHashCode()
    let hc4 = v4.GetHashCode()
    let hc5 = v5.GetHashCode()
    let hc6 = v6.GetHashCode()
    let hc7 = v7.GetHashCode()
    let hc8 = v8.GetHashCode()

    HashCode.initialize(&v1, &v2, &v3, &v4)

    v1 <- HashCode.round(v1, hc1)
    v2 <- HashCode.round(v2, hc2)
    v3 <- HashCode.round(v3, hc3)
    v4 <- HashCode.round(v4, hc4)

    v1 <- HashCode.round(v1, hc5)
    v2 <- HashCode.round(v2, hc6)
    v3 <- HashCode.round(v3, hc7)
    v4 <- HashCode.round(v4, hc8)

    let mutable hash = HashCode.mixState(v1, v2, v3, v4)
    hash <- hash + 32u

    hash <- HashCode.mixFinal(hash)
    int hash

