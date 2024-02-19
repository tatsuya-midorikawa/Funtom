namespace Funtom.winforms

open System.Windows.Forms

[<Struct>]
type MessageButtons =
  | ok = 0
  | ok_cancel = 1
  | abort_retry_ignore = 2
  | yes_no_cancel = 3
  | yes_no = 4
  | retry_cancel = 5

module private MessageButtons =
  let convert (button: MessageButtons) =
    button |> (int >> enum<System.Windows.Forms.MessageBoxButtons>)

[<Struct>]
type MessageIcon =
  | none = 0
  | hand = 16
  | stop = 16
  | error = 16
  | question = 32
  | exclamation = 48
  | warning = 48
  | asterisk = 64
  | information = 64


module private MessageIcon =
  let convert (icon: MessageIcon) =
    icon |> (int >> enum<System.Windows.Forms.MessageBoxIcon>)

type msg () =
  static member show (text: string) = MessageBox.Show(text)  
  static member show (text: string, caption: string) = MessageBox.Show(text, caption)  
  static member show (text: string, caption: string, button: MessageButtons) =
    MessageBox.Show(text, caption, MessageButtons.convert button)
  static member show (text: string, caption: string, button: MessageButtons, icon: MessageIcon) =
    MessageBox.Show(text, caption, MessageButtons.convert button, MessageIcon.convert icon)
  static member show (owner: IWin32Window, text: string) = MessageBox.Show(owner, text)  
  static member show (owner: IWin32Window, text: string, caption: string) = MessageBox.Show(owner, text, caption)  
  static member show (owner: IWin32Window, text: string, caption: string, button: MessageButtons) =
    MessageBox.Show(owner, text, caption, MessageButtons.convert button)
  static member show (owner: IWin32Window, text: string, caption: string, button: MessageButtons, icon: MessageIcon) =
    MessageBox.Show(owner, text, caption, MessageButtons.convert button, MessageIcon.convert icon)  