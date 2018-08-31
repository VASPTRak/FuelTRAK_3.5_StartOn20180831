Imports System.Data
Imports System.Data.SqlClient
Partial Class PollingLogPopUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            LoadLog()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollingLogPopUp.Page_Load", ex)
        End Try

    End Sub
    Public Sub LoadLog()
        Try
            Dim objSql As New GeneralizedDAL()

            Dim Id As String = Session("SentryId")

            'Create New Dataset
            Dim ds As DataSet = New DataSet

            Dim param(1) As SqlParameter


            param(0) = New SqlParameter("@ID", SqlDbType.Int)
            param(0).Value = Id

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
            exLog.errorlog("PollingLogUserControl", ex)
        End Try

    End Sub

End Class
