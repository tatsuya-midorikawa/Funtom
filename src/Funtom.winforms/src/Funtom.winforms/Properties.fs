namespace Funtom.winforms

(* ----------------------------------------
 * Property
 * ---------------------------------------- *)
type Property =
  | Styles of Style list
  | Form of System.Windows.Forms.Form
  | MenuStripItem of System.Windows.Forms.ToolStripMenuItem
  | MenuStrip of System.Windows.Forms.MenuStrip
  | FlowBreak of bool
  | Control of System.Windows.Forms.Control
  | Controls of System.Windows.Forms.Control list
  | Items of obj array
  #if NET8_0_OR_GREATER
  | Command of System.Windows.Input.ICommand
  #endif
  #if NET48_OR_GREATER
  | Command of (obj -> unit)
  #endif
with
  member __.text =
    match __ with
      | Form form -> form.Text
      | Control c -> c.Text
      | _ -> exn $"This property is not supported." |> raise

  member __.add_event_listener (evt: evt) =
    let apply (c: System.Windows.Forms.Control) =
      match evt with
        | evt.click action -> c.Click.Add(action)
    match __ with
      | Control ctrl -> apply ctrl
      | Form form -> apply form
      | _ -> exn $"This property is not supported: {__}" |> raise
    __


[<AutoOpen>]
module PropertyTools =
  let inline style (styles: Style list) = Styles styles
  let inline anchor (anchors: Anchors) = Anchor anchors
  let inline direction (direction: Direction) = Direction direction
  let inline dock (dock: Dock) = Dock dock
  let inline auto_size (auto_size: bool) = AutoSize auto_size
  let inline size (size: Size) = Size size
  let inline position (pos: Position) = Position pos
  let inline location (loc: Location) = Location loc
  let inline text (text: string) = Text text
  let inline id (name: string) = Name name   // id 関数をシャドウイングしてしまうので微妙...
  let inline name (name: string) = Name name
  let inline ctrl (c: System.Windows.Forms.Control) = Control c
  let inline bitmap (path: string) = Image (new System.Drawing.Bitmap(path))
  let inline icon (path: string) = Icon (new System.Drawing.Icon(path))
  let inline selected (c: bool) = Checked c
  let inline items (list: obj list) = Items (list |> List.toArray)
  let inline index (i: int) = Index i
  let flow_break = FlowBreak true
  #if NET48_OR_GREATER
  let inline cmd (c: obj -> unit) = Command c
  #endif

module Properties =
  let (| Forms |) (property: Property) =
    match property with Form form -> form | _ -> exn $"This property is not matched." |> raise

  let (| MenuStripItem |) (property: Property) =
    match property with MenuStripItem item -> item | _ -> exn $"This property is not matched." |> raise

  let inline suspend_layout (property: Property) =
    match property with
      | Property.Form form -> form.SuspendLayout()
      | MenuStrip menu -> menu.SuspendLayout()
      | Control ctrl ->
        match ctrl with
          | :? System.Windows.Forms.GroupBox as c -> c.SuspendLayout()
          | :? System.Windows.Forms.Panel as c -> c.SuspendLayout()
          | _ -> ()
      | _ -> ()
  
  let rec suspend_layouts (properties: Property list) =
    match properties with
      | [] -> ()
      | head::tail ->
        suspend_layout head
        suspend_layouts tail
 
  let inline resume_layout (perform) (property: Property) =
    match property with
      | Property.Form form -> form.ResumeLayout(perform)
      | MenuStrip menu -> menu.ResumeLayout(perform); menu.PerformLayout()
      | Control ctrl ->
        match ctrl with
          | :? System.Windows.Forms.GroupBox as c -> c.ResumeLayout(perform); c.PerformLayout()
          | :? System.Windows.Forms.Panel as c -> c.ResumeLayout(perform)
          | _ -> ()
      | _ -> ()
    
  let rec resume_layouts (perform) (properties: Property list) =
    match properties with
      | [] -> ()
      | head::tail ->
        resume_layout perform head
        resume_layouts perform tail

  let inline enabled enabled (property: Property) =
    match property with
      | Property.Form form -> form.Enabled <- enabled
      | MenuStripItem m -> m.Enabled <- enabled
      | MenuStrip m -> m.Enabled <- enabled
      | Control c -> c.Enabled <- enabled
      | Controls cs -> cs |> List.iter (fun c -> c.Enabled <- enabled)
      | _ -> exn $"This property is not supported: {property}" |> raise
    property