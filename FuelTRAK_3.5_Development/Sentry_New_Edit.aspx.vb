Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Class Sentry
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim i As Integer
    Dim DropTemp As DropDownList
    Dim dal As New GeneralizedDAL
    Dim ArrPulser As ArrayList
    Dim strMessage As String

    'Added By Varun To Test Polling
    Private sentryNumber As String
    Private sentryType As String
    Private cmdType As String
    Private queueStatus As String
    Private timeQueue As DateTime
    Private timePollingStart As DateTime
    Private timePollingComplete As DateTime

    Public Property SentryNum() As String
        Get
            Return sentryNumber
        End Get
        Set(ByVal Value As String)
            sentryNumber = Value
        End Set
    End Property
    Public Property SentryName() As String
        Get
            Return sentryType
        End Get
        Set(ByVal Value As String)
            sentryType = Value
        End Set
    End Property
    Public Property CommandType() As String
        Get
            Return cmdType
        End Get
        Set(ByVal Value As String)
            cmdType = Value
        End Set
    End Property
    Public Property QueuedStatus() As String
        Get
            Return queueStatus
        End Get
        Set(ByVal Value As String)
            queueStatus = Value
        End Set
    End Property
    Public Property TimeQueued() As DateTime
        Get
            Return timeQueue
        End Get
        Set(ByVal Value As DateTime)
            timeQueue = Value
        End Set
    End Property
    Public Property TimePollingStarted() As DateTime
        Get
            Return timePollingStart
        End Get
        Set(ByVal Value As DateTime)
            timePollingStart = Value
        End Set
    End Property
    Public Property TimePollingCompleted() As DateTime
        Get
            Return timePollingComplete
        End Get
        Set(ByVal Value As DateTime)
            timePollingComplete = Value
        End Set
    End Property
    Protected Sub btnok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            'Added By Varun Moota to Check SentryID Exits in DB. 05/28/2010
            If Not SentryIDExists(txtSentry.Text) Then
                If btnok.Text.ToLower() = "ok" Then
                    insertdata("0")
                ElseIf btnok.Text.ToLower() = "update" And (Not Request.QueryString.Get("RowID") = "") Then
                    insertdata("1")
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Sentry ID Already Exists !!');</script>")

            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("btnok_Click_InsertData", ex)
        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Txtstate.Attributes.Add("OnKeyPress", "KeyPress(event);")

        'Added By varun

        btnPollingLog.Attributes.Add("OnClick", "window.showModalDialog('PollingActions.aspx',window,'dialogHeight:650px;dialogWidth:950px;dialogHide:true;help:no;status:no;toolbar:no;location:no;titlebar:no;resizable:yes;scrollbars:yes;');return true;")
        'If Page.IsPostBack Then
        '    System.Threading.Thread.Sleep(5000)
        '    btnDiagnostic.Attributes.Add("OnClick", "window.showModalDialog('Diagnostic.aspx',window,'dialogHeight:650px;dialogWidth:950px;dialogHide:true;help:no;status:no;toolbar:no;location:no;titlebar:no;resizable:yes;scrollbars:yes;');return true;")
        'End If

        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            Else

                If Not Page.IsPostBack Then
                    If Request.QueryString.Count > 0 Then
                        If Not Request.QueryString.Get("RowID") Is Nothing And Not Request.QueryString.Get("RowID") = "" Then
                            BindPulser()
                            BindTank()
                            ' BindSentryTM()

                            ds = GetallData(Request.QueryString.Get("RowID"))
                            If Not ds Is Nothing Then
                                If ds.Tables(0).Rows.Count > 0 Then

                                    'Added By Pritam As per Jose Suggestions Date : 22-Jan-2015
                                    Dim SGProbe_Tanks As String
                                    SGProbe_Tanks = ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString()
                                    Dim SGProbeTanks As String() = SGProbe_Tanks.Split(New Char() {","c})
                                    Dim Tank1 As String = SGProbeTanks(0).ToString()

                                    'Added By Pritam
                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("SGTLD_Tanks") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString.Trim() = "") Then
                                            For i = 0 To ddlprobetank1.Items.Count - 1
                                                If (ddlprobetank1.Items(i).Value = SGProbeTanks(0).ToString()) Then
                                                    ddlprobetank1.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("SGTLD_Tanks") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString.Trim() = "") Then
                                            For i = 0 To ddlprobetank1.Items.Count - 1
                                                If (ddlprobetank2.Items(i).Value = SGProbeTanks(1).ToString()) Then
                                                    ddlprobetank2.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("SGTLD_Tanks") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString.Trim() = "") Then
                                            For i = 0 To ddlprobetank1.Items.Count - 1
                                                If (ddlprobetank3.Items(i).Value = SGProbeTanks(2).ToString()) Then
                                                    ddlprobetank3.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("SGTLD_Tanks") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString.Trim() = "") Then
                                            For i = 0 To ddlprobetank4.Items.Count - 1
                                                If (ddlprobetank4.Items(i).Value = SGProbeTanks(3).ToString()) Then
                                                    ddlprobetank4.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("SGTLD_Tanks") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString.Trim() = "") Then
                                            For i = 0 To ddlprobetank5.Items.Count - 1
                                                If (ddlprobetank5.Items(i).Value = SGProbeTanks(4).ToString()) Then
                                                    ddlprobetank5.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("SGTLD_Tanks") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString.Trim() = "") Then
                                            For i = 0 To ddlprobetank6.Items.Count - 1
                                                If (ddlprobetank6.Items(i).Value = SGProbeTanks(5).ToString()) Then
                                                    ddlprobetank6.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("SGTLD_Tanks") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString.Trim() = "") Then
                                            For i = 0 To ddlprobetank7.Items.Count - 1
                                                If (ddlprobetank7.Items(i).Value = SGProbeTanks(6).ToString()) Then
                                                    ddlprobetank7.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("SGTLD_Tanks") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString.Trim() = "") Then
                                            For i = 0 To ddlprobetank8.Items.Count - 1
                                                If (ddlprobetank8.Items(i).Value = SGProbeTanks(7).ToString()) Then
                                                    ddlprobetank8.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("SGTLD_Tanks") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("SGTLD_Tanks").ToString.Trim() = "") Then
                                            For i = 0 To ddlprobetank9.Items.Count - 1
                                                If (ddlprobetank9.Items(i).Value = SGProbeTanks(8).ToString()) Then
                                                    ddlprobetank9.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    btnok.Text = "Update"
                                    txtSentry.Text = ds.Tables(0).Rows(0)("NUMBER").ToString()
                                    'Added By Varun to set Session Values
                                    Session("SentryID") = txtSentry.Text

                                    TxtName.Text = ds.Tables(0).Rows(0)("NAME").ToString()
                                    Txtaddress.Text = ds.Tables(0).Rows(0)("ADDRESS").ToString()
                                    Txtstate.Text = ds.Tables(0).Rows(0)("STATE").ToString()
                                    'TxtCom.Text = ds.Tables(0).Rows(0)("PHONENO").ToString()


                                    If Not ds.Tables(0).Rows(0)("BAUD") Is DBNull.Value Then
                                        If ds.Tables(0).Rows(0)("BAUD").ToString() = "1200" Then
                                            rdoBuadRate.SelectedIndex = 0
                                        ElseIf ds.Tables(0).Rows(0)("BAUD").ToString() = "2400" Then
                                            rdoBuadRate.SelectedIndex = 1
                                        ElseIf ds.Tables(0).Rows(0)("BAUD").ToString() = "4800" Then
                                            rdoBuadRate.SelectedIndex = 2
                                        ElseIf ds.Tables(0).Rows(0)("BAUD").ToString() = "9600" Then
                                            rdoBuadRate.SelectedIndex = 3
                                        End If
                                    End If

                                    If Not ds.Tables(0).Rows(0)("IPLICENSE") Is DBNull.Value Then
                                        txtIP.Text = ds.Tables(0).Rows(0)("IPLICENSE")
                                    End If

                                    If Not ds.Tables(0).Rows(0)("CODE") Is DBNull.Value Then
                                        txtExpCode.Text = ds.Tables(0).Rows(0)("CODE").ToString()
                                    End If

                                    'Added By Varun Moota
                                    If Not ds.Tables(0).Rows(0)("BOARDTYPE") Is DBNull.Value Then
                                        ddlSentry.SelectedValue = ds.Tables(0).Rows(0)("BOARDTYPE").ToString()
                                    End If

                                    'Commeneted By Varun Moota, as Per John&Marc, there shouldn't be any COMM port.02/19/2010 

                                    Session("COMMValue") = ds.Tables(0).Rows(0)("COMMPORT").ToString()
                                    Session("PhoneValue") = ds.Tables(0).Rows(0)("PHONENO").ToString()


                                    If Session("COMMValue") = Nothing Then
                                        Session("COMMValue") = ""
                                    End If
                                    If Session("PhoneValue") = Nothing Then
                                        Session("PhoneValue") = ""
                                    End If
                                    If Not ds.Tables(0).Rows(0)("IPCOMM") Is DBNull.Value Then
                                        If ds.Tables(0).Rows(0)("IPCOMM") = True Then
                                            CHKCOM.Checked = True
                                            't1.Style.Add("display", "")
                                            Label4.Text = "IP Comm Port #:"
                                            TxtCom.Text = Session("COMMValue").ToString()
                                            HiddenCommVal.Value = Session("COMMValue").ToString()

                                        Else
                                            CHKCOM.Checked = False
                                            ''t1.Style.Add("display", "none")
                                            Label4.Text = "Phone # :"
                                            HiddenPhoneVal.Value = Session("PhoneValue").ToString()

                                            TxtCom.Text = Session("PhoneValue").ToString()


                                        End If
                                    End If

                                    If Not ds.Tables(0).Rows(0)("UseIPWebSocketConn") Is DBNull.Value Then
                                        If ds.Tables(0).Rows(0)("UseIPWebSocketConn") = True Then
                                            chkUseIPWebSocketConn.Checked = True
                                            'Response.Redirect("ExportFile.aspx?file=" & f.Name & "&PortTerminal=XYZ", False)
                                            Session("IPWebSocket") = "true"
                                        Else
                                            chkUseIPWebSocketConn.Checked = False
                                            Session("IPWebSocket") = "false"
                                        End If
                                    End If

                                    'Added By Varun Moota.02/19/2010
                                    'Label4.Text = "Phone # :"


                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP1_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP1_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank1.Items.Count - 1
                                                If (ddltank1.Items(i).Value = ds.Tables(0).Rows(0)("PUMP1_TANK")) Then
                                                    ddltank1.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP1_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP1_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser1.Items.Count - 1
                                                If (ddlPulser1.Items(i).Value = ds.Tables(0).Rows(0)("PUMP1_ADJ")) Then
                                                    ddlPulser1.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    ''Check For Tank having pulser Selected
                                    'If Not ds.Tables(0).Rows(0)("PUMP1_TANK") Is DBNull.Value And Not (ds.Tables(0).Rows(0)("PUMP1_TANK").ToString.Trim() = "") Then
                                    If Not ddltank1.Text = "Select Tank" Then
                                        'If (ds.Tables(0).Rows(0)("PUMP1_ADJ") Is DBNull.Value Or ds.Tables(0).Rows(0)("PUMP1_ADJ").ToString.Trim() = "") Then
                                        If ddlPulser1.Text = "Select Pulser" Then
                                            strMessage = "- Please Select pulser for tank #001 on hose #1. \n"
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP2_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP2_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank2.Items.Count - 1
                                                If (ddltank2.Items(i).Value = ds.Tables(0).Rows(0)("PUMP2_TANK")) Then
                                                    ddltank2.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP2_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP2_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser2.Items.Count - 1
                                                If (ddlPulser2.Items(i).Value = ds.Tables(0).Rows(0)("PUMP2_ADJ")) Then
                                                    ddlPulser2.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    ''Check For Tank having pulser Selected
                                    'If Not ds.Tables(0).Rows(0)("PUMP2_TANK") Is DBNull.Value And Not (ds.Tables(0).Rows(0)("PUMP2_TANK").ToString.Trim() = "") Then
                                    If Not ddltank2.Text = "Select Tank" Then
                                        'If (ds.Tables(0).Rows(0)("PUMP2_ADJ") Is DBNull.Value Or ds.Tables(0).Rows(0)("PUMP2_ADJ").ToString.Trim() = "") Then
                                        If ddlPulser2.Text = "Select Pulser" Then
                                            strMessage = strMessage & "- Please Select pulser for tank #001 on hose #2.\n"
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP3_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP3_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank3.Items.Count - 1
                                                If (ddltank3.Items(i).Value = ds.Tables(0).Rows(0)("PUMP3_TANK")) Then
                                                    ddltank3.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP3_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP3_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser3.Items.Count - 1
                                                If (ddlPulser3.Items(i).Value = ds.Tables(0).Rows(0)("PUMP3_ADJ")) Then
                                                    ddlPulser3.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    ''Check For Tank having pulser Selected
                                    'If Not ds.Tables(0).Rows(0)("PUMP3_TANK") Is DBNull.Value And Not (ds.Tables(0).Rows(0)("PUMP3_TANK").ToString.Trim() = "") Then
                                    If Not ddltank3.Text = "Select Tank" Then
                                        'If (ds.Tables(0).Rows(0)("PUMP3_ADJ") Is DBNull.Value Or ds.Tables(0).Rows(0)("PUMP3_ADJ").ToString.Trim() = "") Then
                                        If ddlPulser3.Text = "Select Pulser" Then
                                            strMessage = strMessage & "- Please Select pulser for tank #001 on hose #3.\n"
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP4_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP4_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank4.Items.Count - 1
                                                If (ddltank4.Items(i).Value = ds.Tables(0).Rows(0)("PUMP4_TANK")) Then
                                                    ddltank4.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                            'While Not ddltank4.SelectedValue = ds.Tables(0).Rows(0)("PUMP4_TANK")
                                            '    ddltank4.SelectedIndex = i
                                            '    i = i + 1
                                            'End While
                                        End If
                                    End If
                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP4_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP4_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser4.Items.Count - 1
                                                If (ddlPulser4.Items(i).Value = ds.Tables(0).Rows(0)("PUMP4_ADJ")) Then
                                                    ddlPulser4.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                            'While Not ddlPulser4.SelectedValue = ds.Tables(0).Rows(0)("PUMP4_ADJ")
                                            '    ddlPulser4.SelectedIndex = i
                                            '    i = i + 1
                                            'End While
                                        End If
                                    End If

                                    ''Check For Tank having pulser Selected
                                    'If Not (ds.Tables(0).Rows(0)("PUMP4_TANK") Is DBNull.Value) And Not (ds.Tables(0).Rows(0)("PUMP4_TANK").ToString.Trim() = "") Then
                                    If Not ddltank4.Text = "Select Tank" Then
                                        'If (ds.Tables(0).Rows(0)("PUMP4_ADJ") Is DBNull.Value Or ds.Tables(0).Rows(0)("PUMP4_ADJ").ToString.Trim() = "") Then
                                        If ddlPulser4.Text = "Select Pulser" Then
                                            strMessage = strMessage & "- Please Select pulser for tank #001 on hose #4.\n"
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP5_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP5_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank5.Items.Count - 1
                                                If (ddltank5.Items(i).Value = ds.Tables(0).Rows(0)("PUMP5_TANK")) Then
                                                    ddltank5.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                            'While Not ddltank5.SelectedValue = ds.Tables(0).Rows(0)("PUMP5_TANK")
                                            '    ddltank5.SelectedIndex = i
                                            '    i = i + 1
                                            'End While
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP5_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP5_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser5.Items.Count - 1
                                                If (ddlPulser5.Items(i).Value = ds.Tables(0).Rows(0)("PUMP5_ADJ")) Then
                                                    ddlPulser5.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    ''Check For Tank having pulser Selected
                                    'If Not ds.Tables(0).Rows(0)("PUMP5_TANK") Is DBNull.Value And Not (ds.Tables(0).Rows(0)("PUMP5_TANK").ToString.Trim() = "") Then
                                    If Not ddltank5.Text = "Select Tank" Then
                                        ' If (ds.Tables(0).Rows(0)("PUMP5_ADJ") Is DBNull.Value Or ds.Tables(0).Rows(0)("PUMP5_ADJ").ToString.Trim() = "") Then
                                        If ddlPulser5.Text = "Select Pulser" Then
                                            strMessage = strMessage & "- Please Select pulser for tank #001 on hose #5.\n"
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP6_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP6_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank6.Items.Count - 1
                                                If (ddltank6.Items(i).Value = ds.Tables(0).Rows(0)("PUMP6_TANK")) Then
                                                    ddltank6.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP6_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP6_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser6.Items.Count - 1
                                                If (ddlPulser6.Items(i).Value = ds.Tables(0).Rows(0)("PUMP6_ADJ")) Then
                                                    ddlPulser6.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    ''Check For Tank having pulser Selected
                                    'If Not ds.Tables(0).Rows(0)("PUMP6_TANK") Is DBNull.Value And Not (ds.Tables(0).Rows(0)("PUMP6_TANK").ToString.Trim() = "") Then
                                    If Not ddltank6.Text = "Select Tank" Then
                                        'If (ds.Tables(0).Rows(0)("PUMP6_ADJ") Is DBNull.Value Or ds.Tables(0).Rows(0)("PUMP6_ADJ").ToString.Trim() = "") Then
                                        If ddlPulser6.Text = "Select Pulser" Then
                                            strMessage = strMessage & "- Please Select pulser for tank #001 on hose #6.\n"
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP7_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP7_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank7.Items.Count - 1
                                                If (ddltank7.Items(i).Value = ds.Tables(0).Rows(0)("PUMP7_TANK")) Then
                                                    ddltank7.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP7_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP7_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser7.Items.Count - 1
                                                If (ddlPulser7.Items(i).Value = ds.Tables(0).Rows(0)("PUMP7_ADJ")) Then
                                                    ddlPulser7.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If
                                    ''Check For Tank having pulser Selected
                                    'If Not ds.Tables(0).Rows(0)("PUMP7_TANK") Is DBNull.Value And Not (ds.Tables(0).Rows(0)("PUMP7_TANK").ToString.Trim() = "") Then
                                    If Not ddltank7.Text = "Select Tank" Then
                                        'If (ds.Tables(0).Rows(0)("PUMP7_ADJ") Is DBNull.Value Or ds.Tables(0).Rows(0)("PUMP7_ADJ").ToString.Trim() = "") Then
                                        If ddlPulser7.Text = "Select Pulser" Then
                                            strMessage = strMessage & "- Please Select pulser for tank #001 on hose #7. \n"
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP8_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP8_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank8.Items.Count - 1
                                                If (ddltank8.Items(i).Value = ds.Tables(0).Rows(0)("PUMP8_TANK")) Then
                                                    ddltank8.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP8_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP8_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser8.Items.Count - 1
                                                If (ddlPulser8.Items(i).Value = ds.Tables(0).Rows(0)("PUMP8_ADJ")) Then
                                                    ddlPulser8.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP9_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP9_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank9.Items.Count - 1
                                                If (ddltank9.Items(i).Value = ds.Tables(0).Rows(0)("PUMP9_TANK")) Then
                                                    ddltank9.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If
                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP9_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP9_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser9.Items.Count - 1
                                                If (ddlPulser9.Items(i).Value = ds.Tables(0).Rows(0)("PUMP9_ADJ")) Then
                                                    ddlPulser9.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP10_TANK") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP10_TANK").ToString.Trim() = "") Then
                                            For i = 0 To ddltank10.Items.Count - 1
                                                If (ddltank10.Items(i).Value = ds.Tables(0).Rows(0)("PUMP10_TANK")) Then
                                                    ddltank10.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    i = 0
                                    If Not ds.Tables(0).Rows(0)("PUMP10_ADJ") Is DBNull.Value Then
                                        If Not (ds.Tables(0).Rows(0)("PUMP10_ADJ").ToString.Trim() = "") Then
                                            For i = 0 To ddlPulser10.Items.Count - 1
                                                If (ddlPulser10.Items(i).Value = ds.Tables(0).Rows(0)("PUMP10_ADJ")) Then
                                                    ddlPulser10.Items(i).Selected = True
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If

                                    ''Check For Tank having pulser Selected
                                    'If Not ds.Tables(0).Rows(0)("PUMP8_TANK") Is DBNull.Value And Not (ds.Tables(0).Rows(0)("PUMP8_TANK").ToString.Trim() = "") Then
                                    If Not ddltank8.Text = "Select Tank" Then
                                        'If (ds.Tables(0).Rows(0)("PUMP8_ADJ") Is DBNull.Value Or ds.Tables(0).Rows(0)("PUMP8_ADJ").ToString.Trim() = "") Then
                                        If ddlPulser8.Text = "Select Pulser" Then
                                            strMessage = strMessage & "- Please Select pulser for tank #001 on hose #8. \n"
                                        End If
                                    End If
                                    If Not strMessage = "" Then
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('" & strMessage & "')</script>")
                                    End If

                                    'Added BY Varun Moota, new SG Tank Monitor.03/21/2011
                                    'If Not ds.Tables(0).Rows(0)("TMNumber") Is DBNull.Value Then
                                    '    If Not (ds.Tables(0).Rows(0)("TMNumber").ToString.Trim() = "") Then
                                    '        ddlSentryTM.Text = ds.Tables(0).Rows(0)("TMNumber").ToString()
                                    '    End If
                                    'End If



                                    'Added By Varun to Bind Pump StartPoint for DFW.12/28/2010
                                    'IMPORTANT:Make enable all the controls for Totalizer start points.09/09/2011
                                    '''TankTotStartReadings(txtSentry.Text) 

                                End If
                            End If
                        End If
                    Else
                        txtSentry.Text = GetMaxRecord()
                        BindPulser()
                        BindTank()
                        Label10.Text = "New Sentry Screen"
                        ' ''If CHKCOM.Checked = False Then
                        ' ''    t1.Style.Add("display", "none")
                        ' ''End If
                        HiddenCommVal.Value = ""
                        HiddenPhoneVal.Value = ""
                    End If

                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_New_Edit_PageLoad", ex)
        End Try
    End Sub

    Public Sub BindTank()
        Try
            ds = dal.GetDataSet("use_tt_GetTankDetails")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    For i = 1 To 10 'Changed to 10 hoses as Per stuart.06/20/2012
                        DropTemp = CType(Page.FindControl("ddltank" & i), DropDownList)
                        DropTemp.DataSource = ds.Tables(0)
                        DropTemp.DataTextField = "Name"
                        DropTemp.DataValueField = "Number"
                        DropTemp.DataBind()
                        DropTemp.Items.Insert(0, "Select Tank")

                        'Added By Varun Moota
                        DropTemp.Items(0).Attributes.Add("style", "color:Blue")
                    Next
                End If
            End If

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    For i = 1 To 9 'Changed to 10 hoses as Per stuart.06/20/2012
                        DropTemp = CType(Page.FindControl("ddlprobetank" & i), DropDownList)
                        DropTemp.DataSource = ds.Tables(0)
                        DropTemp.DataTextField = "Name"
                        DropTemp.DataValueField = "Number"
                        DropTemp.DataBind()
                        DropTemp.Items.Insert(0, "Select Tank")

                        'Added By Varun Moota
                        DropTemp.Items(0).Attributes.Add("style", "color:Blue")
                    Next
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_new_Edit_BindTank", ex)
        End Try
    End Sub

    Public Sub BindPulser()
        Try
            Dim TempDT As New DataTable
            For i = 1 To 10 ' Changed to 10 hoses as Per stuart.06/20/2012
                DropTemp = CType(Page.FindControl("ddlPulser" & i), DropDownList)
                If i = 1 Then
                    TempDT = CreateDT()
                End If
                DropTemp.DataSource = TempDT
                DropTemp.DataTextField = "Name"
                DropTemp.DataValueField = "Number"
                DropTemp.DataBind()
                DropTemp.Items.Insert(0, "Select Pulser")

                'Added By Varun Moota
                DropTemp.Items(0).Attributes.Add("style", "color:Blue")


            Next
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_new_Edit_BindPulser", ex)
        End Try

    End Sub

    Public Function CreateDT() As DataTable
        Try


            Dim Drow As DataRow
            Dim dt As New DataTable()
            Dim dc1, dc2 As DataColumn
            dc1 = New DataColumn("Number", Type.GetType("System.String"))
            dc2 = New DataColumn("Name", Type.GetType("System.String"))

            dt.Columns.Add(dc1)
            dt.Columns.Add(dc2)

            Drow = dt.NewRow()
            Drow("Number") = "01"
            Drow("Name") = "1/10 GALLON 01"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "02"
            Drow("Name") = "2/10 GALLON 02"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "03"
            Drow("Name") = "5/10 GALLON 03"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "04"
            Drow("Name") = "1 GALLON    04"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "05"
            Drow("Name") = "1/10 QUART  05"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "06"
            Drow("Name") = "2/10 QUART  06"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "07"
            Drow("Name") = "5/10 QUART  07"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "08"
            Drow("Name") = "1 QUART     08"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "09"
            Drow("Name") = "1/10 LITER  09"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "10"
            Drow("Name") = "2/10 LITER  10"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "11"
            Drow("Name") = "5/10 LITER  11"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "12"
            Drow("Name") = "1 LITER     12"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "13"
            Drow("Name") = "10 QT LIMIT 13"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "14"
            Drow("Name") = "1 QT LIMIT  14"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "15"
            Drow("Name") = "CAN OIL     15"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "16"
            Drow("Name") = "1/20 GALLON 16"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "17"
            Drow("Name") = "BEN         17"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "18"
            Drow("Name") = "1/100 GAL.  18"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "19"
            Drow("Name") = "1/95 QT.   19"
            dt.Rows.Add(Drow)

            Drow = dt.NewRow()
            Drow("Number") = "21"
            Drow("Name") = "1/380 QT.   21"
            dt.Rows.Add(Drow)
            CreateDT = dt
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_new_Edit_CreateDT", ex)
            Return Nothing
        End Try
    End Function

    Public Function GetallData(ByVal RowId As Integer) As DataSet
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim ParNUMBER = New SqlParameter("@ID", SqlDbType.Int)
            ParNUMBER.Direction = ParameterDirection.Input
            ParNUMBER.value = RowId
            parcollection(0) = ParNUMBER
            GetallData = dal.ExecuteStoredProcedureGetDataSet("Use_tt_GetSentryAllDetails", parcollection)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_new_Edit_GetallData", ex)
            GetallData = Nothing
        End Try
    End Function
    Private Function GetMaxRecord() As String
        Dim strMax As String
        Try
            Dim dal As New GeneralizedDAL()
            strMax = dal.ExecuteScalarGetString("Use_TT_GetMaxSentry")
            If strMax = "" Then
                strMax = "001"
            Else
                strMax = strMax + 1
                strMax = String.Format("{0:000}", Convert.ToInt32(strMax))
            End If
            GetMaxRecord = strMax
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_new_Edit_BindControl", ex)
            GetMaxRecord = ""
        End Try
    End Function

    Private Sub insertdata(ByVal val As String)
        Try

            Dim dal = New GeneralizedDAL()
            Dim parcollection(36) As SqlParameter

            Dim ParNUMBER = New SqlParameter("@NUMBER", SqlDbType.NVarChar, 3)
            Dim ParNAME = New SqlParameter("@NAME", SqlDbType.NVarChar, 25)
            Dim ParADDRESS = New SqlParameter("@ADDRESS", SqlDbType.NVarChar, 25)
            Dim ParSTATE = New SqlParameter("@STATE", SqlDbType.NVarChar, 2)
            Dim ParPHONENO = New SqlParameter("@PHONENO", SqlDbType.NVarChar, 25)
            Dim ParBAUD = New SqlParameter("@BAUD", SqlDbType.NVarChar, 6)
            Dim ParCODE = New SqlParameter("@CODE", SqlDbType.NVarChar, 7)
            Dim ParBOARDTYPE = New SqlParameter("@BOARDTYPE", SqlDbType.NVarChar, 25)
            Dim ParBOARDVer = New SqlParameter("@BOARDVer", SqlDbType.NVarChar, 25) 'Need to Update
            Dim ParPUMP1_TANK = New SqlParameter("@PUMP1_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP1_ADJ = New SqlParameter("@PUMP1_ADJ", SqlDbType.NVarChar, 2)
            Dim ParPUMP2_TANK = New SqlParameter("@PUMP2_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP2_ADJ = New SqlParameter("@PUMP2_ADJ", SqlDbType.NVarChar, 2)
            Dim ParPUMP3_TANK = New SqlParameter("@PUMP3_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP3_ADJ = New SqlParameter("@PUMP3_ADJ", SqlDbType.NVarChar, 2)
            Dim ParPUMP4_TANK = New SqlParameter("@PUMP4_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP4_ADJ = New SqlParameter("@PUMP4_ADJ", SqlDbType.NVarChar, 2)
            Dim ParPUMP5_TANK = New SqlParameter("@PUMP5_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP5_ADJ = New SqlParameter("@PUMP5_ADJ", SqlDbType.NVarChar, 2)
            Dim ParPUMP6_TANK = New SqlParameter("@PUMP6_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP6_ADJ = New SqlParameter("@PUMP6_ADJ", SqlDbType.NVarChar, 2)
            Dim ParPUMP7_TANK = New SqlParameter("@PUMP7_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP7_ADJ = New SqlParameter("@PUMP7_ADJ", SqlDbType.NVarChar, 2)
            Dim ParPUMP8_TANK = New SqlParameter("@PUMP8_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP8_ADJ = New SqlParameter("@PUMP8_ADJ", SqlDbType.NVarChar, 2)
            Dim ParIPCOMM = New SqlParameter("@IPCOMM", SqlDbType.Bit)
            'Edit by Omar, increased parameter size from 10 to 15 to allow storage of an entire IP address
            'NOTE: column size increased in database as well (table column as well as stored procedure definition) 
            Dim ParIPLICENSE = New SqlParameter("@IPLICENSE", SqlDbType.NVarChar, 15)
            Dim ParID = New SqlParameter("@ID", SqlDbType.Int)
            Dim ParFlag = New SqlParameter("@Flag", SqlDbType.NVarChar, 1)
            'Added By Varun Moota. 02/24/2010
            Dim ParCOMM = New SqlParameter("@COMMPORT", SqlDbType.NVarChar, 25)

            'Added By Varun Moota.03/21/2011
            Dim ParLOCALPORT = New SqlParameter("@LOCALPORT", SqlDbType.NVarChar, 25)
            'Added By Pritam Shinde. As per Jose suggestions 22-Jan-2015
            Dim ParSGTLD_Tanks = New SqlParameter("@SGTLD_Tanks", SqlDbType.NVarChar, 50)

            'Added two more hoses.
            Dim ParPUMP9_TANK = New SqlParameter("@PUMP9_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP9_ADJ = New SqlParameter("@PUMP9_ADJ", SqlDbType.NVarChar, 2)
            Dim ParPUMP10_TANK = New SqlParameter("@PUMP10_TANK", SqlDbType.NVarChar, 3)
            Dim ParPUMP10_ADJ = New SqlParameter("@PUMP10_ADJ", SqlDbType.NVarChar, 2)
            Dim ParUseIPWebSocketConn = New SqlParameter("@UseIPWebSocketConn", SqlDbType.Bit)

            ParNUMBER.Direction = ParameterDirection.Input
            ParNAME.Direction = ParameterDirection.Input
            ParADDRESS.Direction = ParameterDirection.Input
            ParSTATE.Direction = ParameterDirection.Input
            ParPHONENO.Direction = ParameterDirection.Input
            ParBAUD.Direction = ParameterDirection.Input
            ParCODE.Direction = ParameterDirection.Input
            ParBOARDTYPE.Direction = ParameterDirection.Input
            ParBOARDVer.Direction = ParameterDirection.Input
            ParPUMP1_TANK.Direction = ParameterDirection.Input
            ParPUMP1_ADJ.Direction = ParameterDirection.Input
            ParPUMP2_TANK.Direction = ParameterDirection.Input
            ParPUMP2_ADJ.Direction = ParameterDirection.Input
            ParPUMP3_TANK.Direction = ParameterDirection.Input
            ParPUMP3_ADJ.Direction = ParameterDirection.Input
            ParPUMP4_TANK.Direction = ParameterDirection.Input
            ParPUMP4_ADJ.Direction = ParameterDirection.Input
            ParPUMP5_TANK.Direction = ParameterDirection.Input
            ParPUMP5_ADJ.Direction = ParameterDirection.Input
            ParPUMP6_TANK.Direction = ParameterDirection.Input
            ParPUMP6_ADJ.Direction = ParameterDirection.Input
            ParPUMP7_TANK.Direction = ParameterDirection.Input
            ParPUMP7_ADJ.Direction = ParameterDirection.Input
            ParPUMP8_TANK.Direction = ParameterDirection.Input
            ParPUMP8_ADJ.Direction = ParameterDirection.Input
            ParIPCOMM.Direction = ParameterDirection.Input
            ParIPLICENSE.Direction = ParameterDirection.Input
            ParID.Direction = ParameterDirection.Input
            ParFlag.Direction = ParameterDirection.Input
            ParCOMM.Direction = ParameterDirection.Input
            ParLOCALPORT.Direction = ParameterDirection.Input
            ParSGTLD_Tanks.Direction = ParameterDirection.Input
            ParUseIPWebSocketConn.Direction = ParameterDirection.Input

            ParNUMBER.value = txtSentry.Text
            ParNAME.value = TxtName.Text
            ParADDRESS.value = Txtaddress.Text
            ParSTATE.value = Txtstate.Text

            'Added By Varun Moota to Fix Comm Port Issue.05/27/2010
            If Session("COMMValue") = Nothing Then
                Session("COMMValue") = ""
            End If
            If Session("PhoneValue") = Nothing Then
                Session("PhoneValue") = ""
            End If

            If chkUseIPWebSocketConn.Checked = True Then
                ParUseIPWebSocketConn.value = True
            Else
                ParUseIPWebSocketConn.value = False
            End If

            If CHKCOM.Checked Then
                ParCOMM.value = TxtCom.Text
                ParPHONENO.value = DBNull.Value
                ParLOCALPORT.value = DBNull.Value
            Else
                If TxtCom.Text = "" Then
                    ParLOCALPORT.value = GetCommValue()
                    ParCOMM.value = DBNull.Value
                    ParPHONENO.value = DBNull.Value
                Else
                    ParPHONENO.value = TxtCom.Text
                    ParCOMM.value = DBNull.Value
                    ParLOCALPORT.value = DBNull.Value
                End If
            End If


            ParBAUD.value = RdoBuadRate.SelectedItem.Value
            ParCODE.value = txtExpCode.Text
            'ParBOARDTYPE.value = Trim(ddlSentry.SelectedItem.ToString())

            'Get Board version.
            If (ddlSentry.SelectedItem.ToString().Contains("Gold")) Then
                ParBOARDVer.value = "7"
                ParBOARDTYPE.value = "SentryGold"
            ElseIf (ddlSentry.SelectedItem.ToString().Contains("Radio")) Then
                ParBOARDVer.value = "R"
                ParBOARDTYPE.value = "ArmRadio"
            Else
                ParBOARDVer.value = "6"
                ParBOARDTYPE.value = "Sentry6"
            End If





            If ddltank1.SelectedIndex > 0 Then
                ParPUMP1_TANK.value = ddltank1.SelectedValue
            Else
                ParPUMP1_TANK.value = DBNull.Value
            End If
            If ddlPulser1.SelectedIndex > 0 Then
                ParPUMP1_ADJ.value = ddlPulser1.SelectedValue
            Else
                ParPUMP1_ADJ.value = DBNull.Value
            End If

            If ddltank2.SelectedIndex > 0 Then
                ParPUMP2_TANK.value = ddltank2.SelectedValue
            Else
                ParPUMP2_TANK.value = DBNull.Value
            End If

            If ddlPulser2.SelectedIndex > 0 Then
                ParPUMP2_ADJ.value = ddlPulser2.SelectedValue
            Else
                ParPUMP2_ADJ.value = DBNull.Value
            End If

            If ddltank3.SelectedIndex > 0 Then
                ParPUMP3_TANK.value = ddltank3.SelectedValue
            Else
                ParPUMP3_TANK.value = DBNull.Value
            End If

            If ddlPulser3.SelectedIndex > 0 Then
                ParPUMP3_ADJ.value = ddlPulser3.SelectedValue
            Else
                ParPUMP3_ADJ.value = DBNull.Value
            End If

            If ddltank4.SelectedIndex > 0 Then
                ParPUMP4_TANK.value = ddltank4.SelectedValue
            Else
                ParPUMP4_TANK.value = DBNull.Value
            End If

            If ddlPulser4.SelectedIndex > 0 Then
                ParPUMP4_ADJ.value = ddlPulser4.SelectedValue
            Else
                ParPUMP4_ADJ.value = DBNull.Value
            End If

            If ddltank5.SelectedIndex > 0 Then
                ParPUMP5_TANK.value = ddltank5.SelectedValue
            Else
                ParPUMP5_TANK.value = DBNull.Value
            End If

            If ddlPulser5.SelectedIndex > 0 Then
                ParPUMP5_ADJ.value = ddlPulser5.SelectedValue
            Else
                ParPUMP5_ADJ.value = DBNull.Value
            End If

            If ddltank6.SelectedIndex > 0 Then
                ParPUMP6_TANK.value = ddltank6.SelectedValue
            Else
                ParPUMP6_TANK.value = DBNull.Value
            End If

            If ddlPulser6.SelectedIndex > 0 Then
                ParPUMP6_ADJ.value = ddlPulser6.SelectedValue
            Else
                ParPUMP6_ADJ.value = DBNull.Value
            End If
            If ddltank7.SelectedIndex > 0 Then
                ParPUMP7_TANK.value = ddltank7.SelectedValue
            Else
                ParPUMP7_TANK.value = DBNull.Value
            End If

            If ddlPulser7.SelectedIndex > 0 Then
                ParPUMP7_ADJ.value = ddlPulser7.SelectedValue
            Else
                ParPUMP7_ADJ.value = DBNull.Value
            End If

            If ddltank8.SelectedIndex > 0 Then
                ParPUMP8_TANK.value = ddltank8.SelectedValue
            Else
                ParPUMP8_TANK.value = DBNull.Value
            End If

            If ddlPulser8.SelectedIndex > 0 Then
                ParPUMP8_ADJ.value = ddlPulser8.SelectedValue
            Else
                ParPUMP8_ADJ.value = DBNull.Value
            End If

            'New hoses
            If ddltank9.SelectedIndex > 0 Then
                ParPUMP9_TANK.value = ddltank9.SelectedValue
            Else
                ParPUMP9_TANK.value = DBNull.Value
            End If

            If ddlPulser9.SelectedIndex > 0 Then
                ParPUMP9_ADJ.value = ddlPulser9.SelectedValue
            Else
                ParPUMP9_ADJ.value = DBNull.Value
            End If

            If ddltank10.SelectedIndex > 0 Then
                ParPUMP10_TANK.value = ddltank10.SelectedValue
            Else
                ParPUMP10_TANK.value = DBNull.Value
            End If

            If ddlPulser10.SelectedIndex > 0 Then
                ParPUMP10_ADJ.value = ddlPulser10.SelectedValue
            Else
                ParPUMP10_ADJ.value = DBNull.Value
            End If

            ''Added SG TM Value.
            'If ddlSentryTM.SelectedIndex > 0 Then
            '    ParTMNumber.value = ddlSentryTM.SelectedValue
            'Else
            '    ParTMNumber.value = DBNull.Value
            'End If

            'Added By Pritam as per Jose suggestions 22-Jan-2015           

            Dim probeTank As String
            If ddlprobetank1.SelectedIndex > 0 Then
                probeTank = ddlprobetank1.SelectedValue & ","
            Else
                probeTank = ","
            End If

            If ddlprobetank2.SelectedIndex > 0 Then
                probeTank = probeTank & ddlprobetank2.SelectedValue & ","
            Else
                probeTank = probeTank & ","
            End If

            If ddlprobetank3.SelectedIndex > 0 Then
                probeTank = probeTank & ddlprobetank3.SelectedValue & ","
            Else
                probeTank = probeTank & ","
            End If

            If ddlprobetank4.SelectedIndex > 0 Then
                probeTank = probeTank & ddlprobetank4.SelectedValue & ","
            Else
                probeTank = probeTank & ","
            End If

            If ddlprobetank5.SelectedIndex > 0 Then
                probeTank = probeTank & ddlprobetank5.SelectedValue & ","
            Else
                probeTank = probeTank & ","
            End If

            If ddlprobetank6.SelectedIndex > 0 Then
                probeTank = probeTank & ddlprobetank6.SelectedValue & ","
            Else
                probeTank = probeTank & ","
            End If
            If ddlprobetank7.SelectedIndex > 0 Then
                probeTank = probeTank & ddlprobetank7.SelectedValue & ","
            Else
                probeTank = probeTank & ","
            End If
            If ddlprobetank8.SelectedIndex > 0 Then
                probeTank = probeTank & ddlprobetank8.SelectedValue & ","
            Else
                probeTank = probeTank & ","
            End If
            If ddlprobetank9.SelectedIndex > 0 Then
                probeTank = probeTank & ddlprobetank9.SelectedValue
            Else
                probeTank = probeTank & ","
            End If
            ParSGTLD_Tanks.value = probeTank
            If CHKCOM.Checked = True Then
                ParIPCOMM.value = True
            Else
                ParIPCOMM.value = False
            End If
            If Request.QueryString.Count > 0 Then
                If Not Request.QueryString.Get("RowID").ToString = "" Then
                    ParID.value = Request.QueryString.Get("RowID")
                End If
            Else
                ParID.value = 0
            End If
            ParFlag.value = val
            ParIPLICENSE.value = txtIP.Text
            parcollection(0) = ParNUMBER
            parcollection(1) = ParNAME
            parcollection(2) = ParADDRESS
            parcollection(3) = ParSTATE
            parcollection(4) = ParPHONENO
            parcollection(5) = ParBAUD
            parcollection(6) = ParCODE
            parcollection(7) = ParBOARDTYPE
            parcollection(8) = ParPUMP1_TANK
            parcollection(9) = ParPUMP1_ADJ
            parcollection(10) = ParPUMP2_TANK
            parcollection(11) = ParPUMP2_ADJ
            parcollection(12) = ParPUMP3_TANK
            parcollection(13) = ParPUMP3_ADJ
            parcollection(14) = ParPUMP4_TANK
            parcollection(15) = ParPUMP4_ADJ
            parcollection(16) = ParPUMP5_TANK
            parcollection(17) = ParPUMP5_ADJ
            parcollection(18) = ParPUMP6_TANK
            parcollection(19) = ParPUMP6_ADJ
            parcollection(20) = ParPUMP7_TANK
            parcollection(21) = ParPUMP7_ADJ
            parcollection(22) = ParPUMP8_TANK
            parcollection(23) = ParPUMP8_ADJ
            parcollection(24) = ParIPCOMM
            parcollection(25) = ParIPLICENSE
            parcollection(26) = ParID
            parcollection(27) = ParFlag
            parcollection(28) = ParCOMM
            parcollection(29) = ParLOCALPORT
            parcollection(30) = ParSGTLD_Tanks
            parcollection(31) = ParBOARDVer
            parcollection(32) = ParPUMP9_TANK
            parcollection(33) = ParPUMP9_ADJ
            parcollection(34) = ParPUMP10_TANK
            parcollection(35) = ParPUMP10_ADJ
            parcollection(36) = ParUseIPWebSocketConn
            dal.ExecuteSQLStoredProcedureGetBoolean("Use_tt_InsertSentryData", parcollection)
            'Added By Varun Moota, to Have Sentry TM# for TLD Polling. 01/25/2011
            'UpdateSentryTM(ddltank1.SelectedValue)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record has been saved successfully !!');</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_new_Edit_InsertData", ex)
        End Try
    End Sub
    Private Function SentryIDExists(ByVal SentryID As String) As Boolean
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParIdentity = New SqlParameter("@Identity", SqlDbType.VarChar, 3)
            ParIdentity.Direction = ParameterDirection.Input
            ParIdentity.Value = SentryID
            parcollection(0) = ParIdentity

            Dim strSentryIDCheck As String = Nothing
            If Not Session("SentryID") = Nothing Then
                strSentryIDCheck = Session("SentryID").ToString.Trim()
            End If
            If txtSentry.Text.Trim = strSentryIDCheck Then
                Return False
            Else
                SentryIDExists = dal.ExecuteStoredProcedureGetBoolean("usp_tt_SentryExists", parcollection)
            End If

            Return SentryIDExists
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Sentry_New_Edit.SentryIDExists", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Function
    Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click

        Try
            Response.Redirect("Sentry.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_New_Edit.btncancel_Click", ex)
        End Try
    End Sub

    'Added By Varun to Test Poll        
    Protected Sub btnpoll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpoll.Click
        Try
            SendPollData()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_New_Edit.btnpoll_Click", ex)
        End Try

    End Sub
    'Added BY Varun to Send Poll Data
    Private Sub SendPollData()

        SentryNum = txtSentry.Text()
        SentryName = ddlSentry.SelectedValue.ToString() '''IIf(ddlSentry.SelectedIndex = 2, "SentryGold", "Sentry6")
        CommandType = "POLL"
        QueuedStatus = "Queued"
        TimeQueued = DateTime.Now()
        TimePollingStarted = DateTime.Now()
        TimePollingCompleted = DateTime.Now()

        Try
            Dim objSql As New GeneralizedDAL()


            Dim sqlStr As String = "Insert Into PollingQueue([ID],[DeviceId],[DeviceType],[Command]"
            sqlStr = sqlStr + ",[Status],[TimeQueued],[TimePollingStarted],[TimePollingCompleted])"
            sqlStr = sqlStr + "Values(@ID,@DeviceId,@DeviceType,@Command,@Status,@TimeQueued,@TimePollingStarted,@TimePollingCompleted)"

            Dim param(7) As SqlParameter

            Dim logKey As Guid = System.Guid.NewGuid()
            param(0) = New SqlParameter("@ID", SqlDbType.UniqueIdentifier)
            param(0).Value = logKey

            param(1) = New SqlParameter("@DeviceId", SqlDbType.NVarChar)
            param(1).Value = SentryNum

            param(2) = New SqlParameter("@DeviceType", SqlDbType.NVarChar)
            param(2).Value = SentryName

            param(3) = New SqlParameter("@Command", SqlDbType.NVarChar)
            param(3).Value = CommandType

            param(4) = New SqlParameter("@Status", SqlDbType.NVarChar)
            param(4).Value = QueuedStatus

            param(5) = New SqlParameter("@TimeQueued", SqlDbType.DateTime)
            param(5).Value = DateTime.Now()

            param(6) = New SqlParameter("@TimePollingStarted", SqlDbType.DateTime)
            param(6).Value = DBNull.Value

            param(7) = New SqlParameter("@TimePollingCompleted", SqlDbType.DateTime)
            param(7).Value = DBNull.Value



            ' ''Dim sqlConn As New SqlConnection("server=192.168.0.12;uid=sa;pwd=29trak01;database=FuelTrak_Beta")
            ' ''sqlConn.Open()
            Dim sqlConn As SqlConnection = objSql.GetsqlConn()
            sqlConn.Open()


            Dim sqlcmd As SqlCommand = sqlConn.CreateCommand()
            sqlcmd.CommandType = Data.CommandType.Text
            sqlcmd.CommandText = sqlStr
            sqlcmd.Parameters.AddRange(param)

            Dim results As Integer = sqlcmd.ExecuteNonQuery()

            If results > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Sentry Poll Being Queued successfully !!');</script>")

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Sentry Polling Queued Failed !!');</script>")

            End If

            'Commented By varun Moota, to just have PollingLog(AJAX).07/27/2010

            'If results > 0 Then
            '    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Polled Being Queued successfully !!');location.href='Sentry.aspx';</script>")

            '    'btnpoll.Enabled = False
            '    'Edit by Omar -- ajax polling
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "poll_logs", "<script>$(function() { beginPolling('" & logKey.ToString() & "', 'polling'); });</script>")
            'Else
            '    Dim CSM As ClientScriptManager = Page.ClientScript

            '    CSM.RegisterClientScriptBlock((Me.GetType), "javascript", "alert('Error!');")
            'End If



        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("Sentry_New_Edit_SendPollData", ex)
        End Try

    End Sub

    Protected Sub btnComtst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComtst.Click
        Try
            SendCommTestData()
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("Sentry_New_Edit.btnComtst_Click", ex)
        End Try

    End Sub
    'Added By Varun Moota To Get Comm Value
    Public Function GetCommValue() As String
        Try
            Dim ds As New DataSet
            Dim dal = New GeneralizedDAL()
            Dim mcName As String
            Dim Commvalue As String = Nothing
            mcName = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName 'Request.UserHostName.ToString()
            Session("MachineName") = mcName

            ds = dal.GetDataSet("Select COMMPORT FROM COMM WHERE MachineName = '" + mcName + "'")

            If ds.Tables(0).Rows.Count > 0 Then
                Session("COMM") = ds.Tables(0).Rows(0)("COMMPORT").ToString()

                Commvalue = Session("COMM").ToString()

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No COMM Port Value Exists.');location.href='StatusInfo.aspx';</script>")

            End If

            Return CommValue
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_New_Edit.GetCommValue()", ex)
            Return ""
        End Try

    End Function
    Private Sub SendCommTestData()




        SentryNum = txtSentry.Text()
        SentryName = ddlSentry.SelectedValue.ToString() '''IIf(ddlSentry.SelectedIndex = 2, "SentryGold", "Sentry6")
        CommandType = "TEST"
        QueuedStatus = "Queued"
        TimeQueued = DateTime.Now()
        TimePollingStarted = DateTime.Now()
        TimePollingCompleted = DateTime.Now()

        Try
            Dim objSql As New GeneralizedDAL()

            Dim sqlStr As String = "Insert Into PollingQueue([ID],[DeviceId],[DeviceType],[Command]"
            sqlStr = sqlStr + ",[Status],[TimeQueued],[TimePollingStarted],[TimePollingCompleted])"
            sqlStr = sqlStr + "Values(@ID,@DeviceId,@DeviceType,@Command,@Status,@TimeQueued,@TimePollingStarted,@TimePollingCompleted)"

            Dim param(7) As SqlParameter

            Dim logKey As Guid = System.Guid.NewGuid()
            param(0) = New SqlParameter("@ID", SqlDbType.UniqueIdentifier)
            param(0).Value = logKey

            param(1) = New SqlParameter("@DeviceId", SqlDbType.NVarChar)
            param(1).Value = SentryNum

            param(2) = New SqlParameter("@DeviceType", SqlDbType.NVarChar)
            param(2).Value = SentryName

            param(3) = New SqlParameter("@Command", SqlDbType.NVarChar)
            param(3).Value = CommandType

            param(4) = New SqlParameter("@Status", SqlDbType.NVarChar)
            param(4).Value = QueuedStatus

            param(5) = New SqlParameter("@TimeQueued", SqlDbType.DateTime)
            param(5).Value = DateTime.Now()

            param(6) = New SqlParameter("@TimePollingStarted", SqlDbType.DateTime)
            param(6).Value = DBNull.Value

            param(7) = New SqlParameter("@TimePollingCompleted", SqlDbType.DateTime)
            param(7).Value = DBNull.Value




            Dim sqlConn As SqlConnection = objSql.GetsqlConn()
            sqlConn.Open()



            Dim sqlcmd As SqlCommand = sqlConn.CreateCommand()
            sqlcmd.CommandType = Data.CommandType.Text
            sqlcmd.CommandText = sqlStr
            sqlcmd.Parameters.AddRange(param)

            Dim results As Integer = sqlcmd.ExecuteNonQuery()

            If results > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('CommTest Being Queued successfully !!');</script>")

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('CommTest Queued Failed !!');</script>")

            End If

            'Commented By varun Moota, to just have PollingLog(AJAX).07/27/2010
            'If results > 0 Then
            '    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('CommTest Being Queued successfully !!');location.href='Sentry.aspx';</script>")

            '    'btnpoll.Enabled = False
            '    'Edit by Omar -- ajax polling
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "poll_logs", "<script>$(function() { beginPolling('" & logKey.ToString() & "', 'commtest'); });</script>")
            'Else
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('CommTest Queued Failed !!');</script>")

            'End If




        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("Sentry_New_Edit.SendCommTestData", ex)
        End Try
    End Sub



    'Private Sub TankTotStartReadings(ByVal TankNum As String)
    '    Try


    '        Dim dal = New GeneralizedDAL()

    '        Dim qry As String = "SELECT * FROM PUMPTOT WHERE TANK_NBR = '" + TankNum + "'"
    '        Dim dsTankTotRecords As New DataSet
    '        dsTankTotRecords = dal.GetDataSet(qry)
    '        If dsTankTotRecords.Tables(0).Rows.Count > 0 Then
    '            txtPump1StartTotRead.Text = dsTankTotRecords.Tables(0).Rows(0)("Pump_Totalizer1")
    '            txtPump1StartTotDT.Text = IIf((dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_ModifiedDT").ToString = ""), dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_CreatedDT").ToString(), dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_ModifiedDT").ToString())
    '        End If

    '        'Check Pump#2 Exits
    '        qry = "SELECT * FROM SENTRY WHERE Number = '" + TankNum.ToString() + "' AND PUMP2_TANK != 'NULL'"
    '        Dim dsPumpAvail As New DataSet
    '        dsPumpAvail = dal.GetDataSet(qry)
    '        If dsPumpAvail.Tables(0).Rows.Count > 0 Then
    '            btnUpdateTot2.Visible = True
    '            txtPump2StartTotRead.Text = dsTankTotRecords.Tables(0).Rows(0)("Pump_totalizer2")
    '            txtPump2StartTotDT.Text = IIf((dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_ModifiedDT").ToString = ""), dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_CreatedDT").ToString(), dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_ModifiedDT").ToString())
    '        Else
    '            txtPump2StartTotRead.Visible = False
    '            txtPump2StartTotDT.Visible = False
    '            btnUpdateTot2.Visible = False


    '        End If




    '    Catch ex As Exception
    '        Dim exLog As New ErrorPage
    '        exLog.errorlog("Sentry_New_Edit.TanTotReadings", ex)
    '    End Try
    'End Sub


    'Protected Sub btnUpdateTot1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateTot1.Click
    '    Try


    '        Dim dal As New GeneralizedDAL()

    '        Dim parcollection(11) As SqlParameter
    '        Dim ParTankNo = New SqlParameter("@TankNbr", SqlDbType.NVarChar, 3)
    '        Dim ParPump1 = New SqlParameter("@PumpTot1", SqlDbType.Decimal)
    '        Dim ParPump2 = New SqlParameter("@PumpTot2", SqlDbType.Decimal)
    '        Dim ParTot1CreatedDT = New SqlParameter("@PumpTot1CreatedDT", SqlDbType.NVarChar, 10)
    '        Dim ParTot1ModifiedDT = New SqlParameter("@PumpTot1ModifiedDT", SqlDbType.NVarChar, 10)
    '        Dim ParTot1CreatedBy = New SqlParameter("@PumpTot1CreatedBy", SqlDbType.NVarChar, 10)
    '        Dim ParTot1ModifiedBy = New SqlParameter("@PumpTot1ModifiedBy", SqlDbType.NVarChar, 10)
    '        Dim ParTot2CreatedDT = New SqlParameter("@PumpTot2CreatedDT", SqlDbType.NVarChar, 10)
    '        Dim ParTot2ModifiedDT = New SqlParameter("@PumpTot2ModifiedDT", SqlDbType.NVarChar, 10)
    '        Dim ParTot2CreatedBy = New SqlParameter("@PumpTot2CreatedBy", SqlDbType.NVarChar, 10)
    '        Dim ParTot2ModifiedBy = New SqlParameter("@PumpTot2ModifiedBy", SqlDbType.NVarChar, 10)
    '        Dim ParFlag = New SqlParameter("@Flag", SqlDbType.NVarChar, 10)


    '        ParTankNo.Direction = ParameterDirection.Input
    '        ParPump1.Direction = ParameterDirection.Input
    '        ParPump2.Direction = ParameterDirection.Input
    '        ParTot1CreatedDT.Direction = ParameterDirection.Input
    '        ParTot1ModifiedDT.Direction = ParameterDirection.Input
    '        ParTot1CreatedBy.Direction = ParameterDirection.Input
    '        ParTot1ModifiedBy.Direction = ParameterDirection.Input
    '        ParTot2CreatedDT.Direction = ParameterDirection.Input
    '        ParTot2ModifiedDT.Direction = ParameterDirection.Input
    '        ParTot2CreatedBy.Direction = ParameterDirection.Input
    '        ParTot2ModifiedBy.Direction = ParameterDirection.Input
    '        ParFlag.direction = ParameterDirection.Input

    '        Dim qry As String = "SELECT * FROM PUMPTOT WHERE TANK_NBR = '" + Session("SentryID").ToString.Trim() + "'"
    '        Dim dsTankTotRecords As New DataSet
    '        dsTankTotRecords = dal.GetDataSet(qry)
    '        If dsTankTotRecords.Tables(0).Rows.Count > 0 Then
    '            ParTankNo.value = Session("SentryID").ToString()
    '            ParPump1.value = CDec(txtPump1StartTotRead.Text.ToString())
    '            ParPump2.value = CDec(IIf(txtPump2StartTotRead.Text.ToString() = "" Or Nothing, 0.0, txtPump2StartTotRead.Text.ToString()))
    '            ParTot1CreatedDT.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_CreatedDT")
    '            ParTot1ModifiedDT.value = DateTime.Now
    '            ParTot1CreatedBy.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_CreatedBy")
    '            ParTot1ModifiedBy.value = Session("User_name").ToString()
    '            ParTot2CreatedDT.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_CreatedDT")
    '            ParTot2ModifiedDT.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_ModifiedDT")
    '            ParTot2CreatedBy.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_CreatedBy")
    '            ParTot2ModifiedBy.value = Session("User_name").ToString()
    '            ParFlag.value = "EDIT"
    '        Else
    '            ParTankNo.value = Session("SentryID").ToString()
    '            ParPump1.value = CDec(txtPump1StartTotRead.Text.ToString())
    '            ParPump2.value = CDec(IIf(txtPump2StartTotRead.Text.ToString() = "" Or Nothing, 0.0, txtPump2StartTotRead.Text.ToString()))
    '            ParTot1CreatedDT.value = DateTime.Now
    '            ParTot1ModifiedDT.value = DBNull.Value.ToString()
    '            ParTot1CreatedBy.value = Session("User_name").ToString()
    '            ParTot1ModifiedBy.value = DBNull.Value.ToString()
    '            ParTot2CreatedDT.value = DateTime.Now
    '            ParTot2ModifiedDT.value = DBNull.Value.ToString()
    '            ParTot2CreatedBy.value = Session("User_name").ToString()
    '            ParTot2ModifiedBy.value = DBNull.Value.ToString()
    '            ParFlag.value = "ADD"
    '        End If

    '        parcollection(0) = ParTankNo
    '        parcollection(1) = ParPump1
    '        parcollection(2) = ParPump2
    '        parcollection(3) = ParTot1CreatedDT
    '        parcollection(4) = ParTot1ModifiedDT
    '        parcollection(5) = ParTot1CreatedBy
    '        parcollection(6) = ParTot1ModifiedBy
    '        parcollection(7) = ParTot2CreatedDT
    '        parcollection(8) = ParTot2ModifiedDT
    '        parcollection(9) = ParTot2CreatedBy
    '        parcollection(10) = ParTot2ModifiedBy
    '        parcollection(11) = ParFlag




    '        dsTankTotRecords = dal.ExecuteStoredProcedureGetDataSet("SP_DFW_InsertUpdate_PumpTotalizer", parcollection)


    '        If dsTankTotRecords.Tables(0).Rows.Count > 0 Then
    '            txtPump1StartTotRead.Text = dsTankTotRecords.Tables(0).Rows(0)("PUMP_TOTALIZER1")
    '            Session("PumpTot1") = txtPump1StartTotRead.Text
    '            txtPump1StartTotDT.Text = IIf((dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_ModifiedDT").ToString = ""), dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_CreatedDT").ToString(), dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_ModifiedDT").ToString())

    '        End If
    '    Catch ex As Exception
    '        Dim exLog As New ErrorPage
    '        exLog.errorlog("Sentry_New_Edit.btnUpdateTot1_Click", ex)
    '    End Try
    'End Sub

    'Protected Sub btnUpdateTot2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateTot2.Click
    '    Try


    '        Dim dal As New GeneralizedDAL()

    '        Dim parcollection(11) As SqlParameter

    '        Dim ParTankNo = New SqlParameter("@TankNbr", SqlDbType.NVarChar, 3)
    '        Dim ParPump1 = New SqlParameter("@PumpTot1", SqlDbType.Decimal)
    '        Dim ParPump2 = New SqlParameter("@PumpTot2", SqlDbType.Decimal)
    '        Dim ParTot1CreatedDT = New SqlParameter("@PumpTot1CreatedDT", SqlDbType.NVarChar, 10)
    '        Dim ParTot1ModifiedDT = New SqlParameter("@PumpTot1ModifiedDT", SqlDbType.NVarChar, 10)
    '        Dim ParTot1CreatedBy = New SqlParameter("@PumpTot1CreatedBy", SqlDbType.NVarChar, 10)
    '        Dim ParTot1ModifiedBy = New SqlParameter("@PumpTot1ModifiedBy", SqlDbType.NVarChar, 10)
    '        Dim ParTot2CreatedDT = New SqlParameter("@PumpTot2CreatedDT", SqlDbType.NVarChar, 10)
    '        Dim ParTot2ModifiedDT = New SqlParameter("@PumpTot2ModifiedDT", SqlDbType.NVarChar, 10)
    '        Dim ParTot2CreatedBy = New SqlParameter("@PumpTot2CreatedBy", SqlDbType.NVarChar, 10)
    '        Dim ParTot2ModifiedBy = New SqlParameter("@PumpTot2ModifiedBy", SqlDbType.NVarChar, 10)
    '        Dim ParFlag = New SqlParameter("@Flag", SqlDbType.NVarChar, 10)



    '        ParTankNo.Direction = ParameterDirection.Input
    '        ParPump1.Direction = ParameterDirection.Input
    '        ParPump2.Direction = ParameterDirection.Input
    '        ParTot1CreatedDT.Direction = ParameterDirection.Input
    '        ParTot1ModifiedDT.Direction = ParameterDirection.Input
    '        ParTot1CreatedBy.Direction = ParameterDirection.Input
    '        ParTot1ModifiedBy.Direction = ParameterDirection.Input
    '        ParTot2CreatedDT.Direction = ParameterDirection.Input
    '        ParTot2ModifiedDT.Direction = ParameterDirection.Input
    '        ParTot2CreatedBy.Direction = ParameterDirection.Input
    '        ParTot2ModifiedBy.Direction = ParameterDirection.Input
    '        ParFlag.direction = ParameterDirection.Input


    '        Dim qry As String = "SELECT * FROM PUMPTOT WHERE TANK_NBR = '" + Session("SentryID").ToString.Trim() + "'"
    '        Dim dsTankTotRecords As New DataSet
    '        dsTankTotRecords = dal.GetDataSet(qry)
    '        If dsTankTotRecords.Tables(0).Rows.Count > 0 Then
    '            ParTankNo.value = Session("SentryID").ToString()
    '            ParPump1.value = CDec(txtPump1StartTotRead.Text.ToString())
    '            ParPump2.value = CDec(txtPump2StartTotRead.Text.ToString())
    '            ParTot1CreatedDT.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_CreatedDT")
    '            ParTot1ModifiedDT.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_ModifiedDT")
    '            ParTot1CreatedBy.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot1_CreatedBy")
    '            ParTot1ModifiedBy.value = Session("User_name").ToString()
    '            ParTot2CreatedDT.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_CreatedDT")
    '            ParTot2ModifiedDT.value = DateTime.Now
    '            ParTot2CreatedBy.value = dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_CreatedBy")
    '            ParTot2ModifiedBy.value = Session("User_name").ToString()
    '            ParFlag.value = "EDIT"
    '        Else
    '            ParTankNo.value = Session("SentryID").ToString()
    '            ParPump1.value = CDec(txtPump1StartTotRead.Text.ToString())
    '            ParPump2.value = CDec(txtPump2StartTotRead.Text.ToString())
    '            ParTot1CreatedDT.value = DateTime.Now
    '            ParTot1ModifiedDT.value = DBNull.Value.ToString()
    '            ParTot1CreatedBy.value = Session("User_name").ToString()
    '            ParTot1ModifiedBy.value = DBNull.Value.ToString()
    '            ParTot2CreatedDT.value = DateTime.Now
    '            ParTot2ModifiedDT.value = DBNull.Value.ToString()
    '            ParTot2CreatedBy.value = Session("User_name").ToString()
    '            ParTot2ModifiedBy.value = DBNull.Value.ToString()
    '            ParFlag.value = "ADD"
    '        End If

    '        parcollection(0) = ParTankNo
    '        parcollection(1) = ParPump1
    '        parcollection(2) = ParPump2
    '        parcollection(3) = ParTot1CreatedDT
    '        parcollection(4) = ParTot1ModifiedDT
    '        parcollection(5) = ParTot1CreatedBy
    '        parcollection(6) = ParTot1ModifiedBy
    '        parcollection(7) = ParTot2CreatedDT
    '        parcollection(8) = ParTot2ModifiedDT
    '        parcollection(9) = ParTot2CreatedBy
    '        parcollection(10) = ParTot2ModifiedBy
    '        parcollection(11) = ParFlag




    '        dsTankTotRecords = dal.ExecuteStoredProcedureGetDataSet("SP_DFW_InsertUpdate_PumpTotalizer", parcollection)


    '        If dsTankTotRecords.Tables(0).Rows.Count > 0 Then
    '            If Not (dsTankTotRecords.Tables(0).Rows(0)("Pump_totalizer2").ToString() = "") Then
    '                txtPump2StartTotRead.Text = dsTankTotRecords.Tables(0).Rows(0)("Pump_totalizer2")
    '                Session("PumpTot2") = txtPump2StartTotRead.Text
    '                txtPump2StartTotDT.Text = IIf((dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_ModifiedDT").ToString = ""), dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_CreatedDT").ToString(), dsTankTotRecords.Tables(0).Rows(0)("Pump_Tot2_ModifiedDT").ToString())
    '                btnUpdateTot2.Visible = True

    '            End If
    '        End If
    '    Catch ex As Exception
    '        Dim exLog As New ErrorPage
    '        exLog.errorlog("Sentry_New_Edit.btnUpdateTot2_Click", ex)
    '    End Try
    'End Sub

    'Private Sub BindSentryTM()
    '    Try
    '        Dim dal As New GeneralizedDAL()
    '        Dim dsTM As DataSet
    '        dsTM = dal.ExecuteStoredProcedureGetDataSet("Use_TT_GetSentryGoldTM") ', parcollection)

    '        If dsTM.Tables(0).Rows.Count > 0 Then

    '            For i = 1 To dsTM.Tables(0).Rows.Count
    '                ddlSentryTM = CType(Page.FindControl("ddlSentryTM"), DropDownList)
    '                ddlSentryTM.DataSource = dsTM.Tables(0)
    '                ddlSentryTM.DataTextField = "TMNumber"
    '                ddlSentryTM.DataValueField = "TMNumber"
    '                ddlSentryTM.DataBind()
    '                ddlSentryTM.Items.Insert(0, "Select TM")

    '            Next
    '        End If


    '    Catch ex As Exception
    '        Dim exLog As New ErrorPage
    '        exLog.errorlog("Sentry_New_Edit.UpdateSentryTM", ex)
    '    End Try
    'End Sub

    'Protected Sub ddlSentry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSentry.SelectedIndexChanged
    '    Try
    '        If (ddlSentry.SelectedItem.ToString().Contains("Radio")) Then
    '            rdoBuadRate.Enabled = False
    '        Else
    '            rdoBuadRate.Enabled = True
    '        End If
    '    Catch ex As Exception
    '        Dim exLog As New ErrorPage
    '        exLog.errorlog("Sentry_New_Edit.ddlSentry_SelectedIndexChanged", ex)
    '    End Try
    'End Sub

    'Protected Sub btnPollingLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPollingLog.Click

    'End Sub

    Protected Sub btnPollingLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPollingLog.Click

    End Sub

    
    'Protected Sub btnDiagnostic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiagnostic.Click
    '    System.Threading.Thread.Sleep(5000)
    '    Dim script As String = String.Empty
    '    script = "window.open('Diagnostic.aspx');"
    '    System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "ScriptKey", script, True)
    'End Sub
End Class
