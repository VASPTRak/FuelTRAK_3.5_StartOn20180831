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


Partial Class WexImport
    Inherits System.Web.UI.Page
    Public Sname As String
    Dim TCount As Integer


    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If (fuTXTN.HasFile) Then
            Dim fileName As String = (fuTXTN.FileName).ToString()

            fuTXTN.SaveAs(Server.MapPath("~/WexImport/" + fileName))
            Dim ExcelFilePath As String = Server.MapPath("~/WexImport/" & fileName)
            Dim ExcelFileName As String = fuTXTN.FileName.ToString()
            'Dim strNewPath As String = Server.MapPath("~/WexImport/" & fileName)

            Try
                Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & ExcelFilePath & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=2"""
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

                ' MyCommand = New OleDbDataAdapter("select * from [Sheet1$]", MyConnection)
                MyCommand = New OleDbDataAdapter("select * from" + "[" + Sname + "]", MyConnection)
                ds = New System.Data.DataSet()
                MyCommand.Fill(ds)
                MyConnection.Close()
                Dim result As Boolean = InsertRecords(ds.Tables(0))

                Dim RecordCount As Integer = ds.Tables(0).Rows.Count()
                Session("CNT") = RecordCount.ToString()
                If (result) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script> alert('Successfully Imported (" + Session("CNT") + ") Transactions from Excel file.');</script>")
                End If

            Catch ex As Exception
                Dim cr As New ErrorPage
                cr.errorlog("WexImport.btnSubmit_Click", ex)
            End Try
        End If
    End Sub
    Private Function InsertRecords(ByVal dt As DataTable) As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(9) As SqlParameter
            Dim ParAcctID = New SqlParameter("@ACCT_ID", SqlDbType.NVarChar, 15)
            Dim ParCustomerVehicleID = New SqlParameter("@CUSTOMERVEHICLEID", SqlDbType.NVarChar, 10)
            Dim ParOOR_Miles = New SqlParameter("@OOR_Miles", SqlDbType.NVarChar, 50)
            Dim ParCOST = New SqlParameter("@COST", SqlDbType.Decimal)
            Dim ParQUANTITY = New SqlParameter("@QUANTITY", SqlDbType.Decimal)
            Dim ParPRODUCT = New SqlParameter("@PRODUCT", SqlDbType.NVarChar, 4)
            Dim ParPERSONNEL = New SqlParameter("@PERSONNEL", SqlDbType.NVarChar, 10)
            Dim ParDATETIME = New SqlParameter("@DATETIME", SqlDbType.DateTime)
            Dim ParCARD_ID = New SqlParameter("@CARD_ID", SqlDbType.NVarChar, 7)
            Dim ParPUMP = New SqlParameter("@PUMP", SqlDbType.NVarChar, 2)
            ParAcctID.Direction = ParameterDirection.Input
            ParCustomerVehicleID.Direction = ParameterDirection.Input
            ParOOR_Miles.Direction = ParameterDirection.Input
            ParCOST.Direction = ParameterDirection.Input
            ParQUANTITY.Direction = ParameterDirection.Input
            ParPRODUCT.Direction = ParameterDirection.Input
            ParPERSONNEL.Direction = ParameterDirection.Input
            ParDATETIME.Direction = ParameterDirection.Input
            ParCARD_ID.Direction = ParameterDirection.Input
            ParPUMP.Direction = ParameterDirection.Input
            Dim i As Integer
            Dim ds As New DataSet
            For i = 0 To dt.Rows.Count - 1
                'Dim VehicleId As Integer = Convert.ToInt32(dt.Rows(i)("CUSTOMER VEHICLE ID")) 'Vehicle ID from ExcelSheet
                ParPUMP.value = "01"
                ParAcctID.value = dt.Rows(i)("ACCOUNT ID (ACCT_ID)").ToString()
                ParCustomerVehicleID.value = dt.Rows(i)("CUSTOMER VEHICLE ID").ToString()
                ParOOR_Miles.value = dt.Rows(i)("ODOMETER").ToString()
                ParCOST.value = Convert.ToDecimal(dt.Rows(i)("FUEL COST"))
                ParQUANTITY.value = Convert.ToDecimal(dt.Rows(i)("UNITS"))
                ParPRODUCT.value = dt.Rows(i)("PRODUCT").ToString()
                ParPERSONNEL.value = dt.Rows(i)("DRIVER ID").ToString()
                Dim DtTXTN As Date = Convert.ToDateTime(dt.Rows(i)("TRANSACTION DATE"))
                Dim TmTXTN As DateTime = Convert.ToDateTime(dt.Rows(i)("TRANSACTION TIME"))
                Dim dt1 As New DateTime(DtTXTN.Year, DtTXTN.Month, DtTXTN.Day, TmTXTN.Hour, TmTXTN.Minute, TmTXTN.Second)
                ParDATETIME.value = dt1
                ParCARD_ID.value = dt.Rows(i)("CARD NUMBER").ToString()
                parcollection(0) = ParAcctID
                parcollection(1) = ParCustomerVehicleID
                parcollection(2) = ParOOR_Miles
                parcollection(3) = ParCOST
                parcollection(4) = ParQUANTITY
                parcollection(5) = ParPRODUCT
                parcollection(6) = ParPERSONNEL
                parcollection(7) = ParDATETIME
                parcollection(8) = ParCARD_ID
                parcollection(9) = ParPUMP
                Dim blnFlag As Boolean = dal.ExecuteSQLStoredProcedureGetBoolean("USP_TT_TXTN_WEXIMPORT", parcollection)
                'Dim Count As Integer
                'End If
            Next
            Dim ds1 As DataSet
            Dim Query1 As String = "Select Number from Sentry where Number = 000"
            ds1 = dal.GetDataSet(Query1)
            If Not ds1.Tables(0).Rows.Count = 1 Then
                Dim dal1 = New GeneralizedDAL()
                Dim par(0) As SqlParameter
                Dim ParNumber = New SqlParameter("@Number", SqlDbType.NVarChar, 3)
                ParNumber.Direction = ParameterDirection.Input
                ParNumber.value = "000"
                par(0) = ParNumber
                dal.ExecuteSQLStoredProcedureGetBoolean("USP_TT_Insert_Sentry", par)
            End If
            Return True
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
