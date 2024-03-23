namespace Funtom.winforms.exp

open System.Windows.Forms
open System.Runtime.CompilerServices

[<Measure>] type px

[<System.Flags>]
type Anchor = none= 0 | top= (1 <<< 0) | bottom= (1 <<< 1) | left= (1 <<< 2) | right= (1 <<< 3)
module Anchor = let cast (anchors: Anchor) =  anchors |> (int >> enum<AnchorStyles>)

type Direction = left_to_right= 0 | top_down= 1 | ritght_to_left= 2 | bottom_up= 3
module Direction = let cast (direction: Direction) = direction |> (int >> enum<FlowDirection>)

type Dock = none= 0 | top= 1 | bottom= 2 | left= 3 | right= 4 | fill= 5
module Dock = let cast (dock: Dock) = dock |> (int >> enum<DockStyle>)

type Alignment = topleft= (1 <<< 0)    | topcenter= (1 <<< 1)    | topright= (1 <<< 2)
               | midleft= (1 <<< 4)    | midcenter= (1 <<< 5)    | midright= (1 <<< 6)
               | bottomleft= (1 <<< 8) | bottomcenter= (1 <<< 9) | bottomright= (1 <<< 10)   
module Alignment = let cast (align: Alignment) = align |> (int >> enum<System.Drawing.ContentAlignment>)

[<Struct; IsReadOnly;>]
type Size = { width: int<px>; height: int<px> }
  with member __.convert () = System.Drawing.Size (int __.width, int __.height)

[<Struct; IsReadOnly;>]
type Location = { x: int<px>; y: int<px> }
  with member __.convert () = System.Drawing.Point (int __.x, int __.y)

[<Struct; IsReadOnly;>]
type Style =
  | Size of size: System.Drawing.Size
  | Location of location: System.Drawing.Point
  | Anchor of anchor: System.Windows.Forms.AnchorStyles
  | Directon of direction: System.Windows.Forms.FlowDirection
  | Dock of dock: System.Windows.Forms.DockStyle
  | AutoSize of auto: bool
  | Text of text: string
  | Name of name: string
  | Image of image: (System.Drawing.Image * System.Drawing.ContentAlignment)
  | Icon of icon: System.Drawing.Icon
  | Checked of marked: bool
  | Index of index: int
  | Command of cmd: (System.EventArgs -> unit)

[<AutoOpen>]
module Style =
  let inline size (value: Size) = value.convert() |> Style.Size
  let inline location (value: Location) = value.convert() |> Style.Location
  let inline anchor anchor = anchor |> Anchor.cast |> Style.Anchor
  let inline direction direction = direction |> Direction.cast |> Style.Directon
  let inline dock dock = dock |> Dock.cast |> Style.Dock
  let inline auto_size auto = auto |> Style.AutoSize
  let inline text text = text |> Style.Text
  let inline name name = name |> Style.Name
  let inline identifier id = id |> Style.Name
  let inline image_to (img_path: string, align: Alignment) = Style.Image (new System.Drawing.Bitmap (img_path), Alignment.cast align)
  let inline image (img_path: string) = image_to (img_path, Alignment.midcenter)
  let inline bitmap_to (img_path: string, align: Alignment) = Style.Image (new System.Drawing.Bitmap (img_path), Alignment.cast align)
  let inline bitmap (img_path: string) = bitmap_to (img_path, Alignment.midcenter)
  let inline icon (ico_path: string) = Style.Icon (new System.Drawing.Icon (ico_path))
  let inline marked marked = marked |> Style.Checked
  let inline index id = id |> Style.Index
  let inline on_click cmd = cmd |> Style.Command

[<Struct>]
type Property = { 
  property: Style list
  controls: Control list
}
with member __.empty = { property = []; controls = [] }
