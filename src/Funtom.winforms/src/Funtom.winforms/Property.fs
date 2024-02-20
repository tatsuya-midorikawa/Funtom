namespace Funtom.winforms

[<System.Flags>]
type Anchors =
  | none   = 0
  | top    = (1 <<< 0)
  | bottom = (1 <<< 1)
  | left   = (1 <<< 2)
  | right  = (1 <<< 3)

module private Anchors =
  let convert (anchors: inref<Anchors>) = anchors |> (int >> enum<System.Windows.Forms.AnchorStyles>)

[<Struct; System.Runtime.CompilerServices.IsReadOnly>]
type Size = { width: int<px>; height: int<px>; }
with
  static member convert (size: inref<Size>) =
    System.Drawing.Size(width = int size.width, height= int size.height)

[<Struct; System.Runtime.CompilerServices.IsReadOnly>]
type Location = { top: int<px>; left: int<px>; }
with
  static member convert (location: inref<Location>) =
    System.Drawing.Point(X= int location.left, Y= int location.top)

type Property =
  | Anchor of Anchors
  | Size of Size
  | Location of Location
  | Text of string
  | Name of string
  | Form of System.Windows.Forms.Form
  | Control of System.Windows.Forms.Control
  | Controls of System.Windows.Forms.Control array
  #if NET8_0_OR_GREATER
  | Command of System.Windows.Input.ICommand
  #endif
  #if NET48_OR_GREATER
  | Command of (obj -> unit)
  #endif

[<AutoOpen>]
module Property =
 let inline anchor (anchors: Anchors) = Anchor anchors
 let inline size (size: Size) = Size size
 let inline location (location: Location) = Location location
 let inline text (text: string) = Text text
 let inline id (name: string) = Name name   // id 関数をシャドウイングしてしまうので微妙...
 let inline name (name: string) = Name name
 let inline ctrl (c: System.Windows.Forms.Control) = Control c
 #if NET48_OR_GREATER
 let inline cmd (c: obj -> unit) = Command c
 #endif

module Ctrl =
  let apply (ctrl: System.Windows.Forms.Control) p =
    match p with
    | Anchor anchor -> ctrl.Anchor <- Anchors.convert &anchor
    | Size size -> ctrl.Size <- Size.convert &size
    | Location location -> ctrl.Location <- Location.convert &location
    | Text text -> ctrl.Text <- text
    | Name name -> ctrl.Name <- name
    | Form form -> ctrl.Controls.Add form
    | Control c -> ctrl.Controls.Add c
    | Controls cs -> ctrl.Controls.AddRange cs
    | _ -> exn $"This property is not supported: %A{p}" |> raise


    