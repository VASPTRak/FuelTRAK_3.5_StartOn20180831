Partial Class LogOut
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Session.Abandon()
            'FormsAuthentication.SignOut()
            'Response.ExpiresAbsolute = DateTime.Now.Subtract(New TimeSpan(1, 0, 0, 0))
            'Response.Expires = 0
            'Response.CacheControl = "no-cache"
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.Cache.SetNoStore()
            Session.RemoveAll() 'to kill current session
            Response.Redirect("Loginpage.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("LogOut.Page_Load", ex)
        End Try

    End Sub
End Class
