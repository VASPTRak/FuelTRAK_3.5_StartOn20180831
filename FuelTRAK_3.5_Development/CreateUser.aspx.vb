Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class CreateUser
    Inherits System.Web.UI.Page
    Dim EditMode As Integer
    Const EDIT = 1
    Const CREATE = 0
    Dim uInfo As UserInfo
    Dim GenFun As GeneralFunctions
    Dim ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim EditMode As Integer

        Try
            If (Not IsPostBack) Then
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.Cache.SetAllowResponseInBrowserHistory(False)
            End If

            EditMode = IIf(String.IsNullOrEmpty(Request.QueryString.Get("RecNum")), 0, 1)
            '**************** check for session is null*********************
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                txtComPort.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                ' Lname.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")

                If (Not IsPostBack) Then
                    Page.SmartNavigation = True 'this property use to avoid redrawing of page
                    dtime.Text = Now.ToLocalTime()
                    txtUName.Focus()
                    If EditMode = EDIT Then
                        Label7.Text = "Update User"
                        btnCreate.Text = "Save"
                        txtUName.ReadOnly = True
                        pullUserData(Request.QueryString.Get("RecNum").Trim)
                    End If
                End If

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("CreateUser_Page_Load", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Public Sub pullUserData(ByVal RecNo As String)
        Try
            uInfo = New UserInfo
            GenFun = New GeneralFunctions

            uInfo.EditUserID = Convert.ToInt32(RecNo)
            ds = New DataSet
            ds = GenFun.GetUserDetails(uInfo)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    txtUname.Text = ds.Tables(0).Rows(0).Item(1)
                    'Password.Text = ds.Tables(0).Rows(0).Item(2)
                    Password.Attributes.Add("value", ds.Tables(0).Rows(0).Item(2))
                    If IsDBNull(ds.Tables(0).Rows(0).Item(3)) Then Fname.Text = "" Else Fname.Text = ds.Tables(0).Rows(0).Item(3)
                    If IsDBNull(ds.Tables(0).Rows(0).Item(4)) Then Lname.Text = "" Else Lname.Text = ds.Tables(0).Rows(0).Item(4)
                    DDLstLevel.SelectedIndex = Val(ds.Tables(0).Rows(0).Item(5))
                    If IsDBNull(ds.Tables(0).Rows(0).Item(6)) Then txtComPort.Text = "" Else txtComPort.Text = ds.Tables(0).Rows(0).Item(6)
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("CreateUser_pullUserData", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
    '*************************This is use to clear field*************************
    Public Sub cleartxt()
        Try

        
            txtUName.Text = ""
            Password.Text = ""
            Fname.Text = ""
            Lname.Text = ""
            txtComPort.Text = ""
            DDLstLevel.SelectedIndex = 0

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("CreateUser.cleartxt()", ex)
        End Try
    End Sub

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            GenFun = New GeneralFunctions
            ds = New DataSet
            Dim dal = New GeneralizedDAL()
            Dim blnFlag As Boolean

            EditMode = IIf(String.IsNullOrEmpty(Request.QueryString.Get("RecNum")), 0, 1)

            ds = GenFun.CheckUserExistance(txtUname.Text.Trim())
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 And txtUname.ReadOnly = True Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('User Name already exist.')</script>")
                    Exit Sub
                End If
                Dim parcollection(6) As SqlParameter
                Dim ParUSER_ID = New SqlParameter("@USER_ID", SqlDbType.Int)
                Dim ParLogin = New SqlParameter("@Login", SqlDbType.VarChar, 10)
                Dim ParAccessCode = New SqlParameter("@AccessCode", SqlDbType.VarChar, 10)
                Dim ParFname = New SqlParameter("@Fname", SqlDbType.VarChar, 15)
                Dim ParLname = New SqlParameter("@Lname", SqlDbType.VarChar, 20)
                Dim ParLevel = New SqlParameter("@Level", SqlDbType.VarChar, 1)
                Dim ParCOMPORT = New SqlParameter("@COMPORT", SqlDbType.VarChar, 9)

                ParUSER_ID.Direction = ParameterDirection.Input
                ParLogin.Direction = ParameterDirection.Input
                ParAccessCode.Direction = ParameterDirection.Input
                ParFname.Direction = ParameterDirection.Input
                ParLname.Direction = ParameterDirection.Input
                ParLevel.Direction = ParameterDirection.Input
                ParCOMPORT.Direction = ParameterDirection.Input

                If EditMode = EDIT Then
                    ParUSER_ID.Value = Convert.ToInt32(Request.QueryString.Get("RecNum").Trim())
                Else
                    ParUSER_ID.Value = 0
                End If

                ParLogin.Value = txtUName.Text.Trim()
                ParAccessCode.Value = Password.Text.Trim()
                ParFname.Value = Fname.Text.Trim()
                ParLname.Value = Lname.Text.Trim()
                ParLevel.Value = (DDLstLevel.SelectedIndex).ToString
                ParCOMPORT.Value = txtComPort.Text.Trim()
                'Added By Varun
                Session("COMPort") = ParCOMPORT.Value

                parcollection(0) = ParUSER_ID
                parcollection(1) = ParLogin
                parcollection(2) = ParAccessCode
                parcollection(3) = ParFname
                parcollection(4) = ParLname
                parcollection(5) = ParLevel
                parcollection(6) = ParCOMPORT
                cleartxt()
                If EditMode = EDIT Then
                    blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_User_Update", parcollection)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('User Modified Successfully');location.href('user.aspx');</script>")
                    'Response.Redirect("user.aspx")

                Else
                    blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_User_Insert", parcollection)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('User Created Successfully');location.href('user.aspx');</script>")
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("CreateUser_btnCreate_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnCancle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Try
            'Response.Redirect("user.aspx", False)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>window.location.href = 'user.aspx';</script>")

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("CreateUser.btnCancle_Click", ex)
        End Try
        'Page.ClientScript.RegisterStartupScript(Me.GetType(),"javascript", "<script>parent.location.href='Mainmenu.aspx'</script>")

    End Sub
End Class
