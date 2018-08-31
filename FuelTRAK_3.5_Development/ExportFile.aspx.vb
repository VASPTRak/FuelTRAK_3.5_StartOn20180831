Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.IO
Partial Class ExportFile
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'By Soham Gangavane July 18, 2017
            Dim path As String
            Dim PortTerminal As String
            If Request.QueryString("PortTerminal") IsNot Nothing Then
                PortTerminal = Request.QueryString("PortTerminal").ToString()
            Else
                PortTerminal = ""
            End If
            Dim filepath As String = Request.QueryString("file").ToString()

            If PortTerminal.StartsWith("Fuel") Then
                path = HttpContext.Current.Server.MapPath("WexImport\" & PortTerminal)
            Else
                'path = Session("FileLocationExport").ToString + Session("FileNameExport").ToString + "." + Session("FileTypeExport").ToString
                path = HttpContext.Current.Server.MapPath("ExportFiles\" & filepath)
            End If

            Dim fileinfo As FileInfo = New FileInfo(path)

            If fileinfo.Exists Then

                Response.Clear()
                Response.BufferOutput = True
                Response.Buffer = True

                Response.AddHeader("Content-Disposition", "attachment; filename=" & fileinfo.Name)
                Response.AddHeader("Content-Length", fileinfo.Length.ToString())

                'Setting content type according to file extension
                'Harshada Mutalik
                '12 Nov 07

                'If (fileinfo.Extension() = ".txt") Then
                '    Response.ContentType = "text/plain"
                'ElseIf (fileinfo.Extension() = ".doc") Then
                '    Response.ContentType = "application/msword"
                'ElseIf (fileinfo.Extension = ".xls") Or (fileinfo.Extension = ".csv") Then
                '    Response.ContentType = "application/vnd.ms-excel"
                'ElseIf (fileinfo.Extension = ".pdf") Then
                '    Response.ContentType = "application/pdf"
                'ElseIf (fileinfo.Extension = ".ppt") Then
                '    Response.ContentType = "application/vnd.ms-powerpoint"
                'ElseIf (fileinfo.Extension = "") Then
                '    'Response.ContentType = "application/octet-stream"
                '    Response.ContentType = "application/unknown"
                'End If


                'Try
                '    If Session("FileTypeExport").ToString = "txt" Then
                '        Response.ContentType = "text/plain"
                '    ElseIf Session("FileTypeExport").ToString = "doc" Then
                '        Response.ContentType = "application/msword"
                '    ElseIf (Session("FileTypeExport").ToString = "xls") Or (Session("FileTypeExport").ToString = "csv") Then
                '        Response.ContentType = "application/vnd.ms-excel"
                '    ElseIf Session("FileTypeExport").ToString = "pdf" Then
                '        Response.ContentType = "application/pdf"
                '    ElseIf Session("FileTypeExport").ToString = "ppt" Then
                '        Response.ContentType = "application/vnd.ms-powerpoint"
                '    ElseIf Session("FileTypeExport").ToString = "" Then
                '        'Response.ContentType = "application/octet-stream"
                '        Response.ContentType = "application/unknown"
                '    End If
                'Catch ex As Exception
                '    Response.ContentType = "application/unknown"
                'End Try
                'Response.WriteFile(fileinfo.FullName)

                'Dim FileLocationExport() As Char = Session("FileLocationExport").ToString.ToCharArray
                'If Not FileLocationExport(FileLocationExport.Length - 1) = "\" Then
                '    Session("FileLocationExport") = Session("FileLocationExport") + "\"
                'End If


                'Dim filePathToUse = Session("FileLocationExport").ToString + Session("FileNameExport").ToString + "." + Session("FileTypeExport").ToString
                'Response.TransmitFile(fileinfo.FullName)

                If Not System.IO.File.Exists(fileinfo.FullName) Then
                    System.IO.File.Create(fileinfo.FullName)
                    Response.TransmitFile(fileinfo.FullName)
                Else
                    Response.WriteFile(fileinfo.FullName)
                    'Response.TransmitFile(fileinfo.FullName)
                End If


                'Response.TransmitFile()
                Response.Flush()
                Response.Clear()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ExportFile.Page_Load", ex)
        End Try
    End Sub
End Class
