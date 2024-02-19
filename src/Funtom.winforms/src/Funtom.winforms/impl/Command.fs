namespace Funtom.winforms

open System
open System.Windows.Input

#if NET8_0_OR_GREATER
type cmd (exec: (cmd * obj option) -> unit, ?can_exec: obj option -> bool) =
  let event = Event<EventHandler, EventArgs>()
  let can_exec = match can_exec with Some e -> e | None -> fun _ -> true

  interface ICommand with
    member __.Execute(p) = 
      (__, Option.ofObj p) |> exec
      __.notify()
    member __.CanExecute(p) = p |> (Option.ofObj >> can_exec)
    [<CLIEvent>]
    member __.CanExecuteChanged = event.Publish

  member __.notify () = event.Trigger(__, null)

  static member op_Implicit (c: cmd) = Command c
  
  static member build (exec: obj option -> unit, ?can_exec: obj option -> bool, ?can_exec_changed: EventArgs option -> unit) =
    let can_exec = match can_exec with Some e -> e | None -> fun _ -> true
    let cmd =
      { new ICommand with
          member __.Execute(p) = p |> (Option.ofObj >> exec)
          member __.CanExecute(p) = p |> (Option.ofObj >> can_exec)
          [<CLIEvent>]
          member __.CanExecuteChanged = Event<EventHandler, EventArgs>().Publish }
    match can_exec_changed with Some e -> cmd.CanExecuteChanged.Add (Option.ofObj >> e) | None -> ()
    Command cmd
#endif