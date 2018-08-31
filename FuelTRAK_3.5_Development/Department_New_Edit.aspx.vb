Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class Department_New_Edit
    Inherits System.Web.UI.Page
    'Update and re-arrange the code by Jatin Kshirsagar as on 28 aug 2008
    Dim btnchk As Boolean
    Dim RecordUpdate As Boolean
    Dim DAL As GeneralizedDAL
    Dim ds As DataSet
    Dim DeptTCnt As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '**************** check for session is null*********************
        'Commented By Varun Moota, Since we Need DeptID to be Editable and Unique. 07/21/2010
        'txtDeptNo.Attributes.Add("readonly", "readonly")
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                'Commented By Varun Moota to Handle Surcharge.
                ' ''txtSurchage.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                ' ''txtSurchage.Attributes.Add("onkeyup", "KeyUpEvent_txtSurchage(event);")
                If Not Session("visited") Or Not IsPostBack Then
                    RecordUpdate = False
                    tblEditDept.Visible = False
                    btnFirst.Visible = False
                    btnLast.Visible = False
                    btnprevious.Visible = False
                    btnNext.Visible = False
                    lblof.Visible = False
                    '****************************if edit the data then u ll enter in below loop********************
                    If (Request.QueryString.Count = 1 And Not IsPostBack) Then
                        trNewNo.Visible = True
                        FillDeptInfo()
                        'txtDeptNo.Enabled = False
                    ElseIf (Not IsPostBack) Then
                        tblEditDept.Visible = True
                        Label1.Text = "New Department Information"
                        trNewNo.Visible = True
                        txtName.Text = ""
                        txtAddress1.Text = ""
                        txtAddress2.Text = ""

                        'By Soham Gangavane Aug 21, 2017
                        txtSurchageGallon.Text = ""

                        txtSurchage.Text = ""
                        txtAccNo.Text = ""
                        txtUploadcode.Text = ""

                        txtDeptNo.Visible = True
                        txtName.Visible = True
                        txtAddress1.Visible = True
                        txtAddress2.Visible = True

                        'By Soham Gangavane Aug 21, 2017
                        txtSurchageGallon.Visible = True

                        txtSurchage.Visible = True
                        txtAccNo.Visible = True
                        txtUploadcode.Visible = True
                    End If
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String

            cr.errorlog("Department_new_edit_Page_Load", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Public Sub FillDeptInfo()
        Try
            btnOk.Visible = False

            'Retrive count Department records.
            DAL = New GeneralizedDAL
            ds = New DataSet
            Dim Adp As New SqlDataAdapter
            Adp = DAL.ExecuteStoredProcedureGetDataAdapter("usp_tt_DeptRecCount")
            Adp.Fill(ds)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    DeptTCnt = Convert.ToInt32(ds.Tables(0).Rows(0)(0))
                End If
            End If
            ds = New DataSet
            Adp = DAL.ExecuteStoredProcedureGetDataAdapter("usp_tt_DeptDetails")

            Dim cnt As Long = 0
            Adp.Fill(ds)
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns(0)}
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dr As DataRow = ds.Tables(0).Rows.Find(Request.QueryString(0).ToString())
                    If Not IsDBNull(dr) Then
                        cnt = ds.Tables(0).Rows.IndexOf(dr)
                    End If

                    Session("currentrecord") = cnt.ToString()
                    Session("visited") = True
                    Session("DeptDS") = ds
                    Session("DeptTCnt") = DeptTCnt
                    txtDeptNo.Text = ds.Tables(0).Rows(cnt).Item("NUMBER")
                    'Added By Varun to set Session Values
                    Session("DeptNo") = txtDeptNo.Text

                    txtDeptNoHide.Text = ds.Tables(0).Rows(cnt).Item("DeptID")
                    txtName.Text = ds.Tables(0).Rows(cnt).Item("NAME")
                    txtAddress1.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS1"))
                    txtAddress2.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS2"))
                    'By Soham Gangavane Aug 21, 2017
                    txtSurchageGallon.Text = If(Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGEPERGALLON")) = "", "0", Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGEPERGALLON")))
                    txtSurchage.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGE"))
                    txtAccNo.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ACCT_ID"))
                    txtUploadcode.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("CODE"))
                    lblof.Text = cnt + 1 & " of " & DeptTCnt
                End If
            End If
            tblEditDept.Visible = True
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
            ElseIf cnt + 1 = DeptTCnt Then
                lblof.Visible = True
                btnprevious.Enabled = True
                btnNext.Enabled = False
                btnFirst.Enabled = True
                btnLast.Enabled = False
            ElseIf cnt + 1 < DeptTCnt Then
                lblof.Visible = True
                btnprevious.Enabled = True
                btnNext.Enabled = True
                btnFirst.Enabled = True
                btnLast.Enabled = True
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String

            cr.errorlog("Department_new_edit_FillDeptInfo()", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Public Sub clrtxt()
        Try

        
            txtName.Text = ""
            txtAddress1.Text = ""
            txtAddress2.Text = ""
            'By Soham Gangavane Aug 21, 2017
            txtSurchageGallon.Visible = ""
            txtSurchage.Text = ""
            txtAccNo.Text = ""
            txtUploadcode.Text = ""
            txtDeptNo.Text = ""
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Department_New_Edit.clrtxt()", ex)
        End Try
    End Sub

    Protected Sub First_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        '*********************** This event use for move recordset to first record**********************************
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            ds = Session("DeptDS")
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = 0

            If (cnt < ds.Tables(0).Rows.Count) Then
                txtDeptNo.Text = ds.Tables(0).Rows(cnt).Item("NUMBER")
                txtDeptNoHide.Text = ds.Tables(0).Rows(cnt).Item("DeptID")
                txtName.Text = ds.Tables(0).Rows(cnt).Item("NAME")
                txtAddress1.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS1"))
                txtAddress2.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS2"))
                'By Soham Gangavane Aug 21, 2017
                txtSurchageGallon.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGEPERGALLON"))
                txtSurchage.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGE"))
                txtAccNo.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ACCT_ID"))
                txtUploadcode.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("CODE"))
            End If
            Session("currentrecord") = "0"
            lblof.Text = "1 of " & Session("DeptTCnt").ToString()
            tblEditDept.Visible = True
            btnFirst.Visible = True
            btnLast.Visible = True
            btnprevious.Visible = True
            btnNext.Visible = True
            lblof.Visible = True

            btnprevious.Enabled = False
            btnFirst.Enabled = False

            btnNext.Enabled = True
            btnLast.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String

            cr.errorlog("Department_new_edit_First_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try


    End Sub

    Protected Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLast.Click
        '*********************** This event use for move recordset to last record**********************************
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            ds = Session("DeptDS")
            cnt = Convert.ToInt32(Session("DeptTCnt").ToString() - 1)
            'cnt = cnt + 1
            If (cnt < ds.Tables(0).Rows.Count) Then
                txtDeptNo.Text = ds.Tables(0).Rows(cnt).Item("NUMBER")
                txtDeptNoHide.Text = ds.Tables(0).Rows(cnt).Item("DeptID")
                txtName.Text = ds.Tables(0).Rows(cnt).Item("NAME")
                txtAddress1.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS1"))
                txtAddress2.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS2"))
                'By Soham Gangavane Aug 21, 2017
                txtSurchageGallon.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGEPERGALLON"))
                txtSurchage.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGE"))
                txtAccNo.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ACCT_ID"))
                txtUploadcode.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("CODE"))
            End If
            lblof.Text = Session("DeptTCnt").ToString() & " of " & Session("DeptTCnt").ToString()
            Session("currentrecord") = (Convert.ToInt32(Session("DeptTCnt").ToString()) - 1).ToString()
            tblEditDept.Visible = True
            btnFirst.Visible = True
            btnLast.Visible = True
            btnprevious.Visible = True
            btnNext.Visible = True
            lblof.Visible = True

            btnNext.Enabled = False
            btnLast.Enabled = False

            btnprevious.Enabled = True
            btnFirst.Enabled = True

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String

            cr.errorlog("Department_new_edit_btnLast_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        '*********************** This event use for move recordset to first record**********************************
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            ds = Session("DeptDS")
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt + 1

            If (cnt < ds.Tables(0).Rows.Count) Then
                txtDeptNo.Text = ds.Tables(0).Rows(cnt).Item("NUMBER")
                txtDeptNoHide.Text = ds.Tables(0).Rows(cnt).Item("DeptID")
                txtName.Text = ds.Tables(0).Rows(cnt).Item("NAME")
                txtAddress1.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS1"))
                txtAddress2.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS2"))
                'By Soham Gangavane Aug 21, 2017
                txtSurchageGallon.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGEPERGALLON"))
                txtSurchage.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGE"))
                txtAccNo.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ACCT_ID"))
                txtUploadcode.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("CODE"))
                Session("currentrecord") = cnt
            End If

            lblof.Text = cnt + 1 & " of " & Session("DeptTCnt").ToString()
            If cnt + 1 = Convert.ToInt32(Session("DeptTCnt").ToString()) Then
                btnNext.Enabled = False
                btnLast.Enabled = False
            End If
            btnprevious.Enabled = True
            btnFirst.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String

            cr.errorlog("Department_new_edit_btnNext_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprevious.Click
        '*********************** This event use for move recordset to first record**********************************
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt - 1
            ds = Session("DeptDS")
            If (cnt < ds.Tables(0).Rows.Count And cnt >= 0) Then
                txtDeptNo.Text = ds.Tables(0).Rows(cnt).Item("NUMBER")
                txtDeptNoHide.Text = ds.Tables(0).Rows(cnt).Item("DeptID")
                txtName.Text = ds.Tables(0).Rows(cnt).Item("NAME")
                txtAddress1.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS1"))
                txtAddress2.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ADDRESS2"))
                'By Soham Gangavane Aug 21, 2017
                txtSurchageGallon.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGEPERGALLON"))
                txtSurchage.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("SURCHARGE"))
                txtAccNo.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("ACCT_ID"))
                txtUploadcode.Text = Convert.ToString(ds.Tables(0).Rows(cnt).Item("CODE"))
                Session("currentrecord") = cnt
            End If

            lblof.Text = cnt + 1 & " of " & Session("DeptTCnt").ToString()
            If (cnt + 1) = 1 Then
                btnFirst.Enabled = False
                btnprevious.Enabled = False
            End If
            btnNext.Enabled = True
            btnLast.Enabled = True

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String

            cr.errorlog("Department_new_edit_btnprevious_Click", ex)
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
            Response.Redirect("Department.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Department_New_Edit.btnCancel_Click", ex)
        End Try

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        '*********************** This event use for save record and move to next window**********************************
        Try
            If (btnchk = False) Then
                Session("AFlag") = "Save"
                btnOk_Click(sender, Nothing)
                btnchk = True
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Department_New_Edit.btnSave_Click", ex)
        End Try

      
    End Sub

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            If Not Session("AFlag") Is Nothing Then
                If Not Session("AFlag") = "Save" Then Session("AFlag") = "Add Another"
            Else
                Session("AFlag") = "Add Another"
            End If

            If (Label1.Text = "Edit Department Information") Then 'Department Edit Screen
                If Not DeptIDExists(txtDeptNo.Text) Then
                    SaveUpdateRecords(txtDeptNo.Text)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Department Number Already Exists.');</script>")
                End If
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Records Updated Successfully');location.href='Department.aspx';</script>")
            Else

                If Not DeptIDExists(txtDeptNo.Text) Then
                    SaveUpdateRecords(txtDeptNo.Text)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Department Number Already Exists.');location.href='Department_New_Edit.aspx';</script>")
                End If

                If Session("AFlag") = "Save" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Records Added Successfully');location.href='Department.aspx';</script>")
                ElseIf Session("AFlag") = "Add Another" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Records Added Successfully');location.href='Department_New_Edit.aspx';</script>")
                End If
                Session.Remove("AFlag")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Department_New_Edit.btnOk_Click", ex)
        End Try
    End Sub

    Public Sub SaveUpdateRecords(ByVal deptno As String)
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            'By Soham Gangavane Aug 21, 2017
            Dim parcollection(9) As SqlParameter
            Dim ParDeptID = New SqlParameter("@DeptID", SqlDbType.Int)
            Dim ParDeptNAME = New SqlParameter("@DeptNAME", SqlDbType.VarChar, 25)
            Dim ParDeptNUMBER = New SqlParameter("@DeptNUMBER", SqlDbType.VarChar, 5)
            Dim ParDeptADDRESS1 = New SqlParameter("@DeptADDRESS1", SqlDbType.VarChar, 25)
            Dim ParDeptADDRESS2 = New SqlParameter("@DeptADDRESS2", SqlDbType.VarChar, 25)
            'By Soham Gangavane Aug 21, 2017
            Dim ParSURCHARGEPERGALLON = New SqlParameter("@SURCHARGEPERGALLON", SqlDbType.Float)
            Dim ParSURCHARGE = New SqlParameter("@SURCHARGE", SqlDbType.Float)
            Dim ParCODE = New SqlParameter("@CODE", SqlDbType.VarChar, 30)
            Dim ParACCT_ID = New SqlParameter("@ACCT_ID", SqlDbType.VarChar, 10)
            Dim ParFlag = New SqlParameter("@Flag", SqlDbType.VarChar, 5)

            ParDeptID.Direction = ParameterDirection.Input
            ParDeptNAME.Direction = ParameterDirection.Input
            ParDeptNUMBER.Direction = ParameterDirection.Input
            ParDeptADDRESS1.Direction = ParameterDirection.Input
            ParDeptADDRESS2.Direction = ParameterDirection.Input
            'By Soham Gangavane Aug 21, 2017
            ParSURCHARGEPERGALLON.Direction = ParameterDirection.Input
            ParSURCHARGE.Direction = ParameterDirection.Input
            ParCODE.Direction = ParameterDirection.Input
            ParACCT_ID.Direction = ParameterDirection.Input
            ParFlag.Direction = ParameterDirection.Input

            If (Label1.Text = "Edit Department Information") Then 'Department Edit Screen
                ParFlag.Value = "Edit"
                ParDeptID.Value = Convert.ToInt32(Val(txtDeptNoHide.Text.Trim()))
            Else
                ParFlag.Value = "ADD"
                ParDeptID.Value = 0
            End If

            ParDeptNAME.Value = txtName.Text.Trim()
            'ParDeptNUMBER.Value = txtDeptNo.Text.Trim()
            ParDeptNUMBER.Value = deptno
            ParDeptADDRESS1.Value = txtAddress1.Text.Trim()
            ParDeptADDRESS2.Value = txtAddress2.Text.Trim()
            'By Soham Gangavane Aug 21, 2017
            ParSURCHARGEPERGALLON.Value = Convert.ToDouble(Val(txtSurchageGallon.Text.Trim()))
            ParSURCHARGE.Value = Convert.ToDouble(Val(txtSurchage.Text.Trim()))
            ParCODE.Value = txtUploadcode.Text.Trim()
            ParACCT_ID.Value = txtAccNo.Text.Trim()

            parcollection(0) = ParDeptID
            parcollection(1) = ParDeptNAME
            parcollection(2) = ParDeptNUMBER
            parcollection(3) = ParDeptADDRESS1
            parcollection(4) = ParDeptADDRESS2
            'By Soham Gangavane Aug 21, 2017
            parcollection(5) = ParSURCHARGEPERGALLON
            parcollection(6) = ParSURCHARGE
            parcollection(7) = ParCODE
            parcollection(8) = ParACCT_ID
            parcollection(9) = ParFlag

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_DeptInsertUpdate", parcollection)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Department_New_Edit.SaveUpdateRecords", ex)
        End Try
       
    End Sub

    Private Function DeptIDExists(ByVal DeptNo As String) As Boolean
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParDeptNo = New SqlParameter("@DeptNUMBER", SqlDbType.VarChar, 3)
            ParDeptNo.Direction = ParameterDirection.Input
            ParDeptNo.Value = DeptNo
            parcollection(0) = ParDeptNo

            Dim strDeptIDCheck As String = Nothing
            If Not Session("DeptNo") = Nothing Then
                strDeptIDCheck = Session("DeptNo").ToString.Trim()

            End If

            If txtDeptNo.Text.Trim = strDeptIDCheck Then

                Return False
            Else

                DeptIDExists = dal.ExecuteStoredProcedureGetBoolean("usp_tt_DeptCheckNumberExist", parcollection)
            End If

            Return DeptIDExists

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Sentry_New_Edit.SentryIDExists", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If


        End Try
    End Function
    
End Class
