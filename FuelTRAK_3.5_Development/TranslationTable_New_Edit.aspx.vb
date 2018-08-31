Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient

Partial Class TranslationTable_New_Edit
    Inherits System.Web.UI.Page
    Private SqlConn As SqlConnection
    Dim sqlReader As SqlDataReader
    Dim sqlCmd As SqlCommand
    Dim str_Connection_string As String = IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString())
    Dim sqlAdapter As SqlDataAdapter
    Dim sqldataset As New DataSet

    'Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    '    Response.Redirect("TranslationTable.aspx")
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User_name") Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
        Else
            txtFieldPos.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
            txtFieldExpLen.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
            btnOk.Attributes.Add("OnMouseDown", "Validate();")

            Dim strQuery As String
            Try
                If Not Session("visited") Or Not IsPostBack Then
                    SqlConn = New SqlConnection(str_Connection_string)
                    SqlConn.Open()
                    If Not IsPostBack Then
                        'select field description from datafd and display in dropdown list
                        '30-Oct-07
                        'Harshada Mutalik
                        strQuery = "select distinct field_no,field_desc from datafd order by field_no"
                        sqlAdapter = New SqlDataAdapter(strQuery, SqlConn)
                        sqldataset = New DataSet()
                        sqlAdapter.Fill(sqldataset, "datafd")
                        If sqldataset.Tables("datafd").Rows.Count > 0 Then
                            ddlFieldDesc.DataSource = sqldataset.Tables("datafd").DefaultView
                            ddlFieldDesc.DataTextField = "field_desc"
                            ddlFieldDesc.DataValueField = "field_no"
                            ddlFieldDesc.DataBind()
                            ddlFieldDesc_SelectedIndexChanged(Nothing, Nothing)
                        End If
                        sqldataset.Clear()
                        sqlAdapter.Dispose()
                    End If
                End If
                'Check if record is selected for editing.
                '1-Nov-07
                'Harshada Mutalik
                If Request.QueryString.Count > 0 And Not IsPostBack Then
                    Dim field As String
                    Dim order As String
                    field = Request.QueryString("field_name").ToString().Trim()
                    order = Request.QueryString("n_order").ToString().Trim()
                    lblNew_Edit.Text = "Edit Translation Information"
                    'strQuery = "select * from translate where field_name='" & field & "' and n_order=" & Convert.ToInt32(order)
                    strQuery = "select * from translate where field_name='" & field & "' and n_order=" & Convert.ToInt32(order) & " and ExportNum='" & Session("ExportNum") & "'"
                    sqlCmd = New SqlCommand(strQuery, SqlConn)
                    If SqlConn.State = ConnectionState.Closed Then SqlConn.Open()
                    sqlReader = sqlCmd.ExecuteReader()
                    If (sqlReader.HasRows) Then
                        sqlReader.Read()
                        Dim i As Integer
                        For i = 0 To ddlFieldDesc.Items.Count - 1
                            If (ddlFieldDesc.Items(i).Text.Trim() = sqlReader("field_desc").ToString().Trim()) Then
                                ddlFieldDesc.SelectedIndex = i
                                Exit For
                            End If
                        Next
                        txtFieldExpLen.Text = sqlReader("n_length").ToString().Trim()
                        txtFieldLen.Text = sqlReader("length").ToString().Trim()
                        txtFieldName.Text = sqlReader("field_name").ToString().Trim()
                        txtFieldPos.Text = sqlReader("n_order").ToString().Trim()
                        txtOldOrder.Value = sqlReader("n_order").ToString().Trim()
                        txtFillChar.Text = sqlReader("fillchar").ToString().Trim()
                        If (sqlReader("startpos").ToString().Trim().ToUpper() = "ALL") Then
                            rdoSelect.Items(0).Selected = True
                        ElseIf (sqlReader("startpos").ToString().Trim().ToUpper() = "LEFT") Then
                            rdoSelect.Items(1).Selected = True
                        ElseIf (sqlReader("startpos").ToString().Trim().ToUpper() = "RIGHT") Then
                            rdoSelect.Items(2).Selected = True
                        End If

                        sqlReader.Close()
                        sqlCmd.Dispose()

                        ddlFieldDesc.Enabled = False
                        txtFieldName.ReadOnly = True
                        txtFieldLen.ReadOnly = True
                        'Commented to make fill character editable.
                        '16 Nov 07
                        'Harshada Mutalik.

                        'If (txtFieldName.Text.Trim() <> "FILLER") Then
                        '    txtFillChar.ReadOnly = True
                        'End If

                        txtFieldPos.Focus()
                    End If
                End If
                txtFillChar.Focus()
            Catch ex As Exception
                Dim cr As New ErrorPage
                Dim errmsg As String
                cr.errorlog("TranslationTable_New_Edit_Page_load", ex)
                If (ex.Message.Contains(";")) Then
                    errmsg = ex.Message.ToString()
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
                ElseIf ex.Message.Contains(vbCrLf) Then
                    errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
                Else
                    errmsg = ex.Message.ToString()
                End If
                'Response.Redirect("Error1.aspx?Err=" + errmsg + "Transaction_New_Edit_Page_load")
            Finally
                If (SqlConn.State = ConnectionState.Open) Then
                    SqlConn.Close()
                End If

            End Try

        End If
    End Sub

    Protected Sub ddlFieldDesc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFieldDesc.SelectedIndexChanged
        'Change the values of field name and length when the field description is changed.
        'Date:-30 Oct 2007
        'Harshada Mutalik

        Dim strQuery As String
        strQuery = "select field_name,length from datafd where field_no=" & ddlFieldDesc.SelectedValue
        Try
            SqlConn = New SqlConnection(str_Connection_string)
            sqlCmd = New SqlCommand(strQuery, SqlConn)
            SqlConn.Open()
            sqlReader = sqlCmd.ExecuteReader()
            If sqlReader.HasRows Then
                sqlReader.Read()
                txtFieldName.Text = sqlReader("field_name").ToString().Trim()
                txtFieldLen.Text = sqlReader("length").ToString().Trim()

                'By default field length in export file=field length in datadwn table
                'Harshada Mutalik
                '17 Nov 07
                txtFieldExpLen.Text = sqlReader("length").ToString().Trim()

                'txtFieldPos.Focus()
            End If
            sqlReader.Close()
            sqlCmd.Dispose()
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TranslationTable_New_Edit_Page_load", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
            'Response.Redirect("Error1.aspx?Err=" + errmsg + "Transaction_New_Edit_Page_load")
        Finally
            If (SqlConn.State = ConnectionState.Open) Then
                SqlConn.Close()
            End If
        End Try
        txtFillChar.Focus()
    End Sub

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        'Insert/Update the details of field to be exported in translate table
        'Date: 31 Oct 2007
        'Harshada Mutalik

        Dim strQuery As String
        Dim field_str As String
        field_str = ""
        strQuery = ""


        strQuery = "'" & txtFieldName.Text.Trim() & "',"
        strQuery = strQuery & txtFieldPos.Text.Trim() & ",'"
        strQuery = strQuery & ddlFieldDesc.SelectedItem.Text.Trim() & "','"
        strQuery = strQuery & txtFillChar.Text.Trim() & "',"
        strQuery = strQuery & txtFieldLen.Text.Trim() & ","
        strQuery = strQuery & txtFieldExpLen.Text.Trim() & ",'"

        'Check whether the substring from a field is retrieved from left or right or all field is exported
        If rdoSelect.Items(0).Selected = True Then
            strQuery = strQuery & rdoSelect.Items(0).Value & "','"
            field_str = "Left"
        ElseIf rdoSelect.Items(1).Selected = True Then
            strQuery = strQuery & rdoSelect.Items(1).Value & "','"
            field_str = "Left"
        ElseIf rdoSelect.Items(2).Selected = True Then
            strQuery = strQuery & rdoSelect.Items(2).Value & "','"
            field_str = "Right"
        End If

        If txtFieldName.Text.Trim().ToUpper() = "FILLER" Then
            strQuery = strQuery & "Replicate(''" & txtFillChar.Text & "''," & txtFieldExpLen.Text & ")'"
        ElseIf txtFieldName.Text.Trim.ToUpper() = "CRLF" Then
            strQuery = strQuery & "CRLF'"
        Else
            'strQuery = strQuery & field_str & "(Datadwn." & txtFieldName.Text & "," & txtFieldExpLen.Text & ")'"
            strQuery = strQuery & field_str & "(Datadwn.[" & txtFieldName.Text.Trim() & "]," & txtFieldExpLen.Text.Trim() & ")'"
        End If
        Try
            'User wants to add new record
            If (lblNew_Edit.Text = "Add Translation Information") Then
                Dim ParameterList As String
                'ParameterList = "field_name,n_order,field_desc,fillchar,length,n_length,startpos,field_strg"
                ParameterList = "field_name,n_order,field_desc,fillchar,length,n_length,startpos,field_strg,ExportNum"
                strQuery = strQuery & ",'" & Session("ExportNum") & "'"
                SqlConn = New SqlConnection(str_Connection_string)
                SqlConn.Open()
                'If a field is inserted in between, change the orders of other fields.

                'Get all fields having position greater than or equal to current position
                'strQuery = "select * from translate where n_order>=" & Convert.ToInt32(txtFieldPos.Text) & " and field_name<>'" & txtFieldName.Text.Trim() & "'"
                Dim strQ As String = "select * from translate where n_order >=" & Convert.ToInt32(txtFieldPos.Text) & " and ExportNum='" & Session("ExportNum") & "'"
                sqlAdapter = New SqlDataAdapter(strQ, SqlConn)
                sqldataset = New DataSet()
                sqlAdapter.Fill(sqldataset, "translate")
                'If records exist, that means the field is inserted in the middle.
                'Change the order of other fields.
                sqldataset.Clear()
                sqlAdapter.Dispose()
                sqlCmd = New SqlCommand()
                sqlCmd.Connection = SqlConn
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "usp_tt_InsertRecords"
                sqlCmd.Parameters.AddWithValue("@TableName", "Translate")
                sqlCmd.Parameters.AddWithValue("@ParameterName", ParameterList)
                sqlCmd.Parameters.AddWithValue("@Values", strQuery)
                sqlCmd.ExecuteNonQuery()

                Dim script As String
                script = "<script>alert('Field is selected for export')</script>"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", script)


                sqlCmd.Dispose()

            Else
                'User wants to edit previous record.
                '1-Nov-07
                'Harshada Mutalik
                Dim i, no, new_order, old_order As Integer
                old_order = Convert.ToInt32(txtOldOrder.Value)
                new_order = txtFieldPos.Text

                If SqlConn.State = ConnectionState.Closed Then SqlConn.Open()
                'If order of the field is greater than current order, then decrease order of other fields
                '1-Nov-07
                'Harshada Mutalik
                If (new_order > old_order) Then
                    For i = old_order To new_order - 1
                        'strQuery = "update translate set n_order=n_order-1 where n_order=" & i + 1 
                        strQuery = "update translate set n_order=n_order-1 where n_order=" & i + 1 & " and ExportNum='" & Session("ExportNum") & "'"
                        sqlCmd = New SqlCommand(strQuery, SqlConn)
                        no = sqlCmd.ExecuteNonQuery()
                        sqlCmd.Dispose()
                    Next
                ElseIf (new_order < old_order) Then
                    'If order of the field is less than current order, then increase order of other fields
                    '1-Nov-07
                    'Harshada Mutalik
                    For i = old_order - 1 To new_order Step -1
                        'strQuery = "update translate set n_order = n_order + 1 where n_order=" & i
                        strQuery = "update translate set n_order = n_order + 1 where n_order=" & i & " and ExportNum='" & Session("ExportNum") & "'"
                        sqlCmd = New SqlCommand(strQuery, SqlConn)
                        no = sqlCmd.ExecuteNonQuery()
                        sqlCmd.Dispose()
                    Next
                End If
                strQuery = "update translate set n_order=" & new_order & ",field_desc='" & ddlFieldDesc.SelectedItem.Text.Trim() & "',fillchar='" & txtFillChar.Text & "',length=" & Convert.ToInt32(txtFieldLen.Text) & ",n_length= " & txtFieldExpLen.Text.Trim() & ",startpos='"
                If rdoSelect.Items(0).Selected = True Then
                    strQuery = strQuery & rdoSelect.Items(0).Value & "',"
                    field_str = "Left"
                ElseIf rdoSelect.Items(1).Selected = True Then
                    strQuery = strQuery & rdoSelect.Items(1).Value & "',"
                    field_str = "Left"
                ElseIf rdoSelect.Items(2).Selected = True Then
                    strQuery = strQuery & rdoSelect.Items(2).Value & "',"
                    field_str = "Right"
                End If
                strQuery = strQuery & "field_strg='"
                If txtFieldName.Text.Trim().ToUpper() = "FILLER" Then
                    strQuery = strQuery & "Replicate(''" & txtFillChar.Text.Trim() & "''," & txtFieldExpLen.Text.Trim() & ")'"
                ElseIf txtFieldName.Text.Trim().ToUpper() = "CRLF" Then
                    strQuery = strQuery & "CRLF'"
                Else
                    strQuery = strQuery & field_str & "(Datadwn.[" & txtFieldName.Text.Trim() & "]," & txtFieldExpLen.Text.Trim() & ")'"
                End If
                'strQuery = strQuery & " where field_name='" & txtFieldName.Text.Trim() & "' and n_order=" & old_order
                strQuery = strQuery & " where field_name='" & txtFieldName.Text.Trim() & "' and ExportNum='" & Session("ExportNum") & "' and n_order=" & old_order
                sqlCmd = New SqlCommand(strQuery, SqlConn)
                no = sqlCmd.ExecuteNonQuery()
                sqlCmd.Dispose()

            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TranslationTable_New_Edit.btnOk", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
            'Response.Redirect("Error1.aspx?Err=" + errmsg + "TranslationTable_New_Edit_BtnOk")
        Finally
            If SqlConn.State = ConnectionState.Open Then
                SqlConn.Close()
            End If

        End Try
        Response.Redirect("TranslationTable.aspx", False)

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("TranslationTable.aspx")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable_New_Edit.btnCancel_Click", ex)
        End Try


    End Sub
End Class
