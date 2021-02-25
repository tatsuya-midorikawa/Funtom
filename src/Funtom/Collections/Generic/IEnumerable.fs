namespace Funtom.Collections.Generic

type IEnumerable<'T> =
  inherit System.Collections.Generic.IEnumerable<'T>
  abstract member getEnumerator : unit -> IEnumerator<'T>
