Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class TankInventory_New_Edit
    Inherits System.Web.UI.Page
    Dim TankInvTcount As Integer
    Dim GenFun As New GeneralFunctions

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dal = New GeneralizedDAL()
        Dim ds = New DataSet()
        '**************** Check for session is null/not*********************
        lblof.Visible = False
        If Session("User_name") Is Nothing Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
        Else
            Dim val As String
            val = lblNew_Edit.Text
            Try
                Session("RecordUpdate") = False
                If Not IsPostBack Then
                    'Fill Tank (DDLstTank) Modify code by Jatin Kshirsagar as on 01 Sept 2008
                    DDLstTank.Items.Clear()
                    ds = New DataSet()
                    ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TankInvPopulateTankList")
                    If Not ds Is Nothing Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            DDLstTank.DataSource = ds.Tables(0)
                            DDLstTank.DataTextField = "TANKNoName"
                            DDLstTank.DataValueField = "NUMBER"
                            DDLstTank.DataBind()
                            'Store size of tank in seperate dropdown to compare with level prior to delivery
                            ddlTankSize.DataSource = ds.Tables(0)
                            ddlTankSize.DataTextField = "TANK_SIZE"
                            ddlTankSize.DataValueField = "TANK_SIZE"
                            ddlTankSize.DataBind()
                        End If
                    End If
                End If
                If (Request.QueryString.Count = 2 And Not IsPostBack) Then
                    ds = New DataSet
                    ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TankInvRecords")
                    ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("RECORD")}
                    Dim cnt As Long = 0
                    If Not ds Is Nothing Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            Dim dr As DataRow = ds.Tables(0).Rows.Find(Request.QueryString(1).ToString())
                            If Not IsDBNull(dr) Then
                                cnt = ds.Tables(0).Rows.IndexOf(dr)
                            End If

                            Session("TankInvDS") = ds
                            Session("currentrecord") = cnt
                            Session("TankInvTCnt") = ds.Tables(1).Rows(0)(0).ToString()
                            fillRecordsForEdit(cnt)
                        End If
                        If ds.Tables(1).Rows.Count > 0 Then
                            Session("TankInvTCnt") = ds.Tables(1).Rows(0)(0).ToString()
                            lblof.Text = cnt + 1 & " " & "of" & " " & ds.Tables(1).Rows(0)(0).ToString()
                            If Session("TankInvTCnt") = "1" Then
                                lblof.Text = "1 of " & Session("TankInvTCnt")
                                btnprevious.Enabled = False
                                btnFirst.Enabled = False
                                btnNext.Enabled = False
                                btnLast.Enabled = False
                            End If
                        End If
                    End If
                ElseIf (Not IsPostBack) Then
                    hdfEntryType.Value = Request.QueryString(0).ToString().Trim()
                    ControlNames(hdfEntryType.Value)
                    If hdfEntryType.Value.Trim() = "R" Then
                        RangeValidator1.ControlToValidate = "txt3"
                        lblNew_Edit.Text = "New Fuel Delivery Information"
                        btnOk.Text = "Add Another"
                        lblof.Visible = False
                        btnFirst.Visible = False
                        btnLast.Visible = False
                        btnprevious.Visible = False
                        btnNext.Visible = False

                        'Added By Varun Moota to Test Cancel Button. 01/13/2010
                        btnEditCancel.Visible = False
                        btnCancel.Visible = True

                    ElseIf hdfEntryType.Value.Trim() = "S" Then
                        lblNew_Edit.Text = "New Tank Setting Information"
                        btnOk.Text = "Add Another"
                        lblof.Visible = False
                        btnFirst.Visible = False
                        btnLast.Visible = False
                        btnprevious.Visible = False
                        btnNext.Visible = False
                        RangeValidator1.ControlToValidate = "txt2"

                        'Added B Varun Moota to Test Cancel Button. 01/13/2010
                        btnEditCancel.Visible = False
                        btnCancel.Visible = True





                    ElseIf hdfEntryType.Value.Trim() = "D" Or hdfEntryType.Value.Trim() = "L" Then
                        lblNew_Edit.Text = "New Tank Level Information"
                        btnOk.Text = "Add Another"
                        lblof.Visible = False
                        btnFirst.Visible = False
                        btnLast.Visible = False
                        btnprevious.Visible = False
                        btnNext.Visible = False
                        RangeValidator1.Visible = False

                        'Added B Varun Moota to Test Cancel Button. 01/13/2010
                        btnEditCancel.Visible = False
                        btnCancel.Visible = True

                    End If
                End If

            Catch ex As Exception
                Dim cr As New ErrorPage
                cr.errorlog("TankInventory_Edit_Page_Load", ex)
            End Try
        End If
    End Sub

    Private Sub VisibleControls()
        Try
            lbl1.Visible = False
            lbl2.Visible = False
            lbl3.Visible = False
            lbl4.Visible = False
            lbl5.Visible = False
            lbl6.Visible = False
            lbl7.Visible = False
            lbl8.Visible = False
            lbl9.Visible = False
            lbl10.Visible = False
            lblof.Visible = False

            txt1.Visible = False
            txt2.Visible = False
            txt3.Visible = False
            txt4.Visible = False
            txt5.Visible = False
            txt6.Visible = False
            txt7.Visible = False
            txt8.Visible = False
            txt10.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory_New_Edit.VisibleControls()", ex)
        End Try
    End Sub

    Private Sub ControlNames(ByVal ENTRY_TYPE As String)
        Try
            VisibleControls()
            If ENTRY_TYPE = "R" Then
                'trHide.Visible = False
                txt2.MaxLength = 5
                txt3.MaxLength = 7
                txt4.MaxLength = 5
                txt5.MaxLength = 10
                lbl1.Text = "Level Prior to Delivery :"
                lbl2.Text = "Quantity Delivered :"
                lbl3.Text = "Delivered Fuel Price :"
                lbl4.Text = "Time :"
                lbl5.Text = "Date :"
                lbl6.Text = "Invoice# :"
                lbl7.Text = "Supplier :"
                lbl8.Text = "Employee :"
                lbl10.Text = "Invoiced Fuel :"
                lbl9.Visible = False
                txt1.Visible = True
                txt2.Visible = True
                txt3.Visible = True
                txt4.Visible = True
                txt5.Visible = True
                txt5.Text = Format(Month(DateAdd(DateInterval.Day, 0, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, 0, Today)), "00") & "/" & Year(DateAdd(DateInterval.Day, 0, Today))
                txt6.Visible = True
                txt7.Visible = True
                txt8.Visible = True
                txt10.Visible = True
                lbl1.Visible = True
                lbl2.Visible = True
                lbl3.Visible = True
                lbl4.Visible = True
                lbl5.Visible = True
                lbl6.Visible = True
                lbl7.Visible = True
                lbl8.Visible = True
                lbl10.Visible = True

                RFV_txt1.ErrorMessage = "Please enter Level Prior to Delivery"
                RFV_txt2.ErrorMessage = "Please enter Quantity Delivered"
                RFV_txt5.ErrorMessage = "Please enter Date of delivery"
                RFV_txt3.ErrorMessage = "Please enter Delivered Fuel Price"
                RFV_txt4.ErrorMessage = "Please enter Time of Delivery"

                txt3.Attributes.Add("OnKeyPress", "TrapKey(event,'txt3');")
                'txt3.Attributes.Add("onfocusout", "ValidateKey('txt3');")

                RangeValidator1.ControlToValidate = "txt3"
                lblNew_Edit.Text = "Edit Fuel Delivery Information"

                txt1.Attributes.Add("OnKeyPress", "KeyPressEvent_txt_nodecimal(event);")
                txt2.Attributes.Add("OnKeyPress", "KeyPressEvent_txt_nodecimal(event);")
                txt3.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txt4.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txt10.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")

                txt1.Attributes.Add("onkeyup", "KeyUpEvent_txt1R(event);")
                txt2.Attributes.Add("onkeyup", "KeyUpEvent_txt2R(event);")
                txt3.Attributes.Add("onkeyup", "KeyUpEvent_txt3R(event);")
                txt4.Attributes.Add("onkeyup", "KeyUpEvent_txt4R(event);")
                txt5.Attributes.Add("onkeyup", "KeyUpEvent_txt5R(event);")
                RangeValidator1.ControlToValidate = "txt3"
            ElseIf ENTRY_TYPE = "S" Then
                trHide.Visible = True

                REV_6.ControlToValidate = "txt4"
                RFV_txt7.ControlToValidate = "txt3"

                txt2.MaxLength = 7
                txt3.MaxLength = 5
                txt4.MaxLength = 10
                lbl1.Text = "Quantity :"
                lbl2.Text = "Price :"
                lbl3.Text = "Time :"
                lbl4.Text = "Date :"
                lbl1.Visible = True
                lbl2.Visible = True
                lbl3.Visible = True
                lbl4.Visible = True
                txt1.Visible = True
                txt2.Visible = True
                txt3.Visible = True
                txt4.Visible = True
                txt4.Text = Format(Month(DateAdd(DateInterval.Day, 0, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, 0, Today)), "00") & "/" & Year(DateAdd(DateInterval.Day, 0, Today))

                RFV_txt1.ErrorMessage = "Please enter Quantity"
                RFV_txt2.ErrorMessage = "Please enter Price"
                RFV_txt3.ErrorMessage = "Please enter Time"
                RFV_txt4.ErrorMessage = "Please enter Date"
                RFV_txt5.Enabled = False

                lblNew_Edit.Text = "Edit Tank Setting Information"

                txt1.Attributes.Add("OnKeyPress", "KeyPressEvent_txt_nodecimal(event);")
                txt2.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txt3.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txt4.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")

                ' txt2.Attributes.Add("onchange", "CountDecimals1();")

                txt1.Attributes.Add("onkeyup", "KeyUpEvent_txt1S(event);")
                txt2.Attributes.Add("onkeyup", "KeyUpEvent_txt2S(event);")
                txt3.Attributes.Add("onkeyup", "KeyUpEvent_txt3S(event);")
                txt4.Attributes.Add("onkeyup", "KeyUpEvent_txt4S(event);")
                RangeValidator1.ControlToValidate = "txt2"
            ElseIf (ENTRY_TYPE = "L") Or (ENTRY_TYPE = "D") Then
                '**if you edit for Fuel Delivery then you will enter in the fallowing loop**
                trHide.Visible = True

                RangeValidator1.Visible = False
                REV_6.ControlToValidate = "txt3"
                RFV_txt7.ControlToValidate = "txt2"
                txt2.MaxLength = 5
                txt3.MaxLength = 10
                lbl1.Text = "Quantity :"
                lbl2.Text = "Time :"
                lbl3.Text = "Date :"
                If (hdfEntryType.Value.Trim() = "L") Then
                    lbl9.Text = "Automated Level Reading"
                Else
                    lbl9.Text = "Manual Level Reading"
                End If

                RFV_txt1.ErrorMessage = "Please enter Quantity"
                RFV_txt2.ErrorMessage = "Please enter Time"
                RFV_txt3.ErrorMessage = "Please enter Date"
                RFV_txt4.Enabled = False
                RFV_txt5.Enabled = False

                lblNew_Edit.Text = "Edit Tank Level Information"

                txt1.Visible = True
                txt2.Visible = True
                txt3.Visible = True
                txt3.Text = Format(Month(DateAdd(DateInterval.Day, 0, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, 0, Today)), "00") & "/" & Year(DateAdd(DateInterval.Day, 0, Today))
                lbl1.Visible = True
                lbl2.Visible = True
                lbl3.Visible = True

                txt1.Attributes.Add("OnKeyPress", "KeyPressEvent_txt_nodecimal(event);")
                txt2.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                txt3.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")

                txt1.Attributes.Add("onkeyup", "KeyUpEvent_txt1D(event);")
                txt2.Attributes.Add("onkeyup", "KeyUpEvent_txt2D(event);")
                txt3.Attributes.Add("onkeyup", "KeyUpEvent_txt3D(event);")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory_New_Edit.ControlNames()", ex)
        End Try
    End Sub

    Private Sub SetCount(ByVal cnt As Integer, ByVal TankInvTcount As Integer)
        Try


            If cnt + 1 = 1 Then
                lblof.Visible = True
                btnprevious.Enabled = False
                btnNext.Enabled = True
                btnFirst.Enabled = False
                btnLast.Enabled = True
            ElseIf cnt + 1 = TankInvTcount Then
                lblof.Visible = True
                btnprevious.Enabled = True
                btnNext.Enabled = False
                btnFirst.Enabled = True
                btnLast.Enabled = False
            ElseIf cnt + 1 < TankInvTcount Then
                lblof.Visible = True
                btnprevious.Enabled = True
                btnNext.Enabled = True
                btnFirst.Enabled = True
                btnLast.Enabled = True
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory_New_Edit.SetCount()", ex)
        End Try
    End Sub

    Private Sub fillRecordsForEdit(ByVal cnt As Integer)
        Try
            Dim ds = New DataSet()
            ds = CType(Session("TankInvDS"), DataSet)
            TankInvTcount = Session("TankInvTCnt")
            Session("currentrecord") = cnt
            VisibleControls()
            btnOk.Visible = False
            btnDelete.Visible = True
            Session("visited") = True
            If (ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString() = "R") Then '**if you edit for Tank Dipping then you will enter in the fallowing loop**

                hdfRecord.Value = ds.Tables(0).Rows(cnt)("RECORD").ToString()
                hdfEntryType.Value = ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString()

                'Added By Varun Moota to Get Entry_TYpe Value. 03/09/2010
                Session("EntryType") = ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString()
                ControlNames(ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString())
                lblNew_Edit.Text = "Edit Fuel Delivery"
                lblof.Text = cnt + 1 & " " & "of" & " " & TankInvTcount
                txt1.Text = ds.Tables(0).Rows(cnt)("QTY_MEAS").ToString() 'sqlReader(0).ToString()
                txt2.Text = ds.Tables(0).Rows(cnt)("QTY_ADDED").ToString() 'sqlReader(1).ToString()
                txt3.Text = ds.Tables(0).Rows(cnt)("PRICE_ADD").ToString() 'sqlReader(2).ToString()
                txt4.Text = Format(ds.Tables(0).Rows(cnt)("DATETIME"), "HH:mm").ToString() 'Format(sqlReader(3), "HH:mm").ToString()
                RFV_txt7.ControlToValidate = "txt4" 'Time
                REV_6.ControlToValidate = "txt5" 'Date
                txt5.Text = Format(Convert.ToDateTime(ds.Tables(0).Rows(cnt)("DATETIME")), "MM/dd/yyyy").ToString() 'Format(Convert.ToDateTime(sqlReader(3)), "MM/dd/yyyy")
                txt6.Text = ds.Tables(0).Rows(cnt)("INVOICE").ToString() 'sqlReader(4).ToString()
                txt7.Text = ds.Tables(0).Rows(cnt)("SUPPLIER").ToString() 'sqlReader(5).ToString()
                txt8.Text = ds.Tables(0).Rows(cnt)("EMPLOYEE").ToString() 'sqlReader(6).ToString()
                'Changed By Varun Moota, since We need Invoiced Amount not Record#. 08/23/2010
                txt10.Text = ds.Tables(0).Rows(cnt)("INVOICE_AMT").ToString() 'sqlReader(8).ToString
                'txt10.Text = ds.Tables(0).Rows(cnt)("RECORD").ToString() 'sqlReader(8).ToString
                DDLstTank.SelectedValue = ds.Tables(0).Rows(cnt)("TANK_NBR").ToString()
                'Added By Varun Moota, Since we are Missing INDEX# after Page Load.08/24/2010
                ddlTankSize.SelectedIndex = DDLstTank.SelectedIndex
                SetCount(cnt, TankInvTcount)
            ElseIf (ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString() = "S") Then '**if you edit for Tank Setting then you will enter in the fallowing loop**
                hdfRecord.Value = ds.Tables(0).Rows(cnt)("RECORD").ToString()
                hdfEntryType.Value = ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString()

                'Added By Varun Moota to Get Entry_TYpe Value. 03/09/2010
                Session("EntryType") = ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString()
                ControlNames(ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString())
                lblNew_Edit.Text = "Edit Tank Setting"
                txt1.Text = ds.Tables(0).Rows(cnt)("QTY_MEAS").ToString() 'sqlReader("QTY_MEAS").ToString()
                txt2.Text = ds.Tables(0).Rows(cnt)("PRICE_ADD").ToString() 'sqlReader("PRICE_ADD").ToString()
                txt3.Text = Format(ds.Tables(0).Rows(cnt)("DATETIME"), "HH:mm").ToString() 'Format(Convert.ToDateTime(sqlReader("DATETIME")), "HH:mm")
                txt4.Text = Format(Convert.ToDateTime(ds.Tables(0).Rows(cnt)("DATETIME")), "MM/dd/yyyy").ToString() 'Format(Convert.ToDateTime(sqlReader("DATETIME")), "MM/dd/yyyy")
                RFV_txt7.ControlToValidate = "txt3" 'Time
                REV_6.ControlToValidate = "txt4" 'Date
                DDLstTank.SelectedValue = ds.Tables(0).Rows(cnt)("TANK_NBR").ToString()
                SetCount(cnt, TankInvTcount)
            ElseIf (ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString() = "L") Or (ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString() = "D") Then '**if you edit for Fuel Delivery then you will enter in the fallowing loop**

                hdfRecord.Value = ds.Tables(0).Rows(cnt)("RECORD").ToString()
                hdfEntryType.Value = ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString()

                'Added By Varun Moota to Get Entry_TYpe Value. 03/09/2010
                Session("EntryType") = ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString()
                lblNew_Edit.Text = "Edit Fuel Level"
                ControlNames(ds.Tables(0).Rows(cnt)("ENTRY_TYPE").ToString())
                RFV_txt7.ControlToValidate = "txt2" 'Time
                REV_6.ControlToValidate = "txt3" 'Date
                txt1.Text = ds.Tables(0).Rows(cnt)("QTY_MEAS").ToString() 'sqlReader("QTY_MEAS").ToString()
                txt2.Text = Format(ds.Tables(0).Rows(cnt)("DATETIME"), "HH:mm").ToString() 'Format(Convert.ToDateTime(sqlReader("DATETIME")), "HH:mm")
                txt3.Text = Format(Convert.ToDateTime(ds.Tables(0).Rows(cnt)("DATETIME")), "MM/dd/yyyy").ToString() 'Format(Convert.ToDateTime(sqlReader("DATETIME")), "MM/dd/yyyy")
                DDLstTank.SelectedValue = ds.Tables(0).Rows(cnt)("TANK_NBR").ToString()
                SetCount(cnt, TankInvTcount)
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory_New_Edit.fillRecordsForEdit()", ex)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Try
            'Commeneted By Varun Moota.01/06/10
            Response.Redirect("TankInventory.aspx", False)

            'Response.Redirect("InventoryPopup.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory_New_Edit.btnCancel_Click", ex)
        End Try

    End Sub

    Protected Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Dim ds As New DataSet
        Try
            Dim cnt As Integer = 0
            ds = CType(Session("TankInvDS"), DataSet)
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = 0

            If (cnt < ds.Tables(0).Rows.Count) Then
                fillRecordsForEdit(cnt)
            End If
            Session("currentrecord") = "0"
            lblof.Text = "1 of " & Session("TankInvTCnt")
            btnprevious.Enabled = False
            btnFirst.Enabled = False
            btnNext.Enabled = True
            btnLast.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankInventory_New_Edit.btnFirst_Click", ex)
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
            ds = CType(Session("TankInvDS"), DataSet)
            cnt = Convert.ToInt32(Session("TankInvTCnt").ToString() - 1)
            If (cnt < ds.Tables(0).Rows.Count) Then
                fillRecordsForEdit(cnt)
            End If
            lblof.Text = Session("TankInvTCnt").ToString() & " of " & Session("TankInvTCnt").ToString()
            Session("currentrecord") = cnt

            btnNext.Enabled = False
            btnLast.Enabled = False

            btnprevious.Enabled = True
            btnFirst.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankInventory_New_Edit.btnLast_Click", ex)
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
            ds = CType(Session("TankInvDS"), DataSet)
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt + 1

            If (cnt < ds.Tables(0).Rows.Count) Then
                fillRecordsForEdit(cnt)
                Session("currentrecord") = cnt
            End If

            lblof.Text = cnt + 1 & " of " & Session("TankInvTCnt").ToString()

            If Not btnFirst.Enabled Then
                btnFirst.Enabled = True
                btnprevious.Enabled = True
            End If

            If (cnt + 1 = Convert.ToInt32(Session("TankInvTCnt"))) Then
                btnLast.Enabled = False
                btnNext.Enabled = False
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankInventory_New_Edit._btnNext_Click", ex)
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
            ds = CType(Session("TankInvDS"), DataSet)
            If (cnt < ds.Tables(0).Rows.Count And cnt >= 0) Then
                fillRecordsForEdit(cnt)
                Session("currentrecord") = cnt
            End If
            lblof.Text = (cnt + 1) & " of " & Session("TankInvTCnt")

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
            cr.errorlog("TankInventory_New_Edit.btnprevious_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Public Sub cleartxt()
        Try
            DDLstTank.SelectedIndex = 0
            txt1.Text = ""
            txt2.Text = ""
            txt3.Text = ""
            txt4.Text = ""
            txt5.Text = ""
            txt6.Text = ""
            txt7.Text = ""
            txt8.Text = ""
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory_New_Edit.cleartxt()", ex)
        End Try
    End Sub
    'Added By Pritam To handle Unique Key Constraint Date: 22-July-2014
    Public Sub ValidateUniqueKeyConatraint(ByVal Sflag As String)
        Try
            Dim ds2 As DataSet
            Dim dal = New GeneralizedDAL()
            Dim GenFun = New GeneralFunctions()
            Dim TANK_NBR As String
            Dim ENTRY_TYPE As String
            Dim FRTD_DateTime As String
            Dim QTY_MEAS As Integer
            Dim QTY_ADDED As Integer
            Dim DtTXTN As Date
            Dim TmTXTN As DateTime
            TANK_NBR = DDLstTank.SelectedValue.Trim()
            ENTRY_TYPE = hdfEntryType.Value.Trim()
            QTY_MEAS = Convert.ToInt32(Val(txt1.Text.Trim()))
            QTY_ADDED = Convert.ToInt32(Val(txt2.Text.Trim()))
            If ENTRY_TYPE = "R" Then
                If (txt4.Text <> "") Then
                    DtTXTN = Convert.ToDateTime(txt4.Text)
                End If
                If (txt5.Text <> "") Then
                    TmTXTN = Convert.ToDateTime(txt5.Text)
                End If
            Else
                If (txt3.Text <> "") Then
                    DtTXTN = Convert.ToDateTime(txt3.Text)
                End If
                If (txt4.Text <> "") Then
                    TmTXTN = Convert.ToDateTime(txt4.Text)
                End If
            End If
            Dim dt1 As New DateTime(DtTXTN.Year, DtTXTN.Month, DtTXTN.Day, TmTXTN.Hour, TmTXTN.Minute, TmTXTN.Second)
            FRTD_DateTime = dt1
            Dim parcollection(4) As SqlParameter

            Dim ParTANK_NBR = New SqlParameter("@TANK_NBR", SqlDbType.NVarChar, 10)
            Dim ParENTRY_TYPE = New SqlParameter("@ENTRY_TYPE", SqlDbType.NVarChar, 10)
            Dim ParDATETIME = New SqlParameter("@DATETIME", SqlDbType.DateTime)
            Dim ParQTY_MEAS = New SqlParameter("@QTY_MEAS", SqlDbType.Int)
            Dim ParQTY_ADDED = New SqlParameter("@QTY_ADDED", SqlDbType.Int)
            ParTANK_NBR.Direction = ParameterDirection.Input
            ParENTRY_TYPE.Direction = ParameterDirection.Input
            ParDATETIME.Direction = ParameterDirection.Input
            ParQTY_MEAS.Direction = ParameterDirection.Input
            ParQTY_ADDED.Direction = ParameterDirection.Input
            ParTANK_NBR.Value = TANK_NBR
            ParENTRY_TYPE.Value = ENTRY_TYPE
            ParDATETIME.Value = dt1
            ParQTY_MEAS.Value = QTY_MEAS
            ParQTY_ADDED.Value = QTY_ADDED
            parcollection(0) = ParTANK_NBR
            parcollection(1) = ParENTRY_TYPE
            parcollection(2) = ParDATETIME
            parcollection(3) = ParQTY_MEAS
            parcollection(4) = ParQTY_ADDED
            ds2 = dal.ExecuteStoredProcedureGetDataSet("USP_TT_FRTD_UniqueKeyContrain", parcollection)
            If Not ds2 Is Nothing Then
                If ds2.Tables(0).Rows.Count > 0 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script> alert('Inventory already exist Please change the time.');</script>")
                Else
                    SaveRecords()
                    If (Sflag = "btnSave_Click") Then
                        Response.Redirect("TankInventory.aspx", False)
                    End If
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

    Public Sub SaveRecords()
        Try

            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            Dim parcollection(13) As SqlParameter
            Dim ParRECORD = New SqlParameter("@RECORD", SqlDbType.Int)
            Dim ParTANK_NBR = New SqlParameter("@TANK_NBR", SqlDbType.VarChar, 3)
            Dim ParENTRY_TYPE = New SqlParameter("@ENTRY_TYPE", SqlDbType.VarChar, 1)
            Dim ParDATETIME = New SqlParameter("@DATETIME", SqlDbType.DateTime)
            Dim ParQTY_MEAS = New SqlParameter("@QTY_MEAS", SqlDbType.Decimal)
            Dim ParQTY_ADDED = New SqlParameter("@QTY_ADDED", SqlDbType.Decimal)
            Dim ParPRICE_ADD = New SqlParameter("@PRICE_ADD", SqlDbType.Decimal)
            Dim ParINVOICE = New SqlParameter("@INVOICE", SqlDbType.VarChar, 10)
            Dim ParSUPPLIER = New SqlParameter("@SUPPLIER", SqlDbType.VarChar, 10)
            Dim ParEMPLOYEE = New SqlParameter("@EMPLOYEE", SqlDbType.VarChar, 10)
            Dim ParNEWUPDT = New SqlParameter("@NEWUPDT", SqlDbType.Int)
            Dim ParINVOICE_AMT = New SqlParameter("@INVOICE_AMT", SqlDbType.Decimal)
            Dim ParAddEdit = New SqlParameter("@AddEdit", SqlDbType.VarChar, 5)
            Dim ParEntryFrom = New SqlParameter("@EntryFrom", SqlDbType.NVarChar, 50)

            ParRECORD.Direction = ParameterDirection.Input
            ParTANK_NBR.Direction = ParameterDirection.Input
            ParENTRY_TYPE.Direction = ParameterDirection.Input
            ParDATETIME.Direction = ParameterDirection.Input
            ParQTY_MEAS.Direction = ParameterDirection.Input
            ParQTY_ADDED.Direction = ParameterDirection.Input
            ParPRICE_ADD.Direction = ParameterDirection.Input
            ParINVOICE.Direction = ParameterDirection.Input
            ParSUPPLIER.Direction = ParameterDirection.Input
            ParEMPLOYEE.Direction = ParameterDirection.Input
            ParNEWUPDT.Direction = ParameterDirection.Input
            ParINVOICE_AMT.Direction = ParameterDirection.Input
            ParAddEdit.Direction = ParameterDirection.Input
            ParEntryFrom.Direction = ParameterDirection.Input

            If (Request.QueryString.Count = 2) Then
                ParAddEdit.Value = "Edit"
                ParRECORD.Value = hdfRecord.Value
                ParNEWUPDT.value = 2
            Else
                ParAddEdit.Value = "ADD"
                ParRECORD.Value = 0
                ParNEWUPDT.value = 1
            End If
            If hdfEntryType.Value.Trim() = "R" Then
                ParQTY_MEAS.Value = Convert.ToInt32(Val(txt1.Text.Trim()))
                ParQTY_ADDED.Value = Convert.ToInt32(Val(txt2.Text.Trim()))
                'ParPRICE_ADD.Value = CDbl(Val(txt3.Text.Trim()))
                If txt3.Text.Trim() = "" Then
                    ParPRICE_ADD.Value = 0.0
                Else
                    ParPRICE_ADD.Value = Double.Parse(txt3.Text.Trim())
                End If

                ParDATETIME.Value = (GenFun.ConvertDate(txt5.Text) & " " & txt4.Text)
                ParINVOICE.Value = txt6.Text.Trim().ToString()
                ParSUPPLIER.Value = txt7.Text.Trim().ToString()
                ParEMPLOYEE.Value = txt8.Text.Trim().ToString()
                If txt10.Text.Trim <> "" Then
                    ParINVOICE_AMT.Value = txt10.Text.Trim()
                Else
                    ParINVOICE_AMT.Value = 0
                End If
            ElseIf hdfEntryType.Value.Trim() = "S" Then
                ParQTY_MEAS.Value = Convert.ToInt32(Val(txt1.Text.Trim()))
                ParPRICE_ADD.Value = CDbl(Val(txt2.Text.Trim()))
                ParDATETIME.Value = (GenFun.ConvertDate(txt4.Text) & " " & txt3.Text)
                ParQTY_ADDED.Value = 0
                ParINVOICE.Value = ""
                ParSUPPLIER.Value = ""
                ParEMPLOYEE.Value = ""
                ParINVOICE_AMT.Value = 0
            ElseIf hdfEntryType.Value.Trim() = "D" Or hdfEntryType.Value.Trim() = "L" Then
                ParQTY_MEAS.Value = Convert.ToInt32(Val(txt1.Text.Trim()))
                ParDATETIME.Value = (GenFun.ConvertDate(txt3.Text) & " " & txt2.Text)
                ParQTY_ADDED.Value = 0
                ParINVOICE.Value = ""
                ParSUPPLIER.Value = ""
                ParEMPLOYEE.Value = ""
                ParPRICE_ADD.Value = 0
                ParINVOICE_AMT.Value = 0
            End If
            ParTANK_NBR.Value = DDLstTank.SelectedValue
            ParENTRY_TYPE.Value = hdfEntryType.Value.Trim()
            ParEntryFrom.Value = "UI"

            parcollection(0) = ParRECORD
            parcollection(1) = ParTANK_NBR
            parcollection(2) = ParENTRY_TYPE
            parcollection(3) = ParDATETIME
            parcollection(4) = ParQTY_MEAS
            parcollection(5) = ParQTY_ADDED
            parcollection(6) = ParPRICE_ADD
            parcollection(7) = ParINVOICE
            parcollection(8) = ParSUPPLIER
            parcollection(9) = ParEMPLOYEE
            parcollection(10) = ParNEWUPDT
            parcollection(11) = ParINVOICE_AMT
            parcollection(12) = ParAddEdit
            parcollection(13) = ParEntryFrom

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_TankInvUpdateInsert", parcollection)

            If blnFlag = True Then
                'dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_FIFOCOSTINGUpdateInsert", parcollection)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record saved successfully.');</script>")
                cleartext()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankInv_new_edit_SaveRecords", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Sub cleartext()
        Try


            txt1.Text = ""
            txt2.Text = ""
            txt3.Text = ""
            txt4.Text = ""
            txt5.Text = ""
            txt6.Text = ""
            txt7.Text = ""
            txt8.Text = ""
            txt10.Text = ""
            DDLstTank.SelectedIndex = 0
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInv_new_edit.cleartext()", ex)
        End Try


    End Sub

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click

        Try
            'SaveRecords()
            If (Request.QueryString.Count = 2) Then
                SaveRecords()
                Response.Redirect("TankInventory.aspx", False)
            Else
                ValidateUniqueKeyConatraint("btnOk_Click")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInv_new_edit.btnOk_Click", ex)
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            '*******************************This event is use for save/update data in to database*******************************
            If (lbl1.Text = "Level Prior to Delivery :") Then

                If Double.Parse(txt1.Text.ToString()) >= Double.Parse(ddlTankSize.SelectedValue.ToString()) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Tank holds a maximum of " + ddlTankSize.SelectedValue + " gallons!');</script>")
                    Exit Sub
                End If
                'If Val(txt1.Text >= Val(ddlTankSize.SelectedValue)) Then
                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Tank holds a maximum of " + ddlTankSize.SelectedValue + " gallons!');</script>")
                '    Exit Sub
                'End If
            End If

            'If (Session("visited") = True) Then
            '    'Update_Inventory()
            '    SaveRecords()
            'Else
            '    'btnOk_Click(sender, Nothing)
            '    SaveRecords()
            'End If
            'Response.Redirect("TankInventory.aspx", False)

            If (Session("visited") = True) Then
                If (Request.QueryString.Count = 2) Then
                    SaveRecords()
                    Response.Redirect("TankInventory.aspx", False)
                Else
                    ValidateUniqueKeyConatraint("btnSave_Click")
                End If
            Else
                If (Request.QueryString.Count = 2) Then
                    SaveRecords()
                    Response.Redirect("TankInventory.aspx", False)
                Else
                    ValidateUniqueKeyConatraint("btnSave_Click")
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankInv_new_edit.btnSave_Click", ex)
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
            Dim ParRECORDID = New SqlParameter("@RECORD", SqlDbType.Int)
            ParRECORDID.Direction = ParameterDirection.Input
            ParRECORDID.Value = Convert.ToInt32(hdfRecord.Value)
            parcollection(0) = ParRECORDID
            Dim blnflag As Boolean
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_TankInvDelete", parcollection)

            If blnflag = False Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Tank Inventory deleted sucessfully.');location.href='TankInventory.aspx';</script>")
                'Response.Redirect("TankInventory.aspx")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Tank_inventry_new_edit_deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub


    Protected Sub DDLstTank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLstTank.SelectedIndexChanged

        Try
            ddlTankSize.SelectedIndex = DDLstTank.SelectedIndex
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank_inventry_new_edit.DDLstTank_SelectedIndexChanged", ex)
        End Try
    End Sub

    Protected Sub btnEditCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditCancel.Click
        Try
            'Response.Redirect("InventoryPopup.aspx", False)
            Response.Redirect("TankInventory.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank_inventry_new_edit.btnEditCancel_Click", ex)

        End Try

    End Sub

    'Costing Methods In-use.VMoota(03/05/2012)
    Private Sub CheckCostingEnabled()
        Try

            Dim ds As New DataSet
            Dim dal = New GeneralizedDAL()
            ds = dal.GetDataSet("SELECT * FROM STATUS WHERE COSTING=3")
            If (ds IsNot Nothing) Then
                If ds.Tables(0).Rows.Count > 0 Then
                    If (Not ClientScript.IsStartupScriptRegistered("alert")) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "warning", "check();", True)
                    End If
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank_inventry_new_edit.CheckCostingEnabled", ex)

        End Try
    End Sub
End Class
