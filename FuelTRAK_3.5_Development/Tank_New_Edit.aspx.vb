Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Partial Class Tank_New_Edit
    Inherits System.Web.UI.Page
    Dim ds, dsdata As DataSet
    Dim i As Integer
    Dim dal As New GeneralizedDAL()

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Tank.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank_New_Edit.btnCancel_Click", ex)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddAttribute()
        Try
            If Session("User_name") Is Nothing Then 'check for session is null/not
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            Else
                If Not Page.IsPostBack Then

                    If Request.QueryString.Count > 0 Then
                        BindProduct()
                        BindTankMonitor()
                        BindTankChart()
                        If Not Request.QueryString.Get("RowID") Is Nothing And Not Request.QueryString.Get("RowID") = "" Then
                            txtName.Focus()
                            dsdata = GetallData(Request.QueryString.Get("RowID"))
                            If Not ds Is Nothing Then
                                If dsdata.Tables(0).Rows.Count > 0 Then
                                    btnOK.Text = "Update"
                                    txtTank.Text = dsdata.Tables(0).Rows(0)(0).ToString()
                                    txtName.Text = dsdata.Tables(0).Rows(0)(1).ToString()
                                    txtAddress1.Text = dsdata.Tables(0).Rows(0)(2).ToString()

                                    If Not dsdata.Tables(0).Rows(0)(3) Is Nothing Then
                                        If Not dsdata.Tables(0).Rows(0)(3).ToString().Trim() = "" Then
                                            While Not DropProduct.SelectedValue.ToLower() = dsdata.Tables(0).Rows(0)(3).ToString().ToLower()
                                                DropProduct.SelectedIndex = i
                                                i = i + 1
                                            End While
                                        Else
                                            DropProduct.SelectedIndex = 0
                                        End If
                                    End If

                                    If Not dsdata.Tables(0).Rows(0)(4) Is DBNull.Value Then
                                        If dsdata.Tables(0).Rows(0)(4) = "0" Then
                                            txtprice.Text = "0.0"
                                        Else
                                            txtprice.Text = Convert.ToDouble(dsdata.Tables(0).Rows(0)(4))
                                        End If

                                    Else
                                        txtprice.Text = "0.0"
                                    End If

                                    txttsize.Text = dsdata.Tables(0).Rows(0)(5).ToString()
                                    txtRnotice.Text = dsdata.Tables(0).Rows(0)(6).ToString()
                                    txtExCode.Text = dsdata.Tables(0).Rows(0)(7).ToString()

                                    If Not dsdata.Tables(0).Rows(0)(8) Is DBNull.Value Then

                                        i = 0
                                        If Not dsdata.Tables(0).Rows(0)(8).ToString().Trim() = "" Then
                                            While Not DropTM.SelectedValue.ToLower() = dsdata.Tables(0).Rows(0)(8).ToString()
                                                DropTM.SelectedIndex = i
                                                i = i + 1
                                            End While
                                        Else
                                            DropTM.SelectedIndex = 0
                                        End If
                                    End If
                                    txtPNO.Text = dsdata.Tables(0).Rows(0)(9).ToString()

                                    'Added By Varun Moota, Tank Charts.01/24/2011
                                    If Not dsdata.Tables(0).Rows(0)(10) Is DBNull.Value Then
                                        If Not dsdata.Tables(0).Rows(0)(10).ToString().Trim() = "" Then
                                            ddlTankCharts.SelectedValue = CInt(dsdata.Tables(0).Rows(0)(10).ToString())
                                        Else
                                            ddlTankCharts.SelectedIndex = 0
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    Else
                        Label1.Text = "New Tank Screen"
                        txtTank.Text = GetMaxRecord()
                        txtName.Focus()
                        BindProduct()
                        BindTankMonitor()
                        BindTankChart()
                    End If

                    'Added by Varun Moota, if FIFO Costing Methods in use, the disable Tank Preset price.03/02/2012
                    Dim iCnt As Integer = dal.ExecuteScalarGetInteger("SELECT COUNT(*) FROM STATUS WHERE COSTING = 3")
                    If (iCnt > 0) Then
                        lblNote.Visible = True
                        txtprice.Enabled = False
                    Else
                        lblNote.Visible = False
                        txtprice.Enabled = True
                    End If



                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank_New_Edit.Page_Load()", ex)
        End Try
    End Sub

    Private Function GetMaxRecord() As String
        Dim strMax As String
        Try
            Dim dal As New GeneralizedDAL()
            strMax = dal.ExecuteScalarGetString("Use_TT_GetMaxTANK")
            If strMax = "" Then
                strMax = "001"
            Else
                strMax = strMax + 1
                strMax = String.Format("{0:000}", Convert.ToInt32(strMax))
            End If
            GetMaxRecord = strMax
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GetMaxRecord_Tank_New_Edit", ex)
            GetMaxRecord = ""
        End Try
    End Function

    Private Sub InsertData(ByVal val As String)
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(12) As SqlParameter
            Dim ParNUMBER = New SqlParameter("@NUMBER", SqlDbType.NVarChar, 3)
            Dim ParNAME = New SqlParameter("@NAME", SqlDbType.NVarChar, 25)
            Dim ParPRODUCT = New SqlParameter("@PRODUCT", SqlDbType.NVarChar, 2)
            Dim ParADDRESS = New SqlParameter("@ADDRESS", SqlDbType.NVarChar, 25)
            Dim ParPRSET_PRIC = New SqlParameter("@PRSET_PRIC", SqlDbType.Float)
            Dim ParTANK_SIZE = New SqlParameter("@TANK_SIZE", SqlDbType.Int)
            Dim ParREFILL_NOT = New SqlParameter("@REFILL_NOT", SqlDbType.Int)
            Dim ParCODE = New SqlParameter("@CODE", SqlDbType.NVarChar, 25)
            Dim ParTM_NUMBER = New SqlParameter("@TM_NUMBER", SqlDbType.NVarChar, 3)
            Dim ParTM_PROBE = New SqlParameter("@TM_PROBE", SqlDbType.NVarChar, 1)
            Dim ParFlag = New SqlParameter("@val", SqlDbType.NVarChar, 1)
            Dim parrecord = New SqlParameter("@Record", SqlDbType.Int)

            Dim parTankChart = New SqlParameter("@tank_chart", SqlDbType.Int)

            ParNUMBER.Direction = ParameterDirection.Input
            ParNAME.Direction = ParameterDirection.Input
            ParPRODUCT.Direction = ParameterDirection.Input
            ParADDRESS.Direction = ParameterDirection.Input
            ParPRSET_PRIC.Direction = ParameterDirection.Input
            ParTANK_SIZE.Direction = ParameterDirection.Input
            ParREFILL_NOT.Direction = ParameterDirection.Input
            ParCODE.Direction = ParameterDirection.Input
            ParTM_NUMBER.Direction = ParameterDirection.Input
            ParTM_PROBE.Direction = ParameterDirection.Input
            ParFlag.Direction = ParameterDirection.Input
            parrecord.Direction = ParameterDirection.Input
            parTankChart.Direction = ParameterDirection.Input
            ParNUMBER.value = txtTank.Text
            ParNAME.value = txtName.Text
            If DropProduct.SelectedIndex > 0 Then
                ParPRODUCT.value = DropProduct.SelectedValue
            Else
                ParPRODUCT.value = ""
            End If

            ParADDRESS.value = txtAddress1.Text
            If Not txtprice.Text = "" Then
                ParPRSET_PRIC.value = Convert.ToDouble(txtprice.Text)
            Else
                ParPRSET_PRIC.value = Convert.ToDouble(0.0)
            End If

            If Not txttsize.Text = "" Then
                ParTANK_SIZE.value = txttsize.Text
            Else
                ParTANK_SIZE.value = 0
            End If
            If Not txtRnotice.Text = "" Then
                ParREFILL_NOT.value = txtRnotice.Text
            Else
                ParREFILL_NOT.value = 0
            End If

            ParCODE.value = txtExCode.Text
            If DropTM.SelectedIndex > 0 Then
                ParTM_NUMBER.value = DropTM.SelectedValue
            Else
                ParTM_NUMBER.value = ""
            End If

            ParTM_PROBE.value = txtPNO.Text

            If ddlTankCharts.SelectedIndex > 0 Then
                parTankChart.value = Convert.ToInt32(ddlTankCharts.SelectedValue)
            Else
                parTankChart.value = 0
            End If

            ParFlag.value = val
            If Request.QueryString.Count > 0 Then
                parrecord.value = Convert.ToInt32(Request.QueryString.Get("RowID"))
            Else
                parrecord.value = 0
            End If

            parcollection(0) = ParNUMBER
            parcollection(1) = ParNAME
            parcollection(2) = ParPRODUCT
            parcollection(3) = ParADDRESS
            parcollection(4) = ParPRSET_PRIC
            parcollection(5) = ParTANK_SIZE
            parcollection(6) = ParREFILL_NOT
            parcollection(7) = ParCODE
            parcollection(8) = ParTM_NUMBER
            parcollection(9) = ParTM_PROBE
            parcollection(10) = ParFlag
            parcollection(11) = parTankChart
            parcollection(12) = parrecord

            dal.ExecuteSQLStoredProcedureGetBoolean("Use_tt_InsertTankDetails", parcollection)

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record has been saved successfully !!');location.href='Tank.aspx';</script>")

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("InsertData", ex)
        End Try
    End Sub

    Public Function GetallData(ByVal RowId As Integer) As DataSet
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim ParRecord = New SqlParameter("@Record", SqlDbType.Int)
            ParRecord.Direction = ParameterDirection.Input
            ParRecord.value = RowId
            parcollection(0) = ParRecord
            GetallData = dal.ExecuteStoredProcedureGetDataSet("use_tt_GetTankAllDetails", parcollection)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GetallData_Tank_New_Edit", ex)
            GetallData = Nothing
        End Try
    End Function

    Public Sub BindProduct()
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim Parval = New SqlParameter("@val", SqlDbType.NVarChar, 1)
            Parval.Direction = ParameterDirection.Input
            Parval.value = "1"
            parcollection(0) = Parval
            ds = dal.ExecuteStoredProcedureGetDataSet("use_tt_Productlist", parcollection)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then

                    DropProduct.DataSource = ds.Tables(0)
                    DropProduct.DataTextField = "ProdDtl"
                    DropProduct.DataValueField = "Number"

                    DropProduct.DataBind()
                    DropProduct.Items.Insert(0, "Select Product")
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("BindProduct_Tank_New_Edit", ex)
        End Try
    End Sub

    Public Sub BindTankMonitor()
        Try
            ds = dal.GetDataSet("use_tt_TMlist")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    DropTM.DataTextField = "TMName"
                    DropTM.DataValueField = "Number"
                    DropTM.DataSource = ds.Tables(0)
                    DropTM.DataBind()
                    DropTM.Items.Insert(0, "Select Tank Monitor")
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("BindTankMonitor_Tank_New_Edit", ex)
        End Try

    End Sub

    Protected Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If btnOK.Text.ToLower() = "save" Then
                InsertData("0")
            Else
                InsertData("1")
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank_New_Edit.btnOK_Click", ex)
        End Try


    End Sub

    Public Sub AddAttribute()
        Try
            txtprice.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
            txtprice.Attributes.Add("onkeyup", "KeyUpEvent_txtSurchage(event);")
            txtRnotice.Attributes.Add("OnKeyPress", "KeyPressNumber(event);")
            txttsize.Attributes.Add("OnKeyPress", "KeyPressNumber(event);")
            txtPNO.Attributes.Add("OnKeyPress", "KeyPressNumber(event);")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank_New_Edit.AddAttribute()", ex)
        End Try
      

    End Sub

    Private Sub BindTankChart()
        Try


            'Added By Varun Moota to Assign Tank Chart.

            Dim dal As New GeneralizedDAL()
            Dim qry As String = "SELECT CHARTNUMBER,CHARTNAME FROM CHARTS ORDER BY CHARTNUMBER"
            Dim dsTankCharts As New DataSet
            dsTankCharts = dal.GetDataSet(qry)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlTankCharts.DataTextField = "CHARTNAME"
                    ddlTankCharts.DataValueField = "CHARTNUMBER"
                    ddlTankCharts.DataSource = dsTankCharts.Tables(0)
                    ddlTankCharts.DataBind()
                    ddlTankCharts.Items.Insert(0, "Select Tank Chart")
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank_New_Edit.BindTankChart()", ex)
        End Try
    End Sub

End Class
