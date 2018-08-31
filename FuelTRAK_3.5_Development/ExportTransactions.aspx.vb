Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading

Partial Class ExportTransactions
    Inherits System.Web.UI.Page
    Dim str_Connection_string As String = IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString())
    Dim path As String = HttpContext.Current.Server.MapPath("ExportFiles\Data")
    Dim GenFun As New GeneralFunctions
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                txtStartDate.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtEndDate.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtStartTime.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtEndTime.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtStartDept.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtEndDept.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtStartDate.Attributes.Add("onkeyup", "KeyUpEvent_txtDate(event,'txtStartDate');")
                txtStartTime.Attributes.Add("onkeyup", "KeyUpEvent_txtTime(event,'txtStartTime');")
                txtEndDate.Attributes.Add("onkeyup", "KeyUpEvent_txtDate(event,'txtEndDate');")
                txtEndTime.Attributes.Add("onkeyup", "KeyUpEvent_txtTime(event,'txtEndTime');")

                If Not IsPostBack Then
                    'Image1.Visible = False
                    'ImgDiv.Visible = False
                    txtStartDate.Text = Format(Month(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Year(DateAdd(DateInterval.Day, -1, Today))
                    txtEndDate.Text = Format(Month(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Year(DateAdd(DateInterval.Day, -1, Today))
                    txtStartTime.Text = "00:00"
                    txtEndTime.Text = "23:59"

                    txtStartDate.Focus()
                End If
                Session("visited") = False
            End If

            'Added By varun Moota.06/02/2010
            If rdoSelect.SelectedValue = "1" Then
                Session("ExportNum") = "1"
            ElseIf rdoSelect.SelectedValue = "2" Then
                Session("ExportNum") = "2"
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportTransactions_DeleteRecord", ex)
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

    Protected Sub btnTransTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransTable.Click
        Try
            Response.Redirect("TranslationTable.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ExportTransactions.btnTransTable_Click", ex)
        End Try

    End Sub

    Private Sub DeleteRecord()
        'Delete all records from Datadwn table
        'Harshada Mutalik
        '2 Nov 07

        Dim sql As SqlConnection = New SqlConnection(str_Connection_string)
        Try
            sql.Open()
            Dim sqlDeleteCmd As SqlCommand = New SqlCommand("Delete from Datadwn", sql)
            sqlDeleteCmd.ExecuteNonQuery()
            sqlDeleteCmd.Dispose()
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportTransactions_DeleteRecord", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        Finally
            If sql.State = ConnectionState.Open Then sql.Close()
        End Try
    End Sub

    Private Sub ExportData()
        'For exporting fields from datadwn to data file
        'Harshada Mutalik
        '3 Nov 07
        'Select the field names that are to be exported from translate table.
        Dim sql As SqlConnection = New SqlConnection(str_Connection_string)
        Dim sqlCmd As SqlCommand
        Dim sqlreader As SqlDataReader
        Dim fields As String = ""
        'Create file for export
        'path = Session("FileLocationExport").ToString + Session("FileNameExport").ToString + "." + Session("FileTypeExport").ToString
        Dim fs As FileStream = New FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)
        Dim sw As StreamWriter = New StreamWriter(fs)
        Try
            'Added By Varun 
            Dim expoNum As Int32

            expoNum = Convert.ToInt32(Session("ExportNum"))
            sql.Open()
            sqlCmd = New SqlCommand("usp_tt_GetExportFields", sql)
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.Parameters.AddWithValue("@ExpoNum", expoNum)
            sqlreader = sqlCmd.ExecuteReader()
            If (sqlreader.HasRows = True) Then
                While (sqlreader.Read())
                    If (sqlreader("field_strg").ToString().Trim().ToUpper() = "CRLF") Then
                        fields = fields & "'" & sqlreader("field_strg") & "' as newline,"
                    Else
                        fields = fields & sqlreader("field_strg").ToString().Trim() & " as [" & sqlreader("field_name").ToString().Trim() & "],"
                    End If
                End While
            End If
            fields = fields.Substring(0, fields.LastIndexOf(","))
            sqlreader.Close()
            sqlCmd.Dispose()
            'Create file for export
            Dim i As Integer
            Dim fieldval As String = ""
            'Select the records from datadwn table in the format specified in translation table.
            'for this pass the above generated string to SP_SelectExportRec stored procedure.
            sqlCmd = New SqlCommand("usp_tt_SelectExportRec", sql)
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.Parameters.AddWithValue("@FieldList", fields)
            sqlreader = sqlCmd.ExecuteReader()
            If (sqlreader.HasRows = True) Then
                path = HttpContext.Current.Server.MapPath("ExportFiles\Data_")
                path += Session("User_name")
                Dim FileLocationExport() As Char = Session("FileLocationExport").ToString.ToCharArray
                'If Not FileLocationExport(FileLocationExport.Length - 1) = "\" Then
                '    Session("FileLocationExport") = Session("FileLocationExport") + "\"
                'End If
                'path = Session("FileLocationExport").ToString + Session("FileNameExport").ToString + "." + Session("FileTypeExport").ToString
                fs.Close()
                fs = New FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)
                sw = New StreamWriter(fs)
                Dim n_order As Integer = 1
                While (sqlreader.Read())
                    'Enter the data selected from datadwn in the file specified in file name text box.
                    For i = 0 To sqlreader.FieldCount - 1
                        'If newline character, add line break in the file.
                        If (sqlreader(i).ToString().Trim().ToUpper() = "CRLF") Then
                            sw.WriteLine()
                        ElseIf (sqlreader(i).ToString().Trim() = "") Then
                            'Format the field according to fill character in translate table.
                            '16 Nov 2007 'Harshada Mutalik.
                            fieldval = FormatField(sqlreader.GetName(i), sqlreader(i).ToString(), n_order)
                            'insert the formatted string in datafile
                            sw.Write(fieldval)
                        Else
                            'Format the field according to fill character in translate table.
                            '16 Nov 2007                            'Harshada Mutalik.
                            fieldval = FormatField(sqlreader.GetName(i), sqlreader(i).ToString(), n_order)
                            'insert the formatted string in datafile
                            sw.Write(fieldval)
                        End If
                        n_order = n_order + 1
                    Next
                    n_order = 1
                End While
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportTransactions_ExportData", ex)
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
            If sql.State = ConnectionState.Open Then sql.Close()
        End Try
    End Sub

    Private Function FormatField(ByVal fieldname As String, ByVal fieldval As String, ByVal n As Integer) As String
        'This function is used to fill the fieldvalue with fill character specified in translation table.
        'If no fill character is specified, fieldvalue will be padded with space.
        '16 Nov 07        'Harshada Mutalik
        Dim sqlConn As SqlConnection = New SqlConnection(str_Connection_string)
        Try
            Dim sqlcmd As SqlCommand = New SqlCommand("Select n_length,fillchar,startpos from translate where field_name='" & fieldname & "' and n_order = '" & n & "'", sqlConn)
            sqlConn.Open()
            Dim sdr As SqlDataReader = sqlcmd.ExecuteReader()
            Dim pad As Char = " "
            If (sdr.HasRows) Then
                sdr.Read()
                If ((sdr("startpos").ToString().Trim().ToUpper() = "LEFT") Or (sdr("startpos").ToString().Trim().ToUpper() = "ALL")) Then
                    'fieldval = fieldval.PadLeft(Convert.ToInt32(Val(sdr("n_length")).ToString()), sdr("fillchar").ToString())
                    'Added By Varun Moota.
                    Dim iLength As Int32 = Convert.ToInt32(Double.Parse(sdr("n_length")))
                    'pad = Convert.ToChar("0")
                    fieldval = fieldval.PadRight(Convert.ToInt32(Val(sdr("n_length").ToString())), sdr("fillchar").ToString())
                    fieldval = fieldval.Trim.PadRight(iLength, pad)
                ElseIf (sdr("startpos").ToString().Trim().ToUpper() = "RIGHT") Then
                    'If (fieldname <> "VEHICLE") And (fieldname <> "PERSONNEL") Then
                    '    pad = Convert.ToChar("0")
                    'End If
                    'If (fieldname = "NUMBER") Then
                    '    If (fieldval = "585660") Then
                    '        Dim check As String = "check"
                    '    Else
                    '        Dim check As String = "No check"
                    '    End If
                    'End If

                    'If (fieldname = "VIN") Then
                    '    Dim check As String = fieldval
                    'Else
                    '    Dim check As String = "No check"
                    'End If

                    Dim iLength1 As Int32 = Convert.ToInt32(Double.Parse(sdr("n_length")))
                    fieldval = fieldval.PadLeft(Convert.ToInt32(Val(sdr("n_length").ToString())), sdr("fillchar").ToString())
                    fieldval = fieldval.Trim.PadLeft(iLength1, pad)
                End If
            End If
            sdr.Close()
            sqlcmd.Dispose()
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportTransactions_FormatField", ex)
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

    Private Sub InsertRecord(ByVal tablename As String, ByVal param As String, ByVal value As String)
        'Insert the transactions details to be exported in datadwn table
        'HM    '2 Nov 07
        Dim sql As SqlConnection = New SqlConnection(str_Connection_string)
        Dim sqlInsertCmd As SqlCommand = New SqlCommand()
        Try
            sql.Open()
            sqlInsertCmd.Connection = sql
            sqlInsertCmd.CommandText = "usp_tt_InsertRecords"
            sqlInsertCmd.CommandType = CommandType.StoredProcedure
            sqlInsertCmd.Parameters.AddWithValue("@TableName", tablename)
            sqlInsertCmd.Parameters.AddWithValue("@ParameterName", param)
            sqlInsertCmd.Parameters.AddWithValue("@Values", value)
            sqlInsertCmd.ExecuteNonQuery()
            sqlInsertCmd.Dispose()
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportTransactions_InsertRecord", ex)
            cr.errorlog("tablename: " + tablename, ex)
            cr.errorlog("param: " + param, ex)
            cr.errorlog("value: " + value, ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        Finally
            If sql.State = ConnectionState.Open Then sql.Close()
        End Try

    End Sub

    Protected Sub btnCreateExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateExport.Click
        Try
            'ImgDiv.Visible = True
            'CreateExport()
            'ImgDiv.Visible = False

            'By Soham Gangavane July 18, 2017
            'If Not txtFileLocation.Text = "" Then
            Session("FileLocationExport") = ""
            'Session("FileLocationExport") = txtFileLocation.Text
            'If Not txtFileName.Text = "" Then
            'If Directory.Exists(txtFileLocation.Text) Then
            'Session("FileNameExport") = txtFileName.Text
            Session("FileNameExport") = ""
            'If Not ddlFileType.SelectedValue = "select" Then
            '    Session("FileTypeExport") = ddlFileType.Text
            CreateExport()
            'Else
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please select File Extension.');</script>")
            'End If
            'Else
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please enter valid file location.');</script>")
            'End If
            'Else
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please enter File Name.');</script>")
            'End If
            'Else
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please enter File location.');</script>")
            'End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ExportTransactions.btnCreateExport_Click", ex)
        End Try
    End Sub

    Private Sub CreateExport()
        Dim sqlConn As SqlConnection = New SqlConnection(str_Connection_string)
        Dim sqlCmd As SqlCommand = New SqlCommand()
        Dim sqlRdr As SqlDataReader
        Dim condition As String = ""
        Dim no As Integer = 0
        Dim startdate As DateTime
        Dim enddate As DateTime
        Dim rec As String = ""
        Dim expdatetime As DateTime
        Dim expd As String
        Dim expt As String
        Dim qty As String
        Dim expqty As Double
        Try
            Dim GenFun As New GeneralFunctions
            If DateDiff(DateInterval.Day, GenFun.ConvertDate(txtStartDate.Text.Trim()), GenFun.ConvertDate(txtEndDate.Text.Trim())) >= 0 Then
                'parameter contains the field names in which we want to add export transaction.
                'this is required to pass in InsertRecord srored procedure
                Dim parameter As String = "sentry,number,date,[time],pump,product,quantity,miles,milehour,[option],vkey,pkey,vehicle,"
                parameter = parameter & "vehtext,personnel,fname,lname,mid,licno,licst,partcd,merch,cost,surchrg,cpg,dept,"
                parameter = parameter & "subdept,tank,prev_miles,hours,prevhours,mpg,veh_type,pm,downloaded,errors,deptcode,"
                parameter = parameter & "vehscode,tankcode,sentcode,prodcode,acct_id,card_id,vacct_id,VIN,Hourmeter"
                'Added By Varun Moota since New Fields added in Export Table.07/22/2010
                'parameter = parameter & "VIN"
                sqlConn.Open()
                sqlCmd.Connection = sqlConn
                'First delete existing records from datadwn table
                DeleteRecord()
                'Call usp_tt_TranslateExportRec to get the records that fit the specified criteria from txtn,vehs,
                'pers,product,tank,sentry Tables
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "usp_tt_TranslateExportRec"
                'pass parameters to srored procedure.
                If txtStartDate.Text.Trim() = "" And txtEndDate.Text.Trim() <> "" Then
                    enddate = GenFun.ConvertDate(txtEndDate.Text.Trim()) & " " & txtEndTime.Text
                    enddate = Format(enddate, "MM/dd/yyyy HH:mm")
                    sqlCmd.Parameters.AddWithValue("@StartDate", "")
                    sqlCmd.Parameters.AddWithValue("@EndDate", enddate)
                ElseIf txtStartDate.Text.Trim() <> "" And txtEndDate.Text.Trim() <> "" Then
                    startdate = GenFun.ConvertDate(txtStartDate.Text.Trim()) & " " & txtStartTime.Text
                    enddate = GenFun.ConvertDate(txtEndDate.Text.Trim()) & " " & txtEndTime.Text
                    sqlCmd.Parameters.AddWithValue("@StartDate", startdate)
                    sqlCmd.Parameters.AddWithValue("@EndDate", enddate)
                End If
                If txtStartDept.Text.Trim() = "" And txtEndDept.Text.Trim() <> "" Then
                    condition = " and VEHS.DEPT<=" & txtEndDept.Text.Trim()
                ElseIf txtStartDept.Text.Trim() <> "" And txtEndDept.Text.Trim() <> "" Then
                    condition = " and VEHS.DEPT between " & txtStartDept.Text.Trim() & " AND " & txtEndDept.Text.Trim()
                ElseIf txtStartDept.Text <> "" And txtEndDept.Text = "" Then
                    condition = " and VEHS.DEPT>=" & txtStartDept.Text.Trim()
                End If
                'Changed to add Export1 is null in condition
                '29 Nov 07            'Harshada Mutalik
                'Commenetd By Varun Moota ,Since we need Zero QTY TXTN's.07/23/2010
                'If chkPrevTxtns.Checked = True Then condition += " and (downloaded >= 0 or downloaded is null )" Else condition += " and (downloaded = 0 or downloaded is null) and Export1 is null"
                'condition += " and (txtn.quantity > 0)"
                'Added by Pritam, Since we need Zero QTY TXTN's. 03/17/2014
                If chkIncZeroQty.Checked = True Then condition += " and (QUANTITY >= 0 )" Else condition += " and (QUANTITY > 0 )"
                If chkPrevTxtns.Checked = True Then condition += " and (downloaded >= 0 or downloaded is null )" Else condition += " and (downloaded >= 0 or downloaded is null)"
                'condition += " and (txtn.quantity > 0)"
                Session("StartDT") = startdate
                Session("EndDT") = enddate
                Session("StrCondition") = condition
                If rdoSelect.SelectedValue = "1" Then
                    Session("ExportNum") = "1"
                ElseIf rdoSelect.SelectedValue = "2" Then
                    Session("ExportNum") = "2"
                End If
                sqlCmd.Parameters.AddWithValue("@Condition", condition)
                sqlRdr = sqlCmd.ExecuteReader()
                'Get the rows selected for transaction from the result of stored procedure SP_TranslateExportRec
                'one by one, format the fields and insert in the datadwn table.
                If (sqlRdr.HasRows = True) Then
                    While (sqlRdr.Read())
                        'create string to be inserted in datadwn table
                        no = no + 1
                        rec = "'" & sqlRdr("sentry").ToString().Trim() & "','"
                        rec = rec & sqlRdr("number").ToString().Trim() & "','"
                        'expdatetime = Convert.ToDateTime(sqlRdr("datetime").ToString().Trim())
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
                        ' Dim iLength As Int32 = Convert.ToInt32(Double.Parse(sqlRdr("quantity").ToString()))
                        expqty = Double.Parse(sqlRdr("quantity").ToString())
                        qty = expqty.ToString("0.0")
                        'Commented by Jatin for Merietta Client as on 03-Jan-2013
                        If (qty.IndexOf(".") > 0) Then qty = qty.Remove(qty.IndexOf("."), 1)
                        qty = qty.PadLeft(6, "0")
                        rec = rec & qty & "','"
                         rec = rec & sqlRdr("miles").ToString().Trim() & "','"

                        'By Soham Gangavane 2018-02-07
                        'For ELPASO Removed milehours and Added Two seperate fields as 
                        '"Meter 1" - Represents the assets (Identity) odometer
                        '"Meter 2" - Represents the assets (Identity) Hours

                        rec = rec & sqlRdr("milehours").ToString().Trim() & "','" ' This is for optional use
                        rec = rec & sqlRdr("option").ToString().Trim() & "','"
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
                        rec = rec & sqlRdr("merch").ToString().Trim() & "','"
                        If IsDBNull(sqlRdr("cost")) = True Then
                            expqty = 0
                        Else
                            expqty = Convert.ToDouble(Val(sqlRdr("cost")).ToString())
                        End If
                        expqty = IIf(Convert.IsDBNull(sqlRdr("cost")) = True, expqty = 0, Convert.ToDouble(Val(sqlRdr("cost")).ToString()))
                        qty = expqty.ToString("00.00").Trim()
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
                        rec = rec & sqlRdr("TANK").ToString().Trim() & "','"
                        rec = rec & sqlRdr("prev_miles").ToString().Trim() & "','"
                        rec = rec & sqlRdr("hours").ToString().Trim() & "','"
                        rec = rec & sqlRdr("prevhours").ToString().Trim() & "','"
                        rec = rec & sqlRdr("mpg").ToString().Trim() & "','"
                        rec = rec & sqlRdr("VEHTYPE").ToString().Trim() & "','"
                        rec = rec & sqlRdr("pm").ToString().Trim() & "','"
                        rec = rec & sqlRdr("downloaded").ToString().Trim() & "','"
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
                        rec = rec & sqlRdr("VIN").ToString().Trim() & "','"

                        'By Soham Gangavane New Fields in Export Table. June 18, 2018
                        rec = rec & sqlRdr("Hourmeter").ToString().Trim() & "'"
                        
                        'Insert this record into datadwn table
                        InsertRecord("Datadwn", parameter, rec)
                        Session("RecordCount") = no
                    End While
                    sqlRdr.Close()
                    sqlCmd.Dispose()
                    'Export records from datadwn to data file
                    ExportData()
                    'Update the txtn table and increase value of downloaded field by 1.
                    'Harshada Mutalik                '14 Nov 07
                    Dim query As String = ""
                    'Commented By Varun Since, we need Zero QTY TXTN's as Per OGNE Request.07/23/2010
                    'If (chkPrevTxtns.Checked = True) Then query = query & " and (downloaded>=0 or downloaded is null ) and quantity > 0" Else query = query & " and (downloaded = 0 or downloaded is null) and quantity > 0 and Export1 is null"
                    'Added by Pritam Since, we need to include Zero QTY TXTN's. 03/17/2014
                    'If (chkIncZeroQty.Checked = True) Then query = query & " and (downloaded>=0 or downloaded is null ) and quantity > 0" Else query = query & " and (downloaded = 0 or downloaded is null) and quantity > 0 and Export1 is null"
                    If (chkPrevTxtns.Checked = True) Then query = query & " and (downloaded>=0 or downloaded is null )" Else query = query & " and (downloaded>=0 or downloaded is null) "
                    sqlCmd = New SqlCommand()
                    sqlCmd.Connection = sqlConn
                    sqlCmd.CommandText = "usp_tt_Export_UpdateTXTN"
                    sqlCmd.CommandType = CommandType.StoredProcedure
                    sqlCmd.Parameters.AddWithValue("@SDate", Session("StartDT"))
                    sqlCmd.Parameters.AddWithValue("@EDate", Session("EndDT"))
                    'Added By Varun To Test the Exp File
                    ' ''Dim strTest1 As String = "2006-04-20 14:54:00.000"
                    ' ''Dim strTest2 As String = "2009-04-20 14:54:00.000"
                    ' ''sqlCmd.Parameters.AddWithValue("@SDate", strTest1)
                    ' ''sqlCmd.Parameters.AddWithValue("@EDate", strTest2)
                    sqlCmd.Parameters.AddWithValue("@SDept", txtStartDept.Text.Trim())
                    sqlCmd.Parameters.AddWithValue("@EDept", txtEndDept.Text.Trim())
                    sqlCmd.Parameters.AddWithValue("@Condition", query)
                    no = sqlCmd.ExecuteNonQuery()
                    sqlCmd.Dispose()
                    Dim ExportDate As String, LastExportDate As String
                    ExportDate = getExportDates(False, True)
                    LastExportDate = getExportDates(True, False)
                    Session("LastDate") = LastExportDate
                    Session("ExportDate") = ExportDate
                    If (no > 0) Then 'Export the file            'Harshada Mutalik            '12 Nov 2007
                        Response.Redirect("ExportSummary.aspx", False)
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records Found.');</script>")
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('From date should be less than To date.');</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportTransactions_btnCreateExport_Click", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        Finally
            If sqlConn.State = ConnectionState.Open Then sqlConn.Close()
        End Try
    End Sub

    Public Function getExportDates(ByVal oldDate As Boolean, ByVal newDate As Boolean)
        Dim sqlConn As SqlConnection = New SqlConnection(str_Connection_string)
        Dim ExportDate As String = ""
        Try
            sqlConn.Open()
            Dim sqlcmd As SqlCommand = New SqlCommand()
            If (oldDate = True) Then
                sqlcmd = New SqlCommand("Select min(Export1) as ExportDate from txtn", sqlConn)
            ElseIf (newDate = True) Then
                sqlcmd = New SqlCommand("Select max(Export1) as ExportDate from txtn", sqlConn)
            End If

            Dim sdr As SqlDataReader
            sdr = sqlcmd.ExecuteReader()
            If (sdr.HasRows) Then
                sdr.Read()
                ExportDate = sdr("ExportDate").ToString()
                sdr.Close()
                sqlcmd.Dispose()
            End If
            Return ExportDate
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportTransactions_getExportDates", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
            ''Response.Redirect("Error1.aspx?Err=" + errmsg + "ExportTransactions_getExportDates", False)
        Finally
            If sqlConn.State = ConnectionState.Open Then sqlConn.Close()
        End Try
    End Function

    Protected Sub btnJustTrnslate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJustTrnslate.Click
        'Just translate existing transaction without changing value of downloaded field from txtn table.
        'Harshada Mutalik
        '12 Nov 2007
        Dim sqlConn As SqlConnection = New SqlConnection(str_Connection_string)
        Dim no As Integer
        no = 0
        Try
            sqlConn.Open()
            Dim sqlCmd As SqlCommand = New SqlCommand("Select count(*) from Datadwn", sqlConn)
            no = sqlCmd.ExecuteScalar
            If (no > 0) Then
                ExportData()
            End If
            Dim ExportDate As String
            Dim LastExportDate As String
            ExportDate = getExportDates(False, True)
            LastExportDate = getExportDates(True, False)

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportTransactions_btnCreateExport_Click", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If

            'Response.Redirect("Error1.aspx?Err=" + errmsg + "ExportTransactions_btnCreateExport_Click", False)
        Finally
            If sqlConn.State = ConnectionState.Open Then sqlConn.Close()

        End Try
        'Export the file.
        If (no > 0) Then
            Dim f As FileInfo = New FileInfo(path)
            Response.Redirect("ExportFile.aspx?file=" & f.Name, False)
            'Page.ClientScript.RegisterStartupScript(Me.GetType(),"javascript", "<script>window.open('ExportFile.aspx?file=data','popup','width=600px,height=500px,top=100,left=310,scrollbars=yes');</script>")
        Else
            'lblResult.Text = "No transactions To Translate"
        End If
    End Sub

    Protected Sub btnUndoExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndoExport.Click
        Dim RedirectUrl As String = ""
        Dim sqlConn As SqlConnection = New SqlConnection(str_Connection_string)
        Dim no As Integer
        no = 0
        Try
            Dim GenFun As New GeneralFunctions
            If DateDiff(DateInterval.Day, GenFun.ConvertDate(txtStartDate.Text.Trim()), GenFun.ConvertDate(txtEndDate.Text.Trim())) >= 0 Then
                sqlConn.Open()
                Dim sqlCmd As SqlCommand = New SqlCommand("Select count(*) from Datadwn", sqlConn)
                no = sqlCmd.ExecuteScalar
                If (no > 0) Then
                    Dim ExportDate As String
                    ExportDate = getExportDates(False, True)
                    If ExportDate <> "" Then
                        Dim startdate, enddate As DateTime
                        enddate = GenFun.ConvertDate(txtEndDate.Text) & " " & txtEndTime.Text
                        startdate = GenFun.ConvertDate(txtStartDate.Text) & " " & txtStartTime.Text

                        RedirectUrl = "UndoExport.aspx?RecordCount=" & no & "&ExportDate=" & ExportDate
                        RedirectUrl = RedirectUrl & "&StartDate=" & startdate.ToString().Trim() & "&EndDate=" & enddate.ToString().Trim()
                        If txtStartDept.Text.Trim() = "" And txtEndDept.Text.Trim() <> "" Then
                            RedirectUrl = RedirectUrl & "&StartDept=&EndDept=" & txtEndDept.Text.Trim()
                        ElseIf txtStartDept.Text.Trim() <> "" And txtEndDept.Text.Trim() <> "" Then
                            RedirectUrl = RedirectUrl & "&EndDept=" & txtEndDept.Text.Trim() & "&StartDept=" & txtStartDept.Text.Trim()
                        ElseIf txtStartDept.Text <> "" And txtEndDept.Text = "" Then
                            RedirectUrl = RedirectUrl & "&StartDept=" & txtStartDept.Text.Trim() & "&EndDept="
                        End If
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>window.open('" & RedirectUrl & "','popup','width=550px,height=130px,top=300,left=45,scrollbars=no');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Transactions to Undo');</script>")
                    End If
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('From date should be less than To date.');</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("ExportTransactions_btnCreateExport_Click", ex)
            If (ex.Message.Contains(";")) Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            ElseIf ex.Message.Contains(vbCrLf) Then
                errmsg = ex.Message.Replace(vbCrLf.ToString(), "")
            Else
                errmsg = ex.Message.ToString()
            End If
        Finally
            If sqlConn.State = ConnectionState.Open Then sqlConn.Close()
        End Try

    End Sub

    Protected Sub rdoSelect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSelect.SelectedIndexChanged
        Try
            If rdoSelect.SelectedValue Then

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("ExportTransactions.rdoSelect_SelectedIndexChanged", ex)
        End Try

    End Sub
End Class
