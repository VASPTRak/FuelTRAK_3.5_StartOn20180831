Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class PollingQueue_New_Edit
    Inherits System.Web.UI.Page
    Dim ds, dsdata As DataSet
    Dim i As Integer
    Dim dal As New GeneralizedDAL()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        Try
            If Session("User_name") Is Nothing Then 'check for session is null/not
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            Else
                If Not Page.IsPostBack Then
                    If Request.QueryString.Count > 0 And Request.QueryString.Count = 1 Then
                        If Request.QueryString(0).ToString().Trim() = "S" Then
                            'Fill DeviceId (ddlDeviceId)
                            ddlDeviceId.Items.Clear()
                            FillDeviceID("Sentry")
                            
                            Label1.Text = "New Sentry Polling Queue Information"
                            lblDeviceType.Text = "Sentry"
                        ElseIf Request.QueryString(0).ToString().Trim() = "TM" Then
                            ddlDeviceId.Items.Clear()
                            FillDeviceId("TM")
                            Label1.Text = "New Tank Monitor Polling Queue Information"
                            lblDeviceType.Text = "Tank Monitor"
                        End If
                    ElseIf Request.QueryString.Count > 1 Then
                        ddlDeviceId.Items.Clear()
                        FillDeviceId(Request.QueryString("DeviceType").ToString())
                        If Request.QueryString("DeviceType").ToString() = "Tank Monitor" Then
                            Label1.Text = "Edit Tank Monitor Polling Queue Information"
                            lblDeviceType.Text = "Tank Monitor"
                        Else
                            Label1.Text = "Edit Sentry Polling Queue Information"
                            lblDeviceType.Text = Request.QueryString("DeviceType").ToString()
                        End If
                    End If


                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueue_New_Edit.Page_Load", ex)
        End Try
    End Sub
    Private Sub FillDeviceId(ByVal DeviceType As String)
        Try

      
            ds = New DataSet()
            If DeviceType = "Sentry" Then
                ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PollQueuePopulateSentryList")
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        ddlDeviceId.DataSource = ds.Tables(0)
                        ddlDeviceId.DataTextField = "SENTRYNoName"
                        ddlDeviceId.DataValueField = "NUMBER"
                        ddlDeviceId.DataBind()
                    End If

                End If
            ElseIf DeviceType = "Tank Monitor" Then
                ds = New DataSet()
                ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PollQueuePopulateTMList")
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        ddlDeviceId.DataSource = ds.Tables(0)
                        ddlDeviceId.DataTextField = "TMNoName"
                        ddlDeviceId.DataValueField = "NUMBER"
                        ddlDeviceId.DataBind()
                    End If
                End If
            End If
            If Not Request.QueryString("DeviceId") Is Nothing Then
                ddlDeviceId.SelectedValue = Request.QueryString("DeviceId")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueue_New_Edit.FillDeviceId", ex)
        End Try
    End Sub
    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            SaveRecords()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueue_New_Edit.btnOK_Click", ex)
        End Try

    End Sub
    Public Sub SaveRecords()
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            Dim parcollection(5) As SqlParameter
            Dim ParID = New SqlParameter("@ID", SqlDbType.VarChar, 50)
            Dim ParDeviceID = New SqlParameter("@DeviceID", SqlDbType.VarChar, 3)
            Dim ParDeviceType = New SqlParameter("@DeviceType", SqlDbType.VarChar, 10)
            Dim ParCommand = New SqlParameter("@Command", SqlDbType.VarChar, 10)
            Dim ParTimeQueued = New SqlParameter("@TimeQueued", SqlDbType.DateTime)
            Dim ParAddEdit = New SqlParameter("@AddEdit", SqlDbType.VarChar, 5)

            ParDeviceID.Direction = ParameterDirection.Input
            ParDeviceType.Direction = ParameterDirection.Input
            ParCommand.Direction = ParameterDirection.Input
            ParTimeQueued.Direction = ParameterDirection.Input
            ParAddEdit.Direction = ParameterDirection.Input

            If (Request.QueryString.Count = 3) Then
                ParAddEdit.Value = "Edit"
                ParID.Value = Request.QueryString("Record").ToString()
            Else
                ParAddEdit.Value = "ADD"
                ParID.Value = 0
            End If


            ParDeviceID.Value = ddlDeviceId.SelectedValue.ToString()
            If lblDeviceType.Text = "Tank Monitor" Then
                ParDeviceType.Value = "TM"
            Else
                ParDeviceType.Value = lblDeviceType.Text
            End If

            ParCommand.Value = ddlCommand.SelectedValue.ToString()
            ParTimeQueued.Value = DateTime.Now.Date.ToString("MM/dd/yy " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString())
            
            parcollection(0) = ParID
            parcollection(1) = ParDeviceID
            parcollection(2) = ParDeviceType
            parcollection(3) = ParCommand
            parcollection(4) = ParTimeQueued
            parcollection(5) = ParAddEdit

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_PollingQueueUpdateInsert", parcollection)
            If blnFlag = True Then
                If (Request.QueryString.Count = 3) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record updated successfully.');location.href='PollingQueueSearch.aspx';</script>")
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record saved successfully.');location.href='PollingQueueSearch.aspx';</script>")
                End If

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("PollingQueue_new_edit.SaveRecords", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("PollingQueueSearch.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueue_new_edit.btnCancel_Click", ex)
        End Try

    End Sub
End Class
