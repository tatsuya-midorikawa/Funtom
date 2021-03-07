namespace Funtom

type IEquatable<'T> =
  inherit System.IEquatable<'T>
  abstract member equals : other: 'T -> bool
