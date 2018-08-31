Imports System.Data
Imports System.Object
Imports System.MarshalByRefObject
Imports System.Net.WebRequest
Imports System.Net.HttpWebRequest
Imports System.IO
Imports System.DirectoryServices

Partial Class LoginPage
    Inherits System.Web.UI.Page

    Dim Uinfo As UserInfo
    Dim GenFun As GeneralFunctions
    Dim ds As DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>history.forward();</script>")
                '    Page.SmartNavigation = True
                '    lblVersion.Text = ConfigurationSettings.AppSettings("VersionNumber").ToString()
                Session.RemoveAll() 'to kill current session
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("LoginPage.Page_Load", ex)
        End Try

    End Sub
    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            Dim username As String = txtUserName.Text.ToString()
            Dim password As String = txtPassword.Text.ToString()
            Dim domain As String = txtDomainName.Text.ToString()
            If domain = String.Empty Then
                Uinfo = New UserInfo()
                GenFun = New GeneralFunctions
                Uinfo.Username = txtUserName.Text.ToString().Trim()
                Uinfo.Password = txtPassword.Text.ToString().Trim()
                ds = New DataSet
                ds = GenFun.AuthenticateAdmin(Uinfo)
                If ds.Tables(0).Rows.Count > 0 Then
                    Session("User_name") = txtUserName.Text.Trim()
                    Session("COMPort") = ds.Tables(0).Rows(0)("COMPort").ToString().Trim()
                    Session("User_Level") = ds.Tables(0).Rows(0)("Level").ToString().Trim()
                    Response.Redirect("Mainmenu.aspx", False)
                Else
                    lblerror.Text = "Incorrect User Name And Password Try again.."
                    txtUserName.Text = ""
                    txtUserName.Focus()
                End If
            End If
            If Not domain = String.Empty Then
                Dim result As Boolean = Authenticate(username, password, domain)
                If (result) Then
                    'Checking User level in FuelTrak
                    Uinfo = New UserInfo()
                    GenFun = New GeneralFunctions
                    Uinfo.Username = txtUserName.Text.ToString().Trim()
                    Uinfo.Password = txtPassword.Text.ToString().Trim()
                    ds = New DataSet
                    ds = GenFun.AuthenticateUser(Uinfo)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Session("User_name") = txtUserName.Text.Trim()
                        Session("COMPort") = ds.Tables(0).Rows(0)("COMPort").ToString().Trim()
                        Session("User_Level") = ds.Tables(0).Rows(0)("Level").ToString().Trim()
                        Response.Redirect("Mainmenu.aspx", False)
                    Else
                        lblerror.Text = "Incorrect User Name And Password Try again.."
                        txtUserName.Text = ""
                        txtUserName.Focus()
                    End If
                Else
                    lblerror.Text = "Incorrect User Name And Password Try again.."
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("LoginPage.btnSubmit_Click", ex)
            lblerror.Text = "Invalid Credentials"
        End Try
        'Try
        '    Uinfo = New UserInfo()
        '    GenFun = New GeneralFunctions

        '    Uinfo.Username = txtUname.Text.Trim()
        '    Uinfo.Password = txtPwd.Text.Trim()
        '    ds = New DataSet
        '    ds = GenFun.AuthenticateUser(Uinfo)
        '    If ds.Tables(0).Rows.Count > 0 Then
        '        Session("User_name") = txtUname.Text.Trim()
        '        Session("COMPort") = ds.Tables(0).Rows(0)("COMPort").ToString().Trim()
        '        Session("User_Level") = ds.Tables(0).Rows(0)("Level").ToString().Trim()
        '        Response.Redirect("Mainmenu.aspx", False)
        '        'Response.Redirect("TestMenu.aspx", False)
        '    Else
        '        lblerror.Text = "Incorrect User Name And Password Try again.."
        '        txtUname.Text = ""
        '        txtUname.Focus()
        '    End If
        'Catch ex As Exception
        '    Dim cr As New ErrorPage
        '    Dim errmsg As String
        '    cr.errorlog("btnLogin_Click", ex)
        '    If ex.Message.Contains(";") Then
        '        errmsg = ex.Message.ToString()
        '        errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
        '    Else
        '        errmsg = ex.Message.ToString()
        '    End If
        'End Try

    End Sub
    Public Function Authenticate(ByVal userName As String, ByVal password As String, ByVal domain As String) As Boolean
        Dim authentic As Boolean = False
        Try
            Dim entry As New DirectoryEntry("LDAP://" & domain, userName, password)
            entry.RefreshCache()
            Try
                Dim nativeObject As Object = entry.NativeObject
                'entry()
                authentic = True
            Catch
                authentic = False

            End Try
        Catch generatedExceptionName As DirectoryServicesCOMException
            authentic = False
        End Try
        Return authentic
    End Function
End Class
