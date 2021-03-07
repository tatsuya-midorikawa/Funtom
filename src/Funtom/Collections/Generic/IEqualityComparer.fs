namespace Funtom.Collections.Generic

type IEqualityComparer<'T> =
  inherit System.Collections.Generic.IEqualityComparer<'T>
  abstract member equals : x: 'T * y: 'T -> bool
  abstract member getHashCode : obj: 'T -> int
