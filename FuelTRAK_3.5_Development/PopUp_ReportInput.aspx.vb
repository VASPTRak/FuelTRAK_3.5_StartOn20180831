Imports System.Data
Imports System.Data.SqlClient

Partial Class PopUp_ReportInput
    Inherits System.Web.UI.Page

    Dim str_Connection_string As String = IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString())

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strQuery As String = ""
        Try
            If Request.QueryString("PopType") = "Tank" Then
                strQuery = "SELECT NUMBER AS [Tank Number], NAME AS [Tank Name] FROM TANK ORDER BY NUMBER"
                Page.Title = "Tank List"
            ElseIf Request.QueryString("PopType") = "Sentry" Then
                strQuery = " SELECT NUMBER as [Sentry Number] , NAME as [Sentry Name] FROM SENTRY "
                strQuery += " WHERE  pump1_tank<>0 OR pump2_tank<>0 OR pump3_tank<>0 OR pump4_tank<>0 OR "
                strQuery += " pump5_tank<>0 OR pump6_tank<>0 OR pump7_tank<>0 OR pump8_tank<>0 "
                strQuery += " ORDER BY SENTRY.NUMBER"
                Page.Title = "Sentry List"
            ElseIf Request.QueryString("PopType") = "TransSentry" Then
                strQuery = " SELECT NUMBER as [Sentry Number] , NAME as [Sentry Name] FROM SENTRY "
                strQuery += " WHERE  pump1_tank<>0 OR pump2_tank<>0 OR pump3_tank<>0 OR pump4_tank<>0 OR "
                strQuery += " pump5_tank<>0 OR pump6_tank<>0 OR pump7_tank<>0 OR pump8_tank<>0 "
                strQuery += " ORDER BY SENTRY.NUMBER"
                Page.Title = "Sentry List"
            End If

            If (Not IsPostBack) Then
                GetData(strQuery)
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("PopUp_ReportInput_PageLoad", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            'Response.Redirect("Error1.aspx?Err=" + errmsg + "PopUp_ReportInput_PageLoad", False)
        End Try
    End Sub

    Public Sub GetData(ByVal strQry As String)
        Dim SqlConn As SqlConnection = New SqlConnection(str_Connection_string)
        Dim sqlReader As SqlDataReader
        Dim sqlCmd As SqlCommand = New SqlCommand(strQry, SqlConn)
        Dim DeptTable As TableElement = New TableElement()
        Try
            SqlConn.Open()
            sqlReader = sqlCmd.ExecuteReader()
            Dim iRowno As Integer = 0
            Dim Row As TableRow
            Dim Cell As TableCell
            Row = DeptTable.GetTableRow()
            Row.Width = Unit.Percentage(100)

            '****************************This all code is use for make table and insert data into the table ***********************************
            Cell = DeptTable.GetTableCell("Center", "Select " + Request.QueryString("PopType"), True)
            Cell.Width = Unit.Percentage(10)

            Cell.ColumnSpan = 3

            Row.Cells.Add(Cell)

            Row.HorizontalAlign = HorizontalAlign.Left
            tblSearch_result_Header.Rows.Add(Row)

            Row = DeptTable.GetTableRow()
            Row.Width = Unit.Percentage(100)

            Cell = DeptTable.GetTableCell("Center", "Select", True)

            Cell.Width = Unit.Percentage(3)
            Row.Cells.Add(Cell)

            Cell = DeptTable.GetTableCell("Center", "ID", True)
            Cell.Width = Unit.Percentage(5)
            Row.Cells.Add(Cell)

            Cell = DeptTable.GetTableCell("Center", "Name", True)
            Cell.Width = Unit.Percentage(25)
            Row.Cells.Add(Cell)

            Cell.Width = Unit.Percentage(10)
            Row.Cells.Add(Cell)

            Row.HorizontalAlign = HorizontalAlign.Left
            tblSearch_result_Header.Rows.Add(Row)

            If (sqlReader.HasRows = True) Then
                While (sqlReader.Read())
                    Row = DeptTable.GetTableRow()
                    Row.Width = Unit.Percentage(100)
                    Dim hLink As HyperLink = New HyperLink
                    Dim bLink As Button = New Button()
                    Dim script As String

                    Cell = DeptTable.GetTableCell("Center", "Select", False)
                    bLink.Text = "Select"

                    If Request.QueryString("PopType") = "TransSentry" Then
                        script = "javascript:window.opener.document.forms[0]." + Request.QueryString("TxtName").ToString() + ".value = '"
                        script += sqlReader(1).ToString + "'" + ";window.opener.document.forms[0]."
                        script += Request.QueryString("TxtId").ToString() + ".value = '"
                        script = script + sqlReader(0).ToString
                        script = script + "';window.opener.Test();self.close();  "
                        bLink.OnClientClick = script
                    Else
                        script = "javascript:window.opener.document.forms[0]." + Request.QueryString("TxtName").ToString() + ".value = '"
                        script = script + sqlReader(0).ToString
                        script = script + "';self.close()"
                        bLink.OnClientClick = script
                    End If
                    Cell.Controls.Add(bLink)
                    Cell.Width = Unit.Percentage(10)
                    Row.Cells.Add(Cell)

                    If sqlReader(0).ToString = " " Then
                        Cell = DeptTable.GetTableCell("Center", "-", False)
                    Else
                        Cell = DeptTable.GetTableCell("Center", sqlReader(0).ToString(), False)
                    End If
                    Cell.Width = Unit.Percentage(20)
                    Row.Cells.Add(Cell)

                    If sqlReader(1).ToString = " " Then
                        Cell = DeptTable.GetTableCell("left", "-", False)
                    Else
                        Cell = DeptTable.GetTableCell("left", sqlReader(1).ToString(), False)
                    End If
                    Cell.Width = Unit.Percentage(45)
                    Row.Cells.Add(Cell)

                    tblSearchResult.Rows.AddAt(iRowno, Row)
                    iRowno = iRowno + 1
                End While
            Else
                Row = DeptTable.GetTableRow()
                Row.Width = Unit.Percentage(100)
                Cell = DeptTable.GetTableCell("Center", "0 records found for selected search criteria", True)
                Cell.Width = Unit.Percentage(100)
                Row.Cells.Add(Cell)
                tblSearchResult.Rows.Add(Row)
            End If
            sqlReader.Close()
            tblSearchResult.DataBind()
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("PopUp_ReportInput.aspx_GetData", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            'Response.Redirect("Error1.aspx?Err=" + errmsg + "PopUp_ReportInput_GetData", False)
        Finally
            If (SqlConn.State = ConnectionState.Open = True) Then
                SqlConn.Close()
            End If
        End Try
    End Sub
End Class
