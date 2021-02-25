namespace Funtom.Reflection

open System.Collections.Generic

type Assembly =
  static member private loadfile = Dictionary<string, Assembly>()
