Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Timers

Public Class AutoExport
    Shared path As String = ConfigurationSettings.AppSettings("AutoExport").ToString() 'HttpContext.Current.Server.MapPath("AutoExportTXTN\")
    ' Run the checking every minute
    'Public Shared timer As New Timer(300000)
    Shared Interval As Integer = Convert.ToInt32(ConfigurationSettings.AppSettings("Delay"))
    Public Shared timer As New Timer(Interval)

#Region "Constructor"
    Public Sub New()
        Dim cr As New ErrorPage
        Try
            'If threadingTimer Is Nothing Then
            '    cr.errorlogText("AutoExport_StartTimer", "threadingTimer Is Nothing")
            '    threadingTimer = New Timer(New TimerCallback(AddressOf CheckData), HttpContext.Current, 0, 60000)
            'End If
            AutoExport.StartRuning()
        Catch ex As Exception
            Dim errmsg As String
            cr.errorlog("AutoExport_Constructor", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
#End Region

    ' Enable the timer to run
    Public Shared Sub StartRuning()
        AddHandler timer.Elapsed, AddressOf CheckData
        timer.Start()
    End Sub

    ' This method will be called every minute
    Private Shared Sub CheckData(ByVal sender As Object, ByVal e As ElapsedEventArgs)
        'Get auto pull setup for Export TXTN
        Dim Fileformat As String = ConfigurationSettings.AppSettings("FileFormat").ToString()
        Dim GenDAL As New GeneralizedDAL
        Dim dsCheckData As DataSet
        Dim cr As New ErrorPage
        Try
            dsCheckData = GenDAL.GetDataSet("SELECT FlagAutoExport, AutoExportTime, AutoExportFileExtn, AutoExportOverwrite, includeZeroQtyTXTN, includePrevExportedTxtn FROM PSCHED")
            Dim IncZeroQty As Integer
            Dim IncPrevExportedtxtn As Integer
            IncZeroQty = Convert.ToInt32(dsCheckData.Tables(0).Rows(0)("includeZeroQtyTXTN"))
            IncPrevExportedtxtn = Convert.ToInt32(dsCheckData.Tables(0).Rows(0)("includePrevExportedTxtn"))
            If Not dsCheckData Is Nothing Then
                If dsCheckData.Tables.Count > 0 Then
                    If dsCheckData.Tables(0).Rows.Count > 0 Then
                        If Convert.ToString(dsCheckData.Tables(0).Rows(0)("FlagAutoExport")) = "1" Then
                            If Convert.ToDateTime(dsCheckData.Tables(0).Rows(0)("AutoExportTime")).ToString("HH:mm") = Now.ToString("HH:mm") Then
                                timer.Stop()
                                If Not System.Convert.ToString(dsCheckData.Tables(0).Rows(0)("AutoExportFileExtn")) = "" Then
                                    'dsCheckData = Nothing
                                    'CreateExport(Now.ToString("yyyyMMdd") + System.Convert.ToString(dsCheckData.Tables(0).Rows(0)("AutoExportFileExtn")))
                                    ' Dim Fileformat As String = ConfigurationSettings.AppSettings("FileFormat").ToString()
                                    CreateExport(Now.ToString(Fileformat) + System.Convert.ToString(dsCheckData.Tables(0).Rows(0)("AutoExportFileExtn")), IncZeroQty, IncPrevExportedtxtn)

                                Else
                                    dsCheckData = Nothing
                                    CreateExport(Now.ToString(Fileformat) + ".txt", IncZeroQty, IncPrevExportedtxtn)
                                End If
                                timer.Start()
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Dim errmsg As String
            cr.errorlog("AutoExport_CheckData", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
            timer.Start()
        End Try
    End Sub

    Private Shared Sub CreateExport(ByVal strFileName As String, ByVal IncZeroQty As Integer, ByVal IncPrevExportTxtn As Integer)
        Dim condition As String = "", expd As String = "", expt As String = "", qty As String = "", rec As String = ""
        Dim no As Integer = 0, expqty As Double
        Dim expdatetime As DateTime
        Dim cr As New ErrorPage
        Try
            Dim GenFun As New GeneralFunctions
            Dim GenDAL As New GeneralizedDAL
            'parameter contains the field names in which we want to add export transaction.
            'this is required to pass in InsertRecord srored procedure
            Dim parameter As String = "sentry,number,date,[time],pump,product,quantity,miles,milehour,[option],vkey,pkey,vehicle,"
            parameter = parameter & "vehtext,personnel,fname,lname,mid,licno,licst,partcd,merch,cost,surchrg,cpg,dept,"
            parameter = parameter & "subdept,tank,prev_miles,hours,prevhours,mpg,veh_type,pm,downloaded,errors,deptcode,"
            parameter = parameter & "vehscode,tankcode,sentcode,prodcode,acct_id,card_id,vacct_id,VIN"
            'First delete existing records from datadwn table
            DeleteRecord()
            'Call usp_tt_TranslateExportRec to get the records that fit the specified criteria from txtn,vehs,

            'Changes by Pritam for Zero Qty Txtn and Prev Exported Txtn
            Dim parcollection_ZeroQty(1) As SqlParameter
            Dim ParIncZeroQty = New SqlParameter("@IncZeroQty", SqlDbType.Int)
            ParIncZeroQty.Direction = ParameterDirection.Input
            ParIncZeroQty.Value = IncZeroQty
            parcollection_ZeroQty(0) = ParIncZeroQty

            Dim ParIncPrevExportedTxtn = New SqlParameter("@IncPrevExportedTxtn", SqlDbType.Int)
            ParIncPrevExportedTxtn.Direction = ParameterDirection.Input
            ParIncPrevExportedTxtn.Value = IncPrevExportTxtn
            parcollection_ZeroQty(1) = ParIncPrevExportedTxtn

            Dim sqlRdr As SqlDataReader
            sqlRdr = GenDAL.ExecuteStoredProcedureGetDataReader("usp_tt_TranslateExportRec_Auto", parcollection_ZeroQty)
            Dim parcollection(0) As SqlParameter
            Dim Par = New SqlParameter("@TXTN_ID", SqlDbType.NVarChar) '@TXTN_ID
            Par.Direction = ParameterDirection.Input
            'Get the rows selected for transaction from the result of stored procedure usp_tt_TranslateExportRec_Auto
            'one by one, format the fields and insert in the datadwn table.
            If (sqlRdr.HasRows = True) Then
                While (sqlRdr.Read())
                    'create string to be inserted in datadwn table
                    no = no + 1
                    rec = "'" & sqlRdr("sentry").ToString().Trim() & "','"

                    expd = sqlRdr("number").ToString().Trim()
                    If expd = "" Then
                        rec = rec & "00000','"
                    Else : rec = rec & expd & "','"
                    End If
                    If IsDBNull(sqlRdr("datetime").ToString()) = True Then
                        expdatetime = Now
                    Else
                        expdatetime = Convert.ToDateTime(sqlRdr("datetime").ToString().Trim())
                    End If
                    expd = expdatetime.Month.ToString().PadLeft(2, "0") & expdatetime.Day.ToString().PadLeft(2, "0") & expdatetime.Year.ToString()
                    expt = expdatetime.Hour.ToString().PadLeft(2, "0") & expdatetime.Minute.ToString().PadLeft(2, "0")
                    rec = rec & expd & "','" & expt & "','"
                    rec = rec & sqlRdr("pump").ToString().Trim() & "','"
                    rec = rec & sqlRdr("product").ToString().Trim() & "','"
                    'expqty = Convert.ToDouble(Val(sqlRdr("quantity").ToString()))
                    expqty = Double.Parse(sqlRdr("quantity").ToString())
                    qty = expqty.ToString("0.0")
                    'Commented by Jatin for Merietta Client as on 03-Jan-2013
                    If (qty.IndexOf(".") > 0) Then qty = qty.Remove(qty.IndexOf("."), 1)
                    qty = qty.PadLeft(6, "0")
                    rec = rec & qty & "','"

                    expd = sqlRdr("miles").ToString().Trim()
                    If expd = "" Then rec = rec & "0','" Else rec = rec & expd & "','"

                    'rec = rec & sqlRdr("miles").ToString().Trim() & "','"
                    expd = sqlRdr("milehours").ToString().Trim()
                    If expd = "" Then
                        rec = rec & "000000','"
                    Else : rec = rec & expd.PadLeft(6, "0") & "','"
                    End If
                    'rec = rec & sqlRdr("milehours").ToString().Trim() & "','"
                    expd = sqlRdr("option").ToString().Trim()
                    If expd = "" Then rec = rec & "None','" Else rec = rec & expd & "','"
                    'rec = rec & sqlRdr("option").ToString().Trim() & "','"
                    rec = rec & sqlRdr("vkey").ToString().Trim() & "','"
                    rec = rec & sqlRdr("pkey").ToString().Trim() & "','"
                    rec = rec & sqlRdr("vehicle").ToString().Trim() & "','"
                    rec = rec & sqlRdr("VEHTEXT").ToString().Trim().Replace("'", " ") & "','"
                    rec = rec & sqlRdr("personnel").ToString().Trim() & "','"
                    rec = rec & sqlRdr("fname").ToString().Trim() & "','"
                    rec = rec & sqlRdr("lname").ToString().Trim() & "','"
                    rec = rec & sqlRdr("mid").ToString().Trim() & "','"
                    rec = rec & sqlRdr("LICNO").ToString().Trim() & "','"
                    rec = rec & sqlRdr("LICST").ToString().Trim() & "','"
                    rec = rec & sqlRdr("partcd").ToString().Trim() & "','"
                    expd = sqlRdr("merch").ToString().Trim()
                    If expd = "" Then rec = rec & "None','" Else rec = rec & expd & "','"
                    'rec = rec & sqlRdr("merch").ToString().Trim() & "','"
                    If IsDBNull(sqlRdr("cost")) = True Then
                        expqty = 0
                    Else
                        expqty = Convert.ToDouble(Val(sqlRdr("cost")).ToString())
                    End If
                    expqty = IIf(Convert.IsDBNull(sqlRdr("cost")) = True, expqty = 0, Convert.ToDouble(Val(sqlRdr("cost")).ToString()))
                    qty = expqty.ToString().Trim()
                    If (qty.IndexOf(".") > 0) Then qty = qty.Remove(qty.IndexOf("."), 1)
                    qty = qty.PadLeft(6, "0")
                    rec = rec & qty & "','"
                    If IsDBNull(sqlRdr("surcharge")) = True Then
                        expqty = 0
                    Else
                        expqty = Convert.ToDouble(Val(sqlRdr("surcharge")).ToString())
                    End If
                    'expqty = IIf(Convert.IsDBNull(sqlRdr("surcharge")) = True, expqty = 0, Convert.ToDouble(Val(sqlRdr("surcharge")).ToString()))
                    qty = expqty.ToString().Trim()
                    If (qty.IndexOf(".") > 0) Then qty = qty.Remove(qty.IndexOf("."), 1)
                    qty = qty.PadLeft(15, "0")

                    rec = rec & qty & "','"
                    qty = sqlRdr("cpg").ToString().Trim()
                    If (qty.IndexOf(".") > 0) Then qty = qty.Remove(qty.IndexOf("."), 1)
                    qty = qty.PadRight(4, "0")

                    'rec = rec & sqlRdr("cpg").ToString().Trim() & "','"
                    rec = rec & qty.ToString().Trim() & "','"
                    rec = rec & sqlRdr("dept").ToString().Trim() & "','"
                    rec = rec & sqlRdr("SUBDEPT").ToString().Trim() & "','"
                    expd = sqlRdr("TANK").ToString().Trim()
                    If expd = "" Then rec = rec & "000','" Else rec = rec & expd & "','"
                    'rec = rec & sqlRdr("TANK").ToString().Trim() & "','"
                    expd = sqlRdr("prev_miles").ToString().Trim()
                    If expd = "" Then rec = rec & "0','" Else rec = rec & expd & "','"
                    'rec = rec & sqlRdr("prev_miles").ToString().Trim() & "','"
                    expd = sqlRdr("hours").ToString().Trim()
                    If expd = "" Then rec = rec & "0','" Else rec = rec & expd & "','"
                    'rec = rec & sqlRdr("hours").ToString().Trim() & "','"
                    expd = sqlRdr("prevhours").ToString().Trim()
                    If expd = "" Then rec = rec & "0','" Else rec = rec & expd & "','"
                    'rec = rec & sqlRdr("prevhours").ToString().Trim() & "','"
                    rec = rec & sqlRdr("mpg").ToString().Trim() & "','"
                    rec = rec & sqlRdr("VEHTYPE").ToString().Trim() & "','"
                    rec = rec & sqlRdr("pm").ToString().Trim() & "','"
                    expd = sqlRdr("downloaded").ToString().Trim()
                    If expd = "" Then rec = rec & "0','" Else rec = rec & expd & "','"
                    'rec = rec & sqlRdr("downloaded").ToString().Trim() & "','"
                    rec = rec & sqlRdr("errors").ToString().Trim() & "','"
                    rec = rec & sqlRdr("DEPTCODE").ToString().Trim() & "','"
                    rec = rec & sqlRdr("VEHSCODE").ToString().Trim() & "','"
                    rec = rec & sqlRdr("TANKCODE").ToString().Trim() & "','"
                    rec = rec & sqlRdr("sentcode").ToString().Trim() & "','"
                    rec = rec & sqlRdr("prodcode").ToString().Trim() & "','"
                    rec = rec & sqlRdr("acct_id").ToString().Trim() & "','"
                    rec = rec & sqlRdr("card_id").ToString().Trim() & "','"
                    rec = rec & sqlRdr("vacct_id").ToString().Trim() & "','"
                    'Added By varun Moota New Fields in Export Table.07/22/2010
                    rec = rec & sqlRdr("VIN").ToString().Trim() & "'"
                    'ALTER TABLE Datadwn 
                    'ALTER COLUMN card_id  nvarchar (7);
                    'Insert this record into datadwn table
                    InsertRecord("Datadwn", parameter, rec)
                    'Session("RecordCount") = no
                    Par = New SqlParameter("@TXTN_ID", SqlDbType.NVarChar) '@TXTN_ID
                    Par.Direction = ParameterDirection.Input
                    Par.Value = sqlRdr("TXTN_ID").ToString()
                    parcollection(0) = Par
                    'Update the txtn table and increase value of downloaded field by 1.
                    no = New GeneralizedDAL().ExecuteStoredProcedureGetInteger("usp_tt_Export_UpdateTXTN_Auto", parcollection)
                End While
                sqlRdr.Close()
                'Export records from datadwn to data file
                ExportData(strFileName, path)
                ''Update the txtn table and increase value of downloaded field by 1.
                'no = New GeneralizedDAL().ExecuteScalarGetInteger("usp_tt_Export_UpdateTXTN_Auto")
            End If

            'End If
        Catch ex As Exception
            'Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("AutoExport_btnCreateExport_Click", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Shared Sub DeleteRecord()
        'Delete all records from Datadwn table
        Dim GenDAL As New GeneralizedDAL
        Try
            Dim i As Integer = GenDAL.ExecuteNonQuery("Delete from Datadwn")
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("AutoExport_DeleteRecord", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Shared Sub InsertRecord(ByVal tablename As String, ByVal param As String, ByVal value As String)
        'Insert the transactions details to be exported in datadwn table
        Dim GenDAL As New GeneralizedDAL
        Try
            Dim parcollection(2) As SqlParameter
            'TableName
            Dim Par = New SqlParameter("@TableName", SqlDbType.NVarChar)
            Par.Direction = ParameterDirection.Input
            Par.Value = tablename
            parcollection(0) = Par
            'ParameterName
            Par = New SqlParameter("@ParameterName", SqlDbType.NVarChar)
            Par.Direction = ParameterDirection.Input
            Par.Value = param
            parcollection(1) = Par
            'Values
            Par = New SqlParameter("@Values", SqlDbType.NVarChar)
            Par.Direction = ParameterDirection.Input
            Par.Value = value
            parcollection(2) = Par

            Dim i As Integer = GenDAL.ExecuteStoredProcedureGetInteger("usp_tt_InsertRecords", parcollection)

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("AutoExport_InsertRecord", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Shared Sub ExportData(ByVal strFileName As String, ByVal strSVRFldrPath As String)
        'For exporting fields from datadwn to data file
        'Select the field names that are to be exported from translate table.
        Dim fields As String = ""
        Dim cr As New ErrorPage
        'Create file for export
        Dim sw As StreamWriter, fs As FileStream
        Dim GenDAL As New GeneralizedDAL
        Try
            'Added By Varun 
            Dim expoNum As Int32 = 1
            Dim parcollection(0) As SqlParameter
            'TableName
            Dim Par = New SqlParameter("@ExpoNum", SqlDbType.NVarChar)
            Par.Direction = ParameterDirection.Input
            Par.Value = expoNum
            parcollection(0) = Par
            Dim sqlRdr As SqlDataReader = GenDAL.ExecuteStoredProcedureGetDataReader("usp_tt_GetExportFields", parcollection)

            If (sqlRdr.HasRows = True) Then
                While (sqlRdr.Read())
                    If (sqlRdr("field_strg").ToString().Trim().ToUpper() = "CRLF") Then
                        fields = fields & "'" & sqlRdr("field_strg") & "' as newline,"
                    Else
                        fields = fields & sqlRdr("field_strg").ToString().Trim() & " as [" & sqlRdr("field_name").ToString().Trim() & "],"
                    End If
                End While
            End If
            fields = fields.Substring(0, fields.LastIndexOf(","))
            sqlRdr.Close()
            sqlRdr.Dispose()
            'Create file for export
            Dim fieldval As String = ""
            'Select the records from datadwn table in the format specified in translation table.
            'for this pass the above generated string to SP_SelectExportRec stored procedure.
            Par = New SqlParameter("@FieldList", SqlDbType.NVarChar)
            Par.Direction = ParameterDirection.Input
            Par.Value = fields
            parcollection(0) = Par
            sqlRdr = GenDAL.ExecuteStoredProcedureGetDataReader("usp_tt_SelectExportRec", parcollection)

            If (sqlRdr.HasRows = True) Then

                path = System.Configuration.ConfigurationManager.AppSettings("AutoExport").ToString()
                path += strFileName
                'path = strSVRFldrPath + strFileName 'HttpContext.Current.Server.MapPath("AutoExportTXTN\")
                'path = System.Configuration.ConfigurationManager.AppSettings("AutoExport").ToString() 'HttpContext.Current.Server.MapPath("AutoExportTXTN\Data")
                'path += strFileName
                fs = New FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)
                sw = New StreamWriter(fs)
                While (sqlRdr.Read())
                    'Enter the data selected from datadwn in the file specified in file name text box.
                    For i As Integer = 0 To sqlRdr.FieldCount - 1
                        'If newline character, add line break in the file.
                        If (sqlRdr(i).ToString().Trim().ToUpper() = "CRLF") Then
                            sw.WriteLine()
                        ElseIf (sqlRdr(i).ToString().Trim() = "") Then
                            'Format the field according to fill character in translate table.
                            fieldval = FormatField(sqlRdr.GetName(i), sqlRdr(i).ToString())
                            'insert the formatted string in datafile
                            sw.Write(fieldval)
                        Else
                            'Format the field according to fill character in translate table.
                            fieldval = FormatField(sqlRdr.GetName(i), sqlRdr(i).ToString())
                            'insert the formatted string in datafile
                            sw.Write(fieldval)
                        End If
                    Next

                End While
            End If
        Catch ex As Exception

            Dim errmsg As String
            cr.errorlog("AutoExport_ExportData", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        Finally
            sw.Flush()
            sw.Close()
            fs.Close()
        End Try
    End Sub

    Private Shared Function FormatField(ByVal fieldname As String, ByVal fieldval As String) As String
        'This function is used to fill the fieldvalue with fill character specified in translation table.
        'If no fill character is specified, fieldvalue will be padded with space.
        'Dim GenDAL As New GeneralizedDAL
        'Try
        '    Dim sdr As SqlDataReader
        '    sdr = GenDAL.GetDataReader("Select n_length,fillchar,startpos from translate where field_name='" & fieldname & "'")
        Dim sqlConn As SqlConnection = New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString())
        Try
            Dim sqlcmd As SqlCommand = New SqlCommand("Select n_length,fillchar,startpos from translate where field_name='" & fieldname & "'", sqlConn)
            sqlConn.Open()
            Dim sdr As SqlDataReader = sqlcmd.ExecuteReader()
            If (sdr.HasRows = True) Then
                sdr.Read()
                If ((sdr("startpos").ToString().Trim().ToUpper() = "LEFT") Or (sdr("startpos").ToString().Trim().ToUpper() = "ALL")) Then
                    Dim iLength As Int32 = Convert.ToInt32(Double.Parse(sdr("n_length")))
                    Dim pad As Char
                    pad = Convert.ToChar("0")
                    fieldval = fieldval.Trim.PadLeft(iLength, pad)
                ElseIf (sdr("startpos").ToString().Trim().ToUpper() = "RIGHT") Then
                    fieldval = fieldval.PadLeft(Convert.ToInt32(Val(sdr("n_length").ToString())), sdr("fillchar").ToString())
                End If
            End If
            sdr.Close()
            sqlcmd.Dispose()
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("AutoExport_FormatField", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        Finally
            If (sqlConn.State = ConnectionState.Open) Then sqlConn.Close()
        End Try
        Return fieldval
    End Function
End Class


