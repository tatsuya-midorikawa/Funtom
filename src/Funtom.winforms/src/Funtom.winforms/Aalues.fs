namespace Funtom.winforms.values

[<Struct>]
type SizeF = { width: float32; height: float32 }
[<RequireQualifiedAccess>]
module SizeF =
  let inline toNative (size: SizeF) = System.Drawing.SizeF(float32 size.width, float32 size.height)


[<Struct>]
type Size = { width: int; height: int }
[<RequireQualifiedAccess>]
module Size =
  let inline toNative (size: Size) = System.Drawing.Size(size.width, size.height)


[<Struct>]
type Position = { top: int; left: int }
[<RequireQualifiedAccess>]
module Position =
  let inline toNative (position: Position) = System.Drawing.Point(position.left, position.top)


[<Struct; RequireQualifiedAccess>]
type AutoScaleMode =
  | none = 0
  | font = 1
  | dpi = 2
  | inheritant = 3
[<RequireQualifiedAccess>]
module AutoScaleMode =
  let inline toNative (mode: AutoScaleMode) = int mode |> enum<System.Windows.Forms.AutoScaleMode>


[<Struct; System.Flags; RequireQualifiedAccess>]
type Anchors =
  | none   = 0
  | top    = (1 <<< 0)
  | bottom = (1 <<< 1)
  | left   = (1 <<< 2)
  | right  = (1 <<< 3)
[<RequireQualifiedAccess>]
module Anchors =
  let inline toNative (anchors: Anchors) = int anchors |> enum<System.Windows.Forms.AnchorStyles>

  
[<Struct; RequireQualifiedAccess>]
type Dock =
  | none = 0
  | top = 1
  | bottom = 2
  | left = 3
  | right = 4
  | fill = 5
[<RequireQualifiedAccess>]
module Dock =
  let inline toNative (dock: Dock) = int dock |> enum<System.Windows.Forms.DockStyle>


[<Struct; RequireQualifiedAccess>]
type Direction =
  | left2right = 0
  | topdown = 1
  | right2left = 2
  | bottomup = 3
[<RequireQualifiedAccess>]
module Direction =
  let inline toNative (direction: Direction) = int direction |> enum<System.Windows.Forms.FlowDirection>

type Form = System.Windows.Forms.Form