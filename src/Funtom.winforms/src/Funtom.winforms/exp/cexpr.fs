namespace Funtom.winforms

open Funtom.winforms.exp
open Funtom.winforms.values

[<AutoOpen>]
module Cexpr =
  type ControlBuilder<'T when 'T :> System.Windows.Forms.Control> (ctrl: 'T) =
    member __.Yield (x) = x
    member __.For (x, f) = f x
    member __.Zero () = ctrl
    
    [<CustomOperation>]
    member __.suspend (_) = ctrl.SuspendLayout(); ctrl
    [<CustomOperation>]
    member __.resume (_, ?perform: bool) = ctrl.ResumeLayout(perform |> Option.defaultWith (fun () -> false)); ctrl
    [<CustomOperation>]
    member __.add (_, child: System.Windows.Forms.Control) = ctrl.Controls.Add child; ctrl
    [<CustomOperation>]
    member __.add<'U when 'U :> System.Windows.Forms.Control> (_, children: 'U[]) =
      children |> Array.iter (fun c -> ctrl.Controls.Add c); ctrl
    [<CustomOperation>]
    member __.text (_, text: string) = ctrl.Text <- text; ctrl
    [<CustomOperation>]
    member __.auto_size (_, enabled: bool) = ctrl.AutoSize <- enabled; ctrl
    [<CustomOperation>]
    member __.anchor (_, anchors: Anchors) = ctrl.Anchor <- (Anchors.toNative anchors); ctrl

  let form = ControlBuilder (new System.Windows.Forms.Form())
  let button = ControlBuilder (new System.Windows.Forms.Button())