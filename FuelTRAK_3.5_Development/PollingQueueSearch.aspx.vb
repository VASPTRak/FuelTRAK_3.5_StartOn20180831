Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class PollingQueueSearch
    Inherits System.Web.UI.Page
    Dim GenFun As New GeneralFunctions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

       
            '**************** Check for session is null/not*********************
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else

                Session("visited") = False
                'Commented By Varun Moota to Show Controls. 12/17/2009
                ' ''If (Not IsPostBack) Then
                ' ''    ShowPopup(btnNew)
                ' ''    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(1);parent.scrollTo(0,0);</script>")
                ' ''    Session("HideControls2") = True
                ' ''End If
                'Added By Varun Moota to Show Search Fields 12/03/2009
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")

                If (Hidtxt.Value = "true") Then
                    If txtVehId.Value <> "" Then
                        deleteRecord(txtVehId.Value)
                        Hidtxt.Value = ""
                        txtVehId.Value = ""
                    End If
                ElseIf (Hidtxt.Value = "false") Then
                    Hidtxt.Value = ""
                    'SearchClick()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueueSearch.Page_Load", ex)
        End Try
    End Sub

    Public Shared Sub ShowPopup(ByVal opener As System.Web.UI.WebControls.WebControl)
        Try
            '*********************Call the helper function to set the calender***********************
            OpenPopUp(opener, "PollingPopup.aspx", "Popup", 300, 200)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueueSearch.ShowPopup", ex)
        End Try
       
    End Sub

    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String)
        Dim clientScript As String
        Try
            '***********************Building the client script- window.open****************
            clientScript = "window.open('" & PagePath & "')"
            'regiter the script to the clientside click event of the 'opener' control
            opener.Attributes.Add("onClick", clientScript)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueueSearch.OpenPopUp", ex)
        End Try
    End Sub

    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String, ByVal windowName As String, ByVal width As Integer, ByVal height As Integer)
        Dim clientScript As String
        Dim windowAttribs As String
        Try

      
            'Building Client side window attributes with width and height.
            'Also the the window will be positioned to the middle of the screen
            windowAttribs = "width=" & width & "px," & _
                            "height=" & height & "px," & _
                            "titlebar=no," & _
                            "left='+((screen.width -" & width & ") / 2)+'," & _
                            "top='+ (screen.height - " & height & ") / 2+'"
            'Building the client script- window.open, with additional parameters
            clientScript = "window.open('" & PagePath & "','" & windowName & "','" & windowAttribs & "');return false;"
            'regiter the script to the clientside click event of the 'opener' control
            opener.Attributes.Add("onClick", clientScript)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueueSearch.OpenPopUp", ex)
        End Try
    End Sub

    Public Sub deleteRecord(ByVal RECORDID As String)
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParRECORDID = New SqlParameter("@RECORD", SqlDbType.VarChar, 50)
            ParRECORDID.Direction = ParameterDirection.Input
            ParRECORDID.Value = RECORDID
            parcollection(0) = ParRECORDID
            Dim blnflag As Boolean
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_PollingQueueDelete", parcollection)

            If blnflag = True Then
                SearchClick()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Polling Queue deleted sucessfully.')</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("PollingQueueSearch_deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            SearchClick()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueueSearch.btnSearch_Click", ex)
        End Try
    End Sub

    Public Sub SearchClick()
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(4)</script>")

            Dim parcollection(0) As SqlParameter

            Dim ParDeviceType = New SqlParameter("@DeviceType", SqlDbType.VarChar, 1)

            ParDeviceType.Direction = ParameterDirection.Input

            ParDeviceType.Value = DDLstType.SelectedItem.Value
            parcollection(0) = ParDeviceType

            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("use_tt_GetPollingQueueSearch", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    grvPollingQueue.DataSource = ds.Tables(0)
                    grvPollingQueue.DataBind()
                Else
                    grvPollingQueue.DataSource = ds.Tables(0)
                    grvPollingQueue.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("PollingQueueSearch.btnSearch_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub DDLstType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLstType.SelectedIndexChanged
        Try
            txtType.Text = DDLstType.SelectedItem.Value
            txtType.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueueSearch.DDLstType_SelectedIndexChanged", ex)
        End Try
        
    End Sub

    Protected Sub grvPollingQueue_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GRVPollingQueue.RowEditing
        Dim Tanktype As String = String.Empty
        Dim TankID As String = String.Empty
        Try
            Tanktype = GRVPollingQueue.Rows(e.NewEditIndex).Cells(2).Text
            TankID = GRVPollingQueue.Rows(e.NewEditIndex).Cells(3).Text
            Response.Redirect("~/PollingQueue_New_Edit.aspx?DeviceType=" + Tanktype + "&DeviceId=" + TankID + "&Record=" + GRVPollingQueue.DataKeys(e.NewEditIndex).Value.ToString(), False)

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueueSearch.grvPollingQueue_RowEditing", ex)
        End Try
    End Sub

    Protected Sub grvPollingQueue_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GRVPollingQueue.RowDeleting
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>check('" + GRVPollingQueue.DataKeys(e.RowIndex).Value.ToString() + "')</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueueSearch.grvPollingQueue_RowDeleting", ex)
        End Try

    End Sub

    Protected Sub grvPollingQueue_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GRVPollingQueue.PageIndexChanging
        Try
            GRVPollingQueue.PageIndex = e.NewPageIndex
            SearchClick()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingQueueSearch.grvPollingQueue_RowDeleting", ex)
        End Try
    End Sub
End Class
