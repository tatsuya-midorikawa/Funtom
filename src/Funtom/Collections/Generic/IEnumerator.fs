namespace Funtom.Collections.Generic

type IEnumerator<'T> =
  inherit System.Collections.Generic.IEnumerator<'T>
  inherit Funtom.Collections.IEnumerator
  inherit Funtom.IDisposable
  abstract member current : 'T with get
