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
    member __.children<'U when 'U :> System.Windows.Forms.Control> (_, children: 'U[]) =
      children |> Array.iter (fun c -> ctrl.Controls.Add c); ctrl
    [<CustomOperation>]
    member __.text (_, text: string) = ctrl.Text <- text; ctrl
    [<CustomOperation>]
    member __.autosize (_, enabled: bool) = ctrl.AutoSize <- enabled; ctrl
    [<CustomOperation>]
    member __.anchor (_, anchors: Anchors) = ctrl.Anchor <- (Anchors.toNative anchors); ctrl
    [<CustomOperation>]
    member __.dock (_, dock: Dock) = ctrl.Dock <- (Dock.toNative dock); ctrl
    [<CustomOperation>]
    member __.size (_, size: Size) = ctrl.Size <- (Size.toNative size); ctrl
    [<CustomOperation>]
    member __.location (_, location: Position) = ctrl.Location <- (Position.toNative location); ctrl
    [<CustomOperation>]
    member __.image (_, img: System.Drawing.Image) = ctrl.BackgroundImage <- img; ctrl
    [<CustomOperation>]
    member __.name (_, name: string) = ctrl.Name <- name; ctrl
    [<CustomOperation>]
    member __.bgimage (_, img: System.Drawing.Image) = ctrl.BackgroundImage <- img; ctrl


  type FormBuilder (form: System.Windows.Forms.Form) = 
    inherit ControlBuilder<System.Windows.Forms.Form> (form)
    [<CustomOperation>]
    member __.activate (_) = form.Activate(); form

    
  type ButtonBuilder (buton: System.Windows.Forms.Button) = 
    inherit ControlBuilder<System.Windows.Forms.Button> (buton)


  type FlowLayoutPanel (panel: System.Windows.Forms.FlowLayoutPanel) = 
    inherit ControlBuilder<System.Windows.Forms.FlowLayoutPanel> (panel)
    [<CustomOperation>]
    member __.flow_break (_, break': bool) = panel.SetFlowBreak(panel.Controls.[panel.Controls.Count - 1], break'); panel
    [<CustomOperation>]
    member __.direction (_, direction: Direction) = panel.FlowDirection <- Direction.toNative direction; panel

  let form ()= FormBuilder (new System.Windows.Forms.Form())
  let button () = ButtonBuilder (new System.Windows.Forms.Button())
  let flowlayout (dock: Dock, direction: Direction) = FlowLayoutPanel (
    new System.Windows.Forms.FlowLayoutPanel(FlowDirection = Direction.toNative direction, Dock = Dock.toNative dock))