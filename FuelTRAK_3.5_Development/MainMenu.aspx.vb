Imports System.Web.UI.HtmlControls
Imports System.Xml

Partial Class MainMenu
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Session("User_name") Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'check for session is null
        If Session("User_name") Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
        Else
            Try
                Session.Add("visited", False)
                Session.Add("Product", String.Empty)
                Session.Add("currentrecord", "0")

                Session("Product") = ""
                Session("visited") = False
                Session("currentrecord") = "0"
            Catch ex As Exception
                Dim cr As New ErrorPage
                cr.errorlog("MainMenu_Page_Load", ex)
            End Try
            IFRAME1.Attributes.Add("height", "650px")
            IFRAME1.Attributes.Add("Width", "90%")

        End If
    End Sub

    Public Sub ReDim1(ByVal arr As String(), ByVal length As Integer)
        Try
            Dim arrTemp(length) As String
            If (length > arr.Length) Then
                Array.Copy(arr, 0, arrTemp, 0, arr.Length)
                arr = arrTemp
            Else
                Array.Copy(arr, 0, arrTemp, 0, length)
                arr = arrTemp
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MainMenu.ReDim1", ex)
        End Try
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If Session("User_name") Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
        End If
    End Sub

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        If Session("User_name") Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
        End If
    End Sub
End Class
