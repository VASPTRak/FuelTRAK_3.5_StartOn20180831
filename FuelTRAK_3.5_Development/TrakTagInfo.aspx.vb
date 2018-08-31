Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Partial Class TrakTagInfo
    Inherits System.Web.UI.Page
    Dim dv As DataView


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TrakTagInfo.Page_Load", ex)
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
                        Dim tagData As String() = line.Split(New Char() {","c})

                        Try

                            'Added By varun Moota, we need to get the data from text file "TrakTagConfigurations.txt", whenever format changes in this
                            'file, then we need to change FuelTRAK as well.Hard Coded Values to be ignore from TrakTagConfiguration file, since they are just Headers.05/05/2011 
                            If Not (line.Trim().StartsWith("Trak") Or line.Trim().StartsWith("---") Or line.Trim().StartsWith("Date")) Then
                                dt = AddDataToTable(tagData, dt)
                            End If


                        Catch ex As Exception
                            Dim cr As New ErrorPage
                            cr.errorlog("TrakTagInfo.FileStream()", ex)
                        End Try

                        '- Write the information to the DataTable

                    End If
                Loop Until line Is Nothing
                '- Close the file/stream
                sr.Close()

                'Bind to the DataGrid.
                If dt.Rows.Count > 0 Then
                    Session.Remove("dvTagInfo")
                    Session("dvTagInfo") = dt.DefaultView
                    dv = dt.DefaultView
                    'Delete the file, once uploaded in DB.
                    File.Delete(filepathVal.Value.ToString())
                    BindGrid()
                Else
                    lblRowsAfft.Text = "No Tag Info exists."
                    lblRowsAfft.Visible = True
                End If

            End If



        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TrakTagInfo.btnSubmit_Click", ex)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            dv = Session("dvTagInfo")
            gvImportVehs.Visible = True
            gvImportVehs.DataSource = dv
            gvImportVehs.DataBind()
            lblRowsAfft.Visible = True
            lblRowsAfft.Text = "New Tag Info exists in FuelTRAK."
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TrakTagInfo.BindGrid()", ex)
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
            cr.errorlog("TrakTagInfo.CreateDT()", ex)
            Return Nothing
        End Try
    End Function
    Private Function AddDataToTable(ByVal dr As String(), ByVal myTable As DataTable) As DataTable
        Try
            Dim row As DataRow


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

            'myTable.Rows.Add(row)
            myTable = InsertRecords(myTable, row)
            Return myTable


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TrakTagInfo.AddDataToTable()", ex)
            Return Nothing
        End Try
    End Function
    Private Function InsertRecords(ByVal dt As DataTable, ByVal dr As DataRow) As DataTable
        Try
            Dim dtResults As New DataTable
            Dim blnFlag As Boolean
           
            Dim dal = New GeneralizedDAL()
            Dim parcollection(11) As SqlParameter
            Dim ParTagID = New SqlParameter("@TagID ", SqlDbType.NVarChar, 10)
            Dim ParSysNbr = New SqlParameter("@SysNbr", SqlDbType.NVarChar, 5)
            Dim ParDateTime = New SqlParameter("@DT", SqlDbType.DateTime)
            Dim ParMiles = New SqlParameter("@Miles", SqlDbType.Int)
            Dim ParHours = New SqlParameter("@Hours", SqlDbType.Int)
            Dim ParLimit = New SqlParameter("@Limit", SqlDbType.NVarChar, 10)
            Dim ParFuels = New SqlParameter("@Fuels", SqlDbType.NVarChar, 10)
            Dim ParPower = New SqlParameter("@Power", SqlDbType.NVarChar, 10)
            Dim ParClicks = New SqlParameter("@Clicks", SqlDbType.NVarChar, 10)
            Dim ParTagMode = New SqlParameter("@Tag_Mode", SqlDbType.NVarChar, 10)
            Dim ParIgntStat = New SqlParameter("@Ignt_Stat", SqlDbType.NVarChar, 10)
            Dim ParMacName = New SqlParameter("@Mac_Name", SqlDbType.NVarChar, 10)



            ParTagID.Direction = ParameterDirection.Input
            ParSysNbr.Direction = ParameterDirection.Input
            ParDateTime.direction = ParameterDirection.Input
            ParMiles.Direction = ParameterDirection.Input
            ParHours.Direction = ParameterDirection.Input
            ParLimit.Direction = ParameterDirection.Input
            ParFuels.Direction = ParameterDirection.Input
            ParPower.Direction = ParameterDirection.Input
            ParClicks.Direction = ParameterDirection.Input
            ParTagMode.Direction = ParameterDirection.Input
            ParIgntStat.Direction = ParameterDirection.Input
            ParMacName.Direction = ParameterDirection.Input
          

            ParTagID.value = dr("Tag_ID").ToString()
            ParSysNbr.value = dr("Sys_Nbr").ToString()
            ParDateTime.value = Convert.ToDateTime(dr("DT"))
            ParMiles.value = dr("Miles").ToString()
            ParHours.value = dr("Hours").ToString()
            ParLimit.value = dr("Limit").ToString()
            ParFuels.value = dr("Fuels").ToString()
            ParPower.value = dr("Power").ToString()
            ParClicks.value = dr("Clicks").ToString()
            ParTagMode.value = dr("Tag_Mode").ToString()
            ParIgntStat.value = dr("Igtn_Stat_det").ToString()
            ParMacName.value = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName
           
         

            parcollection(0) = ParTagID
            parcollection(1) = ParSysNbr
            parcollection(2) = ParDateTime
            parcollection(3) = ParMiles
            parcollection(4) = ParHours
            parcollection(5) = ParLimit
            parcollection(6) = ParFuels
            parcollection(7) = ParPower
            parcollection(8) = ParClicks
            parcollection(9) = ParTagMode
            parcollection(10) = ParIgntStat
            parcollection(11) = ParMacName
            

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("USP_TT_TRAKTag_Info", parcollection)

            If blnFlag = True Then
                dt.Rows.Add(dr)
                Return dt
            End If

            Return dt

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TrakTagInfo.gvImportTXTNs_PageIndexChanging", ex)
            Return Nothing
        End Try
    End Function
    Protected Sub gvImportVehs_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvImportVehs.PageIndexChanging

        Try
            gvImportVehs.PageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TrakTagInfo.gvImportVehs_PageIndexChanging", ex)
        End Try

    End Sub
End Class

