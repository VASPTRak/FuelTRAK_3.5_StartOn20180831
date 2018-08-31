Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class CreateUser
    Inherits System.Web.UI.Page
    Dim GenFun As GeneralFunctions
    Dim ds As DataSet
    Dim dal As GeneralizedDAL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            If Not Page.IsPostBack Then

                txtCommPort.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtEnbrLen.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtVnbrLen.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtPMCnt.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")

                'Dim compName As String() = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_host")).HostName.Split("."c)
                'Dim completeHostName As String = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_host")).HostName
                'Response.Write(compName(0).ToString())
                'Response.Write("---------")
                'Response.Write(completeHostName)

                'Dim ipList As System.Net.IPHostEntry
                'ipList = System.Net.Dns.GetHostByAddress(Request.ServerVariables("REMOTE_HOST").ToString())
                'Response.Write(ipList.HostName.ToString())
                PopulateStatus()
                PopulateCount()

            End If

            If (Hidtxt.Value = "true") Then
                UpdateStatusInfo()
                PopulateCount()
                Hidtxt.Value = ""
            ElseIf (Hidtxt.Value = "false") Then
                Hidtxt.Value = ""
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("StatusInfo.Page_Load", ex)
        End Try

    End Sub

    Private Sub PopulateCount()
        Try


            ds = New DataSet
            dal = New GeneralizedDAL()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_RecCount")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    txtTxtnCnt.Text = ds.Tables(0).Rows(0)("txtncnt").ToString()
                    txtVehCnt.Text = ds.Tables(0).Rows(0)("vehscnt").ToString()
                    txtPersCnt.Text = ds.Tables(0).Rows(0)("perscnt").ToString()
                    txtPollDate.Text = ds.Tables(0).Rows(0)("PollDT").ToString()
                    If Convert.IsDBNull(ds.Tables(0).Rows(0)("txtndate")) = False Then
                        txtTransDate.Text = Convert.ToDateTime(ds.Tables(0).Rows(0)("txtndate")).ToString("MM/dd/yyyy")
                    End If

                End If
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("StatusInfo.PopulateCount()", ex)
        End Try
    End Sub

    Private Sub PopulateStatus()
        Try
            Dim Sentrytype As String
            ds = New DataSet
            dal = New GeneralizedDAL()
            ds = dal.GetDataSet("select * from status")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    txtOwner.Text = ds.Tables(0).Rows(0)("owner").ToString()
                    If Not ds.Tables(0).Rows(0)("SYSTEMTYPE") Is DBNull.Value Then
                        'ddlSentry.SelectedValue = ds.Tables(0).Rows(0)("SYSTEMTYPE").ToString()
                        Sentrytype = ds.Tables(0).Rows(0)("SYSTEMTYPE").ToString().Trim()
                        'ddlSentry.Text = Sentrytype.Trim()
                        If Sentrytype.Equals("Sentry 5") Then
                            ddlSentry.SelectedIndex = 0
                        ElseIf Sentrytype.Equals("Sentry 6") Then
                            ddlSentry.SelectedIndex = 1
                        ElseIf Sentrytype.Equals("Sentry Gold") Then
                            ddlSentry.SelectedIndex = 2
                        End If
                    End If
                    txtSysNo.Text = ds.Tables(0).Rows(0)("sysno").ToString()

                    If Convert.IsDBNull(ds.Tables(0).Rows(0)("lastpolldt")) = False Then
                        txtPollDate.Text = Convert.ToDateTime(ds.Tables(0).Rows(0)("lastpolldt")).ToString("MM/dd/yyyy")
                    End If

                    If Convert.IsDBNull(ds.Tables(0).Rows(0)("enbr_lngth")) = False Then
                        txtEnbrLen.Text = ds.Tables(0).Rows(0)("enbr_lngth").ToString()
                    End If

                    If Convert.IsDBNull(ds.Tables(0).Rows(0)("vnbr_lngth")) = False Then
                        txtVnbrLen.Text = ds.Tables(0).Rows(0)("vnbr_lngth").ToString()
                    End If

                    If Convert.IsDBNull(ds.Tables(0).Rows(0)("PM_TXTN_CNT")) = False Then
                        txtPMCnt.Text = ds.Tables(0).Rows(0)("PM_TXTN_CNT").ToString()
                    End If

                    If Convert.IsDBNull(ds.Tables(0).Rows(0)("Costing")) = False Then
                        rdbCosting.SelectedIndex = CInt(ds.Tables(0).Rows(0)("costing"))
                    Else
                        rdbCosting.SelectedIndex = 0 'Default TXTN Costing
                    End If

                    'Added By Varun Moota to Get COMM Port
                    Dim mcName As String
                    ' mcName = System.Environment.MachineName.ToString()
                    mcName = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName 'Request.UserHostName.ToString()
                    Session("MachineName") = mcName

                    ds = dal.GetDataSet("Select COMMPORT FROM COMM WHERE MachineName = '" + mcName + "'")

                    If ds.Tables(0).Rows.Count > 0 Then
                        If Not ds.Tables(0).Rows(0)("COMMPORT") Is DBNull.Value Then txtCommPort.Text = ds.Tables(0).Rows(0)("COMMPORT").ToString()
                    Else
                        txtCommPort.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("StatusInfo.PopulateStatus", ex)
        End Try
    End Sub

    Protected Sub btnCancle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Try
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>parent.location.href='Mainmenu.aspx'</script>")
                ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>location.href('home.aspx');</script>")
                Response.Redirect("home.aspx", False)

            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("StatusInfo.btnCancle_Click", ex)
        End Try

    End Sub
    Private Sub InsertCOMM()
        Try
            Dim objSql As New GeneralizedDAL()

            Dim mcName As String
            'mcName = System.Environment.MachineName.ToString()
            mcName = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName 'Request.UserHostName.ToString()
            Session("MachineName") = mcName
            Dim param(1) As SqlParameter

            param(0) = New SqlParameter("@COMMPORT", SqlDbType.NVarChar)
            param(0).Value = Val(txtCommPort.Text)

            param(1) = New SqlParameter("@MACHINENAME", SqlDbType.NVarChar)
            param(1).Value = mcName


            Dim sqlStr As String = "Insert Into COMM([COMMPORT],[MACHINENAME])Values(@COMMPORT,@MACHINENAME)"


            Dim sqlConn As SqlConnection = objSql.GetsqlConn()
            sqlConn.Open()

            Dim sqlcmd As SqlCommand = sqlConn.CreateCommand()
            sqlcmd.CommandType = Data.CommandType.Text
            sqlcmd.CommandText = sqlStr
            sqlcmd.Parameters.AddRange(param)

            Dim results As Integer = sqlcmd.ExecuteNonQuery()

            If results > 0 Then

            Else

            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("StatusInfo.InsertCOMM", ex)
        End Try

    End Sub
    'Added By Varun Moota. 01/25/2010

    Private Sub UpdateStatusInfo()
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(9) As SqlParameter

            Dim parSysNo = New SqlParameter("@SysNo", SqlDbType.NChar, 4)
            Dim parOwner = New SqlParameter("@Owner", SqlDbType.NVarChar, 100)
            Dim parSystemType = New SqlParameter("@SystemType", SqlDbType.Char, 20)
            Dim parEnbrLen = New SqlParameter("@EnbrLen", SqlDbType.NVarChar, 1)
            Dim parVnbrLen = New SqlParameter("@VnbrLen", SqlDbType.NVarChar, 1)
            Dim ParCosting = New SqlParameter("@Costing", SqlDbType.Int, 1)
            Dim ParPMCnt = New SqlParameter("@PM_Cnt", SqlDbType.Int, 1)
            Dim parFlag = New SqlParameter("@Flag", SqlDbType.Char, 10)
            'For COMM Port #
            Dim parCommPort = New SqlParameter("@COMMPORT", SqlDbType.Int)
            Dim parMachineName = New SqlParameter("@MachineName", SqlDbType.NVarChar, 25)


            parSysNo.Direction = ParameterDirection.Input
            parOwner.Direction = ParameterDirection.Input
            parSystemType.Direction = ParameterDirection.Input
            parEnbrLen.Direction = ParameterDirection.Input
            parVnbrLen.Direction = ParameterDirection.Input
            parCommPort.Direction = ParameterDirection.Input
            parMachineName.Direction = ParameterDirection.Input
            ParCosting.Direction = ParameterDirection.Input
            ParPMCnt.Direction = ParameterDirection.Input
            parFlag.Direction = ParameterDirection.Input
            'Check Record Exists
            ds = dal.GetDataSet("Select * FROM STATUS ")

            If ds.Tables(0).Rows.Count > 0 Then
                parFlag.Value = "EDIT"
            Else
                parFlag.Value = "ADD"
            End If


            parSysNo.Value = txtSysNo.Text
            parOwner.Value = txtOwner.Text
            parSystemType.Value = ddlSentry.SelectedItem.ToString()
            parEnbrLen.Value = IIf(txtEnbrLen.Text <> "", txtEnbrLen.Text, "0")
            parVnbrLen.Value = IIf(txtVnbrLen.Text <> "", txtVnbrLen.Text, "0")
            ParPMCnt.Value = IIf(txtPMCnt.Text <> "", txtPMCnt.Text, 0)
            ParCosting.Value = IIf(rdbCosting.SelectedIndex = 3, 3, 0)

            Dim mcName As String
            ' mcName = System.Environment.MachineName.ToString()
            mcName = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName 'Request.UserHostName.ToString()
            Session("MachineName") = mcName

            ds = dal.GetDataSet("Select COMMPORT FROM COMM WHERE MachineName = '" + mcName + "'")

            If ds.Tables(0).Rows.Count > 0 Then
                parCommPort.Value = CInt(txtCommPort.Text)
                Session("COMM") = parCommPort.Value

                parMachineName.Value = Session("MachineName").ToString()
            Else
                InsertCOMM()
                If Not String.IsNullOrEmpty(CType(Session("COMM"), String)) Then
                    parCommPort.Value = Session("COMM").ToString()
                Else
                    parCommPort.Value = CInt(txtCommPort.Text)
                End If
                'parCommPort.value = CInt(txtCommPort.Text)
                parMachineName.Value = Session("MachineName").ToString()
            End If



            parcollection(0) = parSysNo
            parcollection(1) = parOwner
            parcollection(2) = parSystemType
            parcollection(3) = parEnbrLen
            parcollection(4) = parVnbrLen
            parcollection(5) = parCommPort
            parcollection(6) = parMachineName
            parcollection(7) = ParPMCnt
            parcollection(8) = ParCosting
            parcollection(9) = parFlag



            Dim blnFlag As Boolean
            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_Status_Update", parcollection)

            If blnFlag Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Status Updated Successfully');location.href('home.aspx');</script>")
                'Update CommPortValue in Sentry, if its LocalPort.01/18/2010
                Dim strLocalPortValue = CInt(txtCommPort.Text) 'Session("COMM").ToString()
                ds = dal.GetDataSet("UPDATE SENTRY SET LOCALPORT = '" + strLocalPortValue + "'")


            Else
                Dim CSM As ClientScriptManager = Page.ClientScript

                CSM.RegisterClientScriptBlock((Me.GetType), "javascript", "alert('Error!');")

            End If




        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("StatusInfo.UpdateStatusInfo", ex)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try


            'Added By Varun Moota. 02/23/2010
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "check();", True)

            End If



            ''Alert Before Updating
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>check();location.href='user.aspx';</script>")

            UpdateStatusInfo()
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("StatusInfo.btnSave_Click", ex)
        End Try
    End Sub
End Class
