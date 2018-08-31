Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Collections.Generic
Imports System.Timers
Imports System.IO

Partial Class TankMonitor_New_Edit
    Inherits System.Web.UI.Page

    Shared DeviceId As String = String.Empty
    Shared UpdEdit As String = String.Empty
    Shared txtAppend As String = String.Empty
    Shared InTime As String = String.Empty
    Shared OutTime As String = String.Empty
    Dim ds As DataSet
    Dim i As Integer
    'Added By Varun To Test Polling
    Private tankNum As String
    Private tankType As String
    Private command As String
    Private queueStatus As String
    Private timeQueue As DateTime
    Private timePollingStart As DateTime
    Private timePollingComplete As DateTime
    Public Property TankNumber() As String
        Get
            Return tankNum
        End Get
        Set(ByVal Value As String)
            tankNum = Value
        End Set
    End Property
    Public Property Type() As String
        Get
            Return tankType
        End Get
        Set(ByVal Value As String)
            tankType = Value
        End Set
    End Property
    Public Property CommandType() As String
        Get
            Return command
        End Get
        Set(ByVal Value As String)
            command = Value
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

    'Public Sub StartRuning()
    '    AddHandler timer.Elapsed, AddressOf GetMsgIn
    '    timer.Start()
    'End Sub
    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnok.Click
        Try
            If (UpdEdit = "0") Then
                InsertData("0")
            ElseIf UpdEdit = "1" And (Not Request.QueryString.Get("RowID") = "") Then
                InsertData("1")
            Else
                InsertData("0")
            End If


            'If btnok.Text.ToLower() = "ok" Then
            '    InsertData("0")
            'ElseIf btnok.Text.ToLower() = "update" And (Not Request.QueryString.Get("RowID") = "") Then
            '    InsertData("1")
            'End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_Edit.btnOK_Click", ex)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session.Timeout = 100
            ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "txtMessageIn", "buttonClicked();", True)

            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            Else
                addattribute()
                If Not Page.IsPostBack Then
                    If Request.QueryString.Count > 0 Then
                        If Not Request.QueryString.Get("RowID") Is Nothing And Not Request.QueryString.Get("RowID") = "" Then
                            ds = GetallData(Request.QueryString.Get("RowID"))
                            If Not ds Is Nothing Then
                                If ds.Tables(0).Rows.Count > 0 Then
                                    btnok.Text = "Update"
                                    If (btnok.Text = "Update") Then
                                        UpdEdit = "1"
                                    Else
                                        UpdEdit = "0"
                                    End If
                                    txtTm.Text = ds.Tables(0).Rows(0)(0).ToString()
                                    DeviceId = txtTm.Text.ToString()
                                    txtname.Text = ds.Tables(0).Rows(0)(1).ToString()
                                    'txtcom.Text = ds.Tables(0).Rows(0)(2).ToString()

                                    'Added By Varun Moota to Fix COMM PORT Issue.09/07/2010
                                    Session("TMCOMMValue") = ds.Tables(0).Rows(0)("COMMPORT").ToString()
                                    HiddenCommVal.Value = Session("TMCOMMValue").ToString()
                                    Session("TMPhoneValue") = ds.Tables(0).Rows(0)("PHONENO").ToString()
                                    HiddenPhoneVal.Value = Session("TMPhoneValue").ToString()
                                    Dim blnIPCOMM As Boolean = ds.Tables(0).Rows(0)(10)
                                    txtcom.Text = GetCOMMValue(blnIPCOMM)

                                    If (ds.Tables(0).Rows(0)(3).ToString() = "1200") Then
                                        RDOList.Items(0).Selected = True
                                    ElseIf (ds.Tables(0).Rows(0)(3).ToString() = "2400") Then
                                        RDOList.Items(1).Selected = True
                                    ElseIf (ds.Tables(0).Rows(0)(3).ToString() = "4800") Then
                                        RDOList.Items(2).Selected = True
                                    Else
                                        RDOList.Items(3).Selected = True
                                    End If

                                    If (ds.Tables(0).Rows(0)(4).ToString() = "1") Then
                                        Rdom1.Checked = True
                                        'ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "2") Then
                                        '    Rdom2.Checked = True
                                        'ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "3") Then
                                        '    Rdom3.Checked = True

                                    ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "4") Then
                                        Rdom4.Checked = True
                                    ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "5") Then
                                        Rdom5.Checked = True
                                    ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "6") Then
                                        Rdom6.Checked = True
                                    ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "7") Then
                                        Rdom7.Checked = True
                                        txtcode.Attributes.Add("disabled", "disabled")
                                        txtWlen.Attributes.Add("disabled", "disabled")
                                        dropParity.Attributes.Add("disabled", "disabled")
                                        RDOList.Attributes.Add("disabled", "disabled")
                                        'Added By varun Moota, no need to Poll Sentry Gold TM.03/23/2011
                                        btnpoll.Attributes.Add("disabled", "disabled")

                                    End If


                                    txtcode.Text = ds.Tables(0).Rows(0)(5).ToString()
                                    'Commented By Varun to Test TM's Edit
                                    'While Not dropParity.SelectedValue.ToLower() = ds.Tables(0).Rows(0)(8).ToString().ToLower()
                                    '    dropParity.SelectedIndex = i
                                    '    i = i + 1
                                    'End While

                                    'Added By Varun
                                    Dim strParity As String = ds.Tables(0).Rows(0)(8).ToString() '.ToLower()
                                    dropParity.SelectedValue = strParity


                                    txtWlen.Text = ds.Tables(0).Rows(0)(9).ToString()

                                    If ds.Tables(0).Rows(0)(10) = True Then
                                        CHKCOm.Checked = True
                                        lblcom.Text = "IP COM # :"
                                        txtComPort.Text = Session("TMCOMMValue").ToString()
                                    Else
                                        CHKCOm.Checked = False
                                        lblcom.Text = "Phone # :"
                                        txtComPort.Text = ""
                                    End If
                                End If
                            End If
                        End If
                    Else
                        txtTm.Text = GetMaxRecord()
                        lblHeader.Text = "New Tank Monitor Screen"
                        RDOList.Items(1).Selected = True

                        HiddenCommVal.Value = ""
                        HiddenPhoneVal.Value = ""
                    End If
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_edit_InsertData", ex)
        End Try
    End Sub

    Private Function GetMaxRecord() As String
        Dim strMax As String
        Try
            Dim dal As New GeneralizedDAL()
            strMax = dal.ExecuteScalarGetString("Use_TT_GetMaxTM")
            If strMax = "" Then
                strMax = "001"
            Else
                strMax = strMax + 1
                strMax = String.Format("{0:000}", Convert.ToInt32(strMax))
            End If
            GetMaxRecord = strMax
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_edit_BindControl", ex)
            GetMaxRecord = ""
        End Try
    End Function

    Private Sub InsertData(ByVal val As Integer)
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(14) As SqlParameter
            Dim ParNUMBER = New SqlParameter("@NUMBER", SqlDbType.NVarChar, 3)
            Dim ParNAME = New SqlParameter("@NAME", SqlDbType.NVarChar, 25)
            Dim ParPHONENO = New SqlParameter("@PHONENO", SqlDbType.NVarChar, 20)
            Dim ParBAUD = New SqlParameter("@BAUD", SqlDbType.NVarChar, 4)
            Dim ParTYPE = New SqlParameter("@TYPE", SqlDbType.SmallInt)
            Dim ParSECCODE = New SqlParameter("@SECCODE", SqlDbType.NVarChar, 6)
            Dim ParPORT = New SqlParameter("@PORT", SqlDbType.SmallInt)
            Dim ParPOLL = New SqlParameter("@POLL", SqlDbType.NVarChar, 1)
            Dim ParPARITY = New SqlParameter("@PARITY", SqlDbType.NVarChar, 1)
            Dim ParWORDLENGTH = New SqlParameter("@WORDLENGTH", SqlDbType.NVarChar, 1)
            Dim ParIPCOMM = New SqlParameter("@IPCOMM", SqlDbType.Bit)
            Dim ParFlag = New SqlParameter("@Flag", SqlDbType.VarChar)
            'Added By Varun
            Dim parID = New SqlParameter("@ID", SqlDbType.Int)
            'Added By Varun Moota.09/07/2010
            Dim parCOMM = New SqlParameter("@COMMPORT", SqlDbType.NVarChar, 20)
            Dim ParLOCALPORT = New SqlParameter("@LOCALPORT", SqlDbType.NVarChar, 20)
            ParNUMBER.Direction = ParameterDirection.Input
            ParNAME.Direction = ParameterDirection.Input
            ParPHONENO.Direction = ParameterDirection.Input
            ParBAUD.Direction = ParameterDirection.Input
            ParTYPE.Direction = ParameterDirection.Input
            ParSECCODE.Direction = ParameterDirection.Input
            ParPORT.Direction = ParameterDirection.Input
            ParPOLL.Direction = ParameterDirection.Input
            ParPARITY.Direction = ParameterDirection.Input
            ParWORDLENGTH.Direction = ParameterDirection.Input
            ParIPCOMM.Direction = ParameterDirection.Input
            ParFlag.Direction = ParameterDirection.Input
            parID.Direction = ParameterDirection.Input
            parCOMM.Direction = ParameterDirection.Input
            ParLOCALPORT.Direction = ParameterDirection.Input
            ParNUMBER.value = Request.Form(txtTm.UniqueID) 'txtTm.Text
            ParNAME.value = txtname.Text
            'ParPHONENO.value = txtcom.Text
            parID.value = 0
            'Added By Varun Moota to Fix Comm Port Issue.05/27/2010
            If Session("TMCOMMValue") = Nothing Then
                Session("TMCOMMValue") = ""
            End If
            If Session("TMPhoneValue") = Nothing Then
                Session("TMPhoneValue") = ""
            End If
            If CHKCOm.Checked Then
                ParCOMM.value = txtcom.Text
                ParPHONENO.value = DBNull.Value
                ParLOCALPORT.value = DBNull.Value
            Else
                If txtcom.Text = "" Then
                    ParLOCALPORT.value = GetLocalPortValue()
                    ParCOMM.value = DBNull.Value
                    ParPHONENO.value = DBNull.Value
                Else
                    ParPHONENO.value = txtcom.Text
                    ParCOMM.value = DBNull.Value
                    ParLOCALPORT.value = DBNull.Value
                End If
            End If
            ParBAUD.value = RDOList.SelectedValue
            If Rdom1.Checked Then
                ParTYPE.value = "1"
                'ElseIf Rdom2.Checked Then
                '    ParTYPE.value = "2"
                'ElseIf Rdom3.Checked Then
                '    ParTYPE.value = "3"
            ElseIf Rdom4.Checked Then
                ParTYPE.value = "4"
            ElseIf Rdom5.Checked Then
                ParTYPE.value = "5"
            ElseIf Rdom6.Checked Then
                ParTYPE.value = "6"
            ElseIf Rdom7.Checked Then
                ParTYPE.value = "7"
            End If
            If Rdom7.Checked Then
                ParSECCODE.value = ""
            Else
                ParSECCODE.value = txtcode.Text
            End If
            ParPORT.value = "1"
            ParPOLL.value = "1"

            ParPARITY.value = dropParity.SelectedValue
            If Rdom7.Checked Then
                ParWORDLENGTH.value = ""
            Else
                ParWORDLENGTH.value = txtWlen.Text
            End If
            If CHKCOm.Checked Then
                ParIPCOMM.value = True
            Else
                ParIPCOMM.value = False
            End If
            ParFlag.value = val
            parcollection(0) = parID
            parcollection(1) = ParNUMBER
            parcollection(2) = ParNAME
            parcollection(3) = ParPHONENO
            parcollection(4) = ParBAUD
            parcollection(5) = ParTYPE
            parcollection(6) = ParSECCODE
            parcollection(7) = ParPORT
            parcollection(8) = ParPOLL
            parcollection(9) = ParPARITY
            parcollection(10) = ParWORDLENGTH
            parcollection(11) = ParIPCOMM
            parcollection(12) = ParFlag
            'Added By Varun Moota.09/08/2010
            parcollection(13) = parCOMM
            parcollection(14) = ParLOCALPORT
            Dim blnFlag As Boolean = dal.ExecuteSQLStoredProcedureGetBoolean("Use_tt_InsertTMDetails", parcollection)

            If blnFlag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record has been saved successfully !!');location.href='TankMonitor.aspx';</script>")
            End If
            'Response.Redirect("TankMonitor.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_edit_InsertData", ex)
        End Try
    End Sub
    Public Sub addattribute()
        Try
            txtcode.Attributes.Add("OnKeyPress", "KeyPressYear(event);")
            Rdom1.Attributes.Add("onClick", "ChkOCtl();")
            'Rdom2.Attributes.Add("onClick", "ChkOCtl();")
            'Rdom3.Attributes.Add("onClick", "ChkOCtl();")
            Rdom4.Attributes.Add("onClick", "ChkOCtl();")
            Rdom5.Attributes.Add("onClick", "ChkOCtl();")
            Rdom6.Attributes.Add("onClick", "ChkOCtl();")
            Rdom7.Attributes.Add("onClick", "Chkctl();")
            txtWlen.Attributes.Add("OnKeyPress", "KeyPressYear(event);")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_edit.addattribute()", ex)
        End Try
    End Sub
    Public Function GetallData(ByVal RowId As String) As DataSet
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim ParNUMBER = New SqlParameter("@NUMBER", SqlDbType.NVarChar, 3)
            ParNUMBER.Direction = ParameterDirection.Input
            ParNUMBER.value = RowId
            parcollection(0) = ParNUMBER
            GetallData = dal.ExecuteStoredProcedureGetDataSet("Use_TT_GetTMAllDetails", parcollection)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_edit_GetallData", ex)
            GetallData = Nothing
        End Try
    End Function
    Public Function DisplayParentData()
        Try
            Display()
        Catch ex As Exception

        End Try
    End Function
    Public Function Display()
        Try
            txtDeviceType.Text = Request.QueryString.Get("RowID").ToString()
            ds = GetallData(Request.QueryString.Get("RowID"))
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    'Added By Pritam  to Show COMM PORT and Device Type.30-May-2014
                    Session("TMCOMMValue") = ds.Tables(0).Rows(0)("COMMPORT").ToString()
                    HiddenCommVal.Value = Session("TMCOMMValue").ToString()
                    Session("TMPhoneValue") = ds.Tables(0).Rows(0)("PHONENO").ToString()
                    HiddenPhoneVal.Value = Session("TMPhoneValue").ToString()
                    Dim blnIPCOMM As Boolean = ds.Tables(0).Rows(0)(10)
                    txtcom.Text = GetCOMMValue(blnIPCOMM)
                    If (ds.Tables(0).Rows(0)(4).ToString() = "1") Then
                        txtDeviceType.Text = "TM" '"VRTLS"
                    ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "4") Then
                        txtDeviceType.Text = "TM" '"RedJacket"
                    ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "5") Then
                        txtDeviceType.Text = "TM" '"VR350"
                    ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "6") Then
                        txtDeviceType.Text = "TM" '"Pneumerator"
                    ElseIf (ds.Tables(0).Rows(0)(4).ToString() = "7") Then
                        txtDeviceType.Text = "TM" '"SentryGold"
                    End If
                    txtComPort.Text = ds.Tables(0).Rows(0)("COMMPORT").ToString()
                    DeviceId = ds.Tables(0).Rows(0)("NUMBER").ToString()
                    txtCrntTime.Value = System.DateTime.Now.ToString()
                End If
            End If
        Catch ex As Exception
        End Try
    End Function
    Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click

        Try
            Response.Redirect("TankMonitor.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_edit.btncancel_Click", ex)
        End Try
    End Sub
    'Added By Varun to Test Polling Tank Monitor
    Protected Sub btnpoll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpoll.Click

        Try
            SendPollData()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_edit.btnpoll_Click", ex)
        End Try
    End Sub
    'Added BY Varun to Send Poll Data
    Private Sub SendPollData()

        TankNumber = txtTm.Text
        Type = "TM"
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


            param(0) = New SqlParameter("@ID", SqlDbType.UniqueIdentifier)
            param(0).Value = System.Guid.NewGuid()

            param(1) = New SqlParameter("@DeviceId", SqlDbType.NVarChar)
            param(1).Value = TankNumber

            param(2) = New SqlParameter("@DeviceType", SqlDbType.NVarChar)
            param(2).Value = Type

            param(3) = New SqlParameter("@Command", SqlDbType.NVarChar)
            param(3).Value = CommandType

            param(4) = New SqlParameter("@Status", SqlDbType.NVarChar)
            param(4).Value = QueuedStatus

            param(5) = New SqlParameter("@TimeQueued", SqlDbType.DateTime)
            param(5).Value = TimeQueued

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
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Polled Being Queued successfully !!');location.href='TankMonitor.aspx';</script>")

                'btnpoll.Enabled = False
            Else
                Dim CSM As ClientScriptManager = Page.ClientScript

                CSM.RegisterClientScriptBlock((Me.GetType), "javascript", "alert('Error!');")
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankMonitor_New_Edit.SendPollData", ex)
        End Try
    End Sub

    Public Function GetCOMMValue(ByVal IPCOMM As Boolean) As String
        Try

            Dim PortNumber As String = ""
            If Session("TMCOMMValue") = Nothing Then
                Session("TMCOMMValue") = ""
            End If
            If Session("TMPhoneValue") = Nothing Then
                Session("TMPhoneValue") = ""
            End If
            If IPCOMM Then
                CHKCOm.Checked = True
                PortNumber = Session("TMCOMMValue").ToString()
                HiddenCommVal.Value = Session("TMCOMMValue").ToString()

            Else
                CHKCOm.Checked = False
                PortNumber = Session("TMPhoneValue").ToString()
                HiddenPhoneVal.Value = Session("TMPhoneValue").ToString()
            End If
            Return PortNumber
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankMonitor_New_Edit.GetCOMMValue", ex)
        End Try
    End Function

    'Added By Varun Moota To Get Comm Value
    Public Function GetLocalPortValue() As String
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

            Return Commvalue
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_New_Edit.GetCommValue()", ex)
        End Try

    End Function

    Protected Sub btnlink_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnlink.Click

        Try
            'txtDeviceType.Text = Session("DeviceType").ToString()

        Catch ex As Exception

        End Try
        ''Response.Redirect("DirectLinkTankMonitor.aspx", False)
        'Try
        '    Dim p As Diagnostics.Process = Nothing
        '    If p Is Nothing Then
        '        p = New Diagnostics.Process
        '        p.StartInfo.FileName = "C:\Program Files\Windows NT\hypertrm.exe"
        '        'p.StartInfo.FileName = "notepad.exe"

        '        p.Start()
        '    Else
        '        p.Close()
        '        p.Dispose()
        '    End If
        'Catch ee As Exception

        'End Try
    End Sub

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        ' Try

        ' txtAppend = String.Empty
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtMessageIn", "buttonClicked();", True)
        Dim ds1 As DataSet
        Dim dal = New GeneralizedDAL()
        Dim MsgOut As String
        Dim CommandMsgOut As String
        Dim MessageOut As String
        MsgOut = txtMessageOut.Text.ToString()
        Dim AppStart As String
        AppStart = System.DateTime.Now.ToString("yyyy-MM-dd")
        CommandMsgOut = txtInterval.Value.ToString()
        Dim CmdMsgOut As String

        Try
            If Not (MsgOut = String.Empty) Then


                If (CommandMsgOut = "n") Then
                    MessageOut = MsgOut & "\n"
                    'ParMessageOut.Value = MessageOut
                    CmdMsgOut = MessageOut
                ElseIf (CommandMsgOut = "r") Then
                    MessageOut = MsgOut & "\r"
                    'ParMessageOut.Value = MessageOut
                    CmdMsgOut = MessageOut
                Else
                    'ParMessageOut.Value = MsgOut
                    CmdMsgOut = MsgOut
                End If

                Dim MessageOutTime As DateTime
                MessageOutTime = Convert.ToDateTime(txtCrntTime.Value)

                ReadMessageIn(AppStart, CmdMsgOut, MessageOutTime)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "txtMessageIn", "buttonClicked()", True)
               
                DisplayParentData()
            End If
            DisplayParentData()
         

            txtMessageOut.Text = ""
        Catch ex As Exception

            txtMessageIn.Text = "No Response, Refresh The Page..!"
            DisplayParentData()

            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_Edit.btnSend_Click()", ex)
        End Try
    End Sub
    Public Function ReadMessageIn(ByVal appstartedat As String, ByVal MsgOut As String, ByVal msgOutTime As DateTime)
        Dim ds1 As DataSet
        Dim dal = New GeneralizedDAL()
        Try
            If Not (MsgOut = String.Empty) Then
                System.Threading.Thread.Sleep(5000)
                Dim parcollection(1) As SqlParameter
                Dim ParMessageOut = New SqlParameter("@Messageout", SqlDbType.NVarChar, 50)
                Dim ParCurrentTime = New SqlParameter("@TimeQueued", SqlDbType.DateTime)
                ParMessageOut.Direction = ParameterDirection.Input
                ParCurrentTime.Direction = ParameterDirection.Input

                ParCurrentTime.Value = Convert.ToDateTime(txtCrntTime.Value)
                parcollection(0) = ParMessageOut
                parcollection(1) = ParCurrentTime
                System.Threading.Thread.Sleep(5000)

                ParMessageOut.Value = MsgOut
                ParCurrentTime.Value = msgOutTime

                ds1 = dal.ExecuteStoredProcedureGetDataSet("USP_TT_SelectMessageIn", parcollection)

                If Not (IsDBNull(ds1.Tables(0).Rows(0)("MessageIn"))) Then
                    InTime = Now.ToString("HH:mm:ss")
                    txtMessageIn.Text = ds1.Tables(0).Rows(0)("MessageIn").ToString()
                    txtMessageIn.TextMode = TextBoxMode.MultiLine
                    'txtAppend = txtAppend & ControlChars.CrLf & "Application Started at " & AppStart & ControlChars.CrLf & "MSG OUT>>" & "<ctrl>" & MsgOut & "<return>".ToString() & ControlChars.CrLf & "MSG IN>>" & txtMessageIn.Text.ToString()
                    txtAppend = txtAppend & ControlChars.CrLf & "Command Started at " & appstartedat & ControlChars.CrLf & OutTime & " " & "MSG OUT>> " & txtMessageOut.Text.ToString() & ControlChars.CrLf & InTime & " " & "MSG IN<< " & txtMessageIn.Text.ToString() & ControlChars.CrLf
                    txtMessageIn.Text = txtAppend
                    Session("PortTerminallog") = txtAppend.ToString()

                    DisplayParentData()
                    Return Nothing
                End If

                Dim i As Integer
                i = 0
                'System.Threading.Thread.Sleep(Interval)
                While i < 5 And IsDBNull(ds1.Tables(0).Rows(0)("MessageIn"))
                    System.Threading.Thread.Sleep(5000)

                    ParMessageOut.Direction = ParameterDirection.Input
                    ParCurrentTime.Direction = ParameterDirection.Input

                    ParMessageOut.Value = MsgOut
                    ParCurrentTime.Value = msgOutTime

                    parcollection(0) = ParMessageOut
                    parcollection(1) = ParCurrentTime
                    ds1 = dal.ExecuteStoredProcedureGetDataSet("USP_TT_SelectMessageIn", parcollection)
                    i += 1

                End While
                If i >= 0 And Not IsDBNull(ds1.Tables(0).Rows(0)("MessageIn")) Then
                    InTime = Now.ToString("HH:mm:ss")
                    txtMessageIn.Text = ds1.Tables(0).Rows(0)("MessageIn").ToString()
                    txtMessageIn.TextMode = TextBoxMode.MultiLine
                    txtAppend = txtAppend & ControlChars.CrLf & "Command Started at " & appstartedat & ControlChars.CrLf & OutTime & " " & "MSG OUT>> " & txtMessageOut.Text.ToString() & ControlChars.CrLf & InTime & " " & "MSG IN<< " & txtMessageIn.Text.ToString() & ControlChars.CrLf
                    txtMessageIn.Text = txtAppend
                    Session("PortTerminallog") = txtAppend.ToString()
                    DisplayParentData()
                    Return Nothing
                End If

                Dim dsRecordId As DataSet
                System.Threading.Thread.Sleep(5000)
                'Dim parcoltn(0) As SqlParameter
                'Dim parMsgOut = New SqlParameter("@Messageout", SqlDbType.NVarChar, 50)
                'Dim parCurrentDateTime = New SqlParameter("@TimeQueued", SqlDbType.DateTime)
                dsRecordId = dal.ExecuteStoredProcedureGetDataSet("USP_TT_SelectMessageInRecordId", parcollection)

                Dim parcoltn(0) As SqlParameter
                Dim dsId As DataSet
                Dim parMsgOutRecordId = New SqlParameter("@Id", SqlDbType.NVarChar, 100)
                parMsgOutRecordId.Direction = ParameterDirection.Input

                parMsgOutRecordId.Value = dsRecordId.Tables(0).Rows(0)("Id").ToString()
                parcoltn(0) = parMsgOutRecordId

                dsId = dal.ExecuteStoredProcedureGetDataSet("USP_TT_SelectMessageInForRecordId", parcoltn)


                If Not (IsDBNull(dsId.Tables(0).Rows(0)("MessageIn"))) Then
                    InTime = Now.ToString("HH:mm:ss")
                    txtMessageIn.Text = dsId.Tables(0).Rows(0)("MessageIn").ToString()
                    txtMessageIn.TextMode = TextBoxMode.MultiLine
                    'txtAppend = txtAppend & ControlChars.CrLf & "Application Started at " & AppStart & ControlChars.CrLf & "MSG OUT>>" & "<ctrl>" & MsgOut & "<return>".ToString() & ControlChars.CrLf & "MSG IN>>" & txtMessageIn.Text.ToString()
                    txtAppend = txtAppend & ControlChars.CrLf & "Command Started at " & appstartedat & ControlChars.CrLf & OutTime & " " & "MSG OUT>> " & txtMessageOut.Text.ToString() & ControlChars.CrLf & InTime & " " & "MSG IN<< " & txtMessageIn.Text.ToString() & ControlChars.CrLf
                    txtMessageIn.Text = txtAppend
                    Session("PortTerminallog") = txtAppend.ToString()
                    DisplayParentData()
                    Return Nothing
                End If

                Dim j As Integer
                Dim k As Integer
                k = 0
                j = 0
                'System.Threading.Thread.Sleep(Interval)
                While k < 6 And IsDBNull(dsId.Tables(0).Rows(0)("MessageIn"))
                    While j < 5 And IsDBNull(dsId.Tables(0).Rows(0)("MessageIn"))
                        System.Threading.Thread.Sleep(5000)

                        Dim parRecordId = New SqlParameter("@Id", SqlDbType.NVarChar, 100)

                        parMsgOutRecordId.Direction = ParameterDirection.Input

                        parRecordId.Value = dsRecordId.Tables(0).Rows(0)("Id").ToString()
                        parcoltn(0) = parRecordId

                        dsId = dal.ExecuteStoredProcedureGetDataSet("USP_TT_SelectMessageInForRecordId", parcoltn)

                        j += 1

                    End While
                    k += 1
                    j = 0
                End While
                If j >= 0 And Not IsDBNull(dsId.Tables(0).Rows(0)("MessageIn")) Then
                    InTime = Now.ToString("HH:mm:ss")
                    txtMessageIn.Text = dsId.Tables(0).Rows(0)("MessageIn").ToString()
                    txtMessageIn.TextMode = TextBoxMode.MultiLine
                    txtAppend = txtAppend & ControlChars.CrLf & "Command Started at " & appstartedat & ControlChars.CrLf & OutTime & " " & "MSG OUT>> " & txtMessageOut.Text.ToString() & ControlChars.CrLf & InTime & " " & "MSG IN<< " & txtMessageIn.Text.ToString() & ControlChars.CrLf
                    txtMessageIn.Text = txtAppend
                    Session("PortTerminallog") = txtAppend.ToString()
                    'DisplayParentData()
                ElseIf IsDBNull(dsId.Tables(0).Rows(0)("MessageIn")) Then
                    txtMessageIn.Text = "Command Timeout..."
                End If
                DisplayParentData()
                txtMessageIn.Focus()
            End If
        Catch ex As Exception
            txtMessageIn.Text = "No Response, Refresh The Page..!"
            DisplayParentData()
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_Edit.btnSend_Click()", ex)
        End Try
    End Function

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtMessageIn.Text = ""
        txtMessageOut.Text = ""
        txtAppend = String.Empty
        DisplayParentData()
    End Sub
    '<WebMethod(EnableSession:=True)> _
    <WebMethod()> _
   Public Shared Function GetAutoCompleteData(ByVal name As String, ByVal asciicode As String, ByVal currentTime As String) As String
        Try
            ' If Not Page.IsPostBack Then
            Dim MessageOut As String
            If Not (name = String.Empty) Then
                Dim dal = New GeneralizedDAL()
                Dim parcollection(5) As SqlParameter
                Dim ParMessageOut = New SqlParameter("@Messageout", SqlDbType.NVarChar, -1)
                Dim ParDeviceType = New SqlParameter("@DeviceType", SqlDbType.NVarChar, 20)
                Dim ParCommand = New SqlParameter("@Command", SqlDbType.NVarChar, 5)
                Dim ParTimeQueued = New SqlParameter("@TimeQueued", SqlDbType.DateTime)
                Dim ParDeviceId = New SqlParameter("@DeviceId", SqlDbType.Int)
                Dim ParStatus = New SqlParameter("@Status", SqlDbType.NVarChar, 10)

                ParMessageOut.Direction = ParameterDirection.Input
                ParDeviceType.Direction = ParameterDirection.Input
                ParCommand.Direction = ParameterDirection.Input
                ParTimeQueued.Direction = ParameterDirection.Input
                ParDeviceId.Direction = ParameterDirection.Input
                ParStatus.Direction = ParameterDirection.Input

                If (asciicode = "1") Then
                    MessageOut = name & "\n"
                    ParMessageOut.Value = MessageOut.ToString()
                ElseIf (asciicode = "2") Then
                    MessageOut = name & "\r"
                    ParMessageOut.Value = MessageOut.ToString()
                Else
                    ParMessageOut.Value = name
                End If
                'ParMessageOut.Value = MessageOut.ToString()
                ParDeviceType.Value = "TM" 'HttpContext.Current.Session("DeviceTypt").ToString()
                ParCommand.Value = "CMND"
                ParTimeQueued.Value = Convert.ToDateTime(currentTime)
                ParDeviceId.Value = DeviceId
                ParStatus.Value = "Queued"

                parcollection(0) = ParMessageOut
                parcollection(1) = ParDeviceType
                parcollection(2) = ParCommand
                parcollection(3) = ParTimeQueued
                parcollection(4) = ParDeviceId
                parcollection(5) = ParStatus

                Dim blnFlag As Boolean = dal.ExecuteSQLStoredProcedureGetBoolean("USP_TT_DirectLink_Insert", parcollection)
                If (blnFlag) Then
                    OutTime = Now.ToString("HH:mm:ss")
                End If
                Return blnFlag
            Else
                Return Nothing
            End If
            ' End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_New_Edit.GetAutoCompleteData()", ex)
            Return Nothing
        End Try
    End Function
    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.Hide()
        txtAppend = String.Empty
        txtMessageIn.Text = ""
        txtMessageOut.Text = ""
        DisplayParentData()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim sw As StreamWriter
            Dim path As String = HttpContext.Current.Server.MapPath("WexImport\FuelTRAKPortTerminal_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt")
            sw = New StreamWriter(path, True)
            'sw.WriteLine(txtAppend)
            Dim PortTerminal As String
            PortTerminal = Session("PortTerminallog").ToString()
            sw.WriteLine(PortTerminal)
            sw.Flush()
            sw.Close()
            Dim f As FileInfo = New FileInfo(HttpContext.Current.Server.MapPath("WexImport\FuelTRAKPortTerminal_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt"))
            'Response.Redirect("FuelTRAKPortTerminal_Export.aspx?file=" & f.Name, False)
            Response.Redirect("ExportFile.aspx?PortTerminal=" & f.Name & "&file=False", False)
            txtMessageIn.Text = String.Empty
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_Edit.btnSave_Click", ex)
        End Try
    End Sub
End Class

