Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.IO

Partial Class ExportSummary
    Inherits System.Web.UI.Page

    Dim Uinfo As UserInfo
    Dim GenFun As GeneralFunctions
    Dim DAL As GeneralizedDAL

    Protected Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Try
            'Dim filepath As String = Session("FileLocationExport").ToString + Session("FileNameExport").ToString + "." + Session("FileTypeExport").ToString
            'Dim f As FileInfo = New FileInfo(filepath)
            Dim f As FileInfo = New FileInfo(HttpContext.Current.Server.MapPath("ExportFiles\Data_" + Session("User_name")))
            Response.Redirect("ExportFile.aspx?file=" & f.Name & "&PortTerminal=XYZ", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ExportSummary.btnDownload_Click", ex)
        End Try
      
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("User_name") Is Nothing Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
                Else
                    lblRecCnt.Text = Session("RecordCount").ToString()
                    'lblLastDate.Text = Format(Convert.ToDateTime(Session("LastDate")), "MM/dd/yyyy HH:mm")
                    'lblCurrentDate.Text = Format(Convert.ToDateTime(Session("ExportDate")), "MM/dd/yyyy HH:mm")
                    lblLastDate.Text = Format(Convert.ToDateTime(Session("StartDT")), "MM/dd/yyyy HH:mm")
                    lblCurrentDate.Text = Format(Convert.ToDateTime(Session("EndDT")), "MM/dd/yyyy HH:mm")

                    If Session("ExportNum") = "1" Then
                        lblExportFormat.Text = "Export 1"
                    ElseIf Session("ExportNum") = "2" Then
                        lblExportFormat.Text = "Export 2"
                    End If
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportSummary_Page_Load", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Protected Sub btnPringSummary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPringSummary.Click
        Try
            GenFun = New GeneralFunctions
            Uinfo = New UserInfo
            Uinfo.ReportID = 1
            Uinfo.ReportHeader = GenFun.Get_Company_Name
            Uinfo.ReportTitle = "Export Summary Report"
            Uinfo.StartDate = Session("StartDT")
            Uinfo.EndDate = Session("EndDT") '
            Session("ReportInputs") = Session("StrCondition")
            Session("Uinfo") = Uinfo
            Dim url As String = "ReportViewer.aspx"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Run_Report('" & url & "');</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ExportSummary.btnPringSummary_Click", ex)
        End Try
    End Sub

   
End Class
