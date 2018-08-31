Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient


Partial Class Transaction_New_Edit
    Inherits System.Web.UI.Page
    Dim btnchk As Boolean
    Dim sqldataset As New DataSet
    Dim strPersNum As String
    Dim TXTNTCount As Integer
    Dim Hose As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DS As DataSet = New DataSet()
        Dim dal = New GeneralizedDAL()
        Try
            OpenPopUp(Button1, "PersPopUp.aspx")
            'OpenVehPopUp(btnVehicle)
            '**************** Check for session is null/not*********************
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                txtNumber.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                'txtVehicle.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                txtOdometer.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                txtHours.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                txtTime.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                'txtQty.Attributes.Add("OnKeyPress", "KeyPressEvent_txtQty(event);")
                txtDate.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                'Added By Varun Moota
                txtName.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                txtName.Attributes.Add("onkeyup", "KeyUpEvent_txtName(event);")
                'txtDate.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                txtDate.Attributes.Add("onkeyup", "KeyUpEvent_txtDate(event,'txtDate');")
                txtOdometer.Attributes.Add("onkeyup", "KeyUpEvent_txtOdometer(event);")
                txtHours.Attributes.Add("onkeyup", "KeyUpEvent_txtHours(event);")
                txtTime.Attributes.Add("onkeyup", "KeyUpEvent_txtTime(event,'txtTime');")
                txtDate.Attributes.Add("onkeyup", "KeyUpEvent_txtDate(event,'txtDate');")
                'txtSentry.Attributes.Add("OnBlur", "EnterSentry();")
                'Allowed decimal places in quantity
                '28 Apr 09
                'Harshada
                'Issue no #139
                'txtQty.Attributes.Add("onkeyup", "KeyUpEvent_txtQty1(event)")
                txtCost.Attributes.Add("OnKeyPress", "TrapKey(event,'txtCost');")
                txtCost.Attributes.Add("onkeyup", "KeyUpEvent_txtCost(event);")
                txtVechKey.Attributes.Add("Readonly", "Readonly")
                txtDept.Attributes.Add("Readonly", "Readonly")
                txtTank.Attributes.Add("Readonly", "Readonly")
                txtCost.Attributes.Add("Readonly", "Readonly")

                Me.txtQty.Attributes.Add("onchange", "javascript:CheckCost();")

                If Not Page.IsPostBack Then
                    Session("HoseValue") = ""
                    DDLstSentry.Items.Clear()
                    DS = New DataSet()
                    DS = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransPopulateSentryList")
                    If Not DS Is Nothing Then
                        If DS.Tables(0).Rows.Count > 0 Then
                            DDLstSentry.DataSource = DS.Tables(0)
                            DDLstSentry.DataTextField = "SentryNoName"
                            DDLstSentry.DataValueField = "NUMBER"
                            DDLstSentry.DataBind()
                        End If
                    End If
                    If CheckSentry(DDLstSentry.SelectedValue.Trim()) = True Then FillHoseList()
                End If

                'Code modified by Jatin Kshirsagar as on 02 Sept 2008
                If Not Session("visited") Or Not IsPostBack Then
                    If (Request.QueryString.Count = 1 And Not IsPostBack) Then
                        lblNew_Edit.Text = "Edit Transaction Information"
                        btnOk.Visible = False
                        'Get Transaction list and TotalCount
                        DS = New DataSet
                        Dim parcollection(0) As SqlParameter
                        Dim ParRecord = New SqlParameter("@Rec", SqlDbType.Int)
                        ParRecord.Direction = ParameterDirection.Input
                        ParRecord.Value = Request.QueryString.Item(0).ToString()
                        parcollection(0) = ParRecord
                        DS = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TXTNRecords", parcollection)

                        DS.Tables(0).PrimaryKey = New DataColumn() {DS.Tables(0).Columns("RECORD")}
                        Dim cnt As Long = 0
                        If Not DS Is Nothing Then
                            If DS.Tables(0).Rows.Count > 0 Then
                                Dim dr As DataRow = DS.Tables(0).Rows.Find(Request.QueryString(0).ToString())
                                If Not IsDBNull(dr) Then cnt = DS.Tables(0).Rows.IndexOf(dr)
                                Session("TXTNDS") = DS
                                Session("currentrecord") = cnt '+ 1
                                Session("TXTNTCnt") = DS.Tables(1).Rows(0)(0).ToString()
                                TXTNTCount = DS.Tables(1).Rows(0)(0).ToString()
                                FillRecords(cnt)
                            End If
                            If DS.Tables(1).Rows.Count > 0 Then
                                Session("TankInvTCnt") = DS.Tables(1).Rows(0)(0).ToString()
                                'lblof.Text = cnt + 1 & " " & "of" & " " & DS.Tables(1).Rows(0)(0).ToString()
                                Dim valu As String = DS.Tables(1).Rows(0)(0).ToString()
                                'Dim rCount As Integer = DS.Tables(1).Rows.Count
                                lblof.Text = cnt + 1 & " " & "of" & " " & DS.Tables(1).Rows(0)(0).ToString()
                            End If
                        End If
                        btnFirst.Visible = True : btnLast.Visible = True : btnprevious.Visible = True : btnNext.Visible = True : lblof.Visible = True

                        If cnt + 1 = 1 Then
                            lblof.Visible = True
                            btnprevious.Enabled = False
                            btnNext.Enabled = True
                            btnFirst.Enabled = False
                            btnLast.Enabled = True
                        ElseIf cnt + 1 = TXTNTCount Then
                            lblof.Visible = True
                            btnprevious.Enabled = True
                            btnNext.Enabled = False
                            btnFirst.Enabled = True
                            btnLast.Enabled = False
                        ElseIf cnt + 1 < TXTNTCount Then
                            lblof.Visible = True
                            btnprevious.Enabled = True
                            btnNext.Enabled = True
                            btnFirst.Enabled = True
                            btnLast.Enabled = True
                        End If
                    ElseIf (Not IsPostBack) Then
                        'Changed default no to *MAN* 27 Apr 09 Harshada Issue no #132
                        txtNumber.Text = "*MAN*"
                        txtVechKey.Text = "0000"
                        lblNew_Edit.Text = "New Transaction Information"
                        'Populate Sentry DropDown 07 May 09 Harshada Issue #130
                        btnFirst.Visible = False
                        btnLast.Visible = False
                        btnprevious.Visible = False
                        btnNext.Visible = False
                        lblof.Visible = False
                        lblErr.Visible = False
                        txtErrors.Visible = False

                        'By Soham Gangavane March 08,2018
                        'lblCost.Visible = False
                        'txtCost.Visible = False

                    End If
                End If
            End If

            'Fill Sentry List in txtHSentry
            'If Not txtSentryId.Value.Trim() = "" Then
            'If Not DDLstSentry.Items.Count = 0 Then
            '    If (Not Page.IsPostBack) Then
            '        'If CheckSentry(txtSentryId.Value.Trim()) = True Then
            '        If CheckSentry(DDLstSentry.SelectedValue.Trim()) = True Then
            '            'Session("SName") = txtSentryName.Text.Trim()
            '            'txtSentry.Text = txtSentryId.Value
            '            FillHoseList()
            '            'txtSentryId.Value = ""
            '            'txtSentryName.Text = Session("SName")
            '        End If
            '    End If
            'End If

            'If Not txtVehicle.Text.Trim() = "" Then
            '    If (Page.IsPostBack) Then
            '        ' If CheckSentry(txtSentryId.Value.Trim()) = True Then
            '        If VehicleExists(txtVehicle.Text.Trim()) = True Then
            '            GetDeptKeyNoAndVehicleName(txtVehicle.Text.Trim())
            '            'FillHoseList()
            '            'txtSentryId.Value = ""
            '        End If
            '    End If
            'End If
            If (Page.IsPostBack) Then
                txtName.Text = txtPersNum.Value.ToString()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_New_Edit_Page_load", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Sub GetRecords(ByVal Product As String, ByVal pump As String, ByVal Sentry As String)
        Dim DS As DataSet = New DataSet()
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(2) As SqlParameter
            Dim ParProduct = New SqlParameter("@Product", SqlDbType.VarChar, 2)
            Dim Parpump = New SqlParameter("@PumpField", SqlDbType.VarChar, 15)
            Dim ParSentry = New SqlParameter("@SentryNUMBER", SqlDbType.VarChar, 10)

            ParProduct.Direction = ParameterDirection.Input
            Parpump.Direction = ParameterDirection.Input
            ParSentry.Direction = ParameterDirection.Input

            ParProduct.Value = Product
            Parpump.Value = "pump" & Right(pump, 1) & "_tank"
            If Sentry = "" Then ParSentry.Value = "000" Else ParSentry.Value = Sentry

            parcollection(0) = ParProduct
            parcollection(1) = Parpump
            parcollection(2) = ParSentry

            DS = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TXTNGetTXTNRecords", parcollection)
            If Not DS Is Nothing Then
                If DS.Tables(0).Rows.Count > 0 Then
                    txtProd.Text = DS.Tables(0).Rows(0)(0).ToString()
                End If
                If DS.Tables(1).Rows.Count > 0 Then
                    txtTank.Text = DS.Tables(1).Rows(0)(0).ToString()
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_new_edit_GetRecords", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Sub FillRecords(ByVal cnt As Integer)
        Try
            Dim ds = New DataSet()
            ds = CType(Session("TXTNDS"), DataSet)
            TXTNTCount = Session("TXTNTCnt")
            Session("currentrecord") = cnt

            txtTime.Text = Format(ds.Tables(0).Rows(cnt)("DATETIME"), "HH:mm").ToString() 'Format(sqlReader(1), "HH:mm")
            txtDate.Text = Format(ds.Tables(0).Rows(cnt)("DATETIME"), "MM/dd/yyyy").ToString() 'Format(sqlReader(1), "MM/dd/yyyy")
            txtOdometer.Text = ds.Tables(0).Rows(cnt)("MILES").ToString() 'sqlReader(3).ToString()
            txtHours.Text = ds.Tables(0).Rows(cnt)("HOURS").ToString() 'sqlReader(4).ToString()
            txtQty.Text = ds.Tables(0).Rows(cnt)("QUANTITY").ToString() 'sqlReader(6).ToString()
            txtDept.Text = ds.Tables(0).Rows(cnt)("Dept").ToString()

            txtNumber.Text = ds.Tables(0).Rows(cnt)("NUMBER").ToString() 'sqlReader(8).ToString()
            txtVechKey.Text = ds.Tables(0).Rows(cnt)("VKEY").ToString() 'sqlReader(9).ToString()
            txtErrors.Text = ds.Tables(0).Rows(cnt)("ERRORS").ToString() 'sqlReader(7).ToString()
            GetRecords(ds.Tables(0).Rows(cnt)("PRODUCT").ToString(), ds.Tables(0).Rows(cnt)("PUMP").ToString(), ds.Tables(0).Rows(cnt)("SENTRY").ToString())
            Dim i As Integer = 0
            For i = 0 To DDLstSentry.Items.Count - 1
                If (DDLstSentry.Items(i).Value = ds.Tables(0).Rows(cnt)("SENTRY").ToString()) Then
                    'DDLstSentry.Items(i).Selected = True
                    DDLstSentry.SelectedIndex = i
                    DDLstSentry_SelectedIndexChanged(Nothing, Nothing)
                    Exit For
                End If
            Next

            For i = 0 To DDLstHose.Items.Count - 1
                If (DDLstHose.Items(i).Value = ds.Tables(0).Rows(cnt)("Pump").ToString()) Then
                    ' DDLstHose.Items(i).Selected = True
                    DDLstHose.SelectedIndex = i
                    Session("HoseValue") = DDLstHose.SelectedValue.ToString()
                    Exit For
                End If
            Next
            DDLstHose_SelectedIndexChanged(Nothing, Nothing)
            If ds.Tables(0).Rows(cnt)("PERSONNEL").ToString() <> "" Then 'sqlReader(11).ToString().Trim() <> "" Then
                Dim flg As Boolean = False
                txtPersNum.Value = ds.Tables(0).Rows(cnt)("PERSONNEL").ToString() 'sqlReader(11).ToString().Trim()
                'Added By Varun Moota as Per John to Check if Personnel Exists in DB. 12/20/2010.
                If ds.Tables(0).Rows(cnt)("LAST_NAME").ToString = "" Or ds.Tables(0).Rows(cnt)("FIRST_NAME").ToString() = "" Then
                    txtName.Text = "NOT AVAILABLE"
                Else
                    txtName.Text = ds.Tables(0).Rows(cnt)("LAST_NAME").ToString + "," + ds.Tables(0).Rows(cnt)("FIRST_NAME").ToString() + " " + ds.Tables(0).Rows(cnt)("MI").ToString() + " : " + Left(ds.Tables(0).Rows(cnt)(11).ToString(), 2)
                End If


            Else
                txtName.Text = ""
            End If
                txtCost.Text = ds.Tables(0).Rows(cnt)("COST").ToString() 'sqlReader(12).ToString().Trim()
                txtoptional.Text = ds.Tables(0).Rows(cnt)("OPTION").ToString() 'sqlReader(13).ToString().Trim()
            i = 0

            txtVehID.Text = ds.Tables(0).Rows(cnt)("VEHICLE").ToString()
            ' ''For i = 0 To DDLstVehicle.Items.Count - 1
            ' ''    If (DDLstVehicle.Items(i).Value = ds.Tables(0).Rows(cnt)("VEHICLE").ToString()) Then
            ' ''        DDLstVehicle.SelectedIndex = i
            ' ''        DDLstVehicle_SelectedIndexChanged(Nothing, Nothing)
            ' ''        Exit For

            ' ''    End If
            ' ''Next



        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_New_Edit_FillRecords", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Private Sub GetDeptKeyNoAndVehicleName(ByVal strVehNumber As String)
        Dim dal = New GeneralizedDAL()
        Dim ds As New DataSet
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParVehNumber = New SqlParameter("@VehNumber", SqlDbType.NVarChar, 10)
            ParVehNumber.Direction = ParameterDirection.Input
            ParVehNumber.Value = strVehNumber
            parcollection(0) = ParVehNumber
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransDeptKeyNoAndVehicleName", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    txtDesc.Text = ds.Tables(0).Rows(0)("VehDesc").ToString()
                    txtVechKey.Text = ds.Tables(0).Rows(0)("KEY_NUMBER").ToString()
                    txtDept.Text = ds.Tables(0).Rows(0)("DEPTName").ToString()
                    txtHidDept.Value = ds.Tables(0).Rows(0)("DEPTNumber").ToString()
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_new_edit_btnDelete_Click_Insert", ex)
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
            Response.Redirect("Transaction.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_new_edit.btnCancel_Click", ex)
        End Try
    End Sub

    Public Sub SaveRecords(ByVal Aflag As String)
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            'By Soham Gangavane Aug 29,2017
            Dim parcollection(23) As SqlParameter
            Dim ParRECORD = New SqlParameter("@RECORD", SqlDbType.Int)
            Dim ParSENTRY = New SqlParameter("@SENTRY", SqlDbType.VarChar, 3)
            Dim ParNUMBER = New SqlParameter("@NUMBER", SqlDbType.VarChar, 6)
            Dim ParVEHICLE = New SqlParameter("@VEHICLE", SqlDbType.VarChar, 10)
            Dim ParMILES = New SqlParameter("@MILES", SqlDbType.Decimal)
            'Added parameter for prev miles, MPG
            '30 Apr 09
            'Harshada
            'Issue #89
            Dim parPrevMiles = New SqlParameter("@PREV_MILES", SqlDbType.Decimal)
            Dim parMPG = New SqlParameter("@MPG", SqlDbType.Decimal)
            parMPG.Precision = 3
            parMPG.Scale = 1
            Dim ParDATETIME = New SqlParameter("@DATETIME", SqlDbType.DateTime)
            Dim ParPUMP = New SqlParameter("@PUMP", SqlDbType.VarChar, 2)
            Dim ParQUANTITY = New SqlParameter("@QUANTITY", SqlDbType.Decimal)
            Dim ParPERSONNEL = New SqlParameter("@PERSONNEL", SqlDbType.VarChar, 10)
            Dim ParERRORS = New SqlParameter("@ERRORS", SqlDbType.VarChar, 7)
            Dim ParHOURS = New SqlParameter("@HOURS", SqlDbType.Decimal)
            'By Soham Gangavane Aug 29,2017
            Dim ParPREVHOURS = New SqlParameter("@PREVHOURS", SqlDbType.Decimal)
            Dim ParCOST = New SqlParameter("@COST", SqlDbType.Decimal)
            ParCOST.Precision = 8
            ParCOST.Scale = 2
            Dim ParOPTION = New SqlParameter("@OPTION", SqlDbType.VarChar, 13)
            Dim ParNEWUPDT = New SqlParameter("@NEWUPDT", SqlDbType.Int)
            Dim ParPRODUCT = New SqlParameter("@PRODUCT", SqlDbType.VarChar, 2)
            Dim ParVKEY = New SqlParameter("@VKEY", SqlDbType.VarChar, 5)
            Dim ParPKEY = New SqlParameter("@PKEY", SqlDbType.VarChar, 5)
            Dim ParFlag = New SqlParameter("@Flag", SqlDbType.VarChar, 5)
            Dim ParDept = New SqlParameter("@Dept", SqlDbType.VarChar, 5)
            Dim ParTank = New SqlParameter("@Tank", SqlDbType.VarChar, 3)

            'By Soham Gangavane March 13,2018
            Dim ParTransactionType = New SqlParameter("@TransactionType", SqlDbType.Char, 1)
            ParTransactionType.Direction = ParameterDirection.Input
            ParTransactionType.Value = "M"

            ParRECORD.Direction = ParameterDirection.Input
            ParSENTRY.Direction = ParameterDirection.Input
            ParNUMBER.Direction = ParameterDirection.Input
            ParVEHICLE.Direction = ParameterDirection.Input
            ParMILES.Direction = ParameterDirection.Input
            'Added parameter for prev miles,MPG
            '30 Apr 09
            'Harshada
            parPrevMiles.Direction = ParameterDirection.Input
            parMPG.Direction = ParameterDirection.Input
            ParDATETIME.Direction = ParameterDirection.Input
            ParPUMP.Direction = ParameterDirection.Input
            ParQUANTITY.Direction = ParameterDirection.Input
            ParPERSONNEL.Direction = ParameterDirection.Input
            ParERRORS.Direction = ParameterDirection.Input
            ParHOURS.Direction = ParameterDirection.Input
            'By Soham Gangavane Aug 29,2017
            ParPREVHOURS.Direction = ParameterDirection.Input
            ParCOST.Direction = ParameterDirection.Input
            ParOPTION.Direction = ParameterDirection.Input
            ParNEWUPDT.Direction = ParameterDirection.Input
            ParPRODUCT.Direction = ParameterDirection.Input
            ParVKEY.Direction = ParameterDirection.Input
            ParPKEY.Direction = ParameterDirection.Input
            ParFlag.Direction = ParameterDirection.Input
            ParDept.Direction = ParameterDirection.Input
            ParTank.Direction = ParameterDirection.Input
            If (Request.QueryString.Count = 1) Then
                ParFlag.Value = "Edit"
                'Changed By Varun Moota,due to Decimal Conversion problem.08/06/2010
                ParRECORD.Value = Convert.ToInt32(Double.Parse(Request.QueryString("TxtnNo").ToString()))
                ParNEWUPDT.value = 2
            Else
                ParFlag.Value = "ADD"
                ParRECORD.Value = 0
                ParNEWUPDT.value = 1
            End If

            Dim miles As Decimal
            'Changed By Varun Moota,due to Decimal Conversion problem.08/06/2010
            If txtOdometer.Text <> "" Then miles = Double.Parse(txtOdometer.Text.Trim()) Else miles = 0


            Dim qty As Decimal
            'Changed By Varun Moota,due to Decimal Conversion problem.08/06/2010
            If txtQty.Text <> "" Then qty = Double.Parse(txtQty.Text.Trim()) Else qty = 0

            
            Dim GenFun As New GeneralFunctions
            'ParSENTRY.Value = txtSentry.Text.Trim()
            ParSENTRY.Value = DDLstSentry.SelectedValue.Trim()
            ParNUMBER.Value = txtNumber.Text.Trim()
            'ParVEHICLE.Value = txtVehicle.Text.Trim()
            ParVEHICLE.Value = txtVehID.Text.Trim()
            ParMILES.Value = miles
            'If txtOdometer.Text <> "" Then ParMILES.Value = Convert.ToDecimal(Val(txtOdometer.Text.Trim())) Else ParMILES.Value = 0
            ParDATETIME.Value = Convert.ToDateTime(GenFun.ConvertDate(txtDate.Text.Trim()) & " " & txtTime.Text.Trim())
            If DDLstHose.Items.Count > 0 Then ParPUMP.Value = DDLstHose.SelectedItem.Text Else ParPUMP.Value = ""
            ParQUANTITY.Value = qty
            'If txtQty.Text <> "" Then ParQUANTITY.Value = Convert.ToDecimal(Val(txtQty.Text.Trim())) Else ParQUANTITY.Value = 0
            ParPERSONNEL.Value = txtPersNum.Value
            txtErrors.Text = ""

            If txtHours.Text <> "" Then ParHOURS.Value = Double.Parse(txtHours.Text.Trim()) Else ParHOURS.Value = 0
            If txtCost.Text <> "" Then ParCOST.Value = Double.Parse(txtCost.Text.Trim()) Else ParCOST.Value = 0
            ParOPTION.Value = txtoptional.Text.Trim

            'Commented By Varun Moota to Check PRODUCT as Per DFW.08/30/2010
            'If DDLstHose.Items.Count > 0 Then ParPRODUCT.Value = DDLstHose.SelectedItem.Text Else ParPRODUCT.Value = ""
            If DDLstHose.Items.Count > 0 Then ParPRODUCT.Value = GetProductValue(txtHideTankNBR.Value.Trim) Else ParPRODUCT.Value = ""

            ParVKEY.Value = txtVechKey.Text.Trim
            ParPKEY.Value = txtPKey.Value.Trim
            ParDept.Value = txtDept.Text.Trim 'txtHidDept.Value.Trim
            ParTank.Value = txtHideTankNBR.Value.Trim 'txtTank.Text.Trim '

            'Get previous miles from database and pass to parameter prevmiles
            '30 Apr 09
            'Harshada
            Dim ds As DataSet
            Dim prevmiles As Decimal
            'By Soham Gangavane Aug 29,2017
            Dim prevhours As Decimal
            'ds = dal.GetDataSet("select [datetime],miles from txtn where vehicle='" + txtVehicle.Text.Trim() + "' and datetime < '" + GenFun.ConvertDate(txtDate.Text.Trim()).ToString("MM/dd/yyyy") + " " + txtTime.Text.Trim() + "' order by [datetime] desc")

            'By Soham Gangavane Aug 29,2017 Add hours in query
            ds = dal.GetDataSet("select [datetime],miles,hours from txtn where vehicle='" + txtVehID.Text.Trim() + "' and datetime < '" + GenFun.ConvertDate(txtDate.Text.Trim()).ToString("MM/dd/yyyy") + " " + txtTime.Text.Trim() + "' order by [datetime] desc")
            If ds Is Nothing Then
                prevmiles = 0
            ElseIf ds.Tables(0).Rows.Count = 0 Then
                prevmiles = 0
                prevhours = 0
            Else
                prevmiles = Double.Parse(ds.Tables(0).Rows(0)("miles").ToString())
                prevhours = Double.Parse(ds.Tables(0).Rows(0)("hours").ToString())
            End If
            parPrevMiles.Value = prevmiles
            ParPREVHOURS.Value = prevhours

            'MPG=(mile-prevmiles)/quantity
            Dim mpg As Decimal
            If (qty > 0) Then
                mpg = Double.Parse(miles - prevmiles) / Double.Parse(qty)
                parMPG.Value = mpg
            Else
                mpg = 0.0
                parMPG.Value = mpg
            End If


            If (mpg < 0) Then
                ParERRORS.Value = txtErrors.Text.Trim() + "M"
            ElseIf (mpg > 999) Then
                mpg = 0
                parMPG.Value = mpg
                ParERRORS.Value = txtErrors.Text.Trim() + "M"
            Else
                ParERRORS.Value = txtErrors.Text.Trim()
            End If

            Dim InvData() As String = {txtHideTankNBR.Value.Trim, qty}
            Session("InvData") = InvData

            parcollection(0) = ParRECORD
            parcollection(1) = ParSENTRY
            parcollection(2) = ParNUMBER
            parcollection(3) = ParVEHICLE
            parcollection(4) = ParMILES
            parcollection(5) = parPrevMiles
            parcollection(6) = parMPG
            parcollection(7) = ParDATETIME
            parcollection(8) = ParPUMP
            parcollection(9) = ParQUANTITY
            parcollection(10) = ParPERSONNEL
            parcollection(11) = ParERRORS
            parcollection(12) = ParHOURS
            parcollection(13) = ParCOST
            parcollection(14) = ParOPTION
            parcollection(15) = ParNEWUPDT
            parcollection(16) = ParPRODUCT
            parcollection(17) = ParVKEY
            parcollection(18) = ParPKEY
            parcollection(19) = ParFlag
            parcollection(20) = ParDept
            parcollection(21) = ParTank
            'By Soham Gangavane Aug 29,2017
            parcollection(22) = ParPREVHOURS

            parcollection(23) = ParTransactionType

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_TransInsertUpdate", parcollection)
            Session.Remove("TXTNDS")
            Session.Remove("TXTNTCnt")
            Session.Remove("currentrecord")

            If (Request.QueryString.Count = 1) And blnFlag = True Then
                Dim script As String = "<script>alert('Transaction updated successfully!');location.href='Transaction.aspx';</script>"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", script)
            Else
                If (blnFlag = True And Aflag = "Add Another") Then
                    Dim script As String = "<script>alert('Transaction saved successfully!');</script>"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", script)


                    'Changed default no to *MAN*
                    '27 Apr 09
                    'Harshada
                    'Issue no #132
                    ' txtNumber.Text = Convert.ToInt32(Val(txtNumber.Text)) + 1
                    txtNumber.Text = "*MAN*"
                    txtOdometer.Text = ""
                    txtHours.Text = ""
                    txtQty.Text = ""
                    txtDate.Text = ""
                    txtTime.Text = ""
                    txtCost.Text = ""
                    txtoptional.Text = ""
                    'txtSentry.Text = ""
                    'txtVehicle.Text = ""
                    'txtSentryName.Text = ""
                    txtSentryId.Value = ""
                    txtDesc.Text = ""
                    txtDept.Text = ""
                    txtTank.Text = ""
                    txtProd.Text = ""
                    DDLstSentry.SelectedIndex = 0
                    DDLstSentry_SelectedIndexChanged(Nothing, Nothing)
                    DDLstHose.SelectedIndex = 0
                    'DDLstHose.SelectedIndex = -1
                ElseIf (blnFlag = True And Aflag = "Save") Then
                    txtName.Text = ""
                    'Decuction from Tank Inventory.
                    blnFlag = CheckCostingEnabled()
                    If blnFlag Then
                        blnFlag = FIFOInvDeduction(Session("InvData"))
                    End If
                    Dim script As String = "<script>alert('Transaction saved successfully!');location.href='Transaction.aspx';</script>"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", script)
                End If
                txtVechKey.Text = "0000"

                btnFirst.Visible = False
                btnLast.Visible = False
                btnprevious.Visible = False
                btnNext.Visible = False
                lblof.Visible = False
                lblErr.Visible = False
                txtErrors.Visible = False

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_new_edit_SaveRecords", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
    'Added By Varun Moota,since there was a Bug while Assigning Product Value.08/30/2010
    Private Function GetProductValue(ByVal prodVal As String) As String
        Try
            Dim dal = New GeneralizedDAL()
            Dim ds As New DataSet
            Dim prodDS As New DataSet
            Dim parTcollection(0) As SqlParameter
            Dim ParNo = New SqlParameter("@No", SqlDbType.NVarChar, 5)
            ParNo.Direction = ParameterDirection.Input
            ParNo.Value = prodVal
            parTcollection(0) = ParNo
            prodDS = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransGetTankNameProduct", parTcollection)

            If Not prodDS Is Nothing Then
                If prodDS.Tables(0).Rows.Count > 0 Then
                    prodVal = prodDS.Tables(0).Rows(0)(1).ToString()
                End If
            Else
                prodVal = String.Empty
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_new_edit.GetProductValue", ex)
        End Try
        Return prodVal
    End Function

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        '*******************************This event is use for save/update data in to database*******************************
        If (btnchk = False) Then
            Dim str As String = txtName.Text
            Dim strHose As String = ""
            Dim hours As Integer = 0
            Dim Qty As Decimal = 0
            Dim btn As Web.UI.WebControls.Button = sender

            Try
                'If txtSentryName.Text.Trim() = "" Then
                If DDLstSentry.Text.Trim() = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Select Sentry')</script>")
                    DDLstSentry.Focus()
                    Exit Sub
                ElseIf txtVehID.Text.Trim() = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Select Vehicle')</script>")
                    txtVehID.Focus()
                    Exit Sub
                End If

                If (CheckSentry(DDLstSentry.SelectedValue.Trim()) = False) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Enter Correct Sentry Number')</script>")
                    DDLstSentry.Focus()
                    Exit Sub
                End If

                If (VehicleExists(txtVehID.Text.Trim()) = False) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Enter Correct Vehicle Number')</script>")
                    txtVehID.Focus()
                    Exit Sub
                End If

                If DDLstHose.Items.Count > 0 Then
                    If DDLstHose.SelectedItem.Text <> "" Then
                        strHose = DDLstHose.SelectedItem.Text
                    End If
                End If

                If txtHours.Text.Trim() <> "" Then
                    hours = Convert.ToDecimal(txtHours.Text.Trim())
                End If
                If txtQty.Text.Trim() <> "" Then
                    Qty = Convert.ToString(txtQty.Text.Trim())
                    'Qty = Convert.ToDecimal(txtQty.Text.Trim())
                End If
                If txtHours.Text.Trim() = "" And txtOdometer.Text.Trim() = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Enter Hours and/or Odometer')</script>")
                    txtOdometer.Focus()
                    Exit Sub
                End If
                ValidateUniqueKeyConatraint("Add Another")
                ' SaveRecords("Add Another")
            Catch ex As Exception
                Dim cr As New ErrorPage
                Dim errmsg As String
                cr.errorlog("Transaction_New_Edit_btnok", ex)
                If ex.Message.Contains(";") Then
                    errmsg = ex.Message.ToString()
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
                Else
                    errmsg = ex.Message.ToString()
                End If
            End Try
        End If
    End Sub
    'Added By Pritam to handle Unique Key Constrain Date : 22-July-2014
    Public Sub ValidateUniqueKeyConatraint(ByVal Insert As String)
        Try
            Dim ds2 As DataSet
            Dim dal = New GeneralizedDAL()
            Dim GenFun = New GeneralFunctions()

            Dim Pump As String
            Dim Sentry As String
            Dim Quantity As Decimal
            Dim Vehicle As String
            Dim TXTN_DateTime As String

            Sentry = DDLstSentry.SelectedValue.Trim()
            Pump = DDLstHose.SelectedItem.Text
            Quantity = txtQty.Text.Trim() 'Double.Parse(txtQty.Text.Trim())
            Vehicle = txtVehID.Text.Trim()

            Dim DtTXTN As Date = Convert.ToDateTime(txtDate.Text)
            Dim TmTXTN As DateTime = Convert.ToDateTime(If(txtTime.Text = "", "00:00", txtTime.Text))
            Dim dt1 As New DateTime(DtTXTN.Year, DtTXTN.Month, DtTXTN.Day, TmTXTN.Hour, TmTXTN.Minute, TmTXTN.Second)
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

            ParPUMP.value = Pump
            ParVehicle.value = Vehicle
            ParDATETIME.value = dt1
            ParSentry.value = Sentry
            ParQUANTITY.value = Quantity

            parcollection(0) = ParPUMP
            parcollection(1) = ParVehicle
            parcollection(2) = ParDATETIME
            parcollection(3) = ParSentry
            parcollection(4) = ParQUANTITY

            ds2 = dal.ExecuteStoredProcedureGetDataSet("USP_TT_TXTN_UniqueKeyContrain", parcollection)

            If Not ds2 Is Nothing Then
                If ds2.Tables(0).Rows.Count > 0 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script> alert('Transaction already exist Please change the time.');</script>")
                Else
                    SaveRecords(Insert)
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_New_Edit_ValidateUniqueKeyConatraint", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParRECORDID = New SqlParameter("@RECORDID", SqlDbType.Int)
            ParRECORDID.Direction = ParameterDirection.Input
            ParRECORDID.Value = Convert.ToInt32(lblRecNo.Text.Trim())
            parcollection(0) = ParRECORDID
            Dim blnflag As Boolean
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_TransDelete", parcollection)
            If blnflag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Transaction deleted sucessfully.')</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_new_edit_btnDelete_Click_Insert", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Function CheckSentry(ByVal strSentry As String) As Boolean
        Dim dal = New GeneralizedDAL()
        Dim ds As New DataSet
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParSentry = New SqlParameter("@Sentry", SqlDbType.NVarChar, 3)
            ParSentry.Direction = ParameterDirection.Input
            ParSentry.Value = strSentry
            parcollection(0) = ParSentry
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransCheckSentry", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    CheckSentry = True
                Else
                    CheckSentry = False
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_new_edit_CheckSentry", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Function

    Private Function GetSentryName(ByVal strSentry As String) As Boolean
        Dim dal = New GeneralizedDAL()
        Dim ds As New DataSet
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParSentry = New SqlParameter("@Sentry", SqlDbType.NVarChar, 3)
            ParSentry.Direction = ParameterDirection.Input
            ParSentry.Value = strSentry
            parcollection(0) = ParSentry
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransCheckSentry", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    'txtSentryName.Text = ds.Tables(0).Rows(0)(1)
                Else
                    'txtSentryName.Text = "Sentry Name Missing"
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_new_edit_CheckSentry", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Function

    Protected Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            ds = CType(Session("TXTNDS"), DataSet)
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = 0

            If (cnt < ds.Tables(0).Rows.Count) Then
                FillRecords(cnt)
            End If
            Session("currentrecord") = "0"
            lblof.Text = "1 of " & Session("TXTNTCnt")
            btnprevious.Enabled = False
            btnFirst.Enabled = False
            btnNext.Enabled = True
            btnLast.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_New_Edit_btnFirst_Click", ex)
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
            ds = CType(Session("TXTNDS"), DataSet)
            cnt = Convert.ToInt32(Session("TXTNTCnt").ToString() - 1)
            If (cnt < ds.Tables(0).Rows.Count) Then
                FillRecords(cnt)
            End If
            lblof.Text = Session("TXTNTCnt").ToString() & " of " & Session("TXTNTCnt").ToString()
            Session("currentrecord") = cnt

            btnNext.Enabled = False
            btnLast.Enabled = False

            btnprevious.Enabled = True
            btnFirst.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_New_Edit_btnLast_Click", ex)
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
            ds = CType(Session("TXTNDS"), DataSet)
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt + 1

            If (cnt < ds.Tables(0).Rows.Count) Then
                FillRecords(cnt)
                Session("currentrecord") = cnt
            End If

            lblof.Text = cnt + 1 & " of " & Session("TXTNTCnt").ToString()

            If Not btnFirst.Enabled Then
                btnFirst.Enabled = True
                btnprevious.Enabled = True
            End If

            If (cnt + 1 = Convert.ToInt32(Session("TXTNTCnt"))) Then
                btnLast.Enabled = False
                btnNext.Enabled = False
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_New_Edit_btnNext_Click", ex)
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
            ds = CType(Session("TXTNDS"), DataSet)
            If (cnt < ds.Tables(0).Rows.Count And cnt >= 0) Then
                FillRecords(cnt)
                Session("currentrecord") = cnt
            End If
            lblof.Text = (cnt + 1) & " of " & Session("TXTNTCnt")

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
            cr.errorlog("Transaction_New_Edit_btnprevious_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Sub FillHoseList()
        '*********************** This event use for DDLstSentry_SelectedIndex Changed then respective data is fetch into lbl control **********************************
        'txtQty.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
        'txtQty.Attributes.Add("OnKeyPress", "KeyPressEvent_txtQty(event);")
        Dim dal = New GeneralizedDAL()
        Dim ds As New DataSet
        Try
            DDLstHose.Items.Clear()

            Dim parcollection(0) As SqlParameter
            Dim ParSentry = New SqlParameter("@Sentry", SqlDbType.NVarChar, 10)
            ParSentry.Direction = ParameterDirection.Input
            'ParSentry.Value = txtSentryId.Value.Trim()
            ParSentry.Value = DDLstSentry.SelectedValue.Trim()

            parcollection(0) = ParSentry
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransFillHoseList", parcollection)
            Dim i As Integer = 0
            Dim bPresent As Boolean = False
            'Changed hose default to blank
            '28 Apr 09
            'Harshada
            'Issue no #137
            DDLstHose.Items.Add("")

            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To 7
                        Dim str As String = ""
                        If ds.Tables(0).Rows(0)(i).ToString() <> "" Then
                            str = (i + 1).ToString.PadLeft(2, "0")
                        End If
                        If str <> "Null" And str <> "" Then
                            DDLstHose.Items.Add(str)
                            bPresent = True
                        End If
                    Next
                    If Not bPresent Then
                        'txtSentryName.Text = "Sentry Name Missing"
                    End If

                    DDLstHose_SelectedIndexChanged(Nothing, Nothing)
                Else
                    'txtSentryName.Text = "Sentry Name Missing"
                End If

            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_New_Edit.FillHoseList", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub DDLstHose_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLstHose.DataBinding
        Try
            Dim i = 0
            i = i + 1
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.DDLstHose_DataBinding", ex)
        End Try
       
    End Sub

    Protected Sub DDLstHose_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLstHose.SelectedIndexChanged
        Dim dal = New GeneralizedDAL()
        Dim ds As New DataSet
        Try
            If Not DDLstHose.SelectedItem.Text = Nothing Then
                Dim selectedItem As String = DDLstHose.SelectedItem.Text

                Dim parcollection(1) As SqlParameter
                Dim ParSentry = New SqlParameter("@Sentry", SqlDbType.NVarChar, 10)
                Dim ParHose = New SqlParameter("@Hose", SqlDbType.Int)
                ParSentry.Direction = ParameterDirection.Input
                ParHose.Direction = ParameterDirection.Input
                'ParSentry.Value = txtSentry.Text.Trim()
                ParSentry.Value = DDLstSentry.SelectedValue.Trim()
                ParHose.Value = Val(selectedItem)
                parcollection(0) = ParSentry
                parcollection(1) = ParHose
                ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransGetProductRate", parcollection)
                If Not ds Is Nothing Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim No As String = 0
                        Dim rate As Double = 0.0
                        No = ds.Tables(0).Rows(0)(0).ToString()
                        If (ds.Tables(0).Rows(0)(1).ToString() <> "" And ds.Tables(0).Rows(0)(1).ToString() <> "Null") Then
                            rate = Convert.ToDecimal(ds.Tables(0).Rows(0)(1).ToString())
                        End If
                        If No = "" Or No = "Null" Or No = "000" Then
                            txtProd.Text = "Not Defined"
                            txtTank.Text = "Not Assigned"
                            'txtQty.Attributes.Add("onkeyup", "KeyUpEvent_txtQty(event,0);")
                            Return
                        End If
                        If No.Length = 1 Then
                            No = "00" + No
                        ElseIf No.Length = 2 Then
                            No = "0" + No
                        End If
                        '-------------------------------
                        Dim Tds As New DataSet
                        Dim parTcollection(0) As SqlParameter
                        Dim ParNo = New SqlParameter("@No", SqlDbType.NVarChar, 5)
                        ParNo.Direction = ParameterDirection.Input
                        ParNo.Value = No
                        parTcollection(0) = ParNo
                        Tds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransGetTankNameProduct", parTcollection)
                        If Not Tds Is Nothing Then
                            If Tds.Tables(0).Rows.Count > 0 Then
                                txtTank.Text = Tds.Tables(0).Rows(0)(0).ToString()
                                txtHideTankNBR.Value = No
                                txtProd.Text = Tds.Tables(1).Rows(0)(0).ToString()
                                'Cost=quantity*price
                                '28 Apr 09
                                'Harshada
                                'Issue no #140
                                txtHideTankPrice.Value = Tds.Tables(0).Rows(0)(2).ToString()

                                'Found bug here, if Hose selected before enetering Qty the cost is EMPTY.
                                Dim InvData() As String = {txtHideTankNBR.Value}
                                'Check FIFO Costing is enabled in Status table.02/29/2012
                                Dim blnFlag As Boolean = CheckCostingEnabled()
                                If blnFlag Then
                                    'Save the Data for FIFO Costing.03/01/2012
                                    ds = FIFOInvData(InvData)
                                    If (ds IsNot Nothing) Then
                                        If (ds.Tables(0).Rows.Count > 0) Then
                                            txtHideTankPrice.Value = ds.Tables(0).Rows(0)("PRICE_ADD").ToString()
                                        End If
                                    End If
                                End If

                                If (txtQty.Text <> "") Then
                                    txtCost.Text = (Val(txtHideTankPrice.Value) * Val(txtQty.Text)).ToString("0.00")
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_New_Edit_DDLstHose_SelectedIndexChanged", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If (btnchk = False) Then
            Dim str As String = txtName.Text
            Dim strHose As String = ""
            Dim hours As Integer = 0
            Dim Qty As Decimal = 0
            Dim btn As Web.UI.WebControls.Button = sender

            Try
                If DDLstSentry.Text.Trim() = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Select Sentry')</script>")
                    DDLstSentry.Focus()
                    Exit Sub
                ElseIf txtVehID.Text.Trim() = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Select Vehicle')</script>")
                    txtVehID.Focus()
                    Exit Sub
                End If
                If DDLstHose.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Select Hose')</script>")
                    DDLstHose.Focus()
                    Exit Sub
                End If

                'If (CheckSentry(txtSentryId.Value.Trim()) = False) Then
                If (CheckSentry(DDLstSentry.SelectedValue.Trim()) = False) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Enter Correct Sentry Number')</script>")
                    DDLstSentry.Focus()
                    Exit Sub
                End If

                If (VehicleExists(txtVehID.Text.Trim()) = False) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Enter Correct Vehicle Number')</script>")
                    txtVehID.Focus()
                    Exit Sub
                End If

                If DDLstHose.Items.Count > 0 Then
                    If DDLstHose.SelectedItem.Text <> "" Then
                        strHose = DDLstHose.SelectedItem.Text
                    End If
                End If

                If txtHours.Text.Trim() <> "" Then
                    hours = Convert.ToDecimal(txtHours.Text.Trim())
                End If
                If txtQty.Text.Trim() <> "" Then
                    Qty = Convert.ToString(txtQty.Text.Trim())
                    'Qty = Convert.ToDecimal(txtQty.Text())
                    'Check FIFO Costing is enabled in Status table.02/29/2012
                    Dim blnFlag As Boolean = CheckCostingEnabled()
                    If blnFlag Then
                        'Save the Data for FIFO Costing.03/01/2012
                        Dim ds As New DataSet
                        Dim InvData As String() = {txtHideTankNBR.Value, Qty}
                        ds = FIFOInvData(InvData)
                        If (ds IsNot Nothing) Then
                            If (ds.Tables(0).Rows.Count > 0) Then
                                txtHideTankPrice.Value = ds.Tables(0).Rows(0)("PRICE_ADD").ToString()
                            End If
                        End If
                    End If

                    If Request.QueryString.Count > 0 Then
                        If Not Session("HoseValue").ToString() = DDLstHose.SelectedValue.ToString() Then
                            If (txtQty.Text <> "") Then
                                txtCost.Text = (Val(txtHideTankPrice.Value) * Val(txtQty.Text)).ToString("0.00")
                            End If
                        End If
                    Else
                        If (txtQty.Text <> "") Then
                            txtCost.Text = (Val(txtHideTankPrice.Value) * Val(txtQty.Text)).ToString("0.00")
                        End If
                    End If
                   
                   
                End If
                If txtHours.Text.Trim() = "" And txtOdometer.Text.Trim() = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please Enter Hours and/or Odometer')</script>")
                    txtOdometer.Focus()
                    Exit Sub
                End If
                If (Request.QueryString.Count = 1) Then
                    SaveRecords("Save")
                Else
                    ValidateUniqueKeyConatraint("Save")
                End If
                ' SaveRecords("Save")
            Catch ex As Exception
                Dim cr As New ErrorPage
                Dim errmsg As String
                cr.errorlog("Transaction_New_Edit_btnSave_Click", ex)
                If ex.Message.Contains(";") Then
                    errmsg = ex.Message.ToString()
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
                Else
                    errmsg = ex.Message.ToString()
                End If
            End Try
        End If
    End Sub

    Private Function VehicleExists(ByVal VehID As String) As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim ds As New DataSet
            'take in a vehicle ID and verify that it exists in the VEHS table
            Dim strQuery As String
            strQuery = "select * from vehs where [IDENTITY] = '" + VehID + "'"
            ds = dal.GetDataSet(strQuery)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    txtDesc.Text = ds.Tables(0).Rows(0)("EXTENSION").ToString()
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.VehicleExists", ex)
        End Try
    End Function

    Public Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String)
        Try

   
            Dim clientScript As String 'txtPKey
            Dim Parameter As String = "Personnel.aspx?txtpers=" & txtName.ClientID & "&txtnum=" & txtPersNum.ClientID & "&txtPKey=" & txtPKey.ClientID & "&PopUp=true"

            Dim width As Integer = 600
            Dim height As Integer = 600
            Dim windowAttribs As String

            windowAttribs = "width=" & width & "px," & _
                            "height=" & height & "px," & _
                            "left='+((screen.width -" & width & ") / 2)+'," & _
                            "top='+ (screen.height - " & (height + 50) & ") / 2+'," & _
                            "scrollbars=yes"
            clientScript = "window.open('" & Parameter & "','PopUp','" & windowAttribs & "');return false;"
            opener.Attributes.Add("onClick", clientScript)

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.OpenPopUp", ex)
        End Try
    End Sub

    Public Sub OpenVehPopUp(ByVal opener As System.Web.UI.WebControls.WebControl)
        Try
            'Dim clientScript As String
            ''Dim Parameter As String = "Vehicle.aspx?txtveh=" & txtVehicle.ClientID & "&txtdesc=" & txtDesc.ClientID & "&txtHidDept=" & txtHidDept.ClientID & "&txtDept=" & txtDept.ClientID & "&txtVechKey=" & txtVechKey.ClientID & "&PopUp=true"

            'Dim width As Integer = 600
            'Dim height As Integer = 600
            'Dim windowAttribs As String
            'windowAttribs = "width=" & width & "px," & _
            '                "height=" & height & "px," & _
            '               "left='+((screen.width -" & width & ") / 2)+'," & _
            '                "top='+ (screen.height - " & (height + 50) & ") / 2+'," & _
            '                "scrollbars=yes"
            'clientScript = "window.open('" & Parameter & "','PopUp','" & windowAttribs & "');return false;"
            'opener.Attributes.Add("onClick", clientScript)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.OpenVehPopUp", ex)
        End Try
      
    End Sub

    Private Function GetIndex(ByVal dt As DataTable, ByVal Myval As String) As Integer
        Try
            Dim dr As DataRow = dt.Rows.Find(Myval)
            If Not IsDBNull(dr) Then
                GetIndex = dt.Rows.IndexOf(dr)
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.GetIndex", ex)
        End Try


    End Function

    Protected Sub DDLstSentry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLstSentry.SelectedIndexChanged
        Try
            If Not DDLstSentry.Items.Count = 0 Then
                If CheckSentry(DDLstSentry.SelectedValue.Trim()) = True Then
                    FillHoseList()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.DDLstSentry_SelectedIndexChanged", ex)
        End Try
    End Sub


    'Added By Varun Moota, to check whether costing methods enabled .02/29/2012
    Private Function CheckCostingEnabled() As Boolean
        Try
            Dim ds As New DataSet
            Dim dal = New GeneralizedDAL()
            ds = dal.GetDataSet("SELECT * FROM STATUS WHERE COSTING=3")
            If (ds IsNot Nothing) Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            End If

            Return False

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.CheckCostingEnabled()", ex)
            Return False
        End Try

    End Function
    'Added By Varun Moota, we are doing Costing Methods now(Transaction Costing &FIFO only for now).02/29/2012
    Private Function FIFOInvData(ByVal FIFOData As String()) As DataSet
        Try
            Dim ds As New DataSet
            Dim dal = New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim ParTank = New SqlParameter("@Tank", SqlDbType.NVarChar, 3)

            ParTank.Direction = ParameterDirection.Input
            ParTank.Value = FIFOData(0).ToString
            parcollection(0) = ParTank

            ds = dal.ExecuteStoredProcedureGetDataSet("SP_TXTN_FIFO_COSTING", parcollection)
            If (ds IsNot Nothing) Then
                If (ds.Tables(0).Rows.Count > 0) Then
                    txtHideTankPrice.Value = ds.Tables(0).Rows(0)("PRICE_ADD").ToString()
                    Return ds
                End If
            End If

            Return Nothing

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.FIFOInvData()", ex)
            Return Nothing
        End Try

    End Function
    'Added By Varun Moota, Inventory Deduction, based on FIFO Costing Methods.03/02/2012
    Private Function FIFOInvDeduction(ByVal FIFOData As String()) As Boolean
        Try
            Dim ds As New DataSet
            Dim dal = New GeneralizedDAL()
            Dim UnAccountedFuel, New_Remaining As Decimal
            Dim iCnt As Integer

            Dim parcollection(3) As SqlParameter
            Dim ParTank = New SqlParameter("@Tank", SqlDbType.NVarChar, 3)
            Dim ParQty = New SqlParameter("@Qty", SqlDbType.Decimal)
            Dim ParRecNo = New SqlParameter("@RecNo", SqlDbType.Int)
            Dim ParFlag = New SqlParameter("@Flag", SqlDbType.NVarChar, 10)


            ParTank.Direction = ParameterDirection.Input
            ParQty.Direction = ParameterDirection.Input
            ParRecNo.Direction = ParameterDirection.Input
            ParFlag.Direction = ParameterDirection.Input


            ds = FIFOInvData(FIFOData)
            If (ds IsNot Nothing) Then
                If (ds.Tables(0).Rows.Count > 0) Then
                    UnAccountedFuel = Val(FIFOData(1))
                    New_Remaining = Val(Val(ds.Tables(0).Rows(0)("Remaining").ToString()) - UnAccountedFuel)

                    If (New_Remaining > 0) Then

                        ParTank.Value = FIFOData(0)
                        ParQty.Value = UnAccountedFuel
                        ParRecNo.Value = Val(ds.Tables(0).Rows(0)("Record").ToString())
                        ParFlag.Value = "PREV"

                        parcollection(0) = ParTank
                        parcollection(1) = ParQty
                        parcollection(2) = ParRecNo
                        parcollection(3) = ParFlag

                        iCnt = dal.ExecuteStoredProcedureGetInteger("USP_FIFO_Inv_Deduction", parcollection)
                        If iCnt > 0 Then
                            Return True
                        End If
                    Else
                        ParTank.Value = FIFOData(0)
                        ParQty.Value = 0.0
                        ParRecNo.Value = Val(ds.Tables(0).Rows(0)("Record").ToString())
                        ParFlag.Value = "NEXT"

                        parcollection(0) = ParTank
                        parcollection(1) = ParQty
                        parcollection(2) = ParRecNo
                        parcollection(3) = ParFlag
                        iCnt = dal.ExecuteStoredProcedureGetInteger("USP_FIFO_Inv_Deduction", parcollection)
                        If iCnt > 0 Then
                            ParTank.Value = FIFOData(0)
                            ParQty.Value = (-New_Remaining)
                            ParRecNo.Value = Val(ds.Tables(0).Rows(1)("Record").ToString())
                            ParFlag.Value = "PREV"

                            parcollection(0) = ParTank
                            parcollection(1) = ParQty
                            parcollection(2) = ParRecNo
                            parcollection(3) = ParFlag

                            iCnt = dal.ExecuteStoredProcedureGetInteger("USP_FIFO_Inv_Deduction", parcollection)
                            If iCnt > 0 Then
                                Return True
                            End If
                        End If

                    End If
                End If
            End If

            Return False

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.FIFOInvDeduction()", ex)
            Return False
        End Try

    End Function

    <WebMethod()> _
    Public Shared Function GetVehicleInfo(ByVal vehID As String) As String
        Try
            Dim dal = New GeneralizedDAL()
            Dim ds As New DataSet

            'take in a vehicle ID and verify that it exists in the VEHS table
            Dim strQuery As String
            strQuery = "select * from vehs where [IDENTITY] = '" + vehID + "'"
            ds = dal.GetDataSet(strQuery)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("Dept").ToString()
                    'Return "[Object]"
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction_New_Edit.checkVeh", ex)
            Return Nothing
        End Try
    End Function

End Class