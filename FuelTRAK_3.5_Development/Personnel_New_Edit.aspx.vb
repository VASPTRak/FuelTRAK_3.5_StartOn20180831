Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class Personnel_New_Edit
    Inherits System.Web.UI.Page
    Dim PerTcount As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dal = New GeneralizedDAL()


        'Check for session is null or not if not to to else part
        If Session("User_name") Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
        Else
            txtKetexp.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
            txtCard.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
            txtCardexp.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")

            txtKetexp.Attributes.Add("onkeyup", "KeyUpEvent_txtKetexp(event);")
            txtFirstname.Attributes.Add("onkeyup", "KeyUpEvent_txtFirstname(event);")
            txtMI.Attributes.Add("onkeyup", "KeyUpEvent_txtMI(event);")
            txtLastName.Attributes.Add("onkeyup", "KeyUpEvent_txtLastName(event);")

            txtCard.Attributes.Add("onkeyup", "KeyUpEvent_txtCard(event);")
            txtCardexp.Attributes.Add("onkeyup", "KeyUpEvent_txtCardexp(event);")

            txtAccId.Attributes.Add("onkeyup", "KeyUpEvent_txtAccId(event);")

            Try
                If Not Session("visited") Or Not IsPostBack Then
                    If Not IsPostBack Then
                        DDLstDepartment.Items.Clear()
                        Dim ds = New DataSet()
                        ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PerPopulateDeptList")
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                DDLstDepartment.DataSource = ds.Tables(0)
                                DDLstDepartment.DataTextField = "DeptNoName"
                                DDLstDepartment.DataValueField = "NUMBER"
                                DDLstDepartment.DataBind()
                            End If
                        End If
                        If Not Session("visited") Or Not IsPostBack Then
                            ds = New DataSet()
                            'retrieve Total Count from Pers Table
                            'Commeneted By varun Moota.01/12/2011
                            ' ''ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PerTCount")
                            ' ''If Not ds Is Nothing Then
                            ' ''    If ds.Tables(0).Rows.Count > 0 Then
                            ' ''        PerTcount = Convert.ToString(ds.Tables(0).Rows(0).Item("PerTCount"))
                            ' ''    End If
                            ' ''End If
                        End If
                        If (Request.QueryString.Count = 1 And Not IsPostBack) Then
                            btnOk.Visible = False
                            lblNew_Edit.Text = "Edit Personnel Information"
                            btnDelete.Visible = True
                            ds = New DataSet
                            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PerDetails")
                            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("PersonnelId")}
                            'Added By varun Moota.01/12/2011
                            PerTcount = CInt(ds.Tables(0).Rows.Count)

                            Dim cnt As Long = 0
                            If Not ds Is Nothing Then
                                If ds.Tables(0).Rows.Count > 0 Then
                                    Dim dr As DataRow = ds.Tables(0).Rows.Find(Request.QueryString(0).ToString())
                                    If Not IsDBNull(dr) Then
                                        cnt = ds.Tables(0).Rows.IndexOf(dr)
                                    End If
                                    Session("PerDS") = ds
                                    Session("PerTCnt") = PerTcount
                                    Session("currentrecord") = cnt
                                    FillTextbox(cnt)
                                    txtPersonnelId.Enabled = False
                                End If
                            End If
                            If cnt + 1 = 1 Then
                                lblof.Visible = True
                                btnprevious.Enabled = False
                                btnNext.Enabled = True
                                btnFirst.Enabled = False
                                btnLast.Enabled = True
                            ElseIf cnt + 1 = PerTcount Then
                                lblof.Visible = True
                                btnprevious.Enabled = True
                                btnNext.Enabled = False
                                btnFirst.Enabled = True
                                btnLast.Enabled = False
                            ElseIf cnt + 1 < PerTcount Then
                                lblof.Visible = True
                                btnprevious.Enabled = True
                                btnNext.Enabled = True
                                btnFirst.Enabled = True
                                btnLast.Enabled = True
                            End If
                            'Added By Varun to Hide the EncodeKey Button.02/09/2010
                            btnEncodeKey.Visible = False
                            encodeKeyLink.Visible = True
                        Else
                            txtKey.Text = "0000"
                            lblNew_Edit.Text = "New Personnel Information"
                            lblof.Visible = False
                            btnprevious.Enabled = False
                            btnNext.Enabled = False
                            btnFirst.Enabled = False
                            btnLast.Enabled = False

                            txtKey.Text = "*NEW*"
                            'Added By Varun to Hide the EncodeKey Button.02/09/2010
                            btnEncodeKey.Visible = False
                            encodeKeyLink.Visible = False

                            txtPersonnelId.Focus()
                        End If
                    End If
                End If
            Catch ex As Exception
                Dim cr As New ErrorPage
                Dim errmsg As String

                cr.errorlog("Personnel_new_edit_Page_Load", ex)
                If ex.Message.Contains(";") Then
                    errmsg = ex.Message.ToString()
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
                Else
                    errmsg = ex.Message.ToString()
                End If
            End Try
        End If

    End Sub

    Public Sub FillTextbox(ByVal cnt As Integer)
        Dim ds = New DataSet()
        ds = Session("PerDS")
        PerTcount = Session("PerTCnt")
        encodeKeyLink.NavigateUrl = "fueltrakencode:p" + ds.Tables(0).Rows(cnt).Item("IDENTITY").ToString()
        txtPersonnelId.Text = ds.Tables(0).Rows(cnt).Item("IDENTITY").ToString()

        Session("PerIDCheck") = txtPersonnelId.Text.Trim
        'Added By Varun Moota As per John's Request. 04/02/2010
        txtPersonnelID2.Text = ds.Tables(0).Rows(cnt).Item("IDENTITY2").ToString()

        txtPersonalIdHide.Text = ds.Tables(0).Rows(cnt).Item("PersonnelId").ToString()
        txtFirstname.Text = ds.Tables(0).Rows(cnt).Item("FIRST_NAME").ToString()
        txtLastName.Text = ds.Tables(0).Rows(cnt).Item("LAST_NAME").ToString()
        txtKey.Text = ds.Tables(0).Rows(cnt).Item("KEY_NUMBER").ToString()

        'Added By Varun to Disable the LostKey Button, if the Key# Exists in KeyLock Table.
        Dim dsLostKeyNumber As New DataSet
        Dim dal As New GeneralizedDAL
        dsLostKeyNumber = dal.GetDataSet("DELETE FROM KeyLock WHERE Key_Number LIKE '%NEW%' or Key_Number ='00000'")
        dsLostKeyNumber = dal.GetDataSet("Select isnull(Key_Number,'00000') FROM KeyLock WHERE Key_Number = '" + txtKey.Text + "'")
        If dsLostKeyNumber.Tables(0).Rows.Count > 0 Then
            If dsLostKeyNumber.Tables(0).Rows(0)(0).ToString() <> "" Then
                txtKey.BackColor = Drawing.Color.DarkRed
                lblLoskKey.Visible = True
                btnLostKey.Disabled = True
            End If
        Else
            txtKey.BackColor = System.Drawing.Color.White 'System.Drawing.ColorTranslator.FromHtml("#C0C0C0")
            lblLoskKey.Visible = False
            btnLostKey.Disabled = False
        End If

        If txtKey.Text = "0000" Then btnLostKey.Disabled = True Else btnLostKey.Disabled = False
        txtKetexp.Text = ds.Tables(0).Rows(cnt).Item("KEY_EXP").ToString()
        txtCard.Text = ds.Tables(0).Rows(cnt).Item("CARD_ID").ToString()
        Session("Card_ID") = txtCard.Text.Trim()


        'Added By Varun Moota to Check Whether Personnel ID is being Deleted or Not.10/12/2010
        Dim dsDeletedVeh As New DataSet
        Session("delPerValue") = "0"
        dsDeletedVeh = dal.GetDataSet("Select * FROM PERS WHERE [IDENTITY] = '" + txtPersonnelId.Text + "' AND NEWUPDT = 3")
        If dsDeletedVeh.Tables(0).Rows.Count > 0 Then
            btnDelete.BackColor = Drawing.Color.Red
            btnDelete.Text = "UnDelete"
            Session("delPerValue") = "1"
            lblDelPers.Visible = True
            'And also Add Key to the KeyLock Table
            'Added By Pritam to disable the buttons for deleted Personnel
            btnSave.Enabled = False
            btnLostKey.Disabled = True
            encodeKeyLink.Enabled = False
        Else
            btnDelete.Text = "Delete"
            btnDelete.BackColor = Drawing.Color.LightGray
            lblDelPers.Visible = False
            'Added By Pritam to Enable the buttons for Non-deleted Personnel
            btnSave.Enabled = True
            btnLostKey.Disabled = False
            encodeKeyLink.Enabled = True

        End If

        If txtCard.Text = "" Then btnLostCard.Disabled = True Else btnLostCard.Disabled = False
        txtCardexp.Text = ds.Tables(0).Rows(cnt).Item("CARD_EXP").ToString()
        txtAccId.Text = ds.Tables(0).Rows(cnt).Item("ACCT_ID").ToString()
        txtMI.Text = ds.Tables(0).Rows(cnt).Item("MI").ToString()
        txtText.Text = ds.Tables(0).Rows(cnt).Item("TEXT").ToString()
        Try
            DDLstDepartment.SelectedValue = ds.Tables(0).Rows(cnt).Item("DEPT").ToString()
            CBoxTemdisabled.Checked = ds.Tables(0).Rows(cnt).Item("LOCKED").ToString()
            CBoxRequiredIdentity.Checked = ds.Tables(0).Rows(cnt).Item("REQIDENTRY").ToString()
        Catch ex As Exception
            CBoxTemdisabled.Checked = ds.Tables(0).Rows(cnt).Item("LOCKED").ToString()
            CBoxRequiredIdentity.Checked = ds.Tables(0).Rows(cnt).Item("REQIDENTRY").ToString()
        End Try
        Session("visited") = True
        lblof.Text = cnt + 1 & " of " & PerTcount.ToString()
    End Sub

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            'Modified by Jatin Kshirsagar dated 29 aug 2008
            'Update
            If Not Session("AFlag") Is Nothing Then
                If Not Session("AFlag") = "Save" Then Session("AFlag") = "Add Another"
            Else
                Session("AFlag") = "Add Another"
            End If
            'If Not DDLstDepartment.SelectedIndex > 0 Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(),"javascript", "<script>alert('Please select department.');</script>")
            '    Exit Sub
            'End If

            'Added By varun Moota to Check the Duplicate Personnel Exist. 04/02/2010
            If Not PersonnelExists(txtPersonnelId.Text) Then
                Dim strCardID As String
                If Session("Card_ID") = Nothing Then
                    strCardID = ""
                Else
                    strCardID = Session("Card_ID").ToString()
                End If

                If (strCardID <> txtCard.Text.Trim()) Then
                    Dim dal = New GeneralizedDAL()
                    Dim parcollection(0) As SqlParameter
                    Dim ParPersIDENTITY = New SqlParameter("@CARD_ID", SqlDbType.VarChar, 10)
                    ParPersIDENTITY.Direction = ParameterDirection.Input
                    'Added By Varun to Check String Empty. 02/15/2010
                    If txtCard.Text = "" Or Nothing Then
                        ParPersIDENTITY.Value = DBNull.Value
                    Else
                        ParPersIDENTITY.Value = txtCard.Text.Trim()
                    End If

                    parcollection(0) = ParPersIDENTITY
                    Dim ds As New DataSet()

                    ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PersCheckCardExist", parcollection)
                    If Not ds Is DBNull.Value Then
                        If ds.Tables(0).Rows.Count <> 0 Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Duplicate card #.');</script>")
                            txtCard.Focus()
                        Else
                            SaveUpdateRecords()
                            CleartText()
                        End If
                    Else
                        SaveUpdateRecords()
                        CleartText()
                    End If
                Else
                    SaveUpdateRecords()
                    CleartText()
                End If



            Else
                Dim chkDuplicate As Boolean
                chkDuplicate = False

                Dim dal = New GeneralizedDAL()
                Dim parcollection(0) As SqlParameter
                Dim ParPersIDENTITY = New SqlParameter("@PersIDENTITY", SqlDbType.VarChar, 10)
                ParPersIDENTITY.Direction = ParameterDirection.Input
                ParPersIDENTITY.Value = txtPersonnelId.Text.Trim()
                parcollection(0) = ParPersIDENTITY
                Dim ds = New DataSet()
                ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PersCheckIDENTITYExist", parcollection)
                If Not ds Is DBNull.Value Then
                    If ds.Tables(0).Rows.Count <> 0 And txtPersonnelId.Enabled = True Then
                        chkDuplicate = True
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Duplicate Personnel ID.');</script>")
                        txtPersonnelId.Focus()
                    End If
                End If

                'Check for duplicate card id
                'Harshada 
                '4 Apr 09
                If chkDuplicate = False Then
                    ParPersIDENTITY = New SqlParameter("@CARD_ID", SqlDbType.VarChar, 10)
                    ParPersIDENTITY.Direction = ParameterDirection.Input

                    'Added By Varun to Check String Empty. 02/15/2010
                    If txtCard.Text = "" Or Nothing Then
                        ParPersIDENTITY.Value = DBNull.Value
                    Else
                        ParPersIDENTITY.Value = txtCard.Text.Trim()
                    End If

                    parcollection(0) = ParPersIDENTITY
                    ds = New DataSet()
                    ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PersCheckCardExist", parcollection)
                    If Not ds Is DBNull.Value Then
                        If ds.Tables(0).Rows.Count <> 0 And txtPersonnelId.Enabled = True Then
                            chkDuplicate = True
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Duplicate card #.');</script>")
                            txtCard.Focus()
                        End If
                    End If
                End If

                If chkDuplicate = False Then
                    SaveUpdateRecords()
                    ' ''CleartText()
                    '' ''Added By Varun Moota, to Add Another Personnel Without sending back to Search Screen.02/09/2010
                    ' ''Response.Redirect("Personnel_New_Edit.aspx", False)
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_new_edit_btnOk_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("~/Personnel.aspx", False)
            Session("Visited") = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel_new_edit.btnCancel_Click", ex)
        End Try


    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim dal = New GeneralizedDAL()
        Try
            'Changed By Varun Moota, to have Un Delete Funcitionality as Per customers request.10/12/2010
            If Session("delPerValue").ToString = "0" Then
                Dim parcollection(0) As SqlParameter
                Dim ParPersID = New SqlParameter("@PersID", SqlDbType.VarChar, 10)
                ParPersID.Direction = ParameterDirection.Input
                'Changed By Sudhanshu 06/26/2015: comment the below line and store the Identity Field for update because we are unable to delete and undelete the records while we use next and previous button.
                'ParPersID.Value = Convert.ToInt32(Request.QueryString.Get("PersID").Trim())
                ParPersID.Value = txtPersonnelId.Text
                '----------------------------------------------------------
                parcollection(0) = ParPersID
                Dim blnflag As Boolean
                blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_PersonnelDelete", parcollection)
                If blnflag = False Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record deleted Successfully.');location.href='Personnel.aspx';</script>")
                End If
            ElseIf Session("delPerValue").ToString = "1" Then
                Dim parcollection(0) As SqlParameter
                Dim ParPersID = New SqlParameter("@PersID", SqlDbType.VarChar, 10)
                ParPersID.Direction = ParameterDirection.Input
                'Changed By Sudhanshu 06/26/2015: : comment the below line and store the Identity Field for update because we are unable to delete and undelete the records while we use next and previous button.
                'ParPersID.Value = Convert.ToInt32(Request.QueryString.Get("PersID").Trim())
                ParPersID.Value = txtPersonnelId.Text
                '----------------------------------------------------------
                parcollection(0) = ParPersID
                Dim blnflag As Boolean
                blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_Personnel_UnDelete", parcollection)
                If blnflag = False Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Personnel Un Deleted sucessfully.');location.href='Personnel.aspx';</script>")
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
    Protected Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        '*********************** This event use for move recordset to first record**********************************
        Try
            Dim ds = New DataSet()
            Dim dal As New GeneralizedDAL
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PerDetails")
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("PersonnelId")}
            Session("PerDS") = ds
            Dim cnt As Integer = 0
            ds = Session("PerDS")
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = 0

            If (cnt < ds.Tables(0).Rows.Count) Then
                FillTextbox(cnt)
            End If
            Session("currentrecord") = "0"
            lblof.Text = "1 of " & Session("PerTCnt").ToString()
            btnprevious.Enabled = False
            btnFirst.Enabled = False
            btnNext.Enabled = True
            btnLast.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_new_edit_btnOk_Click_Insert", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Try
            Dim ds = New DataSet()
            Dim dal As New GeneralizedDAL
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PerDetails")
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("PersonnelId")}
            Session("PerDS") = ds
            Dim cnt As Integer = 0
            ds = Session("PerDS")
            cnt = Convert.ToInt32(Session("PerTCnt").ToString() - 1)
            If (cnt < ds.Tables(0).Rows.Count) Then
                FillTextbox(cnt)
            End If
            lblof.Text = Session("PerTCnt").ToString() & " of " & Session("PerTCnt").ToString()
            Session("currentrecord") = cnt

            btnNext.Enabled = False
            btnLast.Enabled = False

            btnprevious.Enabled = True
            btnFirst.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_new_edit_btnOk_Click_Insert", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprevious.Click
        Try
            Dim ds = New DataSet()
            Dim dal As New GeneralizedDAL
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PerDetails")
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("PersonnelId")}
            Session("PerDS") = ds

            Dim cnt As Integer = 0
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt - 1
            ds = Session("PerDS")
            If (cnt < ds.Tables(0).Rows.Count And cnt >= 0) Then
                FillTextbox(cnt)
                Session("currentrecord") = cnt
            End If
            lblof.Text = (cnt + 1) & " of " & Session("PerTCnt").ToString()

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
            cr.errorlog("Personnel_new_edit_btnOk_Click_Insert", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            Dim ds = New DataSet()
            Dim dal As New GeneralizedDAL
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PerDetails")
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("PersonnelId")}
            Session("PerDS") = ds
            Dim cnt As Integer = 0
            ds = Session("PerDS")
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt + 1

            If (cnt < ds.Tables(0).Rows.Count) Then
                FillTextbox(cnt)
                Session("currentrecord") = cnt
            End If

            lblof.Text = cnt + 1 & " of " & Session("PerTCnt").ToString()

            If Not btnFirst.Enabled Then
                btnFirst.Enabled = True
                btnprevious.Enabled = True
            End If

            If (cnt + 1 = Convert.ToInt32(Session("PerTCnt").ToString())) Then
                btnLast.Enabled = False
                btnNext.Enabled = False
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_new_edit_btnOk_Click_Insert", ex)
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


            '  If DDLstDepartment.SelectedIndex > 0 Then
            Session("AFlag") = "Save"
            btnOk_Click(sender, Nothing)
            'Else
            'Page.ClientScript.RegisterStartupScript(Me.GetType(),"javascript", "<script>alert('Please select department.');</script>")
            'End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel_new_edit.btnSave_Click", ex)
        End Try
    End Sub

    Protected Sub btnEncodeKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEncodeKey.Click
        Dim strKNo As String = ""
        Dim dal = New GeneralizedDAL()
        Try
            Dim ds As New DataSet
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PERS_MAX_KEY_NUMBER")
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)("KeyNo")) Then
                        strKNo = (Val(ds.Tables(0).Rows(0)(0)) + 1).ToString()
                        Session("KeyNumber") = strKNo.PadLeft(5, "0")
                    Else
                        strKNo = (1).ToString()
                        Session("KeyNumber") = strKNo.PadLeft(5, "0")
                    End If
                End If
            Else
                strKNo = (1).ToString()
                Session("KeyNumber") = strKNo.PadLeft(5, "0")
            End If

            Session("PersID") = txtPersonalIdHide.Text.Trim()
            Session("PID") = txtPersonnelId.Text.Trim

            'Session("KeyExp") = txtKetexp.Text.Trim
            'Changed by Harshada
            'To match with WinCC
            '29 Apr 09
            Session("KeyExp") = "NEVER"
            'Session("SysNo") = "1"
            'Changed by Harshada
            'To get system no from status table
            '08 May 09
            Dim sysno As String
            sysno = dal.ExecuteScalarGetString("select sysno from status")
            Session("SysNo") = sysno.Substring(1)

            If CBoxRequiredIdentity.Checked = True Then
                Session("SecondKey") = "Yes"
            ElseIf CBoxRequiredIdentity.Checked = False Then
                Session("SecondKey") = "No"
            End If

            Session("KeyType") = "1"
            Response.Redirect("Key_Encoder.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_btnEncodeKey_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Public Sub SaveUpdateRecords()
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            Dim parcollection(15) As SqlParameter
            Dim ParPersonalId = New SqlParameter("@PersonnelId", SqlDbType.Int)
            Dim ParIDENTITY = New SqlParameter("@IDENTITY", SqlDbType.VarChar, 10)
            Dim ParIDENTITY2 = New SqlParameter("@IDENTITY2", SqlDbType.VarChar, 10)
            Dim ParTEXT = New SqlParameter("@TEXT", SqlDbType.Text)
            Dim ParACCT_ID = New SqlParameter("@ACCT_ID", SqlDbType.VarChar, 11)
            Dim ParLAST_NAME = New SqlParameter("@LAST_NAME", SqlDbType.VarChar, 20)
            Dim ParFIRST_NAME = New SqlParameter("@FIRST_NAME", SqlDbType.VarChar, 15)
            Dim ParMI = New SqlParameter("@MI", SqlDbType.Char, 1)
            Dim ParDEPT = New SqlParameter("@DEPT", SqlDbType.VarChar, 5)
            Dim ParKEY_NUMBER = New SqlParameter("@KEY_NUMBER", SqlDbType.VarChar, 5)
            Dim ParCARD_ID = New SqlParameter("@CARD_ID", SqlDbType.VarChar, 7)
            Dim ParREQIDENTRY = New SqlParameter("@REQIDENTRY", SqlDbType.Bit)
            Dim ParLOCKED = New SqlParameter("@LOCKED", SqlDbType.Bit)
            Dim ParKEY_EXP = New SqlParameter("@KEY_EXP", SqlDbType.VarChar, 50)
            Dim ParCARD_EXP = New SqlParameter("@CARD_EXP", SqlDbType.VarChar, 50)
            Dim ParFlag = New SqlParameter("@Flag", SqlDbType.VarChar, 5)

            ParPersonalId.Direction = ParameterDirection.Input
            ParIDENTITY.Direction = ParameterDirection.Input
            ParIDENTITY2.Direction = ParameterDirection.Input
            ParTEXT.Direction = ParameterDirection.Input
            ParACCT_ID.Direction = ParameterDirection.Input
            ParLAST_NAME.Direction = ParameterDirection.Input
            ParFIRST_NAME.Direction = ParameterDirection.Input
            ParMI.Direction = ParameterDirection.Input
            ParDEPT.Direction = ParameterDirection.Input
            ParKEY_NUMBER.Direction = ParameterDirection.Input
            ParCARD_ID.Direction = ParameterDirection.Input
            ParREQIDENTRY.Direction = ParameterDirection.Input
            ParLOCKED.Direction = ParameterDirection.Input
            ParKEY_EXP.Direction = ParameterDirection.Input
            ParCARD_EXP.Direction = ParameterDirection.Input
            ParFlag.Direction = ParameterDirection.Input

            If (lblNew_Edit.Text = "Edit Personnel Information") Then
                ParFlag.Value = "Edit"
                ParPersonalId.Value = Convert.ToInt32(txtPersonalIdHide.Text.Trim())
            Else
                ParFlag.Value = "ADD"
                ParPersonalId.Value = 0
            End If

            ParIDENTITY.Value = txtPersonnelId.Text.Trim()
            ParIDENTITY2.Value = txtPersonnelID2.Text.Trim()
            ParTEXT.Value = txtText.Text.Trim()
            ParACCT_ID.Value = txtAccId.Text.Trim()
            ParLAST_NAME.Value = txtLastName.Text.Trim()
            ParFIRST_NAME.Value = txtFirstname.Text.Trim()
            ParMI.Value = txtMI.Text.Trim()
            ParDEPT.Value = DDLstDepartment.SelectedValue.Trim()
            ParKEY_NUMBER.Value = txtKey.Text.Trim()
            ParCARD_ID.Value = txtCard.Text.Trim()
            Dim iTempdisabled As Integer = 0
            Dim iReqID As Integer = 0
            If (CBoxTemdisabled.Checked = True) Then
                iTempdisabled = 1
            End If
            If (CBoxRequiredIdentity.Checked = True) Then
                iReqID = 1
            End If
            ParREQIDENTRY.Value = iReqID
            ParLOCKED.Value = iTempdisabled
            ParKEY_EXP.Value = txtKetexp.Text.Trim()
            ParCARD_EXP.Value = txtCardexp.Text.Trim()

            parcollection(0) = ParPersonalId
            parcollection(1) = ParIDENTITY
            parcollection(2) = ParIDENTITY2
            parcollection(3) = ParTEXT
            parcollection(4) = ParACCT_ID
            parcollection(5) = ParLAST_NAME
            parcollection(6) = ParFIRST_NAME
            parcollection(7) = ParMI
            parcollection(8) = ParDEPT
            parcollection(9) = ParKEY_NUMBER
            parcollection(10) = ParCARD_ID
            parcollection(11) = ParREQIDENTRY
            parcollection(12) = ParLOCKED
            parcollection(13) = ParKEY_EXP
            parcollection(14) = ParCARD_EXP
            parcollection(15) = ParFlag
            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_PersInsertUpdate", parcollection)
            If blnFlag = True Then
                If (lblNew_Edit.Text = "Edit Personnel Information") Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record updated successfully');</script>")

                ElseIf lblNew_Edit.Text = "New Personnel Information" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record saved successfully');</script>")
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record not updated successfully');</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_new_edit_btnOk_Click_Insert", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
    Private Function PersonnelExists(ByVal PerID As String) As Boolean
        Dim dal = New GeneralizedDAL()
        Try

            Dim parcollection(0) As SqlParameter
            Dim ParPersIDENTITY = New SqlParameter("@PersIDENTITY", SqlDbType.VarChar, 10)
            ParPersIDENTITY.Direction = ParameterDirection.Input
            ParPersIDENTITY.Value = PerID
            parcollection(0) = ParPersIDENTITY

            'Dim blnflg As Boolean
            'Added By Varun Moota to Check Duplicate VEHICLE ID. 03/08/2010
            Dim PerIDCheck As String = Nothing
            If Not Session("PerIDCheck") = Nothing Then
                PerIDCheck = Session("PerIDCheck").ToString.Trim()

                'ClearSession
                Session("PerIDCheck") = Nothing
            End If

            If txtPersonnelId.Text.Trim = PerIDCheck Then
                PersonnelExists = dal.ExecuteStoredProcedureGetBoolean("usp_tt_PersCheckIDENTITYExist", parcollection)
            Else
                PersonnelExists = dal.ExecuteStoredProcedureGetBoolean("usp_tt_PersCheckIDENTITYExist", parcollection)
            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_New_Edit.PersonnelExists", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If


        End Try
    End Function
    Public Sub CleartText()
        Try


            txtPersonnelId.Text = ""
            txtFirstname.Text = ""
            txtLastName.Text = ""
            txtKey.Text = ""
            txtKetexp.Text = ""
            txtCard.Text = ""
            txtCardexp.Text = ""
            txtAccId.Text = ""
            txtMI.Text = ""
            txtText.Text = ""
            If CBoxTemdisabled.Checked = True Then
                CBoxTemdisabled.Checked = False
            ElseIf CBoxRequiredIdentity.Checked = True Then
                CBoxRequiredIdentity.Checked = False
            End If
            txtPersonnelId.Focus()
            DDLstDepartment.SelectedIndex = 0
            'If Session("AFlag") = "Save" Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record Saved Successfully');location.href='Personnel.aspx';</script>")
            'ElseIf Session("AFlag") = "Add Another" Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record Saved Successfully');</script>")
            'End If
            Session.Remove("AFlag")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel_New_Edit.CleartText()", ex)
        End Try
    End Sub

    Protected Sub btnLostKey_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLostKey.ServerClick
        Try
            If txtLost.Value = "true" Then
                SaveLockDetails("K")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel_New_Edit.btnLostKey_ServerClick", ex)
        End Try

    End Sub

    Private Sub SaveLockDetails(ByVal LockType As String)
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            Dim parcollection(7) As SqlParameter
            Dim ParPersonalId = New SqlParameter("@PersonnelId", SqlDbType.Int)
            Dim ParIDENTITY = New SqlParameter("@IDENTITY", SqlDbType.VarChar, 10)
            Dim ParACCT_ID = New SqlParameter("@ACCT_ID", SqlDbType.VarChar, 11)
            Dim ParKEY_NUMBER = New SqlParameter("@KEY_NUMBER", SqlDbType.VarChar, 5)
            Dim ParCARD_ID = New SqlParameter("@CARD_ID", SqlDbType.VarChar, 7)
            Dim ParKEY_EXP = New SqlParameter("@KEY_EXP", SqlDbType.VarChar, 50)
            Dim ParCARD_EXP = New SqlParameter("@CARD_EXP", SqlDbType.VarChar, 50)
            Dim ParFlag = New SqlParameter("@Flag", SqlDbType.VarChar, 5)

            ParPersonalId.Direction = ParameterDirection.Input
            ParIDENTITY.Direction = ParameterDirection.Input
            ParACCT_ID.Direction = ParameterDirection.Input
            ParKEY_NUMBER.Direction = ParameterDirection.Input
            ParCARD_ID.Direction = ParameterDirection.Input
            ParKEY_EXP.Direction = ParameterDirection.Input
            ParCARD_EXP.Direction = ParameterDirection.Input
            ParFlag.Direction = ParameterDirection.Input

            ParPersonalId.Value = Convert.ToInt32(txtPersonalIdHide.Text.Trim())
            ParFlag.Value = LockType
            ParIDENTITY.Value = txtPersonnelId.Text.Trim()
            ParACCT_ID.Value = txtAccId.Text.Trim()
            ParKEY_NUMBER.Value = txtKey.Text.Trim()
            ParCARD_ID.Value = txtCard.Text.Trim()
            ParKEY_EXP.Value = txtKetexp.Text.Trim()
            ParCARD_EXP.Value = txtCardexp.Text.Trim()

            parcollection(0) = ParPersonalId
            parcollection(1) = ParIDENTITY
            parcollection(2) = ParACCT_ID
            parcollection(3) = ParKEY_NUMBER
            parcollection(4) = ParCARD_ID
            parcollection(5) = ParKEY_EXP
            parcollection(6) = ParCARD_EXP
            parcollection(7) = ParFlag

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_PerLockCardKey", parcollection)
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>location.href='Personnel.aspx';</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_New_Edit.SaveLockDetails", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnLostCard_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLostCard.ServerClick
        Try
            If txtLost.Value = "true" Then
                SaveLockDetails("C")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel_New_Edit.btnLostCard_ServerClick", ex)
        End Try

    End Sub
End Class
