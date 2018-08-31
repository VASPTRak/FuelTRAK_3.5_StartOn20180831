Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Partial Class PollingActions
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            BindData()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingActions.Page_Load", ex)
        End Try
    End Sub
    Public Sub BindData()
        Try
            Dim objSql As New GeneralizedDAL()
            '''Dim Id As String = Session("SentryId")
            'Create New Dataset
            Dim ds As DataSet = New DataSet
            Dim param(1) As SqlParameter
            param(0) = New SqlParameter("@ID", SqlDbType.Int)
            param(0).Value = "001" '''Id
            param(1) = New SqlParameter("@Name", SqlDbType.NVarChar)
            param(1).Value = "SEN7"
            ds = objSql.ExecuteStoredProcedureGetDataSet("SP_PollingLog", param)
            Dim dt As DataTable = New DataTable
            dt = ds.Tables(0)
            If (dt.Rows.Count > 0) Then
                gvPollingLog.DataSource = dt
                gvPollingLog.DataBind()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found.');self.close();</script>")
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("PollingActions.BindData()", ex)
        End Try
    End Sub
    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("PollingActions.Timer1_Tick", ex)
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'Export Results in Excel.
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.Charset = ""
            Me.EnableViewState = False
            Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter()
            Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)
            VerifyRenderingInServerForm(gvPollingLog)
            gvPollingLog.RenderControl(oHtmlTextWriter)
            Response.Write(oStringWriter.ToString())
            Response.End()
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("PollingActions.btnSave_Click", ex)
        End Try
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control) '    Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '      server control at run time. */
        Try
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub gvPollingLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                e.Row.Cells(1).Attributes.Add("style", "word-break:break-all;word-wrap:break-word")
            End If
            '       if (e.Row.RowType == DataControlRowType.DataRow)
            '{
            '      e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            '}
        Catch ex As Exception
        End Try
    End Sub
End Class
