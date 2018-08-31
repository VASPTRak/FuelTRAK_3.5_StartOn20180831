Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.Collections.Generic

Partial Class PollSetup
    Inherits System.Web.UI.Page
    Dim GenFun As New GeneralFunctions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                If (Not IsPostBack) Then
                    FillGrids()
                    ' rdoOverwriteYes.Checked = True

                    chkVehLst.Attributes.Add("onclick", "disableListItems('chkVehLst', '0', '2')")
                    chkPersonnelLst.Attributes.Add("onclick", "disableListItems('chkPersonnelLst', '0', '2')")
                    RDBPollLst.Attributes.Add("onclick", "disableRadioItems('RDBPollLst')")
                    txtPollfromdt.Attributes.Add("onkeyup", "KeyUpEvent_txtPollfromdt(event);")
                    txtPollfromdt.Attributes.Add("OnKeyPress", "AllowNumeric('StartDateTextBox');")

                    txtTime.Attributes.Add("onkeyup", "KeyUpEvent_txtTime(event,'txtTime');")
                    txtTime.Attributes.Add("OnKeyPress", "AllowNumeric('txtTime');")

                    txtAutoExportTime.Attributes.Add("onkeyup", "KeyUpEvent_txtTime(event,'txtAutoExportTime');")
                    txtAutoExportTime.Attributes.Add("OnKeyPress", "AllowNumeric('txtAutoExportTime');")


                    'chkEnableAutoPoll.Attributes.Add("onclick", "disableListItems('ChkListDays', '0', '6')")
                    txtPollfromdt.Text = Now.Date.ToString("MM/dd/yyyy")
                    FillData()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollSetup.Page_Load", ex)
        End Try
    End Sub

    Private Sub FillGrids()

        'Code modify Jatin Kshirsagar as on 01 Sept 2008
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Dim ds = New DataSet()
        Try
            ds = dal.ExecuteStoredProcedureGetDataSet("Use_tt_GetSentry_PollSetup")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    GrdSentry.DataSource = ds.Tables(0)
                    GrdSentry.DataBind()
                Else
                    GrdSentry.DataSource = ds.Tables(0)
                    GrdSentry.DataBind()
                End If


                If ds.Tables(1).Rows.Count > 0 Then
                    GrdTM.DataSource = ds.Tables(1)
                    GrdTM.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("PollSetup_FillSentryGrid", ex)

            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    'Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
    '    Try
    '        Dim blnFlag As Boolean
    '        Dim dal = New GeneralizedDAL()
    '        Dim parcollection(23) As SqlParameter

    '        Dim ParFlagAutoexport = New SqlParameter("@FlagAutoexport", SqlDbType.VarChar, 2)
    '        Dim ParAutoexportfilextn = New SqlParameter("@Autoexportfilextn", SqlDbType.VarChar, 50)
    '        Dim ParAutoexportFileoverwrite = New SqlParameter("@AutoexportilOverwrite", SqlDbType.VarChar, 2)
    '        Dim ParEntry = New SqlParameter("@Entry", SqlDbType.Int)
    '        Dim ParTime = New SqlParameter("@Time", SqlDbType.VarChar, 50)
    '        Dim ParAutoexportTime = New SqlParameter("@AutoExportTime", SqlDbType.VarChar, 50)
    '        Dim ParDow = New SqlParameter("@Dow", SqlDbType.VarChar, 50)
    '        Dim ParPollmo = New SqlParameter("@Pollmo", SqlDbType.Int)
    '        Dim ParPollacct = New SqlParameter("@Pollacct", SqlDbType.Int)
    '        Dim ParSendmsg = New SqlParameter("@Sendmsg", SqlDbType.Int)
    '        Dim ParSendpms = New SqlParameter("@Sendpms", SqlDbType.Int)
    '        Dim ParFetchold = New SqlParameter("@Fetchold", SqlDbType.Int)
    '        Dim ParUpdate_vn = New SqlParameter("@Update_vn", SqlDbType.Int)
    '        Dim ParUpd_new_vn = New SqlParameter("@Upd_new_vn", SqlDbType.Int)
    '        Dim ParUpdate_pn = New SqlParameter("@Update_pn", SqlDbType.Int)
    '        Dim ParUpd_new_pn = New SqlParameter("@Upd_new_pn", SqlDbType.Int)
    '        Dim ParUpdate_lk = New SqlParameter("@Update_lk", SqlDbType.Int)
    '        Dim ParUpdtcd_lk = New SqlParameter("@Updtcd_lk", SqlDbType.Int)
    '        Dim ParPollfromdt = New SqlParameter("@Pollfromdt", SqlDbType.VarChar, 50)
    '        Dim ParPollTwiceDaily = New SqlParameter("@PollTwiceDaily", SqlDbType.Int)
    '        Dim ParAddEdit = New SqlParameter("@AddEdit", SqlDbType.VarChar, 6)

    '        'Added By Varun Moota a New Field to Update Records after Auto Poll.05/04/2011
    '        Dim ParPollUpdtRec = New SqlParameter("@PollUpdtRecs", SqlDbType.Int)
    '        Dim ParPollOBDII = New SqlParameter("@PollOBDII", SqlDbType.Int)



    '        ParEntry.Direction = ParameterDirection.Input : ParTime.Direction = ParameterDirection.Input
    '        ParDow.Direction = ParameterDirection.Input : ParPollmo.Direction = ParameterDirection.Input
    '        ParPollacct.Direction = ParameterDirection.Input : ParSendmsg.Direction = ParameterDirection.Input
    '        ParSendpms.Direction = ParameterDirection.Input : ParFetchold.Direction = ParameterDirection.Input
    '        ParUpdate_vn.Direction = ParameterDirection.Input : ParUpd_new_vn.Direction = ParameterDirection.Input
    '        ParUpdate_pn.Direction = ParameterDirection.Input : ParUpd_new_pn.Direction = ParameterDirection.Input
    '        ParUpdate_lk.Direction = ParameterDirection.Input : ParUpdtcd_lk.Direction = ParameterDirection.Input
    '        ParPollfromdt.Direction = ParameterDirection.Input : ParPollTwiceDaily.Direction = ParameterDirection.Input
    '        ParAddEdit.Direction = ParameterDirection.Input : ParPollUpdtRec.Direction = ParameterDirection.Input
    '        ParPollOBDII.Direction = ParameterDirection.Input : ParAutoexportTime.Direction = ParameterDirection.Input
    '        ParFlagAutoexport.Direction = ParameterDirection.Input : ParAutoexportfilextn.Direction = ParameterDirection.Input
    '        ParAutoexportFileoverwrite.Direction = ParameterDirection.Input


    '        Dim ds = New DataSet()
    '        ds = dal.GetDataSet("SELECT * FROM PSCHED")
    '        If Not ds Is Nothing Then
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                ParAddEdit.value = "Edit"
    '            Else
    '                ParAddEdit.value = "Add"
    '            End If
    '        Else
    '            ParAddEdit.value = "Add"
    '        End If

    '        Dim strDays As String = ""
    '        If chkEnableAutoPoll.Checked = True Then ParEntry.value = 1 Else ParEntry.value = 0
    '        ParTime.value = txtTime.Value
    '        ParAutoexportTime.value = txtAutoExportTime.Value
    '        For Each LItems As ListItem In ChkListDays.Items
    '            If LItems.Selected = True Then strDays = strDays + CStr("1") Else strDays = strDays + CStr("0")
    '        Next
    '        ParDow.value = strDays
    '        'Polling Control
    '        For Each LItems As ListItem In chkPollLst.Items
    '            Select Case LItems.Text
    '                Case "Poll Twice Daily" : If LItems.Selected = True Then ParPollTwiceDaily.value = 1 Else ParPollTwiceDaily.value = 0
    '                Case "Poll Accounts" : If LItems.Selected = True Then ParPollacct.value = 1 Else ParPollacct.value = 0
    '                Case "Send Messages" : If LItems.Selected = True Then ParSendmsg.value = 1 Else ParSendmsg.value = 0
    '                Case "Send PMs" : If LItems.Selected = True Then ParSendpms.value = 1 Else ParSendpms.value = 0
    '                Case "Poll Manual Override" : If LItems.Selected = True Then ParPollmo.value = 1 Else ParPollmo.value = 0
    '                    'Added By Varun Moota a New Field to Poll Records after Auto Poll is completed.
    '                Case "Re-Poll to Update Records" : If LItems.Selected = True Then ParPollUpdtRec.value = 1 Else ParPollUpdtRec.value = 0
    '                Case "Poll OBDII Messages" : If LItems.Selected = True Then ParPollOBDII.value = 1 Else ParPollOBDII.value = 0
    '            End Select
    '        Next

    '        If RDBPollLst.Items(0).Selected = True Then
    '            ParFetchold.value = 1
    '        ElseIf RDBPollLst.Items(1).Selected = True Then
    '            ParFetchold.value = 2
    '        ElseIf RDBPollLst.Items(2).Selected = True Then
    '            ParFetchold.value = 3
    '        Else
    '            ParFetchold.value = 1
    '        End If
    '        'For Each LItems As ListItem In RDBPollLst.Items
    '        '    If Not LItems.Text = "" Then
    '        '        Select Case LItems.Text
    '        '            Case "Normal Poll" : If LItems.Selected = True Then ParFetchold.value = 1 Else  : ParFetchold.value = 1
    '        '            Case "Repoll All Transactions" : If LItems.Selected = True Then ParFetchold.value = 2 Else  : ParFetchold.value = 1
    '        '            Case "Repoll from Date" : If LItems.Selected = True Then ParFetchold.value = 3 Else  : ParFetchold.value = 1
    '        '        End Select
    '        '    End If
    '        'Next
    '        ParPollfromdt.value = txtPollfromdt.Text

    '        ParAutoexportfilextn.value = txtFileExtn.Text

    '        If rdoOverwriteYes.Checked = True Then
    '            ParAutoexportFileoverwrite.value = 1
    '        ElseIf rdoOverwriteNo.Checked = True Then
    '            ParAutoexportFileoverwrite.value = 0
    '        Else
    '            ParAutoexportFileoverwrite.value = "Null"
    '        End If

    '        If chkAutoExport.Checked = True Then
    '            ParFlagAutoexport.value = 1
    '        Else
    '            ParFlagAutoexport.value = 0
    '        End If


    '        'Vehicle
    '        For Each LItems As ListItem In chkVehLst.Items
    '            Select Case LItems.Text
    '                Case "Send All Vehicle Info" : If LItems.Selected = True Then ParUpdate_vn.value = 1 Else ParUpdate_vn.value = 0
    '                Case "Send New,Deleted & Updated Vehicle Info" : If LItems.Selected = True Then ParUpd_new_vn.value = 1 Else ParUpd_new_vn.value = 0
    '            End Select
    '        Next
    '        'Lockout
    '        For Each LItems As ListItem In chkLockoutLst.Items
    '            Select Case LItems.Text
    '                Case "Send Key Lockouts" : If LItems.Selected = True Then ParUpdate_lk.value = 1 Else ParUpdate_lk.value = 0
    '                Case "Send Card Lockouts" : If LItems.Selected = True Then ParUpdtcd_lk.value = 1 Else ParUpdtcd_lk.value = 0
    '            End Select
    '        Next
    '        'Personnel
    '        For Each LItems As ListItem In chkPersonnelLst.Items
    '            Select Case LItems.Text
    '                Case "Send All Personnel Info" : If LItems.Selected = True Then ParUpdate_pn.value = 1 Else ParUpdate_pn.value = 0
    '                Case "Send New & Deleted Personnel Info" : If LItems.Selected = True Then ParUpd_new_pn.value = 1 Else ParUpd_new_pn.value = 0
    '            End Select
    '        Next
    '        parcollection(0) = ParEntry : parcollection(1) = ParTime
    '        parcollection(2) = ParDow : parcollection(3) = ParPollmo
    '        parcollection(4) = ParPollacct : parcollection(5) = ParSendmsg
    '        parcollection(6) = ParSendpms : parcollection(7) = ParFetchold
    '        parcollection(8) = ParUpdate_vn : parcollection(9) = ParUpd_new_vn
    '        parcollection(10) = ParUpdate_pn : parcollection(11) = ParUpd_new_pn
    '        parcollection(12) = ParUpdate_lk : parcollection(13) = ParUpdtcd_lk
    '        parcollection(14) = ParPollfromdt : parcollection(15) = ParPollTwiceDaily
    '        parcollection(16) = ParAddEdit : parcollection(17) = ParPollUpdtRec
    '        parcollection(18) = ParPollOBDII : parcollection(19) = ParAutoexportTime
    '        parcollection(20) = ParFlagAutoexport : parcollection(21) = ParAutoexportfilextn
    '        parcollection(22) = ParAutoexportFileoverwrite

    '        blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("use_tt_PSCHED_UPDATE", parcollection)

    '        Dim i As Integer, cntRow As Integer
    '        cntRow = 0
    '        For Each gvr As GridViewRow In GrdSentry.Rows
    '            'Get a programmatic reference to the CheckBox control
    '            Dim cb As CheckBox = CType(gvr.FindControl("Sentrychk1"), CheckBox)
    '            If cb.Checked = True Then
    '                i = dal.ExecuteNonQuery("UPDATE [Sentry] SET [POLL] =1 WHERE NUMBER='" & GrdSentry.Rows(cntRow).Cells(1).Text & "'")
    '            Else
    '                i = dal.ExecuteNonQuery("UPDATE [Sentry] SET [POLL] =0 WHERE NUMBER='" & GrdSentry.Rows(cntRow).Cells(1).Text & "'")
    '            End If
    '            cntRow = cntRow + 1
    '        Next

    '        cntRow = 0
    '        For Each gvr As GridViewRow In GrdTM.Rows
    '            'Get a programmatic reference to the CheckBox control
    '            Dim cb As CheckBox = CType(gvr.FindControl("TMchk1"), CheckBox)
    '            If cb.Checked = True Then
    '                i = dal.ExecuteNonQuery("UPDATE [TM] SET [POLL] =1 WHERE NUMBER='" & GrdTM.Rows(cntRow).Cells(2).Text & "'")
    '            Else
    '                i = dal.ExecuteNonQuery("UPDATE [TM] SET [POLL] =0 WHERE NUMBER='" & GrdTM.Rows(cntRow).Cells(2).Text & "'")
    '            End If
    '            cntRow = cntRow + 1
    '        Next
    '        If blnFlag = True Then
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record saved successfully.');location.href='PollSetup.aspx';</script>")
    '            'Added By Varun Moota to Work on Auto Poll.
    '            AutoPoll()
    '        End If
    '        'Response.Redirect("MeterReadings.aspx")
    '    Catch ex As Exception
    '        Dim cr As New ErrorPage
    '        cr.errorlog("PollSetup_btnOk_Click", ex)
    '    End Try
    'End Sub

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            Dim parcollection(32) As SqlParameter

            'Added by Pritam include PrevExportedtxtn. 24-March-2014

            Dim ParFlagAutoexport = New SqlParameter("@FlagAutoexport", SqlDbType.VarChar, 2)
            Dim ParAutoexportTime = New SqlParameter("@AutoExportTime", SqlDbType.VarChar, 50)
            Dim ParUseDateAsFileName = New SqlParameter("@UseDateAsFileName", SqlDbType.Int)
            Dim ParAutoIncrementFileNmae = New SqlParameter("@AutoIncrementFileNmae", SqlDbType.Int)
            Dim ParUseFixedFileName = New SqlParameter("@UseFixedFileName", SqlDbType.Int)
            Dim Parfixedfilename = New SqlParameter("@fixedfilename", SqlDbType.VarChar, 50)
            Dim ParAutoexportfilextn = New SqlParameter("@Autoexportfilextn", SqlDbType.VarChar, 50)
            Dim ParExportFileLocation = New SqlParameter("@ExportFileLocation", SqlDbType.VarChar, -1)
            Dim ParRunBatchFile = New SqlParameter("@RunBatchFile", SqlDbType.Int)
            Dim ParBatchProcessFileName = New SqlParameter("@BatchProcessFileName", SqlDbType.VarChar, 80)
            Dim ParBatchFileLocation = New SqlParameter("@BatchFileLocation", SqlDbType.VarChar, 80)
            Dim ParIncludePrevExportedTxtn = New SqlParameter("@IncPrevExportedTxtn", SqlDbType.Int)
            Dim ParIncZeroQty = New SqlParameter("@IncZeroQty", SqlDbType.Int)
            Dim ParAppendFile = New SqlParameter("@AppendFile", SqlDbType.Int)


            'Dim ParAutoexportFileoverwrite = New SqlParameter("@AutoexportilOverwrite", SqlDbType.VarChar, 2)
            Dim ParEntry = New SqlParameter("@Entry", SqlDbType.Int)
            Dim ParTime = New SqlParameter("@Time", SqlDbType.VarChar, 50)

            Dim ParDow = New SqlParameter("@Dow", SqlDbType.VarChar, 50)
            Dim ParPollmo = New SqlParameter("@Pollmo", SqlDbType.Int)
            Dim ParPollacct = New SqlParameter("@Pollacct", SqlDbType.Int)
            Dim ParSendmsg = New SqlParameter("@Sendmsg", SqlDbType.Int)
            Dim ParSendpms = New SqlParameter("@Sendpms", SqlDbType.Int)
            Dim ParFetchold = New SqlParameter("@Fetchold", SqlDbType.Int)
            Dim ParUpdate_vn = New SqlParameter("@Update_vn", SqlDbType.Int)
            Dim ParUpd_new_vn = New SqlParameter("@Upd_new_vn", SqlDbType.Int)
            Dim ParUpdate_pn = New SqlParameter("@Update_pn", SqlDbType.Int)
            Dim ParUpd_new_pn = New SqlParameter("@Upd_new_pn", SqlDbType.Int)
            Dim ParUpdate_lk = New SqlParameter("@Update_lk", SqlDbType.Int)
            Dim ParUpdtcd_lk = New SqlParameter("@Updtcd_lk", SqlDbType.Int)
            Dim ParPollfromdt = New SqlParameter("@Pollfromdt", SqlDbType.VarChar, 50)
            Dim ParPollTwiceDaily = New SqlParameter("@PollTwiceDaily", SqlDbType.Int)
            Dim ParAddEdit = New SqlParameter("@AddEdit", SqlDbType.VarChar, 6)

            'Added By Varun Moota a New Field to Update Records after Auto Poll.05/04/2011
            Dim ParPollUpdtRec = New SqlParameter("@PollUpdtRecs", SqlDbType.Int)
            Dim ParPollOBDII = New SqlParameter("@PollOBDII", SqlDbType.Int)

            'Added By Pritam. 17/04/2014

            ' Dim ParFileName = New SqlParameter("@FileName", SqlDbType.VarChar, 50)
            ParEntry.Direction = ParameterDirection.Input : ParTime.Direction = ParameterDirection.Input
            ParDow.Direction = ParameterDirection.Input : ParPollmo.Direction = ParameterDirection.Input
            ParPollacct.Direction = ParameterDirection.Input : ParSendmsg.Direction = ParameterDirection.Input
            ParSendpms.Direction = ParameterDirection.Input : ParFetchold.Direction = ParameterDirection.Input
            ParUpdate_vn.Direction = ParameterDirection.Input : ParUpd_new_vn.Direction = ParameterDirection.Input
            ParUpdate_pn.Direction = ParameterDirection.Input : ParUpd_new_pn.Direction = ParameterDirection.Input
            ParUpdate_lk.Direction = ParameterDirection.Input : ParUpdtcd_lk.Direction = ParameterDirection.Input
            ParPollfromdt.Direction = ParameterDirection.Input : ParPollTwiceDaily.Direction = ParameterDirection.Input
            ParAddEdit.Direction = ParameterDirection.Input : ParPollUpdtRec.Direction = ParameterDirection.Input
            ParPollOBDII.Direction = ParameterDirection.Input

            ParFlagAutoexport.Direction = ParameterDirection.Input : ParUseDateAsFileName.Direction = ParameterDirection.Input
            ParAutoIncrementFileNmae.Direction = ParameterDirection.Input : ParAutoexportTime.Direction = ParameterDirection.Input
            ParUseFixedFileName.Direction = ParameterDirection.Input : Parfixedfilename.Direction = ParameterDirection.Input
            ParAutoexportfilextn.Direction = ParameterDirection.Input : ParExportFileLocation.Direction = ParameterDirection.Input
            ParRunBatchFile.Direction = ParameterDirection.Input : ParBatchProcessFileName.Direction = ParameterDirection.Input
            ParBatchFileLocation.Direction = ParameterDirection.Input : ParIncludePrevExportedTxtn.Direction = ParameterDirection.Input
            ParIncZeroQty.Direction = ParameterDirection.Input : ParAppendFile.Direction = ParameterDirection.Input

            Dim ds = New DataSet()
            ds = dal.GetDataSet("SELECT * FROM PSCHED")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ParAddEdit.value = "Edit"
                Else
                    ParAddEdit.value = "Add"
                End If
            Else
                ParAddEdit.value = "Add"
            End If

            Dim strDays As String = ""
            If chkEnableAutoPoll.Checked = True Then ParEntry.value = 1 Else ParEntry.value = 0
            ParTime.value = txtTime.Value

            For Each LItems As ListItem In ChkListDays.Items
                If LItems.Selected = True Then strDays = strDays + CStr("1") Else strDays = strDays + CStr("0")
            Next
            ParDow.value = strDays
            'Polling Control
            For Each LItems As ListItem In chkPollLst.Items
                Select Case LItems.Text
                    Case "Poll Twice Daily" : If LItems.Selected = True Then ParPollTwiceDaily.value = 1 Else ParPollTwiceDaily.value = 0
                    Case "Poll Accounts" : If LItems.Selected = True Then ParPollacct.value = 1 Else ParPollacct.value = 0
                    Case "Send Messages" : If LItems.Selected = True Then ParSendmsg.value = 1 Else ParSendmsg.value = 0
                    Case "Send PMs" : If LItems.Selected = True Then ParSendpms.value = 1 Else ParSendpms.value = 0
                    Case "Poll Manual Override" : If LItems.Selected = True Then ParPollmo.value = 1 Else ParPollmo.value = 0
                        'Added By Varun Moota a New Field to Poll Records after Auto Poll is completed.
                    Case "Re-Poll to Update Records" : If LItems.Selected = True Then ParPollUpdtRec.value = 1 Else ParPollUpdtRec.value = 0
                    Case "Poll OBDII Messages" : If LItems.Selected = True Then ParPollOBDII.value = 1 Else ParPollOBDII.value = 0
                End Select
            Next
            If RDBPollLst.Items(0).Selected = True Then
                ParFetchold.value = 1
            ElseIf RDBPollLst.Items(1).Selected = True Then
                ParFetchold.value = 2
            ElseIf RDBPollLst.Items(2).Selected = True Then
                ParFetchold.value = 3
            Else
                ParFetchold.value = 1
            End If
            'For Each LItems As ListItem In RDBPollLst.Items
            '    If Not LItems.Text = "" Then
            '        Select Case LItems.Text
            '            Case "Normal Poll" : If LItems.Selected = True Then ParFetchold.value = 1 Else  : ParFetchold.value = 1
            '            Case "Repoll All Transactions" : If LItems.Selected = True Then ParFetchold.value = 2 Else  : ParFetchold.value = 1
            '            Case "Repoll from Date" : If LItems.Selected = True Then ParFetchold.value = 3 Else  : ParFetchold.value = 1
            '        End Select
            '    End If
            'Next
            ParPollfromdt.value = txtPollfromdt.Text

            If chkAutoExport.Checked = True Then
                ParFlagAutoexport.value = 1
            Else
                ParFlagAutoexport.value = 0
            End If

            ParAutoexportTime.value = txtAutoExportTime.Value
            'Use date As File Name
            If rdoUDFName.Checked = True Then
                ParUseDateAsFileName.value = 1
            ElseIf rdoUDFName.Checked = False Then
                ParUseDateAsFileName.value = 0
            End If

            'Use Auto increment file name
            If rdoAutoIncrmnt.Checked = True Then
                ParAutoIncrementFileNmae.value = 1
            ElseIf rdoAutoIncrmnt.Checked = False Then
                ParAutoIncrementFileNmae.Value = 0
            End If

            'Use fixed file name
            If rdoUfixed.Checked = True Then
                ParUseFixedFileName.value = 1
            ElseIf rdoUfixed.Checked = False Then
                ParUseFixedFileName.value = 0
            End If

            'File Name
            Parfixedfilename.value = txtFixedfilename.Text.ToString()

            'Export File Extension
            ParAutoexportfilextn.value = txtExportFileExtn.Text.ToString()

            'Export File Location 
            ParExportFileLocation.value = txtExportFLocation.Text.ToString()

            'Execute batch process after Auto Export
            If chkExecuteBatchProcess.Checked = True Then
                ParRunBatchFile.value = 1
            ElseIf chkExecuteBatchProcess.Checked = False Then
                ParRunBatchFile.value = 0
            End If

            'Batch Process File Name
            ParBatchProcessFileName.value = txtBatchProcessFileName.Text.ToString()

            'Batch File Location
            ParBatchFileLocation.value = txtBatchFileLocation.Text.ToString()

            'Include Previously Exported Transaction
            If chkIncPrevExprtTxtn.Checked = True Then
                ParIncludePrevExportedTxtn.value = 1
            ElseIf chkIncPrevExprtTxtn.Checked = False Then
                ParIncludePrevExportedTxtn.value = 0
            End If

            'Include Zero Qty TXTN
            If chkIncZeroQtyTxtn.Checked = True Then
                ParIncZeroQty.value = 1
            ElseIf chkIncZeroQtyTxtn.Checked = False Then
                ParIncZeroQty.value = 0
            End If

            'Append to previously created export file
            If chkAppendtoPrevExprtFile.Checked = True Then
                ParAppendFile.value = 1
            ElseIf chkAppendtoPrevExprtFile.Checked = False Then
                ParAppendFile.value = 0
            End If



            'Vehicle
            For Each LItems As ListItem In chkVehLst.Items
                Select Case LItems.Text
                    Case "Send All Vehicle Info" : If LItems.Selected = True Then ParUpdate_vn.value = 1 Else ParUpdate_vn.value = 0
                    Case "Send New,Deleted & Updated Vehicle Info" : If LItems.Selected = True Then ParUpd_new_vn.value = 1 Else ParUpd_new_vn.value = 0
                End Select
            Next
            'Lockout
            For Each LItems As ListItem In chkLockoutLst.Items
                Select Case LItems.Text
                    Case "Send Key Lockouts" : If LItems.Selected = True Then ParUpdate_lk.value = 1 Else ParUpdate_lk.value = 0
                    Case "Send Card Lockouts" : If LItems.Selected = True Then ParUpdtcd_lk.value = 1 Else ParUpdtcd_lk.value = 0
                End Select
            Next
            'Personnel
            For Each LItems As ListItem In chkPersonnelLst.Items
                Select Case LItems.Text
                    Case "Send All Personnel Info" : If LItems.Selected = True Then ParUpdate_pn.value = 1 Else ParUpdate_pn.value = 0
                    Case "Send New & Deleted Personnel Info" : If LItems.Selected = True Then ParUpd_new_pn.value = 1 Else ParUpd_new_pn.value = 0
                End Select
            Next
            parcollection(0) = ParEntry : parcollection(1) = ParTime
            parcollection(2) = ParDow : parcollection(3) = ParPollmo
            parcollection(4) = ParPollacct : parcollection(5) = ParSendmsg
            parcollection(6) = ParSendpms : parcollection(7) = ParFetchold
            parcollection(8) = ParUpdate_vn : parcollection(9) = ParUpd_new_vn
            parcollection(10) = ParUpdate_pn : parcollection(11) = ParUpd_new_pn
            parcollection(12) = ParUpdate_lk : parcollection(13) = ParUpdtcd_lk
            parcollection(14) = ParPollfromdt : parcollection(15) = ParPollTwiceDaily
            parcollection(16) = ParAddEdit : parcollection(17) = ParPollUpdtRec
            parcollection(18) = ParPollOBDII

            parcollection(19) = ParFlagAutoexport : parcollection(20) = ParAutoexportTime
            parcollection(21) = ParUseDateAsFileName : parcollection(22) = ParAutoIncrementFileNmae
            parcollection(23) = ParUseFixedFileName : parcollection(24) = Parfixedfilename
            parcollection(25) = ParAutoexportfilextn : parcollection(26) = ParExportFileLocation
            parcollection(27) = ParRunBatchFile : parcollection(28) = ParBatchProcessFileName
            parcollection(29) = ParBatchFileLocation : parcollection(30) = ParIncludePrevExportedTxtn
            parcollection(31) = ParIncZeroQty : parcollection(32) = ParAppendFile

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("use_tt_PSCHED_UPDATE", parcollection)
            'blnFlag = dal.ExecuteStoredProcedureGetBoolean("use_tt_PSCHED_UPDATE", parcollection)

            Dim i As Integer, cntRow As Integer
            cntRow = 0
            For Each gvr As GridViewRow In GrdSentry.Rows
                'Get a programmatic reference to the CheckBox control
                Dim cb As CheckBox = CType(gvr.FindControl("Sentrychk1"), CheckBox)
                If cb.Checked = True Then
                    i = dal.ExecuteNonQuery("UPDATE [Sentry] SET [POLL] =1 WHERE NUMBER='" & GrdSentry.Rows(cntRow).Cells(1).Text & "'")
                Else
                    i = dal.ExecuteNonQuery("UPDATE [Sentry] SET [POLL] =0 WHERE NUMBER='" & GrdSentry.Rows(cntRow).Cells(1).Text & "'")
                End If
                cntRow = cntRow + 1
            Next

            cntRow = 0
            For Each gvr As GridViewRow In GrdTM.Rows
                'Get a programmatic reference to the CheckBox control
                Dim cb As CheckBox = CType(gvr.FindControl("TMchk1"), CheckBox)
                If cb.Checked = True Then
                    i = dal.ExecuteNonQuery("UPDATE [TM] SET [POLL] =1 WHERE NUMBER='" & GrdTM.Rows(cntRow).Cells(2).Text & "'")
                Else
                    i = dal.ExecuteNonQuery("UPDATE [TM] SET [POLL] =0 WHERE NUMBER='" & GrdTM.Rows(cntRow).Cells(2).Text & "'")
                End If
                cntRow = cntRow + 1
            Next

            'By Soham Gangavane July 24, 2017
            Dim expStatus As Integer = 0
            For Each li As ListItem In rdbExportSelection.Items
                If li.Selected = True Then
                    If li.Value = "1" Then
                        expStatus = 1
                    ElseIf li.Value = "2" And expStatus = 1 Then
                        expStatus = 3
                    ElseIf li.Value = "2" And expStatus = 0 Then
                        expStatus = 2
                    End If
                End If
            Next
            dal.ExecuteNonQuery("UPDATE [PSCHED] SET [ExportStatus] =" & expStatus & "")



            If blnFlag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record saved successfully.');location.href='PollSetup.aspx';</script>")
                'Added By Varun Moota to Work on Auto Poll.
                AutoPoll()
            End If
            'Response.Redirect("MeterReadings.aspx")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollSetup_btnOk_Click", ex)
        End Try
    End Sub

    Protected Sub GrdTM_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdTM.DataBound
        Try
            ' Each time the data is bound to the grid we need to build up the CheckBoxIDs array
            'Get the header CheckBox
            Dim cbHeader As CheckBox = CType(GrdTM.HeaderRow.FindControl("chkTMSelectAll"), CheckBox)

            'Run the ChangeCheckBoxState client-side function whenever the
            'header checkbox is checked/unchecked
            cbHeader.Attributes("onclick") = "ChangeAllCheckBoxStatesTM(this.checked);"

            'Add the CheckBox's ID to the client-side CheckBoxIDs array
            Dim ArrayValues As New List(Of String)
            ArrayValues.Add(String.Concat("'", cbHeader.ClientID, "'"))

            For Each gvr As GridViewRow In GrdTM.Rows
                'Get a programmatic reference to the CheckBox control
                Dim cb As CheckBox = CType(gvr.FindControl("TMchk1"), CheckBox)

                'If the checkbox is unchecked, ensure that the Header CheckBox is unchecked
                cb.Attributes("onclick") = "ChangeHeaderAsNeededTM();"

                'Add the CheckBox's ID to the client-side CheckBoxIDs array
                ArrayValues.Add(String.Concat("'", cb.ClientID, "'"))
            Next

            'Output the array to the Literal control (CheckBoxIDsArray)
            CheckBoxIDsArray.Text = "<script type=""text/javascript"">" & vbCrLf & _
                                    "<!--" & vbCrLf & _
                                    String.Concat("var CheckBoxIDsTM =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") & vbCrLf & _
                                    "// -->" & vbCrLf & _
                                    "</script>"
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollSetup.GrdTM_DataBound", ex)
        End Try
    End Sub

    Protected Sub GrdSentry_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdSentry.DataBound
        'Each time the data is bound to the grid we need to build up the CheckBoxIDs array

        'Get the header CheckBox
        Try

            Dim cbHeader As CheckBox = CType(GrdSentry.HeaderRow.FindControl("chkSentrySelectAll"), CheckBox)

            'Run the ChangeCheckBoxState client-side function whenever the
            'header checkbox is checked/unchecked
            cbHeader.Attributes("onclick") = "ChangeAllCheckBoxStates(this.checked);"

            'Add the CheckBox's ID to the client-side CheckBoxIDs array
            Dim ArrayValues As New List(Of String)
            ArrayValues.Add(String.Concat("'", cbHeader.ClientID, "'"))

            For Each gvr As GridViewRow In GrdSentry.Rows
                'Get a programmatic reference to the CheckBox control
                Dim cb As CheckBox = CType(gvr.FindControl("Sentrychk1"), CheckBox)

                'If the checkbox is unchecked, ensure that the Header CheckBox is unchecked
                cb.Attributes("onclick") = "ChangeHeaderAsNeeded();"

                'Add the CheckBox's ID to the client-side CheckBoxIDs array
                ArrayValues.Add(String.Concat("'", cb.ClientID, "'"))
            Next

            'Output the array to the Literal control (CheckBoxIDsArray)
            CheckBoxIDsArraySentry.Text = "<script type=""text/javascript"">" & vbCrLf & _
                                    "<!--" & vbCrLf & _
                                    String.Concat("var CheckBoxIDs =  new Array(", String.Join(",", ArrayValues.ToArray()), ");") & vbCrLf & _
                                    "// -->" & vbCrLf & _
                                    "</script>"

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollSetup.GrdSentry_DataBound", ex)
        End Try
    End Sub

    Protected Sub chkPersonnelLst_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPersonnelLst.SelectedIndexChanged
        Try
            If chkPersonnelLst.Items(0).Selected Then
                chkPersonnelLst.Items(1).Enabled = False
                chkPersonnelLst.Items(1).Selected = False
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollSetup.chkPersonnelLst_SelectedIndexChanged", ex)
        End Try

    End Sub

    Private Sub AutoPoll()
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter

            Dim parPollFlag = New SqlParameter("@PollFlag", SqlDbType.NVarChar)
            parPollFlag.Direction = ParameterDirection.Input

            Dim blnFlag As Boolean
            Dim polltime As String = Session("PollTimeValue").ToString()
            Dim ttime As String = txtTime.Value.ToString()

            If Not Session("PollTimeValue").ToString() = txtTime.Value.ToString() Then
                parPollFlag.value = "True"
            Else
                parPollFlag.value = "False"
            End If
            parcollection(0) = parPollFlag
            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("SP_AutoPoll", parcollection)

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollSetup.AutoPoll()", ex)
        End Try

    End Sub

    Private Sub FillData()
        Try
            Dim ds = New DataSet()
            Dim dsTemp = New DataSet()
            Dim dal = New GeneralizedDAL()
            Dim i As Integer
            ds = dal.GetDataSet("SELECT * FROM PSCHED")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    If ds.Tables(0).Rows(0)("Entry") = 1 Then chkEnableAutoPoll.Checked = True Else chkEnableAutoPoll.Checked = False
                    txtTime.Value = ds.Tables(0).Rows(0)("Time")

                    Session("PollTimeValue") = txtTime.Value

                    Dim strDays As String = ds.Tables(0).Rows(0)("Dow")
                    i = 1
                    For Each LItems As ListItem In ChkListDays.Items
                        If Val(Mid(strDays, i, 1)) = 1 Then LItems.Selected = True Else LItems.Selected = False
                        i = i + 1
                    Next
                    'Polling Control
                    For Each LItems As ListItem In chkPollLst.Items
                        Select Case LItems.Text
                            Case "Poll Twice Daily" : If ds.Tables(0).Rows(0)("PollTwiceDaily") = 1 Then LItems.Selected = True Else LItems.Selected = False
                            Case "Poll Accounts" : If ds.Tables(0).Rows(0)("Pollacct") = 1 Then LItems.Selected = True Else LItems.Selected = False
                            Case "Send Messages" : If ds.Tables(0).Rows(0)("Sendmsg") = 1 Then LItems.Selected = True Else LItems.Selected = False
                            Case "Send PMs" : If ds.Tables(0).Rows(0)("Sendpms") = 1 Then LItems.Selected = True Else LItems.Selected = False
                            Case "Poll Manual Override" : If ds.Tables(0).Rows(0)("Pollmo") = 1 Then LItems.Selected = True Else LItems.Selected = False
                            Case "Re-Poll to Update Records" : If ds.Tables(0).Rows(0)("PollUpdtRecs") = 1 Then LItems.Selected = True Else LItems.Selected = False
                            Case "Poll OBDII Messages" : If ds.Tables(0).Rows(0)("PollOBDII") = 1 Then LItems.Selected = True Else LItems.Selected = False
                        End Select
                    Next
                    If Not ds.Tables(0).Rows(0)("Fetchold") Is DBNull.Value Then
                        For Each LItems As ListItem In RDBPollLst.Items
                            Select Case LItems.Text
                                Case "Normal Poll" : If ds.Tables(0).Rows(0)("Fetchold") = 1 Then LItems.Selected = True : txtPollfromdt.Enabled = False Else LItems.Selected = False
                                Case "Repoll All Transactions" : If ds.Tables(0).Rows(0)("Fetchold") = 2 Then LItems.Selected = True : txtPollfromdt.Enabled = False Else LItems.Selected = False
                                Case "Repoll from Date" : If ds.Tables(0).Rows(0)("Fetchold") = 3 Then LItems.Selected = True : txtPollfromdt.Enabled = True Else LItems.Selected = False
                            End Select
                        Next
                        txtPollfromdt.Text = ds.Tables(0).Rows(0)("Pollfromdt")

                    Else
                        RDBPollLst.Items(0).Selected = True
                        txtPollfromdt.Text = ds.Tables(0).Rows(0)("Pollfromdt")
                    End If
                    'If Not ds.Tables(0).Rows(0)("Fetchold") = Nothing Then
                    '    For Each LItems As ListItem In RDBPollLst.Items
                    '        Select Case LItems.Text
                    '            Case "Normal Poll" : If ds.Tables(0).Rows(0)("Fetchold") = 1 Then LItems.Selected = True : txtPollfromdt.Enabled = False Else LItems.Selected = False
                    '            Case "Repoll All Transactions" : If ds.Tables(0).Rows(0)("Fetchold") = 2 Then LItems.Selected = True : txtPollfromdt.Enabled = False Else LItems.Selected = False
                    '            Case "Repoll from Date" : If ds.Tables(0).Rows(0)("Fetchold") = 3 Then LItems.Selected = True : txtPollfromdt.Enabled = True Else LItems.Selected = False
                    '        End Select
                    '    Next
                    '    txtPollfromdt.Text = ds.Tables(0).Rows(0)("Pollfromdt")
                    'End If
                    'Else
                    '    For Each LItems As ListItem In RDBPollLst.Items
                    '        Select Case LItems.Text
                    '            Case "Normal Poll" : LItems.Selected = True : txtPollfromdt.Enabled = False
                    '        End Select
                    '    Next
                    'Vehicle
                    For Each LItems As ListItem In chkVehLst.Items
                        Select Case LItems.Text
                            Case "Send All Vehicle Info" : If ds.Tables(0).Rows(0)("Update_vn") = 1 Then LItems.Selected = True Else LItems.Selected = False
                            Case "Send New,Deleted & Updated Vehicle Info" : If ds.Tables(0).Rows(0)("Upd_new_vn") = 1 Then LItems.Selected = True Else LItems.Selected = False
                        End Select
                    Next
                    'Lockout
                    For Each LItems As ListItem In chkLockoutLst.Items
                        Select Case LItems.Text
                            Case "Send Key Lockouts" : If ds.Tables(0).Rows(0)("Update_lk") = 1 Then LItems.Selected = True Else LItems.Selected = False
                            Case "Send Card Lockouts" : If ds.Tables(0).Rows(0)("Updtcd_lk") = 1 Then LItems.Selected = True Else LItems.Selected = False
                        End Select
                    Next
                    'Personnel
                    For Each LItems As ListItem In chkPersonnelLst.Items
                        Select Case LItems.Text
                            Case "Send All Personnel Info" : If ds.Tables(0).Rows(0)("Update_pn") = 1 Then LItems.Selected = True Else LItems.Selected = False
                            Case "Send New & Deleted Personnel Info" : If ds.Tables(0).Rows(0)("Upd_new_pn") = 1 Then LItems.Selected = True Else LItems.Selected = False
                        End Select
                    Next 'isnull(enbr_lngth,'') as enbr_lngth
                    'Commented By Varun Moota, to check with John/Stuart on enbr_lngth,vnbr_lngth.10/20/2010
                    ' ''dsTemp = dal.GetDataSet("SELECT isnull(enbr_lngth,'') as enbr_lngth,isnull(vnbr_lngth,'') as vnbr_lngth  FROM STATUS")
                    ' ''If Not dsTemp Is Nothing Then
                    ' ''    If dsTemp.Tables(0).Rows.Count > 0 Then
                    ' ''        If Len(dsTemp.Tables(0).Rows(0)("enbr_lngth")) > 0 Then ' Personnel
                    ' ''            tblPersonnel.Disabled = False
                    ' ''        Else
                    ' ''            tblPersonnel.Disabled = True
                    ' ''        End If
                    ' ''        If Len(dsTemp.Tables(0).Rows(0)("vnbr_lngth")) > 0 Then ' Vehicle
                    ' ''            tblVeh.Disabled = False
                    ' ''        Else
                    ' ''            tblVeh.Disabled = True
                    ' ''        End If
                    ' ''    End If
                    ' ''End If
                End If
                If ds.Tables(0).Rows(0)("FlagAutoExport") = 0 Then txtAutoExportTime.Disabled = True
                'If ds.Tables(0).Rows(0)("FlagAutoExport") = 0 Then txtFileExtn.Enabled = False
                ' If ds.Tables(0).Rows(0)("FlagAutoExport") = 0 Then tdradio.Disabled = True
                If ds.Tables(0).Rows(0)("FlagAutoExport") = 1 Then chkAutoExport.Checked = True Else chkAutoExport.Checked = False


                txtAutoExportTime.Value = ds.Tables(0).Rows(0)("AutoExportTime")

                '  txtFileExtn.Text = ds.Tables(0).Rows(0)("AutoExportFileExtn").ToString()


                If ds.Tables(0).Rows(0)("UseDateAsFileName") = 0 Then rdoUDFName.Checked = False
                If ds.Tables(0).Rows(0)("UseDateAsFileName") = 1 Then rdoUDFName.Checked = True

                If ds.Tables(0).Rows(0)("UseAutoIncrementFileName") = 1 Then rdoAutoIncrmnt.Checked = True
                If ds.Tables(0).Rows(0)("UseAutoIncrementFileName") = 0 Then rdoAutoIncrmnt.Checked = False

                If ds.Tables(0).Rows(0)("UseFixedFileName") = 0 Then rdoUfixed.Checked = False
                If ds.Tables(0).Rows(0)("UseFixedFileName") = 1 Then rdoUfixed.Checked = True

                txtFixedfilename.Text = ds.Tables(0).Rows(0)("fixedfilename").ToString()

                txtExportFileExtn.Text = ds.Tables(0).Rows(0)("AutoExportFileExtn").ToString()

                txtExportFLocation.Text = ds.Tables(0).Rows(0)("ExportFileLocation").ToString()

                If ds.Tables(0).Rows(0)("runbatchfile") = 0 Then chkExecuteBatchProcess.Checked = False
                If ds.Tables(0).Rows(0)("runbatchfile") = 1 Then chkExecuteBatchProcess.Checked = True

                txtBatchProcessFileName.Text = ds.Tables(0).Rows(0)("BatchProcessFileName").ToString()

                txtBatchFileLocation.Text = ds.Tables(0).Rows(0)("BatchFileLocation").ToString()



                If ds.Tables(0).Rows(0)("includePrevExportedTxtn") = 1 Then chkIncPrevExprtTxtn.Checked = True
                If ds.Tables(0).Rows(0)("includePrevExportedTxtn") = 0 Then chkIncPrevExprtTxtn.Checked = False

                If ds.Tables(0).Rows(0)("includeZeroQtyTXTN") = 1 Then chkIncZeroQtyTxtn.Checked = True
                If ds.Tables(0).Rows(0)("includeZeroQtyTXTN") = 0 Then chkIncZeroQtyTxtn.Checked = False

                If ds.Tables(0).Rows(0)("appendfile") = 1 Then chkAppendtoPrevExprtFile.Checked = True
                If ds.Tables(0).Rows(0)("appendfile") = 0 Then chkAppendtoPrevExprtFile.Checked = False

                'By Soham Gangavane July 24, 2017
                If ds.Tables(0).Rows(0)("ExportStatus") = 0 Then
                    For Each li As ListItem In rdbExportSelection.Items
                        li.Selected = False
                    Next
                ElseIf ds.Tables(0).Rows(0)("ExportStatus") = 1 Then
                    For Each li As ListItem In rdbExportSelection.Items
                        If li.Value = "1" Then
                            li.Selected = True
                        End If
                    Next
                ElseIf ds.Tables(0).Rows(0)("ExportStatus") = 2 Then
                    For Each li As ListItem In rdbExportSelection.Items
                        If li.Value = "2" Then
                            li.Selected = True
                        End If
                    Next
                ElseIf ds.Tables(0).Rows(0)("ExportStatus") = 3 Then
                    For Each li As ListItem In rdbExportSelection.Items
                        li.Selected = True
                    Next
                End If

            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PollSetup.FillData()", ex)
        End Try
    End Sub

End Class
