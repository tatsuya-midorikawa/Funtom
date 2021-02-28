namespace Funtom.Collections.Generic

type IReadOnlyCollection<'T> =
  inherit System.Collections.Generic.IReadOnlyCollection<'T>
  inherit Funtom.Collections.Generic.IEnumerable<'T>
  abstract member count : int with get
