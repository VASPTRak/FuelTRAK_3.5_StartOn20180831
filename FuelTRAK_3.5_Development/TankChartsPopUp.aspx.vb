Imports System.Data
Imports System.Data.SqlClient
Partial Class TankChartsPopUp
    Inherits System.Web.UI.Page
    Dim ds As New DataSet
    Dim dv As New DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                If Request.QueryString.Count > 0 Then
                    If Not Request.QueryString.Get("RowID") Is Nothing And Not Request.QueryString.Get("RowID") = "" Then
                        BindGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Page_Load_TankChartsPopUp", ex)
        End Try
    End Sub

    Public Sub BindGrid()
        Try
            Dim dal As New GeneralizedDAL()
            Dim qry As String = "SELECT * FROM CHARTDETAIL WHERE CHARTNUMBER= " + Request.QueryString.Get("RowID") + " ORDER BY ILEVEL"
            Dim dsTankCharts As New DataSet
            dsTankCharts = dal.GetDataSet(qry)
            If dsTankCharts.Tables(0).Rows.Count > 0 Then
                Session("dsTankChart") = dsTankCharts
                GridTankCharts.DataSource = dsTankCharts.Tables(0)
                GridTankCharts.DataBind()
            End If

        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankChartsPopUp.BindGrid()", ex)
        End Try

    End Sub


    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Response.Redirect("TankCharts.Aspx", False)
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankChartsPopUp.btnClose_Click", ex)
        End Try
    End Sub




    Protected Sub GridTankCharts_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridTankCharts.RowCancelingEdit
        Try
            GridTankCharts.EditIndex = -1
            BindGrid()
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankChartsPopUp.GridTankCharts_RowCancelingEdit", ex)
        End Try

    End Sub

    Protected Sub GridTankCharts_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridTankCharts.RowDataBound
        Try
            'if (e.Row.RowState == DataControlRowState.Edit)
            '{
            '    //Set the focus to control on the edited row
            '    e.Row.Cells[1].Controls[0].Focus(); 

            '    //Or to a specific control in TemplateField
            '   // e.Row.FindControl("txtEdit").Focus(); 
            '}

            If (e.Row.RowState = DataControlRowState.Edit) Then
                e.Row.Cells(1).Controls(0).Focus()
            End If

        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankChartsPopUp.GridTankCharts_RowDataBound", ex)
        End Try
    End Sub

    Protected Sub GridTankCharts_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridTankCharts.RowEditing
        Try
            GridTankCharts.EditIndex = e.NewEditIndex
            Dim txtGLevel As Label = CType(GridTankCharts.Rows(e.NewEditIndex).FindControl("lblGLevel"), Label)
            Session("GLevelValue") = txtGLevel.Text.ToString()


            BindGrid()

            'Set the focus to control on the edited row
            GridTankCharts.Rows(e.NewEditIndex).FindControl("txtGLEVELS").Focus()


        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankChartsPopUp.GridTankCharts_RowEditing", ex)
        End Try
    End Sub

    Protected Sub GridTankCharts_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridTankCharts.RowUpdating
        Try
            Dim txtGLevel As TextBox = CType(GridTankCharts.Rows(e.RowIndex).FindControl("txtGLEVELS"), TextBox)
            'GridTankCharts.EditIndex = -1

            Dim iMin As Label = CType(GridTankCharts.Rows(e.RowIndex - 1).FindControl("lblGLevel"), Label)
            Dim iMax As Label = CType(GridTankCharts.Rows(e.RowIndex + 1).FindControl("lblGLevel"), Label)


            'Verify  Chart
            If (CDec(txtGLevel.Text.ToString()) > CDec(iMin.Text.ToString())) And (CDec(txtGLevel.Text.ToString()) < CDec(iMax.Text.ToString())) Then
                UpdateCharts(txtGLevel.Text.ToString())
            Else

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Invalid Chart Entry at Level " + e.RowIndex.ToString() + "');</script>")
            End If

            ' GridTankCharts.EditIndex = -1
            BindGrid()

            'Set the focus to control on the edited row
            'GridTankCharts.Rows(e.RowIndex).FindControl("txtGLEVELS").Focus()
            GridTankCharts.EditIndex = e.RowIndex

        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankChartsPopUp.GridTankCharts_RowUpdating", ex)
        End Try

    End Sub

    Private Sub UpdateCharts(ByVal NewGLevel As String)
        Try
            Dim dal As New GeneralizedDAL()
            Dim OldGLevel As String = Session("GLevelValue").ToString()

            Dim parcollection(4) As SqlParameter

            Dim ParChartNumber = New SqlParameter("@ChartNum", SqlDbType.Int)
            Dim ParNewGVal = New SqlParameter("@NewGValue", SqlDbType.Decimal)
            Dim ParOldGVal = New SqlParameter("@OldGValue", SqlDbType.Decimal)
            Dim ParModifiedDate = New SqlParameter("@ModifiedDate", SqlDbType.DateTime)
            Dim ParModifiedBy = New SqlParameter("@ModifiedBy", SqlDbType.VarChar, 25)

            ParChartNumber.Direction = ParameterDirection.Input
            ParNewGVal.Direction = ParameterDirection.Input
            ParOldGVal.Direction = ParameterDirection.Input
            ParModifiedDate.Direction = ParameterDirection.Input
            ParModifiedBy.Direction = ParameterDirection.Input

            ParChartNumber.value = CInt(Request.QueryString.Get("RowID").ToString())
            ParNewGVal.value = CDec(NewGLevel)
            ParOldGVal.value = CDec(OldGLevel)
            ParModifiedDate.value = DateTime.Now
            ParModifiedBy.value = Session("User_name").ToString()

            parcollection(0) = ParChartNumber
            parcollection(1) = ParNewGVal
            parcollection(2) = ParOldGVal
            parcollection(3) = ParModifiedDate
            parcollection(4) = ParModifiedBy



            ds = dal.ExecuteStoredProcedureGetDataSet("SP_UPDATE_TANKCHART_DETAIL", parcollection)

            If ds.Tables(0).Rows.Count > 0 Then
                GridTankCharts.DataSource = ds.Tables(0)
                GridTankCharts.DataBind()
            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record Update sucessfully.');</script>")
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankChartsPopUp.UpdateCharts()", ex)
        End Try
    End Sub

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim GenFun As New GeneralFunctions
            Dim Uinfo As New UserInfo

            Uinfo.ReportHeader = "Tank Charts"
            Uinfo.ReportID = 301
            Dim strQuery As String = GenFun.Report(Convert.ToInt32(Uinfo.ReportID.ToString()), Uinfo)

            Session("Uinfo") = Uinfo
            Dim url As String = "ReportViewer.aspx"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Run_Report('" & url & "');</script>")
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankChartsPopUp.btnPrint_Click()", ex)
        End Try
    End Sub
End Class
