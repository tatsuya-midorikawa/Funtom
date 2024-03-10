namespace Funtom.winforms.lit

open System.Windows.Forms

(* ----------------------------------------
 * Common types
 * ---------------------------------------- *)
[<Measure>] type px
[<RequireQualifiedAccess>] type evt = | click of (System.EventArgs -> unit) 


(* ----------------------------------------
 * MessageBox
 * ---------------------------------------- *)
[<Struct>] type MessageButtons = | ok = 0 | ok_cancel = 1 | abort_retry_ignore = 2 | yes_no_cancel = 3 | yes_no = 4 | retry_cancel = 5
[<Struct>] type MessageIcon = | none = 0 | hand = 16 | stop = 16 | error = 16 | question = 32 | exclamation = 48 | warning = 48 | asterisk = 64 | information = 64

type msg =
  new: unit -> msg
  static member show: text:string -> DialogResult
  static member show: text:string * caption:string -> DialogResult
  static member show: text:string * caption:string * button:MessageButtons -> DialogResult
  static member show: text:string * caption:string * button:MessageButtons * icon:MessageIcon -> DialogResult
  static member show: owner:IWin32Window * text:string -> DialogResult
  static member show: owner:IWin32Window * text:string * caption:string -> DialogResult
  static member show: owner:IWin32Window * text:string * caption:string * button:MessageButtons -> DialogResult
  static member show: owner:IWin32Window * text:string * caption:string * button:MessageButtons * icon:MessageIcon -> DialogResult



(* ----------------------------------------
 * Styles
 * ---------------------------------------- *)
[<System.Flags>] type Anchors = | none = 0 | top = (1 <<< 0) | bottom = (1 <<< 1) | left = (1 <<< 2) | right = (1 <<< 3)
[<Struct; System.Runtime.CompilerServices.IsReadOnly>] type Size = { width: int<px>; height: int<px> }
[<Struct; System.Runtime.CompilerServices.IsReadOnly>] type Position = { top: int<px>; left: int<px> }
[<Struct; System.Runtime.CompilerServices.IsReadOnly>] type Location = { top: int<px>; left: int<px>; right: int<px>; bottom: int<px> }
type Direction = | left_to_right = 0 | top_down = 1 | ritght_to_left = 2 | bottom_up = 3
type Dock = | none = 0 | top = 1 | bottom = 2 | left = 3 | right = 4 | fill = 5

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
    member text : string
    member add_event_listener : evt -> Property

[<AutoOpen>]
module property =
  val flow_break : Property
  val inline cmd : (obj -> unit) -> Property
  val inline style : Style list -> Property
  val inline items : obj list -> Property
  val inline ctrl : System.Windows.Forms.Control -> Property

  val inline anchor : Anchors -> Style
  val inline direction : Direction -> Style
  val inline dock : Dock -> Style
  val inline auto_size : bool -> Style
  val inline size : Size -> Style
  val inline position : Position -> Style
  val inline location : Location -> Style
  val inline text : string -> Style
  val inline id : string -> Style
  val inline name : string -> Style
  val inline selected : bool -> Style
  val inline index : int -> Style
  val inline bitmap : string -> Style
  val inline icon : string -> Style
  val inline url : string -> Property

module Properties =
  val inline suspend_layout : Property -> unit
  val suspend_layouts : Property list -> unit
  val inline resume_layout : bool -> Property -> unit
  val resume_layouts : bool -> Property list -> unit
  val inline cast2webview : Property -> Microsoft.Web.WebView2.WinForms.WebView2
  val inline enabled : bool -> Property -> Property

(* ----------------------------------------
 * Controls
 * ---------------------------------------- *)
[<AutoOpen>]
module controls =
  val button : Property list -> Property
  val flow_layout : Property list -> Property
  val label : Property list -> Property
  val input : Property list -> Property
  val check_box : Property list -> Property
  val combo_box : Property list -> Property
  val group : Property list -> Property
  val radio_button : Property list -> Property
  val menu : Property list -> Property
  val menu_item : Property list -> Property
  val webview2 : Property list -> Property


  
(* ----------------------------------------
 * Forms
 * ---------------------------------------- *)
[<AutoOpen>]
module forms =
  val form : Property list -> Property
  val show : Property -> Property
  val show_dialog : Property -> DialogResult


  
(* ----------------------------------------
 * Documents
 * ---------------------------------------- *)
module document =
  val get_elem_by_id : string -> Property -> Property
  val inline add_event_listener : evt -> Property -> Property



(* ----------------------------------------
 * Dialogs
 * ---------------------------------------- *)
module dialogs =
  type dir_browser =
    interface System.IDisposable
    new : unit -> dir_browser
    member show : unit -> DialogResult
    member show : IWin32Window -> DialogResult
    member reset : unit -> unit

    member raw : System.Windows.Forms.FolderBrowserDialog with get
    member selected : string with get, set
    member description : string with get, set
    member show_new_dir : bool with get, set
    member root_dir : System.Environment.SpecialFolder with get, set

  type file_dialog =
    interface System.IDisposable
    interface System.Collections.Generic.IEnumerable<string>
    interface System.Collections.IEnumerable
    new : unit -> file_dialog
    member show : IWin32Window option -> DialogResult
    member open_file : unit -> System.IO.StreamReader
    member reset : unit -> unit

    member raw : System.Windows.Forms.OpenFileDialog with get
    member filter : string with get, set
    member index : int with get, set
    member init_dir : string with get, set
    member restore_dir : bool with get, set
    member multi : bool with get, set
    member title : string with get, set
    member filename : string with get, set
    member filenames : string []
    member readonly : bool with get, set
    member fileok : (System.ComponentModel.CancelEventArgs -> unit) -> unit
    member readonly : bool with get, set
    member Item : int -> string with get