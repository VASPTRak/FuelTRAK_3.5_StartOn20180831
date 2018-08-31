Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Collections.Generic
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Reporting.WebControls
Imports CrystalDecisions.Shared
Partial Class ReconciliationForm
    Inherits System.Web.UI.Page
    Dim visited As Boolean
    Dim GenFun As New GeneralFunctions
    Dim ds As DataSet
    Dim dv As New DataView
    Dim dt As DataTable
    Dim DAL As GeneralizedDAL
    Dim oRpt As ReportDocument
    Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
    Dim myLogin As CrystalDecisions.Shared.TableLogOnInfo

    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                If Not Page.IsPostBack Then
                    UpdateTotalizerRecords()
                    BindGrid()
                    FillData()
                End If
                'Run Reports
                'Response.Redirect("ReportViewer.aspx,false")
                End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReconciliationForm_Page_Load()", ex)
        End Try
    End Sub
    Public Sub BindGrid()
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(1) As SqlParameter
            Dim ParDateTime = New SqlParameter("@dt", SqlDbType.NVarChar, 25)
            Dim ParTankNo = New SqlParameter("@TankNo", SqlDbType.NVarChar, 3)
            ParDateTime.Direction = ParameterDirection.Input
            ParTankNo.Direction = ParameterDirection.Input
            ParDateTime.Value = Session("StartDTRecon").ToString()
            parcollection(0) = ParDateTime
            ParTankNo.Value = Session("TNKNBR").ToString()
            parcollection(1) = ParTankNo
            ds = dal.ExecuteStoredProcedureGetDataSet("USP_TT_TANKRECON_GETDATA_DFW", parcollection)
            '''ds = dal.ExecuteStoredProcedureGetDataSet("USP_TT_TANKRECON_GETDATA_DFW_TESTING", parcollection)
            'ds = dal.ExecuteStoredProcedureGetDataSet("USP_TT_TANKRECON_GETDATA_DFW_DISPENSERS_VIEW", parcollection)

            If ds.Tables(0).Rows.Count > 0 Then
                gvReconciliation.DataSource = ds
                gvReconciliation.DataBind()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records found!');location.href='TankRecon.aspx';</script>")
            End If




        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReconciliationForm.BindGrid()", ex)
        End Try
    End Sub
    Public Sub UpdateTotalizerRecords()
        Try
            Dim DL As New GeneralizedDAL

            Dim parcollection(0) As SqlParameter
            Dim ParTankNo = New SqlParameter("@TankNo", SqlDbType.NVarChar, 3)

            ParTankNo.Direction = ParameterDirection.Input
            ParTankNo.Value = Session("TNKNBR").ToString()
            parcollection(0) = ParTankNo

            Dim blnInsertFlag As Boolean = DL.ExecuteStoredProcedureGetBoolean("usp_tt_TankRecon_DFW", parcollection)
            '''Dim blnInsertFlag As Boolean = DL.ExecuteStoredProcedureGetBoolean("usp_tt_TankRecon_DFW_TESTING", parcollection)
            

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReconciliationForm.UpdateTotalizerRecords()", ex)
        End Try



    End Sub
    Public Sub FillData()
        Try
            Dim qry As String = " Select  [NUMBER],[NAME] from Tank where [NUMBER]='" + Session("TNKNBR").ToString() + "'"
            Dim i As Integer = 0
            Dim dl As New GeneralizedDAL

            Dim dsTanKInfo As New DataSet
            dsTanKInfo = dl.GetDataSet(qry)
            If Not dsTanKInfo.Tables(0).Rows(0)("NUMBER") Is DBNull.Value Then
                txtTankNO.Text = dsTanKInfo.Tables(0).Rows(0)("NUMBER").ToString()
                txtTankLoc.Text = dsTanKInfo.Tables(0).Rows(0)("NAME").ToString()
                Session("TankTotLoc") = txtTankLoc.Text
                'Default Values
                txtTCEQOwnerID.Text = "21747"
                txtTCEQFacilityID.Text = "10457"

                'Get Month And Year.
                Dim dsDateTime As DataSet
                Dim strqry As String = "SELECT  DATENAME(mm,'" + Session("StartDTRecon") + "') as tankMonth, Datepart(yy,'" + Session("StartDTRecon").ToString() + "') as tankYear"
                dsDateTime = dl.GetDataSet(strqry)

                txtDateYear.Text = dsDateTime.Tables(0).Rows(0)("tankYear").ToString()
                Session("TankTotYear") = txtDateYear.Text

                txtMonth.Text = dsDateTime.Tables(0).Rows(0)("tankMonth").ToString()
                Session("TankTotMonth") = txtMonth.Text


                'TANK Calculations
                CalTankRecon()

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReconciliationForm.FillData()", ex)
        End Try
    End Sub
    Public Sub CalTankRecon()
        Try
            Dim dl As New GeneralizedDAL
            'Column A Previous Month Totalizer Count
            Dim qry As String = "select Top (1)  [PUMP_TOTALIZERS]  from tankTot " & _
                                "where datepart(mm,[datetime]) = datepart(mm,dateAdd(mm,-1,'" + Session("StartDTRecon").ToString() + "'))" & _
                                 "AND datepart(yy,[datetime]) =  datepart(yy,dateAdd(mm,-1,'" + Session("StartDTRecon").ToString() + "'))" & _
                                " AND TANK_NBR = '" + Session("TNKNBR").ToString() + "' order by [DateTime] desc"

            Dim dsResult As New DataSet
            Dim strPrevMntTot As String = ""
            dsResult = dl.GetDataSet(qry)

            If dsResult.Tables(0).Rows.Count > 0 Then
                strPrevMntTot = dsResult.Tables(0).Rows(0)("PUMP_TOTALIZERS").ToString()

            Else
                strPrevMntTot = 0.0
            End If
            txtPrevMntAmtCnt.Text = strPrevMntTot.ToString.Trim()
            Session("strPrevMntTot") = strPrevMntTot.ToString.Trim()
            'Column C Previous Month Tank Volume Count
            qry = "select Top (1)  CONVERT(int,isnull([TANK_LEVELS],0)) as [TANK_LEVELS]  from TankTot " & _
                    "where datepart(mm,[datetime]) = datepart(mm,dateAdd(mm,-1,'" + Session("StartDTRecon").ToString() + "'))" & _
                    "And datepart(yy,[datetime]) =  datepart(yy,dateAdd(mm,-1,'" + Session("StartDTRecon").ToString() + "'))" & _
                    "AND TANK_NBR = '" + Session("TNKNBR").ToString() + "' order by [DateTime] desc"

            Dim strPrevMntLevel As String = ""

            dsResult = dl.GetDataSet(qry)

            If dsResult.Tables(0).Rows.Count > 0 Then
                strPrevMntLevel = dsResult.Tables(0).Rows(0)("TANK_LEVELS").ToString()

            Else
                strPrevMntLevel = "0"
            End If
            txtPrevMnthLevels.Text = strPrevMntLevel.ToString.Trim()
            Session("strPrevMntLevel") = strPrevMntLevel.ToString.Trim()

            'Current Month Totalizer Count
            qry = "select Top (1)  isnull([PUMP_TOTALIZERS],0.0) as PUMP_TOTALIZERS  from TankTot " & _
                    "where datepart(mm,[datetime]) = datepart(mm,'" + Session("StartDTRecon").ToString() + "')" & _
                "And datepart(yy,[datetime]) =  datepart(yy,'" + Session("StartDTRecon").ToString() + "') AND [PUMP_TOTALIZERS] > 0" & _
                "AND TANK_NBR = '" + Session("TNKNBR").ToString() + "' order by [DateTime] desc"

            Dim strCurrentMntTot As String = ""
            dsResult = dl.GetDataSet(qry)

            If dsResult.Tables(0).Rows.Count > 0 Then
                strCurrentMntTot = dsResult.Tables(0).Rows(0)("PUMP_TOTALIZERS").ToString()

            Else
                strCurrentMntTot = 0.0
            End If

            Session("strCurrentMntTot") = strCurrentMntTot.ToString.Trim()

            txtCntColA.Text = (CDec(strCurrentMntTot) - CDec(strPrevMntTot)).ToString()
            Session("CurrTotalizer") = (CDec(strCurrentMntTot) - CDec(strPrevMntTot)).ToString()

            'Current Month DELIVERIES
            qry = "select CONVERT(int,isnull(SUM (TANK_DELIVERIES),0)) as Del  from TankTot " & _
                    "where datepart(mm,[datetime]) = datepart(mm,'" + Session("StartDTRecon").ToString() + "') " & _
                    "And datepart(yy,[datetime]) =  datepart(yy,'" + Session("StartDTRecon").ToString() + "') AND TANK_NBR = '" + Session("TNKNBR").ToString() + "'"

            Dim strCurrentMntDel As String = ""
            dsResult = dl.GetDataSet(qry)

            If dsResult.Tables(0).Rows.Count > 0 Then
                strCurrentMntDel = dsResult.Tables(0).Rows(0)("Del").ToString()
            Else
                strCurrentMntDel = "0"
            End If
            txtCntColB.Text = strCurrentMntDel.ToString.Trim()
            Session("strCurrentMntDel") = strCurrentMntDel.ToString.Trim()
            'Current Month Levels
            ' ''qry = "select TOP 1 CONVERT(int,isnull((TANK_LEVELS),0)) as Levels  from TankTot where datepart(mm,[datetime]) = datepart(mm,'" + Session("StartDTRecon").ToString() + "')" & _
            ' ''        "And datepart(yy,[datetime]) =  datepart(yy,'" + Session("StartDTRecon").ToString() + "') AND TANK_NBR = '" + Session("TNKNBR").ToString() + "' " & _
            ' ''        "AND (Convert(varchar, TankTot.[datetime], 101) < Convert(varchar, DateAdd(d, 1, GETDATE()), 101)) Order By [DATETIME] DESC "
            qry = "select TOP 1 CONVERT(int,isnull((TANK_LEVELS),0)) as Levels  from TankTot where datepart(mm,[datetime]) = datepart(mm,'" + Session("StartDTRecon").ToString() + "')" & _
                    "And datepart(yy,[datetime]) =  datepart(yy,'" + Session("StartDTRecon").ToString() + "') AND TANK_NBR = '" + Session("TNKNBR").ToString() + "' " & _
                    "AND (TankTot.[datetime] < Convert(varchar, DateAdd(d, 1, GETDATE()), 101)) Order By [DATETIME] DESC "
            Dim strCurrentMntLevels As String = ""
            dsResult = dl.GetDataSet(qry)

            If dsResult.Tables(0).Rows.Count > 0 Then
                strCurrentMntLevels = dsResult.Tables(0).Rows(0)("Levels").ToString()

            Else
                strCurrentMntLevels = "0"
            End If
            txtCntColC.Text = strCurrentMntLevels.ToString.Trim()
            Session("strCurrentMntLevels") = strCurrentMntLevels.ToString.Trim()


            TotalsNow()

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReconciliationForm.CalTankRecon()", ex)
        End Try

    End Sub

    Public Sub TotalsNow()
        Try

            'Line 1 Totals
            txtLine1C1.Text = Session("strPrevMntLevel").ToString()
            txtLine1C2.Text = Session("CurrTotalizer").ToString()
            txtLine1C3.Text = Session("strCurrentMntDel").ToString()
            'txtLine1C4.Text = (Decimal.Parse(txtLine1C1.Text.Trim()) - (Decimal.Parse(txtLine1C2.Text.Trim()) + Decimal.Parse(txtLine1C3.Text.Trim()))).ToString()
            txtLine1C4.Text = ((Decimal.Parse(txtLine1C1.Text.Trim()) - Decimal.Parse(txtLine1C2.Text.Trim())) + Decimal.Parse(txtLine1C3.Text.Trim())).ToString()
            Session("cntLine1") = txtLine1C4.Text


            'Line 2 Totatls
            txtLine2C1.Text = Session("CurrTotalizer").ToString()
            txtLine2C3.Text = (CDec(txtLine2C1.Text.Trim()) * 0.01 + 130).ToString()
            Session("cntLine2") = txtLine2C3.Text


            'Line 3 Totals
            txtLine3Col1.Text = Session("strCurrentMntLevels").ToString()
            txtLine3Col2.Text = Session("cntLine2").ToString()
            txtLine3Col3.Text = (CDec(txtLine3Col1.Text.Trim()) + CDec(txtLine3Col2.Text.Trim())).ToString()
            Session("cntLine3") = txtLine3Col3.Text

            'Line 4 Totals
            txtLine4Col1.Text = Session("strCurrentMntLevels").ToString()
            txtLine4Col2.Text = Session("cntLine2").ToString()
            txtLine4Col3.Text = (CDec(txtLine4Col1.Text.Trim()) - CDec(txtLine4Col2.Text.Trim())).ToString()
            Session("cntLine4") = txtLine4Col3.Text

            'Line 5 Totals
            txtLine5Col1.Text = Session("cntLine1").ToString()
            txtLine5Col2.Text = Session("cntLine3").ToString()
            txtLine5Col3.Text = (IIf(Double.Parse(txtLine5Col1.Text.Trim) < Double.Parse(txtLine5Col2.Text.Trim) = True, "YES", "NO")).ToString
            Session("cntLine5") = txtLine5Col3.Text

            'Line 6 Totals
            txtLine6Col1.Text = Session("cntLine1").ToString()
            txtLine6Col2.Text = Session("cntLine4").ToString()
            txtLine6Col3.Text = (IIf(Double.Parse(txtLine6Col1.Text.Trim) > Double.Parse(txtLine6Col2.Text.Trim) = True, "YES", "NO")).ToString
            Session("cntLine6") = txtLine6Col3.Text

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReconciliationForm.TotalsNow()", ex)
        End Try
    End Sub
   
    Public Sub TankRecon()
        Try
            Dim ds As DataSet
            Dim ds1 As DataSet
            Dim SBal As Double = 0, EBal As Double = 0, DelBal As Double = 0

            'Added By Varun Moota to show TRAK Quantity Count 
            Dim TRAKCnt As Double = 0
            Dim i As Integer = 0

            'Added By varun Moota to Test DateTime in Reports.08/24/2010
            Dim dsDateTime As DataSet
            Dim dl As New GeneralizedDAL
            Dim strqry As String = "SELECT  dateadd(d,-1,GETDATE()) as StartDate, GETDATE() as EndDate"
            dsDateTime = dl.GetDataSet(strqry)
            Dim dtStartDT As DateTime = dsDateTime.Tables(0).Rows(0)("StartDate") '"08-01-2010"
            Dim dtEndDT As DateTime = dsDateTime.Tables(0).Rows(0)("EndDate") '"08-02-2010"



            Dim parcollection(0) As SqlParameter
            Dim ParTankVal = New SqlParameter("@tankNo", SqlDbType.NVarChar, 3)
            ParTankVal.Direction = ParameterDirection.Input
            ParTankVal.Value = Session("TNKNBR").ToString()
            parcollection(0) = ParTankVal
            ds = DAL.ExecuteStoredProcedureGetDataSet("usp_tt_TankList_DFW", parcollection)

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        SBal = 0 : DelBal = 0 : EBal = 0

                        'Starting Balance

                        Dim qry As String = " Select  top(1)  [datetime] ,qty_meas as [Starting Balance] from FRTD " & _
                                " where  (entry_type = 'D' or entry_type = 'L') AND " & _
                        "FRTD.TANK_NBR ='" + Session("TNKNBR").ToString() + "' AND convert(varchar, [datetime], 110) >'" + dtStartDT.ToString("MM-dd-yyyy") + "'"
                        ds1 = DAL.GetDataSet(qry)
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                SBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
                            End If
                        End If

                        'Delivery Balance
                        qry = "Select isnull(sum(qty_added),0) as [Receipts] from FRTD where  (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + Session("TNKNBR").ToString() + "' AND  convert(varchar, [datetime], 110) between convert(nvarchar(20),'" + dtStartDT.ToString("MM-dd-yyyy") + "',110) AND convert(nvarchar(20),'" + dtEndDT.ToString("MM-dd-yyyy") + "',110)"
                        ds1 = DAL.GetDataSet(qry)
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                DelBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(0))
                            End If
                        End If

                        'Ending Balance
                        qry = "Select top(1)  [datetime] ,qty_meas as [ending Balance] from FRTD where  (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(varchar, [datetime], 110) > '" + dtEndDT.ToString("MM-dd-yyyy") + "'  order by [datetime] desc"
                        ds1 = DAL.GetDataSet(qry)
                        If ds1.Tables(0).Rows.Count > 0 Then
                            If Not ds1 Is Nothing Then
                                If ds1.Tables(0).Rows.Count > 0 Then
                                    EBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))

                                End If
                            End If
                        End If

                        'Added By Varun Moota to Show Trak Calculation in Tank Reconciliation Report. 12/15/2009
                        TRAKCnt = 0




                        qry = "Select Sum(Quantity) as TRAKCnt from TXTN,tank  WHere  TXTN.Tank = Tank.Number And (txtn.[Datetime] between '" + dtStartDT.ToString("MM/dd/yyyy") + "'AND '" + dtEndDT.ToString("MM/dd/yyyy") + "' ) AND TANK = '" + Session("TNKNBR").ToString() + "' Group By TXTN.Tank "
                        ds1 = DAL.GetDataSet(qry)
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                If i <= ds1.Tables(0).Rows.Count - 1 Then
                                    'For i = 0 To ds1.Tables(0).Rows.Count - 1
                                    TRAKCnt = CDec(ds1.Tables(0).Rows(i)("TRAKCnt").ToString())
                                    'TRAKCnt = Convert.ToDouble(ds1.Tables(0).Rows(0)(0))
                                    'Next
                                End If

                            End If
                        End If

                    Next
                End If
            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReconciliationForm.TankRecon()", ex)
        End Try
    End Sub
    Public Function GetCustomMadeDataTable() As System.Data.DataTable
        Dim ds As DataSet
        Dim ds1 As DataSet
        Dim SBal As Double = 0, EBal As Double = 0, DelBal As Double = 0

        'Added By Varun Moota to show TRAK Quantity Count 
        Dim TRAKCnt As Double = 0

        'Added By varun Moota to Test DateTime in Reports.08/24/2010
        Dim dsDateTime As DataSet
        Dim dl As New GeneralizedDAL
        Dim strqry As String = "SELECT  Convert(nvarchar,GETDATE(),101) as StartDate, Convert(nvarchar,GETDATE(),101) as EndDate"
        dsDateTime = dl.GetDataSet(strqry)
        Dim dtStartDT As DateTime = dsDateTime.Tables(0).Rows(0)("StartDate") '"08-01-2010"
        Dim dtEndDT As DateTime = dsDateTime.Tables(0).Rows(0)("EndDate") '"08-02-2010"

        Dim i As Integer = 0
        DAL = New GeneralizedDAL
        Try
            Dim strQuery As String = ""
            'Create a new DataTable object
            Dim objDataTable As New System.Data.DataTable
            'Create three columns with string as their type
            objDataTable.Columns.Add("TankNo", String.Empty.GetType())
            objDataTable.Columns.Add("TankName", String.Empty.GetType())
            objDataTable.Columns.Add("Product", String.Empty.GetType())
            objDataTable.Columns.Add("StartBal", String.Empty.GetType())
            objDataTable.Columns.Add("EndBal", String.Empty.GetType())
            objDataTable.Columns.Add("DelBal", String.Empty.GetType())

            'Added By Varun Moota to Show Trak Calculation in Tank Reconciliation Report. 12/15/2009
            objDataTable.Columns.Add("TRAKCnt", String.Empty.GetType())

            'Added By varun Moota To Test DT in Reconciliation Reports.08/24/2010
            objDataTable.Columns.Add("dtStartDT", DateTime.Now.GetType())
            objDataTable.Columns.Add("dtEndDT", DateTime.Now.GetType())



            Dim parcollection(0) As SqlParameter
            Dim ParTankVal = New SqlParameter("@tankNo", SqlDbType.NVarChar, 3)
            ParTankVal.Direction = ParameterDirection.Input
            ParTankVal.Value = "001"
            parcollection(0) = ParTankVal
            ds = DAL.ExecuteStoredProcedureGetDataSet("usp_tt_TankList_DFW", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        SBal = 0 : DelBal = 0 : EBal = 0

                        'Starting Balance

                        Dim qry As String = " Select  top(1)  [datetime] ,qty_meas as [Starting Balance] from FRTD " & _
                                " where  (entry_type = 'D' or entry_type = 'L') AND " & _
                        "FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(varchar, [datetime], 110) >'" + dtStartDT.ToString("MM-dd-yyyy") + "'"
                        ds1 = DAL.GetDataSet(qry)
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                SBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
                            End If
                        End If

                        'Delivery Balance

                        qry = "Select isnull(sum(qty_added),0) as [Receipts] from FRTD where  (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND  convert(varchar, [datetime], 110) between convert(nvarchar(20),'" + dtStartDT.ToString("MM-dd-yyyy") + "',110) AND convert(nvarchar(20),'" + dtEndDT.ToString("MM-dd-yyyy") + "',110)"
                        ds1 = DAL.GetDataSet(qry)
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                DelBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(0))
                            End If
                        End If

                        'End Balance
                        'Commeneted By varun Moota.08/24/2010

                        qry = "Select top(1)  [datetime] ,qty_meas as [ending Balance] from FRTD where  (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(varchar, [datetime], 110) > '" + dtEndDT.ToString("MM-dd-yyyy") + "'  order by [datetime] desc"
                        ds1 = DAL.GetDataSet(qry)
                        If ds1.Tables(0).Rows.Count > 0 Then
                            If Not ds1 Is Nothing Then
                                If ds1.Tables(0).Rows.Count > 0 Then
                                    EBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))

                                End If
                            End If
                        Else
                            qry = "Select top(1)  [datetime] ,qty_meas as [ending Balance] as ROWSAffected from FRTD where  (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(varchar, [datetime], 110) between convert(nvarchar(20),'" + dtStartDT.ToString("MM-dd-yyyy") + "',110) AND convert(nvarchar(20),'" + dtEndDT.ToString("MM-dd-yyyy") + "',110) order by [datetime] desc"
                            If Not ds1 Is Nothing Then
                                If ds1.Tables(0).Rows.Count > 0 Then
                                    EBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))

                                End If
                            End If
                        End If


                        'Added By Varun Moota to Show Trak Calculation in Tank Reconciliation Report. 12/15/2009
                        TRAKCnt = 0
                        'ADDED Varun Moota
                        qry = "Select Sum(Quantity) as TRAKCnt from TXTN,tank  WHere  TXTN.Tank = Tank.Number And (txtn.[Datetime] between '" + dtStartDT.ToString("MM/dd/yyyy") + "'AND '" + dtEndDT.ToString("MM/dd/yyyy") + "' ) AND TANK = '" + Session("TNKNBR").ToString() + "' Group By TXTN.Tank "
                        ds1 = DAL.GetDataSet(qry)
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                If i <= ds1.Tables(0).Rows.Count - 1 Then
                                    'For i = 0 To ds1.Tables(0).Rows.Count - 1
                                    TRAKCnt = CDec(ds1.Tables(0).Rows(i)("TRAKCnt").ToString())
                                    'TRAKCnt = Convert.ToDouble(ds1.Tables(0).Rows(0)(0))
                                    'Next
                                End If

                            End If
                        End If


                        If (Not SBal = 0.0 Or Not DelBal = 0.0 Or Not EBal = 0.0) Then

                            Session("TNKNBR") = ds.Tables(0).Rows(i)("NUMBER").ToString()
                            'Adding some data in the rows of this DataTable
                            'objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("NUMBER").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("PRODUCTNAME").ToString(), SBal, EBal, DelBal, TRAKCnt})
                            objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("NUMBER").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("PRODUCTNAME").ToString(), SBal, EBal, DelBal, TRAKCnt, dtStartDT, dtEndDT})
                        End If


                        'Added By Varun Moota to INSERT TANK Totalizers

                        Dim sTotValue As Double = SBal + DelBal - EBal



                        qry = " SELECT TOP(1) SUM(PUMP_TOTALIZERS) as PumpTot FROM TANKTOT" & _
                                "  where(Convert(varchar, [DateTime], 110) = Convert(varchar, DateAdd(d, -1, GETDATE()), 110))GROUP BY [DATETIME]"
                        ds1 = DAL.GetDataSet(qry)

                        Dim sPumpTotValue As Double = ds1.Tables(0).Rows(i)("PumpTot")

                        Dim sPumpTotalizerValue As Double = TRAKCnt + sPumpTotValue
                        Dim dtTankTot As DateTime = DateTime.Now.ToShortDateString

                        'Check Any Totalizer Reading Exists on the Datetime.
                        Dim blnDTFLag As Boolean = CheckDT(dtTankTot)
                        If blnDTFLag Then
                            UpdateTankTotRecords(Session("TNKNBR").ToString(), dtStartDT, sPumpTotalizerValue, DelBal, sPumpTotValue, TRAKCnt, "true")
                        Else
                            'InsertUpdateData(Session("TNKNBR").ToString(), DateTime.Now.ToShortDateString, sPumpTotalizerValue, DelBal, sPumpTotValue, TRAKCnt, "false")
                        End If





                    Next
                End If
            End If
            Return objDataTable
        Catch ex As Exception

            Dim cr As New ErrorPage
            cr.errorlog("ReconciliationForm.GetCustomMadeDataTable()", ex)
        End Try
    End Function
    Public Sub UpdateTankTotRecords(ByVal TankNo As String, ByVal dt As DateTime, ByVal tot As Decimal, ByVal del As Decimal, ByVal levels As Decimal, ByVal TRAKCnt As Decimal, ByVal RecordFlag As String)
        Try


            Dim dal = New GeneralizedDAL()
            Dim parcollection(6) As SqlParameter
            Dim parTankNo = New SqlParameter("@TankNo", SqlDbType.NVarChar, 3)
            Dim parDT = New SqlParameter("@DT", SqlDbType.DateTime)
            Dim parTTot = New SqlParameter("@TTot", SqlDbType.Decimal)
            Dim parTDel = New SqlParameter("@TDel", SqlDbType.Decimal)
            Dim parTLevels = New SqlParameter("@TLevels", SqlDbType.Decimal)
            Dim parTrakCnt = New SqlParameter("@TrakCNT", SqlDbType.Decimal)
            Dim parRecordFlag = New SqlParameter("@RecordFlag", SqlDbType.NVarChar, 10)

            parTankNo.Direction = ParameterDirection.Input
            parDT.Direction = ParameterDirection.Input
            parTTot.Direction = ParameterDirection.Input
            parTDel.Direction = ParameterDirection.Input
            parTLevels.Direction = ParameterDirection.Input
            parTrakCnt.Direction = ParameterDirection.Input
            parRecordFlag.direction = ParameterDirection.Input

            parTankNo.value = TankNo
            parDT.value = dt
            parTTot.value = tot
            parTDel.value = del
            parTLevels.value = levels
            parTrakCnt.value = TRAKCnt
            parRecordFlag.value = RecordFlag

            parcollection(0) = parTankNo
            parcollection(1) = parDT
            parcollection(2) = parTTot
            parcollection(3) = parTDel
            parcollection(4) = parTLevels
            parcollection(5) = parTrakCnt
            parcollection(6) = parRecordFlag


            Dim blnInsertFlag As Boolean = dal.ExecuteStoredProcedureGetBoolean("usp_tt_TankRecon_DFW", parcollection)


        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ReconciliationForm.InsertData()", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub
    Private Function CheckDT(ByVal dtTotalizer As DateTime) As Boolean
        Try


            Dim dsFlag As DataSet
            Dim qry As String = " Select  [datetime]  from TankTot " & _
                                    " where  TankTot.[Datetime] = '" + dtTotalizer.ToString("MM/dd/yyyy") + "' "
            dsFlag = DAL.GetDataSet(qry)
            If dsFlag.Tables(0).Rows.Count > 0 Then


                Return True

            End If
            Return False
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ReconciliationForm.CheckDT()", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Function


    

    Protected Sub btnRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRunReport.Click
        Try

            Dim GenFun As New GeneralFunctions
            Dim Uinfo As New UserInfo

            Uinfo.ReportHeader = "INVENROTY RECONCILIATION FORM"
            Uinfo.ReportID = 201
            Dim strQuery As String = GenFun.Report(Convert.ToInt32(Uinfo.ReportID.ToString()), Uinfo)

            Session("Uinfo") = Uinfo
            Dim url As String = "ReportViewer.aspx"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Run_Report('" & url & "');</script>")

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.btnRunReport_Click", ex.InnerException)
        End Try
    End Sub

    Protected Sub gvReconciliation_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvReconciliation.RowCancelingEdit
        Try
            gvReconciliation.EditIndex = -1
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.gvReconciliation_RowCancelingEdit", ex.InnerException)
        End Try
        
    End Sub

    Protected Sub gvReconciliation_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvReconciliation.RowEditing
        Try

            gvReconciliation.EditIndex = e.NewEditIndex
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.gvReconciliation_RowEditing", ex.InnerException)
        End Try
    End Sub

    Protected Sub gvReconciliation_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvReconciliation.RowUpdating

        Try


            Dim txtGVPumpTot As TextBox = CType(gvReconciliation.Rows(e.RowIndex).FindControl("txtGVPumpTot"), TextBox)
            Dim txtGVPumpDay As Label = CType(gvReconciliation.Rows(e.RowIndex).FindControl("lblDT"), Label)

            Dim dal = New GeneralizedDAL()

            Dim dsDateTime As DataSet
            Dim strqry As String = "SELECT  DATEPART(mm,'" + Session("StartDTRecon") + "') as tankMonth, Datepart(yy,'" + Session("StartDTRecon").ToString() + "') as tankYear"
            dsDateTime = dal.GetDataSet(strqry)
            Dim imonth As String = dsDateTime.Tables(0).Rows(0)("tankMonth").ToString()
            Dim iYear As String = dsDateTime.Tables(0).Rows(0)("tankYear").ToString()

            Dim dtStr As Date = imonth + "/" + txtGVPumpDay.Text + "/" + iYear
            UpdateGridValues(dtStr, Session("TNKNBR").ToString(), txtGVPumpTot.Text)


            gvReconciliation.EditIndex = -1
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.gvReconciliation_RowUpdating", ex.InnerException)
        End Try
    End Sub

    Public Sub UpdateGridValues(ByVal dt As DateTime, ByVal tankNbr As String, ByVal TankTot As Decimal)
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(2) As SqlParameter
            Dim parTankNo = New SqlParameter("@TankNo", SqlDbType.NVarChar, 3)
            Dim parDT = New SqlParameter("@Datetime", SqlDbType.DateTime)
            Dim parTTot = New SqlParameter("@PumpTot", SqlDbType.Decimal)

            parTankNo.Direction = ParameterDirection.Input
            parDT.Direction = ParameterDirection.Input
            parTTot.Direction = ParameterDirection.Input

            parTankNo.value = tankNbr
            parDT.value = dt
            parTTot.value = TankTot


            parcollection(0) = parTankNo
            parcollection(1) = parDT
            parcollection(2) = parTTot




            Dim blnInsertFlag As Boolean = dal.ExecuteStoredProcedureGetBoolean("usp_tt_UpdateTankTotalizers_DFW", parcollection)



        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.UpdateGridValues()", ex.InnerException)
        End Try
    End Sub

  
End Class
