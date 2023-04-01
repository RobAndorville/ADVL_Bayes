Public Class frmSeriesAnalysis
    'Analysis and display of a data series.
    'Histograms, PDF and CDF charts.
    'Calculation of series statistics including mean, P50, P10 and P90 values.

#Region " Variable Declarations - All the variables used in this form and this application." '=================================================================================================

    Public Data As New DataSet  'Dataset used to hold the data values. 

    Dim RawPixelsPerInterval As Integer = 200 'The number of pixels per X Axis annotation interval

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _formNo As Integer = -1 'Multiple instances of this form can be displayed. FormNo is the index number of the form in SeriesAnalysisList.
    'If the form is included in Main.SeriesAnalysisList() then FormNo will be > -1 --> when exiting set Main.ClosedFormNo and call Main.SeriesAnalysisClosed(). 
    Public Property FormNo As Integer
        Get
            Return _formNo
        End Get
        Set(ByVal value As Integer)
            _formNo = value
        End Set
    End Property

    'Private _dataSource As Object = Nothing 'DataSource links to the oject containing the Data to be analysed. (Input, Processed, Distribution or MonteCarlo objects in the Main form.)
    'Property DataSource As Object
    '    Get
    '        Return _dataSource
    '    End Get
    '    Set(value As Object)
    '        _dataSource = value
    '        txtDatasetName.Text = DataSource.Name
    '        lblTableCount.Text = DataSource.Data.Tables.Count
    '        If DataSource.Data.Tables.Count > 0 Then
    '            lblTableNo.Text = "1"
    '            cmbSourceDataTable.Items.Clear()
    '            For Each item In DataSource.Data.Tables
    '                cmbSourceDataTable.Items.Add(item.TableName)
    '            Next
    '            cmbSourceDataTable.SelectedIndex = 0
    '            SourceTableName = cmbSourceDataTable.SelectedItem.ToString
    '            'NRows = DataSource.Data.Tables(0).Rows.Count
    '        Else
    '            lblTableNo.Text = "0"
    '        End If
    '    End Set
    'End Property

    'Private _dataSourceDescription As String = "" 'A description of the data source object.
    'Property DataSourceDescription As String
    '    Get
    '        Return _dataSourceDescription
    '    End Get
    '    Set(value As String)
    '        _dataSourceDescription = value
    '        txtDataSource.Text = _dataSourceDescription
    '    End Set
    'End Property

    Private _sourceTableName As String = "" 'The name of the selected table in the DataSource.
    Property SourceTableName As String
        Get
            Return _sourceTableName
        End Get
        Set(value As String)
            _sourceTableName = value
            Debug.WriteLine("Set SourceTableName")
            'If DataSource Is Nothing Then
            '    Main.Message.AddWarning("The data source is empty." & vbCrLf)
            'Else

            'Get the list of tables:
            cmbSourceDataTable.Items.Clear()
            For Each item In Main.BayesSim.Data.Tables
                cmbSourceDataTable.Items.Add(item.TableName)
            Next

            If Main.BayesSim.Data.Tables.Contains(_sourceTableName) Then
                lblTableCount.Text = Main.BayesSim.Data.Tables.Count
                cmbSourceDataTable.SelectedIndex = cmbSourceDataTable.FindStringExact(_sourceTableName)
                lblTableNo.Text = cmbSourceDataTable.SelectedIndex + 1
                'ShowAllFieldStats()

                lblColCount.Text = Main.BayesSim.Data.Tables(_sourceTableName).Columns.Count
                lblColNo.Text = "0"

                NRows = Main.BayesSim.Data.Tables(_sourceTableName).Rows.Count

                'Get the list of columns:
                cmbSourceColumnName.Items.Clear()
                For Each item In Main.BayesSim.Data.Tables(_sourceTableName).Columns
                    cmbSourceColumnName.Items.Add(item.ColumnName)
                Next

                'lblTableCount.Text = Main.BayesSim.Data.Tables.Count
                'cmbSourceDataTable.SelectedIndex = cmbSourceColumnName.FindStringExact(_sourceTableName)
                'lblTableNo.Text = cmbSourceDataTable.SelectedIndex + 1

                lblStats4.Text = "Number of survey repeats used to generate the statistics: " & Format(Main.BayesSim.Data.Tables(_sourceTableName).Rows.Count, "N0")
            Else
                Main.Message.AddWarning("A table named " & _sourceTableName & " was not found." & vbCrLf)
                lblStats4.Text = "Number of survey repeats used to generate the statistics: "
            End If
            'End If

        End Set
    End Property

    Private _sourceColumnName As String = "" 'The name of the column in the Source Table containing the data series.
    Property SourceColumnName As String
        Get
            Return _sourceColumnName
        End Get
        Set(value As String)
            _sourceColumnName = value
            Debug.WriteLine("Set SourceColumnName")
            If Main.BayesSim.Data.Tables.Contains(SourceTableName) Then
                If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(_sourceColumnName) Then
                    cmbSourceColumnName.SelectedIndex = cmbSourceColumnName.FindStringExact(_sourceColumnName)
                    lblColNo.Text = cmbSourceColumnName.SelectedIndex + 1
                    ShowSeriesStats()
                    lblStats2.Text = "Event name: " & _sourceColumnName
                Else
                    Main.Message.AddWarning("A column named " & _sourceColumnName & " was not found." & vbCrLf)
                    lblStats2.Text = "Event name: "
                End If
            End If
        End Set
    End Property




    'Private _scalarName As String = "" 'The name of the selected scalar 
    'Property ScalarName As String
    '    Get
    '        Return _scalarName
    '    End Get
    '    Set(value As String)
    '        _scalarName = value
    '    End Set
    'End Property

    'Private _isDiscrete As Boolean = False 'If True the distribution is discrete else continuous.
    Private _isDiscrete As Boolean = True 'If True the distribution is discrete else continuous. (This form will be used to analyse trial data so IsDiscrete will be True.)
    Property IsDiscrete As Boolean
        Get
            Return _isDiscrete
        End Get
        Set(value As Boolean)
            _isDiscrete = value
            If _isDiscrete Then
                txtContDisc.Text = "Discrete"
            Else
                txtContDisc.Text = "Continuous"
            End If
        End Set
    End Property

    Private _nRows As Integer = 0 'The number of rows in the selected table.
    Property NRows As Integer
        Get
            Return _nRows
        End Get
        Set(value As Integer)
            _nRows = value
            txtNRows.Text = _nRows
        End Set
    End Property

    Private _nTrials As Integer = 1000 'The number of trials in the survey.
    Property NTrials As Integer
        Get
            Return _nTrials
        End Get
        Set(value As Integer)
            _nTrials = value
            lblStats3.Text = "Survey sample size: " & Format(_nTrials, "N0")
        End Set
    End Property

    Private _nRepeats As Integer = 10000 'The number of times the survey is repeated to generate the probability uncertainty distribution.
    Property NRepeats As Integer
        Get
            Return _nRepeats
        End Get
        Set(value As Integer)
            _nRepeats = value
        End Set
    End Property

    Private _minSeriesVal As Double = 0 'The minimum data value in the series 
    Property MinSeriesVal As Double
        Get
            Return _minSeriesVal
        End Get
        Set(value As Double)
            _minSeriesVal = value
            txtDataMin.Text = _minSeriesVal
            txtDataRange.Text = _maxSeriesVal - _minSeriesVal
        End Set
    End Property

    Private _maxSeriesVal As Double = 1 'The maximum data value in the series
    Property MaxSeriesVal As Double
        Get
            Return _maxSeriesVal
        End Get
        Set(value As Double)
            _maxSeriesVal = value
            txtDataMax.Text = _maxSeriesVal
            txtDataRange.Text = _maxSeriesVal - _minSeriesVal
        End Set
    End Property

    Private _isSample As Boolean = True 'If True, the data is a sample of the population. If False, the data is the whole population.
    Property IsSample As Boolean
        Get
            Return _isSample
        End Get
        Set(value As Boolean)
            _isSample = value
            If _isSample Then
                rbSample.Checked = True
            Else
                rbPopulation.Checked = True
            End If
            'If DataSource Is Nothing Then
            'Else
            If Main.BayesSim.Data.Tables(SourceTableName) Is Nothing Then
            Else
                If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(SourceColumnName) Then
                    If XAxisValues = "SurveyCount" Then 'Display the Survey Count variance and standard deviation:
                        Dim Var As Double = Variance(IsSample)
                        SeriesVariance = Var
                        SeriesStdDev = Math.Sqrt(Var) 'Save the Standard Deviation in this proerty for later calculations.
                        txtVariance.Text = Format(Var, txtVarFormat.Text)
                        'txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevFormat.Text)
                        txtStdDev.Text = Format(SeriesStdDev, txtStdDevFormat.Text)
                    Else 'Display the Probability variance and standard deviation:
                        Dim Var As Double = ProbVariance(IsSample)
                        SeriesVariance = Var
                        SeriesStdDev = Math.Sqrt(Var) 'Save the Standard Deviation in this proerty for later calculations.
                        If Main.BayesSim.Settings.ProbabilityMeasure = "Percent" Then
                            txtVariance.Text = Format(Var * 100, txtVarPctFormat.Text) & "%"
                            'txtStdDev.Text = Format(Math.Sqrt(Var) * 100, txtStdDevPctFormat.Text) & "%"
                            txtStdDev.Text = Format(SeriesStdDev * 100, txtStdDevPctFormat.Text) & "%"
                        Else
                            txtVariance.Text = Format(Var, txtVarDecFormat.Text)
                            'txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevDecFormat.Text)
                            txtStdDev.Text = Format(SeriesStdDev, txtStdDevDecFormat.Text)
                        End If

                    End If
                Else
                End If
            End If
            'End If

            'If Main.BayesSim.Data.Tables(TableName) Is Nothing Then
            'Else
            '    If Main.BayesSim.Data.Tables(TableName).Columns.Contains(ColumnName) Then
            '        Dim Var As Double = Variance(IsSample)
            '        txtVariance.Text = Format(Var, txtVarFormat.Text)
            '        txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevFormat.Text)
            '    Else
            '    End If
            'End If

        End Set
    End Property

    Private _seriesAverage As Double = 0 'The series average. The double precision value is saved here. The value in the Statistics text box may have reduced decimal places and may be expressed as a percentage.
    Property SeriesAverage As Double
        Get
            Return _seriesAverage
        End Get
        Set(value As Double)
            _seriesAverage = value
        End Set
    End Property

    Private _seriesVariance As Double = 0 'The series variance. The double precision value is saved here. The value in the Statistics text box may have reduced decimal places and may be expressed as a percentage.
    Property SeriesVariance As Double
        Get
            Return _seriesVariance
        End Get
        Set(value As Double)
            _seriesVariance = value
        End Set
    End Property


    Private _seriesStdDev As Double = 0 'The series standard deviation value. The double precision value is saved here. The value in the Statistics text box may have reduced decimal places and may be expressed as a percentage.
    Property SeriesStdDev As Double
        Get
            Return _seriesStdDev
        End Get
        Set(value As Double)
            _seriesStdDev = value
        End Set
    End Property

    Private _xAxisValues As String = "SurveyCount" 'The values to be displayed along the X Axis (SurveyCount or SurveyProb). SurveyCount is the count of the events in the survey. SurveyProb is the probability of the event based on the survey count.
    Property XAxisValues As String
        Get
            Return _xAxisValues
        End Get
        Set(value As String)
            _xAxisValues = value
        End Set
    End Property

    Private _showReverseCDF As Boolean = False 'If True, the Reverse Cumulative Distribution Function is shown
    Property ShowReverseCDF As Boolean
        Get
            Return _showReverseCDF
        End Get
        Set(value As Boolean)
            _showReverseCDF = value
            If _showReverseCDF = True Then
                rbReverseCdf.Checked = True
            Else
                rbCdf.Checked = True
            End If
        End Set
    End Property

    Private _initNIntervals As Integer = 10 'The initial number of intervals - this is refined using preferred interval settings
    Property InitNIntervals As Integer
        Get
            Return _initNIntervals
        End Get
        Set(value As Integer)
            _initNIntervals = value
        End Set
    End Property

    'Private _nIntervals As Integer = 50 'The number of intervals to use in the histogram
    Private _histNIntervals As Integer = 50 'The number of intervals to use in the histogram
    'Property NIntervals As Integer
    Property HistNIntervals As Integer
        Get
            Return _histNIntervals
        End Get
        Set(value As Integer)
            _histNIntervals = value
            txtNIntervals.Text = _histNIntervals
            txtIntervalWidth.Text = (MaxSeriesVal - MinSeriesVal) / _histNIntervals
        End Set
    End Property

    Private _histIntervalWidth As Double 'The data width of each column in the histogram
    Property HistIntervalWidth As Double
        Get
            Return _histIntervalWidth
        End Get
        Set(value As Double)
            _histIntervalWidth = value
        End Set
    End Property

    Private _histMin As Double 'The midpoint value of the minimum column in the histogram
    Property HistMin As Double
        Get
            Return _histMin
        End Get
        Set(value As Double)
            _histMin = value
        End Set
    End Property

    Private _histMax As Double 'The midpoint value of the maximum column in the histogram
    Property HistMax As Double
        Get
            Return _histMax
        End Get
        Set(value As Double)
            _histMax = value
        End Set
    End Property

    Private _chartXMin As Double 'The chart X Axis minimum 
    Property ChartXMin As Double
        Get
            Return _chartXMin
        End Get
        Set(value As Double)
            _chartXMin = value
        End Set
    End Property

    Private _chartXMax As Double 'The chart X Axis maximum
    Property ChartXMax As Double
        Get
            Return _chartXMax
        End Get
        Set(value As Double)
            _chartXMax = value
        End Set
    End Property

    Private _chartXInterval As Double 'The Chart X Axis label interval
    Property ChartXInterval As Double
        Get
            Return _chartXInterval
        End Get
        Set(value As Double)
            _chartXInterval = value
        End Set
    End Property


    Private _tableName As String = "" 'The calculated data table name selected for display in the Data tab. (The calculated data includes the CDF points and the calculated histogram values.)
    Property TableName As String
        Get
            Return _tableName
        End Get
        Set(value As String)
            _tableName = value

            '29Sep21 - Removing this code. This may be causing an error by changing the SourceTableName!!!
            ''Set up list of available tables:
            'txtDatasetName.Text = "Bayes Simulation"
            'lblTableCount.Text = Main.BayesSim.Data.Tables.Count
            'If Main.BayesSim.Data.Tables.Count > 0 Then
            '    lblTableNo.Text = "1"
            '    cmbSourceDataTable.Items.Clear()
            '    For Each item In Main.BayesSim.Data.Tables
            '        cmbSourceDataTable.Items.Add(item.TableName)
            '    Next
            '    cmbSourceDataTable.SelectedIndex = 0
            '    SourceTableName = cmbSourceDataTable.SelectedItem.ToString
            '    'NRows = DataSource.Data.Tables(0).Rows.Count
            'Else
            '    lblTableNo.Text = "0"
            'End If


            cmbTableName.SelectedIndex = cmbTableName.FindStringExact(_tableName)
            UpdateDataGridView()
        End Set
    End Property

    'Private _calcTableName As String = "" 'The calculated data table name selected for display in the Data tab. (The calculated data includes the CDF points and the calculated histogram values.)
    'Property CalcTableName As String
    '    Get
    '        Return _calcTableName
    '    End Get
    '    Set(value As String)
    '        _calcTableName = value
    '        cmbTableName.SelectedIndex = cmbTableName.FindStringExact(_calcTableName)
    '        UpdateDataGridView()
    '    End Set
    'End Property

    'Private _tableName As String = "" 'The name of the selected table.
    'Property TableName As String
    '    Get
    '        Return _tableName
    '    End Get
    '    Set(value As String)
    '        _tableName = value

    '        If Main.BayesSim.Data.Tables.Contains(_tableName) Then
    '            cmbSourceDataTable.SelectedIndex = cmbSourceDataTable.FindStringExact(_tableName)
    '            lblTableNo.Text = cmbSourceDataTable.SelectedIndex + 1

    '            lblColCount.Text = Main.BayesSim.Data.Tables(_tableName).Columns.Count
    '            lblColNo.Text = "0"

    '            NRows = Main.BayesSim.Data.Tables(_tableName).Rows.Count

    '            'Get the list of columns:
    '            cmbSourceColumnName.Items.Clear()
    '            For Each item In Main.BayesSim.Data.Tables(_tableName).Columns
    '                cmbSourceColumnName.Items.Add(item.ColumnName)
    '            Next
    '        Else
    '            Main.Message.AddWarning("A table named " & _tableName & " was not found." & vbCrLf)
    '        End If

    '    End Set
    'End Property

    'Private _columnName As String = "" 'The name of the column selected for the series analysis.
    'Property ColumnName As String
    '    Get
    '        Return _columnName
    '    End Get
    '    Set(value As String)
    '        _columnName = value

    '        If Main.BayesSim.Data.Tables.Contains(TableName) Then
    '            If Main.BayesSim.Data.Tables(TableName).Columns.Contains(_columnName) Then
    '                cmbSourceColumnName.SelectedIndex = cmbSourceColumnName.FindStringExact(_columnName)
    '                lblColNo.Text = cmbSourceColumnName.SelectedIndex + 1
    '                ShowSeriesStats()
    '            Else
    '                Main.Message.AddWarning("A column named " & _columnName & " was not found." & vbCrLf)
    '            End If
    '        End If


    '    End Set
    'End Property

    'Distribution Model properties:
    Private _distributionName As String = "" 'The name of the model distribution
    Property DistributionName As String
        Get
            Return _distributionName
        End Get
        Set(value As String)
            _distributionName = value
            txtDistributionName.Text = _distributionName
            If _distributionName = "" Then
                chkShowModel.Checked = False
                chkShowModel.Enabled = False
                txtDistributionName.Enabled = False
                txtContDisc.Enabled = False
                txtParamAName.Enabled = False
                txtParamAValue.Enabled = False
                txtParamBName.Enabled = False
                txtParamBValue.Enabled = False
                txtParamCName.Enabled = False
                txtParamCValue.Enabled = False
                txtParamDName.Enabled = False
                txtParamDValue.Enabled = False
                txtParamEName.Enabled = False
                txtParamEValue.Enabled = False
            Else
                'chkShowModel.Checked = True
                chkShowModel.Enabled = True
                txtDistributionName.Enabled = True
                txtContDisc.Enabled = True
                txtParamAName.Enabled = True
                txtParamAValue.Enabled = True
                txtParamBName.Enabled = True
                txtParamBValue.Enabled = True
                txtParamCName.Enabled = True
                txtParamCValue.Enabled = True
                txtParamDName.Enabled = True
                txtParamDValue.Enabled = True
                txtParamEName.Enabled = True
                txtParamEValue.Enabled = True
            End If
        End Set
    End Property

    Private _paramAName As String = "" 'The name of the first distribution parameter
    Property ParamAName As String
        Get
            Return _paramAName
        End Get
        Set(value As String)
            _paramAName = value
            txtParamAName.Text = _paramAName
        End Set
    End Property

    Private _paramAValue As Double = Double.NaN 'The value of the first distribution parameter
    Property ParamAValue As Double
        Get
            Return _paramAValue
        End Get
        Set(value As Double)
            _paramAValue = value
            txtParamAValue.Text = _paramAValue
        End Set
    End Property

    Private _paramBName As String = "" 'The name of the second distribution parameter
    Property ParamBName As String
        Get
            Return _paramBName
        End Get
        Set(value As String)
            _paramBName = value
            txtParamBName.Text = _paramBName
        End Set
    End Property

    Private _paramBValue As Double = Double.NaN 'The value of the second distribution parameter
    Property ParamBValue As Double
        Get
            Return _paramBValue
        End Get
        Set(value As Double)
            _paramBValue = value
            txtParamBValue.Text = _paramBValue
        End Set
    End Property

    Private _paramCName As String = "" 'The name of the third distribution parameter
    Property ParamCName As String
        Get
            Return _paramCName
        End Get
        Set(value As String)
            _paramCName = value
            txtParamCName.Text = _paramCName
        End Set
    End Property

    Private _paramCValue As Double = Double.NaN 'The value of the third distribution parameter
    Property ParamCValue As Double
        Get
            Return _paramCValue
        End Get
        Set(value As Double)
            _paramCValue = value
            txtParamCValue.Text = _paramCValue
        End Set
    End Property

    Private _paramDName As String = "" 'The name of the fourth distribution parameter
    Property ParamDName As String
        Get
            Return _paramDName
        End Get
        Set(value As String)
            _paramDName = value
            txtParamDName.Text = _paramDName
        End Set
    End Property

    Private _paramDValue As Double = Double.NaN 'The value of the fourth distribution parameter
    Property ParamDValue As Double
        Get
            Return _paramDValue
        End Get
        Set(value As Double)
            _paramDValue = value
            txtParamDValue.Text = _paramDValue
        End Set
    End Property

    Private _paramEName As String = "" 'The name of the fifth distribution parameter
    Property ParamEName As String
        Get
            Return _paramEName
        End Get
        Set(value As String)
            _paramEName = value
            txtParamEName.Text = _paramEName
        End Set
    End Property

    Private _paramEValue As Double = Double.NaN 'The value of the fifth distribution parameter
    Property ParamEValue As Double
        Get
            Return _paramEValue
        End Get
        Set(value As Double)
            _paramEValue = value
            txtParamEValue.Text = _paramEValue
        End Set
    End Property

    ''Binomial Distribution Parameters:
    'Private _nTrials As Integer = 1000 'The number of trials
    'Property NTrials As Integer
    '    Get
    '        Return _nTrials
    '    End Get
    '    Set(value As Integer)
    '        _nTrials = value
    '    End Set
    'End Property

    'Private _pSuccess As Double = 0.1 'The probability of success in one trial.
    'Property PSuccess As Double
    '    Get
    '        Return _pSuccess
    '    End Get
    '    Set(value As Double)
    '        _pSuccess = value
    '    End Set
    'End Property


#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------

#Region " Process XML files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.

        dgvAnnot.AllowUserToAddRows = False 'This removed the last blank line

        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <!---->
                               <IsSample><%= IsSample %></IsSample>
                               <MinCountFormat><%= txtMinFormat.Text %></MinCountFormat>
                               <MaxCountFormat><%= txtMaxFormat.Text %></MaxCountFormat>
                               <SumCountFormat><%= txtSumFormat.Text %></SumCountFormat>
                               <AvgCountFormat><%= txtAvgFormat.Text %></AvgCountFormat>
                               <StdDevCountFormat><%= txtStdDevFormat.Text %></StdDevCountFormat>
                               <VarCountFormat><%= txtVarFormat.Text %></VarCountFormat>
                               <MinDecFormat><%= txtMinDecFormat.Text %></MinDecFormat>
                               <MaxDecFormat><%= txtMaxDecFormat.Text %></MaxDecFormat>
                               <SumDecFormat><%= txtSumDecFormat.Text %></SumDecFormat>
                               <AvgDecFormat><%= txtAvgDecFormat.Text %></AvgDecFormat>
                               <StdDevDecFormat><%= txtStdDevDecFormat.Text %></StdDevDecFormat>
                               <VarDecFormat><%= txtVarDecFormat.Text %></VarDecFormat>
                               <MinPctFormat><%= txtMinPctFormat.Text %></MinPctFormat>
                               <MaxPctFormat><%= txtMaxPctFormat.Text %></MaxPctFormat>
                               <SumPctFormat><%= txtSumPctFormat.Text %></SumPctFormat>
                               <AvgPctFormat><%= txtAvgPctFormat.Text %></AvgPctFormat>
                               <StdDevPctFormat><%= txtStdDevPctFormat.Text %></StdDevPctFormat>
                               <VarPctFormat><%= txtVarPctFormat.Text %></VarPctFormat>
                               <ShowReverseCDF><%= ShowReverseCDF %></ShowReverseCDF>
                               <ShowModel><%= chkShowModel.Checked %></ShowModel>
                               <!---->
                               <Annotations>
                                   <%= From item In dgvAnnot.Rows
                                       Select
                                       <Item>
                                           <CdfChartChecked><%= item.Cells(0).Value %></CdfChartChecked>
                                           <HistogramChecked><%= item.Cells(1).Value %></HistogramChecked>
                                           <Type><%= item.Cells(2).Value %></Type>
                                           <Parameter><%= item.Cells(3).Value %></Parameter>
                                           <Label><%= item.Cells(4).Value %></Label>
                                           <CountFormat><%= item.Cells(7).Value %></CountFormat>
                                           <DecProbFormat><%= item.Cells(8).Value %></DecProbFormat>
                                           <PctProbFormat><%= item.Cells(9).Value %></PctProbFormat>
                                       </Item> %>
                               </Annotations>
                           </FormSettings>

        dgvAnnot.AllowUserToAddRows = True

        'Add code to include other settings to save after the comment line <!---->

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        'This version of the settings file name depends on the Monte Carlo model label and the Random Variable name:
        'Dim SettingsFileName As String = "FormSettings_" & Main.MonteCarlo.Label & "_" & SourceColumnName & "_" & Me.Text & ".xml"
        'Dim SettingsFileName As String = "FormSettings_Sim_" & Main.BayesSim.AnnotTitle.Text & "_" & ColumnName & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_Sim_" & Main.BayesSim.AnnotTitle.Text & "_" & SourceColumnName & "_" & Me.Text & ".xml"

        Main.Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        'Dim SettingsFileName As String = "FormSettings_" & Main.ApplicationInfo.Name & "_" & Me.Text & ".xml"
        'This version of the settings file name depends on the Monte Carlo model label and the Random Variable name:
        'Dim SettingsFileName As String = "FormSettings_" & Main.MonteCarlo.Label & "_" & SourceColumnName & "_" & Me.Text & ".xml"
        'NOTE: SourceColumnName is not avaiable when this method is run. Use Main.txtColumnName instead:
        'Dim SettingsFileName As String = "FormSettings_" & Main.MonteCarlo.Label & "_" & Main.txtColumnName.Text.Trim & "_" & Me.Text & ".xml"
        'NOTE: SourceColumnName is now set before the form is shown!
        'Dim SettingsFileName As String = "FormSettings_" & Main.MonteCarlo.Label & "_" & SourceColumnName & "_" & Me.Text & ".xml"
        'Dim SettingsFileName As String = "FormSettings_Sim_" & Main.BayesSim.AnnotTitle.Text & "_" & ColumnName & "_" & Me.Text & ".xml"
        Dim SettingsFileName As String = "FormSettings_Sim_" & Main.BayesSim.AnnotTitle.Text & "_" & SourceColumnName & "_" & Me.Text & ".xml"

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
            If Settings.<FormSettings>.<IsSample>.Value <> Nothing Then IsSample = Settings.<FormSettings>.<IsSample>.Value

            If Settings.<FormSettings>.<MinFormat>.Value <> Nothing Then txtMinFormat.Text = Settings.<FormSettings>.<MinFormat>.Value
            If Settings.<FormSettings>.<MaxFormat>.Value <> Nothing Then txtMaxFormat.Text = Settings.<FormSettings>.<MaxFormat>.Value
            If Settings.<FormSettings>.<SumFormat>.Value <> Nothing Then txtSumFormat.Text = Settings.<FormSettings>.<SumFormat>.Value
            If Settings.<FormSettings>.<AvgFormat>.Value <> Nothing Then txtAvgFormat.Text = Settings.<FormSettings>.<AvgFormat>.Value
            If Settings.<FormSettings>.<StdDevFormat>.Value <> Nothing Then txtStdDevFormat.Text = Settings.<FormSettings>.<StdDevFormat>.Value
            If Settings.<FormSettings>.<VarFormat>.Value <> Nothing Then txtVarFormat.Text = Settings.<FormSettings>.<VarFormat>.Value

            If Settings.<FormSettings>.<MinCountFormat>.Value <> Nothing Then txtMinFormat.Text = Settings.<FormSettings>.<MinCountFormat>.Value
            If Settings.<FormSettings>.<MaxCountFormat>.Value <> Nothing Then txtMaxFormat.Text = Settings.<FormSettings>.<MaxCountFormat>.Value
            If Settings.<FormSettings>.<SumCountFormat>.Value <> Nothing Then txtSumFormat.Text = Settings.<FormSettings>.<SumCountFormat>.Value
            If Settings.<FormSettings>.<AvgCountFormat>.Value <> Nothing Then txtAvgFormat.Text = Settings.<FormSettings>.<AvgCountFormat>.Value
            If Settings.<FormSettings>.<StdDevCountFormat>.Value <> Nothing Then txtStdDevFormat.Text = Settings.<FormSettings>.<StdDevCountFormat>.Value
            If Settings.<FormSettings>.<VarCountFormat>.Value <> Nothing Then txtVarFormat.Text = Settings.<FormSettings>.<VarCountFormat>.Value

            If Settings.<FormSettings>.<MinDecFormat>.Value <> Nothing Then txtMinDecFormat.Text = Settings.<FormSettings>.<MinDecFormat>.Value
            If Settings.<FormSettings>.<MaxDecFormat>.Value <> Nothing Then txtMaxDecFormat.Text = Settings.<FormSettings>.<MaxDecFormat>.Value
            If Settings.<FormSettings>.<SumDecFormat>.Value <> Nothing Then txtSumDecFormat.Text = Settings.<FormSettings>.<SumDecFormat>.Value
            If Settings.<FormSettings>.<AvgDecFormat>.Value <> Nothing Then txtAvgDecFormat.Text = Settings.<FormSettings>.<AvgDecFormat>.Value
            If Settings.<FormSettings>.<StdDevDecFormat>.Value <> Nothing Then txtStdDevDecFormat.Text = Settings.<FormSettings>.<StdDevDecFormat>.Value
            If Settings.<FormSettings>.<VarDecFormat>.Value <> Nothing Then txtVarDecFormat.Text = Settings.<FormSettings>.<VarDecFormat>.Value

            If Settings.<FormSettings>.<MinPctFormat>.Value <> Nothing Then txtMinPctFormat.Text = Settings.<FormSettings>.<MinPctFormat>.Value
            If Settings.<FormSettings>.<MaxPctFormat>.Value <> Nothing Then txtMaxPctFormat.Text = Settings.<FormSettings>.<MaxPctFormat>.Value
            If Settings.<FormSettings>.<SumPctFormat>.Value <> Nothing Then txtSumPctFormat.Text = Settings.<FormSettings>.<SumPctFormat>.Value
            If Settings.<FormSettings>.<AvgPctFormat>.Value <> Nothing Then txtAvgPctFormat.Text = Settings.<FormSettings>.<AvgPctFormat>.Value
            If Settings.<FormSettings>.<StdDevPctFormat>.Value <> Nothing Then txtStdDevPctFormat.Text = Settings.<FormSettings>.<StdDevPctFormat>.Value
            If Settings.<FormSettings>.<VarPctFormat>.Value <> Nothing Then txtVarPctFormat.Text = Settings.<FormSettings>.<VarPctFormat>.Value

            If Settings.<FormSettings>.<ShowReverseCDF>.Value <> Nothing Then
                ShowReverseCDF = Settings.<FormSettings>.<ShowReverseCDF>.Value
                'If ShowReverseCDF Then
                '    RecalcSeriesTable() 'Recalculate the series table. It would have originally calculated the non-reverse CDF by default.
                '    PlotCDF()
                '    UpdateAnnotation()
                'End If
            End If

            If Settings.<FormSettings>.<ShowModel>.Value <> Nothing Then
                If DistributionName = "" Then
                    chkShowModel.Checked = False 'Don't try to show the Distribution Model if the Distribution name has not been provided!
                Else
                    chkShowModel.Checked = Settings.<FormSettings>.<ShowModel>.Value
                End If
                'chkShowModel.Checked = Settings.<FormSettings>.<ShowModel>.Value
            End If



            'Restore annotations:
            Dim Annotations = From item In Settings.<FormSettings>.<Annotations>.<Item>
            For Each Item In Annotations
                'If Item.<Format>.Value = Nothing Then
                If Item.<Format>.Value = Nothing And Item.<CountFormat>.Value = Nothing Then
                    dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, "")
                    'Else
                ElseIf Item.<Format>.Value <> Nothing Then
                    dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, Item.<Format>.Value)
                Else
                    'dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, Item.<Format>.Value)
                    dgvAnnot.Rows.Add(Item.<CdfChartChecked>.Value, Item.<HistogramChecked>.Value, Item.<Type>.Value, Item.<Parameter>.Value, Item.<Label>.Value, 0, 0, Item.<CountFormat>.Value, Item.<DecProbFormat>.Value, Item.<PctProbFormat>.Value)
                End If
            Next

            'dgvAnnot.AutoResizeColumns(autoSizeColumnsMode:=DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader)
            dgvAnnot.AutoResizeColumns()

            CheckFormPos()
        Else
            ApplyDefaultAnnotSettings()
            ResetAnnotFormats()
            ResetStatFormats()
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

        rbSample.Checked = True
        'NOTE: The default interval has been set before the form is loaded!:
        'NIntervals = 50

        'dgvAnnot.ColumnCount = 5
        dgvAnnot.ColumnCount = 7

        Dim chkAnnotCdf As New DataGridViewCheckBoxColumn
        dgvAnnot.Columns.Insert(0, chkAnnotCdf)
        dgvAnnot.Columns(0).HeaderText = "CDF Chart"
        dgvAnnot.Columns(0).Width = 40

        Dim chkAnnotHist As New DataGridViewCheckBoxColumn
        dgvAnnot.Columns.Insert(1, chkAnnotHist)
        dgvAnnot.Columns(1).HeaderText = "Hist Chart"
        dgvAnnot.Columns(1).Width = 40

        Dim cboAnnotType As New DataGridViewComboBoxColumn
        cboAnnotType.Items.Add("Probability")
        cboAnnotType.Items.Add("Value")
        cboAnnotType.Items.Add("Mean")
        cboAnnotType.Items.Add("Standard Deviation")
        dgvAnnot.Columns.Insert(2, cboAnnotType)
        dgvAnnot.Columns(2).HeaderText = "Annotation Type"
        dgvAnnot.Columns(2).Width = 120

        dgvAnnot.Columns(3).HeaderText = "Parameter"
        dgvAnnot.Columns(3).Width = 60
        dgvAnnot.Columns(4).HeaderText = "Label"
        dgvAnnot.Columns(4).Width = 60
        dgvAnnot.Columns(5).HeaderText = "Probability"
        dgvAnnot.Columns(5).Width = 70
        dgvAnnot.Columns(6).HeaderText = "Value"
        dgvAnnot.Columns(6).Width = 100
        'dgvAnnot.Columns(7).HeaderText = "Format"
        dgvAnnot.Columns(7).HeaderText = "Count Format"
        dgvAnnot.Columns(7).Width = 50
        dgvAnnot.Columns(8).HeaderText = "Dec Prob Format"
        dgvAnnot.Columns(8).Width = 50
        dgvAnnot.Columns(9).HeaderText = "Pct Prob Format"
        dgvAnnot.Columns(9).Width = 50

        dgvAnnot.AutoResizeColumns()

        rbCdf.Checked = True 'Select ShowInverseCdf = False by default.
        chkShowModel.Checked = True 'Show Distribution Model by default.

        'Selection of image formats used to save the chart to the clipboard:
        cmbImageFormat.Items.Add("Jpeg")
        cmbImageFormat.Items.Add("Png")
        cmbImageFormat.Items.Add("Bmp")
        cmbImageFormat.Items.Add("Gif")
        cmbImageFormat.Items.Add("Tiff")
        cmbImageFormat.SelectedIndex = 0

        'THIS CODE HAS BEEN MOVED TO THE TableName PROPERTY:
        ''Set up list of available tables:
        'txtDatasetName.Text = "Bayes Simulation"
        'lblTableCount.Text = Main.BayesSim.Data.Tables.Count
        'If Main.BayesSim.Data.Tables.Count > 0 Then
        '    lblTableNo.Text = "1"
        '    cmbSourceDataTable.Items.Clear()
        '    For Each item In Main.BayesSim.Data.Tables
        '        cmbSourceDataTable.Items.Add(item.TableName)
        '    Next
        '    cmbSourceDataTable.SelectedIndex = 0
        '    SourceTableName = cmbSourceDataTable.SelectedItem.ToString
        '    'NRows = DataSource.Data.Tables(0).Rows.Count
        'Else
        '    lblTableNo.Text = "0"
        'End If

        'If XAxisValues = "SurveyCount" Then
        '    rbSurveyCount.Checked = True
        'Else
        '    rbSurveyProb.Checked = True
        'End If


        RestoreFormSettings()   'Restore the form settings
        txtWidth.Text = Me.Width
        txtHeight.Text = Me.Height
        txtTop.Text = Me.Top
        txtLeft.Text = Me.Left
        'CheckFormPos()

        ' UpdateAnnotation()

        If XAxisValues = "SurveyCount" Then
            rbSurveyCount.Checked = True
            ShowSeriesStats() 'Redisplay the series stats with the formats read during RestoreFormSettings.
        Else
            rbSurveyProb.Checked = True
            ShowSurveyProbStats() 'Redisplay the series stats with the formats read during RestoreFormSettings.
        End If


        'Main.Message.Add("Chart.Width: " & Chart1.Width & vbCrLf)
        UpdateCharts()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Form

        If FormNo > -1 Then
            Main.ClosedFormNo = FormNo 'The Main form property ClosedFormNo is set to this form number. This is used in the DataInfoFormClosed method to select the correct form to set to nothing.
        End If

        Me.Close() 'Close the form
    End Sub

    Private Sub Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        Else
            'Dont save settings if the form is minimised.
        End If
    End Sub

    Private Sub frmSeriesAnalysis_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If FormNo > -1 Then
            Main.SeriesAnalysisClosed()
        End If

    End Sub

#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================


    Public Sub UpdateCharts()
        'Update the Chart displays
        GetHistogramSettings()
        CalcChartData()
        'GetChartXScale(200) 'Get the chart X Scale settings with about 200 pixels between X Axis labels NOTE: THIS CANNOT BE DONE HERE - NEED ChartArea(0) DEFINED BEFORE THE SCALE IS CALCULATED - THIS IS NOW EMBEDDED IN PlotCharts()
        If IsDiscrete Then
            PlotDiscreteCharts()
        Else
            PlotCharts()
        End If
        UpdateAnnotation()
    End Sub

    Private Sub ReplotCharts()
        'Replot the charts without regenerating the chart data:
        GetHistogramSettings()
        If IsDiscrete Then
            PlotDiscreteCharts()
        Else
            PlotCharts()
        End If
        UpdateAnnotation()
    End Sub



    Public Sub ShowSeriesStats()
        'Show the statistics of the series.

        'If DataSource.Data.Tables(SourceTableName) Is Nothing Then
        'If Main.BayesSim.Data.Tables(TableName) Is Nothing Then
        If Main.BayesSim.Data.Tables(SourceTableName) Is Nothing Then
            Main.Message.AddWarning("There is no table named " & SourceTableName & vbCrLf)
        Else
            If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(SourceColumnName) Then
                Dim RowNo As Integer
                Dim Var As Double
                MinSeriesVal = Main.BayesSim.Data.Tables(SourceTableName).Compute("Min(" & SourceColumnName & ")", "")
                MaxSeriesVal = Main.BayesSim.Data.Tables(SourceTableName).Compute("Max(" & SourceColumnName & ")", "")

                txtMinimum.Text = Format(MinSeriesVal, txtMinFormat.Text)
                txtMaximum.Text = Format(MaxSeriesVal, txtMaxFormat.Text)
                txtSum.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", ""), txtSumFormat.Text)
                'txtAverage.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Avg(" & SourceColumnName & ")", ""), txtAvgFormat.Text)
                'txtAverage.Text = Format((Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NRows), txtAvgFormat.Text)
                SeriesAverage = Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NRows
                txtAverage.Text = Format(SeriesAverage, txtAvgFormat.Text)
                Var = Variance(IsSample)
                SeriesVariance = Var
                SeriesStdDev = Math.Sqrt(Var) 'Save the Standard Deviation in this proerty for later calculations.
                txtVariance.Text = Format(Var, txtVarFormat.Text)
                'txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevFormat.Text)
                txtStdDev.Text = Format(SeriesStdDev, txtStdDevFormat.Text)
            Else
                Main.Message.AddWarning("SourceTableName = " & SourceTableName & vbCrLf & "There is no column named " & SourceColumnName & vbCrLf)
            End If
        End If
    End Sub

    Private Sub ShowSurveyProbStats()
        'Show the survey probability statistics.

        If Main.BayesSim.Data.Tables(SourceTableName) Is Nothing Then
            Main.Message.AddWarning("There is no table named " & SourceTableName & vbCrLf)
        Else
            If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(SourceColumnName) Then
                Dim RowNo As Integer
                Dim Var As Double
                MinSeriesVal = Main.BayesSim.Data.Tables(SourceTableName).Compute("Min(" & SourceColumnName & ")", "") / NTrials
                MaxSeriesVal = Main.BayesSim.Data.Tables(SourceTableName).Compute("Max(" & SourceColumnName & ")", "") / NTrials
                'txtMinimum.Text = Format(MinSeriesVal, txtMinFormat.Text)
                'txtMaximum.Text = Format(MaxSeriesVal, txtMaxFormat.Text)
                'txtSum.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NTrials, txtSumFormat.Text)
                'txtAverage.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Avg(" & SourceColumnName & ")", "") / NTrials, txtAvgFormat.Text)
                'txtAverage.Text = Format((Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NRows) / NTrials, txtAvgFormat.Text)
                SeriesAverage = (Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NRows) / NTrials
                Var = ProbVariance(IsSample)
                SeriesVariance = Var
                SeriesStdDev = Math.Sqrt(Var) 'Save the Standard Deviation in this proerty for later calculations.
                If Main.BayesSim.Settings.ProbabilityMeasure = "Percent" Then
                    txtMinimum.Text = Format(MinSeriesVal * 100, txtMinPctFormat.Text) & "%"
                    txtMaximum.Text = Format(MaxSeriesVal * 100, txtMaxPctFormat.Text) & "%"
                    txtSum.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") * 100 / NTrials, txtSumPctFormat.Text) & "%"
                    'txtAverage.Text = Format((Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NRows) * 100 / NTrials, txtAvgPctFormat.Text) & "%"
                    txtAverage.Text = Format(SeriesAverage * 100, txtAvgPctFormat.Text) & "%"
                    txtVariance.Text = Format(Var * 100, txtVarPctFormat.Text) & "%"
                    'txtStdDev.Text = Format(Math.Sqrt(Var) * 100, txtStdDevPctFormat.Text) & "%"
                    txtStdDev.Text = Format(SeriesStdDev * 100, txtStdDevPctFormat.Text) & "%"
                Else
                    txtMinimum.Text = Format(MinSeriesVal, txtMinDecFormat.Text)
                    txtMaximum.Text = Format(MaxSeriesVal, txtMaxDecFormat.Text)
                    txtSum.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NTrials, txtSumDecFormat.Text)
                    'txtAverage.Text = Format((Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NRows) / NTrials, txtAvgDecFormat.Text)
                    txtAverage.Text = Format(SeriesAverage, txtAvgDecFormat.Text)
                    txtVariance.Text = Format(Var, txtVarDecFormat.Text)
                    'txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevDecFormat.Text) & "%"
                    txtStdDev.Text = Format(SeriesStdDev, txtStdDevDecFormat.Text)
                End If

            Else
                Main.Message.AddWarning("SourceTableName = " & SourceTableName & vbCrLf & "There is no column named " & SourceColumnName & vbCrLf)
            End If
        End If

    End Sub

    Private Function Variance(ByVal IsSample As Boolean) As Double
        'Calculates the variance of the data in column ColumnName.
        'If IsSample is True, the Sample variance is calculated, else the Population variance is calculated.


        'Dim Mean As Double = Main.BayesSim.Data.Tables(SourceTableName).Compute("Avg(" & SourceColumnName & ")", "")
        Dim Mean As Double = Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NRows
        Dim DiffSq As Double = 0
        For Each Row As DataRow In Main.BayesSim.Data.Tables(SourceTableName).Rows
            DiffSq += (Row.Item(SourceColumnName) - Mean) ^ 2
        Next
        If IsSample Then
            Return DiffSq / (NRows - 1)
        Else
            Return DiffSq / NRows
        End If
    End Function

    Private Function ProbVariance(ByVal IsSample As Boolean) As Double
        'Calculates the variance of the data in table Histogram and column Survey_Prob.
        'If IsSample is True, the Sample variance is calculated, else the Population variance is calculated.

        'Dim NTrials As Integer = Main.BayesSim.Settings.EventSurveySize 'TO FIX - USE SURVEY SIZE PROPERTY (BayesSurveySize may be required!!!)
        'Dim Mean As Double = Main.BayesSim.Data.Tables(SourceTableName).Compute("Avg(" & SourceColumnName & ")", "") / NRows
        'Dim Mean As Double = Main.BayesSim.Data.Tables(SourceTableName).Compute("Avg(" & SourceColumnName & ")", "") / NTrials
        Dim Mean As Double = (Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", "") / NRows) / NTrials
        Dim DiffSq As Double = 0
        For Each Row As DataRow In Main.BayesSim.Data.Tables(SourceTableName).Rows
            'DiffSq += (Row.Item(SourceColumnName) / NRows - Mean) ^ 2
            DiffSq += (Row.Item(SourceColumnName) / NTrials - Mean) ^ 2
        Next
        If IsSample Then
            Return DiffSq / (NRows - 1)
        Else
            Return DiffSq / NRows
        End If



        'Dim Mean As Double = Data.Tables("Histogram").Compute("Avg(" & "Survey_Prob" & ")", "")
        'Dim DiffSq As Double = 0
        'For Each Row As DataRow In Data.Tables("Histogram").Rows
        '    DiffSq += (Row.Item("Survey_Prob") - Mean) ^ 2
        'Next
        'If IsSample Then
        '    'Return DiffSq / (NRows - 1)
        '    Return DiffSq / (Data.Tables("Histogram").Rows.Count - 1)
        'Else
        '    'Return DiffSq / NRows
        '    Return DiffSq / Data.Tables("Histogram").Rows.Count
        'End If

    End Function


    Private Sub txtMinFormat_TextChanged(sender As Object, e As EventArgs) Handles txtMinFormat.TextChanged

    End Sub

    Private Sub txtMinFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMinFormat.LostFocus
        If Main.BayesSim.Data.Tables(SourceTableName) Is Nothing Then
        Else
            If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(SourceColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                txtMinimum.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Min(" & SourceColumnName & ")", ""), txtMinFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub btnIsSample_Click(sender As Object, e As EventArgs) Handles btnIsSample.Click
        'Show the difference between the population and a sample of the population.
        MessageBox.Show("Is the series a sample of the population or the whole population?" & vbCrLf & "The standard devation and variance values are calculated differently for each case.", "Sample or Population")
    End Sub

    Private Sub cmbColumnName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSourceColumnName.SelectedIndexChanged
        If cmbSourceColumnName.Focused Then
            SourceColumnName = cmbSourceColumnName.SelectedItem.ToString
        End If
    End Sub

    Private Sub rbSample_CheckedChanged(sender As Object, e As EventArgs) Handles rbSample.CheckedChanged
        If rbSample.Focused Then
            If rbSample.Checked Then
                IsSample = True
            End If
        End If
    End Sub

    Private Sub rbPopulation_CheckedChanged(sender As Object, e As EventArgs) Handles rbPopulation.CheckedChanged
        If rbPopulation.Focused Then
            If rbPopulation.Checked Then
                IsSample = False
            End If
        End If
    End Sub

    Private Sub txtMaxFormat_TextChanged(sender As Object, e As EventArgs) Handles txtMaxFormat.TextChanged

    End Sub

    Private Sub txtMaxFormat_LostFocus(sender As Object, e As EventArgs) Handles txtMaxFormat.LostFocus
        If Main.BayesSim.Data.Tables(SourceTableName) Is Nothing Then
        Else
            If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(SourceColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                txtMaximum.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Max(" & SourceColumnName & ")", ""), txtMaxFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub txtSumFormat_TextChanged(sender As Object, e As EventArgs) Handles txtSumFormat.TextChanged

    End Sub

    Private Sub txtSumFormat_LostFocus(sender As Object, e As EventArgs) Handles txtSumFormat.LostFocus
        If Main.BayesSim.Data.Tables(SourceTableName) Is Nothing Then
        Else
            If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(SourceColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                txtSum.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Sum(" & SourceColumnName & ")", ""), txtSumFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub txtAvgFormat_TextChanged(sender As Object, e As EventArgs) Handles txtAvgFormat.TextChanged

    End Sub

    Private Sub txtAvgFormat_LostFocus(sender As Object, e As EventArgs) Handles txtAvgFormat.LostFocus
        If Main.BayesSim.Data.Tables(SourceTableName) Is Nothing Then
        Else
            If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(SourceColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                txtAverage.Text = Format(Main.BayesSim.Data.Tables(SourceTableName).Compute("Avg(" & SourceColumnName & ")", ""), txtAvgFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub txtStdDevFormat_TextChanged(sender As Object, e As EventArgs) Handles txtStdDevFormat.TextChanged

    End Sub

    Private Sub txtStdDevFormat_LostFocus(sender As Object, e As EventArgs) Handles txtStdDevFormat.LostFocus
        If Main.BayesSim.Data.Tables(SourceTableName) Is Nothing Then
        Else
            If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(SourceColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                'txtAverage.Text = Format(DataSource.Data.Tables(TableName).Compute("Avg(" & ColumnName & ")", ""), txtAvgFormat.Text)
                Dim Var As Double = Variance(IsSample)
                'txtVariance.Text = Format(Var, txtVarFormat.Text)
                txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub txtVarFormat_TextChanged(sender As Object, e As EventArgs) Handles txtVarFormat.TextChanged

    End Sub

    Private Sub txtVarFormat_LostFocus(sender As Object, e As EventArgs) Handles txtVarFormat.LostFocus
        If Main.BayesSim.Data.Tables(SourceTableName) Is Nothing Then
        Else
            If Main.BayesSim.Data.Tables(SourceTableName).Columns.Contains(SourceColumnName) Then
                'Dim RowNo As Integer
                'Dim Var As Double
                'txtAverage.Text = Format(DataSource.Data.Tables(TableName).Compute("Avg(" & ColumnName & ")", ""), txtAvgFormat.Text)
                Dim Var As Double = Variance(IsSample)
                txtVariance.Text = Format(Var, txtVarFormat.Text)
                'txtStdDev.Text = Format(Math.Sqrt(Var), txtStdDevFormat.Text)
            Else
            End If
        End If
    End Sub

    Private Sub CalcChartData()
        'Calculate the chart data.
        Debug.WriteLine("CalcChartData()")
        'Series             Table of calculated data used to plot the CDF
        '    Value          The value of a random variable
        '    Probability    The probability of that value
        '    Model_Prob     The distribution model probability

        'Histogram
        '    Mid_Interval
        '    Count
        '    Probability
        '    Prob_Density
        '    Model_Prob
        '    Model_Prob_Dens

        'UPDATE:
        'Series                 Table of calculated data used to plot the CDF
        '    Value              The value of a random variable
        '    CDF                The CDF probability of that value               (for continuous distributions)
        '    Reverse_CDF        The Reverse CDF probability of that value       (for continuous distributions)
        '    Model_CDF          The model CDF probability of that value         (for continuous distributions)
        '    Model_Rev_CDF      The model Reverse CDF probability of that value (for continuous distributions)
        '    Model_Prob_Dens    The model Probability Density at that value     (for continuous distributions)

        'UPDATE:
        'Histogram
        '    Mid_Interval
        '    Count
        '    Probability        The probability of a value in that interval     (for continuous distributions)
        '    Prob_Mass          The probability mass at that value              (for discrete distributions)
        '    Prob_Density       The probability density in that interval        (for continuous distributions)
        '    Model_CDF          The model CDF probability of that value         (for discrete distributions)
        '    Model_Rev_CDF      The model Reverse CDF probability of that value (for discrete distributions)
        '    Model_Prob_Mass    The model probability mass at that value        (for discrete distributions)

        If Main.BayesSim.Data.Tables.Contains(SourceTableName) Then
            'OK the required table exists.
        Else
            Main.Message.AddWarning("The simulation table named " & SourceTableName & " does not exist." & vbCrLf)
            Exit Sub
        End If

        Data.Clear()
        Data.Reset()
        cmbTableName.Items.Clear()

        'Generate the data used to plot the Cumulative Distribution Function: -----------------------------------------------------------------------------
        Data.Tables.Add("Series") 'Create the Series table.
        cmbTableName.Items.Add("Series") 'Add the table to the selection list

        Data.Tables("Series").Columns.Add("Value", Main.BayesSim.Data.Tables(SourceTableName).Columns(SourceColumnName).DataType) 'Create the Value column
        'Copy the data from the DataSource to the Series table:
        For Each Row As DataRow In Main.BayesSim.Data.Tables(SourceTableName).Rows
            Data.Tables("Series").Rows.Add(Row.Item(SourceColumnName))
        Next

        Data.Tables("Series").DefaultView.Sort = "Value ASC"     'Sort the data in ascending order

        Dim NValues As Integer = Data.Tables("Series").Rows.Count 'NValues is the number of data values in the series.

        'Data.Tables("Series").Columns.Add("Probability", System.Type.GetType("System.Double"))    'Create the Probability column
        If IsDiscrete Then
            'The calculated values for a discrete distribution are in the Histogram table.
        Else
            Data.Tables("Series").Columns.Add("CDF", System.Type.GetType("System.Double"))    'Create the Cumulative Distribution Function Probability column (for continuous distributions)
            Data.Tables("Series").Columns.Add("Reverse_CDF", System.Type.GetType("System.Double"))    'Create the Reverse Cumulative Distribution Function Probability column (for continuous distributions)

            'NOTE: Model data is generated later - this uses a lengthy Select Case statement and is in a separate loop
            'If chkShowModel.Checked Then
            '    Data.Tables("Series").Columns.Add("Model_CDF", System.Type.GetType("System.Double"))    'Create the Model Cumulative Distribution Function Probability column (for continuous distributions)
            '    Data.Tables("Series").Columns.Add("Model_Rev_CDF", System.Type.GetType("System.Double"))    'Create the Model Reverse Cumulative Distribution Function Probability column (for continuous distributions)
            '    Data.Tables("Series").Columns.Add("Model_Prob_Dens", System.Type.GetType("System.Double"))    'Create the Model Probability Density Function column (for continuous distributions)
            '    For I = 1 To NValues
            '        Data.Tables("Series").DefaultView.Item(I - 1).Item("CDF") = I / NValues 'Calculate each CDF value (for continuous distributions)
            '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Reverse_CDF") = 1 - (I / NValues) 'Calculate each Reverse_CDF value (for continuous distributions)

            '    Next
            'Else

            'NOTE: The Value column is sorted in ascending order. The CDF column ranges from 1/NValues to 1 in uniform increments.
            '      So a cross-plot of Value on the X axis and CDF on the Y axis displays the Cumulative Density Function.
            For I = 1 To NValues
                Data.Tables("Series").DefaultView.Item(I - 1).Item("CDF") = I / NValues 'Calculate each CDF value (for continuous distributions)
                Data.Tables("Series").DefaultView.Item(I - 1).Item("Reverse_CDF") = 1 - (I / NValues) 'Calculate each Reverse_CDF value (for continuous distributions)
            Next
            'End If
        End If

        ''Dim NIntervals As Integer = Data.Tables("Series").Rows.Count - 1
        'Dim NValues As Integer = Data.Tables("Series").Rows.Count

        'If ShowReverseCDF Then
        '    'For I = 0 To NIntervals
        '    For I = 1 To NValues
        '        'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = 1 - (I / NIntervals)
        '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Probability") = 1 - (I / NValues)
        '    Next
        'Else
        '    'For I = 0 To NIntervals
        '    For I = 1 To NValues
        '        'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
        '        Data.Tables("Series").DefaultView.Item(I - 1).Item("Probability") = I / NValues
        '    Next
        'End If

        TableName = "Series" 'This updates the cmbTableName selection and updates the data displayed in the Data tab.
        'CalcTableName = "Series" 'This updates the cmbTableName selection and updates the data displayed in the Data tab.
        '==================================================================================================================================================


        'Generate the Histogram data ----------------------------------------------------------------------------------------------------------------------

        Data.Tables.Add("Histogram") 'Create the Histogram table
        cmbTableName.Items.Add("Histogram") 'Add the table to the selection list

        'Data.Tables("Histogram").Columns.Add("Mid_Interval", System.Type.GetType("System.Double")) 'Create the Mid Interval column
        Data.Tables("Histogram").Columns.Add("Value", System.Type.GetType("System.Double"))        'Create the Value column
        'Data.Tables("Histogram").Columns.Add("Mid_Interval", System.Type.GetType("System.Double"))        'Use Mid_Interval for the column name so the functions used to calculate values from continuous data histograms can still be used for discrete data.
        Data.Tables("Histogram").Columns.Add("Survey_Prob", System.Type.GetType("System.Double"))        'Create the Survey Probability column - this is the probability corresponding to the Survey Count in the Value column.
        Data.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))         'Create the Count column
        Data.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))  'Create the probability column

        '3Sep21 - Adding cumulative counts and probabilities:
        Data.Tables("Histogram").Columns.Add("Cum_Count", System.Type.GetType("System.Int32"))         'Create the Cumulative Count column
        Data.Tables("Histogram").Columns.Add("Cum_Probability", System.Type.GetType("System.Double"))  'Create the Cumulative probability column
        Data.Tables("Histogram").Columns.Add("Reverse_Cum_Prob", System.Type.GetType("System.Double"))  'Create the Reverse Cumulative probability column

        'If IsDiscrete Then Else Data.Tables("Histogram").Columns.Add("Prob_Density", System.Type.GetType("System.Double"))  'Create the probability density column (Only for a continuous distribution)

        If IsDiscrete Then 'This is a discrete distribution
            Dim DistVal As Integer = MinSeriesVal 'The current distribution value
            Dim PointCount As Integer = 0 'The count of points having the current distribution value
            'Dim TotalPoints As Integer = Data.Tables("Series").Compute("Sum(" & "Value" & ")", "")
            Dim CumPointCount As Integer = 0 'The cumulative count of points.
            'Dim NTrials As Integer = Main.BayesSim.Settings.EventSurveySize 'TO FIX - USE SURVEY SIZE PROPERTY (BayesSurveySize may be required!!!)
            For Each Row As DataRowView In Data.Tables("Series").DefaultView 'This uses the sorted row view
                If Row.Item("Value") = DistVal Then
                    PointCount += 1
                    CumPointCount += 1
                Else
                    'Data.Tables("Histogram").Rows.Add(DistVal, PointCount, PointCount / NRows) 'Write the Mid_Interval, Count,  and Probability to the table. (Discrete distributions do not have Prob_Density values.)
                    'Data.Tables("Histogram").Rows.Add(DistVal, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (Discrete distributions do not have Prob_Density values.)
                    'Data.Tables("Histogram").Rows.Add(DistVal, DistVal / NRows, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (Discrete distributions do not have Prob_Density values.)
                    'Data.Tables("Histogram").Rows.Add(DistVal, DistVal / NTrials, PointCount, PointCount / NTrials, CumPointCount, CumPointCount / NTrials, 1 - (CumPointCount / NTrials)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (Discrete distributions do not have Prob_Density values.)
                    Data.Tables("Histogram").Rows.Add(DistVal, DistVal / NTrials, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (Discrete distributions do not have Prob_Density values.)
                    PointCount = 0 'Reset the point count
                    DistVal += 1 'Increment the distribution value
                    'While Row.Item("Value").Value <> DistVal
                    While Row.Item("Value") <> DistVal
                        'Data.Tables("Histogram").Rows.Add(DistVal, 0, 0) 'Write the Mid_Interval, Count,  and Probability to the table. (The count and probability are zero.)
                        'Data.Tables("Histogram").Rows.Add(DistVal, 0, 0, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (The count and probability are zero.)
                        'Data.Tables("Histogram").Rows.Add(DistVal, DistVal / NRows, 0, 0, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (The count and probability are zero.)
                        'Data.Tables("Histogram").Rows.Add(DistVal, DistVal / NTrials, 0, 0, CumPointCount, CumPointCount / NTrials, 1 - (CumPointCount / NTrials)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (The count and probability are zero.)
                        Data.Tables("Histogram").Rows.Add(DistVal, DistVal / NTrials, 0, 0, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the Value, Count, Probability, Cumulative Count, Cumulative Probability and Reverse Cumulative Probability to the table. (The count and probability are zero.)
                        DistVal += 1 'Increment the distribution value
                    End While
                    PointCount += 1
                    CumPointCount += 1
                End If
            Next
            'If PointCount > 0 Then Data.Tables("Histogram").Rows.Add(DistVal, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the last PointCount.
            'If PointCount > 0 Then Data.Tables("Histogram").Rows.Add(DistVal, DistVal / NRows, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the last PointCount.
            'If PointCount > 0 Then Data.Tables("Histogram").Rows.Add(DistVal, DistVal / NTrials, PointCount, PointCount / NTrials, CumPointCount, CumPointCount / NTrials, 1 - (CumPointCount / NTrials)) 'Write the last PointCount.
            If PointCount > 0 Then Data.Tables("Histogram").Rows.Add(DistVal, DistVal / NTrials, PointCount, PointCount / NRows, CumPointCount, CumPointCount / NRows, 1 - (CumPointCount / NRows)) 'Write the last PointCount.
        Else 'This is a continuous distribution
            Data.Tables("Histogram").Columns.Add("Prob_Density", System.Type.GetType("System.Double"))  'Create the probability density column (Only for a continuous distribution)
            Dim StartGap As Double = HistMin - MinSeriesVal 'The gap between the first data value in the series and the midpoint of the first column in the histogram.
            Dim EndInterval As Double = HistMin + HistIntervalWidth / 2 'The maximum value of the current histogram interval
            Dim PointCount As Integer = 0 'The count of points within the current histogram interval
            For Each Row As DataRowView In Data.Tables("Series").DefaultView 'This uses the sorted row view
                If Row.Item("Value") < EndInterval Then
                    PointCount += 1
                Else
                    Data.Tables("Histogram").Rows.Add(EndInterval - HistIntervalWidth / 2, PointCount, PointCount / NRows, PointCount / NRows / HistIntervalWidth) 'Write the Mid_Interval, Count, Probability and Prob_Density to the table.
                    PointCount = 0
                    EndInterval += HistIntervalWidth
                End If
            Next
        End If

        Dim NHistRows As Integer = Data.Tables("Histogram").Rows.Count 'NHistRows is the number of Histogram rows.

        '==================================================================================================================================================

        If chkShowModel.Checked Then
            '    'Generate the data used to plot the Model CDF and PDF (or PMF): -------------------------------------------------------------------

            If IsDiscrete Then
                Data.Tables("Histogram").Columns.Add("Model_CDF", System.Type.GetType("System.Double"))       'Create the Model CDF column              (for discrete distributions)
                Data.Tables("Histogram").Columns.Add("Model_Rev_CDF", System.Type.GetType("System.Double"))   'Create the Model Reverse CDF column      (for discrete distributions)
                Data.Tables("Histogram").Columns.Add("Model_Prob_Mass", System.Type.GetType("System.Double")) 'Create the Model Probability Mass column (for discrete distributions)

                ''For this application, we use the Binomial distribution formula to calculate the probability mass function values.
                'Dim N As Integer = NTrials 'The number of trials. (Variable N is used instead of NTrials - shorter formula and standard notation.)
                'Dim K As Integer 'The number of successes
                'Dim P As Double = PSuccess 'The probability of success in each trial. (Variable P is used instead of PSuccess - shorter formula and standard notation.)
                ''Binomial Formula: P(K successes in Ntrials) = N!/(K!(N-K)!)P^K(1-P)^(N-K)
                'Dim Binomial(0 To NTrials) As Double
                'Try
                '    Dim NFact As Integer = 1

                '    For I = 1 To NTrials
                '        'Binomial(I) = Math.
                '    Next
                'Catch ex As Exception
                '    Main.Message.AddWarning("Error calculating Binomial probabilities: " & vbCrLf & ex.Message & vbCrLf)
                'End Try

                'For Each Row As DataRowView In Data.Tables("Histogram").DefaultView

                'Next


            Else
                Data.Tables("Series").Columns.Add("Model_CDF", System.Type.GetType("System.Double"))       'Create the Model CDF column                 (for continuous distributions)
                Data.Tables("Series").Columns.Add("Model_Rev_CDF", System.Type.GetType("System.Double"))   'Create the Model Reverse CDF column         (for continuous distributions)
                Data.Tables("Series").Columns.Add("Model_Prob_Dens", System.Type.GetType("System.Double")) 'Create the Model Probability Density column (for continuous distributions)
            End If

            '    Select Case DistributionName
            '        Case "C2 - Beta"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Beta.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Beta.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C4 - Beta Scaled"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.BetaScaled.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.BetaScaled.PDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C2 - Cauchy"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Cauchy.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Cauchy.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next
            '        Case "C1 - Chi Squared"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.ChiSquared.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.ChiSquared.PDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next
            '        Case "C2 - Continuous Uniform"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.ContinuousUniform.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.ContinuousUniform.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C1 - Exponential"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Exponential.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Exponential.PDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C2 - Fisher-Snedecor"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.FisherSnedecor.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.FisherSnedecor.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C2 - Gamma"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Gamma.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Gamma.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C2 - Inverse Gaussian"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.InverseGaussian.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.InverseGaussian.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C2 - Log Normal"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.LogNormal.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.LogNormal.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C2 - Normal"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Normal.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Normal.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C2 - Pareto"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Pareto.CDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Pareto.PDF(ParamAValue, ParamBValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C1 - Rayleigh"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Rayleigh.CDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Rayleigh.PDF(ParamAValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C4 - Skewed Generalized Error"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.SkewedGeneralizedError.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.SkewedGeneralizedError.PDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C5 - Skewed Generalized T"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.SkewedGeneralizedT.CDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, ParamEValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.SkewedGeneralizedT.PDF(ParamAValue, ParamBValue, ParamCValue, ParamDValue, ParamEValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C3 - Student's T"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.StudentT.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.StudentT.PDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C3 - Triangular"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Triangular.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.Triangular.PDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "C3 - Truncated Pareto"
            '            For I = 1 To NValues
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.TruncatedPareto.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_CDF")
            '                Data.Tables("Series").DefaultView.Item(I - 1).Item("Model_Prob_Dens") = MathNet.Numerics.Distributions.TruncatedPareto.PDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Series").DefaultView.Item(I - 1).Item("Value"))
            '            Next

            '        Case "D1 - Bernoulli"
            '            For I = 1 To NHistRows
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Bernoulli.CDF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Bernoulli.PMF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '            Next
            '        Case "D2 - Binomial"
            '            For I = 1 To NHistRows
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Binomial.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Binomial.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '            Next

            '        Case "D1 - Categorical"

            '        Case "D2 - Conway-Maxwell-Poisson"
            '            For I = 1 To NHistRows
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.ConwayMaxwellPoisson.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.ConwayMaxwellPoisson.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '            Next

            '        Case "D2 - Discrete Uniform"
            '            For I = 1 To NHistRows
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.DiscreteUniform.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.DiscreteUniform.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '            Next

            '        Case "D1 - Geometric"
            '            For I = 1 To NHistRows
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Geometric.CDF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Geometric.PMF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '            Next

            '        Case "D3 - Hypergeometric"
            '            For I = 1 To NHistRows
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Hypergeometric.CDF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Hypergeometric.PMF(ParamAValue, ParamBValue, ParamCValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '            Next

            '        Case "D2 - Negative Binomial"
            '            For I = 1 To NHistRows
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.NegativeBinomial.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.NegativeBinomial.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '            Next

            '        Case "D1 Poisson"
            '            For I = 1 To NHistRows
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Poisson.CDF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Poisson.PMF(ParamAValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '            Next

            '        Case "D2 - Zipf"
            '            For I = 1 To NHistRows
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF") = MathNet.Numerics.Distributions.Zipf.CDF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Rev_CDF") = 1 - Data.Tables("Histogram").Rows(I - 1).Item("Model_CDF")
            '                Data.Tables("Histogram").Rows(I - 1).Item("Model_Prob_Mass") = MathNet.Numerics.Distributions.Zipf.PMF(ParamAValue, ParamBValue, Data.Tables("Histogram").Rows(I - 1).Item("Mid_Interval"))
            '            Next

            '        Case ""
            '            'No model

            '        Case Else
            '            Main.Message.AddWarning("Unknown model distribution: " & DistributionName & vbCrLf)

            '    End Select

            '    '==============================================================================================================================================
        End If


    End Sub

    'OLD CODE: [Replaced by CalcChartData()]
    Private Sub SetUpSeriesTable()
        'Set up the Series table used to plot the CDF.

        'The Series table will contain the columns:
        'Data - the data points copied from the data source then sorted (ascending)
        'Probability - the cumulative probability of each data point - smallest point has 0 probability, the largest point has 1 probability.

        Data.Clear()
        Data.Reset()
        cmbTableName.Items.Clear()

        Data.Tables.Add("Series") 'Create the Series table.
        cmbTableName.Items.Add("Series")

        Data.Tables("Series").Columns.Add("Value", Main.BayesSim.Data.Tables(SourceTableName).Columns(SourceColumnName).DataType) 'Create the Value column
        'Copy the data from the DataSource to the Series table:

        For Each Row As DataRow In Main.BayesSim.Data.Tables(SourceTableName).Rows
            Data.Tables("Series").Rows.Add(Row.Item(SourceColumnName))
            'I += 1
        Next

        Data.Tables("Series").DefaultView.Sort = "Value ASC"     'Sort the data in ascending order

        'Create the Probability column:
        Data.Tables("Series").Columns.Add("Probability", System.Type.GetType("System.Double"))

        'Dim NIntervals As Integer = Data.Tables("Series").Rows.Count - 1
        Dim NValues As Integer = Data.Tables("Series").Rows.Count

        'For I = 0 To NIntervals
        '    Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
        'Next

        'UPDATE to allow for Reverse CDF:
        If ShowReverseCDF Then
            'For I = 0 To NIntervals
            For I = 1 To NValues
                'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = 1 - (I / NIntervals)
                Data.Tables("Series").DefaultView.Item(I - 1).Item("Probability") = 1 - (I - 1 / NValues)
            Next
        Else
            'For I = 0 To NIntervals
            For I = 1 To NValues
                'Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
                Data.Tables("Series").DefaultView.Item(I - 1).Item("Probability") = I - 1 / NValues
            Next
        End If

        TableName = "Series" 'This updates the cmbTableName selection and updates the data displayed in the Data tab.
        'CalcTableName = "Series" 'This updates the cmbTableName selection and updates the data displayed in the Data tab.

    End Sub

    Private Sub RecalcSeriesTable()
        'Recalculate the Series table used to plot the CDF.

        Dim NIntervals As Integer = Data.Tables("Series").Rows.Count - 1

        'For I = 0 To NIntervals
        '    Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
        'Next

        'UPDATE to allow for Reverse CDF:
        If ShowReverseCDF Then
            For I = 0 To NIntervals
                Data.Tables("Series").DefaultView.Item(I).Item("Probability") = 1 - (I / NIntervals)
            Next
        Else
            For I = 0 To NIntervals
                Data.Tables("Series").DefaultView.Item(I).Item("Probability") = I / NIntervals
            Next
        End If

    End Sub


    Private Sub PlotCharts()
        'Plot the CDF and PDF charts

        Try
            'Plot the CDF of the Series Data:

            Chart1.Legends(0).Docking = DataVisualization.Charting.Docking.Bottom

            Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(0).Color = Color.Red
            Chart1.Series(0).BorderWidth = 3

            'Dim DataInfoIndex As Integer = Main.MonteCarlo.DataInfoNameIndex(SourceColumnName)
            'If DataInfoIndex = -1 Then 'This is a calculated value so there is no information about this in MonteCarlo.DataInfo

            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas(0).AxisX.Title = SourceColumnName
            '    Else
            '        If Main.CalcInfo.ContainsKey(ScalarName) Then
            '            Chart1.ChartAreas(0).AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")" 'Show the CalcInfo abbreviated units if available
            '        Else
            '            Chart1.ChartAreas(0).AxisX.Title = ScalarName
            '        End If
            '    End If

            'Else
            '    'Chart1.ChartAreas(0).AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"

            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas(0).AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '    Else
            '        If Main.CalcInfo.ContainsKey(ScalarName) Then
            '            If Main.CalcInfo(ScalarName).UnitsAbbrev.Trim = "" Then
            '                Chart1.ChartAreas(0).AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '            Else
            '                Chart1.ChartAreas(0).AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")"
            '            End If

            '        Else
            '            Chart1.ChartAreas(0).AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '        End If
            '    End If
            'End If

            'ScalarName is the name of the data in the series being analysed (as used in the Monte Carlo version of this form).
            'ColumnName will be used for the ScalarName in this application.
            Chart1.ChartAreas(0).AxisX.Title = SourceColumnName

            Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            'Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"

            Chart1.ChartAreas(0).AxisY.Title = "Cumulative" & vbCrLf & "Probability"
            Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

            Chart1.ChartAreas(0).AxisY.Minimum = 0
            Chart1.ChartAreas(0).AxisY.Maximum = 1

            'Calculate the XAxis Scale settings:
            Dim XAxisLength As Single = Chart1.ChartAreas(0).InnerPlotPosition.Width 'X Axis length in relative coordinates, which range from 0 to 100.
            If XAxisLength = 0 Then XAxisLength = 100 'When the form is first loaded, XAxisLength is 0!
            Dim ChartPixelWidth As Integer = Chart1.Width 'The width of the entire chart image in pixels.
            Dim XAxisPixelLength As Integer = Int(ChartPixelWidth * XAxisLength / 100)
            Dim XRawNIntervals As Integer = Int(XAxisPixelLength / RawPixelsPerInterval) 'The Raw number of axis annotation intervals based on the Raw pixels per annotation.

            Dim XRawInterval As Double = (MaxSeriesVal - MinSeriesVal) / XRawNIntervals 'First calculate the Raw Interval
            ChartXInterval = PreferredInterval(XRawInterval) 'The preferred X Axis lavel interval has the significant digits: 1, 2, 2.5, 5, 10.
            ChartXMin = Math.Floor(MinSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Minimum for the Chart display
            ChartXMax = Math.Ceiling(MaxSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Maximum for the Chart display
            '------------------------------------------

            Chart1.ChartAreas(0).AxisX.Minimum = ChartXMin
            Chart1.ChartAreas(0).AxisX.Maximum = ChartXMax
            Chart1.ChartAreas(0).AxisX.Interval = ChartXInterval

            Chart1.Titles.Clear()
            Chart1.Titles.Add("Title1")
            Chart1.Titles(0).Text = "Random Variable: " & SourceColumnName
            Chart1.Titles(0).Font = New Font("Arial", 16, FontStyle.Regular Or FontStyle.Bold)

            If ShowReverseCDF Then
                Chart1.Series(0).Name = "Reverse Cumulative Distribution Function"
                Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Reverse_CDF")
                Chart1.Titles.Add("Title2")
                Chart1.Titles(1).Text = "Reverse Cumulative Distribution Function"
            Else
                Chart1.Series(0).Name = "Cumulative Distribution Function"
                Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "CDF")
                Chart1.Titles.Add("Title2")
                Chart1.Titles(1).Text = "Cumulative Distribution Function"
            End If

            Chart1.Titles(1).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
            Chart1.Titles(1).DockedToChartArea = Chart1.ChartAreas(0).Name
            Chart1.Titles(1).IsDockedInsideChartArea = False
            Chart1.Titles(1).Docking = DataVisualization.Charting.Docking.Top

        Catch ex As Exception
            Main.Message.AddWarning("Error plotting the Series Data CDF: " & vbCrLf & ex.Message & vbCrLf)
        End Try

        Try
            'Plot the PDF Histogram (Continuous distribution) or PMF (Discrete distribution):
            If Chart1.Series.Count > 1 Then
                If Chart1.Series(1).Name = "PDF Histogram" Then

                Else
                    Chart1.Series(1).Name = "PDF Histogram"
                End If
            Else
                Chart1.Series.Add("PDF Histogram")
                Chart1.ChartAreas.Add("HistArea")
            End If

            Chart1.Series("PDF Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("PDF Histogram").Color = Color.Blue
            Chart1.Series("PDF Histogram").ChartArea = "HistArea"

            Chart1.Titles.Add("Title3")


            If IsDiscrete Then
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Prob_Mass")
                Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Value", Data.Tables("Histogram").DefaultView, "Prob_Mass")
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Prob_Mass") 'Use Mid_Interval for the column name so the functions used to calculate values from continuous data histograms can still be used for discrete data.
                'Chart1.Titles(2).Text = "Probability Mass"
                Chart1.Titles(2).Text = "Probability Mass Function"
                Chart1.ChartAreas("HistArea").AxisY.Title = "Probability Mass"
            Else
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Prob_Density")
                Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Value", Data.Tables("Histogram").DefaultView, "Prob_Density") 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                Chart1.Titles(2).Text = "Probability Density Histogram"
                Chart1.ChartAreas("HistArea").AxisY.Title = "Probability Density"
            End If
            Chart1.Titles(2).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
            Chart1.Titles(2).DockedToChartArea = "HistArea"
            Chart1.Titles(2).IsDockedInsideChartArea = False
            Chart1.Titles(2).Docking = DataVisualization.Charting.Docking.Top

            Chart1.ChartAreas("HistArea").AxisX.Minimum = ChartXMin
            Chart1.ChartAreas("HistArea").AxisX.Maximum = ChartXMax
            Chart1.ChartAreas("HistArea").AxisX.Interval = ChartXInterval

            'Dim DataInfoIndex As Integer = Main.MonteCarlo.DataInfoNameIndex(SourceColumnName)
            'If DataInfoIndex = -1 Then  'This is a calculated value so there is no information about this in MonteCarlo.DataInfo
            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName
            '    ElseIf Main.CalcInfo.ContainsKey(ScalarName) Then
            '        Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")" 'Show the CalcInfo abbreviated units if available
            '    Else
            '        Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName
            '    End If

            'Else
            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '    Else
            '        If Main.CalcInfo.ContainsKey(ScalarName) Then
            '            If Main.CalcInfo(ScalarName).UnitsAbbrev.Trim = "" Then
            '                Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '            Else
            '                Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")"
            '            End If
            '        Else
            '            Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '        End If
            '    End If
            'End If
            'ScalarName is the name of the data in the series being analysed (as used in the Monte Carlo version of this form).
            'ColumnName will be used for the ScalarName in this application.
            Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName

            Chart1.ChartAreas("HistArea").AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            Chart1.ChartAreas("HistArea").AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

            Chart1.ChartAreas(0).AlignWithChartArea = "HistArea"
            Chart1.ChartAreas(0).AlignmentOrientation = DataVisualization.Charting.AreaAlignmentOrientations.Vertical
            Chart1.ChartAreas(0).AlignmentStyle = DataVisualization.Charting.AreaAlignmentStyles.All

            Chart1.ChartAreas(0).AxisX.RoundAxisValues()

        Catch ex As Exception
            Main.Message.AddWarning("Error plotting the Series Data PDF or PMF: " & vbCrLf & ex.Message & vbCrLf)
        End Try


        'Add a series used to plot vertical bars on the CDF chart:
        Dim IndexNo As Integer = Chart1.Series.IndexOf("CdfVertBar")
        If IndexNo = -1 Then 'Series named CdfVerBar does not exist
            Chart1.Series.Add("CdfVertBar")
            Chart1.Series("CdfVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("CdfVertBar").Color = Color.Orange
            Chart1.Series("CdfVertBar").ChartArea = Chart1.ChartAreas(0).Name
            Chart1.Series("CdfVertBar").SetCustomProperty("PixelPointWidth", "2")
            Chart1.Series("CdfVertBar").IsVisibleInLegend = False
        Else

        End If

        'Add a series used to plot circle markers on the histogram:
        IndexNo = Chart1.Series.IndexOf("HistPoints")
        If IndexNo = -1 Then  'Series named HistPoints does not exist
            Chart1.Series.Add("HistPoints")
            Chart1.Series("HistPoints").ChartType = DataVisualization.Charting.SeriesChartType.Point
            Chart1.Series("HistPoints").Color = Color.Transparent
            Chart1.Series("HistPoints").ChartArea = "HistArea"
            Chart1.Series("HistPoints").IsVisibleInLegend = False
            Chart1.Series("HistPoints").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            Chart1.Series("HistPoints").MarkerSize = 20
            Chart1.Series("HistPoints").MarkerBorderWidth = 2
            Chart1.Series("HistPoints").MarkerBorderColor = Color.Orange
        Else

        End If

        If chkShowModel.Checked Then 'Plot the CDF and PDF (or PMF) of the Model distribution
            If IsDiscrete Then 'Discrete distribution model

            Else 'Continuous distribution model

                If ShowReverseCDF Then
                    If Chart1.Series.Count > 4 Then
                        If Chart1.Series(4).Name = "Model Reverse CDF" Then
                        Else
                            Chart1.Series(4).Name = "Model Reverse CDF"
                        End If
                    Else
                        Chart1.Series.Add("Model Reverse CDF")
                    End If

                    Chart1.Series("Model Reverse CDF").ChartArea = Chart1.ChartAreas(0).Name
                    Chart1.Series("Model Reverse CDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("Model Reverse CDF").Color = Color.Black
                    Chart1.Series("Model Reverse CDF").BorderWidth = 1
                    Chart1.Series("Model Reverse CDF").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Dot

                    Chart1.Series("Model Reverse CDF").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Model_Rev_CDF")
                    'Chart1.Series("Model CDF").IsVisibleInLegend = False
                Else
                    'If Chart1.Series.Count > 2 Then
                    If Chart1.Series.Count > 4 Then
                        If Chart1.Series(4).Name = "Model CDF" Then
                        Else
                            Chart1.Series(4).Name = "Model CDF"
                        End If
                    Else
                        Chart1.Series.Add("Model CDF")
                    End If

                    Chart1.Series("Model CDF").ChartArea = Chart1.ChartAreas(0).Name

                    Chart1.Series("Model CDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("Model CDF").Color = Color.Black
                    Chart1.Series("Model CDF").BorderWidth = 1
                    Chart1.Series("Model CDF").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Dot

                    Chart1.Series("Model CDF").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Model_CDF")
                    'Chart1.Series("Model CDF").IsVisibleInLegend = False
                End If



                If Chart1.Series.Count > 5 Then
                    If Chart1.Series(5).Name = "Model PDF" Then
                    Else
                        Chart1.Series(5).Name = "Model PDF"
                    End If
                Else
                    Chart1.Series.Add("Model PDF")
                End If

                Chart1.Series("Model PDF").ChartArea = "HistArea"

                Chart1.Series("Model PDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
                Chart1.Series("Model PDF").Color = Color.Black
                Chart1.Series("Model PDF").BorderWidth = 2
                'Chart1.Series("Model PDF").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Dash


                Chart1.Series("Model PDF").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Model_Prob_Dens")
                'Chart1.Series("Model PDF").IsVisibleInLegend = False

            End If
        Else
            'Remove any Model series:
            Dim SeriesNo As Integer = Chart1.Series.IndexOf("Model CDF")
            If SeriesNo = -1 Then
                'The Model CDF series does not exist
            Else
                Chart1.Series.RemoveAt(SeriesNo)
            End If

            SeriesNo = Chart1.Series.IndexOf("Model Reverse CDF")
            If SeriesNo = -1 Then
                'The Model Reverse CDF series does not exist
            Else
                Chart1.Series.RemoveAt(SeriesNo)
            End If

            SeriesNo = Chart1.Series.IndexOf("Model PDF")
            If SeriesNo = -1 Then
                'The Model PDF series does not exist
            Else
                Chart1.Series.RemoveAt(SeriesNo)
            End If

        End If

        ApplyChartMinMax()

        Dim NTitles As Integer = Chart1.Titles.Count
        If NTitles > 0 Then
            NumericUpDown1.Minimum = 1
            NumericUpDown1.Maximum = NTitles
            NumericUpDown1.Increment = 1
            NumericUpDown1.Value = 1
            txtEditTitle.Text = Chart1.Titles(0).Text
            txtEditTitle.Font = Chart1.Titles(0).Font
            txtEditTitle.ForeColor = Chart1.Titles(0).ForeColor
        Else
            NumericUpDown1.Minimum = 0
            NumericUpDown1.Maximum = 0
            NumericUpDown1.Increment = 1
            NumericUpDown1.Value = 0
            txtEditTitle.Text = ""
        End If

    End Sub


    Private Sub PlotDiscreteCharts()
        'Plot the CDF and PDF charts
        'The Discrete series version of the charts.

        If Data.Tables.Contains("Histogram") Then

        Else
            Main.Message.AddWarning("The table named Histogram does not exist." & vbCrLf)
            Exit Sub
        End If

        Try
            'Plot the CDF of the Series Data:

            Chart1.Legends(0).Docking = DataVisualization.Charting.Docking.Bottom

            Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(0).Color = Color.Red
            Chart1.Series(0).BorderWidth = 3

            'ScalarName is the name of the data in the series being analysed (as used in the Monte Carlo version of this form).
            'ColumnName will be used for the ScalarName in this application.
            'Chart1.ChartAreas(0).AxisX.Title = SourceColumnName
            If XAxisValues = "SurveyCount" Then
                Chart1.ChartAreas(0).AxisX.Title = SourceColumnName & " (Survey Event Count)"
                Chart1.ChartAreas(0).AxisX.LabelStyle.Format = ""
            Else
                Chart1.ChartAreas(0).AxisX.Title = SourceColumnName & " (Survey Probability)"
                If Main.BayesSim.Settings.ProbabilityMeasure = "Percent" Then
                    Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "P1"
                Else
                    Chart1.ChartAreas(0).AxisX.LabelStyle.Format = ""
                End If
            End If

            Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            'Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"

            Chart1.ChartAreas(0).AxisY.Title = "Cumulative" & vbCrLf & "Probability"
            Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

            Chart1.ChartAreas(0).AxisY.Minimum = 0
            Chart1.ChartAreas(0).AxisY.Maximum = 1



            'Calculate the XAxis Scale settings:
            Dim XAxisLength As Single = Chart1.ChartAreas(0).InnerPlotPosition.Width 'X Axis length in relative coordinates, which range from 0 to 100.
            If XAxisLength = 0 Then XAxisLength = 100 'When the form is first loaded, XAxisLength is 0!
            Dim ChartPixelWidth As Integer = Chart1.Width 'The width of the entire chart image in pixels.
            Dim XAxisPixelLength As Integer = Int(ChartPixelWidth * XAxisLength / 100)
            Dim XRawNIntervals As Integer = Int(XAxisPixelLength / RawPixelsPerInterval) 'The Raw number of axis annotation intervals based on the Raw pixels per annotation.

            Dim XRawInterval As Double = (MaxSeriesVal - MinSeriesVal) / XRawNIntervals 'First calculate the Raw Interval
            ChartXInterval = PreferredInterval(XRawInterval) 'The preferred X Axis lavel interval has the significant digits: 1, 2, 2.5, 5, 10.
            ChartXMin = Math.Floor(MinSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Minimum for the Chart display
            ChartXMax = Math.Ceiling(MaxSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Maximum for the Chart display

            'NOTE: MinSeriesVal and MaxSeriesVal are now updated when ShowSeriesStats or ShowSurveyProbStats is run.
            'If XAxisValues = "SurveyCount" Then
            '    Dim XRawInterval As Double = (MaxSeriesVal - MinSeriesVal) / XRawNIntervals 'First calculate the Raw Interval
            '    ChartXInterval = PreferredInterval(XRawInterval) 'The preferred X Axis lavel interval has the significant digits: 1, 2, 2.5, 5, 10.
            '    ChartXMin = Math.Floor(MinSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Minimum for the Chart display
            '    ChartXMax = Math.Ceiling(MaxSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Maximum for the Chart display
            'Else 'Use probabilites calculated from the series values: The series values are survey counts so the survey count divided by the trials (NRows) is the probability. 
            '    Dim XRawInterval As Double = (MaxSeriesVal / NRows - MinSeriesVal / NRows) / XRawNIntervals 'First calculate the Raw Interval
            '    ChartXInterval = PreferredInterval(XRawInterval) 'The preferred X Axis lavel interval has the significant digits: 1, 2, 2.5, 5, 10.
            '    ChartXMin = Math.Floor((MinSeriesVal / NRows) / ChartXInterval) * ChartXInterval 'The preferred Axis Minimum for the Chart display
            '    ChartXMax = Math.Ceiling((MaxSeriesVal / NRows) / ChartXInterval) * ChartXInterval 'The preferred Axis Maximum for the Chart display
            'End If

            '------------------------------------------

            Chart1.ChartAreas(0).AxisX.Minimum = ChartXMin
            Chart1.ChartAreas(0).AxisX.Maximum = ChartXMax
            Chart1.ChartAreas(0).AxisX.Interval = ChartXInterval


            Chart1.Titles.Clear()
            Chart1.Titles.Add("Title1")
            Chart1.Titles(0).Text = "Random Variable: " & SourceColumnName
            Chart1.Titles(0).Font = New Font("Arial", 16, FontStyle.Regular Or FontStyle.Bold)

            If ShowReverseCDF Then
                Chart1.Series(0).Name = "Reverse Cumulative Distribution Function"
                'Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Reverse_CDF")
                If XAxisValues = "SurveyCount" Then
                    Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Value", Data.Tables("Histogram").DefaultView, "Reverse_Cum_Prob")
                Else
                    Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Survey_Prob", Data.Tables("Histogram").DefaultView, "Reverse_Cum_Prob")
                End If

                'Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Reverse_Cum_Prob")
                Chart1.Titles.Add("Title2")
                Chart1.Titles(1).Text = "Reverse Cumulative Distribution Function"
            Else
                Chart1.Series(0).Name = "Cumulative Distribution Function"
                'Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "CDF")
                If XAxisValues = "SurveyCount" Then
                    Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Value", Data.Tables("Histogram").DefaultView, "Cum_Probability")
                Else
                    Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Survey_Prob", Data.Tables("Histogram").DefaultView, "Cum_Probability")
                End If

                'Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Cum_Probability")
                Chart1.Titles.Add("Title2")
                Chart1.Titles(1).Text = "Cumulative Distribution Function"
            End If

            Chart1.Titles(1).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
            Chart1.Titles(1).DockedToChartArea = Chart1.ChartAreas(0).Name
            Chart1.Titles(1).IsDockedInsideChartArea = False
            Chart1.Titles(1).Docking = DataVisualization.Charting.Docking.Top

        Catch ex As Exception
            Main.Message.AddWarning("Error plotting the Series Data CDF: " & vbCrLf & ex.Message & vbCrLf)
        End Try

        Try
            'Plot the PDF Histogram (Continuous distribution) or PMF (Discrete distribution):
            If Chart1.Series.Count > 1 Then
                'If Chart1.Series(1).Name = "PDF Histogram" Then
                If Chart1.Series(1).Name = "PMF Histogram" Then

                Else
                    'Chart1.Series(1).Name = "PDF Histogram"
                    Chart1.Series(1).Name = "PMF Histogram"
                End If
            Else
                'Chart1.Series.Add("PDF Histogram")
                Chart1.Series.Add("PMF Histogram")
                Chart1.ChartAreas.Add("HistArea")
            End If

            'Chart1.Series("PDF Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Column
            'Chart1.Series("PDF Histogram").Color = Color.Blue
            'Chart1.Series("PDF Histogram").ChartArea = "HistArea"
            Chart1.Series("PMF Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("PMF Histogram").Color = Color.Blue
            Chart1.Series("PMF Histogram").ChartArea = "HistArea"

            Chart1.Titles.Add("Title3")

            If IsDiscrete Then
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Prob_Mass")
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Value", Data.Tables("Histogram").DefaultView, "Prob_Mass")
                'Chart1.Series("PMF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Value", Data.Tables("Histogram").DefaultView, "Prob_Mass")
                If XAxisValues = "SurveyCount" Then
                    Chart1.Series("PMF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Value", Data.Tables("Histogram").DefaultView, "Probability")
                Else
                    Chart1.Series("PMF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Survey_Prob", Data.Tables("Histogram").DefaultView, "Probability")
                End If

                'Chart1.Series("PMF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Probability") 'Use Mid_Interval for the column name so the functions used to calculate values from continuous data histograms can still be used for discrete data.
                'Chart1.Titles(2).Text = "Probability Mass"
                Chart1.Titles(2).Text = "Probability Mass Function"
                Chart1.ChartAreas("HistArea").AxisY.Title = "Probability Mass"

            Else
                'Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Prob_Density")
                Chart1.Series("PDF Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Value", Data.Tables("Histogram").DefaultView, "Prob_Density") 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                Chart1.Titles(2).Text = "Probability Density Histogram"
                Chart1.ChartAreas("HistArea").AxisY.Title = "Probability Density"
            End If
            Chart1.Titles(2).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
            Chart1.Titles(2).DockedToChartArea = "HistArea"
            Chart1.Titles(2).IsDockedInsideChartArea = False
            Chart1.Titles(2).Docking = DataVisualization.Charting.Docking.Top

            Chart1.ChartAreas("HistArea").AxisX.Minimum = ChartXMin
            Chart1.ChartAreas("HistArea").AxisX.Maximum = ChartXMax
            Chart1.ChartAreas("HistArea").AxisX.Interval = ChartXInterval

            'Dim DataInfoIndex As Integer = Main.MonteCarlo.DataInfoNameIndex(SourceColumnName)
            'If DataInfoIndex = -1 Then  'This is a calculated value so there is no information about this in MonteCarlo.DataInfo
            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName
            '    ElseIf Main.CalcInfo.ContainsKey(ScalarName) Then
            '        Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")" 'Show the CalcInfo abbreviated units if available
            '    Else
            '        Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName
            '    End If

            'Else
            '    If ScalarName.Trim = "" Then
            '        Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '    Else
            '        If Main.CalcInfo.ContainsKey(ScalarName) Then
            '            If Main.CalcInfo(ScalarName).UnitsAbbrev.Trim = "" Then
            '                Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '            Else
            '                Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.CalcInfo(ScalarName).UnitsAbbrev & ")"
            '            End If
            '        Else
            '            Chart1.ChartAreas("HistArea").AxisX.Title = ScalarName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
            '        End If
            '    End If
            'End If
            'ScalarName is the name of the data in the series being analysed (as used in the Monte Carlo version of this form).
            'ColumnName will be used for the ScalarName in this application.
            'Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName
            If XAxisValues = "SurveyCount" Then
                Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName & " (Survey Event Count)"
                Chart1.ChartAreas("HistArea").AxisX.LabelStyle.Format = ""
            Else
                Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName & " (Survey Probability)"
                If Main.BayesSim.Settings.ProbabilityMeasure = "Percent" Then
                    Chart1.ChartAreas("HistArea").AxisX.LabelStyle.Format = "P1"
                Else
                    Chart1.ChartAreas("HistArea").AxisX.LabelStyle.Format = ""
                End If
            End If

            Chart1.ChartAreas("HistArea").AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
            Chart1.ChartAreas("HistArea").AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

            Chart1.ChartAreas(0).AlignWithChartArea = "HistArea"
            Chart1.ChartAreas(0).AlignmentOrientation = DataVisualization.Charting.AreaAlignmentOrientations.Vertical
            Chart1.ChartAreas(0).AlignmentStyle = DataVisualization.Charting.AreaAlignmentStyles.All

            Chart1.ChartAreas(0).AxisX.RoundAxisValues()

        Catch ex As Exception
            Main.Message.AddWarning("Error plotting the Series Data PDF or PMF: " & vbCrLf & ex.Message & vbCrLf)
        End Try


        'Add a series used to plot vertical bars on the CDF chart:
        Dim IndexNo As Integer = Chart1.Series.IndexOf("CdfVertBar")
        If IndexNo = -1 Then 'Series named CdfVerBar does not exist
            Chart1.Series.Add("CdfVertBar")
            Chart1.Series("CdfVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
            Chart1.Series("CdfVertBar").Color = Color.Orange
            Chart1.Series("CdfVertBar").ChartArea = Chart1.ChartAreas(0).Name
            Chart1.Series("CdfVertBar").SetCustomProperty("PixelPointWidth", "2")
            Chart1.Series("CdfVertBar").IsVisibleInLegend = False
        Else

        End If

        'Add a series used to plot circle markers on the histogram:
        IndexNo = Chart1.Series.IndexOf("HistPoints")
        If IndexNo = -1 Then  'Series named HistPoints does not exist
            Chart1.Series.Add("HistPoints")
            Chart1.Series("HistPoints").ChartType = DataVisualization.Charting.SeriesChartType.Point
            Chart1.Series("HistPoints").Color = Color.Transparent
            Chart1.Series("HistPoints").ChartArea = "HistArea"
            Chart1.Series("HistPoints").IsVisibleInLegend = False
            Chart1.Series("HistPoints").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
            Chart1.Series("HistPoints").MarkerSize = 20
            Chart1.Series("HistPoints").MarkerBorderWidth = 2
            Chart1.Series("HistPoints").MarkerBorderColor = Color.Orange
        Else

        End If

        If chkShowModel.Checked Then 'Plot the CDF and PDF (or PMF) of the Model distribution
            If IsDiscrete Then 'Discrete distribution model

            Else 'Continuous distribution model

                If ShowReverseCDF Then
                    If Chart1.Series.Count > 4 Then
                        If Chart1.Series(4).Name = "Model Reverse CDF" Then
                        Else
                            Chart1.Series(4).Name = "Model Reverse CDF"
                        End If
                    Else
                        Chart1.Series.Add("Model Reverse CDF")
                    End If

                    Chart1.Series("Model Reverse CDF").ChartArea = Chart1.ChartAreas(0).Name
                    Chart1.Series("Model Reverse CDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("Model Reverse CDF").Color = Color.Black
                    Chart1.Series("Model Reverse CDF").BorderWidth = 1
                    Chart1.Series("Model Reverse CDF").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Dot

                    Chart1.Series("Model Reverse CDF").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Model_Rev_CDF")
                    'Chart1.Series("Model CDF").IsVisibleInLegend = False
                Else
                    'If Chart1.Series.Count > 2 Then
                    If Chart1.Series.Count > 4 Then
                        If Chart1.Series(4).Name = "Model CDF" Then
                        Else
                            Chart1.Series(4).Name = "Model CDF"
                        End If
                    Else
                        Chart1.Series.Add("Model CDF")
                    End If

                    Chart1.Series("Model CDF").ChartArea = Chart1.ChartAreas(0).Name

                    Chart1.Series("Model CDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("Model CDF").Color = Color.Black
                    Chart1.Series("Model CDF").BorderWidth = 1
                    Chart1.Series("Model CDF").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Dot

                    Chart1.Series("Model CDF").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Model_CDF")
                    'Chart1.Series("Model CDF").IsVisibleInLegend = False
                End If



                If Chart1.Series.Count > 5 Then
                    If Chart1.Series(5).Name = "Model PDF" Then
                    Else
                        Chart1.Series(5).Name = "Model PDF"
                    End If
                Else
                    Chart1.Series.Add("Model PDF")
                End If

                Chart1.Series("Model PDF").ChartArea = "HistArea"

                Chart1.Series("Model PDF").ChartType = DataVisualization.Charting.SeriesChartType.Line
                Chart1.Series("Model PDF").Color = Color.Black
                Chart1.Series("Model PDF").BorderWidth = 2
                'Chart1.Series("Model PDF").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Dash


                Chart1.Series("Model PDF").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Model_Prob_Dens")
                'Chart1.Series("Model PDF").IsVisibleInLegend = False

            End If
        Else
            'Remove any Model series:
            Dim SeriesNo As Integer = Chart1.Series.IndexOf("Model CDF")
            If SeriesNo = -1 Then
                'The Model CDF series does not exist
            Else
                Chart1.Series.RemoveAt(SeriesNo)
            End If

            SeriesNo = Chart1.Series.IndexOf("Model Reverse CDF")
            If SeriesNo = -1 Then
                'The Model Reverse CDF series does not exist
            Else
                Chart1.Series.RemoveAt(SeriesNo)
            End If

            SeriesNo = Chart1.Series.IndexOf("Model PDF")
            If SeriesNo = -1 Then
                'The Model PDF series does not exist
            Else
                Chart1.Series.RemoveAt(SeriesNo)
            End If

        End If

        ApplyChartMinMax()

        Dim NTitles As Integer = Chart1.Titles.Count
        If NTitles > 0 Then
            NumericUpDown1.Minimum = 1
            NumericUpDown1.Maximum = NTitles
            NumericUpDown1.Increment = 1
            NumericUpDown1.Value = 1
            txtEditTitle.Text = Chart1.Titles(0).Text
            txtEditTitle.Font = Chart1.Titles(0).Font
            txtEditTitle.ForeColor = Chart1.Titles(0).ForeColor
        Else
            NumericUpDown1.Minimum = 0
            NumericUpDown1.Maximum = 0
            NumericUpDown1.Increment = 1
            NumericUpDown1.Value = 0
            txtEditTitle.Text = ""
        End If

    End Sub

    ''OLD CODE: [Replaced by PlotCharts()]
    'Private Sub PlotCDF()
    '    'Plot the Cumulative Density Function on the chart.

    '    Try
    '        'Chart1.Series(0).Name = "CDF"
    '        If ShowReverseCDF Then
    '            Chart1.Series(0).Name = "Reverse Cumulative Distribution Function"
    '        Else
    '            Chart1.Series(0).Name = "Cumulative Distribution Function"
    '        End If

    '        Chart1.Series(0).ChartType = DataVisualization.Charting.SeriesChartType.Line

    '        Chart1.Series(0).Color = Color.Red
    '        Chart1.Series(0).BorderWidth = 2

    '        'Chart1.ChartAreas(0).AxisX.Minimum = Data.Tables("Series").Rows(0).Item("CDF") 'The returns the first unsorted value - not the minimum
    '        'Chart1.ChartAreas(0).AxisX.Minimum = MinSeriesVal
    '        'Chart1.ChartAreas(0).AxisX.Minimum = Double.NaN 'Auto
    '        Chart1.ChartAreas(0).AxisX.Minimum = Val(txtChartMin.Text)


    '        'Chart1.ChartAreas(0).AxisX.Maximum = Data.Tables("Series").Rows(Data.Tables("Series").Rows.Count - 1).Item("CDF") 'This return the last unsorted value - not the maximum
    '        'Chart1.ChartAreas(0).AxisX.Maximum = MaxSeriesVal
    '        'Chart1.ChartAreas(0).AxisX.Maximum = Double.NaN 'Auto
    '        Chart1.ChartAreas(0).AxisX.Maximum = Val(txtChartMax.Text)
    '        Chart1.ChartAreas(0).AxisX.Interval = Val(txtChartInterval.Text)

    '        Dim DataInfoIndex As Integer = Main.MonteCarlo.DataInfoNameIndex(SourceColumnName)
    '        If DataInfoIndex = -1 Then
    '            Chart1.ChartAreas(0).AxisX.Title = SourceColumnName
    '        Else
    '            Chart1.ChartAreas(0).AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
    '        End If

    '        Chart1.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)
    '        Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"

    '        'Chart1.ChartAreas(0).AxisY.Title = "Probability"
    '        Chart1.ChartAreas(0).AxisY.Title = "Cumulative" & vbCrLf & "Probability"
    '        Chart1.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

    '        Chart1.ChartAreas(0).AxisY.Minimum = 0
    '        Chart1.ChartAreas(0).AxisY.Maximum = 1



    '        'Chart1.ChartAreas(0).AxisX.RoundAxisValues()
    '        'Chart1.Series("CDF").Points.DataBindXY(Data.Tables("Series").DefaultView, "CDF", Data.Tables("Series").DefaultView, "Probability")
    '        'Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "CDF", Data.Tables("Series").DefaultView, "Probability")
    '        If ShowReverseCDF Then
    '            Chart1.Series("Reverse Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Probability")
    '            Chart1.Series("Reverse Cumulative Distribution Function").IsVisibleInLegend = False
    '        Else
    '            Chart1.Series("Cumulative Distribution Function").Points.DataBindXY(Data.Tables("Series").DefaultView, "Value", Data.Tables("Series").DefaultView, "Probability")
    '            Chart1.Series("Cumulative Distribution Function").IsVisibleInLegend = False
    '        End If




    '        Chart1.Titles.Clear()
    '        Chart1.Titles.Add("Title1")
    '        Chart1.Titles(0).Text = "Random Variable: " & SourceColumnName
    '        Chart1.Titles(0).Font = New Font("Arial", 16, FontStyle.Regular Or FontStyle.Bold)
    '        Chart1.Titles(0).DockedToChartArea = Chart1.ChartAreas(0).Name
    '        Chart1.Titles(0).IsDockedInsideChartArea = False
    '        Chart1.Titles(0).Docking = DataVisualization.Charting.Docking.Top

    '        Chart1.Titles.Add("Title2")
    '        If ShowReverseCDF Then
    '            Chart1.Titles(1).Text = "Reverse Cumulative Distribution Function"
    '        Else
    '            Chart1.Titles(1).Text = "Cumulative Distribution Function"
    '        End If

    '        Chart1.Titles(1).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
    '        Chart1.Titles(1).DockedToChartArea = Chart1.ChartAreas(0).Name
    '        Chart1.Titles(1).IsDockedInsideChartArea = False
    '        Chart1.Titles(1).Docking = DataVisualization.Charting.Docking.Top

    '        ''Add a series used to plot vertical bars on the CDF chart:
    '        'Chart1.Series.Add("VertBar")
    '        'Chart1.Series("VertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
    '        'Chart1.Series("VertBar").Color = Color.Orange

    '    Catch ex As Exception
    '        Main.Message.AddWarning(ex.Message & vbCrLf)
    '    End Try
    'End Sub

    Private Sub GenHistogramData(ByVal MidVal As Double, ByVal IntWidth As Double)
        'Generate the histogram data.
        'MidVal is the mid value of an interval - MidVal can be in the first interval - If there is data preceeding this interval, intervals will be added to include all data.
        'IntWidth is the width of the intervals

        'Remove the old PDF table if it exists.
        If Data.Tables.Contains("Histogram") Then
            Data.Tables.Remove("Histogram")
        End If

        Data.Tables.Add("Histogram")
        cmbTableName.Items.Add("Histogram")

        If IsDiscrete Then
            'Create the Value column:
            Data.Tables("Histogram").Columns.Add("Value", System.Type.GetType("System.Double"))
        Else
            'Create the Mid_Interval column:
            Data.Tables("Histogram").Columns.Add("Mid_Interval", System.Type.GetType("System.Double"))
        End If

        'Create the Count column:
        Data.Tables("Histogram").Columns.Add("Count", System.Type.GetType("System.Int32"))
        'Create the Probability column:
        Data.Tables("Histogram").Columns.Add("Probability", System.Type.GetType("System.Double"))

        'The MinSeriesVal property contains the minimum data value in the series.
        Dim StartGap As Double = MidVal - MinSeriesVal 'The gap between the first data value in the series and the MidVal histogram interval.
        'Dim FirstMidVal As Double = MidVal + Int(StartGap / IntWidth) * IntWidth 'The mid value of the first histogram interval - to include all data points.
        Dim FirstMidVal As Double = MidVal - Int(StartGap / IntWidth) * IntWidth 'The mid value of the first histogram interval - to include all data points.

        Dim MidInterval As Double = FirstMidVal 'The mid value of the current histogram interval
        Dim EndInterval As Double = FirstMidVal + IntWidth / 2 'The maximum value of the current histogram interval
        Dim PointCount As Integer = 0 'The count of points within the current histogram interval
        For Each Row As DataRowView In Data.Tables("Series").DefaultView 'This uses the sorted row view
            'If Row.Item("CDF") < EndInterval Then
            If Row.Item("Value") < EndInterval Then
                PointCount += 1
            Else
                Data.Tables("Histogram").Rows.Add(EndInterval - IntWidth / 2, PointCount, PointCount / NRows)
                PointCount = 0
                EndInterval += IntWidth
            End If
        Next
    End Sub

    ''OLD CODE: [Replaced by PlotCharts()]
    'Private Sub PlotHistogram()
    '    'Plot the histogram chart.

    '    'If Chart1.Series(0).Name = "CDF" Then
    '    If (Chart1.Series(0).Name = "Cumulative Distribution Function") Or (Chart1.Series(0).Name = "Reverse Cumulative Distribution Function") Then
    '        If Chart1.Series.Count > 1 Then
    '            If Chart1.Series(1).Name = "Histogram" Then

    '            Else
    '                Chart1.Series(1).Name = "Histogram"
    '            End If
    '        Else
    '            Chart1.Series.Add("Histogram")
    '            Chart1.ChartAreas.Add("HistArea")
    '            'Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Line
    '            ''Chart1.Series("Histogram").ChartArea = Chart1.ChartAreas(0).Name
    '            'Chart1.ChartAreas.Add("HistArea")
    '            'Chart1.Series("Histogram").ChartArea = "HistArea"
    '            ''Chart1.Series("Histogram").XAxisType = DataVisualization.Charting.AxisType.Secondary
    '            ''Chart1.Series("Histogram").YAxisType = DataVisualization.Charting.AxisType.Secondary
    '            'Chart1.ChartAreas(0).AxisX.RoundAxisValues()
    '            'Chart1.Series("Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Probability")

    '            ''Use the same X Axis ranges:
    '            'Chart1.ChartAreas(0).AxisX.Minimum = Chart1.ChartAreas("HistArea").AxisX.Minimum
    '            'Chart1.ChartAreas(0).AxisX.Maximum = Chart1.ChartAreas("HistArea").AxisX.Maximum
    '        End If
    '        'Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Line
    '        'Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Bar
    '        Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Column

    '        Chart1.Series("Histogram").Color = Color.Blue

    '        'Chart1.Series("Histogram").ChartArea = Chart1.ChartAreas(0).Name
    '        'Chart1.ChartAreas.Add("HistArea")
    '        Chart1.Series("Histogram").ChartArea = "HistArea"
    '        'Chart1.Series("Histogram").XAxisType = DataVisualization.Charting.AxisType.Secondary
    '        'Chart1.Series("Histogram").YAxisType = DataVisualization.Charting.AxisType.Secondary
    '        'Chart1.ChartAreas(0).AxisX.RoundAxisValues()
    '        Chart1.Series("Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Probability")

    '        Chart1.Series("Histogram").IsVisibleInLegend = False

    '        'Use the same X Axis ranges:
    '        'Chart1.ChartAreas(0).AxisX.Minimum = Chart1.ChartAreas("HistArea").AxisX.Minimum
    '        'Chart1.ChartAreas(0).AxisX.Maximum = Chart1.ChartAreas("HistArea").AxisX.Maximum

    '        Chart1.ChartAreas("HistArea").AxisX.Minimum = Val(txtChartMin.Text)
    '        Chart1.ChartAreas("HistArea").AxisX.Maximum = Val(txtChartMax.Text)
    '        Chart1.ChartAreas("HistArea").AxisX.Interval = Val(txtChartInterval.Text)

    '        Dim DataInfoIndex As Integer = Main.MonteCarlo.DataInfoNameIndex(SourceColumnName)
    '        If DataInfoIndex = -1 Then
    '            Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName
    '        Else
    '            Chart1.ChartAreas("HistArea").AxisX.Title = SourceColumnName & " (" & Main.MonteCarlo.DataInfo(DataInfoIndex).Units & ")"
    '        End If
    '        Chart1.ChartAreas("HistArea").AxisX.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

    '        Chart1.ChartAreas("HistArea").AxisY.Title = "Probability"
    '        Chart1.ChartAreas("HistArea").AxisY.TitleFont = New Font("Arial", 12, FontStyle.Regular Or FontStyle.Bold)

    '        Chart1.Titles.Add("Title3")
    '        Chart1.Titles(2).Text = "Histogram"
    '        Chart1.Titles(2).Font = New Font("Arial", 14, FontStyle.Regular Or FontStyle.Bold)
    '        Chart1.Titles(2).DockedToChartArea = "HistArea"
    '        Chart1.Titles(2).IsDockedInsideChartArea = False
    '        Chart1.Titles(2).Docking = DataVisualization.Charting.Docking.Top


    '        Chart1.ChartAreas(0).AlignWithChartArea = "HistArea"
    '        Chart1.ChartAreas(0).AlignmentOrientation = DataVisualization.Charting.AreaAlignmentOrientations.Vertical
    '        Chart1.ChartAreas(0).AlignmentStyle = DataVisualization.Charting.AreaAlignmentStyles.All

    '        Chart1.ChartAreas(0).AxisX.RoundAxisValues()

    '        Main.Message.Add("Chart1.ChartAreas(0).AxisX.Minimum " & Chart1.ChartAreas(0).AxisX.Minimum & vbCrLf)
    '        Main.Message.Add("Chart1.ChartAreas(0).AxisX.Maximum " & Chart1.ChartAreas(0).AxisX.Maximum & vbCrLf)
    '        Main.Message.Add("Chart1.ChartAreas(0).AxisX.Interval " & Chart1.ChartAreas(0).AxisX.Interval & vbCrLf)


    '        Main.Message.Add("Chart1.ChartAreas(1).AxisX.Minimum " & Chart1.ChartAreas(1).AxisX.Minimum & vbCrLf)
    '        Main.Message.Add("Chart1.ChartAreas(1).AxisX.Maximum " & Chart1.ChartAreas(1).AxisX.Maximum & vbCrLf)
    '        Main.Message.Add("Chart1.ChartAreas(1).AxisX.Interval " & Chart1.ChartAreas(1).AxisX.Interval & vbCrLf)

    '        'Add a series used to plot vertical bars on the CDF chart:
    '        Dim IndexNo As Integer = Chart1.Series.IndexOf("CdfVertBar")
    '        If IndexNo = -1 Then 'Series named CdfVerBar does not exist
    '            Chart1.Series.Add("CdfVertBar")
    '            Chart1.Series("CdfVertBar").ChartType = DataVisualization.Charting.SeriesChartType.Column
    '            Chart1.Series("CdfVertBar").Color = Color.Orange
    '            Chart1.Series("CdfVertBar").ChartArea = Chart1.ChartAreas(0).Name
    '            Chart1.Series("CdfVertBar").SetCustomProperty("PixelPointWidth", "2")
    '            Chart1.Series("CdfVertBar").IsVisibleInLegend = False
    '        Else

    '        End If

    '        'Add points with circle markers instead:
    '        IndexNo = Chart1.Series.IndexOf("HistPoints")
    '        If IndexNo = -1 Then  'Series named HistPoints does not exist
    '            Chart1.Series.Add("HistPoints")
    '            Chart1.Series("HistPoints").ChartType = DataVisualization.Charting.SeriesChartType.Point
    '            Chart1.Series("HistPoints").Color = Color.Transparent
    '            Chart1.Series("HistPoints").ChartArea = "HistArea"
    '            Chart1.Series("HistPoints").IsVisibleInLegend = False
    '            Chart1.Series("HistPoints").MarkerStyle = DataVisualization.Charting.MarkerStyle.Circle
    '            Chart1.Series("HistPoints").MarkerSize = 20
    '            Chart1.Series("HistPoints").MarkerBorderWidth = 2
    '            Chart1.Series("HistPoints").MarkerBorderColor = Color.Orange
    '        Else

    '        End If

    '    Else
    '        Chart1.Series(0).Name = "Histogram"
    '        Chart1.Series("Histogram").ChartType = DataVisualization.Charting.SeriesChartType.Line
    '        Chart1.ChartAreas(0).AxisX.LabelStyle.Format = "#.##"
    '        Chart1.ChartAreas(0).AxisX.RoundAxisValues()
    '        Chart1.Series("Histogram").Points.DataBindXY(Data.Tables("Histogram").DefaultView, "Mid_Interval", Data.Tables("Histogram").DefaultView, "Probability")
    '    End If

    'End Sub

    Private Sub cmbTableName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTableName.SelectedIndexChanged
        If cmbTableName.Focused Then
            _tableName = cmbTableName.SelectedItem.ToString
            '_calcTableName = cmbTableName.SelectedItem.ToString
            UpdateDataGridView()
        End If
    End Sub

    Private Sub UpdateDataGridView()
        'Update the data grid biew in the Data tab

        dgvData.Columns.Clear()
        dgvData.AutoGenerateColumns = True
        dgvData.DataSource = Data.Tables(TableName)
        'dgvData.DataSource = Data.Tables(CalcTableName)
        dgvData.AutoResizeColumns()
        dgvData.Update()
        dgvData.Refresh()

    End Sub

    Private Sub btnUpdateHistogram_Click(sender As Object, e As EventArgs) Handles btnUpdateHistogram.Click
        'Update the Histogram data

        If txtIntervalWidth.Text.Trim = "" Then
            Main.Message.AddWarning("Enter an interval width to use in the histogram." & vbCrLf)
        Else

            GenHistogramData(Val(txtHistMin.Text), Val(txtIntervalWidth.Text))
            'PlotHistogram()

            If IsDiscrete Then
                PlotDiscreteCharts()
            Else
                PlotCharts()
            End If
        End If
    End Sub

    Private Sub btnShowChartMinMax_Click(sender As Object, e As EventArgs) Handles btnShowChartMinMax.Click
        'Show the current Chart Minimum, Maximum and Interval
        If Chart1.ChartAreas.Count = 0 Then
            Main.Message.AddWarning("No chart areas are defined." & vbCrLf)
        Else
            txtChartMin.Text = Chart1.ChartAreas(0).AxisX.Minimum
            txtChartMax.Text = Chart1.ChartAreas(0).AxisX.Maximum
            txtChartInterval.Text = Chart1.ChartAreas(0).AxisX.Interval
        End If
    End Sub

    Private Sub btnUpdateChartMinMax_Click(sender As Object, e As EventArgs) Handles btnUpdateChartMinMax.Click
        'Update the chart displays with the specified minimum, maximum and interval.

        ApplyChartMinMax()
    End Sub

    Private Sub ApplyChartMinMax()
        'Apply the Chart Minimum, Maximum and Interval if specified.

        Dim NChartAreas As Integer = Chart1.ChartAreas.Count
        Dim I As Integer

        Try
            If txtChartMin.Text.Trim = "" Then
                'The Chart Minimum is not specified.
            Else
                Dim ChartMin As Double = txtChartMin.Text
                For I = 0 To NChartAreas - 1
                    Chart1.ChartAreas(I).AxisX.Minimum = ChartMin
                Next
            End If
        Catch ex As Exception
            Main.Message.AddWarning("Error setting the chart minimum: " & ex.Message & vbCrLf)
        End Try

        Try
            If txtChartMax.Text.Trim = "" Then
                'The Chart Maximum is not specified.
            Else
                Dim ChartMax As Double = txtChartMax.Text
                For I = 0 To NChartAreas - 1
                    Chart1.ChartAreas(I).AxisX.Maximum = ChartMax
                Next
            End If
        Catch ex As Exception
            Main.Message.AddWarning("Error setting the chart maximum: " & ex.Message & vbCrLf)
        End Try

        Try
            If txtChartInterval.Text.Trim = "" Then
                'The Chart Interval is not specified.
            Else
                Dim ChartInt As Double = txtChartInterval.Text
                For I = 0 To NChartAreas - 1
                    Chart1.ChartAreas(I).AxisX.Interval = ChartInt
                Next
            End If
        Catch ex As Exception
            Main.Message.AddWarning("Error setting the chart interval: " & ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub txtNIntervals_TextChanged(sender As Object, e As EventArgs) Handles txtNIntervals.TextChanged

    End Sub

    Private Sub txtNIntervals_LostFocus(sender As Object, e As EventArgs) Handles txtNIntervals.LostFocus
        HistNIntervals = Val(txtNIntervals.Text)
    End Sub

    Private Sub txtIntervalWidth_TextChanged(sender As Object, e As EventArgs) Handles txtIntervalWidth.TextChanged

    End Sub

    Private Sub txtIntervalWidth_LostFocus(sender As Object, e As EventArgs) Handles txtIntervalWidth.LostFocus
        _histNIntervals = Int((MaxSeriesVal - MinSeriesVal) / Val(txtIntervalWidth.Text))
        txtNIntervals.Text = _histNIntervals
    End Sub

    Private Function PreferredInterval(ByVal RawInterval As Double) As Double
        'Return a preferred interval value from a raw interval.
        'Preferred intervals are rounded to a number containing fewer significant figures.
        'Examples: Raw    Preferred
        '          0.234  0.25
        '          497    500
        '          89.4   100  
        '          18.1   20

        'Convert the RawInterval to scientific notation Coeff x 10 ^ Exponent
        Dim Coeff As Double
        Dim Exponent As Integer

        Dim Log10RawInt As Double = Math.Log10(RawInterval)
        Exponent = Math.Floor(Log10RawInt)
        Coeff = RawInterval / 10 ^ Exponent

        Dim PreferredCoeff = NearestPrefCoeff(Coeff, {1, 2, 2.5, 5, 10}) 'Select the coefficient from the preferred coefficient list - the one nearest to the raw coefficient
        Return PreferredCoeff * 10 ^ Exponent 'The preferred interval is reconstructed from the preferred coefficient and the exponent
    End Function

    Private Function NearestPrefCoeff(ByVal RawCoeff As Double, ByVal PrefCoeff() As Double) As Double
        'Returns the nearest preferred coefficient to the Raw Coefficient

        If PrefCoeff.Count > 0 Then
            Dim Nearest As Double = PrefCoeff(0) 'The current nearest preferred coefficent
            Dim NearestAbsDiff As Double = Math.Abs(RawCoeff - PrefCoeff(0)) 'The current nearest absolute difference between the Raw Coefficient and the Preferred Coefficient
            Dim AbsDiff As Double 'The absolute difference between the Raw Coefficient and the Preferred Coefficient
            For Each item In PrefCoeff
                AbsDiff = Math.Abs(RawCoeff - item)
                If AbsDiff < NearestAbsDiff Then
                    Nearest = item
                    NearestAbsDiff = AbsDiff
                End If
            Next
            Return Nearest
        Else
            Main.Message.AddWarning("There are no preferred coefficents in the list." & vbCrLf)
            Return RawCoeff
        End If
    End Function


    'OLD CODE: [Replaced by GetHistogramSettings()]
    Private Sub SetUpHistogram(ByVal NIntervals As Integer)
        'Set up the Histogram settings to match the specified NIntervals.


        Dim RawInterval As Double = (MaxSeriesVal - MinSeriesVal) / NIntervals 'First calculate the Raw Interval
        Dim PrefInterval As Double = PreferredInterval(RawInterval) 'The preferred interval has the significant digits: 1, 2, 2.5, 5, 10
        Dim PrefMin As Double = Math.Floor(MinSeriesVal / PrefInterval) * PrefInterval
        Dim PrefMax As Double = Math.Ceiling(MaxSeriesVal / PrefInterval) * PrefInterval
        Dim NewNIntervals As Integer = Math.Round((PrefMax - PrefMin) / PrefInterval)

        txtHistMin.Text = PrefMin
        txtHistMax.Text = PrefMax
        txtNIntervals.Text = NewNIntervals
        txtIntervalWidth.Text = PrefInterval
        Main.Message.Add("Preferred histogram interval: " & PrefInterval & vbCrLf)
        Main.Message.Add("Number of intervals: " & NewNIntervals & vbCrLf)

    End Sub

    Private Sub GetHistogramSettings()
        'Get settings to display a histogram of the data.

        If IsDiscrete Then
            HistIntervalWidth = 1
            HistMin = MinSeriesVal
            HistMax = MaxSeriesVal
            HistNIntervals = MaxSeriesVal - MinSeriesVal + 1
        Else
            InitNIntervals = Int(NRows / 100) 'Initial number of intervals = the numer of data rows / 100. For 1000 points: 10 intervals, 10000 points: 100 intervals. The number of intervals will be adjusted below.
            Dim RawInterval As Double = (MaxSeriesVal - MinSeriesVal) / InitNIntervals 'First calculate the Raw Interval - InitNIntervals (the initial number of intervals) must be set
            HistIntervalWidth = PreferredInterval(RawInterval) 'The preferred interval has the significant digits: 1, 2, 2.5, 5, 10
            HistMin = Math.Floor(MinSeriesVal / HistIntervalWidth) * HistIntervalWidth
            HistMax = Math.Ceiling(MaxSeriesVal / HistIntervalWidth) * HistIntervalWidth
            HistNIntervals = Math.Round(((HistMax - HistMin) / HistIntervalWidth) + 1)

        End If
    End Sub

    'OLD CODE: [Replaced by GetChartXScale()]
    Private Sub SetUpChartScale(ByVal NLabels As Integer)
        'Set up the Chart X Scale to match the specified number of labels.

        Dim RawInterval As Double = (MaxSeriesVal - MinSeriesVal) / NLabels
        Dim PrefInterval As Double = PreferredInterval(RawInterval)
        Dim PrefMin As Double = Math.Floor(MinSeriesVal / PrefInterval) * PrefInterval
        Dim PrefMax As Double = Math.Ceiling(MaxSeriesVal / PrefInterval) * PrefInterval

        txtChartMin.Text = PrefMin
        txtChartMax.Text = PrefMax
        txtChartInterval.Text = PrefInterval

    End Sub

    Private Sub GetChartXScale(ByVal RawPixelsPerInterval As Integer)
        'Get sutable values for ChartXMin, ChartXMax and ChartXInterval.
        'ChartXInterval is generated using the RawPixelPerInterval value.

        Dim XAxisLength As Single = Chart1.ChartAreas(0).InnerPlotPosition.Width 'X Axis length in relative coordinates, which range from 0 to 100.
        Dim ChartPixelWidth As Integer = Chart1.Width 'The width of the entire chart image in pixels.
        Dim XAxisPixelLength As Integer = Int(ChartPixelWidth * XAxisLength / 100)
        Dim XRawNIntervals As Integer = Int(XAxisPixelLength / RawPixelsPerInterval) 'The Raw number of axis annotation intervals based on the Raw pixels per annotation.

        Dim XRawInterval As Double = (MaxSeriesVal - MinSeriesVal) / XRawNIntervals 'First calculate the Raw Interval
        ChartXInterval = PreferredInterval(XRawInterval) 'The preferred X Axis lavel interval has the significant digits: 1, 2, 2.5, 5, 10.
        ChartXMin = Math.Floor(MinSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Minimum for the Chart display
        ChartXMax = Math.Ceiling(MaxSeriesVal / ChartXInterval) * ChartXInterval 'The preferred Axis Maximum for the Chart display
    End Sub

    'NOTE: See the updated GetValue function that includes Discrete data.
    'Private Function GetValue(ByVal Prob As Double) As Double
    '    'Get the value corresponding the the given probability value.
    '    'The value is interpolated from the records in the Series table spanning the probability value.

    '    Dim Row1 As DataRow
    '    Dim Row2 As DataRow

    '    If ShowReverseCDF Then
    '        Row1 = Data.Tables("Series").Select("Reverse_CDF < '" & Prob & "'").Last
    '        Row2 = Data.Tables("Series").Select("Reverse_CDF > '" & Prob & "'").First
    '    Else
    '        Row1 = Data.Tables("Series").Select("CDF < '" & Prob & "'").Last
    '        Row2 = Data.Tables("Series").Select("CDF > '" & Prob & "'").First
    '    End If


    '    If Row1 Is Nothing Then
    '        Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
    '        Return 0
    '    Else
    '        If Row2 Is Nothing Then
    '            Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
    '            Return 0
    '        Else
    '            If ShowReverseCDF Then
    '                Return Row1.Item("Value") + (Prob - Row1.Item("Reverse_CDF")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("Reverse_CDF") - Row1.Item("Reverse_CDF"))
    '            Else
    '                Return Row1.Item("Value") + (Prob - Row1.Item("CDF")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("CDF") - Row1.Item("CDF"))
    '            End If
    '        End If
    '    End If
    'End Function

    'NOTE: This updated version of GetValue handles Discrete distributions as well as Continuous.
    Private Function GetValue(ByVal Prob As Double) As Double
        'Get the value corresponding the the given probability value.
        'The value is interpolated from the records in the Series table spanning the probability value.
        Try


            Dim Row1 As DataRow
            Dim Row2 As DataRow
            If IsDiscrete Then
                If ShowReverseCDF Then
                    Row1 = Data.Tables("Histogram").Select("Reverse_Cum_Prob < '" & Prob & "'").Last
                    Row2 = Data.Tables("Histogram").Select("Reverse_Cum_Prob > '" & Prob & "'").First
                Else
                    Row1 = Data.Tables("Histogram").Select("Cum_Probability < '" & Prob & "'").Last
                    Row2 = Data.Tables("Histogram").Select("Cum_Probability > '" & Prob & "'").First
                End If

                If Row1 Is Nothing Then
                    Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                    Return 0
                Else
                    If Row2 Is Nothing Then
                        Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                        Return 0
                    Else
                        If ShowReverseCDF Then
                            If XAxisValues = "SurveyCount" Then
                                Return Row1.Item("Value") + (Prob - Row1.Item("Reverse_Cum_Prob")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("Reverse_Cum_Prob") - Row1.Item("Reverse_Cum_Prob"))
                            Else
                                Return Row1.Item("Survey_Prob") + (Prob - Row1.Item("Reverse_Cum_Prob")) * (Row2.Item("Survey_Prob") - Row1.Item("Survey_Prob")) / (Row2.Item("Reverse_Cum_Prob") - Row1.Item("Reverse_Cum_Prob"))
                            End If

                            'Return Row1.Item("Mid_Interval") + (Prob - Row1.Item("Reverse_Cum_Prob")) * (Row2.Item("Mid_Interval") - Row1.Item("Mid_Interval")) / (Row2.Item("Reverse_Cum_Prob") - Row1.Item("Reverse_Cum_Prob"))
                        Else
                            If XAxisValues = "SurveyCount" Then
                                Return Row1.Item("Value") + (Prob - Row1.Item("Cum_Probability")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("Cum_Probability") - Row1.Item("Cum_Probability"))
                            Else
                                Return Row1.Item("Survey_Prob") + (Prob - Row1.Item("Cum_Probability")) * (Row2.Item("Survey_Prob") - Row1.Item("Survey_Prob")) / (Row2.Item("Cum_Probability") - Row1.Item("Cum_Probability"))
                            End If

                            'Return Row1.Item("Mid_Interval") + (Prob - Row1.Item("Cum_Probability")) * (Row2.Item("Mid_Interval") - Row1.Item("Mid_Interval")) / (Row2.Item("Cum_Probability") - Row1.Item("Cum_Probability")) 'Mid_Interval column name is used (as for continuous data) so the same functions can bed used on both data types.
                        End If
                    End If
                End If
            Else
                If ShowReverseCDF Then
                    Row1 = Data.Tables("Series").Select("Reverse_CDF < '" & Prob & "'").Last
                    Row2 = Data.Tables("Series").Select("Reverse_CDF > '" & Prob & "'").First
                Else
                    Row1 = Data.Tables("Series").Select("CDF < '" & Prob & "'").Last
                    Row2 = Data.Tables("Series").Select("CDF > '" & Prob & "'").First
                End If

                If Row1 Is Nothing Then
                    Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                    Return 0
                Else
                    If Row2 Is Nothing Then
                        Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                        Return 0
                    Else
                        If ShowReverseCDF Then
                            Return Row1.Item("Value") + (Prob - Row1.Item("Reverse_CDF")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("Reverse_CDF") - Row1.Item("Reverse_CDF"))
                        Else
                            Return Row1.Item("Value") + (Prob - Row1.Item("CDF")) * (Row2.Item("Value") - Row1.Item("Value")) / (Row2.Item("CDF") - Row1.Item("CDF"))
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Main.Message.AddWarning("Error: GetValue(" & Prob & "): " & vbCrLf & ex.Message & vbCrLf)
        End Try

    End Function

    'NOTE: See the updated GetProb function that includes Discrete data.
    'Private Function GetProb(ByVal Value As Double) As Double
    '    'Get the probability corresponding to the given value.
    '    'The probability is interpolated from the records inthe Series table spanning the value
    '    Try
    '        Dim Row1 As DataRow = Data.Tables("Series").Select("Value < '" & Value & "'").Last
    '        Dim Row2 As DataRow = Data.Tables("Series").Select("Value > '" & Value & "'").First

    '        If Row1 Is Nothing Then
    '            Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
    '            Return 0
    '        Else
    '            If Row2 Is Nothing Then
    '                Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
    '                Return 0
    '            Else
    '                Return Row1.Item("CDF") + (Value - Row1.Item("Value")) * (Row2.Item("CDF") - Row1.Item("CDF")) / (Row2.Item("Value") - Row1.Item("Value"))
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Main.Message.AddWarning(ex.Message & vbCrLf)
    '    End Try

    'End Function

    'NOTE: This updated version of GetProb handles Discrete distributions as well as Continuous.
    Private Function GetProb(ByVal Value As Double) As Double
        'Get the probability corresponding to the given value.
        'The probability is interpolated from the records in the Series table spanning the value
        Try


            If IsDiscrete Then
                'Dim Row1 As DataRow = Data.Tables("Histogram").Select("Value < '" & Value & "'").Last
                'Dim Row2 As DataRow = Data.Tables("Histogram").Select("Value > '" & Value & "'").First
                ''Dim Row1 As DataRow = Data.Tables("Histogram").Select("Mid_Interval < '" & Value & "'").Last
                ''Dim Row2 As DataRow = Data.Tables("Histogram").Select("Mid_Interval > '" & Value & "'").First
                'If Row1 Is Nothing Then
                '    Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                '    Return 0
                'Else
                '    If Row2 Is Nothing Then
                '        Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                '        Return 0
                '    Else
                '        If XAxisValues = "SurveyCount" Then
                '            Return Row1.Item("Cum_Probability") + (Value - Row1.Item("Value")) * (Row2.Item("Cum_Probability") - Row1.Item("Cum_Probability")) / (Row2.Item("Value") - Row1.Item("Value"))
                '        Else
                '            Return Row1.Item("Cum_Probability") + (Value - Row1.Item("Survey_Prob")) * (Row2.Item("Cum_Probability") - Row1.Item("Cum_Probability")) / (Row2.Item("Survey_Prob") - Row1.Item("Survey_Prob"))
                '        End If
                '        'Return Row1.Item("Cum_Probability") + (Value - Row1.Item("Mid_Interval")) * (Row2.Item("Cum_Probability") - Row1.Item("Cum_Probability")) / (Row2.Item("Mid_Interval") - Row1.Item("Mid_Interval"))
                '    End If
                'End If

                If XAxisValues = "SurveyCount" Then
                    Dim Row1 As DataRow = Data.Tables("Histogram").Select("Value < '" & Value & "'").Last
                    Dim Row2 As DataRow = Data.Tables("Histogram").Select("Value > '" & Value & "'").First
                    If Row1 Is Nothing Then
                        Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                        Return 0
                    Else
                        If Row2 Is Nothing Then
                            Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                            Return 0
                        Else
                            Return Row1.Item("Cum_Probability") + (Value - Row1.Item("Value")) * (Row2.Item("Cum_Probability") - Row1.Item("Cum_Probability")) / (Row2.Item("Value") - Row1.Item("Value"))
                        End If
                    End If

                Else
                    Dim Row1 As DataRow = Data.Tables("Histogram").Select("Survey_Prob < '" & Value & "'").Last
                    Dim Row2 As DataRow = Data.Tables("Histogram").Select("Survey_Prob > '" & Value & "'").First
                    If Row1 Is Nothing Then
                        Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                        Return 0
                    Else

                        If Row2 Is Nothing Then
                            Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                            Return 0
                        Else
                            Return Row1.Item("Cum_Probability") + (Value - Row1.Item("Survey_Prob")) * (Row2.Item("Cum_Probability") - Row1.Item("Cum_Probability")) / (Row2.Item("Survey_Prob") - Row1.Item("Survey_Prob"))
                        End If
                    End If
                End If

            Else
                Dim Row1 As DataRow = Data.Tables("Series").Select("Value < '" & Value & "'").Last
                Dim Row2 As DataRow = Data.Tables("Series").Select("Value > '" & Value & "'").First
                If Row1 Is Nothing Then
                    Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                    Return 0
                Else
                    If Row2 Is Nothing Then
                        Main.Message.AddWarning("Error interpolating the value." & vbCrLf)
                        Return 0
                    Else
                        Return Row1.Item("CDF") + (Value - Row1.Item("Value")) * (Row2.Item("CDF") - Row1.Item("CDF")) / (Row2.Item("Value") - Row1.Item("Value"))
                    End If
                End If
            End If
        Catch ex As Exception
            Main.Message.AddWarning("Error getting the probability corresponding to value: " & Value & vbCrLf & ex.Message & vbCrLf)
        End Try

    End Function

    Private Sub dgvAnnot_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAnnot.CellContentClick

    End Sub


    Private Sub AddProbAnnot(ByVal RowNo As Integer)
        'Add a probability annotation entry at the specified row number.

        Dim P1 As Boolean = False 'If True then the P1 annotation is in the list
        Dim P10 As Boolean = False 'If True then the P10 annotation is in the list
        Dim P50 As Boolean = False 'If True then the P50 annotation is in the list
        Dim P90 As Boolean = False 'If True then the P90 annotation is in the list
        Dim P99 As Boolean = False 'If True then the P99 annotation is in the list

        Dim ProbVal As Double 'The selected probability value

        dgvAnnot.AllowUserToAddRows = False

        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(2).Value = "Probability" Then
                If dgvAnnot.Rows(I).Cells(5).Value = 0.01 Then P1 = True
                If dgvAnnot.Rows(I).Cells(5).Value = 0.1 Then P10 = True
                If dgvAnnot.Rows(I).Cells(5).Value = 0.5 Then P50 = True
                If dgvAnnot.Rows(I).Cells(5).Value = 0.9 Then P90 = True
                If dgvAnnot.Rows(I).Cells(5).Value = 0.99 Then P99 = True
            End If
        Next

        If P10 = False Then 'Enter P10 probability annotation settings:
            Dim P10Value As Double = GetValue(0.1)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.1, "P10", 0.1, P10Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.1
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P10"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.1
                dgvAnnot.Rows(RowNo).Cells(6).Value = P10Value
            End If
            DisplayAnnot(0.1, P10Value, "P10")
        ElseIf P50 = False Then 'Enter P50 probability annotation settings:
            Dim P50Value As Double = GetValue(0.5)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.5, "P50", 0.5, P50Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.5
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P50"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.5
                dgvAnnot.Rows(RowNo).Cells(6).Value = P50Value
            End If
            DisplayAnnot(0.5, P50Value, "P50")
        ElseIf P90 = False Then 'Enter P90 probability annotation settings:
            Dim P90Value As Double = GetValue(0.9)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.9, "P90", 0.9, P90Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.9
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P90"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.9
                dgvAnnot.Rows(RowNo).Cells(6).Value = P90Value
            End If
            DisplayAnnot(0.9, P90Value, "P90")
        ElseIf P1 = False Then 'Enter P1 probability annotation settings:
            Dim P1Value As Double = GetValue(0.01)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.01, "P1", 0.01, P1Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.01
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P1"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.01
                dgvAnnot.Rows(RowNo).Cells(6).Value = P1Value
            End If
            DisplayAnnot(0.01, P1Value, "P1")
        ElseIf P99 = False Then 'Enter P99 probability annotation settings:
            Dim P99Value As Double = GetValue(0.99)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Probability", 0.99, "P99", 0.99, P99Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Probability"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0.99
                dgvAnnot.Rows(RowNo).Cells(4).Value = "P99"
                dgvAnnot.Rows(RowNo).Cells(5).Value = 0.99
                dgvAnnot.Rows(RowNo).Cells(6).Value = P99Value
            End If
            DisplayAnnot(0.99, P99Value, "P99")
        Else
            'User defined probability annotation.
        End If
        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub AddMeanAnnot(ByVal RowNo As Integer)
        'Add a mean annotation entry at the specified row number.

        Dim MeanAnnotated As Boolean = False 'If True then the Mean annotation is in the list

        Dim MeanVal As Double 'The Mean value
        Dim Prob As Double 'The corresponding probability value

        dgvAnnot.AllowUserToAddRows = False

        'Check if the Mean value is already annotated:
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(2).Value = "Mean" Then MeanAnnotated = True
        Next

        If MeanAnnotated = False Then
            'MeanVal = txtAverage.Text
            MeanVal = SeriesAverage
            Prob = GetProb(MeanVal)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Mean", "", "Mean", Prob, MeanVal})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Mean"
                dgvAnnot.Rows(RowNo).Cells(3).Value = ""
                dgvAnnot.Rows(RowNo).Cells(4).Value = "Mean"
                dgvAnnot.Rows(RowNo).Cells(5).Value = Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = MeanVal
            End If
            DisplayAnnot(Prob, MeanVal, "Mean")
        Else
            'The Mean annotation is already in the list.
        End If

        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub AddValueAnnot(ByVal RowNo As Integer)
        'Add a value annotation entry at the specified row number.

        Dim Value As Double = dgvAnnot.Rows(RowNo).Cells(3).Value  'The value to be annotated
        Dim Prob As Double = GetProb(Value) 'The corresponding probability value

        dgvAnnot.AllowUserToAddRows = False
        If RowNo > dgvAnnot.RowCount - 1 Then
            dgvAnnot.Rows.Add({True, True, "Value", Prob, "Value", Prob, Value})
        Else
            dgvAnnot.Rows(RowNo).Cells(0).Value = True
            dgvAnnot.Rows(RowNo).Cells(1).Value = True
            dgvAnnot.Rows(RowNo).Cells(2).Value = "Value"
            dgvAnnot.Rows(RowNo).Cells(3).Value = Prob
            dgvAnnot.Rows(RowNo).Cells(4).Value = "Value"
            dgvAnnot.Rows(RowNo).Cells(5).Value = Prob
            dgvAnnot.Rows(RowNo).Cells(6).Value = Value
        End If

        dgvAnnot.AllowUserToAddRows = True
    End Sub

    Private Sub AddStdDevAnnot(ByVal RowNo As Integer)
        'Add a standard deviation annotation entry at the specified row number.

        dgvAnnot.AllowUserToAddRows = False

        Dim SDev1 As Boolean = False 'Corresonds to a Standard Deviation parameter of 1
        Dim SDevN1 As Boolean = False 'Corresonds to a Standard Deviation parameter of -1
        Dim SDev2 As Boolean = False 'Corresonds to a Standard Deviation parameter of 2
        Dim SDevN2 As Boolean = False 'Corresonds to a Standard Deviation parameter of -2
        Dim SDev3 As Boolean = False 'Corresonds to a Standard Deviation parameter of 3
        Dim SDevN3 As Boolean = False 'Corresonds to a Standard Deviation parameter of -3

        'Check if any Standard Deviation values are already annotated:
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(2).Value = "Standard Deviation" Then
                If dgvAnnot.Rows(I).Cells(3).Value = 1 Then SDev1 = True
                If dgvAnnot.Rows(I).Cells(3).Value = -1 Then SDevN1 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 2 Then SDev2 = True
                If dgvAnnot.Rows(I).Cells(3).Value = -2 Then SDevN2 = True
                If dgvAnnot.Rows(I).Cells(3).Value = 3 Then SDev3 = True
                If dgvAnnot.Rows(I).Cells(3).Value = -3 Then SDevN3 = True
            End If
        Next

        Dim StdDev As Double = txtStdDev.Text 'The Standard Deviation of the series
        'Dim Mean As Double = txtAverage.Text ' The Mean value of the series
        Dim Mean As Double = SeriesAverage ' The Mean value of the series

        If SDev1 = False Then
            Dim SDev1Value As Double = Mean + StdDev
            Dim SDev1Prob As Double = GetProb(SDev1Value)
            Dim Label As String = "1" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 1, Label, SDev1Prob, SDev1Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 1
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev1Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDev1Value
            End If
            DisplayAnnot(SDev1Prob, SDev1Value, Label)
        ElseIf SDevN1 = False Then
            Dim SDevN1Value As Double = Mean - StdDev
            Dim SDevN1Prob As Double = GetProb(SDevN1Value)
            Dim Label As String = "-1" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", -1, Label, SDevN1Prob, SDevN1Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 1
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDevN1Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDevN1Value
            End If
            DisplayAnnot(SDevN1Prob, SDevN1Value, Label)
        ElseIf SDev2 = False Then
            Dim SDev2Value As Double = Mean + 2 * StdDev
            Dim SDev2Prob As Double = GetProb(SDev2Value)
            Dim Label As String = "2" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 2, Label, SDev2Prob, SDev2Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 2
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev2Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDev2Value
            End If
            DisplayAnnot(SDev2Prob, SDev2Value, Label)
        ElseIf SDevN2 = False Then
            Dim SDevN2Value As Double = Mean - 2 * StdDev
            Dim SDevN2Prob As Double = GetProb(SDevN2Value)
            Dim Label As String = "-2" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", -2, Label, SDevN2Prob, SDevN2Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = -2
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDevN2Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDevN2Value
            End If
            DisplayAnnot(SDevN2Prob, SDevN2Value, Label)
        ElseIf SDev3 = False Then
            Dim SDev3Value As Double = Mean + 3 * StdDev
            Dim SDev3Prob As Double = GetProb(SDev3Value)
            Dim Label As String = "3" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 3, Label, SDev3Prob, SDev3Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 3
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev3Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDev3Value
            End If
            DisplayAnnot(SDev3Prob, SDev3Value, Label)
        ElseIf SDevN3 = False Then
            Dim SDevN3Value As Double = Mean - 3 * StdDev
            Dim SDevN3Prob As Double = GetProb(SDevN3Value)
            Dim Label As String = "-3" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", -3, Label, SDevN3Prob, SDevN3Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = -3
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDevN3Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDevN3Value
            End If
            DisplayAnnot(SDevN3Prob, SDevN3Value, Label)
        Else
            Dim SDev0Value As Double = Mean
            Dim SDev0Prob As Double = GetProb(SDev0Value)
            Dim Label As String = "0" & ChrW(963)
            If RowNo > dgvAnnot.RowCount - 1 Then
                dgvAnnot.Rows.Add({True, True, "Standard Deviation", 0, Label, SDev0Prob, SDev0Value})
            Else
                dgvAnnot.Rows(RowNo).Cells(0).Value = True
                dgvAnnot.Rows(RowNo).Cells(1).Value = True
                dgvAnnot.Rows(RowNo).Cells(2).Value = "Standard Deviation"
                dgvAnnot.Rows(RowNo).Cells(3).Value = 0
                dgvAnnot.Rows(RowNo).Cells(4).Value = Label
                dgvAnnot.Rows(RowNo).Cells(5).Value = SDev0Prob
                dgvAnnot.Rows(RowNo).Cells(6).Value = SDev0Value
            End If
            DisplayAnnot(SDev0Prob, SDev0Value, Label)
        End If

        dgvAnnot.AllowUserToAddRows = True
    End Sub

    'NOTE: This method is not required! UpdateAnnotationValues is called before the annotation is drawn.
    Private Sub UpdateAnnotValues()
        'Update the annotation values using the probabilities.

        Dim NRows As Integer = dgvAnnot.RowCount - 1
        Dim I As Integer
        Dim PValue As Double
        For I = 1 To NRows
            PValue = dgvAnnot.Rows(I - 1).Cells(5).Value
            dgvAnnot.Rows(I - 1).Cells(6).Value = GetValue(PValue)
        Next
    End Sub

    Private Sub DisplayAnnot(ByVal Prob As Double, ByVal Value As Double, ByVal Text As String)
        'Display the probability annotation on the Series Analysis charts.

        'Display the annotation on the CDF chart: ========================================================

        'Add the vertical bar:
        Dim CDFPoint As New DataVisualization.Charting.DataPoint
        CDFPoint.XValue = Value
        CDFPoint.SetValueY(Prob)
        Chart1.Series("CdfVertBar").Points.Add(CDFPoint)

        'Add the label:
        Dim Annot As New DataVisualization.Charting.TextAnnotation
        Annot.AxisX = Chart1.ChartAreas(0).AxisX
        Annot.AxisY = Chart1.ChartAreas(0).AxisY
        Annot.AnchorX = Value
        Annot.AnchorY = Prob

        Annot.AnchorAlignment = ContentAlignment.MiddleRight

        Annot.Text = Text
        Annot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)

        Chart1.Annotations.Add(Annot)

        'Display the annotation on the Histogram: ====================================================

        Dim HistPoint As New DataVisualization.Charting.DataPoint
        HistPoint.XValue = Value
        Dim YValue As Double = GetHistProb(Value)
        HistPoint.SetValueY(YValue)
        Chart1.Series("HistPoints").Points.Add(HistPoint)

    End Sub

    Private Function GetHistProb(ByVal Value As Double, AdjacentBars As Integer) As Double
        'Get the histogram probability corresponding to the value.
        'If AdjacentBars > 0, the maximum value including the specified number of adjacent bars returned.
        If IsDiscrete Then
            If AdjacentBars = 0 Then
                Dim Row1 As DataRow = Data.Tables("Histogram").Select("Value <= '" & Value & "'").Last
                Dim Row2 As DataRow = Data.Tables("Histogram").Select("Value >= '" & Value & "'").First
                If Row1.Item("Probability") > Row2.Item("Probability") Then
                    Return Row1.Item("Probability")
                Else
                    Return Row2.Item("Probability")
                End If
            Else
                Dim Row1 As IEnumerable(Of DataRow) = Data.Tables("Histogram").Select("Value <= '" & Value & "'", "Value DESC").Take(AdjacentBars + 1)
                Dim Row2 As IEnumerable(Of DataRow) = Data.Tables("Histogram").Select("Value >= '" & Value & "'", "Value ASC").Take(AdjacentBars + 1)
                If Row1(AdjacentBars).Item("Probability") > Row2(AdjacentBars).Item("Probability") Then
                    Return Row1(AdjacentBars).Item("Probability")
                Else
                    Return Row2(AdjacentBars).Item("Probability")
                End If
            End If
        Else
            If AdjacentBars = 0 Then
                'Dim Row1 As DataRow = Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'").Last
                Dim Row1 As DataRow = Data.Tables("Histogram").Select("Value <= '" & Value & "'").Last 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                'Dim Row2 As DataRow = Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'").First
                Dim Row2 As DataRow = Data.Tables("Histogram").Select("Value >= '" & Value & "'").First 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                If Row1.Item("Probability") > Row2.Item("Probability") Then
                    Return Row1.Item("Probability")
                Else
                    Return Row2.Item("Probability")
                End If
            Else
                'Dim Row1 As IEnumerable(Of DataRow) = Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'", "Mid_Interval DESC").Take(AdjacentBars + 1)
                Dim Row1 As IEnumerable(Of DataRow) = Data.Tables("Histogram").Select("Value <= '" & Value & "'", "Value DESC").Take(AdjacentBars + 1) 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                'Dim Row2 As IEnumerable(Of DataRow) = Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'", "Mid_Interval ASC").Take(AdjacentBars + 1)
                Dim Row2 As IEnumerable(Of DataRow) = Data.Tables("Histogram").Select("Value >= '" & Value & "'", "Value ASC").Take(AdjacentBars + 1) 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                If Row1(AdjacentBars).Item("Probability") > Row2(AdjacentBars).Item("Probability") Then
                    Return Row1(AdjacentBars).Item("Probability")
                Else
                    Return Row2(AdjacentBars).Item("Probability")
                End If
            End If
        End If

    End Function

    Private Function GetHistProb(ByVal Value As Double) As Double
        'Get the histogram probability corresponding to the value.
        Try
            If IsDiscrete Then
                If XAxisValues = "SurveyCount" Then
                    Dim Row1 As DataRow = Data.Tables("Histogram").Select("Value <= '" & Value & "'").Last
                    Dim Row2 As DataRow = Data.Tables("Histogram").Select("Value >= '" & Value & "'").First

                    If Math.Abs(Value - Row1.Item("Value")) < Math.Abs(Value - Row2.Item("Value")) Then 'Row1 is closer to Value
                        Return Row1.Item("Probability")
                    Else 'Row2 is closer to Value
                        Return Row2.Item("Probability")
                    End If
                Else
                    Dim Row1 As DataRow = Data.Tables("Histogram").Select("Survey_Prob <= '" & Value & "'").Last
                    Dim Row2 As DataRow = Data.Tables("Histogram").Select("Survey_Prob >= '" & Value & "'").First

                    If Math.Abs(Value - Row1.Item("Survey_Prob")) < Math.Abs(Value - Row2.Item("Survey_Prob")) Then 'Row1 is closer to Value
                        Return Row1.Item("Probability")
                    Else 'Row2 is closer to Value
                        Return Row2.Item("Probability")
                    End If
                End If

            Else
                'Dim Row1 As DataRow = Data.Tables("Histogram").Select("Mid_Interval <= '" & Value & "'").Last
                Dim Row1 As DataRow = Data.Tables("Histogram").Select("Value <= '" & Value & "'").Last 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                'Dim Row2 As DataRow = Data.Tables("Histogram").Select("Mid_Interval >= '" & Value & "'").First
                Dim Row2 As DataRow = Data.Tables("Histogram").Select("Value >= '" & Value & "'").First 'UPDATE 15May22 - Use Value instead of Mid_Interval ????

                'If Math.Abs(Value - Row1.Item("Mid_Interval")) < Math.Abs(Value - Row2.Item("Mid_Interval")) Then 'Row1 is closer to Value
                If Math.Abs(Value - Row1.Item("Value")) < Math.Abs(Value - Row2.Item("Value")) Then 'Row1 is closer to Value 'UPDATE 15May22 - Use Value instead of Mid_Interval ????
                    Return Row1.Item("Probability")
                Else 'Row2 is closer to Value
                    Return Row2.Item("Probability")
                End If
            End If
        Catch ex As Exception
            Main.Message.AddWarning("Error getting the histogram probability corresponding to value: " & Value & vbCrLf & ex.Message & vbCrLf)
        End Try
    End Function


    Private Sub dgvAnnot_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvAnnot.EditingControlShowing

        'If dgvAnnot.CurrentCell.ColumnIndex = 0 Then 'Annotation Type selected
        If dgvAnnot.CurrentCell.ColumnIndex = 2 Then 'Annotation Type selected
            Dim combo As ComboBox = CType(e.Control, ComboBox)
            If (combo IsNot Nothing) Then
                combo.Name = "cboAnnotType"
                'Remove current handler:
                RemoveHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
                'Add the event handler:
                AddHandler combo.SelectionChangeCommitted, New EventHandler(AddressOf ComboBox_SelectionChangeCommitted)
            End If
        End If
    End Sub

    Private Sub ComboBox_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim combo As ComboBox = CType(sender, ComboBox)

        If combo.Name = "cboAnnotType" Then
            Main.Message.Add("Selected annotation type: " & combo.SelectedItem.ToString & vbCrLf)

            Dim RowNo As Integer = dgvAnnot.SelectedCells(0).RowIndex
            Main.Message.Add("Selected row: " & RowNo & vbCrLf)

            Select Case combo.SelectedItem.ToString
                Case "Probability"
                    AddProbAnnot(RowNo)
                Case "Value"
                    AddValueAnnot(RowNo)
                Case "Mean"
                    AddMeanAnnot(RowNo)
                Case "Standard Deviation"
                    AddStdDevAnnot(RowNo)
                Case Else

            End Select
        Else
            Main.Message.AddWarning("Unknown combo box: " & combo.Name & vbCrLf)
        End If
    End Sub

    Private Sub btnDeleteAnnot_Click(sender As Object, e As EventArgs) Handles btnDeleteAnnot.Click
        'Delete the selected annotation.

        If dgvAnnot.SelectedRows.Count > 0 Then
            dgvAnnot.Rows.RemoveAt(dgvAnnot.SelectedRows(0).Index)
        Else
            Main.Message.AddWarning("Select an annotation entry to delete." & vbCrLf)
        End If
    End Sub

    Private Sub btnUpdateAnnotation_Click(sender As Object, e As EventArgs) Handles btnUpdateAnnotation.Click
        'Update the Annotations.
        UpdateAnnotation()
    End Sub

    Private Sub UpdateAnnotation()
        'Update the Annotations
        Chart1.Annotations.Clear()
        Chart1.Series("CdfVertBar").Points.Clear()
        Chart1.Series("HistPoints").Points.Clear()

        UpdateAnnotationValues()

        dgvAnnot.AllowUserToAddRows = False
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            If dgvAnnot.Rows(I).Cells(0).Value = True Then 'Display annotation on the CDF chart
                'Add the vertical bar:
                Dim CdfPoint As New DataVisualization.Charting.DataPoint
                CdfPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
                CdfPoint.SetValueY(dgvAnnot.Rows(I).Cells(5).Value)
                Chart1.Series("CdfVertBar").Points.Add(CdfPoint)
                'Add the label:
                Dim CdfAnnot As New DataVisualization.Charting.TextAnnotation
                CdfAnnot.AxisX = Chart1.ChartAreas(0).AxisX
                CdfAnnot.AxisY = Chart1.ChartAreas(0).AxisY
                CdfAnnot.AnchorX = dgvAnnot.Rows(I).Cells(6).Value
                CdfAnnot.AnchorY = dgvAnnot.Rows(I).Cells(5).Value
                CdfAnnot.AnchorAlignment = ContentAlignment.MiddleRight

                'CdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(7).Value) & ")"

                If XAxisValues = "SurveyCount" Then
                    CdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(7).Value) & ")"
                Else
                    If Main.BayesSim.Settings.ProbabilityMeasure = "Percent" Then
                        CdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value * 100, dgvAnnot.Rows(I).Cells(9).Value) & "%" & ")"
                    Else
                        CdfAnnot.Text = dgvAnnot.Rows(I).Cells(4).Value & " (" & Format(dgvAnnot.Rows(I).Cells(6).Value, dgvAnnot.Rows(I).Cells(8).Value) & ")"
                    End If
                End If

                CdfAnnot.Font = New Font("Arial", 10, FontStyle.Regular Or FontStyle.Bold)
                    Chart1.Annotations.Add(CdfAnnot)
                End If
                If dgvAnnot.Rows(I).Cells(1).Value = True Then 'Display annotation on the Histogram
                'Add the circle symbol to the histogram
                Dim HistPoint As New DataVisualization.Charting.DataPoint
                HistPoint.XValue = dgvAnnot.Rows(I).Cells(6).Value
                Dim YValue As Double = GetHistProb(dgvAnnot.Rows(I).Cells(6).Value) / HistIntervalWidth 'This is the Probability Density
                HistPoint.SetValueY(YValue)
                Chart1.Series("HistPoints").Points.Add(HistPoint)

            End If
        Next

        dgvAnnot.AllowUserToAddRows = True
        'dgvAnnot.AutoResizeColumns(autoSizeColumnsMode:=DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader)
        'dgvAnnot.AutoResizeColumns()

    End Sub

    Private Sub ClearAnnot()
        'Clear the annotations.
        dgvAnnot.Rows.Clear()
    End Sub

    Private Sub ApplyDefaultAnnotSettings()
        'Apply default annotation settings: Default annotations
        'This method will run if a form settings file is not found.

        dgvAnnot.Rows.Add({True, True, "Probability", 0.05, "P5", 0.05, 0})
        'dgvAnnot.Rows.Add({True, True, "Probability", 0.1, "P10", 0.1, 0})
        dgvAnnot.Rows.Add({True, True, "Probability", 0.5, "P50", 0.5, 0})
        'dgvAnnot.Rows.Add({True, True, "Probability", 0.9, "P90", 0.9, 0})
        dgvAnnot.Rows.Add({True, True, "Probability", 0.95, "P95", 0.95, 0})
        dgvAnnot.Rows.Add({True, True, "Mean", "", "Mean", "", 0})
        'ResetAnnotFormats()

    End Sub

    Private Sub UpdateAnnotationValues()
        'Update the annotation values using the current CDF and Histogram data.

        dgvAnnot.AllowUserToAddRows = False
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            Select Case dgvAnnot.Rows(I).Cells(2).Value
                Case "Probability"
                    dgvAnnot.Rows(I).Cells(5).Value = dgvAnnot.Rows(I).Cells(3).Value
                    dgvAnnot.Rows(I).Cells(6).Value = GetValue(dgvAnnot.Rows(I).Cells(5).Value)
                Case "Value"
                    dgvAnnot.Rows(I).Cells(6).Value = dgvAnnot.Rows(I).Cells(3).Value
                    dgvAnnot.Rows(I).Cells(5).Value = GetProb(dgvAnnot.Rows(I).Cells(6).Value)
                Case "Mean"
                    dgvAnnot.Rows(I).Cells(3).Value = ""
                    'dgvAnnot.Rows(I).Cells(6).Value = Val(txtAverage.Text)
                    dgvAnnot.Rows(I).Cells(6).Value = SeriesAverage
                    'dgvAnnot.Rows(I).Cells(5).Value = GetProb(txtAverage.Text)
                    dgvAnnot.Rows(I).Cells(5).Value = GetProb(SeriesAverage)
                Case "Standard Deviation"
                    'dgvAnnot.Rows(I).Cells(6).Value = txtAverage.Text + dgvAnnot.Rows(I).Cells(3).Value * txtStdDev.Text
                    'dgvAnnot.Rows(I).Cells(6).Value = txtAverage.Text + dgvAnnot.Rows(I).Cells(3).Value * SeriesStdDev
                    dgvAnnot.Rows(I).Cells(6).Value = SeriesAverage + dgvAnnot.Rows(I).Cells(3).Value * SeriesStdDev
                    dgvAnnot.Rows(I).Cells(5).Value = GetProb(dgvAnnot.Rows(I).Cells(6).Value)
                Case Else
                    Main.Message.AddWarning("Unknown annotation type: " & dgvAnnot.Rows(I).Cells(2).Value & vbCrLf)
            End Select
        Next
        dgvAnnot.AllowUserToAddRows = True
        'dgvAnnot.AutoResizeColumns(autoSizeColumnsMode:=DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader)
        'dgvAnnot.AutoResizeColumns()
    End Sub

    Private Sub frmSeriesAnalysis_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        txtHeight.Text = Me.Height
        txtWidth.Text = Me.Width
    End Sub

    Private Sub frmSeriesAnalysis_Move(sender As Object, e As EventArgs) Handles Me.Move
        txtTop.Text = Me.Top
        txtLeft.Text = Me.Left
    End Sub

    Private Sub btnApplySize_Click(sender As Object, e As EventArgs) Handles btnApplySize.Click
        'Apply the Height and Width settings.

        Dim Height As Integer = Int(txtHeight.Text)
        Dim Width As Integer = Int(txtWidth.Text)
        Dim Top As Integer = Int(txtTop.Text)
        Dim Left As Integer = Int(txtLeft.Text)

        If Height < 200 Then Height = 200 '200 pixels minimum height
        If Width < 200 Then Width = 200 '200 pixels minimum width

        Me.Height = Height
        Me.Width = Width
        Me.Top = Top
        Me.Left = Left
        CheckFormPos()
    End Sub

    Private Sub rbCdf_CheckedChanged(sender As Object, e As EventArgs) Handles rbCdf.CheckedChanged
        If rbCdf.Checked Then _showReverseCDF = False
    End Sub

    Private Sub rbReverseCdf_CheckedChanged(sender As Object, e As EventArgs) Handles rbReverseCdf.CheckedChanged
        If rbReverseCdf.Checked Then _showReverseCDF = True
    End Sub

    Private Sub btnUpdateCharts_Click(sender As Object, e As EventArgs) Handles btnUpdateCharts.Click
        'Update the charts.
        UpdateCharts()
    End Sub

    Private Sub btnFormatHelp2_Click(sender As Object, e As EventArgs) Handles btnFormatHelp2.Click
        'Show Format information.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub btnPlot_Click(sender As Object, e As EventArgs) Handles btnPlot.Click
        'UpdateCharts()
        ReplotCharts()
    End Sub

    Private Sub frmSeriesAnalysis_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub

    Private Sub chkShowModel_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowModel.CheckedChanged

    End Sub

    Private Sub btnUpdateCharts2_Click(sender As Object, e As EventArgs) Handles btnUpdateCharts2.Click
        UpdateCharts()
    End Sub

    Private Sub btnChartTitleFont_Click(sender As Object, e As EventArgs) Handles btnChartTitleFont.Click
        'Edit chart title font
        FontDialog1.Font = txtAddTitle.Font
        FontDialog1.ShowDialog()
        txtAddTitle.Font = FontDialog1.Font
    End Sub

    Private Sub btnChartTitleColor_Click(sender As Object, e As EventArgs) Handles btnChartTitleColor.Click
        ColorDialog1.Color = txtAddTitle.ForeColor
        ColorDialog1.ShowDialog()
        txtAddTitle.ForeColor = ColorDialog1.Color
    End Sub

    Private Sub btnAddTitle_Click(sender As Object, e As EventArgs) Handles btnAddTitle.Click
        'Add a new title to Chart1

        Dim NewTitleNo As Integer = Chart1.Titles.Count
        Dim NewTitleName As String = "Title" & NewTitleNo + 1
        Chart1.Titles.Add(NewTitleName)

        Chart1.Titles(NewTitleNo).Text = txtAddTitle.Text
        Chart1.Titles(NewTitleNo).Font = txtAddTitle.Font
        Chart1.Titles(NewTitleNo).ForeColor = txtAddTitle.ForeColor

    End Sub

    Private Sub btnEditFont_Click(sender As Object, e As EventArgs) Handles btnEditFont.Click
        'Edit chart title font
        FontDialog1.Font = txtEditTitle.Font
        FontDialog1.ShowDialog()
        txtEditTitle.Font = FontDialog1.Font
    End Sub

    Private Sub btnEditColor_Click(sender As Object, e As EventArgs) Handles btnEditColor.Click
        ColorDialog1.Color = txtEditTitle.ForeColor
        ColorDialog1.ShowDialog()
        txtEditTitle.ForeColor = ColorDialog1.Color
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged

        Dim TitleNo As Integer = NumericUpDown1.Value - 1

        If TitleNo < 0 Then
            Main.Message.AddWarning("The selected title number is too low." & vbCrLf)
        ElseIf TitleNo > Chart1.Titles.Count - 1 Then
            Main.Message.AddWarning("The selected title number is too high." & vbCrLf)
        Else
            txtEditTitle.Text = Chart1.Titles(TitleNo).Text
            txtEditTitle.Font = Chart1.Titles(TitleNo).Font
            txtEditTitle.ForeColor = Chart1.Titles(TitleNo).ForeColor
        End If
    End Sub

    Private Sub btnEditTitle_Click(sender As Object, e As EventArgs) Handles btnEditTitle.Click

        Dim TitleNo As Integer = NumericUpDown1.Value - 1

        If TitleNo < 0 Then
            Main.Message.AddWarning("The selected title number is too low." & vbCrLf)
        ElseIf TitleNo > Chart1.Titles.Count - 1 Then
            Main.Message.AddWarning("The selected title number is too high." & vbCrLf)
        Else
            Chart1.Titles(TitleNo).Text = txtEditTitle.Text
            Chart1.Titles(TitleNo).Font = txtEditTitle.Font
            Chart1.Titles(TitleNo).ForeColor = txtEditTitle.ForeColor
        End If
    End Sub

    Private Sub rbSurveyCount_CheckedChanged(sender As Object, e As EventArgs) Handles rbSurveyCount.CheckedChanged
        If rbSurveyCount.Focused Then
            If rbSurveyCount.Checked Then
                XAxisValues = "SurveyCount"
                'Main.Message.Add("ShowSeriesStats()" & vbCrLf)
                ShowSeriesStats()
                'Main.Message.Add("PlotDiscreteCharts()" & vbCrLf)
                PlotDiscreteCharts()
                'Main.Message.Add("UpdateAnnotation()" & vbCrLf)
                UpdateAnnotation()
                lblStats1.Text = "Survey sample count statistics."
            End If
        End If
    End Sub

    Private Sub rbSurveyProb_CheckedChanged(sender As Object, e As EventArgs) Handles rbSurveyProb.CheckedChanged
        If rbSurveyProb.Focused Then
            If rbSurveyProb.Checked Then
                XAxisValues = "SurveyProb"
                'ShowSurveyProbStats() 'This upsets the chart dispay!
                'UpdateAnnotValues()
                ShowSurveyProbStats()
                PlotDiscreteCharts()
                'ShowSurveyProbStats()
                UpdateAnnotation()
                If Main.BayesSim.Settings.ProbabilityMeasure = "Percent" Then
                    lblStats1.Text = "Survey sample count statistics converted to percentage probabilities."
                Else
                    lblStats1.Text = "Survey sample count statistics converted to decimal probabilities."
                End If

            End If
        End If
    End Sub

    Private Sub btnCalcFactorial_Click(sender As Object, e As EventArgs) Handles btnCalcFactorial.Click
        'Calculate Factorial(NTrials)
        Dim NTrials As Integer = txtNTrials.Text
        'Dim FactNTrials As ULong = 1
        Dim FactNTrials As Double = 1
        Dim I As Integer
        Try
            For I = 1 To NTrials
                FactNTrials *= I
            Next
            txtFactNTrials.Text = FactNTrials
        Catch ex As Exception
            Main.Message.AddWarning("Error calculating factorial at I = " & I & vbCrLf)
            Main.Message.AddWarning(ex.Message & vbCrLf)
        End Try

    End Sub

    Private Sub btnResetStatFmt_Click(sender As Object, e As EventArgs) Handles btnResetStatFmt.Click
        'Reset the Statistics format strings.
        ResetStatFormats()

        'txtMinFormat.Text = Main.Bayes.Settings.SamplesFormat
        'txtMaxFormat.Text = Main.Bayes.Settings.SamplesFormat
        'txtSumFormat.Text = Main.Bayes.Settings.SamplesFormat
        'txtAvgFormat.Text = Main.Bayes.Settings.SamplesFormat
        'txtStdDevFormat.Text = Main.Bayes.Settings.SamplesFormat
        'txtVarFormat.Text = Main.Bayes.Settings.SamplesFormat

        'txtMinDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        'txtMaxDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        'txtSumDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        'txtAvgDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        'txtStdDevDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        'txtVarDecFormat.Text = Main.Bayes.Settings.DecimalFormat

        'txtMinPctFormat.Text = Main.Bayes.Settings.PercentFormat
        'txtMaxPctFormat.Text = Main.Bayes.Settings.PercentFormat
        'txtSumPctFormat.Text = Main.Bayes.Settings.PercentFormat
        'txtAvgPctFormat.Text = Main.Bayes.Settings.PercentFormat
        'txtStdDevPctFormat.Text = Main.Bayes.Settings.PercentFormat
        'txtVarPctFormat.Text = Main.Bayes.Settings.PercentFormat

    End Sub

    Private Sub ResetStatFormats()
        'Reset the Statistics format strings.

        txtMinFormat.Text = Main.Bayes.Settings.SamplesFormat
        txtMaxFormat.Text = Main.Bayes.Settings.SamplesFormat
        txtSumFormat.Text = Main.Bayes.Settings.SamplesFormat
        txtAvgFormat.Text = Main.Bayes.Settings.SamplesFormat
        txtStdDevFormat.Text = Main.Bayes.Settings.SamplesFormat
        txtVarFormat.Text = Main.Bayes.Settings.SamplesFormat

        txtMinDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        txtMaxDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        txtSumDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        txtAvgDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        txtStdDevDecFormat.Text = Main.Bayes.Settings.DecimalFormat
        txtVarDecFormat.Text = Main.Bayes.Settings.DecimalFormat

        txtMinPctFormat.Text = Main.Bayes.Settings.PercentFormat
        txtMaxPctFormat.Text = Main.Bayes.Settings.PercentFormat
        txtSumPctFormat.Text = Main.Bayes.Settings.PercentFormat
        txtAvgPctFormat.Text = Main.Bayes.Settings.PercentFormat
        txtStdDevPctFormat.Text = Main.Bayes.Settings.PercentFormat
        txtVarPctFormat.Text = Main.Bayes.Settings.PercentFormat
    End Sub


    Private Sub btnResetAnnotFmt_Click(sender As Object, e As EventArgs) Handles btnResetAnnotFmt.Click
        'Reset the annotation format strings.
        ResetAnnotFormats()
        'dgvAnnot.AllowUserToAddRows = False
        'Dim I As Integer
        'For I = 0 To dgvAnnot.RowCount - 1
        '    dgvAnnot.Rows(I).Cells(7).Value = Main.Bayes.Settings.SamplesFormat
        '    dgvAnnot.Rows(I).Cells(8).Value = Main.Bayes.Settings.DecimalFormat
        '    dgvAnnot.Rows(I).Cells(9).Value = Main.Bayes.Settings.PercentFormat
        'Next
        'dgvAnnot.AllowUserToAddRows = True
        ''dgvAnnot.AutoResizeColumns()
    End Sub

    Private Sub ResetAnnotFormats()
        'Reset the annotation format strings.

        dgvAnnot.AllowUserToAddRows = False
        Dim I As Integer
        For I = 0 To dgvAnnot.RowCount - 1
            dgvAnnot.Rows(I).Cells(7).Value = Main.Bayes.Settings.SamplesFormat
            dgvAnnot.Rows(I).Cells(8).Value = Main.Bayes.Settings.DecimalFormat
            dgvAnnot.Rows(I).Cells(9).Value = Main.Bayes.Settings.PercentFormat
        Next
        dgvAnnot.AllowUserToAddRows = True
        'dgvAnnot.AutoResizeColumns()

    End Sub

    Private Sub btnDefault_Click(sender As Object, e As EventArgs) Handles btnDefault.Click
        ClearAnnot()
        ApplyDefaultAnnotSettings()
        ResetAnnotFormats()
        UpdateAnnotation()
    End Sub

    Private Sub cmbSourceDataTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSourceDataTable.SelectedIndexChanged

    End Sub

    Private Sub btnCopyChart_Click(sender As Object, e As EventArgs) Handles btnCopyChart.Click
        'Copy the chart to the clipboard.

        Dim myStream As New System.IO.MemoryStream()

        Select Case cmbImageFormat.SelectedItem.ToString
            Case "Jpeg"
                Chart1.SaveImage(myStream, DataVisualization.Charting.ChartImageFormat.Jpeg)
            Case "Png"
                Chart1.SaveImage(myStream, DataVisualization.Charting.ChartImageFormat.Png)
            Case "Bmp"
                Chart1.SaveImage(myStream, DataVisualization.Charting.ChartImageFormat.Bmp)
            Case "Gif"
                Chart1.SaveImage(myStream, DataVisualization.Charting.ChartImageFormat.Gif)
            Case "Tiff"
                Chart1.SaveImage(myStream, DataVisualization.Charting.ChartImageFormat.Tiff)
            Case Else
                Main.Message.AddWarning("Unknown image format: " & cmbImageFormat.SelectedItem.ToString & vbCrLf)
                Main.Message.AddWarning("Jpeg format will be used." & vbCrLf)
                Chart1.SaveImage(myStream, DataVisualization.Charting.ChartImageFormat.Jpeg)
        End Select
        Dim ChartPic As New Bitmap(myStream)
        Clipboard.SetDataObject(ChartPic)
    End Sub

    Private Sub btnDeleteAll_Click(sender As Object, e As EventArgs) Handles btnDeleteAll.Click
        dgvAnnot.Rows.Clear()
    End Sub

    Private Sub btnAddMean_Click(sender As Object, e As EventArgs) Handles btnAddMean.Click
        'Add the Mean Annotation to the list.
        dgvAnnot.Rows.Add(True, True, "Mean", "", "Mean", GetProb(SeriesAverage), SeriesAverage)
    End Sub

    Private Sub AddP50_Click(sender As Object, e As EventArgs) Handles AddP50.Click
        'Add the P50 Annotation to the list.
        dgvAnnot.Rows.Add(True, True, "Probability", 0.5, "P50", 0.5, GetValue(0.5))
    End Sub

    Private Sub Add90PctConfid_Click(sender As Object, e As EventArgs) Handles Add90PctConfid.Click
        'Add the 90% confidence interval Annotation to the list.
        dgvAnnot.Rows.Add(True, True, "Probability", 0.05, "P05", 0.05, GetValue(0.05))
        dgvAnnot.Rows.Add(True, True, "Probability", 0.95, "P95", 0.95, GetValue(0.95))
    End Sub

    Private Sub btn95PctConfid_Click(sender As Object, e As EventArgs) Handles btn95PctConfid.Click
        'Add the 95% confidence interval Annotation to the list.
        dgvAnnot.Rows.Add(True, True, "Probability", 0.025, "P02.5", 0.025, GetValue(0.025))
        dgvAnnot.Rows.Add(True, True, "Probability", 0.975, "P97.5", 0.975, GetValue(0.975))
    End Sub

    Private Sub btn99PctConfid_Click(sender As Object, e As EventArgs) Handles btn99PctConfid.Click
        'Add the 99% confidence interval Annotation to the list.
        dgvAnnot.Rows.Add(True, True, "Probability", 0.005, "P00.5", 0.005, GetValue(0.005))
        dgvAnnot.Rows.Add(True, True, "Probability", 0.995, "P99.5", 0.995, GetValue(0.995))
    End Sub




#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


    'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class