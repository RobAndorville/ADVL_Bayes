Public Class clsBayesSim
    'The Bayes Simulation class simulates a survey using specified event probabilities used in a Bayesian model.
    'By repeating the survey simulation, the uncertainty in event probabilities estimated from the survey data can be determined.

#Region " Variable Declarations - All the variables and class objects used in this class." '===================================================================================================
    Public Data As New DataSet  'The simulation results are stored in the table named 'Simulation'.

    Public WithEvents Settings As New clsSimSettings

    Public WithEvents Diagram As New clsSimDiagram

    Public AnnotTitle As New clsSimLabel 'Simulation Diagram Title

    Public AreaNotAandNotB As New AreaInfo 'The Area displayed on the SImulation Diagram representing Not A and Not B
    Public AreaNotAandB As New AreaInfo    'The Area displayed on the SImulation Diagram representing Not A and B
    Public AreaAandB As New AreaInfo       'The Area displayed on the SImulation Diagram representing A and B
    Public AreaAandNotB As New AreaInfo    'The Area displayed on the SImulation Diagram representing A and Not B

    Public ProbALabel As New clsSimLabel           'The Probability of A label
    Public ProbBLabel As New clsSimLabel           'The Probability of B label
    Public ProbAandBLabel As New clsSimLabel       'The Probability of A and B label
    Public ProbAandNotBLabel As New clsSimLabel    'The Probability of A and Not B label
    Public ProbNotAandBLabel As New clsSimLabel    'The Probability of Not A and B label
    Public ProbNotAandNotBLabel As New clsSimLabel 'The Probability of Not A and Not B label



#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties" '========================================================================================================================================================================



    'Bayes Simulation Properties: -----------------------------------------------------------------------------------
    Private _simProbAandNotBMean As Single = 0 'The Simulated Probability of Event - Mean value
    Property SimProbAandNotBMean As Single
        Get
            Return _simProbAandNotBMean
        End Get
        Set(value As Single)
            _simProbAandNotBMean = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbAandNotBMean As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbAandNotBMean * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbAandNotBMean, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbAandNotBStdDev As Single = 0 'The Simulated Probability of Event - Standard Deviation value
    Property SimProbAandNotBStdDev As Single
        Get
            Return _simProbAandNotBStdDev
        End Get
        Set(value As Single)
            _simProbAandNotBStdDev = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbAandNotBStdDev As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbAandNotBStdDev * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbAandNotBStdDev, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbAMean As Single = 0 'The Simulated Probability of Event - Mean value
    Property SimProbAMean As Single
        Get
            Return _simProbAMean
        End Get
        Set(value As Single)
            _simProbAMean = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbAMean As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbAMean * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbAMean, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbAStdDev As Single = 0 'The Simulated Probability of Event - Standard Deviation value
    Property SimProbAStdDev As Single
        Get
            Return _simProbAStdDev
        End Get
        Set(value As Single)
            _simProbAStdDev = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbAStdDev As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbAStdDev * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbAStdDev, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbAandBMean As Single = 0 'The Simulated Probability of Event - Mean value
    Property SimProbAandBMean As Single
        Get
            Return _simProbAandBMean
        End Get
        Set(value As Single)
            _simProbAandBMean = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbAandBMean As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbAandBMean * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbAandBMean, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbAandBStdDev As Single = 0 'The Simulated Probability of Event - Standard Deviation value
    Property SimProbAandBStdDev As Single
        Get
            Return _simProbAandBStdDev
        End Get
        Set(value As Single)
            _simProbAandBStdDev = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbAandBStdDev As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbAandBStdDev * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbAandBStdDev, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbBMean As Single = 0 'The Simulated Probability of Event - Mean value
    Property SimProbBMean As Single
        Get
            Return _simProbBMean
        End Get
        Set(value As Single)
            _simProbBMean = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbBMean As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbBMean * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbBMean, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbBStdDev As Single = 0 'The Simulated Probability of Event - Standard Deviation value
    Property SimProbBStdDev As Single
        Get
            Return _simProbBStdDev
        End Get
        Set(value As Single)
            _simProbBStdDev = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbBStdDev As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbBStdDev * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbBStdDev, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbNotAandBMean As Single = 0 'The Simulated Probability of Event - Mean value
    Property SimProbNotAandBMean As Single
        Get
            Return _simProbNotAandBMean
        End Get
        Set(value As Single)
            _simProbNotAandBMean = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbNotAandBMean As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbNotAandBMean * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbNotAandBMean, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbNotAandBStdDev As Single = 0 'The Simulated Probability of Event - Standard Deviation value
    Property SimProbNotAandBStdDev As Single
        Get
            Return _simProbNotAandBStdDev
        End Get
        Set(value As Single)
            _simProbNotAandBStdDev = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbNotAandBStdDev As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbNotAandBStdDev * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbNotAandBStdDev, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbNotAandNotBMean As Single = 0 'The Simulated Probability of Event - Mean value
    Property SimProbNotAandNotBMean As Single
        Get
            Return _simProbNotAandNotBMean
        End Get
        Set(value As Single)
            _simProbNotAandNotBMean = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbNotAandNotBMean As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbNotAandNotBMean * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbNotAandNotBMean, Settings.DecimalFormat)
            End If
        End Get
    End Property

    Private _simProbNotAandNotBStdDev As Single = 0 'The Simulated Probability of Event - Standard Deviation value
    Property SimProbNotAandNotBStdDev As Single
        Get
            Return _simProbNotAandNotBStdDev
        End Get
        Set(value As Single)
            _simProbNotAandNotBStdDev = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbNotAandNotBStdDev As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbNotAandNotBStdDev * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbNotAandNotBStdDev, Settings.DecimalFormat)
            End If
        End Get
    End Property

    'Event Simulation Properties: -----------------------------------------------------------------------------------
    'Private _simProbEventMean As Single = 0 'The Simulated Probability of Event - Mean value
    Private _simProbEventMean As Double = 0 'The Simulated Probability of Event - Mean value
    Property SimProbEventMean As Double
        Get
            Return _simProbEventMean
        End Get
        Set(value As Double)
            _simProbEventMean = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbEventMean As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbEventMean * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbEventMean, Settings.DecimalFormat)
            End If
        End Get
    End Property

    'Private _simProbEventStdDev As Single = 0 'The Simulated Probability of Event - Standard Deviation value
    Private _simProbEventStdDev As Double = 0 'The Simulated Probability of Event - Standard Deviation value
    Property SimProbEventStdDev As Double
        Get
            Return _simProbEventStdDev
        End Get
        Set(value As Double)
            _simProbEventStdDev = value
        End Set
    End Property

    ReadOnly Property FormattedSimProbEventStdDev As String
        Get
            If Settings.ProbabilityMeasure = "Percent" Then
                Return Format(SimProbEventStdDev * 100, Settings.PercentFormat) & "%"
            Else
                Return Format(SimProbEventStdDev, Settings.DecimalFormat)
            End If
        End Get
    End Property

#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------





#Region " Methods" '===========================================================================================================================================================================

    Public Sub New()

        AnnotTitle.Settings = Settings
        AnnotTitle.Font = New Font("Arial", 12, FontStyle.Bold)

        AreaNotAandNotB.Settings = Settings
        AreaNotAandB.Settings = Settings
        AreaAandB.Settings = Settings
        AreaAandNotB.Settings = Settings

        ProbALabel.Settings = Settings
        ProbBLabel.Settings = Settings
        ProbAandBLabel.Settings = Settings
        ProbAandNotBLabel.Settings = Settings
        ProbNotAandBLabel.Settings = Settings
        ProbNotAandNotBLabel.Settings = Settings

        AreaNotAandNotB.FillColor = Color.LightGoldenrodYellow
        AreaNotAandNotB.LineColor = Color.Black

        AreaNotAandB.FillColor = Color.LightCyan
        AreaNotAandB.LineColor = Color.Blue

        AreaAandB.FillColor = Color.Thistle
        AreaAandB.LineColor = Color.Purple

        AreaAandNotB.FillColor = Color.MistyRose
        AreaAandNotB.LineColor = Color.Red

        Diagram.Height = 600
        Diagram.Width = 300
        'https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.colors?view=net-5.0
        Diagram.FillColor = Color.WhiteSmoke


    End Sub


    Private Sub Diagram_HeightChanged(NewHeight As Integer) Handles Diagram.HeightChanged
        'The Simulation Diagram height has changed.
        SetSimFigureHeights()
        DefaultProbAnnotPosn()
    End Sub

    Public Sub SetSimFigureHeights()
        'Adjust the Simulation Figure height coordinates.

        'Simulation Diagram: (Y axis increases downwards in a PictureBox)
        '|------------------------------ Y = 1
        '|   Survey Simulation
        '|
        '|        |----|
        '|        |    |
        '|        |    | Not A and Not B
        '|        |    |
        '|        |----|
        '|        |    |
        '|        |    | Not A and B
        '|        |    |
        '|        |----|
        '|        |    |
        '|        |    | A and B
        '|        |    |
        '|        |----|
        '|        |    |
        '|        |    | A and Not B
        '|        |    |
        '|        |----|
        '| ----------------------------- Y = Diagram.Height

        'Dim TopBorder As Integer = 24 'The size of the border between the top of the chart and the top of the diagram (in pixels)
        Dim TopBorder As Integer = 48 'The size of the border between the top of the chart and the top of the diagram (in pixels)
        'Dim BaseBorder As Integer = 8 'The size of the border between the base of the chart and the base of the diagram (in pixels)
        Dim BaseBorder As Integer = 16 'The size of the border between the base of the chart and the base of the diagram (in pixels)
        Dim ChartHeight As Integer = Diagram.Height - TopBorder - BaseBorder
        'Dim AreaGap As Integer = 1 'The vertical gap between chart areas (in pixels)
        Dim AreaGap As Integer = 2 'The vertical gap between chart areas (in pixels)

        'Scaling = Unscaled:
        'Area Not A and Not B ------------------------------------------------------------------------------------
        AreaNotAandNotB.Unscaled.YMin = TopBorder
        AreaNotAandNotB.Unscaled.YMax = AreaNotAandNotB.Unscaled.YMin + ChartHeight / 4 'Each area has height = ChartHeight / 4. Areas are drawn with a gap = AreaGap at the top and base of each area.
        'Area Not A and B ----------------------------------------------------------------------------------------
        AreaNotAandB.Unscaled.YMin = AreaNotAandNotB.Unscaled.YMax
        AreaNotAandB.Unscaled.YMax = AreaNotAandB.Unscaled.YMin + ChartHeight / 4
        'Area A and B --------------------------------------------------------------------------------------------
        AreaAandB.Unscaled.YMin = AreaNotAandB.Unscaled.YMax 'No gap above Area A and B (Area A and B outline will cover the Area Not A and B outline: to help show that B extends into A and B)
        AreaAandB.Unscaled.YMax = AreaAandB.Unscaled.YMin + ChartHeight / 4 'No gap below Area A and B (Area A and B outline will cover the Area A and Not B outline: to help show that A extends into A and B)
        'Area A and Not B ----------------------------------------------------------------------------------------
        AreaAandNotB.Unscaled.YMin = AreaAandB.Unscaled.YMax
        AreaAandNotB.Unscaled.YMax = AreaAandNotB.Unscaled.YMin + ChartHeight / 4

        AreaNotAandNotB.Unscaled.YMin += AreaGap
        AreaNotAandNotB.Unscaled.YMax -= AreaGap
        AreaNotAandB.Unscaled.YMin += AreaGap
        'Note: No gaps between the base of AreaNotAandB and AreaAandB and between the top of AreaAandNotB and AreaAandB. This allows the outline of AreaAandB to overwrite the other area boundaries.
        AreaAandNotB.Unscaled.YMax -= AreaGap


        'Scaling = ScaleA
        AreaAandNotB.ScaleA.YMax = Diagram.Height - BaseBorder  'This is set to the base of the chart.
        AreaAandNotB.ScaleA.YMin = AreaAandNotB.ScaleA.YMax - (ChartHeight / 2) * (Settings.ProbA - Settings.ProbAandB) / Settings.ProbB
        AreaAandB.ScaleA.YMax = AreaAandNotB.ScaleA.YMin
        AreaAandB.ScaleA.YMin = AreaAandB.ScaleA.YMax - (ChartHeight / 2) * Settings.ProbAandB / Settings.ProbB
        AreaNotAandB.ScaleA.YMax = AreaAandB.ScaleA.YMin
        AreaNotAandB.ScaleA.YMin = AreaNotAandB.ScaleA.YMax - (ChartHeight / 2) * (Settings.ProbB - Settings.ProbAandB) / Settings.ProbB + AreaGap
        AreaNotAandNotB.ScaleA.YMax = AreaNotAandB.ScaleA.YMin
        AreaNotAandNotB.ScaleA.YMin = TopBorder

        AreaNotAandNotB.ScaleA.YMin += AreaGap
        AreaNotAandNotB.ScaleA.YMax -= AreaGap
        AreaNotAandB.ScaleA.YMin += AreaGap
        'Note: No gaps between the base of AreaNotAandB and AreaAandB and between the top of AreaAandNotB and AreaAandB. This allows the outline of AreaAandB to overwrite the other area boundaries.
        AreaAandNotB.ScaleA.YMax -= AreaGap


        'Scaling = ScaleB
        AreaAandNotB.ScaleB.YMax = Diagram.Height - BaseBorder  'This is set to the base of the chart.
        AreaAandNotB.ScaleB.YMin = AreaAandNotB.ScaleB.YMax - (ChartHeight / 2) * (Settings.ProbA - Settings.ProbAandB) / Settings.ProbA
        AreaAandB.ScaleB.YMax = AreaAandNotB.ScaleB.YMin
        AreaAandB.ScaleB.YMin = AreaAandB.ScaleB.YMax - (ChartHeight / 2) * Settings.ProbAandB / Settings.ProbA  'Area A and B is scaled relative to Area A and Not B
        AreaNotAandB.ScaleB.YMax = AreaAandB.ScaleB.YMin
        AreaNotAandB.ScaleB.YMin = AreaNotAandB.ScaleB.YMax - (ChartHeight / 2) * (Settings.ProbB - Settings.ProbAandB) / Settings.ProbA + AreaGap
        AreaNotAandNotB.ScaleB.YMax = AreaNotAandB.ScaleB.YMin
        AreaNotAandNotB.ScaleB.YMin = TopBorder

        AreaNotAandNotB.ScaleB.YMin += AreaGap
        AreaNotAandNotB.ScaleB.YMax -= AreaGap
        AreaNotAandB.ScaleB.YMin += AreaGap
        'Note: No gaps between the base of AreaNotAandB and AreaAandB and between the top of AreaAandNotB and AreaAandB. This allows the outline of AreaAandB to overwrite the other area boundaries.
        AreaAandNotB.ScaleB.YMax -= AreaGap


        'Scaling = ScaleAB
        AreaAandNotB.ScaleAB.YMax = Diagram.Height - BaseBorder 'This is set to the base of the chart.
        AreaAandNotB.ScaleAB.YMin = AreaAandNotB.ScaleAB.YMax - ChartHeight * (Settings.ProbA - Settings.ProbAandB)
        AreaAandB.ScaleAB.YMax = AreaAandNotB.ScaleAB.YMin
        AreaAandB.ScaleAB.YMin = AreaAandB.ScaleAB.YMax - ChartHeight * Settings.ProbAandB
        If AreaAandB.ScaleAB.YMin >= AreaAandB.ScaleAB.YMax Then AreaAandB.ScaleAB.YMin = AreaAandB.ScaleAB.YMax - 1
        AreaNotAandB.ScaleAB.YMax = AreaAandB.ScaleAB.YMin
        AreaNotAandB.ScaleAB.YMin = AreaNotAandB.ScaleAB.YMax - ChartHeight * (Settings.ProbB - Settings.ProbAandB)
        AreaNotAandNotB.ScaleAB.YMax = AreaNotAandB.ScaleAB.YMin
        AreaNotAandNotB.ScaleAB.YMin = TopBorder

        AreaNotAandNotB.ScaleAB.YMin += AreaGap
        AreaNotAandNotB.ScaleAB.YMax -= AreaGap
        AreaNotAandB.ScaleAB.YMin += AreaGap
        'Note: No gaps between the base of AreaNotAandB and AreaAandB and between the top of AreaAandNotB and AreaAandB. This allows the outline of AreaAandB to overwrite the other area boundaries.
        AreaAandNotB.ScaleAB.YMax -= AreaGap

    End Sub

    Private Sub Diagram_WidthChanged(NewWidth As Integer) Handles Diagram.WidthChanged
        'The Simulation Diagram width has changed.
        'SetCentredSimFigureWidths()
        SetLeftSimFigureWidths()
        DefaultTitlePosition()
        DefaultProbAnnotPosn()
    End Sub

    Public Sub SetCentredSimFigureWidths()
        'Adjust the Simulation Figure width coordinates.

        Dim ChartXMin As Integer
        Dim ChartXMax As Integer

        'The ChartXMin and ChartXMax values will place the chart in the middle of the diagram. The width of the chart will be NewWidth pixels.
        'ChartXMin = Diagram.Width / 2 - NewWidth / 2
        'ChartXMax = Diagram.Width / 2 + NewWidth / 2
        'ChartXMin = Diagram.Width / 2 - Diagram.Width / 2
        ChartXMin = Diagram.Width / 2 - Diagram.Width / 4
        'ChartXMax = Diagram.Width / 2 + Diagram.Width / 2
        ChartXMax = Diagram.Width / 2 + Diagram.Width / 4

        AreaNotAandNotB.Unscaled.XMin = ChartXMin
        AreaNotAandNotB.ScaleA.XMin = ChartXMin
        AreaNotAandNotB.ScaleB.XMin = ChartXMin
        AreaNotAandNotB.ScaleAB.XMin = ChartXMin

        AreaNotAandNotB.Unscaled.XMax = ChartXMax
        AreaNotAandNotB.ScaleA.XMax = ChartXMax
        AreaNotAandNotB.ScaleB.XMax = ChartXMax
        AreaNotAandNotB.ScaleAB.XMax = ChartXMax

        AreaNotAandB.Unscaled.XMin = ChartXMin
        AreaNotAandB.ScaleA.XMin = ChartXMin
        AreaNotAandB.ScaleB.XMin = ChartXMin
        AreaNotAandB.ScaleAB.XMin = ChartXMin

        AreaNotAandB.Unscaled.XMax = ChartXMax
        AreaNotAandB.ScaleA.XMax = ChartXMax
        AreaNotAandB.ScaleB.XMax = ChartXMax
        AreaNotAandB.ScaleAB.XMax = ChartXMax

        AreaAandB.Unscaled.XMin = ChartXMin
        AreaAandB.ScaleA.XMin = ChartXMin
        AreaAandB.ScaleB.XMin = ChartXMin
        AreaAandB.ScaleAB.XMin = ChartXMin

        AreaAandB.Unscaled.XMax = ChartXMax
        AreaAandB.ScaleA.XMax = ChartXMax
        AreaAandB.ScaleB.XMax = ChartXMax
        AreaAandB.ScaleAB.XMax = ChartXMax

        AreaAandNotB.Unscaled.XMin = ChartXMin
        AreaAandNotB.ScaleA.XMin = ChartXMin
        AreaAandNotB.ScaleB.XMin = ChartXMin
        AreaAandNotB.ScaleAB.XMin = ChartXMin

        AreaAandNotB.Unscaled.XMax = ChartXMax
        AreaAandNotB.ScaleA.XMax = ChartXMax
        AreaAandNotB.ScaleB.XMax = ChartXMax
        AreaAandNotB.ScaleAB.XMax = ChartXMax

    End Sub

    Public Sub SetLeftSimFigureWidths()
        'Adjust the Simulation Figure width coordinates.
        Dim ChartXMin As Integer
        Dim ChartXMax As Integer

        ChartXMin = 20 'The Chart will be 20 pixels fro the left of the SImulation figure.
        ChartXMax = ChartXMin + Diagram.ChartWidth

        AreaNotAandNotB.Unscaled.XMin = ChartXMin
        AreaNotAandNotB.ScaleA.XMin = ChartXMin
        AreaNotAandNotB.ScaleB.XMin = ChartXMin
        AreaNotAandNotB.ScaleAB.XMin = ChartXMin

        AreaNotAandNotB.Unscaled.XMax = ChartXMax
        AreaNotAandNotB.ScaleA.XMax = ChartXMax
        AreaNotAandNotB.ScaleB.XMax = ChartXMax
        AreaNotAandNotB.ScaleAB.XMax = ChartXMax

        AreaNotAandB.Unscaled.XMin = ChartXMin
        AreaNotAandB.ScaleA.XMin = ChartXMin
        AreaNotAandB.ScaleB.XMin = ChartXMin
        AreaNotAandB.ScaleAB.XMin = ChartXMin

        AreaNotAandB.Unscaled.XMax = ChartXMax
        AreaNotAandB.ScaleA.XMax = ChartXMax
        AreaNotAandB.ScaleB.XMax = ChartXMax
        AreaNotAandB.ScaleAB.XMax = ChartXMax

        AreaAandB.Unscaled.XMin = ChartXMin
        AreaAandB.ScaleA.XMin = ChartXMin
        AreaAandB.ScaleB.XMin = ChartXMin
        AreaAandB.ScaleAB.XMin = ChartXMin

        AreaAandB.Unscaled.XMax = ChartXMax
        AreaAandB.ScaleA.XMax = ChartXMax
        AreaAandB.ScaleB.XMax = ChartXMax
        AreaAandB.ScaleAB.XMax = ChartXMax

        AreaAandNotB.Unscaled.XMin = ChartXMin
        AreaAandNotB.ScaleA.XMin = ChartXMin
        AreaAandNotB.ScaleB.XMin = ChartXMin
        AreaAandNotB.ScaleAB.XMin = ChartXMin

        AreaAandNotB.Unscaled.XMax = ChartXMax
        AreaAandNotB.ScaleA.XMax = ChartXMax
        AreaAandNotB.ScaleB.XMax = ChartXMax
        AreaAandNotB.ScaleAB.XMax = ChartXMax
    End Sub


    Private Sub Settings_ProbAChanged(ProbA As Double) Handles Settings.ProbAChanged
        SetSimFigureHeights()
        ProbALabel.Text = "P(A) = " & Settings.FormattedProbA
        ProbAandNotBLabel.Text = "P(AandNotB) = " & Settings.FormattedProbAandNotB
        ProbNotAandNotBLabel.Text = "P(NotAandNotB) = " & Settings.FormattedProbNotAandNotB
    End Sub

    Private Sub Settings_ProbAandBChanged(ProbAandB As Double) Handles Settings.ProbAandBChanged
        SetSimFigureHeights()
        ProbAandBLabel.Text = "P(AandB) = " & Settings.FormattedProbAandB
        ProbAandNotBLabel.Text = "P(AandNotB) = " & Settings.FormattedProbAandNotB
        ProbNotAandBLabel.Text = "P(NotAandB) = " & Settings.FormattedProbNotAandB
        ProbNotAandNotBLabel.Text = "P(NotAandNotB) = " & Settings.FormattedProbNotAandNotB
    End Sub

    Private Sub Settings_ProbBChanged(ProbB As Double) Handles Settings.ProbBChanged
        SetSimFigureHeights()
        ProbBLabel.Text = "P(B) = " & Settings.FormattedProbB
        ProbNotAandBLabel.Text = "P(NotAandB) = " & Settings.FormattedProbNotAandB
        ProbNotAandNotBLabel.Text = "P(NotAandNotB) = " & Settings.FormattedProbNotAandNotB
    End Sub

    Public Sub DefaultTitlePosition()
        'Set the default position for the Title text.

        Dim DiagMidX As Integer = Diagram.Width / 2 'Get the X midpoint of the Diagram.

        AnnotTitle.Unscaled.MidX = DiagMidX
        Debug.WriteLine("AnnotTitle.Unscaled.MidX = " & AnnotTitle.Unscaled.MidX & "    AnnotTitle.Unscaled.X = " & AnnotTitle.Unscaled.X)
        AnnotTitle.Unscaled.Y = 10
        AnnotTitle.ScaleA.MidX = DiagMidX
        Debug.WriteLine("AnnotTitle.ScaleA.MidX = " & AnnotTitle.ScaleA.MidX & "    AnnotTitle.ScaleA.X = " & AnnotTitle.ScaleA.X)
        AnnotTitle.ScaleA.Y = 10
        AnnotTitle.ScaleB.MidX = DiagMidX
        AnnotTitle.ScaleB.Y = 10
        AnnotTitle.ScaleAB.MidX = DiagMidX
        AnnotTitle.ScaleAB.Y = 10
    End Sub

    Public Sub DefaultProbAnnotPosn()
        'Get the default probability and sample count annotation positions
        DefaultAnnotProbAandNotBPosn()
        DefaultAnnotProbAPosn() 'Position defined using MidX and Y
        DefaultAnnotProbAandBPosn()
        DefaultAnnotProbBPosn()
        DefaultAnnotProbNotAandBPosn()
        DefaultAnnotProbNotAandNotBPosn()
    End Sub

    Public Sub DefaultAnnotProbAandNotBPosn()
        'Get the default annotation position for the Event A and Not B probability.
        ProbAandNotBLabel.Unscaled.X = AreaAandNotB.Unscaled.XMax + 20
        ProbAandNotBLabel.Unscaled.MidY = (AreaAandNotB.Unscaled.YMin + AreaAandNotB.Unscaled.YMax) / 2 'The middle of AreaAandNotB

        'Copy the unscaled location to the scaled location versions:
        ProbAandNotBLabel.ScaleA.X = ProbAandNotBLabel.Unscaled.X
        ProbAandNotBLabel.ScaleA.MidY = ProbAandNotBLabel.Unscaled.MidY
        ProbAandNotBLabel.ScaleB.X = ProbAandNotBLabel.Unscaled.X
        ProbAandNotBLabel.ScaleB.MidY = ProbAandNotBLabel.Unscaled.MidY
        ProbAandNotBLabel.ScaleAB.X = ProbAandNotBLabel.Unscaled.X
        ProbAandNotBLabel.ScaleAB.MidY = ProbAandNotBLabel.Unscaled.MidY
    End Sub

    Public Sub DefaultAnnotProbAPosn()
        'Get the default annotation position for the Event A probability.
        ProbALabel.Unscaled.X = AreaAandNotB.Unscaled.XMax + 50
        ProbALabel.Unscaled.MidY = (AreaAandB.Unscaled.YMin + AreaAandNotB.Unscaled.YMax) / 2 'The middle of Event A area

        'Copy the unscaled location to the scaled location versions:
        ProbALabel.ScaleA.X = ProbALabel.Unscaled.X
        ProbALabel.ScaleA.MidY = ProbALabel.Unscaled.MidY
        ProbALabel.ScaleB.X = ProbALabel.Unscaled.X
        ProbALabel.ScaleB.MidY = ProbALabel.Unscaled.MidY
        ProbALabel.ScaleAB.X = ProbALabel.Unscaled.X
        ProbALabel.ScaleAB.MidY = ProbALabel.Unscaled.MidY
    End Sub

    Public Sub DefaultAnnotProbAandBPosn()
        'Get the default annotation position for the Event A and B probability.

        ProbAandBLabel.Unscaled.X = AreaAandNotB.Unscaled.XMax + 20
        ProbAandBLabel.Unscaled.MidY = (AreaAandB.Unscaled.YMin + AreaAandB.Unscaled.YMax) / 2 'The middle of AreaAandB

        'Copy the unscaled location to the scaled location versions:
        ProbAandBLabel.ScaleA.X = ProbAandBLabel.Unscaled.X
        ProbAandBLabel.ScaleA.MidY = ProbAandBLabel.Unscaled.MidY
        ProbAandBLabel.ScaleB.X = ProbAandBLabel.Unscaled.X
        ProbAandBLabel.ScaleB.MidY = ProbAandBLabel.Unscaled.MidY
        ProbAandBLabel.ScaleAB.X = ProbAandBLabel.Unscaled.X
        ProbAandBLabel.ScaleAB.MidY = ProbAandBLabel.Unscaled.MidY
    End Sub

    Public Sub DefaultAnnotProbBPosn()
        'Get the default annotation position for the Event B probability.

        ProbBLabel.Unscaled.X = AreaAandNotB.Unscaled.XMax + 50
        ProbBLabel.Unscaled.MidY = (AreaNotAandB.Unscaled.YMin + AreaAandB.Unscaled.YMax) / 2 'The middle of Event B area

        'Copy the unscaled location to the scaled location versions:
        ProbBLabel.ScaleA.X = ProbBLabel.Unscaled.X
        ProbBLabel.ScaleA.MidY = ProbBLabel.Unscaled.MidY
        ProbBLabel.ScaleB.X = ProbBLabel.Unscaled.X
        ProbBLabel.ScaleB.MidY = ProbBLabel.Unscaled.MidY
        ProbBLabel.ScaleAB.X = ProbBLabel.Unscaled.X
        ProbBLabel.ScaleAB.MidY = ProbBLabel.Unscaled.MidY
    End Sub

    Public Sub DefaultAnnotProbNotAandBPosn()
        'Get the default annotation position for the Event Not A and B probability.
        ProbNotAandBLabel.Unscaled.X = AreaAandNotB.Unscaled.XMax + 20
        ProbNotAandBLabel.Unscaled.MidY = (AreaNotAandB.Unscaled.YMin + AreaNotAandB.Unscaled.YMax) / 2 'The middle of AreaNotAandB

        'Copy the unscaled location to the scaled location versions:
        ProbNotAandBLabel.ScaleA.X = ProbNotAandBLabel.Unscaled.X
        ProbNotAandBLabel.ScaleA.MidY = ProbNotAandBLabel.Unscaled.MidY
        ProbNotAandBLabel.ScaleB.X = ProbNotAandBLabel.Unscaled.X
        ProbNotAandBLabel.ScaleB.MidY = ProbNotAandBLabel.Unscaled.MidY
        ProbNotAandBLabel.ScaleAB.X = ProbNotAandBLabel.Unscaled.X
        ProbNotAandBLabel.ScaleAB.MidY = ProbNotAandBLabel.Unscaled.MidY
    End Sub

    Public Sub DefaultAnnotProbNotAandNotBPosn()
        'Get the default annotation position for the Event Not A and NotB probability.
        ProbNotAandNotBLabel.Unscaled.X = AreaAandNotB.Unscaled.XMax + 20
        ProbNotAandNotBLabel.Unscaled.MidY = (AreaNotAandNotB.Unscaled.YMin + AreaNotAandNotB.Unscaled.YMax) / 2 'The middle of AreaNotAandNotB

        'Copy the unscaled location to the scaled location versions:
        ProbNotAandNotBLabel.ScaleA.X = ProbNotAandNotBLabel.Unscaled.X
        ProbNotAandNotBLabel.ScaleA.MidY = ProbNotAandNotBLabel.Unscaled.MidY
        ProbNotAandNotBLabel.ScaleB.X = ProbNotAandNotBLabel.Unscaled.X
        ProbNotAandNotBLabel.ScaleB.MidY = ProbNotAandNotBLabel.Unscaled.MidY
        ProbNotAandNotBLabel.ScaleAB.X = ProbNotAandNotBLabel.Unscaled.X
        ProbNotAandNotBLabel.ScaleAB.MidY = ProbNotAandNotBLabel.Unscaled.MidY
    End Sub




    Public Sub DefaultCentredAnnotProbAandNotBPosn()
        'Get the default annotation position for the Event A and Not B probability.
        ProbAandNotBLabel.Unscaled.MidX = AreaAandNotB.Unscaled.XMin + (AreaAandNotB.Unscaled.XMax - AreaAandNotB.Unscaled.XMin) / 2 'Set the X mid point in the middle of AreaAandNotB.
        ProbAandNotBLabel.Unscaled.Y = (AreaAandNotB.Unscaled.YMin + AreaAandNotB.Unscaled.YMax) / 2 'The middle of AreaAandNotB

        'Copy the unscaled location to the scaled location versions:
        ProbAandNotBLabel.ScaleA.MidX = ProbAandNotBLabel.Unscaled.MidX
        ProbAandNotBLabel.ScaleA.Y = ProbAandNotBLabel.Unscaled.Y
        ProbAandNotBLabel.ScaleB.MidX = ProbAandNotBLabel.Unscaled.MidX
        ProbAandNotBLabel.ScaleB.Y = ProbAandNotBLabel.Unscaled.Y
        ProbAandNotBLabel.ScaleAB.MidX = ProbAandNotBLabel.Unscaled.MidX
        ProbAandNotBLabel.ScaleAB.Y = ProbAandNotBLabel.Unscaled.Y
    End Sub

    Public Sub DefaultCentredAnnotProbAPosn()
        'Get the default annotation position for the Event A probability.
        'ProbALabel.Unscaled.MidX = EventA.Unscaled.XMin + (EventA.Unscaled.XMax - EventA.Unscaled.XMin) / 2 'Set the X mid point in the middle of the Event A shape.
        ProbALabel.Unscaled.MidX = AreaAandNotB.Unscaled.XMin + (AreaAandNotB.Unscaled.XMax - AreaAandNotB.Unscaled.XMin) / 2 'Set the X mid point in the middle of AreaAandNotB.
        'ProbALabel.Unscaled.Y = EventA.Unscaled.YMin + 10 '10 pixels below the top of the Event A shape.
        'ProbALabel.Unscaled.Y = (AreaAandNotB.Unscaled.YMin + AreaAandB.Unscaled.YMax) / 2 'The middle of Event A area
        ProbALabel.Unscaled.Y = (AreaAandB.Unscaled.YMin + AreaAandNotB.Unscaled.YMax) / 2 'The middle of Event A area

        'Copy the unscaled location to the scaled location versions:
        ProbALabel.ScaleA.MidX = ProbALabel.Unscaled.MidX
        ProbALabel.ScaleA.Y = ProbALabel.Unscaled.Y
        ProbALabel.ScaleB.MidX = ProbALabel.Unscaled.MidX
        ProbALabel.ScaleB.Y = ProbALabel.Unscaled.Y
        ProbALabel.ScaleAB.MidX = ProbALabel.Unscaled.MidX
        ProbALabel.ScaleAB.Y = ProbALabel.Unscaled.Y
    End Sub

    Public Sub DefaultCentredAnnotProbAandBPosn()
        'Get the default annotation position for the Event A and B probability.

        ProbAandBLabel.Unscaled.MidX = AreaAandB.Unscaled.XMin + (AreaAandB.Unscaled.XMax - AreaAandB.Unscaled.XMin) / 2 'Set the X mid point in the middle of AreaAandB.
        ProbAandBLabel.Unscaled.Y = (AreaAandB.Unscaled.YMin + AreaAandB.Unscaled.YMax) / 2 'The middle of AreaAandB

        'Copy the unscaled location to the scaled location versions:
        ProbAandBLabel.ScaleA.MidX = ProbAandBLabel.Unscaled.MidX
        ProbAandBLabel.ScaleA.Y = ProbAandBLabel.Unscaled.Y
        ProbAandBLabel.ScaleB.MidX = ProbAandBLabel.Unscaled.MidX
        ProbAandBLabel.ScaleB.Y = ProbAandBLabel.Unscaled.Y
        ProbAandBLabel.ScaleAB.MidX = ProbAandBLabel.Unscaled.MidX
        ProbAandBLabel.ScaleAB.Y = ProbAandBLabel.Unscaled.Y
    End Sub

    Public Sub DefaultCentredAnnotProbBPosn()
        'Get the default annotation position for the Event B probability.

        ProbBLabel.Unscaled.MidX = AreaNotAandB.Unscaled.XMin + (AreaNotAandB.Unscaled.XMax - AreaNotAandB.Unscaled.XMin) / 2 'Set the X mid point in the middle of AreaNotAandB.
        ProbBLabel.Unscaled.Y = (AreaNotAandB.Unscaled.YMin + AreaAandB.Unscaled.YMax) / 2 'The middle of Event B area

        'Copy the unscaled location to the scaled location versions:
        ProbBLabel.ScaleA.MidX = ProbBLabel.Unscaled.MidX
        ProbBLabel.ScaleA.Y = ProbBLabel.Unscaled.Y
        ProbBLabel.ScaleB.MidX = ProbBLabel.Unscaled.MidX
        ProbBLabel.ScaleB.Y = ProbBLabel.Unscaled.Y
        ProbBLabel.ScaleAB.MidX = ProbBLabel.Unscaled.MidX
        ProbBLabel.ScaleAB.Y = ProbBLabel.Unscaled.Y
    End Sub

    Public Sub DefaultCentredAnnotProbNotAandBPosn()
        'Get the default annotation position for the Event Not A and B probability.
        ProbNotAandBLabel.Unscaled.MidX = AreaNotAandB.Unscaled.XMin + (AreaNotAandB.Unscaled.XMax - AreaNotAandB.Unscaled.XMin) / 2 'Set the X mid point in the middle of AreaNotAandB.
        ProbNotAandBLabel.Unscaled.Y = (AreaNotAandB.Unscaled.YMin + AreaNotAandB.Unscaled.YMax) / 2 'The middle of AreaNotAandB

        'Copy the unscaled location to the scaled location versions:
        ProbNotAandBLabel.ScaleA.MidX = ProbNotAandBLabel.Unscaled.MidX
        ProbNotAandBLabel.ScaleA.Y = ProbNotAandBLabel.Unscaled.Y
        ProbNotAandBLabel.ScaleB.MidX = ProbNotAandBLabel.Unscaled.MidX
        ProbNotAandBLabel.ScaleB.Y = ProbNotAandBLabel.Unscaled.Y
        ProbNotAandBLabel.ScaleAB.MidX = ProbNotAandBLabel.Unscaled.MidX
        ProbNotAandBLabel.ScaleAB.Y = ProbNotAandBLabel.Unscaled.Y
    End Sub

    Public Sub DefaultCentredAnnotProbNotAandNotBPosn()
        'Get the default annotation position for the Event Not A and NotB probability.
        ProbNotAandNotBLabel.Unscaled.MidX = AreaNotAandNotB.Unscaled.XMin + (AreaNotAandNotB.Unscaled.XMax - AreaNotAandNotB.Unscaled.XMin) / 2 'Set the X mid point in the middle of AreaNotAandNotB.
        ProbNotAandNotBLabel.Unscaled.Y = (AreaNotAandNotB.Unscaled.YMin + AreaNotAandNotB.Unscaled.YMax) / 2 'The middle of AreaNotAandNotB

        'Copy the unscaled location to the scaled location versions:
        ProbNotAandNotBLabel.ScaleA.MidX = ProbNotAandNotBLabel.Unscaled.MidX
        ProbNotAandNotBLabel.ScaleA.Y = ProbNotAandNotBLabel.Unscaled.Y
        ProbNotAandNotBLabel.ScaleB.MidX = ProbNotAandNotBLabel.Unscaled.MidX
        ProbNotAandNotBLabel.ScaleB.Y = ProbNotAandNotBLabel.Unscaled.Y
        ProbNotAandNotBLabel.ScaleAB.MidX = ProbNotAandNotBLabel.Unscaled.MidX
        ProbNotAandNotBLabel.ScaleAB.Y = ProbNotAandNotBLabel.Unscaled.Y
    End Sub

    Public Sub RunBayesSimulation()
        'Run the Bayes Simulation.

        ''Çlear the DataTable:
        'Data.Clear()
        'Data.Reset()

        If Data.Tables.Contains("Bayes_Simulation") Then
            Data.Tables("Bayes_Simulation").Rows.Clear()
        Else
            Data.Tables.Add("Bayes_Simulation")
            'Data.Tables("Simulation").Columns.Add("ProbAandNotB", System.Type.GetType("System.Single"))
            'Data.Tables("Simulation").Columns.Add("ProbA", System.Type.GetType("System.Single"))
            'Data.Tables("Simulation").Columns.Add("ProbAandB", System.Type.GetType("System.Single"))
            'Data.Tables("Simulation").Columns.Add("ProbB", System.Type.GetType("System.Single"))
            'Data.Tables("Simulation").Columns.Add("ProbNotAandB", System.Type.GetType("System.Single"))
            'Data.Tables("Simulation").Columns.Add("ProbNotAandNotB", System.Type.GetType("System.Single"))
            'Data.Tables("Bayes_Simulation").Columns.Add("SampsAandNotB", System.Type.GetType("System.Int16"))
            'Data.Tables("Bayes_Simulation").Columns.Add("SampsA", System.Type.GetType("System.Int16"))
            'Data.Tables("Bayes_Simulation").Columns.Add("SampsAandB", System.Type.GetType("System.Int16"))
            'Data.Tables("Bayes_Simulation").Columns.Add("SampsB", System.Type.GetType("System.Int16"))
            'Data.Tables("Bayes_Simulation").Columns.Add("SampsNotAandB", System.Type.GetType("System.Int16"))
            'Data.Tables("Bayes_Simulation").Columns.Add("SampsNotAandNotB", System.Type.GetType("System.Int16"))
            Data.Tables("Bayes_Simulation").Columns.Add("SampsAandNotB", System.Type.GetType("System.Int32"))
            Data.Tables("Bayes_Simulation").Columns.Add("SampsA", System.Type.GetType("System.Int32"))
            Data.Tables("Bayes_Simulation").Columns.Add("SampsAandB", System.Type.GetType("System.Int32"))
            Data.Tables("Bayes_Simulation").Columns.Add("SampsB", System.Type.GetType("System.Int32"))
            Data.Tables("Bayes_Simulation").Columns.Add("SampsNotAandB", System.Type.GetType("System.Int32"))
            Data.Tables("Bayes_Simulation").Columns.Add("SampsNotAandNotB", System.Type.GetType("System.Int32"))
        End If

        Dim NRepeats As Integer 'The number of times the survey will be repeated to generate the probability statistics.
        Dim RepeatNo As Integer 'The current survey repeat number.
        Dim NTrials As Integer 'The number of trials in each survey.
        Dim TrialNo As Integer 'The current survey trial number.

        'Variables to count the samples in each category:
        'Dim SampsAandNotB As Integer = 0
        'Dim SampsA As Integer = 0
        'Dim SampsAandB As Integer = 0
        'Dim SampsB As Integer = 0
        'Dim SampsNotAandB As Integer = 0
        'Dim SampsNotAandNotB As Integer = 0
        Dim SampsAandNotB As Long = 0
        Dim SampsA As Long = 0
        Dim SampsAandB As Long = 0
        Dim SampsB As Long = 0
        Dim SampsNotAandB As Long = 0
        Dim SampsNotAandNotB As Long = 0

        Dim Rand As Double 'Stores the current random number.

        NRepeats = Settings.SurveyRepeatNo
        NTrials = Settings.SurveySize

        Dim myRandom As New Random
        'Apply randomization seed if required:
        If Settings.Seed = -1 Then
            myRandom = New Random 'This starts a new random sequence using a seed based on the time
        Else
            myRandom = New Random(Settings.Seed) 'This starts a new random sequence using the specified seed.
        End If

        'Specify the probability cut-offs:
        Dim PAandNotBCutOff As Double = Settings.ProbA - Settings.ProbAandB                  'If myRandom <= PAandNotBCutOff Then Increment SampsA and SampsAandNotB
        Dim PAandBCutOff As Double = Settings.ProbA                                          'If myRandom <= PAandBCutOff Then Increment SampsA and SampsAandB and SampsB
        Dim PNotAandBCutOff As Double = Settings.ProbA + Settings.ProbB - Settings.ProbAandB 'If myRandom <= PNotAandBCutOff Then Increment SampsB and SampsNotAandB
        '                                                                                     If myRandom  > PNotAandBCutOff Then Increment SampsNotAandNotB
        Dim StartTime As Date = Now
        Dim Duration As TimeSpan

        For RepeatNo = 1 To NRepeats
            Data.Tables("Bayes_Simulation").Rows.Add()
            'For TrialNo = 0 To MaxTrials
            For TrialNo = 1 To NTrials
                Rand = myRandom.NextDouble
                If Rand <= PAandNotBCutOff Then '    AandNotB: Increment AandNotB and A samples
                    SampsAandNotB += 1
                    SampsA += 1
                ElseIf Rand <= PAandBCutOff Then '   AandB: Increment A, AandB and B samples
                    SampsA += 1
                    SampsAandB += 1
                    SampsB += 1
                ElseIf Rand <= PNotAandBCutOff Then 'NotAandB: Increment B and NotAandB samples
                    SampsB += 1
                    SampsNotAandB += 1
                Else '                               NotAandNotB: Increment NotAandNotB samples
                    SampsNotAandNotB += 1
                End If
            Next
            'Convert the sample counts to probabilites and write the values to the Simulation table:
            'Data.Tables("Simulation").Rows(RepeatNo - 1).Item("ProbAandNotB") = SampsAandNotB / Settings.SurveySize
            'Data.Tables("Simulation").Rows(RepeatNo - 1).Item("ProbA") = SampsA / Settings.SurveySize
            'Data.Tables("Simulation").Rows(RepeatNo - 1).Item("ProbAandB") = SampsAandB / Settings.SurveySize
            'Data.Tables("Simulation").Rows(RepeatNo - 1).Item("ProbB") = SampsB / Settings.SurveySize
            'Data.Tables("Simulation").Rows(RepeatNo - 1).Item("ProbNotAandB") = SampsNotAandB / Settings.SurveySize
            'Data.Tables("Simulation").Rows(RepeatNo - 1).Item("ProbNotAandNotB") = SampsNotAandNotB / Settings.SurveySize

            'Write the sample counts to the Simulation table:
            Data.Tables("Bayes_Simulation").Rows(RepeatNo - 1).Item("SampsAandNotB") = SampsAandNotB
            Data.Tables("Bayes_Simulation").Rows(RepeatNo - 1).Item("SampsA") = SampsA
            Data.Tables("Bayes_Simulation").Rows(RepeatNo - 1).Item("SampsAandB") = SampsAandB
            Data.Tables("Bayes_Simulation").Rows(RepeatNo - 1).Item("SampsB") = SampsB
            Data.Tables("Bayes_Simulation").Rows(RepeatNo - 1).Item("SampsNotAandB") = SampsNotAandB
            Data.Tables("Bayes_Simulation").Rows(RepeatNo - 1).Item("SampsNotAandNotB") = SampsNotAandNotB

            'Reset the counts to zero:
            SampsAandNotB = 0
            SampsA = 0
            SampsAandB = 0
            SampsB = 0
            SampsNotAandB = 0
            SampsNotAandNotB = 0

            'Show the progress:
            If RepeatNo Mod 100 = 0 Then
                'RaiseEvent Message("  " & RepeatNo)
                'Application.DoEvents()
                'If RepeatNo Mod 1000 = 0 Then
                '    RaiseEvent Message(vbCrLf)
                'End If
                RaiseEvent Progress(RepeatNo)
            End If

            Duration = Now - StartTime
            If Duration.TotalSeconds > Settings.TimeOutSeconds Then
                RaiseEvent Message(vbCrLf & "The Bayes simulation has timed-out after  " & Duration.TotalSeconds & " seconds." & vbCrLf)
                RaiseEvent Message("The survey was repeated  " & RepeatNo & " times." & vbCrLf)
                Exit For
            End If
        Next

        'Find the Mean Event Probability and Standard Deviation
        'AandNotB:
        'SimProbAandNotBMean = Data.Tables("Bayes_Simulation").Compute("Avg(" & "SampsAandNotB" & ")", "") / NTrials
        SimProbAandNotBMean = (Data.Tables("Bayes_Simulation").Compute("Sum(" & "SampsAandNotB" & ")", "") / NRepeats) / NTrials
        Dim DiffSq As Double = 0
        For Each Row As DataRow In Data.Tables("Bayes_Simulation").Rows
            DiffSq += (Row.Item("SampsAandNotB") / NTrials - SimProbAandNotBMean) ^ 2
        Next
        'SimProbAandNotBStdDev = Math.Sqrt(DiffSq / NTrials)
        SimProbAandNotBStdDev = Math.Sqrt(DiffSq / NRepeats)
        'A:
        'SimProbAMean = Data.Tables("Bayes_Simulation").Compute("Avg(" & "SampsA" & ")", "") / NTrials
        SimProbAMean = (Data.Tables("Bayes_Simulation").Compute("Sum(" & "SampsA" & ")", "") / NRepeats) / NTrials
        DiffSq = 0
        For Each Row As DataRow In Data.Tables("Bayes_Simulation").Rows
            DiffSq += (Row.Item("SampsA") / NTrials - SimProbAMean) ^ 2
        Next
        'SimProbAStdDev = Math.Sqrt(DiffSq / NTrials)
        SimProbAStdDev = Math.Sqrt(DiffSq / NRepeats)
        'AandB:
        'SimProbAandBMean = Data.Tables("Bayes_Simulation").Compute("Avg(" & "SampsAandB" & ")", "") / NTrials
        SimProbAandBMean = (Data.Tables("Bayes_Simulation").Compute("Sum(" & "SampsAandB" & ")", "") / NRepeats) / NTrials
        DiffSq = 0
        For Each Row As DataRow In Data.Tables("Bayes_Simulation").Rows
            DiffSq += (Row.Item("SampsAandB") / NTrials - SimProbAandBMean) ^ 2
        Next
        'SimProbAandBStdDev = Math.Sqrt(DiffSq / NTrials)
        SimProbAandBStdDev = Math.Sqrt(DiffSq / NRepeats)
        'B:
        'SimProbBMean = Data.Tables("Bayes_Simulation").Compute("Avg(" & "SampsB" & ")", "") / NTrials
        SimProbBMean = (Data.Tables("Bayes_Simulation").Compute("Sum(" & "SampsB" & ")", "") / NRepeats) / NTrials
        DiffSq = 0
        For Each Row As DataRow In Data.Tables("Bayes_Simulation").Rows
            DiffSq += (Row.Item("SampsB") / NTrials - SimProbBMean) ^ 2
        Next
        'SimProbBStdDev = Math.Sqrt(DiffSq / NTrials)
        SimProbBStdDev = Math.Sqrt(DiffSq / NRepeats)
        'NotAandB:
        'SimProbNotAandBMean = Data.Tables("Bayes_Simulation").Compute("Avg(" & "SampsNotAandB" & ")", "") / NTrials
        SimProbNotAandBMean = (Data.Tables("Bayes_Simulation").Compute("Sum(" & "SampsNotAandB" & ")", "") / NRepeats) / NTrials
        DiffSq = 0
        For Each Row As DataRow In Data.Tables("Bayes_Simulation").Rows
            DiffSq += (Row.Item("SampsNotAandB") / NTrials - SimProbNotAandBMean) ^ 2
        Next
        'SimProbNotAandBStdDev = Math.Sqrt(DiffSq / NTrials)
        SimProbNotAandBStdDev = Math.Sqrt(DiffSq / NRepeats)
        'NotAandNotB:
        'SimProbNotAandNotBMean = Data.Tables("Bayes_Simulation").Compute("Avg(" & "SampsNotAandNotB" & ")", "") / NTrials
        SimProbNotAandNotBMean = (Data.Tables("Bayes_Simulation").Compute("Sum(" & "SampsNotAandNotB" & ")", "") / NRepeats) / NTrials
        DiffSq = 0
        For Each Row As DataRow In Data.Tables("Bayes_Simulation").Rows
            DiffSq += (Row.Item("SampsNotAandNotB") / NTrials - SimProbNotAandNotBMean) ^ 2
        Next
        'SimProbNotAandNotBStdDev = Math.Sqrt(DiffSq / NTrials)
        SimProbNotAandNotBStdDev = Math.Sqrt(DiffSq / NRepeats)

        RaiseEvent Message("The Bayes simulation completed. Duration:  " & Duration.TotalSeconds & " seconds" & vbCrLf)

    End Sub

    Public Sub RunEventSimulation()
        'Run the Event Simulation.

        If Data.Tables.Contains("Event_Simulation") Then
            Data.Tables("Event_Simulation").Rows.Clear()
        Else
            Data.Tables.Add("Event_Simulation")
            'Data.Tables("Event_Simulation").Columns.Add("EventTrue", System.Type.GetType("System.Int16"))
            'Data.Tables("Event_Simulation").Columns.Add("EventFalse", System.Type.GetType("System.Int16"))
            Data.Tables("Event_Simulation").Columns.Add("EventTrue", System.Type.GetType("System.Int32"))
            Data.Tables("Event_Simulation").Columns.Add("EventFalse", System.Type.GetType("System.Int32"))
        End If

        Dim NRepeats As Integer 'The number of times the survey will be repeated to generate the probability statistics.
        Dim RepeatNo As Integer 'The current survey repeat number.
        Dim NTrials As Integer 'The number of trials in each survey.
        Dim TrialNo As Integer 'The current survey trial number.

        'Variables to count the samples in each category:
        Dim EventTrue As Long = 0
        Dim EventFalse As Long = 0

        Dim Rand As Double 'Stores the current random number.

        NRepeats = Settings.SurveyRepeatNo
        NTrials = Settings.EventSurveySize

        Dim myRandom As New Random
        'Apply randomization seed if required:
        If Settings.Seed = -1 Then
            myRandom = New Random 'This starts a new random sequence using a seed based on the time
        Else
            myRandom = New Random(Settings.Seed) 'This starts a new random sequence using the specified seed.
        End If

        'Specify the probability cut-offs:
        Dim PEventCutOff As Double = Settings.ProbEvent                 'If myRandom <= PEventCutOff Then Increment EventTrue Else Increment EventFalse

        Dim StartTime As Date = Now
        Dim Duration As TimeSpan

        For RepeatNo = 1 To NRepeats
            Data.Tables("Event_Simulation").Rows.Add()
            For TrialNo = 1 To NTrials
                Rand = myRandom.NextDouble
                If Rand <= PEventCutOff Then
                    EventTrue += 1
                Else
                    EventFalse += 1
                End If
            Next

            'Write the sample counts to the Simulation table:
            Data.Tables("Event_Simulation").Rows(RepeatNo - 1).Item("EventTrue") = EventTrue
            Data.Tables("Event_Simulation").Rows(RepeatNo - 1).Item("EventFalse") = EventFalse

            'Reset the counts to zero:
            EventTrue = 0
            EventFalse = 0

            'Show the progress:
            If RepeatNo Mod 100 = 0 Then
                RaiseEvent Progress(RepeatNo)
            End If

            Duration = Now - StartTime
            If Duration.TotalSeconds > Settings.TimeOutSeconds Then
                RaiseEvent Message(vbCrLf & "The Event simulation has timed-out after  " & Duration.TotalSeconds & " seconds." & vbCrLf)
                RaiseEvent Message("The survey was repeated  " & RepeatNo & " times." & vbCrLf)
                Exit For
            End If
        Next

        'Find the Mean Event Probability and Standard Deviation
        'SimProbEventMean = Data.Tables("Event_Simulation").Compute("Avg(" & "EventTrue" & ")", "") / NTrials
        'Alternate:
        SimProbEventMean = (Data.Tables("Event_Simulation").Compute("Sum(" & "EventTrue" & ")", "") / NRepeats) / NTrials


        Dim DiffSq As Double = 0
        For Each Row As DataRow In Data.Tables("Event_Simulation").Rows
            DiffSq += (Row.Item("EventTrue") / NTrials - SimProbEventMean) ^ 2
        Next
        'SimProbEventStdDev = Math.Sqrt(DiffSq / NTrials)
        SimProbEventStdDev = Math.Sqrt(DiffSq / NRepeats)

        RaiseEvent Message("The Event simulation completed. Duration:  " & Duration.TotalSeconds & " seconds" & vbCrLf)

    End Sub


#End Region 'Methods --------------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Events - Events raised by this class." '=============================================================================================================================================
    Event ErrorMessage(ByVal Msg As String) 'Send an error message.
    Event Message(ByVal Msg As String) 'Send a normal message.
    Event Progress(ByVal ProgressVal As Integer) 'Send a progress update
#End Region 'Events ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class 'clsBayesSim

Public Class clsSimDiagram
    'Stores settings for the Bayes diagram

    Private _lineColor As Color = Color.Black 'The Color of the Simulation diagram outline.
    Property LineColor As Color
        Get
            Return _lineColor
        End Get
        Set(value As Color)
            _lineColor = value
        End Set
    End Property


    Private _lineThickness As Integer = 4 'The thickness of the Simulation diagram outline in pixels.
    Property LineThickness As Integer
        Get
            Return _lineThickness
        End Get
        Set(value As Integer)
            _lineThickness = value
        End Set
    End Property

    Private _boldLineThickness As Integer = 6 'The bold thickness of the Simulation diagram outline in pixels.
    Property BoldLineThickness As Integer
        Get
            Return _boldLineThickness
        End Get
        Set(value As Integer)
            _boldLineThickness = value
        End Set
    End Property

    Private _boldLine As Boolean = False 'If True, the line is bold.
    Property BoldLine As Boolean
        Get
            Return _boldLine
        End Get
        Set(value As Boolean)
            _boldLine = value
        End Set
    End Property


    'Private _fillColor As Color = Color.LightYellow 'The Color of the Simulation diagram background.
    'Private _fillColor As Color = Color.LightGoldenrodYellow 'The Color of the Simulation diagram background.
    Private _fillColor As Color = Color.LightGray 'The Color of the Simulation diagram background.
    Property FillColor As Color
        Get
            Return _fillColor
        End Get
        Set(value As Color)
            _fillColor = value
        End Set
    End Property

    Private _height As Integer = 400 'The height of the Simulation diagram in pixels.
    Property Height As Integer
        Get
            Return _height
        End Get
        Set(value As Integer)
            _height = value
            RaiseEvent HeightChanged(_height)
        End Set
    End Property

    Private _width As Integer = 300 'The width of the Simulation diagram in pixels.
    Property Width As Integer
        Get
            Return _width
        End Get
        Set(value As Integer)
            _width = value
            RaiseEvent WidthChanged(_width)
        End Set
    End Property

    Private _chartWidth As Integer = 60 'THe width of the Chart in the Simulation diagam.

    Public Sub New()

    End Sub

    Property ChartWidth As Integer
        Get
            Return _chartWidth
        End Get
        Set(value As Integer)
            _chartWidth = value
        End Set
    End Property

    Event HeightChanged(ByVal NewHeight As Integer) 'Send the new diagram height.
    Event WidthChanged(ByVal NewWidth As Integer) 'Send the new diagram width.

End Class 'clsSimDiagram

Public Class clsSimSettings
    'Simulation Display settings.

    Private _probabilityMeasure As String = "Decimal" 'The probability measure used to display probability values. (Decimal or Percent.)
    Property ProbabilityMeasure As String
        Get
            Return _probabilityMeasure
        End Get
        Set(value As String)
            _probabilityMeasure = value
            RaiseEvent ProbabilityMeasureChanged(_probabilityMeasure)
        End Set
    End Property

    Private _decimalFormat As String = "" 'The format code used to display decimal probabilities. (N4 - Number displayed with thousands separator and 4 decimal places, F4 - Number displayed with 4 decimal places.)
    Property DecimalFormat As String
        Get
            Return _decimalFormat
        End Get
        Set(value As String)
            _decimalFormat = value
            RaiseEvent DecimalFormatChanged(_decimalFormat)
        End Set
    End Property

    Private _percentFormat As String = "" 'The format code used to display percent probabilities.
    Property PercentFormat As String
        Get
            Return _percentFormat
        End Get
        Set(value As String)
            _percentFormat = value
            RaiseEvent PercentFormatChanged(_percentFormat)
        End Set
    End Property

    Private _samplesFormat As String = "" 'The format code used to display the number of samples.
    Property SamplesFormat As String
        Get
            Return _samplesFormat
        End Get
        Set(value As String)
            _samplesFormat = value
            RaiseEvent SamplesFormatChanged(_samplesFormat)
        End Set
    End Property

    'Private _condition As String = "None" 'Condition used to display Probability Diagram catagories. (None, EventATrue, EventAFalse, EventBTrue, EventBFalse)
    'Property Condition As String
    '    Get
    '        Return _condition
    '    End Get
    '    Set(value As String)
    '        _condition = value
    '        RaiseEvent ConditionChanged(_condition)
    '    End Set
    'End Property

    Private _scaling As String = "Unscaled" 'The scaling used to display the Bayes model (Unscaled, ScaleA, ScaleB, ScaleAB)
    'Unscaled: Diagrammatic figure - not to scale.
    'ScaleA: The Event A shape area is scaled relative to the Event B shape area.
    'ScaleB: The Event B shape is scaled relative to the Event A shape area.
    'ScaleAB: The Event A and Event B shape areas are scaled relative the their probabilities, where the total area of the diagram represents the probability of 1.
    Property Scaling As String
        Get
            Return _scaling
        End Get
        Set(value As String)
            _scaling = value
            RaiseEvent ScalingChanged(_scaling)
        End Set
    End Property

    Private _surveySize As Integer = 10000 'The size of the survey.
    Property SurveySize As Integer
        Get
            Return _surveySize
        End Get
        Set(value As Integer)
            _surveySize = value
        End Set
    End Property

    Private _probA As Double = 0.2 'The probability of Event A
    Property ProbA As Double
        Get
            Return _probA
        End Get
        Set(value As Double)
            _probA = value
            RaiseEvent ProbAChanged(_probA)
        End Set
    End Property

    ReadOnly Property FormattedProbA As String
        Get
            If ProbabilityMeasure = "Percent" Then
                Return Format(ProbA * 100, PercentFormat) & "%"
            Else
                Return Format(ProbA, DecimalFormat)
            End If
        End Get
    End Property

    Private _probAandB As Double = 0.1 'The probability of Event A and Event B
    Property ProbAandB As Double
        Get
            Return _probAandB
        End Get
        Set(value As Double)
            _probAandB = value
            RaiseEvent ProbAandBChanged(_probAandB)
        End Set
    End Property

    ReadOnly Property FormattedProbAandB As String
        Get
            If ProbabilityMeasure = "Percent" Then
                Return Format(ProbAandB * 100, PercentFormat) & "%"
            Else
                Return Format(ProbAandB, DecimalFormat)
            End If
        End Get
    End Property

    Private _probB As Double = 0.2 'The probability of Event B
    Property ProbB As Double
        Get
            Return _probB
        End Get
        Set(value As Double)
            _probB = value
            RaiseEvent ProbBChanged(_probB)
        End Set
    End Property

    ReadOnly Property FormattedProbB As String
        Get
            If ProbabilityMeasure = "Percent" Then
                Return Format(ProbB * 100, PercentFormat) & "%"
            Else
                Return Format(ProbB, DecimalFormat)
            End If
        End Get
    End Property

    ReadOnly Property FormattedProbAandNotB As String
        Get
            If ProbabilityMeasure = "Percent" Then
                Return Format((ProbA - ProbAandB) * 100, PercentFormat) & "%"
            Else
                Return Format((ProbA - ProbAandB), DecimalFormat)
            End If
        End Get
    End Property

    ReadOnly Property FormattedProbNotAandB As String
        Get
            If ProbabilityMeasure = "Percent" Then
                Return Format((ProbB - ProbAandB) * 100, PercentFormat) & "%"
            Else
                Return Format((ProbB - ProbAandB), DecimalFormat)
            End If
        End Get
    End Property

    ReadOnly Property FormattedProbNotAandNotB As String
        Get
            If ProbabilityMeasure = "Percent" Then
                Return Format((1 - ProbA - ProbB + ProbAandB) * 100, PercentFormat) & "%"
            Else
                Return Format((1 - ProbA - ProbB + ProbAandB), DecimalFormat)
            End If
        End Get
    End Property

    'Event Simulation Parameters:
    Private _eventSurveySize As Integer = 10000 'The size of the Event survey.
    Property EventSurveySize As Integer
        Get
            Return _eventSurveySize
        End Get
        Set(value As Integer)
            _eventSurveySize = value
        End Set
    End Property

    Private _probEvent As Double = 0.2 'The probability of Event
    Property ProbEvent As Double
        Get
            Return _probEvent
        End Get
        Set(value As Double)
            _probEvent = value
            RaiseEvent ProbEventChanged(_probEvent)
        End Set
    End Property

    ReadOnly Property FormattedProbEvent As String
        Get
            If ProbabilityMeasure = "Percent" Then
                Return Format(ProbEvent * 100, PercentFormat) & "%"
            Else
                Return Format(ProbEvent, DecimalFormat)
            End If
        End Get
    End Property

    'General Simulation Parameters:
    Private _surveyRepeatNo As Integer = 10000 'The number of times the simulated survey will be repeated to determine the uncertainty in probabilities determined using the survey.
    Property SurveyRepeatNo As Integer
        Get
            Return _surveyRepeatNo
        End Get
        Set(value As Integer)
            _surveyRepeatNo = value
        End Set
    End Property

    Private _timeOutSeconds As Integer = 60 'The time-out period in seconds. The simulation will terminate after this time if it has not already finished.
    Property TimeOutSeconds As Integer
        Get
            Return _timeOutSeconds
        End Get
        Set(value As Integer)
            _timeOutSeconds = value
        End Set
    End Property

    Private _seed As Integer = -1 'Randomization seed. Integer >= 1. Use -1 for no seed.
    Property Seed As Integer
        Get
            Return _seed
        End Get
        Set(value As Integer)
            _seed = value
        End Set
    End Property



    Event ProbabilityMeasureChanged(ByVal Measure As String) 'Send the changed Probability Measure
    Event DecimalFormatChanged(ByVal Format As String) 'Send the changed Decimal Format
    Event PercentFormatChanged(ByVal Format As String) 'Send the changed Percent Format
    Event SamplesFormatChanged(ByVal Format As String) 'Send the changed Samples Format
    'Event ConditionChanged(ByVal Condition As String) 'Send the changed condition.
    Event ScalingChanged(ByVal Scaling As String) 'Send the changed scaling.
    Event ProbAChanged(ByVal ProbA As Double) 'Send the changed ProbA value.
    Event ProbAandBChanged(ByVal ProbAandB As Double) 'Send the changed ProbAandB value.
    Event ProbBChanged(ByVal ProbB As Double) 'Send the changed ProbB value.
    Event ProbEventChanged(ByVal ProbEvent As Double) 'Send the changed ProbB value.

End Class 'clsSimSettings

Public Class clsSimLabel
    'SimulationLabel class - stores Simulation diagram label information including text, font color and position.

    Public Settings As clsSimSettings 'This will point to clsBayesSim.Settings

    Private _text As String = "" 'The label text.
    Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
            Unscaled.UpdateLabelPosition()
            ScaleA.UpdateLabelPosition()
            ScaleB.UpdateLabelPosition()
            ScaleAB.UpdateLabelPosition()

            ''UpdateLabelPosition() 'Update the X and Y position
            'Select Case Settings.Scaling
            '    Case "Unscaled"
            '        Unscaled.UpdateLabelPosition()
            '    Case "ScaleA"
            '        ScaleA.UpdateLabelPosition()
            '    Case "ScaleB"
            '        ScaleB.UpdateLabelPosition()
            '    Case "ScaleAB"
            '        ScaleAB.UpdateLabelPosition()
            '    Case Else
            '        RaiseEvent ErrorMessage("Label.Text: Unknown Scaling: " & Settings.Scaling & vbCrLf)
            'End Select
        End Set
    End Property



    Private _font As Font = New Font("Arial", 11, FontStyle.Bold) 'The label font.
    Property Font As Font
        Get
            Return _font
        End Get
        Set(value As Font)
            _font = value
            'UpdateLabelPosition() 'Update the X and Y position
            'Select Case Scaling
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.UpdateLabelPosition()
                Case "ScaleA"
                    ScaleA.UpdateLabelPosition()
                Case "ScaleB"
                    ScaleB.UpdateLabelPosition()
                Case "ScaleAB"
                    ScaleAB.UpdateLabelPosition()
                Case Else
                    RaiseEvent ErrorMessage("Label.Text: Unknown Scaling: " & Settings.Scaling & vbCrLf)
            End Select
        End Set
    End Property

    Private _color As Color = Color.Black 'The label color.
    Property Color As Color
        Get
            Return _color
        End Get
        Set(value As Color)
            _color = value
        End Set
    End Property


    'Coordinates - stores label coordinates for different display scales.
    Public Unscaled As New SimCoord 'Used for displaying a label on an unscaled Simulation diagram.
    Public ScaleA As New SimCoord 'Used for displaying a label on a Simulation diagram with the Event A area scaled in relation to the Event B area (where the areas are proportional to the probabilities).
    Public ScaleB As New SimCoord 'Used for displaying a label on a Simulation diagram with Event B scaled in relation to Event A.
    Public ScaleAB As New SimCoord 'Used for displaying a label on a Simulation diagram with Events A and B scaled in relation to all probabilites.

    'NOTE: The following code is used to set the X and Y coordinate position of all versions of the text (Unscaled, ScaleA, ScaleB, ScaleAB)
    'Setting these label coordinates updates the Unscaled, ScaleA, ScaleB and ScaleAB versions.
    'Private _xPositionReference As String = "Start" 'The position reference used to specify the label X position. (Start, Mid or End)
    'Property XPositionReference As String
    ReadOnly Property XPositionReference As String
        Get
            'Return _xPositionReference
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.XPositionReference
                Case "ScaleA"
                    Return ScaleA.XPositionReference
                Case "ScaleB"
                    Return ScaleB.XPositionReference
                Case "ScaleAB"
                    Return ScaleAB.XPositionReference
                Case Else
                    RaiseEvent ErrorMessage("Get Label.XPositionReference: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.XPositionReference
            End Select
        End Get
    End Property

    ReadOnly Property YPositionReference As String
        Get
            'Return _yPositionReference
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.YPositionReference
                Case "ScaleA"
                    Return ScaleA.YPositionReference
                Case "ScaleB"
                    Return ScaleB.YPositionReference
                Case "ScaleAB"
                    Return ScaleAB.YPositionReference
                Case Else
                    RaiseEvent ErrorMessage("Get Label.YPositionReference: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.YPositionReference
            End Select
        End Get
    End Property

    Property X As Integer
        Get
            'Return _x
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.X
                Case "ScaleA"
                    Return ScaleA.X
                Case "ScaleB"
                    Return ScaleB.X
                Case "ScaleAB"
                    Return ScaleAB.X
                Case Else
                    RaiseEvent ErrorMessage("Get Label.X: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.X
            End Select
        End Get
        Set(value As Integer)
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.X = value
                Case "ScaleA"
                    ScaleA.X = value
                Case "ScaleB"
                    ScaleB.X = value
                Case "ScaleAB"
                    ScaleAB.X = value
                Case Else
                    RaiseEvent ErrorMessage("Set Label.X: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Unscaled.X = value
            End Select
        End Set
    End Property

    Property Y As Integer
        Get
            'Return _y
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.Y
                Case "ScaleA"
                    Return ScaleA.Y
                Case "ScaleB"
                    Return ScaleB.Y
                Case "ScaleAB"
                    Return ScaleAB.Y
                Case Else
                    RaiseEvent ErrorMessage("Get Label.Y: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.Y
            End Select
        End Get
        Set(value As Integer)
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.Y = value
                Case "ScaleA"
                    ScaleA.Y = value
                Case "ScaleB"
                    ScaleB.Y = value
                Case "ScaleAB"
                    ScaleAB.Y = value
                Case Else
                    RaiseEvent ErrorMessage("Set Label.Y: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Unscaled.Y = value
            End Select
        End Set
    End Property

    Property MidX As Integer
        Get
            'Return _midX
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.MidX
                Case "ScaleA"
                    Return ScaleA.MidX
                Case "ScaleB"
                    Return ScaleB.MidX
                Case "ScaleAB"
                    Return ScaleAB.MidX
                Case Else
                    RaiseEvent ErrorMessage("Get Label.MidX: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.MidX
            End Select
        End Get
        Set(value As Integer)
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.MidX = value
                Case "ScaleA"
                    ScaleA.MidX = value
                Case "ScaleB"
                    ScaleB.MidX = value
                Case "ScaleAB"
                    ScaleAB.MidX = value
                Case Else
                    RaiseEvent ErrorMessage("Set Label.MidX: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Unscaled.MidX = value
            End Select
        End Set
    End Property

    Property MidY As Integer
        Get
            'Return _midY
            'Select Case Scaling
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.MidY
                Case "ScaleA"
                    Return ScaleA.MidY
                Case "ScaleB"
                    Return ScaleB.MidY
                Case "ScaleAB"
                    Return ScaleAB.MidY
                Case Else
                    'RaiseEvent ErrorMessage("Get Label.MidY: Unknown scaling: " & Scaling & vbCrLf)
                    RaiseEvent ErrorMessage("Get Label.MidY: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.MidY
            End Select
        End Get
        Set(value As Integer)
            'Select Case Scaling
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.MidY = value
                Case "ScaleA"
                    ScaleA.MidY = value
                Case "ScaleB"
                    ScaleB.MidY = value
                Case "ScaleAB"
                    ScaleAB.MidY = value
                Case Else
                    'RaiseEvent ErrorMessage("Set Label.MidY: Unknown scaling: " & Scaling & vbCrLf)
                    RaiseEvent ErrorMessage("Set Label.MidY: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Unscaled.MidY = value
            End Select
        End Set
    End Property

    Property EndX As Integer
        Get
            'Return _endX
            'Select Case Scaling
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.EndX
                Case "ScaleA"
                    Return ScaleA.EndX
                Case "ScaleB"
                    Return ScaleB.EndX
                Case "ScaleAB"
                    Return ScaleAB.EndX
                Case Else
                    'RaiseEvent ErrorMessage("Get Label.EndX: Unknown scaling: " & Scaling & vbCrLf)
                    RaiseEvent ErrorMessage("Get Label.EndX: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.EndX
            End Select
        End Get
        Set(value As Integer)
            'Select Case Scaling
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.EndX = value
                Case "ScaleA"
                    ScaleA.EndX = value
                Case "ScaleB"
                    ScaleB.EndX = value
                Case "ScaleAB"
                    ScaleAB.EndX = value
                Case Else
                    'RaiseEvent ErrorMessage("Set Label.EndX: Unknown scaling: " & Scaling & vbCrLf)
                    RaiseEvent ErrorMessage("Set Label.EndX: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Unscaled.EndX = value
            End Select
        End Set
    End Property

    Property BaseY As Integer
        Get
            'Return _baseY
            'Select Case Scaling
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.BaseY
                Case "ScaleA"
                    Return ScaleA.BaseY
                Case "ScaleB"
                    Return ScaleB.BaseY
                Case "ScaleAB"
                    Return ScaleAB.BaseY
                Case Else
                    'RaiseEvent ErrorMessage("Get Label.BaseY: Unknown scaling: " & Scaling & vbCrLf)
                    RaiseEvent ErrorMessage("Get Label.BaseY: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.BaseY
            End Select
        End Get
        Set(value As Integer)
            'Select Case Scaling
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.BaseY = value
                Case "ScaleA"
                    ScaleA.BaseY = value
                Case "ScaleB"
                    ScaleB.BaseY = value
                Case "ScaleAB"
                    ScaleAB.BaseY = value
                Case Else
                    'RaiseEvent ErrorMessage("Set Label.BaseY: Unknown scaling: " & Scaling & vbCrLf)
                    RaiseEvent ErrorMessage("Set Label.BaseY: Unknown scaling: " & Settings.Scaling & vbCrLf)
                    Unscaled.BaseY = value
            End Select
        End Set
    End Property



    Public Sub New()
        'These Coord objects need to access parent properties:
        'The following code passes a reference to the parent class:
        Unscaled.parent = Me
        ScaleA.parent = Me
        ScaleB.parent = Me
        ScaleAB.parent = Me
    End Sub

    Private _show As Boolean = True 'The Show flag. If True, the label is shown.
    Property Show As Boolean
        Get
            Return _show
        End Get
        Set(value As Boolean)
            _show = value
        End Set
    End Property

    'Private _scaling As String = "Unscaled" 'The scaling used to display the Bayes model (Unscaled, ScaleA, ScaleB, ScaleAB)
    ''Unscaled: Diagrammatic figure - not to scale.
    ''ScaleA: The Event A shape area is scaled relative to the Event B shape area.
    ''ScaleB: The Event B shape is scaled relative to the Event A shape area.
    ''ScaleAB: The Event A and Event B shape areas are scaled relative the their probabilities, where the total area of the diagram represents the probability of 1.
    'Property Scaling As String
    '    Get
    '        Return _scaling
    '    End Get
    '    Set(value As String)
    '        _scaling = value
    '    End Set
    'End Property

#Region " Events - Events raised by this class." '=============================================================================================================================================
    Event ErrorMessage(ByVal Msg As String) 'Send an error message.
    Event Message(ByVal Msg As String) 'Send a normal message.
#End Region 'Events ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

End Class 'clsLabel

Public Class SimCoord
    'Stores the coordinate of a Simulation display label.

    Public parent As clsSimLabel

    Private _xPositionReference As String = "Start" 'The position reference used to specify the label X position. (Start, Mid or End)
    Property XPositionReference As String
        Get
            Return _xPositionReference
        End Get
        Set(value As String)
            _xPositionReference = value
        End Set
    End Property

    Private _yPositionReference As String = "Top" 'The position reference used to specify the label Y position. (Top, Mid or Base)
    Property YPositionReference As String
        Get
            Return _yPositionReference
        End Get
        Set(value As String)
            _yPositionReference = value
        End Set
    End Property

    Public _x As Integer = 10 'The X pixel position of the start of the label text. 'Graphics.DrawString - X, Y is the upper-left corner of the drawn text.
    Property X As Integer
        Get
            Return _x
        End Get
        Set(value As Integer)
            _x = value
            _xPositionReference = "Start"
        End Set
    End Property

    Public _y As Integer = 10 'The Y pixel position of the top of the label text. 'Graphics.DrawString - X, Y is the upper-left corner of the drawn text.
    Property Y As Integer
        Get
            Return _y
        End Get
        Set(value As Integer)
            _y = value
            _yPositionReference = "Top"
        End Set
    End Property

    Public _midX As Integer = 10 'The X pixel position of the middle of the label text.
    Property MidX As Integer
        Get
            Return _midX
        End Get
        Set(value As Integer)
            _midX = value
            'Dim TextWidth As Integer = TextRenderer.MeasureText(Text, Font).Width
            Dim TextWidth As Integer = TextRenderer.MeasureText(parent.Text, parent.Font).Width
            _x = _midX - TextWidth / 2
            _xPositionReference = "Mid"
        End Set
    End Property

    Public _midY As Integer = 10 'The Y pixel position of the middle of the label text.
    Property MidY As Integer
        Get
            Return _midY
        End Get
        Set(value As Integer)
            _midY = value
            'Dim TextHeight As Integer = TextRenderer.MeasureText(Text, Font).Height
            Dim TextHeight As Integer = TextRenderer.MeasureText(parent.Text, parent.Font).Height
            _y = _midY - TextHeight / 2
            _yPositionReference = "Mid"
        End Set
    End Property

    Public _endX As Integer = 10 'The X pixel position of the end of the label text.
    Property EndX As Integer
        Get
            Return _endX
        End Get
        Set(value As Integer)
            _endX = value
            Dim TextWidth As Integer = TextRenderer.MeasureText(parent.Text, parent.Font).Width
            _x = _endX - TextWidth
            _xPositionReference = "End"
        End Set
    End Property


    Public _baseY As Integer = 10 'The Y pixel position of the base of the label text.
    Property BaseY As Integer
        Get
            Return _baseY
        End Get
        Set(value As Integer)
            _baseY = value
            'Dim TextHeight As Integer = TextRenderer.MeasureText(Text, Font).Height
            Dim TextHeight As Integer = TextRenderer.MeasureText(parent.Text, parent.Font).Height
            _y = _baseY - TextHeight
            _yPositionReference = "Base"
        End Set
    End Property

    Public Sub UpdateLabelPosition()
        'Update the label position.
        'This is required if the Label text or Font is changed.
        If _xPositionReference = "Top" Then
            'The X position remains unchanged.
        ElseIf _xPositionReference = "Mid" Then
            'Update the X position as the string width may have changed.
            Dim TextWidth As Integer = TextRenderer.MeasureText(parent.Text, parent.Font).Width
            _x = _midX - TextWidth / 2
        ElseIf _xPositionReference = "End" Then
            'Update the X position as the string width may have changed.
            Dim TextWidth As Integer = TextRenderer.MeasureText(parent.Text, parent.Font).Width
            _x = _endX - TextWidth
        Else
            'Unknown _xPositionReference
        End If
        If _yPositionReference = "Top" Then
            'The Y position remains unchanged.
        ElseIf _yPositionReference = "Mid" Then
            'Update the Y position as the string height may have changed.
            Dim TextHeight As Integer = TextRenderer.MeasureText(parent.Text, parent.Font).Height
            _y = _midY - TextHeight / 2
        ElseIf _yPositionReference = "Base" Then
            'Update the Y position as the string height may have changed.
            Dim TextHeight As Integer = TextRenderer.MeasureText(parent.Text, parent.Font).Height
            _y = _baseY - TextHeight
        Else
            'Unknown _yPositionReference
        End If
    End Sub

End Class 'SimCoord


Public Class AreaInfo

    Public Settings As clsSimSettings 'This will point to Bayes.Settings

    Private _name As String = "Event" 'The name of the Event.
    Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
            'RaiseEvent NameChanged(_name)
            'If AnnotEventA.Text = "" Then AnnotEventA.Text = _eventAName 'If the Annotation Event A Text is blank, use the Event A Name.
        End Set
    End Property

    Private _lineColor As Color = Color.Red 'The line color of the Venn diagram shape representing the Event.
    Property LineColor As Color
        Get
            Return _lineColor
        End Get
        Set(value As Color)
            _lineColor = value
        End Set
    End Property

    Private _fillColor As Color = Color.MistyRose 'The fill color of the Venn diagram shape representing Event A.
    Property FillColor As Color
        Get
            Return _fillColor
        End Get
        Set(value As Color)
            _fillColor = value
        End Set
    End Property

    Private _lineThickness As Integer = 4 'The line thickness of the Event shape in pixels.
    Property LineThickness As Integer
        Get
            Return _lineThickness
        End Get
        Set(value As Integer)
            _lineThickness = value
        End Set
    End Property

    Private _boldLineThickness As Integer = 6 'The bold line thickness of the Event shape in pixels.
    Property BoldLineThickness As Integer
        Get
            Return _boldLineThickness
        End Get
        Set(value As Integer)
            _boldLineThickness = value
        End Set
    End Property

    Private _boldLine As Boolean = False 'If True, the Event is drawn with a bold outline.
    Property BoldLine As Boolean
        Get
            Return _boldLine
        End Get
        Set(value As Boolean)
            _boldLine = value
        End Set
    End Property

    'Shape Bounds - stores the shape boundaries for different display scales.
    Public Unscaled As New SimShapeBounds 'Used for displaying a shape on an unscaled Bayes diagram.
    Public ScaleA As New SimShapeBounds 'Used for displaying a shape on a Bayes diagram with the Event A area scaled in relation to the Event B area (where the areas are proportional to the probabilities).
    Public ScaleB As New SimShapeBounds 'Used for displaying a shape on a Bayes diagram with Event B scaled in relation to Event A.
    Public ScaleAB As New SimShapeBounds 'Used for displaying a shape on a bayes diagram with Events A and B scaled in relation to all probabilites.


    'This version of the Shape Bounds sets and gets the appropriate bounds for the selected scale. Scaling: Unscaled, ScaleA, ScaleB or ScaleAB
    'Private _xMin As Integer = 100 'The minimum X position of the Event shape in pixels.
    Property XMin As Integer
        Get
            'Return _xMin
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.XMin
                Case "ScaleA"
                    Return ScaleA.XMin
                Case "ScaleB"
                    Return ScaleB.XMin
                Case "ScaleAB"
                    Return ScaleAB.XMin
                Case Else
                    RaiseEvent ErrorMessage("Unknown Scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.XMin
            End Select
        End Get
        Set(value As Integer)
            '_xMin = value
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.XMin = value
                Case "ScaleA"
                    ScaleA.XMin = value
                Case "ScaleB"
                    ScaleB.XMin = value
                Case "ScaleAB"
                    ScaleAB.XMin = value
                Case Else
                    RaiseEvent ErrorMessage("Unknown Scaling: " & Settings.Scaling & vbCrLf)
                    'Unscaled.YMin = value
            End Select
        End Set
    End Property

    'Private _xMax As Integer = 500 'The maximum X position of the Event shape in pixels.
    Property XMax As Integer
        Get
            'Return _xMax
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.XMax
                Case "ScaleA"
                    Return ScaleA.XMax
                Case "ScaleB"
                    Return ScaleB.XMax
                Case "ScaleAB"
                    Return ScaleAB.XMax
                Case Else
                    RaiseEvent ErrorMessage("Unknown Scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.XMax
            End Select
        End Get
        Set(value As Integer)
            '_xMax = value
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.XMax = value
                Case "ScaleA"
                    ScaleA.XMax = value
                Case "ScaleB"
                    ScaleB.XMax = value
                Case "ScaleAB"
                    ScaleAB.XMax = value
                Case Else
                    RaiseEvent ErrorMessage("Unknown Scaling: " & Settings.Scaling & vbCrLf)
                    'Unscaled.YMin = value
            End Select
        End Set
    End Property

    'Private _yMin As Integer = 120 'The minimum Y position of the Event shape in pixels.
    Property YMin As Integer
        Get
            'Return _yMin
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.YMin
                Case "ScaleA"
                    Return ScaleA.YMin
                Case "ScaleB"
                    Return ScaleB.YMin
                Case "ScaleAB"
                    Return ScaleAB.YMin
                Case Else
                    RaiseEvent ErrorMessage("Unknown Scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.YMin
            End Select
        End Get
        Set(value As Integer)
            '_yMin = value
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.YMin = value
                Case "ScaleA"
                    ScaleA.YMin = value
                Case "ScaleB"
                    ScaleB.YMin = value
                Case "ScaleAB"
                    ScaleAB.YMin = value
                Case Else
                    RaiseEvent ErrorMessage("Unknown Scaling: " & Settings.Scaling & vbCrLf)
                    'Unscaled.YMin = value
            End Select
        End Set
    End Property

    'Private _yMax As Integer = 520 'The maximum Y position of the Event shape in pixels.
    Property YMax As Integer
        Get
            'Return _yMax
            Select Case Settings.Scaling
                Case "Unscaled"
                    Return Unscaled.YMax
                Case "ScaleA"
                    Return ScaleA.YMax
                Case "ScaleB"
                    Return ScaleB.YMax
                Case "ScaleAB"
                    Return ScaleAB.YMax
                Case Else
                    RaiseEvent ErrorMessage("Unknown Scaling: " & Settings.Scaling & vbCrLf)
                    Return Unscaled.YMax
            End Select
        End Get
        Set(value As Integer)
            '_yMax = value
            Select Case Settings.Scaling
                Case "Unscaled"
                    Unscaled.YMax = value
                Case "ScaleA"
                    ScaleA.YMax = value
                Case "ScaleB"
                    ScaleB.YMax = value
                Case "ScaleAB"
                    ScaleAB.YMax = value
                Case Else
                    RaiseEvent ErrorMessage("Unknown Scaling: " & Settings.Scaling & vbCrLf)
                    'Unscaled.YMax = value
            End Select
        End Set
    End Property

    Event ErrorMessage(ByVal Msg As String) 'Send an error message

End Class 'AreaInfo


Public Class SimShapeBounds
    'Stores the bounds of a Simulation Area shape.

    Private _xMin As Integer = 100 'The minimum X position of the Event shape in pixels.
    Property XMin As Integer
        Get
            Return _xMin
        End Get
        Set(value As Integer)
            _xMin = value
        End Set
    End Property

    Private _xMax As Integer = 500 'The maximum X position of the Event shape in pixels.
    Property XMax As Integer
        Get
            Return _xMax
        End Get
        Set(value As Integer)
            _xMax = value
        End Set
    End Property

    Private _yMin As Integer = 120 'The minimum Y position of the Event shape in pixels.
    Property YMin As Integer
        Get
            Return _yMin
        End Get
        Set(value As Integer)
            _yMin = value
        End Set
    End Property

    Private _yMax As Integer = 520 'The maximum Y position of the Event shape in pixels.
    Property YMax As Integer
        Get
            Return _yMax
        End Get
        Set(value As Integer)
            _yMax = value
        End Set
    End Property
End Class 'ShapeBounds





