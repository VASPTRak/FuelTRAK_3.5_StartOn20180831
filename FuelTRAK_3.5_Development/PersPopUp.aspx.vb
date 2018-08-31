
Partial Class PersPopUp
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim Script(4) As String
            Script(1) = "<script>window.opener.document.forms[0]." + Request.QueryString("txtpers").ToString() + ".value ="
            ''Script(1) = "<script>"
            Script(2) = "'Geoffy'"
            Script(3) = ";self.close()"
            Script(4) = "</script>"

            RegisterClientScriptBlock("test", Join(Script, ""))
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PersPopUp.Button1_Click", ex)
        End Try
       
    End Sub
End Class
