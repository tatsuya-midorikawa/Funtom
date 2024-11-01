namespace Funtom.winforms

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
  let inline toNative (mode: AutoScaleMode) = 
    match mode with
      | AutoScaleMode.none -> System.Windows.Forms.AutoScaleMode.None
      | AutoScaleMode.font -> System.Windows.Forms.AutoScaleMode.Font
      | AutoScaleMode.dpi -> System.Windows.Forms.AutoScaleMode.Dpi
      | AutoScaleMode.inheritant -> System.Windows.Forms.AutoScaleMode.Inherit
      | _ -> failwith "This value is not supported"


