<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRocChart
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.chtRoc = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.GroupBox19 = New System.Windows.Forms.GroupBox()
        Me.txtTP = New System.Windows.Forms.TextBox()
        Me.Label241 = New System.Windows.Forms.Label()
        Me.txtFP = New System.Windows.Forms.TextBox()
        Me.txtTN = New System.Windows.Forms.TextBox()
        Me.Label240 = New System.Windows.Forms.Label()
        Me.Label239 = New System.Windows.Forms.Label()
        Me.txtFN = New System.Windows.Forms.TextBox()
        Me.Label238 = New System.Windows.Forms.Label()
        Me.GroupBox20 = New System.Windows.Forms.GroupBox()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.chkLockPrevalence = New System.Windows.Forms.CheckBox()
        Me.chkLockSampleSize = New System.Windows.Forms.CheckBox()
        Me.txtPointColor = New System.Windows.Forms.TextBox()
        Me.Label251 = New System.Windows.Forms.Label()
        Me.cmbPointColor = New System.Windows.Forms.ComboBox()
        Me.Label250 = New System.Windows.Forms.Label()
        Me.txtSampleSize = New System.Windows.Forms.TextBox()
        Me.Label249 = New System.Windows.Forms.Label()
        Me.txtPrevalence = New System.Windows.Forms.TextBox()
        Me.txtSensitivity = New System.Windows.Forms.TextBox()
        Me.Label244 = New System.Windows.Forms.Label()
        Me.Label243 = New System.Windows.Forms.Label()
        Me.txtSpecificity = New System.Windows.Forms.TextBox()
        Me.chkShowLine = New System.Windows.Forms.CheckBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.dgvRocData = New System.Windows.Forms.DataGridView()
        Me.btnOpenRoc = New System.Windows.Forms.Button()
        Me.btnSaveRoc = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtRocFileName = New System.Windows.Forms.TextBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.chtRoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox19.SuspendLayout()
        Me.GroupBox20.SuspendLayout()
        CType(Me.dgvRocData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(600, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(48, 22)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 40)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.chtRoc)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox19)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox20)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkShowLine)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtDescription)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label24)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvRocData)
        Me.SplitContainer1.Size = New System.Drawing.Size(636, 901)
        Me.SplitContainer1.SplitterDistance = 408
        Me.SplitContainer1.TabIndex = 9
        '
        'chtRoc
        '
        Me.chtRoc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea1.Name = "ChartArea1"
        Me.chtRoc.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.chtRoc.Legends.Add(Legend1)
        Me.chtRoc.Location = New System.Drawing.Point(3, 3)
        Me.chtRoc.Name = "chtRoc"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.chtRoc.Series.Add(Series1)
        Me.chtRoc.Size = New System.Drawing.Size(630, 402)
        Me.chtRoc.TabIndex = 0
        Me.chtRoc.Text = "Chart1"
        '
        'GroupBox19
        '
        Me.GroupBox19.Controls.Add(Me.txtTP)
        Me.GroupBox19.Controls.Add(Me.Label241)
        Me.GroupBox19.Controls.Add(Me.txtFP)
        Me.GroupBox19.Controls.Add(Me.txtTN)
        Me.GroupBox19.Controls.Add(Me.Label240)
        Me.GroupBox19.Controls.Add(Me.Label239)
        Me.GroupBox19.Controls.Add(Me.txtFN)
        Me.GroupBox19.Controls.Add(Me.Label238)
        Me.GroupBox19.Location = New System.Drawing.Point(9, 120)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Size = New System.Drawing.Size(211, 128)
        Me.GroupBox19.TabIndex = 286
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "Survey Results:"
        '
        'txtTP
        '
        Me.txtTP.Location = New System.Drawing.Point(101, 19)
        Me.txtTP.Name = "txtTP"
        Me.txtTP.ReadOnly = True
        Me.txtTP.Size = New System.Drawing.Size(100, 20)
        Me.txtTP.TabIndex = 32
        '
        'Label241
        '
        Me.Label241.AutoSize = True
        Me.Label241.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label241.Location = New System.Drawing.Point(12, 74)
        Me.Label241.Name = "Label241"
        Me.Label241.Size = New System.Drawing.Size(86, 13)
        Me.Label241.TabIndex = 35
        Me.Label241.Text = "False Positives ="
        '
        'txtFP
        '
        Me.txtFP.Location = New System.Drawing.Point(101, 71)
        Me.txtFP.Name = "txtFP"
        Me.txtFP.ReadOnly = True
        Me.txtFP.Size = New System.Drawing.Size(100, 20)
        Me.txtFP.TabIndex = 36
        '
        'txtTN
        '
        Me.txtTN.Location = New System.Drawing.Point(101, 45)
        Me.txtTN.Name = "txtTN"
        Me.txtTN.ReadOnly = True
        Me.txtTN.Size = New System.Drawing.Size(100, 20)
        Me.txtTN.TabIndex = 34
        '
        'Label240
        '
        Me.Label240.AutoSize = True
        Me.Label240.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label240.Location = New System.Drawing.Point(9, 48)
        Me.Label240.Name = "Label240"
        Me.Label240.Size = New System.Drawing.Size(89, 13)
        Me.Label240.TabIndex = 33
        Me.Label240.Text = "True Negatives ="
        '
        'Label239
        '
        Me.Label239.AutoSize = True
        Me.Label239.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label239.Location = New System.Drawing.Point(6, 100)
        Me.Label239.Name = "Label239"
        Me.Label239.Size = New System.Drawing.Size(92, 13)
        Me.Label239.TabIndex = 37
        Me.Label239.Text = "False Negatives ="
        '
        'txtFN
        '
        Me.txtFN.Location = New System.Drawing.Point(101, 97)
        Me.txtFN.Name = "txtFN"
        Me.txtFN.ReadOnly = True
        Me.txtFN.Size = New System.Drawing.Size(100, 20)
        Me.txtFN.TabIndex = 38
        '
        'Label238
        '
        Me.Label238.AutoSize = True
        Me.Label238.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label238.Location = New System.Drawing.Point(15, 22)
        Me.Label238.Name = "Label238"
        Me.Label238.Size = New System.Drawing.Size(83, 13)
        Me.Label238.TabIndex = 31
        Me.Label238.Text = "True Positives ="
        '
        'GroupBox20
        '
        Me.GroupBox20.Controls.Add(Me.btnUpdate)
        Me.GroupBox20.Controls.Add(Me.chkLockPrevalence)
        Me.GroupBox20.Controls.Add(Me.chkLockSampleSize)
        Me.GroupBox20.Controls.Add(Me.txtPointColor)
        Me.GroupBox20.Controls.Add(Me.Label251)
        Me.GroupBox20.Controls.Add(Me.cmbPointColor)
        Me.GroupBox20.Controls.Add(Me.Label250)
        Me.GroupBox20.Controls.Add(Me.txtSampleSize)
        Me.GroupBox20.Controls.Add(Me.Label249)
        Me.GroupBox20.Controls.Add(Me.txtPrevalence)
        Me.GroupBox20.Controls.Add(Me.txtSensitivity)
        Me.GroupBox20.Controls.Add(Me.Label244)
        Me.GroupBox20.Controls.Add(Me.Label243)
        Me.GroupBox20.Controls.Add(Me.txtSpecificity)
        Me.GroupBox20.Location = New System.Drawing.Point(226, 120)
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.Size = New System.Drawing.Size(356, 128)
        Me.GroupBox20.TabIndex = 285
        Me.GroupBox20.TabStop = False
        Me.GroupBox20.Text = "Analysis Results:"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(254, 85)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(88, 22)
        Me.btnUpdate.TabIndex = 286
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'chkLockPrevalence
        '
        Me.chkLockPrevalence.AutoSize = True
        Me.chkLockPrevalence.Location = New System.Drawing.Point(198, 74)
        Me.chkLockPrevalence.Name = "chkLockPrevalence"
        Me.chkLockPrevalence.Size = New System.Drawing.Size(50, 17)
        Me.chkLockPrevalence.TabIndex = 285
        Me.chkLockPrevalence.Text = "Lock"
        Me.chkLockPrevalence.UseVisualStyleBackColor = True
        '
        'chkLockSampleSize
        '
        Me.chkLockSampleSize.AutoSize = True
        Me.chkLockSampleSize.Location = New System.Drawing.Point(198, 100)
        Me.chkLockSampleSize.Name = "chkLockSampleSize"
        Me.chkLockSampleSize.Size = New System.Drawing.Size(50, 17)
        Me.chkLockSampleSize.TabIndex = 284
        Me.chkLockSampleSize.Text = "Lock"
        Me.chkLockSampleSize.UseVisualStyleBackColor = True
        '
        'txtPointColor
        '
        Me.txtPointColor.Location = New System.Drawing.Point(238, 22)
        Me.txtPointColor.Name = "txtPointColor"
        Me.txtPointColor.Size = New System.Drawing.Size(34, 20)
        Me.txtPointColor.TabIndex = 283
        '
        'Label251
        '
        Me.Label251.AutoSize = True
        Me.Label251.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label251.Location = New System.Drawing.Point(198, 25)
        Me.Label251.Name = "Label251"
        Me.Label251.Size = New System.Drawing.Size(34, 13)
        Me.Label251.TabIndex = 282
        Me.Label251.Text = "Color:"
        '
        'cmbPointColor
        '
        Me.cmbPointColor.FormattingEnabled = True
        Me.cmbPointColor.Location = New System.Drawing.Point(198, 45)
        Me.cmbPointColor.Name = "cmbPointColor"
        Me.cmbPointColor.Size = New System.Drawing.Size(144, 21)
        Me.cmbPointColor.TabIndex = 281
        '
        'Label250
        '
        Me.Label250.AutoSize = True
        Me.Label250.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label250.Location = New System.Drawing.Point(12, 100)
        Me.Label250.Name = "Label250"
        Me.Label250.Size = New System.Drawing.Size(74, 13)
        Me.Label250.TabIndex = 50
        Me.Label250.Text = "Sample Size ="
        '
        'txtSampleSize
        '
        Me.txtSampleSize.Location = New System.Drawing.Point(92, 97)
        Me.txtSampleSize.Name = "txtSampleSize"
        Me.txtSampleSize.Size = New System.Drawing.Size(100, 20)
        Me.txtSampleSize.TabIndex = 51
        '
        'Label249
        '
        Me.Label249.AutoSize = True
        Me.Label249.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label249.Location = New System.Drawing.Point(16, 74)
        Me.Label249.Name = "Label249"
        Me.Label249.Size = New System.Drawing.Size(70, 13)
        Me.Label249.TabIndex = 48
        Me.Label249.Text = "Prevalence ="
        '
        'txtPrevalence
        '
        Me.txtPrevalence.Location = New System.Drawing.Point(92, 71)
        Me.txtPrevalence.Name = "txtPrevalence"
        Me.txtPrevalence.Size = New System.Drawing.Size(100, 20)
        Me.txtPrevalence.TabIndex = 49
        '
        'txtSensitivity
        '
        Me.txtSensitivity.Location = New System.Drawing.Point(92, 19)
        Me.txtSensitivity.Name = "txtSensitivity"
        Me.txtSensitivity.ReadOnly = True
        Me.txtSensitivity.Size = New System.Drawing.Size(100, 20)
        Me.txtSensitivity.TabIndex = 46
        '
        'Label244
        '
        Me.Label244.AutoSize = True
        Me.Label244.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label244.Location = New System.Drawing.Point(22, 22)
        Me.Label244.Name = "Label244"
        Me.Label244.Size = New System.Drawing.Size(63, 13)
        Me.Label244.TabIndex = 41
        Me.Label244.Text = "Sensitivity ="
        '
        'Label243
        '
        Me.Label243.AutoSize = True
        Me.Label243.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label243.Location = New System.Drawing.Point(22, 48)
        Me.Label243.Name = "Label243"
        Me.Label243.Size = New System.Drawing.Size(64, 13)
        Me.Label243.TabIndex = 42
        Me.Label243.Text = "Specificity ="
        '
        'txtSpecificity
        '
        Me.txtSpecificity.Location = New System.Drawing.Point(92, 45)
        Me.txtSpecificity.Name = "txtSpecificity"
        Me.txtSpecificity.ReadOnly = True
        Me.txtSpecificity.Size = New System.Drawing.Size(100, 20)
        Me.txtSpecificity.TabIndex = 47
        '
        'chkShowLine
        '
        Me.chkShowLine.AutoSize = True
        Me.chkShowLine.Location = New System.Drawing.Point(9, 97)
        Me.chkShowLine.Name = "chkShowLine"
        Me.chkShowLine.Size = New System.Drawing.Size(133, 17)
        Me.chkShowLine.TabIndex = 8
        Me.chkShowLine.Text = "Show Connecting Line"
        Me.chkShowLine.UseVisualStyleBackColor = True
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Location = New System.Drawing.Point(3, 21)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(630, 70)
        Me.txtDescription.TabIndex = 7
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 5)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(63, 13)
        Me.Label24.TabIndex = 6
        Me.Label24.Text = "Description:"
        '
        'dgvRocData
        '
        Me.dgvRocData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvRocData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRocData.Location = New System.Drawing.Point(3, 254)
        Me.dgvRocData.Name = "dgvRocData"
        Me.dgvRocData.Size = New System.Drawing.Size(630, 232)
        Me.dgvRocData.TabIndex = 0
        '
        'btnOpenRoc
        '
        Me.btnOpenRoc.Location = New System.Drawing.Point(12, 12)
        Me.btnOpenRoc.Name = "btnOpenRoc"
        Me.btnOpenRoc.Size = New System.Drawing.Size(45, 22)
        Me.btnOpenRoc.TabIndex = 13
        Me.btnOpenRoc.Text = "Open"
        Me.btnOpenRoc.UseVisualStyleBackColor = True
        '
        'btnSaveRoc
        '
        Me.btnSaveRoc.Location = New System.Drawing.Point(63, 12)
        Me.btnSaveRoc.Name = "btnSaveRoc"
        Me.btnSaveRoc.Size = New System.Drawing.Size(45, 22)
        Me.btnSaveRoc.TabIndex = 15
        Me.btnSaveRoc.Text = "Save"
        Me.btnSaveRoc.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(114, 17)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "File:"
        '
        'txtRocFileName
        '
        Me.txtRocFileName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRocFileName.Location = New System.Drawing.Point(146, 12)
        Me.txtRocFileName.Name = "txtRocFileName"
        Me.txtRocFileName.Size = New System.Drawing.Size(448, 20)
        Me.txtRocFileName.TabIndex = 20
        '
        'frmRocChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(660, 953)
        Me.Controls.Add(Me.txtRocFileName)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.btnSaveRoc)
        Me.Controls.Add(Me.btnOpenRoc)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.btnExit)
        Me.Name = "frmRocChart"
        Me.Text = "Receiver Operating Characteristic (ROC) Chart"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.chtRoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox19.ResumeLayout(False)
        Me.GroupBox19.PerformLayout()
        Me.GroupBox20.ResumeLayout(False)
        Me.GroupBox20.PerformLayout()
        CType(Me.dgvRocData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnExit As Button
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents chtRoc As DataVisualization.Charting.Chart
    Friend WithEvents dgvRocData As DataGridView
    Friend WithEvents btnOpenRoc As Button
    Friend WithEvents btnSaveRoc As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents txtRocFileName As TextBox
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents chkShowLine As CheckBox
    Friend WithEvents GroupBox20 As GroupBox
    Friend WithEvents txtPointColor As TextBox
    Friend WithEvents Label251 As Label
    Friend WithEvents cmbPointColor As ComboBox
    Friend WithEvents Label250 As Label
    Friend WithEvents txtSampleSize As TextBox
    Friend WithEvents Label249 As Label
    Friend WithEvents txtPrevalence As TextBox
    Friend WithEvents txtSensitivity As TextBox
    Friend WithEvents Label244 As Label
    Friend WithEvents Label243 As Label
    Friend WithEvents txtSpecificity As TextBox
    Friend WithEvents GroupBox19 As GroupBox
    Friend WithEvents txtTP As TextBox
    Friend WithEvents Label241 As Label
    Friend WithEvents txtFP As TextBox
    Friend WithEvents txtTN As TextBox
    Friend WithEvents Label240 As Label
    Friend WithEvents Label239 As Label
    Friend WithEvents txtFN As TextBox
    Friend WithEvents Label238 As Label
    Friend WithEvents chkLockPrevalence As CheckBox
    Friend WithEvents chkLockSampleSize As CheckBox
    Friend WithEvents btnUpdate As Button
End Class
