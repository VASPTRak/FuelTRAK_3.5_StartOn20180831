Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Reporting.WebControls
Imports CrystalDecisions.Shared

Public Class GeneralFunctions
    Dim pruser As UserInfo
    Dim ParameterValue As ParameterDiscreteValue()

    Public Function AuthenticateUser(ByVal pruser As UserInfo) As DataSet
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim username = New SqlParameter("@UserName", SqlDbType.VarChar, 100)
            ' Dim password = New SqlParameter("@Password", SqlDbType.VarChar, 100)
            username.Direction = ParameterDirection.Input
            username.Value = pruser.Username
            ' password.Direction = ParameterDirection.Input
            ' password.Value = pruser.Password
            parcollection(0) = username
            '  parcollection(1) = password
            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_Ldap_LoginAuthenticate", parcollection)
            If Not ds Is DBNull.Value Then
                Return ds
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("AuthenticateUser", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return Nothing
        End Try
    End Function
    Public Function AuthenticateAdmin(ByVal pruser As UserInfo) As DataSet
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(1) As SqlParameter
            Dim username = New SqlParameter("@UserName", SqlDbType.VarChar, 100)
            Dim password = New SqlParameter("@Password", SqlDbType.VarChar, 100)
            username.Direction = ParameterDirection.Input
            username.Value = pruser.Username
            password.Direction = ParameterDirection.Input
            password.Value = pruser.Password
            parcollection(0) = username
            parcollection(1) = password
            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_LoginAuthenticate", parcollection)
            If Not ds Is DBNull.Value Then
                Return ds
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("AuthenticateUser", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return Nothing
        End Try
    End Function

    Public Function Get_Company_Name() As String
        Try
            Dim dal = New GeneralizedDAL()
            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_GetCompany_Name")
            If Not ds Is Nothing Then
                If (ds.Tables(0).Rows.Count > 0) Then
                    Return ds.Tables(0).Rows(0)(0).ToString()
                End If
            Else
                Return ""
            End If
            Return ""
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Get_Company_Name", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return ""
        End Try

    End Function

    Public Function Get_Report_name(ByVal reportid As Integer) As String
        Try
            Select Case (reportid)
                Case 11 ''Transaction List in Date/Time Order
                    Get_Report_name = "Transactions in Date/Time Order"
                Case 104 ''Transaction List By Customized Order
                    Get_Report_name = "Transaction Customized List - Sheboygan County"
                Case 12 ''Transaction List in Sentry Order
                    Get_Report_name = "Transactions in Sentry Order"
                Case 13 ''Transaction List in Personnel Order
                    Get_Report_name = "Transactions in Personnel Order"
                Case 14 ''Transaction List in Vehicle Order
                    Get_Report_name = "Transactions in Vehicle Order"
                Case 15 ''Transaction List of Errors
                    Get_Report_name = "Transactions of Errors"
                Case 16 ''Transaction List of Master Key Usage
                    Get_Report_name = "Transaction List of Master Key Usage"
                Case 17 ''Transaction List by Vehicle Tyep
                    Get_Report_name = "Transactions List by Vehicle Type"
                Case 21
                    Get_Report_name = "Sentry Report in Date/Time Order"
                Case 22
                    Get_Report_name = "Sentry Report - Totals Only"
                Case 23
                    Get_Report_name = "Sentry Report in Vehicle Order"
                Case 31
                    Get_Report_name = "Billing - Details"
                Case 131
                    Get_Report_name = "Detail Billing Report � Personnel"
                Case 32
                    Get_Report_name = "Billing - Vehicle Summary"
                Case 33
                    Get_Report_name = "Billing - Department Summary"
                Case 34
                    Get_Report_name = "Wright Express Billing Report"
                Case 35
                    Get_Report_name = "Wright Express Billing Report-Sentry Details"
                Case 41
                    Get_Report_name = "Vehicle List by Department"
                Case 42
                    Get_Report_name = "Fuel Use by Department"
                Case 43
                    Get_Report_name = "Vehicle List with Trouble Codes"
                    'Added By Varun Moota, new Vehicle DTC Report.04/13/2011
                Case 110
                    Get_Report_name = "Vehicle - DTC List"
                Case 44
                    Get_Report_name = "Vehicle List in Identity Order"
                Case 45
                    Get_Report_name = "Vehicle List of PM Due"
                Case 46
                    Get_Report_name = "Vehicle Performance Report"
                Case 47
                    Get_Report_name = "Vehicle Mileage Override Report"
                Case 48
                    Get_Report_name = "Vehicle List by Type"
                Case 49
                    Get_Report_name = "Vehicle List - FA Calibration"
                Case 50
                    Get_Report_name = "MPG Deviation Report"
                Case 51
                    Get_Report_name = "Personnel List"
                Case 52
                    Get_Report_name = "Personnel List in ID Order"
                Case 53
                    Get_Report_name = "Personnel List in Last Name Order"
                Case 61
                    'Get_Report_name = "Fuel Use by Last Name"
                    Get_Report_name = "Fuel Use by Personnel"
                Case 62
                    Get_Report_name = "Fuel Use by Vehicle - Summary"
                Case 63
                    Get_Report_name = "Fuel Use by Department"
                Case 64
                    Get_Report_name = "Fuel Use Report by Type"
                Case 65
                    Get_Report_name = "Fuel Use Report by Vehicle - Detail"
                Case 66
                    Get_Report_name = "Fuel Use Report by Personnel/Dept"
                Case 71
                    Get_Report_name = "Inventory Activity"
                Case 72
                    Get_Report_name = "Tank Balance"
                Case 73
                    Get_Report_name = "Tank Reconciliation"
                Case 74
                    Get_Report_name = "Pump Totalizer Report"
                Case 75
                    Get_Report_name = "Percent Usage Report"
                Case 76
                    Get_Report_name = "Tank Reconciliation, No Dippings"
                Case 77
                    Get_Report_name = "Tank Balance Report - FIFO"
                Case 78
                    Get_Report_name = "Inventory Information - No Fuel TXTN"
                Case 79
                    Get_Report_name = "Tank Current Balance Report"
                Case 88
                    Get_Report_name = "Export Summary Report"
                Case 91
                    Get_Report_name = "Vehicle History Summary"
                Case 92
                    Get_Report_name = "Vehicle that have not Fueled"
                    'Added By Varun Moota 12/07/2009
                Case 101
                    Get_Report_name = "Site Analysis Summary Report"
                    
                Case 302
                    Get_Report_name = "Polling Results Report"
                Case 303
                    Get_Report_name = "Vehicle List with SPN Codes"
                Case 304
                    Get_Report_name = "Vehicle List with Engine DTC"
                Case 401
                    Get_Report_name = "Transactions By Dept"
                Case 402
                    Get_Report_name = "Transaction Detail � Exceeds Miles Window"
                Case 403
                    Get_Report_name = "Transaction Detail � MPG out-of-range"
                Case Else
                    Get_Report_name = "UNKOWN REPORT"
            End Select

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Get_Report_name", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Get_Report_name = ""
        End Try

    End Function

    Public Function Report(ByVal ReportID As Integer, ByVal userI As UserInfo) As String
        Dim strQuery As String = ""

        Select Case userI.ReportID

            '' 11 - Transaction List in Date/Time Order,
            '' 12 - Transaction List in Sentry Order
            '' 13 - Transaction List in Personnel Order
            '' 14 - Transaction List in Vehicle Order
            '' 15 - Transaction List of Errors
            '' 16 - Transaction List - Master Key Usage
            '' 17 - Transaction List - By Vehicle Type

            '' 21 - Sentry Report in Date/Time Order
            '' 22 - Sentry Report - Totals Only
            '' 23 - Sentry Report in Vehicle Order

            '' 31 - Dept Billing Report - Dept Summary
            '' 32 - Dept Billing Report - Vehicle Summary
            '' 33 - Dept Billing Report - Department Summary
            '' 34 - Wright Express Billing Report

            '' 41 - Vehicle List by Department
            '' 42 - Fuel Use by Dept/Vehicle Summary
            '' 43 - Vehicle Trouble Code Report
            '' 44 - Vehicle List in Identity Order
            '' 45 - Vehicle List of PM Due
            '' 46 - Vehicle Performance Report
            '' 47 - 
            '' 48 - Vehicle List by Type
            '' 49 - Vehicle List-FA Calibration
            '' 50 - MPG Deviation Report
            '' 91 - Vehicle History Summary
            '' 92 - Vehicle that have not Fueled

            '' 51 - Personnel List in Dept/Name Order
            '' 52 - Personnel List in ID Order
            '' 53 - Personnel List in Last Name Order

            '' 61 - Fuel Use by Personnel
            '' 62 - Fuel Use by Vehicle - Summary
            '' 63 - Fuel Use by Department
            '' 64 - Fuel Use Report By Type
            '' 65 - Fuel Use By Vehilce-Detail

            '' 71 - Inventory Activity
            '' 72 - Tank Balance
            '' 73 - Tank Reconciliation
            '' 74 - Pump Totalizer Report
            '' 75 - Percent Usage Report
            '' 76 - Tank Reconciliation, No Dippings
            '' 77 - Tank Balance-FIFO
            '' 78 - Inventory Information-No Fuel TXTN
            '' 79 - Tank Current Balance Report

            '' 81 - Listing - Departments
            '' 82 - Listing - Site Information
            '' 83 - Listing - Tanks
            '' 84 - Listing - Lockouts
            '' 85 - Listing - Current Export File
            '' 86 - Polling Results
            '' 87 - List of Overrides
            '' 88 - Export Summary Report
            ''303 - Vehicle OBDII Trouble Code Report 
            ''401 - Transaction Report by Dept 
            ''402 - Transaction Detail � Exceeds Miles Window
            ''403 - Transaction Detail � MPG out-of-range

            Case 21, 22, 23
                strQuery = Sentry_Report_Query(ReportID, userI)
            Case 11, 104, 12, 13, 14, 15, 16, 17, 31, 32, 33, 61, 401, 402, 403, 131
                strQuery = Department_Report_Query(ReportID, userI)
            Case 41, 42, 44, 48, 49, 91, 92
                strQuery = Vehicle_Report_Query(ReportID, userI)
            Case 43, 303, 304
                strQuery = Vehicle_TroubleCode_Report(ReportID, userI)
            Case 46
                strQuery = VehiclePerformance_Report_Query(ReportID, userI)
            Case 51
                strQuery = Personnel_Report_Query(ReportID, userI)
            Case 52, 53
                'strQuery = Personnel_Report_Query(ReportID, userI)
                'Commented By Varun
                strQuery = Personnel_Report_Query_Per(ReportID, userI)
            Case 62, 63, 64, 65
                strQuery = Fuel_Report_Query(ReportID, userI)
            Case 71, 73, 75, 76, 77, 78, 79
                strQuery = Inventory_Query(ReportID, userI)
        End Select
        Return strQuery
    End Function

    Private Function Inventory_Query(ByVal reportid As Integer, ByVal pruser As UserInfo) As String
        'pruser = New UserInfo
        Inventory_Query = ""

        If reportid = 76 Or reportid = 73 Then
            If Not pruser.StartTank = "" And Not pruser.EndTank = "" Then
                Inventory_Query += " WHERE LTRIM(RTRIM(TANK.NUMBER)) BETWEEN '" + pruser.StartTank + "' AND '" + pruser.EndTank + "'"
            ElseIf Not (pruser.StartTank = "") Then
                Inventory_Query += " WHERE LTRIM(RTRIM(TANK.NUMBER)) >='" + pruser.StartTank + "''"
            ElseIf Not (pruser.EndTank = "") Then
                Inventory_Query += " WHERE LTRIM(RTRIM(TANK.NUMBER)) <='" + pruser.EndTank + "''"
            End If
        End If
        If reportid = 75 Then
            If Not pruser.StartTank = "" And Not pruser.EndTank = "" Then
                Inventory_Query += " WHERE LTRIM(RTRIM(frtd.tank_nbr)) BETWEEN '" + pruser.StartTank + "' AND '" + pruser.EndTank + "'"
            ElseIf Not (pruser.StartTank = "") Then
                Inventory_Query += " WHERE LTRIM(RTRIM(frtd.tank_nbr)) >='" + pruser.StartTank + "'"
            ElseIf Not (pruser.EndTank = "") Then
                Inventory_Query += " WHERE LTRIM(RTRIM(frtd.tank_nbr)) <='" + pruser.EndTank + "'"
            End If
        End If
        If reportid = 71 Or reportid = 77 Or reportid = 78 Then
            If Not pruser.StartTank = "" And Not pruser.EndTank = "" Then
                Inventory_Query += " AND LTRIM(RTRIM(frtd.tank_nbr)) BETWEEN '" + pruser.StartTank + "' AND '" + pruser.EndTank + "'"
            ElseIf Not (pruser.StartTank = "") Then
                Inventory_Query += " AND LTRIM(RTRIM(frtd.tank_nbr)) >='" + pruser.StartTank + "'"
            ElseIf Not (pruser.EndTank = "") Then
                Inventory_Query += " AND LTRIM(RTRIM(frtd.tank_nbr)) <='" + pruser.EndTank + "'"
            End If
        End If
        Select Case reportid
            Case 71
                pruser.ReportTitle = "Inventory Activity Report"
            Case 73
                pruser.ReportTitle = "Tank Reconciliation Report"
            Case 75
                pruser.ReportTitle = "Percent Usage Report"
            Case 76
                pruser.ReportTitle = "Tank Reconciliation, No Dippings Report"
            Case 77
                pruser.ReportTitle = "Tank Balance Report-FIFO Report"
            Case 78
                pruser.ReportTitle = "Inventory Information-No Fuel TXTN Report"
            Case 79
                pruser.ReportTitle = "Tank Current Balance Report"
        End Select
        Return Inventory_Query
    End Function

    'Private Function Transaction_Report_Query(ByVal ReportID As Integer) As String
    '    Dim strQuery As String = ""

    '    strQuery = " SELECT TXTN.SENTRY, TXTN.NUMBER, TXTN.DATETIME, TANK.NUMBER AS TANK, TXTN.PUMP, TXTN.PRODUCT," & _
    '               " TXTN.QUANTITY, TXTN.VEHICLE, TXTN.MILES,TXTN.PREV_MILES, TXTN.HOURS, TXTN.ERRORS," & _
    '               " TXTN.VKEY, TXTN.PKEY, TXTN.PERSONNEL, TXTN.[OPTION] FROM TXTN INNER JOIN TANK ON " & _
    '               " CONVERT(INT,TXTN.PRODUCT) = CONVERT(INT,TANK.NUMBER) " & _
    '               " WHERE TXTN.[DATETIME] BETWEEN '" & pruser.StartDate & "' AND '" & pruser.EndDate & "'"

    '    ''WHERE CRITERIA FOR VEHICLE
    '    If Not pruser.StartVehicle = "" And Not pruser.EndVehicle = "" Then
    '        strQuery += " AND LTRIM(RTRIM(txtn.VEHICLE)) BETWEEN '" + pruser.StartVehicle + "' AND '" + pruser.EndVehicle + "'"
    '    ElseIf Not (pruser.StartVehicle = "") Then
    '        strQuery += " AND LTRIM(RTRIM(txtn.VEHICLE)) >='" + pruser.StartVehicle + "'"
    '    ElseIf Not (pruser.EndVehicle = "") Then
    '        strQuery += " AND LTRIM(RTRIM(txtn.VEHICLE)) <='" + pruser.EndVehicle + "'"
    '    End If

    '    ''WHERE CRITERIA FOR DEPARTMENT
    '    If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
    '        strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT >=" + pruser.StartDepartment & _
    '          " AND VEHS.DEPT <=" + pruser.EndDepartment + ")"
    '    ElseIf Not (pruser.StartDepartment = "") Then
    '        strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT >=" + pruser.StartDepartment + ")"
    '    ElseIf Not (pruser.EndDepartment = "") Then
    '        strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT <=" + pruser.EndDepartment + ")"
    '    End If

    '    ''WHERE CRITERIA FOR SENTRY
    '    If Not pruser.StartSentry = "" And Not pruser.EndSentry = "" Then
    '        strQuery += " AND LTRIM(RTRIM(TXTN.SENTRY)) BETWEEN '" + pruser.StartSentry + "' AND '" + pruser.EndSentry + "'"
    '    ElseIf Not (pruser.StartSentry = "") Then
    '        strQuery += " AND LTRIM(RTRIM(TXTN.SENTRY)) =" + pruser.StartSentry + ""
    '    ElseIf Not (pruser.EndSentry = "") Then
    '        strQuery += " AND LTRIM(RTRIM(TXTN.SENTRY)) =" + pruser.EndSentry + ""
    '    End If

    '    ''WHERE CRITERIA FOR VEHICLE TYPE
    '    If Not pruser.StartVehType = "" And Not pruser.EndVehicleType = "" Then
    '        strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >='" + pruser.StartVehType & _
    '                    "' AND VEHS.TYPE <='" + pruser.EndVehicleType + "')"
    '    ElseIf Not (pruser.StartVehType = "") Then
    '        strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >='" + pruser.StartVehType + "')"
    '    ElseIf Not (pruser.EndVehicleType = "") Then
    '        strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE <='" + pruser.EndVehicleType + "')"
    '    End If

    '    ''WHERE CRITERIA FOR TANK
    '    If Not pruser.StartTank = "" And Not pruser.EndTank = "" Then
    '        strQuery += " and TXTN.PRODUCT >='" + pruser.StartTank & _
    '                    "' AND TXTN.PRODUCT  <='" + pruser.EndTank + "'"
    '    ElseIf Not (pruser.StartTank = "") Then
    '        strQuery += " and TXTN.PRODUCT >='" + pruser.StartTank + "'"
    '    ElseIf Not (pruser.EndTank = "") Then
    '        strQuery += " AND TXTN.PRODUCT  <='" + pruser.EndTank + "'"
    '    End If

    '    ''WHERE CRITERIA FOR VEHICLE KEY
    '    If Not pruser.StartVKey = "" And Not pruser.EndVKey = "" Then
    '        strQuery += " AND LTRIM(RTRIM(TXTN.VKEY)) BETWEEN '" + pruser.StartVKey + "' AND '" + pruser.EndVKey + "'"
    '    ElseIf Not (pruser.StartVKey = "") Then
    '        strQuery += " AND LTRIM(RTRIM(TXTN.VKEY)) >='" + pruser.StartVKey + "'"
    '    ElseIf Not (pruser.EndVKey = "") Then
    '        strQuery += " AND LTRIM(RTRIM(TXTN.VKEY)) <='" + pruser.EndVKey + "'"
    '    End If

    '    ''WHERE CRITERIA FOR PERSONNEL
    '    If Not pruser.StartPer = "" And Not pruser.EndPer = "" Then
    '        'strQuery += " AND LTRIM(RTRIM(TXTN.PERSONNEL)) BETWEEN '" + pruser.StartPer + "' AND '" + pruser.EndPer + "'"
    '        strQuery += " AND ((PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "') OR (PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'))"
    '    ElseIf Not (pruser.StartPer = "") Then
    '        strQuery += " AND PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "'"
    '    ElseIf Not (pruser.EndPer = "") Then
    '        strQuery += " AND PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'"
    '    End If

    '    ''WHERE CRITERIA FOR SHOW ZERO TRANSACTIONS CONDITION.
    '    If pruser.CheckBoxStatus = True Then
    '        strQuery += " AND QUANTITY = 0 "
    '    End If
    '    ''ORDER BY CRITERIA
    '    Select Case (ReportID)
    '        Case 11 ''Transaction List in Date/Time Order
    '            strQuery += " ORDER BY TXTN.[DATETIME]"
    '            pruser.ReportTitle = "Transaction List in Date/Time Order"
    '        Case 12 ''Transaction List in Sentry Order
    '            strQuery += " ORDER BY LTRIM(RTRIM(TXTN.SENTRY))"
    '            pruser.ReportTitle = "Transaction List in Sentry Order"
    '        Case 13 ''Transaction List in Personnel Order
    '            strQuery += " ORDER BY TXTN.PERSONNEL"
    '            pruser.ReportTitle = "Transaction List in Personnel Order"
    '        Case 14 ''Transaction List in Vehicle Order
    '            strQuery += " ORDER BY TXTN.VEHICLE"
    '            pruser.ReportTitle = "Transaction List in Vehicle Order"
    '        Case 15 ''Transaction List of Errors
    '            strQuery += " and TXTN.ERRORS <> ''  ORDER BY TXTN.ERRORS"
    '            pruser.ReportTitle = "Transaction List of Errors"
    '    End Select
    '    Return strQuery
    'End Function

    Private Function Sentry_Report_Query(ByVal ReportID As Integer, ByVal pruser As UserInfo) As String
        Dim strQuery As String = ""
        Select Case ReportID
            Case 21
                strQuery = ""
                pruser.ReportTitle = "Sentry Report in Date/Time Order"
            Case 22
                strQuery = ""
                pruser.ReportTitle = "Sentry Report - Totals Only"
            Case 23
                strQuery = ""
                pruser.ReportTitle = "Sentry Report in Vehicle Order"
        End Select

        ''WHERE CRITERIA FOR SENTRY
        If Not (LTrim(RTrim(pruser.StartSentry))) = "" And Not (LTrim(RTrim(pruser.EndSentry))) = "" Then
            strQuery += "   AND   TXTN.SENTRY   BETWEEN    '" + pruser.StartSentry + "' AND  '" + pruser.EndSentry + "' "
        ElseIf Not (pruser.StartSentry = "") Then
            strQuery += " AND (LTRIM(RTRIM(TXTN.SENTRY)) >='" + pruser.StartSentry + "') "
        ElseIf Not (pruser.EndSentry = "") Then
            strQuery += " AND (LTRIM(RTRIM(TXTN.SENTRY)) <='" + pruser.EndSentry + "') "
        End If

        'WHERE CRITERIA FOR VEHICLE
        If Not (LTrim(RTrim(pruser.StartVehicle))) = "" And Not (LTrim(RTrim(pruser.EndVehicle))) = "" Then
            strQuery += "    AND    ( TXTN.VEHICLE BETWEEN   '" + pruser.StartVehicle + "'  AND   '" + pruser.EndVehicle + "')   "
        ElseIf Not (pruser.StartVehicle = "") Then
            strQuery += " AND  (LTRIM(RTRIM(TXTN.VEHICLE)) >='" + pruser.StartVehicle + "') "
        ElseIf Not (pruser.EndVehicle = "") Then
            strQuery += " AND  (LTRIM(RTRIM(TXTN.VEHICLE)) <='" + pruser.EndVehicle + "') "
        End If
        'WHERE CRITERIA FOR DEPARTMENT
        If Not (LTrim(RTrim(pruser.StartDepartment))) = "" And Not (LTrim(RTrim(pruser.EndDepartment))) = "" Then
            strQuery += "    AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT BETWEEN  '" + pruser.StartDepartment & _
                  "' AND '" + pruser.EndDepartment + "') "
        ElseIf Not (pruser.StartDepartment = "") Then
            strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT >=" + pruser.StartDepartment + ") "
        ElseIf Not (pruser.EndDepartment = "") Then
            strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT <=" + pruser.EndDepartment + ") "
        End If


        ''WHERE CRITERIA FOR VEHICLE TYPE
        If Not (LTrim(RTrim(pruser.StartVehType))) = "" And Not (LTrim(RTrim(pruser.EndVehicleType))) = "" Then
            strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >='" + pruser.StartVehType & _
                            "' AND VEHS.TYPE <='" + pruser.EndVehicleType + "') "
        ElseIf Not (pruser.StartVehType = "") Then
            strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >='" + pruser.StartVehType + "') "
        ElseIf Not (pruser.EndVehicleType = "") Then
            strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE <='" + pruser.EndVehicleType + "') "
        End If

        ''WHERE CRITERIA FOR TANK
        If Not (LTrim(RTrim(pruser.StartTank))) = "" And Not (LTrim(RTrim(pruser.EndTank))) = "" Then
            strQuery += " AND TXTN.PRODUCT  >='" + pruser.StartTank & _
                            "' AND TXTN.PRODUCT <='" + pruser.EndTank + "'"
        ElseIf Not (pruser.StartTank = "") Then
            strQuery += " AND TXTN.PRODUCT ='" + pruser.StartTank + ""
        ElseIf Not (pruser.EndTank = "") Then
            strQuery += " AND TXTN.PRODUCT =" + pruser.EndTank + ""
        End If

        ''WHERE CRITERIA FOR VEHICLE KEY
        If Not (LTrim(RTrim(pruser.StartVKey))) = "" And Not (LTrim(RTrim(pruser.EndVKey))) = "" Then
            strQuery += " AND (LTRIM(RTRIM(TXTN.VKEY)) BETWEEN   '" + pruser.StartVKey + "' AND '" + pruser.EndVKey + "') "
        ElseIf Not (pruser.StartVKey = "") Then
            strQuery += " AND (LTRIM(RTRIM(TXTN.VKEY)) >='" + pruser.StartVKey + "') "
        ElseIf Not (pruser.EndVKey = "") Then
            strQuery += " AND (LTRIM(RTRIM(TXTN.VKEY)) <='" + pruser.EndVKey + "') "
        End If

        ''WHERE CRITERIA FOR PERSONNEL
        If Not pruser.StartPer = "" And Not pruser.EndPer = "" Then
            'strQuery += " AND LTRIM(RTRIM(TXTN.PERSONNEL)) BETWEEN '" + pruser.StartPer + "' AND '" + pruser.EndPer + "'"
            strQuery += " AND ((PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "') OR (PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'))"
        ElseIf Not (pruser.StartPer = "") Then
            strQuery += " AND PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "'"
        ElseIf Not (pruser.EndPer = "") Then
            strQuery += " AND PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'"
        End If

        ''WHERE CRITERIA FOR SHOW ZERO TRANSACTIONS CONDITION.
        If pruser.CheckBoxStatus = True Then
            strQuery += " AND QUANTITY = 0 "
        End If
        ''ORDER BY CRITERIA
        Select Case ReportID
            Case 21
                ' strQuery += "  ORDER BY SENTRY.NUMBER"
            Case 22
                ' strQuery += "  ORDER BY TXTN.SENTRY"
            Case 23
                ' strQuery += "  ORDER BY CONVERT(int, TXTN.VEHICLE)"
        End Select
        Return strQuery
    End Function

    Private Function Department_Report_Query(ByVal ReportID As Integer, ByVal pruser As UserInfo) As String
        Dim strQuery As String = ""

        Select Case (ReportID)
            Case 11 ''Transaction List in Date/Time Order
                pruser.ReportTitle = "Transaction List in Date/Time Order"
            Case 104 ''Transaction List By Customized Order
                pruser.ReportTitle = "Transaction Customized List - Sheboygan County"
            Case 12 ''Transaction List in Sentry Order
                pruser.ReportTitle = "Transaction List in Sentry Order"
            Case 13 ''Transaction List in Personnel Order
                pruser.ReportTitle = "Transaction List in Personnel Order"
            Case 14 ''Transaction List in Vehicle Order
                pruser.ReportTitle = "Transaction List in Vehicle Order"
            Case 15 ''Transaction List of Errors
                pruser.ReportTitle = "Transaction List of Errors"
            Case 16 ''Transaction List of Master Key Usage
                pruser.ReportTitle = "Transaction List of Master Key Usage"
            Case 17 ''Transaction List of Master Key Usage
                pruser.ReportTitle = "Transactions List by Vehicle Type"
            Case 31
                pruser.ReportTitle = "Dept Billing Report - Details"
            Case 131
                pruser.ReportTitle = "Detail Billing Report � Personnel"
            Case 32
                pruser.ReportTitle = "Dept Billing Report - Vehicle Summary"
            Case 33
                pruser.ReportTitle = "Dept Billing Report - Department Summary"
            Case 61 ''Fuel Use by Personnel
                pruser.ReportTitle = "Fuel Use by Personnel"
                'pruser.ReportTitle = "Fuel Use by Last Name"
            Case 401 ''Fuel Use by Personnel
                pruser.ReportTitle = "Transaction Report by Dept"
            Case 402
                pruser.ReportTitle = "Transaction Detail � Exceeds Miles Window"
            Case 403
                pruser.ReportTitle = "Transaction Detail � MPG out-of-range"
        End Select

        'WHERE CRITERIA FOR VEHICLE
        If Not (LTrim(RTrim(pruser.StartVehicle))) = "" And Not (LTrim(RTrim(pruser.EndVehicle))) = "" Then
            ''strQuery += "  AND LTRIM(RTRIM(TXTN.VEHICLE)) BETWEEN   '" + pruser.StartVehicle + "' AND '" + pruser.EndVehicle + "'"
            strQuery += "  AND LTRIM(RTRIM(TXTN.VEHICLE)) >=   '" + pruser.StartVehicle + "' AND LTRIM(RTRIM(TXTN.VEHICLE)) <=   '" + pruser.EndVehicle + "'"
        ElseIf Not (pruser.StartVehicle = "") Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.VEHICLE)) >= '" + pruser.StartVehicle + "'"
        ElseIf Not (pruser.EndVehicle = "") Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.VEHICLE)) <= '" + pruser.EndVehicle + "')"
        End If
        'WHERE CRITERIA FOR DEPARTMENT
        If (ReportID = 401) Then 'Changed By Varun Moota, for Transaction report for Dept.
            If Not (LTrim(RTrim(pruser.StartDepartment))) = "" And Not (LTrim(RTrim(pruser.EndDepartment))) = "" Then
                strQuery += " AND TXTN.DEPT IN (SELECT DEPT.[NUMBER] FROM DEPT WHERE DEPT.[NUMBER] >=" + pruser.StartDepartment & _
                      " AND DEPT.[NUMBER] <=" + pruser.EndDepartment + ")"
            ElseIf Not (pruser.StartDepartment = "") Then
                strQuery += " AND TXTN.DEPT IN (SELECT DEPT.[NUMBER] FROM DEPT WHERE DEPT.[NUMBER] >=" + pruser.StartDepartment + ")"
            ElseIf Not (pruser.EndDepartment = "") Then
                strQuery += " AND TXTN.DEPT IN (SELECT DEPT.[NUMBER] FROM DEPT WHERE DEPT.[NUMBER] <=" + pruser.EndDepartment + ")"
            End If
        ElseIf (ReportID = 131) Then
            If Not (LTrim(RTrim(pruser.StartDepartment))) = "" And Not (LTrim(RTrim(pruser.EndDepartment))) = "" Then
                strQuery += " AND TXTN.PERSONNEL IN (SELECT Pers.[IDENTITY] FROM Pers WHERE Pers.DEPT >=" + pruser.StartDepartment & _
                      " AND Pers.DEPT <=" + pruser.EndDepartment + ")"
            ElseIf Not (pruser.StartDepartment = "") Then
                strQuery += " AND TXTN.PERSONNEL IN (SELECT Pers.[IDENTITY] FROM Pers WHERE Pers.DEPT >=" + pruser.StartDepartment + ")"
            ElseIf Not (pruser.EndDepartment = "") Then
                strQuery += " AND TXTN.PERSONNEL IN (SELECT Pers.[IDENTITY] FROM Pers WHERE Pers.DEPT <=" + pruser.EndDepartment + ")"
            End If
        Else
            If Not (LTrim(RTrim(pruser.StartDepartment))) = "" And Not (LTrim(RTrim(pruser.EndDepartment))) = "" Then
                strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT >=" + pruser.StartDepartment & _
                      " AND VEHS.DEPT <=" + pruser.EndDepartment + ")"
            ElseIf Not (pruser.StartDepartment = "") Then
                strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT >=" + pruser.StartDepartment + ")"
            ElseIf Not (pruser.EndDepartment = "") Then
                strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT <=" + pruser.EndDepartment + ")"
            End If
        End If

        ''WHERE CRITERIA FOR SENTRY
        If Not (LTrim(RTrim(pruser.StartSentry))) = "" And Not (LTrim(RTrim(pruser.EndSentry))) = "" Then
            strQuery += "  AND (LTRIM(RTRIM(TXTN.SENTRY)) BETWEEN   '" + pruser.StartSentry + "' AND '" + pruser.EndSentry + "')"
        ElseIf Not (pruser.StartSentry = "") Then
            strQuery += "  AND (LTRIM(RTRIM(TXTN.SENTRY)) >='" + pruser.StartSentry + "')"
        ElseIf Not (pruser.EndSentry = "") Then
            strQuery += "  AND (LTRIM(RTRIM(TXTN.SENTRY)) <='" + pruser.EndSentry + "')"
        End If

        ''WHERE CRITERIA FOR VEHICLE TYPE
        If Not (LTrim(RTrim(pruser.StartVehType))) = "" And Not (LTrim(RTrim(pruser.EndVehicleType))) = "" Then
            strQuery += "  AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >='" + pruser.StartVehType & _
                            "'  AND VEHS.TYPE <='" + pruser.EndVehicleType + "')"
        ElseIf Not (pruser.StartVehType = "") Then
            strQuery += "  AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >='" + pruser.StartVehType + "')"
        ElseIf Not (pruser.EndVehicleType = "") Then
            strQuery += "  AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE <='" + pruser.EndVehicleType + "')"
        End If

        ''WHERE CRITERIA FOR TANK
        If Not (LTrim(RTrim(pruser.StartTank))) = "" And Not (LTrim(RTrim(pruser.EndTank))) = "" Then
            strQuery += "  AND TXTN.TANK between " + pruser.StartTank & _
                            " AND " + pruser.EndTank + ""
        ElseIf Not (pruser.StartTank = "") Then
            strQuery += "  AND TXTN.TANK >=" + pruser.StartTank + ""
        ElseIf Not (pruser.EndTank = "") Then
            strQuery += "  AND TXTN.TANK <=" + pruser.EndTank + ""
        End If

        ''WHERE CRITERIA FOR VEHICLE KEY
        If Not (LTrim(RTrim(pruser.StartVKey))) = "" And Not (LTrim(RTrim(pruser.EndVKey))) = "" Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.VKEY)) BETWEEN   '" + pruser.StartVKey + "'  AND   '" + pruser.EndVKey + "'"
        ElseIf Not (pruser.StartVKey = "") Then
            strQuery += " AND LTRIM(RTRIM(TXTN.VKEY)) >='" + pruser.StartVKey + "'"
        ElseIf Not (pruser.EndVKey = "") Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.VKEY)) <='" + pruser.EndVKey + "'"
        End If

        ''WHERE CRITERIA FOR Personnel Id
        If Not (LTrim(RTrim(pruser.StartPerID))) = "" And Not (LTrim(RTrim(pruser.EndPerID))) = "" Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.PERSONNEL)) BETWEEN   '" + pruser.StartPerID + "'  AND   '" + pruser.EndPerID + "'"
        ElseIf Not (pruser.StartPerID = "") Then
            strQuery += " AND LTRIM(RTRIM(TXTN.PERSONNEL)) >='" + pruser.StartPerID + "'"
        ElseIf Not (pruser.EndPerID = "") Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.PERSONNEL)) <='" + pruser.EndPerID + "'"
        End If

        'WHERE CRITERIA FOR PERSONNEL
        If Not (LTrim(RTrim(pruser.StartPer))) = "" And Not (LTrim(RTrim(pruser.EndPer))) = "" Then
            'strQuery += " AND LTRIM(RTRIM(TXTN.PERSONNEL)) BETWEEN '" + pruser.StartPer + "' AND '" + pruser.EndPer + "'"
            strQuery += " AND ((PERS.LAST_NAME LIKE '" + pruser.StartPer + "%'" + ") OR (PERS.LAST_NAME LIKE '" + pruser.EndPer + "%'" + "))"
        ElseIf Not (pruser.StartPer = "") Then
            strQuery += " AND PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "'"
        ElseIf Not (pruser.EndPer = "") Then
            strQuery += " AND PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'"
        End If
        If ReportID = 15 Then
            strQuery += "AND ISNULL(TXTN.ERRORS,'') <> '' "
        End If
        If ReportID = 16 Then strQuery += "  AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.MASTER ='True')"

        If ReportID = 402 Then strQuery += " AND (VEHS.MILES_WIND IS NOT NULL) AND VEHS.MILES_WIND<>0 AND (TXTN.MILES-TXTN.PREV_MILES) > VEHS.MILES_WIND "

        If ReportID = 403 Then strQuery += "AND ((TXTN.MILES - TXTN.PREV_MILES)/ CASE WHEN TXTN.QUANTITY = 0 THEN 1 ELSE TXTN.QUANTITY END ) <= " & pruser.TransMPG & " "

        ''WHERE CRITERIA FOR SHOW ZERO TRANSACTIONS CONDITION.
        If pruser.CheckBoxStatus = True Then strQuery += " AND QUANTITY = 0 "

        Return strQuery
    End Function

    Private Function VehiclePerformance_Report_Query(ByVal ReportID As Integer, ByVal pruser As UserInfo) As String
        Dim strQuery As String = ""

        'WHERE CRITERIA FOR VEHICLE
        If Not (LTrim(RTrim(pruser.StartVehicle))) = "" And Not (LTrim(RTrim(pruser.EndVehicle))) = "" Then
            ''strQuery += "  AND LTRIM(RTRIM(TXTN.VEHICLE)) BETWEEN   '" + pruser.StartVehicle + "' AND '" + pruser.EndVehicle + "'"
            strQuery += "  AND LTRIM(RTRIM(TXTN.VEHICLE)) >=   '" + pruser.StartVehicle + "' AND LTRIM(RTRIM(TXTN.VEHICLE)) <=   '" + pruser.EndVehicle + "'"
        ElseIf Not (pruser.StartVehicle = "") Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.VEHICLE)) >= '" + pruser.StartVehicle + "'"
        ElseIf Not (pruser.EndVehicle = "") Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.VEHICLE)) <= '" + pruser.EndVehicle + "'"
        End If
        'WHERE CRITERIA FOR DEPARTMENT
        If Not (LTrim(RTrim(pruser.StartDepartment))) = "" And Not (LTrim(RTrim(pruser.EndDepartment))) = "" Then
            strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT >=" + pruser.StartDepartment & _
                  " AND VEHS.DEPT <=" + pruser.EndDepartment + ")"
        ElseIf Not (pruser.StartDepartment = "") Then
            strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT >=" + pruser.StartDepartment + ")"
        ElseIf Not (pruser.EndDepartment = "") Then
            strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT <=" + pruser.EndDepartment + ")"
        End If

        ''WHERE CRITERIA FOR SENTRY
        If Not (LTrim(RTrim(pruser.StartSentry))) = "" And Not (LTrim(RTrim(pruser.EndSentry))) = "" Then
            strQuery += "  AND (LTRIM(RTRIM(TXTN.SENTRY)) BETWEEN   '" + pruser.StartSentry + "' AND '" + pruser.EndSentry + "')"
        ElseIf Not (pruser.StartSentry = "") Then
            strQuery += "  AND (LTRIM(RTRIM(TXTN.SENTRY)) >='" + pruser.StartSentry + "')"
        ElseIf Not (pruser.EndSentry = "") Then
            strQuery += "  AND (LTRIM(RTRIM(TXTN.SENTRY)) <='" + pruser.EndSentry + "')"
        End If

        ''WHERE CRITERIA FOR VEHICLE TYPE
        If Not (LTrim(RTrim(pruser.StartVehType))) = "" And Not (LTrim(RTrim(pruser.EndVehicleType))) = "" Then
            strQuery += "  AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >=" + pruser.StartVehType & _
                            "  AND VEHS.TYPE <=" + pruser.EndVehicleType + ")"
        ElseIf Not (pruser.StartVehType = "") Then
            strQuery += "  AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >=" + pruser.StartVehType + ")"
        ElseIf Not (pruser.EndVehicleType = "") Then
            strQuery += "  AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE <=" + pruser.EndVehicleType + ")"
        End If

        ' ''WHERE CRITERIA FOR TANK
        'If Not (LTrim(RTrim(pruser.StartTank))) = "" And Not (LTrim(RTrim(pruser.EndTank))) = "" Then
        '    strQuery += "  AND TANK.NUMBER between " + pruser.StartTank & _
        '                    " AND " + pruser.EndTank + ""
        'ElseIf Not (pruser.StartTank = "") Then
        '    strQuery += "  AND TANK.NUMBER >=" + pruser.StartTank + ""
        'ElseIf Not (pruser.EndTank = "") Then
        '    strQuery += "  AND TANK.NUMBER <=" + pruser.EndTank + ""
        'End If

        ''WHERE CRITERIA FOR VEHICLE KEY
        If Not (LTrim(RTrim(pruser.StartVKey))) = "" And Not (LTrim(RTrim(pruser.EndVKey))) = "" Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.VKEY)) BETWEEN   " + pruser.StartVKey + "  AND   " + pruser.EndVKey + ""
        ElseIf Not (pruser.StartVKey = "") Then
            strQuery += " AND LTRIM(RTRIM(TXTN.VKEY)) >=" + pruser.StartVKey + ""
        ElseIf Not (pruser.EndVKey = "") Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.VKEY)) <=" + pruser.EndVKey + ""
        End If

        ''WHERE CRITERIA FOR VEHICLE KEY
        If Not (LTrim(RTrim(pruser.StartPerID))) = "" And Not (LTrim(RTrim(pruser.EndPerID))) = "" Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.PERSONNEL)) BETWEEN   " + pruser.StartPerID + "  AND   " + pruser.EndPerID + ""
        ElseIf Not (pruser.StartPerID = "") Then
            strQuery += " AND LTRIM(RTRIM(TXTN.PERSONNEL)) >=" + pruser.StartVKey + ""
        ElseIf Not (pruser.EndPerID = "") Then
            strQuery += "  AND LTRIM(RTRIM(TXTN.PERSONNEL)) <=" + pruser.EndVKey + ""
        End If

        ''WHERE CRITERIA FOR SHOW ZERO TRANSACTIONS CONDITION.
        If pruser.CheckBoxStatus = True Then
            strQuery += " AND QUANTITY = 0 "
        End If

        Select Case (ReportID)
            Case 46
                strQuery += " ORDER BY VEHS.[IDENTITY]"
                pruser.ReportTitle = "Vehicle Performance Report"
        End Select

        Return strQuery
    End Function

    Private Function Vehicle_Report_Query(ByVal ReportID As Integer, ByVal pruser As UserInfo) As String
        Try
            Dim strQuery As String = ""
            If ReportID = 41 Then
                strQuery = "SELECT VEHS.[IDENTITY],CASE LEN(VEHS.CARD_ID) WHEN 0 THEN VEHS.KEY_NUMBER ELSE VEHS.CARD_ID END AS CARD_ID,VEHS.[TYPE],VEHS.MILEAGE,VEHS.LASTFUELER, dbo.Vehs.LASTFUELDT," & _
                       "VEHS.VEHYEAR,VEHS.VEHMAKE,VEHS.VEHMODEL, VEHS.LICNO, VEHS.VEHVIN, DEPT.[NAME], FUELS,VEHS.CURRMILES,Vehs.CURRHOURS " & _
                       "FROM VEHS,DEPT WHERE LTRIM(RTRIM(VEHS.DEPT)) = LTRIM(RTRIM(DEPT.NUMBER)) "
            Else
                strQuery = ""
            End If
            ''WHERE CRITERIA FOR VEHICLE IDENTITY
            If Not pruser.StartVehicle = "" And Not pruser.EndVehicle = "" Then
                strQuery += " AND LTRIM(RTRIM(VEHS.[IDENTITY])) BETWEEN '" + pruser.StartVehicle + "' AND '" + pruser.EndVehicle + "'"
            ElseIf Not (pruser.StartVehicle = "") Then
                strQuery += " AND LTRIM(RTRIM(VEHS.[IDENTITY])) >= convert(char(16)," + pruser.StartVehicle + ",20)"
            ElseIf Not (pruser.EndVehicle = "") Then
                strQuery += " AND LTRIM(RTRIM(VEHS.[IDENTITY])) <= convert(char(16)," + pruser.EndVehicle + ",20)"
            End If

            ''WHERE CRITERIA FOR DEPATMENT
            If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
                strQuery += " AND LTRIM(RTRIM(VEHS.DEPT)) BETWEEN  '" + pruser.StartDepartment + "' AND '" + pruser.EndDepartment + "'"
            ElseIf Not (pruser.StartDepartment) = "" Then
                strQuery += " AND LTRIM(RTRIM(VEHS.DEPT)) >= '" + pruser.StartDepartment + "'"
            ElseIf Not (pruser.EndDepartment) = "" Then
                strQuery += " AND LTRIM(RTRIM(VEHS.DEPT)) <= '" + pruser.EndDepartment + "'"
            End If

            If ReportID = 41 Or ReportID = 44 Or ReportID = 48 Or ReportID = 49 Or ReportID = 91 Or ReportID = 92 Then
                ''WHERE CRITERIA FOR VEHICLE TYPE
                If Not pruser.StartVehType = "" And Not pruser.EndVehicleType = "" Then
                    strQuery += " AND LTRIM(RTRIM(VEHS.TYPE)) BETWEEN '" + pruser.StartVehType + "' AND '" + pruser.EndVehicleType + "'"
                ElseIf Not (pruser.StartVehType = "") Then
                    strQuery += " AND LTRIM(RTRIM(VEHS.TYPE)) >= '" + pruser.StartVehType + "'"
                ElseIf Not (pruser.EndVehicleType = "") Then
                    strQuery += " AND LTRIM(RTRIM(VEHS.TYPE)) <= '" + pruser.EndVehicleType + "'"
                End If

                ''WHERE CRITERIA FOR VEHICLE KEY
                If Not pruser.StartVKey = "" And Not pruser.EndVKey = "" Then
                    strQuery += " AND LTRIM(RTRIM(VEHS.KEY_NUMBER)) BETWEEN '" + pruser.StartVKey + "' AND '" + pruser.EndVKey + "'"
                ElseIf Not (pruser.StartVKey) = "" Then
                    strQuery += " AND LTRIM(RTRIM(VEHS.KEY_NUMBER)) >= '" + pruser.StartVKey + "'"
                ElseIf Not (pruser.EndVKey) = "" Then
                    strQuery += " AND LTRIM(RTRIM(VEHS.KEY_NUMBER)) <= '" + pruser.EndVKey + "'"
                End If
            End If

            ''WHERE CRITERIA FOR SHOW ZERO TRANSACTIONS CONDITION.
            If pruser.CheckBoxStatus = True Then
                strQuery += " AND VEHS.NEWUPDT <= 3 "
            Else
                strQuery += " AND VEHS.NEWUPDT < 3 "
            End If
            ''ORDER BY CRITERIA
            Select Case (ReportID)
                Case 41
                    strQuery += " ORDER BY DEPT.[NAME],VEHS.[IDENTITY]"
                    pruser.ReportTitle = "Vehicle List by Department"
                Case 42
                    pruser.ReportTitle = "Fuel Use by Dept/Vehicle Summary"
                    strQuery += " GROUP BY TXTN.VEHICLE,DEPT.[NAME],DEPT.NUMBER ORDER BY DEPT.[NAME],TXTN.VEHICLE "
                Case 44
                    strQuery += " ORDER BY VEHS.[IDENTITY]"
                    pruser.ReportTitle = "Vehicle List in Identity Order "
                Case 48
                    strQuery += " ORDER BY VEHS.[IDENTITY]"
                    pruser.ReportTitle = "Vehicle List by Type"
                Case 49
                    strQuery += " ORDER BY VEHS.[IDENTITY]"
                    pruser.ReportTitle = "Vehicle List - FA Calibration"
                Case 91
                    pruser.ReportTitle = "Vehicle History Summary"
                Case 92
                    pruser.ReportTitle = "Vehicle that have not Fueled"
            End Select
            Return strQuery
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.Vehicle_Report_Query()", ex)
            Return ""
        End Try
    End Function

    Private Function Vehicle_TroubleCode_Report(ByVal ReportID As Integer, ByVal pruser As UserInfo) As String
        Try

      
            Dim strQuery As String = ""

            ''WHERE CRITERIA FOR SHOW ZERO TRANSACTIONS CONDITION.
            If pruser.CheckBoxStatus = True Then
                strQuery += " AND VEHS.NEWUPDT <= 3 "
            Else
                strQuery += " AND VEHS.NEWUPDT < 3 "
            End If
            ''WHERE CRITERIA FOR VEHICLE IDENTITY
            If Not pruser.StartVehicle = "" And Not pruser.EndVehicle = "" Then
                strQuery += " AND (LTRIM(RTRIM(VEHS.[IDENTITY])) BETWEEN '" + pruser.StartVehicle + "' AND '" + pruser.EndVehicle + "') "
            ElseIf Not (pruser.StartVehicle = "") Then
                strQuery += "AND (VEHS.[IDENTITY] >= '" + pruser.StartVehicle + "')" 'AND (LTRIM(RTRIM(VEHS.[IDENTITY])) >= '" + pruser.StartVehicle + "')"
            ElseIf Not (pruser.EndVehicle = "") Then
                strQuery += " AND (LTRIM(RTRIM(VEHS.[IDENTITY])) <= '" + pruser.EndVehicle + "')"
            End If

            ''WHERE CRITERIA FOR DEPATMENT
            If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
                strQuery += " AND LTRIM(RTRIM(VEHS.DEPT)) BETWEEN  '" + pruser.StartDepartment + "' AND '" + pruser.EndDepartment + "'"
            ElseIf Not (pruser.StartDepartment) = "" Then
                strQuery += " AND LTRIM(RTRIM(VEHS.DEPT)) >= '" + pruser.StartDepartment + "'"
            ElseIf Not (pruser.EndDepartment) = "" Then
                strQuery += " AND LTRIM(RTRIM(VEHS.DEPT)) <= '" + pruser.EndDepartment + "'"
            End If

            ''ORDER BY CRITERIA
            Select Case (ReportID)
                Case 43
                    'strQuery += " ORDER BY VEHS.[IDENTITY]"
                    pruser.ReportTitle = "Vehicle List with Trouble Codes"
                Case 303
                    ''WHERE CRITERIA FOR SPN Codes
                    If Not pruser.StartSPN = "" And Not pruser.EndSPN = "" Then
                        strQuery += " AND LTRIM(RTRIM(VTC.SPN)) BETWEEN  " + pruser.StartSPN + " AND " + pruser.EndSPN + " "
                    ElseIf Not (pruser.StartSPN) = "" Then
                        strQuery += " AND LTRIM(RTRIM(VTC.SPN)) >= " + pruser.StartSPN + " "
                    ElseIf Not (pruser.EndSPN) = "" Then
                        strQuery += " AND LTRIM(RTRIM(VTC.SPN)) <= " + pruser.EndSPN + " "
                    End If

                    pruser.ReportTitle = "Vehicle List with SPN Codes"

                Case 304
                    pruser.ReportTitle = "Vehicle List with Engine DTC"


            End Select
            Return strQuery
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.Vehicle_TroubleCode_Report", ex)
            Return ""
        End Try
    End Function

    Private Function Personnel_Report_Query_Per(ByVal ReportID As Integer, ByVal pruser As UserInfo) As String
        Try
            If Personnel_Report_Query_Per = "" Then
                If Not pruser.StartPer = "" And Not pruser.EndPer = "" Then
                    'Commented  By Varun
                    'Personnel_Report_Query_Per += "  AND ((PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "') OR (PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'))"
                    'Added By Varun
                    Personnel_Report_Query_Per += " WHERE  PERS.LAST_NAME BETWEEN '" + pruser.StartPer + "' AND '" + pruser.EndPer + "'"
                ElseIf Not (pruser.StartPer = "") Then
                    Personnel_Report_Query_Per += "  WHERE  PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "'"
                ElseIf Not (pruser.EndPer = "") Then
                    Personnel_Report_Query_Per += "  AND  PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'"
                End If


                'WHERE CRITERIA FOR DEPATMENT
                If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
                    Personnel_Report_Query_Per += " WHERE LTRIM(RTRIM(PERS.DEPT)) BETWEEN  '" + pruser.StartDepartment + "' AND '" + pruser.EndDepartment + "'"
                ElseIf Not (pruser.StartDepartment) = "" Then
                    Personnel_Report_Query_Per += " WHERE LTRIM(RTRIM(PERS.DEPT)) >= '" + pruser.StartDepartment + "'"
                ElseIf Not (pruser.EndDepartment) = "" Then
                    Personnel_Report_Query_Per += " AND LTRIM(RTRIM(PERS.DEPT)) <= '" + pruser.EndDepartment + "'"
                End If

                'WHERE CRITERIA FOR DEPATMENT
                ' ''If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
                ' ''    Personnel_Report_Query_Per += " AND LTRIM(RTRIM(PERS.DEPT)) BETWEEN  '" + pruser.StartDepartment + "' AND '" + pruser.EndDepartment + "'"
                ' ''ElseIf Not (pruser.StartDepartment) = "" Then
                ' ''    Personnel_Report_Query_Per += " AND LTRIM(RTRIM(PERS.DEPT)) >= '" + pruser.StartDepartment + "'"
                ' ''ElseIf Not (pruser.EndDepartment) = "" Then
                ' ''    Personnel_Report_Query_Per += " AND LTRIM(RTRIM(PERS.DEPT)) <= '" + pruser.EndDepartment + "'"
                ' ''End If

                'Added By Varun to Show Personnel Numbers 
                If Not pruser.StartPerID = "" And Not pruser.EndPerID = "" Then
                    Personnel_Report_Query_Per += " WHERE LTRIM(RTRIM(PERS.[IDENTITY])) BETWEEN  '" + pruser.StartPerID + "' AND '" + pruser.EndPerID + "'"
                ElseIf Not (pruser.StartPerID) = "" Then
                    Personnel_Report_Query_Per += " WHERE LTRIM(RTRIM(PERS.[IDENTITY])) >= '" + pruser.StartPerID + "'"
                ElseIf Not (pruser.EndPerID) = "" Then
                    Personnel_Report_Query_Per += " AND LTRIM(RTRIM(PERS.[IDENTITY])) <= '" + pruser.EndPerID + "'"
                End If



                ''WHERE CRITERIA FOR SHOW ZERO TRANSACTIONS CONDITION.
                If pruser.CheckBoxStatus = True Then
                    If Personnel_Report_Query_Per = "" Then
                        Personnel_Report_Query_Per += " WHERE PERS.NEWUPDT <= 3 "
                    Else
                        Personnel_Report_Query_Per += " AND PERS.NEWUPDT <= 3 "
                    End If
                Else
                    If Personnel_Report_Query_Per = "" Then
                        Personnel_Report_Query_Per += " WHERE PERS.NEWUPDT < 3 " '
                    Else
                        Personnel_Report_Query_Per += " AND PERS.NEWUPDT < 3 "
                    End If
                End If
                Select Case ReportID
                    Case 51
                        pruser.ReportTitle = "Personnel List in Dept/Name Order"
                        Personnel_Report_Query_Per += "  ORDER BY LTRIM(RTRIM(DEPT.NUMBER)),PERS.LAST_NAME,PERS.FIRST_NAME"
                    Case 52
                        pruser.ReportTitle = "Personnel List in ID Order"
                        Personnel_Report_Query_Per += "  ORDER BY PERS.[IDENTITY]"
                    Case 53
                        pruser.ReportTitle = "Personnel List in Last Name Order"
                        Personnel_Report_Query_Per += "  ORDER BY PERS.LAST_NAME"
                End Select

            End If
            'Commented By Varun
            'WHERE CRITERIA FOR PERSONNEL
            ' ''If Not pruser.StartPer = "" And Not pruser.EndPer = "" Then
            ' ''    Personnel_Report_Query_Per += "  WHERE ((PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "') OR (PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'))"
            ' ''ElseIf Not (pruser.StartPer = "") Then
            ' ''    Personnel_Report_Query_Per += "  WHERE  PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "'"
            ' ''ElseIf Not (pruser.EndPer = "") Then
            ' ''    Personnel_Report_Query_Per += "  WHERE  PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'"
            ' ''End If
            ' ''If Personnel_Report_Query_Per = "" Then
            ' ''    'WHERE CRITERIA FOR DEPATMENT
            ' ''    If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
            ' ''        Personnel_Report_Query_Per += " WHERE LTRIM(RTRIM(PERS.DEPT)) BETWEEN  '" + pruser.StartDepartment + "' AND '" + pruser.EndDepartment + "'"
            ' ''    ElseIf Not (pruser.StartDepartment) = "" Then
            ' ''        Personnel_Report_Query_Per += " WHERE LTRIM(RTRIM(PERS.DEPT)) >= '" + pruser.StartDepartment + "'"
            ' ''    ElseIf Not (pruser.EndDepartment) = "" Then
            ' ''        Personnel_Report_Query_Per += " WHERE LTRIM(RTRIM(PERS.DEPT)) <= '" + pruser.EndDepartment + "'"
            ' ''    End If
            ' ''Else
            ' ''    'WHERE CRITERIA FOR DEPATMENT
            ' ''    If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
            ' ''        Personnel_Report_Query_Per += " AND LTRIM(RTRIM(PERS.DEPT)) BETWEEN  '" + pruser.StartDepartment + "' AND '" + pruser.EndDepartment + "'"
            ' ''    ElseIf Not (pruser.StartDepartment) = "" Then
            ' ''        Personnel_Report_Query_Per += " AND LTRIM(RTRIM(PERS.DEPT)) >= '" + pruser.StartDepartment + "'"
            ' ''    ElseIf Not (pruser.EndDepartment) = "" Then
            ' ''        Personnel_Report_Query_Per += " AND LTRIM(RTRIM(PERS.DEPT)) <= '" + pruser.EndDepartment + "'"
            ' ''    End If
            ' ''End If
            ' '' ''WHERE CRITERIA FOR SHOW ZERO TRANSACTIONS CONDITION.
            ' ''If Personnel_Report_Query_Per = "" Then
            ' ''    If pruser.CheckBoxStatus = True Then
            ' ''        Personnel_Report_Query_Per += " WHERE PERS.NEWUPDT <= 3 "
            ' ''    Else
            ' ''        Personnel_Report_Query_Per += " WHERE PERS.NEWUPDT < 3 "
            ' ''    End If
            ' ''Else
            ' ''    If pruser.CheckBoxStatus = True Then
            ' ''        Personnel_Report_Query_Per += " AND PERS.NEWUPDT <= 3 "
            ' ''    Else
            ' ''        Personnel_Report_Query_Per += " AND PERS.NEWUPDT < 3 "
            ' ''    End If
            ' ''End If
            ' ''Select Case ReportID
            ' ''    Case 52
            ' ''        pruser.ReportTitle = "Personnel List in ID Order"
            ' ''        Personnel_Report_Query_Per += "  ORDER BY PERS.[IDENTITY]"
            ' ''    Case 53
            ' ''        pruser.ReportTitle = "Personnel List in Last Name Order"
            ' ''        Personnel_Report_Query_Per += "  ORDER BY PERS.LAST_NAME"
            ' ''End Select

            Return Personnel_Report_Query_Per
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.Personnel_Report_Query_Per", ex)
            Personnel_Report_Query_Per = ""
        End Try

    End Function

    Private Function Personnel_Report_Query(ByVal ReportID As Integer, ByVal pruser As UserInfo) As String
        Try

    
            'WHERE CRITERIA FOR PERSONNEL
            If Personnel_Report_Query = "" Then
                If Not pruser.StartPer = "" And Not pruser.EndPer = "" Then
                    'Commented  By Varun
                    'Personnel_Report_Query += "  AND ((PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "') OR (PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'))"
                    'Added By Varun
                    Personnel_Report_Query += " AND  PERS.LAST_NAME BETWEEN '" + pruser.StartPer + "' AND '" + pruser.EndPer + "'"
                ElseIf Not (pruser.StartPer = "") Then
                    Personnel_Report_Query += "  AND  PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "'"
                ElseIf Not (pruser.EndPer = "") Then
                    Personnel_Report_Query += "  AND  PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'"
                End If


                'WHERE CRITERIA FOR DEPATMENT
                If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
                    Personnel_Report_Query += " AND LTRIM(RTRIM(PERS.DEPT)) BETWEEN  '" + pruser.StartDepartment + "' AND '" + pruser.EndDepartment + "'"
                ElseIf Not (pruser.StartDepartment) = "" Then
                    Personnel_Report_Query += " AND LTRIM(RTRIM(PERS.DEPT)) >= '" + pruser.StartDepartment + "'"
                ElseIf Not (pruser.EndDepartment) = "" Then
                    Personnel_Report_Query += " AND LTRIM(RTRIM(PERS.DEPT)) <= '" + pruser.EndDepartment + "'"
                End If

                'WHERE CRITERIA FOR DEPATMENT
                ' ''If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
                ' ''    Personnel_Report_Query += " AND LTRIM(RTRIM(PERS.DEPT)) BETWEEN  '" + pruser.StartDepartment + "' AND '" + pruser.EndDepartment + "'"
                ' ''ElseIf Not (pruser.StartDepartment) = "" Then
                ' ''    Personnel_Report_Query += " AND LTRIM(RTRIM(PERS.DEPT)) >= '" + pruser.StartDepartment + "'"
                ' ''ElseIf Not (pruser.EndDepartment) = "" Then
                ' ''    Personnel_Report_Query += " AND LTRIM(RTRIM(PERS.DEPT)) <= '" + pruser.EndDepartment + "'"
                ' ''End If

                'Added By Varun to Show PERsonnel Numbers 
                If Not pruser.StartPerID = "" And Not pruser.EndPerID = "" Then
                    Personnel_Report_Query += " AND LTRIM(RTRIM(PERS.[IDENTITY])) BETWEEN  '" + pruser.StartPerID + "' AND '" + pruser.EndPerID + "'"
                ElseIf Not (pruser.StartPerID) = "" Then
                    Personnel_Report_Query += " AND LTRIM(RTRIM(PERS.[IDENTITY])) >= '" + pruser.StartPerID + "'"
                ElseIf Not (pruser.EndPerID) = "" Then
                    Personnel_Report_Query += " AND LTRIM(RTRIM(PERS.[IDENTITY])) <= '" + pruser.EndPerID + "'"
                End If



                ''WHERE CRITERIA FOR SHOW ZERO TRANSACTIONS CONDITION.
                If pruser.CheckBoxStatus = True Then
                    Personnel_Report_Query += " AND PERS.NEWUPDT <= 3 "
                Else
                    Personnel_Report_Query += " AND PERS.NEWUPDT < 3 "
                End If
                Select Case ReportID
                    Case 51
                        pruser.ReportTitle = "Personnel List in Dept/Name Order"
                        Personnel_Report_Query += "  ORDER BY LTRIM(RTRIM(DEPT.NUMBER)),PERS.LAST_NAME,PERS.FIRST_NAME"
                    Case 52
                        pruser.ReportTitle = "Personnel List in ID Order"
                        Personnel_Report_Query += "  ORDER BY PERS.[IDENTITY]"
                    Case 53
                        pruser.ReportTitle = "Personnel List in Last Name Order"
                        Personnel_Report_Query += "  ORDER BY PERS.LAST_NAME"
                End Select

            End If

            Return Personnel_Report_Query
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.Personnel_Report_Query_Per", ex)
            Personnel_Report_Query = ""
        End Try

    End Function

    Private Function Fuel_Report_Query(ByVal ReportID As Integer, ByVal pruser As UserInfo) As String
        Try

     
            Dim strQuery As String = ""

            If ReportID = 61 Then
                strQuery = "SELECT TXTN.SENTRY, TXTN.NUMBER, TXTN.DATETIME,TXTN.PUMP,TXTN.PRODUCT," & _
                           "TXTN.QUANTITY,TXTN.COST,TXTN.MILES,TXTN.PREV_MILES,TXTN.ERRORS,TXTN.VEHICLE," & _
                           "TXTN.VKEY, TXTN.PKEY, TXTN.PERSONNEL,PRODUCT.[NAME] FROM TXTN " & _
                           "INNER JOIN PRODUCT ON LTrim(RTrim(TXTN.PRODUCT)) = LTrim(RTrim(PRODUCT.NUMBER))" & _
                           "WHERE TXTN.[DATETIME] BETWEEN '" & pruser.StartDate & "' AND '" & pruser.EndDate & "'"
            ElseIf ReportID = 62 Then
                ' strQuery = "SELECT TXTN.VEHICLE,VEHS.EXTENSION,SUM(TXTN.MILES)AS MILES,SUM(TXTN.QUANTITY)AS QTY," & _
                '            " SUM(TXTN.MPG)AS MPG,SUM(CONVERT(FLOAT,TXTN.COST))AS COST FROM TXTN, VEHS " & _
                '            " WHERE(LTrim(RTrim(TXTN.VEHICLE)) = LTrim(RTrim(VEHS.[IDENTITY]))) " & _
                '            " AND TXTN.[DATETIME] BETWEEN '" & pruser.StartDate & "' AND '" & pruser.EndDate & "'"
            ElseIf ReportID = 63 Then
                'strQuery = "SELECT DEPT.NUMBER,DEPT.[NAME],TXTN.VEHICLE,VEHS.EXTENSION,SUM(TXTN.MILES)AS MILES," & _
                ' '          " SUM(TXTN.QUANTITY)AS QTY,SUM(TXTN.MPG)AS MPG,SUM(CONVERT(FLOAT,TXTN.COST))AS COST " & _
                '           " FROM TXTN INNER JOIN VEHS ON LTrim(RTrim(TXTN.VEHICLE)) = LTrim(RTrim(VEHS.[IDENTITY]))" & _
                '           " INNER JOIN DEPT ON LTrim(RTrim(VEHS.DEPT)) = LTrim(RTrim(DEPT.NUMBER)) " & _
                '           " WHERE TXTN.[DATETIME] BETWEEN '" & pruser.StartDate & "' AND '" & pruser.EndDate & "'"

            End If

            ''WHERE CRITERIA FOR VEHICLE
            If Not pruser.StartVehicle = "" And Not pruser.EndVehicle = "" Then
                strQuery += " AND LTRIM(RTRIM(TXTN.VEHICLE)) BETWEEN '" + pruser.StartVehicle + "' AND '" + pruser.EndVehicle + "'"
            ElseIf Not (pruser.StartVehicle = "") Then
                strQuery += " AND LTRIM(RTRIM(TXTN.VEHICLE)) >='" + pruser.StartVehicle + "'"
            ElseIf Not (pruser.EndVehicle = "") Then
                strQuery += " AND LTRIM(RTRIM(TXTN.VEHICLE)) <='" + pruser.EndVehicle + "'"
            End If

            ''Changed By Varun Moota, look for Dept in TXTN.DEPT
            ''WHERE CRITERIA FOR DEPARTMENT
            If Not pruser.StartDepartment = "" And Not pruser.EndDepartment = "" Then
                ' ''strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT >=" + pruser.StartDepartment & _
                ' ''  " AND VEHS.DEPT <=" + pruser.EndDepartment + ")"
                strQuery += " AND LTRIM(RTRIM(TXTN.DEPT)) BETWEEN '" + pruser.StartDepartment + "' AND '" + pruser.EndDepartment + "'"
            ElseIf Not (pruser.StartDepartment = "") Then
                '''strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT >=" + pruser.StartDepartment + ")"
                strQuery += " AND LTRIM(RTRIM(TXTN.Dept)) >='" + pruser.StartDepartment + "'"
            ElseIf Not (pruser.EndDepartment = "") Then
                '''strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.DEPT <=" + pruser.EndDepartment + ")"
                strQuery += " AND LTRIM(RTRIM(TXTN.Dept)) <='" + pruser.EndDepartment + "'"
            End If

            ''WHERE CRITERIA FOR SENTRY
            If Not pruser.StartSentry = "" And Not pruser.EndSentry = "" Then
                strQuery += " AND LTRIM(RTRIM(TXTN.SENTRY)) BETWEEN '" + pruser.StartSentry + "' AND '" + pruser.EndSentry + "'"
            ElseIf Not (pruser.StartSentry = "") Then
                strQuery += " AND LTRIM(RTRIM(TXTN.SENTRY)) >='" + pruser.StartSentry + "'"
            ElseIf Not (pruser.EndSentry = "") Then
                strQuery += " AND LTRIM(RTRIM(TXTN.SENTRY)) <='" + pruser.EndSentry + "'"
            End If

            ''WHERE CRITERIA FOR VEHICLE TYPE
            If Not pruser.StartVehType = "" And Not pruser.EndVehicleType = "" Then
                strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >='" + pruser.StartVehType & _
                            "' AND VEHS.TYPE <='" + pruser.EndVehicleType + "')"
            ElseIf Not (pruser.StartVehType = "") Then
                strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE >='" + pruser.StartVehType + "')"
            ElseIf Not (pruser.EndVehicleType = "") Then
                strQuery += " AND TXTN.VEHICLE IN (SELECT VEHS.[IDENTITY] FROM VEHS WHERE VEHS.TYPE <='" + pruser.EndVehicleType + "')"
            End If


            ''WHERE CRITERIA FOR TANK
            'Commeneted By Varun Moota, since we don't have Meter Readings in FuelTRAK DB.01/26/2011
            ' ''If Not pruser.StartTank = "" And Not pruser.EndTank = "" Then
            ' ''    strQuery += " AND TXTN.SENTRY IN (SELECT METER.SENTRY FROM METER WHERE METER.TANK >='" + pruser.StartTank & _
            ' ''                "' AND METER.TANK <='" + pruser.EndTank + "')"
            ' ''ElseIf Not (pruser.StartTank = "") Then
            ' ''    strQuery += " AND TXTN.SENTRY IN (SELECT METER.SENTRY FROM METER WHERE METER.TANK >='" + pruser.StartTank + "')"
            ' ''ElseIf Not (pruser.EndTank = "") Then
            ' ''    strQuery += " AND TXTN.SENTRY IN (SELECT METER.SENTRY FROM METER WHERE METER.TANK <='" + pruser.EndTank + "')"
            ' ''End If
            'Added By varun Moota, since FuelTRAK Reports fail to retrieve data when Tank Search criteria being used.01/26/2011
            If Not pruser.StartTank = "" And Not pruser.EndTank = "" Then
                strQuery += " AND TXTN.TANK IN (SELECT TANK.[NUMBER] FROM TANK WHERE TANK.[NUMBER] >='" + pruser.StartTank & _
                            "' AND TANK.[NUMBER] <='" + pruser.EndTank + "')"
            ElseIf Not (pruser.StartTank = "") Then
                strQuery += " AND TXTN.TANK IN (SELECT TANK.[NUMBER] FROM TANK WHERE TANK.[NUMBER] >='" + pruser.StartTank + "')"
            ElseIf Not (pruser.EndTank = "") Then
                strQuery += " AND TXTN.TANK IN (SELECT TANK.[NUMBER] FROM TANK WHERE TANK.[NUMBER]<='" + pruser.EndTank + "')"
            End If



            ''WHERE CRITERIA FOR VEHICLE KEY
            If Not pruser.StartVKey = "" And Not pruser.EndVKey = "" Then
                strQuery += " AND LTRIM(RTRIM(TXTN.VKEY)) BETWEEN '" + pruser.StartVKey + "' AND '" + pruser.EndVKey + "'"
            ElseIf Not (pruser.StartVKey = "") Then
                strQuery += " AND LTRIM(RTRIM(TXTN.VKEY)) >='" + pruser.StartVKey + "'"
            ElseIf Not (pruser.EndVKey = "") Then
                strQuery += " AND LTRIM(RTRIM(TXTN.VKEY)) <='" + pruser.EndVKey + "'"
            End If

            ''WHERE CRITERIA FOR PERSONNEL
            If Not pruser.StartPer = "" And Not pruser.EndPer = "" Then
                strQuery += " AND ((PERS.LAST_NAME LIKE '" + pruser.StartPer + "'%'" + "') OR (PERS.LAST_NAME LIKE '" + pruser.EndPer + "'%'" + "'))"
            ElseIf Not (pruser.StartPer = "") Then
                strQuery += " AND PERS.LAST_NAME LIKE '" + pruser.StartPer + "%" + "'"
            ElseIf Not (pruser.EndPer = "") Then
                strQuery += " AND PERS.LAST_NAME LIKE '" + pruser.EndPer + "%" + "'"
            End If

            ''WHERE CRITERIA TO SHOW ZERO TRANSACTIONS CONDITION.
            If pruser.CheckBoxStatus = True Then
                strQuery += " AND QUANTITY = 0 "
            End If

            ''ORDER BY CRITERIA
            Select Case (ReportID)
                Case 62 ''Fuel Use by Vehicle - Summary
                    pruser.ReportTitle = "Fuel Use by Vehicle - Summary"
                Case 63 ''Fuel Use by Department
                    pruser.ReportTitle = "Fuel Use by Department"
               

            End Select
            Return strQuery
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.Personnel_Report_Query_Per", ex)
            Return Nothing
        End Try
    End Function

    Public Function GetUserList() As IDataAdapter
        Try

     
            Dim dal = New GeneralizedDAL()

            Dim dap As SqlDataAdapter
            dap = dal.ExecuteStoredProcedureGetDataAdapter("usp_tt_UserList")
            If Not dap Is Nothing Then
                Return dap
            Else
                Return Nothing
            End If
            Return Nothing
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.GetUserList()", ex)
            Return Nothing
        End Try
    End Function

    Public Function GetUserDetails(ByVal pruser As UserInfo) As DataSet
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim USER_ID = New SqlParameter("@USER_ID", SqlDbType.VarChar, 100)
            USER_ID.Direction = ParameterDirection.Input
            USER_ID.Value = pruser.EditUserID
            parcollection(0) = USER_ID
            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_UserDetails", parcollection)
            If Not ds Is DBNull.Value Then
                Return ds
            Else
                Return Nothing
            End If
            Return Nothing
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.GetUserDetails()", ex)
            Return Nothing
        End Try
    End Function

    Public Function CheckUserExistance(ByVal UName As String) As DataSet
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim Login = New SqlParameter("@Login", SqlDbType.VarChar, 10)
            Login.Direction = ParameterDirection.Input
            Login.Value = UName
            parcollection(0) = Login
            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_UserExistance", parcollection)
            If Not ds Is DBNull.Value Then
                Return ds
            Else
                Return Nothing
            End If
            Return Nothing
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.CheckUserExistance()", ex)
            Return Nothing
        End Try
    End Function

    Public Function LoadReport(ByVal ReportId As Integer) As String
        Try
            Dim reportname As String = ""
            Select Case ReportId
                Case 1
                    reportname = "~/Reports/ExportSummary.rpt"
                Case 2
                    reportname = "~/Reports/Translation.rpt"
                Case 11, 12, 13, 14
                    reportname = "~/Reports/TransactionReport.rpt"
                Case 104
                    reportname = "~/Reports/TransactionReportCustomized.rpt"
                Case 402
                    reportname = "~/Reports/TransactionReportMiles.rpt"
                Case 403
                    reportname = "~/Reports/TransactionReportMPG.rpt"
                Case 15
                    reportname = "~/Reports/TxtnListError.rpt"
                Case 16
                    reportname = "~/Reports/TxtnMasterKeyUsage.rpt"
                Case 17
                    reportname = "~/Reports/TransVehicleType.rpt"
                Case 21
                    reportname = "~/Reports/Sentrymain.rpt"
                Case 22
                    reportname = "~/Reports/SentryTotalOnly.rpt"
                Case 23
                    reportname = "~/Reports/SentryVehicleOrder.rpt"
                Case 31
                    reportname = "~/Reports/Bill_DeptReport.rpt"
                Case 131
                    reportname = "~/Reports/PersDeptBill_DeptReport.rpt"
                Case 32
                    reportname = "~/Reports/Bill_VehSummReport.rpt"
                Case 33
                    reportname = "~/Reports/Bill_DeptSummReport.rpt"
                Case 34
                    reportname = "~/Reports/WrightExpressBilling.rpt"
                Case 35
                    reportname = "~/Reports/WrightExpressBilling_Sub1.rpt"
                Case 41
                    reportname = "~/Reports/VehicleReport.rpt"
                Case 42
                    reportname = "~/Reports/VehicleReport_FuelUsebyDept.rpt"
                Case 43
                    reportname = "~/Reports/VehTroubleCodeReport.rpt"
                    'New Report Vehicle DTC-List.04/13/2011
                Case 110
                    reportname = "~/Reports/Veh-DTC List.rpt"
                Case 44
                    reportname = "~/Reports/VehicleListIDOrder.rpt"
                Case 45
                    reportname = "~/Reports/VehicleListOfPMDue.rpt"
                Case 46
                    reportname = "~/Reports/VehPerformanceReport.rpt"
                Case 48
                    reportname = "~/Reports/VehicleListByType.rpt"
                Case 49
                    reportname = "~/Reports/VehicleListFA-Calibration.rpt"
                Case 50
                    reportname = "~/Reports/VehicleMPGDeviationReport.rpt"
                Case 51
                    reportname = "~/Reports/PersonnelReport.rpt"
                Case 52, 53
                    reportname = "~/Reports/PersonnelListID_LastName.rpt"
                Case 61
                    reportname = "~/Reports/FuelUseReport_byPersonnelSP.rpt"
                Case 62
                    reportname = "~/Reports/FuelUseReport_byVehicleSP.rpt"
                Case 63
                    reportname = "~/Reports/FuelUseReport_byDepartment2.rpt"

                    'New Report For Vallecitos.Varun Moota(11/19/2010)
                Case 64
                    reportname = "~/Reports/FuelUse_byVehType.rpt"
                    'New Report For St.Louis County.Varun Moota(01/01/27)
                Case 65
                    reportname = "~/Reports/FuelUse_byVehDetail.rpt"
                    'New Report For ISD.Varun Moota(01/01/27)
                Case 66
                    reportname = "~/Reports/FuelUseReport_byPersonnelDept.rpt"
                Case 71
                    reportname = "~/Reports/Inventory_Activity.rpt"
                Case 72
                    reportname = "~/Reports/TankBalance.rpt"
                Case 73
                    reportname = "~/Reports/InvTankReconciliation.rpt"
                Case 74
                    reportname = "~/Reports/InventoryPumpTotalizer.rpt"
                Case 75
                    reportname = "~/Reports/InvPercentUsageReport.rpt"
                Case 76
                    reportname = "~/Reports/InvTankReconciliationNoDippings.rpt"
                Case 77
                    reportname = "~/Reports/InvTankBalanceReport_FIFO.rpt"
                Case 78
                    reportname = "~/Reports/InventoryInformationNoFuelTxtn.rpt"
                Case 79
                    reportname = "~/Reports/InvTankBalanceCurrentReport.rpt"
                Case 81
                    reportname = "~/Reports/MiscReportListing_Departments.rpt"
                Case 82
                    reportname = "~/Reports/MiscReportListing_SiteInfo.rpt"
                Case 83
                    reportname = "~/Reports/MiscListingTanks.rpt"
                Case 84
                    reportname = "~/Reports/MiscReportListing_LockOuts.rpt"
                Case 85
                    reportname = "~/Reports/MiscReportListing_CurrentExportFile.rpt"
                Case 88
                    reportname = "~/Reports/MiscReportListing_ExportSumm.rpt"
                Case 91
                    reportname = "~/Reports/VehListHistorySummary.rpt"
                Case 92
                    reportname = "~/Reports/VehThatHaveNotFueled.rpt"
                    'Added By Varun Moota to show Site Analysis Summary Report
                Case 101
                    reportname = "~/Reports/SiteAnalaysisSummaryReport.rpt"

                    'Added By Varun Moota to Show DFW Reports.10/11/2010
                Case 201
                    reportname = "~/Reports/TankTot_DFW.rpt"
                Case 202
                    reportname = "~/Reports/TankTot2_DFW.rpt"

                    'Tank Chart Report. Varun Moota 01/13/2010
                Case 301
                    reportname = "~/Reports/TankChart.rpt"
                    'Polling Results Report. Varun Moota 01/13/2010
                Case 302
                    reportname = "~/Reports/PResults.rpt"

                    'New Vehicle OBDII Trouble Code Report.10/11/2011
                Case 303
                    reportname = "~/Reports/VehOBDIITroubleCodeReport.rpt"

                    'New Vehicle OBDII Z Report.10/11/2011
                Case 304
                    reportname = "~/Reports/VehOBDIIZReport.rpt"

                Case 401
                    reportname = "~/Reports/TransactionReportByDept.rpt"


            End Select
            Return reportname
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.LoadReport", ex)
            Return Nothing
        End Try
    End Function

    Public Sub AssignDB(ByVal oRpt As ReportDocument)
        Try

       
            Dim subRepDoc As New ReportDocument()
            Dim crSections As Sections
            Dim crSection As Section
            Dim crReportObjects As ReportObjects
            Dim crReportObject As ReportObject
            Dim crSubreportObject As SubreportObject
            Dim crDatabase As Database
            Dim crTables As Tables
            Dim crTable As Table
            Dim crLogOnInfo As TableLogOnInfo
            Dim crConnInfo As New ConnectionInfo()

            'This call is required by the Windows Form Designer.
            'InitializeComponent()

            'Add any initialization after the InitializeComponent() call

            'Report code starts here
            'Set the database and the tables objects to the main report 'repDoc'
            crDatabase = oRpt.Database
            crTables = crDatabase.Tables

            'Loop through each table and set the connection info
            'Pass the connection info to the logoninfo object then apply the
            'logoninfo to the main report

            For Each crTable In crTables
                With crConnInfo
                    .ServerName = ConfigurationManager.AppSettings("servername")
                    .DatabaseName = ConfigurationManager.AppSettings("database")
                    If ConfigurationManager.AppSettings("username") = "" Then
                        .IntegratedSecurity = True
                    Else
                        .UserID = ConfigurationManager.AppSettings("username")
                        .Password = ConfigurationManager.AppSettings("password")
                    End If
                End With
                crLogOnInfo = crTable.LogOnInfo
                crLogOnInfo.ConnectionInfo = crConnInfo
                crTable.ApplyLogOnInfo(crLogOnInfo)

            Next
            ' crTable = crTables.Item(0)

            crTable.Location = ConfigurationManager.AppSettings("database") & ".dbo." & crTable.Location.Substring(crTable.Location.LastIndexOf(".") + 1)

            'Set the sections collection with report sections
            crSections = oRpt.ReportDefinition.Sections

            'Loop through each section and find all the report objects
            'Loop through all the report objects to find all subreport objects, then set the
            'logoninfo to the subreport

            For Each crSection In crSections
                crReportObjects = crSection.ReportObjects
                For Each crReportObject In crReportObjects
                    If crReportObject.Kind = ReportObjectKind.SubreportObject Then

                        'If you find a subreport, typecast the reportobject to a subreport object
                        crSubreportObject = CType(crReportObject, SubreportObject)

                        'Open the subreport
                        subRepDoc = crSubreportObject.OpenSubreport(crSubreportObject.SubreportName)

                        crDatabase = subRepDoc.Database
                        crTables = crDatabase.Tables

                        'Loop through each table and set the connection info
                        'Pass the connection info to the logoninfo object then apply the
                        'logoninfo to the subreport

                        For Each crTable In crTables
                            With crConnInfo
                                .ServerName = ConfigurationManager.AppSettings("servername")
                                .DatabaseName = ConfigurationManager.AppSettings("database")
                                If ConfigurationManager.AppSettings("username") = "" Then
                                    .IntegratedSecurity = True
                                Else
                                    .UserID = ConfigurationManager.AppSettings("username")
                                    .Password = ConfigurationManager.AppSettings("password")
                                End If
                            End With
                            crLogOnInfo = crTable.LogOnInfo
                            crLogOnInfo.ConnectionInfo = crConnInfo
                            'crTable.ApplyLogOnInfo(crLogOnInfo)
                        Next
                        'crTable.Location = ConfigurationManager.AppSettings("database") & ".dbo." & crTable.Location.Substring(crTable.Location.LastIndexOf(".") + 1)
                    End If
                Next
            Next
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.AssignDB", ex)

        End Try
    End Sub

    Public Function SetCurrentValuesForParameterField(ByVal reportDocument As ReportDocument, ByVal arrayList As ArrayList)
        Try
            Dim currentParameterValues As ParameterValues = New ParameterValues()
            Dim i As Integer
            Dim submittedValue As Object
            For Each submittedValue In arrayList

                Dim parameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
                parameterDiscreteValue.Value = submittedValue.ToString()

                currentParameterValues.Add(parameterDiscreteValue)

                Dim parameterFieldDefinitions As ParameterFieldDefinitions = reportDocument.DataDefinition.ParameterFields

                Dim parameterFieldDefinition As ParameterFieldDefinition

                If (i < 2) Then
                    If (submittedValue.ToString() = "11") Then
                        parameterFieldDefinition = parameterFieldDefinitions("@month")
                    Else
                        parameterFieldDefinition = parameterFieldDefinitions("@year")
                    End If
                Else
                    If (submittedValue.ToString() = "11") Then
                        parameterFieldDefinition = parameterFieldDefinitions("@month", "MonthlyScheduleGraph.rpt")
                    Else
                        parameterFieldDefinition = parameterFieldDefinitions("@year", "MonthlyScheduleGraph.rpt")
                    End If
                End If
                parameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
                i = i + 1
            Next
            Return ""
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.SetCurrentValuesForParameterField()", ex)
            Return ""
        End Try
    End Function

    Public Function SetCurrentValuesForParameterField(ByVal reportDocument As ReportDocument, ByVal strParameterName As String, ByVal strValue As String)
        Try
            Dim currentParameterValues As ParameterValues = New ParameterValues()
            Dim i As Integer = 0
            Dim parameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()
            parameterDiscreteValue.Value = strValue
            currentParameterValues.Add(parameterDiscreteValue)
            Dim parameterFieldDefinitions As ParameterFieldDefinitions = reportDocument.DataDefinition.ParameterFields
            Dim parameterFieldDefinition As ParameterFieldDefinition
            parameterFieldDefinition = parameterFieldDefinitions(strParameterName)
            parameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
            Return ""
        Catch ex As Exception
            Dim cr As New ErrorPage

            cr.errorlog("App_Code/GeneralFunctions.SetCurrentValuesForParameterField ", ex.InnerException)
            Return ""
        End Try
    End Function

    Public Function SetCurrentValuesForParameterFieldInSubReport(ByVal reportDocument As ReportDocument, ByVal strParameterName As String, ByVal strValue As String, ByVal strSubReport As String)
        Try

      
            Dim currentParameterValues As ParameterValues = New ParameterValues()
            Dim parameterDiscreteValue As ParameterDiscreteValue = New ParameterDiscreteValue()

            parameterDiscreteValue.Value = strValue
            currentParameterValues.Add(parameterDiscreteValue)

            Dim parameterFieldDefinitions As ParameterFieldDefinitions = reportDocument.DataDefinition.ParameterFields

            Dim parameterFieldDefinition As ParameterFieldDefinition

            parameterFieldDefinition = parameterFieldDefinitions(strParameterName, strSubReport)
            parameterFieldDefinition.CurrentValues.Clear()
            parameterFieldDefinition.ApplyCurrentValues(currentParameterValues)
            Return ""
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.SetCurrentValuesForParameterFieldInSubReport()", ex)
            Return ""
        End Try
    End Function

    Public Function ConvertDate(ByVal strInDate As String) As DateTime
        Try

   
            Dim d As DateTime
            Dim s() As String
            s = strInDate.Split("/")
            d = New DateTime(Convert.ToInt32(s(2)), Convert.ToInt32(s(0)), Convert.ToInt32(s(1)))
            Return d
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GeneralFunctions.ConvertDate()", ex)
            Return ""
        End Try
    End Function
End Class