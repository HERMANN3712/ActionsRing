[Setup]
AppName=Actions Ring
AppVersion=1.0.0
DefaultDirName={autopf}\Actions Ring
DefaultGroupName=Actions Ring
OutputDir=Output
OutputBaseFilename=ActionsRingSetup
Compression=lzma
SolidCompression=yes
WizardStyle=modern
UninstallDisplayIcon={app}\ActionsRing.exe

[Files]
Source: "bin\Release\net8.0-windows\win-x64\ActionsRing.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0-windows\win-x64\actions.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0-windows\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion
Source: "bin\Release\net8.0-windows\win-x64\Assets\*"; DestDir: "{app}\Assets"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Actions Ring"; Filename: "{app}\ActionsRing.exe"
Name: "{autodesktop}\Actions Ring"; Filename: "{app}\ActionsRing.exe"; Tasks: desktopicon

[Tasks]
Name: "desktopicon"; Description: "Créer un raccourci sur le bureau"; Flags: unchecked
Name: "startup"; Description: "Lancer Actions Ring au démarrage de Windows"; Flags: unchecked

[Registry]
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; \
    ValueType: string; ValueName: "ActionsRing"; ValueData: """{app}\ActionsRing.exe"""; \
    Tasks: startup; Flags: uninsdeletevalue

[Run]
Filename: "{app}\ActionsRing.exe"; Description: "Lancer Actions Ring"; Flags: nowait postinstall skipifsilent