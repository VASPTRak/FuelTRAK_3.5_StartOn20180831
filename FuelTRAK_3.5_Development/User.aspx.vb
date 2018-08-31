Imports System.Data.SqlClient
Imports System.Data
Partial Class User
    Inherits System.Web.UI.Page
    Dim GenFun As GeneralFunctions
    Dim uInfo As UserInfo

    Public Sub GetUser_List()
        Try
            GenFun = New GeneralFunctions
            Dim adp As SqlDataAdapter
            Dim ods = New DataSet()
            adp = GenFun.GetUserList()
            adp.Fill(ods)
            Griduser1.DataSource = ods.Tables(0)
            Griduser1.DataBind()
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("BindList", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub


    Public Sub deleteRecord(ByVal UseID As Integer)
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParUseID = New SqlParameter("@UseID", SqlDbType.Int)
            ParUseID.Direction = ParameterDirection.Input
            ParUseID.Value = UseID
            parcollection(0) = ParUseID
            Dim blnflag As Boolean
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_UserDelete", parcollection)
            If blnflag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('User deleted sucessfully.')</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("UserList_deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Hidtxt.Value = "true") Then
                If Session("UseID").ToString() <> "" Then
                    deleteRecord(Convert.ToInt32(Session("UseID")))
                    Session.Remove("UseID")
                    Hidtxt.Value = ""
                    GetUser_List()
                End If
            End If
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                If (Not IsPostBack) Then
                    GetUser_List()
                    btnNew.Visible = True
                    btnsearch.Visible = True
                End If
            End If

            '' ''Added By Varun to Show Search Fields 12/03/2009
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")
            TduserList.Visible = True
            btnNew.Visible = True
            btnsearch.Visible = False
            

            '' ''Added By Varun Moota to Show USer's Screen With DataGrid When it Loads. 12/17/2009
            ' ''TduserList.Visible = True
            ' ''btnNew.Visible = False
            ' ''btnsearch.Visible = False



        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("User.PageLoad", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub Griduser1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles Griduser1.RowEditing

        Try
            Response.Redirect("CreateUser.aspx?RecNum=" & Griduser1.DataKeys(e.NewEditIndex).Value)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("User.Griduser1_RowEditing", ex)
        End Try
    End Sub

    Protected Sub Griduser1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles Griduser1.RowDeleting
        
        Try
            Session("UseID") = Griduser1.DataKeys(e.RowIndex).Value
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>check();</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("User.Griduser1_RowDeleting", ex)
        End Try
    End Sub

    Protected Sub btnsearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        
        Try
            TduserList.Visible = True
            btnNew.Visible = False
            btnsearch.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("User.btnsearch_Click", ex)
        End Try
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            Response.Redirect("createuser.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("User.btnNew_Click", ex)
        End Try

    End Sub

    Protected Sub Griduser1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Griduser1.PageIndexChanging
        Try
            Griduser1.PageIndex = e.NewPageIndex
            GetUser_List()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("User.btnNew_Click", ex)
        End Try
    End Sub

    Protected Sub Griduser1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Griduser1.RowDataBound
        Try
            If Not e.Row.RowIndex = -1 Then
                If e.Row.Cells(5).Text = "1" Then
                    e.Row.Cells(5).Text = LevelString("1")
                ElseIf e.Row.Cells(5).Text = "2" Then
                    e.Row.Cells(5).Text = LevelString("2")
                ElseIf e.Row.Cells(5).Text = "3" Then
                    e.Row.Cells(5).Text = LevelString("3")
                ElseIf e.Row.Cells(5).Text = "4" Then
                    e.Row.Cells(5).Text = LevelString("4")
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("User.Griduser1_RowDataBound", ex)
        End Try
       
    End Sub
    Function LevelString(ByVal level As String) As String
        Try

  
            Select Case Val(level)
                Case 1
                    Return "Administrator"
                Case 2
                    Return "User"
                Case 3
                    Return "Reports & Inventory"
                Case 4
                    Return "Reports Only"
                Case Else
                    Return "UNKNOWN!"
            End Select

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("User.LevelString", ex)
            Return "UNKNOWN!"
        End Try
    End Function
End Class
