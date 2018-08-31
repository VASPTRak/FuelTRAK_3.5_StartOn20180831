Namespace OpenPopup

End Namespace
Partial Class InventoryPopup
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("InventoryPopUp.Page_Load", ex)
        End Try


    End Sub


    Public Shared Sub ShowPopup(ByVal url As String, ByVal opener As System.Web.UI.WebControls.WebControl)
        Try
            '*********************** This event use for show the popup window**********************************

            'Call the helper function to set the calender
            'OpenPopUp(opener, "Default.aspx?Checkbox=" & dateControl.ClientID, "Popup", 300, 500)
            OpenPopUp(opener, url, "Popup", 300, 500)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("InventoryPopUp.ShowPopup", ex)
        End Try
    End Sub

    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String)
        Try
            '*********************** This event use for open popup window**********************************

            Dim clientScript As String
            'Building the client script- window.open
            clientScript = "window.open('" & PagePath & "')"
            'regiter the script to the clientside click event of the 'opener' control
            opener.Attributes.Add("onClick", clientScript)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("InventoryPopUp.OpenPopUp", ex)
        End Try

    End Sub

    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String, ByVal windowName As String, ByVal width As Integer, ByVal height As Integer)
        Dim clientScript As String
        Dim windowAttribs As String
        Try
            '***************Building Client side window attributes with width and height.**********
            'Also the the window will be positioned to the middle of the screen

            windowAttribs = "width=" & width & "px," & _
                            "height=" & height & "px," & _
                            "left='+((screen.width -" & width & ") / 2)+'," & _
                            "top='+ (screen.height - " & height & ") / 2+'"


            'Building the client script- window.open, with additional parameters
            clientScript = "window.open('" & PagePath & "','" & windowName & "','" & windowAttribs & "');return false;"
            'regiter the script to the clientside click event of the 'opener' control
            opener.Attributes.Add("onClick", clientScript)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("InventoryPopUp.OpenPopUp", ex)
        End Try
    End Sub

    Protected Sub btnFuelDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFuelDel.Click

        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>OpnNewWin(1)</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("InventoryPopUp.btnFuelDel_Click", ex)
        End Try
    End Sub

    Protected Sub btnTankSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTankSet.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>OpnNewWin(2)</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("InventoryPopUp.btnTankSet_Click", ex)
        End Try

    End Sub

    Protected Sub btnTankDip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTankDip.Click

        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>OpnNewWin(3)</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("InventoryPopUp.btnTankDip_Click", ex)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Try
            Response.Redirect("Home.aspx", False)
            ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>OpnNewWin(4)</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("InventoryPopUp.btnCancel_Click", ex)
        End Try

    End Sub
End Class
