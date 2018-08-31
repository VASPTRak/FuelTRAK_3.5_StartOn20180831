Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class TranslationTable
    Inherits System.Web.UI.Page
    Dim Uinfo As UserInfo
    Dim GenFun As GeneralFunctions
    Dim DAL As GeneralizedDAL

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                Session("visited") = False
                'Shows records in table.
                If Not IsPostBack Then
                    If Session("ExportNum") = "" Then Session("ExportNum") = "1"
                    If Session("ExportNum") = "1" Then
                        rdoSelect.Items(0).Selected = True
                        rdoSelect.Items(1).Selected = False
                    Else
                        rdoSelect.Items(1).Selected = True
                        rdoSelect.Items(0).Selected = False
                    End If
                    FillRecords()
                End If
                If (Hidtxt.Value = "true") Then
                    If txtFieldName.Value <> "" And txtOrder.Value <> "" Then
                        deleteRecord(txtFieldName.Value, Convert.ToInt32(txtOrder.Value))
                        Hidtxt.Value = ""
                        txtFieldName.Value = ""
                        txtOrder.Value = ""
                    Else
                        Hidtxt.Value = ""
                    End If
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable.Page_Load", ex)
        End Try
    End Sub

    Private Sub deleteRecord(ByVal FieldName As String, ByVal Order As Integer)
        Dim strQuery As String
        Dim i As Integer
        Dim ds As New DataSet
        DAL = New GeneralizedDAL

        Dim nRowsAffected As Int32
        Try
            'change the order of remaining fields
            'strQuery = "Select * from translate where n_order >" & Order & " and ExportNum='" & Session("ExportNum") & "'"
            strQuery = "Select * from translate where n_order >" & Order & " and ExportNum='" & Session("ExportNum") & "' ORDER BY n_order"
            ds = DAL.GetDataSet(strQuery)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1 'ds.Tables("translate").Rows.Count - 1
                    'strQuery = "Update translate set n_order=n_order-1 where n_order=" & ds.Tables("translate").Rows(i)("n_order").ToString().Trim() & " and field_name='" & ds.Tables("translate").Rows(i)("field_name").ToString().Trim() & "' and ExportNum='" & Session("ExportNum") & "'"
                    strQuery = "Update translate set n_order=n_order-1 where n_order=" & ds.Tables(0).Rows(i)("n_order").ToString().Trim() & " and field_name='" & ds.Tables(0).Rows(i)("field_name").ToString().Trim() & "' and ExportNum='" & Session("ExportNum") & "'"
                    'blnFlag = DAL.GetDataSetbool(strQuery)


                    nRowsAffected = DAL.ExecuteNonQuery(strQuery)


                Next
            End If
            'delete selected record
            strQuery = "Delete from translate where field_name='" & FieldName.Trim() & "'" & " and n_order=" & Order & " and ExportNum='" & Session("ExportNum") & "'"
            'blnFlag = DAL.GetDataSetbool(strQuery)
            
            nRowsAffected = DAL.ExecuteNonQuery(strQuery)

            ds.Clear()
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TranslationTable_Page_Load", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
            'Response.Redirect("Error1.aspx?Err=" + errmsg + "TranslationTable_Page_Load", False)
        End Try
    End Sub

    Private Sub FillRecords()
        '******************This event is use for retrieving records from translate table***********************
        'Date of Creation: 30-Oct-07
        'Author: Harshada Mutalik
        'Date of Modified: 16-Oct-08
        'Author: Jatin Kshirsagar
        Dim str_Query As String
        DAL = New GeneralizedDAL
        GenFun = New GeneralFunctions
        Uinfo = New UserInfo
        Try
            'Select data from translate table
            str_Query = "SELECT * FROM TRANSLATE where ExportNum='" & Session("ExportNum").ToString() & " '  order by n_order "
            'Changed to get export fields according to export format selected.
            Dim sqldataset As DataSet = New DataSet()
            sqldataset = DAL.GetDataSet(str_Query)
            If Not sqldataset Is Nothing Then
                If sqldataset.Tables(0).Rows.Count > 0 Then
                    GridView1.DataSource = sqldataset.Tables(0).DefaultView
                    GridView1.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TranslationTable.FillRecords", ex)
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

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            Response.Redirect("TranslationTable_New_Edit.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable.btnAdd_Click", ex)
        End Try
    End Sub

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender
        
        Try
            If Page.IsPostBack Then
                FillRecords()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable.GridView1_PreRender", ex)
        End Try
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Try
            'call form to edit values
            '1-Nov-07
            'Harshada Mutalik

            Response.Redirect("TranslationTable_New_Edit.aspx?field_name=" + GridView1.DataKeys(e.NewEditIndex).Values.Item(1).ToString().Trim() + "&n_order=" + GridView1.DataKeys(e.NewEditIndex).Values.Item(0).ToString().Trim(), False)

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable.GridView1_RowEditing", ex)
        End Try

    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Try
            'Delete selected record
            '1-Nov-07      'Harshada Mutalik

            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>check('" + GridView1.DataKeys(e.RowIndex).Values(1).ToString() + "','" + GridView1.DataKeys(e.RowIndex).Values(0).ToString() + "' )</script>")
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "check('" + GridView1.DataKeys(e.RowIndex).Values(1).ToString() + "','" + GridView1.DataKeys(e.RowIndex).Values(0).ToString() + "');", True)

            End If


            ' ''Try
            ' ''    Dim str_Query As String
            ' ''    DAL = New GeneralizedDAL
            ' ''    GenFun = New GeneralFunctions
            ' ''    Uinfo = New UserInfo
            ' ''    Dim ID As String = GridView1.DataKeys(e.RowIndex).Value
            ' ''    str_Query = "Delete TRANSLATE where n_Order=" & ID
            ' ''    DAL.ExecuteNonQuery(str_Query)

            ' ''Catch ex As Exception
            ' ''End Try


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable.GridView1_RowDeleting", ex)
        End Try

    End Sub

    Protected Sub PrintTranslationTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintTranslationTable.Click
        Try
            GenFun = New GeneralFunctions
            Uinfo = New UserInfo

            Uinfo.ReportID = 2
            Uinfo.ReportHeader = GenFun.Get_Company_Name
            Uinfo.ReportTitle = "Translation Table for Export File"
            Session("Uinfo") = Uinfo
            Dim url As String = "ReportViewer.aspx"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Run_Report('" & url & "');</script>")


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable.PrintTranslationTable_Click", ex)
        End Try
    End Sub

    Protected Sub rdoSelect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSelect.SelectedIndexChanged
        Try

      
            'To set session variable to current export format.
            'Added on 6 Dec 07
            'Harshada Mutalik
            If rdoSelect.Items(0).Selected = True Then Session("ExportNum") = "1" Else Session("ExportNum") = "2"
            FillRecords()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable.rdoSelect_SelectedIndexChanged", ex)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("ExportTransactions.aspx")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TranslationTable.btnCancel_Click", ex)
        End Try

    End Sub

   
End Class
