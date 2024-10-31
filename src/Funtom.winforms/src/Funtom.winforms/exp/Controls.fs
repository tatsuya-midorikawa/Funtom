namespace Funtom.winforms.exp

open Funtom.winforms

module Control =
  let inline suspendLayout<^T when ^T: (member SuspendLayout: unit -> unit)>
    (ctrl: ^T) =
      ctrl.SuspendLayout()
      ctrl

  let inline resumeLayout<^T when ^T: (member ResumeLayout: bool -> unit)>
    (performLayout: bool)
    (ctrl: ^T) =
      ctrl.ResumeLayout(performLayout)
      ctrl

  let inline add<^T when ^T: (member Controls: System.Windows.Forms.Control.ControlCollection)>
    (child: #System.Windows.Forms.Control)
    (ctrl: ^T) =
      ctrl.Controls.Add(child)
      ctrl

  let inline show<^T when ^T: (member Show: unit -> unit)>
    (ctrl: ^T) =
      ctrl.Show()
      ctrl

  let inline showDialog<^T when ^T: (member ShowDialog: unit -> System.Windows.Forms.DialogResult)>
    (ctrl: ^T) =
      ctrl.ShowDialog()

  let inline autoScaleDimensions<^T when ^T :> System.Windows.Forms.ContainerControl>
    (size: SizeF)
    (ctrl: ^T) =
      ctrl.AutoScaleDimensions <- (SizeF.toNative size)
      ctrl

  let inline autoScaleMode<^T when ^T :> System.Windows.Forms.ContainerControl>
    (mode: AutoScaleMode)
    (ctrl: ^T) =
      ctrl.AutoScaleMode <- (AutoScaleMode.toNative mode)
      ctrl

module Form =
  let inline suspendLayout (form: System.Windows.Forms.Form) = Control.suspendLayout form
  let inline resumeLayout (performLayout: bool) (form: System.Windows.Forms.Form) = Control.resumeLayout performLayout form
  let inline add (child: #System.Windows.Forms.Control) (form: System.Windows.Forms.Form) = Control.add child form
  let inline show (form: System.Windows.Forms.Form) = Control.show form
  let inline showDialog (form: System.Windows.Forms.Form) = Control.showDialog form
  let inline autoScaleDimensions (size: SizeF) (form: System.Windows.Forms.Form) = Control.autoScaleDimensions size form
  let inline autoScaleMode (mode: AutoScaleMode) (form: System.Windows.Forms.Form) = Control.autoScaleMode mode form

module Button =
  let inline suspendLayout (btn: System.Windows.Forms.Button) = Control.suspendLayout btn
  let inline resumeLayout (performLayout: bool) (btn: System.Windows.Forms.Button) = Control.resumeLayout performLayout btn
  let inline add (child: #System.Windows.Forms.Control) (btn: System.Windows.Forms.Button) = Control.add child btn
  

