Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class Transactions
    Inherits System.Web.UI.Page
    Dim blnSearchFlag As Boolean
    Dim dv As New DataView

    Protected Sub Page_InitComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.InitComplete

    End Sub
    Public Property GridViewSortDirection() As SortDirection
        Get

            If ViewState("sortDirection") Is Nothing Then

                ViewState("sortDirection") = SortDirection.Ascending
            End If

            Return CType(ViewState("sortDirection"), SortDirection)

        End Get
        Set(ByVal Value As SortDirection)
            ViewState("sortDirection") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                txtYear.Attributes.Add("OnKeyPress", "KeyPressYear(event);")
                txtVechKeyNo.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                DateTextBox1.Attributes.Add("OnKeyUp", "KeyUpEvent_txtDate(event,'DateTextBox1');")
                DateTextBox2.Attributes.Add("OnKeyUp", "KeyUpEvent_txtDate(event,'DateTextBox2');")
                'Added by Jatin as on 22 Aug 2008
                'Commeneted BY Varun Moota to have DOT's,Dashes in some Controls.11/19/2010
                'VehicleTextBox1.Attributes.Add("OnKeyPress", "AllowNumeric('VehicleTextBox1');")
                'VehicleTextBox2.Attributes.Add("OnKeyPress", "AllowNumeric('VehicleTextBox2');")
                'SentryTextBox1.Attributes.Add("OnKeyPress", "AllowNumeric('SentryTextBox1');")
                'SentryTextBox2.Attributes.Add("OnKeyPress", "AllowNumeric('SentryTextBox2');")
                'KeyNumberTextBox1.Attributes.Add("OnKeyPress", "AllowNumeric('KeyNumberTextBox1');")
                'KeyNumberTextBox2.Attributes.Add("OnKeyPress", "AllowNumeric('KeyNumberTextBox2');")
                'txtVechModel.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtVechModel');")
                'txtDesc.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtDesc');")
                'txtVechKeyNo.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtVechKeyNo');")
                'txtVINNo.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtVINNo');")
                '-------------
                'Allow both letters and numbers in licence no field.
                '30 Apr 09
                'Harshada
                'Issue No #144
                ' txtLicNo.Attributes.Add("OnKeyPress", "AllowNumeric('txtLicNo');")
                txtVechCrdNo.Attributes.Add("OnKeyPress", "AllowNumeric('txtVechCrdNo');")
                txtxDept.Attributes.Add("OnKeyPress", "AllowNumeric('txtxDept');")
                Session("visited") = False
                'Commented By Varun Moota to Show Searach Control. 12/18/2009
                ' ''If (Not IsPostBack) Then
                ' ''    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(0);parent.scrollTo(0,0)</script>") '**parent.scrollTo(0,0) is use for set window position to top**
                ' ''    DateTextBox1.Text = Format(Month(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Year(DateAdd(DateInterval.Day, -1, Today))
                ' ''    'To date=current date
                ' ''    '29 Apr 09
                ' ''    'Harshada
                ' ''    'Issue no #141
                ' ''    DateTextBox2.Text = Format(Month(DateAdd(DateInterval.Day, 0, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, 0, Today)), "00") & "/" & Year(DateAdd(DateInterval.Day, 0, Today))
                ' ''End If
                'Added By Varun to Show Search Fields 12/03/2009
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")
                If (Hidtxt.Value = "true") Then
                    If txtVehId.Value <> "" Then
                        DeleteRecord(Convert.ToInt32(txtVehId.Value))
                        Hidtxt.Value = ""
                        txtVehId.Value = ""
                    End If
                ElseIf (Hidtxt.Value = "false") Then
                    Hidtxt.Value = ""
                    If (IsPostBack) Then
                        blnSearchFlag = SearchCriteria()
                        If blnSearchFlag Then
                            SearchTransaction()
                        Else
                            SearchTransaction()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("Transaction.Page_load", ex)
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim GenFun As New GeneralFunctions
            If DateDiff(DateInterval.Day, GenFun.ConvertDate(DateTextBox1.Text.Trim()), GenFun.ConvertDate(DateTextBox2.Text.Trim())) >= 0 Then
                blnSearchFlag = SearchCriteria()
                If blnSearchFlag Then
                    SearchTransaction()
                Else
                    SearchTXTN()
                End If
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('From date should be less than To date.');</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction_btnSearch_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
    Private Sub SearchTransaction()
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Dim dt As Date = Now.Date
        Dim objCF As New GeneralFunctions
        Dim ds = New DataSet()
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch();</script>")
            Dim parcollection(18) As SqlParameter
            Dim ParSDate = New SqlParameter("@SDate", SqlDbType.DateTime)
            Dim ParEDate = New SqlParameter("@EDate", SqlDbType.DateTime)
            Dim ParSVeh = New SqlParameter("@SVeh", SqlDbType.VarChar, 10)
            Dim ParEVeh = New SqlParameter("@EVeh", SqlDbType.VarChar, 10)
            Dim ParSPer = New SqlParameter("@SPer", SqlDbType.VarChar, 10)
            Dim ParEPer = New SqlParameter("@EPer", SqlDbType.VarChar, 10)
            Dim ParSSentry = New SqlParameter("@SSentry", SqlDbType.VarChar, 3)
            Dim ParESentry = New SqlParameter("@ESentry", SqlDbType.VarChar, 3)
            Dim ParSVKeyNo = New SqlParameter("@SVKeyNo", SqlDbType.VarChar, 5)
            Dim ParEVKeyNo = New SqlParameter("@EVKeyNo", SqlDbType.VarChar, 5)
            Dim ParLICNO = New SqlParameter("@LICNO", SqlDbType.VarChar, 9)
            Dim ParVehModel = New SqlParameter("@VehModel", SqlDbType.VarChar, 20)
            Dim ParVehEXTENSION = New SqlParameter("@VehEXTENSION", SqlDbType.VarChar, 50)
            Dim ParVEHMAKE = New SqlParameter("@VEHMAKE", SqlDbType.VarChar, 20)
            Dim ParVEHCARD_ID = New SqlParameter("@VEHCARD_ID", SqlDbType.VarChar, 7)
            Dim ParVEHVIN = New SqlParameter("@VEHVIN", SqlDbType.VarChar, 20)
            Dim ParVehDEPT = New SqlParameter("@VehDEPT", SqlDbType.VarChar, 3)
            Dim ParVEHyear = New SqlParameter("@VEHYear", SqlDbType.VarChar, 4)
            Dim ParVehKey = New SqlParameter("@VehKey", SqlDbType.VarChar, 5)

            ParSDate.Direction = ParameterDirection.Input
            ParEDate.Direction = ParameterDirection.Input
            ParSVeh.Direction = ParameterDirection.Input
            ParEVeh.Direction = ParameterDirection.Input
            ParSPer.Direction = ParameterDirection.Input
            ParEPer.Direction = ParameterDirection.Input
            ParSSentry.Direction = ParameterDirection.Input
            ParESentry.Direction = ParameterDirection.Input
            ParSVKeyNo.Direction = ParameterDirection.Input
            ParEVKeyNo.Direction = ParameterDirection.Input
            ParLICNO.Direction = ParameterDirection.Input
            ParVehModel.Direction = ParameterDirection.Input
            ParVehEXTENSION.Direction = ParameterDirection.Input
            ParVEHMAKE.Direction = ParameterDirection.Input
            ParVEHCARD_ID.Direction = ParameterDirection.Input
            ParVEHVIN.Direction = ParameterDirection.Input
            ParVehDEPT.Direction = ParameterDirection.Input
            ParVEHyear.Direction = ParameterDirection.Input
            ParVehKey.Direction = ParameterDirection.Input
            ParSDate.Value = objCF.ConvertDate(DateTextBox1.Text.Trim())
            ParEDate.Value = objCF.ConvertDate(DateTextBox2.Text.Trim())

            If (VehicleTextBox1.Text.Trim() <> "") Then ParSVeh.Value = VehicleTextBox1.Text.Trim() Else ParSVeh.Value = ""
            If (VehicleTextBox2.Text.Trim() <> "") Then ParEVeh.Value = VehicleTextBox2.Text.Trim() Else ParEVeh.Value = ""
            If (txtPerFrom.Text.Trim() <> "") Then ParSPer.Value = txtPerFrom.Text.Trim() Else ParSPer.Value = ""
            If (txtPerTo.Text.Trim() <> "") Then ParEPer.Value = txtPerTo.Text.Trim() Else ParEPer.Value = ""
            If (SentryTextBox1.Text.Trim() <> "") Then ParSSentry.Value = SentryTextBox1.Text.Trim() Else ParSSentry.Value = ""
            If (SentryTextBox2.Text.Trim() <> "") Then ParESentry.Value = SentryTextBox2.Text.Trim() Else ParESentry.Value = ""
            If (KeyNumberTextBox1.Text.Trim() <> "") Then ParSVKeyNo.Value = KeyNumberTextBox1.Text.Trim() Else ParSVKeyNo.Value = ""
            If (KeyNumberTextBox2.Text.Trim() <> "") Then ParEVKeyNo.Value = KeyNumberTextBox2.Text.Trim() Else ParEVKeyNo.Value = ""

            If (txtLicNo.Text.Trim() <> "") Then ParLICNO.Value = txtLicNo.Text.Trim() Else ParLICNO.Value = ""
            If (txtVechModel.Text.Trim() <> "") Then ParVehModel.Value = Replace(Replace(Replace(txtVechModel.Text.Trim(), ";", ""), "--", ""), "'", "").ToString() Else ParVehModel.Value = ""
            'Added By Pritam to Avoid SQL injection Date : 12-Nov-2014
            If (txtDesc.Text.Trim() <> "") Then ParVehEXTENSION.Value = Replace(Replace(Replace(txtDesc.Text.Trim(), ";", ""), "--", ""), "'", "").ToString() Else ParVehEXTENSION.Value = ""
            If (txtVechMake.Text.Trim() <> "") Then ParVEHMAKE.Value = Replace(Replace(Replace(txtVechMake.Text.Trim(), ";", ""), "--", ""), "'", "").ToString() Else ParVEHMAKE.Value = ""
            If (txtVechCrdNo.Text.Trim() <> "") Then ParVEHCARD_ID.Value = txtVechCrdNo.Text.Trim() Else ParVEHCARD_ID.Value = ""
            'Added By Pritam to Avoid SQL injection Date : 12-Nov-2014
            If (txtVINNo.Text.Trim() <> "") Then ParVEHVIN.Value = Replace(Replace(Replace(txtVINNo.Text.Trim(), ";", ""), "--", ""), "'", "").ToString() Else ParVEHVIN.Value = ""
            If (txtxDept.Text.Trim() <> "") Then ParVehDEPT.Value = txtxDept.Text.Trim() Else ParVehDEPT.Value = ""
            If (txtYear.Text.Trim() <> "") Then ParVEHyear.Value = txtYear.Text.Trim() Else ParVEHyear.Value = ""
            If (txtVechKeyNo.Text.Trim() <> "") Then ParVehKey.Value = txtVechKeyNo.Text.Trim() Else ParVehKey.Value = ""

            parcollection(0) = ParSDate
            parcollection(1) = ParEDate
            parcollection(2) = ParSVeh
            parcollection(3) = ParEVeh
            parcollection(4) = ParSPer
            parcollection(5) = ParEPer
            parcollection(6) = ParSSentry
            parcollection(7) = ParESentry
            parcollection(8) = ParSVKeyNo
            parcollection(9) = ParEVKeyNo
            parcollection(10) = ParLICNO
            parcollection(11) = ParVehModel
            parcollection(12) = ParVehEXTENSION
            parcollection(13) = ParVEHMAKE
            parcollection(14) = ParVEHCARD_ID
            parcollection(15) = ParVEHVIN
            parcollection(16) = ParVehDEPT
            parcollection(17) = ParVEHyear
            parcollection(18) = ParVehKey
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransactionSearchRecords", parcollection)
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))
                dv.Sort = CType(ViewState("sortExpr"), String)
                GridView1.DataSource = dv
                GridView1.DataBind()
            Else
                dv = ds.Tables(0).DefaultView

                GridView1.DataSource = dv
                GridView1.DataBind()
            End If
            ''Commeneted BY Varun Moota
            ' ''If Not ds Is Nothing Then
            ' ''    If ds.Tables(0).Rows.Count > 0 Then
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''    Else
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''        txtLicNo.Text = "" 'And txtLicNo.Text.Trim() <> "") Then ParLICNO.Value = txtLicNo.Text.Trim() Else ParLICNO.Value = ""
            ' ''        txtVechModel.Text = "" 'And txtVechModel.Text.Trim() <> "") Then ParVehModel.Value = txtVechModel.Text.Trim() Else ParVehModel.Value = ""
            ' ''        txtDesc.Text = "" 'And 'txtDesc.Text.Trim() <> "") Then ParVehEXTENSION.Value = txtDesc.Text.Trim() Else ParVehEXTENSION.Value = ""
            ' ''        txtVechMake.Text = "" 'And txtVechMake.Text.Trim() <> "") Then ParVEHMAKE.Value = txtVechMake.Text.Trim() Else ParVEHMAKE.Value = ""
            ' ''        txtVechCrdNo.Text = "" 'And txtVechCrdNo.Text.Trim() <> "") Then ParVEHCARD_ID.Value = txtVechCrdNo.Text.Trim() Else ParVEHCARD_ID.Value = ""
            ' ''        txtVINNo.Text = "" 'And txtVINNo.Text.Trim() <> "") Then ParVEHVIN.Value = txtVINNo.Text.Trim() Else ParVEHVIN.Value = ""
            ' ''        txtxDept.Text = "" 'And txtxDept.Text.Trim() <> "") Then ParVehDEPT.Value = txtxDept.Text.Trim() Else ParVehDEPT.Value = ""
            ' ''        txtYear.Text = "" 'And txtYear.Text.Trim() <> "") Then ParVEHyear.Value = txtYear.Text.Trim() Else ParVEHyear.Value = ""
            ' ''        txtVechKeyNo.Text = ""
            ' ''    End If
            ' ''End If
            If (txtLicNo.Text.Trim() <> "") Then
                gridflag = 10
            ElseIf (txtVechMake.Text.Trim() <> "") Then
                gridflag = 11
            ElseIf (txtVechModel.Text.Trim() <> "") Then
                gridflag = 12
            ElseIf (txtDesc.Text.Trim() <> "") Then
                gridflag = 13
            ElseIf (txtVechCrdNo.Text.Trim() <> "") Then
                gridflag = 14
            ElseIf (txtVINNo.Text.Trim() <> "") Then
                gridflag = 15
            ElseIf (txtxDept.Text.Trim() <> "") Then
                gridflag = 16
            ElseIf (txtYear.Text.Trim() <> "") Then
                gridflag = 1
            Else
                gridflag = 1
            End If
            HideGridColumn(gridflag)

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Transaction.SearchTransactions", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
    Private Sub HideGridColumn(ByVal GCols As Integer)
        Try
            Select Case GCols
                Case 1
                    GridView1.Columns(5).Visible = True
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                    GridView1.Columns(12).Visible = False
                    GridView1.Columns(13).Visible = False
                    GridView1.Columns(14).Visible = True
                    GridView1.Columns(15).Visible = False
                    GridView1.Columns(16).Visible = False
                Case 10
                    GridView1.Columns(5).Visible = True
                    GridView1.Columns(10).Visible = True
                    GridView1.Columns(11).Visible = False
                    GridView1.Columns(12).Visible = False
                    GridView1.Columns(13).Visible = False
                    GridView1.Columns(14).Visible = True
                    GridView1.Columns(15).Visible = False
                    GridView1.Columns(16).Visible = False
                Case 11
                    GridView1.Columns(5).Visible = True
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = True
                    GridView1.Columns(12).Visible = False
                    GridView1.Columns(13).Visible = False
                    GridView1.Columns(14).Visible = True
                    GridView1.Columns(15).Visible = False
                    GridView1.Columns(16).Visible = False
                Case 12
                    GridView1.Columns(5).Visible = True
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                    GridView1.Columns(12).Visible = True
                    GridView1.Columns(13).Visible = False
                    GridView1.Columns(14).Visible = True
                    GridView1.Columns(15).Visible = False
                    GridView1.Columns(16).Visible = False
                Case 13
                    GridView1.Columns(5).Visible = True
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                    GridView1.Columns(12).Visible = False
                    GridView1.Columns(13).Visible = True
                    GridView1.Columns(14).Visible = True
                    GridView1.Columns(15).Visible = False
                    GridView1.Columns(16).Visible = False
                Case 14
                    GridView1.Columns(5).Visible = True
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                    GridView1.Columns(12).Visible = False
                    GridView1.Columns(13).Visible = False
                    GridView1.Columns(14).Visible = True
                    GridView1.Columns(15).Visible = False
                    GridView1.Columns(16).Visible = False
                Case 15
                    GridView1.Columns(5).Visible = True
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                    GridView1.Columns(12).Visible = False
                    GridView1.Columns(13).Visible = False
                    GridView1.Columns(14).Visible = True
                    GridView1.Columns(15).Visible = True
                    GridView1.Columns(16).Visible = False
                Case 16
                    GridView1.Columns(5).Visible = True
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                    GridView1.Columns(12).Visible = False
                    GridView1.Columns(13).Visible = False
                    GridView1.Columns(14).Visible = True
                    GridView1.Columns(15).Visible = False
                    GridView1.Columns(16).Visible = True
            End Select
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction.HideGridColumn", ex)
        End Try

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click

        Try
            Response.Redirect("Transaction_New_Edit.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction.btnNew_Click", ex)
        End Try
    End Sub

    Public Sub DeleteRecord(ByVal RECORDID As Integer)
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParRECORDID = New SqlParameter("@RECORDID", SqlDbType.Int)
            ParRECORDID.Direction = ParameterDirection.Input
            ParRECORDID.Value = RECORDID
            parcollection(0) = ParRECORDID
            Dim blnflag As Boolean
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_TransDelete", parcollection)
            If blnflag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Transaction deleted sucessfully.')</script>")
            End If
            'SearchTransaction()
            SearchTXTN()
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TransactionSearch_deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try

 
            GridView1.PageIndex = e.NewPageIndex
            'SearchTransaction()
            SearchTXTN()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction.GridView1_PageIndexChanging", ex)
        End Try
    End Sub

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender
        Try

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Try
            Dim i As Integer = e.NewEditIndex
            Response.Redirect("Transaction_New_Edit.aspx?TxtnNo=" + GridView1.DataKeys(e.NewEditIndex).Values.Item(0).ToString(), False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction.GridView1_RowEditing", ex)
        End Try
       
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>check('" + GridView1.DataKeys(e.RowIndex).Values.Item(0).ToString() + "')</script>")
        Try

      
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "check('" + GridView1.DataKeys(e.RowIndex).Values.Item(0).ToString() + "');", True)

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Transaction.GridView1_RowDeleting", ex)
        End Try
    End Sub
    'Added By Varun Moota to Show all the Legitimate TXTN's As Per John's Request.  04/06/2010

    Private Sub SearchTXTN()
        Try
            Dim dal = New GeneralizedDAL()
            Dim dt As Date = Now.Date
            Dim objCF As New GeneralFunctions
            Dim ds = New DataSet()

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch();</script>")

            Dim parcollection(18) As SqlParameter

            Dim ParSDate = New SqlParameter("@SDate", SqlDbType.DateTime)
            Dim ParEDate = New SqlParameter("@EDate", SqlDbType.DateTime)
            Dim ParSVeh = New SqlParameter("@SVeh", SqlDbType.VarChar, 10)
            Dim ParEVeh = New SqlParameter("@EVeh", SqlDbType.VarChar, 10)
            Dim ParSPer = New SqlParameter("@SPer", SqlDbType.VarChar, 10)
            Dim ParEPer = New SqlParameter("@EPer", SqlDbType.VarChar, 10)
            Dim ParSSentry = New SqlParameter("@SSentry", SqlDbType.VarChar, 3)
            Dim ParESentry = New SqlParameter("@ESentry", SqlDbType.VarChar, 3)
            Dim ParSVKeyNo = New SqlParameter("@SVKeyNo", SqlDbType.VarChar, 5)
            Dim ParEVKeyNo = New SqlParameter("@EVKeyNo", SqlDbType.VarChar, 5)
            Dim ParLICNO = New SqlParameter("@LICNO", SqlDbType.VarChar, 9)
            Dim ParVehModel = New SqlParameter("@VehModel", SqlDbType.VarChar, 20)
            Dim ParVehEXTENSION = New SqlParameter("@VehEXTENSION", SqlDbType.VarChar, 50)
            Dim ParVEHMAKE = New SqlParameter("@VEHMAKE", SqlDbType.VarChar, 20)
            Dim ParVEHCARD_ID = New SqlParameter("@VEHCARD_ID", SqlDbType.VarChar, 7)
            Dim ParVEHVIN = New SqlParameter("@VEHVIN", SqlDbType.VarChar, 20)
            Dim ParVehDEPT = New SqlParameter("@VehDEPT", SqlDbType.VarChar, 3)
            Dim ParVEHyear = New SqlParameter("@VEHYear", SqlDbType.VarChar, 4)
            Dim ParVehKey = New SqlParameter("@VehKey", SqlDbType.VarChar, 5)

            ParSDate.Direction = ParameterDirection.Input
            ParEDate.Direction = ParameterDirection.Input
            ParSVeh.Direction = ParameterDirection.Input
            ParEVeh.Direction = ParameterDirection.Input
            ParSPer.Direction = ParameterDirection.Input
            ParEPer.Direction = ParameterDirection.Input
            ParSSentry.Direction = ParameterDirection.Input
            ParESentry.Direction = ParameterDirection.Input
            ParSVKeyNo.Direction = ParameterDirection.Input
            ParEVKeyNo.Direction = ParameterDirection.Input
            ParLICNO.Direction = ParameterDirection.Input
            ParVehModel.Direction = ParameterDirection.Input
            ParVehEXTENSION.Direction = ParameterDirection.Input
            ParVEHMAKE.Direction = ParameterDirection.Input
            ParVEHCARD_ID.Direction = ParameterDirection.Input
            ParVEHVIN.Direction = ParameterDirection.Input
            ParVehDEPT.Direction = ParameterDirection.Input
            ParVEHyear.Direction = ParameterDirection.Input
            ParVehKey.Direction = ParameterDirection.Input
           

            ParSDate.Value = objCF.ConvertDate(DateTextBox1.Text.Trim())
            ParEDate.Value = objCF.ConvertDate(DateTextBox2.Text.Trim())

            If (VehicleTextBox1.Text.Trim() <> "") Then ParSVeh.Value = VehicleTextBox1.Text.Trim() Else ParSVeh.Value = ""
            If (VehicleTextBox2.Text.Trim() <> "") Then ParEVeh.Value = VehicleTextBox2.Text.Trim() Else ParEVeh.Value = ""
            If (txtPerFrom.Text.Trim() <> "") Then ParSPer.Value = txtPerFrom.Text.Trim() Else ParSPer.Value = ""
            If (txtPerTo.Text.Trim() <> "") Then ParEPer.Value = txtPerTo.Text.Trim() Else ParEPer.Value = ""
            If (SentryTextBox1.Text.Trim() <> "") Then ParSSentry.Value = SentryTextBox1.Text.Trim() Else ParSSentry.Value = ""
            If (SentryTextBox2.Text.Trim() <> "") Then ParESentry.Value = SentryTextBox2.Text.Trim() Else ParESentry.Value = ""
            If (KeyNumberTextBox1.Text.Trim() <> "") Then ParSVKeyNo.Value = KeyNumberTextBox1.Text.Trim() Else ParSVKeyNo.Value = ""
            If (KeyNumberTextBox2.Text.Trim() <> "") Then ParEVKeyNo.Value = KeyNumberTextBox2.Text.Trim() Else ParEVKeyNo.Value = ""

            If (txtLicNo.Text.Trim() <> "") Then ParLICNO.Value = txtLicNo.Text.Trim() Else ParLICNO.Value = ""
            If (txtVechModel.Text.Trim() <> "") Then ParVehModel.Value = Replace(Replace(Replace(txtVechModel.Text.Trim(), ";", ""), "--", ""), "'", "").ToString() Else ParVehModel.Value = ""
            'Added By Pritam to Avoid SQL injection Date : 12-Nov-2014
            If (txtDesc.Text.Trim() <> "") Then ParVehEXTENSION.Value = Replace(Replace(Replace(txtDesc.Text.Trim(), ";", ""), "--", ""), "'", "").ToString() Else ParVehEXTENSION.Value = ""
            If (txtVechMake.Text.Trim() <> "") Then ParVEHMAKE.Value = Replace(Replace(Replace(txtVechMake.Text.Trim(), ";", ""), "--", ""), "'", "").ToString() Else ParVEHMAKE.Value = ""
            If (txtVechCrdNo.Text.Trim() <> "") Then ParVEHCARD_ID.Value = txtVechCrdNo.Text.Trim() Else ParVEHCARD_ID.Value = ""
            'Added By Pritam to Avoid SQL injection Date : 12-Nov-2014
            If (txtVINNo.Text.Trim() <> "") Then ParVEHVIN.Value = Replace(Replace(Replace(txtVINNo.Text.Trim(), ";", ""), "--", ""), "'", "").ToString() Else ParVEHVIN.Value = ""
            If (txtxDept.Text.Trim() <> "") Then ParVehDEPT.Value = txtxDept.Text.Trim() Else ParVehDEPT.Value = ""
            If (txtYear.Text.Trim() <> "") Then ParVEHyear.Value = txtYear.Text.Trim() Else ParVEHyear.Value = ""

            If (txtVechKeyNo.Text.Trim() <> "") Then ParVehKey.Value = txtVechKeyNo.Text.Trim() Else ParVehKey.Value = ""
            parcollection(0) = ParSDate
            parcollection(1) = ParEDate
            parcollection(2) = ParSVeh
            parcollection(3) = ParEVeh
            parcollection(4) = ParSPer
            parcollection(5) = ParEPer
            parcollection(6) = ParSSentry
            parcollection(7) = ParESentry
            parcollection(8) = ParSVKeyNo
            parcollection(9) = ParEVKeyNo
            parcollection(10) = ParLICNO
            parcollection(11) = ParVehModel
            parcollection(12) = ParVehEXTENSION
            parcollection(13) = ParVEHMAKE
            parcollection(14) = ParVEHCARD_ID
            parcollection(15) = ParVEHVIN
            parcollection(16) = ParVehDEPT
            parcollection(17) = ParVEHyear
            parcollection(18) = ParVehKey
            'ds = dal.ExecuteStoredProcedureGetDataSet("SP_TRANSACTIONLIST", parcollection)//Commented since bug while Sorting.08/24/2011
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TransactionSearchRecords", parcollection)

            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))
                dv.Sort = CType(ViewState("sortExpr"), String)
                GridView1.DataSource = dv
                GridView1.DataBind()
            Else
                dv = ds.Tables(0).DefaultView
                GridView1.DataSource = dv
                GridView1.DataBind()
            End If
            'Commented By Varun Moota
            ' ''If Not ds Is Nothing Then
            ' ''    If ds.Tables(0).Rows.Count > 0 Then
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''    Else
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''        txtLicNo.Text = "" 'And txtLicNo.Text.Trim() <> "") Then ParLICNO.Value = txtLicNo.Text.Trim() Else ParLICNO.Value = ""
            ' ''        txtVechModel.Text = "" 'And txtVechModel.Text.Trim() <> "") Then ParVehModel.Value = txtVechModel.Text.Trim() Else ParVehModel.Value = ""
            ' ''        txtDesc.Text = "" 'And 'txtDesc.Text.Trim() <> "") Then ParVehEXTENSION.Value = txtDesc.Text.Trim() Else ParVehEXTENSION.Value = ""
            ' ''        txtVechMake.Text = "" 'And txtVechMake.Text.Trim() <> "") Then ParVEHMAKE.Value = txtVechMake.Text.Trim() Else ParVEHMAKE.Value = ""
            ' ''        txtVechCrdNo.Text = "" 'And txtVechCrdNo.Text.Trim() <> "") Then ParVEHCARD_ID.Value = txtVechCrdNo.Text.Trim() Else ParVEHCARD_ID.Value = ""
            ' ''        txtVINNo.Text = "" 'And txtVINNo.Text.Trim() <> "") Then ParVEHVIN.Value = txtVINNo.Text.Trim() Else ParVEHVIN.Value = ""
            ' ''        txtxDept.Text = "" 'And txtxDept.Text.Trim() <> "") Then ParVehDEPT.Value = txtxDept.Text.Trim() Else ParVehDEPT.Value = ""
            ' ''        txtYear.Text = "" 'And txtYear.Text.Trim() <> "") Then ParVEHyear.Value = txtYear.Text.Trim() Else ParVEHyear.Value = ""
            ' ''        txtVechKeyNo.Text = ""
            ' ''    End If
            ' ''End If
            HideGridColumn(16)
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TransactionSearch_SearchTXTN", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try


    End Sub
    'Added By Varun Moota to Check ONLY IF None of the Search Criteria is being given Except DateTime.04/06/2010
    Private Function SearchCriteria() As Boolean
        Try
            If VehicleTextBox1.Text.Trim <> "" Then
                Return True
            ElseIf VehicleTextBox2.Text <> "" Then
                Return True
            ElseIf txtPerFrom.Text <> "" Then
                Return True
            ElseIf txtPerTo.Text <> "" Then
                Return True
            ElseIf SentryTextBox1.Text <> "" Then
                Return True
            ElseIf SentryTextBox2.Text <> "" Then
                Return True
            ElseIf KeyNumberTextBox1.Text <> "" Then
                Return True
            ElseIf KeyNumberTextBox2.Text <> "" Then
                Return True
            ElseIf txtLicNo.Text <> "" Then
                Return True
            ElseIf txtVechModel.Text <> "" Then
                Return True
            ElseIf txtDesc.Text <> "" Then
                Return True
            ElseIf txtVechMake.Text <> "" Then
                Return True
            ElseIf txtVechCrdNo.Text <> "" Then
                Return True
            ElseIf txtVINNo.Text <> "" Then
                Return True
            ElseIf txtxDept.Text <> "" Then
                Return True
            ElseIf txtYear.Text <> "" Then
                Return True
            ElseIf txtVechKeyNo.Text <> "" Then
                Return True
            Else
                Return False

            End If
         
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TransactionSearch_SearchCriteria", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

        Return False
    End Function


    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        Dim sortExpression As String = e.SortExpression

        ViewState("sortExpr") = e.SortExpression


        Try

            If GridViewSortDirection = SortDirection.Ascending Then



                SearchTXTN()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "ASC"
                GridView1.DataSource = dv
                GridView1.DataBind()
                GridViewSortDirection = SortDirection.Descending


            Else


                SearchTXTN()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "DESC"
                GridView1.DataSource = dv
                GridView1.DataBind()
                GridViewSortDirection = SortDirection.Ascending

            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehicle.GridView1_Sorting", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try
    End Sub
End Class
