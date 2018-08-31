Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class MeterReadings_New_Edit
    Inherits System.Web.UI.Page
    'Modify code by Jatin Kshirsagar as on 01 Sept 2008
    Dim MeterTcount As Integer
    Dim GenFun As New GeneralFunctions

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("MeterReadings.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReadings_New_Edit.btnCancel_Click", ex)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dal = New GeneralizedDAL()
        Dim ds = New DataSet()

        ' check for session is null/not
        If Session("User_name") Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
        Else
            Try
                txtHose.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtReading.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtDate.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtTime.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txtHose.Attributes.Add("onkeyup", "KeyUpEvent_txtHose(event);")
                txtReading.Attributes.Add("onkeyup", "KeyUpEvent_Reading(event);")
                txtTime.Attributes.Add("onkeyup", "KeyUpEvent_txtTime(event,'txtTime');")
                txtDate.Attributes.Add("onkeyup", "KeyUpEvent_txtDate(event,'txtDate');")

                txtDate.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                'txtDate.Attributes.Add("onkeyup", "KeyUpEvent_txtDate(event,'txtDate');")

                'txtDate.Text = Format(Month(DateAdd(DateInterval.Day, 0, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Year(DateAdd(DateInterval.Day, 0, Today))
                If Not Session("visited") Or Not IsPostBack Then
                    If Not IsPostBack Then
                        'Fill Tank (DDLstTank) Modify code by Jatin Kshirsagar as on 01 Sept 2008
                        DDLstSentry.Items.Clear()
                        ds = New DataSet()
                        ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_MeterPopulateSENTRY")
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                DDLstSentry.DataSource = ds.Tables(0)
                                DDLstSentry.DataTextField = "SentryNoName"
                                DDLstSentry.DataValueField = "NUMBER"
                                DDLstSentry.DataBind()
                            End If
                        End If
                    End If

                    If (Request.QueryString.Count = 1 And Not IsPostBack) Then
                        btnOk.Visible = False
                        lblNew_Edit.Text = "Edit Meter Information"
                        btnDelete.Visible = True

                        ds = New DataSet
                        ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_MeterRecords")
                        ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("RECORD")}
                        Dim cnt As Long = 0
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                hdfRecord.Value = Request.QueryString("Record").ToString()
                                While Not (ds.Tables(0).Rows(cnt).Item("RECORD").ToString() = hdfRecord.Value)
                                    cnt = cnt + 1
                                End While
                                Session("MeterDS") = ds
                                Session("currentrecord") = cnt
                                Session("MeterTCnt") = ds.Tables(1).Rows(0)(0).ToString()
                                MeterTcount = ds.Tables(1).Rows(0)(0).ToString()
                                fillRecordsForEdit(cnt)
                            End If
                            If ds.Tables(1).Rows.Count > 0 Then
                                Session("MeterTCnt") = ds.Tables(1).Rows(0)(0).ToString()
                                MeterTcount = ds.Tables(1).Rows(0)(0).ToString()
                                lblof.Text = cnt + 1 & " " & "of" & " " & MeterTcount
                            End If
                        End If
                        btnFirst.Visible = True
                        btnLast.Visible = True
                        btnprevious.Visible = True
                        btnNext.Visible = True
                        lblof.Visible = True
                        If cnt + 1 = 1 Then
                            lblof.Visible = True
                            btnprevious.Enabled = False
                            btnNext.Enabled = True
                            btnFirst.Enabled = False
                            btnLast.Enabled = True
                        ElseIf cnt = MeterTcount Then
                            lblof.Visible = True
                            btnprevious.Enabled = True
                            btnNext.Enabled = False
                            btnFirst.Enabled = True
                            btnLast.Enabled = False
                        ElseIf cnt < MeterTcount Then
                            lblof.Visible = True
                            btnprevious.Enabled = True
                            btnNext.Enabled = True
                            btnFirst.Enabled = True
                            btnLast.Enabled = True
                        End If
                    ElseIf (Not IsPostBack) Then
                        lblNew_Edit.Text = "New Meter Information"
                        btnFirst.Visible = False
                        btnLast.Visible = False
                        btnNext.Visible = False
                        btnprevious.Visible = False
                        lblof.Visible = False
                    End If
                End If
            Catch ex As Exception
                Dim cr As New ErrorPage
                Dim errmsg As String
                cr.errorlog("MeterReading_Edit_Page_Load", ex)

                If ex.Message.Contains(";") Then
                    errmsg = ex.Message.ToString()
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
                Else
                    errmsg = ex.Message.ToString()
                End If
            End Try
        End If
    End Sub

    Private Sub fillRecordsForEdit(ByVal cnt As Integer)
        Dim ds = New DataSet()
        ds = CType(Session("MeterDS"), DataSet)
        MeterTcount = Session("MeterTCnt")
        Try
            DDLstSentry.SelectedValue = ds.Tables(0).Rows(cnt).Item("SENTRY").ToString()
        Catch ex As Exception
            DDLstSentry.SelectedIndex = 0
        End Try

        txtHose.Text = ds.Tables(0).Rows(cnt).Item("PUMP").ToString() 'sqlReader(1).ToString()
        TxtHoseDDl.Text = ds.Tables(0).Rows(cnt).Item("PUMP").ToString() 'sqlReader(1).ToString()
        txtReading.Text = ds.Tables(0).Rows(cnt).Item("METERREAD").ToString() 'sqlReader(2).ToString()
        txtTime.Text = Format(Convert.ToDateTime(ds.Tables(0).Rows(cnt).Item("DATETIME")), "hh:mm") 'Format(Convert.ToDateTime(sqlReader(3)), "hh:mm")
        txtDate.Text = Format(Convert.ToDateTime(ds.Tables(0).Rows(cnt).Item("DATETIME")), "MM/dd/yyyy") 'Format(Convert.ToDateTime(sqlReader(3)), "MM/dd/yyyy")
        Session("currentrecord") = cnt
    End Sub

    Protected Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            ds = CType(Session("MeterDS"), DataSet)
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = 0

            If (cnt < ds.Tables(0).Rows.Count) Then
                fillRecordsForEdit(cnt)
            End If
            Session("currentrecord") = "0"
            lblof.Text = "1 of " & Session("MeterTCnt")
            btnprevious.Enabled = False
            btnFirst.Enabled = False
            btnNext.Enabled = True
            btnLast.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("MeterNewEdit_btnFirst_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            ds = CType(Session("MeterDS"), DataSet)
            cnt = Convert.ToInt32(Session("MeterTCnt").ToString() - 1)
            If (cnt < ds.Tables(0).Rows.Count) Then
                fillRecordsForEdit(cnt)
            End If
            lblof.Text = Session("MeterTCnt").ToString() & " of " & Session("MeterTCnt").ToString()
            Session("currentrecord") = cnt

            btnNext.Enabled = False
            btnLast.Enabled = False

            btnprevious.Enabled = True
            btnFirst.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("MeterNewEdit_btnLast_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            ds = CType(Session("MeterDS"), DataSet)
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt + 1

            If (cnt < ds.Tables(0).Rows.Count) Then
                fillRecordsForEdit(cnt)
                Session("currentrecord") = cnt
            End If

            lblof.Text = cnt + 1 & " of " & Session("MeterTCnt").ToString()

            If Not btnFirst.Enabled Then
                btnFirst.Enabled = True
                btnprevious.Enabled = True
            End If

            If (cnt + 1 = Convert.ToInt32(Session("MeterTCnt"))) Then
                btnLast.Enabled = False
                btnNext.Enabled = False
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("MeterEdit_btnNext_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprevious.Click
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt - 1
            ds = CType(Session("MeterDS"), DataSet)
            If (cnt < ds.Tables(0).Rows.Count And cnt >= 0) Then
                fillRecordsForEdit(cnt)
                Session("currentrecord") = cnt
            End If
            lblof.Text = (cnt + 1) & " of " & Session("MeterTCnt")

            If Not btnLast.Enabled Then
                btnLast.Enabled = True
                btnNext.Enabled = True
            End If
            If (cnt + 1 = 1) Then
                btnFirst.Enabled = False
                btnprevious.Enabled = False
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankInventory_btnprevious_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            Dim parcollection(6) As SqlParameter

            Dim ParRECORD = New SqlParameter("@RECORD", SqlDbType.Int)
            Dim ParSENTRY = New SqlParameter("@SENTRY", SqlDbType.VarChar, 3)
            Dim ParPUMP = New SqlParameter("@PUMP", SqlDbType.VarChar, 1)
            Dim ParDATETIME = New SqlParameter("@DATETIME", SqlDbType.DateTime)
            Dim ParNEWUPDT = New SqlParameter("@NEWUPDT", SqlDbType.VarChar, 5)
            Dim ParMETERREAD = New SqlParameter("@METERREAD", SqlDbType.VarChar, 5)
            Dim ParAddEdit = New SqlParameter("@AddEdit", SqlDbType.VarChar, 5)

            ParRECORD.Direction = ParameterDirection.Input
            ParSENTRY.Direction = ParameterDirection.Input
            ParPUMP.Direction = ParameterDirection.Input
            ParNEWUPDT.Direction = ParameterDirection.Input
            ParMETERREAD.Direction = ParameterDirection.Input
            ParDATETIME.Direction = ParameterDirection.Input
            ParAddEdit.Direction = ParameterDirection.Input

            If (lblNew_Edit.Text = "Edit Meter Information") Then
                ParAddEdit.Value = "Edit"
                ParRECORD.Value = Convert.ToInt32(hdfRecord.Value)
                ParNEWUPDT.value = 2
            Else
                ParAddEdit.Value = "ADD"
                ParRECORD.Value = 0
                ParNEWUPDT.value = 1
            End If
            ParSENTRY.Value = DDLstSentry.SelectedValue
            ParPUMP.value = TxtHoseDDl.Text.Trim()
            ParMETERREAD.value = txtReading.Text.Trim()
            If txtDate.Text.Trim <> "" Then
                ParDATETIME.value = GenFun.ConvertDate(txtDate.Text.Trim()) & " " & txtTime.Text.Trim()
            Else
                ParDATETIME.value = System.DBNull.Value
            End If

            parcollection(0) = ParRECORD
            parcollection(1) = ParSENTRY
            parcollection(2) = ParPUMP
            parcollection(3) = ParDATETIME
            parcollection(4) = ParMETERREAD
            parcollection(5) = ParNEWUPDT
            parcollection(6) = ParAddEdit
            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_MeterUpdateInsert", parcollection)
            If blnFlag = True Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record saved successfully.');location.href='MeterReadings.aspx';</script>")
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record saved successfully.');location.href='MeterReadings_New_Edit.aspx';</script>")
                cleartext()
            End If
            'Response.Redirect("MeterReadings.aspx")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReading_Edit_btnOk_Click", ex)
        End Try
    End Sub
    Private Sub cleartext()
        Try
            txtDate.Text = ""
            txtHose.Text = ""
            TxtHoseDDl.Text = ""
            txtReading.Text = ""
            txtTime.Text = ""
            DDLstSentry.SelectedIndex = 0
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReading_Edit.cleartext()", ex)
        End Try
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParRECORDID = New SqlParameter("@RECORDID", SqlDbType.Int)
            ParRECORDID.Direction = ParameterDirection.Input
            ParRECORDID.Value = Convert.ToInt32(Val(hdfRecord.Value.Trim()))
            parcollection(0) = ParRECORDID
            Dim blnflag As Boolean
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_MeterDelete", parcollection)
            If blnflag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Meter deleted sucessfully.')</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("MeterSearch_deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            btnOk_Click(sender, Nothing)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReading_Edit.btnSave_Click", ex)
        End Try

    End Sub
End Class
