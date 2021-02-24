// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Funtom.Numerics

module BitOperations =
  let inline public rotateLeft (value:^T, offset:int) = 
    (value <<< offset) ||| (value >>> (sizeof< ^T> * 8 - offset))
    
  let inline public rotateRight (value:^T, offset:int) = 
    (value >>> offset) ||| (value <<< (sizeof< ^T> * 8 - offset))
