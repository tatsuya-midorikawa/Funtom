namespace Funtom.Collections

type IDictionaryEnumerator =
  inherit System.Collections.IDictionaryEnumerator
  inherit Funtom.Collections.IEnumerator
  abstract member key : obj with get
  abstract member value : obj with get
  abstract member entry : DictionaryEntry with get

