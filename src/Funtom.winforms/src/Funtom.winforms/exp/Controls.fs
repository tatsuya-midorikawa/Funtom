namespace Funtom.winforms.exp

open Funtom.winforms.values

[<RequireQualifiedAccess>]
module Control =
  let inline suspendLayout<^T when ^T: (member SuspendLayout: unit -> unit)>
    (ctrl: ^T) =
      ctrl.SuspendLayout(); ctrl

  let inline resumeLayout<^T when ^T: (member ResumeLayout: bool -> unit)>
    (performLayout: bool)
    (ctrl: ^T) =
      ctrl.ResumeLayout(performLayout); ctrl

  let inline add<^T when ^T: (member Controls: System.Windows.Forms.Control.ControlCollection with get)>
    (child: System.Windows.Forms.Control)
    (ctrl: ^T) =
      ctrl.get_Controls().Add(child); ctrl

  let inline show<^T when ^T: (member Show: unit -> unit)>
    (ctrl: ^T) =
      ctrl.Show(); ctrl

  let inline showDialog<^T when ^T: (member ShowDialog: unit -> System.Windows.Forms.DialogResult)>
    (ctrl: ^T) =
      ctrl.ShowDialog()

  let inline autoScaleDimensions<^T when ^T: (member set_AutoScaleDimensions: System.Drawing.SizeF -> unit)>
    (size: SizeF)
    (ctrl: ^T) =
      ctrl.set_AutoScaleDimensions (SizeF.toNative size); ctrl

  let inline autoScaleMode<^T when ^T: (member set_AutoScaleMode: System.Windows.Forms.AutoScaleMode -> unit)>
    (mode: AutoScaleMode)
    (ctrl: ^T) =
      ctrl.set_AutoScaleMode (AutoScaleMode.toNative mode); ctrl

  let inline autoSize<^T when ^T: (member set_AutoSize: bool -> unit)>
    (autoSize: bool)
    (ctrl: ^T) =
      ctrl.set_AutoSize autoSize; ctrl

  let inline anchor<^T when ^T: (member set_Anchor: System.Windows.Forms.AnchorStyles -> unit)>
    (anchors: Anchors)
    (ctrl: ^T) =
      ctrl.set_Anchor (Anchors.toNative anchors); ctrl
  
  let inline dock<^T when ^T: (member set_Dock: System.Windows.Forms.DockStyle -> unit)>
    (dock: Dock)
    (ctrl: ^T) =
      ctrl.set_Dock (Dock.toNative dock); ctrl

  let inline direction<^T when ^T: (member set_FlowDirection: System.Windows.Forms.FlowDirection -> unit)>
    (direction: Direction)
    (ctrl: ^T) =
      ctrl.set_FlowDirection (Direction.toNative direction); ctrl
      
  let inline location<^T when ^T: (member set_Location: System.Drawing.Point -> unit)>
    (position: Position)
    (ctrl: ^T) =
      ctrl.set_Location (Position.toNative position); ctrl

  let inline text<^T when ^T: (member set_Text: string -> unit)>
    (text: string)
    (ctrl: ^T) =
      ctrl.set_Text text; ctrl

  let inline check<^T when ^T: (member set_Checked: bool -> unit)>
    (checked': bool)
    (ctrl: ^T) =
      ctrl.set_Checked checked'; ctrl

  let inline items<^T, ^U when ^T: (member get_Items:unit -> ^U when ^U :> System.Collections.IEnumerable)>
    (ctrl: ^T) =
      ctrl.get_Items()

  let inline controls<^T, ^U when ^T: (member get_Controls:unit -> ^U when ^U :> System.Windows.Forms.Control.ControlCollection)>
    (ctrl: ^T) =
      ctrl.get_Controls()

  let inline mainMenuStrip<^T when ^T: (member set_MainMenuStrip: System.Windows.Forms.MenuStrip -> unit)>
    (menu: System.Windows.Forms.MenuStrip)
    (ctrl: ^T) =
      ctrl.set_MainMenuStrip menu; ctrl

  let inline contextMenuStrip<^T when ^T: (member set_ContextMenuStrip: System.Windows.Forms.ContextMenuStrip -> unit)>
    (menu: System.Windows.Forms.ContextMenuStrip)
    (ctrl: ^T) =
      ctrl.set_ContextMenuStrip menu; ctrl



[<RequireQualifiedAccess>]
module Form =
  let inline suspendLayout (form: System.Windows.Forms.Form) = Control.suspendLayout form
  let inline resumeLayout (performLayout: bool) (form: System.Windows.Forms.Form) = Control.resumeLayout performLayout form
  let inline add (child: System.Windows.Forms.Control) (form: System.Windows.Forms.Form) = Control.add child form
  let inline show (form: System.Windows.Forms.Form) = Control.show form
  let inline showDialog (form: System.Windows.Forms.Form) = Control.showDialog form
  let inline autoScaleDimensions (size: SizeF) (form: System.Windows.Forms.Form) = Control.autoScaleDimensions size form
  let inline autoScaleMode (mode: AutoScaleMode) (form: System.Windows.Forms.Form) = Control.autoScaleMode mode form
  let inline autoSize (autoSize: bool) (form: System.Windows.Forms.Form) = Control.autoSize autoSize form
  let inline anchor (anchors: Anchors) (form: System.Windows.Forms.Form) = Control.anchor anchors form
  let inline dock (dock: Dock) (form: System.Windows.Forms.Form) = Control.dock dock form
  let inline location (position: Position) (form: System.Windows.Forms.Form) = form.Location <- (Position.toNative position); form
  let inline text (text: string) (form: System.Windows.Forms.Form) = Control.text text form
  let inline controls (form: System.Windows.Forms.Form) = Control.controls form
  let inline mainMenuStrip (menu: System.Windows.Forms.MenuStrip) (form: System.Windows.Forms.Form) = Control.mainMenuStrip menu form
  let inline contextMenuStrip (menu: System.Windows.Forms.ContextMenuStrip) (form: System.Windows.Forms.Form) = Control.contextMenuStrip menu form


[<RequireQualifiedAccess>]
module FlowLayoutPanel =
  let inline suspendLayout (panel: System.Windows.Forms.FlowLayoutPanel) = Control.suspendLayout panel
  let inline resumeLayout (performLayout: bool) (panel: System.Windows.Forms.FlowLayoutPanel) = Control.resumeLayout performLayout panel
  let inline add (child: System.Windows.Forms.Control) (panel: System.Windows.Forms.FlowLayoutPanel) = Control.add child panel
  let inline autoSize (autoSize: bool) (panel: System.Windows.Forms.FlowLayoutPanel) = Control.autoSize autoSize panel
  let inline anchor (anchors: Anchors) (panel: System.Windows.Forms.FlowLayoutPanel) = Control.anchor anchors panel
  let inline dock (dock: Dock) (panel: System.Windows.Forms.FlowLayoutPanel) = Control.dock dock panel
  let inline direction (direction: Direction) (panel: System.Windows.Forms.FlowLayoutPanel) = Control.direction direction panel
  let inline location (position: Position) (panel: System.Windows.Forms.FlowLayoutPanel) = Control.location position panel
  let inline text (text: string) (panel: System.Windows.Forms.FlowLayoutPanel) = Control.text text panel
  let inline flowBreak (break': bool) (panel: System.Windows.Forms.FlowLayoutPanel) = panel.SetFlowBreak(panel.Controls.[panel.Controls.Count - 1], break'); panel
  let inline controls (panel: System.Windows.Forms.FlowLayoutPanel) = Control.controls panel
  let inline contextMenuStrip (menu: System.Windows.Forms.ContextMenuStrip) (panel: System.Windows.Forms.FlowLayoutPanel) = Control.contextMenuStrip menu panel


[<RequireQualifiedAccess>]
module Button =
  let inline suspendLayout (btn: System.Windows.Forms.Button) = Control.suspendLayout btn
  let inline resumeLayout (performLayout: bool) (btn: System.Windows.Forms.Button) = Control.resumeLayout performLayout btn
  let inline add (child: System.Windows.Forms.Control) (btn: System.Windows.Forms.Button) = Control.add child btn
  let inline autoSize (autoSize: bool) (btn: System.Windows.Forms.Button) = Control.autoSize autoSize btn
  let inline anchor (anchors: Anchors) (btn: System.Windows.Forms.Button) = Control.anchor anchors btn
  let inline dock (dock: Dock) (btn: System.Windows.Forms.Button) = Control.dock dock btn
  let inline location (position: Position) (btn: System.Windows.Forms.Button) = Control.location position btn
  let inline text (text: string) (btn: System.Windows.Forms.Button) = Control.text text btn
  let inline controls (btn: System.Windows.Forms.Button) = Control.controls btn
  let inline contextMenuStrip (menu: System.Windows.Forms.ContextMenuStrip) (btn: System.Windows.Forms.Button) = Control.contextMenuStrip menu btn
  

[<RequireQualifiedAccess>]
module CheckBox =
  let inline suspendLayout (chk: System.Windows.Forms.CheckBox) = Control.suspendLayout chk
  let inline resumeLayout (performLayout: bool) (chk: System.Windows.Forms.CheckBox) = Control.resumeLayout performLayout chk
  let inline add (child: System.Windows.Forms.Control) (chk: System.Windows.Forms.CheckBox) = Control.add child chk
  let inline autoSize (autoSize: bool) (chk: System.Windows.Forms.CheckBox) = Control.autoSize autoSize chk
  let inline anchor (anchors: Anchors) (chk: System.Windows.Forms.CheckBox) = Control.anchor anchors chk
  let inline dock (dock: Dock) (chk: System.Windows.Forms.CheckBox) = Control.dock dock chk
  let inline location (position: Position) (chk: System.Windows.Forms.CheckBox) = Control.location position chk
  let inline text (text: string) (chk: System.Windows.Forms.CheckBox) = Control.text text chk
  let inline check (checked': bool) (chk: System.Windows.Forms.CheckBox) = Control.check checked' chk

[<RequireQualifiedAccess>]
module ComboBox =
  let inline suspendLayout (cbo: System.Windows.Forms.ComboBox) = Control.suspendLayout cbo
  let inline resumeLayout (performLayout: bool) (cbo: System.Windows.Forms.ComboBox) = Control.resumeLayout performLayout cbo
  let inline add (child: System.Windows.Forms.Control) (cbo: System.Windows.Forms.ComboBox) = Control.add child cbo
  let inline autoSize (autoSize: bool) (cbo: System.Windows.Forms.ComboBox) = Control.autoSize autoSize cbo
  let inline anchor (anchors: Anchors) (cbo: System.Windows.Forms.ComboBox) = Control.anchor anchors cbo
  let inline dock (dock: Dock) (cbo: System.Windows.Forms.ComboBox) = Control.dock dock cbo
  let inline location (position: Position) (cbo: System.Windows.Forms.ComboBox) = Control.location position cbo
  let inline text (text: string) (cbo: System.Windows.Forms.ComboBox) = Control.text text cbo
  let inline items (cbo: System.Windows.Forms.ComboBox) = Control.items cbo

[<RequireQualifiedAccess>]
module MenuStrip =
  let inline suspendLayout (menu: System.Windows.Forms.MenuStrip) = Control.suspendLayout menu
  let inline resumeLayout (performLayout: bool) (menu: System.Windows.Forms.MenuStrip) = Control.resumeLayout performLayout menu
  let inline autoSize (autoSize: bool) (menu: System.Windows.Forms.MenuStrip) = Control.autoSize autoSize menu
  let inline anchor (anchors: Anchors) (menu: System.Windows.Forms.MenuStrip) = Control.anchor anchors menu
  let inline dock (dock: Dock) (menu: System.Windows.Forms.MenuStrip) = Control.dock dock menu
  let inline location (position: Position) (menu: System.Windows.Forms.MenuStrip) = Control.location position menu
  let inline text (text: string) (menu: System.Windows.Forms.MenuStrip) = Control.text text menu
  let inline items (menu: System.Windows.Forms.MenuStrip) = Control.items menu











