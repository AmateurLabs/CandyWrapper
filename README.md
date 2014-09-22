CandyWrapper
============

A collection of small wrapper programs.

## ScrWrapper ##
A wrapper for launching any executable as a Windows Screensaver (.scr). While you can theoretically rename any .exe to .scr and use it as a screensaver, Windows will launch them with special command-line arguments and reset the working directory, which can break programs that rely on custom flags or relative paths. ScrWrapper can help smooth over those problems.

#### Setup ####
Download the latest build from Releases and place it in the same folder as the program you want to launch. The wrapper and the program you want to launch can either be in the same folder, like this:
```
YourApplication.scr (renamed copy of ScrWrapper.scr)
YourApplication.exe (your program)
config.txt (optional configuration file)
```

or the program can be in a folder called Data, like this:
```
YourApplication.scr
Data/
  YourApplication.exe
  config.txt
```

If you need more flexibility, you can include a file called config.txt in either the same folder or the Data folder with the following content:
```
executable=YourApplication.exe (the path to your program, the default is the wrapper's filename with a .exe extension)
arguments=/p -someflag (the command-line arguments to use when calling your program)
handleDefault=true (true if launching the wrapper directly should run the program, otherwise false)
handlePreview=false (true if your program knows how to handle the Windows preview request, otherwise false)
handleSettings=false (true if your program knows how to handle the Windows config request, otherwise false)
```

Distribution is up to you, but I would recommend dumping everything into a single .zip or similar archive.

#### Usage ####
To install the screensaver, just right-click on the .scr file and click Install. The Windows screensaver selection window should automatically open up. After that, it can be handled just like any other screensaver.

#### Unity 3D Notes ####
The wrapper was written with standalone Windows Unity builds in mind, and should work right out of the box. However, make sure you keep your _Data folder in the same place as your executable. Also, ScrWrapper doesn't handle the 'exit on key press or mouse movement' behavior automatically, so you'll probably want to stick something like this in an Update function somewhere:
```csharp
if (Input.GetAxisRaw("Mouse X") != 0f || Input.GetAxisRaw("Mouse Y") != 0f || Input.anyKey)
  Application.Quit();
```
