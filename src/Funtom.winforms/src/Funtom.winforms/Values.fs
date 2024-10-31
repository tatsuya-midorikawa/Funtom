namespace Funtom.winforms

[<Struct>]
type SizeF = { width: float32; height: float32 }
module SizeF =
  let inline toNative (size: SizeF) = System.Drawing.SizeF(float32 size.width, float32 size.height)

[<Struct; RequireQualifiedAccess>]
type AutoScaleMode =
  | none = 0
  | font = 1
  | dpi = 2
  | inheritant = 3
module AutoScaleMode =
  let inline toNative (mode: AutoScaleMode) = 
    match mode with
      | AutoScaleMode.none -> System.Windows.Forms.AutoScaleMode.None
      | AutoScaleMode.font -> System.Windows.Forms.AutoScaleMode.Font
      | AutoScaleMode.dpi -> System.Windows.Forms.AutoScaleMode.Dpi
      | AutoScaleMode.inheritant -> System.Windows.Forms.AutoScaleMode.Inherit
      | _ -> failwith "This value is not supported"


