namespace Funtom.Threading.Tasks

[<Sealed>]
type Task private() =
  static member await (async:Async<'T>) =
      async

  static member await (task:System.Threading.Tasks.Task) =
      Async.AwaitTask task

  static member await (task:System.Threading.Tasks.Task<'T>) =
      Async.AwaitTask task
      
#if NETSTANDARD2_1
  static member await (task:System.Threading.Tasks.ValueTask) =
      Async.AwaitTask (task.AsTask())

  static member await (task:System.Threading.Tasks.ValueTask<'T>) =
      Async.AwaitTask (task.AsTask())
#endif

