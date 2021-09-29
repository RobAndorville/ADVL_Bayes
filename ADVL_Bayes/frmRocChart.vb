Imports System.Windows.Forms.DataVisualization.Charting
Public Class frmRocChart
    'The ROC Chart form displays a Diagnostic Test performance point on a cross-plot of 1 - Specificity and Sensitivity.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                               <SplitterDistance><%= SplitContainer1.SplitterDistance %></SplitterDistance>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"

        If Main.Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Main.Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value <> Nothing Then Me.Left = Settings.<FormSettings>.<Left>.Value
            If Settings.<FormSettings>.<Top>.Value <> Nothing Then Me.Top = Settings.<FormSettings>.<Top>.Value
            If Settings.<FormSettings>.<Height>.Value <> Nothing Then Me.Height = Settings.<FormSettings>.<Height>.Value
            If Settings.<FormSettings>.<Width>.Value <> Nothing Then Me.Width = Settings.<FormSettings>.<Width>.Value

            'Add code to read other saved setting here:
            If Settings.<FormSettings>.<SplitterDistance>.Value <> Nothing Then SplitContainer1.SplitterDistance = Settings.<FormSettings>.<SplitterDistance>.Value

            CheckFormPos()
        End If
    End Sub

    Private Sub CheckFormPos()
        'Check that the form can be seen on a screen.

        Dim MinWidthVisible As Integer = 192 'Minimum number of X pixels visible. The form will be moved if this many form pixels are not visible.
        Dim MinHeightVisible As Integer = 64 'Minimum number of Y pixels visible. The form will be moved if this many form pixels are not visible.

        Dim FormRect As New Rectangle(Me.Left, Me.Top, Me.Width, Me.Height)
        Dim WARect As Rectangle = Screen.GetWorkingArea(FormRect) 'The Working Area rectangle - the usable area of the screen containing the form.

        'Check if the top of the form is above the top of the Working Area:
        If Me.Top < WARect.Top Then
            Me.Top = WARect.Top
        End If

        'Check if the top of the form is too close to the bottom of the Working Area:
        If (Me.Top + MinHeightVisible) > (WARect.Top + WARect.Height) Then
            Me.Top = WARect.Top + WARect.Height - MinHeightVisible
        End If

        'Check if the left edge of the form is too close to the right edge of the Working Area:
        If (Me.Left + MinWidthVisible) > (WARect.Left + WARect.Width) Then
            Me.Left = WARect.Left + WARect.Width - MinWidthVisible
        End If

        'Check if the right edge of the form is too close to the left edge of the Working Area:
        If (Me.Left + Me.Width - MinWidthVisible) < WARect.Left Then
            Me.Left = WARect.Left - Me.Width + MinWidthVisible
        End If

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message) 'Save the form settings before the form is minimised:
        If m.Msg = &H112 Then 'SysCommand
            If m.WParam.ToInt32 = &HF020 Then 'Form is being minimised
                SaveFormSettings()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Display Methods - Code used to display this form." '============================================================================================================================

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        RestoreFormSettings()   'Restore the form settings

        dgvRocData.ColumnCount = 8
        dgvRocData.Columns(0).HeaderText = "Test Name"
        dgvRocData.Columns(0).Width = 150
        dgvRocData.Columns(1).HeaderText = "Point Label"
        dgvRocData.Columns(1).Width = 120
        dgvRocData.Columns(2).HeaderText = "Specificity"
        dgvRocData.Columns(2).Width = 70
        dgvRocData.Columns(3).HeaderText = "1 - Specificity"
        dgvRocData.Columns(3).Width = 100
        dgvRocData.Columns(4).HeaderText = "Sensitivity"
        dgvRocData.Columns(4).Width = 70
        dgvRocData.Columns(5).HeaderText = "Point Color"
        dgvRocData.Columns(5).Width = 100
        dgvRocData.Columns(6).HeaderText = "Prevalence"
        dgvRocData.Columns(6).Width = 70
        dgvRocData.Columns(7).HeaderText = "Sample Size"
        dgvRocData.Columns(7).Width = 70

        PlotChart()

        'Get a list of Color names:
        For Each Color As KnownColor In [Enum].GetValues(GetType(KnownColor))
            If Color > 27 And Color < 168 Then
                cmbPointColor.Items.Add([Enum].GetName(GetType(KnownColor), Color)) 'AliceBlue to YellowGreen - System color names not included.
            End If
        Next



    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form
        Me.Close() 'Close the form
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Public Sub PlotChart()
        'Plot the ROC chart.

        Try
            chtRoc.Series.Clear()
            chtRoc.Series.Add("RefLine")

            'Display the Reference line:
            chtRoc.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Line
            chtRoc.Series(0).Color = Color.DarkGray
            chtRoc.Series(0).BorderWidth = 2
            chtRoc.Series(0).Points.Clear()
            chtRoc.Series(0).Points.AddXY(0, 0)
            chtRoc.Series(0).Points.AddXY(1, 1)
            'chtRoc.Series(0).Label = "Reference Line" 'This displays the Reference Line text at each end of the line.
            chtRoc.Series(0).LegendText = "Reference Line"

            chtRoc.ChartAreas(0).AxisX.Title = "1 - Specificity"
            chtRoc.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            chtRoc.ChartAreas(0).AxisX.Minimum = 0
            chtRoc.ChartAreas(0).AxisX.Maximum = 1
            chtRoc.ChartAreas(0).AxisX.MajorGrid.Interval = 0.2
            chtRoc.ChartAreas(0).AxisX.Interval = 0.2

            chtRoc.ChartAreas(0).AxisY.Title = "Sensitivity"
            chtRoc.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            'chtRoc.ChartAreas(0).AxisY.MajorGrid.Interval = Double.NaN
            chtRoc.ChartAreas(0).AxisY.IntervalAutoMode = False
            chtRoc.ChartAreas(0).AxisY.Minimum = 0
            chtRoc.ChartAreas(0).AxisY.Maximum = 1
            chtRoc.ChartAreas(0).AxisY.Interval = 0.2
            chtRoc.ChartAreas(0).AxisY.MajorGrid.Interval = 0.2
            chtRoc.ChartAreas(0).AxisY.MajorGrid.Enabled = True
            chtRoc.ChartAreas(0).AxisY.RoundAxisValues()
            chtRoc.ChartAreas(0).AxisY.MajorGrid.LineColor = Color.Black


            chtRoc.Titles.Clear()
            chtRoc.Titles.Add("Title1")
            chtRoc.Titles(0).Text = "Receiver Operating Characteristic (ROC)"
            chtRoc.Titles(0).Font = New Font("Arial", 16, FontStyle.Regular Or FontStyle.Bold)



            'Plot the data points.
            dgvRocData.AllowUserToAddRows = False
            Dim TestName As String = ""
            Dim IndexNo As Integer
            Dim XPos As Double
            Dim YPos As Double
            Dim PointLabel As String
            Dim PointColorName As String
            Dim PointNo As Integer
            For Each Row As DataGridViewRow In dgvRocData.Rows
                TestName = Row.Cells(0).Value
                PointLabel = Row.Cells(1).Value
                XPos = Row.Cells(3).Value
                YPos = Row.Cells(4).Value
                PointColorName = Row.Cells(5).Value
                IndexNo = chtRoc.Series.IndexOf(TestName)
                If IndexNo = -1 Then
                    chtRoc.Series.Add(TestName)
                    'chtRoc.Series(TestName).ChartType = DataVisualization.Charting.SeriesChartType.Line
                    If chkShowLine.Checked Then
                        chtRoc.Series(TestName).ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Else
                        chtRoc.Series(TestName).ChartType = DataVisualization.Charting.SeriesChartType.Point
                    End If
                    'chtRoc.Series(TestName).Color = Color.Red
                    chtRoc.Series(TestName).Color = Color.Black
                    chtRoc.Series(TestName).BorderWidth = 2
                    chtRoc.Series(TestName).ChartArea = chtRoc.ChartAreas(0).Name
                    'chtRoc.Series(TestName).Points.AddXY(XPos, YPos)
                    PointNo = chtRoc.Series(TestName).Points.AddXY(XPos, YPos)
                    chtRoc.Series(TestName).MarkerSize = 12
                    'chtRoc.Series(TestName).MarkerColor = Color.Red
                    chtRoc.Series(TestName).MarkerColor = Color.FromName(PointColorName)
                    chtRoc.Series(TestName).MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
                    chtRoc.Series(TestName).MarkerBorderWidth = 1
                    chtRoc.Series(TestName).MarkerBorderColor = Color.Black
                    chtRoc.Series(TestName).Points.Item(PointNo).Label = PointLabel

                    chtRoc.Series(TestName).Points.Item(PointNo).SetCustomProperty("RowNo", Row.Index.ToString)

                Else
                    'chtRoc.Series(TestName).Points.AddXY(XPos, YPos)
                    PointNo = chtRoc.Series(TestName).Points.AddXY(XPos, YPos)
                    chtRoc.Series(TestName).Points.Item(PointNo).Label = PointLabel
                    chtRoc.Series(TestName).Points.Item(PointNo).MarkerColor = Color.FromName(PointColorName)
                    chtRoc.Series(TestName).Points.Item(PointNo).SetCustomProperty("RowNo", Row.Index.ToString)
                End If
            Next
            dgvRocData.AllowUserToAddRows = True
        Catch ex As Exception

        End Try


    End Sub

    Private Sub btnSaveRoc_Click(sender As Object, e As EventArgs) Handles btnSaveRoc.Click
        'Save the ROC Chart.

        Dim FileName As String = Trim(txtRocFileName.Text)

        'Check if a file name has been specified:
        If FileName = "" Then
            Main.Message.AddWarning("Please enter a file name." & vbCrLf)
            Exit Sub
        End If

        'Check the file name extension:
        If LCase(FileName).EndsWith(".roc") Then
            FileName = IO.Path.GetFileNameWithoutExtension(FileName) & ".Roc"
        ElseIf FileName.Contains(".") Then
            Main.Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(FileName) & vbCrLf)
            Exit Sub
        Else
            FileName = FileName & ".Roc"
        End If

        txtRocFileName.Text = FileName

        dgvRocData.AllowUserToAddRows = False

        Dim RocData = <?xml version="1.0" encoding="utf-8"?>
                      <!---->
                      <RocData>
                          <Description><%= txtDescription.Text.Trim %></Description>
                          <ShowLine><%= chkShowLine.Checked %></ShowLine>
                          <ChartPoints>
                              <%= From item In dgvRocData.Rows
                                  Select
                                  <Item>
                                      <TestName><%= item.Cells(0).Value %></TestName>
                                      <PointLabel><%= item.Cells(1).Value %></PointLabel>
                                      <Specificity><%= item.Cells(2).Value %></Specificity>
                                      <OneMinusSpecificity><%= item.Cells(3).Value %></OneMinusSpecificity>
                                      <Sensitivity><%= item.Cells(4).Value %></Sensitivity>
                                      <PointColor><%= item.Cells(5).Value %></PointColor>
                                      <Prevalence><%= item.Cells(6).Value %></Prevalence>
                                      <SampleSize><%= item.Cells(7).Value %></SampleSize>
                                  </Item> %>
                          </ChartPoints>
                      </RocData>

        dgvRocData.AllowUserToAddRows = True

        Main.Project.SaveXmlData(FileName, RocData)
    End Sub

    Private Sub btnOpenRoc_Click(sender As Object, e As EventArgs) Handles btnOpenRoc.Click
        'Open a ROC Chart.

        Dim FileName As String = Main.Project.SelectDataFile("ROC Chart files", "Roc")

        If FileName = "" Then
            'No file has been selected.
        Else
            txtRocFileName.Text = FileName
            OpenRocChart(FileName)
        End If
    End Sub

    Private Sub OpenRocChart(ByVal FileName As String)
        'Open a ROC Chart file.

        Dim XDoc As System.Xml.Linq.XDocument
        Main.Project.ReadXmlData(FileName, XDoc)

        dgvRocData.Rows.Clear()
        txtDescription.Text = XDoc.<RocData>.<Description>.Value
        If XDoc.<RocData>.<ShowLine>.Value <> Nothing Then chkShowLine.Checked = XDoc.<RocData>.<ShowLine>.Value

        Dim ChartPoints = From item In XDoc.<RocData>.<ChartPoints>.<Item>
        Dim Prevalence As String
        Dim SampleSize As String
        For Each Item In ChartPoints
            If Item.<Prevalence>.Value <> Nothing Then
                Prevalence = Item.<Prevalence>.Value
            Else
                Prevalence = ""
            End If
            If Item.<SampleSize>.Value <> Nothing Then
                SampleSize = Item.<SampleSize>.Value
            Else
                SampleSize = ""
            End If
            'dgvRocData.Rows.Add(Item.<TestName>.Value, Item.<PointLabel>.Value, Item.<Specificity>.Value, Item.<OneMinusSpecificity>.Value, Item.<Sensitivity>.Value, Item.<PointColor>.Value)
            dgvRocData.Rows.Add(Item.<TestName>.Value, Item.<PointLabel>.Value, Item.<Specificity>.Value, Item.<OneMinusSpecificity>.Value, Item.<Sensitivity>.Value, Item.<PointColor>.Value, Prevalence, SampleSize)
        Next

        PlotChart()
    End Sub

    Private Sub chkShowLine_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowLine.CheckedChanged
        PlotChart()
    End Sub

    Private Sub chtRoc_MouseClick(sender As Object, e As MouseEventArgs) Handles chtRoc.MouseClick


        Dim Result As HitTestResult = chtRoc.HitTest(e.X, e.Y)
        If Result.ChartElementType = ChartElementType.DataPoint Then


            Dim I As Integer = Result.PointIndex
            Dim SeriesName As String = Result.Series.Name
            'Main.Message.Add("Series name: " & SeriesName & vbCrLf)
            'Main.Message.Add("Point number: " & I & vbCrLf)
            'Main.Message.Add("RowNo = " & chtRoc.Series(SeriesName).Points(I).GetCustomProperty("RowNo") & vbCrLf)

            DisplayRow(chtRoc.Series(SeriesName).Points(I).GetCustomProperty("RowNo"))
            dgvRocData.ClearSelection()
            dgvRocData.Rows(chtRoc.Series(SeriesName).Points(I).GetCustomProperty("RowNo")).Selected = True
            CalcSurveyResults()
        End If


    End Sub

    Private Sub dgvRocData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRocData.CellContentClick

        If dgvRocData.SelectedCells.Count > 0 Then
            Dim RowNo As Integer = dgvRocData.SelectedCells(0).RowIndex
            dgvRocData.Rows(RowNo).Selected = True
            DisplayRow(RowNo)
            CalcSurveyResults()
        End If
    End Sub

    Private Sub DisplayRow(ByVal RowNo As Integer)
        'Display the data in the selected row.

        'If dgvRocData.RowCount < RowNo Then
        If RowNo < dgvRocData.RowCount Then
            txtSensitivity.Text = dgvRocData.Rows(RowNo).Cells(4).Value
            txtSpecificity.Text = dgvRocData.Rows(RowNo).Cells(2).Value
            If chkLockPrevalence.Checked = False Then txtPrevalence.Text = dgvRocData.Rows(RowNo).Cells(6).Value
            If chkLockSampleSize.Checked = False Then txtSampleSize.Text = dgvRocData.Rows(RowNo).Cells(7).Value
            cmbPointColor.SelectedIndex = cmbPointColor.FindStringExact(dgvRocData.Rows(RowNo).Cells(5).Value)
        End If
    End Sub

    Private Sub cmbPointColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPointColor.SelectedIndexChanged
        txtPointColor.BackColor = Color.FromName(cmbPointColor.SelectedItem.ToString)

    End Sub

    Private Sub CalcSurveyResults()


        'Survey sample counts:
        Dim TP As Double 'True Positive survey test results 
        Dim TN As Double 'True Negative survey test results 
        Dim FP As Double 'False Positive survey test results 
        Dim FN As Double 'False Negative survey test results 

        'Survey analysis results:
        Dim Sensitivity As Double '(aka Recall) How many of thoose that are positive tested positive.
        Dim Specificity As Double 'How many of those that are negative tested negative.
        Dim Prevalence As Double  'The proportion of the population with the specified condition.
        Dim SampleSize As Double 'The number of samples used in the survey.

        If txtSampleSize.Text = "" Then Exit Sub
        SampleSize = txtSampleSize.Text
        If Main.Bayes.Settings.ProbabilityMeasure = "Percent" Then
            If txtSensitivity.Text = "" Then Exit Sub
            Sensitivity = txtSensitivity.Text.Replace("%", "")
            Sensitivity /= 100
            If txtSpecificity.Text = "" Then Exit Sub
            Specificity = txtSpecificity.Text.Replace("%", "")
            Specificity /= 100
            If txtPrevalence.Text = "" Then Exit Sub
            Prevalence = txtPrevalence.Text.Replace("%", "")
            Prevalence /= 100
        Else
            If txtSensitivity.Text = "" Then Exit Sub
            Sensitivity = txtSensitivity.Text
            If txtSpecificity.Text = "" Then Exit Sub
            Specificity = txtSpecificity.Text
            If txtPrevalence.Text = "" Then Exit Sub
            Prevalence = txtPrevalence.Text
        End If

        TP = Prevalence * SampleSize * Sensitivity
        TN = SampleSize * Specificity - Prevalence * SampleSize * Specificity
        FP = SampleSize - Prevalence * SampleSize - SampleSize * Specificity + Prevalence * SampleSize * Specificity
        FN = Prevalence * SampleSize - Prevalence * SampleSize * Sensitivity

        'Precision = TP / (TP + FP) 'How many of those testing positive are truly positive.
        'Accuracy = (TP + TN) / (TP + FP + FN + TN) 'How many of those tested were correctly identified as positive or negative.
        'F1_Score = 2 * Sensitivity * Precision / (Sensitivity + Precision) 'The harmonic mean of the Precision and Sensitivity.

        ''Display formatted values:
        'If Main.Bayes.Settings.ProbabilityMeasure = "Percent" Then
        '    txtCalcPrecision.Text = Format(Precision * 100, Bayes.Settings.PercentFormat) & "%"
        '    txtCalcAccuracy.Text = Format(Accuracy * 100, Bayes.Settings.PercentFormat) & "%"
        '    'txtCalcSensitivity.Text = Format(Sensitivity * 100, Bayes.Settings.PercentFormat) & "%"
        '    'txtCalcSpecificity.Text = Format(Specificity * 100, Bayes.Settings.PercentFormat) & "%"
        '    txtCalcF1Score.Text = Format(F1_Score * 100, Bayes.Settings.PercentFormat) & "%"
        'Else
        '    txtCalcPrecision.Text = Format(Precision, Bayes.Settings.DecimalFormat)
        '    txtCalcAccuracy.Text = Format(Accuracy, Bayes.Settings.DecimalFormat)
        '    'txtCalcSensitivity.Text = Format(Sensitivity, Bayes.Settings.DecimalFormat)
        '    'txtCalcSpecificity.Text = Format(Specificity, Bayes.Settings.DecimalFormat)
        '    txtCalcF1Score.Text = Format(F1_Score, Bayes.Settings.DecimalFormat)
        'End If

        txtTP.Text = Format(TP, Main.Bayes.Settings.SamplesFormat)
        txtTN.Text = Format(TN, Main.Bayes.Settings.SamplesFormat)
        txtFP.Text = Format(FP, Main.Bayes.Settings.SamplesFormat)
        txtFN.Text = Format(FN, Main.Bayes.Settings.SamplesFormat)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        'Update the data in the DataGridView.

        If dgvRocData.SelectedRows.Count = 1 Then
            dgvRocData.SelectedRows(0).Cells(5).Value = cmbPointColor.SelectedItem.ToString
            dgvRocData.SelectedRows(0).Cells(6).Value = txtPrevalence.Text
            dgvRocData.SelectedRows(0).Cells(7).Value = txtSampleSize.Text
        ElseIf dgvRocData.SelectedRows.Count = 0 Then
            Main.Message.AddWarning("Please select a row to update." & vbCrLf)
        Else
            Main.Message.AddWarning("More than one row has been selected." & vbCrLf)
            'Add code to update multiple rows.
        End If
    End Sub


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events that can be triggered by this form." '==========================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class