namespace Funtom.winforms

open System
open System.Windows.Input

type cmd (exec: obj option -> unit, ?can_exec: obj option -> bool, ?can_exec_changed: EventArgs option -> unit) =
  let canExecuteChanged = Event<EventHandler, EventArgs>().Publish
  let can_exec = match can_exec with Some e -> e | None -> fun _ -> true
  
  do
    match can_exec_changed with Some e -> canExecuteChanged.Add (Option.ofObj >> e) | None -> ()

  interface ICommand with
    member __.Execute(p) = p |> (Option.ofObj >> exec)
    member __.CanExecute(p) = p |> (Option.ofObj >> can_exec)
    [<CLIEvent>]
    member __.CanExecuteChanged = canExecuteChanged

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