Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Text
Partial Class VehCustMsgs
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim dt As DataTable
    Dim dal = New GeneralizedDAL()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("User_name") Is Nothing Then 'check for session is null/not
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                If Not Page.IsPostBack Then
                    BindGrid()
					btnCancel.Visible = False
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehCustMsgs.Page_Load", ex)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            Dim dal As New GeneralizedDAL()

            Dim qry As String = "SELECT * FROM VEHICLECUSTMSGS ORDER BY [ID]ASC"
            Dim dsvehCustMsgs As New DataSet
            dsvehCustMsgs = dal.GetDataSet(qry)
            If dsvehCustMsgs.Tables(0).Rows.Count > 0 Then
                gvVehMsgs.DataSource = dsvehCustMsgs.Tables(0)
                gvVehMsgs.DataBind()
            Else
                dt = GetData()
                gvVehMsgs.DataSource = dt
                gvVehMsgs.DataBind()
            End If

        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("VehCustMsgs.BindGrid()", ex)
        End Try

    End Sub
    Protected Function GetData() As DataTable
        Try

            dt = FormatDataTable()
            Dim dr As DataRow

            ' Populate the datatable with your TEST data 
            dr = dt.NewRow
            dr("id") = 1
            dr("VehMessages") = "Test Message"
            ' Add the row
            dt.Rows.Add(dr)

            dt.AcceptChanges()
            Session("dtVehMsgs") = dt
            Return dt
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehCustMsgs.GetData()", ex)
            Return Nothing
        End Try
    End Function

    Protected Function FormatDataTable() As DataTable
        Try

            dt = New DataTable()
            ' Create Columns
            Dim dtCol As DataColumn

            dtCol = New DataColumn()
            dtCol.DataType = Type.GetType("System.Int32")
            dtCol.ColumnName = "ID"
            dt.Columns.Add(dtCol)


            dtCol = New DataColumn()
            dtCol.DataType = Type.GetType("System.String")
            dtCol.ColumnName = "VehMessages"
            dt.Columns.Add(dtCol)
            Return dt
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehCustMsgs.FormatDataTable()", ex)
            Return Nothing
        End Try
    End Function
    Protected Sub gvVehMsgs_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvVehMsgs.RowCancelingEdit
        Try
            gvVehMsgs.EditIndex = -1
            BindGrid()
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("VehCustMsgs.gvVehMsgs_RowCancelingEdit", ex)
        End Try

    End Sub

    Protected Sub gvVehMsgs_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVehMsgs.RowCommand
        Try

            If (e.CommandName = "NoDataInsert") Then
                Dim txtMsgVal As TextBox = CType(gvVehMsgs.Controls(0).Controls(0).FindControl("NoDataMessage"), TextBox)
                If txtMsgVal.Text <> "" Or Not Nothing Then
                    InsertRecords(txtMsgVal.Text.ToString())
                    BindGrid()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Message is provided ');</script>")
                End If
            ElseIf (e.CommandName = "InsertNew") Then
                Dim txtMsgVal As TextBox = CType(gvVehMsgs.FooterRow.FindControl("txtNewMsg"), TextBox)
                If txtMsgVal.Text <> "" Or Not Nothing Then
                    InsertRecords(txtMsgVal.Text.ToString())
                    gvVehMsgs.ShowFooter = False
					btnCancel.Visible = False
                    BindGrid()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Message is provided ');</script>")
                End If
            ElseIf (e.CommandName = "CancelNew") Then
                gvVehMsgs.ShowFooter = False
				btnCancel.Visible = False
                BindGrid()
            End If


        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("VehCustMsgs.gvVehMsgs_RowCommand", ex)
        End Try
    End Sub

    Protected Sub gvVehMsgs_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvVehMsgs.RowEditing
        Try
            gvVehMsgs.EditIndex = e.NewEditIndex
            Dim txtMsgID As Label = CType(gvVehMsgs.Rows(e.NewEditIndex).FindControl("lblMsgID"), Label)
            Session("MsgID") = txtMsgID.Text.ToString()


            BindGrid()
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("VehCustMsgs.gvVehMsgs_RowEditing", ex)
        End Try
    End Sub

    Protected Sub gvVehMsgs_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvVehMsgs.RowUpdating
        Try
            Dim txtMsg As TextBox = CType(gvVehMsgs.Rows(e.RowIndex).FindControl("txtVehMessages"), TextBox)
            Dim ID As Label = CType(gvVehMsgs.Rows(e.RowIndex).FindControl("lblMsgID"), Label)

            If txtMsg.Text <> "" Then
                UpdateRecord(ID.Text.ToString(), txtMsg.Text.ToString())
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Message is provided " + e.RowIndex.ToString() + "');</script>")
            End If

            gvVehMsgs.EditIndex = -1
            BindGrid()

        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("VehCustMsgs.gvVehMsgs_RowUpdating", ex)
        End Try

    End Sub

    Private Sub UpdateRecord(ByVal ID As String, ByVal Message As String)
        Try
            Dim dal As New GeneralizedDAL()
            Dim iCnt As Integer = 0

            Dim qry As String = "UPDATE vehicleCustMsgs SET VEHMESSAGES = '" + Message + "' WHERE ID= " + ID + ""

            iCnt = dal.ExecuteScalarGetInteger(qry)

            If iCnt > 0 Then
                gvVehMsgs.DataSource = ds.Tables(0)
                gvVehMsgs.DataBind()
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("VehCustMsgs.UpdateRecord", ex)
        End Try
    End Sub
    Private Sub InsertRecords(ByVal VehMsg As String)
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter

            Dim ParVehMessage = New SqlParameter("@VehMsg", SqlDbType.NVarChar)
            ParVehMessage.Direction = ParameterDirection.Input

            ParVehMessage.value = VehMsg.ToString()
            parcollection(0) = ParVehMessage

            ds = dal.ExecuteStoredProcedureGetDataSet("SP_INSERT_VEH_CUSTOM_MSGS", parcollection)
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("VehCustMsgs.InsertRecords", ex)
        End Try

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            
            gvVehMsgs.ShowFooter = True
			btnCancel.Visible = True
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehCustMsgs.btnAdd_Click()", ex)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            gvVehMsgs.ShowFooter = False
            BindGrid()
			btnCancel.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehCustMsgs.btnCancel", ex)
        End Try
    End Sub
End Class
