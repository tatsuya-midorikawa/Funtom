namespace Funtom.winforms

open System.Runtime.CompilerServices

module components =
  (* ----------------------------------------
   * FolderBrowserDialog
   * ---------------------------------------- *)
  type folder_browser(?description, ?show_new_folder, ?root_folder) =
    let self = new System.Windows.Forms.FolderBrowserDialog()
    do
      match description with Some d -> self.Description <- d | None -> ()
      match show_new_folder with Some b -> self.ShowNewFolderButton <- b | None -> ()
      match root_folder with Some dir -> self.RootFolder <- dir | None -> ()

    member __.show() = self.ShowDialog()
    member __.selected with get() = self.SelectedPath

    
  (* ----------------------------------------
   * OpenFileDialog
   * ---------------------------------------- *)
  //let inline open_file_dialog () = new System.Windows.Forms.OpenFileDialog()
