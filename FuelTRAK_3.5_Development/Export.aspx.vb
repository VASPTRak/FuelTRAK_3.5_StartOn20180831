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

Partial Class Export
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
                Exit Sub
            End If
            If Request.QueryString.Count > 0 Then
                If Not (IsPostBack) Then
                    ExportReport(Request.QueryString.Get(0).ToString())
                    'Export(Request.QueryString.Get(0).ToString())
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Export_PageLoad", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Sub ExportReport(ByVal str As String)

        Try

            If Not Session("Rpt") Is Nothing Then
                Dim oStream As New MemoryStream ' // using System.IO

                Dim oRpt1 As ReportDocument

                oRpt1 = CType(Session("Rpt"), ReportDocument)

                Select Case str 'this contains the value of the selected export format.

                    Case "Rich Text (RTF)"
                        '--------------------------------------------------------------------
                        'oStream = oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows)
                        oStream = CType(oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows), MemoryStream)
                        Response.Clear()
                        Response.Buffer = True
                        Response.ContentType = "application/rtf"
                        Response.Charset = "Windows-1252" ' Set appropriate to codepage
                        Response.AddHeader("content-disposition", "attachment;filename=report.rtf")
                        '--------------------------------------------------------------------
                    Case "Portable Document (PDF)"
                        oStream = CType(oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), MemoryStream)
                        'oStream = oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
                        Response.Clear()
                        Response.Buffer = True
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("Content-Disposition", "attachment;filename=Report.pdf")
                        '--------------------------------------------------------------------
                    Case "MS Word (DOC)"
                        'oRpt1.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")

                        oStream = CType(oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows), MemoryStream)
                        'oStream = oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows)
                        Response.Clear()
                        Response.Buffer = True
                        Response.ContentType = "application/msword"
                        Response.AddHeader("content-disposition", "attachment;filename=Report.doc")
                        '--------------------------------------------------------------------
                    Case "MS Excel (XLS)"
                        'oStream = oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel)
                        oStream = CType(oRpt1.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel), MemoryStream)
                        Response.Clear()
                        Response.Buffer = True
                        Response.ContentType = "application/vnd.ms-excel"
                        Response.AddHeader("content-disposition", "attachment;filename=Report.xls")
                        '--------------------------------------------------------------------
                End Select 'export format
                Response.BinaryWrite(oStream.ToArray())
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Javascript", "<script> CloseParent();</script>")
                oRpt1.Close()
                Response.End()

            End If
        Catch ex As Exception

            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Export.ExportReport()", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try
    End Sub

    Private Sub Export(ByVal str As String)
        ' Export report to the selected format
        Try
            Dim oRpt1 As ReportDocument

            oRpt1 = CType(Session("Rpt"), ReportDocument)

            If str = "Adobe (PDF)" Then

                oRpt1.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")

            ElseIf str = "MS Word (DOC)" Then

                oRpt1.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, True, "ExportedReport")

            ElseIf str = "MS Excel 97 - 2000" Then

                oRpt1.ExportToHttpResponse(ExportFormatType.Excel, Response, True, "ExportedReport")

            ElseIf str = "MS Excel 97 - 2000 (Data Only)" Then

                oRpt1.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "ExportedReport")

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Export.Export()", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
End Class
