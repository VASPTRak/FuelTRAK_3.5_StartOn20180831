Imports System.IO
Imports System.Data
Imports System.Data.SqlClient


Partial Class ImportTagInfo
    Inherits System.Web.UI.Page

    Dim dal As New GeneralizedDAL

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If fuTagInfo.HasFile Then
                Dim filename As String = Path.GetFileName(fuTagInfo.FileName)
                fuTagInfo.SaveAs(Server.MapPath("~/TagInfoXML/") + filename)
                Response.Write("Upload status: File uploaded!")
                ReadXML(Server.MapPath("~/TagInfoXML/" + filename))
                'GridView1.DataBind()
            End If
        Catch ex As Exception
            Response.Write("Upload status: The file could not be uploaded. The following error occured: " + ex.Message)
        End Try
    End Sub

    Public Sub ReadXML(ByVal file As String)

        'create the DataTable that will hold the data
        Dim table As New DataTable("Table")
        Try
            'open the file using a Stream
            Using stream As Stream = New FileStream(file, FileMode.Open, FileAccess.Read)
                'create the table with the appropriate column names
                table.Columns.Add("ID")
                table.Columns.Add("Tag_ID")
                table.Columns.Add("Sys_Nbr")
                table.Columns.Add("Date_Added")
                table.Columns.Add("Miles")
                table.Columns.Add("Hours")
                table.Columns.Add("Limit")
                table.Columns.Add("Fuels")
                table.Columns.Add("Power")
                table.Columns.Add("Clicks")
                table.Columns.Add("Tag_Mode")
                table.Columns.Add("Ignition_State")
                table.Columns.Add("MachineName")
                table.Columns.Add("ProgrammedBy")
                table.Columns.Add("Vehicle_Make")
                table.Columns.Add("Vehicle_Model")
                table.Columns.Add("Vehicle_Year")
                table.Columns.Add("Vehicle_Product")
                table.Columns.Add("Calib_Miles")
                table.Columns.Add("Calib_Completed")
                table.Columns.Add("OBDII_Interface")
                table.Columns.Add("InstalledBy")
                table.Columns.Add("Mileage_Confirm")
                table.Columns.Add("Tag_Install_LOC")
                table.Columns.Add("Interface_Install_LOC")
                table.Columns.Add("Space_Under_Tag")
                table.Columns.Add("ECM_LOC")
                table.Columns.Add("ECM_Conn_Type")
                table.Columns.Add("Avg_Signal")
                table.Columns.Add("Tag_Hex_Date")
                table.Columns.Add("PID31_Exists")
                table.Columns.Add("Comments")
                table.Columns.Add("Life_Time_Miles")
                table.Columns.Add("ChkECM")
                table.Columns.Add("SyncFlag")
                table.Columns.Add("MechanicalOdometer")
                'use ReadXml to read the XML stream
                table.ReadXml(stream)
                'Return table
                'insert into table taginfo
                Dim i As Integer
                If table.Rows.Count > 0 Then
                    Dim qry As String = "DELETE FROM TAGINFO WHERE [Sys_Nbr] = '" & table.Rows(0)("Sys_Nbr") & "'"
                    Dim j As Integer = dal.ExecuteNonQuery(qry)
                    If (j > -1) Then
                        For i = 0 To table.Rows.Count - 1
                            InsertTagInfo(table.Rows(i))
                        Next
                    End If
                End If
            End Using
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ImportTagInfo.ReadXML", ex.InnerException)
        End Try
    End Sub
    Private Sub InsertTagInfo(ByVal dtRow As DataRow)
        Try
            Try
                Dim qry As String = "Select * from Vehs WHERE [IDENTITY] = '" & dtRow("Tag_ID") & "'"
                Dim blnStatus As Boolean
                blnStatus = dal.GetDataSetbool(qry)
                If (blnStatus = False) Then
                    Dim param1 As SqlParameter() = New SqlParameter(20) {}
                    param1(0) = New SqlParameter("@Identity", DbType.VarNumeric)
                    param1(1) = New SqlParameter("@VehYear", DbType.VarNumeric)
                    param1(2) = New SqlParameter("@VehMake", DbType.VarNumeric)
                    param1(3) = New SqlParameter("@VehModel", DbType.VarNumeric)
                    param1(4) = New SqlParameter("@DateAdded", DbType.DateTime)
                    param1(5) = New SqlParameter("@Mileage", DbType.[Boolean])
                    param1(6) = New SqlParameter("@SecondKey", DbType.[Boolean])
                    param1(7) = New SqlParameter("@Reqodom", DbType.[Boolean])
                    param1(8) = New SqlParameter("@CurrHours", DbType.Int32)
                    param1(9) = New SqlParameter("@Limit", DbType.VarNumeric)
                    param1(10) = New SqlParameter("@Inventory", DbType.[Boolean])
                    param1(11) = New SqlParameter("@Bulk", DbType.[Boolean])
                    param1(12) = New SqlParameter("@Bus", DbType.[Boolean])
                    param1(13) = New SqlParameter("@Master", DbType.[Boolean])
                    param1(14) = New SqlParameter("@Locked", DbType.[Boolean])
                    param1(15) = New SqlParameter("@Fuels", DbType.VarNumeric)
                    param1(16) = New SqlParameter("@TagId", DbType.VarNumeric)
                    param1(17) = New SqlParameter("@Power", DbType.VarNumeric)
                    param1(18) = New SqlParameter("@Clicks", DbType.VarNumeric)
                    param1(19) = New SqlParameter("@TagMode", DbType.VarNumeric)
                    param1(20) = New SqlParameter("@IgnitionState", DbType.VarNumeric)
                    param1(0).Value = dtRow("Tag_ID")
                    param1(1).Value = dtRow("Vehicle_Year")
                    param1(2).Value = dtRow("Vehicle_Make")
                    param1(3).Value = dtRow("Vehicle_Model")
                    param1(4).Value = DateTime.Parse(dtRow("Date_Added"))
                    param1(5).Value = 0
                    param1(6).Value = 0
                    param1(7).Value = 0
                    param1(8).Value = dtRow("Hours")
                    param1(9).Value = dtRow("Limit")
                    param1(10).Value = 0
                    param1(11).Value = 0
                    param1(12).Value = 0
                    param1(13).Value = 0
                    param1(14).Value = 0
                    param1(15).Value = dtRow("Fuels")
                    param1(16).Value = dtRow("Tag_ID")
                    param1(17).Value = dtRow("Power")
                    param1(18).Value = dtRow("Clicks")
                    param1(19).Value = dtRow("Tag_Mode")
                    param1(20).Value = dtRow("Ignition_State")
                    'the parameters @Mileage,@SecondKey,@Reqodom,@Inventory,@Bulk,@Bus,@Master,@Locked  are not provided by XML file... 
                    'but these parameters are required to insert record in Vehs table Since AllowNull property of them  is set to false.
                    'All these parameters are of DataType Bit .... hence they are set to false while inserting record in to Vehs table.
                    Dim blnFlag1 As Boolean = dal.ExecuteStoredProcedureGetBoolean("USP_TT_INSERT_VEHICLE_FROM_XML", param1)
                    If blnFlag1 Then
                    Else
                    End If
                End If
            Catch ex As Exception
                Dim cr As New ErrorPage
                cr.errorlog("ImportTagInfo.VehsDataInsert", ex)
            End Try
            Dim param As SqlParameter() = New SqlParameter(34) {}
            param(0) = New SqlParameter("@Tag_ID", DbType.VarNumeric)
            param(1) = New SqlParameter("@Sys_Nbr", DbType.VarNumeric)
            param(2) = New SqlParameter("@Date_Added", DbType.DateTime)
            param(3) = New SqlParameter("@Miles", DbType.Int32)
            param(4) = New SqlParameter("@Hours", DbType.Int32)
            param(5) = New SqlParameter("@Limit", DbType.VarNumeric)
            param(6) = New SqlParameter("@Fuels", DbType.VarNumeric)
            param(7) = New SqlParameter("@Power", DbType.VarNumeric)
            param(8) = New SqlParameter("@Clicks", DbType.VarNumeric)
            param(9) = New SqlParameter("@Tag_Mode", DbType.VarNumeric)
            param(10) = New SqlParameter("@Ignition_State", DbType.VarNumeric)
            param(11) = New SqlParameter("@MachineName", DbType.VarNumeric)
            param(12) = New SqlParameter("@ProgrammedBy", DbType.VarNumeric)
            param(13) = New SqlParameter("@Vehicle_Make", DbType.VarNumeric)
            param(14) = New SqlParameter("@Vehicle_Model", DbType.VarNumeric)
            param(15) = New SqlParameter("@Vehicle_Year", DbType.Int32)
            param(16) = New SqlParameter("@Vehicle_Product", DbType.VarNumeric)
            param(17) = New SqlParameter("@Calib_Miles", DbType.Int32)
            param(18) = New SqlParameter("@Calib_Completed", DbType.VarNumeric)
            param(19) = New SqlParameter("@OBDII_Interface", DbType.VarNumeric)
            param(20) = New SqlParameter("@InstalledBy", DbType.VarNumeric)
            param(21) = New SqlParameter("@Mileage_Confirm", DbType.VarNumeric)
            param(22) = New SqlParameter("@Tag_Install_LOC", DbType.VarNumeric)
            param(23) = New SqlParameter("@Interface_Install_LOC", DbType.VarNumeric)
            param(24) = New SqlParameter("@Space_Under_Tag", DbType.VarNumeric)
            param(25) = New SqlParameter("@ECM_LOC", DbType.VarNumeric)
            param(26) = New SqlParameter("@ECM_Conn_Type", DbType.VarNumeric)
            param(27) = New SqlParameter("@Avg_Signal", DbType.Int32)
            param(28) = New SqlParameter("@Tag_Hex_Date", DbType.VarNumeric)
            param(29) = New SqlParameter("@PID31_Exists", DbType.[Boolean])
            param(30) = New SqlParameter("@Comments", DbType.VarNumeric)
            param(31) = New SqlParameter("@Life_Time_Miles", DbType.VarNumeric)
            param(32) = New SqlParameter("@ChkECM", DbType.[Boolean])
            param(33) = New SqlParameter("@MechanicalOdometer", DbType.Int32)
            param(34) = New SqlParameter("@Vehs_IDENTITY", DbType.VarNumeric)

            param(0).Value = dtRow("Tag_ID")
            param(1).Value = dtRow("Sys_Nbr")
            param(2).Value = DateTime.Parse(dtRow("Date_Added"))
            param(3).Value = dtRow("Miles")
            param(4).Value = dtRow("Hours")
            param(5).Value = dtRow("Limit")
            param(6).Value = dtRow("Fuels")
            param(7).Value = dtRow("Power")
            param(8).Value = dtRow("Clicks")
            param(9).Value = dtRow("Tag_Mode")
            param(10).Value = dtRow("Ignition_State")
            param(11).Value = dtRow("MachineName")
            param(12).Value = dtRow("ProgrammedBy")
            param(13).Value = dtRow("Vehicle_Make")
            param(14).Value = dtRow("Vehicle_Model")
            param(15).Value = dtRow("Vehicle_Year")
            param(16).Value = dtRow("Vehicle_Product")
            param(17).Value = dtRow("Calib_Miles")
            param(18).Value = dtRow("Calib_Completed")
            param(19).Value = dtRow("OBDII_Interface")
            param(20).Value = dtRow("InstalledBy")
            param(21).Value = dtRow("Mileage_Confirm")
            param(22).Value = dtRow("Tag_Install_LOC")
            param(23).Value = dtRow("Interface_Install_LOC")
            param(24).Value = dtRow("Space_Under_Tag")
            param(25).Value = dtRow("ECM_LOC")
            param(26).Value = dtRow("ECM_Conn_Type")
            param(27).Value = dtRow("Avg_Signal")
            param(28).Value = dtRow("Tag_Hex_Date")
            param(29).Value = dtRow("PID31_Exists")
            param(30).Value = dtRow("Comments")
            param(31).Value = dtRow("Life_Time_Miles")
            param(32).Value = dtRow("ChkECM")
            param(33).Value = dtRow("MechanicalOdometer")
            param(34).Value = dtRow("Tag_ID")

            Dim blnFlag As Boolean = dal.ExecuteStoredProcedureGetBoolean("USP_TT_INSERTTAGINFO", param)

            If blnFlag Then

            Else

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ImportTagInfo.TagInfoDataInsert", ex)
        End Try
    End Sub
    'Public Sub InsertTagInfo(ByVal ds As DataSet)
    '    Try
    '        Try
    '            Dim qry As String = "Select * from Vehs WHERE [IDENTITY] = '" & dtb.Rows(0).Item("Tag_ID") & "'"
    '            Dim blnStatus As Boolean
    '            blnStatus = dal.GetDataSetbool(qry)
    '            If (blnStatus = False) Then
    '                Dim param1 As SqlParameter() = New SqlParameter(20) {}
    '                param1(0) = New SqlParameter("@Identity", DbType.VarNumeric)
    '                param1(1) = New SqlParameter("@VehYear", DbType.VarNumeric)
    '                param1(2) = New SqlParameter("@VehMake", DbType.VarNumeric)
    '                param1(3) = New SqlParameter("@VehModel", DbType.VarNumeric)
    '                param1(4) = New SqlParameter("@DateAdded", DbType.DateTime)
    '                param1(5) = New SqlParameter("@Mileage", DbType.[Boolean])
    '                param1(6) = New SqlParameter("@SecondKey", DbType.[Boolean])
    '                param1(7) = New SqlParameter("@Reqodom", DbType.[Boolean])
    '                param1(8) = New SqlParameter("@CurrHours", DbType.Int32)
    '                param1(9) = New SqlParameter("@Limit", DbType.VarNumeric)
    '                param1(10) = New SqlParameter("@Inventory", DbType.[Boolean])
    '                param1(11) = New SqlParameter("@Bulk", DbType.[Boolean])
    '                param1(12) = New SqlParameter("@Bus", DbType.[Boolean])
    '                param1(13) = New SqlParameter("@Master", DbType.[Boolean])
    '                param1(14) = New SqlParameter("@Locked", DbType.[Boolean])
    '                param1(15) = New SqlParameter("@Fuels", DbType.VarNumeric)
    '                param1(16) = New SqlParameter("@TagId", DbType.VarNumeric)
    '                param1(17) = New SqlParameter("@Power", DbType.VarNumeric)
    '                param1(18) = New SqlParameter("@Clicks", DbType.VarNumeric)
    '                param1(19) = New SqlParameter("@TagMode", DbType.VarNumeric)
    '                param1(20) = New SqlParameter("@IgnitionState", DbType.VarNumeric)

    '                param1(0).Value = dtb.Rows(0).Item("Tag_ID")
    '                param1(1).Value = dtb.Rows(0).Item("Vehicle_Year")
    '                param1(2).Value = dtb.Rows(0).Item("Vehicle_Make")
    '                param1(3).Value = dtb.Rows(0).Item("Vehicle_Model")
    '                param1(4).Value = DateTime.Parse(dtb.Rows(0).Item("Date_Added"))
    '                param1(5).Value = 0
    '                param1(6).Value = 0
    '                param1(7).Value = 0
    '                param1(8).Value = dtb.Rows(0).Item("Hours")
    '                param1(9).Value = dtb.Rows(0).Item("Limit")
    '                param1(10).Value = 0
    '                param1(11).Value = 0
    '                param1(12).Value = 0
    '                param1(13).Value = 0
    '                param1(14).Value = 0
    '                param1(15).Value = dtb.Rows(0).Item("Fuels")
    '                param1(16).Value = dtb.Rows(0).Item("Tag_ID")
    '                param1(17).Value = dtb.Rows(0).Item("Power")
    '                param1(18).Value = dtb.Rows(0).Item("Clicks")
    '                param1(19).Value = dtb.Rows(0).Item("Tag_Mode")
    '                param1(20).Value = dtb.Rows(0).Item("Ignition_State")

    '                'the parameters @Mileage,@SecondKey,@Reqodom,@Inventory,@Bulk,@Bus,@Master,@Locked  are not provided by XML file... 
    '                'but these parameters are required to insert record in Vehs table Since AllowNull property of them  is set to false.
    '                'All these parameters are of DataType Bit .... hence they are set to false while inserting record in to Vehs table.

    '                Dim blnFlag1 As Boolean = dal.ExecuteStoredProcedureGetBoolean("USP_TT_INSERT_VEHICLE_FROM_XML", param1)

    '                If blnFlag1 Then

    '                Else

    '                End If

    '            End If

    '        Catch ex As Exception
    '            Dim cr As New ErrorPage
    '            cr.errorlog("ImportTagInfo.VehsDataInsert", ex)
    '        End Try


    '        Dim param As SqlParameter() = New SqlParameter(34) {}
    '        param(0) = New SqlParameter("@Tag_ID", DbType.VarNumeric)
    '        param(1) = New SqlParameter("@Sys_Nbr", DbType.VarNumeric)
    '        param(2) = New SqlParameter("@Date_Added", DbType.DateTime)
    '        param(3) = New SqlParameter("@Miles", DbType.Int32)
    '        param(4) = New SqlParameter("@Hours", DbType.Int32)
    '        param(5) = New SqlParameter("@Limit", DbType.VarNumeric)
    '        param(6) = New SqlParameter("@Fuels", DbType.VarNumeric)
    '        param(7) = New SqlParameter("@Power", DbType.VarNumeric)
    '        param(8) = New SqlParameter("@Clicks", DbType.VarNumeric)
    '        param(9) = New SqlParameter("@Tag_Mode", DbType.VarNumeric)
    '        param(10) = New SqlParameter("@Ignition_State", DbType.VarNumeric)
    '        param(11) = New SqlParameter("@MachineName", DbType.VarNumeric)
    '        param(12) = New SqlParameter("@ProgrammedBy", DbType.VarNumeric)
    '        param(13) = New SqlParameter("@Vehicle_Make", DbType.VarNumeric)
    '        param(14) = New SqlParameter("@Vehicle_Model", DbType.VarNumeric)
    '        param(15) = New SqlParameter("@Vehicle_Year", DbType.Int32)
    '        param(16) = New SqlParameter("@Vehicle_Product", DbType.VarNumeric)
    '        param(17) = New SqlParameter("@Calib_Miles", DbType.Int32)
    '        param(18) = New SqlParameter("@Calib_Completed", DbType.VarNumeric)
    '        param(19) = New SqlParameter("@OBDII_Interface", DbType.VarNumeric)
    '        param(20) = New SqlParameter("@InstalledBy", DbType.VarNumeric)
    '        param(21) = New SqlParameter("@Mileage_Confirm", DbType.VarNumeric)
    '        param(22) = New SqlParameter("@Tag_Install_LOC", DbType.VarNumeric)
    '        param(23) = New SqlParameter("@Interface_Install_LOC", DbType.VarNumeric)
    '        param(24) = New SqlParameter("@Space_Under_Tag", DbType.VarNumeric)
    '        param(25) = New SqlParameter("@ECM_LOC", DbType.VarNumeric)
    '        param(26) = New SqlParameter("@ECM_Conn_Type", DbType.VarNumeric)
    '        param(27) = New SqlParameter("@Avg_Signal", DbType.Int32)
    '        param(28) = New SqlParameter("@Tag_Hex_Date", DbType.VarNumeric)
    '        param(29) = New SqlParameter("@PID31_Exists", DbType.[Boolean])
    '        param(30) = New SqlParameter("@Comments", DbType.VarNumeric)
    '        param(31) = New SqlParameter("@Life_Time_Miles", DbType.VarNumeric)
    '        param(32) = New SqlParameter("@ChkECM", DbType.[Boolean])
    '        param(33) = New SqlParameter("@MechanicalOdometer", DbType.Int32)
    '        param(34) = New SqlParameter("@Vehs_IDENTITY", DbType.VarNumeric)

    '        param(0).Value = dtb.Rows(0).Item("Tag_ID")
    '        param(1).Value = dtb.Rows(0).Item("Sys_Nbr")
    '        param(2).Value = DateTime.Parse(dtb.Rows(0).Item("Date_Added"))
    '        param(3).Value = dtb.Rows(0).Item("Miles")
    '        param(4).Value = dtb.Rows(0).Item("Hours")
    '        param(5).Value = dtb.Rows(0).Item("Limit")
    '        param(6).Value = dtb.Rows(0).Item("Fuels")
    '        param(7).Value = dtb.Rows(0).Item("Power")
    '        param(8).Value = dtb.Rows(0).Item("Clicks")
    '        param(9).Value = dtb.Rows(0).Item("Tag_Mode")
    '        param(10).Value = dtb.Rows(0).Item("Ignition_State")
    '        param(11).Value = dtb.Rows(0).Item("MachineName")
    '        param(12).Value = dtb.Rows(0).Item("ProgrammedBy")
    '        param(13).Value = dtb.Rows(0).Item("Vehicle_Make")
    '        param(14).Value = dtb.Rows(0).Item("Vehicle_Model")
    '        param(15).Value = dtb.Rows(0).Item("Vehicle_Year")
    '        param(16).Value = dtb.Rows(0).Item("Vehicle_Product")
    '        param(17).Value = dtb.Rows(0).Item("Calib_Miles")
    '        param(18).Value = dtb.Rows(0).Item("Calib_Completed")
    '        param(19).Value = dtb.Rows(0).Item("OBDII_Interface")
    '        param(20).Value = dtb.Rows(0).Item("InstalledBy")
    '        param(21).Value = dtb.Rows(0).Item("Mileage_Confirm")
    '        param(22).Value = dtb.Rows(0).Item("Tag_Install_LOC")
    '        param(23).Value = dtb.Rows(0).Item("Interface_Install_LOC")
    '        param(24).Value = dtb.Rows(0).Item("Space_Under_Tag")
    '        param(25).Value = dtb.Rows(0).Item("ECM_LOC")
    '        param(26).Value = dtb.Rows(0).Item("ECM_Conn_Type")
    '        param(27).Value = dtb.Rows(0).Item("Avg_Signal")
    '        param(28).Value = dtb.Rows(0).Item("Tag_Hex_Date")
    '        param(29).Value = dtb.Rows(0).Item("PID31_Exists")
    '        param(30).Value = dtb.Rows(0).Item("Comments")
    '        param(31).Value = dtb.Rows(0).Item("Life_Time_Miles")
    '        param(32).Value = dtb.Rows(0).Item("ChkECM")
    '        param(33).Value = dtb.Rows(0).Item("MechanicalOdometer")
    '        param(34).Value = dtb.Rows(0).Item("Tag_ID")

    '        Dim blnFlag As Boolean = dal.ExecuteStoredProcedureGetBoolean("USP_TT_INSERTTAGINFO", param)

    '        If blnFlag Then

    '        Else

    '        End If
    '    Catch ex As Exception
    '        Dim cr As New ErrorPage
    '        cr.errorlog("ImportTagInfo.TagInfoDataInsert", ex)
    '    End Try
    'End Sub





End Class
