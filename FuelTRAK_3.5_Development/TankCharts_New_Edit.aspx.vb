Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Partial Class TankCharts_New_Edit
    Inherits System.Web.UI.Page
    Dim ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            Else

                If Not Page.IsPostBack Then
                    If Request.QueryString.Count > 0 Then
                        If Not Request.QueryString.Get("RowID") Is Nothing And Not Request.QueryString.Get("RowID") = "" Then
                            lblText.Text = "Edit Tank Chart"
                            btnViewChart.Visible = True
                            txtProdVal.Visible = True
                            btnContinue.Text = "Update"
                            HideControls()
                            FillData()
                        End If
                    Else
                        ClearItems()
                    End If

                End If


                
                'Added By Varun Moota. 01/07/2010
                ' btnContinue.Attributes.Add("OnClick", "window.showModalDialog('TankChartsPopUp.aspx',window,'dialogHeight:500px;dialogWidth:650px;dialogHide:true;help:no;status:no;toolbar:no;location:no;titlebar:no;resizable:yes;scrollbars:yes;');return true;")

                End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor_New_edit_InsertData", ex)
        End Try
    End Sub
    Public Function GetallData(ByVal RowId As String) As DataSet
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim ParNUMBER = New SqlParameter("@NUMBER", SqlDbType.Int)
            ParNUMBER.Direction = ParameterDirection.Input
            ParNUMBER.value = RowId
            parcollection(0) = ParNUMBER
            GetallData = dal.ExecuteStoredProcedureGetDataSet("Use_TT_GetTankChartDetails", parcollection)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.GetallData", ex)
            GetallData = Nothing
        End Try
    End Function
    Public Sub FillData()
        Try
            ds = GetallData(Request.QueryString.Get("RowID"))
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Session("TankChartNum") = ds.Tables(0).Rows(0)("CHARTNUMBER").ToString()
                    txtChartName.Text = ds.Tables(0).Rows(0)("CHARTNAME").ToString()
                    txtDesc.Text = ds.Tables(0).Rows(0)("DESC").ToString()
                    txtTankSize.Text = ds.Tables(0).Rows(0)("TANKSIZE").ToString()
                    txtIncCTR.Text = ds.Tables(0).Rows(0)("INCCTR").ToString()
                    ddlProdSelect.Text = ds.Tables(0).Rows(0)("PRODTYPE").ToString()
                    txtProdVal.Text = ds.Tables(0).Rows(0)("PRODVALUE").ToString()
                    txtZeroOffset.Text = ds.Tables(0).Rows(0)("ZeroOffSet").ToString()

                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.FillData()", ex)
        End Try
    End Sub

    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Try
            If Not Request.QueryString.Get("RowID") Is Nothing And Not Request.QueryString.Get("RowID") = "" Then
                UpdateTankChart()
            Else
                InsertTankChart()
            End If


            'Response.Redirect("TankChartsPopUp.aspx", False)
            ' Response.Redirect("TankChartsPopUp.aspx?RowID=" + txtChartName.ToString(), False)

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.btnContinue_Click", ex)
        End Try
    End Sub
    Private Sub InsertTankChart()
        Try
            'Create New Tank Chart

            Dim dal As New GeneralizedDAL()
            Dim parcollection(9) As SqlParameter


            Dim ParChartName = New SqlParameter("@ChartName", SqlDbType.VarChar, 25)
            Dim ParDesc = New SqlParameter("@Desc", SqlDbType.NVarChar, 50)
            Dim ParTankSize = New SqlParameter("@TankSize", SqlDbType.Decimal)
            Dim ParEntries = New SqlParameter("@Entries", SqlDbType.Int)
            Dim ParIncCtr = New SqlParameter("@IncCtr", SqlDbType.Int)
            Dim ParProdType = New SqlParameter("@ProdType", SqlDbType.VarChar)
            Dim ParProdVal = New SqlParameter("@ProdVal", SqlDbType.Decimal)
            Dim ParZeroOffSet = New SqlParameter("@ZeroOffSet", SqlDbType.NVarChar)
            Dim ParDateAdd = New SqlParameter("@DateAdd", SqlDbType.DateTime)
            Dim ParCreatedBy = New SqlParameter("@CreatedBy", SqlDbType.VarChar, 25)

            ParChartName.Direction = ParameterDirection.Input
            ParDesc.Direction = ParameterDirection.Input
            ParTankSize.Direction = ParameterDirection.Input
            ParEntries.Direction = ParameterDirection.Input
            ParIncCtr.Direction = ParameterDirection.Input
            ParProdType.Direction = ParameterDirection.Input
            ParProdVal.Direction = ParameterDirection.Input
            ParZeroOffSet.Direction = ParameterDirection.Input
            ParDateAdd.Direction = ParameterDirection.Input
            ParCreatedBy.Direction = ParameterDirection.Input

            ParChartName.value = txtChartName.Text.ToString()
            ParDesc.value = txtDesc.Text.ToString()
            ParTankSize.value = CDec(txtTankSize.Text.ToString())
            ParEntries.value = 0
            ParIncCtr.value = CInt(txtIncCTR.Text.ToString())
            ParProdType.value = ddlProdSelect.SelectedValue.ToString()
            ParProdVal.value = IIf(Not txtProdVal.Text = "", CDec(txtProdVal.Text.ToString()), 0)
            ParZeroOffSet.value = IIf(Not txtZeroOffset.Text = "", txtZeroOffset.Text.ToString(), 0)
            ParDateAdd.value = DateTime.Now
            ParCreatedBy.value = Session("User_name").ToString()

            parcollection(0) = ParChartName
            parcollection(1) = ParDesc
            parcollection(2) = ParTankSize
            parcollection(3) = ParEntries
            parcollection(4) = ParIncCtr
            parcollection(5) = ParProdType
            parcollection(6) = ParProdVal
            parcollection(7) = ParZeroOffSet
            parcollection(8) = ParDateAdd
            parcollection(9) = ParCreatedBy

            ds = dal.ExecuteStoredProcedureGetDataSet("SP_INSERT_TANKCHART", parcollection)

            If ds.Tables(0).Rows.Count > 0 Then
                InsertTankChartDetail(ds.Tables(0).Rows(0)(0).ToString())
                Response.Redirect("TankChartsPopUp.aspx?RowID=" + ds.Tables(0).Rows(0)(0).ToString(), False)

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Failure to create chart!');location.href='TankCharts.aspx';</script>")

            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.InsertTankChart", ex)
        End Try

    End Sub
    Private Sub InsertTankChartDetail(ByVal ChartNum As String)
        Try
            'Create New Tank Chart Detail

            Dim dal As New GeneralizedDAL()
            Dim parcollection(3) As SqlParameter


            Dim ParChartNumber = New SqlParameter("@ChartNumber", SqlDbType.Int)
            Dim ParILevel = New SqlParameter("@ILevel", SqlDbType.Decimal)
            Dim ParGLevel = New SqlParameter("@Glevel", SqlDbType.Decimal)
            Dim ParIncCtr = New SqlParameter("@IncCTR", SqlDbType.Decimal)


            ParChartNumber.Direction = ParameterDirection.Input
            ParILevel.Direction = ParameterDirection.Input
            ParGLevel.Direction = ParameterDirection.Input
            ParIncCtr.Direction = ParameterDirection.Input
          
            ParChartNumber.value = CInt(ChartNum.ToString())
            ParILevel.value = CDec(rbtnList.SelectedValue.ToString())
            ParGLevel.value = CDec(txtTankSize.Text.ToString())
            ParIncCtr.value = CDec(txtIncCTR.Text.ToString())

            parcollection(0) = ParChartNumber
            parcollection(1) = ParILevel
            parcollection(2) = ParGLevel
            parcollection(3) = ParIncCtr



            Dim blnFlag As Boolean = dal.ExecuteStoredProcedureGetBoolean("SP_INSERT_TANKCHART_DETAIL", parcollection)

            If Not blnFlag Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Failure to create chart!');location.href='TankCharts.aspx';</script>")

            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.InsertTankChartDetail", ex)
        End Try
    End Sub
    Private Sub UpdateTankChart()
        Try
            'Create New Tank Chart

            Dim dal As New GeneralizedDAL()
            Dim parcollection(7) As SqlParameter

            Dim ParChartNum = New SqlParameter("@ChartNumber", SqlDbType.Int)
            Dim ParChartName = New SqlParameter("@ChartName", SqlDbType.VarChar, 25)
            Dim ParDesc = New SqlParameter("@Desc", SqlDbType.NVarChar, 50)
            Dim ParProdType = New SqlParameter("@ProdType", SqlDbType.VarChar)
            Dim ParProdVal = New SqlParameter("@ProdVal", SqlDbType.Decimal)
            Dim ParZeroOffSet = New SqlParameter("@ZeroOffSet", SqlDbType.NVarChar)
            Dim ParModifiedDate = New SqlParameter("@ModifiedDT", SqlDbType.DateTime)
            Dim ParModifiedBy = New SqlParameter("@ModifiedBy", SqlDbType.VarChar, 25)

            ParChartNum.Direction = ParameterDirection.Input
            ParChartName.Direction = ParameterDirection.Input
            ParDesc.Direction = ParameterDirection.Input
            ParProdType.Direction = ParameterDirection.Input
            ParProdVal.Direction = ParameterDirection.Input
            ParZeroOffSet.Direction = ParameterDirection.Input
            ParModifiedDate.Direction = ParameterDirection.Input
            ParModifiedBy.Direction = ParameterDirection.Input

            ParChartNum.value = CInt(Session("TankChartNum").ToString())
            ParChartName.value = txtChartName.Text.ToString()
            ParDesc.value = txtDesc.Text.ToString()
            ParProdType.value = ddlProdSelect.SelectedValue.ToString()
            ParProdVal.value = IIf(Not txtProdVal.Text = "", CDec(txtProdVal.Text.ToString()), 0)
            ParZeroOffSet.value = IIf(Not txtZeroOffset.Text = "", txtZeroOffset.Text.ToString(), 0)
            ParModifiedDate.value = DateTime.Now
            ParModifiedBy.value = Session("User_name").ToString()

            parcollection(0) = ParChartNum
            parcollection(1) = ParChartName
            parcollection(2) = ParDesc
            parcollection(3) = ParProdType
            parcollection(4) = ParProdVal
            parcollection(5) = ParZeroOffSet
            parcollection(6) = ParModifiedDate
            parcollection(7) = ParModifiedBy

            Dim iVal As Int16 = dal.ExecuteStoredProcedureGetInteger("SP_UPDATE_TANKCHART", parcollection)

            If iVal > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Updated Successfully!');</script>")
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Fail to Update Chart!');location.href='TankCharts.aspx';</script>")

            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.InsertTankChart", ex)
        End Try

    End Sub
    Protected Sub ddlProdSelect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProdSelect.SelectedIndexChanged
        Try
            If ddlProdSelect.SelectedIndex = 0 Then
                txtProdVal.Visible = False
            ElseIf ddlProdSelect.SelectedIndex = 1 Then
                txtProdVal.Visible = True
                txtProdVal.Text = "0.0465" 'UNLEADED
            ElseIf ddlProdSelect.SelectedIndex = 2 Then
                txtProdVal.Visible = True
                txtProdVal.Text = "0.0399" 'DISEL
            ElseIf ddlProdSelect.SelectedIndex = 3 Then
                txtProdVal.Visible = True
                txtProdVal.Text = ""
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.ddlProdSelect_SelectedIndexChanged", ex)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("TankCharts.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.ddlProdSelect_SelectedIndexChanged", ex)
        End Try
    End Sub

    Private Sub ClearItems()
        Try
            txtChartName.Text = ""
            txtDesc.Text = ""
            txtIncCTR.Text = ""
            txtProdVal.Text = ""
            txtTankSize.Text = ""
            txtZeroOffset.Text = ""
            rbtnList.SelectedIndex = 0

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.ClearItems()", ex)
        End Try
    End Sub

    Private Sub HideControls()
        Try

            rbtnList.Visible = False
            txtTankSize.Enabled = False
            txtIncCTR.Enabled = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.HideControls()", ex)
        End Try
       
    End Sub

    Protected Sub btnViewChart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnViewChart.Click
        Try
            Response.Redirect("TankChartsPopUp.aspx?RowID=" + Session("TankChartNum").ToString(), False)

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts_New_Edit.btnViewChart_Click", ex)
        End Try
    End Sub
End Class
