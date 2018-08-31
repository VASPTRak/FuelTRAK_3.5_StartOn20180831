
Partial Class Diagnostic
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub rbnYes1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnYes1.CheckedChanged
        lblStatus.Text = "Manual Override"
        lblStatus1.ForeColor = Drawing.Color.White
        rbnNo1.Checked = False
    End Sub

    Protected Sub rbnYes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnYes.CheckedChanged
        lblStatus1.Text = "Manual Override"
        lblStatus1.ForeColor = Drawing.Color.White
        rbnNo.Checked = False
    End Sub

    Protected Sub rbnNo1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnNo1.CheckedChanged
        lblStatus.Text = "Working "
        rbnYes1.Checked = False
    End Sub

    Protected Sub rbnNo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnNo.CheckedChanged
        lblStatus1.Text = "Working"
        rbnYes.Checked = False
    End Sub

    Protected Sub btnDisconnect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
    End Sub
End Class
