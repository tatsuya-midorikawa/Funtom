namespace Funtom.Collections

type IEqualityComparer =
  inherit System.Collections.IEqualityComparer
  abstract member equals : x: obj * y: obj -> bool
  abstract member getHashCode : obj: obj -> int
