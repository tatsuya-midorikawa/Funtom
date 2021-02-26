namespace Funtom.Collections.Generic

type IEnumerable<'T> =
  inherit System.Collections.Generic.IEnumerable<'T>
  inherit Funtom.Collections.IEnumerable
  abstract member getEnumerator : unit -> IEnumerator<'T>
