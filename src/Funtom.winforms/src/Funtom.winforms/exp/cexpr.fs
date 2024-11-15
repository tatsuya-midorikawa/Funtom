namespace Funtom.winforms

open Funtom.winforms.exp

[<AutoOpen>]
module Cexpr =
  type ControlBuilder<'T when 'T :> System.Windows.Forms.Control> (ctrl: 'T) =
    member __.Yield (x) = x
    member __.For (x, f) = f x
    member __.Zero () = __
    
    [<CustomOperation>]
    member __.suspend (_) = ctrl.SuspendLayout(); ctrl
    [<CustomOperation>]
    member __.resume (_, ?perform: bool) = ctrl.ResumeLayout(perform |> Option.defaultWith (fun () -> false)); ctrl
    [<CustomOperation>]
    member __.add (_, child: System.Windows.Forms.Control) = ctrl.Controls.Add child; ctrl
    [<CustomOperation>]
    member __.add (_, children: System.Windows.Forms.Control[]) = ctrl.Controls.AddRange children; ctrl
    [<CustomOperation>]
    member __.text (_, text: string) = ctrl.Text <- text; ctrl

  let form = ControlBuilder (new System.Windows.Forms.Form())
  let button = ControlBuilder (new System.Windows.Forms.Button())