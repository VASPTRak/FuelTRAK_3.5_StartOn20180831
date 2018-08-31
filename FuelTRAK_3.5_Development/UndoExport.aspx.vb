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

Partial Class UndoExport
    Inherits System.Web.UI.Page
    Dim str_Connection_string As String = IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                If Not IsPostBack Then
                    Dim no As String
                    Dim expdate As String
                    no = Request.QueryString("RecordCount").ToString()
                    expdate = Format(Convert.ToDateTime(Request.QueryString("ExportDate")), "MM/dd/yyyy HH:mm")
                    lblResult.Text = "Last export run: " & expdate & " , " & no & " transactions"
                End If
                Session("visited") = False
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable_New_Edit.Page_Load", ex)
        End Try
    End Sub

    Protected Sub btnUndoExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndoExport.Click
        Dim sqlConn As SqlConnection = New SqlConnection(str_Connection_string)
        Try
            Dim strQuery As String
            strQuery = ""
            sqlConn.Open()
            Dim dt As Date = Convert.ToDateTime(Request.QueryString("ExportDate"))
            If Session("ExportNum") = "" Then Session("ExportNum") = "1"

            Dim sqlCmd As SqlCommand = New SqlCommand() '(strQuery, sqlConn)

            sqlCmd = New SqlCommand()
            sqlCmd.Connection = sqlConn
            sqlCmd.CommandText = "usp_tt_Export_UndoTXTN"
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.Parameters.AddWithValue("@Dt", dt)
            If (Session("ExportNum") = "1") Then
                sqlCmd.Parameters.AddWithValue("@Export", "1")
            Else
                sqlCmd.Parameters.AddWithValue("@Export", "2")
            End If
            Dim no As Integer
            no = sqlCmd.ExecuteNonQuery()
            sqlCmd.Dispose()

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("UndoExport_Click", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        Finally
            If sqlConn.State = ConnectionState.Open Then sqlConn.Close()
        End Try
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>self.close();</script>")
    End Sub
End Class
