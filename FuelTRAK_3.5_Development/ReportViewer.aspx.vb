Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Reporting.WebControls
Imports CrystalDecisions.Shared
'Imports CrystalDecisions.Web.CrystalReportViewer
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.IO
Imports System.Data.OleDb
Imports System.Collections.Generic

Partial Class ReportViewer
    Inherits System.Web.UI.Page
    Dim oRpt As ReportDocument
    Dim MyNull As System.DBNull
    Dim GenFun As GeneralFunctions
    Dim dt As DataTable
    Dim DAL As GeneralizedDAL
    Dim myTable As CrystalDecisions.CrystalReports.Engine.Table
    Dim myLogin As CrystalDecisions.Shared.TableLogOnInfo

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'CRViewer.HasDrillUpButtonTab = False
            'CRViewer.HasToggleParameterPanelButton = False
            CRViewer.HasToggleGroupTreeButton = False
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
                Exit Sub
            End If
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            Select Case Uinfo.ReportID
                Case 2
                    TranslationReport()
                Case 81, 82, 83, 84, 85, 88
                    MiscReports()
                    'Added By Varun Moota To RUN DFW Reports.10/04/2010
                Case 201
                    TankTotReports()
                Case 202
                    TankTot2Reports()
                    'Tank Chart Report. Varun Moota 03/16/2011
                Case 301
                    TankChartReport()
                    'Polling Results Report. Varun Moota 06/16/2011
                Case 302
                    PollingResultsReport()
                Case Else
                    ConfigureCrystalReports()
            End Select
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ReportViewer_Page_Load", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString() : errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Sub ConfigureCrystalReports()
        Try
            GenFun = New GeneralFunctions
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            DAL = New GeneralizedDAL
            Dim ds = New DataSet, ds1 = New DataSet
            Dim objDataTable As New System.Data.DataTable

            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
                Exit Sub
            End If

            If (Not Uinfo Is MyNull) Then
                oRpt = New ReportDocument()
                oRpt.Load(Server.MapPath(GenFun.LoadReport(Uinfo.ReportID)))

                oRpt.SummaryInfo.ReportAuthor = Uinfo.ReportHeader
                oRpt.SummaryInfo.ReportTitle = Uinfo.ReportTitle
                oRpt.SummaryInfo.ReportComments = Uinfo.ReportComment

                'Added BY Varun to Test Logon Error while loading Report's. 12/30/2009
                LogON(oRpt)
                Dim strQuery As String = ""
                If Not Session("ReportInputs") Is Nothing Then
                    strQuery = Session("ReportInputs").ToString()
                End If
                Select Case Uinfo.ReportID
                    Case 11, 104, 12, 13, 14, 15, 16, 21, 22, 70, 23, 31, 32, 33, 61, 62, 63, 401, 402, 403
                        strQuery.Replace("dept", "vehs.dept")
                        Select Case Uinfo.ReportID
                            Case 70
                                strQuery.Replace("dept", "pers.dept")
                        End Select
                        'for transaction reports, set the order condition
                        Select Case Uinfo.ReportID
                            Case 11, 104, 61, 402, 62 : strQuery += " ORDER BY TXTN.DATETIME"
                            Case 12 : strQuery += " ORDER BY TXTN.SENTRY,TXTN.DATETIME"
                            Case 13 : strQuery += " ORDER BY TXTN.PERSONNEL,TXTN.DATETIME"
                            Case 14 : strQuery += " ORDER BY TXTN.VEHICLE,TXTN.DATETIME"
                            Case 15 : strQuery += " ORDER BY TXTN.SENTRY,TXTN.DATETIME"
                            Case 401 : strQuery += " ORDER BY TXTN.DEPT,TXTN.DATETIME"
                        End Select
                    Case 131
                        strQuery.Replace("dept", "Pers.dept")
                End Select
                Dim parcollection(2) As SqlParameter
                Dim Parstartdate = New SqlParameter("@startdate", SqlDbType.DateTime)
                Dim Parenddate = New SqlParameter("@enddate", SqlDbType.DateTime)
                Dim ParCondition = New SqlParameter("@Condition", SqlDbType.NVarChar, 1500)
                Parstartdate.Direction = ParameterDirection.Input
                Parenddate.Direction = ParameterDirection.Input
                ParCondition.Direction = ParameterDirection.Input
                Parstartdate.Value = Convert.ToDateTime(CDate(Uinfo.StartDate))
                Parenddate.Value = Convert.ToDateTime(CDate(Uinfo.EndDate))
                If Uinfo.ReportID = 1 Then ParCondition.Value = "" Else ParCondition.Value = strQuery
                'Added By Varun Moota To Test ParCollection
                Dim parcollSARPT(1) As SqlParameter
                parcollSARPT(0) = Parstartdate
                parcollSARPT(1) = Parenddate
                parcollection(0) = Parstartdate
                parcollection(1) = Parenddate
                parcollection(2) = ParCondition
                ds = New DataSet
                Select Case Uinfo.ReportID
                    Case 1
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_ExportSummReport", parcollection)
                        'Case 402 ' New Report for USP_TT_TRANSACTION_REPORT_ExceedMiles
                        'Case 403 ' New Report for Transaction Detail – MPG out-of-range
                    Case 11, 12, 13, 14, 17, 402, 403
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_TRANSACTION_REPORT", parcollection)
                    Case 104
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_TRANSACTION_Customized_REPORT", parcollection)
                    Case 401 ' New Report for Gainesville(Transaction Report by Dept)
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_TRANSACTION_REPORT_BY_DEPT", parcollection)
                    Case 15, 16 'For Transaction List OF Errors
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_TRANSErrorList_REPORT", parcollection)
                    Case 21, 22
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_SentryDateTime_REPORT", parcollection)
                    Case 23
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_SentryVehOrd_REPORT", parcollection)
                    Case 31, 32, 33, 61, 62
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_MainBilling_REPORT", parcollection)
                        If Uinfo.ReportID = 62 Then ds = GetMileage(ds)
                    Case 131
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_PerDeptMainBilling_REPORT", parcollection)
                        If Uinfo.ReportID = 62 Then ds = GetMileage(ds)
                    Case 70
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_DeptPersMainBilling_REPORT", parcollection)
                    Case 45
                        ds = DAL.ExecuteStoredProcedureGetDataSet("usp_tt_VehPMReport")
                    Case 46
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VEH_Performance_REPORT", parcollection)
                    Case 50
                        dt = GetVEHSMPGDeviationReport()
                    Case 63
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_FUELUSEDEPT_REPORT", parcollection)
                        'New Report created for Vallecitos(Fuel Use by Type).11/19/2010
                    Case 64
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_Fuel_Use_Report_Type", parcollection)
                        'New Report created for Vallecitos(Fuel Use by VehDetail).11/19/2010
                    Case 65
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_Fuel_Use_Report_VehDetail", parcollection)
                        'New Report for ISD(Fuel Use By Personnel/Dept).03/10/2011
                    Case 66
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_Fuel_Use_Report_PersDept", parcollection)
                    Case 75
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_InvPercentUsageReport", parcollection)
                    Case 77
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_InvTankBalanceReport_FIFO", parcollection)
                    Case 78
                        dt = InventotyInformationNoFuelTxtn(Uinfo)
                    Case 79
                        dt = TankCurrentBalReport(Uinfo)
                    Case 91
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehListHistorySummary", parcollection)
                    Case 101 'Added By Varun Moota 12/07/2009
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_SiteAnalysisSummary_REPORT", parcollSARPT)
                End Select
                Select Case Uinfo.ReportID
                    Case 1
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SummaryInfo.ReportTitle = "Export Transaction Summary"
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                                oRpt.SummaryInfo.ReportComments = "From " + Format(Convert.ToDateTime(Session("StartDT")), "MM/dd/yyyy HH:mm") + " To " + Format(Convert.ToDateTime(Session("EndDT")), "MM/dd/yyyy HH:mm")
                                GenFun.SetCurrentValuesForParameterField(oRpt, "OldTransactionDate", Format(Convert.ToDateTime(Session("LastDate")), "MM/dd/yyyy HH:mm"))
                                GenFun.SetCurrentValuesForParameterField(oRpt, "NewTransactionDate", Format(Convert.ToDateTime(Session("ExportDate")), "MM/dd/yyyy HH:mm"))
                                GenFun.SetCurrentValuesForParameterField(oRpt, "TotTransactions", Session("RecordCount").ToString())
                                Page.Title = "Export Summary Report"
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 11, 104, 12, 13, 14, 15, 16, 17, 31, 70, 32, 33, 401, 402, 403, 61, 131
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                                Select Case Uinfo.ReportID
                                    Case 11, 104, 12, 13, 14, 22, 33, 401, 402, 403, 61
                                        'Sub Report 
                                        ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPRODUCT")
                                        Dim i As Integer = 1
                                        Dim str As String = String.Empty
                                        Dim dsProd As New DataSet

                                        dsProd = DAL.GetDataSet("Select * FROM PRODUCT")

                                        If dsProd.Tables(0).Rows.Count > 0 Then
                                            Dim icount As Int32 = dsProd.Tables(0).Rows.Count

                                            If (Uinfo.ReportID = 14) Then
                                                For i = 1 To icount
                                                    str = ds1.Tables(0).Rows(i - 1).Item(0)
                                                    GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), ""))
                                                Next

                                                For j As Integer = icount + 1 To 16
                                                    GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(j), "Not In Use")
                                                Next
                                            ElseIf (Uinfo.ReportID = 61) Then
                                                For i = 1 To icount
                                                    str = ds1.Tables(0).Rows(i - 1).Item(0)
                                                    GenFun.SetCurrentValuesForParameterField(oRpt, "Pd" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), ""))
                                                Next

                                                For j As Integer = icount + 1 To 16
                                                    GenFun.SetCurrentValuesForParameterField(oRpt, "Pd" + CStr(j), "Not In Use")
                                                Next

                                                ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPUMP")
                                                Dim iCnt As Integer = 1
                                                iCnt = ds1.Tables(0).Rows.Count
                                                For i = 1 To 8 '''ds1.tables(0).rows.count
                                                    If (iCnt < i) Then
                                                        GenFun.SetCurrentValuesForParameterField(oRpt, "hs" + CStr(i), i.ToString())
                                                    Else
                                                        str = ds1.Tables(0).Rows(i - 1).Item(0)
                                                        GenFun.SetCurrentValuesForParameterField(oRpt, "hs" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "0"))
                                                    End If
                                                Next
                                            Else
                                                For i = 1 To icount
                                                    str = ds1.Tables(0).Rows(i - 1).Item(0)
                                                    GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), ""))
                                                Next

                                                For j As Integer = icount + 1 To 16
                                                    GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(j), "Not In Use")
                                                Next

                                                
                                            End If



                                        Else
                                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Tanks are Available.');location.href='Reports.aspx';</script>")
                                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                                        End If

                                        'Commeneted By Varun Moota. 02/18/2010
                                        'For i = 1 To 5 '16 Changed By Varun Moota
                                        '    str = ds1.Tables(0).Rows(i - 1).Item(0)
                                        '    GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "Not Available"))
                                        'Next

                                    Case 31
                                        Dim dsPD As New DataSet
                                        dsPD = DAL.ExecuteStoredProcedureGetDataSet("SP_MainBilling_REPORT_TotProd", parcollection)
                                        Dim RptPD As New ReportDocument   'use generic ReportDocument
                                        RptPD = oRpt.OpenSubreport("MainBilling_REPORT_TotProd.rpt")
                                        dsPD.Tables(0).TableName = "MainBilling_REPORT_TotProd"

                                        Dim strSql As String = "SELECT coalesce (TXTN.PRODUCT,'') as Product, product.name AS ProdName, SUM(TXTN.QUANTITY) AS TQTY, SUM(TXTN.COST) AS TCost, coalesce(SUM(DEPT.SURCHARGE),0) AS TSurcharge,coalesce(SUM(DEPT.SURCHARGEPERGALLON),0) AS TSurchargePERGALLON,Count(TXTN.PERSONNEL) as TXTXCnt FROM TXTN LEFT OUTER JOIN Vehs ON TXTN.VEHICLE = Vehs.[IDENTITY] LEFT OUTER JOIN PERS ON TXTN.PERSONNEL = PERS.[IDENTITY] LEFT OUTER JOIN product ON TXTN.PRODUCT = product.number LEFT OUTER JOIN DEPT ON Vehs.DEPT = DEPT.NUMBER "
                                        strSql = strSql + " WHERE TXTN.[DATETIME] BETWEEN '" + Convert.ToDateTime(CDate(Uinfo.StartDate)) + "' AND '" + Convert.ToDateTime(CDate(Uinfo.EndDate)) + "'"
                                        strSql = strSql + strQuery + " GROUP BY dbo.TXTN.PRODUCT, dbo.product.name ORDER BY txtn.product "
                                        Dim dtb As New DataTable
                                        Using cnn As New SqlConnection(IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString()))
                                            cnn.Open()
                                            Using dad As New SqlDataAdapter(strSql, cnn)
                                                dad.Fill(dtb)
                                            End Using
                                            cnn.Close()
                                        End Using
                                        RptPD.SetDataSource(dtb)

                                        Dim RptDept As New ReportDocument
                                        RptDept = oRpt.OpenSubreport("MainBilling_REPORT_ToDept.rpt")
                                        Dim strSqlDept As String = "select  Dept + ' ' + min(isnull(DEPTNAME,'')) as ProdName,Sum(isnull(Miles,0) - isnull(PREV_MILES,0)) As TQTY , '' as Product, 0 as TCost , 0 TSurcharge, 0 AS TSurchargePERGALLON,0 as TXTXCnt from (SELECT [TXTN].[MILES],ISNULL([TXTN].[COST],'0') AS COST,ISNULL([TXTN].[PREV_MILES],'0') AS PREV_MILES,isnull([VEHS].[DEPT],'TAG') as Dept,[DEPT].[NAME] AS DEPTNAME FROM [dbo].[TXTN] LEFT OUTER JOIN [dbo].[vehs] ON TXTN.VEHICLE =  VEHS.[IDENTITY] LEFT OUTER JOIN [dbo].[PERS] ON TXTN.PERSONNEL = PERS.[IDENTITY] LEFT OUTER JOIN [dbo].[PRODUCT] ON TXTN.PRODUCT = PRODUCT.NUMBER LEFT OUTER JOIN [dbo].DEPT ON VEHS.DEPT = DEPT.NUMBER LEFT OUTER JOIN TANK ON TXTN.PRODUCT = TANK.PRODUCT AND TXTN.SENTRY = TANK.NUMBER "
                                        strSqlDept = strSqlDept + " WHERE TXTN.[DATETIME] BETWEEN '" + Convert.ToDateTime(CDate(Uinfo.StartDate)) + "' AND '" + Convert.ToDateTime(CDate(Uinfo.EndDate)) + "'"
                                        strSqlDept = strSqlDept + strQuery + " ) x group by Dept "
                                        '+ strQuery
                                        Dim dtbDept As New DataTable
                                        Using cnn As New SqlConnection(IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString()))
                                            cnn.Open()
                                            Using dad As New SqlDataAdapter(strSqlDept, cnn)
                                                dad.Fill(dtbDept)
                                            End Using
                                            cnn.Close()
                                        End Using
                                        RptDept.SetDataSource(dtbDept)
                                    Case 131
                                        'Dim dsPD As New DataSet
                                        'dsPD = DAL.ExecuteStoredProcedureGetDataSet("SP_MainBilling_REPORT_TotProd", parcollection)
                                        'dsPD.Tables(0).TableName = "MainBilling_REPORT_TotProd"

                                        Dim RptPD As New ReportDocument   'use generic ReportDocument
                                        RptPD = oRpt.OpenSubreport("PersDeptMainBilling_REPORT_TotProd.rpt")
                                        Dim strSql As String = "SELECT coalesce (TXTN.PRODUCT,'') as Product, product.name AS ProdName, SUM(TXTN.QUANTITY) AS TQTY, SUM(TXTN.COST) AS TCost, coalesce(SUM(DEPT.SURCHARGE),0) AS TSurcharge,coalesce(SUM(DEPT.SURCHARGEPERGALLON),0) AS TSurchargePERGALLON,Count(TXTN.PERSONNEL) as TXTXCnt FROM TXTN LEFT OUTER JOIN Vehs ON TXTN.VEHICLE = Vehs.[IDENTITY] LEFT OUTER JOIN PERS ON TXTN.PERSONNEL = PERS.[IDENTITY] LEFT OUTER JOIN product ON TXTN.PRODUCT = product.number LEFT OUTER JOIN [dbo].DEPT ON PERS.DEPT = DEPT.NUMBER "
                                        strSql = strSql + " WHERE TXTN.[DATETIME] BETWEEN '" + Convert.ToDateTime(CDate(Uinfo.StartDate)) + "' AND '" + Convert.ToDateTime(CDate(Uinfo.EndDate)) + "'"
                                        strSql = strSql + strQuery + " GROUP BY dbo.TXTN.PRODUCT, dbo.product.name ORDER BY txtn.product "
                                        Dim dtb As New DataTable
                                        Using cnn As New SqlConnection(IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString()))
                                            cnn.Open()
                                            Using dad As New SqlDataAdapter(strSql, cnn)
                                                dad.Fill(dtb)
                                            End Using
                                            cnn.Close()
                                        End Using
                                        RptPD.SetDataSource(dtb)

                                        Dim RptDept As New ReportDocument
                                        RptDept = oRpt.OpenSubreport("PersDeptMainBilling_REPORT_ToDept.rpt")
                                        Dim strSqlDept As String = "select  Dept + ' ' + min(isnull(DEPTNAME,'')) as ProdName, Sum(QTY) As TQTY , '' as Product, Sum(COST) as TCost , 0 TSurcharge, 0 AS TSurchargePERGALLON,0 as TXTXCnt from ( SELECT [TXTN].[MILES],ISNULL([TXTN].[COST],'0') AS COST,ISNULL([TXTN].[PREV_MILES],'0') AS PREV_MILES,isnull([PERS].[DEPT],'TAG') as Dept,[DEPT].[NAME] AS DEPTNAME,[TXTN].[QUANTITY] as QTY FROM [dbo].[TXTN] LEFT OUTER JOIN [dbo].[vehs] ON TXTN.VEHICLE =  VEHS.[IDENTITY] LEFT OUTER JOIN [dbo].[PERS] ON TXTN.PERSONNEL = PERS.[IDENTITY] LEFT OUTER JOIN [dbo].[PRODUCT] ON TXTN.PRODUCT = PRODUCT.NUMBER LEFT OUTER JOIN [dbo].DEPT ON PERS.DEPT = DEPT.NUMBER LEFT OUTER JOIN TANK ON TXTN.PRODUCT = TANK.PRODUCT AND TXTN.SENTRY = TANK.NUMBER "
                                        strSqlDept = strSqlDept + " WHERE TXTN.[DATETIME] BETWEEN '" + Convert.ToDateTime(CDate(Uinfo.StartDate)) + "' AND '" + Convert.ToDateTime(CDate(Uinfo.EndDate)) + "'"
                                        strSqlDept = strSqlDept + strQuery + " ) x group by Dept order by Dept  "
                                        '+ strQuery
                                        Dim dtbDept As New DataTable
                                        Using cnn As New SqlConnection(IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString()))
                                            cnn.Open()
                                            Using dad As New SqlDataAdapter(strSqlDept, cnn)
                                                dad.Fill(dtbDept)
                                            End Using
                                            cnn.Close()
                                        End Using
                                        RptDept.SetDataSource(dtbDept)
                                    Case 70
                                        Dim dsPD As New DataSet
                                        dsPD = DAL.ExecuteStoredProcedureGetDataSet("SP_MainBilling_REPORT_TotProd", parcollection)
                                        Dim RptPD As New ReportDocument   'use generic ReportDocument
                                        RptPD = oRpt.OpenSubreport("MainBilling_REPORT_TotProd.rpt")
                                        RptPD.SetDataSource(dsPD.Tables(0))
                                        'Case 33
                                        '    Dim dsPD As New DataSet
                                        '    dsPD = DAL.ExecuteStoredProcedureGetDataSet("SP_MainBilling_REPORT_Prod", parcollection)
                                        '    Dim RptPD As New ReportDocument   'use generic ReportDocument
                                        '    RptPD = oRpt.OpenSubreport("MainBilling_REPORT_Prod.rpt")
                                        '    RptPD.SetDataSource(dsPD.Tables(0))
                                End Select
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 21, 22
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)

                                Dim parcollection1(2) As SqlParameter
                                Dim Parstartdate1 = New SqlParameter("@startdate", SqlDbType.DateTime)
                                Dim Parenddate1 = New SqlParameter("@enddate", SqlDbType.DateTime)
                                Dim ParCondition1 = New SqlParameter("@Condition", SqlDbType.NVarChar, 1500)
                                Parstartdate1.Direction = ParameterDirection.Input
                                Parenddate1.Direction = ParameterDirection.Input
                                ParCondition1.Direction = ParameterDirection.Input
                                Parstartdate1.Value = Convert.ToDateTime(CDate(Uinfo.StartDate))
                                Parenddate1.Value = Convert.ToDateTime(CDate(Uinfo.EndDate))
                                ParCondition1.Value = strQuery
                                parcollection1(0) = Parstartdate1
                                parcollection1(1) = Parenddate1
                                parcollection1(2) = ParCondition1
                                ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SentryDateTime_HS_REPORT", parcollection1)

                                Dim RptHS As New ReportDocument   'use generic ReportDocument
                                RptHS = oRpt.OpenSubreport("Site_Hose.rpt")
                                RptHS.SetDataSource(ds1.Tables(0))

                                'Dim dsPD As New DataSet
                                'dsPD = DAL.ExecuteStoredProcedureGetDataSet("SP_SentryDateTime_Prod_REPORT", parcollection1)
                                'Dim RptPD As New ReportDocument   'use generic ReportDocument
                                'RptPD = oRpt.OpenSubreport("Site_Product.rpt")
                                'RptPD.SetDataSource(dsPD.Tables(0))

                                'Sub Report 
                                ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPRODUCT")
                                Dim i As Integer = 1
                                Dim str As String = String.Empty
                                For i = 1 To 16
                                    str = ds1.Tables(0).Rows(i - 1).Item(0)
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "Not In Use"))
                                Next
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 23
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)

                                Dim parcollection1(2) As SqlParameter
                                Dim Parstartdate1 = New SqlParameter("@startdate", SqlDbType.DateTime)
                                Dim Parenddate1 = New SqlParameter("@enddate", SqlDbType.DateTime)
                                Dim ParCondition1 = New SqlParameter("@Condition", SqlDbType.NVarChar, 1500)
                                Parstartdate1.Direction = ParameterDirection.Input
                                Parenddate1.Direction = ParameterDirection.Input
                                ParCondition1.Direction = ParameterDirection.Input
                                Parstartdate1.Value = Convert.ToDateTime(CDate(Uinfo.StartDate))
                                Parenddate1.Value = Convert.ToDateTime(CDate(Uinfo.EndDate))
                                ParCondition1.Value = strQuery
                                parcollection1(0) = Parstartdate1
                                parcollection1(1) = Parenddate1
                                parcollection1(2) = ParCondition1
                                ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SentryDateTime_HS_REPORT", parcollection1)

                                Dim RptHS As New ReportDocument   'use generic ReportDocument
                                RptHS = oRpt.OpenSubreport("Site_Hose.rpt")
                                RptHS.SetDataSource(ds1.Tables(0))

                                Dim dsPD As New DataSet
                                dsPD = DAL.ExecuteStoredProcedureGetDataSet("SP_SentryDateTime_Prod_REPORT", parcollection1)
                                Dim RptPD As New ReportDocument   'use generic ReportDocument
                                RptPD = oRpt.OpenSubreport("Site_Product.rpt")
                                RptPD.SetDataSource(dsPD.Tables(0))
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 34
                        Dim parcollection1(1) As SqlParameter
                        Dim Parstartdate1 = New SqlParameter("@startdate", SqlDbType.DateTime)
                        Dim Parenddate1 = New SqlParameter("@enddate", SqlDbType.DateTime)
                        Parstartdate1.Direction = ParameterDirection.Input
                        Parenddate1.Direction = ParameterDirection.Input
                        Parstartdate1.Value = Convert.ToDateTime(CDate(Uinfo.StartDate))
                        Parenddate1.Value = Convert.ToDateTime(CDate(Uinfo.EndDate))
                        parcollection1(0) = Parstartdate
                        parcollection1(1) = Parenddate
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_WrightExpressBillingReport_Header", parcollection1)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = "Wright Express Billing"
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                                ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_WrightExpressBillingReport_Footer", parcollection1)
                                Dim RptSub As New ReportDocument   'use generic ReportDocument
                                RptSub = oRpt.OpenSubreport("WrightExpressBilling_Sub.rpt")
                                RptSub.SetDataSource(ds1.Tables(0))
                                GenFun.SetCurrentValuesForParameterField(oRpt, "@SD", Uinfo.StartDate)
                                GenFun.SetCurrentValuesForParameterField(oRpt, "@ED", Uinfo.EndDate)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 35
                        Dim parcollection1(1) As SqlParameter
                        Dim Parstartdate1 = New SqlParameter("@startdate", SqlDbType.DateTime)
                        Dim Parenddate1 = New SqlParameter("@enddate", SqlDbType.DateTime)
                        Parstartdate1.Direction = ParameterDirection.Input
                        Parenddate1.Direction = ParameterDirection.Input
                        Parstartdate1.Value = Convert.ToDateTime(CDate(Uinfo.StartDate))
                        Parenddate1.Value = Convert.ToDateTime(CDate(Uinfo.EndDate))
                        parcollection1(0) = Parstartdate
                        parcollection1(1) = Parenddate
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_WrightExpressBillingReport_Footer", parcollection1)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = "Wright Express Billing Report-Sentry Details "
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 41
                        ds = DAL.GetDataSet(strQuery)
                        dt = VehicleDepartmentFuelNames(ds) 'ds.Tables(0)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SetDataSource(dt)
                                Page.Title = oRpt.SummaryInfo.ReportTitle
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 42
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehicleFuelUseByDeptVehSummary_REPORT", parcollection)
                        dt = ds.Tables(0)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SetDataSource(dt)
                                Page.Title = oRpt.SummaryInfo.ReportTitle
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 43
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehicleTroubleCode_REPORT", parcollection)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SummaryInfo.ReportComments = "From " + Uinfo.StartDate + " To " + Uinfo.EndDate
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                        'New Report Vehicle OBDII Trouble COde Report.10/11/2011
                    Case 303
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehicleOBDIITroubleCode_REPORT", parcollection)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SummaryInfo.ReportComments = "From " + Uinfo.StartDate + " To " + Uinfo.EndDate
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                        'New Report Vehicle OBDII "Z" Report.10/14/2011
                    Case 304
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehicleDTC_Engine_Report", parcollection)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SummaryInfo.ReportComments = "From " + Uinfo.StartDate + " To " + Uinfo.EndDate
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                        'New Report Vehicle DTC-List.04/13/2011
                    Case 110
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehicleDTC_REPORT", parcollection)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SummaryInfo.ReportComments = "From " + Uinfo.StartDate + " To " + Uinfo.EndDate
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 44, 48, 49
                        dt = GetVehicleDetails(Uinfo)
                        If Uinfo.ReportID = 48 Then dt = VehicleTypeFuelNames(dt)
                        If dt.Rows.Count > 0 Then
                            oRpt.SetDataSource(dt)
                            Page.Title = Uinfo.ReportTitle
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                        End If
                    Case 92
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehicleThatHaveNotFueled", parcollection)
                        'ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehicleFuelUseByDeptVehSummary_REPORT", parcollection)
                        dt = ds.Tables(0)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SetDataSource(dt)
                                Page.Title = oRpt.SummaryInfo.ReportTitle
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 45, 91
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SetDataSource(ds.Tables(0))
                                Page.Title = Uinfo.ReportTitle
                                If Uinfo.ReportID = 91 Then oRpt.SummaryInfo.ReportComments = "From " + Uinfo.StartDate + " To " + Uinfo.EndDate
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                        ' ''    'Added By Varun Moota 12/07/2009
                    Case 101
                        'Dim qry As String = " Select Count(Sentry) as ZCount from TXTN Where Quantity = 0 Group By Sentry,PUMP"
                        'ds1 = DAL.GetDataSet(qry)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                oRpt.SetDataSource(ds.Tables(0))
                                Page.Title = Uinfo.ReportTitle
                                '''ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SiteAnalysisSummary_REPORTEightCount")
                                '''Dim objDataTable1 As New System.Data.DataTable
                                '''Dim objDataTable11 As New System.Data.DataTable
                                '''objDataTable1 = ds.Tables(0)
                                '''objDataTable.Columns(0).ColumnName = "Sentry"

                                '''objDataTable11 = ds1.Tables(0)
                                '''objDataTable1.Merge(objDataTable11, False, MissingSchemaAction.AddWithKey)
                                '''oRpt.SetDataSource(objDataTable1)
                                '''Page.Title = Uinfo.ReportTitle
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 46
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                dt = GetVehiclePerformanceReport(ds)
                                If dt.Rows.Count > 0 Then
                                    oRpt.SetDataSource(dt)
                                    Page.Title = Uinfo.ReportTitle
                                Else
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                                End If
                            End If
                        End If
                    Case 50
                        If dt.Rows.Count > 0 Then
                            oRpt.SetDataSource(dt)
                            dt.DefaultView.Sort = "VEHICLE"
                            Page.Title = "MPG Deviation Report"
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                        End If
                    Case 51
                        Dim parcollection1(0) As SqlParameter
                        ParCondition = New SqlParameter("@Condition", SqlDbType.NVarChar)
                        ParCondition.Direction = ParameterDirection.Input
                        ParCondition.Value = strQuery
                        parcollection1(0) = ParCondition
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_PersonnelList", parcollection1)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 52, 53
                        dt = GetPersonnelDetails()
                        If dt.Rows.Count > 0 Then
                            oRpt.SetDataSource(dt)
                            Page.Title = oRpt.SummaryInfo.ReportTitle
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                        End If
                    Case 62
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)

                                'Added by PRITAM TO TEST
                                Dim MIL As Integer
                                MIL = ds.Tables(0).Rows(0)("miles")

                                oRpt.SetDataSource(objDataTable)
                                Select Case Uinfo.ReportID
                                    Case 62
                                        'Added By Varun Moota with new parameter fields,instead of Sub-report.06/15/2011
                                        If Not ds Is Nothing Then
                                            If ds.Tables(0).Rows.Count > 0 Then
                                                Page.Title = Uinfo.ReportTitle
                                                objDataTable = ds.Tables(0)
                                                oRpt.SetDataSource((objDataTable))
                                                ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPRODUCT")
                                                Dim i As Integer = 1
                                                Dim str As String = String.Empty
                                                For i = 1 To 16
                                                    str = ds1.Tables(0).Rows(i - 1).Item(0)
                                                    GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "NOT IN USE"))
                                                Next
                                                ' ''ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPUMP")
                                                ' ''For i = 1 To ds1.tables(0).rows.count
                                                ' ''    str = ds1.Tables(0).Rows(i - 1).Item(0)
                                                ' ''    GenFun.SetCurrentValuesForParameterField(oRpt, "hs" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "0"))
                                                ' ''Next
                                                ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPUMP")
                                                Dim iCnt As Integer = 1
                                                iCnt = ds1.Tables(0).Rows.Count
                                                For i = 1 To 8 '''ds1.tables(0).rows.count
                                                    If (iCnt < i) Then
                                                        GenFun.SetCurrentValuesForParameterField(oRpt, "hs" + CStr(i), "0")
                                                    Else
                                                        str = ds1.Tables(0).Rows(i - 1).Item(0)
                                                        GenFun.SetCurrentValuesForParameterField(oRpt, "hs" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "0"))
                                                    End If
                                                Next
                                            Else
                                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                                            End If
                                        End If
                                        'Commeneted By Varun Moota, since the report fails some times opening sub-report.06/05/2011
                                        ' ''Dim dsHS As New DataSet
                                        ' ''Dim RptSubHS As New ReportDocument   'use generic ReportDocument
                                        ' ''dsHS = DAL.ExecuteStoredProcedureGetDataSet("SP_MainBilling_REPORT_HS_FuelUseBYPERS", parcollection)
                                        ' ''RptSubHS = oRpt.OpenSubreport("FuelUseReport_byPersonnelSP_HS.rpt")
                                        ' ''RptSubHS.SetDataSource(dsHS.Tables(0))

                                        ' ''Dim dsPD As New DataSet
                                        ' ''Dim RptSubPD As New ReportDocument   'use generic ReportDocument
                                        ' ''dsPD = DAL.ExecuteStoredProcedureGetDataSet("SP_MainBilling_REPORT_Prod_FuelUseBYPERS", parcollection)
                                        ' ''RptSubPD = oRpt.OpenSubreport("FuelUseReport_byPersonnelSP_PD.rpt")
                                        ' ''RptSubPD.SetDataSource(dsPD.Tables(0))
                                    Case 62
                                        ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPRODUCT")
                                        Dim i As Integer = 1
                                        Dim str As String = String.Empty
                                        For i = 1 To 16
                                            str = ds1.Tables(0).Rows(i - 1).Item(0)
                                            GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "Not In Use"))
                                        Next
                                End Select
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 63
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                                Select Case Uinfo.ReportID
                                    Case 63
                                        ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPRODUCT")
                                        Dim i As Integer = 1
                                        Dim str As String = String.Empty
                                        For i = 1 To 16
                                            str = ds1.Tables(0).Rows(i - 1).Item(0)
                                            GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "Not In Use"))
                                        Next
                                End Select
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 71
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_INVACTIVITY_REPORT", parcollection)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = Uinfo.ReportTitle
                                oRpt.SummaryInfo.ReportComments = "From " + Uinfo.StartDate.ToString("MM/dd/yyyy HH:mm") + " To " + Uinfo.EndDate.ToString("MM/dd/yyyy HH:mm")
                                oRpt.PrintOptions.PaperOrientation = PaperOrientation.Landscape
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 72
                        Dim parcollection1(0) As SqlParameter
                        Dim ParQueryDate = New SqlParameter("@QueryDate", SqlDbType.DateTime)
                        ParQueryDate.Direction = ParameterDirection.Input
                        ParQueryDate.Value = Convert.ToDateTime(CDate(Uinfo.EndDate))
                        parcollection1(0) = ParQueryDate
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_TANKBALANCEREPORT", parcollection1)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = "Tank Balance Report"
                                oRpt.SummaryInfo.ReportTitle = Page.Title '"Balance as of " + Format(Uinfo.EndDate, "MM/dd/yyyy").ToString()
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                                'Sub Report 
                                ds = New DataSet
                                ds = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPRODUCT")
                                Dim i As Integer = 1
                                For i = 1 To 16
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "P" + CStr(i), ds.Tables(0).Rows(i - 1).Item(0))
                                Next
                                'TankBalDt()
                                GenFun.SetCurrentValuesForParameterField(oRpt, "TankBalDt", Format(Uinfo.EndDate, "MM/dd/yyyy").ToString())
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 74
                        dt = GetTempTable_PumpTotalizer(Uinfo)
                        If Not dt Is Nothing Then
                            If dt.Rows.Count > 0 Then
                                oRpt.SetDataSource(dt)
                                oRpt.SummaryInfo.ReportTitle = "Pump Totalizer Report"
                                Page.Title = oRpt.SummaryInfo.ReportTitle
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                        End If
                    Case 75
                        dt = GetInvPercentUsageReport(ds)
                        If dt.Rows.Count > 0 Then
                            oRpt.SetDataSource(dt)
                            Page.Title = oRpt.SummaryInfo.ReportTitle
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                        End If
                    Case 76, 73
                        'dt = GetCustomMadeDataTable(Uinfo)
                        'If Not dt Is Nothing Then
                        '    If dt.Rows.Count > 0 Then
                        '        oRpt.SetDataSource(dt)
                        '        Page.Title = oRpt.SummaryInfo.ReportTitle
                        '        If Uinfo.ReportID = 73 Or Uinfo.ReportID = 76 Then
                        '            Dim dt As DateTime
                        '            dt = Uinfo.StartDate.Date + " 00:00"
                        '            GenFun.SetCurrentValuesForParameterField(oRpt, "SDate", dt.ToString("MM/dd/yyyy HH:mm"))
                        '            dt = Uinfo.EndDate.Date + " 23:59"
                        '            GenFun.SetCurrentValuesForParameterField(oRpt, "EDate", dt.ToString("MM/dd/yyyy HH:mm"))
                        '        Else
                        '            GenFun.SetCurrentValuesForParameterField(oRpt, "SDate", "")
                        '            GenFun.SetCurrentValuesForParameterField(oRpt, "EDate", "")
                        '        End If
                        '    Else

                        '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                        '    End If
                        'End If
                        'Added By Varun Moota to test Tank Reconciliation Report.08/24/2010
                        dt = GetCustomMadeDataTable(Uinfo)
                        If Not dt Is Nothing Then
                            If dt.Rows.Count > 0 Then
                                oRpt.SetDataSource(dt)
                                Page.Title = oRpt.SummaryInfo.ReportTitle
                                If Uinfo.ReportID = 73 Or Uinfo.ReportID = 76 Then
                                    'Dim dt As DateTime
                                    'dt = Uinfo.StartDate.Date + " 00:00"
                                    'GenFun.SetCurrentValuesForParameterField(oRpt, "SDate", dt.ToString("MM/dd/yyyy HH:mm"))
                                    'dt = Uinfo.EndDate.Date + " 23:59"
                                    'GenFun.SetCurrentValuesForParameterField(oRpt, "EDate", dt.ToString("MM/dd/yyyy HH:mm"))
                                    Dim dtStart As DateTime
                                    Dim dtEnd As DateTime = Convert.ToDateTime(dt.Rows(0)(8))

                                    'oRpt.SummaryInfo.ReportComments = "From " + Uinfo.StartDate + " To " + dtEnd
                                    oRpt.SummaryInfo.ReportComments = "From " + Uinfo.StartDate.ToString("MM/dd/yyyy HH:mm") + " To " + Uinfo.EndDate.ToString("MM/dd/yyyy HH:mm")
                                    dtStart = Uinfo.StartDate.Date + " 00:00"
                                    'GenFun.SetCurrentValuesForParameterField(oRpt, "SDate", dtStart.ToString())
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "SDate", dtStart.ToString("MM/dd/yyyy HH:mm"))
                                    'dtEnd = Uinfo.EndDate.Date + " 23:59"
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "EDate", dtEnd.ToString("MM/dd/yyyy HH:mm"))
                                Else
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "SDate", "")
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "EDate", "")
                                End If
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 77
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource(objDataTable)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case 78, 79
                        If Not dt Is Nothing Then
                            If dt.Rows.Count > 0 Then
                                If Uinfo.ReportID = 78 Then
                                    Page.Title = "Inventory Information-No Fuel TXTN"
                                ElseIf Uinfo.ReportID = 79 Then
                                    Page.Title = "Tank Current Balance Report"
                                End If
                                oRpt.SetDataSource(dt)
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                        End If
                    Case 81
                        dt = GetMiscDetails(Uinfo)
                        If dt.Rows.Count > 0 Then
                            oRpt.SetDataSource(dt)
                            Page.Title = Uinfo.ReportTitle
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                        End If
                        'Added By Varun Moota for Report#64 & #65
                    Case 64, 65, 66
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Page.Title = Uinfo.ReportTitle
                                objDataTable = ds.Tables(0)
                                oRpt.SetDataSource((objDataTable))
                                ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPRODUCT")
                                Dim i As Integer = 1
                                Dim str As String = String.Empty
                                For i = 1 To 16
                                    str = ds1.Tables(0).Rows(i - 1).Item(0)
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "pd" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "NOT IN USE"))
                                Next
                                '' ''found bug if we have 8 hoses, change any old report(FuelUse_byVehDetail.rpt) with 8 hoses.
                                ' ''ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPUMP")
                                ' ''For i = 1 To ds1.tables(0).rows.count
                                ' ''    str = ds1.Tables(0).Rows(i - 1).Item(0)
                                ' ''    GenFun.SetCurrentValuesForParameterField(oRpt, "hs" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "0"))
                                ' ''Next
                                ds1 = DAL.ExecuteStoredProcedureGetDataSet("SP_SELECTPUMP")
                                Dim iCnt As Integer = 1
                                iCnt = ds1.Tables(0).Rows.Count
                                For i = 1 To 8 '''ds1.tables(0).rows.count
                                    If (iCnt < i) Then
                                        GenFun.SetCurrentValuesForParameterField(oRpt, "hs" + CStr(i), "0")
                                    Else
                                        str = ds1.Tables(0).Rows(i - 1).Item(0)
                                        GenFun.SetCurrentValuesForParameterField(oRpt, "hs" + CStr(i), IIf(str <> "", ds1.Tables(0).Rows(i - 1).Item(0), "0"))
                                    End If
                                Next
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                    Case Else
                        ds = DAL.GetDataSet(strQuery)
                        dt = ds.Tables(0)
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                If (Uinfo.ReportID = 41) Then Fuels_Name()
                                oRpt.SetDataSource(dt)
                                Page.Title = oRpt.SummaryInfo.ReportTitle
                            Else
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                            End If
                        End If
                End Select
                Select Case Uinfo.ReportID
                    Case 44, 46, 48, 49, 50, 52, 53, 73, 74, 75, 76, 78, 79, 81, 92
                        If dt.Rows.Count > 0 Then
                            Session("Rpt") = oRpt
                            CRViewer.ReportSource = oRpt
                        End If
                    Case Else
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                Session("Rpt") = oRpt
                                CRViewer.ReportSource = oRpt
                            End If
                        End If
                End Select
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            'cr.errorlog("ConfigureCrystalReports() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

            Dim sw As StreamWriter
            Dim str1 As String = ""

            Dim path As String = HttpContext.Current.Server.MapPath("TrakLogError.txt")
            str1 = DateTime.Now()
            If Not File.Exists(path) Then
                File.Create(path)
            End If

            sw = New StreamWriter(path, True)
            sw.WriteLine("Date/Time----- " + str1 + "Error Message:---- " + ex.Message.ToString() + "Error Occured in:-" + "ConfigureCrystalReports() ReportID=" + Uinfo.ReportID.ToString())
            sw.WriteLine("Details: 1 ----- " + str1 + "Error Message:---- " + ex.InnerException.ToString() + " \n Error Occured in:-" + "ConfigureCrystalReports() ReportID=" + Uinfo.ReportID.ToString())
            sw.WriteLine("Details: 2 StackTrace : ----- " + str1 + "Error Message:---- " + ex.StackTrace.ToString() + " \n Error Occured in:-" + "ConfigureCrystalReports() ReportID=" + Uinfo.ReportID.ToString())
            sw.Flush()
            sw.Close()

        End Try
    End Sub

    Private Function VehicleDepartmentFuelNames(ByVal ds As DataSet) As System.Data.DataTable
        Dim objDataTable As New System.Data.DataTable
        Try
            Dim i As Integer = 0, k As Integer = 0
            'Create three columns with string as their type
            objDataTable.Columns.Add("IDENTITY", String.Empty.GetType())
            objDataTable.Columns.Add("CARD_ID", String.Empty.GetType())
            objDataTable.Columns.Add("TYPE", String.Empty.GetType())
            objDataTable.Columns.Add("MILEAGE", String.Empty.GetType())
            objDataTable.Columns.Add("LASTFUELER", String.Empty.GetType())
            objDataTable.Columns.Add("LASTFUELDT", DateTime.Now.GetType())
            objDataTable.Columns.Add("VEHYEAR", String.Empty.GetType())
            objDataTable.Columns.Add("VEHMAKE", String.Empty.GetType())
            objDataTable.Columns.Add("VEHMODEL", String.Empty.GetType())
            objDataTable.Columns.Add("LICNO", String.Empty.GetType())
            objDataTable.Columns.Add("VEHVIN", String.Empty.GetType())
            objDataTable.Columns.Add("NAME", String.Empty.GetType())
            objDataTable.Columns.Add("FUELS", String.Empty.GetType())
            objDataTable.Columns.Add("CURRMILES", Decimal.Zero.GetType())
            objDataTable.Columns.Add("CURRHOURS", Decimal.Zero.GetType())
            'SELECT [IDENTITY], CARD_ID,  KEY_NUMBER , [TYPE], DEPT, 
            'CURRMILES, CURRHOURS, LASTFUELER, LASTFUELDT, VEHYEAR, VEHMAKE, VEHMODEL, LICNO, VEHVIN, FUELS
            Dim strFuel As String = "", strProduct As String = ""
            Dim dsFuel As New DataSet
            dsFuel = DAL.GetDataSet("SELECT NAME FROM PRODUCT")
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    strFuel = ds.Tables(0).Rows(i)("FUELS")
                    For k = 0 To strFuel.Length - 1
                        'MidFuel = UCase(Mid(strFuel(, k + 1, k + 1))
                        If strFuel(k) = "Y" Then strProduct += Left(dsFuel.Tables(0).Rows(k)("NAME"), 3) + ","
                    Next
                    With ds.Tables(0)
                        ' objDataTable.Rows.Add(New String() {.Rows(i)("IDENTITY"), .Rows(i)("CARD_ID"), .Rows(i)("TYPE"), .Rows(i)("MILEAGE"), IIf(IsDBNull(.Rows(i)("LASTFUELER")), "No Info", .Rows(i)("LASTFUELER")), IIf(IsDBNull(.Rows(i)("LASTFUELDT")), Now.Date, .Rows(i)("LASTFUELDT")), .Rows(i)("VEHYEAR"), .Rows(i)("VEHMAKE"), .Rows(i)("VEHMODEL"), .Rows(i)("LICNO"), .Rows(i)("VEHVIN"), .Rows(i)("NAME"), strProduct, IIf(IsDBNull(.Rows(i)("CURRMILES")), 0, .Rows(i)("CURRMILES")), IIf(IsDBNull(.Rows(i)("CURRHOURS")), 0, .Rows(i)("CURRHOURS"))})
                        ' objDataTable.Rows.Add(New String() {Convert.ToString(.Rows(i)("IDENTITY")), Convert.ToString(.Rows(i)("CARD_ID")), Convert.ToString(.Rows(i)("TYPE")), Convert.ToString(.Rows(i)("MILEAGE")), IIf(IsDBNull(.Rows(i)("LASTFUELER")), "No Info", .Rows(i)("LASTFUELER").ToString()), IIf(IsDBNull(.Rows(i)("LASTFUELDT")), Now.Date, .Rows(i)("LASTFUELDT").ToString()), Convert.ToString(.Rows(i)("VEHYEAR")), Convert.ToString(.Rows(i)("VEHMAKE")), Convert.ToString(.Rows(i)("VEHMODEL")), Convert.ToString(.Rows(i)("LICNO")), Convert.ToString(.Rows(i)("VEHVIN")), Convert.ToString(.Rows(i)("NAME")), strProduct, IIf(IsDBNull(.Rows(i)("CURRMILES")), 0, .Rows(i)("CURRMILES").ToString()), IIf(IsDBNull(.Rows(i)("CURRHOURS")), 0, .Rows(i)("CURRHOURS").ToString())})
                        objDataTable.Rows.Add(New String() {IIf(IsDBNull(.Rows(i)("IDENTITY")), "No Info", .Rows(i)("IDENTITY")), IIf(IsDBNull(.Rows(i)("CARD_ID")), "No Info", .Rows(i)("CARD_ID")), IIf(IsDBNull(.Rows(i)("TYPE")), "No Info", .Rows(i)("TYPE")), IIf(IsDBNull(.Rows(i)("MILEAGE")), "N0 Info", .Rows(i)("MILEAGE")), IIf(IsDBNull(.Rows(i)("LASTFUELER")), "No Info", .Rows(i)("LASTFUELER").ToString()), IIf(IsDBNull(.Rows(i)("LASTFUELDT")), Now.Date, .Rows(i)("LASTFUELDT").ToString()), IIf(IsDBNull(.Rows(i)("VEHYEAR")), "No Info", .Rows(i)("VEHYEAR")), IIf(IsDBNull(.Rows(i)("VEHMAKE")), "No Info", .Rows(i)("VEHMAKE")), IIf(IsDBNull(.Rows(i)("VEHMODEL")), "No Info", .Rows(i)("VEHMODEL")), IIf(IsDBNull(.Rows(i)("LICNO")), "No Info", .Rows(i)("LICNO")), IIf(IsDBNull(.Rows(i)("VEHVIN")), "No Info", .Rows(i)("VEHVIN")), IIf(IsDBNull(.Rows(i)("NAME")), "No Info", .Rows(i)("NAME")), strProduct, IIf(IsDBNull(.Rows(i)("CURRMILES")), 0, .Rows(i)("CURRMILES").ToString()), IIf(IsDBNull(.Rows(i)("CURRHOURS")), 0, .Rows(i)("CURRHOURS").ToString())})

                    End With
                    strProduct = ""
                Next
            End If
            objDataTable.DefaultView.Sort = "Name"
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.VehicleDepartmentFuelNames", ex)
        End Try
        Return objDataTable
    End Function

    Private Function VehicleTypeFuelNames(ByVal dt As DataTable) As System.Data.DataTable
        Dim objDataTable As New System.Data.DataTable
        Try
            Dim i As Integer = 0, k As Integer = 0
            'Create three columns with string as their type
            objDataTable.Columns.Add("IDENTITY", String.Empty.GetType())
            objDataTable.Columns.Add("EXTENSION", String.Empty.GetType())
            objDataTable.Columns.Add("CARD", String.Empty.GetType())
            objDataTable.Columns.Add("DEPT", String.Empty.GetType())
            objDataTable.Columns.Add("TYPE", String.Empty.GetType())
            objDataTable.Columns.Add("MorH_Value", Decimal.Zero.GetType())
            objDataTable.Columns.Add("MorH", String.Empty.GetType())
            objDataTable.Columns.Add("LASTFUELER", String.Empty.GetType())
            objDataTable.Columns.Add("LASTFUELDT", DateTime.Now.GetType())
            objDataTable.Columns.Add("DATE_ADDED", DateTime.Now.GetType())
            objDataTable.Columns.Add("FUEL", String.Empty.GetType())
            objDataTable.Columns.Add("VEHYEAR", String.Empty.GetType())
            objDataTable.Columns.Add("VEHMAKE", String.Empty.GetType())
            objDataTable.Columns.Add("VEHMODEL", String.Empty.GetType())
            objDataTable.Columns.Add("LICNO", String.Empty.GetType())

            Dim strFuel As String = "", strProduct As String = ""
            Dim dsFuel As New DataSet
            dsFuel = DAL.GetDataSet("SELECT NAME FROM PRODUCT")
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    strFuel = dt.Rows(i)("FUELS")
                    For k = 0 To strFuel.Length - 1
                        If strFuel(k) = "Y" Then
                            strProduct += Left(dsFuel.Tables(0).Rows(k)("NAME"), 3) + ","
                        End If
                    Next
                    With dt
                        objDataTable.Rows.Add(New String() {.Rows(i)("IDENTITY"), .Rows(i)("EXTENSION"), .Rows(i)("CARD"), _
                                .Rows(i)("DEPT"), .Rows(i)("TYPE"), IIf(IsDBNull(.Rows(i)("MorH_Value")), 0, .Rows(i)("MorH_Value")), .Rows(i)("MorH"), _
                                IIf(IsDBNull(.Rows(i)("LASTFUELER")), "No Info", .Rows(i)("LASTFUELER")), _
                                IIf(IsDBNull(.Rows(i)("LASTFUELDT")), Now.Date, .Rows(i)("LASTFUELDT")), _
                                IIf(IsDBNull(.Rows(i)("DATE_ADDED")), Now.Date, .Rows(i)("DATE_ADDED")), _
                                strProduct, .Rows(i)("VEHYEAR"), .Rows(i)("VEHMAKE"), .Rows(i)("VEHMODEL"), _
                                .Rows(i)("LICNO")})
                    End With
                    strProduct = ""
                Next
            End If
            objDataTable.DefaultView.Sort = "TYPE"
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.VehicleTypeFuelNames", ex)
        End Try
        Return objDataTable
    End Function

    Private Function GetTempTable_PumpTotalizer(ByVal userI As UserInfo) As System.Data.DataTable

        Dim Hose As Integer, i As Integer = 0, j As Integer = 0
        DAL = New GeneralizedDAL
        Dim QrySENTRY As String = "", QryTank As String = "", QryTxtn As String = "", QryMeter As String = ""

        Dim StartSentry As String = ".F.", EndSentry As String = ".F.", preSentry As String = " ", Sentry As String = " ", QtyTXTN As String = ""
        Dim StRead As Double, EdRead As Double, TRead As Double

        Dim StartDate As DateTime, EndDate As DateTime

        Dim dsSENTRY As DataSet, dsTxtn As DataSet, dsMeter As DataSet

        Dim objDataTable As New System.Data.DataTable
        Try
            If Not userI.StartTank = "" And Not userI.EndTank = "" Then
                QryTank += " AND LTRIM(RTRIM(TXTN.Tank)) BETWEEN '" + userI.StartTank + "' AND '" + userI.EndTank + "'"
            ElseIf Not (userI.StartTank = "") Then
                QryTank += " AND LTRIM(RTRIM(TXTN.Tank)) >='" + userI.StartTank + "''"
            ElseIf Not (userI.EndTank = "") Then
                QryTank += " AND LTRIM(RTRIM(TXTN.Tank)) <='" + userI.EndTank + "''"
            End If

            ''WHERE CRITERIA FOR SENTRY
            If Not (LTrim(RTrim(userI.StartSentry))) = "" And Not (LTrim(RTrim(userI.EndSentry))) = "" Then
                QrySENTRY += "  WHERE (LTRIM(RTRIM(NUMBER)) BETWEEN   '" + userI.StartSentry + "' AND '" + userI.EndSentry + "')"
            ElseIf Not (userI.StartSentry = "") Then
                QrySENTRY += "  WHERE (LTRIM(RTRIM(NUMBER)) >='" + userI.StartSentry + "')"
            ElseIf Not (userI.EndSentry = "") Then
                QrySENTRY += "  WHERE (LTRIM(RTRIM(NUMBER)) <='" + userI.EndSentry + "')"
            End If

            'Create three columns with string as their type
            objDataTable.Columns.Add("SentryNo", String.Empty.GetType())
            objDataTable.Columns.Add("SentryName", String.Empty.GetType())
            objDataTable.Columns.Add("PumpNo", String.Empty.GetType())
            objDataTable.Columns.Add("PumpName", String.Empty.GetType())
            objDataTable.Columns.Add("STTD", DateTime.Now.GetType())
            objDataTable.Columns.Add("EndTD", DateTime.Now.GetType())
            objDataTable.Columns.Add("STRead", Decimal.Zero.GetType())
            objDataTable.Columns.Add("EdRead", Decimal.Zero.GetType())
            objDataTable.Columns.Add("MRead", Decimal.Zero.GetType())

            ''SENTRY Name
            dsSENTRY = DAL.GetDataSet("SELECT NUMBER as SentryNo, NAME as SentryName FROM  SENTRY " + QrySENTRY + " ORDER BY NUMBER")

            If Not dsSENTRY Is Nothing Then
                If dsSENTRY.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsSENTRY.Tables(0).Rows.Count - 1
                        For Hose = 1 To 8
                            QryTxtn = "SELECT [SENTRY],[PUMP],[METERREAD],[DATETIME] FROM [METER] where [NEWUPDT]<3 AND " & _
                                " SENTRY= '" & dsSENTRY.Tables(0).Rows(i)(0).ToString() & "' AND Convert(Int,PUMP)= " + Val(Hose).ToString + " AND " & _
                                " (CONVERT(nvarchar(16),[DATETIME],20) BETWEEN convert(nvarchar(16),'" & userI.StartDate.ToString("yyyy-MM-dd HH:mm:ss") & "',20) AND convert(nvarchar(16),'" & userI.EndDate.ToString("yyyy-MM-dd HH:mm:ss") & "',20))" & _
                                " ORDER BY [DATETIME] ASC"
                            dsTxtn = DAL.GetDataSet(QryTxtn)
                            If Not dsTxtn Is Nothing Then
                                If dsTxtn.Tables(0).Rows.Count > 0 Then
                                    If dsTxtn.Tables(0).Rows.Count > 0 Then
                                        Sentry = dsTxtn.Tables(0).Rows(0)("SENTRY")
                                        StartDate = dsTxtn.Tables(0).Rows(0)("DATETIME")
                                        StRead = Val(IIf(IsDBNull(dsTxtn.Tables(0).Rows(0)("METERREAD")) = True, 0, dsTxtn.Tables(0).Rows(0)("METERREAD")))
                                        EndDate = dsTxtn.Tables(0).Rows(dsTxtn.Tables(0).Rows.Count - 1)("DATETIME")
                                        EdRead = Val(IIf(IsDBNull(dsTxtn.Tables(0).Rows(dsTxtn.Tables(0).Rows.Count - 1)("METERREAD")) = True, 0, dsTxtn.Tables(0).Rows(dsTxtn.Tables(0).Rows.Count - 1)("METERREAD")))
                                        'Getting Total Qty used from TXTN table
                                        QryMeter = "SELECT Sum(Quantity) as TXTNQty FROM TXTN  WHERE  TXTN.SENTRY = '" & dsSENTRY.Tables(0).Rows(i)(0).ToString() & "' And " & _
                                            " Convert(Int,PUMP)= " + Val(Hose).ToString + " AND " & _
                                            " (CONVERT(nvarchar(16),[DATETIME],20) BETWEEN convert(nvarchar(16),'" & StartDate.ToString("yyyy-MM-dd HH:mm:ss") & "',20) AND convert(nvarchar(16),'" & EndDate.ToString("yyyy-MM-dd HH:mm:ss") & "',20))" '& _
                                        dsMeter = DAL.GetDataSet(QryMeter)
                                        If Not dsMeter Is Nothing Then
                                            If dsMeter.Tables(0).Rows.Count > 0 Then
                                                QtyTXTN = dsMeter.Tables(0).Rows(0)("TXTNQty")
                                            Else
                                                QtyTXTN = 0
                                            End If
                                        End If
                                        'Getting Production details which link with TXTN
                                        QryMeter = "SELECT DISTINCT TXTN.SENTRY, TXTN.PRODUCT, PRODUCT.NAME AS ProdName,TXTN.PUMP,TXTN.Tank " & _
                                                " FROM TXTN INNER JOIN PRODUCT ON TXTN.PRODUCT = PRODUCT.NUMBER " & _
                                                " WHERE  TXTN.SENTRY = '" & dsSENTRY.Tables(0).Rows(i)(0).ToString() & "' And " & _
                                                " Convert(Int,PUMP)= " + Val(Hose).ToString + " AND " & _
                                                " (CONVERT(nvarchar(16),[DATETIME],20) BETWEEN convert(nvarchar(16),'" & StartDate.ToString("yyyy-MM-dd HH:mm:ss") & "',20) AND convert(nvarchar(16),'" & EndDate.ToString("yyyy-MM-dd HH:mm:ss") & "',20))" '& _
                                        dsMeter = DAL.GetDataSet(QryMeter)

                                        'If Not dsMeter Is Nothing Then
                                        '    If dsMeter.Tables(0).Rows.Count > 0 Then
                                        '        QtyTXTN = dsMeter.Tables(0).Rows(0)("MRead")
                                        '    Else
                                        '        QtyTXTN = 0
                                        '    End If
                                        'End If
                                        'objDataTable.Rows.Add(New String() {dsSENTRY.Tables(0).Rows(i)(0), dsSENTRY.Tables(0).Rows(i)("NAME"), Hose, dsTxtn.Tables(0).Rows(j)("ProdName"), StartDate, EndDate, StRead, EdRead, 0})
                                        objDataTable.Rows.Add(New String() _
                                                {dsSENTRY.Tables(0).Rows(i)("SentryNo"), dsSENTRY.Tables(0).Rows(i)("SentryName"), _
                                                 Hose, dsMeter.Tables(0).Rows(0)("ProdName"), StartDate, EndDate, StRead, EdRead, _
                                                 QtyTXTN})
                                    Else

                                    End If
                                    'preSentry = ""
                                    '        For j = 0 To dsTxtn.Tables(0).Rows.Count - 1
                                    '            Sentry = dsTxtn.Tables(0).Rows(j)("SENTRY")
                                    '            If Not Sentry = preSentry And EndSentry = ".T." Then
                                    '                TRead = EdRead - StRead
                                    '                EndSentry = ".F."
                                    '                QryMeter = "declare @sDt datetime,@eDt datetime set @sDt='" & userI.StartDate.ToString("dd-MMM-yyyy") & "' set @eDt='" & userI.EndDate.ToString("dd-MMM-yyyy") & _
                                    '                QryMeter = QryMeter + "' SELECT     SUM(CONVERT(float,METERREAD)) AS MRead FROM   METER WHERE (convert(nvarchar(20),METER.DATETIME,20) between  convert(nvarchar(20),@sDt,20) " & _
                                    '                QryMeter = QryMeter + " AND convert(nvarchar(20),@eDt,20) ) AND Convert(Int,METER.PUMP)= " + Val(Hose).ToString + " AND " & _
                                    '                QryMeter = QryMeter + " METER.SENTRY= '" & dsSENTRY.Tables(0).Rows(i)(0) & "'"
                                    '                dsMeter = DAL.GetDataSet(QryMeter)
                                    '                If Not dsMeter Is Nothing Then
                                    '                    If dsMeter.Tables(0).Rows.Count > 0 Then
                                    '                        objDataTable.Rows.Add(New String() {dsSENTRY.Tables(0).Rows(i)(0), dsSENTRY.Tables(0).Rows(i)("NAME"), Hose, dsTxtn.Tables(0).Rows(j)("ProdName"), StartDate, EndDate, StRead, EdRead, Val(IIf(IsDBNull(dsMeter.Tables(0).Rows(0)("MRead")) = True, 0, dsMeter.Tables(0).Rows(0)("MRead")))})
                                    '                    Else
                                    '                        objDataTable.Rows.Add(New String() {dsSENTRY.Tables(0).Rows(i)(0), dsSENTRY.Tables(0).Rows(i)("NAME"), Hose, dsTxtn.Tables(0).Rows(j)("ProdName"), StartDate, EndDate, StRead, EdRead, 0})
                                    '                    End If
                                    '                End If
                                    '                Sentry = dsTxtn.Tables(0).Rows(j)("SENTRY")
                                    '            End If
                                    '            If Sentry = preSentry Then
                                    '                EdRead = EdRead + dsTxtn.Tables(0).Rows(j)("QUANTITY")
                                    '                EndDate = dsTxtn.Tables(0).Rows(j)("datetime")
                                    '                EndSentry = ".T."
                                    '            End If
                                    '            If Not Sentry = preSentry Then
                                    '                StRead = Val(IIf(IsDBNull(dsTxtn.Tables(0).Rows(j)("QUANTITY")) = True, 0, dsTxtn.Tables(0).Rows(j)("QUANTITY")))
                                    '                StartDate = dsTxtn.Tables(0).Rows(j)("datetime")
                                    '                EndDate = dsTxtn.Tables(0).Rows(j)("datetime")
                                    '                EdRead = Val(IIf(IsDBNull(dsTxtn.Tables(0).Rows(j)("QUANTITY")) = True, 0, dsTxtn.Tables(0).Rows(j)("QUANTITY")))
                                    '                preSentry = Sentry
                                    '                EndSentry = ".T."
                                    '            End If
                                    '            If j = dsTxtn.Tables(0).Rows.Count - 1 And EndSentry = ".T." Then
                                    '                TRead = EdRead - StRead
                                    '                EndSentry = ".F."
                                    '                QryMeter = "declare @sDt datetime,@eDt datetime set @sDt='" & userI.StartDate.ToString("dd-MMM-yyyy HH:mm") & "' set @eDt='" & userI.EndDate.ToString("dd-MMM-yyyy HH:mm") & "' Select Sum(Quantity) as MRead from TXTN,SENTRY  WHere  TXTN.SENTRY = SENTRY.NUMBER And (convert(nvarchar(20),TXTN.[Datetime],20) between  convert(nvarchar(20),@sDt,20)  AND convert(nvarchar(20),@eDt,20) ) AND Convert(Int,TXTN.PUMP)= " + Val(Hose).ToString + " AND " & _
                                    '                            " TXTN.SENTRY= '" & dsSENTRY.Tables(0).Rows(i)(0) & "'"
                                    '                dsMeter = DAL.GetDataSet(QryMeter)
                                    '                If Not dsMeter Is Nothing Then
                                    '                    If dsMeter.Tables(0).Rows.Count > 0 Then
                                    '                        objDataTable.Rows.Add(New String() {dsSENTRY.Tables(0).Rows(i)(0), dsSENTRY.Tables(0).Rows(i)("NAME"), Hose, dsTxtn.Tables(0).Rows(j)("ProdName"), StartDate, EndDate, StRead, EdRead, Val(IIf(IsDBNull(dsMeter.Tables(0).Rows(0)("MRead")) = True, 0, dsMeter.Tables(0).Rows(0)("MRead")))})
                                    '                    Else
                                    '                        objDataTable.Rows.Add(New String() {dsSENTRY.Tables(0).Rows(i)(0), dsSENTRY.Tables(0).Rows(i)("NAME"), Hose, dsTxtn.Tables(0).Rows(j)("ProdName"), StartDate, EndDate, StRead, EdRead, 0})
                                    '                    End If
                                    '                End If
                                    '                Sentry = dsTxtn.Tables(0).Rows(j)("SENTRY")
                                    '            End If
                                    '        Next
                                End If
                            End If
                        Next
                    Next
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetTempTable_PumpTotalizer() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

        Return objDataTable
    End Function

    Private Function GetVehiclePerformanceReport(ByVal ds As DataSet) As System.Data.DataTable
        Dim i As Integer = 0

        Dim preVehicle As String = " ", veh As String = "", endVehs As String = ".F.", lastVehs As String = ".F.", strVehDesc As String = ""
        Dim startMile As Integer = 0, endMile As Integer = 0, totalMiles As Integer = 0
        Dim startMPG As Double = 0, endMPG As Double = 0, totalMPG As Double = 0
        Dim startCost As Double = 0, endCost As Double = 0, totalCost As Double = 0
        Dim startGals As Double = 0, endGals As Double = 0, totalGals As Double = 0

        Dim strMorH As String = "", HVeh As String = ""

        Dim objDataTable As New System.Data.DataTable
        Try
            With objDataTable.Columns
                'Create three columns with string as their type
                .Add("Vehicle", String.Empty.GetType()) : .Add("Desciption", String.Empty.GetType())
                .Add("TotalMiles", Decimal.Zero.GetType()) : .Add("MorH", String.Empty.GetType())
                .Add("TotGals", Decimal.Zero.GetType()) : .Add("MPG", Decimal.Zero.GetType()) : .Add("TotCost", Decimal.Zero.GetType())
            End With
            'modified the logic by Jatin on 27-Feb-2013 to get correct datatable 

            Dim dtUniqRecords As New DataTable()
            dtUniqRecords = ds.Tables(0).DefaultView.ToTable(True, "vehicle")
            For i = 0 To dtUniqRecords.Rows.Count - 1
                veh = dtUniqRecords.Rows(i)("vehicle")
                preVehicle = veh
                Dim str As String = "vehicle = '" & veh & "'"
                Dim results As DataRow() = ds.Tables(0).Select(str)

                strVehDesc = results(0).Item(1).ToString()
                totalMiles = Val(ds.Tables(0).Compute("Sum(TotalMiles)", "vehicle = '" & veh & "'"))
                strMorH = results(0).Item(6).ToString()
                totalGals = Val(ds.Tables(0).Compute("Sum(QUANTITY)", "vehicle = '" & veh & "'"))
                totalMPG = Val(totalMiles / totalGals) 'Val(ds.Tables(0).Compute("Sum(MPG)", "vehicle = '" & veh & "'"))
                totalCost = Val(ds.Tables(0).Compute("Sum(Cost)", "vehicle = '" & veh & "'"))
                objDataTable.Rows.Add(New String() {preVehicle, strVehDesc, totalMiles, strMorH, totalGals, totalMPG, totalCost})
            Next
            'commented by Jatin on 27-Feb-2013
            'For i = 0 To ds.Tables(0).Rows.Count - 1
            '    veh = ds.Tables(0).Rows(i)("vehicle")
            '    If Not veh = preVehicle And endVehs = ".T." Then
            '        totalMiles = startMile : totalGals = Val(startGals) : totalMPG = Val(startMPG)
            '        totalCost = Val(startCost) : preVehicle = HVeh : endVehs = ".F." : lastVehs = ".F."
            '        objDataTable.Rows.Add(New String() {preVehicle, strVehDesc, totalMiles, strMorH, totalGals, totalMPG, totalCost})
            '        veh = ds.Tables(0).Rows(i)("vehicle")
            '        endMile = 0 : endGals = 0 : endMPG = 0 : endCost = 0
            '    ElseIf veh = preVehicle Then
            '        startMile = startMile + Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("TotalMiles")) = True, 0, ds.Tables(0).Rows(i)("TotalMiles")))
            '        startGals = startGals + Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("QUANTITY")) = True, 0, ds.Tables(0).Rows(i)("QUANTITY")))
            '        startMPG = startMPG + Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("MPG")) = True, 0, ds.Tables(0).Rows(i)("MPG")))
            '        startCost = startCost + Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("Cost")) = True, 0, ds.Tables(0).Rows(i)("Cost")))
            '    End If
            '    If Not veh = preVehicle Then
            '        startMile = Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("TotalMiles")) = True, 0, ds.Tables(0).Rows(i)("TotalMiles")))
            '        startGals = Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("QUANTITY")) = True, 0, ds.Tables(0).Rows(i)("QUANTITY")))
            '        startMPG = Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("MPG")) = True, 0, ds.Tables(0).Rows(i)("MPG")))
            '        startCost = Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("Cost")) = True, 0, ds.Tables(0).Rows(i)("Cost")))

            '        preVehicle = veh : HVeh = preVehicle
            '        strVehDesc = ds.Tables(0).Rows(i)("EXTENSION")
            '        strMorH = ds.Tables(0).Rows(i)("MorH")
            '        endVehs = ".T." : lastVehs = ".T."
            '    End If
            '    If i = ds.Tables(0).Rows.Count - 2 Then
            '        HVeh = preVehicle : preVehicle = ""
            '    End If
            'Next
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetVehiclePerformanceReport() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
        Return objDataTable
    End Function

    Private Function GetInvPercentUsageReport(ByVal ds As DataSet) As System.Data.DataTable
        Dim i As Integer = 0
        Dim endVehs As String = ".F."
        Dim lastVehs As String = ".F."
        Dim preVehicle As String = " "
        Dim veh As String = ""
        Dim startMile As Integer = 0
        Dim endMile As Integer = 0
        Dim startOption As Integer = 0
        Dim endOption As Integer = 0
        Dim totalMiles As Integer = 0
        Dim totalOptions As Integer = 0
        Dim endDate As DateTime
        Dim StartDate As DateTime
        Dim objDataTable As New System.Data.DataTable
        Try
            'Create three columns with string as their type
            objDataTable.Columns.Add("Vehicle", String.Empty.GetType())
            objDataTable.Columns.Add("STTD", DateTime.Now.GetType())
            objDataTable.Columns.Add("EndTD", DateTime.Now.GetType())
            objDataTable.Columns.Add("STMiles", Decimal.Zero.GetType())
            objDataTable.Columns.Add("EndMiles", Decimal.Zero.GetType())
            objDataTable.Columns.Add("STOption", Decimal.Zero.GetType())
            objDataTable.Columns.Add("TMiles", Decimal.Zero.GetType())
            objDataTable.Columns.Add("TOptions", Decimal.Zero.GetType())

            For i = 0 To ds.Tables(0).Rows.Count - 1
                veh = ds.Tables(0).Rows(i)("vehicle")
                If Not veh = preVehicle And endVehs = ".T." Then
                    totalMiles = endMile - startMile
                    totalOptions = Val(endOption) - Val(startOption)
                    endVehs = ".F."
                    lastVehs = ".F."
                    objDataTable.Rows.Add(New String() {preVehicle, StartDate, endDate, startMile, endMile, startOption, totalMiles, totalOptions})
                    veh = ds.Tables(0).Rows(i)("vehicle")
                End If
                If veh = preVehicle Then
                    endMile = ds.Tables(0).Rows(i)("miles")
                    endDate = ds.Tables(0).Rows(i)("datetime")
                    endOption = Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("option")) = True, 0, ds.Tables(0).Rows(i)("option")))
                    lastVehs = ".T."
                End If
                If Not veh = preVehicle Then
                    startMile = ds.Tables(0).Rows(i)("miles")
                    StartDate = ds.Tables(0).Rows(i)("datetime")
                    endDate = ds.Tables(0).Rows(i)("datetime")
                    endMile = ds.Tables(0).Rows(i)("miles")
                    startOption = Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("option")) = True, 0, ds.Tables(0).Rows(i)("option")))
                    endOption = Val(IIf(IsDBNull(ds.Tables(0).Rows(i)("option")) = True, 0, ds.Tables(0).Rows(i)("option")))
                    preVehicle = veh
                    endVehs = ".T."
                    lastVehs = ".T."
                End If
            Next
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetInvPercentUsageReport() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

        Return objDataTable
    End Function

    Private Sub Fuels_Name()
        Dim fuelTypeTable As DataTable = New DataTable()
        Dim Query As String = "SELECT FUELNUMBER,FUELTYPE FROM FUEL ORDER BY FUELNUMBER"
        Dim ds1 As New DataSet
        Dim dt1 As New DataTable
        Try
            ds1 = DAL.GetDataSet(Query)
            If Not ds1 Is Nothing Then
                If ds1.Tables(0).Rows.Count > 0 Then
                    fuelTypeTable = ds1.Tables(0)
                    Dim strFuels As String = ""
                    Dim Row As DataRow
                    For Each Row In fuelTypeTable.Rows
                        Dim strFuel As String = Row("FUELS").ToString()
                        Dim I As Integer = 0
                        For I = 0 To strFuel.Length - 1
                            If (strFuel(I) = "Y") Then
                                If Not fuelTypeTable.Rows(I)("FUELTYPE").ToString().Trim() = "" Then
                                    strFuels += ", " + fuelTypeTable.Rows(I)("FUELTYPE").ToString().Trim()
                                End If
                            End If
                        Next I
                        strFuels = strFuel.Remove(0, 1)
                        Row("FUELS") = strFuels
                        strFuels = ""
                    Next Row
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("Fuels_Name() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LogON(ByVal crpt As ReportDocument)
        'Added By Varun Moota, to Get LogON parameters for Crystal Reports to Run Everytime. 03/18/2010
        Try

            Dim props As String() = {"Data Source", "Initial Catalog", "uid", "pwd"}
            Dim conn As String = IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString())
            Dim tokens As String() = conn.Split(";"c)
            Dim map As New Dictionary(Of String, String)

            Dim prop, item As String

            For Each prop In props
                For Each item In tokens
                    If (item.StartsWith(prop, StringComparison.OrdinalIgnoreCase)) Then
                        Dim val As String = item.Split("="c)(1)
                        map.Add(prop, val)

                    End If
                Next
            Next
            For Each myTable In crpt.Database.Tables
                myLogin = myTable.LogOnInfo
                myLogin.ConnectionInfo.DatabaseName = map("Initial Catalog")
                myLogin.ConnectionInfo.ServerName = map("Data Source")
                myLogin.ConnectionInfo.UserID = map("uid")
                myLogin.ConnectionInfo.Password = map("pwd")
                myTable.ApplyLogOnInfo(myLogin)
            Next
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.LogON()", ex.InnerException)
        End Try
    End Sub

    Public Function GetCustomMadeDataTable(ByVal userI As UserInfo) As System.Data.DataTable
        Dim ds As DataSet, ds1 As DataSet
        Dim SBal As Double = 0, EBal As Double = 0, DelBal As Double = 0

        'Added By Varun Moota to show TRAK Quantity Count 
        Dim TRAKCnt As Double = 0

        'Added By varun Moota to Test DateTime in Reports.08/24/2010
        Dim dtStartDT As DateTime, dtEndDT As DateTime

        Dim i As Integer = 0
        DAL = New GeneralizedDAL
        'Create a new DataTable object
        Dim objDataTable As New System.Data.DataTable
        Try
            Dim strQuery As String = Session("ReportInputs").ToString()

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
            Dim ParCondition = New SqlParameter("@qry", SqlDbType.NVarChar, 1000)
            ParCondition.Direction = ParameterDirection.Input
            ParCondition.Value = strQuery
            parcollection(0) = ParCondition
            ds = DAL.ExecuteStoredProcedureGetDataSet("usp_tt_TankList_report", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        SBal = 0 : DelBal = 0 : EBal = 0
                        If userI.ReportID = 73 Then ' Tank Reconciliation Report                           
                            Dim qry As String = ""
                            'Starting Balance
                            qry = " Select  [datetime] ,qty_meas as [Starting Balance] from FRTD " & _
                                    " WHERE [FRTD].NEWUPDT<3 AND (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
                                    " convert(nvarchar(20),datetime,20) between convert(nvarchar(20),'" + userI.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "',20) AND " & _
                                    " convert(nvarchar(20),'" + userI.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "',20) order by [datetime] ASC"
                            ds1 = DAL.GetDataSet(qry)
                            If Not ds1 Is Nothing Then
                                If ds1.Tables(0).Rows.Count > 1 Then
                                    SBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
                                    dtStartDT = Convert.ToDateTime(ds1.Tables(0).Rows(0)(0)).ToString("yyyy-MM-dd HH:mm:ss")
                                Else
                                    qry = " Select  top(1)  [datetime] ,qty_meas as [Starting Balance] from FRTD " & _
                                       " WHERE [FRTD].NEWUPDT<3 AND (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
                                       " convert(nvarchar(20),datetime,20) <= convert(nvarchar(20),'" + userI.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "',20) " & _
                                       " order by [datetime] DESC"
                                    ds1 = DAL.GetDataSet(qry)
                                    If Not ds1 Is Nothing Then
                                        If ds1.Tables(0).Rows.Count > 0 Then
                                            SBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
                                            dtStartDT = Convert.ToDateTime(ds1.Tables(0).Rows(0)(0)).ToString("yyyy-MM-dd HH:mm:ss")
                                        End If
                                    End If
                                End If
                            End If

                            'End Balance
                            qry = " Select [datetime] ,qty_meas as [ending Balance] from FRTD " & _
                                    " WHERE [FRTD].NEWUPDT<3 AND (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
                                    " convert(nvarchar(20),datetime,20) between convert(nvarchar(20),'" + userI.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "',20) AND " & _
                                    " convert(nvarchar(20),'" + userI.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "',20) order by [datetime] DESC"
                            ds1 = DAL.GetDataSet(qry)
                            If Not ds1 Is Nothing Then
                                If ds1.Tables(0).Rows.Count > 1 Then
                                    EBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
                                    'dtStartDT = Convert.ToDateTime(userI.StartDate.ToString("yyyy-MM-dd HH:mm:ss"))
                                    dtEndDT = Convert.ToDateTime(ds1.Tables(0).Rows(0)(0)).ToString("yyyy-MM-dd HH:mm:ss")
                                Else
                                    qry = " Select  top(1)  [datetime] ,qty_meas as [ending Balance] from FRTD " & _
                                        " WHERE [FRTD].NEWUPDT<3 AND (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
                                        " convert(nvarchar(20),datetime,20) >=convert(nvarchar(20),'" + userI.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "',20) order by [datetime] ASC"
                                    ds1 = DAL.GetDataSet(qry)
                                    If ds1.Tables(0).Rows.Count > 0 Then
                                        If Not ds1 Is Nothing Then
                                            If ds1.Tables(0).Rows.Count > 0 Then
                                                EBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
                                                'dtStartDT = Convert.ToDateTime(userI.StartDate.ToString("yyyy-MM-dd HH:mm:ss"))
                                                dtEndDT = Convert.ToDateTime(ds1.Tables(0).Rows(0)(0)).ToString("yyyy-MM-dd HH:mm:ss")
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            If dtStartDT.ToString("HH:mm") = "00:00" Then
                                dtStartDT = userI.StartDate.ToString("yyyy-MM-dd HH:mm:ss")
                            End If
                            If dtEndDT.ToString("HH:mm") = "00:00" Then
                                dtEndDT = userI.EndDate.ToString("yyyy-MM-dd HH:mm:ss")
                            End If
                            'Delivery Balance
                            qry = "Select isnull(sum(qty_added),0) as [Receipts] from FRTD where  " & _
                                   " [FRTD].NEWUPDT<3 AND ENTRY_TYPE ='R' AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
                                   " convert(nvarchar(20),datetime,20) between convert(nvarchar(20),'" + dtStartDT.ToString("yyyy-MM-dd HH:mm:ss") + "',20) AND " & _
                                   " convert(nvarchar(20),'" + dtEndDT.ToString("yyyy-MM-dd HH:mm:ss") + "',20) AND qty_added IS NOT NULL"
                            ds1 = DAL.GetDataSet(qry)
                            If Not ds1 Is Nothing Then
                                If ds1.Tables(0).Rows.Count > 0 Then DelBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(0))
                            End If

                            'Trak Calculation
                            If ds1.Tables(0).Rows.Count > 0 Then
                                If Not ds1 Is Nothing Then
                                    qry = "Select Sum(Quantity) as TRAKCnt from TXTN,tank  WHere  TXTN.Tank = Tank.Number And " & _
                                           " [TXTN].NEWUPDT<3 AND tank.NUMBER ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
                                           " [Datetime] between convert(nvarchar(20),'" + dtStartDT.ToString("yyyy-MM-dd HH:mm:ss") + "',20) AND " & _
                                           " convert(nvarchar(20),'" + dtEndDT.ToString("yyyy-MM-dd HH:mm:ss") + "',20) Group By TXTN.Tank "
                                    ds1 = DAL.GetDataSet(qry)
                                    If Not ds1 Is Nothing Then
                                        If ds1.Tables(0).Rows.Count > 0 Then
                                            TRAKCnt = CDec(ds1.Tables(0).Rows(0)("TRAKCnt").ToString())
                                        End If
                                    End If
                                End If
                            End If
                            If (Not SBal = 0.0 Or Not DelBal = 0.0 Or Not EBal = 0.0) Then
                                'Adding some data in the rows of this DataTable
                                objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("NUMBER").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("PRODUCTNAME").ToString(), SBal, EBal, DelBal, TRAKCnt, dtStartDT, dtEndDT})
                            End If
                        ElseIf userI.ReportID = 76 Then ' Tank Reconciliation - No dipping Report
                            'Starting Balance
                            Dim qry As String = "Select  top(1)  datetime ,qty_meas as [Starting Balance] from FRTD where  entry_type = 'R' AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(nvarchar(20),datetime,20) > convert(nvarchar(20),'" + userI.EndDate + "',20)"
                            ds1 = DAL.GetDataSet(qry)
                            If Not ds1 Is Nothing Then
                                If ds1.Tables(0).Rows.Count > 0 Then
                                    SBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
                                End If
                            End If
                            'Delivery Balance
                            qry = "Select isnull(sum(qty_added),0) as [Receipts] from FRTD where  entry_type = 'R' AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(nvarchar(20),datetime,20) between convert(nvarchar(20),'" + userI.StartDate + "',20) AND convert(nvarchar(20),'" + userI.EndDate + "',20)"
                            ds1 = DAL.GetDataSet(qry)
                            If Not ds1 Is Nothing Then
                                If ds1.Tables(0).Rows.Count > 0 Then
                                    DelBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(0))
                                End If
                            End If
                            'End Balance
                            qry = "Select top(1)  datetime ,qty_meas as [ending Balance] from FRTD where  entry_type = 'R' AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(nvarchar(20),datetime,20) > convert(nvarchar(20),'" + userI.StartDate + "',20)"
                            ds1 = DAL.GetDataSet(qry)
                            If Not ds1 Is Nothing Then
                                If ds1.Tables(0).Rows.Count > 0 Then
                                    EBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
                                End If
                            End If
                            If (Not SBal = 0.0 Or Not DelBal = 0.0 Or Not EBal = 0.0) Then
                                'Adding some data in the rows of this DataTable
                                'objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("NUMBER").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("PRODUCTNAME").ToString(), SBal, EBal, DelBal, TRAKCnt})
                                objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("NUMBER").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("PRODUCTNAME").ToString(), SBal, EBal, DelBal, TRAKCnt, dtStartDT, dtEndDT})
                            End If
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage, errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetCustomMadeDataTable() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
        Return objDataTable
    End Function

    'Public Function GetCustomMadeDataTable(ByVal userI As UserInfo) As System.Data.DataTable
    '    Dim ds As DataSet, ds1 As DataSet
    '    Dim SBal As Double = 0, EBal As Double = 0, DelBal As Double = 0

    '    'Added By Varun Moota to show TRAK Quantity Count 
    '    Dim TRAKCnt As Double = 0

    '    'Added By varun Moota to Test DateTime in Reports.08/24/2010
    '    Dim dtStartDT As DateTime, dtEndDT As DateTime

    '    Dim i As Integer = 0
    '    DAL = New GeneralizedDAL
    '    'Create a new DataTable object
    '    Dim objDataTable As New System.Data.DataTable
    '    Try
    '        Dim strQuery As String = Session("ReportInputs").ToString()

    '        'Create three columns with string as their type
    '        objDataTable.Columns.Add("TankNo", String.Empty.GetType())
    '        objDataTable.Columns.Add("TankName", String.Empty.GetType())
    '        objDataTable.Columns.Add("Product", String.Empty.GetType())
    '        objDataTable.Columns.Add("StartBal", String.Empty.GetType())
    '        objDataTable.Columns.Add("EndBal", String.Empty.GetType())
    '        objDataTable.Columns.Add("DelBal", String.Empty.GetType())

    '        'Added By Varun Moota to Show Trak Calculation in Tank Reconciliation Report. 12/15/2009
    '        objDataTable.Columns.Add("TRAKCnt", String.Empty.GetType())

    '        'Added By varun Moota To Test DT in Reconciliation Reports.08/24/2010
    '        objDataTable.Columns.Add("dtStartDT", DateTime.Now.GetType())
    '        objDataTable.Columns.Add("dtEndDT", DateTime.Now.GetType())

    '        Dim parcollection(0) As SqlParameter
    '        Dim ParCondition = New SqlParameter("@qry", SqlDbType.NVarChar, 1000)
    '        ParCondition.Direction = ParameterDirection.Input
    '        ParCondition.Value = strQuery
    '        parcollection(0) = ParCondition
    '        ds = DAL.ExecuteStoredProcedureGetDataSet("usp_tt_TankList_report", parcollection)
    '        If Not ds Is Nothing Then
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                For i = 0 To ds.Tables(0).Rows.Count - 1
    '                    SBal = 0 : DelBal = 0 : EBal = 0
    '                    If userI.ReportID = 73 Then ' Tank Reconciliation Report
    '                        dtStartDT = Convert.ToDateTime(userI.StartDate.ToString("yyyy-MM-dd"))
    '                        'Starting Balance
    '                        'Added By Varun Moota to test.08/25/2010
    '                        'Dim qry As String = " Select  top(1)  [datetime] ,qty_meas as [Starting Balance] from FRTD " & _
    '                        '        " WHERE  (entry_type = 'D' or entry_type = 'L') AND " & _
    '                        '        " FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
    '                        '        " convert(nvarchar(20),[datetime],20) > convert(nvarchar(20),'" + userI.StartDate.ToString("yyyy-MM-dd") + "',20) order by [datetime] desc"
    '                        Dim qry As String = " Select  top(1)  [datetime] ,qty_meas as [Starting Balance] from FRTD " & _
    '                                " WHERE  FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
    '                                " convert(nvarchar(20),datetime,20) between convert(nvarchar(20),'" + userI.StartDate + "',20) AND convert(nvarchar(20),'" + userI.EndDate + "',20) order by [datetime] ASC"
    '                        ds1 = DAL.GetDataSet(qry)
    '                        If Not ds1 Is Nothing Then
    '                            If ds1.Tables(0).Rows.Count > 0 Then
    '                                SBal = Convert.ToDouble(ds1.Tables(0).Rows(0)("Starting Balance"))
    '                                dtStartDT = Convert.ToDateTime(Convert.ToDateTime(ds1.Tables(0).Rows(0)("datetime")).ToString("yyyy-MM-dd"))
    '                            End If
    '                        End If

    '                        'Delivery Balance
    '                        'Added By Varun Moota to test.08/25/2010
    '                        'qry = "Select isnull(sum(qty_added),0) as [Receipts] from FRTD where  (entry_type = 'D' or entry_type = 'L') AND " & _
    '                        '        " FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
    '                        '        " convert(nvarchar(20),datetime,20) between convert(nvarchar(20),'" + userI.StartDate + "',20) AND convert(nvarchar(20),'" + userI.EndDate + "',20)"
    '                        qry = "Select DISTINCT qty_added as [Receipts] from FRTD where  " & _
    '                               " FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
    '                               " convert(nvarchar(20),datetime,20) between convert(nvarchar(20),'" + userI.StartDate + "',20) AND convert(nvarchar(20),'" + userI.EndDate + "',20) AND qty_added IS NOT NULL"
    '                        ds1 = DAL.GetDataSet(qry)
    '                        If Not ds1 Is Nothing Then
    '                            If ds1.Tables(0).Rows.Count > 0 Then
    '                                DelBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(0))
    '                            End If
    '                        End If

    '                        'End Balance
    '                        'Added By Varun Moota to test DateTime in Report.08/24/2010
    '                        'qry = "Select top(1)  [datetime] ,qty_meas as [ending Balance] from FRTD where  (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(nvarchar(20),datetime,20) > convert(nvarchar(20),'" + userI.EndDate.ToString("yyyy-MM-dd") + "',20) order by [datetime] desc"
    '                        qry = " Select  top(1)  [datetime] ,qty_meas as [Starting Balance] from FRTD " & _
    '                            " WHERE  FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
    '                            " convert(nvarchar(20),datetime,20) between convert(nvarchar(20),'" + userI.StartDate + "',20) AND convert(nvarchar(20),'" + userI.EndDate + "',20) order by [datetime] DESC"
    '                        ds1 = DAL.GetDataSet(qry)
    '                        If ds1.Tables(0).Rows.Count > 0 Then
    '                            If Not ds1 Is Nothing Then
    '                                If ds1.Tables(0).Rows.Count > 0 Then
    '                                    EBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
    '                                    'dtStartDT = Convert.ToDateTime(userI.StartDate.ToString("yyyy-MM-dd"))
    '                                    dtEndDT = Convert.ToDateTime(ds1.Tables(0).Rows(0)(0)).ToString("yyyy-MM-dd")
    '                                End If
    '                            End If
    '                            'Else
    '                            '    qry = "Select top(1)  [datetime] ,qty_meas as [ending Balance] from FRTD where  (entry_type = 'D' or entry_type = 'L') AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(nvarchar(20),datetime,20) < convert(nvarchar(20),'" + userI.EndDate.ToString("yyyy-MM-dd") + "',20) order by [datetime] desc"
    '                            '    ds1 = DAL.GetDataSet(qry)
    '                            '    If Not ds1 Is Nothing Then
    '                            '        If ds1.Tables(0).Rows.Count > 0 Then
    '                            '            EBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
    '                            '            'dtStartDT = Convert.ToDateTime(userI.StartDate.ToString("yyyy-MM-dd"))
    '                            '            dtEndDT = Convert.ToDateTime(ds1.Tables(0).Rows(0)(0)).ToString("yyyy-MM-dd")
    '                            '        End If
    '                            '    End If
    '                        End If
    '                        'Added By Varun Moota to Show Trak Calculation in Tank Reconciliation Report. 12/15/2009
    '                        TRAKCnt = 0
    '                        'ADDED Varun Moota
    '                        'dtStartDT = Convert.ToDateTime(userI.StartDate.ToString("yyyy-MM-dd"))
    '                        dtEndDT = Convert.ToDateTime(userI.EndDate.ToString("yyyy-MM-dd"))
    '                        'qry = "Select Sum(Quantity) as TRAKCnt from TXTN,tank  WHere  TXTN.Tank = Tank.Number And [Datetime] between convert(nvarchar(20),'" + userI.StartDate + "',20) AND convert(nvarchar(20),'" + userI.EndDate + "',20)Group By TXTN.Tank "
    '                        qry = "Select Sum(Quantity) as TRAKCnt from TXTN,tank  WHere  TXTN.Tank = Tank.Number And " & _
    '                        " tank.NUMBER ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND " & _
    '                        " [Datetime] between convert(nvarchar(20),'" + userI.StartDate.ToString("yyyy-MM-dd") + "',20) AND convert(nvarchar(20),'" + userI.EndDate.ToString("yyyy-MM-dd") + "',20)Group By TXTN.Tank "
    '                        ds1 = DAL.GetDataSet(qry)
    '                        If Not ds1 Is Nothing Then
    '                            If ds1.Tables(0).Rows.Count > 0 Then
    '                                If ds1.Tables(0).Rows.Count > 0 Then
    '                                    'For i = 0 To ds1.Tables(0).Rows.Count - 1
    '                                    TRAKCnt = CDec(ds1.Tables(0).Rows(0)("TRAKCnt").ToString())
    '                                    'TRAKCnt = Convert.ToDouble(ds1.Tables(0).Rows(0)(0))
    '                                    'Next
    '                                End If

    '                            End If
    '                        End If
    '                    ElseIf userI.ReportID = 76 Then ' Tank Reconciliation - No dipping Report
    '                        'Starting Balance
    '                        Dim qry As String = "Select  top(1)  datetime ,qty_meas as [Starting Balance] from FRTD where  entry_type = 'R' AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(nvarchar(20),datetime,20) > convert(nvarchar(20),'" + userI.EndDate + "',20)"
    '                        ds1 = DAL.GetDataSet(qry)
    '                        If Not ds1 Is Nothing Then
    '                            If ds1.Tables(0).Rows.Count > 0 Then
    '                                SBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
    '                            End If
    '                        End If
    '                        'Delivery Balance
    '                        qry = "Select isnull(sum(qty_added),0) as [Receipts] from FRTD where  entry_type = 'R' AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(nvarchar(20),datetime,20) between convert(nvarchar(20),'" + userI.StartDate + "',20) AND convert(nvarchar(20),'" + userI.EndDate + "',20)"
    '                        ds1 = DAL.GetDataSet(qry)
    '                        If Not ds1 Is Nothing Then
    '                            If ds1.Tables(0).Rows.Count > 0 Then
    '                                DelBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(0))
    '                            End If
    '                        End If
    '                        'End Balance
    '                        qry = "Select top(1)  datetime ,qty_meas as [ending Balance] from FRTD where  entry_type = 'R' AND FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' AND convert(nvarchar(20),datetime,20) > convert(nvarchar(20),'" + userI.StartDate + "',20)"
    '                        ds1 = DAL.GetDataSet(qry)
    '                        If Not ds1 Is Nothing Then
    '                            If ds1.Tables(0).Rows.Count > 0 Then
    '                                EBal = Convert.ToDouble(ds1.Tables(0).Rows(0)(1))
    '                            End If
    '                        End If
    '                    End If
    '                    If (Not SBal = 0.0 Or Not DelBal = 0.0 Or Not EBal = 0.0) Then
    '                        'Adding some data in the rows of this DataTable
    '                        'objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("NUMBER").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("PRODUCTNAME").ToString(), SBal, EBal, DelBal, TRAKCnt})
    '                        objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("NUMBER").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("PRODUCTNAME").ToString(), SBal, EBal, DelBal, TRAKCnt, dtStartDT, dtEndDT})
    '                    End If
    '                Next
    '            End If
    '        End If

    '    Catch ex As Exception
    '        Dim cr As New ErrorPage
    '        Dim errmsg As String
    '        Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
    '        cr.errorlog("GetCustomMadeDataTable() ReportID=" + Uinfo.ReportID.ToString(), ex)
    '        If ex.Message.Contains(";") Then
    '            errmsg = ex.Message.ToString()
    '            errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
    '        Else
    '            errmsg = ex.Message.ToString()
    '        End If
    '    End Try
    '    Return objDataTable
    'End Function

    Private Function GetPersonnelDetails() As System.Data.DataTable
        Dim ds As DataSet
        DAL = New GeneralizedDAL

        Dim strQuery As String = Session("ReportInputs").ToString()
        Dim parcollection(0) As SqlParameter
        Dim ParCondition = New SqlParameter("@Condition", SqlDbType.NVarChar, 1000)
        'Create a new DataTable object
        Dim objDataTable As New System.Data.DataTable
        Try
            ParCondition.Direction = ParameterDirection.Input
            ParCondition.Value = strQuery
            parcollection(0) = ParCondition
            ds = DAL.ExecuteStoredProcedureGetDataSet("SP_PersList_IDOrder", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    objDataTable = ds.Tables(0)
                End If
            End If
            Return objDataTable
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetPersonnelDetails() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return Nothing
        End Try
    End Function

    Public Function GetVehicleDetails(ByVal userI As UserInfo) As System.Data.DataTable
        Dim ds As DataSet
        DAL = New GeneralizedDAL

        Dim strQuery As String = Session("ReportInputs").ToString()
        Dim parcollection(0) As SqlParameter
        Dim ParCondition = New SqlParameter("@Condition", SqlDbType.NVarChar, 1255)
        'Create a new DataTable object
        Dim objDataTable As New System.Data.DataTable
        Try
            ParCondition.Direction = ParameterDirection.Input
            ParCondition.Value = strQuery
            parcollection(0) = ParCondition
            ds = New DataSet
            Select Case userI.ReportID
                Case 44
                    ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehListInIDOrder", parcollection)
                Case 48
                    ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehListInVEHType", parcollection)
                Case 49
                    ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehListFACalibration", parcollection)
                Case 92
                    ds = DAL.ExecuteStoredProcedureGetDataSet("SP_VehicleThatHaveNotFueled", parcollection)
            End Select
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    objDataTable = ds.Tables(0)
                End If
            End If
            If userI.ReportID = 92 Then objDataTable.DefaultView.Sort = "[IDENTITY]"
            Return objDataTable
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetVehicleDetails() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return Nothing
        End Try

    End Function

    Public Function GetMiscDetails(ByVal userI As UserInfo) As System.Data.DataTable
        Dim ds As DataSet
        DAL = New GeneralizedDAL
        'Create a new DataTable object
        Dim objDataTable As New System.Data.DataTable
        ds = New DataSet
        Try
            Select Case userI.ReportID
                Case 81
                    ds = DAL.ExecuteStoredProcedureGetDataSet("SP_MiscReportListing_Departments")
            End Select
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    objDataTable = ds.Tables(0)
                End If
            End If
            Return objDataTable
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetMiscDetails() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return Nothing
        End Try

    End Function

    Public Function GetVEHSMPGDeviationReport() As System.Data.DataTable
        Dim ds As DataSet
        Dim ds1 As DataSet
        Dim AvgMPG As Double = 0
        Dim CurrMPG As Double = 0
        Dim MaxMPG As Double = 0
        Dim MinMPG As Double = 0
        Dim SumQty As Double = 0
        Dim i As Integer
        DAL = New GeneralizedDAL
        'Create a new DataTable object
        Dim objDataTable As New System.Data.DataTable
        'Create three columns with string as their type
        objDataTable.Columns.Add("Vehicle", String.Empty.GetType())
        objDataTable.Columns.Add("CurrMPG", String.Empty.GetType())
        objDataTable.Columns.Add("AVGMPG", String.Empty.GetType())

        ds = DAL.GetDataSet("SELECT [IDENTITY] FROM [VEHS] WHERE  NEWUPDT <> 3")
        Try
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    

                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        MaxMPG = 0 : MinMPG = 0 : SumQty = 0 : AvgMPG = 0
                        ds1 = DAL.GetDataSet("SELECT TOP (10)* FROM TXTN WHERE VEHICLE ='" + ds.Tables(0).Rows(i)(0) + "' AND MILES > 0  order by datetime desc ")
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                MaxMPG = Convert.ToDouble(ds1.Tables(0).Compute("MAX(MILES)", ""))
                                MinMPG = Convert.ToDouble(ds1.Tables(0).Compute("Min(MILES)", ""))
                                SumQty = Convert.ToDouble(ds1.Tables(0).Compute("SUM(QUANTITY)", ""))
                                'Added By Varun Moota to Check if SUMQTY is 0,change to 1.11/17/2010
                                If SumQty = 0.0 Or 0 Then
                                    SumQty = 1
                                End If
                                AvgMPG = (MaxMPG - MinMPG) / SumQty
                            End If
                        End If

                        ds1 = DAL.GetDataSet("SELECT TOP (2)* FROM TXTN WHERE VEHICLE ='" + ds.Tables(0).Rows(i)(0) + "' AND MILES > 0  order by datetime desc ")
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                MaxMPG = Convert.ToDouble(ds1.Tables(0).Compute("MAX(MILES)", ""))
                                MinMPG = Convert.ToDouble(ds1.Tables(0).Compute("Min(MILES)", ""))
                                SumQty = Convert.ToDouble(ds1.Tables(0).Compute("SUM(QUANTITY)", ""))
                                'Added By Varun Moota to Check if SUMQTY is 0,change to 1.11/17/2010
                                If SumQty = 0.0 Or 0 Then
                                    SumQty = 1
                                End If
                                CurrMPG = (MaxMPG - MinMPG) / SumQty
                            End If
                        End If
                        'Adding some data in the rows of this DataTable
                        objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)(0), CurrMPG.ToString(), AvgMPG.ToString()})
                    Next
                End If
            End If
            Return objDataTable
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetVEHSMPGDeviationReport() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return Nothing
        End Try

    End Function

    Public Function GetMileage(ByVal dsMileage As DataSet) As System.Data.DataSet
        Dim i As Integer, j As Integer
        Dim exprn As String
        Dim dsVeh As New DataSet
        DAL = New GeneralizedDAL
        Try
            'For distinct vehicles
            Dim dtUniqVehicle As New DataTable()
            Dim stMiles As Int32 = 0, edMiles As Int32 = 0
            Dim dtVehicledatabyproduct As DataRow()

            Dim dtforvehicle As New DataTable()
            ' Dim dtforvehicle As DataSet
            dtforvehicle = dsMileage.Tables(0).Clone()
            'dtforvehicle = dsMileage.Tables(0)

            dtUniqVehicle = dsMileage.Tables(0).DefaultView.ToTable(True, "Vehicle", "Product")
            For i = 0 To dtUniqVehicle.Rows.Count - 1
                exprn = " Vehicle = '" + dtUniqVehicle.Rows(i)("Vehicle") + "' and product = '" + dtUniqVehicle.Rows(i)("Product") + "'"
                'Distinct Product
                dtVehicledatabyproduct = dsMileage.Tables(0).Select(exprn) 'DefaultView.ToTable(True, "Product")
                ' For j=0 To dtvehicledata 

                For j = 0 To dtVehicledatabyproduct.Length - 1
                    stMiles = dtVehicledatabyproduct(0)("Miles")
                    edMiles = dtVehicledatabyproduct(dtVehicledatabyproduct.Length - 1)("Miles")
                    dtVehicledatabyproduct(j)("MPG") = edMiles - stMiles
                    dtforvehicle.ImportRow(dtVehicledatabyproduct(j))
                Next
            Next
            dsVeh.Tables.Add(dtforvehicle)
            Return dsVeh
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetMileage() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try
    End Function

    Public Function GetVEHSPerformanceReport() As System.Data.DataTable
        Dim ds As DataSet
        Dim ds1 As DataSet
        Dim AvgMPG As Integer = 0
        Dim CurrMPG As Integer = 0
        Dim MaxMPG As Integer = 0
        Dim MinMPG As Integer = 0
        Dim SumQty As Integer = 0
        Dim i As Integer
        DAL = New GeneralizedDAL
        'Create a new DataTable object
        Dim objDataTable As New System.Data.DataTable
        'Create three columns with string as their type
        objDataTable.Columns.Add("Vehicle", String.Empty.GetType())
        objDataTable.Columns.Add("Extension", String.Empty.GetType())
        objDataTable.Columns.Add("Dept", String.Empty.GetType())
        objDataTable.Columns.Add("TotMiles", String.Empty.GetType())
        objDataTable.Columns.Add("TotQty", String.Empty.GetType())
        objDataTable.Columns.Add("TotCost", String.Empty.GetType())
        objDataTable.Columns.Add("TxtnCnt", String.Empty.GetType())
        objDataTable.Columns.Add("Flag", String.Empty.GetType())
        objDataTable.Columns.Add("Mileage", String.Empty.GetType())

        ds = DAL.GetDataSet("SELECT [IDENTITY] FROM [VEHS] WHERE  NEWUPDT <> 3")
        Try
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        MaxMPG = 0 : MinMPG = 0 : SumQty = 0 : AvgMPG = 0
                        ds1 = DAL.GetDataSet("SELECT TOP (10)* FROM TXTN WHERE VEHICLE ='" + ds.Tables(0).Rows(i)(0) + "' AND MILES > 0  order by datetime desc ")
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                MaxMPG = Convert.ToInt32(ds1.Tables(0).Compute("MAX(MILES)", ""))
                                MinMPG = Convert.ToInt32(ds1.Tables(0).Compute("Min(MILES)", ""))
                                SumQty = Convert.ToInt32(ds1.Tables(0).Compute("SUM(QUANTITY)", ""))
                                AvgMPG = (MaxMPG - MinMPG) / SumQty
                            End If
                        End If

                        ds1 = DAL.GetDataSet("SELECT TOP (2)* FROM TXTN WHERE VEHICLE ='" + ds.Tables(0).Rows(i)(0) + "' AND MILES > 0  order by datetime desc ")
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                MaxMPG = Convert.ToInt32(ds1.Tables(0).Compute("MAX(MILES)", ""))
                                MinMPG = Convert.ToInt32(ds1.Tables(0).Compute("Min(MILES)", ""))
                                SumQty = Convert.ToInt32(ds1.Tables(0).Compute("SUM(QUANTITY)", ""))
                                CurrMPG = (MaxMPG - MinMPG) / SumQty
                            End If
                        End If
                        'Adding some data in the rows of this DataTable
                        objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)(0), CurrMPG.ToString(), AvgMPG.ToString()})
                    Next
                End If
            End If
            Return objDataTable
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("GetVEHSPerformanceReport() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return Nothing
        End Try
    End Function

    Private Function TankCurrentBalReport(ByVal userI As UserInfo) As System.Data.DataTable
        Dim ds As DataSet
        Dim ds1 As DataSet
        Dim i As Integer = 0
        DAL = New GeneralizedDAL
        'Create a new DataTable object
        Dim objDataTable As New System.Data.DataTable
        Try
            'Create three columns with string as their type
            objDataTable.Columns.Add("TankNo", String.Empty.GetType())
            objDataTable.Columns.Add("TankName", String.Empty.GetType())
            objDataTable.Columns.Add("Product", String.Empty.GetType())
            objDataTable.Columns.Add("Balance", String.Empty.GetType())
            objDataTable.Columns.Add("Height", String.Empty.GetType())
            objDataTable.Columns.Add("Size", String.Empty.GetType())
            objDataTable.Columns.Add("CurrentDate", DateTime.Now.GetType())
            objDataTable.Columns.Add("TankMonProb", String.Empty.GetType())

            ds = DAL.GetDataSet("SELECT TANK.NUMBER, TANK.PRODUCT, PRODUCT.NAME AS PRODUCTNAME, TANK.NAME AS TankName, TANK.TANK_SIZE,TANK.TM_PROBE FROM TANK INNER JOIN PRODUCT ON TANK.PRODUCT = PRODUCT.NUMBER " & _
                                "GROUP BY TANK.NUMBER, TANK.PRODUCT, PRODUCT.NAME, TANK.NAME, TANK.TANK_SIZE,TANK.TM_PROBE") '.ExecuteStoredProcedureGetDataSet("usp_tt_TankList_report", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        'Starting Balance
                        Dim qry As String = "Select  top(1) *, qty_meas + qty_added as TankBal,datetime,qty_meas as gallon,height from FRTD where  FRTD.TANK_NBR ='" + ds.Tables(0).Rows(i)("NUMBER").ToString() + "' ORDER BY FRTD.datetime DESC"
                        ds1 = DAL.GetDataSet(qry)
                        If Not ds1 Is Nothing Then
                            If ds1.Tables(0).Rows.Count > 0 Then
                                'Adding some data in the rows of this DataTable
                                objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("NUMBER").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("PRODUCTNAME").ToString(), Convert.ToDouble(Val(ds1.Tables(0).Rows(0)("TankBal").ToString())), Convert.ToDouble(Val(ds1.Tables(0).Rows(0)("height").ToString())), ds.Tables(0).Rows(i)("TANK_SIZE").ToString(), Convert.ToDateTime(CDate(ds1.Tables(0).Rows(0)("datetime"))), ds.Tables(0).Rows(i)("TM_PROBE").ToString()})
                            Else
                                objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("NUMBER").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("PRODUCTNAME").ToString(), 0, "", ds.Tables(0).Rows(i)("TANK_SIZE").ToString(), Now, ds.Tables(0).Rows(i)("TM_PROBE").ToString()})
                            End If
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("TankCurrentBalReport() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return Nothing
        End Try
        Return objDataTable
    End Function

    Public Function InventotyInformationNoFuelTxtn(ByVal userI As UserInfo) As System.Data.DataTable
        Dim ds As DataSet
        Dim i As Integer = 0
        DAL = New GeneralizedDAL
        'Dim strQuery As String = Session("ReportInputs").ToString()
        'Create a new DataTable object
        Dim objDataTable As New System.Data.DataTable
        Try
            'Create three columns with string as their type
            objDataTable.Columns.Add("TankNbr", String.Empty.GetType())
            objDataTable.Columns.Add("TankName", String.Empty.GetType())
            objDataTable.Columns.Add("EntryType", String.Empty.GetType())
            objDataTable.Columns.Add("Date_Time", DateTime.Now.GetType())
            objDataTable.Columns.Add("QTYMeas", String.Empty.GetType())
            objDataTable.Columns.Add("QTYAdded", String.Empty.GetType())
            objDataTable.Columns.Add("PriceAdd", String.Empty.GetType())
            objDataTable.Columns.Add("InvoiceNo", String.Empty.GetType())
            objDataTable.Columns.Add("Supplier", String.Empty.GetType())
            objDataTable.Columns.Add("Employee", String.Empty.GetType())

            Dim strQuery As String = Session("ReportInputs").ToString()
            Dim parcollection(2) As SqlParameter
            Dim Parstartdate = New SqlParameter("@startdate", SqlDbType.DateTime)
            Dim Parenddate = New SqlParameter("@enddate", SqlDbType.DateTime)
            Dim ParCondition = New SqlParameter("@Condition", SqlDbType.NVarChar, 1500)

            Parstartdate.Direction = ParameterDirection.Input
            Parenddate.Direction = ParameterDirection.Input
            ParCondition.Direction = ParameterDirection.Input

            Parstartdate.Value = Convert.ToDateTime(CDate(userI.StartDate))
            Parenddate.Value = Convert.ToDateTime(CDate(userI.EndDate))
            ParCondition.Value = strQuery

            parcollection(0) = Parstartdate
            parcollection(1) = Parenddate
            parcollection(2) = ParCondition
            Dim strTankNbr As String = ""
            ds = DAL.ExecuteStoredProcedureGetDataSet("SP_InventoryInformationNoFuelTxtn", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        If Not strTankNbr = ds.Tables(0).Rows(i)("TANK_NBR").ToString() Then
                            strTankNbr = ds.Tables(0).Rows(i)("TANK_NBR").ToString()
                            objDataTable.Rows.Add(New String() {ds.Tables(0).Rows(i)("TANK_NBR").ToString(), ds.Tables(0).Rows(i)("TankName").ToString(), ds.Tables(0).Rows(i)("ENTRY_TYPE").ToString(), CDate(ds.Tables(0).Rows(i)("DATETIME")), ds.Tables(0).Rows(i)("QTY_MEAS").ToString(), ds.Tables(0).Rows(i)("QTY_ADDED").ToString(), ds.Tables(0).Rows(i)("PRICE_ADD").ToString(), ds.Tables(0).Rows(i)("INVOICE").ToString(), ds.Tables(0).Rows(i)("SUPPLIER").ToString(), ds.Tables(0).Rows(i)("EMPLOYEE").ToString()})
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            cr.errorlog("InventotyInformationNoFuelTxtn() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return Nothing
        End Try
        objDataTable.DefaultView.Sort = "Date_Time"
        Return objDataTable
    End Function

    Private Sub MiscReports()
        Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
        GenFun = New GeneralFunctions
        Dim ds As DataSet
        DAL = New GeneralizedDAL
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
                Exit Sub
            End If

            If (Not Uinfo Is MyNull) Then
                oRpt = New ReportDocument()
                oRpt.Load(Server.MapPath(GenFun.LoadReport(Uinfo.ReportID)))
                oRpt.SummaryInfo.ReportAuthor = Uinfo.ReportHeader
                oRpt.SummaryInfo.ReportTitle = Uinfo.ReportTitle
                Page.Title = Uinfo.ReportTitle
                Select Case Uinfo.ReportID
                    Case 81
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_MiscReportListing_Departments")
                    Case 82
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_MiscReportListing_SiteInfo")
                    Case 83
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_MiscListingTanks")
                    Case 84
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_MiscReportListing_LockOuts")
                    Case 85
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_MiscReportListing_CurrentExportFile")
                    Case 88
                        Dim parcollection(0) As SqlParameter
                        Dim Parenddate = New SqlParameter("@enddate", SqlDbType.DateTime)
                        Parenddate.Direction = ParameterDirection.Input
                        Parenddate.Value = Convert.ToDateTime(CDate(Uinfo.EndDate))
                        parcollection(0) = Parenddate
                        ds = DAL.ExecuteStoredProcedureGetDataSet("SP_MiscReportListing_ExportSummReport", parcollection)
                End Select
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        oRpt.SetDataSource(ds.Tables(0))
                        Select Case Uinfo.ReportID
                            Case 85
                                Dim RptSub As New ReportDocument   'use generic ReportDocument
                                RptSub = oRpt.OpenSubreport("MiscReportListing_CurrentExportFile_Hose.rpt")
                                RptSub.SetDataSource(ds.Tables(1))
                                Dim RptSub1 As New ReportDocument   'use generic ReportDocument
                                RptSub1 = oRpt.OpenSubreport("MiscReportListing_CurrentExportFile_Product.rpt")
                                RptSub1.SetDataSource(ds.Tables(2))
                                Dim RptSub2 As New ReportDocument   'use generic ReportDocument
                                RptSub2 = oRpt.OpenSubreport("MiscReportListing_CurrentExportFile_Product_Summ.rpt")
                                RptSub2.SetDataSource(ds.Tables(3))
                            Case 88
                                Dim RptSub As New ReportDocument   'use generic ReportDocument
                                RptSub = oRpt.OpenSubreport("MiscReportListing_ExportSummTxtnDtl.rpt")
                                RptSub.SetDataSource(ds.Tables(1))
                                Dim RptSub1 As New ReportDocument   'use generic ReportDocument
                                RptSub1 = oRpt.OpenSubreport("MiscReportListing_ExportTxtnCnt.rpt")
                                RptSub1.SetDataSource(ds.Tables(2))
                                oRpt.SummaryInfo.ReportTitle = "Export Summary Report"
                                Page.Title = oRpt.SummaryInfo.ReportTitle
                                oRpt.SummaryInfo.ReportComments = "Export Ending : " + Format(Uinfo.EndDate, "MM/dd/yyyy").ToString()
                                If ds.Tables(3).Rows.Count > 0 Then
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "FTxtnDt", ds.Tables(3).Rows(0)("DATETIME"))
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "LTxtnDt", ds.Tables(4).Rows(0)("DATETIME"))
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "FTxtnSetry", ds.Tables(3).Rows(0)("SENTRY"))
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "LTxtnSetry", ds.Tables(4).Rows(0)("SENTRY"))
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "FTxtnPM", ds.Tables(3).Rows(0)("PUMP"))
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "LTxtnPM", ds.Tables(4).Rows(0)("PUMP"))
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "FTxtnQty", ds.Tables(3).Rows(0)("QUANTITY"))
                                    GenFun.SetCurrentValuesForParameterField(oRpt, "LTxtnQty", ds.Tables(4).Rows(0)("QUANTITY"))
                                End If

                        End Select

                    Else


                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                    End If
                End If
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Session("Rpt") = oRpt
                        CRViewer.ReportSource = oRpt
                    End If
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("MiscReports() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try
    End Sub

    Private Sub TranslationReport()
        Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
        GenFun = New GeneralFunctions
        DAL = New GeneralizedDAL
        Dim ds = New DataSet
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
                Exit Sub
            End If

            If (Not Uinfo Is MyNull) Then
                Dim parcollection1(0) As SqlParameter
                Dim ParExportNum = New SqlParameter("@ExportNum", SqlDbType.Int)
                ParExportNum.Direction = ParameterDirection.Input
                ParExportNum.Value = Convert.ToInt32(Session("ExportNum"))
                parcollection1(0) = ParExportNum

                oRpt = New ReportDocument()
                oRpt.Load(Server.MapPath(GenFun.LoadReport(Uinfo.ReportID)))
                ds = DAL.ExecuteStoredProcedureGetDataSet("SP_Translate", parcollection1)
                oRpt.SummaryInfo.ReportAuthor = Uinfo.ReportHeader 'Request.QueryString.Item("ReportHeader").ToString()
                oRpt.SummaryInfo.ReportTitle = Uinfo.ReportTitle 'Request.QueryString.Item("ReportName").ToString()
                Page.Title = oRpt.SummaryInfo.ReportTitle
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Page.Title = oRpt.SummaryInfo.ReportTitle
                        oRpt.SetDataSource(ds.Tables(0))
                        Session("Rpt") = oRpt
                        CRViewer.ReportSource = oRpt
                    Else

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                    End If
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TranslationReport() ReportID=" + Uinfo.ReportID.ToString(), ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try
    End Sub

    'Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
    '    'Response.Redirect("ReportExport.aspx?abc=" + DDLExportType.SelectedItem.Text)
    'End Sub

    'Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    '    Dim oRpt1 As ReportDocument

    '    oRpt1 = CType(Session("Rpt"), ReportDocument)
    '    oRpt1.PrintToPrinter(1, False, 0, 0) 

    'End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>window.open('ReportExport.aspx','popup','toolbar=false,width=550px,height=360px,top=(screen.height/2),left=(screen.width/2),scrollbars=no');</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.ImageButton1_Click", ex)
        End Try

    End Sub

    Protected Overrides Sub Finalize()
        Try
            MyBase.Finalize()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.Finalize()", ex)
        End Try

    End Sub

    Private Sub TankTotReports()

        Try

            Dim dsTot As New DataSet
            Dim dl As New GeneralizedDAL
            Dim objDataTable As New System.Data.DataTable
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            GenFun = New GeneralFunctions


            Dim oRpt = New ReportDocument()
            oRpt.Load(Server.MapPath(GenFun.LoadReport(Uinfo.ReportID)))


            Dim parcollection(1) As SqlParameter
            Dim ParDateTime = New SqlParameter("@dt", SqlDbType.NVarChar, 25)
            Dim ParTankNo = New SqlParameter("@TankNo", SqlDbType.NVarChar, 3)

            ParDateTime.Direction = ParameterDirection.Input
            ParTankNo.Direction = ParameterDirection.Input

            ParDateTime.Value = Session("StartDTRecon").ToString()
            parcollection(0) = ParDateTime
            ParTankNo.Value = Session("TNKNBR").ToString()
            parcollection(1) = ParTankNo

            dsTot = dl.ExecuteStoredProcedureGetDataSet("USP_TT_TANKRECON_GETDATA_DFW", parcollection)
            'dsTot = dl.ExecuteStoredProcedureGetDataSet("USP_TT_TANKRECON_GETDATA_DFW_DISPENSERS_VIEW", parcollection)


            If Not dsTot Is Nothing Then
                If dsTot.Tables(0).Rows.Count > 0 Then
                    oRpt.SummaryInfo.ReportTitle = "INVENTORY RECONCILIATION FORM"
                    oRpt.SummaryInfo.ReportAuthor = "DFW Airport"
                    objDataTable = dsTot.Tables(0)
                    oRpt.SetDataSource(objDataTable)

                    LogON(oRpt)

                    'Parameters
                    GenFun.SetCurrentValuesForParameterField(oRpt, "TankLocation", Session("TankTotLoc").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "dtYear", Session("TankTotYear").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "dtMonth", Session("TankTotMonth").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "TankNo", Session("TNKNBR").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "TCEQOwnerID", "21747")
                    GenFun.SetCurrentValuesForParameterField(oRpt, "TCEQFacilityID", "10457")
                    GenFun.SetCurrentValuesForParameterField(oRpt, "prevMnthTotalizer", Session("strPrevMntTot").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "PrevTankVolume", Session("strPrevMntLevel").ToString())



                    GenFun.SetCurrentValuesForParameterField(oRpt, "cntLine1", Session("CurrTotalizer").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "cntLine2", Session("strCurrentMntDel").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "cntLineB", Session("strCurrentMntLevels").ToString())

                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL1C1", Session("strPrevMntLevel").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL1C2", Session("CurrTotalizer").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL1C3", Session("strCurrentMntDel").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL1C4", Session("cntLine1").ToString())

                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL2C1", Session("CurrTotalizer").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL2C2", Session("cntLine2").ToString())


                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL3C1", Session("strCurrentMntLevels").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL3C2", Session("cntLine2").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL3C3", Session("cntLine3").ToString())

                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL4C1", Session("strCurrentMntLevels").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL4C2", Session("cntLine2").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL4C3", Session("cntLine4").ToString())


                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL5C1", Session("cntLine1").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL5C2", Session("cntLine3").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL5C3", Session("cntLine5").ToString())


                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL6C1", Session("cntLine1").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL6C2", Session("cntLine4").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL6C3", Session("cntLine6").ToString())





                    Session("Rpt") = oRpt
                    CRViewer.ReportSource = oRpt

                Else

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                End If


            End If




        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.TankTotReports()", ex)
        End Try


    End Sub

    Private Sub TankTot2Reports()

        Try

            Dim dsTot As New DataSet
            Dim dl As New GeneralizedDAL
            Dim objDataTable As New System.Data.DataTable
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            GenFun = New GeneralFunctions


            Dim oRpt = New ReportDocument()
            oRpt.Load(Server.MapPath(GenFun.LoadReport(Uinfo.ReportID)))


            Dim parcollection(1) As SqlParameter
            Dim ParDateTime = New SqlParameter("@dt", SqlDbType.NVarChar, 25)
            Dim ParTankNo = New SqlParameter("@TankNo", SqlDbType.NVarChar, 3)

            ParDateTime.Direction = ParameterDirection.Input
            ParTankNo.Direction = ParameterDirection.Input

            ParDateTime.Value = Session("StartDTRecon").ToString()
            parcollection(0) = ParDateTime
            ParTankNo.Value = Session("TNKNBR").ToString()
            parcollection(1) = ParTankNo

            dsTot = dl.ExecuteStoredProcedureGetDataSet("USP_TT_TANKRECON_GETDATA_DFW_DISPENSERS", parcollection)
            'dsTot = dl.ExecuteStoredProcedureGetDataSet("USP_TT_TANKRECON_GETDATA_DFW_DISPENSERS_VIEW", parcollection)

            If Not dsTot Is Nothing Then
                If dsTot.Tables(0).Rows.Count > 0 Then
                    oRpt.SummaryInfo.ReportTitle = "INVENTORY RECONCILIATION FORM"
                    oRpt.SummaryInfo.ReportAuthor = "DFW Airport"
                    objDataTable = dsTot.Tables(0)
                    oRpt.SetDataSource(objDataTable)

                    LogON(oRpt)

                    'Parameters
                    GenFun.SetCurrentValuesForParameterField(oRpt, "TankLocation", Session("TankTotLoc").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "dtYear", Session("TankTotYear").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "dtMonth", Session("TankTotMonth").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "TankNo", Session("TNKNBR").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "TCEQOwnerID", "21747")
                    GenFun.SetCurrentValuesForParameterField(oRpt, "TCEQFacilityID", "10457")
                    GenFun.SetCurrentValuesForParameterField(oRpt, "prevMnthTotalizer", Session("strPrevMntTot").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "PrevTankVolume", Session("strPrevMntLevel").ToString())



                    GenFun.SetCurrentValuesForParameterField(oRpt, "cntLine1", Session("CurrTotalizer").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "CurrentMntTot2", Session("CurrTotalizer2").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "cntLine2", Session("strCurrentMntDel").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "cntLineB", Session("strCurrentMntLevels").ToString())

                    GenFun.SetCurrentValuesForParameterField(oRpt, "PrevMntTot2", Session("strPrevMntTot2").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "CurrentTotalizerAB", Session("CntCurrentTotalizerAB").ToString())

                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL1C1", Session("strPrevMntLevel").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL1C2", Session("CntCurrentTotalizerAB").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL1C3", Session("strCurrentMntDel").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL1C4", Session("cntLine1").ToString())

                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL2C1", Session("CntCurrentTotalizerAB").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL2C2", Session("cntLine2").ToString())


                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL3C1", Session("strCurrentMntLevels").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL3C2", Session("cntLine2").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL3C3", Session("cntLine3").ToString())

                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL4C1", Session("strCurrentMntLevels").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL4C2", Session("cntLine2").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL4C3", Session("cntLine4").ToString())


                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL5C1", Session("cntLine1").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL5C2", Session("cntLine3").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL5C3", Session("cntLine5").ToString())


                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL6C1", Session("cntLine1").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL6C2", Session("cntLine4").ToString())
                    GenFun.SetCurrentValuesForParameterField(oRpt, "txtL6C3", Session("cntLine6").ToString())





                    Session("Rpt") = oRpt
                    CRViewer.ReportSource = oRpt

                Else

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                End If


            End If




        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.TankTot2Reports()", ex)
        End Try


    End Sub

    Private Sub TankChartReport()

        Try

            Dim dsTankChart As New DataSet
            Dim dl As New GeneralizedDAL
            Dim objDataTable As New System.Data.DataTable
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            GenFun = New GeneralFunctions


            Dim oRpt = New ReportDocument()
            oRpt.Load(Server.MapPath(GenFun.LoadReport(Uinfo.ReportID)))


            Dim parcollection(0) As SqlParameter

            Dim ParChartNo = New SqlParameter("@Number", SqlDbType.Int)

            ParChartNo.Direction = ParameterDirection.Input
            ParChartNo.Value = CInt(Session("TankChartNum").ToString())
            parcollection(0) = ParChartNo


            dsTankChart = dl.ExecuteStoredProcedureGetDataSet("Use_TT_GetTankChart_PRINT_REPORT", parcollection)


            If Not dsTankChart Is Nothing Then
                If dsTankChart.Tables(0).Rows.Count > 0 Then
                    oRpt.SummaryInfo.ReportTitle = "Print-out of Tank Chart"
                    oRpt.SummaryInfo.ReportAuthor = "DFW Airport"
                    objDataTable = dsTankChart.Tables(0)
                    oRpt.SetDataSource(objDataTable)

                    LogON(oRpt)

                    'Parameters
                    GenFun.SetCurrentValuesForParameterField(oRpt, "ChartName", dsTankChart.Tables(0).Rows(0)(1).ToString())

                    Session("Rpt") = oRpt
                    CRViewer.ReportSource = oRpt

                Else

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
                End If


            End If




        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.TankChartReport()", ex)
        End Try


    End Sub

    Private Sub PollingResultsReport()
        Try
            Dim qry As String
            Dim dsPResults As DataSet
            Dim DAL As New GeneralizedDAL
            Dim objDataTable As New System.Data.DataTable
            Dim Uinfo As UserInfo = CType(Session("Uinfo"), UserInfo)
            GenFun = New GeneralFunctions
            Dim oRpt = New ReportDocument()
            oRpt.Load(Server.MapPath(GenFun.LoadReport(Uinfo.ReportID)))
            qry = "SELECT * FROM PRESULTS ORDER BY [DATETIME] ASC"
            dsPResults = DAL.GetDataSet(qry)
            If dsPResults.Tables(0).Rows.Count > 0 Then
                objDataTable = dsPResults.Tables(0)
                oRpt.SetDataSource(objDataTable)
                LogON(oRpt)
                Session("Rpt") = oRpt
                CRViewer.ReportSource = oRpt
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found!');window.close();</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ReportViewer.PollingResultsReport()", ex)
        End Try
    End Sub
End Class
