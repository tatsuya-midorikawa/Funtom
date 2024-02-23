namespace Funtom.winforms

(* ----------------------------------------
 * Anchors
 * ---------------------------------------- *)
[<System.Flags>]
type Anchors =
  | none   = 0
  | top    = (1 <<< 0)
  | bottom = (1 <<< 1)
  | left   = (1 <<< 2)
  | right  = (1 <<< 3)

module private Anchors =
  let convert (anchors: inref<Anchors>) =
    anchors |> (int >> enum<System.Windows.Forms.AnchorStyles>)



(* ----------------------------------------
 * Size
 * ---------------------------------------- *)
[<Struct; System.Runtime.CompilerServices.IsReadOnly>]
type Size = { width: int<px>; height: int<px>; }
with
  static member convert (size: inref<Size>) =
    System.Drawing.Size(width = int size.width, height= int size.height)



(* ----------------------------------------
 * Position
 * ---------------------------------------- *)
[<Struct; System.Runtime.CompilerServices.IsReadOnly>]
type Position = { top: int<px>; left: int<px>; }
with
  static member convert (location: inref<Position>) =
    System.Drawing.Point(X= int location.left, Y= int location.top)



(* ----------------------------------------
 * Location
 * ---------------------------------------- *)
[<Struct; System.Runtime.CompilerServices.IsReadOnly>]
type Location = { top: int<px>; left: int<px>; right: int<px>; bottom: int<px> }
with
  static member to_point (l: inref<Location>) =
    System.Drawing.Point(X= int l.left, Y= int l.top)
  static member to_size (l: inref<Location>) =
    System.Drawing.Size(Width= (int l.right) - (int l.left), Height= (int l.bottom) - (int l.top))


(* ----------------------------------------
 * Direction
 * ---------------------------------------- *)
type Direction =
  | left_to_right   = 0
  | top_down        = 1
  | ritght_to_left  = 2
  | bottom_up       = 3

module private Direction =
  let convert (direction: inref<Direction>) =
    direction |> (int >> enum<System.Windows.Forms.FlowDirection>)



(* ----------------------------------------
* Dock
* ---------------------------------------- *)
type Dock =
 | none   = 0
 | top    = 1
 | bottom = 2
 | left   = 3
 | right  = 4
 | fill   = 5

module private Dock =
  let convert (dock: inref<Dock>) =
    dock |> (int >> enum<System.Windows.Forms.DockStyle>)



(* ----------------------------------------
 * Property
 * ---------------------------------------- *)
type Property =
  | Anchor of Anchors
  | Direction of Direction
  | Dock of Dock
  | AutoSize of bool
  | Size of Size
  | Position of Position
  | Location of Location
  | Text of string
  | Name of string
  | Form of System.Windows.Forms.Form
  | Control of System.Windows.Forms.Control
  | Controls of System.Windows.Forms.Control list
  #if NET8_0_OR_GREATER
  | Command of System.Windows.Input.ICommand
  #endif
  #if NET48_OR_GREATER
  | Command of (obj -> unit)
  #endif
with
  member __.text =
    match __ with
    | Anchor anchor -> $"{anchor}"
    | Dock dock -> $"{dock}"
    | Size size -> $"{size}"
    | AutoSize auto_size -> $"{auto_size}"
    | Position position -> $"{position}"
    | Location location -> $"{location}"
    | Text text -> text
    | Name name -> name
    | Form form -> form.Text
    | Control c -> c.Text
    | _ -> exn $"This property is not supported." |> raise


[<AutoOpen>]
module Property =
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
  #if NET48_OR_GREATER
  let inline cmd (c: obj -> unit) = Command c
  #endif
