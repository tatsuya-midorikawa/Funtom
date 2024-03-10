(* ------------------------------------------------
 *        Funtom.winforms.lit ver. 0.0.1
 *             Apache-2.0 license
 *   2024 Tatsuya Midorikawa. All rights rserved
 * ------------------------------------------------ *)
namespace Funtom.winforms.lit

open System.Windows.Forms

(* ----------------------------------------
* Common types
* ---------------------------------------- *)
[<Measure>] type px

module Debug =
  let inline log msg = System.Diagnostics.Debug.WriteLine $"{msg}"

[<RequireQualifiedAccess>]
type evt =
  | click of (System.EventArgs -> unit) 

module Utilities =
  let binder (o: 't) = if o = null then None else Some o



(* ----------------------------------------
* MessageBox
* ---------------------------------------- *)
[<Struct>] type MessageButtons = | ok = 0 | ok_cancel = 1 | abort_retry_ignore = 2 | yes_no_cancel = 3 | yes_no = 4 | retry_cancel = 5
module private MessageButtons =
  let convert (button: MessageButtons) =
    button |> (int >> enum<System.Windows.Forms.MessageBoxButtons>)


[<Struct>] type MessageIcon = | none = 0 | hand = 16 | stop = 16 | error = 16 | question = 32 | exclamation = 48 | warning = 48 | asterisk = 64 | information = 64
module private MessageIcon =
  let convert (icon: MessageIcon) =
    icon |> (int >> enum<System.Windows.Forms.MessageBoxIcon>)


type msg () =
  static member show (text: string) = MessageBox.Show(text)  
  static member show (text: string, caption: string) = MessageBox.Show(text, caption)  
  static member show (text: string, caption: string, button: MessageButtons) =
    MessageBox.Show(text, caption, MessageButtons.convert button)
  static member show (text: string, caption: string, button: MessageButtons, icon: MessageIcon) =
    MessageBox.Show(text, caption, MessageButtons.convert button, MessageIcon.convert icon)
  static member show (owner: IWin32Window, text: string) = MessageBox.Show(owner, text)  
  static member show (owner: IWin32Window, text: string, caption: string) = MessageBox.Show(owner, text, caption)  
  static member show (owner: IWin32Window, text: string, caption: string, button: MessageButtons) =
    MessageBox.Show(owner, text, caption, MessageButtons.convert button)
  static member show (owner: IWin32Window, text: string, caption: string, button: MessageButtons, icon: MessageIcon) =
    MessageBox.Show(owner, text, caption, MessageButtons.convert button, MessageIcon.convert icon)



(* ----------------------------------------
 * Style
 * ---------------------------------------- *)
 // Anchors
[<System.Flags>]
type Anchors = | none = 0 | top = (1 <<< 0) | bottom = (1 <<< 1) | left = (1 <<< 2) | right = (1 <<< 3)
module private Anchors =
  let convert (anchors: inref<Anchors>) =
    anchors |> (int >> enum<System.Windows.Forms.AnchorStyles>)

 // Size
[<Struct; System.Runtime.CompilerServices.IsReadOnly>]
type Size = { width: int<px>; height: int<px>; }
with
  static member convert (size: inref<Size>) =
    System.Drawing.Size(width = int size.width, height= int size.height)

 // Position
[<Struct; System.Runtime.CompilerServices.IsReadOnly>]
type Position = { top: int<px>; left: int<px>; }
with
  static member convert (location: inref<Position>) =
    System.Drawing.Point(X= int location.left, Y= int location.top)

 // Location
[<Struct; System.Runtime.CompilerServices.IsReadOnly>]
type Location = { top: int<px>; left: int<px>; right: int<px>; bottom: int<px> }
with
  static member to_point (l: inref<Location>) =
    System.Drawing.Point(X= int l.left, Y= int l.top)
  static member to_size (l: inref<Location>) =
    System.Drawing.Size(Width= (int l.right) - (int l.left), Height= (int l.bottom) - (int l.top))
 
 // Direction
type Direction = | left_to_right = 0 | top_down = 1 | ritght_to_left = 2 | bottom_up = 3
module private Direction =
  let convert (direction: inref<Direction>) =
    direction |> (int >> enum<System.Windows.Forms.FlowDirection>)

// Dock
type Dock = | none = 0 | top = 1 | bottom = 2 | left = 3 | right = 4 | fill = 5
module private Dock =
  let convert (dock: inref<Dock>) =
    dock |> (int >> enum<System.Windows.Forms.DockStyle>)

type Style =
  | Anchor of Anchors
  | Direction of Direction
  | Dock of Dock
  | AutoSize of bool
  | Size of Size
  | Position of Position
  | Location of Location
  | Text of string
  | Name of string
  | Checked of bool
  | Index of int
  | Image of System.Drawing.Image
  | Icon of System.Drawing.Icon



(* ----------------------------------------
* Property
* ---------------------------------------- *)
type Property =
  | Styles of Style list
  | Form of System.Windows.Forms.Form
  | MenuStripItem of System.Windows.Forms.ToolStripMenuItem
  | MenuStrip of System.Windows.Forms.MenuStrip
  | FlowBreak of bool
  | WebView of Microsoft.Web.WebView2.WinForms.WebView2
  | Control of System.Windows.Forms.Control
  | Controls of System.Windows.Forms.Control list
  | Items of obj array
  | Uri of System.Uri
  | Command of (obj -> unit)
with
  member __.text =
    match __ with
      | Form form -> form.Text
      | Control c -> c.Text
      | MenuStripItem item -> item.Text
      | MenuStrip m -> m.Text
      | Uri uri -> uri.OriginalString
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
module property =
  let flow_break = FlowBreak true
  let inline cmd (c: obj -> unit) = Command c
  let inline style (styles: Style list) = Styles styles
  let inline items (list: obj list) = Items (list |> List.toArray)
  let inline ctrl (c: System.Windows.Forms.Control) = Control c

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
  let inline selected (c: bool) = Checked c
  let inline index (i: int) = Index i
  let inline bitmap (path: string) = Image (new System.Drawing.Bitmap(path))
  let inline icon (path: string) = Icon (new System.Drawing.Icon(path))
  let inline url (uri: string) = Uri (System.Uri (uri, System.UriKind.Absolute))
  
module Properties =
  let inline suspend_layout (property: Property) =
    match property with
      | Property.Form form -> form.SuspendLayout()
      | Property.MenuStrip menu -> menu.SuspendLayout()
      | Property.Control ctrl ->
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
      | Property.MenuStrip menu -> menu.ResumeLayout(perform); menu.PerformLayout()
      | Property.Control ctrl ->
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
  
  let inline cast2webview (property: Property) =
    match property with
      | WebView w -> w
      | _ -> exn $"This property is not supported: {property}" |> raise

  let inline enabled enabled (property: Property) =
    match property with
      | Property.Form form -> form.Enabled <- enabled
      | Property.MenuStripItem m -> m.Enabled <- enabled
      | Property.MenuStrip m -> m.Enabled <- enabled
      | Property.WebView w -> w.Enabled <- enabled
      | Property.Control c -> c.Enabled <- enabled
      | Property.Controls cs -> cs |> List.iter (fun c -> c.Enabled <- enabled)
      | _ -> exn $"This property is not supported: {property}" |> raise
    property



(* ----------------------------------------
 * Controls
 * ---------------------------------------- *)
[<AutoOpen>]
module controls =

  module internals =
    let apply (ctrl: System.Windows.Forms.Control) p =
      match p with
        | Styles styles ->
            let apply' = function
              | Anchor anchor -> ctrl.Anchor <- Anchors.convert &anchor
              | Dock dock -> ctrl.Dock <- Dock.convert &dock
              | Size size -> ctrl.Size <- Size.convert &size
              | AutoSize auto_size -> ctrl.AutoSize <- auto_size
              | Position location -> ctrl.Location <- Position.convert &location
              | Location location -> ctrl.Location <- Location.to_point &location; ctrl.Size <- Location.to_size &location
              | Text text -> ctrl.Text <- text
              | Name name -> ctrl.Name <- name
              | Image img -> ctrl.BackgroundImage <- img
              | _ -> ()
            styles |> List.iter apply'
        | FlowBreak _ -> ()
        | Form form -> ctrl.Controls.Add form
        | MenuStrip menu -> ctrl.Controls.Add menu
        | WebView wb -> ctrl.Controls.Add wb
        | Control c -> ctrl.Controls.Add c
        | Controls cs -> ctrl.Controls.AddRange (cs |> List.toArray)
        | _ -> exn $"This property is not supported: %A{p}" |> raise

  // Button
  module private Button =
    let apply (btn: System.Windows.Forms.Button) p =
      match p with
        | Command cmd -> btn.Click.Add cmd
        | _ -> internals.apply btn p

  let button (properties: Property list) =
    let btn = new System.Windows.Forms.Button()
    btn.SuspendLayout ()
    properties |> List.iter (Button.apply btn)
    btn.ResumeLayout false
    Control btn
    
  // FlowLayoutPanel
  module private FlowLayoutPanel =
    let apply (panel: System.Windows.Forms.FlowLayoutPanel) p =
      let apply' = function Direction d -> panel.FlowDirection <- Direction.convert &d | _ -> ()
      match p with
        | Styles s -> s |> List.iter apply'
        | FlowBreak b -> panel.SetFlowBreak(panel.Controls.[panel.Controls.Count - 1], b)
        | _ -> ()
      internals.apply panel p
    
  let flow_layout (properties: Property list) =
    let panel = new System.Windows.Forms.FlowLayoutPanel()
    panel.SuspendLayout ()
    properties |> List.iter (FlowLayoutPanel.apply panel)
    panel.ResumeLayout false
    Control panel

  // Label
  module private Label =
    let apply (lbl: System.Windows.Forms.Label) p =
      match p with
        | _ -> internals.apply lbl p

  let label (properties: Property list) =
    let lbl = new System.Windows.Forms.Label()
    lbl.SuspendLayout ()
    properties |> List.iter (Label.apply lbl)
    lbl.ResumeLayout false
    Control lbl

   // TextBox
  module private TextBox =
    let apply (txt: System.Windows.Forms.TextBox) p =
      match p with
        | _ -> internals.apply txt p

  let input (properties: Property list) =
    let txt = new System.Windows.Forms.TextBox()
    txt.SuspendLayout ()
    properties |> List.iter (TextBox.apply txt)
    txt.ResumeLayout false
    Control txt

  // CheckBox
  module private CheckBox =
    let apply (chk: System.Windows.Forms.CheckBox) p =
      match p with
        | Styles styles ->
          let apply' = function
            | Checked c -> chk.Checked <- c
            | _ -> internals.apply chk p
          styles |> List.iter apply'
        | _ -> internals.apply chk p

  let check_box (properties: Property list) =
    let chk = new System.Windows.Forms.CheckBox()
    chk.SuspendLayout ()
    properties |> List.iter (CheckBox.apply chk)
    chk.ResumeLayout false
    Control chk

  // ComboBox
  module private ComboBox =
    let apply (cmb: System.Windows.Forms.ComboBox) p =
      match p with
        | Items items -> cmb.Items.AddRange items
        | _ -> internals.apply cmb p

    let rec get_index (xs: Property list) =
      match xs with
        | [] -> None
        | x::ys ->
          match x with
            | Styles styles ->
                styles 
                |> List.tryFind (function Index i -> true | _ -> false)
                |> (function 
                    | Some style -> match style with Index i -> Some i | _ -> None
                    | None -> None)
            | _ -> get_index ys

  let combo_box (properties: Property list) =
    let cmb = new System.Windows.Forms.ComboBox()
    cmb.SuspendLayout ()
    properties |> List.iter (ComboBox.apply cmb)
    match ComboBox.get_index properties with Some i -> cmb.SelectedIndex <- i | None -> ()
    cmb.ResumeLayout false
    Control cmb

  // GroupBox
  module private GroupBox =
    let apply (gb: System.Windows.Forms.GroupBox) p =
      match p with
        | _ -> internals.apply gb p

  let group (properties: Property list) =
    let gb = new System.Windows.Forms.GroupBox()
    gb.SuspendLayout ()
    properties |> List.iter (GroupBox.apply gb)
    gb.ResumeLayout false
    Control gb

  // RadioButton
  module private RadioButton =
    let apply (rb: System.Windows.Forms.RadioButton) p =
      match p with
        | _ -> internals.apply rb p

  let radio_button (properties: Property list) =
    let rb = new System.Windows.Forms.RadioButton()
    rb.SuspendLayout ()
    properties |> List.iter (RadioButton.apply rb)
    rb.ResumeLayout false
    Control rb

  // MenuStrip
  module private MenuStrip =
    let apply (menu: System.Windows.Forms.MenuStrip) p =
      match p with
        | MenuStripItem item -> menu.Items.Add item |> ignore
        | Command cmd -> menu.Click.Add cmd
        | _ -> internals.apply menu p

  let menu (properties: Property list) =
    let menu = new System.Windows.Forms.MenuStrip()
    menu.SuspendLayout ()
    properties |> List.iter (MenuStrip.apply menu)
    menu.ResumeLayout false
    MenuStrip menu

  // MenuStripItem
  module private MenuStripItem =
    let apply (item: System.Windows.Forms.ToolStripMenuItem) p =
      match p with
        | Styles styles ->
          let apply' = function
            | Anchor anchor -> item.Anchor <- Anchors.convert &anchor
            | Dock dock -> item.Dock <- Dock.convert &dock
            | Size size -> item.Size <- Size.convert &size
            | AutoSize auto_size -> item.AutoSize <- auto_size
            | Text text -> item.Text <- text
            | Name name -> item.Name <- name
            | Image img -> item.Image <- img
            | _ -> exn $"This property is not supported: %A{p}" |> raise
          styles |> List.iter apply'
        | MenuStripItem item' -> item.DropDownItems.Add item' |> ignore
        | Command cmd -> item.Click.Add cmd
        | _ -> exn $"This property is not supported: %A{p}" |> raise

  let menu_item (properties: Property list) =
    let item = new System.Windows.Forms.ToolStripMenuItem()
    properties |> List.iter (MenuStripItem.apply item)
    MenuStripItem item

  // WebView2
  module private WebView2 =
    let apply (wb: Microsoft.Web.WebView2.WinForms.WebView2) p =
      match p with
        | Uri uri -> wb.Source <- uri
        | _ -> internals.apply wb p

  let webview2 (properties: Property list) =
    let wb = new Microsoft.Web.WebView2.WinForms.WebView2()
    (wb :> System.ComponentModel.ISupportInitialize).BeginInit()
    wb.AllowExternalDrop <- true
    wb.DefaultBackgroundColor <- System.Drawing.Color.White
    properties |> List.iter (WebView2.apply wb)
    (wb :> System.ComponentModel.ISupportInitialize).EndInit()
    WebView wb



(* ----------------------------------------
 * Forms
 * ---------------------------------------- *)
[<AutoOpen>]
module forms =
  let form (properties: Property list) =
    let f = new System.Windows.Forms.Form()
    f.SuspendLayout()
    f.AutoScaleDimensions <- System.Drawing.SizeF(7f, 15f)
    f.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font
    
    
    let mutable menu = None
    let mutable (panels) = ResizeArray<System.Windows.Forms.Panel>()
    let apply (form: System.Windows.Forms.Form) p =
      match p with
        | Styles styles ->
          let apply' = function
            | Icon ico -> form.Icon <- ico
            | _ -> internals.apply form p
          styles |> List.iter apply'
        | MenuStrip m -> menu <- Some m
        | Control ctrl ->
          match ctrl with
            | :? System.Windows.Forms.Panel as p -> panels.Add p
            | _ -> internals.apply form p
        | _ -> internals.apply form p

    properties |> Properties.suspend_layouts
    properties |> List.iter (apply f)
    
    // Panel -> MenuStrip でないと正しくレイアウトされないため、そこをハンドリングする
    panels |> Seq.iter (fun p -> f.Controls.Add p)
    menu |> Option.iter (fun m -> f.Controls.Add m; f.MainMenuStrip <- m)
    
    properties |> Properties.resume_layouts false
    f.ResumeLayout(false)
    Form f

  let show (property: Property) =
    match property with Form f -> f.Show(); property | _ -> exn $"This property is not supported: {property}" |> raise
    
  let show_dialog (property: Property) =
    match property with Form f -> f.ShowDialog() | _ -> exn $"This property is not supported: {property}" |> raise



(* ----------------------------------------
 * Documnts
 * ---------------------------------------- *)
module document =
  open System.Linq
  let rec private get_elem_by_id'' (id: string) (items:System.Windows.Forms.ToolStripItemCollection) =
    match items[id] with
    | null -> 
      let items = items.Cast<System.Windows.Forms.ToolStripMenuItem>() |> (Seq.cast >> Seq.toList)
      let rec dig (items: System.Windows.Forms.ToolStripMenuItem list) =
        match items with
        | [] -> None
        | head :: tail ->
            let (ret: Property option) = get_elem_by_id'' id head.DropDownItems
            if ret.IsSome then ret else dig tail
      dig items
    | item -> Some (MenuStripItem (item :?> System.Windows.Forms.ToolStripMenuItem))

  let rec private get_elem_by_id' (id: string) (ctrl: System.Windows.Forms.Control.ControlCollection) =
    match ctrl[id] with
      | null ->
        let ctrls = ctrl |> (Seq.cast >> Seq.toList)
        let rec dig (ctrls: System.Windows.Forms.Control list) =
          match ctrls with
          | [] -> None
          | head :: tail ->
              match head with
                | :? System.Windows.Forms.MenuStrip as menu ->
                    match get_elem_by_id'' id menu.Items with Some p -> Some p | _ -> dig tail
                | _ ->
                    let (ret: Property option) = get_elem_by_id' id head.Controls
                    if ret.IsSome then ret else dig tail
        dig ctrls
      | ctrl -> Some (Control ctrl)
      
  let rec get_elem_by_id (id: string) (property: Property) =
    match property with
      | Form form -> get_elem_by_id' id form.Controls
      | Control ctrl -> get_elem_by_id' id ctrl.Controls
      | _ -> exn $"This property is not supported: {property}" |> raise
    |> (fun p -> if p.IsSome then p.Value else exn $"Element with id '{id}' not found." |> raise)

  let inline add_event_listener (evt: evt) (property: Property) =
    let apply (c: System.Windows.Forms.Control) =
      match evt with
      | evt.click action -> c.Click.Add(action)

    let apply' (menu: System.Windows.Forms.ToolStripMenuItem) =
      match evt with
      | evt.click action -> menu.Click.Add(action)

    match property with
      | Control ctrl -> apply ctrl
      | Form form -> apply form
      | MenuStrip menu -> apply menu
      | MenuStripItem item -> apply' item
      | _ -> exn $"This property is not supported: {property}" |> raise
    
    property



(* ----------------------------------------
 * Dialogs
 * ---------------------------------------- *)
module dialogs =
  // FolderBrowserDialog
  type dir_browser () =
    let raw' = new System.Windows.Forms.FolderBrowserDialog()

    interface System.IDisposable with
      member __.Dispose() = raw'.Dispose()
      
    member __.show () = raw'.ShowDialog()
    member __.show (owner: System.Windows.Forms.IWin32Window) = raw'.ShowDialog(owner)
    member __.reset () = raw'.Reset()

    member __.raw with get() = raw'
    member __.selected with get() = raw'.SelectedPath and set v = raw'.SelectedPath <- v
    member __.description with get() = raw'.Description and set v = raw'.Description <- v
    member __.show_new_dir with get() = raw'.ShowNewFolderButton and set v = raw'.ShowNewFolderButton <- v
    member __.root_dir with get() = raw'.RootFolder and set v = raw'.RootFolder <- v

   // OpenFileDialog
  type file_dialog () =
    let raw' = new System.Windows.Forms.OpenFileDialog()

    interface System.IDisposable with
      member __.Dispose() = raw'.Dispose()

    interface System.Collections.Generic.IEnumerable<string> with
      member __.GetEnumerator() = (raw'.FileNames :> System.Collections.Generic.IEnumerable<string>).GetEnumerator()

    interface System.Collections.IEnumerable with
      member __.GetEnumerator() = raw'.FileNames.GetEnumerator()

    member __.show (?owner: System.Windows.Forms.IWin32Window) = match owner with Some o -> raw'.ShowDialog o | None -> raw'.ShowDialog ()
    member __.open_file () = new System.IO.StreamReader(raw'.OpenFile ())
    member __.reset () = raw'.Reset()

    member __.raw with get() = raw'
    member __.filter with get () = raw'.Filter and set v = raw'.Filter <- v
    member __.index with get () = raw'.FilterIndex and set v = raw'.FilterIndex <- v
    member __.init_dir with get () = raw'.InitialDirectory and set v = raw'.InitialDirectory <- v
    member __.restore_dir with get () = raw'.RestoreDirectory and set v = raw'.RestoreDirectory <- v
    member __.multi with get () = raw'.Multiselect and set v = raw'.Multiselect <- v
    member __.title with get () = raw'.Title and set v = raw'.Title <- v
    member __.filename with get () = raw'.FileName and set v = raw'.FileName <- v
    member __.filenames with get () = raw'.FileNames
    member __.readonly with get () = raw'.ReadOnlyChecked and set v = raw'.ReadOnlyChecked <- v
    member __.fileok(e: System.ComponentModel.CancelEventArgs -> unit) = raw'.FileOk.Add e

    member __.Item with get (i: int) = raw'.FileNames.[i]