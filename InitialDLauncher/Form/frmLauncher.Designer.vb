<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLauncher
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLauncher))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnMin = New WMMTLauncher.TextButton()
        Me.btnExit = New WMMTLauncher.TextButton()
        Me.lblStart5 = New WMMTLauncher.TextButton()
        Me.lblVersion = New WMMTLauncher.TextButton()
        Me.lblExit = New WMMTLauncher.TextButton()
        Me.lblSetting = New WMMTLauncher.TextButton()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 5000
        '
        'btnMin
        '
        Me.btnMin.AddEffect = False
        Me.btnMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMin.BackColor = System.Drawing.Color.Transparent
        Me.btnMin.DisabledColor = System.Drawing.Color.DarkGray
        Me.btnMin.EffectAfter = ""
        Me.btnMin.EffectBefore = Nothing
        Me.btnMin.EffectWidth = 39
        Me.btnMin.Font = New System.Drawing.Font("Marlett", 10.0!)
        Me.btnMin.ForeColor = System.Drawing.Color.Silver
        Me.btnMin.Location = New System.Drawing.Point(792, 9)
        Me.btnMin.MouseHoverColor = System.Drawing.Color.White
        Me.btnMin.MousePressedColor = System.Drawing.Color.Gold
        Me.btnMin.Name = "btnMin"
        Me.btnMin.NormalColor = System.Drawing.Color.Silver
        Me.btnMin.P = Me
        Me.btnMin.Size = New System.Drawing.Size(19, 19)
        Me.btnMin.TabIndex = 10
        Me.btnMin.Text = "0"
        Me.btnMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExit
        '
        Me.btnExit.AddEffect = False
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.Color.Transparent
        Me.btnExit.DisabledColor = System.Drawing.Color.DarkGray
        Me.btnExit.EffectAfter = ""
        Me.btnExit.EffectBefore = Nothing
        Me.btnExit.EffectWidth = 39
        Me.btnExit.Font = New System.Drawing.Font("Marlett", 10.0!)
        Me.btnExit.ForeColor = System.Drawing.Color.Silver
        Me.btnExit.Location = New System.Drawing.Point(817, 8)
        Me.btnExit.MouseHoverColor = System.Drawing.Color.White
        Me.btnExit.MousePressedColor = System.Drawing.Color.Gold
        Me.btnExit.Name = "btnExit"
        Me.btnExit.NormalColor = System.Drawing.Color.Silver
        Me.btnExit.P = Me
        Me.btnExit.Size = New System.Drawing.Size(19, 19)
        Me.btnExit.TabIndex = 11
        Me.btnExit.Text = "r"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStart5
        '
        Me.lblStart5.AddEffect = True
        Me.lblStart5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStart5.BackColor = System.Drawing.Color.Transparent
        Me.lblStart5.DisabledColor = System.Drawing.Color.DarkGray
        Me.lblStart5.EffectAfter = " <<"
        Me.lblStart5.EffectBefore = Nothing
        Me.lblStart5.EffectWidth = 39
        Me.lblStart5.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.lblStart5.ForeColor = System.Drawing.Color.Silver
        Me.lblStart5.Location = New System.Drawing.Point(441, 329)
        Me.lblStart5.MouseHoverColor = System.Drawing.Color.White
        Me.lblStart5.MousePressedColor = System.Drawing.Color.Gold
        Me.lblStart5.Name = "lblStart5"
        Me.lblStart5.NormalColor = System.Drawing.Color.Silver
        Me.lblStart5.P = Me
        Me.lblStart5.Size = New System.Drawing.Size(359, 32)
        Me.lblStart5.TabIndex = 2
        Me.lblStart5.Text = "Play WMMT5"
        Me.lblStart5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVersion
        '
        Me.lblVersion.AddEffect = False
        Me.lblVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblVersion.AutoSize = True
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.DisabledColor = System.Drawing.Color.DarkGray
        Me.lblVersion.EffectAfter = " <<"
        Me.lblVersion.EffectBefore = Nothing
        Me.lblVersion.EffectWidth = 43
        Me.lblVersion.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblVersion.ForeColor = System.Drawing.Color.Silver
        Me.lblVersion.Location = New System.Drawing.Point(12, 452)
        Me.lblVersion.MouseHoverColor = System.Drawing.Color.White
        Me.lblVersion.MousePressedColor = System.Drawing.Color.Gold
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.NormalColor = System.Drawing.Color.Silver
        Me.lblVersion.P = Me
        Me.lblVersion.Size = New System.Drawing.Size(81, 19)
        Me.lblVersion.TabIndex = 9
        Me.lblVersion.Text = "Version: 1.0"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblExit
        '
        Me.lblExit.AddEffect = True
        Me.lblExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblExit.BackColor = System.Drawing.Color.Transparent
        Me.lblExit.DisabledColor = System.Drawing.Color.DarkGray
        Me.lblExit.EffectAfter = " <<"
        Me.lblExit.EffectBefore = Nothing
        Me.lblExit.EffectWidth = 39
        Me.lblExit.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.lblExit.ForeColor = System.Drawing.Color.Silver
        Me.lblExit.Location = New System.Drawing.Point(441, 413)
        Me.lblExit.MouseHoverColor = System.Drawing.Color.White
        Me.lblExit.MousePressedColor = System.Drawing.Color.Gold
        Me.lblExit.Name = "lblExit"
        Me.lblExit.NormalColor = System.Drawing.Color.Silver
        Me.lblExit.P = Me
        Me.lblExit.Size = New System.Drawing.Size(359, 32)
        Me.lblExit.TabIndex = 8
        Me.lblExit.Text = "Quit Game"
        Me.lblExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSetting
        '
        Me.lblSetting.AddEffect = True
        Me.lblSetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSetting.BackColor = System.Drawing.Color.Transparent
        Me.lblSetting.DisabledColor = System.Drawing.Color.DarkGray
        Me.lblSetting.EffectAfter = " <<"
        Me.lblSetting.EffectBefore = Nothing
        Me.lblSetting.EffectWidth = 39
        Me.lblSetting.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.lblSetting.ForeColor = System.Drawing.Color.Silver
        Me.lblSetting.Location = New System.Drawing.Point(441, 381)
        Me.lblSetting.MouseHoverColor = System.Drawing.Color.White
        Me.lblSetting.MousePressedColor = System.Drawing.Color.Gold
        Me.lblSetting.Name = "lblSetting"
        Me.lblSetting.NormalColor = System.Drawing.Color.Silver
        Me.lblSetting.P = Me
        Me.lblSetting.Size = New System.Drawing.Size(359, 32)
        Me.lblSetting.TabIndex = 7
        Me.lblSetting.Text = "Settings"
        Me.lblSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmLauncher
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = Global.WMMTLauncher.My.Resources.Resources.new_bg
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(848, 480)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnMin)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblStart5)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblExit)
        Me.Controls.Add(Me.lblSetting)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLauncher"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "WMMT Launcher"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents lblStart5 As TextButton
    Friend WithEvents lblExit As TextButton
    Friend WithEvents lblSetting As TextButton
    Friend WithEvents lblVersion As TextButton
    Friend WithEvents btnMin As TextButton
    Friend WithEvents btnExit As TextButton
End Class
