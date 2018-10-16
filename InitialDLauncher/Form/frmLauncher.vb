Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Windows
Imports PluginContract

Public Class frmLauncher

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Public WithEvents proc As Process
    Dim threadU As Thread
    Public Shared RunGameThread As Thread
    Public shadow As Dropshadow
    Dim curVer As Integer = 1
    Public buildDate As String = "17/10/2018"

    Public Shared parrotData As ParrotData = New ParrotData(".\ParrotData.xml").ReadFromFile

    Public Shared selPath As String = Nothing
    Public Shared lastGame As Integer = 0
    Public Shared isGameRunning As Boolean = False
    Public Shared pluginControls As New List(Of Control)
    Public plugins As ICollection(Of iPlugin) = PluginLoader.LoadPlugins("Plugins")

    'Translation
    Dim new_version, no_card_selected As String

    Private Sub frmLauncher_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub frmLauncher_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub frmLauncher_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        drag = False
    End Sub

    Private Sub frmLauncher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LauncherNormalLoad()
    End Sub

    Private Sub LauncherNormalLoad()
        Try
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
            Me.SetStyle(ControlStyles.UserPaint, True)

            If My.Settings.FullScreen Then
                WindowState = FormWindowState.Maximized
            Else
                WindowState = FormWindowState.Normal
            End If

            shadow = New Dropshadow(Me) With {.ShadowBlur = 30, .ShadowSpread = 1, .ShadowColor = Color.Black}
            shadow.RefreshShadow()

            CheckForIllegalCrossThreadCalls = False
            'GetGamePath()

            If My.Settings.WMMT5Path = Nothing Then lblStart5.Enabled = False

            Translate()

            For Each item In plugins
                item.DoSomething()
                Dim timer As System.Windows.Forms.Timer = New System.Windows.Forms.Timer With {.Interval = 60, .Enabled = True}
                AddHandler timer.Tick, AddressOf item.TimerTick
            Next

            If lblStart5.Enabled Then lblStart5.Focus()

            Timer1.Start()


            For Each ctrl In pluginControls
                Me.Controls.Add(ctrl)
            Next
        Catch ex As Exception
            NSMessageBox.ShowOk(ex.Message, MessageBoxIcon.Error, "Error")
            Logger.Log(ex.Message & ex.StackTrace)
            Exit Sub
        End Try
    End Sub

    Private Function CheckForUpdate() As Integer
        Dim client As WebClientEx = New WebClientEx() With {.Timeout = 10000}

        Dim reader As StreamReader = New StreamReader(client.OpenRead("https://raw.githubusercontent.com/qiangqiang101/Initial-D-Arcade-Stage-Teknoparrot-Launcher/master/ver/ver.txt"))
        Return reader.ReadToEnd
    End Function

    Private Sub lblExit_Click(sender As Object, e As EventArgs) Handles lblExit.Click, lblExit.EnterPressed
        My.Computer.Audio.Play(My.Resources.play, AudioPlayMode.Background)
        Me.Enabled = False
        wait(2000)
        End
    End Sub

    Private Sub RunGame(GameID As Integer, MySettingGameDir As String, Profile As String)
        Try
            Dim rd As Random = New Random
            Dim x As Integer = rd.Next(1, 27)
            My.Computer.Audio.Play(My.Resources.play, AudioPlayMode.Background)

            isGameRunning = True
            If My.Settings.ExtraLaunchOptions.Contains(";") Then
                For Each item In My.Settings.ExtraLaunchOptions.Split(",")
                    Dim psi = New ProcessStartInfo With {.FileName = "CMD", .Arguments = String.Format("/C start """" ""{0}""", item.Replace(";", "")), .WorkingDirectory = Path.GetDirectoryName(item.Replace(";", "")), .UseShellExecute = True, .CreateNoWindow = False, .WindowStyle = ProcessWindowStyle.Normal}
                    Process.Start(psi)
                Next
            End If

            lastGame = GameID
            Me.WindowState = FormWindowState.Minimized

            If My.Settings.Multiplayer Then
                RunGameThread = New Thread(Sub() RunTeknoParrotOnline(True))
                RunGameThread.Start()
            Else
                RunGameThread = New Thread(Sub() RunParrotLoaderUI(Profile, True, My.Settings.TestMode))
                RunGameThread.Start()
            End If
        Catch ex As Exception
            NSMessageBox.ShowOk(ex.Message, MessageBoxIcon.Error, "Error")
            Logger.Log(ex.Message & ex.StackTrace)
            isGameRunning = False
            Exit Sub
        End Try
    End Sub

    Private Sub lblStart5_Click(sender As Object, e As EventArgs) Handles lblStart5.Click, lblStart5.EnterPressed
        'isGameRunning = True
        wait(500)

        RunGame(5, My.Settings.WMMT5Path, "--profile=ID6.xml")
    End Sub

    Private Sub lblSetting_Click(sender As Object, e As EventArgs) Handles lblSetting.Click, lblSetting.EnterPressed
        My.Computer.Audio.Play(My.Resources.play, AudioPlayMode.Background)
        If Me.WindowState = FormWindowState.Maximized Then
            frmSettings.TopLevel = False
            Me.Controls.Add(frmSettings)
            frmSettings.Show()
            frmSettings.Focus()
        Else
            frmSettings.Show()
            Me.Enabled = False
        End If
    End Sub

    Private Sub frmLauncher_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not proc.HasExited Then
            e.Cancel = False
        Else
            isGameRunning = False
        End If
    End Sub

    Private Sub RunParrotLoader(ByVal arg As String, wait As Boolean, Optional test As Boolean = False)
        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = My.Application.Info.DirectoryPath & "\ParrotLoader.exe"
        startInfo.WindowStyle = ProcessWindowStyle.Minimized
        If test Then
            startInfo.Arguments = String.Format("""{0}"" {1}", arg, "-t")
        Else
            startInfo.Arguments = """" & arg & """"
        End If
        proc = Process.Start(startInfo)
        If wait Then
            proc.EnableRaisingEvents = True
            proc.WaitForExit()
        Else
            proc.EnableRaisingEvents = False
        End If
    End Sub

    Private Sub RunParrotLoaderUI(ByVal arg As String, wait As Boolean, Optional test As Boolean = False)
        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = My.Application.Info.DirectoryPath & "\TeknoParrotUi.exe"
        startInfo.WindowStyle = ProcessWindowStyle.Minimized
        If test Then
            startInfo.Arguments = String.Format("""{0}"" {1}", arg, "--test")
        Else
            startInfo.Arguments = """" & arg & """"
        End If
        proc = Process.Start(startInfo)
        If wait Then
            proc.EnableRaisingEvents = True
            proc.WaitForExit()
        Else
            proc.EnableRaisingEvents = False
        End If
    End Sub

    Private Sub RunTeknoParrotOnline(wait As Boolean)
        Process.Start("steam://rungameid/0")
        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = My.Application.Info.DirectoryPath & "\TeknoParrotOnline.exe"
        startInfo.WindowStyle = ProcessWindowStyle.Normal
        proc = Process.Start(startInfo)
        If wait Then
            proc.EnableRaisingEvents = True
            proc.WaitForExit()
        Else
            proc.EnableRaisingEvents = False
        End If
    End Sub

    Private Sub Proc_Exited() Handles proc.Exited
        isGameRunning = False
        Me.Enabled = True
    End Sub

    Private Sub lblVersion_Click(sender As Object, e As EventArgs) Handles lblVersion.Click, lblVersion.EnterPressed
        If Me.WindowState = FormWindowState.Maximized Then
            frmAbout.TopLevel = False
            Me.Controls.Add(frmAbout)
            frmAbout.Show()
            frmAbout.Focus()
        Else
            frmAbout.Show()
            Me.Enabled = False
        End If
    End Sub

    Private Sub lblVersion_MouseEnter(sender As Object, e As EventArgs) Handles lblVersion.MouseEnter
        My.Computer.Audio.Play(My.Resources._select, AudioPlayMode.Background)
        Me.Cursor = Cursors.Hand
        lblVersion.ForeColor = Color.Gold
    End Sub

    Private Sub lblVersion_MouseLeave(sender As Object, e As EventArgs) Handles lblVersion.MouseLeave
        Me.Cursor = Cursors.Default
        lblVersion.ForeColor = Color.White
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Timer1.Stop()
            threadU = New Thread(AddressOf CheckUpdate)
            threadU.IsBackground = True
            threadU.Start()
            If Today.Month = 4 AndAlso Today.Day = 1 Then
                NSMessageBox.ShowYesNo("Your license has expired. Click Yes to buy a license for 1 month for $100 or 1 year for $1,000. Click No to close this application.", MessageBoxIcon.Warning, Text)
            End If
        Catch ex As Exception
            NSMessageBox.ShowOk(ex.Message, MessageBoxIcon.Error, "Error")
            Logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Private Sub CheckUpdate()
        Try
            If curVer < CheckForUpdate() Then
                Dim result As DialogResult = NSMessageBox.ShowYesNo(new_version, MessageBoxIcon.Exclamation, Text)
                If result = DialogResult.No Then
                    Exit Sub
                ElseIf result = DialogResult.Yes Then
                    If IsURLValid("https://www.imnotmental.com/tool/initial-d-arcade-stage-launcher-teknoparrot/") Then
                        Process.Start("https://www.imnotmental.com/tool/initial-d-arcade-stage-launcher-teknoparrot/")
                        End
                    Else
                        Process.Start("https://www.patreon.com/posts/initiald-arcade-16661248")
                        End
                    End If
                End If
            End If
        Catch ex As Exception
            NSMessageBox.ShowOk(ex.Message, MessageBoxIcon.Error, "Error")
            Logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Enabled = False
        wait(2000)
        End
    End Sub

    Private Sub btnMin_Click(sender As Object, e As EventArgs) Handles btnMin.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Public Sub Translate()
        Try
            Dim langFile As String = String.Format("{0}\Languages\{1}.ini", My.Application.Info.DirectoryPath, My.Settings.Language)
            Text = ReadCfgValue("LauncherTitle", langFile)
            lblStart5.Text = ReadCfgValue("Start6", langFile)
            lblSetting.Text = ReadCfgValue("Settings", langFile)
            lblExit.Text = ReadCfgValue("QuitGame", langFile)
            lblVersion.Text = String.Format(ReadCfgValue("VersionBuild", langFile), FileVersionInfo.GetVersionInfo(Forms.Application.ExecutablePath).FileVersion, buildDate)
            new_version = ReadCfgValue("NewVersion", langFile)
            no_card_selected = ReadCfgValue("NoCardSelected", langFile)
        Catch ex As Exception
            NSMessageBox.ShowOk(ex.Message, MessageBoxIcon.Error, "Error")
            Logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub
End Class
