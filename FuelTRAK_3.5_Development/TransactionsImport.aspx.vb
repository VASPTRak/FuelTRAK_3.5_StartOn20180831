Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Partial Class TransactionsImport
    Inherits System.Web.UI.Page
    Dim dv As DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            PopUpListColumn.Visible = False
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            End If

            'By Soham Gangavane July 24,2017
            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn() {New DataColumn("ColumnOrder", GetType(String)), _
                                                   New DataColumn("ColumnName", GetType(String)), _
                                                   New DataColumn("ColumnSize", GetType(String))})
            dt.Rows.Add("1", "Sentry", "Character 3")
            dt.Rows.Add("2", "Transaction Number", "Character 6")
            dt.Rows.Add("3", "Start Date", "DateTime")
            dt.Rows.Add("4", "End Date", "DateTime")
            dt.Rows.Add("5", "Vehicle", "Character 10")
            dt.Rows.Add("6", "Vkey", "Character 5")
            dt.Rows.Add("7", "VCardID", "Character 10")
            dt.Rows.Add("8", "AcctID", "Character 12")
            dt.Rows.Add("9", "Personnel", "Character 10")
            dt.Rows.Add("10", "PKey", "Character 5")
            dt.Rows.Add("11", "Miles", "Integer")
            dt.Rows.Add("12", "Pump", "Character 2")
            dt.Rows.Add("13", "Product", "Character 2")
            dt.Rows.Add("14", "Quantity", "Decimal")
            dt.Rows.Add("15", "Opt1", "Character 13")
            dt.Rows.Add("16", "Opt2", "Character 13")
            dt.Rows.Add("17", "Opt3", "Character 13")
            dt.Rows.Add("18", "Miles Overridden", "Character 1")
            dt.Rows.Add("19", "PCardId", "Character 10")
            dt.Rows.Add("20", "CardId#", "Character 7")

            grListOFColumn.DataSource = dt
            grListOFColumn.DataBind()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.Page_Load", ex)
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If (fuTXTN.HasFile) Then
                'Create a DataTable
                Dim dt = New DataTable()
                dt = CreateDT()
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Sentry ID Already Exists !!');</script>")
                ' Session("dvImpTbl") = dt
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Sentry ID Already Exists !!');</script>")
                'Dim fsIn As FileStream = Nothing
                'Added By Pritam on 03-Dec-2014 
                'Location of the Text file.
                Dim fileName As String = Path.GetFileName(fuTXTN.FileName)
                fuTXTN.SaveAs(Server.MapPath("~/TransactionImport/" + fileName))
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Sentry ID Already Exists !!');</script>")
                Dim NewPath As String = Server.MapPath("~/TransactionImport/" + fileName)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Sentry ID Already Exists !!');</script>")
                'Dim fileName As String = filepathVal.Value.ToString()
                'Dim fileName As String = System.IO.Path.GetFullP             ath(filepathVal.Value.ToString())
                '- Set the line counter
                Dim lineNumber As Integer = 0
                'fsIn = New FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None)
                '- Open the text file/stream
                Dim sr As StreamReader = New StreamReader(NewPath)
                ' Dim sr As StreamReader = New StreamReader(fsIn, System.Text.Encoding.[Default])
                Dim line As String = ""
                Dim cnt As Integer = 0
                Dim NotImportedRecords As String = ""
                Do

                    line = sr.ReadLine()
                    cnt = cnt + 1
                    '- If it goes past the last line of the file,
                    '- it drops out of the loop
                    If line <> Nothing Then
                        Dim txtnData As String() = line.Split(New Char() {","c})
                        Try
                            'By Soham Gangavane July 27, 2017
                            If (txtnData(2).ToString.Contains("4000")) Then
                                ' check  record contains valid Transaction number, sentry, quantity a unique composite key 

                                'By Soham Gangavane July 31, 2017
                                If txtnData(1).Replace("""", "").ToString = "" Then
                                    txtnData(1) = "0"
                                End If
                                If txtnData(0).Replace("""", "").ToString = "" Then
                                    txtnData(0) = "000"
                                End If
                                If txtnData(13).Replace("""", "").ToString = "" Then
                                    txtnData(13) = "0.0"
                                End If
                           
                                Dim dtTran As Boolean = True
                                'By Soham Gangavane July 25, 2017
                                'Validation
                                ' 1. Duplicate Transaction number
                                'dtTran = VehicleExists(txtnData(4).ToString)
                                If dtTran Then
                                    dtTran = checkDuplicateTransaction(txtnData(1).ToString)
                                    If dtTran Then
                                        dtTran = ValidateUniqueKeyConatraint(txtnData)
                                        If dtTran Then
                                            dt = AddDataToTable(txtnData, dt)
                                        Else
                                            NotImportedRecords = NotImportedRecords + "\n" + line
                                            Continue Do
                                        End If
                                    Else
                                        dt = AddDataToTable(txtnData, dt)
                                    End If
                                Else
                                    Continue Do
                                End If

                            Else
                                'By Soham Gangavane July 31, 2017
                                If txtnData(1).Replace("""", "").ToString = "" Then
                                    txtnData(1) = "0"
                                End If
                                If txtnData(0).Replace("""", "").ToString = "" Then
                                    txtnData(0) = "000"
                                End If
                                If txtnData(13).Replace("""", "").ToString = "" Then
                                    txtnData(13) = "0.0"
                                End If

                                Dim dtTran As Boolean = True
                                'By Soham Gangavane July 25, 2017
                                'Validation
                                ' 1. Duplicate Transaction number
                                'dtTran = VehicleExists(txtnData(4).ToString)
                                If dtTran Then
                                    dtTran = checkDuplicateTransaction(txtnData(1).ToString)
                                    If dtTran Then
                                        dtTran = ValidateUniqueKeyConatraint(txtnData)
                                        If dtTran Then
                                            dt = AddDataToTable(txtnData, dt)
                                        Else
                                            NotImportedRecords = NotImportedRecords + "\n" + line
                                            Continue Do
                                        End If
                                    Else
                                        dt = AddDataToTable(txtnData, dt)
                                    End If
                                Else
                                    Continue Do
                                End If
                            End If
                        Catch ex As Exception
                            Dim cr As New ErrorPage
                            cr.errorlog("Transaction_Import.FileStream()", ex)
                        End Try
                        '- Write the information to the DataTable
                    End If
                Loop Until line Is Nothing
                '- Close the file/stream
                cnt = cnt
                sr.Close()
                'Bind to the DataGrid.
                If dt.Rows.Count > 0 Then
                    dv = dt.DefaultView
                    Session("dvImpTXTN") = dv
                    BindGrid()
                Else
                    lblRowsAfft.Text = "No TXTN's are Imported into FuelTRAK."
                    lblRowsAfft.Visible = True
                End If
                'Added By Pritam on 03-Dec-2014 
                'Dim file As New FileInfo(NewPath)
                'If file.Exists Then
                '    file.Delete()
                'End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.btnSubmit_Click", ex)
        End Try
    End Sub

    Private Function CreateDT() As DataTable
        Try
            Dim tempTable As DataTable = New DataTable()
            Dim tempDataColumn As DataColumn
            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Sentry"
            tempTable.Columns.Add(tempDataColumn)
            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "TXTN"
            tempTable.Columns.Add(tempDataColumn)
            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "StartDT"
            tempTable.Columns.Add(tempDataColumn)
            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "EndDT"
            tempTable.Columns.Add(tempDataColumn)
            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Vehicle"
            tempTable.Columns.Add(tempDataColumn)
            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Vkey"
            tempTable.Columns.Add(tempDataColumn)
            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Miles"
            tempTable.Columns.Add(tempDataColumn)
            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "VCardID"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "AcctID"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Personnel"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "PKey"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Pump"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Product"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Quantity"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Opt1"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Opt2"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Opt3"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "MilesOverridden"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "PCardID"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "CARD_ID"
            tempTable.Columns.Add(tempDataColumn)

            Return tempTable
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.CreateDT()", ex)
            Return Nothing
        End Try
    End Function

    Private Function AddDataToTable(ByVal dr As String(), ByVal myTable As DataTable) As DataTable
        Try
            Dim row As DataRow

            'Format in SG4

            ' ''strTxtn = """" + SentryInfo.SentryNum.Trim + ""","""
            ' ''strTxtn = strTxtn + Str(dsTxtn.Item("txtnID")).Trim + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("BeginDateTime")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("EndDateTime")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("Vehicle")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("vkey")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("vcardID")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("acctid")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("Personnel")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("pkey")) + ""","""
            ' ''strTxtn = strTxtn + Str(dsTxtn.Item("odometer")).Trim + ""","""
            ' ''strTxtn = strTxtn + Str(dsTxtn.Item("pump")).Trim + ""","""
            ' ''strTxtn = strTxtn + Str(dsTxtn.Item("product")).Trim + ""","""
            ' ''strTxtn = strTxtn + Str(dsTxtn.Item("quantity")).Trim + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("option1")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("option2")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("option3")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("MilesOverridden")) + ""","""
            ' ''strTxtn = strTxtn + Trim(dsTxtn.Item("pcardID")) + """"
            ' ''sr.WriteLine(strTxtn)
            row = myTable.NewRow()
            row("Sentry") = dr(0).Replace("""", "")
            row("TXTN") = dr(1).Trim.ToString().Replace("""", "")
            row("StartDT") = dr(2).Trim.ToString().Replace("""", "")
            row("EndDT") = dr(3).Trim.ToString().Replace("""", "")
            row("Vehicle") = dr(4).Trim.ToString().Replace("""", "")
            row("VKey") = dr(5).Trim.ToString().Replace("""", "")
            row("VCardID") = dr(6).ToString().Replace("""", "")
            row("AcctID") = dr(7).ToString().Replace("""", "")
            row("Personnel") = dr(8).ToString().Replace("""", "")
            row("Pkey") = dr(9).Trim.ToString().Replace("""", "")
            row("Miles") = dr(10).ToString().Replace("""", "")
            row("Pump") = dr(11).ToString().Replace("""", "")
            row("Product") = dr(12).ToString().Replace("""", "")
            row("Quantity") = (Convert.ToDouble(dr(13).ToString().Replace("""", "")) / 20).ToString()
            row("Opt1") = dr(14).ToString().Replace("""", "")
            row("Opt2") = dr(15).ToString().Replace("""", "")
            row("Opt3") = dr(16).ToString().Replace("""", "")
            row("MilesOverridden") = dr(17).ToString().Replace("""", "")
            row("PCardID") = dr(18).ToString().Replace("""", "")
            row("CARD_ID") = IIf(dr(19) = Nothing, "", dr(19).ToString().Replace("""", ""))
            'myTable.Rows.Add(row)

            myTable = InsertRecords(myTable, row)
            Return myTable
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.AddDataToTable()", ex)
            Return Nothing
        End Try
    End Function

    Private Sub BindGrid()
        Try
            gvImportTXTNs.Visible = True
            dv = Session("dvImpTXTN")
            gvImportTXTNs.DataSource = dv
            gvImportTXTNs.DataBind()
            lblRowsAfft.Visible = True
            lblRowsAfft.Text = "New TXTNs uploaded to FuelTRAK."
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.BindGrid()", ex)
        End Try
    End Sub

    Protected Sub gvImportTXTNs_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvImportTXTNs.PageIndexChanging
        Try
            gvImportTXTNs.PageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.gvImportTXTNs_PageIndexChanging", ex)
        End Try
    End Sub

    Private Function InsertRecords(ByVal dt As DataTable, ByVal dr As DataRow) As DataTable
        Try
            Dim dtResults As New DataTable
            Dim blnFlag As Boolean
            Dim PrevMiles, PrevHours As Integer
            Dim dal = New GeneralizedDAL()
            Dim parcollection(25) As SqlParameter

            Dim ParSentry = New SqlParameter("@Sentry", SqlDbType.NVarChar, 3)
            Dim ParTXTN = New SqlParameter("@TXTN", SqlDbType.NVarChar, 6)
            Dim ParNewPoll = New SqlParameter("@NewPoll", SqlDbType.Int)
            Dim ParDateTime = New SqlParameter("@DT", SqlDbType.DateTime)
            Dim ParVehID = New SqlParameter("@VehId", SqlDbType.NVarChar, 10)
            Dim ParVKey = New SqlParameter("@VKey", SqlDbType.NVarChar, 5)
            Dim ParMiles = New SqlParameter("@Miles", SqlDbType.Int)
            Dim ParPrevMiles = New SqlParameter("@PrevMiles", SqlDbType.Int)
            Dim ParHours = New SqlParameter("@Hours", SqlDbType.Int)
            Dim ParPrevHours = New SqlParameter("@PrevHours", SqlDbType.Int)
            Dim ParVCardID = New SqlParameter("@VCardID", SqlDbType.NVarChar, 10)
            Dim ParAcctID = New SqlParameter("@AcctID", SqlDbType.NVarChar, 12)
            Dim ParPersonnel = New SqlParameter("@Personnel", SqlDbType.NVarChar, 10)
            Dim ParPKey = New SqlParameter("@PKey", SqlDbType.NVarChar, 5)
            Dim ParPump = New SqlParameter("@Pump", SqlDbType.NVarChar, 2)
            Dim ParProd = New SqlParameter("@Prod", SqlDbType.NVarChar, 2)
            Dim ParQty = New SqlParameter("@Qty", SqlDbType.Decimal)
            Dim ParOpt1 = New SqlParameter("@Opt1", SqlDbType.NVarChar, 13)
            Dim ParNewUpdt = New SqlParameter("@NewUpdt", SqlDbType.Int)
            Dim ParDownload = New SqlParameter("@Download", SqlDbType.NVarChar, 15)
            Dim ParCost = New SqlParameter("@Cost", SqlDbType.Decimal)
            Dim ParDept = New SqlParameter("@Dept", SqlDbType.NVarChar, 3)
            Dim ParTank = New SqlParameter("@Tank", SqlDbType.NVarChar, 3)

            'By Soham Gangavane July 26, 2017
            Dim ParPCARDID = New SqlParameter("@PCARDID", SqlDbType.NVarChar, 10)
            Dim ParTCARDID = New SqlParameter("@CARD_ID", SqlDbType.NVarChar, 10)

            'By Soham Gangavane march 23, 2018
            Dim ParTransactionType = New SqlParameter("@TransactionType", SqlDbType.Char, 1)

            ParSentry.Direction = ParameterDirection.Input
            ParTXTN.Direction = ParameterDirection.Input
            ParNewPoll.Direction = ParameterDirection.Input
            ParDateTime.Direction = ParameterDirection.Input
            ParVehID.Direction = ParameterDirection.Input
            ParVKey.Direction = ParameterDirection.Input
            ParMiles.Direction = ParameterDirection.Input
            ParHours.Direction = ParameterDirection.Input
            ParPrevHours.Direction = ParameterDirection.Input
            ParPrevMiles.Direction = ParameterDirection.Input
            ParVCardID.Direction = ParameterDirection.Input
            ParAcctID.Direction = ParameterDirection.Input
            ParPersonnel.Direction = ParameterDirection.Input
            ParPKey.Direction = ParameterDirection.Input
            ParPump.Direction = ParameterDirection.Input
            ParProd.Direction = ParameterDirection.Input
            ParQty.Direction = ParameterDirection.Input
            ParOpt1.Direction = ParameterDirection.Input
            ParNewUpdt.Direction = ParameterDirection.Input
            ParDownload.Direction = ParameterDirection.Input
            ParCost.Direction = ParameterDirection.Input
            ParDept.Direction = ParameterDirection.Input
            ParTank.Direction = ParameterDirection.Input
            ParPCARDID.Direction = ParameterDirection.Input
            ParTCARDID.Direction = ParameterDirection.Input
            ParTransactionType.Direction = ParameterDirection.Input

            ParSentry.Value = dr("Sentry").ToString()
            ParTXTN.Value = dr("TXTN").ToString()
            ParNewPoll.Value = 1
            ParDateTime.Value = Convert.ToDateTime(dr("StartDT")) 'Startdate
            ParVehID.Value = dr("Vehicle").ToString()
            ParVKey.Value = dr("VKey").ToString()
            ParVCardID.Value = dr("VCardID").ToString()
            ParAcctID.Value = dr("AcctID").ToString()
            ParPersonnel.Value = dr("Personnel").ToString()
            ParPKey.Value = dr("PKey").ToString()
            ParPump.Value = dr("Pump").ToString().PadLeft(2, "0")
            ParQty.Value = Convert.ToDecimal(dr("Quantity").ToString())
            ParProd.Value = dr("Product").ToString().PadLeft(2, "0")
            ParOpt1.Value = dr("Opt1").ToString() '"FT-IMPORT" 

            'By Soham Gangavane July 26, 2017
            ParPCARDID.Value = dr("PCARDID").ToString()
            ParTCARDID.Value = dr("CARD_ID").ToString()

            ParTransactionType.Value = "I"

            ParNewUpdt.Value = 1
            ParDownload.Value = 0
            'Added By Pritam Date 29-Jan-2015 
            'Calculating Cost while importing Transaction from T_All.txt
            Dim Tds As New DataSet
            Dim parTcollection(0) As SqlParameter
            Dim ParNo = New SqlParameter("@No", SqlDbType.NVarChar, 5)
            ParNo.Direction = ParameterDirection.Input
            ParNo.Value = TankLookUp(dr("Sentry").ToString(), dr("Product").ToString().PadLeft(2, "0"))
            parTcollection(0) = ParNo
            Tds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransGetTankNameProduct", parTcollection)
            If Not Tds Is Nothing Then
                If Tds.Tables(0).Rows.Count > 0 Then
                    Dim TankPrice As String = Tds.Tables(0).Rows(0)(2).ToString()
                    Dim CalculateCost As Decimal = Convert.ToDecimal((Val(TankPrice) * Val(Convert.ToDecimal(dr("Quantity").ToString())))) '.ToString("0.00")
                    ParCost.Value = CalculateCost
                Else
                    'By Soham Gangavane July 24, 2017
                    ParCost.Value = 0
                End If
            Else
                ParCost.Value = 0
            End If
            'Tank Info
            ParTank.Value = TankLookUp(dr("Sentry").ToString(), dr("Product").ToString().PadLeft(2, "0"))
            'Vehicle Info
            dtResults = VehLookUp(dr("Vehicle").ToString())
            If Not dtResults Is Nothing Then
                If dtResults.Rows.Count > 0 Then
                    PrevMiles = Convert.ToInt32(IIf(dtResults.Rows(0)("CURRMILES").ToString() = "", "0", dtResults.Rows(0)("CURRMILES").ToString().ToString())) 'dtResults.Rows(0)("CURRMILES"))
                    PrevHours = Convert.ToInt32(IIf(dtResults.Rows(0)("CURRHours").ToString() = "", "0", dtResults.Rows(0)("CURRHours").ToString().ToString())) 'dtResults.Rows(0)("CURRHours"))
                    ParDept.Value = dtResults.Rows(0)("Dept").ToString()

                    If PrevMiles > 0 Then
                        ParMiles.Value = Convert.ToInt32(IIf(dr("Miles").ToString() = "", "0", dr("Miles").ToString()))
                        ParPrevMiles.Value = PrevMiles
                        ParHours.Value = 0
                        ParPrevHours.Value = 0
                    Else
                        ParHours.Value = Convert.ToInt32(IIf(dr("Miles").ToString() = "", "0", dr("Miles").ToString()))
                        ParPrevHours.Value = PrevHours
                        ParMiles.Value = 0
                        ParPrevMiles.Value = 0
                    End If
                Else
                    ParDept.Value = "000"
                    ParPrevMiles.Value = 0
                    ParPrevHours.Value = 0
                End If
            Else
                ParMiles.Value = Convert.ToInt32(IIf(dr("Miles").ToString() = "", 0, dr("Miles").ToString()))
                ParHours.Value = Convert.ToInt32(IIf(dr("Miles").ToString() = "", 0, dr("Miles").ToString()))
                ParDept.Value = "000"
                ParPrevMiles.Value = 0
                ParPrevHours.Value = 0
            End If



            parcollection(0) = ParSentry
            parcollection(1) = ParTXTN
            parcollection(2) = ParNewPoll
            parcollection(3) = ParDateTime
            parcollection(4) = ParVehID
            parcollection(5) = ParVKey
            parcollection(6) = ParMiles
            parcollection(7) = ParPrevMiles
            parcollection(8) = ParHours
            parcollection(9) = ParPrevHours
            parcollection(10) = ParVCardID
            parcollection(11) = ParAcctID
            parcollection(12) = ParPersonnel
            parcollection(13) = ParPKey
            parcollection(14) = ParPump
            parcollection(15) = ParProd
            parcollection(16) = ParQty
            parcollection(17) = ParOpt1
            parcollection(18) = ParNewUpdt
            parcollection(19) = ParDownload
            parcollection(20) = ParCost
            parcollection(21) = ParDept
            parcollection(22) = ParTank

            'By Soham Gangavane July 26, 2017
            parcollection(23) = ParPCARDID
            parcollection(24) = ParTCARDID

            parcollection(25) = ParTransactionType


            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("USP_TT_TXTN_IMPORT_SG4File", parcollection)
            If blnFlag = True Then
                dt.Rows.Add(dr)
                Return dt
            End If

            Return dt

        Catch SQLEx As SqlException
            Dim cr As New ErrorPage
            If (SQLEx.Class = 14 And SQLEx.Number = 2627) Then
                Return dt
            Else
                cr.errorlog("TransactionsImport.InsertRecords", SQLEx)
                Return Nothing
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.InsertRecords", ex)
            Return Nothing
        End Try
    End Function

    Private Function VehLookUp(ByVal VehID As String) As DataTable
        Try
            Dim ds = New DataSet()
            Dim dsTemp = New DataSet()
            Dim dal = New GeneralizedDAL()
            ds = dal.GetDataSet("SELECT * FROM VEHS WHERE [IDENTITY] ='" + VehID + "'")

            If Not ds Is Nothing Then
                If ds.tables(0).rows.count > 0 Then
                    Return ds.tables(0)
                End If
            End If

            Return Nothing
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.VehLookUp", ex)
            Return Nothing

        End Try
       
    End Function

    Private Function TankLookUp(ByVal Sentry As String, ByVal Prod As String) As String
        Try
            Dim ds, dsResult As New DataSet()
            Dim dsTemp = New DataSet()
            Dim dal As New GeneralizedDAL()
            ds = dal.GetDataSet("SELECT * FROM TANK WHERE [PRODUCT] ='" + Prod + "'")

            If Not ds Is Nothing And Not ds.Tables(0).Rows.Count = 0 Then

                Dim parcollection(1) As SqlParameter
                Dim ParSentry = New SqlParameter("@SentryNo", SqlDbType.NVarChar, 3)
                Dim ParTank = New SqlParameter("@TankNo", SqlDbType.NVarChar, 6)

                ParSentry.Direction = ParameterDirection.Input
                ParTank.Direction = ParameterDirection.Input

                If ds.Tables(0).Rows.Count > 1 Then
                    Dim i As Integer
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        ParSentry.value = Sentry
                        ParTank.value = ds.Tables(0).Rows(i)("NUMBER").ToString()

                        parcollection(0) = ParSentry
                        parcollection(1) = ParTank

                        dsResult = dal.ExecuteStoredProcedureGetDataSet("SP_Get_TankInfo", parcollection)
                        If Not dsResult Is Nothing Then
                            If dsResult.Tables(0).Rows.Count = 0 Then
                            Else
                                Return dsResult.Tables(0).Rows(0)(0).ToString()
                            End If
                        End If
                    Next
                Else

                    ParSentry.value = Sentry
                    ParTank.value = ds.Tables(0).Rows(0)("NUMBER").ToString()

                    parcollection(0) = ParSentry
                    parcollection(1) = ParTank

                    dsResult = dal.ExecuteStoredProcedureGetDataSet("SP_Get_TankInfo", parcollection)
                    If Not dsResult Is Nothing Then
                        If dsResult.Tables(0).Rows.Count <> 0 Then
                            Return dsResult.Tables(0).Rows(0)(0).ToString()
                        Else
                            Return "000"
                        End If
                    End If
                End If
            Else
                Return "000"
            End If




            Return "000"

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TransactionsImport.TankLookUp", ex)
            Return "000"

        End Try
    End Function


    'By Soham Gangavane July 23,2017

    Protected Sub btnProdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProdOK.Click
        Try
            PopUpListColumn.Visible = False
            btnListColumns.Visible = True

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("btnProdOK_Click", ex)
        End Try
    End Sub

    Protected Sub btnListColumns_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListColumns.Click
        Try
            PopUpListColumn.Visible = True
            btnListColumns.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("btnListColumns_Click", ex)
        End Try
    End Sub

    'By Soham Gangavane July 25,2017
    Private Function checkDuplicateTransaction(ByVal TranNumber As String) As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim ds As New DataSet

            Dim strQuery As String
            strQuery = "select * from TXTN where [NUMBER] = '" + TranNumber.Replace("""", "") + "'"
            ds = dal.GetDataSet(strQuery)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.VehicleExists", ex)
        End Try
    End Function

    Private Function VehicleExists(ByVal VehID As String) As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim ds As New DataSet
            'take in a vehicle ID and verify that it exists in the VEHS table
            Dim strQuery As String
            strQuery = "select * from vehs where [IDENTITY] = '" + VehID.Replace("""", "") + "'"
            ds = dal.GetDataSet(strQuery)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage 
            cr.errorlog("TransactionImport.VehicleExists", ex)
        End Try
    End Function


    Private Function ValidateUniqueKeyConatraint(ByVal txtnData As String()) As Boolean
        Try
            Dim ds2 As DataSet
            Dim dal = New GeneralizedDAL()
            Dim GenFun = New GeneralFunctions()

            Dim Pump As String
            Dim Sentry As String
            Dim Quantity As Decimal
            Dim Vehicle As String
            Dim TXTN_DateTime As String

            Sentry = txtnData(0).Replace("""", "").Trim
            Pump = txtnData(11).Replace("""", "").Trim
            Quantity = (Convert.ToDouble(txtnData(13).ToString().Replace("""", "")) / 20).ToString() 'txtnData(13).Replace("""", "").Trim 'Double.Parse(txtQty.Text.Trim())
            Vehicle = txtnData(4).Replace("""", "").Trim

            'Dim DtTXTN As Date = Convert.ToDateTime(txtDate.Text)
            'Dim TmTXTN As DateTime = Convert.ToDateTime(If(txtTime.Text = "", "00:00", txtTime.Text))
            Dim dt1 As New DateTime
            dt1 = Convert.ToDateTime(txtnData(2).Replace("""", "").Trim) '2 for begindatetime
            TXTN_DateTime = dt1

            Dim parcollection(4) As SqlParameter

            Dim ParQUANTITY = New SqlParameter("@QUANTITY", SqlDbType.Decimal)
            Dim ParSentry = New SqlParameter("@Sentry", SqlDbType.NVarChar, 10)
            Dim ParDATETIME = New SqlParameter("@DATETIME", SqlDbType.DateTime)
            Dim ParPUMP = New SqlParameter("@PUMP", SqlDbType.NVarChar, 2)
            Dim ParVehicle = New SqlParameter("@Vehicle", SqlDbType.NVarChar, 15)

            ParQUANTITY.Direction = ParameterDirection.Input
            ParSentry.Direction = ParameterDirection.Input
            ParDATETIME.Direction = ParameterDirection.Input
            ParVehicle.Direction = ParameterDirection.Input
            ParPUMP.Direction = ParameterDirection.Input

            ParPUMP.Value = Pump
            ParVehicle.Value = Vehicle
            ParDATETIME.Value = dt1
            ParSentry.Value = Sentry
            ParQUANTITY.Value = Quantity

            parcollection(0) = ParPUMP
            parcollection(1) = ParVehicle
            parcollection(2) = ParDATETIME
            parcollection(3) = ParSentry
            parcollection(4) = ParQUANTITY

            Dim str As String = Pump + " " + Vehicle + " " + dt1 + " " + Sentry + " " + Quantity.ToString
            Dim strdate As String = dt1.Year.ToString + "-" + dt1.Month.ToString + "-" + dt1.Day.ToString + " " + dt1.Hour.ToString + ":" + dt1.Minute.ToString + ":" + dt1.Second.ToString

            ds2 = dal.GetDataSet("SELECT * FROM TXTN WHERE [VEHICLE] = '" + Vehicle + "' AND [SENTRY] = '" + Sentry + "' AND [PUMP] =" + Pump + " AND [QUANTITY] = " + Quantity.ToString + " AND TXTN.[DATETIME] ='" + strdate + "'")
            'ds2 = dal.ExecuteStoredProcedureGetDataSet("USP_TT_TXTN_UniqueKeyContrain", parcollection)

            If Not ds2 Is Nothing Then
                If ds2.Tables(0).Rows.Count > 0 Then
                    Return False
                Else
                    Return True
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TransactionImport_ValidateUniqueKeyConatraint", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try
    End Function

End Class
