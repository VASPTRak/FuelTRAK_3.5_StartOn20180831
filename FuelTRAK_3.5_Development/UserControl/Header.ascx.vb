
Partial Class UserControl_Header
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Label3.Text = ConfigurationSettings.AppSettings("VersionNumber").ToString()
        End If
    End Sub
End Class
