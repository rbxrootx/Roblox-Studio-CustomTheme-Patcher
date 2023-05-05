<img align=right src="main.ico" />

# Roblox Studio CustomTheme Patcher

Enables the loading of custom themes into Roblox Studio.

## Usage

### GitHub

1. Download `Release.zip` from the [latest GitHub release](https://github.com/rbxrootx/Roblox-Studio-CustomTheme-Patcher/releases/latest)

2. Open `Release.zip` in File Explorer and extract all the files

3. In the extracted `Release` folder, there should be a folder called `Resources` containing the .json files for the themes you can modify to achieve your desired theme

4. Once you have finished changing theme files, navigate back and double-click `StudioPatcher2.exe` to launch it, a terminal should open (A [false positive](#false-positives) might appear when you open `StudioPatcher2.exe`; you can click <kbd>More info</kbd> > <kbd>Run anyway</kbd>)

5. Locate `RobloxStudioBeta.exe`, which should be located around `%APPDATA%\..\Local\Roblox\Versions\version-VERSION_NUMBER_HERE\RobloxStudioBeta.exe` and drag it into the terminal window

6. Press <kbd>Enter</kbd> and wait for the patching process to finish

7. Launch `RobloxStudioPatched.exe`, which should be in the same path as `RobloxStudioBeta.exe`, and enjoy your custom themes (If the patch is in the `Release` folder, you need manually move it to the same path as `RobloxStudioBeta.exe`)

## False Positives

Your browser or OS may detect that the compiled binary is a virus. This is due to it patching bytes in the .exe.
You can compile the project from the provided source code if you wish.

## VirusTotal Scan

[VirusTotal - File - d1fbbd50702ec997f934e34387442586590d8786c4e2d97f4c719a2aa8c01632](https://www.virustotal.com/gui/file/d1fbbd50702ec997f934e34387442586590d8786c4e2d97f4c719a2aa8c01632?nocache=1)
