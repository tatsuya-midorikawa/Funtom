namespace Funtom.winforms

module components =
  (* ----------------------------------------
   * FolderBrowserDialog
   * ---------------------------------------- *)
  type dir_browser(?description, ?show_new_dir, ?root_dir, ?init_dir, ?show_hiddens) =
    let self = new System.Windows.Forms.FolderBrowserDialog()
    do
      description |> Option.iter (fun v -> self.Description <- v)
      show_new_dir |> Option.iter (fun v -> self.ShowNewFolderButton <- v)
      root_dir |> Option.iter (fun v -> self.RootFolder <- v)
      init_dir |> Option.iter (fun v -> self.InitialDirectory <- v)
      show_hiddens |> Option.iter (fun v -> self.ShowHiddenFiles <- v)

    interface System.IDisposable with
      member __.Dispose() = self.Dispose()

    member __.show (?owner) = match owner with Some v -> self.ShowDialog(v) | None -> self.ShowDialog()
    member __.reset () = self.Reset()

    member __.selected with get() = self.SelectedPath and set v = self.SelectedPath <- v
    member __.description with get() = self.Description and set v = self.Description <- v
    member __.show_new_dir with get() = self.ShowNewFolderButton and set v = self.ShowNewFolderButton <- v
    member __.root_dir with get() = self.RootFolder and set v = self.RootFolder <- v
    member __.init_dir with get() = self.InitialDirectory and set v = self.InitialDirectory <- v
    member __.show_hiddens with get() = self.ShowHiddenFiles and set v = self.ShowHiddenFiles <- v
    
  (* ----------------------------------------
   * OpenFileDialog
   * ---------------------------------------- *)
  //let inline open_file_dialog () = new System.Windows.Forms.OpenFileDialog()
