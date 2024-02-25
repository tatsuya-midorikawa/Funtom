﻿namespace Funtom.winforms

(* ----------------------------------------
 * Property
 * ---------------------------------------- *)
type Property =
  | Styles of Style array
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
