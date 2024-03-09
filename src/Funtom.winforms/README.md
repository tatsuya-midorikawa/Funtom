# Funtom.winforms

## Overview

This library makes WinForms development in F# convenient.
It is primarily intended to be invoked with F# script (.fsx), and the purpose is to provide a more lightweight way to start GUI programming.

---
## Usage

This library is used as shown in the following codes:

1. Create a folder for your project.

    ```powershell
    mkdir C:\pj\MyWinformApp
    cd C:\pj\MyWinformApp
    ```

2. Download the project template. Run the following script in PowerShell.

    ```powershell
    $src = "https://github.com/tatsuya-midorikawa/Funtom/releases/download/Funtom.winforms.lit_ver.0.0.1/Funtom.winforms.lit_ver.0.0.1.zip"
    $zip = "./Funtom.winforms.lit_ver.0.0.1.zip"
    Invoke-WebRequest -Uri $src -OutFile $zip
    Expand-Archive -Path $zip -DestinationPath "./"
    Remove-Item -Path $zip

    $ls = dotnet --list-runtimes | where {$_ -like 'Microsoft.WindowsDesktop.App*'}
    $ver = $ls[$ls.Count-1].Split()[1]
    $script = "./script.fsx"
    $content = Get-Content -Encoding UTF8 $script | ForEach-Object {$_ -replace "8.0.1", $ver} 
    $content | Out-File -Force -Encoding UTF8 -FilePath $script
    ```
3. Run the following script in PowerShell.

		```powershell
		dotnet fsi ./script.fsx
		```

4. Edit script.fsx and have fun!

