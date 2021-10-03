'==============================================================================================================================================================================================
'
'Copyright 2021 Signalworks Pty Ltd, ABN 26 066 681 598

'Licensed under the Apache License, Version 2.0 (the "License");
'you may not use this file except in compliance with the License.
'You may obtain a copy of the License at
'
'http://www.apache.org/licenses/LICENSE-2.0
'
'Unless required by applicable law or agreed to in writing, software
'distributed under the License is distributed on an "AS IS" BASIS,
''WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
'See the License for the specific language governing permissions and
'limitations under the License.
'
'----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Imports System.ComponentModel
Imports System.Security.Permissions
<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
<System.Runtime.InteropServices.ComVisibleAttribute(True)> 'Note: There should be no blank lines between this line and the line: Public Class Main
Public Class Main
    'The ADVL_Bayes application demonstrates and applies Bayes theorem.
    'Using Bayes theorem, the probability of an event is updated after the occurrence of a related event.



#Region " Coding Notes - Notes on the code used in this class." '==============================================================================================================================

    'ADD THE SYSTEM UTILITIES REFERENCE: ==========================================================================================
    'The following references are required by this software: 
    'ADVL_Utilities_Library_1.dll
    'To add the reference, press Project \ Add Reference... 
    '  Select the Browse option then press the Browse button
    '  Find the ADVL_Utilities_Library_1.dll file (it should be located in the directory ...\Projects\ADVL_Utilities_Library_1\ADVL_Utilities_Library_1\bin\Debug\)
    '  Press the Add button. Press the OK button.
    'The Utilities Library is used for Project Management, Archive file management, running XSequence files and running XMessage files.
    'If there are problems with a reference, try deleting it from the references list and adding it again.

    'Add a reference to System.IO.Compression:
    '  Project \ Add Refernce... \ Assemblies \ System.IO.Compression

    'ADD THE SERVICE REFERENCE: ===================================================================================================
    'A service reference to the Message Service must be added to the source code before this service can be used.
    'This is used to connect to the Application Network.

    'Adding the service reference to a project that includes the Message Service project: -----------------------------------------
    'Project \ Add Service Reference
    'Press the Discover button.
    'Expand the items in the Services window and select IMsgService.
    'Press OK.
    '------------------------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------------------------
    'Adding the service reference to other projects that dont include the Message Service project: -------------------------------
    'Run the ADVL_Network_1 application to start the message service.
    'In Microsoft Visual Studio select: Project \ Add Service Reference
    'Enter the address: http://localhost:8734/ADVLService
    'Press the Go button.
    'MsgService is found.
    'Press OK to add ServiceReference1 to the project.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE MsgServiceCallback CODE: =============================================================================================
    'This is used to connect to the Application Network.
    'In Microsoft Visual Studio select: Project \ Add Class
    'MsgServiceCallback.vb
    'Add the following code to the class:
    'Imports System.ServiceModel
    'Public Class MsgServiceCallback
    '    Implements ServiceReference1.IMsgServiceCallback
    '    Public Sub OnSendMessage(message As String) Implements ServiceReference1.IMsgServiceCallback.OnSendMessage
    '        'A message has been received.
    '        'Set the InstrReceived property value to the message (usually in XMessage format). This will also apply the instructions in the XMessage.
    '        Main.InstrReceived = message
    '    End Sub
    'End Class
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'DEBUGGING TIPS:
    '1. If an application based on the Application Template does not initially run correctly,
    '    check that the copied methods, such as Main_Load, have the correct Handles statement.
    '    For example: the Main_Load method should have the following declaration: Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
    '      It will not run when the application loads, with this declaration:      Private Sub Main_Load(sender As Object, e As EventArgs)
    '    For example: the Main_FormClosing method should have the following declaration: Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '      It will not run when the application closes, with this declaration:     Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs)
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE Timer1 Control to the Main Form: =====================================================================================
    'Select the Main.vb [Design] tab.
    'Press Toolbox \ Components \ Timer and add Timer1 to the Main form.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'EDIT THE DefaultAppProperties() CODE: ========================================================================================
    'This sets the Application properties that are stored in the Application_Info_ADVL_2.xml settings file.
    'The following properties need to be updated:
    '  ApplicationInfo.Name
    '  ApplicationInfo.Description
    '  ApplicationInfo.CreationDate
    '  ApplicationInfo.Author
    '  ApplicationInfo.Copyright
    '  ApplicationInfo.Trademarks
    '  ApplicationInfo.License
    '  ApplicationInfo.SourceCode          (Optional - Preliminary implemetation coded.)
    '  ApplicationInfo.ModificationSummary (Optional - Preliminary implemetation coded.)
    '  ApplicationInfo.Libraries           (Optional - Preliminary implemetation coded.)
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'ADD THE Application Icon: ====================================================================================================
    'Double-click My Project in the Solution Explorer window to open the project tab.
    'In the Application section press the Icon box and select Browse.
    'Select an application icon.
    'This icon can also be selected for the Main form icon by editing the properties of this form.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'EDIT THE Application Info Text: ==============================================================================================
    'The Application Info Text is used to label the application icon in the Application Network tree view.
    'This is edited in the SendApplicationInfo() method of the Main form.
    'Edit the line of code: Dim text As New XElement("Text", "Application Template").
    'Replace the default text "Application Template" with the required text.
    'Note that this text can be updated at any time and when the updated executable is run, it will update the Application Network tree view the next time it is connected.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Calling JavaScript from VB.NET:
    'The following Imports statement and permissions are required for the Main form:
    'Imports System.Security.Permissions
    '<PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
    '<System.Runtime.InteropServices.ComVisibleAttribute(True)> _
    'NOTE: the line continuation characters (_) will disappear form the code view after they have been typed!
    '------------------------------------------------------------------------------------------------------------------------------
    'Calling VB.NET from JavaScript
    'Add the following line to the Main.Load method:
    '  Me.WebBrowser1.ObjectForScripting = Me
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Adding a Context Menu Strip:
    'In Visual Studio select the tab Main.vb [Design]
    'Select Toolbox \ Menus & Toolbars \ ContextMenuStrip and add it to the form. ContextMenuStrip1 appears in the panel below the form.
    'Right-click ContextMenuStrip1 and select Edit Items...
    'Press Add to add a new menu item
    '  Add item: Name: ToolStripMenuItem1_EditWorkflowTabPage         Text: Edit Workflow Tab Page (Edit the name and text on the right half of the Items Collection Editor.)
    '  Add item: Name: ToolStripMenuItem1_ShowStartPageInWorkflowTab  Text: Show Start Page In Workflow Tab
    'Select the Workflows button on the main form and select ContectMenuStrip property = ContextMenuStrip1
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Edit the AppInfoHtmlString function to display the appropriate information about the application.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'The ADVL_Network_1 application should be running the first time the new application is run.
    'The Network application will automatically send its executable file location to the new application.
    'This will allow the new application to start the Network when required.
    '------------------------------------------------------------------------------------------------------------------------------
    '
    'Other code edits:
    '  Main.Load - Message.AddText("------------------- Starting Application: ADVL Application Template ----------------- " & vbCrLf, "Heading")
    '  Private Sub SendApplicationInfo() - Dim text As New XElement("Text", "Application Template")
    '------------------------------------------------------------------------------------------------------------------------------
    '

#End Region 'Coding Notes ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Variable Declarations - All the variables and class objects used in this form and this application." '===============================================================================

    Public WithEvents ApplicationInfo As New ADVL_Utilities_Library_1.ApplicationInfo 'This object is used to store application information.
    Public WithEvents Project As New ADVL_Utilities_Library_1.Project 'This object is used to store Project information.
    Public WithEvents Message As New ADVL_Utilities_Library_1.Message 'This object is used to display messages in the Messages window.
    Public WithEvents ApplicationUsage As New ADVL_Utilities_Library_1.Usage 'This object stores application usage information.

    'Declare Forms used by the application:
    Public WithEvents WebPageList As frmWebPageList
    Public WithEvents ProjectArchive As frmArchive 'Form used to view the files in a Project archive
    Public WithEvents SettingsArchive As frmArchive 'Form used to view the files in a Settings archive
    Public WithEvents DataArchive As frmArchive 'Form used to view the files in a Data archive
    Public WithEvents SystemArchive As frmArchive 'Form used to view the files in a System archive

    Public WithEvents NewHtmlDisplay As frmHtmlDisplay
    Public HtmlDisplayFormList As New ArrayList 'Used for displaying multiple HtmlDisplay forms.

    Public WithEvents NewWebPage As frmWebPage
    Public WebPageFormList As New ArrayList 'Used for displaying multiple WebView forms.

    Public WithEvents SeriesAnalysis As frmSeriesAnalysis
    Public SeriesAnalysisList As New ArrayList 'Used for displaying multiple Series Analysis forms.

    Public WithEvents RocChart As frmRocChart
    'Public WithEvents RocChartList As New ArrayList 'Used for displaying multiple ROC Charts. NOTE: Currently only one ROC Chart is displayed at once.

    'Declare objects used to connect to the Message Service:
    Public client As ServiceReference1.MsgServiceClient
    Public WithEvents XMsg As New ADVL_Utilities_Library_1.XMessage
    Dim XDoc As New System.Xml.XmlDocument
    Public Status As New System.Collections.Specialized.StringCollection
    Dim ClientProNetName As String = "" 'The name of the client Project Network requesting service. 
    Dim ClientAppName As String = "" 'The name of the client requesting service
    Dim ClientConnName As String = "" 'The name of the client connection requesting service
    Dim MessageXDoc As System.Xml.Linq.XDocument
    Dim xmessage As XElement 'This will contain the message. It will be added to MessageXDoc.
    Dim xlocns As New List(Of XElement) 'A list of locations. Each location forms part of the reply message. The information in the reply message will be sent to the specified location in the client application.
    Dim MessageText As String = "" 'The text of a message sent through the Application Network.

    Public OnCompletionInstruction As String = "Stop" 'The last instruction returned on completion of the processing of an XMessage.
    Public EndInstruction As String = "Stop" 'Another method of specifying the last instruction. This is processed in the EndOfSequence section of XMsg.Instructions.

    Public ConnectionName As String = "" 'The name of the connection used to connect this application to the ComNet (Message Service).

    Public ProNetName As String = "" 'The name of the Project Network
    Public ProNetPath As String = "" 'The path of the Project Network

    Public AdvlNetworkAppPath As String = "" 'The application path of the ADVL Network application (ComNet). This is where the "Application.Lock" file will be while ComNet is running
    Public AdvlNetworkExePath As String = "" 'The executable path of the ADVL Network.

    'Variable for local processing of an XMessage:
    Public WithEvents XMsgLocal As New ADVL_Utilities_Library_1.XMessage
    Dim XDocLocal As New System.Xml.XmlDocument
    Public StatusLocal As New System.Collections.Specialized.StringCollection

    'Main.Load variables:
    Dim ProjectSelected As Boolean = False 'If True, a project has been selected using Command Arguments. Used in Main.Load.
    Dim StartupConnectionName As String = "" 'If not "" the application will be connected to the ComNet using this connection name in  Main.Load.

    'The following variables are used to run JavaScript in Web Pages loaded into the Document View: -------------------
    Public WithEvents XSeq As New ADVL_Utilities_Library_1.XSequence
    'To run an XSequence:
    '  XSeq.RunXSequence(xDoc, Status) 'ImportStatus in Import
    '    Handle events:
    '      XSeq.ErrorMsg
    '      XSeq.Instruction(Info, Locn)

    Private XStatus As New System.Collections.Specialized.StringCollection

    'Variables used to restore Item values on a web page.
    Private FormName As String
    Private ItemName As String
    Private SelectId As String

    'StartProject variables:
    Private StartProject_AppName As String  'The application name
    Private StartProject_ConnName As String 'The connection name
    Private StartProject_ProjID As String   'The project ID
    Private StartProject_ProjName As String ' The project name

    Private WithEvents bgwComCheck As New System.ComponentModel.BackgroundWorker 'Used to perform communication checks on a separate thread.

    Public WithEvents bgwSendMessage As New System.ComponentModel.BackgroundWorker 'Used to send a message through the Message Service.
    Dim SendMessageParams As New clsSendMessageParams 'This holds the Send Message parameters: .ProjectNetworkName, .ConnectionName & .Message

    'Alternative SendMessage background worker - needed to send a message while instructions are being processed.
    Public WithEvents bgwSendMessageAlt As New System.ComponentModel.BackgroundWorker 'Used to send a message through the Message Service - alternative backgound worker.
    Dim SendMessageParamsAlt As New clsSendMessageParams 'This hold the Send Message parameters: .ProjectNetworkName, .ConnectionName & .Message - for the alternative background worker.

    Public WithEvents bgwRunInstruction As New System.ComponentModel.BackgroundWorker 'Used to run a single instruction
    Dim InstructionParams As New clsInstructionParams 'This holds the Info and Locn parameters of an instruction.

    'Dim Bayes As New clsBayes 'Stores a probability model and applies Bayes theorem to update event probabilities.
    'Dim WithEvents Bayes As New clsBayes 'Stores a probability model and applies Bayes theorem to update event probabilities.
    Public WithEvents Bayes As New clsBayes 'Stores a probability model and applies Bayes theorem to update event probabilities.
    'NOTE: Diagram information is now contained in Bayes
    'Dim Diagram As New clsDiagram 'Stores the information required to display the Bayes probability diagram.

    'Dim myImage As New Bitmap(My.Resources.Bayes_Prob_Diag.Width, My.Resources.Bayes_Prob_Diag.Height)

    Public WithEvents BayesSim As New clsBayesSim 'Stores and runs the Bayes Simulation model.

    Public WithEvents SimData As frmTable

#End Region 'Variable Declarations ------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Properties - All the properties used in this form and this application" '============================================================================================================

    Private _movePixels As Integer = 1 'The number of pixels to move labels on the Probability Diagram.
    Property MovePixels As Integer
        Get
            Return _movePixels
        End Get
        Set(value As Integer)
            _movePixels = value
        End Set
    End Property

    Private _confidence As Double = 0.95 'The confidence level used to determine the upper and lower bounds of probability estimates.
    Property Confidence As Double
        Get
            Return _confidence
        End Get
        Set(value As Double)
            _confidence = value
        End Set
    End Property

    'The General Confidence Interval calculator settings: ---------------------------------------------------------------------------------
    Private _genConfidence As Double = 0.95 'The confidence level used in the General Confidence Interval calculator.
    Property GenConfidence As Double
        Get
            Return _genConfidence
        End Get
        Set(value As Double)
            _genConfidence = value
        End Set
    End Property

    Private _genSurveySize As Long = 5000 'The survey size used in the General Confidence Interval calculator.
    Property GenSurveySize As Double
        Get
            Return _genSurveySize
        End Get
        Set(value As Double)
            _genSurveySize = value
            _genNEvent = GenSurveySize * GenMLProbEvent 'Update the _genNEvent value
        End Set
    End Property

    Private _genNEvent As Double = 50 'The survey event count used in the General Confidence Interval calculator.
    Property GenNEvent As Double
        Get
            Return _genNEvent
        End Get
        Set(value As Double)
            _genNEvent = value
            _genMLProbEvent = GenNEvent / GenSurveySize 'Update the _genMLProbEvent value
        End Set
    End Property

    Private _genMLProbEvent As Double = _genNEvent / _genSurveySize 'The Most Likely Probability of the Event in the General Confidence Interval calculator.
    Property GenMLProbEvent As Double
        Get
            Return _genMLProbEvent
        End Get
        Set(value As Double)
            _genMLProbEvent = value
            _genNEvent = GenSurveySize * GenMLProbEvent 'Update the _genNEvent value
        End Set
    End Property

    'NOTE: These properties are stored in BayesSim:
    ''The Event Simulation settings: --------------------------------------------------------------------------------------------------

    'Private _eventSimSurveySize As Long = 5000 'The survey size used in the Event Simulation.
    'Property EventSimSurveySize As Long
    '    Get
    '        Return _eventSimSurveySize
    '    End Get
    '    Set(value As Long)
    '        _eventSimSurveySize = value
    '    End Set
    'End Property

    'Private _eventSimProbEvent As Double = 0.2 'The Event Probability used in the Event Simulation.
    'Property EventSimProbEvent As Double
    '    Get

    '    End Get
    '    Set(value As Double)

    '    End Set
    'End Property

    ''The General Simulation settings: --------------------------------------------------------------------------------------------------

    'Private _simRepeats As Long = 10000 'The number of survey repeats to use in the survey simulation.
    'Property SimRepeats As Long
    '    Get
    '        Return _simRepeats
    '    End Get
    '    Set(value As Long)
    '        _simRepeats = value
    '    End Set
    'End Property

    'Private _simTimeoutSecs As Integer = 60 'The survey simulation timeout period in seconds. The simulation will be terminated after this time if it is still running.
    'Property SimTimeoutSecs As Integer
    '    Get
    '        Return _simTimeoutSecs
    '    End Get
    '    Set(value As Integer)
    '        _simTimeoutSecs = value
    '    End Set
    'End Property

    'Private _simSeed As Integer = -1 'The randomisation seed to used for the survey simulation.
    'Property SimSeed As Integer
    '    Get
    '        Return _simSeed
    '    End Get
    '    Set(value As Integer)
    '        _simSeed = value
    '    End Set
    'End Property
    ''---------------------------------------------------------------------------------------------------------------------------------------

    Private _connectionHashcode As Integer 'The Message Service connection hashcode. This is used to identify a connection in the Message Service when reconnecting.
    Property ConnectionHashcode As Integer
        Get
            Return _connectionHashcode
        End Get
        Set(value As Integer)
            _connectionHashcode = value
        End Set
    End Property

    Private _connectedToComNet As Boolean = False  'True if the application is connected to the Communication Network (Message Service).
    Property ConnectedToComNet As Boolean
        Get
            Return _connectedToComNet
        End Get
        Set(value As Boolean)
            _connectedToComNet = value
        End Set
    End Property

    Private _instrReceived As String = "" 'Contains Instructions received via the Message Service.
    Property InstrReceived As String
        Get
            Return _instrReceived
        End Get
        Set(value As String)
            If value = Nothing Then
                Message.Add("Empty message received!")
            Else
                _instrReceived = value
                ProcessInstructions(_instrReceived)
            End If
        End Set
    End Property

    Private Sub ProcessInstructions(ByVal Instructions As String)
        'Process the XMessage instructions.

        Dim MsgType As String
        If Instructions.StartsWith("<XMsg>") Then
            MsgType = "XMsg"
            If ShowXMessages Then
                'Add the message header to the XMessages window:
                Message.XAddText("Message received: " & vbCrLf, "XmlReceivedNotice")
            End If
        ElseIf Instructions.StartsWith("<XSys>") Then
            MsgType = "XSys"
            If ShowSysMessages Then
                'Add the message header to the XMessages window:
                Message.XAddText("System Message received: " & vbCrLf, "XmlReceivedNotice")
            End If
        Else
            MsgType = "Unknown"
        End If

        If MsgType = "XMsg" Or MsgType = "XSys" Then 'This is an XMessage or XSystem set of instructions.
            Try
                'Inititalise the reply message:
                ClientProNetName = ""
                ClientConnName = ""
                ClientAppName = ""
                xlocns.Clear() 'Clear the list of locations in the reply message. 
                Dim Decl As New XDeclaration("1.0", "utf-8", "yes")
                MessageXDoc = New XDocument(Decl, Nothing) 'Reply message - this will be sent to the Client App.
                xmessage = New XElement(MsgType)
                xlocns.Add(New XElement("Main")) 'Initially set the location in the Client App to Main.

                'Run the received message:
                Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
                XDoc.LoadXml(XmlHeader & vbCrLf & Instructions.Replace("&", "&amp;")) 'Replace "&" with "&amp:" before loading the XML text.

                If (MsgType = "XMsg") And ShowXMessages Then
                    Message.XAddXml(XDoc)  'Add the message to the XMessages window.
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                ElseIf (MsgType = "XSys") And ShowSysMessages Then
                    Message.XAddXml(XDoc)  'Add the message to the XMessages window.
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If

                XMsg.Run(XDoc, Status)
            Catch ex As Exception
                Message.Add("Error running XMsg: " & ex.Message & vbCrLf)
            End Try

            'XMessage has been run.
            'Reply to this message:
            'Add the message reply to the XMessages window:
            'Complete the MessageXDoc:
            xmessage.Add(xlocns(xlocns.Count - 1)) 'Add the last location reply instructions to the message.
            MessageXDoc.Add(xmessage)
            MessageText = MessageXDoc.ToString

            If ClientConnName = "" Then
                'No client to send a message to - process the message locally.

                If (MsgType = "XMsg") And ShowXMessages Then
                    Message.XAddText("Message processed locally:" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                ElseIf (MsgType = "XSys") And ShowSysMessages Then
                    Message.XAddText("System Message processed locally:" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If
                ProcessLocalInstructions(MessageText)
            Else
                If (MsgType = "XMsg") And ShowXMessages Then
                    Message.XAddText("Message sent to [" & ClientProNetName & "]." & ClientConnName & ":" & vbCrLf, "XmlSentNotice")   'NOTE: There is no SendMessage code in the Message Service application!
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                ElseIf (MsgType = "XSys") And ShowSysMessages Then
                    Message.XAddText("System Message sent to [" & ClientProNetName & "]." & ClientConnName & ":" & vbCrLf, "XmlSentNotice")   'NOTE: There is no SendMessage code in the Message Service application!
                    Message.XAddXml(MessageText)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If

                'Send Message on a new thread:
                SendMessageParams.ProjectNetworkName = ClientProNetName
                SendMessageParams.ConnectionName = ClientConnName
                SendMessageParams.Message = MessageText
                If bgwSendMessage.IsBusy Then
                    Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    bgwSendMessage.RunWorkerAsync(SendMessageParams)
                End If
            End If

        Else 'This is not an XMessage!
            If Instructions.StartsWith("<XMsgBlk>") Then 'This is an XMessageBlock.
                'Process the received message:
                Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
                XDoc.LoadXml(XmlHeader & vbCrLf & Instructions.Replace("&", "&amp;")) 'Replace "&" with "&amp:" before loading the XML text.
                If ShowXMessages Then
                    Message.XAddXml(XDoc)   'Add the message to the XMessages window.
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                End If

                'Process the XMessageBlock:
                Dim XMsgBlkLocn As String
                XMsgBlkLocn = XDoc.GetElementsByTagName("ClientLocn")(0).InnerText
                Select Case XMsgBlkLocn
                    Case "TestLocn" 'Replace this with the required location name.
                        Dim XInfo As Xml.XmlNodeList = XDoc.GetElementsByTagName("XInfo") 'Get the XInfo node list
                        Dim InfoXDoc As New Xml.Linq.XDocument 'Create an XDocument to hold the information contained in XInfo 
                        InfoXDoc = XDocument.Parse("<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>" & vbCrLf & XInfo(0).InnerXml) 'Read the information into InfoXDoc
                        'Add processing instructions here 
                        ' The information in the InfoXDoc is usually sent to an XDocument in the application or stored as an XML file in the project.

                    Case Else
                        Message.AddWarning("Unknown XInfo Message location: " & XMsgBlkLocn & vbCrLf)
                End Select
            Else
                Message.XAddText("The message is not an XMessage or XMessageBlock: " & vbCrLf & Instructions & vbCrLf & vbCrLf, "Normal")
            End If
        End If
    End Sub

    Private Sub ProcessLocalInstructions(ByVal Instructions As String)
        'Process the XMessage instructions locally.

        If Instructions.StartsWith("<XMsg>") Or Instructions.StartsWith("<XSys>") Then 'This is an XMessage set of instructions.
            'Run the received message:
            Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
            XDocLocal.LoadXml(XmlHeader & vbCrLf & Instructions)
            XMsgLocal.Run(XDocLocal, StatusLocal)
        Else 'This is not an XMessage!
            Message.XAddText("The message is not an XMessage: " & Instructions & vbCrLf, "Normal")
        End If
    End Sub

    Private _showXMessages As Boolean = True 'If True, XMessages that are sent or received will be shown in the Messages window.
    Property ShowXMessages As Boolean
        Get
            Return _showXMessages
        End Get
        Set(value As Boolean)
            _showXMessages = value
        End Set
    End Property

    Private _showSysMessages As Boolean = True 'If True, System messages that are sent or received will be shown in the messages window.
    Property ShowSysMessages As Boolean
        Get
            Return _showSysMessages
        End Get
        Set(value As Boolean)
            _showSysMessages = value
        End Set
    End Property

    Private _closedFormNo As Integer 'Temporarily holds the number of the form that is being closed. 
    Property ClosedFormNo As Integer
        Get
            Return _closedFormNo
        End Get
        Set(value As Integer)
            _closedFormNo = value
        End Set
    End Property

    Private _workflowFileName As String = "" 'The file name of the html document displayed in the Workflow tab.
    Public Property WorkflowFileName As String
        Get
            Return _workflowFileName
        End Get
        Set(value As String)
            _workflowFileName = value
        End Set
    End Property




#End Region 'Properties -----------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Process XML Files - Read and write XML files." '=====================================================================================================================================

    Private Sub SaveFormSettings()
        'Save the form settings in an XML document.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <!--Form settings for Main form.-->
                           <FormSettings>
                               <Left><%= Me.Left %></Left>
                               <Top><%= Me.Top %></Top>
                               <Width><%= Me.Width %></Width>
                               <Height><%= Me.Height %></Height>
                               <AdvlNetworkAppPath><%= AdvlNetworkAppPath %></AdvlNetworkAppPath>
                               <AdvlNetworkExePath><%= AdvlNetworkExePath %></AdvlNetworkExePath>
                               <ShowXMessages><%= ShowXMessages %></ShowXMessages>
                               <ShowSysMessages><%= ShowSysMessages %></ShowSysMessages>
                               <WorkFlowFileName><%= WorkflowFileName %></WorkFlowFileName>
                               <!---->
                               <SelectedTabIndex><%= TabControl1.SelectedIndex %></SelectedTabIndex>
                               <Split1Distance><%= SplitContainer1.SplitterDistance %></Split1Distance>
                               <Split2Distance><%= SplitContainer2.SplitterDistance %></Split2Distance>
                               <Split3Distance><%= SplitContainer3.SplitterDistance %></Split3Distance>
                               <Split4Distance><%= SplitContainer4.SplitterDistance %></Split4Distance>
                               <Confidence><%= Confidence %></Confidence>
                               <!--General Survey Confidence Interval Calculator Settings.-->
                               <GenConfidence><%= GenConfidence %></GenConfidence>
                               <GenSurveySize><%= GenSurveySize %></GenSurveySize>
                               <GenNEvent><%= GenNEvent %></GenNEvent>
                               <!--Event Simulation Settings.-->
                               <EventSimSurveySize><%= BayesSim.Settings.EventSurveySize %></EventSimSurveySize>
                               <EventSimProbEvent><%= BayesSim.Settings.ProbEvent %></EventSimProbEvent>
                               <!--General Simulation Settings.-->
                               <SimRepeats><%= BayesSim.Settings.SurveyRepeatNo %></SimRepeats>
                               <SimTimeoutSecs><%= BayesSim.Settings.TimeOutSeconds %></SimTimeoutSecs>
                               <SimSeed><%= BayesSim.Settings.Seed %></SimSeed>
                           </FormSettings>

        'Add code to include other settings to save after the comment line <!---->

        Dim SettingsFileName As String = "FormSettings_" & ApplicationInfo.Name & " - Main.xml"
        Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreFormSettings()
        'Read the form settings from an XML document.

        Dim SettingsFileName As String = "FormSettings_" & ApplicationInfo.Name & " - Main.xml"

        If Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore form position and size:
            If Settings.<FormSettings>.<Left>.Value <> Nothing Then Me.Left = Settings.<FormSettings>.<Left>.Value
            If Settings.<FormSettings>.<Top>.Value <> Nothing Then Me.Top = Settings.<FormSettings>.<Top>.Value
            If Settings.<FormSettings>.<Height>.Value <> Nothing Then Me.Height = Settings.<FormSettings>.<Height>.Value
            If Settings.<FormSettings>.<Width>.Value <> Nothing Then Me.Width = Settings.<FormSettings>.<Width>.Value

            If Settings.<FormSettings>.<AdvlNetworkAppPath>.Value <> Nothing Then AdvlNetworkAppPath = Settings.<FormSettings>.<AdvlNetworkAppPath>.Value
            If Settings.<FormSettings>.<AdvlNetworkExePath>.Value <> Nothing Then AdvlNetworkExePath = Settings.<FormSettings>.<AdvlNetworkExePath>.Value

            If Settings.<FormSettings>.<ShowXMessages>.Value <> Nothing Then ShowXMessages = Settings.<FormSettings>.<ShowXMessages>.Value
            If Settings.<FormSettings>.<ShowSysMessages>.Value <> Nothing Then ShowSysMessages = Settings.<FormSettings>.<ShowSysMessages>.Value

            If Settings.<FormSettings>.<WorkFlowFileName>.Value <> Nothing Then WorkflowFileName = Settings.<FormSettings>.<WorkFlowFileName>.Value

            'Add code to read other saved setting here:
            If Settings.<FormSettings>.<SelectedTabIndex>.Value <> Nothing Then TabControl1.SelectedIndex = Settings.<FormSettings>.<SelectedTabIndex>.Value

            If Settings.<FormSettings>.<Split1Distance>.Value <> Nothing Then SplitContainer1.SplitterDistance = Settings.<FormSettings>.<Split1Distance>.Value
            If Settings.<FormSettings>.<Split2Distance>.Value <> Nothing Then SplitContainer2.SplitterDistance = Settings.<FormSettings>.<Split2Distance>.Value
            If Settings.<FormSettings>.<Split3Distance>.Value <> Nothing Then SplitContainer3.SplitterDistance = Settings.<FormSettings>.<Split3Distance>.Value
            If Settings.<FormSettings>.<Split4Distance>.Value <> Nothing Then SplitContainer4.SplitterDistance = Settings.<FormSettings>.<Split4Distance>.Value

            If Settings.<FormSettings>.<Confidence>.Value <> Nothing Then Confidence = Settings.<FormSettings>.<Confidence>.Value

            If Settings.<FormSettings>.<GenConfidence>.Value <> Nothing Then GenConfidence = Settings.<FormSettings>.<GenConfidence>.Value
            If Settings.<FormSettings>.<GenSurveySize>.Value <> Nothing Then GenSurveySize = Settings.<FormSettings>.<GenSurveySize>.Value
            If Settings.<FormSettings>.<GenNEvent>.Value <> Nothing Then
                GenNEvent = Settings.<FormSettings>.<GenNEvent>.Value
                RedisplayGenConfIntVals()
                GenWilsonInterval() 'Calculate the Confidence Interval
            End If

            If Settings.<FormSettings>.<EventSimSurveySize>.Value <> Nothing Then BayesSim.Settings.EventSurveySize = Settings.<FormSettings>.<EventSimSurveySize>.Value
            If Settings.<FormSettings>.<EventSimProbEvent>.Value <> Nothing Then BayesSim.Settings.ProbEvent = Settings.<FormSettings>.<EventSimProbEvent>.Value
            If Settings.<FormSettings>.<SimRepeats>.Value <> Nothing Then BayesSim.Settings.SurveyRepeatNo = Settings.<FormSettings>.<SimRepeats>.Value
            If Settings.<FormSettings>.<SimTimeoutSecs>.Value <> Nothing Then BayesSim.Settings.TimeOutSeconds = Settings.<FormSettings>.<SimTimeoutSecs>.Value
            If Settings.<FormSettings>.<SimSeed>.Value <> Nothing Then BayesSim.Settings.Seed = Settings.<FormSettings>.<SimSeed>.Value


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

    Private Sub ReadApplicationInfo()
        'Read the Application Information.

        If ApplicationInfo.FileExists Then
            ApplicationInfo.ReadFile()
        Else
            'There is no Application_Info_ADVL_2.xml file.
            DefaultAppProperties() 'Create a new Application Info file with default application properties.
            ApplicationInfo.WriteFile() 'Write the file now. The file information may be used by other applications.
        End If
    End Sub

    Private Sub DefaultAppProperties()
        'These properties will be saved in the Application_Info.xml file in the application directory.
        'If this file is deleted, it will be re-created using these default application properties.

        'Change this to show your application Name, Description and Creation Date.
        ApplicationInfo.Name = "ADVL_Bayes"

        'ApplicationInfo.ApplicationDir is set when the application is started.
        ApplicationInfo.ExecutablePath = Application.ExecutablePath

        ApplicationInfo.Description = "The Bayes application demonstrates and applies Bayes theorem."
        ApplicationInfo.CreationDate = "29-May-2021 22:22:00"

        'Author -----------------------------------------------------------------------------------------------------------
        'Change this to show your Name, Description and Contact information.
        ApplicationInfo.Author.Name = "Signalworks Pty Ltd"
        ApplicationInfo.Author.Description = "Signalworks Pty Ltd" & vbCrLf &
            "Australian Proprietary Company" & vbCrLf &
            "ABN 26 066 681 598" & vbCrLf &
            "Registration Date 05/10/1994"

        ApplicationInfo.Author.Contact = "http://www.andorville.com.au/"

        'File Associations: -----------------------------------------------------------------------------------------------
        'Add any file associations here.
        'The file extension and a description of files that can be opened by this application are specified.
        'The example below specifies a coordinate system parameter file type with the file extension .ADVLCoord.
        'Dim Assn1 As New ADVL_System_Utilities.FileAssociation
        'Assn1.Extension = "ADVLCoord"
        'Assn1.Description = "Andorville™ software coordinate system parameter file"
        'ApplicationInfo.FileAssociations.Add(Assn1)

        'Version ----------------------------------------------------------------------------------------------------------
        ApplicationInfo.Version.Major = My.Application.Info.Version.Major
        ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
        ApplicationInfo.Version.Build = My.Application.Info.Version.Build
        ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision

        'Copyright --------------------------------------------------------------------------------------------------------
        'Add your copyright information here.
        ApplicationInfo.Copyright.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.Copyright.PublicationYear = "2021"

        'Trademarks -------------------------------------------------------------------------------------------------------
        'Add your trademark information here.
        Dim Trademark1 As New ADVL_Utilities_Library_1.Trademark
        Trademark1.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark1.Text = "Andorville"
        Trademark1.Registered = False
        Trademark1.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark1)
        Dim Trademark2 As New ADVL_Utilities_Library_1.Trademark
        Trademark2.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark2.Text = "AL-H7"
        Trademark2.Registered = False
        Trademark2.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark2)
        Dim Trademark3 As New ADVL_Utilities_Library_1.Trademark
        Trademark3.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark3.Text = "AL-M7"
        Trademark3.Registered = False
        Trademark3.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark3)
        Dim Trademark4 As New ADVL_Utilities_Library_1.Trademark
        Trademark4.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark4.Text = "AL-S7"
        Trademark4.Registered = False
        Trademark4.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark4)
        Dim Trademark5 As New ADVL_Utilities_Library_1.Trademark
        Trademark5.OwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        Trademark5.Text = "AL-Q7"
        Trademark5.Registered = False
        Trademark5.GenericTerm = "software"
        ApplicationInfo.Trademarks.Add(Trademark5)

        'License -------------------------------------------------------------------------------------------------------
        'Add your license information here.
        ApplicationInfo.License.CopyrightOwnerName = "Signalworks Pty Ltd, ABN 26 066 681 598"
        ApplicationInfo.License.PublicationYear = "2021"

        'License Links:
        'http://choosealicense.com/
        'http://www.apache.org/licenses/
        'http://opensource.org/

        'Apache License 2.0 ---------------------------------------------
        ApplicationInfo.License.Code = ADVL_Utilities_Library_1.License.Codes.Apache_License_2_0
        ApplicationInfo.License.Notice = ApplicationInfo.License.ApacheLicenseNotice 'Get the pre-defined Aapche license notice.
        ApplicationInfo.License.Text = ApplicationInfo.License.ApacheLicenseText     'Get the pre-defined Apache license text.

        'Code to use other pre-defined license types is shown below:

        'GNU General Public License, version 3 --------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.GNU_GPL_V3_0
        'ApplicationInfo.License.Notice = 'Add the License Notice to ADVL_Utilities_Library_1 License class.
        'ApplicationInfo.License.Text = 'Add the License Text to ADVL_Utilities_Library_1 License class.

        'The MIT License ------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.MIT_License
        'ApplicationInfo.License.Notice = ApplicationInfo.License.MITLicenseNotice
        'ApplicationInfo.License.Text = ApplicationInfo.License.MITLicenseText

        'No License Specified -------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.None
        'ApplicationInfo.License.Notice = ""
        'ApplicationInfo.License.Text = ""

        'The Unlicense --------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.The_Unlicense
        'ApplicationInfo.License.Notice = ApplicationInfo.License.UnLicenseNotice
        'ApplicationInfo.License.Text = ApplicationInfo.License.UnLicenseText

        'Unknown License ------------------------------------------------
        'ApplicationInfo.License.Type = ADVL_Utilities_Library_1.License.Types.Unknown
        'ApplicationInfo.License.Notice = ""
        'ApplicationInfo.License.Text = ""

        'Source Code: --------------------------------------------------------------------------------------------------
        'Add your source code information here if required.
        'THIS SECTION WILL BE UPDATED TO ALLOW A GITHUB LINK.
        ApplicationInfo.SourceCode.Language = "Visual Basic 2019"
        ApplicationInfo.SourceCode.FileName = ""
        ApplicationInfo.SourceCode.FileSize = 0
        ApplicationInfo.SourceCode.FileHash = ""
        ApplicationInfo.SourceCode.WebLink = ""
        ApplicationInfo.SourceCode.Contact = ""
        ApplicationInfo.SourceCode.Comments = ""

        'ModificationSummary: -----------------------------------------------------------------------------------------
        'Add any source code modification here is required.
        ApplicationInfo.ModificationSummary.BaseCodeName = ""
        ApplicationInfo.ModificationSummary.BaseCodeDescription = ""
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Major = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Minor = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Build = 0
        ApplicationInfo.ModificationSummary.BaseCodeVersion.Revision = 0
        ApplicationInfo.ModificationSummary.Description = "This is the first released version of the application. No earlier base code used."

        'Library List: ------------------------------------------------------------------------------------------------
        'Add the ADVL_Utilties_Library_1 library:
        Dim NewLib As New ADVL_Utilities_Library_1.LibrarySummary
        NewLib.Name = "ADVL_System_Utilities"
        NewLib.Description = "System Utility classes used in Andorville™ software development system applications"
        NewLib.CreationDate = "7-Jan-2016 12:00:00"
        NewLib.LicenseNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598" & vbCrLf &
                               vbCrLf &
                               "Licensed under the Apache License, Version 2.0 (the ""License"");" & vbCrLf &
                               "you may not use this file except in compliance with the License." & vbCrLf &
                               "You may obtain a copy of the License at" & vbCrLf &
                               vbCrLf &
                               "http://www.apache.org/licenses/LICENSE-2.0" & vbCrLf &
                               vbCrLf &
                               "Unless required by applicable law or agreed to in writing, software" & vbCrLf &
                               "distributed under the License is distributed on an ""AS IS"" BASIS," & vbCrLf &
                               "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied." & vbCrLf &
                               "See the License for the specific language governing permissions and" & vbCrLf &
                               "limitations under the License." & vbCrLf

        NewLib.CopyrightNotice = "Copyright 2016 Signalworks Pty Ltd, ABN 26 066 681 598"

        NewLib.Version.Major = 1
        NewLib.Version.Minor = 0
        'NewLib.Version.Build = 1
        NewLib.Version.Build = 0
        NewLib.Version.Revision = 0

        NewLib.Author.Name = "Signalworks Pty Ltd"
        NewLib.Author.Description = "Signalworks Pty Ltd" & vbCrLf &
            "Australian Proprietary Company" & vbCrLf &
            "ABN 26 066 681 598" & vbCrLf &
            "Registration Date 05/10/1994"

        NewLib.Author.Contact = "http://www.andorville.com.au/"

        Dim NewClass1 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass1.Name = "ZipComp"
        NewClass1.Description = "The ZipComp class is used to compress files into and extract files from a zip file."
        NewLib.Classes.Add(NewClass1)
        Dim NewClass2 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass2.Name = "XSequence"
        NewClass2.Description = "The XSequence class is used to run an XML property sequence (XSequence) file. XSequence files are used to record and replay processing sequences in Andorville™ software applications."
        NewLib.Classes.Add(NewClass2)
        Dim NewClass3 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass3.Name = "XMessage"
        NewClass3.Description = "The XMessage class is used to read an XML Message (XMessage). An XMessage is a simplified XSequence used to exchange information between Andorville™ software applications."
        NewLib.Classes.Add(NewClass3)
        Dim NewClass4 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass4.Name = "Location"
        NewClass4.Description = "The Location class consists of properties and methods to store data in a location, which is either a directory or archive file."
        NewLib.Classes.Add(NewClass4)
        Dim NewClass5 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass5.Name = "Project"
        NewClass5.Description = "An Andorville™ software application can store data within one or more projects. Each project stores a set of related data files. The Project class contains properties and methods used to manage a project."
        NewLib.Classes.Add(NewClass5)
        Dim NewClass6 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass6.Name = "ProjectSummary"
        NewClass6.Description = "ProjectSummary stores a summary of a project."
        NewLib.Classes.Add(NewClass6)
        Dim NewClass7 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass7.Name = "DataFileInfo"
        NewClass7.Description = "The DataFileInfo class stores information about a data file."
        NewLib.Classes.Add(NewClass7)
        Dim NewClass8 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass8.Name = "Message"
        NewClass8.Description = "The Message class contains text properties and methods used to display messages in an Andorville™ software application."
        NewLib.Classes.Add(NewClass8)
        Dim NewClass9 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass9.Name = "ApplicationSummary"
        NewClass9.Description = "The ApplicationSummary class stores a summary of an Andorville™ software application."
        NewLib.Classes.Add(NewClass9)
        Dim NewClass10 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass10.Name = "LibrarySummary"
        NewClass10.Description = "The LibrarySummary class stores a summary of a software library used by an application."
        NewLib.Classes.Add(NewClass10)
        Dim NewClass11 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass11.Name = "ClassSummary"
        NewClass11.Description = "The ClassSummary class stores a summary of a class contained in a software library."
        NewLib.Classes.Add(NewClass11)
        Dim NewClass12 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass12.Name = "ModificationSummary"
        NewClass12.Description = "The ModificationSummary class stores a summary of any modifications made to an application or library."
        NewLib.Classes.Add(NewClass12)
        Dim NewClass13 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass13.Name = "ApplicationInfo"
        NewClass13.Description = "The ApplicationInfo class stores information about an Andorville™ software application."
        NewLib.Classes.Add(NewClass13)
        Dim NewClass14 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass14.Name = "Version"
        NewClass14.Description = "The Version class stores application, library or project version information."
        NewLib.Classes.Add(NewClass14)
        Dim NewClass15 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass15.Name = "Author"
        NewClass15.Description = "The Author class stores information about an Author."
        NewLib.Classes.Add(NewClass15)
        Dim NewClass16 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass16.Name = "FileAssociation"
        NewClass16.Description = "The FileAssociation class stores the file association extension and description. An application can open files on its file association list."
        NewLib.Classes.Add(NewClass16)
        Dim NewClass17 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass17.Name = "Copyright"
        NewClass17.Description = "The Copyright class stores copyright information."
        NewLib.Classes.Add(NewClass17)
        Dim NewClass18 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass18.Name = "License"
        NewClass18.Description = "The License class stores license information."
        NewLib.Classes.Add(NewClass18)
        Dim NewClass19 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass19.Name = "SourceCode"
        NewClass19.Description = "The SourceCode class stores information about the source code for the application."
        NewLib.Classes.Add(NewClass19)
        Dim NewClass20 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass20.Name = "Usage"
        NewClass20.Description = "The Usage class stores information about application or project usage."
        NewLib.Classes.Add(NewClass20)
        Dim NewClass21 As New ADVL_Utilities_Library_1.ClassSummary
        NewClass21.Name = "Trademark"
        NewClass21.Description = "The Trademark class stored information about a trademark used by the author of an application or data."
        NewLib.Classes.Add(NewClass21)

        ApplicationInfo.Libraries.Add(NewLib)

        'Add other library information here: --------------------------------------------------------------------------

    End Sub

    'Save the form settings if the form is being minimised:
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &H112 Then 'SysCommand
            If m.WParam.ToInt32 = &HF020 Then 'Form is being minimised
                SaveFormSettings()
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub SaveProjectSettings()
        'Save the project settings in an XML file.
        'Add any Project Settings to be saved into the settingsData XDocument.
        Dim settingsData = <?xml version="1.0" encoding="utf-8"?>
                           <!---->
                           <!--Project settings for ADVL_Coordinates_1 application.-->
                           <ProjectSettings>
                           </ProjectSettings>

        Dim SettingsFileName As String = "ProjectSettings_" & ApplicationInfo.Name & "_" & ".xml"
        Project.SaveXmlSettings(SettingsFileName, settingsData)
    End Sub

    Private Sub RestoreProjectSettings()
        'Restore the project settings from an XML document.

        Dim SettingsFileName As String = "ProjectSettings_" & ApplicationInfo.Name & "_" & ".xml"

        If Project.SettingsFileExists(SettingsFileName) Then
            Dim Settings As System.Xml.Linq.XDocument
            Project.ReadXmlSettings(SettingsFileName, Settings)

            If IsNothing(Settings) Then 'There is no Settings XML data.
                Exit Sub
            End If

            'Restore a Project Setting example:
            If Settings.<ProjectSettings>.<Setting1>.Value = Nothing Then
                'Project setting not saved.
                'Setting1 = ""
            Else
                'Setting1 = Settings.<ProjectSettings>.<Setting1>.Value
            End If

            'Continue restoring saved settings.

        End If

    End Sub

#End Region 'Process XML Files ----------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Display Methods - Code used to display this form." '============================================================================================================================

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Loading the Main form.

        'Set the Application Directory path: ------------------------------------------------
        Project.ApplicationDir = My.Application.Info.DirectoryPath.ToString

        'Read the Application Information file: ---------------------------------------------
        ApplicationInfo.ApplicationDir = My.Application.Info.DirectoryPath.ToString 'Set the Application Directory property

        ''Get the Application Version Information:
        'ApplicationInfo.Version.Major = My.Application.Info.Version.Major
        'ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
        'ApplicationInfo.Version.Build = My.Application.Info.Version.Build
        'ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision

        If ApplicationInfo.ApplicationLocked Then
            MessageBox.Show("The application is locked. If the application is not already in use, remove the 'Application_Info.lock file from the application directory: " & ApplicationInfo.ApplicationDir, "Notice", MessageBoxButtons.OK)
            Dim dr As System.Windows.Forms.DialogResult
            dr = MessageBox.Show("Press 'Yes' to unlock the application", "Notice", MessageBoxButtons.YesNo)
            If dr = System.Windows.Forms.DialogResult.Yes Then
                ApplicationInfo.UnlockApplication()
            Else
                Application.Exit()
                Exit Sub
            End If
        End If

        ReadApplicationInfo()

        'Read the Application Usage information: --------------------------------------------
        ApplicationUsage.StartTime = Now
        ApplicationUsage.SaveLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory
        ApplicationUsage.SaveLocn.Path = Project.ApplicationDir
        ApplicationUsage.RestoreUsageInfo()

        'Restore Project information: -------------------------------------------------------
        Project.Application.Name = ApplicationInfo.Name

        'Set up Message object:
        Message.ApplicationName = ApplicationInfo.Name

        'Set up a temporary initial settings location:
        Dim TempLocn As New ADVL_Utilities_Library_1.FileLocation
        TempLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory
        TempLocn.Path = ApplicationInfo.ApplicationDir
        Message.SettingsLocn = TempLocn

        Me.Show() 'Show this form before showing the Message form - This will show the App icon on top in the TaskBar.

        'Start showing messages here - Message system is set up.
        Message.AddText("------------------- Starting Application: ADVL Bayes ------------------------ " & vbCrLf, "Heading")
        'Message.AddText("Application usage: Total duration = " & Format(ApplicationUsage.TotalDuration.TotalHours, "#.##") & " hours" & vbCrLf, "Normal")
        Dim TotalDuration As String = ApplicationUsage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                           ApplicationUsage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                           ApplicationUsage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                           ApplicationUsage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"
        Message.AddText("Application usage: Total duration = " & TotalDuration & vbCrLf, "Normal")

        'https://msdn.microsoft.com/en-us/library/z2d603cy(v=vs.80).aspx#Y550
        'Process any command line arguments:
        Try
            For Each s As String In My.Application.CommandLineArgs
                Message.Add("Command line argument: " & vbCrLf)
                Message.AddXml(s & vbCrLf & vbCrLf)
                InstrReceived = s
            Next
        Catch ex As Exception
            Message.AddWarning("Error processing command line arguments: " & ex.Message & vbCrLf)
        End Try

        If ProjectSelected = False Then
            'Read the Settings Location for the last project used:
            Project.ReadLastProjectInfo()
            'The Last_Project_Info.xml file contains:
            '  Project Name and Description. Settings Location Type and Settings Location Path.
            Message.Add("Last project details:" & vbCrLf)
            Message.Add("Project Type:  " & Project.Type.ToString & vbCrLf)
            Message.Add("Project Path:  " & Project.Path & vbCrLf)

            'At this point read the application start arguments, if any.
            'The selected project may be changed here.

            'Check if the project is locked:
            If Project.ProjectLocked Then
                Message.AddWarning("The project is locked: " & Project.Name & vbCrLf)
                Dim dr As System.Windows.Forms.DialogResult
                dr = MessageBox.Show("Press 'Yes' to unlock the project", "Notice", MessageBoxButtons.YesNo)
                If dr = System.Windows.Forms.DialogResult.Yes Then
                    Project.UnlockProject()
                    Message.AddWarning("The project has been unlocked: " & Project.Name & vbCrLf)
                    'Read the Project Information file: -------------------------------------------------
                    Message.Add("Reading project info." & vbCrLf)
                    Project.ReadProjectInfoFile()                 'Read the file in the SettingsLocation: ADVL_Project_Info.xml
                    Project.ReadParameters()
                    Project.ReadParentParameters()
                    If Project.ParentParameterExists("ProNetName") Then
                        Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
                        ProNetName = Project.Parameter("ProNetName").Value
                    Else
                        ProNetName = Project.GetParameter("ProNetName")
                    End If
                    If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
                        Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
                        ProNetPath = Project.Parameter("ProNetPath").Value
                    Else
                        ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
                    End If
                    Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

                    Project.LockProject() 'Lock the project while it is open in this application.
                    'Set the project start time. This is used to track project usage.
                    Project.Usage.StartTime = Now
                    ApplicationInfo.SettingsLocn = Project.SettingsLocn
                    'Set up the Message object:
                    Message.SettingsLocn = Project.SettingsLocn
                    Message.Show()
                Else
                    'Continue without any project selected.
                    Project.Name = ""
                    Project.Type = ADVL_Utilities_Library_1.Project.Types.None
                    Project.Description = ""
                    Project.SettingsLocn.Path = ""
                    Project.DataLocn.Path = ""
                End If
            Else
                'Read the Project Information file: -------------------------------------------------
                Message.Add("Reading project info." & vbCrLf)
                Project.ReadProjectInfoFile()  'Read the file in the Project Location: ADVL_Project_Info.xml
                Project.ReadParameters()
                Project.ReadParentParameters()
                If Project.ParentParameterExists("ProNetName") Then
                    Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
                    ProNetName = Project.Parameter("ProNetName").Value
                Else
                    ProNetName = Project.GetParameter("ProNetName")
                End If
                If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
                    Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
                    ProNetPath = Project.Parameter("ProNetPath").Value
                Else
                    ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
                End If
                Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

                Project.LockProject() 'Lock the project while it is open in this application.
                'Set the project start time. This is used to track project usage.
                Project.Usage.StartTime = Now
                ApplicationInfo.SettingsLocn = Project.SettingsLocn
                'Set up the Message object:
                Message.SettingsLocn = Project.SettingsLocn
                Message.Show() 'Added 18May19
            End If

        Else  'Project has been opened using Command Line arguments.
            Project.ReadParameters()
            Project.ReadParentParameters()
            If Project.ParentParameterExists("ProNetName") Then
                Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
                ProNetName = Project.Parameter("ProNetName").Value
            Else
                ProNetName = Project.GetParameter("ProNetName")
            End If
            If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
                Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
                ProNetPath = Project.Parameter("ProNetPath").Value
            Else
                ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
            End If
            Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

            Project.LockProject() 'Lock the project while it is open in this application.
            ProjectSelected = False 'Reset the Project Selected flag.

            'Set up the Message object:
            Message.SettingsLocn = Project.SettingsLocn
            Message.Show() 'Added 18May19
        End If

        'START Initialise the form: ===============================================================

        Me.WebBrowser1.ObjectForScripting = Me
        'IF THE LINE ABOVE PRODUCES AN ERROR ON STARTUP, CHECK THAT THE CODE ON THE FOLLOWING THREE LINES IS INSERTED JUST ABOVE THE Public Class Main STATEMENT.
        'Imports System.Security.Permissions
        '<PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
        '<System.Runtime.InteropServices.ComVisibleAttribute(True)>

        bgwSendMessage.WorkerReportsProgress = True
        bgwSendMessage.WorkerSupportsCancellation = True

        bgwSendMessageAlt.WorkerReportsProgress = True
        bgwSendMessageAlt.WorkerSupportsCancellation = True

        bgwRunInstruction.WorkerReportsProgress = True
        bgwRunInstruction.WorkerSupportsCancellation = True

        'cmbEventAShape.Items.Add("Circle")
        cmbEventAShape.Items.Add("Ellipse")
        cmbEventAShape.Items.Add("Rectangle")
        cmbEventAShape.SelectedIndex = 0

        'cmbEventBShape.Items.Add("Circle")
        cmbEventBShape.Items.Add("Ellipse")
        cmbEventBShape.Items.Add("Rectangle")
        cmbEventBShape.SelectedIndex = 0

        cmbInputInfo.Items.Add("P(B|A), P(A), P(B)")
        'cmbInputInfo.Items.Add("P(B|A), P(A), P(B|~A)")
        cmbInputInfo.Items.Add("P(B|A), P(A), P(B|NotA)")
        cmbInputInfo.Items.Add("Sample Counts (TP, TN, FP, FN)")
        cmbInputInfo.Items.Add("Sample Size")
        cmbInputInfo.SelectedIndex = 0

        trvEventA.ImageList = ImageList1
        trvEventB.ImageList = ImageList1
        DrawEventATree()
        DrawEventBTree()


        'pbVenn.Image = ImageList1.Images(0) 'Image too small and too low resolution when stretched. ImageList maximum size is 256x256.
        'pbVenn.Image = My.Resources.Bayes_Prob_Diag

        'Show the Probability Diagram settings:
        'txtLineColor.BackColor = Diagram.LineColor
        txtLineColor.BackColor = Bayes.Diagram.LineColor
        'txtFillColor.BackColor = Diagram.FillColor
        txtFillColor.BackColor = Bayes.Diagram.FillColor
        'txtLineThickness.Text = Diagram.LineThickness
        txtLineThickness.Text = Bayes.Diagram.LineThickness
        'txtBoldLineThickness.Text = Diagram.BoldLineThickness
        txtBoldLineThickness.Text = Bayes.Diagram.BoldLineThickness
        'chkBoldLine.Checked = Diagram.BoldLine
        chkBoldLine.Checked = Bayes.Diagram.BoldLine
        'txtWidth.Text = Diagram.Width
        txtWidth.Text = Bayes.Diagram.Width
        'txtHeight.Text = Diagram.Height
        txtHeight.Text = Bayes.Diagram.Height

        'txtEventALineColor.BackColor = Diagram.EventALineColor
        'txtEventALineColor.BackColor = Bayes.EventALineColor
        txtEventALineColor.BackColor = Bayes.EventA.LineColor
        'txtEventAFillColor.BackColor = Diagram.EventAFillColor
        'txtEventAFillColor.BackColor = Bayes.EventAFillColor
        txtEventAFillColor.BackColor = Bayes.EventA.FillColor
        'txtEventALineThickness.Text = Diagram.EventALineThickness
        txtEventALineThickness.Text = Bayes.EventA.LineThickness
        'txtEventABoldLineThickness.Text = Diagram.EventABoldLineThickness
        txtEventABoldLineThickness.Text = Bayes.EventA.BoldLineThickness
        'chkEventABoldLine.Checked = Diagram.EventABoldLine
        chkEventABoldLine.Checked = Bayes.EventA.BoldLine
        'cmbEventAShape.SelectedIndex = cmbEventAShape.FindStringExact(Diagram.EventAShape)
        cmbEventAShape.SelectedIndex = cmbEventAShape.FindStringExact(Bayes.EventA.Shape)

        'txtEventAXMin.Text = Diagram.EventAXMin
        txtEventAXMin.Text = Bayes.EventA.Unscaled.XMin
        'txtEventAXMax.Text = Diagram.EventAXMax
        txtEventAXMax.Text = Bayes.EventA.Unscaled.XMax
        'txtEventAYMin.Text = Diagram.EventAYMin
        txtEventAYMin.Text = Bayes.EventA.Unscaled.YMin
        'txtEventAYMax.Text = Diagram.EventAYMax
        txtEventAYMax.Text = Bayes.EventA.Unscaled.YMax

        'txtEventBLineColor.BackColor = Diagram.EventBLineColor
        txtEventBLineColor.BackColor = Bayes.EventB.LineColor
        'txtEventBFillColor.BackColor = Diagram.EventBFillColor
        txtEventBFillColor.BackColor = Bayes.EventB.FillColor
        'txtEventBLineThickness.Text = Diagram.EventBLineThickness
        txtEventBLineThickness.Text = Bayes.EventB.LineThickness
        'txtEventBBoldLineThickness.Text = Diagram.EventBBoldLineThickness
        txtEventBBoldLineThickness.Text = Bayes.EventB.BoldLineThickness
        'chkEventBBoldLine.Checked = Diagram.EventBBoldLine
        chkEventBBoldLine.Checked = Bayes.EventB.BoldLine
        'cmbEventBShape.SelectedIndex = cmbEventBShape.FindStringExact(Diagram.EventBShape)
        cmbEventBShape.SelectedIndex = cmbEventBShape.FindStringExact(Bayes.EventB.Shape)

        'txtEventBXMin.Text = Diagram.EventBXMin
        txtEventBXMin.Text = Bayes.EventB.Unscaled.XMin
        'txtEventBXMax.Text = Diagram.EventBXMax
        txtEventBXMax.Text = Bayes.EventB.Unscaled.XMax
        'txtEventBYMin.Text = Diagram.EventBYMin
        txtEventBYMin.Text = Bayes.EventB.Unscaled.YMin
        'txtEventBYMax.Text = Diagram.EventBYMax
        txtEventBYMax.Text = Bayes.EventB.Unscaled.YMax

        'txtEventAandBFillColor.BackColor = Diagram.EventAandBFillColor
        txtEventAandBFillColor.BackColor = Bayes.EventAandBFillColor

        rbNone.Checked = True
        'txtHighlightColor.BackColor = Diagram.HighlightRegionColor
        txtHighlightColor.BackColor = Bayes.HighlightRegion.Color
        'txtZeroProbColor.BackColor = Diagram.ZeroProbabilityColor
        txtZeroProbColor.BackColor = Bayes.ZeroProbRegion.Color

        rbUnscaled.Checked = True

        rbConditionNone.Checked = True 'Select No Conditions

        DrawDiagram() 'Draw the Bayes probability diagram.

        cmbImageFormat.Items.Add("Jpeg")
        cmbImageFormat.Items.Add("Png")
        cmbImageFormat.Items.Add("Bmp")
        'cmbImageFormat.Items.Add("Emf") 'System.ArgumentNullException: 'Value cannot be null.  Parameter Name: encoder'
        'cmbImageFormat.Items.Add("Exif") 'System.ArgumentNullException: 'Value cannot be null.  Parameter Name: encoder'
        cmbImageFormat.Items.Add("Gif")
        'cmbImageFormat.Items.Add("Icon") 'System.ArgumentNullException: 'Value cannot be null.  Parameter Name: encoder'
        cmbImageFormat.Items.Add("Tiff")
        'cmbImageFormat.Items.Add("Wmf") 'System.ArgumentNullException: 'Value cannot be null.  Parameter Name: encoder'
        cmbImageFormat.SelectedIndex = 0

        cmbSimImageFormat.Items.Add("Jpeg")
        cmbSimImageFormat.Items.Add("Png")
        cmbSimImageFormat.Items.Add("Bmp")
        cmbSimImageFormat.Items.Add("Gif")
        cmbSimImageFormat.Items.Add("Tiff")
        cmbSimImageFormat.SelectedIndex = 0

        'The simulation info is now displayed after RestoreFormSettings()
        'txtEventSimSurveySize.Text = BayesSim.Settings.EventSurveySize
        'txtSimPEvent.Text = BayesSim.Settings.FormattedProbEvent
        'txtSimRepeats.Text = BayesSim.Settings.SurveyRepeatNo
        'txtTimeOutSecs.Text = BayesSim.Settings.TimeOutSeconds
        'txtSeed.Text = BayesSim.Settings.Seed

        BayesSim.AreaNotAandNotB.FillColor = Color.LightGoldenrodYellow
        BayesSim.AreaNotAandNotB.LineColor = Color.Black

        BayesSim.AreaNotAandB.FillColor = Color.LightCyan
        BayesSim.AreaNotAandB.LineColor = Color.Blue

        BayesSim.AreaAandB.FillColor = Color.Thistle
        BayesSim.AreaAandB.LineColor = Color.Purple

        BayesSim.AreaAandNotB.FillColor = Color.MistyRose
        BayesSim.AreaAandNotB.LineColor = Color.Red
        txtTestName.Text = "Diagnostic Test"
        txtPointLabel.Text = "Diagnostic Test"
        txtTP.Text = "100"
        txtTN.Text = "880"
        txtFP.Text = "10"
        txtFN.Text = "10"
        rbEnterSurvey.Checked = True 'Default setting - Enter the Survey Results in the Performance Metric calculator.
        CalcMetrics()

        'Get a list of Color names:
        For Each Color As KnownColor In [Enum].GetValues(GetType(KnownColor))
            'cmbRocColor.Items.Add([Enum].GetName(GetType(KnownColor), Color)) 'ActiveBorder to MenuHighlight - Includes system color names
            If Color > 27 And Color < 168 Then
                cmbCalcRocColor.Items.Add([Enum].GetName(GetType(KnownColor), Color)) 'AliceBlue to YellowGreen - System color names not included.
                cmbRocColor.Items.Add([Enum].GetName(GetType(KnownColor), Color))
            End If
        Next
        cmbCalcRocColor.SelectedIndex = cmbCalcRocColor.FindStringExact("Red")
        cmbRocColor.SelectedIndex = cmbRocColor.FindStringExact("Red")

        'rbEnterSurvey.Checked = True 'Default setting - Enter the Survey Results in the Performance Metric calculator.'MOVED UP

        'txtPixels.Text = MovePixels 'Set the default amount to move labels on the Probability Display.
        numPixels.Value = MovePixels 'Set the default amount to move labels on the Probability Display.
        numPixels.Minimum = 1
        numPixels.Increment = 1

        'Options for moving labels on the Probability Diagram:
        chkSelProbSamp.Checked = True 'Select corresponding Probability and Sample Caount lables when one is selected.
        chkUncondLabel.Checked = True 'Move the unconditional version of the label
        chkGivenALabel.Checked = True 'Move the Given A conditional version of the label
        chkGivenNotALabel.Checked = True 'Move the Given Not A conditional version of the label
        chkGivenBLabel.Checked = True 'Move the Given B conditional version of the label
        chkGivenNotBLabel.Checked = True 'Move the Given Not B conditional version of the label

        txtConfidence.Text = ProbString(Confidence)

        'Default General Confidence Interval settings:
        txtGenConfid.Text = ProbString(GenConfidence) 'The initial Confidence value to use in the General Confidence Interval calculator.
        txtGenSurveySize.Text = SampString(GenSurveySize) 'The initial Survey Size value to use in the General Confidence Interval calculator.
        txtGenNEvent.Text = SampString(GenNEvent) 'The initial Survey Event Count to use in the General Confidence Interval calculator.
        txtPMLEvent.Text = ProbString(GenMLProbEvent) 'The initial Most Likely Event Probability to use in the General Confidence Interval calculator.
        GenWilsonInterval() 'Calculate the Confidence Interval.


        InitialiseForm() 'Initialise the form for a new project.

        'END   Initialise the form: ---------------------------------------------------------------

        RestoreFormSettings() 'Restore the form settings
        OpenStartPage()
        Message.ShowXMessages = ShowXMessages
        Message.ShowSysMessages = ShowSysMessages
        RestoreProjectSettings() 'Restore the Project settings

        ShowProjectInfo() 'Show the project information.

        txtEventSimSurveySize.Text = BayesSim.Settings.EventSurveySize
        txtSimPEvent.Text = ProbString(BayesSim.Settings.ProbEvent)
        txtSimRepeats.Text = BayesSim.Settings.SurveyRepeatNo
        txtTimeOutSecs.Text = BayesSim.Settings.TimeOutSeconds
        txtSeed.Text = BayesSim.Settings.Seed

        Message.AddText("------------------- Started OK -------------------------------------------------------------------------- " & vbCrLf & vbCrLf, "Heading")

        If StartupConnectionName = "" Then
            If Project.ConnectOnOpen Then
                ConnectToComNet() 'The Project is set to connect when it is opened.
            ElseIf ApplicationInfo.ConnectOnStartup Then
                ConnectToComNet() 'The Application is set to connect when it is started.
            Else
                'Don't connect to ComNet.
            End If
        Else
            'Connect to ComNet using the connection name StartupConnectionName.
            ConnectToComNet(StartupConnectionName)
        End If

        'Get the Application Version Information:
        If System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed Then
            'Application is network deployed.
            ApplicationInfo.Version.Number = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
            ApplicationInfo.Version.Major = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Major
            ApplicationInfo.Version.Minor = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Minor
            ApplicationInfo.Version.Build = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Build
            ApplicationInfo.Version.Revision = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Revision
            ApplicationInfo.Version.Source = "Publish"
            Message.Add("Application version: " & System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString & vbCrLf)
        Else
            'Application is not network deployed.
            ApplicationInfo.Version.Number = My.Application.Info.Version.ToString
            ApplicationInfo.Version.Major = My.Application.Info.Version.Major
            ApplicationInfo.Version.Minor = My.Application.Info.Version.Minor
            ApplicationInfo.Version.Build = My.Application.Info.Version.Build
            ApplicationInfo.Version.Revision = My.Application.Info.Version.Revision
            ApplicationInfo.Version.Source = "Assembly"
            Message.Add("Application version: " & My.Application.Info.Version.ToString & vbCrLf)
        End If

    End Sub

    Private Sub InitialiseForm()
        'Initialise the form for a new project.
        'OpenStartPage()
    End Sub

    Private Sub ShowProjectInfo()
        'Show the project information:

        txtParentProject.Text = Project.ParentProjectName
        txtProNetName.Text = Project.GetParameter("ProNetName")
        txtProjectName.Text = Project.Name
        txtProjectDescription.Text = Project.Description
        Select Case Project.Type
            Case ADVL_Utilities_Library_1.Project.Types.Directory
                txtProjectType.Text = "Directory"
            Case ADVL_Utilities_Library_1.Project.Types.Archive
                txtProjectType.Text = "Archive"
            Case ADVL_Utilities_Library_1.Project.Types.Hybrid
                txtProjectType.Text = "Hybrid"
            Case ADVL_Utilities_Library_1.Project.Types.None
                txtProjectType.Text = "None"
        End Select

        txtCreationDate.Text = Format(Project.Usage.FirstUsed, "d-MMM-yyyy H:mm:ss")
        txtLastUsed.Text = Format(Project.Usage.LastUsed, "d-MMM-yyyy H:mm:ss")

        txtProjectPath.Text = Project.Path

        Select Case Project.SettingsLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtSettingsLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtSettingsLocationType.Text = "Archive"
        End Select
        txtSettingsPath.Text = Project.SettingsLocn.Path

        Select Case Project.DataLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtDataLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtDataLocationType.Text = "Archive"
        End Select
        txtDataPath.Text = Project.DataLocn.Path

        Select Case Project.SystemLocn.Type
            Case ADVL_Utilities_Library_1.FileLocation.Types.Directory
                txtSystemLocationType.Text = "Directory"
            Case ADVL_Utilities_Library_1.FileLocation.Types.Archive
                txtSystemLocationType.Text = "Archive"
        End Select
        txtSystemPath.Text = Project.SystemLocn.Path

        If Project.ConnectOnOpen Then
            chkConnect.Checked = True
        Else
            chkConnect.Checked = False
        End If

        'txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                        Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                        Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                        Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c)

        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                          Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                          Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                          Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                  Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                  Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                  Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'Exit the Application

        DisconnectFromComNet() 'Disconnect from the Communication Network (Message Service).

        SaveProjectSettings() 'Save project settings.

        ApplicationInfo.WriteFile() 'Update the Application Information file.

        Project.SaveLastProjectInfo() 'Save information about the last project used.

        Project.SaveParameters()

        'Project.SaveProjectInfoFile() 'Update the Project Information file. This is not required unless there is a change made to the project.

        Project.Usage.SaveUsageInfo() 'Save Project usage information.

        Project.UnlockProject() 'Unlock the project.

        ApplicationUsage.SaveUsageInfo() 'Save Application usage information.
        ApplicationInfo.UnlockApplication()

        Application.Exit()

    End Sub

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Save the form settings if the form state is normal. (A minimised form will have the incorrect size and location.)
        If WindowState = FormWindowState.Normal Then
            SaveFormSettings()
        End If
    End Sub


#End Region 'Form Display Methods -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Open and Close Forms - Code used to open and close other forms." '===================================================================================================================

    'Private Sub btnOpenTemplateForm_Click(sender As Object, e As EventArgs) Handles btnOpenTemplateForm.Click
    '    'Open the Template form:
    '    If IsNothing(TemplateForm) Then
    '        TemplateForm = New frmTemplate
    '        TemplateForm.Show()
    '    Else
    '        TemplateForm.Show()
    '    End If
    'End Sub

    'Private Sub TemplateForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles TemplateForm.FormClosed
    '    TemplateForm = Nothing
    'End Sub

    Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMessages.Click
        'Show the Messages form.
        Message.ApplicationName = ApplicationInfo.Name
        Message.SettingsLocn = Project.SettingsLocn
        Message.Show()
        Message.ShowXMessages = ShowXMessages
        Message.MessageForm.BringToFront()
    End Sub

    Private Sub btnWebPages_Click(sender As Object, e As EventArgs) Handles btnWebPages.Click
        'Open the Web Pages form.

        If IsNothing(WebPageList) Then
            WebPageList = New frmWebPageList
            WebPageList.Show()
        Else
            WebPageList.Show()
            WebPageList.BringToFront()
        End If
    End Sub

    Private Sub WebPageList_FormClosed(sender As Object, e As FormClosedEventArgs) Handles WebPageList.FormClosed
        WebPageList = Nothing
    End Sub

    Public Function OpenNewWebPage() As Integer
        'Open a new HTML Web View window, or reuse an existing one if avaiable.
        'The new forms index number in WebViewFormList is returned.

        NewWebPage = New frmWebPage
        If WebPageFormList.Count = 0 Then
            WebPageFormList.Add(NewWebPage)
            WebPageFormList(0).FormNo = 0
            WebPageFormList(0).Show
            Return 0 'The new HTML Display is at position 0 in WebViewFormList()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To WebPageFormList.Count - 1 'Check if there are closed forms in WebViewFormList. They can be re-used.
                If IsNothing(WebPageFormList(I)) Then
                    WebPageFormList(I) = NewWebPage
                    WebPageFormList(I).FormNo = I
                    WebPageFormList(I).Show
                    FormAdded = True
                    Return I 'The new Html Display is at position I in WebViewFormList()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to WebViewFormList
                Dim FormNo As Integer
                WebPageFormList.Add(NewWebPage)
                FormNo = WebPageFormList.Count - 1
                WebPageFormList(FormNo).FormNo = FormNo
                WebPageFormList(FormNo).Show
                Return FormNo 'The new WebPage is at position FormNo in WebPageFormList()
            End If
        End If
    End Function

    Public Sub WebPageFormClosed()
        'This subroutine is called when the Web Page form has been closed.
        'The subroutine is usually called from the FormClosed event of the WebPage form.
        'The WebPage form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the WebPage form.
        'This property should be updated by the WebPage form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in WebPageList should be set to Nothing.

        If WebPageFormList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in WebPageFormList
            Exit Sub
        End If

        If IsNothing(WebPageFormList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            WebPageFormList(ClosedFormNo) = Nothing
        End If
    End Sub

    Public Function OpenNewHtmlDisplayPage() As Integer
        'Open a new HTML display window, or reuse an existing one if avaiable.
        'The new forms index number in HtmlDisplayFormList is returned.

        NewHtmlDisplay = New frmHtmlDisplay
        If HtmlDisplayFormList.Count = 0 Then
            HtmlDisplayFormList.Add(NewHtmlDisplay)
            HtmlDisplayFormList(0).FormNo = 0
            HtmlDisplayFormList(0).Show
            Return 0 'The new HTML Display is at position 0 in HtmlDisplayFormList()
        Else
            Dim I As Integer
            Dim FormAdded As Boolean = False
            For I = 0 To HtmlDisplayFormList.Count - 1 'Check if there are closed forms in HtmlDisplayFormList. They can be re-used.
                If IsNothing(HtmlDisplayFormList(I)) Then
                    HtmlDisplayFormList(I) = NewHtmlDisplay
                    HtmlDisplayFormList(I).FormNo = I
                    HtmlDisplayFormList(I).Show
                    FormAdded = True
                    Return I 'The new Html Display is at position I in HtmlDisplayFormList()
                    Exit For
                End If
            Next
            If FormAdded = False Then 'Add a new form to HtmlDisplayFormList
                Dim FormNo As Integer
                HtmlDisplayFormList.Add(NewHtmlDisplay)
                FormNo = HtmlDisplayFormList.Count - 1
                HtmlDisplayFormList(FormNo).FormNo = FormNo
                HtmlDisplayFormList(FormNo).Show
                Return FormNo 'The new HtmlDisplay is at position FormNo in HtmlDisplayFormList()
            End If
        End If
    End Function

    Public Sub HtmlDisplayFormClosed()
        'This subroutine is called when the Html Display form has been closed.
        'The subroutine is usually called from the FormClosed event of the HtmlDisplay form.
        'The HtmlDisplay form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the HtmlDisplay form.
        'This property should be updated by the HtmlDisplay form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in HtmlDisplayList should be set to Nothing.

        If HtmlDisplayFormList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in HtmlDisplayFormList
            Exit Sub
        End If

        If IsNothing(HtmlDisplayFormList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            HtmlDisplayFormList(ClosedFormNo) = Nothing
        End If
    End Sub

    Private Sub btnShowData_Click(sender As Object, e As EventArgs) Handles btnShowData.Click
        'Open the Simulated Data form.

        If BayesSim.Data.Tables.Contains("Bayes_Simulation") Then
            If IsNothing(SimData) Then
                SimData = New frmTable
                SimData.Show()
                'SimData.dgvResults.DataSource = BayesSim.Data.Tables("Bayes_Simulation")
                'SimData.dgvResults.AutoResizeColumns()
                'SimData.dgvResults.Update()
                'SimData.dgvResults.Refresh()
                SimData.TableName = "Bayes_Simulation"
            Else
                SimData.Show()
                If SimData.TableName = "Bayes_Simulation" Then
                Else
                    SimData.UpdateTableList()
                    SimData.TableName = "Bayes_Simulation"
                End If
                SimData.BringToFront()
            End If
        Else
            Message.AddWarning("The Bayes_Simulation table was not found. Please run a simulation." & vbCrLf)
        End If
    End Sub

    Private Sub SimData_FormClosed(sender As Object, e As FormClosedEventArgs) Handles SimData.FormClosed
        SimData = Nothing
    End Sub

    Private Sub btnShowEventSimData_Click(sender As Object, e As EventArgs) Handles btnShowEventSimData.Click
        'Open the Simulated Data form.

        If BayesSim.Data.Tables.Contains("Event_Simulation") Then
            If IsNothing(SimData) Then
                SimData = New frmTable
                SimData.Show()
                'SimData.dgvResults.DataSource = BayesSim.Data.Tables("Event_Simulation")
                'SimData.dgvResults.AutoResizeColumns()
                'SimData.dgvResults.Update()
                'SimData.dgvResults.Refresh()
                SimData.TableName = "Event_Simulation"
            Else
                SimData.Show()
                If SimData.TableName = "Event_Simulation" Then
                Else
                    SimData.UpdateTableList()
                    SimData.TableName = "Event_Simulation"
                End If
                SimData.BringToFront()
            End If
        Else
            Message.AddWarning("The Event_Simulation table was not found. Please run a simulation." & vbCrLf)
        End If
    End Sub


    Public Function OpenNewSeriesAnalysis() As Integer
        'Open a new SeriesAnalysis form, or reuse an existing one if available.
        'The new forms index number in SeriesAnalysisList is returned.

        SeriesAnalysis = New frmSeriesAnalysis
        If SeriesAnalysisList.Count = 0 Then
            SeriesAnalysisList.Add(SeriesAnalysis)
            SeriesAnalysisList(0).FormNo = 0
            'SeriesAnalysisList(0).Show 'NOTE: This is now opened later - after the SourceColumnName is set - this is needed to find the settings file.
            Return 0 'The new SeriesAnalysis is at position 0 in SeriesAnalysisList()
        Else
            Dim I As Integer
            Dim SeriesAnalysisAdded As Boolean = False
            For I = 0 To SeriesAnalysisList.Count - 1
                If IsNothing(SeriesAnalysisList(I)) Then
                    SeriesAnalysisList(I) = SeriesAnalysis
                    SeriesAnalysisList(I).FormNo = I
                    'SeriesAnalysisList(I).Show  'NOTE: This is now opened later - after the SourceColumnName is set - this is needed to find the settings file.
                    SeriesAnalysisAdded = True
                    Return I 'The new SeriesAnalysis is at position I in SeriesAnalysisList()
                    Exit For
                End If
            Next
            If SeriesAnalysisAdded = False Then 'Add a new SeriesAnalysis to SeriesAnalysisList()
                Dim SeriesAnalysisNo As Integer
                SeriesAnalysisList.Add(SeriesAnalysis)
                SeriesAnalysisNo = SeriesAnalysisList.Count - 1
                SeriesAnalysisList(SeriesAnalysisNo).FormNo = SeriesAnalysisNo
                'SeriesAnalysisList(SeriesAnalysisNo).Show 'NOTE: This is now opened later - after the SourceColumnName is set - this is needed to find the settings file.
                Return SeriesAnalysisNo 'The new SeriesAnalysis is at position SeriesAnalysisNo in SeriesAnalysisList()
            End If
        End If
    End Function

    Private Function SeriesAnalysisOpen(ByVal ColumnName As String) As Boolean
        'Return True if the Series Analysis for for the specified Column Name is open.

        Dim FormOpen As Boolean = False
        For Each Item In SeriesAnalysisList
            If Item Is Nothing Then
                'The corresponding form has been closed.
            Else
                If Item.SourceColumnName = ColumnName Then 'The Series Analysis form corresponding to ColumnName is open.
                    Item.BringToFront 'Bring the form to the front
                    FormOpen = True
                    Exit For
                End If
            End If
        Next
        Return FormOpen
    End Function

    Private Function SeriesAnalysisFormNo(ByVal ColumnName As String) As Integer
        'Return the form number of the Series Analysis form for ColumnName.
        'Returns -1 if the form for the ColumnName is not shown

        Dim FormNo As Integer = -1
        For Each Item In SeriesAnalysisList
            If Item Is Nothing Then
                'The corresponding form has been closed.
            Else
                If Item.SourceColumnName = ColumnName Then 'The Series Analysis form corresponding to ColumnName is open.
                    Item.BringToFront 'Bring the form to the front
                    'FormOpen = True
                    FormNo = Item.FormNo
                    Exit For
                End If
            End If
        Next
        'Return FormOpen
        Return FormNo
    End Function

    Public Sub SeriesAnalysisClosed()
        'This subroutine is called when the SeriesAnalysis form has been closed.
        'The subroutine is usually called from the FormClosed event of the SeriesAnalysis form.
        'The SeriesAnalysis form may have multiple instances.
        'The ClosedFormNumber property should contain the number of the instance of the SeriesAnalysis form.
        'This property should be updated by the SeriesAnalysis form when it is being closed.
        'The ClosedFormNumber property value is used to determine which element in SeriesAnalysisList should be set to Nothing.RocChart.dgvRocData.AllowUserToAddRows = False

        If SeriesAnalysisList.Count < ClosedFormNo + 1 Then
            'ClosedFormNo is too large to exist in SeriesAnalysisList
            Exit Sub
        End If

        If IsNothing(SeriesAnalysisList(ClosedFormNo)) Then
            'The form is already set to nothing
        Else
            SeriesAnalysisList(ClosedFormNo) = Nothing
        End If
    End Sub

    Private Sub btnRocChart_Click(sender As Object, e As EventArgs) Handles btnRocChart.Click
        'Show a blank Receiver Operating Characteristic (ROC) chart.

        If IsNothing(RocChart) Then
            RocChart = New frmRocChart
            RocChart.Show()
        Else
            RocChart.Show()
            RocChart.BringToFront()
        End If
    End Sub

    Private Sub btnShowRoc_Click(sender As Object, e As EventArgs) Handles btnShowRoc.Click
        'Show the Receiver Operating Characteristic (ROC) chart.

        If IsNothing(RocChart) Then
            RocChart = New frmRocChart
            RocChart.Show()
            RocChart.dgvRocData.Rows.Clear()
            'RocChart.dgvRocData.Rows.Add(Bayes.Name, Bayes.Name, Bayes.Performance.Specificity, 1 - Bayes.Performance.Specificity, Bayes.Performance.Sensitivity)
            'RocChart.dgvRocData.Rows.Add(Bayes.Name, Bayes.Name, Format(Bayes.Performance.Specificity, "F4"), Format(1 - Bayes.Performance.Specificity, "F4"), Format(Bayes.Performance.Sensitivity, "F4"), "Red")
            'RocChart.dgvRocData.Rows.Add(Bayes.Name, Bayes.Name, Format(Bayes.Performance.Specificity, "F4"), Format(1 - Bayes.Performance.Specificity, "F4"), Format(Bayes.Performance.Sensitivity, "F4"), cmbRocColor.SelectedItem.ToString)
            'RocChart.dgvRocData.Rows.Add(Bayes.Name, Bayes.Name, Format(Bayes.Performance.Specificity, "F4"), Format(1 - Bayes.Performance.Specificity, "F4"), Format(Bayes.Performance.Sensitivity, "F4"), cmbRocColor.SelectedItem.ToString, Format(Bayes.Performance.Prevalence, "F4"), Format(Bayes.SampleSize, "F2"))
            RocChart.dgvRocData.Rows.Add(Bayes.Name, Bayes.Name, Format(Bayes.Performance.Specificity, "F4"), Format(1 - Bayes.Performance.Specificity, "F4"), Format(Bayes.Performance.Sensitivity, "F4"), cmbRocColor.SelectedItem.ToString, Format(Bayes.Performance.Prevalence, "F4"), Format(Bayes.SampleSize.Value, "F2"))
            RocChart.PlotChart()
        Else
            RocChart.Show()
            RocChart.BringToFront()
            RocChart.dgvRocData.Rows.Clear()
            'RocChart.dgvRocData.Rows.Add(Bayes.Name, Bayes.Name, Bayes.Performance.Specificity, 1 - Bayes.Performance.Specificity, Bayes.Performance.Sensitivity)
            'RocChart.dgvRocData.Rows.Add(Bayes.Name, Bayes.Name, Format(Bayes.Performance.Specificity, "F4"), Format(1 - Bayes.Performance.Specificity, "F4"), Format(Bayes.Performance.Sensitivity, "F4"), "Red")
            'RocChart.dgvRocData.Rows.Add(Bayes.Name, Bayes.Name, Format(Bayes.Performance.Specificity, "F4"), Format(1 - Bayes.Performance.Specificity, "F4"), Format(Bayes.Performance.Sensitivity, "F4"), cmbRocColor.SelectedItem.ToString)
            RocChart.dgvRocData.Rows.Add(Bayes.Name, Bayes.Name, Format(Bayes.Performance.Specificity, "F4"), Format(1 - Bayes.Performance.Specificity, "F4"), Format(Bayes.Performance.Sensitivity, "F4"), cmbRocColor.SelectedItem.ToString, Format(Bayes.Performance.Prevalence, "F4"), Format(Bayes.SampleSize.Value, "F2"))
            RocChart.PlotChart()
        End If
    End Sub

    Private Sub btnAddToROC_Click(sender As Object, e As EventArgs) Handles btnAddToROC.Click

        Dim Specificity As Single
        Dim Sensitivity As Single
        Dim Prevalence As Single
        Dim SampleSize As Single
        If Bayes.Settings.ProbabilityMeasure = "Percent" Then
            Sensitivity = txtCalcSensitivity.Text.Replace("%", "")
            Sensitivity /= 100
            Specificity = txtCalcSpecificity.Text.Replace("%", "")
            Specificity /= 100
            Prevalence = txtCalcPrevalence.Text.Replace("%", "")
            Prevalence /= 100
        Else
            Sensitivity = txtCalcSensitivity.Text
            Specificity = txtCalcSpecificity.Text
            Prevalence = txtCalcPrevalence.Text
        End If
        SampleSize = txtCalcSampSize.Text

        If IsNothing(RocChart) Then
            RocChart = New frmRocChart
            RocChart.Show()
            RocChart.dgvRocData.Rows.Clear()
            'RocChart.dgvRocData.Rows.Add(txtTestName.Text, txtPointLabel.Text, Format(Specificity, "F4"), Format(1 - Specificity, "F4"), Format(Sensitivity, "F4"), cmbCalcRocColor.SelectedItem.ToString)
            RocChart.dgvRocData.Rows.Add(txtTestName.Text, txtPointLabel.Text, Format(Specificity, "F4"), Format(1 - Specificity, "F4"), Format(Sensitivity, "F4"), cmbCalcRocColor.SelectedItem.ToString, Format(Prevalence, "F4"), SampleSize)
            RocChart.PlotChart()
        Else
            RocChart.Show()
            RocChart.BringToFront()
            'RocChart.dgvRocData.Rows.Clear()
            RocChart.dgvRocData.AllowUserToAddRows = False
            'RocChart.dgvRocData.Rows.Add(txtTestName.Text, txtPointLabel.Text, Format(Specificity, "F4"), Format(1 - Specificity, "F4"), Format(Sensitivity, "F4"), cmbCalcRocColor.SelectedItem.ToString)
            RocChart.dgvRocData.Rows.Add(txtTestName.Text, txtPointLabel.Text, Format(Specificity, "F4"), Format(1 - Specificity, "F4"), Format(Sensitivity, "F4"), cmbCalcRocColor.SelectedItem.ToString, Format(Prevalence, "F4"), SampleSize)
            RocChart.dgvRocData.AllowUserToAddRows = True
            RocChart.PlotChart()
        End If
    End Sub

    Private Sub RocChart_FormClosed(sender As Object, e As FormClosedEventArgs) Handles RocChart.FormClosed
        RocChart = Nothing
    End Sub


#End Region 'Open and Close Forms -------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Methods - The main actions performed by this form." '===========================================================================================================================

    Public Sub CloseAppAtConnection(ByVal ProNetName As String, ByVal ConnectionName As String)
        'Close the application and project at the specified connection.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to close the application at the connection.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class

                Dim command As New XElement("Command", "Close")
                xmessage.Add(command)
                doc.Add(xmessage)

                'Show the message sent:
                Message.XAddText("Message sent to: [" & ProNetName & "]." & ConnectionName & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line
                client.SendMessage(ProNetName, ConnectionName, doc.ToString)
            End If
        End If
    End Sub

    Private Sub btnProject_Click(sender As Object, e As EventArgs) Handles btnProject.Click
        Project.SelectProject()
    End Sub

    Private Sub btnParameters_Click(sender As Object, e As EventArgs) Handles btnParameters.Click
        Project.ShowParameters()
    End Sub

    Private Sub btnAppInfo_Click(sender As Object, e As EventArgs) Handles btnAppInfo.Click
        ApplicationInfo.ShowInfo()
    End Sub

    Private Sub btnAndorville_Click(sender As Object, e As EventArgs) Handles btnAndorville.Click
        ApplicationInfo.ShowInfo()
    End Sub

    Private Sub ApplicationInfo_UpdateExePath() Handles ApplicationInfo.UpdateExePath
        'Update the Executable Path.
        ApplicationInfo.ExecutablePath = Application.ExecutablePath
    End Sub

    Private Sub ApplicationInfo_RestoreDefaults() Handles ApplicationInfo.RestoreDefaults
        'Restore the default application settings.
        DefaultAppProperties()
    End Sub

    Public Sub UpdateWebPage(ByVal FileName As String)
        'Update the web page in WebPageFormList if the Web file name is FileName.

        Dim NPages As Integer = WebPageFormList.Count
        Dim I As Integer

        Try
            For I = 0 To NPages - 1
                If IsNothing(WebPageFormList(I)) Then
                    'Web page has been deleted!
                Else
                    If WebPageFormList(I).FileName = FileName Then
                        WebPageFormList(I).OpenDocument
                    End If
                End If
            Next
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub


#Region " Start Page Code" '=========================================================================================================================================

    Public Sub OpenStartPage()
        'Open the workflow page:

        If Project.DataFileExists(WorkflowFileName) Then
            'Note: WorkflowFileName should have been restored when the application started.
            DisplayWorkflow()
        ElseIf Project.DataFileExists("StartPage.html") Then
            WorkflowFileName = "StartPage.html"
            DisplayWorkflow()
        Else
            CreateStartPage()
            WorkflowFileName = "StartPage.html"
            DisplayWorkflow()
        End If

        'Open the StartPage.html file and display in the Workflow tab.
        'If Project.DataFileExists("StartPage.html") Then
        '    WorkflowFileName = "StartPage.html"
        '    DisplayWorkflow()
        'Else
        '    CreateStartPage()
        '    WorkflowFileName = "StartPage.html"
        '    DisplayWorkflow()
        'End If

    End Sub

    Public Sub DisplayWorkflow()
        'Display the StartPage.html file in the Start Page tab.

        If Project.DataFileExists(WorkflowFileName) Then
            Dim rtbData As New IO.MemoryStream
            Project.ReadData(WorkflowFileName, rtbData)
            rtbData.Position = 0
            Dim sr As New IO.StreamReader(rtbData)
            WebBrowser1.DocumentText = sr.ReadToEnd()
        Else
            Message.AddWarning("Web page file not found: " & WorkflowFileName & vbCrLf)
        End If
    End Sub

    Private Sub CreateStartPage()
        'Create a new default StartPage.html file.

        Dim htmData As New IO.MemoryStream
        Dim sw As New IO.StreamWriter(htmData)
        sw.Write(AppInfoHtmlString("Application Information")) 'Create a web page providing information about the application.
        sw.Flush()
        Project.SaveData("StartPage.html", htmData)
    End Sub

    Public Function AppInfoHtmlString(ByVal DocumentTitle As String) As String
        'Create an Application Information Web Page.

        'This function should be edited to provide a brief description of the Application.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf)
        sb.Append("<head>" & vbCrLf)
        sb.Append("<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("<meta name=""description"" content=""Application information."">" & vbCrLf)
        sb.Append("</head>" & vbCrLf)

        sb.Append("<body style=""font-family:arial;"">" & vbCrLf & vbCrLf)

        sb.Append("<h2>" & "Andorville&trade; Bayes" & "</h2>" & vbCrLf & vbCrLf) 'Add the page title.
        sb.Append("<hr>" & vbCrLf) 'Add a horizontal divider line.
        sb.Append("<p>The Bayes application demonstrates and applies Bayes theorem.</p>" & vbCrLf) 'Add an application description.
        sb.Append("<hr>" & vbCrLf & vbCrLf) 'Add a horizontal divider line.

        sb.Append(DefaultJavaScriptString)

        sb.Append("</body>" & vbCrLf)
        sb.Append("</html>" & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultJavaScriptString() As String
        'Generate the default JavaScript section of an Andorville(TM) Workflow Web Page.

        Dim sb As New System.Text.StringBuilder

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the Start Up code section.
        sb.Append("//Code to execute on Start Up:" & vbCrLf)
        sb.Append("function StartUpCode() {" & vbCrLf)
        sb.Append("  RestoreSettings() ;" & vbCrLf)
        sb.Append("}" & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append(vbCrLf)

        'sb.Append(vbCrLf)
        'sb.Append("  case ""Status"" :" & vbCrLf)
        'sb.Append("    if (Info = ""OK"") { " & vbCrLf)
        'sb.Append("      //Instruction processing completed OK:" & vbCrLf)
        'sb.Append("      } else {" & vbCrLf)
        'sb.Append("      window.external.AddWarning(""Error: Unknown Status information: "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        'sb.Append("     }" & vbCrLf)
        'sb.Append("    break ;" & vbCrLf)
        'sb.Append(vbCrLf)

        'sb.Append("  case ""OnCompletion"" :" & vbCrLf)
        sb.Append("  case ""EndInstruction"" :" & vbCrLf)
        sb.Append("    switch(Info) {" & vbCrLf)
        sb.Append("      case ""Stop"" :" & vbCrLf)
        sb.Append("        //Do nothing." & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("      default:" & vbCrLf)
        'sb.Append("        window.external.AddWarning(""Error: Unknown OnCompletion information:  "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("        window.external.AddWarning(""Error: Unknown EndInstruction information:  "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append("    }" & vbCrLf)
        sb.Append("    break ;" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("  case ""Status"" :" & vbCrLf)
        sb.Append("    switch(Info) {" & vbCrLf)
        sb.Append("      case ""OK"" :" & vbCrLf)
        sb.Append("        //Instruction processing completed OK." & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("      default:" & vbCrLf)
        sb.Append("        window.external.AddWarning(""Error: Unknown Status information:  "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("        break ;" & vbCrLf)
        sb.Append("    }" & vbCrLf)
        sb.Append("    break ;" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        'sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append("window.onload = StartUpCode ; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultJavaScriptString_Old() As String
        'Generate the default JavaScript section of an Andorville(TM) Workflow Web Page.

        Dim sb As New System.Text.StringBuilder

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the Start Up code section.
        sb.Append("//Code to execute on Start Up:" & vbCrLf)
        sb.Append("function StartUpCode() {" & vbCrLf)
        sb.Append("  RestoreSettings() ;" & vbCrLf)
        'sb.Append("  GetCalcsDbPath() ;" & vbCrLf)
        sb.Append("}" & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append("  case ""Status"" :" & vbCrLf)
        sb.Append("    if (Info = ""OK"") { " & vbCrLf)
        sb.Append("      //Instruction processing completed OK:" & vbCrLf)
        sb.Append("      } else {" & vbCrLf)
        sb.Append("      window.external.AddWarning(""Error: Unknown Status information: "" + "" Info: "" + Info + ""\r\n"") ;" & vbCrLf)
        sb.Append("     }" & vbCrLf)
        sb.Append("    break ;" & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        'sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append("window.onload = StartUpCode ; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultHtmlString(ByVal DocumentTitle As String) As String
        'Create a blank HTML Web Page.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf)
        sb.Append("<!-- Andorville(TM) Workflow File -->" & vbCrLf)
        sb.Append("<!-- Application Name:    " & ApplicationInfo.Name & " -->" & vbCrLf)
        sb.Append("<!-- Application Version: " & My.Application.Info.Version.ToString & " -->" & vbCrLf)
        sb.Append("<!-- Creation Date:          " & Format(Now, "dd MMMM yyyy") & " -->" & vbCrLf)
        sb.Append("<head>" & vbCrLf)
        sb.Append("<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("<meta name=""description"" content=""Workflow description."">" & vbCrLf)
        sb.Append("</head>" & vbCrLf)

        sb.Append("<body style=""font-family:arial;"">" & vbCrLf & vbCrLf)

        sb.Append("<h2>" & DocumentTitle & "</h2>" & vbCrLf & vbCrLf)

        sb.Append(DefaultJavaScriptString)

        sb.Append("</body>" & vbCrLf)
        sb.Append("</html>" & vbCrLf)

        Return sb.ToString

    End Function

    Public Function DefaultHtmlString_Old(ByVal DocumentTitle As String) As String
        'Create a blank HTML Web Page.

        Dim sb As New System.Text.StringBuilder

        sb.Append("<!DOCTYPE html>" & vbCrLf)
        sb.Append("<html>" & vbCrLf & "<head>" & vbCrLf & "<title>" & DocumentTitle & "</title>" & vbCrLf)
        sb.Append("</head>" & vbCrLf & "<body>" & vbCrLf & vbCrLf)
        sb.Append("<h1>" & DocumentTitle & "</h1>" & vbCrLf & vbCrLf)

        'Add JavaScript section:
        sb.Append("<script>" & vbCrLf & vbCrLf)

        'START: User defined JavaScript functions ==========================================================================
        'Add functions to implement the main actions performed by this web page.
        sb.Append("//START: User defined JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Add functions to implement the main actions performed by this web page." & vbCrLf & vbCrLf)

        sb.Append("//END:   User defined JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User defined JavaScript functions --------------------------------------------------------------------------


        'START: User modified JavaScript functions ==========================================================================
        'Modify these function to save all required web page settings and process all expected XMessage instructions.
        sb.Append("//START: User modified JavaScript functions ==========================================================================" & vbCrLf)
        sb.Append("//  Modify these function to save all required web page settings and process all expected XMessage instructions." & vbCrLf & vbCrLf)

        'Add the SaveSettings function - This is used to save web page settings between sessions.
        sb.Append("//Save the web page settings." & vbCrLf)
        sb.Append("function SaveSettings() {" & vbCrLf)
        sb.Append("  var xSettings = ""<Settings>"" + "" \n"" ; //String containing the web page settings in XML format." & vbCrLf)
        sb.Append("  //Add xml lines to save each setting." & vbCrLf & vbCrLf)
        sb.Append("  xSettings +=    ""</Settings>"" + ""\n"" ; //End of the Settings element." & vbCrLf)
        sb.Append(vbCrLf)
        sb.Append("  //Save the settings as an XML file in the project." & vbCrLf)
        sb.Append("  window.external.SaveHtmlSettings(xSettings) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Process a single XMsg instruction (Information:Location pair)
        sb.Append("//Process an XMessage instruction:" & vbCrLf)
        sb.Append("function XMsgInstruction(Info, Locn) {" & vbCrLf)
        sb.Append("  switch(Locn) {" & vbCrLf)
        sb.Append("  //Insert case statements here." & vbCrLf)
        sb.Append("  default:" & vbCrLf)
        sb.Append("    window.external.AddWarning(""Unknown location: "" + Locn + ""\r\n"") ;" & vbCrLf)
        sb.Append("  }" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   User modified JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf & vbCrLf)
        'END:   User modified JavaScript functions --------------------------------------------------------------------------

        'START: Required Document Library Web Page JavaScript functions ==========================================================================
        sb.Append("//START: Required Document Library Web Page JavaScript functions ==========================================================================" & vbCrLf & vbCrLf)

        'Add the AddText function - This sends a message to the message window using a named text type.
        sb.Append("//Add text to the Message window using a named txt type:" & vbCrLf)
        sb.Append("function AddText(Msg, TextType) {" & vbCrLf)
        sb.Append("  window.external.AddText(Msg, TextType) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddMessage function - This sends a message to the message window using default black text.
        sb.Append("//Add a message to the Message window using the default black text:" & vbCrLf)
        sb.Append("function AddMessage(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddMessage(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the AddWarning function - This sends a red, bold warning message to the message window.
        sb.Append("//Add a warning message to the Message window using bold red text:" & vbCrLf)
        sb.Append("function AddWarning(Msg) {" & vbCrLf)
        sb.Append("  window.external.AddWarning(Msg) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreSettings function - This is used to restore web page settings.
        sb.Append("//Restore the web page settings." & vbCrLf)
        sb.Append("function RestoreSettings() {" & vbCrLf)
        sb.Append("  window.external.RestoreHtmlSettings() " & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'This line runs the RestoreSettings function when the web page is loaded.
        sb.Append("//Restore the web page settings when the page loads." & vbCrLf)
        sb.Append("window.onload = RestoreSettings; " & vbCrLf)
        sb.Append(vbCrLf)

        'Restores a single setting on the web page.
        sb.Append("//Restore a web page setting." & vbCrLf)
        sb.Append("  function RestoreSetting(FormName, ItemName, ItemValue) {" & vbCrLf)
        sb.Append("  document.forms[FormName][ItemName].value = ItemValue ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        'Add the RestoreOption function - This is used to add an option to a Select list.
        sb.Append("//Restore a Select control Option." & vbCrLf)
        sb.Append("function RestoreOption(SelectId, OptionText) {" & vbCrLf)
        sb.Append("  var x = document.getElementById(SelectId) ;" & vbCrLf)
        sb.Append("  var option = document.createElement(""Option"") ;" & vbCrLf)
        sb.Append("  option.text = OptionText ;" & vbCrLf)
        sb.Append("  x.add(option) ;" & vbCrLf)
        sb.Append("}" & vbCrLf)
        sb.Append(vbCrLf)

        sb.Append("//END:   Required Document Library Web Page JavaScript functions __________________________________________________________________________" & vbCrLf & vbCrLf)
        'END:   Required Document Library Web Page JavaScript functions --------------------------------------------------------------------------

        sb.Append("</script>" & vbCrLf & vbCrLf)

        sb.Append("</body>" & vbCrLf & "</html>" & vbCrLf)

        Return sb.ToString

    End Function

#End Region 'Start Page Code ------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Methods Called by JavaScript - A collection of methods that can be called by JavaScript in a web page shown in WebBrowser1" '==================================
    'These methods are used to display HTML pages in the Workflow tab.
    'The same methods can be found in the WebView form, which displays web pages on seprate forms.


    'Display Messages ==============================================================================================

    Public Sub AddMessage(ByVal Msg As String)
        'Add a normal text message to the Message window.
        Message.Add(Msg)
    End Sub

    Public Sub AddWarning(ByVal Msg As String)
        'Add a warning text message to the Message window.
        Message.AddWarning(Msg)
    End Sub

    Public Sub AddTextTypeMessage(ByVal Msg As String, ByVal TextType As String)
        'Add a message with the specified Text Type to the Message window.
        Message.AddText(Msg, TextType)
    End Sub

    Public Sub AddXmlMessage(ByVal XmlText As String)
        'Add an Xml message to the Message window.
        Message.AddXml(XmlText)
    End Sub

    'END Display Messages ------------------------------------------------------------------------------------------


    'Run an XSequence ==============================================================================================

    Public Sub RunClipboardXSeq()
        'Run the XSequence instructions in the clipboard.

        Dim XDocSeq As System.Xml.Linq.XDocument
        Try
            XDocSeq = XDocument.Parse(My.Computer.Clipboard.GetText)
        Catch ex As Exception
            Message.AddWarning("Error reading Clipboard data. " & ex.Message & vbCrLf)
            Exit Sub
        End Try

        If IsNothing(XDocSeq) Then
            Message.Add("No XSequence instructions were found in the clipboard.")
        Else
            Dim XmlSeq As New System.Xml.XmlDocument
            Try
                XmlSeq.LoadXml(XDocSeq.ToString) 'Convert XDocSeq to an XmlDocument to process with XSeq.
                'Run the sequence:
                XSeq.RunXSequence(XmlSeq, Status)
            Catch ex As Exception
                Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub RunXSequence(ByVal XSequence As String)
        'Run the XMSequence
        Dim XmlSeq As New System.Xml.XmlDocument
        XmlSeq.LoadXml(XSequence)
        XSeq.RunXSequence(XmlSeq, Status)
    End Sub

    Private Sub XSeq_ErrorMsg(ErrMsg As String) Handles XSeq.ErrorMsg
        Message.AddWarning(ErrMsg & vbCrLf)
    End Sub

    Private Sub XSeq_Instruction(Data As String, Locn As String) Handles XSeq.Instruction
        'Execute each instruction produced by running the XSeq file.

        Select Case Locn

            'Restore Web Page Settings: -------------------------------------------------
            Case "Settings:Form:Name"
                FormName = Data

            Case "Settings:Form:Item:Name"
                ItemName = Data

            Case "Settings:Form:Item:Value"
                RestoreSetting(FormName, ItemName, Data)

            Case "Settings:Form:SelectId"
                SelectId = Data

            Case "Settings:Form:OptionText"
                RestoreOption(SelectId, Data)
            'END Restore Web Page Settings: ---------------------------------------------

            ''Start Project commands: ----------------------------------------------------

            'Case "StartProject:AppName"
            '    StartProject_AppName = Data

            'Case "StartProject:ConnectionName"
            '    StartProject_ConnName = Data

            'Case "StartProject:ProNetName"
            '    StartProject_ProNetName = Data

            'Case "StartProject:ProjectID"
            '    StartProject_ProjID = Data

            'Case "StartProject:ProjectName"
            '    StartProject_ProjName = Data

            'Case "StartProject:Command"
            '    Select Case Data
            '        Case "Apply"
            '            If StartProject_ProjName <> "" Then
            '                StartApp_ProjectName(StartProject_AppName, StartProject_ProjName, StartProject_ConnName)
            '            ElseIf StartProject_ProjID <> "" Then
            '                StartApp_ProjectID(StartProject_AppName, StartProject_ProjID, StartProject_ConnName)
            '            Else
            '                Message.AddWarning("Project not specified. Project Name and Project ID are blank." & vbCrLf)
            '            End If
            '        Case Else
            '            Message.AddWarning("Unknown Start Project command : " & Data & vbCrLf)
            '    End Select

            ''END Start project commands ---------------------------------------------

            Case "Settings"
                'Do nothing


            Case "EndOfSequence"
                'Main.Message.Add("End of processing sequence" & Data & vbCrLf)

            Case Else
                Message.AddWarning("Unknown location: " & Locn & "  Data: " & Data & vbCrLf)

        End Select
    End Sub

    'END Run an XSequence ------------------------------------------------------------------------------------------


    'Run an XMessage ===============================================================================================

    Public Sub RunXMessage(ByVal XMsg As String)
        'Run the XMessage by sending it to InstrReceived.
        InstrReceived = XMsg
    End Sub

    Public Sub SendXMessage(ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMessage to the application with the connection name ConnName.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                If bgwSendMessage.IsBusy Then
                    Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    Dim SendMessageParams As New Main.clsSendMessageParams
                    SendMessageParams.ProjectNetworkName = ProNetName
                    SendMessageParams.ConnectionName = ConnName
                    SendMessageParams.Message = XMsg
                    bgwSendMessage.RunWorkerAsync(SendMessageParams)
                    If ShowXMessages Then
                        Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                        Message.XAddXml(XMsg)
                        Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub SendXMessageExt(ByVal ProNetName As String, ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName and Project Network Name ProNetname.
        'This version can send the XMessage to a connection external to the current Project Network.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                If bgwSendMessage.IsBusy Then
                    Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                Else
                    Dim SendMessageParams As New Main.clsSendMessageParams
                    SendMessageParams.ProjectNetworkName = ProNetName
                    SendMessageParams.ConnectionName = ConnName
                    SendMessageParams.Message = XMsg
                    bgwSendMessage.RunWorkerAsync(SendMessageParams)
                    If ShowXMessages Then
                        Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                        Message.XAddXml(XMsg)
                        Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub SendXMessageWait(ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName.
        'Wait for the connection to be made.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            Try
                'Application.DoEvents() 'TRY THE METHOD WITHOUT THE DOEVENTS
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("client state is faulted. Message not sent!" & vbCrLf)
                Else
                    Dim StartTime As Date = Now
                    Dim Duration As TimeSpan
                    'Wait up to 16 seconds for the connection ConnName to be established
                    While client.ConnectionExists(ProNetName, ConnName) = False 'Wait until the required connection is made.
                        System.Threading.Thread.Sleep(1000) 'Pause for 1000ms
                        Duration = Now - StartTime
                        If Duration.Seconds > 16 Then Exit While
                    End While

                    If client.ConnectionExists(ProNetName, ConnName) = False Then
                        Message.AddWarning("Connection not available: " & ConnName & " in application network: " & ProNetName & vbCrLf)
                    Else
                        If bgwSendMessage.IsBusy Then
                            Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                        Else
                            Dim SendMessageParams As New Main.clsSendMessageParams
                            SendMessageParams.ProjectNetworkName = ProNetName
                            SendMessageParams.ConnectionName = ConnName
                            SendMessageParams.Message = XMsg
                            bgwSendMessage.RunWorkerAsync(SendMessageParams)
                            If ShowXMessages Then
                                Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                                Message.XAddXml(XMsg)
                                Message.XAddText(vbCrLf, "Normal") 'Add extra line
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Message.AddWarning(ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub SendXMessageExtWait(ByVal ProNetName As String, ByVal ConnName As String, ByVal XMsg As String)
        'Send the XMsg to the application with the connection name ConnName and Project Network Name ProNetName.
        'Wait for the connection to be made.
        'This version can send the XMessage to a connection external to the current Project Network.
        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                Dim StartTime As Date = Now
                Dim Duration As TimeSpan
                'Wait up to 16 seconds for the connection ConnName to be established
                While client.ConnectionExists(ProNetName, ConnName) = False
                    System.Threading.Thread.Sleep(1000) 'Pause for 1000ms
                    Duration = Now - StartTime
                    If Duration.Seconds > 16 Then Exit While
                End While

                If client.ConnectionExists(ProNetName, ConnName) = False Then
                    Message.AddWarning("Connection not available: " & ConnName & " in application network: " & ProNetName & vbCrLf)
                Else
                    If bgwSendMessage.IsBusy Then
                        Message.AddWarning("Send Message backgroundworker is busy." & vbCrLf)
                    Else
                        Dim SendMessageParams As New Main.clsSendMessageParams
                        SendMessageParams.ProjectNetworkName = ProNetName
                        SendMessageParams.ConnectionName = ConnName
                        SendMessageParams.Message = XMsg
                        bgwSendMessage.RunWorkerAsync(SendMessageParams)
                        If ShowXMessages Then
                            Message.XAddText("Message sent to " & "[" & ProNetName & "]." & ConnName & ":" & vbCrLf, "XmlSentNotice")
                            Message.XAddXml(XMsg)
                            Message.XAddText(vbCrLf, "Normal") 'Add extra line
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub XMsgInstruction(ByVal Info As String, ByVal Locn As String)
        'Send the XMessage Instruction to the JavaScript function XMsgInstruction for processing.
        Me.WebBrowser1.Document.InvokeScript("XMsgInstruction", New String() {Info, Locn})
    End Sub

    'END Run an XMessage -------------------------------------------------------------------------------------------


    'Get Information ===============================================================================================

    Public Function GetFormNo() As String
        'Return the Form Number of the current instance of the WebPage form.
        'Return FormNo.ToString
        Return "-1" 'The Main Form is not a Web Page form.
    End Function

    Public Function GetParentFormNo() As String
        'Return the Form Number of the Parent Form (that called this form).
        'Return ParentWebPageFormNo.ToString
        Return "-1" 'The Main Form does not have a Parent Web Page.
    End Function

    Public Function GetConnectionName() As String
        'Return the Connection Name of the Project.
        Return ConnectionName
    End Function

    Public Function GetProNetName() As String
        'Return the Project Network Name of the Project.
        Return ProNetName
    End Function

    Public Sub ParentProjectName(ByVal FormName As String, ByVal ItemName As String)
        'Return the Parent Project name:
        RestoreSetting(FormName, ItemName, Project.ParentProjectName)
    End Sub

    Public Sub ParentProjectPath(ByVal FormName As String, ByVal ItemName As String)
        'Return the Parent Project path:
        RestoreSetting(FormName, ItemName, Project.ParentProjectPath)
    End Sub

    Public Sub ParentProjectParameterValue(ByVal FormName As String, ByVal ItemName As String, ByVal ParameterName As String)
        'Return the specified Parent Project parameter value:
        RestoreSetting(FormName, ItemName, Project.ParentParameter(ParameterName).Value)
    End Sub

    Public Sub ProjectParameterValue(ByVal FormName As String, ByVal ItemName As String, ByVal ParameterName As String)
        'Return the specified Project parameter value:
        RestoreSetting(FormName, ItemName, Project.Parameter(ParameterName).Value)
    End Sub

    Public Sub ProjectNetworkName(ByVal FormName As String, ByVal ItemName As String)
        'Return the name of the Project Network:
        RestoreSetting(FormName, ItemName, Project.Parameter("ProNetName").Value)
    End Sub

    'END Get Information -------------------------------------------------------------------------------------------


    'Open a Web Page ===============================================================================================

    Public Sub OpenWebPage(ByVal FileName As String)
        'Open the web page with the specified File Name.

        If FileName = "" Then

        Else
            'First check if the HTML file is already open:
            Dim FileFound As Boolean = False
            If WebPageFormList.Count = 0 Then

            Else
                Dim I As Integer
                For I = 0 To WebPageFormList.Count - 1
                    If WebPageFormList(I) Is Nothing Then

                    Else
                        If WebPageFormList(I).FileName = FileName Then
                            FileFound = True
                            WebPageFormList(I).BringToFront
                        End If
                    End If
                Next
            End If

            If FileFound = False Then
                Dim FormNo As Integer = OpenNewWebPage()
                WebPageFormList(FormNo).FileName = FileName
                WebPageFormList(FormNo).OpenDocument
                WebPageFormList(FormNo).BringToFront
            End If
        End If
    End Sub

    'END Open a Web Page -------------------------------------------------------------------------------------------


    'Open and Close Projects =======================================================================================

    Public Sub OpenProjectAtRelativePath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Open the Project at the specified Relative Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            ProjectPath = Project.Path & RelativePath
            client.StartProjectAtPath(ProjectPath, ConnectionName)
        Else
            ProjectPath = Project.Path & "\" & RelativePath
            client.StartProjectAtPath(ProjectPath, ConnectionName)
        End If
    End Sub

    Public Sub CheckOpenProjectAtRelativePath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Check if the project at the specified Relative Path is open.
        'Open it if it is not already open.
        'Open the Project at the specified Relative Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            ProjectPath = Project.Path & RelativePath
            If client.ProjectOpen(ProjectPath) Then
                'Project is already open.
            Else
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            End If
        Else
            ProjectPath = Project.Path & "\" & RelativePath
            If client.ProjectOpen(ProjectPath) Then
                'Project is already open.
            Else
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            End If
        End If
    End Sub

    Public Sub OpenProjectAtProNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Open the Project at the specified Path (relative to the Project Network Path) using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & RelativePath
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        Else
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & "\" & RelativePath
                client.StartProjectAtPath(ProjectPath, ConnectionName)
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        End If
    End Sub

    Public Sub CheckOpenProjectAtProNetPath(ByVal RelativePath As String, ByVal ConnectionName As String)
        'Check if the project at the specified Path (relative to the Project Network Path) is open.
        'Open it if it is not already open.
        'Open the Project at the specified Path using the specified Connection Name.

        Dim ProjectPath As String
        If RelativePath.StartsWith("\") Then
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & RelativePath
                'client.StartProjectAtPath(ProjectPath, ConnectionName)
                If client.ProjectOpen(ProjectPath) Then
                    'Project is already open.
                Else
                    client.StartProjectAtPath(ProjectPath, ConnectionName)
                End If
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        Else
            If Project.ParameterExists("ProNetPath") Then
                ProjectPath = Project.GetParameter("ProNetPath") & "\" & RelativePath
                'client.StartProjectAtPath(ProjectPath, ConnectionName)
                If client.ProjectOpen(ProjectPath) Then
                    'Project is already open.
                Else
                    client.StartProjectAtPath(ProjectPath, ConnectionName)
                End If
            Else
                Message.AddWarning("The Project Network Path is not known." & vbCrLf)
            End If
        End If
    End Sub


    Public Sub CloseProjectAtConnection(ByVal ProNetName As String, ByVal ConnectionName As String)
        'Close the Project at the specified connection.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to close the application at the connection.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class

                'NOTE: No reply expected. No need to provide the following client information(?)
                'Dim clientConnName As New XElement("ClientConnectionName", Me.ConnectionName)
                'xmessage.Add(clientConnName)

                Dim command As New XElement("Command", "Close")
                xmessage.Add(command)
                doc.Add(xmessage)

                'Show the message sent:
                Message.XAddText("Message sent to: [" & ProNetName & "]." & ConnectionName & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line

                client.SendMessage(ProNetName, ConnectionName, doc.ToString)
            End If
        End If
    End Sub

    'END Open and Close Projects -----------------------------------------------------------------------------------


    'System Methods ================================================================================================

    Public Sub SaveHtmlSettings(ByVal xSettings As String, ByVal FileName As String)
        'Save the Html settings for a web page.

        'Convert the XSettings to XML format:
        Dim XmlHeader As String = "<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>"
        Dim XDocSettings As New System.Xml.Linq.XDocument

        Try
            XDocSettings = System.Xml.Linq.XDocument.Parse(XmlHeader & vbCrLf & xSettings)
        Catch ex As Exception
            Message.AddWarning("Error saving HTML settings file. " & ex.Message & vbCrLf)
        End Try

        Project.SaveXmlData(FileName, XDocSettings)
    End Sub

    Public Sub RestoreHtmlSettings()
        'Restore the Html settings for a web page.

        Dim SettingsFileName As String = WorkflowFileName & "Settings"
        Dim XDocSettings As New System.Xml.Linq.XDocument
        Project.ReadXmlData(SettingsFileName, XDocSettings)

        If XDocSettings Is Nothing Then
            'Message.Add("No HTML Settings file : " & SettingsFileName & vbCrLf)
        Else
            Dim XSettings As New System.Xml.XmlDocument
            Try
                XSettings.LoadXml(XDocSettings.ToString)
                'Run the Settings file:
                XSeq.RunXSequence(XSettings, Status)
            Catch ex As Exception
                Message.AddWarning("Error restoring HTML settings. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Public Sub RestoreSetting(ByVal FormName As String, ByVal ItemName As String, ByVal ItemValue As String)
        'Restore the setting value with the specified Form Name and Item Name.
        Me.WebBrowser1.Document.InvokeScript("RestoreSetting", New String() {FormName, ItemName, ItemValue})
    End Sub

    Public Sub RestoreOption(ByVal SelectId As String, ByVal OptionText As String)
        'Restore the Option text in the Select control with the Id SelectId.
        Me.WebBrowser1.Document.InvokeScript("RestoreOption", New String() {SelectId, OptionText})
    End Sub

    Private Sub SaveWebPageSettings()
        'Call the SaveSettings JavaScript function:
        Try
            Me.WebBrowser1.Document.InvokeScript("SaveSettings")
        Catch ex As Exception
            Message.AddWarning("Web page settings not saved: " & ex.Message & vbCrLf)
        End Try
    End Sub

    'END System Methods --------------------------------------------------------------------------------------------


    'Legacy Code (These methods should no longer be used) ==========================================================

    Public Sub JSMethodTest1()
        'Test method that is called from JavaScript.
        Message.Add("JSMethodTest1 called OK." & vbCrLf)
    End Sub

    Public Sub JSMethodTest2(ByVal Var1 As String, ByVal Var2 As String)
        'Test method that is called from JavaScript.
        Message.Add("Var1 = " & Var1 & " Var2 = " & Var2 & vbCrLf)
    End Sub

    Public Sub JSDisplayXml(ByRef XDoc As XDocument)
        Message.Add(XDoc.ToString & vbCrLf & vbCrLf)
    End Sub

    Public Sub ShowMessage(ByVal Msg As String)
        Message.Add(Msg)
    End Sub

    Public Sub AddText(ByVal Msg As String, ByVal TextType As String)
        Message.AddText(Msg, TextType)
    End Sub

    'END Legacy Code -----------------------------------------------------------------------------------------------


#End Region 'Methods Called by JavaScript -------------------------------------------------------------------------------------------------------------------------------


#Region " Project Events Code"

    Private Sub Project_Message(Msg As String) Handles Project.Message
        'Display the Project message:
        Message.Add(Msg & vbCrLf)
    End Sub

    Private Sub Project_ErrorMessage(Msg As String) Handles Project.ErrorMessage
        'Display the Project error message:
        Message.AddWarning(Msg & vbCrLf)
    End Sub

    Private Sub Project_Closing() Handles Project.Closing
        'The current project is closing.
        CloseProject()
        'SaveFormSettings() 'Save the form settings - they are saved in the Project before is closes.
        'SaveProjectSettings() 'Update this subroutine if project settings need to be saved.
        'Project.Usage.SaveUsageInfo() 'Save the current project usage information.
        'Project.UnlockProject() 'Unlock the current project before it Is closed.
        'If ConnectedToComNet Then DisconnectFromComNet() 'ADDED 9Apr20
    End Sub

    Private Sub CloseProject()
        'Close the Project:
        SaveFormSettings() 'Save the form settings - they are saved in the Project before is closes.
        SaveProjectSettings() 'Update this subroutine if project settings need to be saved.
        Project.Usage.SaveUsageInfo() 'Save the current project usage information.
        Project.UnlockProject() 'Unlock the current project before it Is closed.
        If ConnectedToComNet Then DisconnectFromComNet() 'ADDED 9Apr20
    End Sub

    Private Sub Project_Selected() Handles Project.Selected
        'A new project has been selected.
        OpenProject()
        'RestoreFormSettings()
        'Project.ReadProjectInfoFile()

        'Project.ReadParameters()
        'Project.ReadParentParameters()
        'If Project.ParentParameterExists("ProNetName") Then
        '    Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
        '    ProNetName = Project.Parameter("ProNetName").Value
        'Else
        '    ProNetName = Project.GetParameter("ProNetName")
        'End If
        'If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
        '    Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
        '    ProNetPath = Project.Parameter("ProNetPath").Value
        'Else
        '    ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
        'End If
        'Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

        'Project.LockProject() 'Lock the project while it is open in this application.

        'Project.Usage.StartTime = Now

        'ApplicationInfo.SettingsLocn = Project.SettingsLocn
        'Message.SettingsLocn = Project.SettingsLocn
        'Message.Show() 'Added 18May19

        ''Restore the new project settings:
        'RestoreProjectSettings() 'Update this subroutine if project settings need to be restored.

        'ShowProjectInfo()

        'If Project.ConnectOnOpen Then
        '    ConnectToComNet() 'The Project is set to connect when it is opened.
        'ElseIf ApplicationInfo.ConnectOnStartup Then
        '    ConnectToComNet() 'The Application is set to connect when it is started.
        'Else
        '    'Don't connect to ComNet.
        'End If

    End Sub

    Private Sub OpenProject()
        'Open the Project:
        RestoreFormSettings()
        Project.ReadProjectInfoFile()

        Project.ReadParameters()
        Project.ReadParentParameters()
        If Project.ParentParameterExists("ProNetName") Then
            Project.AddParameter("ProNetName", Project.ParentParameter("ProNetName").Value, Project.ParentParameter("ProNetName").Description) 'AddParameter will update the parameter if it already exists.
            ProNetName = Project.Parameter("ProNetName").Value
        Else
            ProNetName = Project.GetParameter("ProNetName")
        End If
        If Project.ParentParameterExists("ProNetPath") Then 'Get the parent parameter value - it may have been updated.
            Project.AddParameter("ProNetPath", Project.ParentParameter("ProNetPath").Value, Project.ParentParameter("ProNetPath").Description) 'AddParameter will update the parameter if it already exists.
            ProNetPath = Project.Parameter("ProNetPath").Value
        Else
            ProNetPath = Project.GetParameter("ProNetPath") 'If the parameter does not exist, the value is set to ""
        End If
        Project.SaveParameters() 'These should be saved now - child projects look for parent parameters in the parameter file.

        Project.LockProject() 'Lock the project while it is open in this application.

        Project.Usage.StartTime = Now

        ApplicationInfo.SettingsLocn = Project.SettingsLocn
        Message.SettingsLocn = Project.SettingsLocn
        Message.Show() 'Added 18May19

        'Restore the new project settings:
        RestoreProjectSettings() 'Update this subroutine if project settings need to be restored.

        ShowProjectInfo()

        If Project.ConnectOnOpen Then
            ConnectToComNet() 'The Project is set to connect when it is opened.
        ElseIf ApplicationInfo.ConnectOnStartup Then
            ConnectToComNet() 'The Application is set to connect when it is started.
        Else
            'Don't connect to ComNet.
        End If
    End Sub

    Private Sub chkConnect_LostFocus(sender As Object, e As EventArgs) Handles chkConnect.LostFocus
        If chkConnect.Checked Then
            Project.ConnectOnOpen = True
        Else
            Project.ConnectOnOpen = False
        End If
        Project.SaveProjectInfoFile()
    End Sub

#End Region 'Project Events Code

#Region " Online/Offline Code" '=========================================================================================================================================

    Private Sub btnOnline_Click(sender As Object, e As EventArgs) Handles btnOnline.Click
        'Connect to or disconnect from the Message System (ComNet).
        If ConnectedToComNet = False Then
            ConnectToComNet()
        Else
            DisconnectFromComNet()
        End If
    End Sub

    Private Sub ConnectToComNet()
        'Connect to the Message Service. (ComNet)

        If IsNothing(client) Then
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        'UPDATE 14 Feb 2021 - If the VS2019 version of the ADVL Network is running it may not detected by ComNetRunning()!
        'Check if the Message Service is running by trying to open a connection:
        Try
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds (8 seconds is too short for a slow computer!)
            ConnectionName = ApplicationInfo.Name 'This name will be modified if it is already used in an existing connection.
            ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)
            If ConnectionName <> "" Then
                Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                btnOnline.Text = "Online"
                btnOnline.ForeColor = Color.ForestGreen
                ConnectedToComNet = True
                SendApplicationInfo()
                SendProjectInfo()
                client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                bgwComCheck.WorkerReportsProgress = True
                bgwComCheck.WorkerSupportsCancellation = True
                If bgwComCheck.IsBusy Then
                    'The ComCheck thread is already running.
                Else
                    bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                End If
                Exit Sub 'Connection made OK
            Else
                'Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                Message.Add("The Andorville™ Network was not found. Attempting to start it." & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            End If
        Catch ex As System.TimeoutException
            Message.Add("Message Service Check Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        Catch ex As Exception
            Message.Add("Error message: " & ex.Message & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        End Try
        'END UPDATE

        If ComNetRunning() Then
            'The Application.Lock file has been found at AdvlNetworkAppPath
            'The Message Service is Running.
        Else 'The Message Service is NOT running'
            'Start the Andorville™ Network:
            If AdvlNetworkAppPath = "" Then
                Message.AddWarning("Andorville™ Network application path is unknown." & vbCrLf)
            Else
                If System.IO.File.Exists(AdvlNetworkExePath) Then 'OK to start the Message Service application:
                    Shell(Chr(34) & AdvlNetworkExePath & Chr(34), AppWinStyle.NormalFocus) 'Start Message Service application with no argument
                Else
                    'Incorrect Message Service Executable path.
                    Message.AddWarning("Andorville™ Network exe file not found. Service not started." & vbCrLf)
                End If
            End If
        End If

        'Try to fix a faulted client state:
        If client.State = ServiceModel.CommunicationState.Faulted Then
            client = Nothing
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        If client.State = ServiceModel.CommunicationState.Faulted Then
            Message.AddWarning("Client state is faulted. Connection not made!" & vbCrLf)
        Else
            Try
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds (8 seconds is too short for a slow computer!)

                ConnectionName = ApplicationInfo.Name 'This name will be modified if it is already used in an existing connection.
                ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)

                If ConnectionName <> "" Then
                    Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                    btnOnline.Text = "Online"
                    btnOnline.ForeColor = Color.ForestGreen
                    ConnectedToComNet = True
                    SendApplicationInfo()
                    SendProjectInfo()
                    client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                    bgwComCheck.WorkerReportsProgress = True
                    bgwComCheck.WorkerSupportsCancellation = True
                    If bgwComCheck.IsBusy Then
                        'The ComCheck thread is already running.
                    Else
                        bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                    End If

                Else
                    Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                End If
            Catch ex As System.TimeoutException
                Message.Add("Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
            Catch ex As Exception
                Message.Add("Error message: " & ex.Message & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            End Try
        End If
    End Sub

    Private Sub ConnectToComNet(ByVal ConnName As String)
        'Connect to the Message Service (ComNet) with the connection name ConnName.

        'UPDATE 14 Feb 2021 - If the VS2019 version of the ADVL Network is running it may not be detected by ComNetRunning()!
        'Check if the Message Service is running by trying to open a connection:

        If IsNothing(client) Then
            client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
        End If

        Try
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeaout to 16 seconds (8 seconds is too short for a slow computer!)
            ConnectionName = ConnName 'This name will be modified if it is already used in an existing connection.
            ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)
            If ConnectionName <> "" Then
                Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                btnOnline.Text = "Online"
                btnOnline.ForeColor = Color.ForestGreen
                ConnectedToComNet = True
                SendApplicationInfo()
                SendProjectInfo()
                client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                bgwComCheck.WorkerReportsProgress = True
                bgwComCheck.WorkerSupportsCancellation = True
                If bgwComCheck.IsBusy Then
                    'The ComCheck thread is already running.
                Else
                    bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                End If
                Exit Sub 'Connection made OK
            Else
                'Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                Message.Add("The Andorville™ Network was not found. Attempting to start it." & vbCrLf)
                client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            End If
        Catch ex As System.TimeoutException
            Message.Add("Message Service Check Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        Catch ex As Exception
            Message.Add("Error message: " & ex.Message & vbCrLf)
            client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
            Message.Add("Attempting to start the Message Service." & vbCrLf)
        End Try
        'END UPDATE

        If ConnectedToComNet = False Then
            If IsNothing(client) Then
                client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
            End If

            'Try to fix a faulted client state:
            If client.State = ServiceModel.CommunicationState.Faulted Then
                client = Nothing
                client = New ServiceReference1.MsgServiceClient(New System.ServiceModel.InstanceContext(New MsgServiceCallback))
            End If

            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.AddWarning("client state is faulted. Connection not made!" & vbCrLf)
            Else
                Try
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(0, 0, 16) 'Temporarily set the send timeout to 16 seconds (8 seconds is too short for a slow computer!)
                    ConnectionName = ConnName 'This name will be modified if it is already used in an existing connection.
                    ConnectionName = client.Connect(ProNetName, ApplicationInfo.Name, ConnectionName, Project.Name, Project.Description, Project.Type, Project.Path, False, False)

                    If ConnectionName <> "" Then
                        Message.Add("Connected to the Andorville™ Network with Connection Name: [" & ProNetName & "]." & ConnectionName & vbCrLf)
                        client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                        btnOnline.Text = "Online"
                        btnOnline.ForeColor = Color.ForestGreen
                        ConnectedToComNet = True
                        SendApplicationInfo()
                        SendProjectInfo()
                        client.GetAdvlNetworkAppInfoAsync() 'Update the Exe Path in case it has changed. This path may be needed in the future to start the ComNet (Message Service).

                        bgwComCheck.WorkerReportsProgress = True
                        bgwComCheck.WorkerSupportsCancellation = True
                        If bgwComCheck.IsBusy Then
                            'The ComCheck thread is already running.
                        Else
                            bgwComCheck.RunWorkerAsync() 'Start the ComCheck thread.
                        End If

                    Else
                        Message.Add("Connection to the Andorville™ Network failed!" & vbCrLf)
                        client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                    End If
                Catch ex As System.TimeoutException
                    Message.Add("Timeout error. Check if the Andorville™ Network (Message Service) is running." & vbCrLf)
                Catch ex As Exception
                    Message.Add("Error message: " & ex.Message & vbCrLf)
                    client.Endpoint.Binding.SendTimeout = New System.TimeSpan(1, 0, 0) 'Restore the send timeout to 1 hour
                End Try
            End If
        Else
            Message.AddWarning("Already connected to the Andorville™ Network (Message Service)." & vbCrLf)
        End If
    End Sub

    Private Sub DisconnectFromComNet()
        'Disconnect from the Communication Network (Message Service).

        If ConnectedToComNet = True Then
            If IsNothing(client) Then
                Message.Add("Already disconnected from the Andorville™ Network (Message Service)." & vbCrLf)
                btnOnline.Text = "Offline"
                btnOnline.ForeColor = Color.Red
                ConnectedToComNet = False
                ConnectionName = ""
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("client state is faulted." & vbCrLf)
                    ConnectionName = ""
                Else
                    Try
                        client.Disconnect(ProNetName, ConnectionName)
                        btnOnline.Text = "Offline"
                        btnOnline.ForeColor = Color.Red
                        ConnectedToComNet = False
                        ConnectionName = ""
                        Message.Add("Disconnected from the Andorville™ Network (Message Service)." & vbCrLf)

                        If bgwComCheck.IsBusy Then
                            bgwComCheck.CancelAsync()
                        End If

                    Catch ex As Exception
                        Message.AddWarning("Error disconnecting from Andorville™ Network (Message Service): " & ex.Message & vbCrLf)
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub SendApplicationInfo()
        'Send the application information to the Network application.

        If IsNothing(client) Then
            Message.Add("No client connection available!" & vbCrLf)
        Else
            If client.State = ServiceModel.CommunicationState.Faulted Then
                Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
            Else
                'Create the XML instructions to send application information.
                Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                Dim applicationInfo As New XElement("ApplicationInfo")
                Dim name As New XElement("Name", Me.ApplicationInfo.Name)
                applicationInfo.Add(name)

                Dim text As New XElement("Text", "Bayes")
                applicationInfo.Add(text)

                Dim exePath As New XElement("ExecutablePath", Me.ApplicationInfo.ExecutablePath)
                applicationInfo.Add(exePath)

                Dim directory As New XElement("Directory", Me.ApplicationInfo.ApplicationDir)
                applicationInfo.Add(directory)
                Dim description As New XElement("Description", Me.ApplicationInfo.Description)
                applicationInfo.Add(description)
                xmessage.Add(applicationInfo)
                doc.Add(xmessage)

                'Show the message sent to ComNet:
                Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                Message.XAddXml(doc.ToString)
                Message.XAddText(vbCrLf, "Normal") 'Add extra line

                client.SendMessage("", "MessageService", doc.ToString)
            End If
        End If
    End Sub

    Private Sub SendProjectInfo()
        'Send the project information to the Network application.

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    Dim Path As New XElement("Path", Project.Path)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to the Message Service:
                    Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    client.SendMessage("", "MessageService", doc.ToString)
                End If
            End If
        End If
    End Sub

    Public Sub SendProjectInfo(ByVal ProjectPath As String)
        'Send the project information to the Network application.
        'This version of SendProjectInfo uses the ProjectPath argument.

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    'Dim Path As New XElement("Path", Project.Path)
                    Dim Path As New XElement("Path", ProjectPath)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to the Message Service:
                    Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    client.SendMessage("", "MessageService", doc.ToString)
                End If
            End If
        End If
    End Sub

    Private Function ComNetRunning() As Boolean
        'Return True if ComNet (Message Service) is running.
        ''If System.IO.File.Exists(MsgServiceAppPath & "\Application.Lock") Then
        'If System.IO.File.Exists(AdvlNetworkAppPath & "\Application.Lock") Then
        '    Return True
        'Else
        '    Return False
        'End If

        'If MsgServiceAppPath = "" Then
        If AdvlNetworkAppPath = "" Then
            'Message.Add("Message Service application path is not known." & vbCrLf)
            Message.Add("Andorville™ Network application path is not known." & vbCrLf)
            'Message.Add("Run the Message Service before connecting to update the path." & vbCrLf)
            Message.Add("Run the Andorville™ Network before connecting to update the path." & vbCrLf)
            Return False
        Else
            'If System.IO.File.Exists(MsgServiceAppPath & "\Application.Lock") Then
            If System.IO.File.Exists(AdvlNetworkAppPath & "\Application.Lock") Then
                'Message.Add("AppLock found - ComNet is running." & vbCrLf)
                Return True
            Else
                'Message.Add("AppLock not found - ComNet is running." & vbCrLf)
                Return False
            End If
        End If

    End Function

#End Region 'Online/Offline code ----------------------------------------------------------------------------------------------------------------------------------------

    Private Sub TabPage2_Enter(sender As Object, e As EventArgs) Handles TabPage2.Enter
        'Update the current duration:

        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                           Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                           Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                           Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                           Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                           Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                           Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

        Timer1.Interval = 5000 '5 seconds
        Timer1.Enabled = True
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Update the current duration:

        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
        '                   Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
        '                   Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
        '                   Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                   Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                   Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                   Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"
    End Sub

    Private Sub TabPage2_Leave(sender As Object, e As EventArgs) Handles TabPage2.Leave
        Timer1.Enabled = False
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Add the current project to the Message Service list.

        If Project.ParentProjectName <> "" Then
            Message.AddWarning("This project has a parent: " & Project.ParentProjectName & vbCrLf)
            Message.AddWarning("Child projects can not be added to the list." & vbCrLf)
            Exit Sub
        End If

        If ConnectedToComNet = False Then
            Message.AddWarning("The application is not connected to the Message Service." & vbCrLf)
        Else 'Connected to the Message Service (ComNet).
            If IsNothing(client) Then
                Message.Add("No client connection available!" & vbCrLf)
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    Message.Add("Client state is faulted. Message not sent!" & vbCrLf)
                Else
                    'Construct the XMessage to send to AppNet:
                    Dim decl As New XDeclaration("1.0", "utf-8", "yes")
                    Dim doc As New XDocument(decl, Nothing) 'Create an XDocument to store the instructions.
                    Dim xmessage As New XElement("XMsg") 'This indicates the start of the message in the XMessage class
                    Dim projectInfo As New XElement("ProjectInfo")

                    Dim Path As New XElement("Path", Project.Path)
                    projectInfo.Add(Path)
                    xmessage.Add(projectInfo)
                    doc.Add(xmessage)

                    'Show the message sent to AppNet:
                    Message.XAddText("Message sent to " & "Message Service" & ":" & vbCrLf, "XmlSentNotice")
                    Message.XAddXml(doc.ToString)
                    Message.XAddText(vbCrLf, "Normal") 'Add extra line
                    client.SendMessage("", "MessageService", doc.ToString)
                End If
            End If
        End If
    End Sub

    Private Sub btnOpenProject_Click(sender As Object, e As EventArgs) Handles btnOpenProject.Click

        If Project.Type = ADVL_Utilities_Library_1.Project.Types.Archive Then
            If IsNothing(ProjectArchive) Then
                ProjectArchive = New frmArchive
                ProjectArchive.Show()
                ProjectArchive.Title = "Project Archive"
                ProjectArchive.Path = Project.Path
            Else
                ProjectArchive.Show()
                ProjectArchive.BringToFront()
            End If
        Else
            Process.Start(Project.Path)
        End If

    End Sub

    Private Sub ProjectArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles ProjectArchive.FormClosed
        ProjectArchive = Nothing
    End Sub

    Private Sub btnOpenSettings_Click(sender As Object, e As EventArgs) Handles btnOpenSettings.Click
        If Project.SettingsLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.SettingsLocn.Path)
        ElseIf Project.SettingsLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Archive Then
            If IsNothing(SettingsArchive) Then
                SettingsArchive = New frmArchive
                SettingsArchive.Show()
                SettingsArchive.Title = "Settings Archive"
                SettingsArchive.Path = Project.SettingsLocn.Path
            Else
                SettingsArchive.Show()
                SettingsArchive.BringToFront()
            End If
        End If
    End Sub

    Private Sub SettingsArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles SettingsArchive.FormClosed
        SettingsArchive = Nothing
    End Sub

    Private Sub btnOpenData_Click(sender As Object, e As EventArgs) Handles btnOpenData.Click
        If Project.DataLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.DataLocn.Path)
        ElseIf Project.DataLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Archive Then
            If IsNothing(DataArchive) Then
                DataArchive = New frmArchive
                DataArchive.Show()
                DataArchive.Title = "Data Archive"
                DataArchive.Path = Project.DataLocn.Path
            Else
                DataArchive.Show()
                DataArchive.BringToFront()
            End If
        End If
    End Sub

    Private Sub DataArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles DataArchive.FormClosed
        DataArchive = Nothing
    End Sub


    Private Sub btnOpenSystem_Click(sender As Object, e As EventArgs) Handles btnOpenSystem.Click
        If Project.SystemLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Directory Then
            Process.Start(Project.SystemLocn.Path)
        ElseIf Project.SystemLocn.Type = ADVL_Utilities_Library_1.FileLocation.Types.Archive Then
            If IsNothing(SystemArchive) Then
                SystemArchive = New frmArchive
                SystemArchive.Show()
                SystemArchive.Title = "System Archive"
                SystemArchive.Path = Project.SystemLocn.Path
            Else
                SystemArchive.Show()
                SystemArchive.BringToFront()
            End If
        End If
    End Sub

    Private Sub SystemArchive_FormClosed(sender As Object, e As FormClosedEventArgs) Handles SystemArchive.FormClosed
        SystemArchive = Nothing
    End Sub

    Private Sub btnOpenAppDir_Click(sender As Object, e As EventArgs) Handles btnOpenAppDir.Click
        Process.Start(ApplicationInfo.ApplicationDir)
    End Sub

    Private Sub btnCreateArchive_Click(sender As Object, e As EventArgs) Handles btnCreateArchive.Click
        'Create a Project Archive file.
        If Project.Type = ADVL_Utilities_Library_1.Project.Types.Archive Then
            Message.Add("The Project is an Archive type. It is already in an archived format." & vbCrLf)

        Else
            'The project is contained in the directory Project.Path.
            'This directory and contents will be saved in a zip file in the parent directory with the same name but with extension .AdvlArchive.

            Dim ParentDir As String = System.IO.Directory.GetParent(Project.Path).FullName
            Dim ProjectArchiveName As String = System.IO.Path.GetFileName(Project.Path) & ".AdvlArchive"

            If My.Computer.FileSystem.FileExists(ParentDir & "\" & ProjectArchiveName) Then 'The Project Archive file already exists.
                Message.Add("The Project Archive file already exists: " & ParentDir & "\" & ProjectArchiveName & vbCrLf)
            Else 'The Project Archive file does not exist. OK to create the Archive.
                System.IO.Compression.ZipFile.CreateFromDirectory(Project.Path, ParentDir & "\" & ProjectArchiveName)

                'Remove all Lock files:
                Dim Zip As System.IO.Compression.ZipArchive
                Zip = System.IO.Compression.ZipFile.Open(ParentDir & "\" & ProjectArchiveName, IO.Compression.ZipArchiveMode.Update)
                Dim DeleteList As New List(Of String) 'List of entry names to delete
                Dim myEntry As System.IO.Compression.ZipArchiveEntry
                For Each entry As System.IO.Compression.ZipArchiveEntry In Zip.Entries
                    If entry.Name = "Project.Lock" Then
                        DeleteList.Add(entry.FullName)
                    End If
                Next
                For Each item In DeleteList
                    myEntry = Zip.GetEntry(item)
                    myEntry.Delete()
                Next
                Zip.Dispose()

                Message.Add("Project Archive file created: " & ParentDir & "\" & ProjectArchiveName & vbCrLf)
            End If
        End If
    End Sub

    Private Sub btnOpenArchive_Click(sender As Object, e As EventArgs) Handles btnOpenArchive.Click
        'Open a Project Archive file.

        'Use the OpenFileDialog to look for an .AdvlArchive file.      
        OpenFileDialog1.Title = "Select an Archived Project File"
        OpenFileDialog1.InitialDirectory = System.IO.Directory.GetParent(Project.Path).FullName 'Start looking in the ParentDir.
        OpenFileDialog1.Filter = "Archived Project|*.AdvlArchive"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Dim FileName As String = OpenFileDialog1.FileName
            OpenArchivedProject(FileName)
        End If
    End Sub

    Private Sub OpenArchivedProject(ByVal FilePath As String)
        'Open the archived project at the specified path.

        Dim Zip As System.IO.Compression.ZipArchive
        Try
            Zip = System.IO.Compression.ZipFile.OpenRead(FilePath)

            Dim Entry As System.IO.Compression.ZipArchiveEntry = Zip.GetEntry("Project_Info_ADVL_2.xml")
            If IsNothing(Entry) Then
                Message.AddWarning("The file is not an Archived Andorville Project." & vbCrLf)
                'Check if it is an Archive project type with a .AdvlProject extension.
                'NOTE: These are already zip files so no need to archive.

            Else
                Message.Add("The file is an Archived Andorville Project." & vbCrLf)
                Dim ParentDir As String = System.IO.Directory.GetParent(FilePath).FullName
                Dim ProjectName As String = System.IO.Path.GetFileNameWithoutExtension(FilePath)
                Message.Add("The Project will be expanded in the directory: " & ParentDir & vbCrLf)
                Message.Add("The Project name will be: " & ProjectName & vbCrLf)
                Zip.Dispose()
                If System.IO.Directory.Exists(ParentDir & "\" & ProjectName) Then
                    Message.AddWarning("The Project already exists: " & ParentDir & "\" & ProjectName & vbCrLf)
                Else
                    System.IO.Compression.ZipFile.ExtractToDirectory(FilePath, ParentDir & "\" & ProjectName) 'Extract the project from the archive                   
                    Project.AddProjectToList(ParentDir & "\" & ProjectName)
                    'Open the new project                 
                    CloseProject()  'Close the current project
                    Project.SelectProject(ParentDir & "\" & ProjectName) 'Select the project at the specifed path.
                    OpenProject() 'Open the selected project.
                End If
            End If
        Catch ex As Exception
            Message.AddWarning("Error opening Archived Andorville Project: " & ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub TabPage2_DragEnter(sender As Object, e As DragEventArgs) Handles TabPage2.DragEnter
        'DragEnter: An object has been dragged into TabPage2 - Project Information tab.
        'This code is required to get the link to the item(s) being dragged into Project Information:
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Link
        End If
    End Sub

    Private Sub TabPage2_DragDrop(sender As Object, e As DragEventArgs) Handles TabPage2.DragDrop
        'A file has been dropped into the Project Information tab.

        Dim Path As String()
        Path = e.Data.GetData(DataFormats.FileDrop)
        Dim I As Integer

        If Path.Count > 0 Then
            If Path.Count > 1 Then
                Message.AddWarning("More than one file has been dropped into the Project Information tab. Only the first one will be opened." & vbCrLf)
            End If

            Try
                Dim ArchivedProjectPath As String = Path(0)
                If ArchivedProjectPath.EndsWith(".AdvlArchive") Then
                    Message.Add("The archived project will be opened: " & vbCrLf & ArchivedProjectPath & vbCrLf)
                    OpenArchivedProject(ArchivedProjectPath)
                Else
                    Message.Add("The dropped file is not an archived project: " & vbCrLf & ArchivedProjectPath & vbCrLf)
                End If
            Catch ex As Exception
                Message.AddWarning("Error opening dropped archived project. " & ex.Message & vbCrLf)
            End Try
        End If
    End Sub

    Private Sub btnOpenParentDir_Click(sender As Object, e As EventArgs) Handles btnOpenParentDir.Click
        'Open the Parent directory of the selected project.
        Dim ParentDir As String = System.IO.Directory.GetParent(Project.Path).FullName
        If System.IO.Directory.Exists(ParentDir) Then
            Process.Start(ParentDir)
        Else
            Message.AddWarning("The parent directory was not found: " & ParentDir & vbCrLf)
        End If

    End Sub


#Region " Process XMessages" '===========================================================================================================================================

    Private Sub XMsg_Instruction(Data As String, Locn As String) Handles XMsg.Instruction
        'Process an XMessage instruction.
        'An XMessage is a simplified XSequence. It is used to exchange information between Andorville™ applications.
        '
        'An XSequence file is an AL-H7™ Information Sequence stored in an XML format.
        'AL-H7™ is the name of a programming system that uses sequences of data and location value pairs to store information or processing steps.
        'Any program, mathematical expression or data set can be expressed as an Information Sequence.

        'Add code here to process the XMessage instructions.
        'See other Andorville™ applications for examples.

        If IsDBNull(Data) Then
            Data = ""
        End If

        'Intercept instructions with the prefix "WebPage_"
        If Locn.StartsWith("WebPage_") Then 'Send the Data, Location data to the correct Web Page:
            'Message.Add("Web Page Location: " & Locn & vbCrLf)
            If Locn.Contains(":") Then
                Dim EndOfWebPageNoString As Integer = Locn.IndexOf(":")
                If Locn.Contains("-") Then
                    Dim HyphenLocn As Integer = Locn.IndexOf("-")
                    If HyphenLocn < EndOfWebPageNoString Then 'Web Page Location contains a sub-location in the web page - WebPage_1-SubLocn:Locn - SubLocn:Locn will be sent to Web page 1
                        EndOfWebPageNoString = HyphenLocn
                    End If
                End If
                Dim PageNoLen As Integer = EndOfWebPageNoString - 8
                Dim WebPageNoString As String = Locn.Substring(8, PageNoLen)
                Dim WebPageNo As Integer = CInt(WebPageNoString)
                Dim WebPageData As String = Data
                Dim WebPageLocn As String = Locn.Substring(EndOfWebPageNoString + 1)

                'Message.Add("WebPageData = " & WebPageData & "  WebPageLocn = " & WebPageLocn & vbCrLf)

                WebPageFormList(WebPageNo).XMsgInstruction(WebPageData, WebPageLocn)
            Else
                Message.AddWarning("XMessage instruction location is not complete: " & Locn & vbCrLf)
            End If
        Else

            Select Case Locn

                Case "ClientProNetName"
                    ClientProNetName = Data 'The name of the Client Application Network requesting service. AD

                Case "ClientName"
                    ClientAppName = Data 'The name of the Client application requesting service.

                Case "ClientConnectionName"
                    ClientConnName = Data 'The name of the client connection requesting service.

                Case "ClientLocn" 'The Location within the Client requesting service.
                    Dim statusOK As New XElement("Status", "OK") 'Add Status OK element when the Client Location is changed
                    xlocns(xlocns.Count - 1).Add(statusOK)

                    xmessage.Add(xlocns(xlocns.Count - 1)) 'Add the instructions for the last location to the reply xmessage
                    xlocns.Add(New XElement(Data)) 'Start the new location instructions

                Case "OnCompletion"
                    OnCompletionInstruction = Data

                Case "Main"
                 'Blank message - do nothing.

                Case "Main:EndInstruction"
                    Select Case Data
                        Case "Stop"
                            'Stop at the end of the instruction sequence.

                            'Add other cases here:
                    End Select

                Case "Main:Status"
                    Select Case Data
                        Case "OK"
                            'Main instructions completed OK
                    End Select



                Case "Command"
                    Select Case Data
                        Case "ConnectToComNet" 'Startup Command
                            If ConnectedToComNet = False Then
                                ConnectToComNet()
                            End If
                        Case "AppComCheck"
                            'Add the Appplication Communication info to the reply message:
                            Dim clientProNetName As New XElement("ClientProNetName", ProNetName) 'The Project Network Name
                            xlocns(xlocns.Count - 1).Add(clientProNetName)
                            Dim clientName As New XElement("ClientName", "ADVL_Bayes") 'The name of this application.
                            xlocns(xlocns.Count - 1).Add(clientName)
                            Dim clientConnectionName As New XElement("ClientConnectionName", ConnectionName)
                            xlocns(xlocns.Count - 1).Add(clientConnectionName)
                            '<Status>OK</Status> will be automatically appended to the XMessage before it is sent.
                    End Select


            'Startup Command Arguments ================================================
                Case "ProNetName"
                'This is currently not used.
                'The ProNetName is determined elsewhere.

                Case "ProjectName"
                    If Project.OpenProject(Data) = True Then
                        ProjectSelected = True 'Project has been opened OK.
                    Else
                        ProjectSelected = False 'Project could not be opened.
                    End If

                Case "ProjectID"
                    Message.AddWarning("Add code to handle ProjectID parameter at StartUp!" & vbCrLf)
                'Note the ComNet will usually select a project using ProjectPath.

                Case "ProjectPath"
                    If Project.OpenProjectPath(Data) = True Then
                        ProjectSelected = True 'Project has been opened OK.
                        'THE PROJECT IS LOCKED IN THE Form.Load EVENT:

                        ApplicationInfo.SettingsLocn = Project.SettingsLocn
                        Message.SettingsLocn = Project.SettingsLocn 'Set up the Message object
                        Message.Show() 'Added 18May19

                        'txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                        '              Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                        '              Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                        '              Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c)

                        'txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & ":" &
                        '               Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & ":" &
                        '               Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & ":" &
                        '               Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c)

                        txtTotalDuration.Text = Project.Usage.TotalDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                        Project.Usage.TotalDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                        Project.Usage.TotalDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                        Project.Usage.TotalDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

                        txtCurrentDuration.Text = Project.Usage.CurrentDuration.Days.ToString.PadLeft(5, "0"c) & "d:" &
                                       Project.Usage.CurrentDuration.Hours.ToString.PadLeft(2, "0"c) & "h:" &
                                       Project.Usage.CurrentDuration.Minutes.ToString.PadLeft(2, "0"c) & "m:" &
                                       Project.Usage.CurrentDuration.Seconds.ToString.PadLeft(2, "0"c) & "s"

                    Else
                        ProjectSelected = False 'Project could not be opened.
                        Message.AddWarning("Project could not be opened at path: " & Data & vbCrLf)
                    End If

                Case "ConnectionName"
                    StartupConnectionName = Data
            '--------------------------------------------------------------------------

            'Application Information  =================================================
            'returned by client.GetAdvlNetworkAppInfoAsync()
                Case "AdvlNetworkAppInfo:Name"
                'The name of the Andorville™ Network Application. (Not used.)

                Case "AdvlNetworkAppInfo:ExePath"
                    'The executable file path of the Andorville™ Network Application.
                    AdvlNetworkExePath = Data

                Case "AdvlNetworkAppInfo:Path"
                    'The path of the Andorville™ Network Application (ComNet). (This is where an Application.Lock file will be found while ComNet is running.)
                    AdvlNetworkAppPath = Data
           '---------------------------------------------------------------------------

           'Message Window Instructions  ==============================================
                Case "MessageWindow:Left"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Left = Data
                Case "MessageWindow:Top"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Top = Data
                Case "MessageWindow:Width"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Width = Data
                Case "MessageWindow:Height"
                    If IsNothing(Message.MessageForm) Then
                        Message.ApplicationName = ApplicationInfo.Name
                        Message.SettingsLocn = Project.SettingsLocn
                        Message.Show()
                    End If
                    Message.MessageForm.Height = Data
                Case "MessageWindow:Command"
                    Select Case Data
                        Case "BringToFront"
                            If IsNothing(Message.MessageForm) Then
                                Message.ApplicationName = ApplicationInfo.Name
                                Message.SettingsLocn = Project.SettingsLocn
                                Message.Show()
                            End If
                            Message.MessageForm.Activate()
                            Message.MessageForm.TopMost = True
                            Message.MessageForm.TopMost = False
                        Case "SaveSettings"
                            Message.MessageForm.SaveFormSettings()
                    End Select

            '---------------------------------------------------------------------------

            'Command to bring the Application window to the front:
                Case "ApplicationWindow:Command"
                    Select Case Data
                        Case "BringToFront"
                            Me.Activate()
                            Me.TopMost = True
                            Me.TopMost = False
                    End Select

                Case "EndOfSequence"
                    'End of Information Sequence reached.
                    'Add Status OK element at the end of the sequence:
                    Dim statusOK As New XElement("Status", "OK")
                    xlocns(xlocns.Count - 1).Add(statusOK)

                    Select Case EndInstruction
                        Case "Stop"
                            'No instructions.

                            'Add any other Cases here:

                        Case Else
                            Message.AddWarning("Unknown End Instruction: " & EndInstruction & vbCrLf)
                    End Select
                    EndInstruction = "Stop"

                    'Add the final EndInstruction:
                    If OnCompletionInstruction = "Stop" Then
                        'Final EndInstruction is not required.
                    Else
                        Dim xEndInstruction As New XElement("EndInstruction", OnCompletionInstruction)
                        xlocns(xlocns.Count - 1).Add(xEndInstruction)
                        OnCompletionInstruction = "Stop" 'Reset the OnCompletion Instruction
                    End If

                Case Else
                    Message.AddWarning("Unknown location: " & Locn & vbCrLf)
                    Message.AddWarning("            data: " & Data & vbCrLf & vbCrLf)
            End Select
        End If
    End Sub

    Private Sub XMsgLocal_Instruction(Data As String, Locn As String) Handles XMsgLocal.Instruction
        'Process an XMessage instruction locally.

        If IsDBNull(Data) Then
            Data = ""
        End If

        'Intercept instructions with the prefix "WebPage_"
        If Locn.StartsWith("WebPage_") Then 'Send the Data, Location data to the correct Web Page:
            'Message.Add("Web Page Location: " & Locn & vbCrLf)
            If Locn.Contains(":") Then
                Dim EndOfWebPageNoString As Integer = Locn.IndexOf(":")
                If Locn.Contains("-") Then
                    Dim HyphenLocn As Integer = Locn.IndexOf("-")
                    If HyphenLocn < EndOfWebPageNoString Then 'Web Page Location contains a sub-location in the web page - WebPage_1-SubLocn:Locn - SubLocn:Locn will be sent to Web page 1
                        EndOfWebPageNoString = HyphenLocn
                    End If
                End If
                Dim PageNoLen As Integer = EndOfWebPageNoString - 8
                Dim WebPageNoString As String = Locn.Substring(8, PageNoLen)
                Dim WebPageNo As Integer = CInt(WebPageNoString)
                Dim WebPageData As String = Data
                Dim WebPageLocn As String = Locn.Substring(EndOfWebPageNoString + 1)

                'Message.Add("WebPageData = " & WebPageData & "  WebPageLocn = " & WebPageLocn & vbCrLf)

                WebPageFormList(WebPageNo).XMsgInstruction(WebPageData, WebPageLocn)
            Else
                Message.AddWarning("XMessage instruction location is not complete: " & Locn & vbCrLf)
            End If
        Else

            Select Case Locn
                Case "ClientName"
                    ClientAppName = Data 'The name of the Client requesting service.

                'UPDATE:
                Case "OnCompletion"
                    OnCompletionInstruction = Data

                Case "Main"
                 'Blank message - do nothing.


                Case "Main:EndInstruction"
                    Select Case Data
                        Case "Stop"
                            'Stop at the end of the instruction sequence.

                            'Add other cases here:
                    End Select

                Case "Main:Status"
                    Select Case Data
                        Case "OK"
                            'Main instructions completed OK
                    End Select

                Case "EndOfSequence"
                    'End of Information Vector Sequence reached.

                Case Else
                    Message.AddWarning("Local XMessage: " & Locn & vbCrLf)
                    Message.AddWarning("Unknown location: " & Locn & vbCrLf)
                    Message.AddWarning("            data: " & Data & vbCrLf & vbCrLf)
            End Select
        End If
    End Sub



#End Region 'Process XMessages ------------------------------------------------------------------------------------------------------------------------------------------


    Private Sub ToolStripMenuItem1_EditWorkflowTabPage_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1_EditWorkflowTabPage.Click
        'Edit the Workflow Web Page:

        If WorkflowFileName = "" Then
            Message.AddWarning("No page to edit." & vbCrLf)
        Else
            Dim FormNo As Integer = OpenNewHtmlDisplayPage()
            HtmlDisplayFormList(FormNo).FileName = WorkflowFileName
            HtmlDisplayFormList(FormNo).OpenDocument
        End If

    End Sub

    Private Sub ToolStripMenuItem1_ShowStartPageInWorkflowTab_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1_ShowStartPageInWorkflowTab.Click
        'Show the Start Page in the Workflow Tab:
        OpenStartPage()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    'Private Sub Main_Move(sender As Object, e As EventArgs) Handles Me.Move
    '    txtLeft.Text = Me.Left
    '    txtTop.Text = Me.Top
    'End Sub

    Private Sub bgwComCheck_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwComCheck.DoWork
        'The communications check thread.
        While ConnectedToComNet
            Try
                If client.IsAlive() Then
                    bgwComCheck.ReportProgress(1, Format(Now, "HH:mm:ss") & " Connection OK." & vbCrLf)
                Else
                    bgwComCheck.ReportProgress(1, Format(Now, "HH:mm:ss") & " Connection Fault.")
                End If
            Catch ex As Exception
                bgwComCheck.ReportProgress(1, "Error in bgeComCheck_DoWork!" & vbCrLf)
                bgwComCheck.ReportProgress(1, ex.Message & vbCrLf)
            End Try

            'System.Threading.Thread.Sleep(60000) 'Sleep time in milliseconds (60 seconds) - For testing only.
            'System.Threading.Thread.Sleep(3600000) 'Sleep time in milliseconds (60 minutes)
            System.Threading.Thread.Sleep(1800000) 'Sleep time in milliseconds (30 minutes)
        End While
    End Sub

    Private Sub bgwComCheck_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwComCheck.ProgressChanged
        Message.Add(e.UserState.ToString) 'Show the ComCheck message 
    End Sub

    Private Sub XMsg_ErrorMsg(ErrMsg As String) Handles XMsg.ErrorMsg
        Message.AddWarning(ErrMsg & vbCrLf)
    End Sub

    Private Sub bgwSendMessage_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwSendMessage.DoWork
        'Send a message on a separate thread:
        Try
            If IsNothing(client) Then
                bgwSendMessage.ReportProgress(1, "No Connection available. Message not sent!")
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    bgwSendMessage.ReportProgress(1, "Connection state is faulted. Message not sent!")
                Else
                    Dim SendMessageParams As clsSendMessageParams = e.Argument
                    client.SendMessage(SendMessageParams.ProjectNetworkName, SendMessageParams.ConnectionName, SendMessageParams.Message)
                End If
            End If
        Catch ex As Exception
            bgwSendMessage.ReportProgress(1, ex.Message)
        End Try
    End Sub

    Private Sub bgwSendMessage_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwSendMessage.ProgressChanged
        'Display an error message:
        Message.AddWarning("Send Message error: " & e.UserState.ToString & vbCrLf) 'Show the bgwSendMessage message 
    End Sub

    Private Sub bgwSendMessageAlt_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwSendMessageAlt.DoWork
        'Alternative SendMessage background worker - used to send a message while instructions are being processed. 
        'Send a message on a separate thread
        Try
            If IsNothing(client) Then
                bgwSendMessageAlt.ReportProgress(1, "No Connection available. Message not sent!")
            Else
                If client.State = ServiceModel.CommunicationState.Faulted Then
                    bgwSendMessageAlt.ReportProgress(1, "Connection state is faulted. Message not sent!")
                Else
                    Dim SendMessageParamsAlt As clsSendMessageParams = e.Argument
                    client.SendMessage(SendMessageParamsAlt.ProjectNetworkName, SendMessageParamsAlt.ConnectionName, SendMessageParamsAlt.Message)
                End If
            End If
        Catch ex As Exception
            bgwSendMessageAlt.ReportProgress(1, ex.Message)
        End Try
    End Sub

    Private Sub bgwSendMessageAlt_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwSendMessageAlt.ProgressChanged
        'Display an error message:
        Message.AddWarning("Send Message error: " & e.UserState.ToString & vbCrLf) 'Show the bgwSendMessageAlt message 
    End Sub

    Private Sub bgwRunInstruction_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgwRunInstruction.DoWork
        'Run a single instruction.
        Try
            Dim Instruction As clsInstructionParams = e.Argument
            XMsg_Instruction(Instruction.Info, Instruction.Locn)
        Catch ex As Exception
            bgwRunInstruction.ReportProgress(1, ex.Message)
        End Try
    End Sub

    Private Sub bgwRunInstruction_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bgwRunInstruction.ProgressChanged
        'Display an error message:
        Message.AddWarning("Run Instruction error: " & e.UserState.ToString & vbCrLf) 'Show the bgwRunInstruction message 
    End Sub


    Private Sub btnShowProjectInfo_Click(sender As Object, e As EventArgs) Handles btnShowProjectInfo.Click
        'Show the current Project information:
        Message.Add("--------------------------------------------------------------------------------------" & vbCrLf)
        Message.Add("Project ------------------------ " & vbCrLf)
        Message.Add("   Name: " & Project.Name & vbCrLf)
        Message.Add("   Type: " & Project.Type.ToString & vbCrLf)
        Message.Add("   Description: " & Project.Description & vbCrLf)
        Message.Add("   Creation Date: " & Project.CreationDate & vbCrLf)
        Message.Add("   ID: " & Project.ID & vbCrLf)
        Message.Add("   Relative Path: " & Project.RelativePath & vbCrLf)
        Message.Add("   Path: " & Project.Path & vbCrLf & vbCrLf)

        Message.Add("Parent Project ----------------- " & vbCrLf)
        Message.Add("   Name: " & Project.ParentProjectName & vbCrLf)
        Message.Add("   Path: " & Project.ParentProjectPath & vbCrLf)

        Message.Add("Application -------------------- " & vbCrLf)
        Message.Add("   Name: " & Project.Application.Name & vbCrLf)
        Message.Add("   Description: " & Project.Application.Description & vbCrLf)
        Message.Add("   Path: " & Project.ApplicationDir & vbCrLf)

        Message.Add("Settings ----------------------- " & vbCrLf)
        Message.Add("   Settings Relative Location Type: " & Project.SettingsRelLocn.Type.ToString & vbCrLf)
        Message.Add("   Settings Relative Location Path: " & Project.SettingsRelLocn.Path & vbCrLf)
        Message.Add("   Settings Location Type: " & Project.SettingsLocn.Type.ToString & vbCrLf)
        Message.Add("   Settings Location Path: " & Project.SettingsLocn.Path & vbCrLf)

        Message.Add("Data --------------------------- " & vbCrLf)
        Message.Add("   Data Relative Location Type: " & Project.DataRelLocn.Type.ToString & vbCrLf)
        Message.Add("   Data Relative Location Path: " & Project.DataRelLocn.Path & vbCrLf)
        Message.Add("   Data Location Type: " & Project.DataLocn.Type.ToString & vbCrLf)
        Message.Add("   Data Location Path: " & Project.DataLocn.Path & vbCrLf)

        Message.Add("System ------------------------- " & vbCrLf)
        Message.Add("   System Relative Location Type: " & Project.SystemRelLocn.Type.ToString & vbCrLf)
        Message.Add("   System Relative Location Path: " & Project.SystemRelLocn.Path & vbCrLf)
        Message.Add("   System Location Type: " & Project.SystemLocn.Type.ToString & vbCrLf)
        Message.Add("   System Location Path: " & Project.SystemLocn.Path & vbCrLf)
        Message.Add("======================================================================================" & vbCrLf)

    End Sub

    Private Sub Message_ShowXMessagesChanged(Show As Boolean) Handles Message.ShowXMessagesChanged
        ShowXMessages = Show
    End Sub

    Private Sub Message_ShowSysMessagesChanged(Show As Boolean) Handles Message.ShowSysMessagesChanged
        ShowSysMessages = Show
    End Sub

    Private Sub Project_NewProjectCreated(ProjectPath As String) Handles Project.NewProjectCreated
        SendProjectInfo(ProjectPath) 'Send the path of the new project to the Network application. The new project will be added to the list of projects.
    End Sub

    Private Sub txtLineColor_TextChanged(sender As Object, e As EventArgs) Handles txtLineColor.TextChanged

    End Sub

    Private Sub txtLineColor_Click(sender As Object, e As EventArgs) Handles txtLineColor.Click
        ColorDialog1.Color = txtLineColor.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            txtLineColor.BackColor = ColorDialog1.Color
            'Diagram.LineColor = ColorDialog1.Color
            Bayes.Diagram.LineColor = ColorDialog1.Color
            'pbVenn.BackColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtEventALineColor_TextChanged(sender As Object, e As EventArgs) Handles txtEventALineColor.TextChanged

    End Sub

    Private Sub txtEventALineColor_Click(sender As Object, e As EventArgs) Handles txtEventALineColor.Click
        ColorDialog1.Color = txtEventALineColor.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            txtEventALineColor.BackColor = ColorDialog1.Color
            'Diagram.EventALineColor = ColorDialog1.Color
            'Bayes.EventALineColor = ColorDialog1.Color
            Bayes.EventA.LineColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtEventBLineColor_TextChanged(sender As Object, e As EventArgs) Handles txtEventBLineColor.TextChanged

    End Sub

    Private Sub txtEventBColor_Click(sender As Object, e As EventArgs) Handles txtEventBLineColor.Click
        ColorDialog1.Color = txtEventBLineColor.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            txtEventBLineColor.BackColor = ColorDialog1.Color
            'Diagram.EventBLineColor = ColorDialog1.Color
            Bayes.EventB.LineColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtAllOutcomesFillColor_TextChanged(sender As Object, e As EventArgs) Handles txtFillColor.TextChanged

    End Sub

    Private Sub txtFillColor_Click(sender As Object, e As EventArgs) Handles txtFillColor.Click
        ColorDialog1.Color = txtFillColor.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            txtFillColor.BackColor = ColorDialog1.Color
            'Diagram.FillColor = ColorDialog1.Color
            Bayes.Diagram.FillColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtEventAFillColor_TextChanged(sender As Object, e As EventArgs) Handles txtEventAFillColor.TextChanged

    End Sub

    Private Sub txtEventAFillColor_Click(sender As Object, e As EventArgs) Handles txtEventAFillColor.Click
        ColorDialog1.Color = txtEventAFillColor.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            txtEventAFillColor.BackColor = ColorDialog1.Color
            'Diagram.EventAFillColor = ColorDialog1.Color
            Bayes.EventA.FillColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtEventBFillColor_TextChanged(sender As Object, e As EventArgs) Handles txtEventBFillColor.TextChanged

    End Sub

    Private Sub txtEventBFillColor_Click(sender As Object, e As EventArgs) Handles txtEventBFillColor.Click
        ColorDialog1.Color = txtEventBFillColor.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            txtEventBFillColor.BackColor = ColorDialog1.Color
            'Diagram.EventBFillColor = ColorDialog1.Color
            Bayes.EventB.FillColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtEventAandBFillColor_TextChanged(sender As Object, e As EventArgs) Handles txtEventAandBFillColor.TextChanged

    End Sub

    Private Sub txtEventAandBFillColor_Click(sender As Object, e As EventArgs) Handles txtEventAandBFillColor.Click
        ColorDialog1.Color = txtEventAandBFillColor.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            txtEventAandBFillColor.BackColor = ColorDialog1.Color
            'Diagram.EventAandBFillColor = ColorDialog1.Color
            Bayes.EventAandBFillColor = ColorDialog1.Color
        End If
    End Sub

    Private Sub txtLineThickness_TextChanged(sender As Object, e As EventArgs) Handles txtLineThickness.TextChanged

    End Sub

    Private Sub txtLineThickness_LostFocus(sender As Object, e As EventArgs) Handles txtLineThickness.LostFocus
        'Diagram.LineThickness = txtLineThickness.Text
        Bayes.Diagram.LineThickness = txtLineThickness.Text
    End Sub

    Private Sub txtBoldLineThickness_TextChanged(sender As Object, e As EventArgs) Handles txtBoldLineThickness.TextChanged

    End Sub

    Private Sub txtBoldLineThickness_LostFocus(sender As Object, e As EventArgs) Handles txtBoldLineThickness.LostFocus
        'Diagram.BoldLineThickness = txtBoldLineThickness.Text
        Bayes.Diagram.BoldLineThickness = txtBoldLineThickness.Text
    End Sub

    Private Sub chkBoldLine_CheckedChanged(sender As Object, e As EventArgs) Handles chkBoldLine.CheckedChanged
        'Diagram.BoldLine = chkBoldLine.Checked
        Bayes.Diagram.BoldLine = chkBoldLine.Checked
        DrawDiagram()
    End Sub

    Private Sub txtProbDiagramWidth_TextChanged(sender As Object, e As EventArgs) Handles txtWidth.TextChanged

    End Sub

    Private Sub txtProbDiagramWidth_LostFocus(sender As Object, e As EventArgs) Handles txtWidth.LostFocus
        'Diagram.Width = txtWidth.Text
        Bayes.Diagram.Width = txtWidth.Text
    End Sub

    Private Sub txtProbDiagramHeight_TextChanged(sender As Object, e As EventArgs) Handles txtHeight.TextChanged

    End Sub

    Private Sub txtProbDiagramHeight_LostFocus(sender As Object, e As EventArgs) Handles txtHeight.LostFocus
        'Diagram.Height = txtHeight.Text
        Bayes.Diagram.Height = txtHeight.Text
    End Sub

    Private Sub txtEventALineThickness_TextChanged(sender As Object, e As EventArgs) Handles txtEventALineThickness.TextChanged

    End Sub

    Private Sub txtEventALineThickness_LostFocus(sender As Object, e As EventArgs) Handles txtEventALineThickness.LostFocus
        'Diagram.EventALineThickness = txtEventALineThickness.Text
        Bayes.EventA.LineThickness = txtEventALineThickness.Text
    End Sub

    Private Sub txtEventABoldLineThickness_TextChanged(sender As Object, e As EventArgs) Handles txtEventABoldLineThickness.TextChanged

    End Sub

    Private Sub txtEventABoldLineThickness_LostFocus(sender As Object, e As EventArgs) Handles txtEventABoldLineThickness.LostFocus
        'Diagram.EventABoldLineThickness = txtEventABoldLineThickness.Text
        Bayes.EventA.BoldLineThickness = txtEventABoldLineThickness.Text
    End Sub

    Private Sub chkEventABoldLine_CheckedChanged(sender As Object, e As EventArgs) Handles chkEventABoldLine.CheckedChanged
        'Diagram.EventABoldLine = chkEventABoldLine.Checked
        Bayes.EventA.BoldLine = chkEventABoldLine.Checked
        DrawDiagram()
    End Sub

    Private Sub cmbEventAShape_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEventAShape.SelectedIndexChanged
        'Diagram.EventAShape = cmbEventAShape.SelectedItem.ToString
        Bayes.EventA.Shape = cmbEventAShape.SelectedItem.ToString
    End Sub

    Private Sub txtEventAXMin_TextChanged(sender As Object, e As EventArgs) Handles txtEventAXMin.TextChanged

    End Sub

    Private Sub txtEventAXMin_LostFocus(sender As Object, e As EventArgs) Handles txtEventAXMin.LostFocus
        'Diagram.EventAXMin = txtEventAXMin.Text
        'Bayes.EventA.Unscaled.XMin = txtEventAXMin.Text
        Bayes.EventA.XMin = txtEventAXMin.Text
        If Bayes.Settings.Scaling = "ScaleB" Then Bayes.SetEllipseScaleB()
        DrawDiagram()
    End Sub

    Private Sub txtEventAYMin_TextChanged(sender As Object, e As EventArgs) Handles txtEventAYMin.TextChanged

    End Sub

    Private Sub txtEventAYMin_LostFocus(sender As Object, e As EventArgs) Handles txtEventAYMin.LostFocus
        'Diagram.EventAYMin = txtEventAYMin.Text
        'Bayes.EventA.Unscaled.YMin = txtEventAYMin.Text
        If Bayes.Settings.Scaling = "ScaleAB" Then 'Move the ellipse in the Y direction.
            Dim YShift As Integer = txtEventAYMin.Text - Bayes.EventA.YMin
            Bayes.EventA.YMin += YShift
            Bayes.EventA.YMax += YShift
            Bayes.SetEllipseScaleAB()
        Else
            Bayes.EventA.YMin = txtEventAYMin.Text
            If Bayes.Settings.Scaling = "ScaleB" Then Bayes.SetEllipseScaleB()
        End If
        DrawDiagram()
    End Sub

    Private Sub txtEventAXMax_TextChanged(sender As Object, e As EventArgs) Handles txtEventAXMax.TextChanged

    End Sub

    Private Sub txtEventAXMax_LostFocus(sender As Object, e As EventArgs) Handles txtEventAXMax.LostFocus
        'Diagram.EventAXMax = txtEventAXMax.Text
        'Bayes.EventA.Unscaled.XMax = txtEventAXMax.Text
        If Bayes.Settings.Scaling = "ScaleAB" Then 'Move the ellipse in the X direction.
            Dim XShift As Integer = txtEventAXMax.Text - Bayes.EventA.XMax
            Bayes.EventA.XMax += XShift
            Bayes.EventA.XMin += XShift
            Bayes.SetEllipseScaleAB()
        Else
            Bayes.EventA.XMax = txtEventAXMax.Text
            If Bayes.Settings.Scaling = "ScaleB" Then Bayes.SetEllipseScaleB()
        End If
        DrawDiagram()
    End Sub

    Private Sub txtEventAYMax_TextChanged(sender As Object, e As EventArgs) Handles txtEventAYMax.TextChanged

    End Sub

    Private Sub txtEventAYMax_LostFocus(sender As Object, e As EventArgs) Handles txtEventAYMax.LostFocus
        'Diagram.EventAYMax = txtEventAYMax.Text
        'Bayes.EventA.Unscaled.YMax = txtEventAYMax.Text
        Bayes.EventA.YMax = txtEventAYMax.Text
        If Bayes.Settings.Scaling = "ScaleB" Then Bayes.SetEllipseScaleB()
        DrawDiagram()
    End Sub

    Private Sub txtEventBLineThickness_TextChanged(sender As Object, e As EventArgs) Handles txtEventBLineThickness.TextChanged

    End Sub

    Private Sub txtEventBLineThickness_LostFocus(sender As Object, e As EventArgs) Handles txtEventBLineThickness.LostFocus
        'Diagram.EventBLineThickness = txtEventBLineThickness.Text
        Bayes.EventB.LineThickness = txtEventBLineThickness.Text
    End Sub

    Private Sub txtEventBBoldLineThickness_TextChanged(sender As Object, e As EventArgs) Handles txtEventBBoldLineThickness.TextChanged

    End Sub

    Private Sub txtEventBBoldLineThickness_LostFocus(sender As Object, e As EventArgs) Handles txtEventBBoldLineThickness.LostFocus
        'Diagram.EventBBoldLineThickness = txtEventBBoldLineThickness.Text
        Bayes.EventB.BoldLineThickness = txtEventBBoldLineThickness.Text
    End Sub

    Private Sub chkEventBBoldLine_CheckedChanged(sender As Object, e As EventArgs) Handles chkEventBBoldLine.CheckedChanged
        'Diagram.EventBBoldLine = chkEventBBoldLine.Checked
        Bayes.EventB.BoldLine = chkEventBBoldLine.Checked
        DrawDiagram()
    End Sub

    Private Sub cmbEventBShape_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEventBShape.SelectedIndexChanged
        'Diagram.EventBShape = cmbEventBShape.SelectedItem.ToString
        Bayes.EventB.Shape = cmbEventBShape.SelectedItem.ToString
    End Sub

    Private Sub txtEventBXMin_TextChanged(sender As Object, e As EventArgs) Handles txtEventBXMin.TextChanged

    End Sub

    Private Sub txtEventBXMin_LostFocus(sender As Object, e As EventArgs) Handles txtEventBXMin.LostFocus
        'Diagram.EventBXMin = txtEventBXMin.Text
        'Bayes.EventB.Unscaled.XMin = txtEventBXMin.Text
        Bayes.EventB.XMin = txtEventBXMin.Text
        If Bayes.Settings.Scaling = "ScaleA" Then Bayes.SetEllipseScaleA()
        DrawDiagram()
    End Sub

    Private Sub txtEventBYMin_TextChanged(sender As Object, e As EventArgs) Handles txtEventBYMin.TextChanged

    End Sub

    Private Sub txtEventBYMin_LostFocus(sender As Object, e As EventArgs) Handles txtEventBYMin.LostFocus
        'Diagram.EventBYMin = txtEventBYMin.Text
        'Bayes.EventB.Unscaled.YMin = txtEventBYMin.Text
        Bayes.EventB.YMin = txtEventBYMin.Text
        If Bayes.Settings.Scaling = "ScaleA" Then Bayes.SetEllipseScaleA()
        DrawDiagram()
    End Sub

    Private Sub txtEventBXMax_TextChanged(sender As Object, e As EventArgs) Handles txtEventBXMax.TextChanged

    End Sub

    Private Sub txtEventBXMax_LostFocus(sender As Object, e As EventArgs) Handles txtEventBXMax.LostFocus
        'Diagram.EventBXMax = txtEventBXMax.Text
        'Bayes.EventB.Unscaled.XMax = txtEventBXMax.Text
        Bayes.EventB.XMax = txtEventBXMax.Text
        If Bayes.Settings.Scaling = "ScaleA" Then Bayes.SetEllipseScaleA()
        DrawDiagram()
    End Sub

    Private Sub txtEventBYMax_TextChanged(sender As Object, e As EventArgs) Handles txtEventBYMax.TextChanged

    End Sub

    Private Sub txtEventBYMax_LostFocus(sender As Object, e As EventArgs) Handles txtEventBYMax.LostFocus
        'Diagram.EventBYMax = txtEventBYMax.Text
        'Bayes.EventB.Unscaled.YMax = txtEventBYMax.Text
        Bayes.EventB.YMax = txtEventBYMax.Text
        If Bayes.Settings.Scaling = "ScaleA" Then Bayes.SetEllipseScaleA()
        DrawDiagram()
    End Sub


    Private Sub btnDrawDiagram_Click(sender As Object, e As EventArgs) Handles btnDrawDiagram.Click
        DrawDiagram()
    End Sub

    Private Sub DrawSimDiagram()
        'Draws the Bayes Simulation diagram.
        Dim Diag As New Bitmap(BayesSim.Diagram.Width, BayesSim.Diagram.Height)
        Using g = Graphics.FromImage(Diag)
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

            Dim Background As New Drawing2D.GraphicsPath
            Background.AddRectangle(New Rectangle(0, 0, BayesSim.Diagram.Width - 1, BayesSim.Diagram.Height - 1))
            Dim BackgroundRegion As New Region(Background)

            Dim EventAandNotBShape As New Drawing2D.GraphicsPath
            EventAandNotBShape.AddRectangle(New Rectangle(BayesSim.AreaAandNotB.XMin, BayesSim.AreaAandNotB.YMin, BayesSim.AreaAandNotB.XMax - BayesSim.AreaAandNotB.XMin, BayesSim.AreaAandNotB.YMax - BayesSim.AreaAandNotB.YMin)) 'x, y, width, height
            Dim EventAandNotBRegion As New Region(EventAandNotBShape)

            Dim EventAandBShape As New Drawing2D.GraphicsPath
            EventAandBShape.AddRectangle(New Rectangle(BayesSim.AreaAandB.XMin, BayesSim.AreaAandB.YMin, BayesSim.AreaAandB.XMax - BayesSim.AreaAandB.XMin, BayesSim.AreaAandB.YMax - BayesSim.AreaAandB.YMin)) 'x, y, width, height
            Dim EventAandBRegion As New Region(EventAandBShape)

            Dim EventNotAandBShape As New Drawing2D.GraphicsPath
            EventNotAandBShape.AddRectangle(New Rectangle(BayesSim.AreaNotAandB.XMin, BayesSim.AreaNotAandB.YMin, BayesSim.AreaNotAandB.XMax - BayesSim.AreaNotAandB.XMin, BayesSim.AreaNotAandB.YMax - BayesSim.AreaNotAandB.YMin)) 'x, y, width, height
            Dim EventNotAandBRegion As New Region(EventNotAandBShape)

            Dim EventNotAandNotBShape As New Drawing2D.GraphicsPath
            EventNotAandNotBShape.AddRectangle(New Rectangle(BayesSim.AreaNotAandNotB.XMin, BayesSim.AreaNotAandNotB.YMin, BayesSim.AreaNotAandNotB.XMax - BayesSim.AreaNotAandNotB.XMin, BayesSim.AreaNotAandNotB.YMax - BayesSim.AreaNotAandNotB.YMin)) 'x, y, width, height
            Dim EventNotAandNotBRegion As New Region(EventNotAandNotBShape)

            'Add the Background color:
            g.FillRegion(New SolidBrush(BayesSim.Diagram.FillColor), BackgroundRegion)


            'Add the Event A and Not B background:
            g.FillRegion(New SolidBrush(BayesSim.AreaAandNotB.FillColor), EventAandNotBRegion)

            'Add the Event A and B background:
            g.FillRegion(New SolidBrush(BayesSim.AreaAandB.FillColor), EventAandBRegion)

            'Add the Event Not A and B background:
            g.FillRegion(New SolidBrush(BayesSim.AreaNotAandB.FillColor), EventNotAandBRegion)

            'Add the Event Not A and Not B background:
            g.FillRegion(New SolidBrush(BayesSim.AreaNotAandNotB.FillColor), EventNotAandNotBRegion)

            'Add the Background outline:
            If BayesSim.Diagram.BoldLine Then
                g.DrawPath(New Pen(BayesSim.Diagram.LineColor, BayesSim.Diagram.BoldLineThickness * 2), Background) 'The line thickness is doubled because the outer hald of the line is outside the graphics display area.
            Else
                g.DrawPath(New Pen(BayesSim.Diagram.LineColor, BayesSim.Diagram.LineThickness * 2), Background) 'The line thickness is doubled because the outer hald of the line is outside the graphics display area.
            End If

            'Add the Area A outline:
            If BayesSim.AreaAandNotB.BoldLine Then
                g.DrawPath(New Pen(BayesSim.AreaAandNotB.LineColor, BayesSim.AreaAandNotB.BoldLineThickness), EventAandNotBShape)
            Else
                g.DrawPath(New Pen(BayesSim.AreaAandNotB.LineColor, BayesSim.AreaAandNotB.LineThickness), EventAandNotBShape)
            End If

            ''Add the Area A and B outline:
            'If BayesSim.AreaAandB.BoldLine Then
            '    g.DrawPath(New Pen(BayesSim.AreaAandB.LineColor, BayesSim.AreaAandB.BoldLineThickness), EventAandBShape)
            'Else
            '    g.DrawPath(New Pen(BayesSim.AreaAandB.LineColor, BayesSim.AreaAandB.LineThickness), EventAandBShape)
            'End If

            'Add the Area B outline:
            If BayesSim.AreaNotAandB.BoldLine Then
                g.DrawPath(New Pen(BayesSim.AreaNotAandB.LineColor, BayesSim.AreaNotAandB.BoldLineThickness), EventNotAandBShape)
            Else
                g.DrawPath(New Pen(BayesSim.AreaNotAandB.LineColor, BayesSim.AreaNotAandB.LineThickness), EventNotAandBShape)
            End If

            'Add the Area Not A and Not B outline:
            If BayesSim.AreaNotAandNotB.BoldLine Then
                g.DrawPath(New Pen(BayesSim.AreaNotAandNotB.LineColor, BayesSim.AreaNotAandNotB.BoldLineThickness), EventNotAandNotBShape)
            Else
                g.DrawPath(New Pen(BayesSim.AreaNotAandNotB.LineColor, BayesSim.AreaNotAandNotB.LineThickness), EventNotAandNotBShape)
            End If

            'Add the Area A and B outline: (Draw this last to overwrite the adjacent outlines: to help show that Area A extends into A and B: and area B extends into A and B.)
            If BayesSim.AreaAandB.BoldLine Then
                g.DrawPath(New Pen(BayesSim.AreaAandB.LineColor, BayesSim.AreaAandB.BoldLineThickness), EventAandBShape)
            Else
                g.DrawPath(New Pen(BayesSim.AreaAandB.LineColor, BayesSim.AreaAandB.LineThickness), EventAandBShape)
            End If

            'Add the text annotations:
            Dim myBrush As New SolidBrush(BayesSim.AnnotTitle.Color)
            g.DrawString(BayesSim.AnnotTitle.Text, BayesSim.AnnotTitle.Font, myBrush, New PointF(BayesSim.AnnotTitle.X, BayesSim.AnnotTitle.Y)) 'Chart title
            myBrush = New SolidBrush(BayesSim.ProbAandNotBLabel.Color)
            g.DrawString(BayesSim.ProbAandNotBLabel.Text, BayesSim.ProbAandNotBLabel.Font, myBrush, New PointF(BayesSim.ProbAandNotBLabel.X, BayesSim.ProbAandNotBLabel.Y)) 'AreaAandNotB label
            myBrush = New SolidBrush(BayesSim.ProbALabel.Color)
            g.DrawString(BayesSim.ProbALabel.Text, BayesSim.ProbALabel.Font, myBrush, New PointF(BayesSim.ProbALabel.X, BayesSim.ProbALabel.Y)) 'AreaA label
            myBrush = New SolidBrush(BayesSim.ProbAandBLabel.Color)
            g.DrawString(BayesSim.ProbAandBLabel.Text, BayesSim.ProbAandBLabel.Font, myBrush, New PointF(BayesSim.ProbAandBLabel.X, BayesSim.ProbAandBLabel.Y)) 'AreaAandB label
            myBrush = New SolidBrush(BayesSim.ProbBLabel.Color)
            g.DrawString(BayesSim.ProbBLabel.Text, BayesSim.ProbBLabel.Font, myBrush, New PointF(BayesSim.ProbBLabel.X, BayesSim.ProbBLabel.Y)) 'AreaB label
            myBrush = New SolidBrush(BayesSim.ProbNotAandBLabel.Color)
            g.DrawString(BayesSim.ProbNotAandBLabel.Text, BayesSim.ProbNotAandBLabel.Font, myBrush, New PointF(BayesSim.ProbNotAandBLabel.X, BayesSim.ProbNotAandBLabel.Y)) 'AreaNotAandB label
            myBrush = New SolidBrush(BayesSim.ProbNotAandNotBLabel.Color)
            g.DrawString(BayesSim.ProbNotAandNotBLabel.Text, BayesSim.ProbNotAandNotBLabel.Font, myBrush, New PointF(BayesSim.ProbNotAandNotBLabel.X, BayesSim.ProbNotAandNotBLabel.Y)) 'AreaNotAandNotB label

        End Using

        pbSim.Image?.Dispose()
        pbSim.Image = Diag

    End Sub

    Private Sub DrawDiagram()
        'Draw the Bayes Probability diagram.

        'Dim Diag As New Bitmap(Diagram.Width, Diagram.Height)
        Dim Diag As New Bitmap(Bayes.Diagram.Width, Bayes.Diagram.Height)
        Using g = Graphics.FromImage(Diag)
            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

            Dim Background As New Drawing2D.GraphicsPath
            'Background.AddRectangle(New Rectangle(0, 0, Diagram.Width, Diagram.Height))
            'Background.AddRectangle(New Rectangle(1, 1, Diagram.Width, Diagram.Height))
            'Background.AddRectangle(New Rectangle(0, 0, Diagram.Width - 1, Diagram.Height - 1))
            Background.AddRectangle(New Rectangle(0, 0, Bayes.Diagram.Width - 1, Bayes.Diagram.Height - 1))

            Dim BackgroundRegion As New Region(Background)

            Dim EventAShape As New Drawing2D.GraphicsPath
            'Select Case Diagram.EventAShape
            Select Case Bayes.EventA.Shape
                Case "Ellipse"
                    'EventAShape.AddEllipse(New Rectangle(Bayes.EventA.Unscaled.XMin, Bayes.EventA.Unscaled.YMin, Bayes.EventA.Unscaled.XMax - Bayes.EventA.Unscaled.XMin, Bayes.EventA.Unscaled.YMax - Bayes.EventA.Unscaled.YMin)) 'x, y, width, height
                    EventAShape.AddEllipse(New Rectangle(Bayes.EventA.XMin, Bayes.EventA.YMin, Bayes.EventA.XMax - Bayes.EventA.XMin, Bayes.EventA.YMax - Bayes.EventA.YMin)) 'x, y, width, height
                Case "Rectangle"
                    'EventAShape.AddRectangle(New Rectangle(Bayes.EventA.Unscaled.XMin, Bayes.EventA.Unscaled.YMin, Bayes.EventA.Unscaled.XMax - Bayes.EventA.Unscaled.XMin, Bayes.EventA.Unscaled.YMax - Bayes.EventA.Unscaled.YMin)) 'x, y, width, height
                    EventAShape.AddRectangle(New Rectangle(Bayes.EventA.XMin, Bayes.EventA.YMin, Bayes.EventA.XMax - Bayes.EventA.XMin, Bayes.EventA.YMax - Bayes.EventA.YMin)) 'x, y, width, height
                Case Else
                    Message.AddWarning("Unknown Event A shape: " & Bayes.EventA.Shape & vbCrLf)
                    Message.AddWarning("An ellipse will be used." & vbCrLf)
                    'EventAShape.AddEllipse(New Rectangle(Bayes.EventA.Unscaled.XMin, Bayes.EventA.Unscaled.YMin, Bayes.EventA.Unscaled.XMax - Bayes.EventA.Unscaled.XMin, Bayes.EventA.Unscaled.YMax - Bayes.EventA.Unscaled.YMin)) 'x, y, width, height
                    EventAShape.AddEllipse(New Rectangle(Bayes.EventA.XMin, Bayes.EventA.YMin, Bayes.EventA.XMax - Bayes.EventA.XMin, Bayes.EventA.YMax - Bayes.EventA.YMin)) 'x, y, width, height
            End Select

            Dim EventARegion As New Region(EventAShape)

            Dim EventBShape As New Drawing2D.GraphicsPath
            Select Case Bayes.EventB.Shape
                Case "Ellipse"
                    'EventBShape.AddEllipse(New Rectangle(Bayes.EventB.Unscaled.XMin, Bayes.EventB.Unscaled.YMin, Bayes.EventB.Unscaled.XMax - Bayes.EventB.Unscaled.XMin, Bayes.EventB.Unscaled.YMax - Bayes.EventB.Unscaled.YMin)) 'x, y, width, height
                    EventBShape.AddEllipse(New Rectangle(Bayes.EventB.XMin, Bayes.EventB.YMin, Bayes.EventB.XMax - Bayes.EventB.XMin, Bayes.EventB.YMax - Bayes.EventB.YMin)) 'x, y, width, height
                Case "Rectangle"
                    'EventBShape.AddRectangle(New Rectangle(Bayes.EventB.Unscaled.XMin, Bayes.EventB.Unscaled.YMin, Bayes.EventB.Unscaled.XMax - Bayes.EventB.Unscaled.XMin, Bayes.EventB.Unscaled.YMax - Bayes.EventB.Unscaled.YMin)) 'x, y, width, height
                    EventBShape.AddRectangle(New Rectangle(Bayes.EventB.XMin, Bayes.EventB.YMin, Bayes.EventB.XMax - Bayes.EventB.XMin, Bayes.EventB.YMax - Bayes.EventB.YMin)) 'x, y, width, height
                Case Else
                    Message.AddWarning("Unknown Event B shape: " & Bayes.EventA.Shape & vbCrLf)
                    Message.AddWarning("An ellipse will be used." & vbCrLf)
                    'EventBShape.AddEllipse(New Rectangle(Bayes.EventB.Unscaled.XMin, Bayes.EventB.Unscaled.YMin, Bayes.EventB.Unscaled.XMax - Bayes.EventB.Unscaled.XMin, Bayes.EventB.Unscaled.YMax - Bayes.EventB.Unscaled.YMin)) 'x, y, width, height
                    EventBShape.AddEllipse(New Rectangle(Bayes.EventB.XMin, Bayes.EventB.YMin, Bayes.EventB.XMax - Bayes.EventB.XMin, Bayes.EventB.YMax - Bayes.EventB.YMin)) 'x, y, width, height
            End Select

            Dim EventBRegion As New Region(EventBShape)

            'EventBRegion.GetRegionData.Data.

            'Add the Background color:
            g.FillRegion(New SolidBrush(Bayes.Diagram.FillColor), BackgroundRegion)
            ''Add the Background outline:
            'g.DrawPath(New Pen(Diagram.LineColor, Diagram.LineThickness), Background)

            'Add the Event A background:
            g.FillRegion(New SolidBrush(Bayes.EventA.FillColor), EventARegion)

            'Add the Event B background:
            g.FillRegion(New SolidBrush(Bayes.EventB.FillColor), EventBRegion)

            'Add the A and B background:
            Dim EventAandBRegion As New Region(EventAShape)
            EventAandBRegion.Intersect(EventBShape)
            g.FillRegion(New SolidBrush(Bayes.EventAandBFillColor), EventAandBRegion)


            'Add any selected zero probability areas:
            If Bayes.ZeroProbRegion.A Then
                'g.FillRegion(New SolidBrush(Bayes.ZeroProbabilityColor), EventARegion)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), EventARegion)
            End If

            If Bayes.ZeroProbRegion.NotA Then
                Dim NotARegion As New Region(Background)
                NotARegion.Exclude(EventAShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), NotARegion)
            End If

            If Bayes.ZeroProbRegion.B Then
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), EventBRegion)
            End If

            If Bayes.ZeroProbRegion.NotB Then
                Dim NotBRegion As New Region(Background)
                NotBRegion.Exclude(EventBShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), NotBRegion)
            End If

            If Bayes.ZeroProbRegion.AandB Then
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), EventAandBRegion)
            End If

            If Bayes.ZeroProbRegion.AandNotB Then
                Dim AandNotBRegion As New Region(EventAShape)
                AandNotBRegion.Exclude(EventBShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), AandNotBRegion)
            End If

            If Bayes.ZeroProbRegion.NotAandB Then
                Dim NotAandBRegion As New Region(EventBShape)
                NotAandBRegion.Exclude(EventAShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), NotAandBRegion)
            End If

            If Bayes.ZeroProbRegion.NotAandNotB Then
                Dim NotAandNotBRegion As New Region(Background)
                NotAandNotBRegion.Exclude(EventAShape)
                NotAandNotBRegion.Exclude(EventBShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), NotAandNotBRegion)
            End If


            'Add any selected highlight areas:
            If Bayes.HighlightRegion.A Then
                g.FillRegion(New SolidBrush(Bayes.HighlightRegion.Color), EventARegion)
                'txtArea.Text = EventARegion.GetRegionData.Data.Length
            End If

            If Bayes.HighlightRegion.NotA Then
                Dim NotARegion As New Region(Background)
                NotARegion.Exclude(EventAShape)
                g.FillRegion(New SolidBrush(Bayes.HighlightRegion.Color), NotARegion)
                'txtArea.Text = NotARegion.GetRegionData.Data.Length
            End If

            If Bayes.HighlightRegion.B Then
                g.FillRegion(New SolidBrush(Bayes.HighlightRegion.Color), EventBRegion)
                'txtArea.Text = EventBRegion.GetRegionData.Data.Length
            End If

            If Bayes.HighlightRegion.NotB Then
                Dim NotBRegion As New Region(Background)
                NotBRegion.Exclude(EventBShape)
                g.FillRegion(New SolidBrush(Bayes.HighlightRegion.Color), NotBRegion)
                'txtArea.Text = NotBRegion.GetRegionData.Data.Length
            End If

            If Bayes.HighlightRegion.AandB Then
                g.FillRegion(New SolidBrush(Bayes.HighlightRegion.Color), EventAandBRegion)
                'txtArea.Text = EventAandBRegion.GetRegionData.Data.Length
            End If

            If Bayes.HighlightRegion.AandNotB Then
                Dim AandNotBRegion As New Region(EventAShape)
                AandNotBRegion.Exclude(EventBShape)
                g.FillRegion(New SolidBrush(Bayes.HighlightRegion.Color), AandNotBRegion)
                'txtArea.Text = AandNotBRegion.GetRegionData.Data.Length
            End If

            If Bayes.HighlightRegion.NotAandB Then
                Dim NotAandBRegion As New Region(EventBShape)
                NotAandBRegion.Exclude(EventAShape)
                g.FillRegion(New SolidBrush(Bayes.HighlightRegion.Color), NotAandBRegion)
                'txtArea.Text = NotAandBRegion.GetRegionData.Data.Length
            End If

            If Bayes.HighlightRegion.NotAandNotB Then
                Dim NotAandNotBRegion As New Region(Background)
                NotAandNotBRegion.Exclude(EventAShape)
                NotAandNotBRegion.Exclude(EventBShape)
                g.FillRegion(New SolidBrush(Bayes.HighlightRegion.Color), NotAandNotBRegion)
                'txtArea.Text = NotAandNotBRegion.GetRegionData.Data.Length
            End If

            'Add any Zero Probability areas:
            If Bayes.ZeroProbRegion.A Then
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), EventARegion)
            End If

            If Bayes.ZeroProbRegion.NotA Then
                Dim NotARegion As New Region(Background)
                NotARegion.Exclude(EventAShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), NotARegion)
            End If

            If Bayes.ZeroProbRegion.B Then
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), EventBRegion)
            End If

            If Bayes.ZeroProbRegion.NotB Then
                Dim NotBRegion As New Region(Background)
                NotBRegion.Exclude(EventBShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), NotBRegion)
            End If

            If Bayes.ZeroProbRegion.AandB Then
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), EventAandBRegion)
            End If

            If Bayes.ZeroProbRegion.AandNotB Then
                Dim AandNotBRegion As New Region(EventAShape)
                AandNotBRegion.Exclude(EventBShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), AandNotBRegion)
            End If

            If Bayes.ZeroProbRegion.NotAandB Then
                Dim NotAandBRegion As New Region(EventBShape)
                NotAandBRegion.Exclude(EventAShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), NotAandBRegion)
            End If

            If Bayes.ZeroProbRegion.NotAandNotB Then
                Dim NotAandNotBRegion As New Region(Background)
                NotAandNotBRegion.Exclude(EventAShape)
                NotAandNotBRegion.Exclude(EventBShape)
                g.FillRegion(New SolidBrush(Bayes.ZeroProbRegion.Color), NotAandNotBRegion)
            End If

            'Add the Background outline:
            If Bayes.Diagram.BoldLine Then
                'g.DrawPath(New Pen(Bayes.LineColor, Bayes.BoldLineThickness), Background)
                g.DrawPath(New Pen(Bayes.Diagram.LineColor, Bayes.Diagram.BoldLineThickness * 2), Background) 'The line thickness is doubled because the outer hald of the line is outside the graphics display area.
            Else
                'g.DrawPath(New Pen(Bayes.LineColor, Bayes.LineThickness), Background)
                g.DrawPath(New Pen(Bayes.Diagram.LineColor, Bayes.Diagram.LineThickness * 2), Background) 'The line thickness is doubled because the outer hald of the line is outside the graphics display area.
            End If

            'Add the Event A outline:
            If Bayes.EventA.BoldLine Then
                g.DrawPath(New Pen(Bayes.EventA.LineColor, Bayes.EventA.BoldLineThickness), EventAShape)
            Else
                g.DrawPath(New Pen(Bayes.EventA.LineColor, Bayes.EventA.LineThickness), EventAShape)
            End If

            'Add the Event B outline:
            If Bayes.EventB.BoldLine Then
                g.DrawPath(New Pen(Bayes.EventB.LineColor, Bayes.EventB.BoldLineThickness), EventBShape)
            Else
                g.DrawPath(New Pen(Bayes.EventB.LineColor, Bayes.EventB.LineThickness), EventBShape)
            End If

            'Add the text annotations:
            'Diagram Title:
            If Bayes.AnnotTitle.X = -1 Then Bayes.DefaultTitlePosition()
            If Bayes.AnnotTitle.Y = -1 Then Bayes.DefaultTitlePosition()
            Dim myFontHeight As Integer '= g.MeasureString(Bayes.AnnotTitleText, Bayes.AnnotTitleFont).Height
            Dim myFontWidth As Integer '= g.MeasureString(Bayes.AnnotTitle.Text, Bayes.AnnotTitle.Font).Width
            'Dim myBrush As New SolidBrush(Color.Black)
            Dim myBrush As New SolidBrush(Bayes.AnnotTitle.Color)
            'g.DrawString(Bayes.AnnotTitle.Text, Bayes.AnnotTitle.Font, myBrush, New PointF(Bayes.AnnotTitle.X - myFontWidth / 2, Bayes.AnnotTitle.Y))
            g.DrawString(Bayes.AnnotTitle.Text, Bayes.AnnotTitle.Font, myBrush, New PointF(Bayes.AnnotTitle.X, Bayes.AnnotTitle.Y))

            'Diagram Description:
            If Bayes.AnnotDescr.X = -1 Then Bayes.DefaultDescrPosition()
            If Bayes.AnnotDescr.Y = -1 Then Bayes.DefaultDescrPosition()
            'myFontHeight = g.MeasureString(Bayes.AnnotTitleText, Bayes.AnnotTitleFont).Height
            'myFontWidth = g.MeasureString(Bayes.AnnotDescr.Text, Bayes.AnnotDescr.Font).Width
            myBrush = New SolidBrush(Bayes.AnnotDescr.Color)
            'If Bayes.AnnotDescr.Y = -1 Then
            '    myFontHeight = g.MeasureString(Bayes.AnnotTitle.Text, Bayes.AnnotTitle.Font).Height 'The font height of the Title
            '    'g.DrawString(Bayes.AnnotDescr.Text, Bayes.AnnotDescr.Font, myBrush, New PointF(Bayes.AnnotDescr.X - myFontWidth / 2, Bayes.AnnotTitle.Y + myFontHeight))
            '    'g.DrawString(Bayes.AnnotDescr.Text, Bayes.AnnotDescr.Font, myBrush, New PointF(Bayes.AnnotDescr.X, Bayes.AnnotTitle.Y + myFontHeight))
            '    g.DrawString(Bayes.AnnotDescr.Text, Bayes.AnnotDescr.Font, myBrush, New PointF(Bayes.AnnotDescr.X, Bayes.AnnotTitle.Y))
            'Else
            '    'g.DrawString(Bayes.AnnotDescr.Text, Bayes.AnnotDescr.Font, myBrush, New PointF(Bayes.AnnotDescr.X - myFontWidth / 2, Bayes.AnnotDescr.Y))
            '    g.DrawString(Bayes.AnnotDescr.Text, Bayes.AnnotDescr.Font, myBrush, New PointF(Bayes.AnnotDescr.X, Bayes.AnnotTitle.Y))
            'End If
            g.DrawString(Bayes.AnnotDescr.Text, Bayes.AnnotDescr.Font, myBrush, New PointF(Bayes.AnnotDescr.X, Bayes.AnnotDescr.Y))

            'If Bayes.AnnotEventA.Unscaled.Y = Bayes.AnnotEventB.Unscaled.Y Then 'Check if the text overlaps.
            If Bayes.AnnotEventA.Y = Bayes.AnnotEventB.Y Then 'Check if the text overlaps.
                'Dim EventAandBGap As Integer = Bayes.AnnotEventB.Unscaled.X - Bayes.AnnotEventA.Unscaled.X - g.MeasureString(Bayes.AnnotEventA.Unscaled.Text, Bayes.AnnotEventA.Unscaled.Font).Width / 2 - g.MeasureString(Bayes.AnnotEventB.Unscaled.Text, Bayes.AnnotEventB.Unscaled.Font).Width / 2
                'Dim EventAandBGap As Integer = Bayes.AnnotEventB.Unscaled.X - Bayes.AnnotEventA.X - g.MeasureString(Bayes.AnnotEventA.Text, Bayes.AnnotEventA.Font).Width / 2 - g.MeasureString(Bayes.AnnotEventB.Text, Bayes.AnnotEventB.Font).Width / 2
                Dim EventAandBGap As Integer = Bayes.AnnotEventB.X - Bayes.AnnotEventA.X - g.MeasureString(Bayes.AnnotEventA.Text, Bayes.AnnotEventA.Font).Width / 2 - g.MeasureString(Bayes.AnnotEventB.Text, Bayes.AnnotEventB.Font).Width / 2
                'If IsNothing(Message) Then Else Message.Add("EventAandBGap = " & EventAandBGap & vbCrLf)
                If EventAandBGap < 10 Then 'Ensure there is a 10 pixel gap between the labels
                    'If IsNothing(Message) Then Else Message.Add("Bayes.AnnotEventAX = " & Bayes.AnnotEventAX & vbCrLf)
                    'If IsNothing(Message) Then Else Message.Add("Bayes.AnnotEventBX = " & Bayes.AnnotEventBX & vbCrLf)
                    'Bayes.AnnotEventA.Unscaled.X = Bayes.AnnotEventA.Unscaled.X + EventAandBGap / 2 - 5 'Note: EventAandBGap is negative or < 5
                    Bayes.AnnotEventA.X = Bayes.AnnotEventA.X + EventAandBGap / 2 - 5 'Note: EventAandBGap is negative or < 5
                    'Bayes.AnnotEventB.Unscaled.X = Bayes.AnnotEventB.Unscaled.X - EventAandBGap / 2 + 5
                    Bayes.AnnotEventB.X = Bayes.AnnotEventB.X - EventAandBGap / 2 + 5
                    'If IsNothing(Message) Then Else Message.Add("Adjusted Bayes.AnnotEventAX = " & Bayes.AnnotEventAX & vbCrLf)
                    'If IsNothing(Message) Then Else Message.Add("Adjusted Bayes.AnnotEventBX = " & Bayes.AnnotEventBX & vbCrLf)
                End If
            End If

            'Event A Label:
            If Bayes.AnnotEventA.Unscaled.X = -1 Then Bayes.DefaultEventAandBPositions()
            If Bayes.AnnotEventA.Unscaled.Y = -1 Then Bayes.DefaultEventAandBPositions()
            'myFontWidth = g.MeasureString(Bayes.AnnotEventA.Unscaled.Text, Bayes.AnnotEventA.Unscaled.Font).Width
            'myBrush = New SolidBrush(Bayes.AnnotEventA.Unscaled.Color)
            myBrush = New SolidBrush(Bayes.AnnotEventA.Color)
            'g.DrawString(Bayes.AnnotEventA.Unscaled.Text, Bayes.AnnotEventA.Unscaled.Font, myBrush, New PointF(Bayes.AnnotEventA.Unscaled.X - myFontWidth / 2, Bayes.AnnotEventA.Unscaled.Y))
            'g.DrawString(Bayes.AnnotEventA.Unscaled.Text, Bayes.AnnotEventA.Unscaled.Font, myBrush, New PointF(Bayes.AnnotEventA.Unscaled.X, Bayes.AnnotEventA.Unscaled.Y))
            'g.DrawString(Bayes.AnnotEventA.Text, Bayes.AnnotEventA.Font, myBrush, New PointF(Bayes.AnnotEventA.Unscaled.X, Bayes.AnnotEventA.Unscaled.Y))
            g.DrawString(Bayes.AnnotEventA.Text, Bayes.AnnotEventA.Font, myBrush, New PointF(Bayes.AnnotEventA.X, Bayes.AnnotEventA.Y))

            'Event B Label:
            If Bayes.AnnotEventB.Unscaled.X = -1 Then Bayes.DefaultEventAandBPositions()
            If Bayes.AnnotEventB.Unscaled.Y = -1 Then Bayes.DefaultEventAandBPositions()
            'myFontWidth = g.MeasureString(Bayes.AnnotEventB.Unscaled.Text, Bayes.AnnotEventB.Unscaled.Font).Width
            'myBrush = New SolidBrush(Bayes.AnnotEventB.Unscaled.Color)
            myBrush = New SolidBrush(Bayes.AnnotEventB.Color)
            'g.DrawString(Bayes.AnnotEventB.Unscaled.Text, Bayes.AnnotEventB.Unscaled.Font, myBrush, New PointF(Bayes.AnnotEventB.Unscaled.X - myFontWidth / 2, Bayes.AnnotEventB.Unscaled.Y))
            'g.DrawString(Bayes.AnnotEventB.Unscaled.Text, Bayes.AnnotEventB.Unscaled.Font, myBrush, New PointF(Bayes.AnnotEventB.Unscaled.X, Bayes.AnnotEventB.Unscaled.Y))
            g.DrawString(Bayes.AnnotEventB.Text, Bayes.AnnotEventB.Font, myBrush, New PointF(Bayes.AnnotEventB.X, Bayes.AnnotEventB.Y))

            'Dim myBrush As New SolidBrush(txtDisplayText.ForeColor)
            'g.DrawString(txtDisplayText.Text, txtDisplayText.Font, myBrush, New PointF(txtTextX.Text - myFontWidth / 2, txtTextY.Text))
            'g.DrawString(txtDisplayText.Text, txtDisplayText.Font, myBrush, New PointF(txtTextX.Text - myFontWidth / 2, txtTextY.Text + myFontHeight))



            'Annotate probability and sample count values

            'Event A probability:
            If Bayes.ProbA.Label.Show Then
                myBrush = New SolidBrush(Bayes.ProbA.Label.Color)
                If Bayes.ProbA.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotProbAPosn()
                If Bayes.ProbA.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotProbAPosn()
                'g.DrawString(Bayes.ProbA.Label.Text, Bayes.ProbA.Label.Font, myBrush, New PointF(Bayes.ProbA.Label.X, Bayes.ProbA.Label.Y))
                g.DrawString(Bayes.ProbA.ConditionalText, Bayes.ProbA.Label.Font, myBrush, New PointF(Bayes.ProbA.ConditionalX, Bayes.ProbA.ConditionalY))
                'Debug.Print("Bayes.ProbA.ConditionalX = " & Bayes.ProbA.ConditionalX & "Bayes.ProbA.ConditionalY = " & Bayes.ProbA.ConditionalY & vbCrLf)
            End If

            'Event A Sample Count:
            If Bayes.SampsA.Label.Show Then
                myBrush = New SolidBrush(Bayes.SampsA.Label.Color)
                If Bayes.SampsA.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotSampsAPosn()
                If Bayes.SampsA.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotSampsAPosn()
                'g.DrawString(Bayes.SampsA.Label.Text, Bayes.SampsA.Label.Font, myBrush, New PointF(Bayes.SampsA.Label.X, Bayes.SampsA.Label.Y))
                g.DrawString(Bayes.SampsA.ConditionalText, Bayes.SampsA.Label.Font, myBrush, New PointF(Bayes.SampsA.ConditionalX, Bayes.SampsA.ConditionalY))
                'Debug.Print("Bayes.SampsA.ConditionalX = " & Bayes.SampsA.ConditionalX & "Bayes.SampsA.ConditionalY = " & Bayes.SampsA.ConditionalY & vbCrLf)
            End If

            'Event Not A probability:
            If Bayes.ProbNotA.Label.Show Then
                myBrush = New SolidBrush(Bayes.ProbNotA.Label.Color)
                If Bayes.ProbNotA.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotProbNotAPosn()
                If Bayes.ProbNotA.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotProbNotAPosn()
                'g.DrawString(Bayes.ProbNotA.Label.Text, Bayes.ProbNotA.Label.Font, myBrush, New PointF(Bayes.ProbNotA.Label.X, Bayes.ProbNotA.Label.Y))
                g.DrawString(Bayes.ProbNotA.ConditionalText, Bayes.ProbNotA.Label.Font, myBrush, New PointF(Bayes.ProbNotA.ConditionalX, Bayes.ProbNotA.ConditionalY))
                'Debug.Print("Bayes.ProbNotA.ConditionalX = " & Bayes.ProbNotA.ConditionalX & "Bayes.ProbNotA.ConditionalY = " & Bayes.ProbNotA.ConditionalY & vbCrLf)
            End If

            'Event Not A Sample Count:
            If Bayes.SampsNotA.Label.Show Then
                myBrush = New SolidBrush(Bayes.SampsNotA.Label.Color)
                If Bayes.SampsNotA.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotSampsNotAPosn()
                If Bayes.SampsNotA.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotSampsNotAPosn()
                'g.DrawString(Bayes.SampsNotA.Label.Text, Bayes.SampsNotA.Label.Font, myBrush, New PointF(Bayes.SampsNotA.Label.X, Bayes.SampsNotA.Label.Y))
                g.DrawString(Bayes.SampsNotA.ConditionalText, Bayes.SampsNotA.Label.Font, myBrush, New PointF(Bayes.SampsNotA.ConditionalX, Bayes.SampsNotA.ConditionalY))
                'Debug.Print("Bayes.SampsNotA.ConditionalX = " & Bayes.SampsNotA.ConditionalX & "Bayes.SampsNotA.ConditionalY = " & Bayes.SampsNotA.ConditionalY & vbCrLf)
            End If

            'Event B probability:
            If Bayes.ProbB.Label.Show Then
                myBrush = New SolidBrush(Bayes.ProbB.Label.Color)
                If Bayes.ProbB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotProbBPosn()
                If Bayes.ProbB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotProbBPosn()
                'g.DrawString(Bayes.ProbB.Label.Text, Bayes.ProbB.Label.Font, myBrush, New PointF(Bayes.ProbB.Label.X, Bayes.ProbB.Label.Y))
                g.DrawString(Bayes.ProbB.ConditionalText, Bayes.ProbB.Label.Font, myBrush, New PointF(Bayes.ProbB.ConditionalX, Bayes.ProbB.ConditionalY))
            End If

            'Event B Sample Count:
            If Bayes.SampsB.Label.Show Then
                myBrush = New SolidBrush(Bayes.SampsB.Label.Color)
                If Bayes.SampsB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotSampsBPosn()
                If Bayes.SampsB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotSampsBPosn()
                'g.DrawString(Bayes.SampsB.Label.Text, Bayes.SampsB.Label.Font, myBrush, New PointF(Bayes.SampsB.Label.X, Bayes.SampsB.Label.Y))
                g.DrawString(Bayes.SampsB.ConditionalText, Bayes.SampsB.Label.Font, myBrush, New PointF(Bayes.SampsB.ConditionalX, Bayes.SampsB.ConditionalY))
            End If

            'Event Not B probability:
            If Bayes.ProbNotB.Label.Show Then
                myBrush = New SolidBrush(Bayes.ProbNotB.Label.Color)
                If Bayes.ProbNotB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotProbNotBPosn()
                If Bayes.ProbNotB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotProbNotBPosn()
                'g.DrawString(Bayes.ProbNotB.Label.Text, Bayes.ProbNotB.Label.Font, myBrush, New PointF(Bayes.ProbNotB.Label.X, Bayes.ProbNotB.Label.Y))
                g.DrawString(Bayes.ProbNotB.ConditionalText, Bayes.ProbNotB.Label.Font, myBrush, New PointF(Bayes.ProbNotB.ConditionalX, Bayes.ProbNotB.ConditionalY))
            End If

            'Event Not B Sample Count:
            If Bayes.SampsNotB.Label.Show Then
                myBrush = New SolidBrush(Bayes.SampsNotB.Label.Color)
                If Bayes.SampsNotB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotSampsNotBPosn()
                If Bayes.SampsNotB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotSampsNotBPosn()
                'g.DrawString(Bayes.SampsNotB.Label.Text, Bayes.SampsNotB.Label.Font, myBrush, New PointF(Bayes.SampsNotB.Label.X, Bayes.SampsNotB.Label.Y))
                g.DrawString(Bayes.SampsNotB.ConditionalText, Bayes.SampsNotB.Label.Font, myBrush, New PointF(Bayes.SampsNotB.ConditionalX, Bayes.SampsNotB.ConditionalY))
            End If

            'A and B probability:
            If Bayes.ProbAandB.Label.Show Then
                myBrush = New SolidBrush(Bayes.ProbAandB.Label.Color)
                If Bayes.ProbAandB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotProbAandBPosn()
                If Bayes.ProbAandB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotProbAandBPosn()
                'g.DrawString(Bayes.ProbAandB.Label.Text, Bayes.ProbAandB.Label.Font, myBrush, New PointF(Bayes.ProbAandB.Label.X, Bayes.ProbAandB.Label.Y))
                g.DrawString(Bayes.ProbAandB.ConditionalText, Bayes.ProbAandB.Label.Font, myBrush, New PointF(Bayes.ProbAandB.ConditionalX, Bayes.ProbAandB.ConditionalY))
            End If

            'A and B sample count:
            If Bayes.SampsAandB.Label.Show Then
                myBrush = New SolidBrush(Bayes.SampsAandB.Label.Color)
                If Bayes.SampsAandB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotSampsAandBPosn()
                If Bayes.SampsAandB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotSampsAandBPosn()
                'g.DrawString(Bayes.SampsAandB.Label.Text, Bayes.SampsAandB.Label.Font, myBrush, New PointF(Bayes.SampsAandB.Label.X, Bayes.SampsAandB.Label.Y))
                g.DrawString(Bayes.SampsAandB.ConditionalText, Bayes.SampsAandB.Label.Font, myBrush, New PointF(Bayes.SampsAandB.ConditionalX, Bayes.SampsAandB.ConditionalY))
            End If

            'A and Not B probability:
            If Bayes.ProbAandNotB.Label.Show Then
                myBrush = New SolidBrush(Bayes.ProbAandNotB.Label.Color)
                If Bayes.ProbAandNotB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotProbAandNotBPosn()
                If Bayes.ProbAandNotB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotProbAandNotBPosn()
                'g.DrawString(Bayes.ProbAandNotB.Label.Text, Bayes.ProbAandNotB.Label.Font, myBrush, New PointF(Bayes.ProbAandNotB.Label.X, Bayes.ProbAandNotB.Label.Y))
                g.DrawString(Bayes.ProbAandNotB.ConditionalText, Bayes.ProbAandNotB.Label.Font, myBrush, New PointF(Bayes.ProbAandNotB.ConditionalX, Bayes.ProbAandNotB.ConditionalY))
            End If

            'A and Not B sample count:
            If Bayes.SampsAandNotB.Label.Show Then
                myBrush = New SolidBrush(Bayes.SampsAandNotB.Label.Color)
                If Bayes.SampsAandNotB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotSampsAandNotBPosn()
                If Bayes.SampsAandNotB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotSampsAandNotBPosn()
                'g.DrawString(Bayes.SampsAandNotB.Label.Text, Bayes.SampsAandNotB.Label.Font, myBrush, New PointF(Bayes.SampsAandNotB.Label.X, Bayes.SampsAandNotB.Label.Y))
                g.DrawString(Bayes.SampsAandNotB.ConditionalText, Bayes.SampsAandNotB.Label.Font, myBrush, New PointF(Bayes.SampsAandNotB.ConditionalX, Bayes.SampsAandNotB.ConditionalY))
            End If

            'Not A and B probability:
            If Bayes.ProbNotAandB.Label.Show Then
                myBrush = New SolidBrush(Bayes.ProbNotAandB.Label.Color)
                If Bayes.ProbNotAandB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotProbNotAandBPosn()
                If Bayes.ProbNotAandB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotProbNotAandBPosn()
                'g.DrawString(Bayes.ProbNotAandB.Label.Text, Bayes.ProbNotAandB.Label.Font, myBrush, New PointF(Bayes.ProbNotAandB.Label.X, Bayes.ProbNotAandB.Label.Y))
                g.DrawString(Bayes.ProbNotAandB.ConditionalText, Bayes.ProbNotAandB.Label.Font, myBrush, New PointF(Bayes.ProbNotAandB.ConditionalX, Bayes.ProbNotAandB.ConditionalY))
            End If

            'Not A and B sample count:
            If Bayes.SampsNotAandB.Label.Show Then
                myBrush = New SolidBrush(Bayes.SampsNotAandB.Label.Color)
                If Bayes.SampsNotAandB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotSampsNotAandBPosn()
                If Bayes.SampsNotAandB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotSampsNotAandBPosn()
                'g.DrawString(Bayes.SampsNotAandB.Label.Text, Bayes.SampsNotAandB.Label.Font, myBrush, New PointF(Bayes.SampsNotAandB.Label.X, Bayes.SampsNotAandB.Label.Y))
                g.DrawString(Bayes.SampsNotAandB.ConditionalText, Bayes.SampsNotAandB.Label.Font, myBrush, New PointF(Bayes.SampsNotAandB.ConditionalX, Bayes.SampsNotAandB.ConditionalY))
            End If

            'Not A and Not B probability:
            If Bayes.ProbNotAandNotB.Label.Show Then
                'myFontWidth = g.MeasureString(Bayes.ProbNotAandNotB.Label.Text & ProbString(1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value), Bayes.ProbNotAandNotB.Label.Font).Width
                myFontWidth = g.MeasureString(Bayes.ProbNotAandNotB.Label.Text & Bayes.ProbNotAandNotB.FormattedValue, Bayes.ProbNotAandNotB.Label.Font).Width
                myBrush = New SolidBrush(Bayes.ProbNotAandNotB.Label.Color)
                If Bayes.ProbNotAandNotB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotProbNotAandNotBPosn()
                If Bayes.ProbNotAandNotB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotProbNotAandNotBPosn()
                'g.DrawString(Bayes.ProbNotAandNotB.Label.Text, Bayes.ProbNotAandNotB.Label.Font, myBrush, New PointF(Bayes.ProbNotAandNotB.Label.X, Bayes.ProbNotAandNotB.Label.Y)) 'X is the position of the start of the text
                g.DrawString(Bayes.ProbNotAandNotB.ConditionalText, Bayes.ProbNotAandNotB.Label.Font, myBrush, New PointF(Bayes.ProbNotAandNotB.ConditionalX, Bayes.ProbNotAandNotB.ConditionalY)) 'X is the position of the start of the text
            End If

            'Not A and Not B sample count:
            If Bayes.SampsNotAandNotB.Label.Show Then
                'myFontWidth = g.MeasureString(Bayes.SampsNotAandNotB.Label.Text & SampString((1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) * Bayes.SampleSize), Bayes.SampsNotAandNotB.Label.Font).Width
                myFontWidth = g.MeasureString(Bayes.SampsNotAandNotB.Label.Text & SampString((1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) * Bayes.SampleSize.Value), Bayes.SampsNotAandNotB.Label.Font).Width

                myBrush = New SolidBrush(Bayes.SampsNotAandNotB.Label.Color)
                If Bayes.SampsNotAandNotB.Label.Unscaled.X = -1 Then Bayes.DefaultAnnotSampsNotAandNotBPosn()
                If Bayes.SampsNotAandNotB.Label.Unscaled.Y = -1 Then Bayes.DefaultAnnotSampsNotAandNotBPosn()
                'g.DrawString(Bayes.SampsNotAandNotB.Label.Text, Bayes.SampsNotAandNotB.Label.Font, myBrush, New PointF(Bayes.SampsNotAandNotB.Label.X, Bayes.SampsNotAandNotB.Label.Y)) 'X is the position of the start of the text
                g.DrawString(Bayes.SampsNotAandNotB.ConditionalText, Bayes.SampsNotAandNotB.Label.Font, myBrush, New PointF(Bayes.SampsNotAandNotB.ConditionalX, Bayes.SampsNotAandNotB.ConditionalY)) 'X is the position of the start of the text
            End If

            'Sample Size:
            'If Bayes.AnnotSampleSize.Show Then
            '    myFontWidth = g.MeasureString(Bayes.AnnotSampleSize.Text & SampString(Bayes.SampleSize.Value), Bayes.AnnotSampleSize.Font).Width
            '    myBrush = New SolidBrush(Bayes.AnnotSampleSize.Color)
            '    If Bayes.AnnotSampleSize.X = -1 Then Bayes.DefaultAnnotSampleSizePosn()
            '    If Bayes.AnnotSampleSize.Y = -1 Then Bayes.DefaultAnnotSampleSizePosn()
            '    'g.DrawString(Bayes.AnnotSampleSize.Text & SampString(Bayes.SampleSize), Bayes.AnnotSampleSize.Font, myBrush, New PointF(Bayes.AnnotSampleSize.X, Bayes.AnnotSampleSize.Y)) 'X is the position of the start of the text
            '    g.DrawString(Bayes.AnnotSampleSize.Text, Bayes.AnnotSampleSize.Font, myBrush, New PointF(Bayes.AnnotSampleSize.X, Bayes.AnnotSampleSize.Y)) 'X is the position of the start of the text
            'End If

            If Bayes.SampleSize.Show Then
                myFontWidth = g.MeasureString(Bayes.SampleSize.Label.Text & SampString(Bayes.SampleSize.Value), Bayes.AnnotSampleSize.Font).Width
                myBrush = New SolidBrush(Bayes.SampleSize.Label.Color)
                If Bayes.SampleSize.Label.X = -1 Then Bayes.DefaultAnnotSampleSizePosn()
                If Bayes.SampleSize.Label.Y = -1 Then Bayes.DefaultAnnotSampleSizePosn()
                'g.DrawString(Bayes.AnnotSampleSize.Text & SampString(Bayes.SampleSize), Bayes.AnnotSampleSize.Font, myBrush, New PointF(Bayes.AnnotSampleSize.X, Bayes.AnnotSampleSize.Y)) 'X is the position of the start of the text
                g.DrawString(Bayes.SampleSize.Label.Text, Bayes.SampleSize.Label.Font, myBrush, New PointF(Bayes.SampleSize.Label.X, Bayes.SampleSize.Label.Y)) 'X is the position of the start of the text
            End If

            'Category Condition:
            If Bayes.AnnotCondition.Show Then
                myBrush = New SolidBrush(Bayes.AnnotCondition.Color)
                If Bayes.AnnotCondition.X = -1 Then Bayes.DefaultAnnotConditionPosn()
                If Bayes.AnnotCondition.Y = -1 Then Bayes.DefaultAnnotConditionPosn()
                g.DrawString(Bayes.AnnotCondition.Text, Bayes.AnnotCondition.Font, myBrush, New PointF(Bayes.AnnotCondition.X, Bayes.AnnotCondition.Y)) 'X is the position of the end of the text. (It is usually right justified near the RHS of the diagram.)
            End If

            'Conditional sample size:
            If Bayes.AnnotConditionalSampleSize.Show Then
                myBrush = New SolidBrush(Bayes.AnnotConditionalSampleSize.Color)
                If Bayes.AnnotConditionalSampleSize.X = -1 Then Bayes.DefaultAnnotSampleSizePosn()
                If Bayes.AnnotConditionalSampleSize.Y = -1 Then Bayes.DefaultAnnotSampleSizePosn()
                g.DrawString(Bayes.AnnotConditionalSampleSize.Text, Bayes.AnnotConditionalSampleSize.Font, myBrush, New PointF(Bayes.AnnotConditionalSampleSize.X, Bayes.AnnotConditionalSampleSize.Y))
            End If

        End Using

        pbVenn.Image?.Dispose()
        pbVenn.Image = Diag

    End Sub


    Private Sub btnNewMCModel_Click(sender As Object, e As EventArgs) Handles btnNewMCModel.Click
        'Çreate a New Bayes model.

        'Get the new model File Name, Model Name and Description:
        Dim EntryForm As New ADVL_Utilities_Library_1.frmNewDataNameModal
        EntryForm.EntryName = "NewBayesModel"
        EntryForm.Title = "New Bayes Model"
        EntryForm.FileExtension = "Bayes"
        EntryForm.GetFileName = True
        EntryForm.GetDataName = True
        EntryForm.GetDataLabel = True
        EntryForm.GetDataDescription = True
        EntryForm.SettingsLocn = Project.SettingsLocn
        EntryForm.DataLocn = Project.DataLocn
        EntryForm.ApplicationName = ApplicationInfo.Name
        EntryForm.RestoreFormSettings()

        If EntryForm.ShowDialog() = DialogResult.OK Then
            If txtFileName.Text.Trim = "" Then
                'There is no model to save.
            Else
                If Bayes.Modified Then
                    Dim Result As DialogResult = MessageBox.Show("Do you want to save the changes in the current Bayes model?", "Warning", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        'SaveMonteCarloModel()
                        SaveBayesModel()
                    ElseIf Result = DialogResult.Cancel Then
                        Exit Sub
                    Else
                        'Contunue without saving the current model.
                        Bayes.Modified = False
                    End If
                Else

                End If
            End If
            Bayes.Clear() 'Clear the current Bayes model.
            Bayes.FileName = EntryForm.FileName
            Bayes.Name = EntryForm.DataName
            Bayes.Label = EntryForm.DataLabel
            Bayes.Description = EntryForm.DataDescription
            UpdateForm()
        End If

    End Sub

    Private Sub btnSaveMCModel_Click(sender As Object, e As EventArgs) Handles btnSaveMCModel.Click
        'Save the Bayes model.
        SaveBayesModel()
    End Sub

    Private Sub SaveBayesModel()
        'Save the Bayes mode.

        Dim FileName As String = txtFileName.Text.Trim

        'Check if a file name has been specified:
        If FileName = "" Then
            Message.AddWarning("Please enter a file name." & vbCrLf)
            Exit Sub
        End If

        'Check the fine name extension:
        If LCase(FileName).EndsWith(".bayes") Then
            FileName = IO.Path.GetFileNameWithoutExtension(FileName) & ".Bayes"
        ElseIf FileName.Contains(".") Then
            Message.AddWarning("Unknown file extension: " & IO.Path.GetExtension(FileName) & vbCrLf)
            Exit Sub
        Else
            FileName = FileName & ".Bayes"
        End If

        txtFileName.Text = FileName

        'Update the Bayes settings:
        Bayes.Name = txtModelName.Text.Trim
        Bayes.Label = txtLabel.Text.Trim
        Bayes.Description = txtDescription.Text.Trim

        Project.SaveXmlData(FileName, Bayes.BayesToXDoc)
        Bayes.Modified = False

    End Sub

    Private Sub btnOpenMCModel_Click(sender As Object, e As EventArgs) Handles btnOpenMCModel.Click
        'Open a Bayes model.

        Dim FileName As String = Project.SelectDataFile("Bayes model files", "Bayes")
        If FileName = "" Then
            'No file has been selected.
        Else
            OpenBayesModel(FileName)
            CurrentSimSettings() 'Use the current Bayes model for the simulation settings.
        End If

    End Sub



    Private Sub OpenBayesModel(ByVal FileName As String)
        'Open a Bayes model.

        'Remove existing model data:
        txtFileName.Text = ""
        txtModelName.Text = ""
        txtLabel.Text = ""
        txtDescription.Text = ""
        txtNotes.Text = ""

        txtEventAName.Text = ""
        txtEventADescr.Text = ""
        txtEventBName.Text = ""
        txtEventBDescr.Text = ""

        txtEventA.Text = ""
        txtEventB.Text = ""
        txtProbA.Text = ""
        txtProbB.Text = ""
        txtProbBgivenA.Text = ""
        txtProbBgivenNotA.Text = ""
        txtProbAgivenB.Text = ""

        txtProbNotAgivenB.Text = ""
        txtProbNotAgivenNotB.Text = ""
        txtProbAgivenNotB.Text = ""

        Dim XDoc As System.Xml.Linq.XDocument
        Project.ReadXmlData(FileName, XDoc)
        Bayes.FileName = FileName
        txtFileName.Text = FileName
        Bayes.XDocToBayes(XDoc)

        UpdateForm()

        Exit Sub

        'OLD CODE:
        txtModelName.Text = Bayes.Name
        txtLabel.Text = Bayes.Label
        txtDescription.Text = Bayes.Description

        txtEventAName.Text = Bayes.EventA.Name
        txtEventA.Text = Bayes.EventA.Name
        txtEventNotAName.Text = Bayes.EventA.NotName
        txtEventADescr.Text = Bayes.EventA.Description
        txtEventBName.Text = Bayes.EventB.Name
        txtEventB.Text = Bayes.EventB.Name
        txtEventBDescr.Text = Bayes.EventB.Description
        txtEventNotBName.Text = Bayes.EventB.NotName

        'Default annotation:
        txtAnnotTitle.Text = Bayes.Label
        txtAnnotDescr.Text = Bayes.Description
        txtAnnotEventALabel.Text = Bayes.EventA.Name
        txtAnnotEventBLabel.Text = Bayes.EventB.Name

        'txtSampleSize.Text = Bayes.SampleSize

        Select Case Bayes.InputInfoType
            'Case "P(A), P(B), P(B|A)"
            Case "P(B|A), P(A), P(B)"
                'cmbInputInfo.SelectedIndex = cmbInputInfo.FindStringExact("P(A), P(B), P(B|A)")
                cmbInputInfo.SelectedIndex = cmbInputInfo.FindStringExact("P(B|A), P(A), P(B)")
                'Case "P(A), P(B|A), P(B|NotA)"
            Case "P(B|A), P(A), P(B|NotA)"
                'cmbInputInfo.SelectedIndex = cmbInputInfo.FindStringExact("P(A), P(B), P(B|NotA)")
                cmbInputInfo.SelectedIndex = cmbInputInfo.FindStringExact("P(B|A), P(A), P(B|NotA)")
            Case Else
                Message.AddWarning("Unknown input information type: " & Bayes.InputInfoType & vbCrLf)
        End Select

        'Select Case Bayes.ProbabilityMeasure
        Select Case Bayes.Settings.ProbabilityMeasure
            Case "Decimal"
                rbDecimal.Checked = True
                ShowDecimalProbabilities()
            Case "Percent"
                rbPercent.Checked = True
                ShowPercentProbabilities()
                'Case "Samples"
                '    rbSamples.Checked = True
                '    ShowSampleCounts()
            Case Else
                'Message.AddWarning("Unknown probability measure: " & Bayes.ProbabilityMeasure & vbCrLf)
                Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
        End Select

        'Select Case Bayes.Condition
        Select Case Bayes.Settings.Condition
            Case "None"
                rbConditionNone.Checked = True
            Case "EventATrue"
                rbConditionA.Checked = True
            Case "EventAFalse"
                rbConditionNotA.Checked = True
            Case "EventBTrue"
                rbConditionB.Checked = True
            Case "EventBFalse"
                rbConditionNotB.Checked = True
            Case Else
                'Message.AddWarning("Unknown condition: " & Bayes.Condition & vbCrLf)
                Message.AddWarning("Unknown condition: " & Bayes.Settings.Condition & vbCrLf)
                rbConditionNone.Checked = True
        End Select


        ShowSampleCounts()

        'txtDecimalFormat.Text = Bayes.DecimalFormat
        txtDecimalFormat.Text = Bayes.Settings.DecimalFormat
        txtPercentFormat.Text = Bayes.Settings.PercentFormat
        txtSamplesFormat.Text = Bayes.Settings.SamplesFormat

        'chkShowDecimalProb.Checked = Bayes.ShowDecimalProbability
        'chkShowPercentProb.Checked = Bayes.ShowPercentProbability
        'chkShowSamples.Checked = Bayes.ShowSampleCounts

        UpdateCategoryInfo() 'Show the Sample Count and Probability in each event category
        'UpdateAnnotProbPage() 'Update the annotation probability page.
        ShowPerformanceMetrics()

    End Sub

    Private Sub UpdateForm()
        'Update the application form with the Bayes model.

        'Update the Bayes Model tab: -------------------------------------------------------------
        txtFileName.Text = Bayes.FileName
        txtModelName.Text = Bayes.Name
        txtLabel.Text = Bayes.Label
        txtDescription.Text = Bayes.Description
        txtNotes.Text = Bayes.Notes


        'Update the Events tab: -------------------------------------------------------------------
        txtEventAName.Text = Bayes.EventA.Name
        txtEventA.Text = Bayes.EventA.Name
        txtEventNotAName.Text = Bayes.EventA.NotName
        txtEventADescr.Text = Bayes.EventA.Description
        txtEventBName.Text = Bayes.EventB.Name
        txtEventB.Text = Bayes.EventB.Name
        txtEventBDescr.Text = Bayes.EventB.Description
        txtEventNotBName.Text = Bayes.EventB.NotName

        txtEventAName.Text = Bayes.EventA.Name
        txtEventADescr.Text = Bayes.EventA.Description
        txtEventNotAName.Text = Bayes.EventA.NotName
        txtEventBName.Text = Bayes.EventB.Name
        txtEventBDescr.Text = Bayes.EventB.Description
        txtEventNotBName.Text = Bayes.EventB.NotName

        'Update the Probabilities tab: ------------------------------------------------------------
        Select Case Bayes.InputInfoType
            'Case "P(A), P(B), P(B|A)"
            Case "P(B|A), P(A), P(B)"
                'cmbInputInfo.SelectedIndex = cmbInputInfo.FindStringExact("P(A), P(B), P(B|A)")
                cmbInputInfo.SelectedIndex = cmbInputInfo.FindStringExact("P(B|A), P(A), P(B)")
                'Case "P(A), P(B|A), P(B|NotA)"
            Case "P(B|A), P(A), P(B|NotA)"
                'cmbInputInfo.SelectedIndex = cmbInputInfo.FindStringExact("P(A), P(B), P(B|NotA)")
                cmbInputInfo.SelectedIndex = cmbInputInfo.FindStringExact("P(B|A), P(A), P(B|NotA)")

            Case "Sample Counts"
                cmbInputInfo.SelectedIndex = cmbInputInfo.FindStringExact("Sample Counts (TP, TN, FP, FN)")

            Case Else
                Message.AddWarning("Unknown input information type: " & Bayes.InputInfoType & vbCrLf)
        End Select

        txtEventA.Text = Bayes.EventA.Name
        txtEventB.Text = Bayes.EventB.Name

        If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
            rbDecimal.Checked = True
        ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
            rbPercent.Checked = True
        Else
            Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
            Message.AddWarning("Decimal probabilites will be selected." & vbCrLf)
            Bayes.Settings.ProbabilityMeasure = "Decimal"
            rbDecimal.Checked = True
        End If

        txtDecimalFormat.Text = Bayes.Settings.DecimalFormat
        txtPercentFormat.Text = Bayes.Settings.PercentFormat
        txtSamplesFormat.Text = Bayes.Settings.SamplesFormat

        ShowProbabilities()
        ShowSampleCounts()

        RedisplayGenConfIntVals() 'This redisplays the values in the General Confidence Interval calculator using the preferred sample count and probability formats.
        UpdateEventSimSettings() 'This redisplays the Event Simulation settings using the preferred sample count and probability formats.

        'Update the Categories tab: -----------------------------------------------------------------------
        UpdateCategoryInfo()

        'Update the Display tab: --------------------------------------------------------------------------------
        UpdateDisplayTab()
        'txtLineColor.BackColor = Bayes.Diagram.LineColor
        'txtLineThickness.Text = Bayes.Diagram.LineThickness
        'txtBoldLineThickness.Text = Bayes.Diagram.BoldLineThickness
        'chkBoldLine.Checked = Bayes.Diagram.BoldLine
        'txtFillColor.BackColor = Bayes.Diagram.FillColor
        'txtWidth.Text = Bayes.Diagram.Width
        'txtHeight.Text = Bayes.Diagram.Height

        'txtEventALineColor.BackColor = Bayes.EventA.LineColor
        'txtEventALineThickness.Text = Bayes.EventA.LineThickness
        'txtEventABoldLineThickness.Text = Bayes.EventA.BoldLineThickness
        'chkEventABoldLine.Checked = Bayes.EventA.BoldLine
        'txtEventAFillColor.BackColor = Bayes.EventA.FillColor
        'cmbEventAShape.SelectedIndex = cmbEventAShape.FindStringExact(Bayes.EventA.Shape)
        'txtEventAXMin.Text = Bayes.EventA.Unscaled.XMin
        'txtEventAXMax.Text = Bayes.EventA.Unscaled.XMax
        'txtEventAYMin.Text = Bayes.EventA.Unscaled.YMin
        'txtEventAYMax.Text = Bayes.EventA.Unscaled.YMax

        'txtEventBLineColor.BackColor = Bayes.EventB.LineColor
        'txtEventBLineThickness.Text = Bayes.EventB.LineThickness
        'txtEventBBoldLineThickness.Text = Bayes.EventB.BoldLineThickness
        'chkEventBBoldLine.Checked = Bayes.EventB.BoldLine
        'txtEventBFillColor.BackColor = Bayes.EventB.FillColor
        'cmbEventBShape.SelectedIndex = cmbEventBShape.FindStringExact(Bayes.EventB.Shape)
        'txtEventBXMin.Text = Bayes.EventB.Unscaled.XMin
        'txtEventBXMax.Text = Bayes.EventB.Unscaled.XMax
        'txtEventBYMin.Text = Bayes.EventB.Unscaled.YMin
        'txtEventBYMax.Text = Bayes.EventB.Unscaled.YMax

        'txtEventAandBFillColor.BackColor = Bayes.EventAandBFillColor

        'Update the Annotation tab: -----------------------------------------------------------------------
        UpdateAnnotationTab()
        ''Labels:
        'txtAnnotTitle.Text = Bayes.AnnotTitle.Text
        'txtAnnotTitle.Font = Bayes.AnnotTitle.Font
        'txtAnnotTitle.ForeColor = Bayes.AnnotTitle.Color
        ''txtAnnotTitleMidX.Text = Bayes.AnnotTitle.X
        'txtAnnotTitleMidX.Text = Bayes.AnnotTitle.MidX
        'txtAnnotTitleY.Text = Bayes.AnnotTitle.Y

        'txtAnnotDescr.Text = Bayes.AnnotDescr.Text
        'txtAnnotDescr.Font = Bayes.AnnotDescr.Font
        'txtAnnotDescr.ForeColor = Bayes.AnnotDescr.Color
        ''txtAnnotDescrMidX.Text = Bayes.AnnotDescr.X
        'txtAnnotDescrMidX.Text = Bayes.AnnotDescr.MidX
        'txtAnnotDescrY.Text = Bayes.AnnotDescr.Y

        ''txtAnnotEventALabel.Text = Bayes.AnnotEventA.Unscaled.Text
        ''txtAnnotEventALabel.Font = Bayes.AnnotEventA.Unscaled.Font
        ''txtAnnotEventALabel.ForeColor = Bayes.AnnotEventA.Unscaled.Color
        ''txtAnnotEventAX.Text = Bayes.AnnotEventA.Unscaled.X
        ''txtAnnotEventAY.Text = Bayes.AnnotEventA.Unscaled.Y
        'txtAnnotEventALabel.Text = Bayes.AnnotEventA.Text
        'txtAnnotEventALabel.Font = Bayes.AnnotEventA.Font
        'txtAnnotEventALabel.ForeColor = Bayes.AnnotEventA.Color
        ''txtAnnotEventAMidX.Text = Bayes.AnnotEventA.X
        'txtAnnotEventAMidX.Text = Bayes.AnnotEventA.MidX
        'txtAnnotEventAY.Text = Bayes.AnnotEventA.Y

        'txtAnnotEventBLabel.Text = Bayes.AnnotEventB.Text
        'txtAnnotEventBLabel.Font = Bayes.AnnotEventB.Font
        'txtAnnotEventBLabel.ForeColor = Bayes.AnnotEventB.Color
        ''txtAnnotEventBMidX.Text = Bayes.AnnotEventB.X
        'txtAnnotEventBMidX.Text = Bayes.AnnotEventB.MidX
        'txtAnnotEventBY.Text = Bayes.AnnotEventB.Y

        UpdateAnnotProbPage()
        ''Probabilities:
        ''txtAnnotProbA.Text = Bayes.ProbA.Label.Unscaled.Text
        'txtAnnotProbA.Text = Bayes.ProbA.Prefix
        'txtAnnotProbA.Font = Bayes.ProbA.Label.Font
        'txtAnnotProbA.ForeColor = Bayes.ProbA.Label.Color
        'txtAnnotProbAX.Text = Bayes.ProbA.Label.X
        'txtAnnotProbAY.Text = Bayes.ProbA.Label.Y
        'chkAnnotProbA.Checked = Bayes.ProbA.Label.Show

        ''txtAnnotProbNotA.Text = Bayes.ProbNotA.Label.Unscaled.Text
        'txtAnnotProbNotA.Text = Bayes.ProbNotA.Prefix
        'txtAnnotProbNotA.Font = Bayes.ProbNotA.Label.Font
        'txtAnnotProbNotA.ForeColor = Bayes.ProbNotA.Label.Color
        'txtAnnotProbNotAX.Text = Bayes.ProbNotA.Label.X
        'txtAnnotProbNotAY.Text = Bayes.ProbNotA.Label.Y
        'chkAnnotProbNotA.Checked = Bayes.ProbNotA.Label.Show

        ''txtAnnotProbB.Text = Bayes.ProbB.Label.Unscaled.Text
        'txtAnnotProbB.Text = Bayes.ProbB.Prefix
        'txtAnnotProbB.Font = Bayes.ProbB.Label.Font
        'txtAnnotProbB.ForeColor = Bayes.ProbB.Label.Color
        'txtAnnotProbBX.Text = Bayes.ProbB.Label.X
        'txtAnnotProbBY.Text = Bayes.ProbB.Label.Y
        'chkAnnotProbB.Checked = Bayes.ProbB.Label.Show

        'txtAnnotProbNotB.Text = Bayes.ProbNotB.Prefix
        'txtAnnotProbNotB.Font = Bayes.ProbNotB.Label.Font
        'txtAnnotProbNotB.ForeColor = Bayes.ProbNotB.Label.Color
        'txtAnnotProbNotBX.Text = Bayes.ProbNotB.Label.X
        'txtAnnotProbNotBY.Text = Bayes.ProbNotB.Label.Y
        'chkAnnotProbNotB.Checked = Bayes.ProbNotB.Label.Show

        ''txtAnnotProbAandB.Text = Bayes.ProbAandBText
        'txtAnnotProbAandB.Text = Bayes.ProbAandB.Prefix
        'txtAnnotProbAandB.Font = Bayes.ProbAandB.Label.Font
        'txtAnnotProbAandB.ForeColor = Bayes.ProbAandB.Label.Color
        'txtAnnotProbAandBX.Text = Bayes.ProbAandB.Label.X
        'txtAnnotProbAandBY.Text = Bayes.ProbAandB.Label.Y
        'chkAnnotProbAandB.Checked = Bayes.ProbAandB.Label.Show

        'txtAnnotProbAandNotB.Text = Bayes.ProbAandNotB.Prefix
        'txtAnnotProbAandNotB.Font = Bayes.ProbAandNotB.Label.Font
        'txtAnnotProbAandNotB.ForeColor = Bayes.ProbAandNotB.Label.Color
        'txtAnnotProbAandNotBX.Text = Bayes.ProbAandNotB.Label.X
        'txtAnnotProbAandNotBY.Text = Bayes.ProbAandNotB.Label.Y
        'chkAnnotProbAandNotB.Checked = Bayes.ProbAandNotB.Label.Show

        'txtAnnotProbNotAandB.Text = Bayes.ProbNotAandB.Prefix
        'txtAnnotProbNotAandB.Font = Bayes.ProbNotAandB.Label.Font
        'txtAnnotProbNotAandB.ForeColor = Bayes.ProbNotAandB.Label.Color
        'txtAnnotProbNotAandBX.Text = Bayes.ProbNotAandB.Label.X
        'txtAnnotProbNotAandBY.Text = Bayes.ProbNotAandB.Label.Y
        'chkAnnotProbNotAandB.Checked = Bayes.ProbNotAandB.Label.Show

        'txtAnnotProbNotAandNotB.Text = Bayes.ProbNotAandNotB.Prefix
        'txtAnnotProbNotAandNotB.Font = Bayes.ProbNotAandNotB.Label.Font
        'txtAnnotProbNotAandNotB.ForeColor = Bayes.ProbNotAandNotB.Label.Color
        'txtAnnotProbNotAandNotBX.Text = Bayes.ProbNotAandNotB.Label.X
        'txtAnnotProbNotAandNotBY.Text = Bayes.ProbNotAandNotB.Label.Y
        'chkAnnotProbNotAandNotB.Checked = Bayes.ProbNotAandNotB.Label.Show

        ''Samples
        ''txtAnnotSampsA.Text = Bayes.SampsA.Label.Unscaled.Text
        'txtAnnotSampsA.Text = Bayes.SampsA.Prefix
        'txtAnnotSampsA.Font = Bayes.SampsA.Label.Font
        'txtAnnotSampsA.ForeColor = Bayes.SampsA.Label.Color
        'txtAnnotSampsAVal.Text = SampString(Bayes.ProbA.Value * Bayes.SampleSize)
        'txtAnnotSampsAX.Text = Bayes.SampsA.Label.X
        'txtAnnotSampsAY.Text = Bayes.SampsA.Label.Y
        'chkAnnotSampsA.Checked = Bayes.SampsA.Label.Show

        'txtAnnotSampsNotA.Text = Bayes.SampsNotA.Prefix
        'txtAnnotSampsNotA.Font = Bayes.SampsNotA.Label.Font
        'txtAnnotSampsNotA.ForeColor = Bayes.SampsNotA.Label.Color
        'txtAnnotSampsNotAVal.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize)
        'txtAnnotSampsNotAX.Text = Bayes.SampsNotA.Label.X
        'txtAnnotSampsNotAY.Text = Bayes.SampsNotA.Label.Y
        'chkAnnotSampsNotA.Checked = Bayes.SampsNotA.Label.Show

        'txtAnnotSampsB.Text = Bayes.SampsB.Prefix
        'txtAnnotSampsB.Font = Bayes.SampsB.Label.Font
        'txtAnnotSampsB.ForeColor = Bayes.SampsB.Label.Color
        'txtAnnotSampsBVal.Text = SampString(Bayes.ProbB.Value * Bayes.SampleSize)
        'txtAnnotSampsBX.Text = Bayes.SampsB.Label.X
        'txtAnnotSampsBY.Text = Bayes.SampsB.Label.Y
        'chkAnnotSampsB.Checked = Bayes.SampsB.Label.Show

        'txtAnnotSampsNotB.Text = Bayes.SampsNotB.Prefix
        'txtAnnotSampsNotB.Font = Bayes.SampsNotB.Label.Font
        'txtAnnotSampsNotB.ForeColor = Bayes.SampsNotB.Label.Color
        'txtAnnotSampsNotBVal.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize)
        'txtAnnotSampsNotBX.Text = Bayes.SampsNotB.Label.X
        'txtAnnotSampsNotBY.Text = Bayes.SampsNotB.Label.Y
        'chkAnnotSampsNotB.Checked = Bayes.SampsNotB.Label.Show

        'txtAnnotSampsAandB.Text = Bayes.SampsAandB.Prefix
        'txtAnnotSampsAandB.Font = Bayes.SampsAandB.Label.Font
        'txtAnnotSampsAandB.ForeColor = Bayes.SampsAandB.Label.Color
        'txtAnnotSampsAandBVal.Text = SampString(Bayes.ProbAandB.Value * Bayes.SampleSize)
        'txtAnnotSampsAandBX.Text = Bayes.SampsAandB.Label.Unscaled.X
        'txtAnnotSampsAandBY.Text = Bayes.SampsAandB.Label.Unscaled.Y
        'chkAnnotSampsAandB.Checked = Bayes.SampsAandB.Label.Show

        'txtAnnotSampsAandNotB.Text = Bayes.SampsAandNotB.Prefix
        'txtAnnotSampsAandNotB.Font = Bayes.SampsAandNotB.Label.Font
        'txtAnnotSampsAandNotB.ForeColor = Bayes.SampsAandNotB.Label.Color
        'txtAnnotSampsAandNotBVal.Text = SampString((Bayes.ProbA.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
        'txtAnnotSampsAandNotBX.Text = Bayes.SampsAandNotB.Label.X
        'txtAnnotSampsAandNotBY.Text = Bayes.SampsAandNotB.Label.Y
        'chkAnnotSampsAandNotB.Checked = Bayes.SampsAandNotB.Label.Show

        'txtAnnotSampsNotAandB.Text = Bayes.SampsNotAandB.Prefix
        'txtAnnotSampsNotAandB.Font = Bayes.SampsNotAandB.Label.Font
        'txtAnnotSampsNotAandB.ForeColor = Bayes.SampsNotAandB.Label.Color
        'txtAnnotSampsNotAandBVal.Text = SampString((Bayes.ProbB.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
        'txtAnnotSampsNotAandBX.Text = Bayes.SampsNotAandB.Label.X
        'txtAnnotSampsNotAandBY.Text = Bayes.SampsNotAandB.Label.Y
        'chkAnnotSampsNotAandB.Checked = Bayes.SampsNotAandB.Label.Show

        'txtAnnotSampsNotAandNotB.Text = Bayes.SampsNotAandNotB.Prefix
        'txtAnnotSampsNotAandNotB.Font = Bayes.SampsNotAandNotB.Label.Font
        'txtAnnotSampsNotAandNotB.ForeColor = Bayes.SampsNotAandNotB.Label.Color
        'txtAnnotSampsNotAandNotBVal.Text = SampString((1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) * Bayes.SampleSize)
        'txtAnnotSampsNotAandNotBX.Text = Bayes.SampsNotAandNotB.Label.X
        'txtAnnotSampsNotAandNotBY.Text = Bayes.SampsNotAandNotB.Label.Y
        'chkAnnotSampsNotAandNotB.Checked = Bayes.SampsNotAandNotB.Label.Show

        'txtAnnotSampsSize.Text = Bayes.SampsSizeText
        'txtAnnotSampsSize.Text = Bayes.AnnotSampleSize.Text
        'txtAnnotSampsSize.Text = Bayes.SampleSizePrefix
        txtAnnotSampsSize.Text = Bayes.SampleSize.Prefix
        txtAnnotSampsSize.Font = Bayes.AnnotSampleSize.Font
        txtAnnotSampsSize.ForeColor = Bayes.AnnotSampleSize.Color
        txtAnnotSampsSizeVal.Text = SampString(Bayes.SampleSize.Value)
        txtAnnotSampsSizeMidX.Text = Bayes.AnnotSampleSize.X
        'txtAnnotSampsSizeBaseY.Text = Bayes.AnnotSampleSize.Y
        txtAnnotSampsSizeBaseY.Text = Bayes.AnnotSampleSize.BaseY
        chkAnnotSampsSize.Checked = Bayes.AnnotSampleSize.Show

        'Update the Probability Diagram: -------------------------------------------------------------------------
        DrawDiagram()
        DrawEventATree()
        DrawEventBTree()

    End Sub

    Private Sub UpdateAnnotProbPage()
        'Update the Annotation Probabilities page.

        'NOTE: The text and position of the annotation will depend on the selected scaling

        'Probabilities:
        'txtAnnotProbA.Text = Bayes.ProbAText
        'txtAnnotProbA.Text = Bayes.ProbA.Label.Unscaled.Text
        'txtAnnotProbA.Text = Bayes.ProbA.Prefix
        txtAnnotProbA.Text = Bayes.ProbA.ConditionalPrefix
        'txtAnnotProbA.Font = Bayes.ProbA.Label.Unscaled.Font
        txtAnnotProbA.Font = Bayes.ProbA.Label.Font
        txtAnnotProbA.ForeColor = Bayes.ProbA.Label.Color
        'txtAnnotProbAVal.Text = ProbString(Bayes.ProbA.Value)
        'txtAnnotProbAVal.Text = Bayes.ProbA.FormattedValue
        txtAnnotProbAVal.Text = Bayes.ProbA.ConditionalFormattedValue
        'txtAnnotProbAMidX.Text = Bayes.ProbA.Label.MidX
        txtAnnotProbAMidX.Text = Bayes.ProbA.ConditionalMidX
        'txtAnnotProbAY.Text = Bayes.ProbA.Label.Y
        txtAnnotProbAY.Text = Bayes.ProbA.ConditionalY
        chkAnnotProbA.Checked = Bayes.ProbA.Label.Show

        'txtAnnotProbNotA.Text = Bayes.ProbNotA.Label.Unscaled.Text
        'txtAnnotProbNotA.Text = Bayes.ProbNotA.Prefix
        txtAnnotProbNotA.Text = Bayes.ProbNotA.ConditionalPrefix
        txtAnnotProbNotA.Font = Bayes.ProbNotA.Label.Font
        txtAnnotProbNotA.ForeColor = Bayes.ProbNotA.Label.Color
        'txtAnnotProbNotAVal.Text = Bayes.ProbNotA.FormattedValue
        txtAnnotProbNotAVal.Text = Bayes.ProbNotA.ConditionalFormattedValue
        'txtAnnotProbNotAMidX.Text = Bayes.ProbNotA.Label.MidX
        txtAnnotProbNotAMidX.Text = Bayes.ProbNotA.ConditionalMidX
        'txtAnnotProbNotAY.Text = Bayes.ProbNotA.Label.Y
        txtAnnotProbNotAY.Text = Bayes.ProbNotA.ConditionalY
        chkAnnotProbNotA.Checked = Bayes.ProbNotA.Label.Show

        'txtAnnotProbB.Text = Bayes.ProbB.Prefix
        txtAnnotProbB.Text = Bayes.ProbB.ConditionalPrefix
        txtAnnotProbB.Font = Bayes.ProbB.Label.Font
        txtAnnotProbB.ForeColor = Bayes.ProbB.Label.Color
        'txtAnnotProbBVal.Text = Bayes.ProbB.FormattedValue
        txtAnnotProbBVal.Text = Bayes.ProbB.ConditionalFormattedValue
        'txtAnnotProbBMidX.Text = Bayes.ProbB.Label.MidX
        txtAnnotProbBMidX.Text = Bayes.ProbB.ConditionalMidX
        'txtAnnotProbBY.Text = Bayes.ProbB.Label.Y
        txtAnnotProbBY.Text = Bayes.ProbB.ConditionalY
        chkAnnotProbB.Checked = Bayes.ProbB.Label.Show

        'txtAnnotProbNotB.Text = Bayes.ProbNotB.Prefix
        txtAnnotProbNotB.Text = Bayes.ProbNotB.ConditionalPrefix
        txtAnnotProbNotB.Font = Bayes.ProbNotB.Label.Font
        txtAnnotProbNotB.ForeColor = Bayes.ProbNotB.Label.Color
        'txtAnnotProbNotBVal.Text = Bayes.ProbNotB.FormattedValue
        txtAnnotProbNotBVal.Text = Bayes.ProbNotB.ConditionalFormattedValue
        'txtAnnotProbNotBMidX.Text = Bayes.ProbNotB.Label.MidX
        txtAnnotProbNotBMidX.Text = Bayes.ProbNotB.ConditionalMidX
        'txtAnnotProbNotBY.Text = Bayes.ProbNotB.Label.Y
        txtAnnotProbNotBY.Text = Bayes.ProbNotB.ConditionalY
        chkAnnotProbNotB.Checked = Bayes.ProbNotB.Label.Show

        'txtAnnotProbAandB.Text = Bayes.ProbAandB.Prefix
        txtAnnotProbAandB.Text = Bayes.ProbAandB.ConditionalPrefix
        txtAnnotProbAandB.Font = Bayes.ProbAandB.Label.Font
        txtAnnotProbAandB.ForeColor = Bayes.ProbAandB.Label.Color
        'txtAnnotProbAandBVal.Text = Bayes.ProbAandB.FormattedValue
        txtAnnotProbAandBVal.Text = Bayes.ProbAandB.ConditionalFormattedValue
        'txtAnnotProbAandBMidX.Text = Bayes.ProbAandB.Label.MidX
        txtAnnotProbAandBMidX.Text = Bayes.ProbAandB.ConditionalMidX
        'txtAnnotProbAandBY.Text = Bayes.ProbAandB.Label.Y
        txtAnnotProbAandBY.Text = Bayes.ProbAandB.ConditionalY
        chkAnnotProbAandB.Checked = Bayes.ProbAandB.Label.Show

        'txtAnnotProbAandNotB.Text = Bayes.ProbAandNotB.Prefix
        txtAnnotProbAandNotB.Text = Bayes.ProbAandNotB.ConditionalPrefix
        txtAnnotProbAandNotB.Font = Bayes.ProbAandNotB.Label.Font
        txtAnnotProbAandNotB.ForeColor = Bayes.ProbAandNotB.Label.Color
        'txtAnnotProbAandNotBVal.Text = Bayes.ProbAandNotB.FormattedValue
        txtAnnotProbAandNotBVal.Text = Bayes.ProbAandNotB.ConditionalFormattedValue
        'txtAnnotProbAandNotBMidX.Text = Bayes.ProbAandNotB.Label.MidX
        txtAnnotProbAandNotBMidX.Text = Bayes.ProbAandNotB.ConditionalMidX
        'txtAnnotProbAandNotBY.Text = Bayes.ProbAandNotB.Label.Y
        txtAnnotProbAandNotBY.Text = Bayes.ProbAandNotB.ConditionalY
        chkAnnotProbAandNotB.Checked = Bayes.ProbAandNotB.Label.Show

        'txtAnnotProbNotAandB.Text = Bayes.ProbNotAandB.Prefix
        txtAnnotProbNotAandB.Text = Bayes.ProbNotAandB.ConditionalPrefix
        txtAnnotProbNotAandB.Font = Bayes.ProbNotAandB.Label.Font
        txtAnnotProbNotAandB.ForeColor = Bayes.ProbNotAandB.Label.Color
        'txtAnnotProbNotAandBVal.Text = Bayes.ProbNotAandB.FormattedValue
        txtAnnotProbNotAandBVal.Text = Bayes.ProbNotAandB.ConditionalFormattedValue
        'txtAnnotProbNotAandBMidX.Text = Bayes.ProbNotAandB.Label.MidX
        txtAnnotProbNotAandBMidX.Text = Bayes.ProbNotAandB.ConditionalMidX
        'txtAnnotProbNotAandBY.Text = Bayes.ProbNotAandB.Label.Y
        txtAnnotProbNotAandBY.Text = Bayes.ProbNotAandB.ConditionalY
        chkAnnotProbNotAandB.Checked = Bayes.ProbNotAandB.Label.Show

        'txtAnnotProbNotAandNotB.Text = Bayes.ProbNotAandNotB.Prefix
        txtAnnotProbNotAandNotB.Text = Bayes.ProbNotAandNotB.ConditionalPrefix
        txtAnnotProbNotAandNotB.Font = Bayes.ProbNotAandNotB.Label.Font
        txtAnnotProbNotAandNotB.ForeColor = Bayes.ProbNotAandNotB.Label.Color
        'txtAnnotProbNotAandNotBVal.Text = Bayes.ProbNotAandNotB.FormattedValue
        txtAnnotProbNotAandNotBVal.Text = Bayes.ProbNotAandNotB.ConditionalFormattedValue
        'txtAnnotProbNotAandNotBX.Text = Bayes.ProbNotAandNotB.Label.X
        txtAnnotProbNotAandNotBX.Text = Bayes.ProbNotAandNotB.ConditionalX
        'txtAnnotProbNotAandNotBY.Text = Bayes.ProbNotAandNotB.Label.Y
        txtAnnotProbNotAandNotBY.Text = Bayes.ProbNotAandNotB.ConditionalY
        chkAnnotProbNotAandNotB.Checked = Bayes.ProbNotAandNotB.Label.Show

        'Samples
        'txtAnnotSampsA.Text = Bayes.SampsA.Prefix
        txtAnnotSampsA.Text = Bayes.SampsA.ConditionalPrefix
        txtAnnotSampsA.Font = Bayes.SampsA.Label.Font
        txtAnnotSampsA.ForeColor = Bayes.SampsA.Label.Color
        'txtAnnotSampsAVal.Text = SampString(Bayes.ProbA.Value * Bayes.SampleSize.Value)
        txtAnnotSampsAVal.Text = Bayes.SampsA.ConditionalFormattedValue
        'txtAnnotSampsAMidX.Text = Bayes.SampsA.Label.MidX
        txtAnnotSampsAMidX.Text = Bayes.SampsA.ConditionalMidX
        'txtAnnotSampsAY.Text = Bayes.SampsA.Label.Y
        txtAnnotSampsAY.Text = Bayes.SampsA.ConditionalY
        chkAnnotSampsA.Checked = Bayes.SampsA.Label.Show

        'txtAnnotSampsNotA.Text = Bayes.SampsNotA.Prefix
        txtAnnotSampsNotA.Text = Bayes.SampsNotA.ConditionalPrefix
        txtAnnotSampsNotA.Font = Bayes.SampsNotA.Label.Font
        txtAnnotSampsNotA.ForeColor = Bayes.SampsNotA.Label.Color
        'txtAnnotSampsNotAVal.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize.Value)
        txtAnnotSampsNotAVal.Text = Bayes.SampsNotA.ConditionalFormattedValue
        'txtAnnotSampsNotAMidX.Text = Bayes.SampsNotA.Label.MidX
        txtAnnotSampsNotAMidX.Text = Bayes.SampsNotA.ConditionalMidX
        'txtAnnotSampsNotAY.Text = Bayes.SampsNotA.Label.Y
        txtAnnotSampsNotAY.Text = Bayes.SampsNotA.ConditionalY
        chkAnnotSampsNotA.Checked = Bayes.SampsNotA.Label.Show

        'txtAnnotSampsB.Text = Bayes.SampsB.Prefix
        txtAnnotSampsB.Text = Bayes.SampsB.ConditionalPrefix
        txtAnnotSampsB.Font = Bayes.SampsB.Label.Font
        txtAnnotSampsB.ForeColor = Bayes.SampsB.Label.Color
        'txtAnnotSampsBVal.Text = SampString(Bayes.ProbB.Value * Bayes.SampleSize.Value)
        txtAnnotSampsBVal.Text = Bayes.SampsB.ConditionalFormattedValue
        'txtAnnotSampsBMidX.Text = Bayes.SampsB.Label.MidX
        txtAnnotSampsBMidX.Text = Bayes.SampsB.ConditionalMidX
        'txtAnnotSampsBY.Text = Bayes.SampsB.Label.Y
        txtAnnotSampsBY.Text = Bayes.SampsB.ConditionalY
        chkAnnotSampsB.Checked = Bayes.SampsB.Label.Show

        'txtAnnotSampsNotB.Text = Bayes.SampsNotB.Prefix
        txtAnnotSampsNotB.Text = Bayes.SampsNotB.ConditionalPrefix
        txtAnnotSampsNotB.Font = Bayes.SampsNotB.Label.Font
        txtAnnotSampsNotB.ForeColor = Bayes.SampsNotB.Label.Color
        'txtAnnotSampsNotBVal.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize.Value)
        txtAnnotSampsNotBVal.Text = Bayes.SampsNotB.ConditionalFormattedValue
        'txtAnnotSampsNotBMidX.Text = Bayes.SampsNotB.Label.MidX
        txtAnnotSampsNotBMidX.Text = Bayes.SampsNotB.ConditionalMidX
        'txtAnnotSampsNotBY.Text = Bayes.SampsNotB.Label.Y
        txtAnnotSampsNotBY.Text = Bayes.SampsNotB.ConditionalY
        chkAnnotSampsNotB.Checked = Bayes.SampsNotB.Label.Show

        'txtAnnotSampsAandB.Text = Bayes.SampsAandB.Prefix
        txtAnnotSampsAandB.Text = Bayes.SampsAandB.ConditionalPrefix
        txtAnnotSampsAandB.Font = Bayes.SampsAandB.Label.Font
        txtAnnotSampsAandB.ForeColor = Bayes.SampsAandB.Label.Color
        'txtAnnotSampsAandBVal.Text = SampString(Bayes.ProbAandB.Value * Bayes.SampleSize.Value)
        txtAnnotSampsAandBVal.Text = Bayes.SampsAandB.ConditionalFormattedValue
        'txtAnnotSampsAandBMidX.Text = Bayes.SampsAandB.Label.MidX
        txtAnnotSampsAandBMidX.Text = Bayes.SampsAandB.ConditionalMidX
        'txtAnnotSampsAandBY.Text = Bayes.SampsAandB.Label.Y
        txtAnnotSampsAandBY.Text = Bayes.SampsAandB.ConditionalY
        chkAnnotSampsAandB.Checked = Bayes.SampsAandB.Label.Show

        'txtAnnotSampsAandNotB.Text = Bayes.SampsAandNotB.Prefix
        txtAnnotSampsAandNotB.Text = Bayes.SampsAandNotB.ConditionalPrefix
        txtAnnotSampsAandNotB.Font = Bayes.SampsAandNotB.Label.Font
        txtAnnotSampsAandNotB.ForeColor = Bayes.SampsAandNotB.Label.Color
        'txtAnnotSampsAandNotBVal.Text = SampString((Bayes.ProbA.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize.Value)
        txtAnnotSampsAandNotBVal.Text = Bayes.SampsAandNotB.ConditionalFormattedValue
        'txtAnnotSampsAandNotBMidX.Text = Bayes.SampsAandNotB.Label.MidX
        txtAnnotSampsAandNotBMidX.Text = Bayes.SampsAandNotB.ConditionalMidX
        'txtAnnotSampsAandNotBY.Text = Bayes.SampsAandNotB.Label.Y
        txtAnnotSampsAandNotBY.Text = Bayes.SampsAandNotB.ConditionalY
        chkAnnotSampsAandNotB.Checked = Bayes.SampsAandNotB.Label.Show

        'txtAnnotSampsNotAandB.Text = Bayes.SampsNotAandB.Prefix
        txtAnnotSampsNotAandB.Text = Bayes.SampsNotAandB.ConditionalPrefix
        txtAnnotSampsNotAandB.Font = Bayes.SampsNotAandB.Label.Font
        txtAnnotSampsNotAandB.ForeColor = Bayes.SampsNotAandB.Label.Color
        'txtAnnotSampsNotAandBVal.Text = SampString((Bayes.ProbB.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize.Value)
        txtAnnotSampsNotAandBVal.Text = Bayes.SampsNotAandB.ConditionalFormattedValue
        'txtAnnotSampsNotAandBMidX.Text = Bayes.SampsNotAandB.Label.MidX
        txtAnnotSampsNotAandBMidX.Text = Bayes.SampsNotAandB.ConditionalMidX
        'txtAnnotSampsNotAandBY.Text = Bayes.SampsNotAandB.Label.Y
        txtAnnotSampsNotAandBY.Text = Bayes.SampsNotAandB.ConditionalY
        chkAnnotSampsNotAandB.Checked = Bayes.SampsNotAandB.Label.Show

        'txtAnnotSampsNotAandNotB.Text = Bayes.SampsNotAandNotB.ConditionalPrefix
        txtAnnotSampsNotAandNotB.Text = Bayes.SampsNotAandNotB.ConditionalPrefix
        txtAnnotSampsNotAandNotB.Font = Bayes.SampsNotAandNotB.Label.Font
        txtAnnotSampsNotAandNotB.ForeColor = Bayes.SampsNotAandNotB.Label.Color
        'txtAnnotSampsNotAandNotBVal.Text = SampString((1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) * Bayes.SampleSize.Value)
        txtAnnotSampsNotAandNotBVal.Text = Bayes.SampsNotAandNotB.ConditionalFormattedValue
        'txtAnnotSampsNotAandNotBX.Text = Bayes.SampsNotAandNotB.Label.X
        txtAnnotSampsNotAandNotBX.Text = Bayes.SampsNotAandNotB.ConditionalX
        'txtAnnotSampsNotAandNotBY.Text = Bayes.SampsNotAandNotB.Label.Y
        txtAnnotSampsNotAandNotBY.Text = Bayes.SampsNotAandNotB.ConditionalY
        chkAnnotSampsNotAandNotB.Checked = Bayes.SampsNotAandNotB.Label.Show

        'txtAnnotSampsSize.Text = Bayes.SampsSizeText
        txtAnnotSampsSize.Text = Bayes.AnnotSampleSize.Text
        txtAnnotSampsSize.Font = Bayes.AnnotSampleSize.Font
        txtAnnotSampsSize.ForeColor = Bayes.AnnotSampleSize.Color
        txtAnnotSampsSizeVal.Text = SampString(Bayes.SampleSize.Value)
        txtAnnotSampsSizeMidX.Text = Bayes.AnnotSampleSize.X
        txtAnnotSampsSizeBaseY.Text = Bayes.AnnotSampleSize.BaseY
        chkAnnotSampsSize.Checked = Bayes.AnnotSampleSize.Show

    End Sub

    Private Sub UpdateCategoryInfo()
        'Update the Sample Count and Probability in each event category.

        txtSampleSize_Cat.Text = Format(Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)

        txtASamps.Text = Format(Bayes.ProbA.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtNotASamps.Text = Format((1 - Bayes.ProbA.Value) * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtBSamps.Text = Format(Bayes.ProbB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtNotBSamps.Text = Format((1 - Bayes.ProbB.Value) * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtAandBSamps.Text = Format((Bayes.ProbAandB.Value) * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtAandNotBSamps.Text = Format((Bayes.ProbA.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtNotAandBSamps.Text = Format((Bayes.ProbB.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtNotAandNotBSamps.Text = Format((1 - (Bayes.ProbA.Value + Bayes.ProbB.Value - Bayes.ProbAandB.Value)) * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)

        chkShowConditionLabel.Checked = Bayes.AnnotCondition.Show

        txtConditionLabel.Font = Bayes.AnnotCondition.Font
        txtConditionLabel.ForeColor = Bayes.AnnotCondition.Color

        'Select Case Bayes.Condition
        '    Case "None"
        '        rbConditionNone.Checked = True
        '        'txtConditionLabel.Text = Bayes.AnnotContraintLabelNone
        '        txtConditionLabel.Text = Bayes.AnnotCondition.Text
        '    Case "EventATrue"
        '        rbConditionA.Checked = True
        '        'txtConditionLabel.Text = Bayes.AnnotConditionLabelATrue
        '        txtConditionLabel.Text = Bayes.AnnotCondition.GivenA.Text
        '    Case "EventAFalse"
        '        rbConditionNotA.Checked = True
        '        'txtConditionLabel.Text = Bayes.AnnotConditionLabelAFalse
        '        txtConditionLabel.Text = Bayes.AnnotCondition.GivenNotA.Text
        '    Case "EventBTrue"
        '        rbConditionB.Checked = True
        '        'txtConditionLabel.Text = Bayes.AnnotConditionLabelBTrue
        '        txtConditionLabel.Text = Bayes.AnnotCondition.GivenB.Text
        '    Case "EventBFalse"
        '        rbConditionNotB.Checked = True
        '        'txtConditionLabel.Text = Bayes.AnnotConditionLabelBFalse
        '        txtConditionLabel.Text = Bayes.AnnotCondition.GivenNotB.Text
        '    Case Else
        '        Message.AddWarning("Unknown condition " & Bayes.Condition & vbCrLf)
        '        rbConditionNone.Checked = True
        '        'txtConditionLabel.Text = Bayes.AnnotContraintLabelNone
        '        txtConditionLabel.Text = Bayes.AnnotCondition.Text
        'End Select
        txtConditionLabel.Text = Bayes.AnnotCondition.Text

        'txtConditionLabelX.Text = Bayes.AnnotConditionLabelX
        txtConditionLabelX.Text = Bayes.AnnotCondition.X
        'txtConditionLabelY.Text = Bayes.AnnotConditionLabelY
        txtConditionLabelY.Text = Bayes.AnnotCondition.Y


        If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
            txtAProb.Text = Format(Bayes.ProbA.Value, Bayes.Settings.DecimalFormat)
            txtNotAProb.Text = Format((1 - Bayes.ProbA.Value), Bayes.Settings.DecimalFormat)
            txtBProb.Text = Format(Bayes.ProbB.Value, Bayes.Settings.DecimalFormat)
            txtNotBProb.Text = Format((1 - Bayes.ProbB.Value), Bayes.Settings.DecimalFormat)
            txtAandBProb.Text = Format((Bayes.ProbAandB.Value), Bayes.Settings.DecimalFormat)
            txtAandNotBProb.Text = Format((Bayes.ProbA.Value - Bayes.ProbAandB.Value), Bayes.Settings.DecimalFormat)
            txtNotAandBProb.Text = Format((Bayes.ProbB.Value - Bayes.ProbAandB.Value), Bayes.Settings.DecimalFormat)
            txtNotAandNotBProb.Text = Format((1 - (Bayes.ProbA.Value + Bayes.ProbB.Value - Bayes.ProbAandB.Value)), Bayes.Settings.DecimalFormat)

            'lblApct.Text = " "
            'lblNotApct.Text = " "
            'lblBpct.Text = " "
            'lblNotBpct.Text = " "
            'lblAandBpct.Text = " "
            'lblAandNotBpct.Text = " "
            'lblNotAandBpct.Text = " "
            'lblNotAandNotBpct.Text = " "

        ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
            txtAProb.Text = Format(Bayes.ProbA.Value * 100, Bayes.Settings.PercentFormat) & "%"
            txtNotAProb.Text = Format((1 - Bayes.ProbA.Value) * 100, Bayes.Settings.PercentFormat) & "%"
            txtBProb.Text = Format(Bayes.ProbB.Value * 100, Bayes.Settings.PercentFormat) & "%"
            txtNotBProb.Text = Format((1 - Bayes.ProbB.Value) * 100, Bayes.Settings.PercentFormat) & "%"
            txtAandBProb.Text = Format((Bayes.ProbAandB.Value) * 100, Bayes.Settings.PercentFormat) & "%"
            txtAandNotBProb.Text = Format((Bayes.ProbA.Value - Bayes.ProbAandB.Value) * 100, Bayes.Settings.PercentFormat) & "%"
            txtNotAandBProb.Text = Format((Bayes.ProbB.Value - Bayes.ProbAandB.Value) * 100, Bayes.Settings.PercentFormat) & "%"
            txtNotAandNotBProb.Text = Format((1 - (Bayes.ProbA.Value + Bayes.ProbB.Value - Bayes.ProbAandB.Value)) * 100, Bayes.Settings.PercentFormat) & "%"

            'lblApct.Text = "%"
            'lblNotApct.Text = "%"
            'lblBpct.Text = "%"
            'lblNotBpct.Text = "%"
            'lblAandBpct.Text = "%"
            'lblAandNotBpct.Text = "%"
            'lblNotAandBpct.Text = "%"
            'lblNotAandNotBpct.Text = "%"
        Else
            Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
            Message.AddWarning("Percent probabilities are displayed for each event category." & vbCrLf)

        End If






    End Sub

    Private Sub UpdateDisplayTab()
        txtLineColor.BackColor = Bayes.Diagram.LineColor
        txtLineThickness.Text = Bayes.Diagram.LineThickness
        txtBoldLineThickness.Text = Bayes.Diagram.BoldLineThickness
        chkBoldLine.Checked = Bayes.Diagram.BoldLine
        txtFillColor.BackColor = Bayes.Diagram.FillColor
        txtWidth.Text = Bayes.Diagram.Width
        txtHeight.Text = Bayes.Diagram.Height

        txtEventALineColor.BackColor = Bayes.EventA.LineColor
        txtEventALineThickness.Text = Bayes.EventA.LineThickness
        txtEventABoldLineThickness.Text = Bayes.EventA.BoldLineThickness
        chkEventABoldLine.Checked = Bayes.EventA.BoldLine
        txtEventAFillColor.BackColor = Bayes.EventA.FillColor
        cmbEventAShape.SelectedIndex = cmbEventAShape.FindStringExact(Bayes.EventA.Shape)
        'txtEventAXMin.Text = Bayes.EventA.Unscaled.XMin
        'txtEventAXMax.Text = Bayes.EventA.Unscaled.XMax
        'txtEventAYMin.Text = Bayes.EventA.Unscaled.YMin
        'txtEventAYMax.Text = Bayes.EventA.Unscaled.YMax
        txtEventAXMin.Text = Bayes.EventA.XMin
        txtEventAXMax.Text = Bayes.EventA.XMax
        txtEventAYMin.Text = Bayes.EventA.YMin
        txtEventAYMax.Text = Bayes.EventA.YMax

        txtEventBLineColor.BackColor = Bayes.EventB.LineColor
        txtEventBLineThickness.Text = Bayes.EventB.LineThickness
        txtEventBBoldLineThickness.Text = Bayes.EventB.BoldLineThickness
        chkEventBBoldLine.Checked = Bayes.EventB.BoldLine
        txtEventBFillColor.BackColor = Bayes.EventB.FillColor
        cmbEventBShape.SelectedIndex = cmbEventBShape.FindStringExact(Bayes.EventB.Shape)
        'txtEventBXMin.Text = Bayes.EventB.Unscaled.XMin
        'txtEventBXMax.Text = Bayes.EventB.Unscaled.XMax
        'txtEventBYMin.Text = Bayes.EventB.Unscaled.YMin
        'txtEventBYMax.Text = Bayes.EventB.Unscaled.YMax
        txtEventBXMin.Text = Bayes.EventB.XMin
        txtEventBXMax.Text = Bayes.EventB.XMax
        txtEventBYMin.Text = Bayes.EventB.YMin
        txtEventBYMax.Text = Bayes.EventB.YMax

        txtEventAandBFillColor.BackColor = Bayes.EventAandBFillColor
    End Sub

    Private Sub UpdateAnnotationTab()
        'Labels:
        txtAnnotTitle.Text = Bayes.AnnotTitle.Text
        txtAnnotTitle.Font = Bayes.AnnotTitle.Font
        txtAnnotTitle.ForeColor = Bayes.AnnotTitle.Color
        'txtAnnotTitleMidX.Text = Bayes.AnnotTitle.X
        txtAnnotTitleMidX.Text = Bayes.AnnotTitle.MidX
        txtAnnotTitleY.Text = Bayes.AnnotTitle.Y

        txtAnnotDescr.Text = Bayes.AnnotDescr.Text
        txtAnnotDescr.Font = Bayes.AnnotDescr.Font
        txtAnnotDescr.ForeColor = Bayes.AnnotDescr.Color
        'txtAnnotDescrMidX.Text = Bayes.AnnotDescr.X
        txtAnnotDescrMidX.Text = Bayes.AnnotDescr.MidX
        txtAnnotDescrY.Text = Bayes.AnnotDescr.Y

        'txtAnnotEventALabel.Text = Bayes.AnnotEventA.Unscaled.Text
        'txtAnnotEventALabel.Font = Bayes.AnnotEventA.Unscaled.Font
        'txtAnnotEventALabel.ForeColor = Bayes.AnnotEventA.Unscaled.Color
        'txtAnnotEventAX.Text = Bayes.AnnotEventA.Unscaled.X
        'txtAnnotEventAY.Text = Bayes.AnnotEventA.Unscaled.Y
        txtAnnotEventALabel.Text = Bayes.AnnotEventA.Text
        txtAnnotEventALabel.Font = Bayes.AnnotEventA.Font
        txtAnnotEventALabel.ForeColor = Bayes.AnnotEventA.Color
        'txtAnnotEventAMidX.Text = Bayes.AnnotEventA.X
        txtAnnotEventAMidX.Text = Bayes.AnnotEventA.MidX
        txtAnnotEventAY.Text = Bayes.AnnotEventA.Y

        txtAnnotEventBLabel.Text = Bayes.AnnotEventB.Text
        txtAnnotEventBLabel.Font = Bayes.AnnotEventB.Font
        txtAnnotEventBLabel.ForeColor = Bayes.AnnotEventB.Color
        'txtAnnotEventBMidX.Text = Bayes.AnnotEventB.X
        txtAnnotEventBMidX.Text = Bayes.AnnotEventB.MidX
        txtAnnotEventBY.Text = Bayes.AnnotEventB.Y
    End Sub

    Private Sub cmbInputInfo_Click(sender As Object, e As EventArgs) Handles cmbInputInfo.Click

    End Sub

    Private Sub cmbInputInfo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbInputInfo.SelectedIndexChanged
        'The type of Bayes Model Input Inpformation has been changed.

        Select Case cmbInputInfo.SelectedItem.ToString
            Case "P(B|A), P(A), P(B)"
                Label14.Text = "Bayes Theorem: P(A|B) = P(B|A) x P(A) / P(B)"

                txtSampsAandB.ReadOnly = True
                txtSampsNotAandB.ReadOnly = True
                txtSampsNotAandNotB.ReadOnly = True
                txtSampsAandNotB.ReadOnly = True
                txtSampleSize.ReadOnly = True

                txtProbA.ReadOnly = False
                txtProbB.ReadOnly = False
                txtProbBgivenNotA.ReadOnly = True
                txtProbBgivenA.ReadOnly = False
                Bayes.InputInfoType = "P(B|A), P(A), P(B)"

            Case "P(B|A), P(A), P(B|NotA)"
                Label14.Text = "Bayes Theorem: P(A|B) = P(B|A) x P(A) / (P(B|A) x P(A) + P(B|NotA) x P(NotA))"

                txtSampsAandB.ReadOnly = True
                txtSampsNotAandB.ReadOnly = True
                txtSampsNotAandNotB.ReadOnly = True
                txtSampsAandNotB.ReadOnly = True
                txtSampleSize.ReadOnly = True

                txtProbA.ReadOnly = False
                txtProbB.ReadOnly = False
                txtProbBgivenNotA.ReadOnly = False
                txtProbBgivenA.ReadOnly = True
                Bayes.InputInfoType = "P(B|A), P(A), P(B|NotA)"

            Case "Sample Counts (TP, TN, FP, FN)"
                Label14.Text = "Bayes Theorem: P(A|B) = P(B|A) x P(A) / P(B)"

                txtSampsAandB.ReadOnly = False
                txtSampsNotAandB.ReadOnly = False
                txtSampsNotAandNotB.ReadOnly = False
                txtSampsAandNotB.ReadOnly = False
                txtSampleSize.ReadOnly = True

                txtProbA.ReadOnly = True
                txtProbB.ReadOnly = True
                txtProbBgivenNotA.ReadOnly = True
                txtProbBgivenA.ReadOnly = True

                Bayes.InputInfoType = "Sample Counts"

            Case "Sample Size"
                Label14.Text = "Bayes Theorem: P(A|B) = P(B|A) x P(A) / P(B)"

                txtSampsAandB.ReadOnly = True
                txtSampsNotAandB.ReadOnly = True
                txtSampsNotAandNotB.ReadOnly = True
                txtSampsAandNotB.ReadOnly = True
                txtSampleSize.ReadOnly = False

                txtProbA.ReadOnly = True
                txtProbB.ReadOnly = True
                txtProbBgivenNotA.ReadOnly = True
                txtProbBgivenA.ReadOnly = True

                Bayes.InputInfoType = "Sample Size"

            Case Else

        End Select

        'If cmbInputInfo.SelectedItem.ToString = "P(B|A), P(A), P(B)" Then
        '    Label14.Text = "Bayes Theorem: P(A|B) = P(B|A) x P(A) / P(B)"
        '    txtProbBgivenNotA.ReadOnly = True
        '    txtProbBgivenA.ReadOnly = False
        '    Bayes.InputInfoType = "P(B|A), P(A), P(B)"
        '    'ElseIf cmbInputInfo.SelectedItem.ToString = "P(B|A), P(A), P(B|~A)" Then
        'ElseIf cmbInputInfo.SelectedItem.ToString = "P(B|A), P(A), P(B|NotA)" Then
        '    'Label14.Text = "Bayes Theorem: P(A|B) = P(B|A) x P(A) / (P(B|A) x P(A) + P(B|~A) x P(~A))"
        '    Label14.Text = "Bayes Theorem: P(A|B) = P(B|A) x P(A) / (P(B|A) x P(A) + P(B|NotA) x P(NotA))"
        '    txtProbBgivenNotA.ReadOnly = False
        '    txtProbBgivenA.ReadOnly = True
        '    Bayes.InputInfoType = "P(B|A), P(A), P(B|NotA)"
        'Else
        '    Message.AddWarning("Unknown input information: " & cmbInputInfo.SelectedItem.ToString & vbCrLf)
        'End If

    End Sub

    Private Sub DrawEventATree()
        'Draw the probability tree for Event A

        trvEventA.Nodes.Clear()

        'Dim myNode1 As TreeNode = New TreeNode("Bayes Model") 'Text, ImageIndex, SelectedImageIndex
        'trvEventA.Nodes.Add(myNode1)

        Dim NodeText As String

        Dim tnc As TreeNodeCollection = trvEventA.Nodes
        'Dim TopNode As TreeNode = tnc.Add("Bayes Model")
        'Dim BayesModel As TreeNode = tnc.Add("Bayes Model", "Bayes Model", 0, 1) 'Key, Text, Image index, Selected image index.
        NodeText = "Bayes Model - " & Bayes.Label
        Dim BayesModel As TreeNode = tnc.Add("Bayes Model", NodeText, 0, 1) 'Key, Text, Image index, Selected image index.
        BayesModel.EnsureVisible()

        'Dim EventA As TreeNode = BayesModel.Nodes.Add("Event A", "Event A", 2, 3)
        NodeText = "Event A - " & Bayes.EventA.Name
        Dim EventA As TreeNode = BayesModel.Nodes.Add("Event A", NodeText, 2, 3)
        EventA.EnsureVisible()

        'Dim EventATrue As TreeNode = EventA.Nodes.Add("Event A True", "True", 6, 7)
        NodeText = "[" & Bayes.ProbA.FormattedValue & "] " & "True"
        Dim EventATrue As TreeNode = EventA.Nodes.Add("Event A True", NodeText, 6, 7)
        EventATrue.EnsureVisible()

        'Dim EventATrueEventB As TreeNode = EventATrue.Nodes.Add("Event A True - Event B", "Event B", 4, 5)
        NodeText = "Event B - " & Bayes.EventB.Name
        Dim EventATrueEventB As TreeNode = EventATrue.Nodes.Add("Event A True - Event B", NodeText, 4, 5)
        EventATrueEventB.EnsureVisible()

        'Dim EventATrueEventBTrue As TreeNode = EventATrueEventB.Nodes.Add("Event A True - Event B True", "True", 10, 11)
        NodeText = "[" & Bayes.ProbB.GivenA.FormattedValue & "] " & "True : " & Bayes.ProbAandB.FormattedValue
        'Dim EventATrueEventBTrue As TreeNode = EventATrueEventB.Nodes.Add("Event A True - Event B True", NodeText, 10, 11)
        Dim EventATrueEventBTrue As TreeNode = EventATrueEventB.Nodes.Add("Event A True and Event B True", NodeText, 10, 11)
        EventATrueEventBTrue.EnsureVisible()

        'Dim EventATrueEventBFalse As TreeNode = EventATrueEventB.Nodes.Add("Event A True - Event B False", "False", 12, 13)
        NodeText = "[" & Bayes.ProbNotB.GivenA.FormattedValue & "] " & "False : " & Bayes.ProbAandNotB.FormattedValue
        'Dim EventATrueEventBFalse As TreeNode = EventATrueEventB.Nodes.Add("Event A True - Event B False", NodeText, 12, 13)
        Dim EventATrueEventBFalse As TreeNode = EventATrueEventB.Nodes.Add("Event A True and Event B False", NodeText, 12, 13)
        EventATrueEventBFalse.EnsureVisible()

        'Dim EventAFalse As TreeNode = EventA.Nodes.Add("Event A False", "False", 8, 9)
        NodeText = "[" & Bayes.ProbNotA.FormattedValue & "] " & "False"
        Dim EventAFalse As TreeNode = EventA.Nodes.Add("Event A False", NodeText, 8, 9)
        EventAFalse.EnsureVisible()

        'Dim EventAFalseEventB As TreeNode = EventAFalse.Nodes.Add("Event A False - Event B", "Event B", 4, 5)
        NodeText = "Event B - " & Bayes.EventB.Name
        Dim EventAFalseEventB As TreeNode = EventAFalse.Nodes.Add("Event A False - Event B", NodeText, 4, 5)
        EventATrueEventB.EnsureVisible()

        'Dim EventAFalseEventBTrue As TreeNode = EventAFalseEventB.Nodes.Add("Event A False - Event B True", "True", 10, 11)
        NodeText = "[" & Bayes.ProbB.GivenNotA.FormattedValue & "] " & "True : " & Bayes.ProbNotAandB.FormattedValue
        'Dim EventAFalseEventBTrue As TreeNode = EventAFalseEventB.Nodes.Add("Event A False - Event B True", NodeText, 10, 11)
        Dim EventAFalseEventBTrue As TreeNode = EventAFalseEventB.Nodes.Add("Event A False and Event B True", NodeText, 10, 11)
        EventAFalseEventBTrue.EnsureVisible()

        'Dim EventAFalseEventBFalse As TreeNode = EventAFalseEventB.Nodes.Add("Event A False - Event B False", "False", 12, 13)
        NodeText = "[" & Bayes.ProbNotB.GivenNotA.FormattedValue & "] " & "False : " & Bayes.ProbNotAandNotB.FormattedValue
        'Dim EventAFalseEventBFalse As TreeNode = EventAFalseEventB.Nodes.Add("Event A False - Event B False", NodeText, 12, 13)
        Dim EventAFalseEventBFalse As TreeNode = EventAFalseEventB.Nodes.Add("Event A False and Event B False", NodeText, 12, 13)
        EventAFalseEventBFalse.EnsureVisible()

    End Sub
    Private Sub DrawEventATree_Old()
        'Draw the probability tree for Event A

        trvEventA.Nodes.Clear()

        'Dim myNode1 As TreeNode = New TreeNode("Bayes Model") 'Text, ImageIndex, SelectedImageIndex
        'trvEventA.Nodes.Add(myNode1)

        Dim tnc As TreeNodeCollection = trvEventA.Nodes
        'Dim TopNode As TreeNode = tnc.Add("Bayes Model")
        Dim BayesModel As TreeNode = tnc.Add("Bayes Model", "Bayes Model", 0, 1)
        BayesModel.EnsureVisible()
        Dim EventA As TreeNode = BayesModel.Nodes.Add("Event A", "Event A", 2, 3)
        EventA.EnsureVisible()
        Dim EventATrue As TreeNode = EventA.Nodes.Add("Event A True", "True", 6, 7)
        EventATrue.EnsureVisible()
        Dim EventATrueEventB As TreeNode = EventATrue.Nodes.Add("Event A True - Event B", "Event B", 4, 5)
        EventATrueEventB.EnsureVisible()
        Dim EventATrueEventBTrue As TreeNode = EventATrueEventB.Nodes.Add("Event A True - Event B True", "True", 14, 15)
        EventATrueEventBTrue.EnsureVisible()
        Dim EventATrueEventBFalse As TreeNode = EventATrueEventB.Nodes.Add("Event A True - Event B False", "False", 16, 17)
        EventATrueEventBFalse.EnsureVisible()
        Dim EventAFalse As TreeNode = EventA.Nodes.Add("Event A False", "False", 8, 9)
        EventAFalse.EnsureVisible()
        Dim EventAFalseEventB As TreeNode = EventAFalse.Nodes.Add("Event A False - Event B", "Event B", 4, 5)
        EventATrueEventB.EnsureVisible()
        Dim EventAFalseEventBTrue As TreeNode = EventAFalseEventB.Nodes.Add("Event A False - Event B True", "True", 18, 19)
        EventAFalseEventBTrue.EnsureVisible()
        Dim EventAFalseEventBFalse As TreeNode = EventAFalseEventB.Nodes.Add("Event A False - Event B False", "False", 20, 21)
        EventAFalseEventBFalse.EnsureVisible()

    End Sub

    Private Sub DrawEventBTree()
        'Draw the probability tree for Event B

        trvEventB.Nodes.Clear()

        Dim NodeText As String

        Dim tnc As TreeNodeCollection = trvEventB.Nodes
        'Dim BayesModel As TreeNode = tnc.Add("Bayes Model", "Bayes Model", 0, 1)
        NodeText = "Bayes Model - " & Bayes.Label
        Dim BayesModel As TreeNode = tnc.Add("Bayes Model", NodeText, 0, 1)
        BayesModel.EnsureVisible()

        'Dim EventB As TreeNode = BayesModel.Nodes.Add("Event B", "Event B", 4, 5)
        NodeText = "Event B - " & Bayes.EventB.Name
        Dim EventB As TreeNode = BayesModel.Nodes.Add("Event B", NodeText, 4, 5)
        EventB.EnsureVisible()

        'Dim EventBTrue As TreeNode = EventB.Nodes.Add("Event B True", "True", 10, 11)
        NodeText = "[" & Bayes.ProbB.FormattedValue & "] " & "True"
        Dim EventBTrue As TreeNode = EventB.Nodes.Add("Event B True", NodeText, 10, 11)
        EventBTrue.EnsureVisible()

        'Dim EventBTrueEventA As TreeNode = EventBTrue.Nodes.Add("Event B True - Event A", "Event A", 2, 3)
        NodeText = "Event A - " & Bayes.EventA.Name
        Dim EventBTrueEventA As TreeNode = EventBTrue.Nodes.Add("Event B True - Event A", NodeText, 2, 3)
        EventBTrueEventA.EnsureVisible()

        'Dim EventBTrueEventATrue As TreeNode = EventBTrueEventA.Nodes.Add("Event B True - Event A True", "True", 6, 7)
        NodeText = "[" & Bayes.ProbA.GivenB.FormattedValue & "] " & "True : " & Bayes.ProbAandB.FormattedValue
        'Dim EventBTrueEventATrue As TreeNode = EventBTrueEventA.Nodes.Add("Event B True - Event A True", NodeText, 6, 7)
        Dim EventBTrueEventATrue As TreeNode = EventBTrueEventA.Nodes.Add("Event B True and Event A True", NodeText, 6, 7)
        EventBTrueEventATrue.EnsureVisible()

        'Dim EventBTrueEventAFalse As TreeNode = EventBTrueEventA.Nodes.Add("Event B True - Event A False", "False", 8, 9)
        NodeText = "[" & Bayes.ProbNotA.GivenB.FormattedValue & "] " & "False : " & Bayes.ProbNotAandB.FormattedValue
        'Dim EventBTrueEventAFalse As TreeNode = EventBTrueEventA.Nodes.Add("Event B True - Event A False", NodeText, 8, 9)
        Dim EventBTrueEventAFalse As TreeNode = EventBTrueEventA.Nodes.Add("Event B True and Event A False", NodeText, 8, 9)
        EventBTrueEventAFalse.EnsureVisible()

        'Dim EventBFalse As TreeNode = EventB.Nodes.Add("Event B False", "False", 12, 13)
        NodeText = "[" & Bayes.ProbNotB.FormattedValue & "] " & "False"
        Dim EventBFalse As TreeNode = EventB.Nodes.Add("Event B False", NodeText, 12, 13)
        EventBFalse.EnsureVisible()

        'Dim EventBFalseEventA As TreeNode = EventBFalse.Nodes.Add("Event B False - Event A", "Event A", 2, 3)
        NodeText = "Event A - " & Bayes.EventA.Name
        Dim EventBFalseEventA As TreeNode = EventBFalse.Nodes.Add("Event B False - Event A", NodeText, 2, 3)
        EventBTrueEventA.EnsureVisible()

        'Dim EventBFalseEventATrue As TreeNode = EventBFalseEventA.Nodes.Add("Event B False - Event A True", "True", 6, 7)
        NodeText = "[" & Bayes.ProbA.GivenNotB.FormattedValue & "] " & "True : " & Bayes.ProbAandNotB.FormattedValue
        'Dim EventBFalseEventATrue As TreeNode = EventBFalseEventA.Nodes.Add("Event B False - Event A True", NodeText, 6, 7)
        Dim EventBFalseEventATrue As TreeNode = EventBFalseEventA.Nodes.Add("Event B False and Event A True", NodeText, 6, 7)
        EventBFalseEventATrue.EnsureVisible()

        'Dim EventBFalseEventAFalse As TreeNode = EventBFalseEventA.Nodes.Add("Event B False - Event A False", "False", 8, 9)
        NodeText = "[" & Bayes.ProbNotA.GivenNotB.FormattedValue & "] " & "False : " & Bayes.ProbNotAandNotB.FormattedValue
        'Dim EventBFalseEventAFalse As TreeNode = EventBFalseEventA.Nodes.Add("Event B False - Event A False", NodeText, 8, 9)
        Dim EventBFalseEventAFalse As TreeNode = EventBFalseEventA.Nodes.Add("Event B False and Event A False", NodeText, 8, 9)
        EventBFalseEventAFalse.EnsureVisible()
    End Sub

    Private Sub txtModelName_LostFocus(sender As Object, e As EventArgs) Handles txtModelName.LostFocus
        'The Bayes model 
        Bayes.Name = txtModelName.Text.Trim
        Bayes.Modified = True
    End Sub

    Private Sub txtLabel_TextChanged(sender As Object, e As EventArgs) Handles txtLabel.TextChanged

    End Sub

    Private Sub txtLabel_LostFocus(sender As Object, e As EventArgs) Handles txtLabel.LostFocus
        Bayes.Label = txtLabel.Text.Trim
        Bayes.AnnotTitle.Text = txtLabel.Text.Trim
        Bayes.AnnotTitle.ScaleA.UpdateLabelPosition()
        Bayes.AnnotTitle.ScaleB.UpdateLabelPosition()
        Bayes.AnnotTitle.ScaleAB.UpdateLabelPosition()
        Bayes.Modified = True
        DrawDiagram()
    End Sub

    Private Sub txtDescription_TextChanged(sender As Object, e As EventArgs) Handles txtDescription.TextChanged

    End Sub

    Private Sub txtDescription_LostFocus(sender As Object, e As EventArgs) Handles txtDescription.LostFocus
        Bayes.Description = txtDescription.Text.Trim
        Bayes.AnnotDescr.Text = txtDescription.Text.Trim
        Bayes.AnnotDescr.ScaleA.UpdateLabelPosition()
        Bayes.AnnotDescr.ScaleB.UpdateLabelPosition()
        Bayes.AnnotDescr.ScaleAB.UpdateLabelPosition()
        Bayes.Modified = True
        DrawDiagram()
    End Sub

    Private Sub txtNotes_TextChanged(sender As Object, e As EventArgs) Handles txtNotes.TextChanged

    End Sub

    Private Sub txtNotes_LostFocus(sender As Object, e As EventArgs) Handles txtNotes.LostFocus
        Bayes.Notes = txtNotes.Text.Trim
        Bayes.Modified = True
    End Sub

    Private Sub trvEventA_GotFocus(sender As Object, e As EventArgs) Handles trvEventA.GotFocus
        'DrawDiagram()
        'Bayes.Settings.Condition = "None"
        trvEventB.SelectedNode = Nothing
    End Sub

    Private Sub trvEventA_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvEventA.AfterSelect
        txtEventANodeInfo.Text = e.Node.Name
        Select Case e.Node.Name
            Case "Bayes Model"
                txtEventANodeInfo.Text = "Node name: Bayes Model"
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionNone.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "None"
                    rbConditionNone.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True
            Case "Event A"
                txtEventANodeInfo.Text = "Node name: Event A"
                Bayes.EventA.BoldLine = True
                Bayes.EventB.BoldLine = False
                If rbConditionNone.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "None"
                    rbConditionNone.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True
            Case "Event A True"
                txtEventANodeInfo.Text = "Node name: Event A True" & vbCrLf & Bayes.ProbA.Label.Text
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionA.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventATrue"
                    rbConditionA.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True
            Case "Event A True - Event B"
                txtEventANodeInfo.Text = "Node name: Event A True - Event B"
                'Case "Event A True - Event B True"
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = True
                If rbConditionA.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventATrue"
                    rbConditionA.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True

            Case "Event A True and Event B True"
                'txtEventANodeInfo.Text = "Node name: Event A True - Event B True" & vbCrLf & Bayes.ProbB.GivenA.Label.Text & vbCrLf & Bayes.ProbAandB.Label.Text
                txtEventANodeInfo.Text = "Node name: Event A True and Event B True" & vbCrLf & Bayes.ProbB.GivenA.Label.Text & vbCrLf & Bayes.ProbAandB.Label.Text
                'Case "Event A True - Event B False"
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionA.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventATrue"
                    rbConditionA.Checked = True
                End If
                If rbB.Checked = False Then rbB.Checked = True

            Case "Event A True and Event B False"
                'txtEventANodeInfo.Text = "Node name: Event A True - Event B False" & vbCrLf & Bayes.ProbNotB.GivenA.Label.Text & vbCrLf & Bayes.ProbAandNotB.Label.Text
                txtEventANodeInfo.Text = "Node name: Event A True and Event B False" & vbCrLf & Bayes.ProbNotB.GivenA.Label.Text & vbCrLf & Bayes.ProbAandNotB.Label.Text
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionA.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventATrue"
                    rbConditionA.Checked = True
                End If
                If rbNotB.Checked = False Then rbNotB.Checked = True
            Case "Event A False"
                txtEventANodeInfo.Text = "Node name: Event A False" & vbCrLf & Bayes.ProbNotA.Label.Text
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionNotA.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventAFalse"
                    rbConditionNotA.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True
            Case "Event A False - Event B"
                txtEventANodeInfo.Text = "Node name: Event A False - Event B"
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = True
                If rbConditionNotA.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventAFalse"
                    rbConditionNotA.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True
                'Case "Event A False - Event B True"
            Case "Event A False and Event B True"
                'txtEventANodeInfo.Text = "Node name: Event A False - Event B True" & vbCrLf & Bayes.ProbB.GivenNotA.Label.Text & vbCrLf & Bayes.ProbNotAandB.Label.Text
                txtEventANodeInfo.Text = "Node name: Event A False and Event B True" & vbCrLf & Bayes.ProbB.GivenNotA.Label.Text & vbCrLf & Bayes.ProbNotAandB.Label.Text
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionNotA.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventAFalse"
                    rbConditionNotA.Checked = True
                End If
                If rbB.Checked = False Then rbB.Checked = True
                'Case "Event A False - Event B False"
            Case "Event A False and Event B False"
                'txtEventANodeInfo.Text = "Node name: Event A False - Event B False" & vbCrLf & Bayes.ProbNotB.GivenNotA.Label.Text & vbCrLf & Bayes.ProbNotAandNotB.Label.Text
                txtEventANodeInfo.Text = "Node name: Event A False and Event B False" & vbCrLf & Bayes.ProbNotB.GivenNotA.Label.Text & vbCrLf & Bayes.ProbNotAandNotB.Label.Text
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionNotA.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventAFalse"
                    rbConditionNotA.Checked = True
                End If
                If rbNotB.Checked = False Then rbNotB.Checked = True
            Case Else
                Message.AddWarning("Unknown node name: " & e.Node.Name & vbCrLf)
                txtEventANodeInfo.Text = "Unknown node name: " & e.Node.Name
        End Select
    End Sub

    Private Sub trvEventB_GotFocus(sender As Object, e As EventArgs) Handles trvEventB.GotFocus
        'DrawDiagram()
        'Bayes.Settings.Condition = "None"
        trvEventA.SelectedNode = Nothing
    End Sub

    Private Sub trvEventB_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles trvEventB.AfterSelect
        txtEventBNodeInfo.Text = e.Node.Name

        Select Case e.Node.Name
            Case "Bayes Model"
                txtEventBNodeInfo.Text = "Node name: Bayes Model"
                'rbConditionNone.Checked = True
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionNone.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "None"
                    rbConditionNone.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True

            Case "Event B"
                txtEventBNodeInfo.Text = "Node name: Event B"
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = True
                If rbConditionNone.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "None"
                    rbConditionNone.Checked = True
                End If

            Case "Event B True"
                txtEventBNodeInfo.Text = "Node name: Event B True" & vbCrLf & Bayes.ProbB.Label.Text
                'rbConditionB.Checked = True
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionB.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventBTrue"
                    rbConditionB.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True

            Case "Event B True - Event A"
                txtEventBNodeInfo.Text = "Node name: Event B True - Event A"
                Bayes.EventA.BoldLine = True
                Bayes.EventB.BoldLine = False
                If rbConditionB.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventBTrue"
                    rbConditionB.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True

                'Case "Event B True - Event A True"
            Case "Event B True and Event A True"
                'txtEventBNodeInfo.Text = "Node name: Event B True - Event A True" & vbCrLf & Bayes.ProbA.GivenB.Label.Text & vbCrLf & Bayes.ProbAandB.Label.Text
                txtEventBNodeInfo.Text = "Node name: Event B True and Event A True" & vbCrLf & Bayes.ProbA.GivenB.Label.Text & vbCrLf & Bayes.ProbAandB.Label.Text
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionB.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventBTrue"
                    rbConditionB.Checked = True
                End If
                If rbA.Checked = False Then rbA.Checked = True

                'Case "Event B True - Event A False"
            Case "Event B True and Event A False"
                'txtEventBNodeInfo.Text = "Node name: Event B True - Event A False" & vbCrLf & Bayes.ProbNotA.GivenB.Label.Text & vbCrLf & Bayes.ProbNotAandB.Label.Text
                txtEventBNodeInfo.Text = "Node name: Event B True and Event A False" & vbCrLf & Bayes.ProbNotA.GivenB.Label.Text & vbCrLf & Bayes.ProbNotAandB.Label.Text
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionB.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventBTrue"
                    rbConditionB.Checked = True
                End If
                If rbNotA.Checked = False Then rbNotA.Checked = True

            Case "Event B False"
                txtEventBNodeInfo.Text = "Node name: Event B False" & vbCrLf & Bayes.ProbNotB.Label.Text
                'rbConditionNotB.Checked = True
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionNotB.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventBFalse"
                    rbConditionNotB.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True

            Case "Event B False - Event A"
                txtEventBNodeInfo.Text = "Node name: Event B False - Event A"
                Bayes.EventA.BoldLine = True
                Bayes.EventB.BoldLine = False
                If rbConditionNotB.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventBFalse"
                    rbConditionNotB.Checked = True
                End If
                If rbNone.Checked = False Then rbNone.Checked = True

                'Case "Event B False - Event A True"
            Case "Event B False and Event A True"
                'txtEventBNodeInfo.Text = "Node name: Event B False - Event A True" & vbCrLf & Bayes.ProbA.GivenNotB.Label.Text & vbCrLf & Bayes.ProbAandNotB.Label.Text
                txtEventBNodeInfo.Text = "Node name: Event B False and Event A True" & vbCrLf & Bayes.ProbA.GivenNotB.Label.Text & vbCrLf & Bayes.ProbAandNotB.Label.Text
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionNotB.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventBFalse"
                    rbConditionNotB.Checked = True
                End If
                If rbA.Checked = False Then rbA.Checked = True

                'Case "Event B False - Event A False"
            Case "Event B False and Event A False"
                'txtEventBNodeInfo.Text = "Node name: Event B False - Event A False" & vbCrLf & Bayes.ProbNotA.GivenNotB.Label.Text & vbCrLf & Bayes.ProbNotAandNotB.Label.Text
                txtEventBNodeInfo.Text = "Node name: Event B False and Event A False" & vbCrLf & Bayes.ProbNotA.GivenNotB.Label.Text & vbCrLf & Bayes.ProbNotAandNotB.Label.Text
                Bayes.EventA.BoldLine = False
                Bayes.EventB.BoldLine = False
                If rbConditionNotB.Checked Then
                    DrawDiagram()
                Else
                    Bayes.Settings.Condition = "EventBFalse"
                    rbConditionNotB.Checked = True
                End If
                If rbNotA.Checked = False Then rbNotA.Checked = True

            Case Else
                Message.AddWarning("Unknown node name: " & e.Node.Name & vbCrLf)
                txtEventBNodeInfo.Text = "Unknown node name: " & e.Node.Name
        End Select

    End Sub

    Private Sub txtEventAName_TextChanged(sender As Object, e As EventArgs) Handles txtEventAName.TextChanged

    End Sub

    Private Sub txtEventAName_LostFocus(sender As Object, e As EventArgs) Handles txtEventAName.LostFocus
        'Event A Name changed.
        Bayes.EventA.Name = txtEventAName.Text.Trim
        txtEventA.Text = Bayes.EventA.Name
        Bayes.AnnotEventA.Text = txtEventAName.Text.Trim
        txtAnnotEventALabel.Text = txtEventAName.Text.Trim
        Bayes.Modified = True
    End Sub

    Private Sub txtEventADescr_LostFocus(sender As Object, e As EventArgs) Handles txtEventADescr.LostFocus
        'Event A Description changed.
        Bayes.EventA.Description = txtEventADescr.Text.Trim
        Bayes.Modified = True
    End Sub

    Private Sub txtEventNotAName_TextChanged(sender As Object, e As EventArgs) Handles txtEventNotAName.TextChanged

    End Sub

    Private Sub txtEventNotAName_LostFocus(sender As Object, e As EventArgs) Handles txtEventNotAName.LostFocus
        'Event Not A Name changed.
        Bayes.EventA.NotName = txtEventNotAName.Text.Trim
        Bayes.Modified = True
    End Sub

    Private Sub txtEventBName_TextChanged(sender As Object, e As EventArgs) Handles txtEventBName.TextChanged

    End Sub


    Private Sub txtEventBName_LostFocus(sender As Object, e As EventArgs) Handles txtEventBName.LostFocus
        'Event B Name changed.
        Bayes.EventB.Name = txtEventBName.Text.Trim
        txtEventB.Text = Bayes.EventB.Name
        Bayes.AnnotEventB.Text = txtEventBName.Text.Trim
        txtAnnotEventBLabel.Text = txtEventBName.Text.Trim
        Bayes.Modified = True
    End Sub

    Private Sub txtEventBDescr_LostFocus(sender As Object, e As EventArgs) Handles txtEventBDescr.LostFocus
        'Event A Description changed.
        Bayes.EventB.Description = txtEventBDescr.Text.Trim
        Bayes.Modified = True
    End Sub



    Private Sub txtEventNotBName_TextChanged(sender As Object, e As EventArgs) Handles txtEventNotBName.TextChanged

    End Sub

    Private Sub txtEventNotBName_LostFocus(sender As Object, e As EventArgs) Handles txtEventNotBName.LostFocus
        'Event Not B Name changed.
        Bayes.EventB.NotName = txtEventNotBName.Text.Trim
        Bayes.Modified = True
    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs)
        'Show Format inforamtion.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub rbDecimal_CheckedChanged(sender As Object, e As EventArgs) Handles rbDecimal.CheckedChanged
        If rbDecimal.Focused Then
            If rbDecimal.Checked Then
                Bayes.Settings.ProbabilityMeasure = "Decimal"
                ShowDecimalProbabilities()
                UpdateCategoryInfo()
                DrawDiagram()
                DrawEventATree()
                DrawEventBTree()
            End If
        End If
    End Sub

    Private Sub rbPercent_CheckedChanged(sender As Object, e As EventArgs) Handles rbPercent.CheckedChanged
        If rbPercent.Focused Then
            If rbPercent.Checked Then
                Bayes.Settings.ProbabilityMeasure = "Percent"
                ShowPercentProbabilities()
                UpdateCategoryInfo()
                DrawDiagram()
                DrawEventATree()
                DrawEventBTree()
            End If
        End If
    End Sub

    Private Sub btnUpdateDiagram_3_Click(sender As Object, e As EventArgs) Handles btnUpdateDiagram_3.Click
        DrawDiagram()
        DrawEventATree()
        DrawEventBTree()
    End Sub

    Private Sub btnFormatHelp_Click_1(sender As Object, e As EventArgs) Handles btnFormatHelp.Click
        'Show Format inforamtion.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    'Private Sub rbSamples_CheckedChanged(sender As Object, e As EventArgs) Handles rbSamples.CheckedChanged
    '    If rbSamples.Focused Then
    '        If rbSamples.Checked Then
    '            Bayes.ProbabilityMeasure = "Samples"
    '            ShowSampleCounts()
    '        End If
    '    End If
    'End Sub

    'Private Sub chkShowDecimalProb_CheckedChanged(sender As Object, e As EventArgs)
    '    If chkShowDecimalProb.Focused Then
    '        If chkShowDecimalProb.Checked Then
    '            Bayes.ProbabilityMeasure = "Decimal"
    '        End If
    '    End If
    'End Sub

    'Private Sub chkShowPercentProb_CheckedChanged(sender As Object, e As EventArgs)
    '    If chkShowPercentProb.Focused Then
    '        If chkShowPercentProb.Checked Then
    '            Bayes.ProbabilityMeasure = "Percent"
    '        End If
    '    End If
    'End Sub

    'Private Sub chkShowSamples_CheckedChanged(sender As Object, e As EventArgs)
    '    If chkShowSamples.Focused Then
    '        If chkShowSamples.Checked Then
    '            Bayes.ProbabilityMeasure = "Samples"
    '        End If
    '    End If
    'End Sub

    Private Sub txtDecimalFormat_TextChanged(sender As Object, e As EventArgs) Handles txtDecimalFormat.TextChanged

    End Sub

    Private Sub txtDecimalFormat_LostFocus(sender As Object, e As EventArgs) Handles txtDecimalFormat.LostFocus
        Bayes.Settings.DecimalFormat = txtDecimalFormat.Text.Trim
        'Debug.Print(Bayes.ProbabilityMeasure)
        If Bayes.Settings.ProbabilityMeasure = "Decimal" Then ShowDecimalProbabilities()
        'Debug.Print(Bayes.ProbabilityMeasure)
        DrawDiagram()
    End Sub

    Private Sub txtPercentFormat_TextChanged(sender As Object, e As EventArgs) Handles txtPercentFormat.TextChanged

    End Sub

    Private Sub txtPercentFormat_LostFocus(sender As Object, e As EventArgs) Handles txtPercentFormat.LostFocus
        Bayes.Settings.PercentFormat = txtPercentFormat.Text.Trim
        If Bayes.Settings.ProbabilityMeasure = "Percent" Then ShowPercentProbabilities()
        DrawDiagram()
    End Sub

    Private Sub txtSampleFormat_TextChanged(sender As Object, e As EventArgs) Handles txtSamplesFormat.TextChanged, txtSampleFormat.TextChanged

    End Sub

    Private Sub txtSampleFormat_LostFocus(sender As Object, e As EventArgs) Handles txtSamplesFormat.LostFocus, txtSampleFormat.LostFocus
        Bayes.Settings.SamplesFormat = txtSamplesFormat.Text.Trim
        'If Bayes.ProbabilityMeasure = "Samples" Then ShowSampleCounts()
        ShowSampleCounts()
        DrawDiagram()
    End Sub

    Private Sub txtSampsAandB_TextChanged(sender As Object, e As EventArgs) Handles txtSampsAandB.TextChanged

    End Sub

    Private Sub txtSampsAandB_LostFocus(sender As Object, e As EventArgs) Handles txtSampsAandB.LostFocus
        'The Event A and Event B (True Positive) sample count has changed.
        Try
            If txtSampsAandB.ReadOnly = False Then
                Dim SampleCount As Single = txtSampsAandB.Text
                If Bayes.SampsAandB.Value <> SampleCount Then 'Only set the DefinedValue if the Value has changed
                    Bayes.SampsAandB.DefinedValue = SampleCount
                    UpdateFormData()
                End If
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtSampsNotAandB_TextChanged(sender As Object, e As EventArgs) Handles txtSampsNotAandB.TextChanged

    End Sub

    Private Sub txtSampsNotAandB_LostFocus(sender As Object, e As EventArgs) Handles txtSampsNotAandB.LostFocus
        'The Event Not A and Event B (False Positive) sample count has changed.
        Try
            If txtSampsNotAandB.ReadOnly = False Then
                Dim SampleCount As Single = txtSampsNotAandB.Text
                If Bayes.SampsNotAandB.Value <> SampleCount Then 'Only set the DefinedValue if the Value has changed
                    Bayes.SampsNotAandB.DefinedValue = SampleCount
                    UpdateFormData()
                End If
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtSampsNotAandNotB_TextChanged(sender As Object, e As EventArgs) Handles txtSampsNotAandNotB.TextChanged

    End Sub

    Private Sub txtSampsNotAandNotB_LostFocus(sender As Object, e As EventArgs) Handles txtSampsNotAandNotB.LostFocus
        'The Event Not A and Event Not B (True Negative) sample count has changed.
        Try
            If txtSampsNotAandNotB.ReadOnly = False Then
                Dim SampleCount As Single = txtSampsNotAandNotB.Text
                If Bayes.SampsNotAandNotB.Value <> SampleCount Then 'Only set the DefinedValue if the Value has changed
                    Bayes.SampsNotAandNotB.DefinedValue = SampleCount
                    UpdateFormData()
                End If
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtSampsAandNotB_TextChanged(sender As Object, e As EventArgs) Handles txtSampsAandNotB.TextChanged

    End Sub

    Private Sub txtSampsAandNotB_LostFocus(sender As Object, e As EventArgs) Handles txtSampsAandNotB.LostFocus
        'The Event A and Event Not B (False Negative) sample count has changed.
        Try
            If txtSampsAandNotB.ReadOnly = False Then
                Dim SampleCount As Single = txtSampsAandNotB.Text
                If Bayes.SampsAandNotB.Value <> SampleCount Then 'Only set the DefinedValue if the Value has changed
                    Bayes.SampsAandNotB.DefinedValue = SampleCount
                    UpdateFormData()
                End If
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtSampleSize_TextChanged(sender As Object, e As EventArgs) Handles txtSampleSize.TextChanged

    End Sub

    Private Sub txtSampleSize_LostFocus(sender As Object, e As EventArgs) Handles txtSampleSize.LostFocus
        Try

            If txtSampleSize.ReadOnly = False Then
                Dim SampSize As Single
                SampSize = txtSampleSize.Text
                If Bayes.SampleSize.Value <> SampSize Then '(Only set a new SampleSize value if it has changed.)
                    'Bayes.SampleSize.Value = SampSize
                    Bayes.SampleSize.DefinedValue = SampSize
                    'If Bayes.Settings.ProbabilityMeasure = "Samples" Then ShowSampleCounts()
                    UpdateFormData()
                End If
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub


    Private Sub ShowProbabilities()
        'Show the Bayes model probabilities.
        If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
            ShowDecimalProbabilities()
        ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
            ShowPercentProbabilities()
        Else
            Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
            Message.AddWarning("Decimal probabilities will be displayed." & vbCrLf)
            ShowDecimalProbabilities()
        End If

        WilsonInterval() 'Show the confidence intervals for the probabilities

        'CalcPerformanceMetrics()
        ShowPerformanceMetrics()
    End Sub

    Private Sub ShowDecimalProbabilities()
        'Display the probabilities as decimal numbers.
        txtProbA.Text = Format(Bayes.ProbA.Value, Bayes.Settings.DecimalFormat)
        txtProbB.Text = Format(Bayes.ProbB.Value, Bayes.Settings.DecimalFormat)
        'txtProbAandB.Text = Format(Bayes.ProbAandB, Bayes.DecimalFormat)
        txtProbAandB.Text = Format(Bayes.ProbAandB.Value, Bayes.Settings.DecimalFormat)

        txtProbNotAandNotB.Text = Format(Bayes.ProbNotAandNotB.Value, Bayes.Settings.DecimalFormat)
        txtProbNotAandB.Text = Format(Bayes.ProbNotAandB.Value, Bayes.Settings.DecimalFormat)
        txtProbAandNotB.Text = Format(Bayes.ProbAandNotB.Value, Bayes.Settings.DecimalFormat)

        'txtProbBgivenA.Text = Format(Bayes.ProbBgivenA, Bayes.DecimalFormat)
        txtProbBgivenA.Text = Format(Bayes.ProbB.GivenA.Value, Bayes.Settings.DecimalFormat)
        'txtProbBgivenNotA.Text = Format(Bayes.ProbBgivenNotA, Bayes.DecimalFormat)
        txtProbBgivenNotA.Text = Format(Bayes.ProbB.GivenNotA.Value, Bayes.Settings.DecimalFormat)
        'txtProbAgivenB.Text = Format(Bayes.ProbAgivenB, Bayes.DecimalFormat)
        txtProbAgivenB.Text = Format(Bayes.ProbA.GivenB.Value, Bayes.Settings.DecimalFormat)

        txtProbNotAgivenB.Text = Format(Bayes.ProbNotA.GivenB.Value, Bayes.Settings.DecimalFormat)
        txtProbNotAgivenNotB.Text = Format(Bayes.ProbNotA.GivenNotB.Value, Bayes.Settings.DecimalFormat)
        txtProbAgivenNotB.Text = Format(Bayes.ProbA.GivenNotB.Value, Bayes.Settings.DecimalFormat)



        'Label31.Text = "Probability of Event A (before Event B results)"
        'Label32.Text = "Probability of Event B (positive test result)"
        'Label51.Text = "Probability of Event A and Event B"
        'Label33.Text = "Probability of Event B given Event A (true positive)"

        'Label34.Text = "Probability of Event B given Event Not A (false positive)"
        'Label36.Text = "Probability of Event A given Event B (updated probability)"

        'Update the Confidence tab:
        txtPMLAandB.Text = Format(Bayes.ProbAandB.Value, Bayes.Settings.DecimalFormat)
        txtPMLNotAandB.Text = Format(Bayes.ProbNotAandB.Value, Bayes.Settings.DecimalFormat)
        txtPMLNotAandNotB.Text = Format(Bayes.ProbNotAandNotB.Value, Bayes.Settings.DecimalFormat)
        txtPMLAandNotB.Text = Format(Bayes.ProbAandNotB.Value, Bayes.Settings.DecimalFormat)
        txtPMLA.Text = Format(Bayes.ProbA.Value, Bayes.Settings.DecimalFormat)
        txtPMLB.Text = Format(Bayes.ProbB.Value, Bayes.Settings.DecimalFormat)

        txtConfidence.Text = Format(Confidence, Bayes.Settings.DecimalFormat)

    End Sub

    Private Sub ShowPercentProbabilities()
        'Display the probabilities as percentages.
        txtProbA.Text = Format(Bayes.ProbA.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtProbB.Text = Format(Bayes.ProbB.Value * 100, Bayes.Settings.PercentFormat) & "%"
        'txtProbAandB.Text = Format(Bayes.ProbAandB * 100, Bayes.PercentFormat) & "%"
        txtProbAandB.Text = Format(Bayes.ProbAandB.Value * 100, Bayes.Settings.PercentFormat) & "%"

        txtProbNotAandNotB.Text = Format(Bayes.ProbNotAandNotB.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtProbNotAandB.Text = Format(Bayes.ProbNotAandB.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtProbAandNotB.Text = Format(Bayes.ProbAandNotB.Value * 100, Bayes.Settings.PercentFormat) & "%"

        'txtProbBgivenA.Text = Format(Bayes.ProbBgivenA * 100, Bayes.PercentFormat) & "%"
        txtProbBgivenA.Text = Format(Bayes.ProbB.GivenA.Value * 100, Bayes.Settings.PercentFormat) & "%"
        'txtProbBgivenNotA.Text = Format(Bayes.ProbBgivenNotA * 100, Bayes.PercentFormat) & "%"
        txtProbBgivenNotA.Text = Format(Bayes.ProbB.GivenNotA.Value * 100, Bayes.Settings.PercentFormat) & "%"
        'txtProbAgivenB.Text = Format(Bayes.ProbAgivenB * 100, Bayes.PercentFormat) & "%"
        txtProbAgivenB.Text = Format(Bayes.ProbA.GivenB.Value * 100, Bayes.Settings.PercentFormat) & "%"

        txtProbNotAgivenB.Text = Format(Bayes.ProbNotA.GivenB.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtProbNotAgivenNotB.Text = Format(Bayes.ProbNotA.GivenNotB.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtProbAgivenNotB.Text = Format(Bayes.ProbA.GivenNotB.Value * 100, Bayes.Settings.PercentFormat) & "%"

        'Label31.Text = "Probability of Event A (before Event B results)" 'Note: the % symbol is now included int he value string
        'Label32.Text = "Probability of Event B (positive test result)"
        'Label51.Text = "Probability of Event A and Event B"
        'Label33.Text = "Probability of Event B given Event A (true positive)"

        'Label34.Text = "Probability of Event B given Event Not A (false positive)"
        'Label36.Text = "Probability of Event A given Event B (updated probability)"

        'Update the Confidence tab:
        txtPMLAandB.Text = Format(Bayes.ProbAandB.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtPMLNotAandB.Text = Format(Bayes.ProbNotAandB.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtPMLNotAandNotB.Text = Format(Bayes.ProbNotAandNotB.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtPMLAandNotB.Text = Format(Bayes.ProbAandNotB.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtPMLA.Text = Format(Bayes.ProbA.Value * 100, Bayes.Settings.PercentFormat) & "%"
        txtPMLB.Text = Format(Bayes.ProbB.Value * 100, Bayes.Settings.PercentFormat) & "%"

        txtConfidence.Text = Format(Confidence * 100, Bayes.Settings.PercentFormat) & "%"

    End Sub

    Private Sub ShowSampleCounts()
        'Display sample counts:

        txtSampsA.Text = Format(Bayes.ProbA.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtSampsB.Text = Format(Bayes.ProbB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtSampsAandB.Text = Format(Bayes.ProbAandB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)

        txtSampsNotAandNotB.Text = Format(Bayes.ProbNotAandNotB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtSampsNotAandB.Text = Format(Bayes.ProbNotAandB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtSampsAandNotB.Text = Format(Bayes.ProbAandNotB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)

        txtSampleSize.Text = Format(Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)

        'txtNAandB.Text = Format(Bayes.ProbAandB.Value * Bayes.SampleSize, Bayes.SamplesFormat)
        txtCalcProbNum.Text = Format(Bayes.ProbAandB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        'txtNA.Text = Format(Bayes.ProbA.Value * Bayes.SampleSize, Bayes.SamplesFormat)
        txtCalcProbDenom.Text = Format(Bayes.ProbB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)

        'Update the Confidence tab:
        txtNAandB.Text = Format(Bayes.ProbAandB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtNNotAandB.Text = Format(Bayes.ProbNotAandB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtNNotAandNotB.Text = Format(Bayes.ProbNotAandNotB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
        txtNAandNotB.Text = Format(Bayes.ProbAandNotB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)

        txtSurveySize.Text = Format(Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)

    End Sub

    Private Sub ShowProbAgivenB()
        Select Case Bayes.Settings.ProbabilityMeasure
            Case "Decimal"
                'txtProbAgivenB.Text = Format(Bayes.ProbAgivenB.Value, Bayes.DecimalFormat)
                txtProbAgivenB.Text = Format(Bayes.ProbA.GivenB.Value, Bayes.Settings.DecimalFormat)
            Case "Percent"
                'txtProbAgivenB.Text = Format(Bayes.ProbAgivenB.Value * 100, Bayes.PercentFormat) & "%"
                txtProbAgivenB.Text = Format(Bayes.ProbA.GivenB.Value * 100, Bayes.Settings.PercentFormat) & "%"
            Case "Samples"
                'txtProbAgivenB.Text = Format(Bayes.ProbAgivenB.Value * Bayes.SampleSize, Bayes.SamplesFormat)
                txtProbAgivenB.Text = Format(Bayes.ProbA.GivenB.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
            Case Else
                Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
        End Select
    End Sub

    Private Sub ShowProbBgivenA()
        Select Case Bayes.Settings.ProbabilityMeasure
            Case "Decimal"
                'txtProbBgivenA.Text = Format(Bayes.ProbBgivenA, Bayes.DecimalFormat)
                txtProbBgivenA.Text = Format(Bayes.ProbB.GivenA.Value, Bayes.Settings.DecimalFormat)
            Case "Percent"
                'txtProbBgivenA.Text = Format(Bayes.ProbBgivenA * 100, Bayes.PercentFormat) & "%"
                'txtProbBgivenA.Text = Format(Bayes.ProbBgivenA * 100, Bayes.PercentFormat) & "%"
                txtProbBgivenA.Text = Format(Bayes.ProbB.GivenA.Value * 100, Bayes.Settings.PercentFormat) & "%"
            Case "Samples"
                'txtProbBgivenA.Text = Format(Bayes.ProbBgivenA * Bayes.SampleSize, Bayes.SamplesFormat)
                'txtProbBgivenA.Text = Format(Bayes.ProbBgivenA * Bayes.SampleSize, Bayes.SamplesFormat)
                txtProbBgivenA.Text = Format(Bayes.ProbB.GivenA.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
            Case Else
                Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
        End Select
    End Sub

    Private Sub ShowProbBgivenNotA()
        Select Case Bayes.Settings.ProbabilityMeasure
            Case "Decimal"
                'txtProbBgivenNotA.Text = Format(Bayes.ProbBgivenNotA, Bayes.DecimalFormat)
                txtProbBgivenNotA.Text = Format(Bayes.ProbB.GivenNotA.Value, Bayes.Settings.DecimalFormat)
            Case "Percent"
                'txtProbAgivenB.Text = Format(Bayes.ProbBgivenNotA * 100, Bayes.PercentFormat) & "%"
                txtProbAgivenB.Text = Format(Bayes.ProbB.GivenNotA.Value * 100, Bayes.Settings.PercentFormat) & "%"
            Case "Samples"
                'txtProbAgivenB.Text = Format(Bayes.ProbBgivenNotA * Bayes.SampleSize, Bayes.SamplesFormat)
                txtProbAgivenB.Text = Format(Bayes.ProbB.GivenNotA.Value * Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)
            Case Else
                Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
        End Select
    End Sub

    Private Sub txtProbA_TextChanged(sender As Object, e As EventArgs) Handles txtProbA.TextChanged

    End Sub

    Private Sub UpdateFormData()
        'Update the form with the new probabilites and sample counts.
        DrawDiagram()
        DrawEventATree()
        DrawEventBTree()
        ShowProbabilities()
        ShowSampleCounts()
    End Sub

    Private Sub txtProbA_LostFocus(sender As Object, e As EventArgs) Handles txtProbA.LostFocus
        'Event A probability changed.
        Try
            If txtProbA.ReadOnly = False Then
                Dim ProbValue As Single
                If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
                    ProbValue = txtProbA.Text
                    If Bayes.ProbA.Value <> ProbValue Then
                        Bayes.ProbA.DefinedValue = ProbValue 'Only set the DefinedValue if the Value has changed
                        UpdateFormData()
                    End If
                ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
                    ProbValue = txtProbA.Text.Replace("%", "")
                    ProbValue = ProbValue / 100
                    If Bayes.ProbA.Value <> ProbValue Then
                        Bayes.ProbA.DefinedValue = ProbValue 'Only set the DefinedValue if the Value has changed
                        UpdateFormData()
                    End If
                Else
                    Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
                End If
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtProbB_TextChanged(sender As Object, e As EventArgs) Handles txtProbB.TextChanged

    End Sub

    Private Sub txtProbB_LostFocus(sender As Object, e As EventArgs) Handles txtProbB.LostFocus
        'Event B probability changed.
        Try
            If txtProbB.ReadOnly = False Then
                Dim ProbValue As Single
                'ProbValue = txtProbB.Text
                If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
                    ProbValue = txtProbB.Text
                    'Bayes.ProbB.Value = ProbValue
                    'Bayes.ProbB.DefinedValue = ProbValue
                    If Bayes.ProbB.Value <> ProbValue Then
                        Bayes.ProbB.DefinedValue = ProbValue 'Only set the DefinedValue if the Value has changed
                        UpdateFormData()
                    End If
                ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
                    ProbValue = txtProbB.Text.Replace("%", "")
                    'Bayes.ProbB.Value = ProbValue / 100
                    'Bayes.ProbB.DefinedValue = ProbValue / 100
                    ProbValue = ProbValue / 100
                    If Bayes.ProbB.Value <> ProbValue Then
                        Bayes.ProbB.DefinedValue = ProbValue 'Only set the DefinedValue if the Value has changed
                        UpdateFormData()
                    End If
                    'ElseIf Bayes.ProbabilityMeasure = "Samples" Then
                    '    Bayes.ProbB.Value = ProbValue / Bayes.SampleSize
                Else
                    Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
                End If
                'ShowProbAgivenB()
                'DrawDiagram()
                'DrawEventATree()
                'DrawEventBTree()
                'ShowProbabilities()
                'ShowSampleCounts()
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtProbAandB_TextChanged(sender As Object, e As EventArgs) Handles txtProbAandB.TextChanged

    End Sub

    Private Sub txtProbAandB_LostFocus(sender As Object, e As EventArgs) Handles txtProbAandB.LostFocus

    End Sub

    Private Sub txtProbBgivenA_TextChanged(sender As Object, e As EventArgs) Handles txtProbBgivenA.TextChanged

    End Sub

    Private Sub txtProbBgivenA_LostFocus(sender As Object, e As EventArgs) Handles txtProbBgivenA.LostFocus
        'Event B given A probability changed.
        Try
            If txtProbBgivenA.ReadOnly = False Then
                Dim ProbValue As Single
                'ProbValue = txtProbBgivenA.Text
                If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
                    'Bayes.ProbBgivenA = ProbValue
                    ProbValue = txtProbBgivenA.Text
                    'Bayes.ProbB.GivenA.Value = ProbValue
                    'Bayes.ProbB.GivenA.DefinedValue = ProbValue
                    If Bayes.ProbB.GivenA.Value <> ProbValue Then
                        Bayes.ProbB.GivenA.DefinedValue = ProbValue 'Only set the DefinedValue if the Value has changed
                        UpdateFormData()
                    End If
                ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
                    'Bayes.ProbBgivenA = ProbValue / 100
                    ProbValue = txtProbBgivenA.Text.Replace("%", "")
                    'Bayes.ProbB.GivenA.Value = ProbValue / 100
                    'Bayes.ProbB.GivenA.DefinedValue = ProbValue / 100
                    ProbValue = ProbValue / 100
                    If Bayes.ProbB.GivenA.Value <> ProbValue Then
                        Bayes.ProbB.GivenA.DefinedValue = ProbValue 'Only set the DefinedValue if the Value has changed
                        UpdateFormData()
                    End If
                    'ElseIf Bayes.ProbabilityMeasure = "Samples" Then
                    '    'Bayes.ProbBgivenA = ProbValue / Bayes.SampleSize
                    '    Bayes.ProbB.GivenA.Value = ProbValue / Bayes.SampleSize
                Else
                    Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
                End If
                'ShowProbAgivenB() 'Use Bayes formula to update Event A probability (given Event B)
                'ShowProbBgivenNotA() 'Update Prob B given Not A
                'DrawDiagram()
                'DrawEventATree()
                'DrawEventBTree()
                'ShowProbabilities()
                'ShowSampleCounts()
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtProbBgivenNotA_TextChanged(sender As Object, e As EventArgs) Handles txtProbBgivenNotA.TextChanged

    End Sub

    Private Sub txtProbBgivenNotA_LostFocus(sender As Object, e As EventArgs) Handles txtProbBgivenNotA.LostFocus
        'Event B given Not A probability changed.
        Try
            If txtProbBgivenNotA.ReadOnly = False Then
                Dim ProbValue As Single
                'ProbValue = txtProbBgivenNotA.Text
                If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
                    'Bayes.ProbBgivenNotA = ProbValue
                    ProbValue = txtProbBgivenNotA.Text
                    'Bayes.ProbB.GivenNotA.Value = ProbValue
                    'Bayes.ProbB.GivenNotA.DefinedValue = ProbValue
                    If Bayes.ProbB.GivenNotA.Value <> ProbValue Then
                        Bayes.ProbB.GivenNotA.DefinedValue = ProbValue 'Only set the DefinedValue if the Value has changed
                        UpdateFormData()
                    End If
                ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
                    'Bayes.ProbBgivenNotA = ProbValue / 100
                    ProbValue = txtProbBgivenNotA.Text.Replace("%", "")
                    'Bayes.ProbB.GivenNotA.Value = ProbValue / 100
                    'Bayes.ProbB.GivenNotA.DefinedValue = ProbValue / 100
                    ProbValue = ProbValue / 100
                    If Bayes.ProbB.GivenNotA.Value <> ProbValue Then
                        Bayes.ProbB.GivenNotA.DefinedValue = ProbValue 'Only set the DefinedValue if the Value has changed
                        UpdateFormData()
                    End If
                    'ElseIf Bayes.ProbabilityMeasure = "Samples" Then
                    '    'Bayes.ProbBgivenNotA = ProbValue / Bayes.SampleSize
                    '    Bayes.ProbB.GivenNotA.Value = ProbValue / Bayes.SampleSize
                Else
                    Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
                End If
                'ShowProbAgivenB() 'Use Bayes formula to update Event A probability (given Event B)
                'ShowProbBgivenA() 'Update Prob B given A
                'DrawDiagram()
                'DrawEventATree()
                'DrawEventBTree()
                'ShowProbabilities()
                'ShowSampleCounts()
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    'Private Sub CalcPerformanceMetrics()
    Private Sub ShowPerformanceMetrics()
        'Calculate the test performance metrics.
        'https://towardsdatascience.com/accuracy-recall-precision-f-score-specificity-which-to-optimize-on-867d3f11124
        'https://www.dataschool.io/simple-guide-to-confusion-matrix-terminology/

        'Dim TP As Double = Bayes.ProbA.GivenB.Value    'True Positive probability  - txtProbAgivenB
        'Dim TN As Double = Bayes.ProbNotA.GivenNotB.Value 'True Negative probability  - txtProbNotAgivenNotB
        'Dim FP As Double = Bayes.ProbNotA.GivenB.Value    'False Positive probability - txtProbNotAgivenB
        'Dim FN As Double = Bayes.ProbA.GivenNotB.Value    'False Negative probability - txtProbAgivenNotB

        'Dim Precision As Double = TP / (TP + FP) 'How many of those testing positive are truly positive.
        'Dim Accuracy As Double = (TP + TN) / (TP + FP + FN + TN) 'How many of those tested were correctly identified as positive or negative.
        'Dim Sensitivity As Double = TP / (TP + FN) '(aka Recall) How many of thoose that are positive tested positive.
        'Dim Specificity As Double = TN / (TN + FP) 'How many of those that are negative tested negative.
        'Dim F1_Score As Double = 2 * Sensitivity * Precision / (Sensitivity + Precision) 'The harmonic mean of the Precision and Sensitivity.

        'txtProbTruePos.Text = TP
        'txtProbTrueNeg.Text = TN
        'txtProbFalsePos.Text = FP
        'txtProbFalseNeg.Text = FN

        'txtSampsTruePos.Text = Bayes.ProbA.GivenB.FormattedValue
        'txtPSampsTrueNeg.Text = Bayes.ProbNotA.GivenNotB.FormattedValue
        'txtSampsFalsePos.Text = Bayes.ProbNotA.GivenB.FormattedValue
        'txtSampsFalseNeg.Text = Bayes.ProbA.GivenNotB.FormattedValue

        txtSampsTruePos.Text = Bayes.SampsA.GivenB.FormattedValue
        txtPSampsTrueNeg.Text = Bayes.SampsNotA.GivenNotB.FormattedValue
        txtSampsFalsePos.Text = Bayes.SampsNotA.GivenB.FormattedValue
        txtSampsFalseNeg.Text = Bayes.SampsA.GivenNotB.FormattedValue

        'txtPrecision.Text = Precision
        'txtAccuracy.Text = Accuracy
        'txtSensitivity.Text = Sensitivity
        'txtSpecificity.Text = Specificity
        'txtF1Score.Text = F1_Score
        txtPrecision.Text = Bayes.Performance.FormattedPrecision
        txtNPR.Text = Bayes.Performance.FormattedNegativePredictiveValue
        txtAccuracy.Text = Bayes.Performance.FormattedAccuracy
        txtF1Score.Text = Bayes.Performance.FormattedF1_Score

        txtSensitivity.Text = Bayes.Performance.FormattedSensitivity
        txtSpecificity.Text = Bayes.Performance.FormattedSpecificity

        txtPrevalence.Text = Bayes.Performance.FormattedPrevalence
        txtSampSize.Text = Format(Bayes.SampleSize.Value, Bayes.Settings.SamplesFormat)

        txtFPR.Text = Bayes.Performance.FormattedFalsePositiveRate
        txtFNR.Text = Bayes.Performance.FormattedFalseNegativeRate
        txtFOR.Text = Bayes.Performance.FormattedFalseOmissionRate
        txtFDR.Text = Bayes.Performance.FormattedFalseDiscoveryRate
        txtPLR.Text = Bayes.Performance.FormattedPositiveLikelihoodRatio
        txtNLR.Text = Bayes.Performance.FormattedNegativeLikelihoodRatio
        txtDOR.Text = Bayes.Performance.FormattedDiagnosticOddsRatio

        'txtPerfFormat.Text = Bayes.Performance.DisplayFormat

        'Set Metric Calculator values:
        txtTestName.Text = Bayes.Name
        txtPointLabel.Text = Bayes.Name
        txtTP.Text = Bayes.SampsA.GivenB.FormattedValue
        txtTN.Text = Bayes.SampsNotA.GivenNotB.FormattedValue
        txtFP.Text = Bayes.SampsNotA.GivenB.FormattedValue
        txtFN.Text = Bayes.SampsA.GivenNotB.FormattedValue
        CalcMetrics()


    End Sub

    Private Sub txtProbAgivenB_TextChanged(sender As Object, e As EventArgs) Handles txtProbAgivenB.TextChanged

    End Sub

    Private Sub txtProbAgivenB_LostFocus(sender As Object, e As EventArgs) Handles txtProbAgivenB.LostFocus

    End Sub

    Private Sub txtProbNotAgivenB_TextChanged(sender As Object, e As EventArgs) Handles txtProbNotAgivenB.TextChanged

    End Sub

    Private Sub txtProbNotAgivenB_LostFocus(sender As Object, e As EventArgs) Handles txtProbNotAgivenB.LostFocus

    End Sub

    Private Sub txtProbNotAgivenNotB_TextChanged(sender As Object, e As EventArgs) Handles txtProbNotAgivenNotB.TextChanged

    End Sub

    Private Sub txtProbNotAgivenNotB_LostFocus(sender As Object, e As EventArgs) Handles txtProbNotAgivenNotB.LostFocus

    End Sub

    Private Sub txtProbAgivenNotB_TextChanged(sender As Object, e As EventArgs) Handles txtProbAgivenNotB.TextChanged

    End Sub

    Private Sub txtProbAgivenNotB_LostFocus(sender As Object, e As EventArgs) Handles txtProbAgivenNotB.LostFocus

    End Sub

    Private Sub txtProbAandB_MouseHover(sender As Object, e As EventArgs) Handles txtProbAandB.MouseHover
        'Show the value calculated using the sample counts.
        lblCalcProbNum.Text = "P(AandB) = n(AandB)"
        txtCalcProbNum.Text = Bayes.SampsAandB.FormattedValue
        lblCalcProbDenom.Text = "/ Sample Size"
        txtCalcProbDenom.Text = Bayes.SampleSize.Value
        txtCalcProbValue.Text = ProbString(Bayes.SampsAandB.Value / Bayes.SampleSize.Value)

    End Sub

    Private Sub txtProbBgivenA_MouseHover(sender As Object, e As EventArgs) Handles txtProbBgivenA.MouseHover
        'Show the value calculated using the sample counts.
        lblCalcProbNum.Text = "P(B|A) = n(AandB)"
        txtCalcProbNum.Text = Bayes.SampsAandB.FormattedValue
        lblCalcProbDenom.Text = "/ n(A)"
        txtCalcProbDenom.Text = Bayes.SampsA.FormattedValue
        'txtCalcProbValue.Text = ProbString(Bayes.SampsAandB.Value / Bayes.SampsA.Value)
        txtCalcProbValue.Text = ProbString(Bayes.SampsAandB.Value / Bayes.SampsA.Value)
    End Sub

    Private Sub txtProbBgivenNotA_MouseHover(sender As Object, e As EventArgs) Handles txtProbBgivenNotA.MouseHover
        'Show the value calculated using the sample counts.
        lblCalcProbNum.Text = "P(B|NotA) = n(NotAandB)"
        txtCalcProbNum.Text = Bayes.SampsNotAandB.FormattedValue
        lblCalcProbDenom.Text = "/ n(NotA)"
        txtCalcProbDenom.Text = Bayes.SampsNotA.FormattedValue
        txtCalcProbValue.Text = ProbString(Bayes.SampsNotAandB.Value / Bayes.SampsNotA.Value)
    End Sub

    Private Sub txtProbAgivenB_MouseHover(sender As Object, e As EventArgs) Handles txtProbAgivenB.MouseHover
        'Show the value calculated using the sample counts.
        lblCalcProbNum.Text = "P(A|B) = n(AandB)"
        txtCalcProbNum.Text = Bayes.SampsAandB.FormattedValue
        lblCalcProbDenom.Text = "/ n(B)"
        txtCalcProbDenom.Text = Bayes.SampsB.FormattedValue
        txtCalcProbValue.Text = ProbString(Bayes.SampsAandB.Value / Bayes.SampsB.Value)
    End Sub

    Private Sub txtProbNotAgivenB_MouseHover(sender As Object, e As EventArgs) Handles txtProbNotAgivenB.MouseHover
        'Show the value calculated using the sample counts.
        lblCalcProbNum.Text = "P(NotA|B) = n(NotAandB)"
        txtCalcProbNum.Text = Bayes.SampsNotAandB.FormattedValue
        lblCalcProbDenom.Text = "/ n(B)"
        txtCalcProbDenom.Text = Bayes.SampsB.FormattedValue
        txtCalcProbValue.Text = ProbString(Bayes.SampsNotAandB.Value / Bayes.SampsB.Value)
    End Sub

    Private Sub txtProbNotAgivenNotB_MouseHover(sender As Object, e As EventArgs) Handles txtProbNotAgivenNotB.MouseHover
        'Show the value calculated using the sample counts.
        lblCalcProbNum.Text = "P(NotA|NotB) = n(NotAandNotB)"
        txtCalcProbNum.Text = Bayes.SampsNotAandNotB.FormattedValue
        lblCalcProbDenom.Text = "/ n(NotB)"
        txtCalcProbDenom.Text = Bayes.SampsNotB.FormattedValue
        txtCalcProbValue.Text = ProbString(Bayes.SampsNotAandNotB.Value / Bayes.SampsNotB.Value)
    End Sub

    Private Sub txtProbAgivenNotB_MouseHover(sender As Object, e As EventArgs) Handles txtProbAgivenNotB.MouseHover
        'Show the value calculated using the sample counts.
        lblCalcProbNum.Text = "P(A|NotB) = n(AandNotB)"
        txtCalcProbNum.Text = Bayes.SampsAandNotB.FormattedValue
        lblCalcProbDenom.Text = "/ n(NotB)"
        txtCalcProbDenom.Text = Bayes.SampsNotB.FormattedValue
        txtCalcProbValue.Text = ProbString(Bayes.SampsAandNotB.Value / Bayes.SampsNotB.Value)
    End Sub

    'Private Sub chkShowProbabilities_CheckedChanged(sender As Object, e As EventArgs)
    '    If chkShowProbabilities.Focused Then
    '        Bayes.ShowProbabilities = chkShowProbabilities.Checked
    '    End If
    'End Sub

    'Private Sub chkShowSamples_CheckedChanged_1(sender As Object, e As EventArgs) Handles chkShowSamples.CheckedChanged
    '    If chkShowSamples.Focused Then
    '        Bayes.ShowSampleCounts = chkShowSamples.Checked
    '    End If
    'End Sub

    'Private Sub txtSampleFormat_TextChanged_1(sender As Object, e As EventArgs) Handles txtSampleFormat.TextChanged

    'End Sub

    'Private Sub txtSampleFormat_LostFocus(sender As Object, e As EventArgs) Handles txtSampleFormat.LostFocus
    '    Bayes.SamplesFormat = txtSampleFormat.Text.Trim
    'End Sub

    'Private Sub txtDecimalFormat_TextChanged_1(sender As Object, e As EventArgs) Handles txtDecimalFormat.TextChanged

    'End Sub

    'Private Sub txtDecimalFormat_LostFocus(sender As Object, e As EventArgs) Handles txtDecimalFormat.LostFocus
    '    Bayes.DecimalFormat = txtDecimalFormat.Text.Trim

    'End Sub



    'Private Sub txtPercentFormat_LostFocus(sender As Object, e As EventArgs) Handles txtPercentFormat.LostFocus
    '    Bayes.PercentFormat = txtPercentFormat.Text.Trim
    'End Sub

    Private Sub pbVenn_Click(sender As Object, e As EventArgs) Handles pbVenn.Click

    End Sub

    'Private Sub pbVenn_SizeChanged(sender As Object, e As EventArgs) Handles pbVenn.SizeChanged
    '    txtPictureHeight.Text = pbVenn.Height
    '    txtPictureWidth.Text = pbVenn.Width
    '    txtImageHeight.Text = pbVenn.Image.Height
    '    txtImageWidth.Text = pbVenn.Image.Width
    'End Sub

    'Private Sub btnDisplayText_Click(sender As Object, e As EventArgs)

    '    'Dim img As New Bitmap(pbVenn.Width, pbVenn.Height)


    '    'Dim img As New Bitmap(My.Resources.Bayes_Prob_Diag)
    '    Dim img As New Bitmap(Bayes.Width, Bayes.Height)
    '    Using g = Graphics.FromImage(img)
    '        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
    '        'g.DrawString(txtDisplayText.Text, txtDisplayText.Font, Brushes.Black, New PointF(txtTextX.Text, txtTextY.Text))

    '        'Dim myFontHeight As Integer = txtDisplayText.Font.Height
    '        Dim myFontHeight As Integer = g.MeasureString(txtDisplayText.Text, txtDisplayText.Font).Height
    '        Dim myFontWidth As Integer = g.MeasureString(txtDisplayText.Text, txtDisplayText.Font).Width


    '        Dim myBrush As New SolidBrush(txtDisplayText.ForeColor)
    '        g.DrawString(txtDisplayText.Text, txtDisplayText.Font, myBrush, New PointF(txtTextX.Text - myFontWidth / 2, txtTextY.Text))
    '        g.DrawString(txtDisplayText.Text, txtDisplayText.Font, myBrush, New PointF(txtTextX.Text - myFontWidth / 2, txtTextY.Text + myFontHeight))

    '        'g.DrawEllipse(New Pen(txtDisplayText.ForeColor, 3), New Rectangle(200, 200, 300, 300))
    '        'g.DrawEllipse(New Pen(txtDisplayText.ForeColor, 3), New Rectangle(300, 200, 300, 300))

    '        Dim myEllipse1 As New Drawing2D.GraphicsPath
    '        myEllipse1.AddEllipse(New Rectangle(200, 200, 300, 300))
    '        Dim reg1 As New Region(myEllipse1)
    '        'g.DrawPath(New Pen(Color.Red, 4), myEllipse1)

    '        Dim myEllipse2 As New Drawing2D.GraphicsPath
    '        myEllipse2.AddEllipse(New Rectangle(300, 200, 300, 300))
    '        Dim reg2 As New Region(myEllipse2)
    '        'g.DrawPath(New Pen(Color.Blue, 4), myEllipse2)

    '        Dim myRegion As New Region(myEllipse1)
    '        myRegion.Intersect(myEllipse2)

    '        g.FillRegion(New SolidBrush(Color.LightGray), myRegion)

    '        g.DrawPath(New Pen(Color.Red, 4), myEllipse1)
    '        g.DrawPath(New Pen(Color.Blue, 4), myEllipse2)




    '    End Using



    '    pbVenn.Image?.Dispose()
    '    pbVenn.Image = img


    'End Sub


    'Private Sub btnFont_Click(sender As Object, e As EventArgs)
    '    FontDialog1.Font = txtDisplayText.Font
    '    FontDialog1.ShowDialog()
    '    txtDisplayText.Font = FontDialog1.Font
    'End Sub

    'Private Sub btnColor_Click(sender As Object, e As EventArgs)
    '    ColorDialog1.Color = txtDisplayText.ForeColor
    '    ColorDialog1.ShowDialog()
    '    txtDisplayText.ForeColor = ColorDialog1.Color
    'End Sub

    Private Sub txtHighlightColor_TextChanged(sender As Object, e As EventArgs) Handles txtHighlightColor.TextChanged

    End Sub

    Private Sub txtHighlightColor_Click(sender As Object, e As EventArgs) Handles txtHighlightColor.Click
        ColorDialog1.Color = txtHighlightColor.BackColor
        ColorDialog1.ShowDialog()
        txtHighlightColor.BackColor = ColorDialog1.Color
        'Diagram.HighlightRegionColor = ColorDialog1.Color
        Bayes.HighlightRegion.Color = ColorDialog1.Color
        DrawDiagram()
    End Sub


    Private Sub rbNone_CheckedChanged(sender As Object, e As EventArgs) Handles rbNone.CheckedChanged
        If rbNone.Checked Then
            Bayes.ClearHighlights()
            'Diagram.HighlightA = False
            'Diagram.HighlightNotA = False
            'Diagram.HighlightB = False
            'Diagram.HighlightNotB = False
            'Diagram.HighlightAandB = False
            'Diagram.HighlightAandNotB = False
            'Diagram.HighlightNotAandB = False
            'Diagram.HighlightNotAandNotB = False
            DrawDiagram()
        End If
    End Sub

    Private Sub rbA_CheckedChanged(sender As Object, e As EventArgs) Handles rbA.CheckedChanged
        If rbA.Checked Then
            Bayes.ClearHighlights()
            Bayes.HighlightRegion.A = True
            'Diagram.HighlightNotA = False
            'Diagram.HighlightB = False
            'Diagram.HighlightNotB = False
            'Diagram.HighlightAandB = False
            'Diagram.HighlightAandNotB = False
            'Diagram.HighlightNotAandB = False
            'Diagram.HighlightNotAandNotB = False
            DrawDiagram()
        End If
    End Sub

    Private Sub rbNotA_CheckedChanged(sender As Object, e As EventArgs) Handles rbNotA.CheckedChanged
        If rbNotA.Checked Then
            Bayes.ClearHighlights()
            'Diagram.HighlightA = False
            Bayes.HighlightRegion.NotA = True
            'Diagram.HighlightB = False
            'Diagram.HighlightNotB = False
            'Diagram.HighlightAandB = False
            'Diagram.HighlightAandNotB = False
            'Diagram.HighlightNotAandB = False
            'Diagram.HighlightNotAandNotB = False
            DrawDiagram()
        End If
    End Sub

    Private Sub rbB_CheckedChanged(sender As Object, e As EventArgs) Handles rbB.CheckedChanged
        If rbB.Checked Then
            Bayes.ClearHighlights()
            'Diagram.HighlightA = False
            'Diagram.HighlightNotA = False
            Bayes.HighlightRegion.B = True
            'Diagram.HighlightNotB = False
            'Diagram.HighlightAandB = False
            'Diagram.HighlightAandNotB = False
            'Diagram.HighlightNotAandB = False
            'Diagram.HighlightNotAandNotB = False
            DrawDiagram()
        End If
    End Sub

    Private Sub rbNotB_CheckedChanged(sender As Object, e As EventArgs) Handles rbNotB.CheckedChanged
        If rbNotB.Checked Then
            Bayes.ClearHighlights()
            'Diagram.HighlightA = False
            'Diagram.HighlightNotA = False
            'Diagram.HighlightB = False
            Bayes.HighlightRegion.NotB = True
            'Diagram.HighlightAandB = False
            'Diagram.HighlightAandNotB = False
            'Diagram.HighlightNotAandB = False
            'Diagram.HighlightNotAandNotB = False
            DrawDiagram()
        End If
    End Sub

    Private Sub rbAandB_CheckedChanged(sender As Object, e As EventArgs) Handles rbAandB.CheckedChanged
        If rbAandB.Checked Then
            Bayes.ClearHighlights()
            'Diagram.HighlightA = False
            'Diagram.HighlightNotA = False
            'Diagram.HighlightB = False
            'Diagram.HighlightNotB = False
            Bayes.HighlightRegion.AandB = True
            'Diagram.HighlightAandNotB = False
            'Diagram.HighlightNotAandB = False
            'Diagram.HighlightNotAandNotB = False
            DrawDiagram()
        End If
    End Sub

    Private Sub rbAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles rbAandNotB.CheckedChanged
        If rbAandNotB.Checked Then
            Bayes.ClearHighlights()
            'Diagram.HighlightA = False
            'Diagram.HighlightNotA = False
            'Diagram.HighlightB = False
            'Diagram.HighlightNotB = False
            'Diagram.HighlightAandB = False
            Bayes.HighlightRegion.AandNotB = True
            'Diagram.HighlightNotAandB = False
            'Diagram.HighlightNotAandNotB = False
            DrawDiagram()
        End If
    End Sub

    Private Sub rbNotAandB_CheckedChanged(sender As Object, e As EventArgs) Handles rbNotAandB.CheckedChanged
        If rbNotAandB.Checked Then
            Bayes.ClearHighlights()
            'Diagram.HighlightA = False
            'Diagram.HighlightNotA = False
            'Diagram.HighlightB = False
            'Diagram.HighlightNotB = False
            'Diagram.HighlightAandB = False
            'Diagram.HighlightAandNotB = False
            Bayes.HighlightRegion.NotAandB = True
            'Diagram.HighlightNotAandNotB = False
            DrawDiagram()
        End If
    End Sub

    Private Sub rbNotAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles rbNotAandNotB.CheckedChanged
        If rbNotAandNotB.Checked Then
            Bayes.ClearHighlights()
            'Diagram.HighlightA = False
            'Diagram.HighlightNotA = False
            'Diagram.HighlightB = False
            'Diagram.HighlightNotB = False
            'Diagram.HighlightAandB = False
            'Diagram.HighlightAandNotB = False
            'Diagram.HighlightNotAandB = False
            Bayes.HighlightRegion.NotAandNotB = True
            DrawDiagram()
        End If
    End Sub

    Private Sub rbConditionNone_CheckedChanged(sender As Object, e As EventArgs) Handles rbConditionNone.CheckedChanged

        'Debug.Print("rbConditionNone clicked.")
        'Debug.Print("Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
        'Debug.Print("Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
        If rbConditionNone.Checked Then
            'txtCondition.Text = "No contraints."
            txtCondition.Text = "No condition."

            rbLabelCondNone.Checked = True

            'txtConditionLabel.Text = Bayes.AnnotContraintLabelNone
            'txtConditionLabel.Text = Bayes.AnnotCondition.Text
            'txtCondition.Text = Bayes.AnnotCondition.None.Text
            txtConditionLabel.Text = Bayes.AnnotCondition.None.Text

            If rbConditionNone.Focused Then Bayes.Settings.Condition = "None" 'Only change this if the user has clicked the radio button.

            'Debug.Print("1. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("1. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)

            Bayes.ZeroProbRegion.A = False
            'Debug.Print("1a. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("1a. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
            rbA.Enabled = True
            rbA.ForeColor = SystemColors.ControlText
            'Debug.Print("1b. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("1b. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
            txtASamps.Enabled = True
            'Debug.Print("1c. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("1c. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
            txtASamps.BackColor = SystemColors.Control
            'Debug.Print("1d. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("1d. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
            'txtASamps.Text = SampString(Bayes.ProbA.Value * Bayes.SampleSize)
            txtASamps.Text = Bayes.SampsA.FormattedValue
            'Debug.Print("1e. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("1e. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
            txtAProb.Enabled = True
            'Debug.Print("1f. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("1f. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
            txtAProb.BackColor = SystemColors.Control
            'Debug.Print("1g. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("1g. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
            'txtAProb.Text = ProbString(Bayes.ProbA.Value) 'Ensure this is set to the Event A probability. 'NOTE: If Percent format is selected, a decimal value is displayed for Bayes.ProbA.ConditionalText after this line!!!
            txtAProb.Text = Bayes.ProbA.FormattedValue
            'Debug.Print("1h. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("1h. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)

            'Debug.Print("2. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("2. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)

            Bayes.ZeroProbRegion.NotA = False
            rbNotA.Enabled = True
            rbNotA.ForeColor = SystemColors.ControlText
            txtNotASamps.Enabled = True
            txtNotASamps.BackColor = SystemColors.Control
            'txtNotASamps.Text = SampString((1 - Bayes.ProbA.Value) * Bayes.SampleSize)
            txtNotASamps.Text = Bayes.SampsNotA.FormattedValue
            txtNotAProb.Enabled = True
            txtNotAProb.BackColor = SystemColors.Control
            'txtNotAProb.Text = ProbString(1 - Bayes.ProbA.Value) 'Ensure this is set to the Event Not A probability.
            txtNotAProb.Text = Bayes.ProbNotA.FormattedValue

            'Debug.Print("3. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("3. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)

            Bayes.ZeroProbRegion.B = False
            rbB.Enabled = True
            rbB.ForeColor = SystemColors.ControlText
            txtBSamps.Enabled = True
            txtBSamps.BackColor = SystemColors.Control
            'txtBSamps.Text = SampString(Bayes.ProbB.Value * Bayes.SampleSize)
            txtBSamps.Text = Bayes.SampsB.FormattedValue
            txtBProb.Enabled = True
            txtBProb.BackColor = SystemColors.Control
            'txtBProb.Text = ProbString(Bayes.ProbB.Value) 'Ensure this is set to the Event B probability.
            txtBProb.Text = Bayes.ProbB.FormattedValue

            'Debug.Print("4. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("4. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)

            Bayes.ZeroProbRegion.NotB = False
            rbNotB.Enabled = True
            rbNotB.ForeColor = SystemColors.ControlText
            txtNotBSamps.Enabled = True
            txtNotBSamps.BackColor = SystemColors.Control
            'txtNotBSamps.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize)
            txtNotBSamps.Text = Bayes.SampsNotB.FormattedValue
            txtNotBProb.Enabled = True
            txtNotBProb.BackColor = SystemColors.Control
            'txtNotBProb.Text = ProbString(1 - Bayes.ProbB.Value) 'Ensure this is set to the Event Not B probability.
            txtNotBProb.Text = Bayes.ProbNotB.FormattedValue

            Bayes.ZeroProbRegion.AandB = False
            rbAandB.Enabled = True
            rbAandB.ForeColor = SystemColors.ControlText
            txtAandBSamps.Enabled = True
            txtAandBSamps.BackColor = SystemColors.Control
            'txtAandBSamps.Text = SampString(Bayes.ProbAandB * Bayes.SampleSize)
            'txtAandBSamps.Text = SampString(Bayes.ProbAandB.Value * Bayes.SampleSize)
            txtAandBSamps.Text = Bayes.SampsAandB.FormattedValue
            txtAandBProb.Enabled = True
            txtAandBProb.BackColor = SystemColors.Control
            'txtAandBProb.Text = ProbString(Bayes.ProbAandB.Value) 'Ensure this is set to the Event A and Event B probability.
            txtAandBProb.Text = Bayes.ProbAandB.FormattedValue

            Bayes.ZeroProbRegion.AandNotB = False
            rbAandNotB.Enabled = True
            rbAandNotB.ForeColor = SystemColors.ControlText
            txtAandNotBSamps.Enabled = True
            txtAandNotBSamps.BackColor = SystemColors.Control
            'txtAandNotBSamps.Text = SampString((Bayes.ProbA.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtAandNotBSamps.Text = Bayes.SampsAandNotB.FormattedValue
            txtAandNotBProb.Enabled = True
            txtAandNotBProb.BackColor = SystemColors.Control
            'txtAandNotBProb.Text = ProbString(Bayes.ProbA.Value - Bayes.ProbAandB.Value) 'Ensure this is set to the Event A and Event Not B probability.
            txtAandNotBProb.Text = Bayes.ProbAandNotB.FormattedValue

            Bayes.ZeroProbRegion.NotAandB = False
            rbNotAandB.Enabled = True
            rbNotAandB.ForeColor = SystemColors.ControlText
            txtNotAandBSamps.Enabled = True
            txtNotAandBSamps.BackColor = SystemColors.Control
            'txtNotAandBSamps.Text = SampString((Bayes.ProbB.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtNotAandBSamps.Text = Bayes.SampsNotAandB.FormattedValue
            txtNotAandBProb.Enabled = True
            txtNotAandBProb.BackColor = SystemColors.Control
            'txtNotAandBProb.Text = ProbString(Bayes.ProbB.Value - Bayes.ProbAandB.Value) 'Ensure this is set to the Event Not A and Event B probability.
            txtNotAandBProb.Text = Bayes.ProbNotAandB.FormattedValue

            Bayes.ZeroProbRegion.NotAandNotB = False
            rbNotAandNotB.Enabled = True
            rbNotAandNotB.ForeColor = SystemColors.ControlText
            txtNotAandNotBSamps.Enabled = True
            txtNotAandNotBSamps.BackColor = SystemColors.Control
            'txtNotAandNotBSamps.Text = SampString((1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtNotAandNotBSamps.Text = Bayes.SampsNotAandNotB.FormattedValue
            txtNotAandNotBProb.Enabled = True
            txtNotAandNotBProb.BackColor = SystemColors.Control
            'txtNotAandNotBProb.Text = ProbString(1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) 'Ensure this is set to the Event Not A and Event Not B probability.
            txtNotAandNotBProb.Text = Bayes.ProbNotAandNotB.FormattedValue

            'Debug.Print("5. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("5. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)

            txtConditionalSampSize.Text = SampString(Bayes.SampleSize.Value) 'Set the constrained Sample Size to the total number of Samples.
            txtSampleSize_Cat.Text = SampString(Bayes.SampleSize.Value) 'Set the Sample Size to the total number of Samples.

            'Debug.Print("6. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("6. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
            'Debug.Print("DrawDiagram.")
            DrawDiagram()
            'Debug.Print("7. Bayes.ProbA.ConditionalText = " & Bayes.ProbA.ConditionalText)
            'Debug.Print("7. Bayes.ProbA.Label.Text" & Bayes.ProbA.Label.Text)
        End If
    End Sub

    Private Sub rbConditionA_CheckedChanged(sender As Object, e As EventArgs) Handles rbConditionA.CheckedChanged
        'Set the probabilities to match the Event A condition.

        If rbConditionA.Checked Then 'Event A is True
            If Bayes.EventA.Name.EndsWith(".") Then
                txtCondition.Text = "Event A is True:" & vbCrLf & Bayes.EventA.Name & "."
            Else
                txtCondition.Text = "Event A is True:" & vbCrLf & Bayes.EventA.Name
            End If


            rbLabelCondA.Checked = True
            'txtConditionLabel.Text = Bayes.AnnotConditionLabelATrue
            txtConditionLabel.Text = Bayes.AnnotCondition.EventATrue.Text

            If rbConditionA.Focused Then
                Bayes.Settings.Condition = "EventATrue" 'Only change this if the user has clicked the radio button.

            End If

            Bayes.ZeroProbRegion.A = False
            rbA.Enabled = True
            rbA.ForeColor = SystemColors.ControlText
            txtASamps.Enabled = True
            txtASamps.BackColor = SystemColors.Control
            'txtASamps.Text = SampString(Bayes.ProbA.Value * Bayes.SampleSize)
            txtASamps.Text = Bayes.SampsA.GivenA.FormattedValue
            txtAProb.Enabled = True
            txtAProb.BackColor = SystemColors.Control
            'txtAProb.Text = ProbString(1) 'Event A is True so the probability is set to 1.
            txtAProb.Text = Bayes.ProbA.GivenA.FormattedValue 'Event A is True so the probability is set to 1.

            Bayes.ZeroProbRegion.NotA = False
            rbNotA.Enabled = False
            rbNotA.ForeColor = SystemColors.ControlLight
            txtNotASamps.Enabled = False
            txtNotASamps.BackColor = SystemColors.ControlLightLight
            'txtNotASamps.Text = SampString(0) 'Event Not A is False so the number of samples is set to 0. 'UPDATE: SAMPLE COUNTS REMAIN UNCHANGED
            'txtNotASamps.Text = SampString((1 - Bayes.ProbA.Value) * Bayes.SampleSize) 'Leave the survey sample counts unchanged.
            txtNotASamps.Text = Bayes.SampsNotA.GivenA.FormattedValue
            txtNotAProb.Enabled = False
            txtNotAProb.BackColor = SystemColors.ControlLightLight
            'txtNotAProb.Text = ProbString(0) 'Event Not A is False so the probability is set to 0.
            txtNotAProb.Text = Bayes.ProbNotA.GivenA.FormattedValue 'Event Not A is False so the probability is set to 0.

            Bayes.ZeroProbRegion.B = False
            rbB.Enabled = True
            rbB.ForeColor = SystemColors.ControlText
            txtBSamps.Enabled = True
            txtBSamps.BackColor = SystemColors.Control
            'txtBSamps.Text = SampString(Bayes.EventBProb * Bayes.SampleSize)
            'txtBSamps.Text = SampString(Bayes.ProbAandB * Bayes.SampleSize)
            'txtBSamps.Text = SampString(Bayes.ProbB.Value * Bayes.SampleSize) 'Leave the survey sample counts unchanged.
            txtBSamps.Text = Bayes.SampsB.GivenA.FormattedValue
            txtBProb.Enabled = True
            txtBProb.BackColor = SystemColors.Control
            'txtBProb.Text = ProbString(Bayes.EventBProb) 'Ensure this is set to the Event B probability.
            'txtBProb.Text = ProbString(Bayes.ProbAandB.Value / Bayes.ProbA.Value)
            txtBProb.Text = Bayes.ProbB.GivenA.FormattedValue

            Bayes.ZeroProbRegion.NotB = False
            rbNotB.Enabled = True
            rbNotB.ForeColor = SystemColors.ControlText
            txtNotBSamps.Enabled = True
            txtNotBSamps.BackColor = SystemColors.Control
            'txtNotBSamps.Text = SampString((1 - Bayes.EventBProb) * Bayes.SampleSize)
            'txtNotBSamps.Text = SampString((Bayes.EventAProb - Bayes.ProbAandB) * Bayes.SampleSize)
            'txtNotBSamps.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize) 'Leave the survey sample counts unchanged.
            txtNotBSamps.Text = Bayes.SampsNotB.GivenA.FormattedValue
            txtNotBProb.Enabled = True
            txtNotBProb.BackColor = SystemColors.Control
            'txtNotBProb.Text = ProbString(1 - Bayes.EventBProb) 'Ensure this is set to the Event Not B probability.
            'txtNotBProb.Text = ProbString((Bayes.ProbA.Value - Bayes.ProbAandB.Value) / Bayes.ProbA.Value)
            txtNotBProb.Text = Bayes.ProbNotB.GivenA.FormattedValue

            Bayes.ZeroProbRegion.AandB = False
            rbAandB.Enabled = True
            rbAandB.ForeColor = SystemColors.ControlText
            txtAandBSamps.Enabled = True
            txtAandBSamps.BackColor = SystemColors.Control
            'txtAandBSamps.Text = SampString(Bayes.ProbAandB.Value * Bayes.SampleSize)
            txtAandBSamps.Text = Bayes.SampsAandB.GivenA.FormattedValue
            txtAandBProb.Enabled = True
            txtAandBProb.BackColor = SystemColors.Control
            'txtAandBProb.Text = ProbString(Bayes.ProbAandB) 'Ensure this is set to the Event A and Event B probability.
            'txtAandBProb.Text = ProbString(Bayes.ProbAandB.Value / Bayes.ProbA.Value)
            txtAandBProb.Text = Bayes.ProbAandB.GivenA.FormattedValue

            Bayes.ZeroProbRegion.AandNotB = False
            rbAandNotB.Enabled = True
            rbAandNotB.ForeColor = SystemColors.ControlText
            txtAandNotBSamps.Enabled = True
            txtAandNotBSamps.BackColor = SystemColors.Control
            'txtAandNotBSamps.Text = SampString(Bayes.SampleSize * (Bayes.EventAProb - Bayes.ProbAandB) / Bayes.EventAProb)
            'txtAandNotBSamps.Text = SampString((Bayes.ProbA.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize) 'Leave the survey sample counts unchanged.
            txtAandNotBSamps.Text = Bayes.SampsAandNotB.GivenA.FormattedValue
            txtAandNotBProb.Enabled = True
            txtAandNotBProb.BackColor = SystemColors.Control
            'txtAandNotBProb.Text = ProbString(Bayes.EventAProb - Bayes.ProbAandB) 'Ensure this is set to the Event A and Event Not B probability.
            'txtAandNotBProb.Text = ProbString((Bayes.ProbA.Value - Bayes.ProbAandB.Value) / Bayes.ProbA.Value)
            txtAandNotBProb.Text = Bayes.ProbAandNotB.GivenA.FormattedValue

            Bayes.ZeroProbRegion.NotAandB = True
            rbNotAandB.Enabled = False
            rbNotAandB.ForeColor = SystemColors.ControlLight
            txtNotAandBSamps.Enabled = False
            txtNotAandBSamps.BackColor = SystemColors.ControlLightLight
            'txtNotAandBSamps.Text = SampString(0)
            'txtNotAandBSamps.Text = SampString((Bayes.ProbB.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize) 'Leave the survey sample counts unchanged.
            txtNotAandBSamps.Text = Bayes.SampsNotAandB.GivenA.FormattedValue
            txtNotAandBProb.Enabled = False
            txtNotAandBProb.BackColor = SystemColors.ControlLightLight
            'txtNotAandBProb.Text = ProbString(Bayes.EventBProb - Bayes.ProbAandB) 'Ensure this is set to the Event Not A and Event B probability.
            'txtNotAandBProb.Text = ProbString(0)
            txtNotAandBProb.Text = Bayes.ProbNotAandB.GivenA.FormattedValue

            Bayes.ZeroProbRegion.NotAandNotB = True
            rbNotAandNotB.Enabled = False
            rbNotAandNotB.ForeColor = SystemColors.ControlLight
            txtNotAandNotBSamps.Enabled = False
            txtNotAandNotBSamps.BackColor = SystemColors.ControlLightLight
            'txtNotAandNotBSamps.Text = SampString(0)
            'txtNotAandNotBSamps.Text = SampString((1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtNotAandNotBSamps.Text = Bayes.SampsNotAandNotB.GivenA.FormattedValue
            txtNotAandNotBProb.Enabled = False
            txtNotAandNotBProb.BackColor = SystemColors.ControlLightLight
            'txtNotAandNotBProb.Text = ProbString(1 - Bayes.EventAProb - Bayes.EventBProb + Bayes.ProbAandB) 'Ensure this is set to the Event Not A and Event Not B probability.
            'txtNotAandNotBProb.Text = ProbString(0)
            txtNotAandNotBProb.Text = Bayes.ProbNotAandNotB.GivenA.FormattedValue

            txtConditionalSampSize.Text = SampString(Bayes.ProbA.Value * Bayes.SampleSize.Value) 'Set the constrained Sample Size to the number of Event A Samples.
            'txtSampleSize_Cat.Text = SampString(Bayes.EventAProb * Bayes.SampleSize) 'Set the Sample Size to the number of Event A Samples.
            txtSampleSize_Cat.Text = SampString(Bayes.SampleSize.Value)

            DrawDiagram()
        End If
    End Sub

    Private Sub rbConditionNotA_CheckedChanged(sender As Object, e As EventArgs) Handles rbConditionNotA.CheckedChanged
        'Set the probabilities to match the Event A condition.

        If rbConditionNotA.Checked Then 'Event A is False
            If Bayes.EventA.NotName.EndsWith(".") Then
                txtCondition.Text = "Event A is False:" & vbCrLf & Bayes.EventA.NotName
            Else
                txtCondition.Text = "Event A is False:" & vbCrLf & Bayes.EventA.NotName & "."
            End If

            rbLabelCondNotA.Checked = True

            'txtConditionLabel.Text = Bayes.AnnotConditionLabelAFalse
            'txtConditionLabel.Text = Bayes.AnnotCondition.GivenNotA.Text
            txtConditionLabel.Text = Bayes.AnnotCondition.EventAFalse.Text
            If rbConditionNotA.Focused Then Bayes.Settings.Condition = "EventAFalse" 'Only change this if the user has clicked the radio button.

            Bayes.ZeroProbRegion.A = False
            rbA.Enabled = False
            rbA.ForeColor = SystemColors.ControlLight
            txtASamps.Enabled = False
            txtASamps.BackColor = SystemColors.ControlLightLight
            'txtASamps.Text = SampString(Bayes.ProbA.Value * Bayes.SampleSize)
            txtASamps.Text = Bayes.SampsA.GivenNotA.FormattedValue
            txtAProb.Enabled = False
            txtAProb.BackColor = SystemColors.ControlLightLight
            'txtAProb.Text = ProbString(0) 'Event A is False so the probability is set to 0.
            txtAProb.Text = Bayes.ProbA.GivenNotA.FormattedValue

            Bayes.ZeroProbRegion.NotA = False
            rbNotA.Enabled = True
            rbNotA.ForeColor = SystemColors.ControlText
            txtNotASamps.Enabled = True
            txtNotASamps.BackColor = SystemColors.Control
            'txtNotASamps.Text = SampString((1 - Bayes.ProbA.Value) * Bayes.SampleSize)
            txtNotASamps.Text = Bayes.SampsNotA.GivenNotA.FormattedValue
            txtNotAProb.Enabled = True
            txtNotAProb.BackColor = SystemColors.Control
            'txtNotAProb.Text = ProbString(1) 'Event Not A is True so the probability is set to 1.
            txtNotAProb.Text = Bayes.ProbNotA.GivenNotA.FormattedValue

            Bayes.ZeroProbRegion.B = False
            rbB.Enabled = True
            rbB.ForeColor = SystemColors.ControlText
            txtBSamps.Enabled = True
            txtBSamps.BackColor = SystemColors.Control
            'txtBSamps.Text = SampString(Bayes.ProbB.Value * Bayes.SampleSize)
            txtBSamps.Text = Bayes.SampsB.GivenNotA.FormattedValue
            txtBProb.Enabled = True
            txtBProb.BackColor = SystemColors.Control
            'txtBProb.Text = ProbString(Bayes.ProbB.Value) 'Ensure this is set to the Event B probability.
            txtBProb.Text = Bayes.ProbB.GivenNotA.FormattedValue

            Bayes.ZeroProbRegion.NotB = False
            rbNotB.Enabled = True
            rbNotB.ForeColor = SystemColors.ControlText
            txtNotBSamps.Enabled = True
            txtNotBSamps.BackColor = SystemColors.Control
            'txtNotBSamps.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize)
            txtNotBSamps.Text = Bayes.SampsNotB.GivenNotA.FormattedValue
            txtNotBProb.Enabled = True
            txtNotBProb.BackColor = SystemColors.Control
            'txtNotBProb.Text = ProbString(1 - Bayes.ProbB.Value) 'Ensure this is set to the Event Not B probability.
            txtNotBProb.Text = Bayes.ProbNotB.GivenNotA.FormattedValue

            Bayes.ZeroProbRegion.AandB = True
            rbAandB.Enabled = False
            rbAandB.ForeColor = SystemColors.ControlLight
            txtAandBSamps.Enabled = False
            txtAandBSamps.BackColor = SystemColors.ControlLightLight
            'txtAandBSamps.Text = SampString(Bayes.ProbAandB.Value * Bayes.SampleSize)
            txtAandBSamps.Text = Bayes.SampsAandB.GivenNotA.FormattedValue
            txtAandBProb.Enabled = False
            txtAandBProb.BackColor = SystemColors.ControlLightLight
            txtAandBProb.Text = Bayes.ProbAandB.GivenNotA.FormattedValue

            Bayes.ZeroProbRegion.AandNotB = True
            rbAandNotB.Enabled = False
            rbAandNotB.ForeColor = SystemColors.ControlLight
            txtAandNotBSamps.Enabled = False
            txtAandNotBSamps.BackColor = SystemColors.ControlLightLight
            'txtAandNotBSamps.Text = SampString((Bayes.ProbA.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtAandNotBSamps.Text = Bayes.SampsAandNotB.GivenNotA.FormattedValue
            txtAandNotBProb.Enabled = False
            txtAandNotBProb.BackColor = SystemColors.ControlLightLight
            txtAandNotBProb.Text = Bayes.ProbAandNotB.GivenNotA.FormattedValue

            Bayes.ZeroProbRegion.NotAandB = False
            rbNotAandB.Enabled = True
            rbNotAandB.ForeColor = SystemColors.ControlText
            txtNotAandBSamps.Enabled = True
            txtNotAandBSamps.BackColor = SystemColors.Control
            'txtNotAandBSamps.Text = SampString((Bayes.ProbB.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtNotAandBSamps.Text = Bayes.SampsNotAandB.GivenNotA.FormattedValue
            txtNotAandBProb.Enabled = True
            txtNotAandBProb.BackColor = SystemColors.Control
            txtNotAandBProb.Text = Bayes.ProbNotAandB.GivenNotA.FormattedValue

            Bayes.ZeroProbRegion.NotAandNotB = False
            rbNotAandNotB.Enabled = True
            rbNotAandNotB.ForeColor = SystemColors.ControlText
            txtNotAandNotBSamps.Enabled = True
            txtNotAandNotBSamps.BackColor = SystemColors.Control
            'txtNotAandNotBSamps.Text = SampString((1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtNotAandNotBSamps.Text = Bayes.SampsNotAandNotB.GivenNotA.FormattedValue
            txtNotAandNotBProb.Enabled = True
            txtNotAandNotBProb.BackColor = SystemColors.Control
            txtNotAandNotBProb.Text = Bayes.ProbNotAandNotB.GivenNotA.FormattedValue

            txtConditionalSampSize.Text = SampString((1 - Bayes.ProbA.Value) * Bayes.SampleSize.Value) 'Set the constrained Sample Size to the number of Event Not A Samples.
            'txtSampleSize_Cat.Text = SampString((1 - Bayes.EventAProb) * Bayes.SampleSize) 'Set the Sample Size to the number of Event Not A Samples.
            txtSampleSize_Cat.Text = SampString(Bayes.SampleSize.Value)

            DrawDiagram()
        End If
    End Sub

    Private Sub rbConditionB_CheckedChanged(sender As Object, e As EventArgs) Handles rbConditionB.CheckedChanged
        'Set the probabilities to match the Event B condition.

        If rbConditionB.Checked Then 'Event B is True
            If Bayes.EventB.Name.EndsWith(".") Then
                txtCondition.Text = "Event B is True:" & vbCrLf & Bayes.EventB.Name
            Else
                txtCondition.Text = "Event B is True:" & vbCrLf & Bayes.EventB.Name & "."
            End If

            rbLabelCondB.Checked = True

            'txtConditionLabel.Text = Bayes.AnnotConditionLabelBTrue
            'txtConditionLabel.Text = Bayes.AnnotCondition.GivenB.MidY
            txtConditionLabel.Text = Bayes.AnnotCondition.EventBTrue.Text
            If rbConditionB.Focused Then Bayes.Settings.Condition = "EventBTrue" 'Only change this if the user has clicked the radio button.

            Bayes.ZeroProbRegion.A = False
            rbA.Enabled = True
            rbA.ForeColor = SystemColors.ControlText
            txtASamps.Enabled = True
            txtASamps.BackColor = SystemColors.Control
            'txtASamps.Text = SampString(Bayes.ProbA.Value * Bayes.SampleSize)
            txtASamps.Text = Bayes.SampsA.GivenB.FormattedValue
            txtAProb.Enabled = True
            txtAProb.BackColor = SystemColors.Control
            'txtAProb.Text = ProbString(Bayes.ProbA.Value) 'Ensure this is set to the Event A probability.
            txtAProb.Text = Bayes.ProbA.GivenB.FormattedValue

            Bayes.ZeroProbRegion.NotA = False
            rbNotA.Enabled = True
            rbNotA.ForeColor = SystemColors.ControlText
            txtNotASamps.Enabled = True
            txtNotASamps.BackColor = SystemColors.Control
            'txtNotASamps.Text = SampString((1 - Bayes.ProbA.Value) * Bayes.SampleSize)
            txtNotASamps.Text = Bayes.SampsNotA.GivenB.FormattedValue
            txtNotAProb.Enabled = True
            txtNotAProb.BackColor = SystemColors.Control
            'txtNotAProb.Text = ProbString(1 - Bayes.ProbA.Value) 'Ensure this is set to the Event Not A probability.
            txtNotAProb.Text = Bayes.ProbNotA.GivenB.FormattedValue

            Bayes.ZeroProbRegion.B = False
            rbB.Enabled = True
            rbB.ForeColor = SystemColors.ControlText
            txtBSamps.Enabled = True
            txtBSamps.BackColor = SystemColors.Control
            'txtBSamps.Text = SampString(Bayes.ProbB.Value * Bayes.SampleSize)
            txtBSamps.Text = Bayes.SampsB.GivenB.FormattedValue
            txtBProb.Enabled = True
            txtBProb.BackColor = SystemColors.Control
            'txtBProb.Text = ProbString(1) 'Event B is True so the probability is set to 1.
            txtBProb.Text = Bayes.ProbB.GivenB.FormattedValue

            Bayes.ZeroProbRegion.NotB = False
            rbNotB.Enabled = False
            rbNotB.ForeColor = SystemColors.ControlLight
            txtNotBSamps.Enabled = False
            txtNotBSamps.BackColor = SystemColors.ControlLightLight
            'txtNotBSamps.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize)
            txtNotBSamps.Text = Bayes.SampsNotB.GivenB.FormattedValue
            txtNotBProb.Enabled = False
            txtNotBProb.BackColor = SystemColors.ControlLightLight
            'txtNotBProb.Text = ProbString(0) 'Event Not B is False so the probability is set to 0.
            txtNotBProb.Text = Bayes.ProbNotB.GivenB.FormattedValue

            Bayes.ZeroProbRegion.AandB = False
            rbAandB.Enabled = True
            rbAandB.ForeColor = SystemColors.ControlText
            txtAandBSamps.Enabled = True
            txtAandBSamps.BackColor = SystemColors.Control
            'txtAandBSamps.Text = SampString(Bayes.ProbAandB.Value * Bayes.SampleSize)
            txtAandBSamps.Text = Bayes.SampsAandB.GivenB.FormattedValue
            txtAandBProb.Enabled = True
            txtAandBProb.BackColor = SystemColors.Control
            txtAandBProb.Text = Bayes.ProbAandB.GivenB.FormattedValue

            Bayes.ZeroProbRegion.AandNotB = True
            rbAandNotB.Enabled = False
            rbAandNotB.ForeColor = SystemColors.ControlLight
            txtAandNotBSamps.Enabled = False
            txtAandNotBSamps.BackColor = SystemColors.ControlLightLight
            'txtAandNotBSamps.Text = SampString((Bayes.ProbA.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtAandNotBSamps.Text = Bayes.SampsAandNotB.GivenB.FormattedValue
            txtAandNotBProb.Enabled = False
            txtAandNotBProb.BackColor = SystemColors.ControlLightLight
            txtAandNotBProb.Text = Bayes.ProbAandNotB.GivenB.FormattedValue

            Bayes.ZeroProbRegion.NotAandB = False
            rbNotAandB.Enabled = True
            rbNotAandB.ForeColor = SystemColors.ControlText
            txtNotAandBSamps.Enabled = True
            txtNotAandBSamps.BackColor = SystemColors.Control
            'txtNotAandBSamps.Text = SampString((Bayes.ProbB.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtNotAandBSamps.Text = Bayes.SampsNotAandB.GivenB.FormattedValue
            txtNotAandBProb.Enabled = True
            txtNotAandBProb.BackColor = SystemColors.Control
            txtNotAandBProb.Text = Bayes.ProbNotAandB.GivenB.FormattedValue

            Bayes.ZeroProbRegion.NotAandNotB = True
            rbNotAandNotB.Enabled = False
            rbNotAandNotB.ForeColor = SystemColors.ControlLight
            txtNotAandNotBSamps.Enabled = False
            txtNotAandNotBSamps.BackColor = SystemColors.ControlLightLight
            'txtNotAandNotBSamps.Text = SampString((1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtNotAandNotBSamps.Text = Bayes.SampsNotAandNotB.GivenB.FormattedValue
            txtNotAandNotBProb.Enabled = False
            txtNotAandNotBProb.BackColor = SystemColors.ControlLightLight
            txtNotAandNotBProb.Text = Bayes.ProbNotAandNotB.GivenB.FormattedValue

            txtConditionalSampSize.Text = SampString(Bayes.ProbB.Value * Bayes.SampleSize.Value) 'Set the Sample Size to the number of Event B Samples.
            'txtSampleSize_Cat.Text = SampString(Bayes.EventBProb * Bayes.SampleSize) 'Set the Sample Size to the number of Event B Samples.
            txtSampleSize_Cat.Text = SampString(Bayes.SampleSize.Value)

            DrawDiagram()
        End If
    End Sub

    Private Sub rbConditionNotB_CheckedChanged(sender As Object, e As EventArgs) Handles rbConditionNotB.CheckedChanged
        If rbConditionNotB.Checked Then
            If Bayes.EventB.NotName.EndsWith(".") Then
                txtCondition.Text = "Event B is False:" & vbCrLf & Bayes.EventB.NotName
            Else
                txtCondition.Text = "Event B is False:" & vbCrLf & Bayes.EventB.NotName & "."
            End If

            rbLabelCondNotB.Checked = True

            'txtConditionLabel.Text = Bayes.AnnotConditionLabelBFalse
            'txtConditionLabel.Text = Bayes.AnnotCondition.GivenNotB.Text
            txtConditionLabel.Text = Bayes.AnnotCondition.EventBFalse.Text
            If rbConditionNotB.Focused Then Bayes.Settings.Condition = "EventBFalse" 'Only change this if the user has clicked the radio button.

            Bayes.ZeroProbRegion.A = False
            rbA.Enabled = True
            rbA.ForeColor = SystemColors.ControlText
            txtASamps.Enabled = True
            txtASamps.BackColor = SystemColors.Control
            'txtASamps.Text = SampString(Bayes.ProbA.Value * Bayes.SampleSize)
            txtASamps.Text = Bayes.SampsA.GivenNotB.FormattedValue
            txtAProb.Enabled = True
            txtAProb.BackColor = SystemColors.Control
            'txtAProb.Text = ProbString(Bayes.ProbA.Value) 'Ensure this is set to the Event A probability. 'THIS LINE MESSES THE PROBABILITY FORMATTING!!!
            txtAProb.Text = Bayes.ProbA.GivenNotB.FormattedValue

            Bayes.ZeroProbRegion.NotA = False
            rbNotA.Enabled = True
            rbNotA.ForeColor = SystemColors.ControlText
            txtNotASamps.Enabled = True
            txtNotASamps.BackColor = SystemColors.Control
            'txtNotASamps.Text = SampString((1 - Bayes.ProbA.Value) * Bayes.SampleSize)
            txtNotASamps.Text = Bayes.SampsNotA.GivenNotB.FormattedValue
            txtNotAProb.Enabled = True
            txtNotAProb.BackColor = SystemColors.Control
            'txtNotAProb.Text = ProbString(1 - Bayes.ProbA.Value) 'Ensure this is set to the Event Not A probability.
            txtNotAProb.Text = Bayes.ProbNotA.GivenNotB.FormattedValue

            Bayes.ZeroProbRegion.B = False
            rbB.Enabled = False
            rbB.ForeColor = SystemColors.ControlLight
            txtBSamps.Enabled = False
            txtBSamps.BackColor = SystemColors.ControlLightLight
            'txtBSamps.Text = SampString(Bayes.ProbB.Value * Bayes.SampleSize)
            txtBSamps.Text = Bayes.SampsB.GivenNotB.FormattedValue
            txtBProb.Enabled = False
            txtBProb.BackColor = SystemColors.ControlLightLight
            'txtBProb.Text = ProbString(0) 'Event B is False so the probability is set to 0.
            txtBProb.Text = Bayes.ProbB.GivenNotB.FormattedValue

            Bayes.ZeroProbRegion.NotB = False
            rbNotB.Enabled = True
            rbNotB.ForeColor = SystemColors.ControlText
            txtNotBSamps.Enabled = True
            txtNotBSamps.BackColor = SystemColors.Control
            'txtNotBSamps.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize)
            txtNotBSamps.Text = Bayes.SampsNotB.GivenNotB.FormattedValue
            txtNotBProb.Enabled = True
            txtNotBProb.BackColor = SystemColors.Control
            'txtNotBProb.Text = ProbString(1) 'Event Not B is True so the probability is set to 1.
            txtNotBProb.Text = Bayes.ProbNotB.GivenNotB.FormattedValue

            Bayes.ZeroProbRegion.AandB = True
            rbAandB.Enabled = False
            rbAandB.ForeColor = SystemColors.ControlLight
            txtAandBSamps.Enabled = False
            txtAandBSamps.BackColor = SystemColors.ControlLightLight
            'txtAandBSamps.Text = SampString(Bayes.ProbAandB.Value * Bayes.SampleSize)
            txtAandBSamps.Text = Bayes.SampsAandB.GivenNotB.FormattedValue
            txtAandBProb.Enabled = False
            txtAandBProb.BackColor = SystemColors.ControlLightLight
            txtAandBProb.Text = Bayes.ProbAandB.GivenNotB.FormattedValue

            Bayes.ZeroProbRegion.AandNotB = False
            rbAandNotB.Enabled = True
            rbAandNotB.ForeColor = SystemColors.ControlText
            txtAandNotBSamps.Enabled = True
            txtAandNotBSamps.BackColor = SystemColors.Control
            'txtAandNotBSamps.Text = SampString((Bayes.ProbA.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtAandNotBSamps.Text = Bayes.SampsAandNotB.GivenNotB.FormattedValue
            txtAandNotBProb.Enabled = True
            txtAandNotBProb.BackColor = SystemColors.Control
            txtAandNotBProb.Text = Bayes.ProbAandNotB.GivenNotB.FormattedValue

            Bayes.ZeroProbRegion.NotAandB = True
            rbNotAandB.Enabled = False
            rbNotAandB.ForeColor = SystemColors.ControlLight
            txtNotAandBSamps.Enabled = False
            txtNotAandBSamps.BackColor = SystemColors.ControlLightLight
            'txtNotAandBSamps.Text = SampString((Bayes.ProbB.Value - Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtNotAandBSamps.Text = Bayes.SampsNotAandB.GivenNotB.FormattedValue 'UPDATE ALL SAMPLE DISPLAYS *********************************************
            txtNotAandBProb.Enabled = False
            txtNotAandBProb.BackColor = SystemColors.ControlLightLight
            txtNotAandBProb.Text = Bayes.ProbNotAandB.GivenNotB.FormattedValue 'UPDATE ALL PROBABILITY DISPLAYS ****************************************************

            Bayes.ZeroProbRegion.NotAandNotB = False
            rbNotAandNotB.Enabled = True
            rbNotAandNotB.ForeColor = SystemColors.ControlText
            txtNotAandNotBSamps.Enabled = True
            txtNotAandNotBSamps.BackColor = SystemColors.Control
            'txtNotAandNotBSamps.Text = SampString((1 - Bayes.ProbA.Value - Bayes.ProbB.Value + Bayes.ProbAandB.Value) * Bayes.SampleSize)
            txtNotAandNotBSamps.Text = Bayes.SampsNotAandNotB.GivenNotB.FormattedValue
            txtNotAandNotBProb.Enabled = True
            txtNotAandNotBProb.BackColor = SystemColors.Control
            txtNotAandNotBProb.Text = Bayes.ProbNotAandNotB.GivenNotB.FormattedValue

            txtConditionalSampSize.Text = SampString((1 - Bayes.ProbB.Value) * Bayes.SampleSize.Value) 'Set the Sample Size to the number of Event Not B Samples.
            'txtSampleSize_Cat.Text = SampString((1 - Bayes.EventBProb) * Bayes.SampleSize) 'Set the Sample Size to the number of Event Not B Samples.
            txtSampleSize_Cat.Text = SampString(Bayes.SampleSize.Value)

            DrawDiagram()
        End If
    End Sub

    Private Sub chkShowConditionLabel_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowConditionLabel.CheckedChanged
        'The Show Condition Label check box selection has changed.
        If chkShowConditionLabel.Focused Then
            'Bayes.AnnotConditionShow = chkShowConditionLabel.Checked
            Bayes.AnnotCondition.Show = chkShowConditionLabel.Checked
            DrawDiagram()
        End If
    End Sub

    Private Sub txtConditionLabel_TextChanged(sender As Object, e As EventArgs) Handles txtConditionLabel.TextChanged

    End Sub

    Private Sub txtConditionLabel_MouseUp(sender As Object, e As MouseEventArgs) Handles txtConditionLabel.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtConditionLabel_LostFocus(sender As Object, e As EventArgs) Handles txtConditionLabel.LostFocus
        'Select Case Bayes.Condition 'None, EventATrue, EventAFalse, EventBTrue, EventBFalse
        '    Case "None"
        '        'Bayes.AnnotContraintLabelNone = txtConditionLabel.Text
        '        Bayes.AnnotCondition.Text = txtConditionLabel.Text
        '    Case "EventATrue"
        '        'Bayes.AnnotConditionLabelATrue = txtConditionLabel.Text
        '        Bayes.AnnotCondition.GivenA.Text = txtConditionLabel.Text
        '    Case "EventAFalse"
        '        'Bayes.AnnotConditionLabelAFalse = txtConditionLabel.Text
        '        Bayes.AnnotCondition.GivenNotA.Text = txtConditionLabel.Text
        '    Case "EventBTrue"
        '        'Bayes.AnnotConditionLabelBTrue = txtConditionLabel.Text
        '        Bayes.AnnotCondition.GivenB.Text = txtConditionLabel.Text
        '    Case "EventBFalse"
        '        'Bayes.AnnotConditionLabelBFalse = txtConditionLabel.Text
        '        Bayes.AnnotCondition.GivenNotB.Text = txtConditionLabel.Text
        '    Case Else
        '        Message.AddWarning("Unknown category condition: " & Bayes.Condition & vbCrLf)
        'End Select
        Bayes.AnnotCondition.Text = txtConditionLabel.Text 'The correct version of the condition text will be updated.
    End Sub

    Private Sub txtConditionLabelX_TextChanged(sender As Object, e As EventArgs) Handles txtConditionLabelX.TextChanged

    End Sub

    Private Sub txtConditionLabelX_LostFocus(sender As Object, e As EventArgs) Handles txtConditionLabelX.LostFocus
        'Bayes.AnnotConditionLabelX = txtConditionLabelX.Text
        Bayes.AnnotCondition.X = txtConditionLabelX.Text
    End Sub

    Private Sub txtConditionLabelY_TextChanged(sender As Object, e As EventArgs) Handles txtConditionLabelY.TextChanged

    End Sub

    Private Sub txtConditionLabelY_LostFocus(sender As Object, e As EventArgs) Handles txtConditionLabelY.LostFocus
        Bayes.AnnotCondition.Y = txtConditionLabelY.Text
    End Sub

    'Use .FormattedValue instead. This function produces some incorrenct formatting!!!
    Private Function ProbString(ByRef DecimalProb As Double) As String
        'Return the formatted probability text corrersponding to the given decimal probability value.
        'The ProbabilityMeasure, DecimalFormat and PercentFormat settings in Bayes will be used for formatting.

        If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
            Return Format(DecimalProb, Bayes.Settings.DecimalFormat)
        ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
            Return Format(DecimalProb * 100, Bayes.Settings.PercentFormat) & "%"
        Else
            Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
        End If
    End Function

    Private Function SampString(ByRef Samples As Double) As String
        'Return the formatted sample count text corrersponding to the given sample count value.
        'The SamplesFormat setting in Bayes will be used for formatting.

        Return Format(Samples, Bayes.Settings.SamplesFormat)
    End Function

    Private Sub txtAnnotTitle_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotTitle.TextChanged

    End Sub

    Private Sub txtAnnotTitle_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotTitle.LostFocus
        Bayes.AnnotTitle.Text = txtAnnotTitle.Text
    End Sub

    Private Sub txtAnnotTitle_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotTitle.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub
    Private Sub txtAnnotDescr_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotDescr.TextChanged

    End Sub

    Private Sub txtAnnotDescr_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotDescr.LostFocus
        Bayes.AnnotDescr.Text = txtAnnotDescr.Text
    End Sub

    Private Sub txtAnnotDescr_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotDescr.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotEventALabel_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotEventALabel.TextChanged

    End Sub

    Private Sub txtAnnotEventALabel_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotEventALabel.LostFocus
        'Bayes.AnnotEventAText = txtAnnotEventALabel.Text
        'Bayes.AnnotEventA.Unscaled.Text = txtAnnotEventALabel.Text
        Bayes.AnnotEventA.Text = txtAnnotEventALabel.Text
        'Message.Add("txtAnnotEventALabel.LostFocus" & vbCrLf)
    End Sub

    Private Sub txtAnnotEventALabel_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotEventALabel.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotEventBLabel_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotEventBLabel.TextChanged

    End Sub
    Private Sub txtAnnotEventBLabel_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotEventBLabel.LostFocus
        Bayes.AnnotEventB.Text = txtAnnotEventBLabel.Text
        'Message.Add("txtAnnotEventBLabel.LostFocus" & vbCrLf)
    End Sub

    Private Sub txtAnnotEventBLabel_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotEventBLabel.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender '
        End If
    End Sub

    Private Sub ToolStripMenuItem_SelectFont_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_SelectFont.Click
        ContextMenuStrip2.Close()
        If ContextMenuStrip2.Tag Is Nothing Then

        Else
            Dim myControl As Windows.Forms.Control
            Try
                myControl = ContextMenuStrip2.Tag
                FontDialog1.Font = myControl.Font
                FontDialog1.ShowDialog()
                myControl.Font = FontDialog1.Font
                'Message.Add("Control name: " & myControl.Name & vbCrLf)
                Select Case myControl.Name
                    Case "txtAnnotTitle"
                        'Bayes.AnnotTitleFont = FontDialog1.Font
                        Bayes.AnnotTitle.Font = FontDialog1.Font
                    Case "txtAnnotDescr"
                        Bayes.AnnotDescr.Font = FontDialog1.Font
                    Case "txtAnnotEventALabel"
                        'Bayes.AnnotEventA.Unscaled.Font = FontDialog1.Font
                        Bayes.AnnotEventA.Font = FontDialog1.Font
                    Case "txtAnnotEventBLabel"
                        Bayes.AnnotEventB.Font = FontDialog1.Font
                    Case "txtAnnotProbA"
                        'Bayes.ProbAFont = FontDialog1.Font
                        Bayes.ProbA.Label.Font = FontDialog1.Font
                    Case "txtAnnotProbNotA"
                        Bayes.ProbNotA.Label.Font = FontDialog1.Font
                    Case "txtAnnotProbB"
                        Bayes.ProbB.Label.Font = FontDialog1.Font
                    Case "txtAnnotProbNotB"
                        Bayes.ProbNotB.Label.Font = FontDialog1.Font
                    Case "txtAnnotProbAandB"
                        Bayes.ProbAandB.Label.Font = FontDialog1.Font
                    Case "txtAnnotProbAandNotB"
                        Bayes.ProbAandNotB.Label.Font = FontDialog1.Font
                    Case "txtAnnotProbNotAandB"
                        Bayes.ProbNotAandB.Label.Font = FontDialog1.Font
                    Case "txtAnnotProbNotAandNotB"
                        Bayes.ProbNotAandNotB.Label.Font = FontDialog1.Font
                    Case "txtAnnotSampsA"
                        Bayes.SampsA.Label.Font = FontDialog1.Font
                    Case "txtAnnotSampsNotA"
                        Bayes.SampsNotA.Label.Font = FontDialog1.Font
                    Case "txtAnnotSampsB"
                        Bayes.SampsB.Label.Font = FontDialog1.Font
                    Case "txtAnnotSampsNotB"
                        Bayes.SampsNotB.Label.Font = FontDialog1.Font
                    Case "txtAnnotSampsAandB"
                        Bayes.SampsAandB.Label.Font = FontDialog1.Font
                    Case "txtAnnotSampsAandNotB"
                        Bayes.SampsAandNotB.Label.Font = FontDialog1.Font
                    Case "txtAnnotSampsNotAandB"
                        Bayes.SampsNotAandB.Label.Font = FontDialog1.Font
                    Case "txtAnnotSampsNotAandNotB"
                        Bayes.SampsNotAandNotB.Label.Font = FontDialog1.Font
                    Case "txtAnnotSampsSize"
                        'Bayes.SampsSizeFont = FontDialog1.Font
                        Bayes.AnnotSampleSize.Font = FontDialog1.Font
                    Case "txtConditionLabel"
                        'Bayes.AnnotConditionLabelFont = FontDialog1.Font
                        Bayes.AnnotCondition.Font = FontDialog1.Font
                    Case Else
                        Message.AddWarning("Unknown control name: " & myControl.Name & vbCrLf)
                End Select
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ToolStripMenuItem_SelectColor_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_SelectColor.Click
        ContextMenuStrip2.Close()
        If ContextMenuStrip2.Tag Is Nothing Then

        Else
            Dim myControl As Windows.Forms.Control
            Try
                myControl = ContextMenuStrip2.Tag
                ColorDialog1.Color = myControl.ForeColor
                ColorDialog1.ShowDialog()
                myControl.ForeColor = ColorDialog1.Color
                'Message.Add("Control name: " & myControl.Name & vbCrLf)
                Select Case myControl.Name
                    Case "txtAnnotTitle"
                        'Bayes.AnnotTitleColor = ColorDialog1.Color
                        Bayes.AnnotTitle.Color = ColorDialog1.Color
                    Case "txtAnnotDescr"
                        Bayes.AnnotDescr.Color = ColorDialog1.Color
                    Case "txtAnnotEventALabel"
                        'Bayes.AnnotEventAColor = ColorDialog1.Color
                        'Bayes.AnnotEventA.Unscaled.Color = ColorDialog1.Color
                        Bayes.AnnotEventA.Color = ColorDialog1.Color
                    Case "txtAnnotEventBLabel"
                        Bayes.AnnotEventB.Color = ColorDialog1.Color
                    Case "txtAnnotProbA"
                        Bayes.ProbA.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbNotA"
                        Bayes.ProbNotA.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbB"
                        Bayes.ProbB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbNotB"
                        Bayes.ProbNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbAandB"
                        Bayes.ProbAandB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbAandNotB"
                        Bayes.ProbAandNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbNotAandB"
                        Bayes.ProbNotAandB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbNotAandNotB"
                        Bayes.ProbNotAandNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsA"
                        Bayes.SampsA.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsNotA"
                        Bayes.SampsNotA.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsB"
                        Bayes.SampsB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsNotB"
                        Bayes.SampsNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsAandB"
                        Bayes.SampsAandB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsAandNotB"
                        Bayes.SampsAandNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsNotAandB"
                        Bayes.SampsNotAandB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsNotAandNotB"
                        Bayes.SampsNotAandNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsSize"
                        'Bayes.SampsSizeColor = ColorDialog1.Color
                        Bayes.AnnotSampleSize.Color = ColorDialog1.Color
                    Case "txtConditionLabel"
                        'Bayes.AnnotConditionLabelColor = ColorDialog1.Color
                        Bayes.AnnotCondition.Color = ColorDialog1.Color
                    Case Else
                        Message.AddWarning("Unknown control name: " & myControl.Name & vbCrLf)
                End Select
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ToolStripMenuItem_DefaultPosn_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem_DefaultPosn.Click
        ContextMenuStrip2.Close()

        If ContextMenuStrip2.Tag Is Nothing Then

        Else
            Dim myControl As Windows.Forms.Control
            Try
                myControl = ContextMenuStrip2.Tag
                'ColorDialog1.Color = myControl.ForeColor
                'ColorDialog1.ShowDialog()
                'myControl.ForeColor = ColorDialog1.Color
                'Message.Add("Control name: " & myControl.Name & vbCrLf)
                Select Case myControl.Name
                    Case "txtAnnotTitle"
                        'Bayes.AnnotTitle.Color = ColorDialog1.Color
                    Case "txtAnnotDescr"
                        'Bayes.AnnotDescr.Color = ColorDialog1.Color
                    Case "txtAnnotEventALabel"
                        'Bayes.AnnotEventA.Color = ColorDialog1.Color
                    Case "txtAnnotEventBLabel"
                        'Bayes.AnnotEventB.Color = ColorDialog1.Color
                    Case "txtAnnotProbA"
                        'Bayes.ProbA.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbNotA"
                        'Bayes.ProbNotA.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbB"
                        'Bayes.ProbB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbNotB"
                        'Bayes.ProbNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbAandB"
                        'Bayes.ProbAandB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbAandNotB"
                        'Bayes.ProbAandNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbNotAandB"
                        'Bayes.ProbNotAandB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotProbNotAandNotB"
                        'Bayes.ProbNotAandNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsA"
                        'Bayes.SampsA.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsNotA"
                        'Bayes.SampsNotA.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsB"
                        'Bayes.SampsB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsNotB"
                        'Bayes.SampsNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsAandB"
                        'Bayes.SampsAandB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsAandNotB"
                        'Bayes.SampsAandNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsNotAandB"
                        'Bayes.SampsNotAandB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsNotAandNotB"
                        'Bayes.SampsNotAandNotB.Label.Color = ColorDialog1.Color
                    Case "txtAnnotSampsSize"
                        'Bayes.AnnotSampleSize.Color = ColorDialog1.Color
                        Bayes.DefaultAnnotSampleSizePosn()
                        txtAnnotSampsSizeMidX.Text = Bayes.AnnotSampleSize.MidX
                        txtAnnotSampsSizeBaseY.Text = Bayes.AnnotSampleSize.BaseY
                    Case "txtConditionLabel"
                        'Bayes.AnnotCondition.Color = ColorDialog1.Color
                    Case Else
                        Message.AddWarning("Unknown control name: " & myControl.Name & vbCrLf)
                End Select
            Catch ex As Exception

            End Try
        End If




    End Sub

    Private Sub btnDefaultText_Click(sender As Object, e As EventArgs) Handles btnDefaultText.Click
        Bayes.DefaultAnnotText()
        'txtAnnotTitle.Text = Bayes.AnnotTitleText
        txtAnnotTitle.Text = Bayes.AnnotTitle.Text
        txtAnnotTitle.Font = Bayes.AnnotTitle.Font
        txtAnnotTitle.ForeColor = Bayes.AnnotTitle.Color
        txtAnnotDescr.Text = Bayes.AnnotDescr.Text
        txtAnnotDescr.Font = Bayes.AnnotDescr.Font
        txtAnnotDescr.ForeColor = Bayes.AnnotDescr.Color
        'txtAnnotEventALabel.Text = Bayes.AnnotEventAText
        'txtAnnotEventALabel.Text = Bayes.AnnotEventA.Unscaled.Text
        txtAnnotEventALabel.Text = Bayes.AnnotEventA.Text
        txtAnnotEventALabel.Font = Bayes.AnnotEventA.Font
        txtAnnotEventALabel.ForeColor = Bayes.AnnotEventA.Color
        txtAnnotEventBLabel.Text = Bayes.AnnotEventB.Text
        txtAnnotEventBLabel.Font = Bayes.AnnotEventB.Font
        txtAnnotEventBLabel.ForeColor = Bayes.AnnotEventB.Color
    End Sub

    Private Sub btnDefaultPositions_Click(sender As Object, e As EventArgs) Handles btnDefaultPositions.Click
        'Bayes.DefaultAnnotPositions()
        Bayes.DefaultTitlePosition()
        Bayes.DefaultDescrPosition()
        Bayes.DefaultEventAandBPositions()

        'txtAnnotTitleX.Text = Bayes.AnnotTitle.X
        'txtAnnotTitleY.Text = Bayes.AnnotTitle.Y
        'txtAnnotDescrX.Text = Bayes.AnnotDescr.X
        'txtAnnotDescrY.Text = Bayes.AnnotDescr.Y

        'txtAnnotEventAX.Text = Bayes.AnnotEventA.Unscaled.X
        'txtAnnotEventAY.Text = Bayes.AnnotEventA.Unscaled.Y
        'txtAnnotEventBX.Text = Bayes.AnnotEventB.Unscaled.X
        'txtAnnotEventBY.Text = Bayes.AnnotEventB.Unscaled.Y

        txtAnnotTitleMidX.Text = Bayes.AnnotTitle.MidX
        txtAnnotTitleY.Text = Bayes.AnnotTitle.Y
        txtAnnotDescrMidX.Text = Bayes.AnnotDescr.MidX
        txtAnnotDescrY.Text = Bayes.AnnotDescr.Y

        'txtAnnotEventAMidX.Text = Bayes.AnnotEventA.Unscaled.MidX
        'txtAnnotEventAY.Text = Bayes.AnnotEventA.Unscaled.Y
        'txtAnnotEventBMidX.Text = Bayes.AnnotEventB.Unscaled.MidX
        'txtAnnotEventBY.Text = Bayes.AnnotEventB.Unscaled.Y
        txtAnnotEventAMidX.Text = Bayes.AnnotEventA.MidX
        txtAnnotEventAY.Text = Bayes.AnnotEventA.Y
        txtAnnotEventBMidX.Text = Bayes.AnnotEventB.MidX
        txtAnnotEventBY.Text = Bayes.AnnotEventB.Y

    End Sub

    Private Sub btnUpdateDiagram_Click(sender As Object, e As EventArgs) Handles btnUpdateDiagram.Click
        DrawDiagram()
    End Sub

    Private Sub txtAnnotTitleMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotTitleMidX.TextChanged

    End Sub

    Private Sub txtAnnotTitleMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotTitleMidX.LostFocus
        Bayes.AnnotTitle.MidX = txtAnnotTitleMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotTitleY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotTitleY.TextChanged

    End Sub

    Private Sub txtAnnotTitleY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotTitleY.LostFocus
        Bayes.AnnotTitle.Y = txtAnnotTitleY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotDescrMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotDescrMidX.TextChanged

    End Sub

    Private Sub txtAnnotDescrMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotDescrMidX.LostFocus
        Bayes.AnnotDescr.MidX = txtAnnotDescrMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotDescrY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotDescrY.TextChanged

    End Sub

    Private Sub txtAnnotDescrY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotDescrY.LostFocus
        Bayes.AnnotDescr.Y = txtAnnotDescrY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotEventAMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotEventAMidX.TextChanged

    End Sub

    Private Sub txtAnnotEventAMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotEventAMidX.LostFocus
        'Bayes.AnnotEventAX = txtAnnotEventAX.Text
        'Bayes.AnnotEventA.Unscaled.X = txtAnnotEventAX.Text
        'Bayes.AnnotEventA.Unscaled.MidX = txtAnnotEventAMidX.Text
        Bayes.AnnotEventA.MidX = txtAnnotEventAMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotEventAY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotEventAY.TextChanged

    End Sub

    Private Sub txtAnnotEventAY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotEventAY.LostFocus
        Bayes.AnnotEventA.Y = txtAnnotEventAY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotEventBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotEventBMidX.TextChanged

    End Sub

    Private Sub txtAnnotEventBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotEventBMidX.LostFocus
        'Bayes.AnnotEventBX = txtAnnotEventBX.Text
        'Bayes.AnnotEventB.Unscaled.X = txtAnnotEventBX.Text
        'Bayes.AnnotEventB.Unscaled.MidX = txtAnnotEventBMidX.Text
        Bayes.AnnotEventB.MidX = txtAnnotEventBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotEventBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotEventBY.TextChanged

    End Sub

    Private Sub txtAnnotEventBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotEventBY.LostFocus
        Bayes.AnnotEventB.Y = txtAnnotEventBY.Text
        DrawDiagram()
    End Sub



    Private Sub txtAnnotProbAMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbAMidX.TextChanged

    End Sub

    Private Sub txtAnnotProbAMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbAMidX.LostFocus
        'Bayes.ProbA.Label.MidX = txtAnnotProbAMidX.Text
        Bayes.ProbA.ConditionalMidX = txtAnnotProbAMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbAY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbAY.TextChanged

    End Sub

    Private Sub txtAnnotProbAY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbAY.LostFocus
        'Bayes.ProbA.Label.Y = txtAnnotProbAY.Text
        Bayes.ProbA.ConditionalY = txtAnnotProbAY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbNotAMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotAMidX.TextChanged

    End Sub

    Private Sub txtAnnotProbNotAMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotAMidX.LostFocus
        'Bayes.ProbNotA.Label.MidX = txtAnnotProbNotAMidX.Text
        Bayes.ProbNotA.ConditionalMidX = txtAnnotProbNotAMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbNotAY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotAY.TextChanged

    End Sub

    Private Sub txtAnnotProbNotAY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotAY.LostFocus
        'Bayes.ProbNotA.Label.Y = txtAnnotProbNotAY.Text
        Bayes.ProbNotA.ConditionalY = txtAnnotProbNotAY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbBMidX.TextChanged

    End Sub

    Private Sub txtAnnotProbBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbBMidX.LostFocus
        'Bayes.ProbB.Label.MidX = txtAnnotProbBMidX.Text
        Bayes.ProbB.ConditionalMidX = txtAnnotProbBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbBY.TextChanged

    End Sub

    Private Sub txtAnnotProbBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbBY.LostFocus
        'Bayes.ProbB.Label.Y = txtAnnotProbBY.Text
        Bayes.ProbB.ConditionalY = txtAnnotProbBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbNotBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotBMidX.TextChanged

    End Sub

    Private Sub txtAnnotProbNotBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotBMidX.LostFocus
        'Bayes.ProbNotB.Label.MidX = txtAnnotProbNotBMidX.Text
        Bayes.ProbNotB.ConditionalMidX = txtAnnotProbNotBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbNotBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotBY.TextChanged

    End Sub

    Private Sub txtAnnotProbNotBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotBY.LostFocus
        'Bayes.ProbNotB.Label.Y = txtAnnotProbNotBY.Text
        Bayes.ProbNotB.ConditionalY = txtAnnotProbNotBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbAandBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbAandBMidX.TextChanged

    End Sub

    Private Sub txtAnnotProbAandBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbAandBMidX.LostFocus
        'Bayes.ProbAandB.Label.MidX = txtAnnotProbAandBMidX.Text
        Bayes.ProbAandB.ConditionalMidX = txtAnnotProbAandBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbAandBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbAandBY.TextChanged

    End Sub

    Private Sub txtAnnotProbAandBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbAandBY.LostFocus
        'Bayes.ProbAandB.Label.Y = txtAnnotProbAandBY.Text
        Bayes.ProbAandB.ConditionalY = txtAnnotProbAandBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbAandNotBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbAandNotBMidX.TextChanged

    End Sub

    Private Sub txtAnnotProbAandNotBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbAandNotBMidX.LostFocus
        'Bayes.ProbAandNotB.Label.MidX = txtAnnotProbAandNotBMidX.Text
        Bayes.ProbAandNotB.ConditionalMidX = txtAnnotProbAandNotBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbAandNotBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbAandNotBY.TextChanged

    End Sub

    Private Sub txtAnnotProbAandNotBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbAandNotBY.LostFocus
        'Bayes.ProbAandNotB.Label.Y = txtAnnotProbAandNotBY.Text
        Bayes.ProbAandNotB.ConditionalY = txtAnnotProbAandNotBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbNotAandBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandBMidX.TextChanged

    End Sub

    Private Sub txtAnnotProbNotAandBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandBMidX.LostFocus
        'Bayes.ProbNotAandB.Label.MidX = txtAnnotProbNotAandBMidX.Text
        Bayes.ProbNotAandB.ConditionalMidX = txtAnnotProbNotAandBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbNotAandBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandBY.TextChanged

    End Sub

    Private Sub txtAnnotProbNotAandBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandBY.LostFocus
        'Bayes.ProbNotAandB.Label.Y = txtAnnotProbNotAandBY.Text
        Bayes.ProbNotAandB.ConditionalY = txtAnnotProbNotAandBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbNotAandNotBX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandNotBX.TextChanged

    End Sub

    Private Sub txtAnnotProbNotAandNotBX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandNotBX.LostFocus
        'Bayes.ProbNotAandNotB.Label.MidX = txtAnnotProbNotAandNotBX.Text
        'Bayes.ProbNotAandNotB.ConditionalMidX = txtAnnotProbNotAandNotBX.Text
        Bayes.ProbNotAandNotB.ConditionalX = txtAnnotProbNotAandNotBX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbNotAandNotBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandNotBY.TextChanged

    End Sub

    Private Sub txtAnnotProbNotAandNotBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandNotBY.LostFocus
        'Bayes.ProbNotAandNotB.Label.Y = txtAnnotProbNotAandNotBY.Text
        Bayes.ProbNotAandNotB.ConditionalY = txtAnnotProbNotAandNotBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsAMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsAMidX.TextChanged

    End Sub

    Private Sub txtAnnotSampsAMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsAMidX.LostFocus
        'Bayes.SampsA.Label.MidX = txtAnnotSampsAMidX.Text
        Bayes.SampsA.ConditionalMidX = txtAnnotSampsAMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsAY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsAY.TextChanged

    End Sub

    Private Sub txtAnnotSampsAY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsAY.LostFocus
        'Bayes.SampsA.Label.Y = txtAnnotSampsAY.Text
        Bayes.SampsA.ConditionalY = txtAnnotSampsAY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsNotAMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAMidX.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotAMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAMidX.LostFocus
        'Bayes.SampsNotA.Label.MidX = txtAnnotSampsNotAMidX.Text
        Bayes.SampsNotA.ConditionalMidX = txtAnnotSampsNotAMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsNotAY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAY.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotAY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAY.LostFocus
        'Bayes.SampsNotA.Label.Y = txtAnnotSampsNotAY.Text
        Bayes.SampsNotA.ConditionalY = txtAnnotSampsNotAY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsBMidX.TextChanged

    End Sub

    Private Sub txtAnnotSampsBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsBMidX.LostFocus
        'Bayes.SampsB.Label.MidX = txtAnnotSampsBMidX.Text
        Bayes.SampsB.ConditionalMidX = txtAnnotSampsBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsBY.TextChanged

    End Sub

    Private Sub txtAnnotSampsBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsBY.LostFocus
        'Bayes.SampsB.Label.Y = txtAnnotSampsBY.Text
        Bayes.SampsB.ConditionalY = txtAnnotSampsBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsNotBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotBMidX.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotBMidX.LostFocus
        'Bayes.SampsNotB.Label.MidX = txtAnnotSampsNotBMidX.Text
        Bayes.SampsNotB.ConditionalMidX = txtAnnotSampsNotBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsNotBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotBY.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotBY.LostFocus
        'Bayes.SampsNotB.Label.Y = txtAnnotSampsNotBY.Text
        Bayes.SampsNotB.ConditionalY = txtAnnotSampsNotBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsAandBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsAandBMidX.TextChanged

    End Sub

    Private Sub txtAnnotSampsAandBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsAandBMidX.LostFocus
        'Bayes.SampsAandB.Label.MidX = txtAnnotSampsAandBMidX.Text
        Bayes.SampsAandB.ConditionalMidX = txtAnnotSampsAandBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsAandBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsAandBY.TextChanged

    End Sub

    Private Sub txtAnnotSampsAandBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsAandBY.LostFocus
        'Bayes.SampsAandB.Label.Y = txtAnnotSampsAandBY.Text
        Bayes.SampsAandB.ConditionalY = txtAnnotSampsAandBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsAandNotBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsAandNotBMidX.TextChanged

    End Sub

    Private Sub txtAnnotSampsAandNotBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsAandNotBMidX.LostFocus
        'Bayes.SampsAandNotB.Label.MidX = txtAnnotSampsAandNotBMidX.Text
        Bayes.SampsAandNotB.ConditionalMidX = txtAnnotSampsAandNotBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsAandNotBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsAandNotBY.TextChanged

    End Sub

    Private Sub txtAnnotSampsAandNotBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsAandNotBY.LostFocus
        'Bayes.SampsAandNotB.Label.Y = txtAnnotSampsAandNotBY.Text
        Bayes.SampsAandNotB.ConditionalY = txtAnnotSampsAandNotBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsNotAandBMidX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandBMidX.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotAandBMidX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandBMidX.LostFocus
        'Bayes.SampsNotAandB.Label.MidX = txtAnnotSampsNotAandBMidX.Text
        Bayes.SampsNotAandB.ConditionalMidX = txtAnnotSampsNotAandBMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsNotAandBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandBY.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotAandBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandBY.LostFocus
        'Bayes.SampsNotAandB.Label.Y = txtAnnotSampsNotAandBY.Text
        Bayes.SampsNotAandB.ConditionalY = txtAnnotSampsNotAandBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsNotAandNotBX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandNotBX.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotAandNotBX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandNotBX.LostFocus
        'Bayes.SampsNotAandNotB.Label.MidX = txtAnnotSampsNotAandNotBX.Text
        'Bayes.SampsNotAandNotB.ConditionalMidX = txtAnnotSampsNotAandNotBX.Text
        Bayes.SampsNotAandNotB.ConditionalX = txtAnnotSampsNotAandNotBX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsNotAandNotBY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandNotBY.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotAandNotBY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandNotBY.LostFocus
        'Bayes.SampsNotAandNotB.Label.Y = txtAnnotSampsNotAandNotBY.Text
        Bayes.SampsNotAandNotB.ConditionalY = txtAnnotSampsNotAandNotBY.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsSizeX_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsSizeMidX.TextChanged

    End Sub

    Private Sub txtAnnotSampsSizeX_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsSizeMidX.LostFocus
        'Bayes.AnnotSampleSize.X = txtAnnotSampsSizeMidX.Text
        Bayes.AnnotSampleSize.X = txtAnnotSampsSizeMidX.Text
        DrawDiagram()
    End Sub

    Private Sub txtAnnotSampsSizeBaseY_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsSizeBaseY.TextChanged

    End Sub

    Private Sub txtAnnotSampsSizeBaseY_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsSizeBaseY.LostFocus
        'Bayes.AnnotSampleSize.BaseY = txtAnnotSampsSizeBaseY.Text
        Bayes.AnnotSampleSize.BaseY = txtAnnotSampsSizeBaseY.Text
        DrawDiagram()
    End Sub



    Private Sub chkAnnotProbA_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotProbA.CheckedChanged
        'Bayes.ProbAShow = chkAnnotProbA.Checked
        'Bayes.ProbA.Label.Unscaled.Show = chkAnnotProbA.Checked
        Bayes.ProbA.Label.Show = chkAnnotProbA.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotProbNotA_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotProbNotA.CheckedChanged
        Bayes.ProbNotA.Label.Show = chkAnnotProbNotA.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotProbB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotProbB.CheckedChanged
        Bayes.ProbB.Label.Show = chkAnnotProbB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotProbNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotProbNotB.CheckedChanged
        Bayes.ProbNotB.Label.Show = chkAnnotProbNotB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotProbAandB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotProbAandB.CheckedChanged
        Bayes.ProbAandB.Label.Show = chkAnnotProbAandB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotProbAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotProbAandNotB.CheckedChanged
        Bayes.ProbAandNotB.Label.Show = chkAnnotProbAandNotB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotProbNotAandB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotProbNotAandB.CheckedChanged
        Bayes.ProbNotAandB.Label.Show = chkAnnotProbNotAandB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotProbNotAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotProbNotAandNotB.CheckedChanged
        Bayes.ProbNotAandNotB.Label.Show = chkAnnotProbNotAandNotB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotSampsA_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotSampsA.CheckedChanged
        Bayes.SampsA.Label.Show = chkAnnotSampsA.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotSampsNotA_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotSampsNotA.CheckedChanged
        Bayes.SampsNotA.Label.Show = chkAnnotSampsNotA.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotSampsB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotSampsB.CheckedChanged
        Bayes.SampsB.Label.Show = chkAnnotSampsB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotSampsNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotSampsNotB.CheckedChanged
        Bayes.SampsNotB.Label.Show = chkAnnotSampsNotB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotSampsAandB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotSampsAandB.CheckedChanged
        Bayes.SampsAandB.Label.Show = chkAnnotSampsAandB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotSampsAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotSampsAandNotB.CheckedChanged
        Bayes.SampsAandNotB.Label.Show = chkAnnotSampsAandNotB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotSampsNotAandB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotSampsNotAandB.CheckedChanged
        Bayes.SampsNotAandB.Label.Show = chkAnnotSampsNotAandB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotSampsNotAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotSampsNotAandNotB.CheckedChanged
        Bayes.SampsNotAandNotB.Label.Show = chkAnnotSampsNotAandNotB.Checked
        DrawDiagram()
    End Sub

    Private Sub chkAnnotSampsSize_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnnotSampsSize.CheckedChanged
        'Bayes.SampsSizeShow = chkAnnotSampsSize.Checked
        Bayes.AnnotSampleSize.Show = chkAnnotSampsSize.Checked
        DrawDiagram()
    End Sub

    Private Sub txtAnnotProbA_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbA.TextChanged

    End Sub

    Private Sub txtAnnotProbA_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotProbA.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotProbA_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbA.LostFocus
        Bayes.ProbA.ConditionalPrefix = txtAnnotProbA.Text
    End Sub

    Private Sub txtAnnotProbNotA_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotProbNotA.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotProbNotA_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotA.LostFocus
        Bayes.ProbNotA.ConditionalPrefix = txtAnnotProbNotA.Text
    End Sub

    Private Sub txtAnnotProbB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbB.TextChanged

    End Sub

    Private Sub txtAnnotProbB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotProbB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotProbB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbB.LostFocus
        Bayes.ProbB.ConditionalPrefix = txtAnnotProbB.Text
    End Sub

    Private Sub txtAnnotProbNotB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotB.TextChanged

    End Sub

    Private Sub txtAnnotProbNotB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotProbNotB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotProbNotB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotB.LostFocus
        Bayes.ProbNotB.ConditionalPrefix = txtAnnotProbNotB.Text
    End Sub

    Private Sub txtAnnotProbAandB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbAandB.TextChanged

    End Sub

    Private Sub txtAnnotProbAandB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotProbAandB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotProbAandB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbAandB.LostFocus
        'Bayes.ProbAandB.ConditionalText = txtAnnotProbAandB.Text
        Bayes.ProbAandB.ConditionalPrefix = txtAnnotProbAandB.Text
    End Sub

    Private Sub txtAnnotProbAandNotB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbAandNotB.TextChanged

    End Sub

    Private Sub txtAnnotProbAandNotB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotProbAandNotB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotProbAandNotB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbAandNotB.LostFocus
        Bayes.ProbAandNotB.ConditionalPrefix = txtAnnotProbAandNotB.Text
    End Sub

    Private Sub txtAnnotProbNotAandB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandB.TextChanged

    End Sub

    Private Sub txtAnnotProbNotAandB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotProbNotAandB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotProbNotAandB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandB.LostFocus
        Bayes.ProbNotAandB.ConditionalPrefix = txtAnnotProbNotAandB.Text
    End Sub

    Private Sub txtAnnotProbNotAandNotB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandNotB.TextChanged

    End Sub

    Private Sub txtAnnotProbNotAandNotB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotProbNotAandNotB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotProbNotAandNotB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotAandNotB.LostFocus
        Bayes.ProbNotAandNotB.ConditionalPrefix = txtAnnotProbNotAandNotB.Text
    End Sub

    Private Sub txtAnnotSampsA_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsA.TextChanged

    End Sub

    Private Sub txtAnnotSampsA_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotSampsA.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotSampsA_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsA.LostFocus
        Bayes.SampsA.ConditionalPrefix = txtAnnotSampsA.Text
    End Sub

    Private Sub txtAnnotSampsNotA_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotA.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotA_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotSampsNotA.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotSampsNotA_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotA.LostFocus
        Bayes.SampsNotA.ConditionalPrefix = txtAnnotSampsNotA.Text
    End Sub

    Private Sub txtAnnotSampsB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsB.TextChanged

    End Sub

    Private Sub txtAnnotSampsB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotSampsB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotSampsB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsB.LostFocus
        Bayes.SampsB.ConditionalPrefix = txtAnnotSampsB.Text
    End Sub

    Private Sub txtAnnotSampsNotB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotB.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotSampsNotB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotSampsNotB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotB.LostFocus
        Bayes.SampsNotB.ConditionalPrefix = txtAnnotSampsNotB.Text
    End Sub

    Private Sub txtAnnotSampsAandB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsAandB.TextChanged

    End Sub

    Private Sub txtAnnotSampsAandB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotSampsAandB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotSampsAandB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsAandB.LostFocus
        Bayes.SampsAandB.ConditionalPrefix = txtAnnotSampsAandB.Text
    End Sub

    Private Sub txtAnnotSampsAandNotB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsAandNotB.TextChanged

    End Sub

    Private Sub txtAnnotSampsAandNotB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotSampsAandNotB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotSampsAandNotB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsAandNotB.LostFocus
        Bayes.SampsAandNotB.ConditionalPrefix = txtAnnotSampsAandNotB.Text
    End Sub

    Private Sub txtAnnotSampsNotAandB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandB.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotAandB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotSampsNotAandB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotSampsNotAandB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandB.LostFocus
        Bayes.SampsNotAandB.ConditionalPrefix = txtAnnotSampsNotAandB.Text
    End Sub

    Private Sub txtAnnotSampsNotAandNotB_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandNotB.TextChanged

    End Sub

    Private Sub txtAnnotSampsNotAandNotB_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotSampsNotAandNotB.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotSampsNotAandNotB_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsNotAandNotB.LostFocus
        Bayes.SampsNotAandNotB.ConditionalPrefix = txtAnnotSampsNotAandNotB.Text
    End Sub

    Private Sub txtAnnotSampsSize_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotSampsSize.TextChanged

    End Sub

    Private Sub txtAnnotSampsSize_MouseUp(sender As Object, e As MouseEventArgs) Handles txtAnnotSampsSize.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip2.Tag = sender
        End If
    End Sub

    Private Sub txtAnnotSampsSize_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotSampsSize.LostFocus

    End Sub

    Private Sub btnDefaultProbPositions_Click(sender As Object, e As EventArgs) Handles btnDefaultProbPositions.Click
        'Set default position for the Probability Value annotations.
        Bayes.DefaultProbAnnotPosn()
        UpdateAnnotProbPage()
        DrawDiagram()
    End Sub

    Private Sub btnShowPositions_Click(sender As Object, e As EventArgs) Handles btnShowPositions.Click
        'Show the annotation positions:
        Message.AddText("Annotation Positions: " & vbCrLf, "Bold")
        Message.Add(Bayes.ProbA.Label.Text & " X = " & Bayes.ProbA.Label.X & " Y = " & Bayes.ProbA.Label.Y & vbCrLf)
        Message.Add(Bayes.ProbNotA.Label.Text & " X = " & Bayes.ProbNotA.Label.X & " Y = " & Bayes.ProbNotA.Label.Y & vbCrLf)
        Message.Add(Bayes.ProbB.Label.Text & " X = " & Bayes.ProbB.Label.X & " Y = " & Bayes.ProbB.Label.Y & vbCrLf)
        Message.Add(Bayes.ProbNotB.Label.Text & " X = " & Bayes.ProbNotB.Label.X & " Y = " & Bayes.ProbNotB.Label.Y & vbCrLf)
        Message.Add(Bayes.ProbAandB.Label.Text & " X = " & Bayes.ProbNotB.Label.X & " Y = " & Bayes.ProbNotB.Label.Y & vbCrLf)
        Message.Add(Bayes.ProbAandNotB.Label.Text & " X = " & Bayes.ProbAandNotB.Label.X & " Y = " & Bayes.ProbAandNotB.Label.Y & vbCrLf)
        Message.Add(Bayes.ProbNotAandB.Label.Text & " X = " & Bayes.ProbNotAandB.Label.X & " Y = " & Bayes.ProbNotAandB.Label.Y & vbCrLf)
        Message.Add(Bayes.ProbNotAandNotB.Label.Text & " X = " & Bayes.ProbNotAandNotB.Label.X & " Y = " & Bayes.ProbNotAandNotB.Label.Y & vbCrLf)

    End Sub

    Private Sub btnUpdateDiagram_2_Click(sender As Object, e As EventArgs) Handles btnUpdateDiagram_2.Click
        DrawDiagram()
    End Sub



    Private Sub btnCopyDiagram_Click(sender As Object, e As EventArgs) Handles btnCopyDiagram.Click
        'Copy the Probability Diagram to the Clipboard.
        Dim myStream As New System.IO.MemoryStream()
        Select Case cmbImageFormat.SelectedItem.ToString
            Case "Jpeg"
                'Dim Params As New System.Drawing.Imaging.EncoderParameters(1)
                'Params.Param(0) = New Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100)
                pbVenn.Image.Save(myStream, Imaging.ImageFormat.Jpeg)
            Case "Png"
                pbVenn.Image.Save(myStream, Imaging.ImageFormat.Png)
            Case "Bmp"
                pbVenn.Image.Save(myStream, Imaging.ImageFormat.Bmp)
            'Case "Emf"
            '    pbVenn.Image.Save(myStream, Imaging.ImageFormat.Emf) 'System.ArgumentNullException: 'Value cannot be null.  Parameter Name: encoder'
            'Case "Exif"
            '    pbVenn.Image.Save(myStream, Imaging.ImageFormat.Exif) 'System.ArgumentNullException: 'Value cannot be null.  Parameter Name: encoder'
            Case "Gif"
                pbVenn.Image.Save(myStream, Imaging.ImageFormat.Gif)
            'Case "Icon"
            '    pbVenn.Image.Save(myStream, Imaging.ImageFormat.Icon) 'System.ArgumentNullException: 'Value cannot be null.  Parameter Name: encoder'
            Case "Tiff"
                pbVenn.Image.Save(myStream, Imaging.ImageFormat.Tiff)
                'Case "Wmf"
                '    pbVenn.Image.Save(myStream, Imaging.ImageFormat.Wmf) 'System.ArgumentNullException: 'Value cannot be null.  Parameter Name: encoder'
            Case Else
                Message.AddWarning("Unknown image format: " & cmbImageFormat.SelectedItem.ToString & vbCrLf)
                Message.AddWarning("Jpeg format will be used." & vbCrLf)
                pbVenn.Image.Save(myStream, Imaging.ImageFormat.Jpeg)
        End Select
        Dim ChartPic As New Bitmap(myStream)
        Clipboard.SetDataObject(ChartPic)
    End Sub

    Private Sub btnCopySimDiag_Click(sender As Object, e As EventArgs) Handles btnCopySimDiag.Click
        'Copy the Simulation Diagram to the Clipboard.
        Dim myStream As New System.IO.MemoryStream()
        Select Case cmbSimImageFormat.SelectedItem.ToString
            Case "Jpeg"
                pbSim.Image.Save(myStream, Imaging.ImageFormat.Jpeg)
            Case "Png"
                pbSim.Image.Save(myStream, Imaging.ImageFormat.Png)
            Case "Bmp"
                pbSim.Image.Save(myStream, Imaging.ImageFormat.Bmp)
            Case "Gif"
                pbSim.Image.Save(myStream, Imaging.ImageFormat.Gif)
            Case "Tiff"
                pbSim.Image.Save(myStream, Imaging.ImageFormat.Tiff)
            Case Else
                Message.AddWarning("Unknown image format: " & cmbSimImageFormat.SelectedItem.ToString & vbCrLf)
                Message.AddWarning("Jpeg format will be used." & vbCrLf)
                pbSim.Image.Save(myStream, Imaging.ImageFormat.Jpeg)
        End Select
        Dim ChartPic As New Bitmap(myStream)
        Clipboard.SetDataObject(ChartPic)
    End Sub

    Private Sub rbNotToScale_CheckedChanged(sender As Object, e As EventArgs) Handles rbUnscaled.CheckedChanged
        If rbUnscaled.Focused Then
            If rbUnscaled.Checked Then
                Bayes.Settings.Scaling = "Unscaled"
                BayesSim.Settings.Scaling = "Unscaled"
            End If
            DrawDiagram()
            DrawSimDiagram()

            txtEventAXMin.Enabled = True
            txtEventAXMin.Text = Bayes.EventA.Unscaled.XMin
            txtEventAXMax.Enabled = True
            txtEventAXMax.Text = Bayes.EventA.Unscaled.XMax
            txtEventAYMin.Enabled = True
            txtEventAYMin.Text = Bayes.EventA.Unscaled.YMin
            txtEventAYMax.Enabled = True
            txtEventAYMax.Text = Bayes.EventA.Unscaled.YMax

            txtEventBXMin.Enabled = True
            txtEventBXMin.Text = Bayes.EventB.Unscaled.XMin
            txtEventBXMax.Enabled = True
            txtEventBXMax.Text = Bayes.EventB.Unscaled.XMax
            txtEventBYMin.Enabled = True
            txtEventBYMin.Text = Bayes.EventB.Unscaled.YMin
            txtEventBYMax.Enabled = True
            txtEventBYMax.Text = Bayes.EventB.Unscaled.YMax

            UpdateAnnotationTab()
            UpdateAnnotProbPage()
        End If
    End Sub

    Private Sub rbScaleA_CheckedChanged(sender As Object, e As EventArgs) Handles rbScaleA.CheckedChanged
        If rbScaleA.Focused Then
            If rbScaleA.Checked Then
                Bayes.Settings.Scaling = "ScaleA"
                BayesSim.Settings.Scaling = "ScaleA"
            End If
            DrawDiagram()
            DrawSimDiagram()

            txtEventAXMin.Enabled = False
            txtEventAXMin.Text = Bayes.EventA.ScaleA.XMin
            txtEventAXMax.Enabled = False
            txtEventAXMax.Text = Bayes.EventA.ScaleA.XMax
            txtEventAYMin.Enabled = False
            txtEventAYMin.Text = Bayes.EventA.ScaleA.YMin
            txtEventAYMax.Enabled = False
            txtEventAYMax.Text = Bayes.EventA.ScaleA.YMax

            txtEventBXMin.Enabled = True
            txtEventBXMin.Text = Bayes.EventB.ScaleA.XMin
            txtEventBXMax.Enabled = True
            txtEventBXMax.Text = Bayes.EventB.ScaleA.XMax
            txtEventBYMin.Enabled = True
            txtEventBYMin.Text = Bayes.EventB.ScaleA.YMin
            txtEventBYMax.Enabled = True
            txtEventBYMax.Text = Bayes.EventB.ScaleA.YMax

            UpdateAnnotationTab()
            UpdateAnnotProbPage()
        End If
    End Sub

    Private Sub rbScaleB_CheckedChanged(sender As Object, e As EventArgs) Handles rbScaleB.CheckedChanged
        If rbScaleB.Focused Then
            If rbScaleB.Checked Then
                Bayes.Settings.Scaling = "ScaleB"
                BayesSim.Settings.Scaling = "ScaleB"
            End If
            DrawDiagram()
            DrawSimDiagram()

            txtEventAXMin.Enabled = True
            txtEventAXMin.Text = Bayes.EventA.ScaleB.XMin
            txtEventAXMax.Enabled = True
            txtEventAXMax.Text = Bayes.EventA.ScaleB.XMax
            txtEventAYMin.Enabled = True
            txtEventAYMin.Text = Bayes.EventA.ScaleB.YMin
            txtEventAYMax.Enabled = True
            txtEventAYMax.Text = Bayes.EventA.ScaleB.YMax

            txtEventBXMin.Enabled = False
            txtEventBXMin.Text = Bayes.EventB.ScaleB.XMin
            txtEventBXMax.Enabled = False
            txtEventBXMax.Text = Bayes.EventB.ScaleB.XMax
            txtEventBYMin.Enabled = False
            txtEventBYMin.Text = Bayes.EventB.ScaleB.YMin
            txtEventBYMax.Enabled = False
            txtEventBYMax.Text = Bayes.EventB.ScaleB.YMax

            UpdateAnnotationTab()
            UpdateAnnotProbPage()
        End If
    End Sub

    Private Sub rbScaleAB_CheckedChanged(sender As Object, e As EventArgs) Handles rbScaleAB.CheckedChanged
        If rbScaleAB.Focused Then
            If rbScaleAB.Checked Then
                Bayes.Settings.Scaling = "ScaleAB"
                BayesSim.Settings.Scaling = "ScaleAB"
            End If
            DrawDiagram()
            DrawSimDiagram()

            txtEventAXMin.Enabled = False
            txtEventAXMin.Text = Bayes.EventA.ScaleAB.XMin
            txtEventAXMax.Enabled = True
            txtEventAXMax.Text = Bayes.EventA.ScaleAB.XMax
            txtEventAYMin.Enabled = True
            txtEventAYMin.Text = Bayes.EventA.ScaleAB.YMin
            txtEventAYMax.Enabled = False
            txtEventAYMax.Text = Bayes.EventA.ScaleAB.YMax

            txtEventBXMin.Enabled = False
            txtEventBXMin.Text = Bayes.EventB.ScaleAB.XMin
            txtEventBXMax.Enabled = False
            txtEventBXMax.Text = Bayes.EventB.ScaleAB.XMax
            txtEventBYMin.Enabled = False
            txtEventBYMin.Text = Bayes.EventB.ScaleAB.YMin
            txtEventBYMax.Enabled = False
            txtEventBYMax.Text = Bayes.EventB.ScaleAB.YMax

            UpdateAnnotationTab()
            UpdateAnnotProbPage()
        End If
    End Sub

    Private Sub Bayes_Message(Msg As String) Handles Bayes.Message
        Message.Add(Msg)
    End Sub

    Private Sub Bayes_ErrorMessage(Msg As String) Handles Bayes.ErrorMessage
        Message.AddWarning(Msg)
    End Sub

    Private Sub btnDisplayValues_Click(sender As Object, e As EventArgs) Handles btnDisplayValues.Click
        'Display all of the Bayes Probability and Sample Count values.

        'String.Format Method:
        'https://docs.microsoft.com/en-us/dotnet/api/system.string.format?view=net-5.0


        Message.AddText(vbCrLf & "Bayes Probabilities" & vbCrLf, "Bold")
        Message.SetFontName(ADVL_Utilities_Library_1.Message.FontList.Courier_New) 'This is a mono-spaced font.
        'Message.Add(String.Format("{0,-24} {1,-8} {2,-10}", "Probability", "Rank", "Status") & vbCrLf) 'Negative numbers used to left align text in the field width.
        'Message.AddText(String.Format("{0,-24} {1,-8} {2,-10}", "Probability", "Rank", "Status") & vbCrLf, "Bold") 'Negative numbers used to left align text in the field width. NOTE: This uses the wrong font!!!
        Message.SetBoldStyle()
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", "Probability", "Rank", "Status") & vbCrLf) 'Negative numbers used to left align text in the field width.
        Message.SetNotBoldStyle()
        'Message.SetBoldStyle()
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbA.Label.Text, Bayes.ProbA.Rank, Bayes.ProbA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbA.Label.Text & "  Rank: " & Bayes.ProbA.Rank & "  Status: " & Bayes.ProbA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotA.Label.Text, Bayes.ProbNotA.Rank, Bayes.ProbNotA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotA.Label.Text & "  Rank: " & Bayes.ProbNotA.Rank & "  Status: " & Bayes.ProbNotA.Status & vbCrLf)
        'Message.Add(Bayes.ProbB.Label.Text & "  Rank: " & Bayes.ProbB.Rank & "  Status: " & Bayes.ProbB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbB.Label.Text, Bayes.ProbB.Rank, Bayes.ProbB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotB.Label.Text & "  Rank: " & Bayes.ProbNotB.Rank & "  Status: " & Bayes.ProbNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotB.Label.Text, Bayes.ProbNotB.Rank, Bayes.ProbNotB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandB.Label.Text & "  Rank: " & Bayes.ProbAandB.Rank & "  Status: " & Bayes.ProbAandB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandB.Label.Text, Bayes.ProbAandB.Rank, Bayes.ProbAandB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandNotB.Label.Text & "  Rank: " & Bayes.ProbAandNotB.Rank & "  Status: " & Bayes.ProbAandNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandNotB.Label.Text, Bayes.ProbAandNotB.Rank, Bayes.ProbAandNotB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandB.Label.Text & "  Rank: " & Bayes.ProbNotAandB.Rank & "  Status: " & Bayes.ProbNotAandB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandB.Label.Text, Bayes.ProbNotAandB.Rank, Bayes.ProbNotAandB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandNotB.Label.Text & "  Rank: " & Bayes.ProbNotAandNotB.Rank & "  Status: " & Bayes.ProbNotAandNotB.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandNotB.Label.Text, Bayes.ProbNotAandNotB.Rank, Bayes.ProbNotAandNotB.Status) & vbCrLf & vbCrLf)

        'Message.Add(Bayes.ProbA.GivenA.Label.Text & "  Rank: " & Bayes.ProbA.GivenA.Rank & "  Status: " & Bayes.ProbA.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbA.GivenA.Label.Text, Bayes.ProbA.GivenA.Rank, Bayes.ProbA.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotA.GivenA.Label.Text & "  Rank: " & Bayes.ProbNotA.GivenA.Rank & "  Status: " & Bayes.ProbNotA.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotA.GivenA.Label.Text, Bayes.ProbNotA.GivenA.Rank, Bayes.ProbNotA.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbB.GivenA.Label.Text & "  Rank: " & Bayes.ProbB.GivenA.Rank & "  Status: " & Bayes.ProbB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbB.GivenA.Label.Text, Bayes.ProbB.GivenA.Rank, Bayes.ProbB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotB.GivenA.Label.Text & "  Rank: " & Bayes.ProbNotB.GivenA.Rank & "  Status: " & Bayes.ProbNotB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotB.GivenA.Label.Text, Bayes.ProbNotB.GivenA.Rank, Bayes.ProbNotB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandB.GivenA.Label.Text & "  Rank: " & Bayes.ProbAandB.GivenA.Rank & "  Status: " & Bayes.ProbAandB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandB.GivenA.Label.Text, Bayes.ProbAandB.GivenA.Rank, Bayes.ProbAandB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandNotB.GivenA.Label.Text & "  Rank: " & Bayes.ProbAandNotB.GivenA.Rank & "  Status: " & Bayes.ProbAandNotB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandNotB.GivenA.Label.Text, Bayes.ProbAandNotB.GivenA.Rank, Bayes.ProbAandNotB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandB.GivenA.Label.Text & "  Rank: " & Bayes.ProbNotAandB.GivenA.Rank & "  Status: " & Bayes.ProbNotAandB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandB.GivenA.Label.Text, Bayes.ProbNotAandB.GivenA.Rank, Bayes.ProbNotAandB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandNotB.GivenA.Label.Text & "  Rank: " & Bayes.ProbNotAandNotB.GivenA.Rank & "  Status: " & Bayes.ProbNotAandNotB.GivenA.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandNotB.GivenA.Label.Text, Bayes.ProbNotAandNotB.GivenA.Rank, Bayes.ProbNotAandNotB.GivenA.Status) & vbCrLf & vbCrLf)

        'Message.Add(Bayes.ProbA.GivenNotA.Label.Text & "  Rank: " & Bayes.ProbA.GivenNotA.Rank & "  Status: " & Bayes.ProbA.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbA.GivenNotA.Label.Text, Bayes.ProbA.GivenNotA.Rank, Bayes.ProbA.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotA.GivenNotA.Label.Text & "  Rank: " & Bayes.ProbNotA.GivenNotA.Rank & "  Status: " & Bayes.ProbNotA.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotA.GivenNotA.Label.Text, Bayes.ProbNotA.GivenNotA.Rank, Bayes.ProbNotA.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbB.GivenNotA.Label.Text & "  Rank: " & Bayes.ProbB.GivenNotA.Rank & "  Status: " & Bayes.ProbB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbB.GivenNotA.Label.Text, Bayes.ProbB.GivenNotA.Rank, Bayes.ProbB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotB.GivenNotA.Label.Text & "  Rank: " & Bayes.ProbNotB.GivenNotA.Rank & "  Status: " & Bayes.ProbNotB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotB.GivenNotA.Label.Text, Bayes.ProbNotB.GivenNotA.Rank, Bayes.ProbNotB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandB.GivenNotA.Label.Text & "  Rank: " & Bayes.ProbAandB.GivenNotA.Rank & "  Status: " & Bayes.ProbAandB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandB.GivenNotA.Label.Text, Bayes.ProbAandB.GivenNotA.Rank, Bayes.ProbAandB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandNotB.GivenNotA.Label.Text & "  Rank: " & Bayes.ProbAandNotB.GivenNotA.Rank & "  Status: " & Bayes.ProbAandNotB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandNotB.GivenNotA.Label.Text, Bayes.ProbAandNotB.GivenNotA.Rank, Bayes.ProbAandNotB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandB.GivenNotA.Label.Text & "  Rank: " & Bayes.ProbNotAandB.GivenNotA.Rank & "  Status: " & Bayes.ProbNotAandB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandB.GivenNotA.Label.Text, Bayes.ProbNotAandB.GivenNotA.Rank, Bayes.ProbNotAandB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandNotB.GivenNotA.Label.Text & "  Rank: " & Bayes.ProbNotAandNotB.GivenNotA.Rank & "  Status: " & Bayes.ProbNotAandNotB.GivenNotA.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandNotB.GivenNotA.Label.Text, Bayes.ProbNotAandNotB.GivenNotA.Rank, Bayes.ProbNotAandNotB.GivenNotA.Status) & vbCrLf & vbCrLf)

        'Message.Add(Bayes.ProbA.GivenB.Label.Text & "  Rank: " & Bayes.ProbA.GivenB.Rank & "  Status: " & Bayes.ProbA.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbA.GivenB.Label.Text, Bayes.ProbA.GivenB.Rank, Bayes.ProbA.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotA.GivenB.Label.Text & "  Rank: " & Bayes.ProbNotA.GivenB.Rank & "  Status: " & Bayes.ProbNotA.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotA.GivenB.Label.Text, Bayes.ProbNotA.GivenB.Rank, Bayes.ProbNotA.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbB.GivenB.Label.Text & "  Rank: " & Bayes.ProbB.GivenB.Rank & "  Status: " & Bayes.ProbB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbB.GivenB.Label.Text, Bayes.ProbB.GivenB.Rank, Bayes.ProbB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotB.GivenB.Label.Text & "  Rank: " & Bayes.ProbNotB.GivenB.Rank & "  Status: " & Bayes.ProbNotB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotB.GivenB.Label.Text, Bayes.ProbNotB.GivenB.Rank, Bayes.ProbNotB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandB.GivenB.Label.Text & "  Rank: " & Bayes.ProbAandB.GivenB.Rank & "  Status: " & Bayes.ProbAandB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandB.GivenB.Label.Text, Bayes.ProbAandB.GivenB.Rank, Bayes.ProbAandB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandNotB.GivenB.Label.Text & "  Rank: " & Bayes.ProbAandNotB.GivenB.Rank & "  Status: " & Bayes.ProbAandNotB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandNotB.GivenB.Label.Text, Bayes.ProbAandNotB.GivenB.Rank, Bayes.ProbAandNotB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandB.GivenB.Label.Text & "  Rank: " & Bayes.ProbNotAandB.GivenB.Rank & "  Status: " & Bayes.ProbNotAandB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandB.GivenB.Label.Text, Bayes.ProbNotAandB.GivenB.Rank, Bayes.ProbNotAandB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandNotB.GivenB.Label.Text & "  Rank: " & Bayes.ProbNotAandNotB.GivenB.Rank & "  Status: " & Bayes.ProbNotAandNotB.GivenB.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandNotB.GivenB.Label.Text, Bayes.ProbNotAandNotB.GivenB.Rank, Bayes.ProbNotAandNotB.GivenB.Status) & vbCrLf & vbCrLf)

        'Message.Add(Bayes.ProbA.GivenNotB.Label.Text & "  Rank: " & Bayes.ProbA.GivenNotB.Rank & "  Status: " & Bayes.ProbA.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbA.GivenNotB.Label.Text, Bayes.ProbA.GivenNotB.Rank, Bayes.ProbA.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotA.GivenNotB.Label.Text & "  Rank: " & Bayes.ProbNotA.GivenNotB.Rank & "  Status: " & Bayes.ProbNotA.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotA.GivenNotB.Label.Text, Bayes.ProbNotA.GivenNotB.Rank, Bayes.ProbNotA.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbB.GivenNotB.Label.Text & "  Rank: " & Bayes.ProbB.GivenNotB.Rank & "  Status: " & Bayes.ProbB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbB.GivenNotB.Label.Text, Bayes.ProbB.GivenNotB.Rank, Bayes.ProbB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotB.GivenNotB.Label.Text & "  Rank: " & Bayes.ProbNotB.GivenNotB.Rank & "  Status: " & Bayes.ProbNotB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotB.GivenNotB.Label.Text, Bayes.ProbNotB.GivenNotB.Rank, Bayes.ProbNotB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandB.GivenNotB.Label.Text & "  Rank: " & Bayes.ProbAandB.GivenNotB.Rank & "  Status: " & Bayes.ProbAandB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandB.GivenNotB.Label.Text, Bayes.ProbAandB.GivenNotB.Rank, Bayes.ProbAandB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbAandNotB.GivenNotB.Label.Text & "  Rank: " & Bayes.ProbAandNotB.GivenNotB.Rank & "  Status: " & Bayes.ProbAandNotB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbAandNotB.GivenNotB.Label.Text, Bayes.ProbAandNotB.GivenNotB.Rank, Bayes.ProbAandNotB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandB.GivenNotB.Label.Text & "  Rank: " & Bayes.ProbNotAandB.GivenNotB.Rank & "  Status: " & Bayes.ProbNotAandB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandB.GivenNotB.Label.Text, Bayes.ProbNotAandB.GivenNotB.Rank, Bayes.ProbNotAandB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.ProbNotAandNotB.GivenNotB.Label.Text & "  Rank: " & Bayes.ProbNotAandNotB.GivenNotB.Rank & "  Status: " & Bayes.ProbNotAandNotB.GivenNotB.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.ProbNotAandNotB.GivenNotB.Label.Text, Bayes.ProbNotAandNotB.GivenNotB.Rank, Bayes.ProbNotAandNotB.GivenNotB.Status) & vbCrLf & vbCrLf)

        'Message.AddText(vbCrLf & "Bayes Sample Counts" & vbCrLf, "Bold")
        Message.AddText(vbCrLf & "Bayes Sample Counts" & vbCrLf, "Bold")
        'Message.Add(Bayes.SampsA.Label.Text & "  Rank: " & Bayes.SampsA.Rank & "  Status: " & Bayes.SampsA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsA.Label.Text, Bayes.SampsA.Rank, Bayes.SampsA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotA.Label.Text & "  Rank: " & Bayes.SampsNotA.Rank & "  Status: " & Bayes.SampsNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotA.Label.Text, Bayes.SampsNotA.Rank, Bayes.SampsNotA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsB.Label.Text & "  Rank: " & Bayes.SampsB.Rank & "  Status: " & Bayes.SampsB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsB.Label.Text, Bayes.SampsB.Rank, Bayes.SampsB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotB.Label.Text & "  Rank: " & Bayes.SampsNotB.Rank & "  Status: " & Bayes.SampsNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotB.Label.Text, Bayes.SampsNotB.Rank, Bayes.SampsNotB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandB.Label.Text & "  Rank: " & Bayes.SampsAandB.Rank & "  Status: " & Bayes.SampsAandB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandB.Label.Text, Bayes.SampsAandB.Rank, Bayes.SampsAandB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandNotB.Label.Text & "  Rank: " & Bayes.SampsAandNotB.Rank & "  Status: " & Bayes.SampsAandNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandNotB.Label.Text, Bayes.SampsAandNotB.Rank, Bayes.SampsAandNotB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandB.Label.Text & "  Rank: " & Bayes.SampsNotAandB.Rank & "  Status: " & Bayes.SampsNotAandB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandB.Label.Text, Bayes.SampsNotAandB.Rank, Bayes.SampsNotAandB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandNotB.Label.Text & "  Rank: " & Bayes.SampsNotAandNotB.Rank & "  Status: " & Bayes.SampsNotAandNotB.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandNotB.Label.Text, Bayes.SampsNotAandNotB.Rank, Bayes.SampsNotAandNotB.Status) & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampleSize.Label.Text, Bayes.SampleSize.Rank, Bayes.SampleSize.Status) & vbCrLf & vbCrLf)


        'Message.Add(Bayes.SampsA.GivenA.Label.Text & "  Rank: " & Bayes.SampsA.GivenA.Rank & "  Status: " & Bayes.SampsA.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsA.GivenA.Label.Text, Bayes.SampsA.GivenA.Rank, Bayes.SampsA.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotA.GivenA.Label.Text & "  Rank: " & Bayes.SampsNotA.GivenA.Rank & "  Status: " & Bayes.SampsNotA.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotA.GivenA.Label.Text, Bayes.SampsNotA.GivenA.Rank, Bayes.SampsNotA.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsB.GivenA.Label.Text & "  Rank: " & Bayes.SampsB.GivenA.Rank & "  Status: " & Bayes.SampsB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsB.GivenA.Label.Text, Bayes.SampsB.GivenA.Rank, Bayes.SampsB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotB.GivenA.Label.Text & "  Rank: " & Bayes.SampsNotB.GivenA.Rank & "  Status: " & Bayes.SampsNotB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotB.GivenA.Label.Text, Bayes.SampsNotB.GivenA.Rank, Bayes.SampsNotB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandB.GivenA.Label.Text & "  Rank: " & Bayes.SampsAandB.GivenA.Rank & "  Status: " & Bayes.SampsAandB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandB.GivenA.Label.Text, Bayes.SampsAandB.GivenA.Rank, Bayes.SampsAandB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandNotB.GivenA.Label.Text & "  Rank: " & Bayes.SampsAandNotB.GivenA.Rank & "  Status: " & Bayes.SampsAandNotB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandNotB.GivenA.Label.Text, Bayes.SampsAandNotB.GivenA.Rank, Bayes.SampsAandNotB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandB.GivenA.Label.Text & "  Rank: " & Bayes.SampsNotAandB.GivenA.Rank & "  Status: " & Bayes.SampsNotAandB.GivenA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandB.GivenA.Label.Text, Bayes.SampsNotAandB.GivenA.Rank, Bayes.SampsNotAandB.GivenA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandNotB.GivenA.Label.Text & "  Rank: " & Bayes.SampsNotAandNotB.GivenA.Rank & "  Status: " & Bayes.SampsNotAandNotB.GivenA.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandNotB.GivenA.Label.Text, Bayes.SampsNotAandNotB.GivenA.Rank, Bayes.SampsNotAandNotB.GivenA.Status) & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampleSize.GivenA.Label.Text, Bayes.SampleSize.GivenA.Rank, Bayes.SampleSize.GivenA.Status) & vbCrLf & vbCrLf)

        'Message.Add(Bayes.SampsA.GivenNotA.Label.Text & "  Rank: " & Bayes.SampsA.GivenNotA.Rank & "  Status: " & Bayes.SampsA.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsA.GivenNotA.Label.Text, Bayes.SampsA.GivenNotA.Rank, Bayes.SampsA.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotA.GivenNotA.Label.Text & "  Rank: " & Bayes.SampsNotA.GivenNotA.Rank & "  Status: " & Bayes.SampsNotA.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotA.GivenNotA.Label.Text, Bayes.SampsNotA.GivenNotA.Rank, Bayes.SampsNotA.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsB.GivenNotA.Label.Text & "  Rank: " & Bayes.SampsB.GivenNotA.Rank & "  Status: " & Bayes.SampsB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsB.GivenNotA.Label.Text, Bayes.SampsB.GivenNotA.Rank, Bayes.SampsB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotB.GivenNotA.Label.Text & "  Rank: " & Bayes.SampsNotB.GivenNotA.Rank & "  Status: " & Bayes.SampsNotB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotB.GivenNotA.Label.Text, Bayes.SampsNotB.GivenNotA.Rank, Bayes.SampsNotB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandB.GivenNotA.Label.Text & "  Rank: " & Bayes.SampsAandB.GivenNotA.Rank & "  Status: " & Bayes.SampsAandB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandB.GivenNotA.Label.Text, Bayes.SampsAandB.GivenNotA.Rank, Bayes.SampsAandB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandNotB.GivenNotA.Label.Text & "  Rank: " & Bayes.SampsAandNotB.GivenNotA.Rank & "  Status: " & Bayes.SampsAandNotB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandNotB.GivenNotA.Label.Text, Bayes.SampsAandNotB.GivenNotA.Rank, Bayes.SampsAandNotB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandB.GivenNotA.Label.Text & "  Rank: " & Bayes.SampsNotAandB.GivenNotA.Rank & "  Status: " & Bayes.SampsNotAandB.GivenNotA.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandB.GivenNotA.Label.Text, Bayes.SampsNotAandB.GivenNotA.Rank, Bayes.SampsNotAandB.GivenNotA.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandNotB.GivenNotA.Label.Text & "  Rank: " & Bayes.SampsNotAandNotB.GivenNotA.Rank & "  Status: " & Bayes.SampsNotAandNotB.GivenNotA.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandNotB.GivenNotA.Label.Text, Bayes.SampsNotAandNotB.GivenNotA.Rank, Bayes.SampsNotAandNotB.GivenNotA.Status) & vbCrLf & vbCrLf)

        'Message.Add(Bayes.SampsA.GivenB.Label.Text & "  Rank: " & Bayes.SampsA.GivenB.Rank & "  Status: " & Bayes.SampsA.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsA.GivenB.Label.Text, Bayes.SampsA.GivenB.Rank, Bayes.SampsA.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotA.GivenB.Label.Text & "  Rank: " & Bayes.SampsNotA.GivenB.Rank & "  Status: " & Bayes.SampsNotA.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotA.GivenB.Label.Text, Bayes.SampsNotA.GivenB.Rank, Bayes.SampsNotA.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsB.GivenB.Label.Text & "  Rank: " & Bayes.SampsB.GivenB.Rank & "  Status: " & Bayes.SampsB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsB.GivenB.Label.Text, Bayes.SampsB.GivenB.Rank, Bayes.SampsB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotB.GivenB.Label.Text & "  Rank: " & Bayes.SampsNotB.GivenB.Rank & "  Status: " & Bayes.SampsNotB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotB.GivenB.Label.Text, Bayes.SampsNotB.GivenB.Rank, Bayes.SampsNotB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandB.GivenB.Label.Text & "  Rank: " & Bayes.SampsAandB.GivenB.Rank & "  Status: " & Bayes.SampsAandB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandB.GivenB.Label.Text, Bayes.SampsAandB.GivenB.Rank, Bayes.SampsAandB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandNotB.GivenB.Label.Text & "  Rank: " & Bayes.SampsAandNotB.GivenB.Rank & "  Status: " & Bayes.SampsAandNotB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandNotB.GivenB.Label.Text, Bayes.SampsAandNotB.GivenB.Rank, Bayes.SampsAandNotB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandB.GivenB.Label.Text & "  Rank: " & Bayes.SampsNotAandB.GivenB.Rank & "  Status: " & Bayes.SampsNotAandB.GivenB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandB.GivenB.Label.Text, Bayes.SampsNotAandB.GivenB.Rank, Bayes.SampsNotAandB.GivenB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandNotB.GivenB.Label.Text & "  Rank: " & Bayes.SampsNotAandNotB.GivenB.Rank & "  Status: " & Bayes.SampsNotAandNotB.GivenB.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandNotB.GivenB.Label.Text, Bayes.SampsNotAandNotB.GivenB.Rank, Bayes.SampsNotAandNotB.GivenB.Status) & vbCrLf & vbCrLf)

        'Message.Add(Bayes.SampsA.GivenNotB.Label.Text & "  Rank: " & Bayes.SampsA.GivenNotB.Rank & "  Status: " & Bayes.SampsA.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsA.GivenNotB.Label.Text, Bayes.SampsA.GivenNotB.Rank, Bayes.SampsA.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotA.GivenNotB.Label.Text & "  Rank: " & Bayes.SampsNotA.GivenNotB.Rank & "  Status: " & Bayes.SampsNotA.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotA.GivenNotB.Label.Text, Bayes.SampsNotA.GivenNotB.Rank, Bayes.SampsNotA.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsB.GivenNotB.Label.Text & "  Rank: " & Bayes.SampsB.GivenNotB.Rank & "  Status: " & Bayes.SampsB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsB.GivenNotB.Label.Text, Bayes.SampsB.GivenNotB.Rank, Bayes.SampsB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotB.GivenNotB.Label.Text & "  Rank: " & Bayes.SampsNotB.GivenNotB.Rank & "  Status: " & Bayes.SampsNotB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotB.GivenNotB.Label.Text, Bayes.SampsNotB.GivenNotB.Rank, Bayes.SampsNotB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandB.GivenNotB.Label.Text & "  Rank: " & Bayes.SampsAandB.GivenNotB.Rank & "  Status: " & Bayes.SampsAandB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandB.GivenNotB.Label.Text, Bayes.SampsAandB.GivenNotB.Rank, Bayes.SampsAandB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsAandNotB.GivenNotB.Label.Text & "  Rank: " & Bayes.SampsAandNotB.GivenNotB.Rank & "  Status: " & Bayes.SampsAandNotB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsAandNotB.GivenNotB.Label.Text, Bayes.SampsAandNotB.GivenNotB.Rank, Bayes.SampsAandNotB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandB.GivenNotB.Label.Text & "  Rank: " & Bayes.SampsNotAandB.GivenNotB.Rank & "  Status: " & Bayes.SampsNotAandB.GivenNotB.Status & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandB.GivenNotB.Label.Text, Bayes.SampsNotAandB.GivenNotB.Rank, Bayes.SampsNotAandB.GivenNotB.Status) & vbCrLf)
        'Message.Add(Bayes.SampsNotAandNotB.GivenNotB.Label.Text & "  Rank: " & Bayes.SampsNotAandNotB.GivenNotB.Rank & "  Status: " & Bayes.SampsNotAandNotB.GivenNotB.Status & vbCrLf & vbCrLf)
        Message.Add(String.Format("{0,32}   {1,-6} {2,-10}", Bayes.SampsNotAandNotB.GivenNotB.Label.Text, Bayes.SampsNotAandNotB.GivenNotB.Rank, Bayes.SampsNotAandNotB.GivenNotB.Status) & vbCrLf & vbCrLf)

        Message.SetNormalStyle() 'Restore the font to the normal style.

    End Sub


    Private Sub btnCalcAreas_Click(sender As Object, e As EventArgs) Handles btnCalcAreas.Click
        'Calculate the Diagram, Event A and Event B areas.

        'Select Case Bayes.Settings.Scaling
        '    Case "Unscaled"
        '        txtDiagArea.Text = Format(Bayes.Diagram.Width * Bayes.Diagram.Height, "N1")
        '    Case "ScaleA"
        '        txtDiagArea.Text = Format(Bayes.Diagram.Width * Bayes.Diagram.Height, "N1")

        '    Case "ScaleB"
        '        txtDiagArea.Text = Format(Bayes.Diagram.Width * Bayes.Diagram.Height, "N1")

        '    Case "ScaleAB"
        '        txtDiagArea.Text = Format(Bayes.Diagram.Width * Bayes.Diagram.Height, "N1")

        '    Case Else
        '        Message.AddWarning("Unknown scaling: " & Bayes.Settings.Scaling & vbCrLf)
        'End Select

        txtDiagArea.Text = Format(Bayes.Diagram.Width * Bayes.Diagram.Height, "N1")
        txtEventAArea.Text = Format(Bayes.EventAEllipseArea, "N1")
        txtEventBArea.Text = Format(Bayes.EventBEllipseArea, "N1")

    End Sub

    Private Sub btnCalcYVals_Click(sender As Object, e As EventArgs) Handles btnCalcYVals.Click
        'Calculate the Event A and Event B shape intersections with the specified X value.

        Dim XVal As Single = txtXValue.Text
        Dim YIntersectA As ADVL_Bayes.YIntersect = Bayes.YIntersectShapeA(XVal)
        txtEventAYU.Text = Format(YIntersectA.Upper, "N1")
        txtEventAYL.Text = Format(YIntersectA.Lower, "N1")

        Dim YIntersectB As ADVL_Bayes.YIntersect = Bayes.YIntersectShapeB(XVal)
        txtEventBYU.Text = Format(YIntersectB.Upper, "N1")
        txtEventBYL.Text = Format(YIntersectB.Lower, "N1")

        txtARightArea.Text = Format(Bayes.EllipseARightArea(XVal), "N1")
        txtBLeftArea.Text = Format(Bayes.EllipseBLeftArea(XVal), "N1")

        'Select Case Bayes.Settings.Scaling
        '    Case "Unscaled"
        '        txtEventAArea.Text = Bayes.EventAArea

        '    Case "ScaleA"
        '        txtEventAArea.Text = Bayes.EventAArea

        '    Case "ScaleB"
        '        txtEventAArea.Text = Bayes.EventAArea

        '    Case "ScaleAB"
        '        txtEventAArea.Text = Bayes.EventAArea

        '    Case Else
        '        txtEventAArea.Text = Bayes.EventAArea
        '        Message.AddWarning("Unknown scaling: " & Bayes.Settings.Scaling & vbCrLf)
        'End Select

    End Sub


    Private Sub btnCalcOverlapArea_Click(sender As Object, e As EventArgs) Handles btnCalcOverlapArea.Click
        'Calculate the overlap area
        txtOverlapArea.Text = Format(Bayes.EllipseOverlapArea("Unscaled"), "N1")
        txtIntersectXPos.Text = Format(Bayes.EllipseIntersectXPos("Unscaled"), "N1")
    End Sub

    Private Sub btnCalcScaleA_Click(sender As Object, e As EventArgs) Handles btnCalcScaleA.Click
        Bayes.SetEllipseScaleA()
    End Sub

    Private Sub btnCalcScaleB_Click(sender As Object, e As EventArgs) Handles btnCalcScaleB.Click
        Bayes.SetEllipseScaleB()
    End Sub

    Private Sub btnCalcScaleAB_Click(sender As Object, e As EventArgs) Handles btnCalcScaleAB.Click
        Bayes.SetEllipseScaleAB()
    End Sub


    Private Sub btnDrawSimDiagram_Click(sender As Object, e As EventArgs) Handles btnDrawSimDiagram.Click
        'BayesSim.SetCentredSimFigureWidths()
        BayesSim.SetLeftSimFigureWidths()
        DrawSimDiagram()
    End Sub

    Private Sub btnUseCurrent_Click(sender As Object, e As EventArgs) Handles btnUseCurrent.Click
        'Use the current Bayes model settings in the Bayes Simulation.
        CurrentSimSettings()
    End Sub

    Private Sub CurrentSimSettings()
        'Use the current Bayes Model for the simulation settings
        BayesSim.Settings.SurveySize = Bayes.SampleSize.Value
        txtSimSurveySize.Text = BayesSim.Settings.SurveySize
        BayesSim.Settings.ProbabilityMeasure = Bayes.Settings.ProbabilityMeasure
        BayesSim.Settings.DecimalFormat = Bayes.Settings.DecimalFormat
        BayesSim.Settings.PercentFormat = Bayes.Settings.PercentFormat
        BayesSim.Settings.ProbA = Bayes.ProbA.Value
        txtSimPA.Text = BayesSim.Settings.FormattedProbA
        BayesSim.Settings.ProbAandB = Bayes.ProbAandB.Value
        txtSimPAandB.Text = BayesSim.Settings.FormattedProbAandB
        BayesSim.Settings.ProbB = Bayes.ProbB.Value
        txtSimPB.Text = BayesSim.Settings.FormattedProbB
        BayesSim.AnnotTitle.Text = Bayes.AnnotTitle.Text

        BayesSim.ProbAandNotBLabel.Color = Bayes.ProbAandNotB.Label.Color
        BayesSim.ProbALabel.Color = Bayes.ProbA.Label.Color
        BayesSim.ProbAandBLabel.Color = Bayes.ProbAandB.Label.Color
        BayesSim.ProbBLabel.Color = Bayes.ProbB.Label.Color
        BayesSim.ProbNotAandBLabel.Color = Bayes.ProbNotAandB.Label.Color
        BayesSim.ProbNotAandNotBLabel.Color = Bayes.ProbNotAandNotB.Label.Color

        DrawSimDiagram()
    End Sub

    Private Sub txtSimRepeats_TextChanged(sender As Object, e As EventArgs) Handles txtSimRepeats.TextChanged

    End Sub

    Private Sub txtSimRepeats_LostFocus(sender As Object, e As EventArgs) Handles txtSimRepeats.LostFocus
        'A new number of repeats has been specified.
        Dim NRepeats As Integer
        Try
            NRepeats = txtSimRepeats.Text
            txtSimRepeats.Text = NRepeats
            BayesSim.Settings.SurveyRepeatNo = NRepeats
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtTimeOutSecs_TextChanged(sender As Object, e As EventArgs) Handles txtTimeOutSecs.TextChanged

    End Sub

    Private Sub txtTimeOutSecs_LostFocus(sender As Object, e As EventArgs) Handles txtTimeOutSecs.LostFocus

    End Sub


    Private Sub txtSeed_TextChanged(sender As Object, e As EventArgs) Handles txtSeed.TextChanged

    End Sub

    Private Sub txtSeed_LostFocus(sender As Object, e As EventArgs) Handles txtSeed.LostFocus
        'The seed value has been changed
        Dim Seed As Integer = txtSeed.Text
        If Seed < -1 Then Seed = -1
        txtSeed.Text = Seed
        BayesSim.Settings.Seed = Seed

    End Sub


    Private Sub txtSimSurveySize_TextChanged(sender As Object, e As EventArgs) Handles txtSimSurveySize.TextChanged

    End Sub
    Private Sub txtSimSurveySize_LostFocus(sender As Object, e As EventArgs) Handles txtSimSurveySize.LostFocus
        'A new Bayes survey size has been entered.
        Dim SurveySize As Integer
        Try
            SurveySize = txtSimSurveySize.Text
            txtSimSurveySize.Text = SurveySize
            BayesSim.Settings.SurveySize = SurveySize
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub


    Private Sub txtSimPAandB_TextChanged(sender As Object, e As EventArgs) Handles txtSimPAandB.TextChanged

    End Sub

    Private Sub txtSimPAandB_LostFocus(sender As Object, e As EventArgs) Handles txtSimPAandB.LostFocus

    End Sub

    Private Sub txtSimPA_TextChanged(sender As Object, e As EventArgs) Handles txtSimPA.TextChanged

    End Sub

    Private Sub txtSimPA_LostFocus(sender As Object, e As EventArgs) Handles txtSimPA.LostFocus

    End Sub

    Private Sub txtSimPB_TextChanged(sender As Object, e As EventArgs) Handles txtSimPB.TextChanged

    End Sub

    Private Sub txtSimPB_LostFocus(sender As Object, e As EventArgs) Handles txtSimPB.LostFocus

    End Sub

    Private Sub txtEventSimSurveySize_TextChanged(sender As Object, e As EventArgs) Handles txtEventSimSurveySize.TextChanged

    End Sub

    Private Sub txtEventSimSurveySize_LostFocus(sender As Object, e As EventArgs) Handles txtEventSimSurveySize.LostFocus
        'A new Event survey size has been entered.
        Dim EventSurveySize As Integer
        Try
            EventSurveySize = txtEventSimSurveySize.Text
            txtEventSimSurveySize.Text = SampString(EventSurveySize)
            BayesSim.Settings.EventSurveySize = EventSurveySize
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtSimPEvent_TextChanged(sender As Object, e As EventArgs) Handles txtSimPEvent.TextChanged

    End Sub

    Private Sub txtSimPEvent_LostFocus(sender As Object, e As EventArgs) Handles txtSimPEvent.LostFocus
        'A new Event probability has been entered.
        'Dim PEvent As Double
        'Try
        '    PEvent = txtSimPEvent.Text
        '    txtSimPEvent.Text = PEvent
        '    BayesSim.Settings.ProbEvent = PEvent
        'Catch ex As Exception
        '    Message.AddWarning(ex.Message & vbCrLf)
        'End Try

        Try
            Dim PEvent As Double
            If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
                PEvent = txtSimPEvent.Text
                txtSimPEvent.Text = ProbString(PEvent)  'Redisplay the event probability with the specified format.
                If BayesSim.Settings.ProbEvent <> PEvent Then
                    BayesSim.Settings.ProbEvent = PEvent 'Only set the BayesSim.Settings.ProbEvent if the Value has changed
                End If
            ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
                PEvent = txtSimPEvent.Text.Replace("%", "")
                PEvent = PEvent / 100
                txtSimPEvent.Text = ProbString(PEvent)  'Redisplay the event probability with the specified format.
                If BayesSim.Settings.ProbEvent <> PEvent Then
                    BayesSim.Settings.ProbEvent = PEvent 'Only set the BayesSim.Settings.ProbEvent if the Value has changed
                End If
            Else
                Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
            End If

        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try




    End Sub

    Private Sub UpdateEventSimSettings()
        'Update the Event Simulation settings.
        'This will be required if the display formats have changed.
        txtEventSimSurveySize.Text = SampString(BayesSim.Settings.EventSurveySize)
        txtSimPEvent.Text = ProbString(BayesSim.Settings.ProbEvent)

        txtSimRepeats.Text = SampString(BayesSim.Settings.SurveyRepeatNo)
        txtTimeOutSecs.Text = BayesSim.Settings.TimeOutSeconds
        txtSeed.Text = BayesSim.Settings.Seed

    End Sub





    Private Sub btnRunSim_Click(sender As Object, e As EventArgs) Handles btnRunSim.Click
        pbSimulation.Minimum = 1
        pbSimulation.Maximum = BayesSim.Settings.SurveyRepeatNo
        pbSimulation.Value = 1
        BayesSim.RunBayesSimulation()
        'txtPAMean.Text = BayesSim.SimProbAMean
        txtPAMean.Text = BayesSim.FormattedSimProbAMean
        'txtPAStdDev.Text = BayesSim.SimProbAStdDev
        txtPAStdDev.Text = BayesSim.FormattedSimProbAStdDev
        'txtPAandNotBMean.Text = BayesSim.SimProbAandNotBMean
        txtPAandNotBMean.Text = BayesSim.FormattedSimProbAandNotBMean
        'txtPAandNotBStdDev.Text = BayesSim.SimProbAandNotBStdDev
        txtPAandNotBStdDev.Text = BayesSim.FormattedSimProbAandNotBStdDev
        'txtPAandBMean.Text = BayesSim.SimProbAandBMean
        txtPAandBMean.Text = BayesSim.FormattedSimProbAandBMean
        'txtPAandBStdDev.Text = BayesSim.SimProbAandBStdDev
        txtPAandBStdDev.Text = BayesSim.FormattedSimProbAandBStdDev
        'txtPNotAandBMean.Text = BayesSim.SimProbNotAandBMean
        txtPNotAandBMean.Text = BayesSim.FormattedSimProbNotAandBMean
        'txtPNotAandBStdDev.Text = BayesSim.SimProbNotAandBStdDev
        txtPNotAandBStdDev.Text = BayesSim.FormattedSimProbNotAandBStdDev
        'txtPBMean.Text = BayesSim.SimProbBMean
        txtPBMean.Text = BayesSim.FormattedSimProbBMean
        'txtPBStdDev.Text = BayesSim.SimProbBStdDev
        txtPBStdDev.Text = BayesSim.FormattedSimProbBStdDev
        'txtPNotAandNotBMean.Text = BayesSim.SimProbNotAandNotBMean
        txtPNotAandNotBMean.Text = BayesSim.FormattedSimProbNotAandNotBMean
        'txtPNotAandNotBStdDev.Text = BayesSim.SimProbNotAandNotBStdDev
        txtPNotAandNotBStdDev.Text = BayesSim.FormattedSimProbNotAandNotBStdDev
    End Sub

    Private Sub btnRunEventSim_Click(sender As Object, e As EventArgs) Handles btnRunEventSim.Click
        pbSimulation.Minimum = 1
        pbSimulation.Maximum = BayesSim.Settings.SurveyRepeatNo
        pbSimulation.Value = 1
        BayesSim.RunEventSimulation()
        'txtPEventMean.Text = BayesSim.SimProbEventMean
        txtPEventMean.Text = BayesSim.FormattedSimProbEventMean
        'txtPEventStdDev.Text = BayesSim.SimProbEventStdDev
        txtPEventStdDev.Text = BayesSim.FormattedSimProbEventStdDev
    End Sub



    Private Sub BayesSim_Message(Msg As String) Handles BayesSim.Message
        Message.Add(Msg)
    End Sub

    Private Sub BayesSim_ErrorMessage(Msg As String) Handles BayesSim.ErrorMessage
        Message.AddWarning(Msg)
    End Sub
    Private Sub BayesSim_Progress(ProgressVal As Integer) Handles BayesSim.Progress
        pbSimulation.Value = ProgressVal
    End Sub

    Private Sub btnChartPA_Click(sender As Object, e As EventArgs) Handles btnChartPA.Click
        'Display the series analysis of the Bayes Event A simulation.

        'Dim TableName As String = "Bayes_Simulation"
        'Dim ColumnName As String = "EventTrue"

        'Table: Bayes_Simulation
        'Fields: SampsAandNotB, SampsA, SampsAandB, SampsB, SampsNotAandB, SampsNotAandNotB

        If BayesSim.Data.Tables.Contains("Bayes_Simulation") Then
            Dim FormNo As Integer = OpenNewSeriesAnalysis()
            SeriesAnalysisList(FormNo).NTrials = BayesSim.Settings.SurveySize
            SeriesAnalysisList(FormNo).SourceTableName = "Bayes_Simulation"
            SeriesAnalysisList(FormNo).SourceColumnName = "SampsA"
            SeriesAnalysisList(FormNo).Show
        Else
            Message.AddWarning("The Bayes_Simulation table does not exist. Please run a simulation." & vbCrLf)
        End If

    End Sub

    Private Sub btnChartPAandNotB_Click(sender As Object, e As EventArgs) Handles btnChartPAandNotB.Click
        'Display the series analysis of the Bayes Event A and Not B simulation.

        'Table: Bayes_Simulation
        'Fields: SampsAandNotB, SampsA, SampsAandB, SampsB, SampsNotAandB, SampsNotAandNotB
        If BayesSim.Data.Tables.Contains("Bayes_Simulation") Then
            Dim FormNo As Integer = OpenNewSeriesAnalysis()
            SeriesAnalysisList(FormNo).NTrials = BayesSim.Settings.SurveySize
            SeriesAnalysisList(FormNo).SourceTableName = "Bayes_Simulation"
            SeriesAnalysisList(FormNo).SourceColumnName = "SampsAandNotB"
            SeriesAnalysisList(FormNo).Show
        Else
            Message.AddWarning("The Bayes_Simulation table does not exist. Please run a simulation." & vbCrLf)
        End If

    End Sub

    Private Sub btnChartPAandB_Click(sender As Object, e As EventArgs) Handles btnChartPAandB.Click
        'Display the series analysis of the Bayes Event A and B simulation.

        'Table: Bayes_Simulation
        'Fields: SampsAandNotB, SampsA, SampsAandB, SampsB, SampsNotAandB, SampsNotAandNotB
        If BayesSim.Data.Tables.Contains("Bayes_Simulation") Then
            Dim FormNo As Integer = OpenNewSeriesAnalysis()
            SeriesAnalysisList(FormNo).NTrials = BayesSim.Settings.SurveySize
            SeriesAnalysisList(FormNo).SourceTableName = "Bayes_Simulation"
            SeriesAnalysisList(FormNo).SourceColumnName = "SampsAandB"
            SeriesAnalysisList(FormNo).Show
        Else
            Message.AddWarning("The Bayes_Simulation table does not exist. Please run a simulation." & vbCrLf)
        End If

    End Sub

    Private Sub btnChartPNotAandB_Click(sender As Object, e As EventArgs) Handles btnChartPNotAandB.Click
        'Display the series analysis of the Bayes Event Not A and B simulation.

        'Table: Bayes_Simulation
        'Fields: SampsAandNotB, SampsA, SampsAandB, SampsB, SampsNotAandB, SampsNotAandNotB
        If BayesSim.Data.Tables.Contains("Bayes_Simulation") Then
            Dim FormNo As Integer = OpenNewSeriesAnalysis()
            SeriesAnalysisList(FormNo).NTrials = BayesSim.Settings.SurveySize
            SeriesAnalysisList(FormNo).SourceTableName = "Bayes_Simulation"
            SeriesAnalysisList(FormNo).SourceColumnName = "SampsNotAandB"
            SeriesAnalysisList(FormNo).Show
        Else
            Message.AddWarning("The Bayes_Simulation table does not exist. Please run a simulation." & vbCrLf)
        End If

    End Sub

    Private Sub btnChartPB_Click(sender As Object, e As EventArgs) Handles btnChartPB.Click
        'Display the series analysis of the Bayes Event B simulation.

        'Table: Bayes_Simulation
        'Fields: SampsAandNotB, SampsA, SampsAandB, SampsB, SampsNotAandB, SampsNotAandNotB
        If BayesSim.Data.Tables.Contains("Bayes_Simulation") Then
            Dim FormNo As Integer = OpenNewSeriesAnalysis()
            SeriesAnalysisList(FormNo).NTrials = BayesSim.Settings.SurveySize
            SeriesAnalysisList(FormNo).SourceTableName = "Bayes_Simulation"
            SeriesAnalysisList(FormNo).SourceColumnName = "SampsB"
            SeriesAnalysisList(FormNo).Show
        Else
            Message.AddWarning("The Bayes_Simulation table does not exist. Please run a simulation." & vbCrLf)
        End If

    End Sub

    Private Sub btnChartPNotAandNotB_Click(sender As Object, e As EventArgs) Handles btnChartPNotAandNotB.Click
        'Display the series analysis of the Bayes Event Not A and Not B simulation.

        'Table: Bayes_Simulation
        'Fields: SampsAandNotB, SampsA, SampsAandB, SampsB, SampsNotAandB, SampsNotAandNotB
        If BayesSim.Data.Tables.Contains("Bayes_Simulation") Then
            Dim FormNo As Integer = OpenNewSeriesAnalysis()
            SeriesAnalysisList(FormNo).NTrials = BayesSim.Settings.SurveySize
            SeriesAnalysisList(FormNo).SourceTableName = "Bayes_Simulation"
            SeriesAnalysisList(FormNo).SourceColumnName = "SampsNotAandNotB"
            SeriesAnalysisList(FormNo).Show
        Else
            Message.AddWarning("The Bayes_Simulation table does not exist. Please run a simulation." & vbCrLf)
        End If

    End Sub

    Private Sub btnChartPEvent_Click(sender As Object, e As EventArgs) Handles btnChartPEvent.Click
        'Display the series analysis of the Event simulation.
        Dim TableName As String = "Event_Simulation"
        Dim ColumnName As String = "EventTrue"
        'Table: Event_Simulation
        'Fields: EventTrue, EventFalse
        If BayesSim.Data.Tables.Contains("Event_Simulation") Then
            Dim FormNo As Integer = OpenNewSeriesAnalysis()
            'SeriesAnalysisList(FormNo).TableName = TableName
            SeriesAnalysisList(FormNo).NTrials = BayesSim.Settings.EventSurveySize
            SeriesAnalysisList(FormNo).SourceTableName = TableName
            'SeriesAnalysisList(FormNo).ColumnName = ColumnName
            'SeriesAnalysisList(FormNo).NTrials = BayesSim.Settings.EventSurveySize
            SeriesAnalysisList(FormNo).SourceColumnName = ColumnName
            SeriesAnalysisList(FormNo).Show
        Else
            Message.AddWarning("The Event_Simulation table does not exist. Please run a simulation." & vbCrLf)
        End If


    End Sub

    Private Sub btnFormatHelp2_Click(sender As Object, e As EventArgs)
        'Show Format inforamtion.
        MessageBox.Show("Format string examples:" & vbCrLf & "N4 - Number displayed with thousands separator and 4 decimal places" & vbCrLf & "F4 - Number displayed with 4 decimal places.", "Number Formatting")
    End Sub

    Private Sub txtTP_TextChanged(sender As Object, e As EventArgs) Handles txtTP.TextChanged

    End Sub

    Private Sub txtTP_LostFocus(sender As Object, e As EventArgs) Handles txtTP.LostFocus
        CalcMetrics()
    End Sub

    Private Sub txtTN_TextChanged(sender As Object, e As EventArgs) Handles txtTN.TextChanged

    End Sub

    Private Sub txtTN_LostFocus(sender As Object, e As EventArgs) Handles txtTN.LostFocus
        CalcMetrics()
    End Sub

    Private Sub txtFP_TextChanged(sender As Object, e As EventArgs) Handles txtFP.TextChanged

    End Sub

    Private Sub txtFP_LostFocus(sender As Object, e As EventArgs) Handles txtFP.LostFocus
        CalcMetrics()
    End Sub

    Private Sub txtFN_TextChanged(sender As Object, e As EventArgs) Handles txtFN.TextChanged

    End Sub

    Private Sub txtFN_LostFocus(sender As Object, e As EventArgs) Handles txtFN.LostFocus
        CalcMetrics()
    End Sub

    Private Sub txtSensitivity_TextChanged(sender As Object, e As EventArgs) Handles txtSensitivity.TextChanged

    End Sub

    Private Sub txtCalcSensitivity_TextChanged(sender As Object, e As EventArgs) Handles txtCalcSensitivity.TextChanged

    End Sub

    Private Sub txtCalcSensitivity_LostFocus(sender As Object, e As EventArgs) Handles txtCalcSensitivity.LostFocus
        CalcMetrics()
    End Sub

    Private Sub txtCalcSpecificity_TextChanged(sender As Object, e As EventArgs) Handles txtCalcSpecificity.TextChanged

    End Sub

    Private Sub txtCalcSpecificity_LostFocus(sender As Object, e As EventArgs) Handles txtCalcSpecificity.LostFocus
        CalcMetrics()
    End Sub

    Private Sub txtCalcPrevalence_TextChanged(sender As Object, e As EventArgs) Handles txtCalcPrevalence.TextChanged

    End Sub

    Private Sub txtCalcPrevalence_LostFocus(sender As Object, e As EventArgs) Handles txtCalcPrevalence.LostFocus
        CalcMetrics()
    End Sub

    Private Sub txtCalcSampSize_TextChanged(sender As Object, e As EventArgs) Handles txtCalcSampSize.TextChanged

    End Sub

    Private Sub txtCalcSampSize_LostFocus(sender As Object, e As EventArgs) Handles txtCalcSampSize.LostFocus
        CalcMetrics()
    End Sub

    Private Sub CalcMetrics()
        'Calculate the Diagnostic Test Performance Metrics.

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

        'Other diagnostic test performance metrics:
        Dim Accuracy As Double  'How many of those tested were correctly identified as positive or negative.
        Dim F1_Score As Double  'The harmonic mean of the Precision and Sensitivity.
        Dim Precision As Double 'How many of those testing positive are truly positive.
        Dim NegPredValue As Double 'Proportion of those that tested negative that are truly negative.
        Dim FalsePosRate As Double 'Proportion of those that are truly negative that tested positive.
        Dim FalseNegRate As Double 'Proportion of those that are truly positive that tested negative.
        Dim FalseOmissionRate As Double 'Proportion of those that tested negative that are truly positive.
        Dim FalseDiscoveryRate As Double 'Proportion of those that tested positive that are truly negative.
        Dim PosLikelihoodRatio As Double 'Positive Likelihood Ratio
        Dim NegLikelihoodRatio As Double 'Negative Likelihood Ratio
        Dim DiagOddsRatio As Double  'Diagnostic Odds Ratio

        If rbEnterSurvey.Checked Then 'Use the survey sample counts to calculate the diagnostic test performance metrics.
            TP = txtTP.Text   'True Positive survey test results 
            TN = txtTN.Text 'True Negative survey test results 
            FP = txtFP.Text    'False Positive survey test results 
            FN = txtFN.Text   'False Negative survey test results 

            'Precision = TP / (TP + FP) 'How many of those testing positive are truly positive.
            'Accuracy = (TP + TN) / (TP + FP + FN + TN) 'How many of those tested were correctly identified as positive or negative.
            'Sensitivity = TP / (TP + FN) '(aka Recall) How many of thoose that are positive tested positive.
            'Specificity = TN / (TN + FP) 'How many of those that are negative tested negative.
            'Prevalence = (TP + FN) / (TP + TN + FP + FN)
            'SampleSize = TP + TN + FP + FN
            'F1_Score = 2 * Sensitivity * Precision / (Sensitivity + Precision) 'The harmonic mean of the Precision and Sensitivity.
            'NegPredValue = TN / (FN + TN) 'NegativePredictiveValue = TN / (FN + TN)
            'FalseNegRate = FN / (TP + FN) 'FalseNegativeRate = FN / (TP + FN)
            'FalsePosRate = FP / (FP + TN)  'FalsePositiveRate = FP / (FP + TN)
            'FalseOmissionRate = FN / (FN + TN)  'FalseOmissionRate = FN / (FN + TN)
            'FalseDiscoveryRate = FP / (TP + FP) 'FalseDiscoveryRate = FP / (TP + FP)
            'PosLikelihoodRatio = Sensitivity / FalsePosRate
            'NegLikelihoodRatio = FalseNegRate / Specificity
            'DiagOddsRatio = PosLikelihoodRatio / NegLikelihoodRatio

            ''Display formatted values:
            'If Bayes.Settings.ProbabilityMeasure = "Percent" Then
            '    txtCalcPrecision.Text = Format(Precision * 100, Bayes.Settings.PercentFormat) & "%"
            '    txtCalcAccuracy.Text = Format(Accuracy * 100, Bayes.Settings.PercentFormat) & "%"
            '    txtCalcSensitivity.Text = Format(Sensitivity * 100, Bayes.Settings.PercentFormat) & "%"
            '    txtCalcSpecificity.Text = Format(Specificity * 100, Bayes.Settings.PercentFormat) & "%"
            '    txtCalcPrevalence.Text = Format(Prevalence * 100, Bayes.Settings.PercentFormat) & "%"
            '    'txtCalcF1Score.Text = Format(F1_Score * 100, Bayes.Settings.PercentFormat) & "%"
            '    txtCalcNPV.Text = Format(NegPredValue * 100, Bayes.Settings.PercentFormat) & "%"
            '    txtCalcFPR.Text = Format(FalsePosRate * 100, Bayes.Settings.PercentFormat) & "%"
            '    txtCalcFNR.Text = Format(FalseNegRate * 100, Bayes.Settings.PercentFormat) & "%"
            '    txtCalcFOR.Text = Format(FalseOmissionRate * 100, Bayes.Settings.PercentFormat) & "%"
            '    txtCalcFDR.Text = Format(FalseDiscoveryRate * 100, Bayes.Settings.PercentFormat) & "%"
            'Else
            '    txtCalcPrecision.Text = Format(Precision, Bayes.Settings.DecimalFormat)
            '    txtCalcAccuracy.Text = Format(Accuracy, Bayes.Settings.DecimalFormat)
            '    txtCalcSensitivity.Text = Format(Sensitivity, Bayes.Settings.DecimalFormat)
            '    txtCalcSpecificity.Text = Format(Specificity, Bayes.Settings.DecimalFormat)
            '    txtCalcPrevalence.Text = Format(Prevalence, Bayes.Settings.DecimalFormat)
            '    'txtCalcF1Score.Text = Format(F1_Score, Bayes.Settings.DecimalFormat)
            '    txtCalcNPV.Text = Format(NegPredValue, Bayes.Settings.DecimalFormat) & "%"
            '    txtCalcFPR.Text = Format(FalsePosRate, Bayes.Settings.DecimalFormat) & "%"
            '    txtCalcFNR.Text = Format(FalseNegRate, Bayes.Settings.DecimalFormat) & "%"
            '    txtCalcFOR.Text = Format(FalseOmissionRate, Bayes.Settings.DecimalFormat) & "%"
            '    txtCalcFDR.Text = Format(FalseDiscoveryRate, Bayes.Settings.DecimalFormat) & "%"
            'End If
            'txtCalcSampSize.Text = Format(SampleSize, Bayes.Settings.SamplesFormat)

            'txtCalcF1Score.Text = Format(F1_Score, Bayes.Settings.DecimalFormat)
            'txtCalcPLR.Text = Format(PosLikelihoodRatio, Bayes.Settings.DecimalFormat)
            'txtCalcNLR.Text = Format(NegLikelihoodRatio, Bayes.Settings.DecimalFormat)
            'txtCalcDOR.Text = Format(DiagOddsRatio, Bayes.Settings.DecimalFormat)


        Else 'Use the survey analysis results to back-calculate the survey sample counts and selected diagnostic test performance metrics.
            If txtSampleSize.Text = "" Then
                Message.AddWarning("Please enter the Sample Size." & vbCrLf)
                Beep()
                Exit Sub
            End If
            SampleSize = txtCalcSampSize.Text
            If Bayes.Settings.ProbabilityMeasure = "Percent" Then
                If txtCalcSensitivity.Text = "" Then
                    Message.AddWarning("Please enter the Sensitivity." & vbCrLf)
                    Beep()
                    Exit Sub
                End If
                Sensitivity = txtCalcSensitivity.Text.Replace("%", "")
                'Sensitivity = Sensitivity / 100
                Sensitivity /= 100
                If txtCalcSpecificity.Text = "" Then
                    Message.AddWarning("Please enter the Specificity." & vbCrLf)
                    Beep()
                    Exit Sub
                End If
                Specificity = txtCalcSpecificity.Text.Replace("%", "")
                'Specificity = Specificity / 100
                Specificity /= 100
                If txtCalcPrevalence.Text = "" Then
                    Message.AddWarning("Please enter the Prevalence." & vbCrLf)
                    Beep()
                    Exit Sub
                End If
                Prevalence = txtCalcPrevalence.Text.Replace("%", "")
                'Prevalence = Prevalence / 100
                Prevalence /= 100
            Else
                If txtCalcSensitivity.Text = "" Then
                    Message.AddWarning("Please enter the Sensitivity." & vbCrLf)
                    Beep()
                    Exit Sub
                End If
                Sensitivity = txtCalcSensitivity.Text
                If txtCalcSpecificity.Text = "" Then
                    Message.AddWarning("Please enter the Specificity." & vbCrLf)
                    Beep()
                    Exit Sub
                End If
                Specificity = txtCalcSpecificity.Text
                If txtCalcPrevalence.Text = "" Then
                    Message.AddWarning("Please enter the Specificity." & vbCrLf)
                    Beep()
                    Exit Sub
                End If
                Prevalence = txtCalcPrevalence.Text
            End If

            TP = Prevalence * SampleSize * Sensitivity
            TN = SampleSize * Specificity - Prevalence * SampleSize * Specificity
            FP = SampleSize - Prevalence * SampleSize - SampleSize * Specificity + Prevalence * SampleSize * Specificity
            FN = Prevalence * SampleSize - Prevalence * SampleSize * Sensitivity

            'Precision = TP / (TP + FP) 'How many of those testing positive are truly positive.
            'Accuracy = (TP + TN) / (TP + FP + FN + TN) 'How many of those tested were correctly identified as positive or negative.
            'F1_Score = 2 * Sensitivity * Precision / (Sensitivity + Precision) 'The harmonic mean of the Precision and Sensitivity.

            ''Display formatted values:
            'If Bayes.Settings.ProbabilityMeasure = "Percent" Then
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

            txtTP.Text = Format(TP, Bayes.Settings.SamplesFormat)
            txtTN.Text = Format(TN, Bayes.Settings.SamplesFormat)
            txtFP.Text = Format(FP, Bayes.Settings.SamplesFormat)
            txtFN.Text = Format(FN, Bayes.Settings.SamplesFormat)
        End If


        Precision = TP / (TP + FP) 'How many of those testing positive are truly positive.
        Accuracy = (TP + TN) / (TP + FP + FN + TN) 'How many of those tested were correctly identified as positive or negative.
        Sensitivity = TP / (TP + FN) '(aka Recall) How many of thoose that are positive tested positive.
        Specificity = TN / (TN + FP) 'How many of those that are negative tested negative.
        Prevalence = (TP + FN) / (TP + TN + FP + FN)
        SampleSize = TP + TN + FP + FN
        F1_Score = 2 * Sensitivity * Precision / (Sensitivity + Precision) 'The harmonic mean of the Precision and Sensitivity.
        NegPredValue = TN / (FN + TN) 'NegativePredictiveValue = TN / (FN + TN)
        FalseNegRate = FN / (TP + FN) 'FalseNegativeRate = FN / (TP + FN)
        FalsePosRate = FP / (FP + TN)  'FalsePositiveRate = FP / (FP + TN)
        FalseOmissionRate = FN / (FN + TN)  'FalseOmissionRate = FN / (FN + TN)
        FalseDiscoveryRate = FP / (TP + FP) 'FalseDiscoveryRate = FP / (TP + FP)
        PosLikelihoodRatio = Sensitivity / FalsePosRate
        NegLikelihoodRatio = FalseNegRate / Specificity
        DiagOddsRatio = PosLikelihoodRatio / NegLikelihoodRatio

        'Display formatted values:
        If Bayes.Settings.ProbabilityMeasure = "Percent" Then
            txtCalcPrecision.Text = Format(Precision * 100, Bayes.Settings.PercentFormat) & "%"
            txtCalcAccuracy.Text = Format(Accuracy * 100, Bayes.Settings.PercentFormat) & "%"
            txtCalcSensitivity.Text = Format(Sensitivity * 100, Bayes.Settings.PercentFormat) & "%"
            txtCalcSpecificity.Text = Format(Specificity * 100, Bayes.Settings.PercentFormat) & "%"
            txtCalcPrevalence.Text = Format(Prevalence * 100, Bayes.Settings.PercentFormat) & "%"
            'txtCalcF1Score.Text = Format(F1_Score * 100, Bayes.Settings.PercentFormat) & "%"
            txtCalcNPV.Text = Format(NegPredValue * 100, Bayes.Settings.PercentFormat) & "%"
            txtCalcFPR.Text = Format(FalsePosRate * 100, Bayes.Settings.PercentFormat) & "%"
            txtCalcFNR.Text = Format(FalseNegRate * 100, Bayes.Settings.PercentFormat) & "%"
            txtCalcFOR.Text = Format(FalseOmissionRate * 100, Bayes.Settings.PercentFormat) & "%"
            txtCalcFDR.Text = Format(FalseDiscoveryRate * 100, Bayes.Settings.PercentFormat) & "%"
        Else
            txtCalcPrecision.Text = Format(Precision, Bayes.Settings.DecimalFormat)
            txtCalcAccuracy.Text = Format(Accuracy, Bayes.Settings.DecimalFormat)
            txtCalcSensitivity.Text = Format(Sensitivity, Bayes.Settings.DecimalFormat)
            txtCalcSpecificity.Text = Format(Specificity, Bayes.Settings.DecimalFormat)
            txtCalcPrevalence.Text = Format(Prevalence, Bayes.Settings.DecimalFormat)
            'txtCalcF1Score.Text = Format(F1_Score, Bayes.Settings.DecimalFormat)
            txtCalcNPV.Text = Format(NegPredValue, Bayes.Settings.DecimalFormat) & "%"
            txtCalcFPR.Text = Format(FalsePosRate, Bayes.Settings.DecimalFormat) & "%"
            txtCalcFNR.Text = Format(FalseNegRate, Bayes.Settings.DecimalFormat) & "%"
            txtCalcFOR.Text = Format(FalseOmissionRate, Bayes.Settings.DecimalFormat) & "%"
            txtCalcFDR.Text = Format(FalseDiscoveryRate, Bayes.Settings.DecimalFormat) & "%"
        End If
        txtCalcSampSize.Text = Format(SampleSize, Bayes.Settings.SamplesFormat)

        txtCalcF1Score.Text = Format(F1_Score, Bayes.Settings.DecimalFormat)
        txtCalcPLR.Text = Format(PosLikelihoodRatio, Bayes.Settings.DecimalFormat)
        txtCalcNLR.Text = Format(NegLikelihoodRatio, Bayes.Settings.DecimalFormat)
        txtCalcDOR.Text = Format(DiagOddsRatio, Bayes.Settings.DecimalFormat)

        lblTPpct.Text = Format(TP / SampleSize * 100, Bayes.Settings.PercentFormat) & "%"
        lblTNpct.Text = Format(TN / SampleSize * 100, Bayes.Settings.PercentFormat) & "%"
        lblFPpct.Text = Format(FP / SampleSize * 100, Bayes.Settings.PercentFormat) & "%"
        lblFNpct.Text = Format(FN / SampleSize * 100, Bayes.Settings.PercentFormat) & "%"



    End Sub

    Private Sub cmbRocColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCalcRocColor.SelectedIndexChanged
        txtCalcColor.BackColor = Color.FromName(cmbCalcRocColor.SelectedItem.ToString)
    End Sub

    Private Sub cmbRocColor_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles cmbRocColor.SelectedIndexChanged
        txtColor.BackColor = Color.FromName(cmbRocColor.SelectedItem.ToString)
    End Sub

    Private Sub rbEnterSurvey_CheckedChanged(sender As Object, e As EventArgs) Handles rbEnterSurvey.CheckedChanged
        If rbEnterSurvey.Checked Then
            'txtTP.Enabled = True
            'txtTN.Enabled = True
            'txtFP.Enabled = True
            'txtFN.Enabled = True

            'txtCalcSensitivity.Enabled = False
            'txtCalcSpecificity.Enabled = False
            'txtCalcPrevalence.Enabled = False
            'txtCalcSampSize.Enabled = False

            txtTP.ReadOnly = False
            txtTN.ReadOnly = False
            txtFP.ReadOnly = False
            txtFN.ReadOnly = False

            txtCalcSensitivity.ReadOnly = True
            txtCalcSpecificity.ReadOnly = True
            txtCalcPrevalence.ReadOnly = True
            txtCalcSampSize.ReadOnly = True
        End If
    End Sub

    Private Sub rbEnterAnalysis_CheckedChanged(sender As Object, e As EventArgs) Handles rbEnterAnalysis.CheckedChanged
        If rbEnterAnalysis.Checked Then
            'txtTP.Enabled = False
            'txtTN.Enabled = False
            'txtFP.Enabled = False
            'txtFN.Enabled = False

            'txtCalcSensitivity.Enabled = True
            'txtCalcSpecificity.Enabled = True
            'txtCalcPrevalence.Enabled = True
            'txtCalcSampSize.Enabled = True

            txtTP.ReadOnly = True
            txtTN.ReadOnly = True
            txtFP.ReadOnly = True
            txtFN.ReadOnly = True

            txtCalcSensitivity.ReadOnly = False
            txtCalcSpecificity.ReadOnly = False
            txtCalcPrevalence.ReadOnly = False
            txtCalcSampSize.ReadOnly = False
        End If
    End Sub

    Private Sub btnClearSel_Click(sender As Object, e As EventArgs) Handles btnClearSel.Click
        'Clear the annotation labels selections.

        chkSelAnnotProbA.Checked = False
        chkSelAnnotProbNotA.Checked = False
        chkSelAnnotProbB.Checked = False
        chkSelAnnotProbNotB.Checked = False
        chkSelAnnotProbAandB.Checked = False
        chkSelAnnotProbAandNotB.Checked = False
        chkSelAnnotProbNotAandB.Checked = False
        chkSelAnnotProbNotAandNotB.Checked = False
        chkSelAnnotSampsA.Checked = False
        chkSelAnnotSampsNotA.Checked = False
        chkSelAnnotSampsB.Checked = False
        chkSelAnnotSampsNotB.Checked = False
        chkSelAnnotSampsAandB.Checked = False
        chkSelAnnotSampsAandNotB.Checked = False
        chkSelAnnotSampsNotAandB.Checked = False
        chkSelAnnotSampsNotAandNotB.Checked = False
        chkSelAnnotSampsSize.Checked = False

    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        'Move the selected annotation label up.

        ''Apply the move to the corresponding conditional labels:
        'If chkSelAnnotProbA.Checked Then Bayes.ProbA.ConditionalY = Bayes.ProbA.ConditionalY - MovePixels
        'If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.ConditionalY = Bayes.ProbNotA.ConditionalY - MovePixels
        'If chkSelAnnotProbB.Checked Then Bayes.ProbB.ConditionalY = Bayes.ProbB.ConditionalY - MovePixels
        'If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.ConditionalY = Bayes.ProbNotB.ConditionalY - MovePixels
        'If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.ConditionalY = Bayes.ProbAandB.ConditionalY - MovePixels
        'If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.ConditionalY = Bayes.ProbAandNotB.ConditionalY - MovePixels
        'If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.ConditionalY = Bayes.ProbNotAandB.ConditionalY - MovePixels
        'If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.ConditionalY = Bayes.ProbNotAandNotB.ConditionalY - MovePixels

        'If chkSelAnnotSampsA.Checked Then Bayes.SampsA.ConditionalY = Bayes.SampsA.ConditionalY - MovePixels
        'If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.ConditionalY = Bayes.SampsNotA.ConditionalY - MovePixels
        'If chkSelAnnotSampsB.Checked Then Bayes.SampsB.ConditionalY = Bayes.SampsB.ConditionalY - MovePixels
        'If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.ConditionalY = Bayes.SampsNotB.ConditionalY - MovePixels
        'If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.ConditionalY = Bayes.SampsAandB.ConditionalY - MovePixels
        'If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.ConditionalY = Bayes.SampsAandNotB.ConditionalY - MovePixels
        'If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.ConditionalY = Bayes.SampsNotAandB.ConditionalY - MovePixels
        'If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.ConditionalY = Bayes.SampsNotAandNotB.ConditionalY - MovePixels

        If chkUncondLabel.Checked Or Bayes.Settings.Condition = "None" Then 'Apply the move to the unconditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.Label.Y = Bayes.ProbA.Label.Y - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.Label.Y = Bayes.ProbNotA.Label.Y - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.Label.Y = Bayes.ProbB.Label.Y - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.Label.Y = Bayes.ProbNotB.Label.Y - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.Label.Y = Bayes.ProbAandB.Label.Y - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.Label.Y = Bayes.ProbAandNotB.Label.Y - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.Label.Y = Bayes.ProbNotAandB.Label.Y - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.Label.Y = Bayes.ProbNotAandNotB.Label.Y - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.Label.Y = Bayes.SampsA.Label.Y - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.Label.Y = Bayes.SampsNotA.Label.Y - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.Label.Y = Bayes.SampsB.Label.Y - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.Label.Y = Bayes.SampsNotB.Label.Y - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.Label.Y = Bayes.SampsAandB.Label.Y - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.Label.Y = Bayes.SampsAandNotB.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.Label.Y = Bayes.SampsNotAandB.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.Label.Y = Bayes.SampsNotAandNotB.Label.Y - MovePixels
        End If

        If chkGivenALabel.Checked Or Bayes.Settings.Condition = "EventATrue" Then 'Apply the move to the Given A conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenA.Label.Y = Bayes.ProbA.GivenA.Label.Y - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenA.Label.Y = Bayes.ProbNotA.GivenA.Label.Y - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenA.Label.Y = Bayes.ProbB.GivenA.Label.Y - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenA.Label.Y = Bayes.ProbNotB.GivenA.Label.Y - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenA.Label.Y = Bayes.ProbAandB.GivenA.Label.Y - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenA.Label.Y = Bayes.ProbAandNotB.GivenA.Label.Y - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenA.Label.Y = Bayes.ProbNotAandB.GivenA.Label.Y - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenA.Label.Y = Bayes.ProbNotAandNotB.GivenA.Label.Y - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenA.Label.Y = Bayes.SampsA.GivenA.Label.Y - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenA.Label.Y = Bayes.SampsNotA.GivenA.Label.Y - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenA.Label.Y = Bayes.SampsB.GivenA.Label.Y - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenA.Label.Y = Bayes.SampsNotB.GivenA.Label.Y - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenA.Label.Y = Bayes.SampsAandB.GivenA.Label.Y - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenA.Label.Y = Bayes.SampsAandNotB.GivenA.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenA.Label.Y = Bayes.SampsNotAandB.GivenA.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenA.Label.Y = Bayes.SampsNotAandNotB.GivenA.Label.Y - MovePixels
        End If

        If chkGivenNotALabel.Checked Or Bayes.Settings.Condition = "EventAFalse" Then 'Apply the move to the Given Not A conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenNotA.Label.Y = Bayes.ProbA.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenNotA.Label.Y = Bayes.ProbNotA.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenNotA.Label.Y = Bayes.ProbB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenNotA.Label.Y = Bayes.ProbNotB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenNotA.Label.Y = Bayes.ProbAandB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenNotA.Label.Y = Bayes.ProbAandNotB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenNotA.Label.Y = Bayes.ProbNotAandB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenNotA.Label.Y = Bayes.ProbNotAandNotB.GivenNotA.Label.Y - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenNotA.Label.Y = Bayes.SampsA.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenNotA.Label.Y = Bayes.SampsNotA.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenNotA.Label.Y = Bayes.SampsB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenNotA.Label.Y = Bayes.SampsNotB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenNotA.Label.Y = Bayes.SampsAandB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenNotA.Label.Y = Bayes.SampsAandNotB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenNotA.Label.Y = Bayes.SampsNotAandB.GivenNotA.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenNotA.Label.Y = Bayes.SampsNotAandNotB.GivenNotA.Label.Y - MovePixels
        End If

        If chkGivenBLabel.Checked Or Bayes.Settings.Condition = "EventBTrue" Then 'Apply the move to the Given B conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenB.Label.Y = Bayes.ProbA.GivenB.Label.Y - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenB.Label.Y = Bayes.ProbNotA.GivenB.Label.Y - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenB.Label.Y = Bayes.ProbB.GivenB.Label.Y - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenB.Label.Y = Bayes.ProbNotB.GivenB.Label.Y - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenB.Label.Y = Bayes.ProbAandB.GivenB.Label.Y - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenB.Label.Y = Bayes.ProbAandNotB.GivenB.Label.Y - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenB.Label.Y = Bayes.ProbNotAandB.GivenB.Label.Y - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenB.Label.Y = Bayes.ProbNotAandNotB.GivenB.Label.Y - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenB.Label.Y = Bayes.SampsA.GivenB.Label.Y - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenB.Label.Y = Bayes.SampsNotA.GivenB.Label.Y - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenB.Label.Y = Bayes.SampsB.GivenB.Label.Y - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenB.Label.Y = Bayes.SampsNotB.GivenB.Label.Y - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenB.Label.Y = Bayes.SampsAandB.GivenB.Label.Y - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenB.Label.Y = Bayes.SampsAandNotB.GivenB.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenB.Label.Y = Bayes.SampsNotAandB.GivenB.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenB.Label.Y = Bayes.SampsNotAandNotB.GivenB.Label.Y - MovePixels
        End If

        If chkGivenNotBLabel.Checked Or Bayes.Settings.Condition = "EventBFalse" Then 'Apply the move to the Given Not B conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenNotB.Label.Y = Bayes.ProbA.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenNotB.Label.Y = Bayes.ProbNotA.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenNotB.Label.Y = Bayes.ProbB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenNotB.Label.Y = Bayes.ProbNotB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenNotB.Label.Y = Bayes.ProbAandB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenNotB.Label.Y = Bayes.ProbAandNotB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenNotB.Label.Y = Bayes.ProbNotAandB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenNotB.Label.Y = Bayes.ProbNotAandNotB.GivenNotB.Label.Y - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenNotB.Label.Y = Bayes.SampsA.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenNotB.Label.Y = Bayes.SampsNotA.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenNotB.Label.Y = Bayes.SampsB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenNotB.Label.Y = Bayes.SampsNotB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenNotB.Label.Y = Bayes.SampsAandB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenNotB.Label.Y = Bayes.SampsAandNotB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenNotB.Label.Y = Bayes.SampsNotAandB.GivenNotB.Label.Y - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenNotB.Label.Y = Bayes.SampsNotAandNotB.GivenNotB.Label.Y - MovePixels
        End If

        DrawDiagram()
        UpdateAnnotProbPage()
    End Sub

    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        'Move the selected annotation label down.

        'If chkSelAnnotProbA.Checked Then Bayes.ProbA.Label.Y = Bayes.ProbA.Label.Y + MovePixels
        'If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.Label.Y = Bayes.ProbNotA.Label.Y + MovePixels
        'If chkSelAnnotProbB.Checked Then Bayes.ProbB.Label.Y = Bayes.ProbB.Label.Y + MovePixels
        'If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.Label.Y = Bayes.ProbNotB.Label.Y + MovePixels
        'If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.Label.Y = Bayes.ProbAandB.Label.Y + MovePixels
        'If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.Label.Y = Bayes.ProbAandNotB.Label.Y + MovePixels
        'If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.Label.Y = Bayes.ProbNotAandB.Label.Y + MovePixels
        'If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.Label.Y = Bayes.ProbNotAandNotB.Label.Y + MovePixels

        'If chkSelAnnotSampsA.Checked Then Bayes.SampsA.Label.Y = Bayes.SampsA.Label.Y + MovePixels
        'If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.Label.Y = Bayes.SampsNotA.Label.Y + MovePixels
        'If chkSelAnnotSampsB.Checked Then Bayes.SampsB.Label.Y = Bayes.SampsB.Label.Y + MovePixels
        'If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.Label.Y = Bayes.SampsNotB.Label.Y + MovePixels
        'If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.Label.Y = Bayes.SampsAandB.Label.Y + MovePixels
        'If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.Label.Y = Bayes.SampsAandNotB.Label.Y + MovePixels
        'If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.Label.Y = Bayes.SampsNotAandB.Label.Y + MovePixels
        'If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.Label.Y = Bayes.SampsNotAandNotB.Label.Y + MovePixels

        If chkUncondLabel.Checked Or Bayes.Settings.Condition = "None" Then 'Apply the move to the unconditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.Label.Y = Bayes.ProbA.Label.Y + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.Label.Y = Bayes.ProbNotA.Label.Y + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.Label.Y = Bayes.ProbB.Label.Y + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.Label.Y = Bayes.ProbNotB.Label.Y + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.Label.Y = Bayes.ProbAandB.Label.Y + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.Label.Y = Bayes.ProbAandNotB.Label.Y + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.Label.Y = Bayes.ProbNotAandB.Label.Y + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.Label.Y = Bayes.ProbNotAandNotB.Label.Y + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.Label.Y = Bayes.SampsA.Label.Y + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.Label.Y = Bayes.SampsNotA.Label.Y + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.Label.Y = Bayes.SampsB.Label.Y + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.Label.Y = Bayes.SampsNotB.Label.Y + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.Label.Y = Bayes.SampsAandB.Label.Y + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.Label.Y = Bayes.SampsAandNotB.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.Label.Y = Bayes.SampsNotAandB.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.Label.Y = Bayes.SampsNotAandNotB.Label.Y + MovePixels
        End If

        If chkGivenALabel.Checked Or Bayes.Settings.Condition = "EventATrue" Then 'Apply the move to the Given A conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenA.Label.Y = Bayes.ProbA.GivenA.Label.Y + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenA.Label.Y = Bayes.ProbNotA.GivenA.Label.Y + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenA.Label.Y = Bayes.ProbB.GivenA.Label.Y + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenA.Label.Y = Bayes.ProbNotB.GivenA.Label.Y + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenA.Label.Y = Bayes.ProbAandB.GivenA.Label.Y + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenA.Label.Y = Bayes.ProbAandNotB.GivenA.Label.Y + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenA.Label.Y = Bayes.ProbNotAandB.GivenA.Label.Y + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenA.Label.Y = Bayes.ProbNotAandNotB.GivenA.Label.Y + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenA.Label.Y = Bayes.SampsA.GivenA.Label.Y + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenA.Label.Y = Bayes.SampsNotA.GivenA.Label.Y + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenA.Label.Y = Bayes.SampsB.GivenA.Label.Y + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenA.Label.Y = Bayes.SampsNotB.GivenA.Label.Y + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenA.Label.Y = Bayes.SampsAandB.GivenA.Label.Y + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenA.Label.Y = Bayes.SampsAandNotB.GivenA.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenA.Label.Y = Bayes.SampsNotAandB.GivenA.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenA.Label.Y = Bayes.SampsNotAandNotB.GivenA.Label.Y + MovePixels
        End If

        If chkGivenNotALabel.Checked Or Bayes.Settings.Condition = "EventAFalse" Then 'Apply the move to the Given Not A conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenNotA.Label.Y = Bayes.ProbA.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenNotA.Label.Y = Bayes.ProbNotA.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenNotA.Label.Y = Bayes.ProbB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenNotA.Label.Y = Bayes.ProbNotB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenNotA.Label.Y = Bayes.ProbAandB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenNotA.Label.Y = Bayes.ProbAandNotB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenNotA.Label.Y = Bayes.ProbNotAandB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenNotA.Label.Y = Bayes.ProbNotAandNotB.GivenNotA.Label.Y + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenNotA.Label.Y = Bayes.SampsA.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenNotA.Label.Y = Bayes.SampsNotA.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenNotA.Label.Y = Bayes.SampsB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenNotA.Label.Y = Bayes.SampsNotB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenNotA.Label.Y = Bayes.SampsAandB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenNotA.Label.Y = Bayes.SampsAandNotB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenNotA.Label.Y = Bayes.SampsNotAandB.GivenNotA.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenNotA.Label.Y = Bayes.SampsNotAandNotB.GivenNotA.Label.Y + MovePixels
        End If

        If chkGivenBLabel.Checked Or Bayes.Settings.Condition = "EventBTrue" Then 'Apply the move to the Given B conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenB.Label.Y = Bayes.ProbA.GivenB.Label.Y + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenB.Label.Y = Bayes.ProbNotA.GivenB.Label.Y + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenB.Label.Y = Bayes.ProbB.GivenB.Label.Y + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenB.Label.Y = Bayes.ProbNotB.GivenB.Label.Y + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenB.Label.Y = Bayes.ProbAandB.GivenB.Label.Y + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenB.Label.Y = Bayes.ProbAandNotB.GivenB.Label.Y + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenB.Label.Y = Bayes.ProbNotAandB.GivenB.Label.Y + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenB.Label.Y = Bayes.ProbNotAandNotB.GivenB.Label.Y + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenB.Label.Y = Bayes.SampsA.GivenB.Label.Y + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenB.Label.Y = Bayes.SampsNotA.GivenB.Label.Y + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenB.Label.Y = Bayes.SampsB.GivenB.Label.Y + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenB.Label.Y = Bayes.SampsNotB.GivenB.Label.Y + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenB.Label.Y = Bayes.SampsAandB.GivenB.Label.Y + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenB.Label.Y = Bayes.SampsAandNotB.GivenB.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenB.Label.Y = Bayes.SampsNotAandB.GivenB.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenB.Label.Y = Bayes.SampsNotAandNotB.GivenB.Label.Y + MovePixels
        End If

        If chkGivenNotBLabel.Checked Or Bayes.Settings.Condition = "EventBFalse" Then 'Apply the move to the Given Not B conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenNotB.Label.Y = Bayes.ProbA.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenNotB.Label.Y = Bayes.ProbNotA.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenNotB.Label.Y = Bayes.ProbB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenNotB.Label.Y = Bayes.ProbNotB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenNotB.Label.Y = Bayes.ProbAandB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenNotB.Label.Y = Bayes.ProbAandNotB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenNotB.Label.Y = Bayes.ProbNotAandB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenNotB.Label.Y = Bayes.ProbNotAandNotB.GivenNotB.Label.Y + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenNotB.Label.Y = Bayes.SampsA.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenNotB.Label.Y = Bayes.SampsNotA.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenNotB.Label.Y = Bayes.SampsB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenNotB.Label.Y = Bayes.SampsNotB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenNotB.Label.Y = Bayes.SampsAandB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenNotB.Label.Y = Bayes.SampsAandNotB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenNotB.Label.Y = Bayes.SampsNotAandB.GivenNotB.Label.Y + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenNotB.Label.Y = Bayes.SampsNotAandNotB.GivenNotB.Label.Y + MovePixels
        End If

        DrawDiagram()
        UpdateAnnotProbPage()
    End Sub

    Private Sub btnLeft_Click(sender As Object, e As EventArgs) Handles btnLeft.Click
        'Move the selected annotation label left.

        'If chkSelAnnotProbA.Checked Then Bayes.ProbA.Label.MidX = Bayes.ProbA.Label.MidX - MovePixels
        'If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.Label.MidX = Bayes.ProbNotA.Label.MidX - MovePixels
        'If chkSelAnnotProbB.Checked Then Bayes.ProbB.Label.MidX = Bayes.ProbB.Label.MidX - MovePixels
        'If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.Label.MidX = Bayes.ProbNotB.Label.MidX - MovePixels
        'If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.Label.MidX = Bayes.ProbAandB.Label.MidX - MovePixels
        'If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.Label.MidX = Bayes.ProbAandNotB.Label.MidX - MovePixels
        'If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.Label.MidX = Bayes.ProbNotAandB.Label.MidX - MovePixels
        'If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.Label.MidX = Bayes.ProbNotAandNotB.Label.MidX - MovePixels

        'If chkSelAnnotSampsA.Checked Then Bayes.SampsA.Label.MidX = Bayes.SampsA.Label.MidX - MovePixels
        'If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.Label.MidX = Bayes.SampsNotA.Label.MidX - MovePixels
        'If chkSelAnnotSampsB.Checked Then Bayes.SampsB.Label.MidX = Bayes.SampsB.Label.MidX - MovePixels
        'If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.Label.MidX = Bayes.SampsNotB.Label.MidX - MovePixels
        'If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.Label.MidX = Bayes.SampsAandB.Label.MidX - MovePixels
        'If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.Label.MidX = Bayes.SampsAandNotB.Label.MidX - MovePixels
        'If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.Label.MidX = Bayes.SampsNotAandB.Label.MidX - MovePixels
        'If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.Label.MidX = Bayes.SampsNotAandNotB.Label.MidX - MovePixels

        If chkUncondLabel.Checked Or Bayes.Settings.Condition = "None" Then 'Apply the move to the unconditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.Label.MidX = Bayes.ProbA.Label.MidX - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.Label.MidX = Bayes.ProbNotA.Label.MidX - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.Label.MidX = Bayes.ProbB.Label.MidX - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.Label.MidX = Bayes.ProbNotB.Label.MidX - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.Label.MidX = Bayes.ProbAandB.Label.MidX - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.Label.MidX = Bayes.ProbAandNotB.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.Label.MidX = Bayes.ProbNotAandB.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.Label.MidX = Bayes.ProbNotAandNotB.Label.MidX - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.Label.MidX = Bayes.SampsA.Label.MidX - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.Label.MidX = Bayes.SampsNotA.Label.MidX - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.Label.MidX = Bayes.SampsB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.Label.MidX = Bayes.SampsNotB.Label.MidX - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.Label.MidX = Bayes.SampsAandB.Label.MidX - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.Label.MidX = Bayes.SampsAandNotB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.Label.MidX = Bayes.SampsNotAandB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.Label.MidX = Bayes.SampsNotAandNotB.Label.MidX - MovePixels
        End If

        If chkGivenALabel.Checked Or Bayes.Settings.Condition = "EventATrue" Then 'Apply the move to the Given A conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenA.Label.MidX = Bayes.ProbA.GivenA.Label.MidX - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenA.Label.MidX = Bayes.ProbNotA.GivenA.Label.MidX - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenA.Label.MidX = Bayes.ProbB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenA.Label.MidX = Bayes.ProbNotB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenA.Label.MidX = Bayes.ProbAandB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenA.Label.MidX = Bayes.ProbAandNotB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenA.Label.MidX = Bayes.ProbNotAandB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenA.Label.MidX = Bayes.ProbNotAandNotB.GivenA.Label.MidX - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenA.Label.MidX = Bayes.SampsA.GivenA.Label.MidX - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenA.Label.MidX = Bayes.SampsNotA.GivenA.Label.MidX - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenA.Label.MidX = Bayes.SampsB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenA.Label.MidX = Bayes.SampsNotB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenA.Label.MidX = Bayes.SampsAandB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenA.Label.MidX = Bayes.SampsAandNotB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenA.Label.MidX = Bayes.SampsNotAandB.GivenA.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenA.Label.MidX = Bayes.SampsNotAandNotB.GivenA.Label.MidX - MovePixels
        End If

        If chkGivenNotALabel.Checked Or Bayes.Settings.Condition = "EventAFalse" Then 'Apply the move to the Given Not A conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenNotA.Label.MidX = Bayes.ProbA.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenNotA.Label.MidX = Bayes.ProbNotA.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenNotA.Label.MidX = Bayes.ProbB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenNotA.Label.MidX = Bayes.ProbNotB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenNotA.Label.MidX = Bayes.ProbAandB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenNotA.Label.MidX = Bayes.ProbAandNotB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenNotA.Label.MidX = Bayes.ProbNotAandB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenNotA.Label.MidX = Bayes.ProbNotAandNotB.GivenNotA.Label.MidX - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenNotA.Label.MidX = Bayes.SampsA.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenNotA.Label.MidX = Bayes.SampsNotA.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenNotA.Label.MidX = Bayes.SampsB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenNotA.Label.MidX = Bayes.SampsNotB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenNotA.Label.MidX = Bayes.SampsAandB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenNotA.Label.MidX = Bayes.SampsAandNotB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenNotA.Label.MidX = Bayes.SampsNotAandB.GivenNotA.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenNotA.Label.MidX = Bayes.SampsNotAandNotB.GivenNotA.Label.MidX - MovePixels
        End If

        If chkGivenBLabel.Checked Or Bayes.Settings.Condition = "EventBTrue" Then 'Apply the move to the Given B conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenB.Label.MidX = Bayes.ProbA.GivenB.Label.MidX - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenB.Label.MidX = Bayes.ProbNotA.GivenB.Label.MidX - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenB.Label.MidX = Bayes.ProbB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenB.Label.MidX = Bayes.ProbNotB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenB.Label.MidX = Bayes.ProbAandB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenB.Label.MidX = Bayes.ProbAandNotB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenB.Label.MidX = Bayes.ProbNotAandB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenB.Label.MidX = Bayes.ProbNotAandNotB.GivenB.Label.MidX - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenB.Label.MidX = Bayes.SampsA.GivenB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenB.Label.MidX = Bayes.SampsNotA.GivenB.Label.MidX - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenB.Label.MidX = Bayes.SampsB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenB.Label.MidX = Bayes.SampsNotB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenB.Label.MidX = Bayes.SampsAandB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenB.Label.MidX = Bayes.SampsAandNotB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenB.Label.MidX = Bayes.SampsNotAandB.GivenB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenB.Label.MidX = Bayes.SampsNotAandNotB.GivenB.Label.MidX - MovePixels
        End If

        If chkGivenNotBLabel.Checked Or Bayes.Settings.Condition = "EventBFalse" Then 'Apply the move to the Given Not B conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenNotB.Label.MidX = Bayes.ProbA.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenNotB.Label.MidX = Bayes.ProbNotA.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenNotB.Label.MidX = Bayes.ProbB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenNotB.Label.MidX = Bayes.ProbNotB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenNotB.Label.MidX = Bayes.ProbAandB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenNotB.Label.MidX = Bayes.ProbAandNotB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenNotB.Label.MidX = Bayes.ProbNotAandB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenNotB.Label.MidX = Bayes.ProbNotAandNotB.GivenNotB.Label.MidX - MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenNotB.Label.MidX = Bayes.SampsA.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenNotB.Label.MidX = Bayes.SampsNotA.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenNotB.Label.MidX = Bayes.SampsB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenNotB.Label.MidX = Bayes.SampsNotB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenNotB.Label.MidX = Bayes.SampsAandB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenNotB.Label.MidX = Bayes.SampsAandNotB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenNotB.Label.MidX = Bayes.SampsNotAandB.GivenNotB.Label.MidX - MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenNotB.Label.MidX = Bayes.SampsNotAandNotB.GivenNotB.Label.MidX - MovePixels
        End If

        DrawDiagram()
        UpdateAnnotProbPage()
    End Sub

    Private Sub btnRight_Click(sender As Object, e As EventArgs) Handles btnRight.Click
        'Move the selected annotation label right.

        'If chkSelAnnotProbA.Checked Then Bayes.ProbA.Label.MidX = Bayes.ProbA.Label.MidX + MovePixels
        'If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.Label.MidX = Bayes.ProbNotA.Label.MidX + MovePixels
        'If chkSelAnnotProbB.Checked Then Bayes.ProbB.Label.MidX = Bayes.ProbB.Label.MidX + MovePixels
        'If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.Label.MidX = Bayes.ProbNotB.Label.MidX + MovePixels
        'If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.Label.MidX = Bayes.ProbAandB.Label.MidX + MovePixels
        'If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.Label.MidX = Bayes.ProbAandNotB.Label.MidX + MovePixels
        'If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.Label.MidX = Bayes.ProbNotAandB.Label.MidX + MovePixels
        'If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.Label.MidX = Bayes.ProbNotAandNotB.Label.MidX + MovePixels

        'If chkSelAnnotSampsA.Checked Then Bayes.SampsA.Label.MidX = Bayes.SampsA.Label.MidX + MovePixels
        'If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.Label.MidX = Bayes.SampsNotA.Label.MidX + MovePixels
        'If chkSelAnnotSampsB.Checked Then Bayes.SampsB.Label.MidX = Bayes.SampsB.Label.MidX + MovePixels
        'If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.Label.MidX = Bayes.SampsNotB.Label.MidX + MovePixels
        'If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.Label.MidX = Bayes.SampsAandB.Label.MidX + MovePixels
        'If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.Label.MidX = Bayes.SampsAandNotB.Label.MidX + MovePixels
        'If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.Label.MidX = Bayes.SampsNotAandB.Label.MidX + MovePixels
        'If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.Label.MidX = Bayes.SampsNotAandNotB.Label.MidX + MovePixels

        If chkUncondLabel.Checked Or Bayes.Settings.Condition = "None" Then 'Apply the move to the unconditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.Label.MidX = Bayes.ProbA.Label.MidX + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.Label.MidX = Bayes.ProbNotA.Label.MidX + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.Label.MidX = Bayes.ProbB.Label.MidX + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.Label.MidX = Bayes.ProbNotB.Label.MidX + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.Label.MidX = Bayes.ProbAandB.Label.MidX + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.Label.MidX = Bayes.ProbAandNotB.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.Label.MidX = Bayes.ProbNotAandB.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.Label.MidX = Bayes.ProbNotAandNotB.Label.MidX + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.Label.MidX = Bayes.SampsA.Label.MidX + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.Label.MidX = Bayes.SampsNotA.Label.MidX + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.Label.MidX = Bayes.SampsB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.Label.MidX = Bayes.SampsNotB.Label.MidX + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.Label.MidX = Bayes.SampsAandB.Label.MidX + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.Label.MidX = Bayes.SampsAandNotB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.Label.MidX = Bayes.SampsNotAandB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.Label.MidX = Bayes.SampsNotAandNotB.Label.MidX + MovePixels
        End If

        If chkGivenALabel.Checked Or Bayes.Settings.Condition = "EventATrue" Then 'Apply the move to the Given A conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenA.Label.MidX = Bayes.ProbA.GivenA.Label.MidX + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenA.Label.MidX = Bayes.ProbNotA.GivenA.Label.MidX + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenA.Label.MidX = Bayes.ProbB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenA.Label.MidX = Bayes.ProbNotB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenA.Label.MidX = Bayes.ProbAandB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenA.Label.MidX = Bayes.ProbAandNotB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenA.Label.MidX = Bayes.ProbNotAandB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenA.Label.MidX = Bayes.ProbNotAandNotB.GivenA.Label.MidX + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenA.Label.MidX = Bayes.SampsA.GivenA.Label.MidX + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenA.Label.MidX = Bayes.SampsNotA.GivenA.Label.MidX + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenA.Label.MidX = Bayes.SampsB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenA.Label.MidX = Bayes.SampsNotB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenA.Label.MidX = Bayes.SampsAandB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenA.Label.MidX = Bayes.SampsAandNotB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenA.Label.MidX = Bayes.SampsNotAandB.GivenA.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenA.Label.MidX = Bayes.SampsNotAandNotB.GivenA.Label.MidX + MovePixels
        End If

        If chkGivenNotALabel.Checked Or Bayes.Settings.Condition = "EventAFalse" Then 'Apply the move to the Given Not A conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenNotA.Label.MidX = Bayes.ProbA.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenNotA.Label.MidX = Bayes.ProbNotA.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenNotA.Label.MidX = Bayes.ProbB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenNotA.Label.MidX = Bayes.ProbNotB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenNotA.Label.MidX = Bayes.ProbAandB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenNotA.Label.MidX = Bayes.ProbAandNotB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenNotA.Label.MidX = Bayes.ProbNotAandB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenNotA.Label.MidX = Bayes.ProbNotAandNotB.GivenNotA.Label.MidX + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenNotA.Label.MidX = Bayes.SampsA.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenNotA.Label.MidX = Bayes.SampsNotA.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenNotA.Label.MidX = Bayes.SampsB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenNotA.Label.MidX = Bayes.SampsNotB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenNotA.Label.MidX = Bayes.SampsAandB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenNotA.Label.MidX = Bayes.SampsAandNotB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenNotA.Label.MidX = Bayes.SampsNotAandB.GivenNotA.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenNotA.Label.MidX = Bayes.SampsNotAandNotB.GivenNotA.Label.MidX + MovePixels
        End If

        If chkGivenBLabel.Checked Or Bayes.Settings.Condition = "EventBTrue" Then 'Apply the move to the Given B conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenB.Label.MidX = Bayes.ProbA.GivenB.Label.MidX + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenB.Label.MidX = Bayes.ProbNotA.GivenB.Label.MidX + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenB.Label.MidX = Bayes.ProbB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenB.Label.MidX = Bayes.ProbNotB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenB.Label.MidX = Bayes.ProbAandB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenB.Label.MidX = Bayes.ProbAandNotB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenB.Label.MidX = Bayes.ProbNotAandB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenB.Label.MidX = Bayes.ProbNotAandNotB.GivenB.Label.MidX + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenB.Label.MidX = Bayes.SampsA.GivenB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenB.Label.MidX = Bayes.SampsNotA.GivenB.Label.MidX + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenB.Label.MidX = Bayes.SampsB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenB.Label.MidX = Bayes.SampsNotB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenB.Label.MidX = Bayes.SampsAandB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenB.Label.MidX = Bayes.SampsAandNotB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenB.Label.MidX = Bayes.SampsNotAandB.GivenB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenB.Label.MidX = Bayes.SampsNotAandNotB.GivenB.Label.MidX + MovePixels
        End If

        If chkGivenNotBLabel.Checked Or Bayes.Settings.Condition = "EventBFalse" Then 'Apply the move to the Given Not B conditional labels:
            If chkSelAnnotProbA.Checked Then Bayes.ProbA.GivenNotB.Label.MidX = Bayes.ProbA.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotProbNotA.Checked Then Bayes.ProbNotA.GivenNotB.Label.MidX = Bayes.ProbNotA.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotProbB.Checked Then Bayes.ProbB.GivenNotB.Label.MidX = Bayes.ProbB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotProbNotB.Checked Then Bayes.ProbNotB.GivenNotB.Label.MidX = Bayes.ProbNotB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotProbAandB.Checked Then Bayes.ProbAandB.GivenNotB.Label.MidX = Bayes.ProbAandB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotProbAandNotB.Checked Then Bayes.ProbAandNotB.GivenNotB.Label.MidX = Bayes.ProbAandNotB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandB.Checked Then Bayes.ProbNotAandB.GivenNotB.Label.MidX = Bayes.ProbNotAandB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotProbNotAandNotB.Checked Then Bayes.ProbNotAandNotB.GivenNotB.Label.MidX = Bayes.ProbNotAandNotB.GivenNotB.Label.MidX + MovePixels

            If chkSelAnnotSampsA.Checked Then Bayes.SampsA.GivenNotB.Label.MidX = Bayes.SampsA.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotA.Checked Then Bayes.SampsNotA.GivenNotB.Label.MidX = Bayes.SampsNotA.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotSampsB.Checked Then Bayes.SampsB.GivenNotB.Label.MidX = Bayes.SampsB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotB.Checked Then Bayes.SampsNotB.GivenNotB.Label.MidX = Bayes.SampsNotB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotSampsAandB.Checked Then Bayes.SampsAandB.GivenNotB.Label.MidX = Bayes.SampsAandB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotSampsAandNotB.Checked Then Bayes.SampsAandNotB.GivenNotB.Label.MidX = Bayes.SampsAandNotB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandB.Checked Then Bayes.SampsNotAandB.GivenNotB.Label.MidX = Bayes.SampsNotAandB.GivenNotB.Label.MidX + MovePixels
            If chkSelAnnotSampsNotAandNotB.Checked Then Bayes.SampsNotAandNotB.GivenNotB.Label.MidX = Bayes.SampsNotAandNotB.GivenNotB.Label.MidX + MovePixels
        End If

        DrawDiagram()
        UpdateAnnotProbPage()
    End Sub

    Private Sub numPixels_ValueChanged(sender As Object, e As EventArgs) Handles numPixels.ValueChanged
        MovePixels = numPixels.Value
    End Sub

    Private Sub btnUpdatePage_Click(sender As Object, e As EventArgs) Handles btnUpdatePage.Click
        UpdateAnnotProbPage()
    End Sub

    Private Sub chkSelAnnotProbA_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotProbA.CheckedChanged
        If chkSelAnnotProbA.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotSampsA.Checked = chkSelAnnotProbA.Checked
        End If
    End Sub

    Private Sub chkSelAnnotProbNotA_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotProbNotA.CheckedChanged
        If chkSelAnnotProbNotA.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotSampsNotA.Checked = chkSelAnnotProbNotA.Checked
        End If
    End Sub

    Private Sub chkSelAnnotProbB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotProbB.CheckedChanged
        If chkSelAnnotProbB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotSampsB.Checked = chkSelAnnotProbB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotProbNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotProbNotB.CheckedChanged
        If chkSelAnnotProbNotB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotSampsNotB.Checked = chkSelAnnotProbNotB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotProbAandB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotProbAandB.CheckedChanged
        If chkSelAnnotProbAandB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotSampsAandB.Checked = chkSelAnnotProbAandB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotProbAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotProbAandNotB.CheckedChanged
        If chkSelAnnotProbAandNotB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotSampsAandNotB.Checked = chkSelAnnotProbAandNotB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotProbNotAandB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotProbNotAandB.CheckedChanged
        If chkSelAnnotProbNotAandB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotSampsNotAandB.Checked = chkSelAnnotProbNotAandB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotProbNotAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotProbNotAandNotB.CheckedChanged
        If chkSelAnnotProbNotAandNotB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotSampsNotAandNotB.Checked = chkSelAnnotProbNotAandNotB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotSampsA_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotSampsA.CheckedChanged
        If chkSelAnnotSampsA.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotProbA.Checked = chkSelAnnotSampsA.Checked
        End If
    End Sub

    Private Sub chkSelAnnotSampsNotA_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotSampsNotA.CheckedChanged
        If chkSelAnnotSampsNotA.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotProbNotA.Checked = chkSelAnnotSampsNotA.Checked
        End If
    End Sub

    Private Sub chkSelAnnotSampsB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotSampsB.CheckedChanged
        If chkSelAnnotSampsB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotProbB.Checked = chkSelAnnotSampsB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotSampsNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotSampsNotB.CheckedChanged
        If chkSelAnnotSampsNotB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotProbNotB.Checked = chkSelAnnotSampsNotB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotSampsAandB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotSampsAandB.CheckedChanged
        If chkSelAnnotSampsAandB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotProbAandB.Checked = chkSelAnnotSampsAandB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotSampsAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotSampsAandNotB.CheckedChanged
        If chkSelAnnotSampsAandNotB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotProbAandNotB.Checked = chkSelAnnotSampsAandNotB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotSampsNotAandB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotSampsNotAandB.CheckedChanged
        If chkSelAnnotSampsNotAandB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotProbNotAandB.Checked = chkSelAnnotSampsNotAandB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotSampsNotAandNotB_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotSampsNotAandNotB.CheckedChanged
        If chkSelAnnotSampsNotAandNotB.Focused Then
            If chkSelProbSamp.Checked Then chkSelAnnotProbNotAandNotB.Checked = chkSelAnnotSampsNotAandNotB.Checked
        End If
    End Sub

    Private Sub chkSelAnnotSampsSize_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelAnnotSampsSize.CheckedChanged

    End Sub

    Private Sub rbLabelCondNone_CheckedChanged(sender As Object, e As EventArgs) Handles rbLabelCondNone.CheckedChanged
        If rbLabelCondNone.Focused Then
            Bayes.Settings.Condition = "None"
            If rbLabelCondNone.Checked Then rbConditionNone.Checked = True
            UpdateAnnotProbPage()
        End If
    End Sub

    Private Sub rbLabelCondA_CheckedChanged(sender As Object, e As EventArgs) Handles rbLabelCondA.CheckedChanged
        If rbLabelCondA.Focused Then
            Bayes.Settings.Condition = "EventATrue"
            If rbLabelCondA.Checked Then rbConditionA.Checked = True
            UpdateAnnotProbPage()
        End If
    End Sub

    Private Sub rbLabelCondNotA_CheckedChanged(sender As Object, e As EventArgs) Handles rbLabelCondNotA.CheckedChanged
        If rbLabelCondNotA.Focused Then
            Bayes.Settings.Condition = "EventAFalse"
            If rbLabelCondNotA.Checked Then rbConditionNotA.Checked = True
            UpdateAnnotProbPage()
        End If
    End Sub

    Private Sub rbLabelCondB_CheckedChanged(sender As Object, e As EventArgs) Handles rbLabelCondB.CheckedChanged
        If rbLabelCondB.Focused Then
            Bayes.Settings.Condition = "EventBTrue"
            If rbLabelCondB.Checked Then rbConditionB.Checked = True
            UpdateAnnotProbPage()
        End If
    End Sub

    Private Sub rbLabelCondNotB_CheckedChanged(sender As Object, e As EventArgs) Handles rbLabelCondNotB.CheckedChanged
        If rbLabelCondNotB.Focused Then
            Bayes.Settings.Condition = "EventBFalse"
            If rbLabelCondNotB.Checked Then rbConditionNotB.Checked = True
            UpdateAnnotProbPage()
        End If
    End Sub

    Private Sub btnConfidence_Click(sender As Object, e As EventArgs) Handles btnConfidence.Click
        'Update the confidence interval
        'NOTE: the is updated when the focus has left the Confidence text box.
        'WilsonInterval()

    End Sub

    Private Sub txtConfid_TextChanged(sender As Object, e As EventArgs) Handles txtConfidence.TextChanged

    End Sub

    Private Sub txtConfid_LostFocus(sender As Object, e As EventArgs) Handles txtConfidence.LostFocus
        'The Confidence level may have changed.

        Try
            Dim ConfidValue As Double
            If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
                ConfidValue = txtConfidence.Text
                If Confidence <> ConfidValue Then
                    Confidence = ConfidValue 'Only set the DefinedValue if the Value has changed
                    WilsonInterval()
                End If
            ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
                ConfidValue = txtConfidence.Text.Replace("%", "")
                ConfidValue = ConfidValue / 100
                If Confidence <> ConfidValue Then
                    Confidence = ConfidValue 'Only set the DefinedValue if the Value has changed
                    WilsonInterval()
                End If
            Else
                Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
            End If

        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub WilsonInterval()
        'Calculates the Wilson Confidence Interval for the survey probability estimates.

        'Formulas are from the Research Article: "Ensemble confidence intervals for binomial proportions" by Hayeon Park & Lawrence M. Leemis, Statistics in Medicine. 2019;38:3460–3475.
        'See also: https://www.itl.nist.gov/div898/handbook/prc/section2/prc241.htm

        'The confidence intervals can be checked using these pages:
        'https://www.statskingdom.com/41_proportion_confidence_interval.html
        'https://epitools.ausvet.com.au/ciproportion

        Dim N As Long = Bayes.SampleSize.Value 'The number of samples in the survey
        Dim P As Double 'The estimated probability
        Dim Alpha As Double = 1 - Confidence 'The required confidence level. The Confidence property is edited using the txtConfidence text box.
        Dim ZHA As Double = InvStdNormalCdf(1 - (Alpha / 2)) 'This is the (1 - Alpha/2) percentile of the standard normal distribution.
        Dim ZHASq As Double = ZHA ^ 2

        'Calculate intervals for P(AandB):
        P = Bayes.ProbAandB.Value
        Dim LowerProb As Double = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) - ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        Dim UpperProb As Double = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) + ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        txtPLBAandB.Text = ProbString(LowerProb)
        txtPUBAandB.Text = ProbString(UpperProb)

        'Calculate intervals for P(NotAandB):
        P = Bayes.ProbNotAandB.Value
        LowerProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) - ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        UpperProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) + ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        txtPLBNotAandB.Text = ProbString(LowerProb)
        txtPUBNotAandB.Text = ProbString(UpperProb)

        'Calculate intervals for P(NotAandNotB):
        P = Bayes.ProbNotAandNotB.Value
        LowerProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) - ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        UpperProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) + ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        txtPLBNotAandNotB.Text = ProbString(LowerProb)
        txtPUBNotAandNotB.Text = ProbString(UpperProb)

        'Calculate intervals for P(AandNotB):
        P = Bayes.ProbAandNotB.Value
        LowerProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) - ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        UpperProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) + ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        txtPLBAandNotB.Text = ProbString(LowerProb)
        txtPUBAandNotB.Text = ProbString(UpperProb)

        'Calculate intervals for P(A):
        P = Bayes.ProbA.Value
        LowerProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) - ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        UpperProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) + ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        txtPLBA.Text = ProbString(LowerProb)
        txtPUBA.Text = ProbString(UpperProb)

        'Calculate intervals for P(B):
        P = Bayes.ProbB.Value
        LowerProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) - ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        UpperProb = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) + ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
        txtPLBB.Text = ProbString(LowerProb)
        txtPUBB.Text = ProbString(UpperProb)

    End Sub

    'Private Sub Wilson()
    '    'Calculate the Wilson Score Interval.
    '    'FOR TESTING.

    '    Dim Confidence As Double = txtConfid.Text 'The required confidence level for the interval. The is the decimal confidence level (between 0 and 1).
    '    Dim N As Long = txtSurveySize.Text 'The number of samples in the survey
    '    Dim P As Double = txtEstProb.Text 'The estimated probability of success

    '    'If the required confidence level is 95%, the decimal confidence level is 0.95 and alpha is 1 - 0.95 = 0.05.
    '    Dim Alpha As Double = 1 - Confidence

    '    'Dim KA As Double = InvStdNormalCdf(Alpha)
    '    'Dim KvalSq As Double = InvStdNormalCdf(1 - (Alpha / 2)) ^ 2

    '    'Dim LowerProb As Double = (2 * N * P + KvalSq - KA * Math.Sqrt(4 * N * P * (1 - P) + KvalSq)) / (2 * (N + KvalSq))
    '    'Dim UpperProb As Double = (2 * N * P + KvalSq + KA * Math.Sqrt(4 * N * P * (1 - P) + KvalSq)) / (2 * (N + KvalSq))


    '    Dim ZHA As Double = InvStdNormalCdf(1 - (Alpha / 2)) 'This is the (1 - Alpha/2) percentile of the standard normal distribution.
    '    'Dim ZHASq As Double = InvStdNormalCdf(1 - (Alpha / 2)) ^ 2
    '    Dim ZHASq As Double = ZHA ^ 2

    '    'Formulas are from the Research Article: "Ensemble confidence intervals for binomial proportions" by Hayeon Park & Lawrence M. Leemis, Statistics in Medicine. 2019;38:3460–3475.
    '    Dim LowerProb As Double = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) - ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
    '    Dim UpperProb As Double = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) + ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))

    '    Dim Center As Double = P + ZHASq / (2 * N) / (1 + ZHASq / N)

    '    txtLower.Text = LowerProb
    '    txtUpper.Text = UpperProb
    '    txtCenter.Text = Center

    'End Sub

    'Private Sub txtSndX_TextChanged(sender As Object, e As EventArgs)
    '    'The Standard Normal Distribution X value has changed.
    '    'Recalculate the probability density at X

    '    Try
    '        Dim X As Double = txtSndX.Text
    '        Dim PD As Double
    '        'PD = (1 / Math.Sqrt(2 * Math.PI)) * Math.E ^ (-X ^ 2 / 2)
    '        PD = (1 / Math.Sqrt(2 * Math.PI)) * Math.Exp(-0.5 * X ^ 2)
    '        txtSndPdf.Text = PD
    '        txtSndCdf.Text = StdNormalCdf(X)
    '    Catch ex As Exception

    '    End Try


    'End Sub

    'Private Sub txtInputCdf_TextChanged(sender As Object, e As EventArgs)
    '    'The Input Standard Normal CDF value has changed.
    '    'Recalculate the Inverse CDF value.

    '    Try
    '        Dim CDF As Double = txtInputCdf.Text
    '        txtInvCdf.Text = InvStdNormalCdf(CDF)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub GenWilsonInterval()
        'Calculates the Wilson Confidence Interval for the survey probability estimates.
        'This method uses the data in the General Survey Confidence Inteval Calculator group box.
        Try
            Dim N As Long = GenSurveySize 'The number of samples in the survey
            Dim P As Double = GenMLProbEvent 'The estimated probability
            Dim Alpha As Double = 1 - GenConfidence 'The required confidence level. The Confidence property is edited using the txtConfidence text box.
            Dim ZHA As Double = InvStdNormalCdf(1 - (Alpha / 2)) 'This is the (1 - Alpha/2) percentile of the standard normal distribution.
            Dim ZHASq As Double = ZHA ^ 2

            'Calculate the confidence intervals:
            Dim LowerProb As Double = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) - ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
            Dim UpperProb As Double = (1 / (1 + ZHASq / N)) * (P + ZHASq / (2 * N) + ZHA * Math.Sqrt(P * (1 - P) / N + ZHASq / (4 * N * N)))
            txtPLBEvent.Text = ProbString(LowerProb)
            txtPUBEvent.Text = ProbString(UpperProb)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RedisplayGenConfIntVals()
        'Redisplay the General Confidence Interval Calculator values.
        txtGenConfid.Text = ProbString(GenConfidence)
        txtGenSurveySize.Text = SampString(GenSurveySize)
        txtGenNEvent.Text = SampString(GenNEvent)
        txtPMLEvent.Text = ProbString(GenMLProbEvent)
        GenWilsonInterval()
    End Sub

    Private Sub txtGenConfid_TextChanged(sender As Object, e As EventArgs) Handles txtGenConfid.TextChanged

    End Sub

    Private Sub txtGenConfid_LostFocus(sender As Object, e As EventArgs) Handles txtGenConfid.LostFocus
        'The General Calc Confidence level may have changed.
        Try
            Dim ConfidValue As Double
            If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
                ConfidValue = txtGenConfid.Text
                txtGenConfid.Text = ProbString(ConfidValue) 'Redisplay the confidence level with the specified format.
                If GenConfidence <> ConfidValue Then
                    GenConfidence = ConfidValue 'Only set the GenConfidence if the Value has changed
                    GenWilsonInterval()
                End If
            ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
                ConfidValue = txtGenConfid.Text.Replace("%", "")
                ConfidValue = ConfidValue / 100
                txtGenConfid.Text = ProbString(ConfidValue) 'Redisplay the confidence level with the specified format.
                If GenConfidence <> ConfidValue Then
                    GenConfidence = ConfidValue 'Only set the GenConfidence if the Value has changed
                    GenWilsonInterval()
                End If
            Else
                Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
            End If

        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtGenSurveySize_TextChanged(sender As Object, e As EventArgs) Handles txtGenSurveySize.TextChanged

    End Sub

    Private Sub txtGenSurveySize_LostFocus(sender As Object, e As EventArgs) Handles txtGenSurveySize.LostFocus
        'The General Calc Survey Size may have changed.
        Try
            Dim SurveySize As Double
            SurveySize = txtGenSurveySize.Text
            txtGenSurveySize.Text = SampString(SurveySize) 'Redisplay the survey size with the specified format.
            If GenSurveySize <> SurveySize Then
                GenSurveySize = SurveySize 'Only set the GenSurveySize if the Value has changed
                txtGenNEvent.Text = SampString(GenNEvent) 'Changing the survey size changes the survey event count
                GenWilsonInterval()
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtGenNEvent_TextChanged(sender As Object, e As EventArgs) Handles txtGenNEvent.TextChanged

    End Sub

    Private Sub txtGenNEvent_LostFocus(sender As Object, e As EventArgs) Handles txtGenNEvent.LostFocus
        'The General Calc Survey Event Count may have changed.
        Try
            Dim NEvent As Double
            NEvent = txtGenNEvent.Text
            txtGenNEvent.Text = SampString(NEvent) 'Redisplay the survey event count with the specified format.
            If GenNEvent <> NEvent Then
                GenNEvent = NEvent 'Only set the GenNEvent if the Value has changed
                txtPMLEvent.Text = ProbString(GenMLProbEvent) 'Changing the survey event count changes the most likely event probability
                GenWilsonInterval()
            End If
        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Sub txtPMLEvent_TextChanged(sender As Object, e As EventArgs) Handles txtPMLEvent.TextChanged

    End Sub

    Private Sub txtPMLEvent_LostFocus(sender As Object, e As EventArgs) Handles txtPMLEvent.LostFocus
        'The General Calc Most Likely Event Probability may have changed.
        Try
            Dim PEvent As Double
            If Bayes.Settings.ProbabilityMeasure = "Decimal" Then
                PEvent = txtPMLEvent.Text
                txtPMLEvent.Text = ProbString(PEvent)  'Redisplay the survey event probability with the specified format.
                If GenMLProbEvent <> PEvent Then
                    GenMLProbEvent = PEvent 'Only set the GenMLProbEvent if the Value has changed
                    txtGenNEvent.Text = SampString(GenNEvent) 'Changing the event probability changes the survey event count
                    GenWilsonInterval()
                End If
            ElseIf Bayes.Settings.ProbabilityMeasure = "Percent" Then
                PEvent = txtPMLEvent.Text.Replace("%", "")
                PEvent = PEvent / 100
                txtPMLEvent.Text = ProbString(PEvent)  'Redisplay the survey event probability with the specified format.
                If GenMLProbEvent <> PEvent Then
                    GenMLProbEvent = PEvent 'Only set the GenMLProbEvent if the Value has changed
                    txtGenNEvent.Text = SampString(GenNEvent) 'Changing the event probability changes the survey event count
                    GenWilsonInterval()
                End If
            Else
                Message.AddWarning("Unknown probability measure: " & Bayes.Settings.ProbabilityMeasure & vbCrLf)
            End If

        Catch ex As Exception
            Message.AddWarning(ex.Message & vbCrLf)
        End Try
    End Sub

    Private Function StdNormalCdf(ByRef X As Double) As Double
        'An approximation of the standard cumulative Normal distribution.
        'Abramowitz & Stegun (1964)
        'http://www.vbforums.com/showthread.php?359156-Calculate-Normal-Distribution
        'https://www.mathworks.com/matlabcentral/mlc-downloads/downloads/submissions/7025/versions/1/previews/normcdfM.m/index.html

        Dim PosX As Double
        If X >= 0 Then
            PosX = X
        Else
            PosX = -X
        End If

        Dim P As Double = 0.2316419
        'Dim T As Double = 1 / (1 + P * X)
        Dim T As Double = 1 / (1 + P * PosX)
        Dim B1 As Double = 0.31938153
        Dim B2 As Double = -0.356563782
        Dim B3 As Double = 1.781477937
        Dim B4 As Double = -1.821255978
        Dim B5 As Double = 1.330274429
        'Dim Z As Double = 1 / (Math.Sqrt(2 * Math.PI)) * Math.Exp(-X ^ 2 / 2)
        Dim Z As Double = 1 / (Math.Sqrt(2 * Math.PI)) * Math.Exp(-PosX ^ 2 / 2)

        If X >= 0 Then
            Return 1 - Z * (B1 * T + B2 * T ^ 2 + B3 * T ^ 3 + B4 * T ^ 4 + B5 * T ^ 5)
        Else
            Return Z * (B1 * T + B2 * T ^ 2 + B3 * T ^ 3 + B4 * T ^ 4 + B5 * T ^ 5)
        End If
    End Function

    Private Function InvStdNormalCdf(ByRef Prob As Double) As Double
        'The Inverse Standard Cumulative Normal distribution.
        'https://www.source-code.biz/snippets/vbasic/9.htm

        Const a1 = -39.6968302866538, a2 = 220.946098424521, a3 = -275.928510446969
        Const a4 = 138.357751867269, a5 = -30.6647980661472, a6 = 2.50662827745924
        Const b1 = -54.4760987982241, b2 = 161.585836858041, b3 = -155.698979859887
        Const b4 = 66.8013118877197, b5 = -13.2806815528857, c1 = -0.00778489400243029
        Const c2 = -0.322396458041136, c3 = -2.40075827716184, c4 = -2.54973253934373
        Const c5 = 4.37466414146497, c6 = 2.93816398269878, d1 = 0.00778469570904146
        Const d2 = 0.32246712907004, d3 = 2.445134137143, d4 = 3.75440866190742
        Const PLow = 0.02425
        Const PHigh = 1 - PLow
        Dim q As Double
        Dim r As Double
        If Prob < 0 Or Prob > 1 Then
            Message.AddWarning("Probability value not between 0 and 1: " & Prob & vbCrLf)
            Return Double.NaN
        ElseIf Prob < PLow Then
            q = Math.Sqrt(-2 * Math.Log(Prob))
            Return (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)

        ElseIf Prob <= PHigh Then
            q = Prob - 0.5 : r = q * q
            Return (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q / (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1)
        Else
            q = Math.Sqrt(-2 * Math.Log(1 - Prob))
            Return -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1)
        End If
    End Function


#End Region 'Form Methods ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Events - Events raised by this form." '=========================================================================================================================================

#End Region 'Form Events ----------------------------------------------------------------------------------------------------------------------------------------------------------------------


#Region " Form Classes - Other classes used by this form." '===================================================================================================================================

    Public Class clsSendMessageParams
        'Parameters used when sending a message using the Message Service.
        Public ProjectNetworkName As String
        Public ConnectionName As String
        Public Message As String
    End Class

    Public Class clsInstructionParams
        'Parameters used when executing an instruction.
        Public Info As String 'The information in an instruction.
        Public Locn As String 'The location to send the information.
    End Class

    Private Sub txtAProb_TextChanged(sender As Object, e As EventArgs) Handles txtAProb.TextChanged

    End Sub

    Private Sub txtNotAProb_TextChanged(sender As Object, e As EventArgs) Handles txtNotAProb.TextChanged

    End Sub

    Private Sub txtBProb_TextChanged(sender As Object, e As EventArgs) Handles txtBProb.TextChanged

    End Sub

    Private Sub txtNotBProb_TextChanged(sender As Object, e As EventArgs) Handles txtNotBProb.TextChanged

    End Sub

    Private Sub Label218_Click(sender As Object, e As EventArgs) Handles Label218.Click

    End Sub

    Private Sub Label254_Click(sender As Object, e As EventArgs) Handles Label254.Click

    End Sub

    Private Sub chkUncondLabel_CheckedChanged(sender As Object, e As EventArgs) Handles chkUncondLabel.CheckedChanged

    End Sub

    Private Sub txtAnnotProbAandBVal_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbAandBVal.TextChanged

    End Sub

    Private Sub txtAnnotProbAandBVal_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbAandBVal.LostFocus

    End Sub

    Private Sub txtAnnotProbNotBVal_TextChanged(sender As Object, e As EventArgs) Handles txtAnnotProbNotBVal.TextChanged

    End Sub

    Private Sub txtAnnotProbNotBVal_LostFocus(sender As Object, e As EventArgs) Handles txtAnnotProbNotBVal.LostFocus

    End Sub







































































































































#End Region 'Form Classes ---------------------------------------------------------------------------------------------------------------------------------------------------------------------


End Class


