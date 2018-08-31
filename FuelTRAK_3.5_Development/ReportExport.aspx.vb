Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Reporting.WebControls
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.IO

Partial Class Test
    Inherits System.Web.UI.Page
    Dim oRpt1 As ReportDocument
    Dim oRpt2 As ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
                Exit Sub
            End If
            If Not (IsPostBack) Then
                With DDLExportType.Items
                    .Clear()
                    .Add("Rich Text (RTF)")
                    .Add("Portable Document (PDF)")
                    .Add("MS Word (DOC)")
                    .Add("MS Excel (XLS)")
                End With
            End If
            Page.Title = "Export Report"
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ReportExport.PageLoad", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    'Private Sub ExportReport(ByVal str As String)
    '    If Session("flg") = "True" Then
    '        Session.Remove("flg")
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(),"javascript", "<script>alert('Report exported successfully.');self.close();</script>")
    '    ElseIf btnExport.Text = "Ok" Then
    '        If Not Session("Rpt") Is Nothing Then
    '            Dim oStream As New MemoryStream ' // using System.IO
    '            oRpt1 = CType(Session("Rpt"), ReportDocument)
    '            Session("flg") = "True"
    '            'oRpt2 = oRpt1
    '            Select Case str 'this contains the value of the selected export format.
    '                Case "Rich Text (RTF)"
    '                    '--------------------------------------------------------------------
    '                    oStream = oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows)
    '                    Response.Clear()
    '                    Response.Buffer = True
    '                    Response.ContentType = "application/rtf"
    '                    Response.Charset = "Windows-1252" ' Set appropriate to codepage
    '                    Response.AddHeader("content-disposition", "attachment;filename=report.rtf")
    '                    '--------------------------------------------------------------------
    '                Case "Portable Document (PDF)"
    '                    oStream = oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
    '                    Response.Clear()
    '                    Response.Buffer = True
    '                    Response.ContentType = "application/pdf"
    '                    Response.AddHeader("Content-Disposition", "attachment;filename=Report.pdf")
    '                    '--------------------------------------------------------------------
    '                Case "MS Word (DOC)"
    '                    oStream = oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows)
    '                    Response.Clear()
    '                    Response.Buffer = True
    '                    Response.ContentType = "application/msword"
    '                    Response.AddHeader("content-disposition", "attachment;filename=Report.doc")
    '                    '--------------------------------------------------------------------
    '                Case "MS Excel (XLS)"
    '                    oStream = oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel)
    '                    Response.Clear()
    '                    Response.Buffer = True
    '                    Response.ContentType = "application/vnd.ms-excel"
    '                    Response.AddHeader("content-disposition", "attachment;filename=Report.xls")
    '                    '--------------------------------------------------------------------
    '            End Select 'export format
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(),"javascript", "<script>self.close();</script>")
    '            Response.BinaryWrite(oStream.ToArray())
    '            oRpt1.Close()
    '            btnExport.Text = "Close"
    '            Response.End()

    '        End If
    '    End If
    'End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        ''ExportReport(DDLExportType.SelectedItem.Text)
        'CloseWindow()
        Try
            btnExport.Visible = False
            btnClose.Visible = True
            Server.Transfer("Export.aspx?abc=" + DDLExportType.SelectedItem.Text, False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportExport.btnExport_Click", ex)
        End Try
        
    End Sub

    Private Sub CloseWindow()
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>self.close();</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportExport.CloseWindow()", ex)
        End Try

    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            CloseWindow()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportExport.btnClose_Click", ex)
        End Try

    End Sub

    Protected Sub Button1_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.ServerClick
        Try

        Catch ex As Exception

        End Try

    End Sub
End Class
