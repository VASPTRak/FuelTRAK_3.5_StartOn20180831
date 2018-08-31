Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.IO
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Web.Hosting


Public Class ErrorPage

    Public Function errorlog(ByVal str As String, ByVal Ex As Exception)
        Try
            Dim sw As StreamWriter
            Dim str1 As String = ""

            Dim path As String = HttpContext.Current.Server.MapPath("TrakLogError.txt")
            str1 = DateTime.Now()
            If Not File.Exists(path) Then
                File.Create(path)
            End If

            sw = New StreamWriter(path, True)
            sw.WriteLine("Date/Time----- " + str1 + "Error Message:---- " + Ex.Message.ToString() + "Error Occured in:-" + str)
            sw.Flush()
            sw.Close()
        Catch exce As Exception
        End Try
        Return ""
    End Function

    'Public Function errorlogText(ByVal str As String, ByVal strText As String)
    '    Try
    '        Dim sw As StreamWriter
    '        Dim str1 As String = ""

    '        Dim path As String = "C:\inetpub\FuelTrak Versions\FTAutoExport\TrakLogError.txt" ' HttpContext.Current.Server.MapPath("TrakLogError.txt")
    '        str1 = DateTime.Now()
    '        If Not File.Exists(path) Then
    '            File.Create(path)
    '        End If

    '        sw = New StreamWriter(path, True)
    '        sw.WriteLine("Date/Time----- " + str1 + "Event Message:---- " + strText + "Event Occured in:-" + str)
    '        sw.Flush()
    '        sw.Close()
    '    Catch exce As Exception
    '    End Try
    '    Return ""
    'End Function
End Class
