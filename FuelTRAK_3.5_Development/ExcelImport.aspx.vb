Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Data.OleDb
Imports System.Security.Permissions
Partial Class ExcelImport
    Inherits System.Web.UI.Page
    Public Sname As String
    Dim TCount As Integer
    Dim Result As Boolean
    Dim _Messsage As String = "Successfully Imported from Excel file."
    Dim DisplayMessage As String
    Dim Vehs As Integer = 0
    Dim Dept As Integer = 0
    Dim Pers As Integer = 0
    Dim Fuels As Integer = 0

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If (fuTXTN.HasFile) Then
            Dim fileName As String = (fuTXTN.FileName).ToString()
            fuTXTN.SaveAs(Server.MapPath("~/WexImport/" + fileName))
            Dim ExcelFilePath As String = Server.MapPath("~/WexImport/" & fileName)
            Dim ExcelFileName As String = fuTXTN.FileName.ToString()
            'Dim strNewPath As String = Server.MapPath("~/WexImport/" & fileName)
            Try
                Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & ExcelFilePath & ";Extended Properties=""Excel 8.0;HDR=No;IMEX=1"""
                Dim MyConnection As OleDbConnection
                Dim ds As DataSet
                Dim MyCommand As OleDbDataAdapter
                MyConnection = New OleDbConnection(connStr)
                If MyConnection.State = ConnectionState.Closed Then MyConnection.Open()
                'Getting Sheet name from ExcelFile.
                Dim dtSheets As DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                Dim listSheet As New List(Of String)
                Dim drSheet As DataRow
                For Each drSheet In dtSheets.Rows
                    listSheet.Add(drSheet("TABLE_NAME").ToString())
                Next
                For Each sheet As String In listSheet
                    Sname = sheet
                Next
                'Pritam Testing Implementation
                For Each sheet As String In listSheet
                    Sname = sheet
                    If Sname.Contains("'") Then
                        Sname = Sname.Replace("'", "")
                    End If
                    MyCommand = New OleDbDataAdapter("select * from" + "[" + Sname + "]", MyConnection)
                    ds = New System.Data.DataSet()
                    MyCommand.Fill(ds)
                    Select Case Sname
                        Case "Departments Names$"
                            Result = InsertDepartMent(ds.Tables(0))
                            DisplayMessage = "(" + Dept.ToString() + ")" + "Departments"
                            'If (Result Or False) Then
                            '    DisplayMessage = "(" + Dept.ToString() + ")" + "Departments"
                            'End If
                        Case "Fuels$"
                            Dim i As Integer
                            For i = 1 To 16
                                InsertFuels(i.ToString().PadLeft(2, "0"), "", False, "")
                            Next
                            Result = InsertFuels(ds.Tables(0))
                            DisplayMessage = DisplayMessage + ",(" + Fuels.ToString() + ") Fuels"
                            'If (Result Or False) Then
                            '    DisplayMessage = DisplayMessage + ",(" + Fuels.ToString() + ") Fuels"
                            'End If
                        Case "Personnel$"
                            Result = InsertPersonnel(ds.Tables(0))
                            DisplayMessage = DisplayMessage + ",(" + Pers.ToString() + ") Personnel"
                            'If (Result Or False) Then
                            '    DisplayMessage = DisplayMessage + ",(" + Pers.ToString() + ") Personnel"
                            'End If
                        Case "Vehicles$"
                            Result = InsertVehicles(ds.Tables(0))
                            DisplayMessage = DisplayMessage + ", " + "(" + Vehs.ToString() + ")" + "Vehicles"
                            'If (Result Or False) Then
                            '    DisplayMessage = DisplayMessage + ", " + "(" + Vehs.ToString() + ")" + "Vehicles"
                            'End If
                            'Case Else
                            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script> alert('Failed to Import from Excel file.');</script>")
                    End Select
                    MyConnection.Close()
                Next
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", " alert('" + DisplayMessage + _Messsage + "');", True)
            Catch ex As Exception
                Dim cr As New ErrorPage
                cr.errorlog("ExcelImport.btnSubmit_Click", ex)
            End Try
        End If
    End Sub
    'Import Departments
    Private Function InsertDepartMent(ByVal dt As DataTable) As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(5) As SqlParameter
            Dim ParDeptName = New SqlParameter("@DepartmentName", SqlDbType.NVarChar, 50)
            Dim ParAddress1 = New SqlParameter("@Address1", SqlDbType.NVarChar, 10)
            Dim ParAddress2 = New SqlParameter("@Address2", SqlDbType.NVarChar, 50)
            Dim ParSurcharge = New SqlParameter("@Surcharge", SqlDbType.Float)
            Dim ParCustomerCode = New SqlParameter("@CustomerCode", SqlDbType.VarChar, 30)
            Dim ParDeptNo = New SqlParameter("@DeptNo", SqlDbType.VarChar, 3)
            ParDeptName.Direction = ParameterDirection.Input
            ParAddress1.Direction = ParameterDirection.Input
            ParAddress2.Direction = ParameterDirection.Input
            ParSurcharge.Direction = ParameterDirection.Input
            ParCustomerCode.Direction = ParameterDirection.Input
            ParDeptNo.Direction = ParameterDirection.Input
            Dim i As Integer
            Dim ds As New DataSet

            For i = 1 To dt.Rows.Count
                If IsDBNull(dt.Rows(i - 1)(0)) Then
                    dt.Rows(i - 1).Delete()
                End If
            Next
            'For i = 4 To dt.Rows.Count - i
            '    If IsDBNull(dt.Rows(i - 1)(0)) Then
            '        dt.Rows(i - 1).Delete()
            '    End If
            'Next
            dt.AcceptChanges()
            Dim blnFlag As Boolean = False
            If (dt.Rows.Count > 3) Then
                For i = 0 To dt.Rows.Count - 1
                    ParDeptName.Value = dt.Rows(i)("F1").ToString()
                    ParAddress1.Value = dt.Rows(i)("F2").ToString()
                    ParAddress2.Value = dt.Rows(i)("F3").ToString()
                    ParSurcharge.Value = IIf(IsDBNull(dt.Rows(i)("F4")), Convert.ToDouble(0), (Convert.ToDouble(Val(dt.Rows(i)("F4").ToString()))))
                    ParCustomerCode.Value = dt.Rows(i)("F5").ToString()
                    ParDeptNo.Value = dt.Rows(i)("F6").ToString().PadLeft(3, "0")
                    parcollection(0) = ParDeptName
                    parcollection(1) = ParAddress1
                    parcollection(2) = ParAddress2
                    parcollection(3) = ParSurcharge
                    parcollection(4) = ParCustomerCode
                    parcollection(5) = ParDeptNo
                    blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("SP_INSERT_DEPARTMENT", parcollection)
                    If blnFlag Then
                        Dept = Dept + 1
                    End If
                Next
            End If
            Return blnFlag
        Catch ex As Exception
            Dim Cr As New ErrorPage
            Cr.errorlog("ExcelImport.InsertDepartMent", ex)
        End Try
    End Function

    'To create Product entries as per New Product page in FuelTRAK 
    Private Function InsertFuels(ByVal Number As String, ByVal name As String, ByVal Primary As Boolean, ByVal code As String)
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(3) As SqlParameter
            Dim ParName = New SqlParameter("@NAME", SqlDbType.NVarChar, 20)
            Dim ParPrimary = New SqlParameter("@Primary", SqlDbType.Bit)
            Dim ParCode = New SqlParameter("@code", SqlDbType.NVarChar, 50)
            Dim ParNumber = New SqlParameter("@Number", SqlDbType.NVarChar, 2)

            ParName.Direction = ParameterDirection.Input
            ParPrimary.Direction = ParameterDirection.Input
            ParCode.Direction = ParameterDirection.Input
            ParNumber.Direction = ParameterDirection.Input

            ParName.value = name
            ParPrimary.value = Primary
            ParCode.value = code
            ParNumber.value = Number

            parcollection(0) = ParName
            parcollection(1) = ParPrimary
            parcollection(2) = ParCode
            parcollection(3) = ParNumber

            Return dal.ExecuteSQLStoredProcedureGetBoolean("use_tt_InsertProduct", parcollection)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ExcelImport.InsertFuels1", ex)
        End Try
    End Function

    'Import Fuels
    Private Function InsertFuels(ByVal dt As DataTable) As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(3) As SqlParameter
            Dim ParName = New SqlParameter("@NAME", SqlDbType.NVarChar, 20)
            Dim ParPrimary = New SqlParameter("@Primary", SqlDbType.Bit)
            Dim ParCode = New SqlParameter("@code", SqlDbType.NVarChar, 50)
            Dim ParNumber = New SqlParameter("@Number", SqlDbType.NVarChar, 2)


            ParName.Direction = ParameterDirection.Input
            ParPrimary.Direction = ParameterDirection.Input
            ParCode.Direction = ParameterDirection.Input
            ParNumber.Direction = ParameterDirection.Input

            Dim blnFlag As Boolean = False
            Dim i As Integer
            Dim ds As New DataSet
            For i = 1 To dt.Rows.Count
                If IsDBNull(dt.Rows(i - 1)(0)) Then
                    dt.Rows(i - 1).Delete()
                End If
            Next
            dt.AcceptChanges()
            'Dim blnFlag As Boolean
            If (dt.Rows.Count > 3) Then
                For i = 0 To dt.Rows.Count - 1
                    ParName.Value = dt.Rows(i)("F1").ToString()
                    ParPrimary.Value = IIf(dt.Rows(i)("F2").ToString() = "Y", True, False)
                    ParCode.Value = dt.Rows(i)("F3").ToString()
                    ParNumber.Value = Convert.ToString(i - 0).PadLeft(2, "0")

                    parcollection(0) = ParName
                    parcollection(1) = ParPrimary
                    parcollection(2) = ParCode
                    parcollection(3) = ParNumber
                    blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("use_tt_InsertProduct", parcollection)

                    If blnFlag Then
                        Fuels = Fuels + 1
                    End If
                Next
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ExcelImport.InsertFuels", ex)
        End Try

    End Function

    'Import Personnel
    Private Function InsertPersonnel(ByVal dt As DataTable) As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(5) As SqlParameter
            Dim ParPersonnelId = New SqlParameter("@PersonnelId", SqlDbType.NVarChar, 10)
            Dim ParLAST_NAME = New SqlParameter("@LAST_NAME", SqlDbType.NVarChar, 20)
            Dim ParFIRST_NAME = New SqlParameter("@FIRST_NAME", SqlDbType.NVarChar, 15)
            Dim ParMI = New SqlParameter("@MI", SqlDbType.Char)
            Dim ParDEPT = New SqlParameter("@DEPT", SqlDbType.NVarChar, 3)
            Dim ParAcctID = New SqlParameter("@AcctID", SqlDbType.NVarChar, 11)
            ParPersonnelId.Direction = ParameterDirection.Input
            ParLAST_NAME.Direction = ParameterDirection.Input
            ParFIRST_NAME.Direction = ParameterDirection.Input
            ParMI.Direction = ParameterDirection.Input
            ParDEPT.Direction = ParameterDirection.Input
            ParAcctID.Direction = ParameterDirection.Input
            Dim i As Integer
            Dim ds As New DataSet
            Dim blnFlag As Boolean = False
            For i = 1 To dt.Rows.Count
                If IsDBNull(dt.Rows(i - 1)(0)) Then
                    dt.Rows(i - 1).Delete()
                End If
            Next
            dt.AcceptChanges()

            If (dt.Rows.Count > 3) Then
                For i = 0 To dt.Rows.Count - 1
                    ParPersonnelId.Value = dt.Rows(i)("F1").ToString()
                    ParLAST_NAME.Value = dt.Rows(i)("F2").ToString()
                    ParFIRST_NAME.Value = dt.Rows(i)("F3").ToString()
                    ParMI.Value = dt.Rows(i)("F4").ToString()
                    Dim dsDept = New DataSet()
                    dsDept = dal.GetDataSet("select Number from Dept where [NAME] = '" + dt.Rows(i)("F5").ToString() + "'")
                    ParDEPT.Value = dsDept.Tables(0).Rows(0)(0)
                    ParAcctID.Value = dt.Rows(i)("F6").ToString()
                    parcollection(0) = ParPersonnelId
                    parcollection(1) = ParLAST_NAME
                    parcollection(2) = ParFIRST_NAME
                    parcollection(3) = ParMI
                    parcollection(4) = ParDEPT
                    parcollection(5) = ParAcctID
                    blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("SP_INSERT_Personnel", parcollection)

                    If blnFlag Then
                        Pers = Pers + 1
                    End If
                Next
            End If
            Return blnFlag

        Catch ex As Exception
            Dim Cr As New ErrorPage
            Cr.errorlog("ExcelImport.InserPersonnel", ex)
        End Try

    End Function

    Private Function InsertVehicles(ByVal dt As DataTable) As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(20) As SqlParameter
            Dim VehicleId = New SqlParameter("@VehicleId", SqlDbType.NVarChar, 10)
            Dim EXTENSION = New SqlParameter("@EXTENSION", SqlDbType.VarChar, 50)
            Dim DEPT = New SqlParameter("@DEPT", SqlDbType.NVarChar, 3)
            Dim FUELS = New SqlParameter("@FUELS", SqlDbType.NVarChar, 15)
            Dim LICNO = New SqlParameter("@LICNO", SqlDbType.NVarChar, 9)
            Dim LICST = New SqlParameter("@LICST", SqlDbType.NVarChar, 2)
            Dim Year = New SqlParameter("@Year", SqlDbType.NVarChar, 4)
            Dim Vehmake = New SqlParameter("@Vehmake", SqlDbType.NVarChar, 20)
            Dim Vehmodel = New SqlParameter("@Vehmodel", SqlDbType.NVarChar, 20)
            Dim VEHVIN = New SqlParameter("@VEHVIN", SqlDbType.NVarChar, 20)
            Dim MILES_WIND = New SqlParameter("@MILES_WIND", SqlDbType.NVarChar, 10)
            Dim MILEAGE = New SqlParameter("@MILEAGE", SqlDbType.Bit)
            Dim REQODOM = New SqlParameter("@REQODOM", SqlDbType.Bit)
            Dim LIMIT = New SqlParameter("@LIMIT", SqlDbType.NVarChar, 5)
            Dim MESS_COUNT = New SqlParameter("@MESS_COUNT", SqlDbType.Int)
            Dim MESSAGELINE1 = New SqlParameter("@MESSAGELINE1", SqlDbType.NVarChar, 60)
            Dim NEXTPMMILE = New SqlParameter("@NEXTPMMILE", SqlDbType.Float)
            Dim PM_INCREM = New SqlParameter("@PM_INCREM", SqlDbType.Float)
            Dim PMTXTNCNT = New SqlParameter("@PMTXTNCNT", SqlDbType.Float)
            Dim CURRMILES = New SqlParameter("@CURRMILES", SqlDbType.Float)
            Dim KeyNumber = New SqlParameter("@KeyNumber", SqlDbType.NVarChar, 6)
            VehicleId.Direction = ParameterDirection.Input
            EXTENSION.Direction = ParameterDirection.Input
            DEPT.Direction = ParameterDirection.Input
            FUELS.Direction = ParameterDirection.Input
            LICNO.Direction = ParameterDirection.Input
            LICST.Direction = ParameterDirection.Input
            Year.Direction = ParameterDirection.Input
            Vehmake.Direction = ParameterDirection.Input
            Vehmodel.Direction = ParameterDirection.Input
            VEHVIN.Direction = ParameterDirection.Input
            MILES_WIND.Direction = ParameterDirection.Input
            MILEAGE.Direction = ParameterDirection.Input
            REQODOM.Direction = ParameterDirection.Input
            LIMIT.Direction = ParameterDirection.Input
            MESS_COUNT.Direction = ParameterDirection.Input
            MESSAGELINE1.Direction = ParameterDirection.Input
            NEXTPMMILE.Direction = ParameterDirection.Input
            PM_INCREM.Direction = ParameterDirection.Input
            PMTXTNCNT.Direction = ParameterDirection.Input
            CURRMILES.Direction = ParameterDirection.Input
            KeyNumber.Direction = ParameterDirection.Input
            Dim i As Integer
            Dim ds As New DataSet
            Dim blnFlag As Boolean = False
            For i = 1 To dt.Rows.Count
                If IsDBNull(dt.Rows(i - 1)(0)) Then
                    dt.Rows(i - 1).Delete()
                End If
            Next
            dt.AcceptChanges()
            If (dt.Rows.Count > 3) Then
                For i = 0 To dt.Rows.Count - 1
                    VehicleId.Value = dt.Rows(i)("F1").ToString()
                    EXTENSION.Value = dt.Rows(i)("F2").ToString()
                    Dim dsDept = New DataSet()
                    dsDept = dal.GetDataSet("select Number from Dept where [NAME] = '" + dt.Rows(i)("F3").ToString() + "'")
                    If (dsDept.Tables(0).Rows.Count >= 1) Then
                        DEPT.Value = dsDept.Tables(0).Rows(0)(0)
                    Else
                        DEPT.Value = "000"
                    End If
                    'DEPT.value = dsDept.Tables(0).Rows(0)(0)
                    Dim dsFuels = New DataSet()
                    dsFuels = dal.GetDataSet("select Number from Product where [NAME] = '" + dt.Rows(i)("F4").ToString() + "'")
                    Dim indx As Integer = Convert.ToInt32(dsFuels.Tables(0).Rows(0)(0)) - 1
                    Dim PD As String = "NNNNNNNNNNNNNNN"
                    Dim ReplaceChar As Char = "Y"c
                    Dim sbString1 As New System.Text.StringBuilder(PD)
                    sbString1(indx) = ReplaceChar
                    FUELS.Value = sbString1.ToString()
                    LICNO.Value = dt.Rows(i)("F5").ToString()
                    LICST.Value = dt.Rows(i)("F6").ToString()
                    Year.Value = dt.Rows(i)("F7").ToString()
                    Vehmake.Value = dt.Rows(i)("F8").ToString()
                    Vehmodel.Value = dt.Rows(i)("F9").ToString()
                    VEHVIN.Value = dt.Rows(i)("F10").ToString()
                    MILES_WIND.Value = dt.Rows(i)("F11").ToString()
                    MILEAGE.Value = IIf(dt.Rows(i)("F12").ToString() = "M", True, False)
                    REQODOM.Value = IIf(dt.Rows(i)("F13").ToString() = "Y", True, False)
                    LIMIT.Value = dt.Rows(i)("F14").ToString()
                    If dt.Rows(i).IsNull("F15") Then
                        MESS_COUNT.Value = Convert.ToInt32(0)
                    Else
                        MESS_COUNT.Value = Convert.ToDecimal(dt.Rows(i)("F15"))
                    End If
                    'MESS_COUNT.value = IIf((dt.Rows(i)("F15") = ""), Convert.ToInt32(0), Convert.ToInt32(dt.Rows(i)("F16")))
                    MESSAGELINE1.Value = dt.Rows(i)("F16").ToString()
                    If dt.Rows(i).IsNull("F17") Then
                        NEXTPMMILE.Value = Convert.ToInt32(0)
                    Else
                        NEXTPMMILE.Value = Convert.ToDecimal(dt.Rows(i)("F17"))
                    End If

                    If dt.Rows(i).IsNull("F18") Then
                        PM_INCREM.Value = Convert.ToInt32(0)
                    Else
                        PM_INCREM.Value = Convert.ToDecimal(dt.Rows(i)("F18"))
                    End If

                    If dt.Rows(i).IsNull("F19") Then
                        PMTXTNCNT.Value = Convert.ToInt32(0)
                    Else
                        PMTXTNCNT.Value = Convert.ToDecimal(dt.Rows(i)("F19"))
                    End If

                    If dt.Rows(i).IsNull("F20") Then
                        CURRMILES.Value = Convert.ToInt32(0)
                    Else
                        CURRMILES.Value = Convert.ToDecimal(dt.Rows(i)("F20"))
                    End If
                    If dt.Rows(i).IsNull("F21") Then
                        KeyNumber.Value = Convert.ToInt32(0)
                    Else
                        KeyNumber.Value = dt.Rows(i)("F21").ToString() ' Convert.ToDecimal(dt.Rows(i)("F21"))
                    End If
                    ' NEXTPMMILE.value = IIf((dt.Rows(i).IsNull("F17")), Convert.ToDecimal(0), Convert.ToDecimal(dt.Rows(i)("F18")))
                    ' PM_INCREM.value = IIf((dt.Rows(i).IsNull("F18")), Convert.ToDecimal(0), Convert.ToDecimal(dt.Rows(i)("F19")))
                    ' PMTXTNCNT.value = IIf((dt.Rows(i).IsNull("F19")), Convert.ToDecimal(0), Convert.ToDecimal(dt.Rows(i)("F20")))
                    'CURRMILES.value = IIf((dt.Rows(i).IsNull("F20")), Convert.ToDecimal(0), Convert.ToDecimal(dt.Rows(i)("F21")))

                    parcollection(0) = VehicleId
                    parcollection(1) = EXTENSION
                    parcollection(2) = DEPT
                    parcollection(3) = FUELS
                    parcollection(4) = LICNO
                    parcollection(5) = LICST
                    parcollection(6) = Year
                    parcollection(7) = Vehmake
                    parcollection(8) = Vehmodel
                    parcollection(9) = VEHVIN
                    parcollection(10) = MILES_WIND
                    parcollection(11) = MILEAGE
                    parcollection(12) = REQODOM
                    parcollection(13) = LIMIT
                    parcollection(14) = MESS_COUNT
                    parcollection(15) = MESSAGELINE1
                    parcollection(16) = NEXTPMMILE
                    parcollection(17) = PM_INCREM
                    parcollection(18) = PMTXTNCNT
                    parcollection(19) = CURRMILES
                    parcollection(20) = KeyNumber
                    blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("SP_INSERT_Vehicles", parcollection)
                    If blnFlag Then
                        Vehs = Vehs + 1
                    End If
                Next
            End If
            Return blnFlag

        Catch ex As Exception
            Dim Cr As New ErrorPage
            Cr.errorlog("WexImport.InsertRecords", ex)
        End Try

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.Page_Load", ex)
        End Try
    End Sub
End Class

