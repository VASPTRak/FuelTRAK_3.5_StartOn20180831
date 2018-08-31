Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports System.IO


Partial Class Vehicle_Import
    Inherits System.Web.UI.Page
    Dim dv As DataView


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
           
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Import.Page_Load", ex)
        End Try

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If (fuVehs.HasFile) Then
                'Create a DataTable
                Dim dt = New DataTable()
                dt = CreateDT()

                'Location of the Text file.
                Dim fileName As String = filepathVal.Value.ToString()
                '- Set the line counter
                Dim lineNumber As Integer = 0

                '- Open the text file/stream
                Dim sr As StreamReader = New StreamReader(fileName)
                Dim line As String = ""

                Do
                    line = sr.ReadLine()
                    '- If it goes past the last line of the file,
                    '- it drops out of the loop
                    If line <> Nothing Then
                        Dim vehData As String() = line.Split(New Char() {","c})

                        Try

                            'Added By varun Moota, we need to get the data from text file "TrakTagConfigurations.txt", whenever format changes in this
                            'file, then we need to change FuelTRAK as well.Hard Coded Values to be ignore from TrakTagConfiguration file, since they are just Headers.05/05/2011 
                            If Not (line.Trim().StartsWith("Trak") Or line.Trim().StartsWith("---") Or line.Trim().StartsWith("Date")) Then

                                Dim blnVehExists As Boolean
                                blnVehExists = VehLookUp(vehData(2).Trim.ToString())
                                If Not blnVehExists Then
                                    dt = AddDataToTable(vehData, dt)
                                End If

                            End If


                        Catch ex As Exception
                            Dim cr As New ErrorPage
                            cr.errorlog("Vehicle_Import.FileStream()", ex)
                        End Try

                        '- Write the information to the DataTable

                    End If
                Loop Until line Is Nothing
                '- Close the file/stream
                sr.Close()

                'Bind to the DataGrid.
                If dt.Rows.Count > 0 Then
                    Session.Remove("dvVehsUpld")
                    Session("dvVehsUpld") = dt.DefaultView
                    dv = dt.DefaultView
                    BindGrid()
                Else
                    lblRowsAfft.Text = "No vehicles are Imported in FuelTRAK."
                    lblRowsAfft.Visible = True
                End If

            End If



        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Import.btnSubmit_Click", ex)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            dv = Session("dvVehsUpld")
            gvImportVehs.Visible = True
            gvImportVehs.DataSource = dv
            gvImportVehs.DataBind()
            lblRowsAfft.Visible = True
            lblRowsAfft.Text = "New Vehicles uploaded to FuelTRAK."
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Import.BindGrid()", ex)
        End Try
    End Sub
    Private Function CreateDT() As datatable
        Try


            Dim tempTable As DataTable = New DataTable()

            Dim tempDataColumn As DataColumn

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "DT"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Sys_Nbr"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Tag_ID"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Miles"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Hours"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Limit"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Fuels"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Power"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Clicks"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Tag_Mode"
            tempTable.Columns.Add(tempDataColumn)

            tempDataColumn = New DataColumn()
            tempDataColumn.DataType = Type.GetType("System.String")
            tempDataColumn.ColumnName = "Igtn_Stat_det"
            tempTable.Columns.Add(tempDataColumn)

            Return tempTable
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Import.CreateDT()", ex)
            Return Nothing
        End Try
    End Function

    Private Function AddDataToTable(ByVal dr As String(), ByVal myTable As DataTable) As DataTable
        Try
            Dim row As DataRow
            Dim blnFlag As Boolean


            row = myTable.NewRow()

            row("DT") = dr(0).Replace("""", "")
            row("Sys_Nbr") = dr(1).Trim.ToString().Replace("""", "")
            row("Tag_ID") = dr(2).Trim.ToString().Replace("""", "")
            row("Miles") = dr(3).Trim.ToString().Replace("""", "")
            row("Hours") = dr(4).Trim.ToString().Replace("""", "")
            row("Limit") = dr(5).ToString().Replace("""", "")
            row("Fuels") = dr(6).ToString().Replace("""", "")
            row("Power") = dr(7).ToString().Replace("""", "")
            row("Clicks") = dr(8).Trim.ToString().Replace("""", "")
            row("Tag_Mode") = dr(9).ToString().Replace("""", "")
            row("Igtn_Stat_det") = dr(10).ToString().Replace("""", "")

            myTable.Rows.Add(row)


            Dim dal = New GeneralizedDAL()
            Dim parcollection(17) As SqlParameter
            Dim ParVehId = New SqlParameter("@VehId", SqlDbType.NVarChar, 10)
            Dim ParNewUpdt = New SqlParameter("@NewUpdt", SqlDbType.Int)
            Dim ParMiles = New SqlParameter("@Miles", SqlDbType.Int)
            Dim ParHours = New SqlParameter("@Hours", SqlDbType.Int)
            Dim ParLimit = New SqlParameter("@Limit", SqlDbType.NVarChar, 5)
            Dim ParFuels = New SqlParameter("@Fuels", SqlDbType.NVarChar, 10)
            Dim ParMileage = New SqlParameter("@Mileage", SqlDbType.Bit)
            Dim ParSecondKey = New SqlParameter("@Second_key", SqlDbType.Bit)
            Dim ParReqOdom = New SqlParameter("@ReqOdom", SqlDbType.Bit)
            Dim ParInventory = New SqlParameter("@Inventory", SqlDbType.Bit)
            Dim ParBulk = New SqlParameter("@Bulk", SqlDbType.Bit)
            Dim ParBus = New SqlParameter("@Bus", SqlDbType.Bit)
            Dim ParMaster = New SqlParameter("@Master", SqlDbType.Bit)
            Dim ParLocked = New SqlParameter("@Locked", SqlDbType.Bit)
            Dim ParPower = New SqlParameter("@Power", SqlDbType.NVarChar, 10)
            Dim ParClicks = New SqlParameter("@Clicks", SqlDbType.NVarChar, 10)
            Dim ParTagMode = New SqlParameter("@Tag_Mode", SqlDbType.NVarChar, 10)
            Dim ParIngStat = New SqlParameter("@Ignt_Stat", SqlDbType.NVarChar, 10)


            ParVehId.Direction = ParameterDirection.Input
            ParNewUpdt.Direction = ParameterDirection.Input
            ParMiles.Direction = ParameterDirection.Input
            ParHours.Direction = ParameterDirection.Input
            ParLimit.Direction = ParameterDirection.Input
            ParFuels.Direction = ParameterDirection.Input
            ParMileage.Direction = ParameterDirection.Input
            ParSecondKey.Direction = ParameterDirection.Input
            ParReqOdom.Direction = ParameterDirection.Input
            ParInventory.Direction = ParameterDirection.Input
            ParBulk.Direction = ParameterDirection.Input
            ParBus.Direction = ParameterDirection.Input
            ParMaster.Direction = ParameterDirection.Input
            ParLocked.Direction = ParameterDirection.Input
            ParPower.Direction = ParameterDirection.Input
            ParClicks.Direction = ParameterDirection.Input
            ParTagMode.Direction = ParameterDirection.Input
            ParIngStat.Direction = ParameterDirection.Input

            ParVehId.value = Convert.ToString(row("Tag_ID").ToString())
            ParNewUpdt.value = 1
            ParMiles.value = Convert.ToInt32(row("Miles").ToString())
            ParHours.value = Convert.ToInt32(row("Hours").ToString())
            ParLimit.value = Convert.ToString(row("Limit").ToString())
            ParFuels.value = Convert.ToString(row("Fuels").ToString())
            If (row("Miles").ToString() = "") Then
                ParMileage.value = False
            Else
                ParMileage.value = True

            End If


            ParSecondKey.value = False
            ParReqOdom.value = False
            ParInventory.value = False
            ParBulk.value = False
            ParBus.value = False
            ParMaster.value = False
            ParLocked.value = False
            ParPower.value = Convert.ToString(row("Power").ToString())
            ParClicks.value = Convert.ToString(row("Clicks").ToString())
            ParTagMode.value = Convert.ToString(row("Tag_Mode").ToString())
            ParIngStat.value = Convert.ToString(row("Igtn_Stat_det").ToString())

            parcollection(0) = ParVehId
            parcollection(1) = ParNewUpdt
            parcollection(2) = ParMiles
            parcollection(3) = ParHours
            parcollection(4) = ParLimit
            parcollection(5) = ParFuels
            parcollection(6) = ParMileage
            parcollection(7) = ParSecondKey
            parcollection(8) = ParReqOdom
            parcollection(9) = ParInventory
            parcollection(10) = ParBulk
            parcollection(11) = ParBus
            parcollection(12) = ParMaster
            parcollection(13) = ParLocked
            parcollection(14) = ParPower
            parcollection(15) = ParClicks
            parcollection(16) = ParTagMode
            parcollection(17) = ParIngStat

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_Veh_Import_TagConfigFile", parcollection)

            If blnFlag Then
                Return myTable
            End If

            Return Nothing
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Import.AddDataToTable()", ex)
            Return Nothing
        End Try
    End Function
    
    Protected Sub gvImportVehs_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvImportVehs.PageIndexChanging

        Try
            gvImportVehs.PageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Import.gvImportVehs_PageIndexChanging", ex)
        End Try
        
    End Sub

    Private Function VehLookUp(ByVal VehID As String) As Boolean
        Try
            Dim dal As New GeneralizedDAL
            Dim dsVehData As New DataSet
            VehID = VehID.ToString().Trim.Replace("""", "")
            dsVehData = dal.GetDataSet("Select [IDENTITY] FROM VEHS WHERE [IDENTITY] = '" + VehID.ToString().Replace("""", "") + "'")
            If dsVehData.Tables(0).Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Import.VehLookUp", ex)
            Return False
        End Try
    End Function
End Class
