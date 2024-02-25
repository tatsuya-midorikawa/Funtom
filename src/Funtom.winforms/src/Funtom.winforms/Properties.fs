namespace Funtom.winforms

(* ----------------------------------------
 * Property
 * ---------------------------------------- *)
type Property =
  | Styles of Style list
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
module Property =
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
  #if NET48_OR_GREATER
  let inline cmd (c: obj -> unit) = Command c
  #endif
