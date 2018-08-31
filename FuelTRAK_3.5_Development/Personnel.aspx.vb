Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class Personnel
    Inherits System.Web.UI.Page
    Dim popup As String

    Dim dv As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        
            popup = IIf(String.IsNullOrEmpty(Request.QueryString("popup")), "false", "true")
            '**************** Check for session is null/not*********************
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                If (Hidtxt.Value = "true") Then
                    If txtVehId.Value <> "" Then
                        deleteRecord(txtVehId.Value)
                        SaveLockDetails(txtVehId.Value, "K")
                        SearchClick()
                        Hidtxt.Value = ""
                        txtVehId.Value = ""
                    End If
                ElseIf (Hidtxt.Value = "false") Then
                    Hidtxt.Value = ""
                    If (IsPostBack) Then
                        SearchClick()
                    End If
                End If
                txtPersonnelID.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtPersonnelID');")
                txtLastname.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtLastname');")
                txtAccountID.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtAccountID');")
                txtKeynumber.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtKeynumber');")
                txtCardNumber.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtCardNumber');")
                txtDepartment.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtDepartment');")

                'Added By Varun to Show Search Fields 12/03/2009
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")

                If popup = "true" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")
                End If

                txtKeynumber.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                'txtLastname.Attributes.Add("OnKeyPress", "KeyPressEvent_txtLast(event);")

                Session("visited") = False
                Session("currentrecord") = "0"

                If (Not IsPostBack) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Tbl();parent.scrollTo(0,0);</script>")
                    Page.Title = "Personnel Search"
                    'Added By varun Moota to Show Search Results When Page Loads. 02/12/2010
                    SearchClick()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel.Page_Load", ex)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            SearchClick()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel.btnSearch_Click", ex)
        End Try
    End Sub

    Public Sub SearchClick()

        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")
            Dim parcollection(5) As SqlParameter

            Dim ParPersID = New SqlParameter("@PersID", SqlDbType.VarChar, 10)
            Dim ParLName = New SqlParameter("@LName", SqlDbType.VarChar, 20)
            Dim ParAccountID = New SqlParameter("@AccountID", SqlDbType.VarChar, 11)
            Dim ParKeynumber = New SqlParameter("@Keynumber", SqlDbType.VarChar, 5)
            Dim ParCardNumber = New SqlParameter("@CardNumber", SqlDbType.VarChar, 7)
            Dim ParDepartment = New SqlParameter("@Department", SqlDbType.VarChar, 3)

            ParPersID.Direction = ParameterDirection.Input
            ParLName.Direction = ParameterDirection.Input
            ParAccountID.Direction = ParameterDirection.Input
            ParKeynumber.Direction = ParameterDirection.Input
            ParCardNumber.Direction = ParameterDirection.Input
            ParDepartment.Direction = ParameterDirection.Input

            If (txtPersonnelID.Text.Trim() <> "hidden" And txtPersonnelID.Text.Trim() <> "") Then
                gridflag = 1
                ParPersID.Value = txtPersonnelID.Text.Trim()
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            ElseIf (txtLastname.Text.Trim() <> "hidden" And txtLastname.Text.Trim() <> "") Then
                gridflag = 1
                ParPersID.Value = ""
                'Added By Pritam 04-Nov-2014
                Dim Lname As String = txtLastname.Text.Trim()
                If Not Lname = "" Then
                    Lname = Replace(Replace(Replace(Lname, ";", ""), "--", ""), "'", "").ToString()
                    ParLName.Value = Lname
                Else
                    ParLName.Value = ""
                End If

                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            ElseIf (txtAccountID.Text.Trim() <> "hidden" And txtAccountID.Text.Trim() <> "") Then
                gridflag = 2
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = txtAccountID.Text.Trim()
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            ElseIf (txtKeynumber.Text.Trim() <> "hidden" And txtKeynumber.Text.Trim() <> "") Then
                gridflag = 3
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = txtKeynumber.Text.Trim()
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            ElseIf (txtCardNumber.Text.Trim() <> "hidden" And txtCardNumber.Text.Trim() <> "") Then
                gridflag = 4
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = txtCardNumber.Text.Trim()
                ParDepartment.Value = ""
            ElseIf (txtDepartment.Text.Trim() <> "hidden" And txtDepartment.Text.Trim() <> "") Then
                gridflag = 5
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = txtDepartment.Text.Trim()
            Else
                gridflag = 1
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            End If

            parcollection(0) = ParPersID
            parcollection(1) = ParLName
            parcollection(2) = ParAccountID
            parcollection(3) = ParKeynumber
            parcollection(4) = ParCardNumber
            parcollection(5) = ParDepartment

            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PersList", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    GridView1.DataSource = ds.Tables(0)
                    GridView1.DataBind()

                    dv = New DataView(ds.Tables(0))
                    Session("dvPersonnel") = dv

                    Select Case gridflag
                        Case 1
                            GridView1.Columns(5).Visible = False
                            GridView1.Columns(6).Visible = False
                            GridView1.Columns(7).Visible = False
                            GridView1.Columns(8).Visible = True
                        Case 2
                            GridView1.Columns(5).Visible = True
                            GridView1.Columns(6).Visible = False
                            GridView1.Columns(7).Visible = False
                            GridView1.Columns(8).Visible = True
                        Case 3
                            GridView1.Columns(5).Visible = False
                            GridView1.Columns(6).Visible = True
                            GridView1.Columns(7).Visible = False
                            GridView1.Columns(8).Visible = True
                        Case 4
                            GridView1.Columns(5).Visible = False
                            GridView1.Columns(6).Visible = False
                            GridView1.Columns(8).Visible = True
                            GridView1.Columns(7).Visible = True
                        Case 5
                            GridView1.Columns(8).Visible = True
                            GridView1.Columns(5).Visible = False
                            GridView1.Columns(6).Visible = False
                            GridView1.Columns(7).Visible = False
                    End Select
                Else
                    GridView1.DataSource = ds.Tables(0)
                    GridView1.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_btnSearch_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
       
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("Personnel_New_Edit.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel.Personnel_New_Edit.aspx", ex)
        End Try

       
    End Sub

    Public Sub deleteRecord(ByVal PerID As String)
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParPersID = New SqlParameter("@PersID", SqlDbType.VarChar)
            ParPersID.Direction = ParameterDirection.Input
            ParPersID.Value = PerID
            parcollection(0) = ParPersID
            Dim blnflag As Boolean
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_PersonnelDelete", parcollection)
            If blnflag = False Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record deleted Successfully.');ShowSearch();</script>")
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
	
    Private Sub SaveLockDetails(ByVal PersID As String, ByVal LockType As String)
        Try
            Dim blnFlag As Boolean

            Dim dal = New GeneralizedDAL()
            Dim dsDeletedPersonnelInfo As New DataSet
            Dim qry As String = "Select * FROM PERS WHERE [PersonnelId] = " + PersID.Trim + " AND NEWUPDT = 3"

            dsDeletedPersonnelInfo = dal.GetDataSet(qry)
            If dsDeletedPersonnelInfo.Tables(0).Rows.Count > 0 Then
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

                ParPersonalId.Value = Convert.ToInt32(PersID.Trim())
                ParFlag.Value = LockType
                ParIDENTITY.Value = dsDeletedPersonnelInfo.Tables(0).Rows(0)("IDENTITY").ToString()
                ParACCT_ID.Value = dsDeletedPersonnelInfo.Tables(0).Rows(0)("ACCT_ID").ToString()
                ParKEY_NUMBER.Value = dsDeletedPersonnelInfo.Tables(0).Rows(0)("KEY_NUMBER").ToString()
                ParCARD_ID.Value = dsDeletedPersonnelInfo.Tables(0).Rows(0)("CARD_ID").ToString()
                ParKEY_EXP.Value = dsDeletedPersonnelInfo.Tables(0).Rows(0)("KEY_EXP").ToString()
                ParCARD_EXP.Value = dsDeletedPersonnelInfo.Tables(0).Rows(0)("CARD_EXP").ToString()

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
            End If
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


    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try

     
            If ViewState("sortExpr") Is Nothing Then
                'SearchClick()
                dv = Session("dvPersonnel")
                GridView1.DataSource = dv
                GridView1.PageIndex = e.NewPageIndex
                GridView1.DataBind()

            Else

                If (GridView1.PageCount < e.NewPageIndex) Then
                    GridView1.PageIndex = 0

                    dv = Session("dvPersonnel")
                    GridView1.DataSource = dv
                    GridView1.DataBind()

                Else
                    dv = Session("dvPersonnel")
                    GridView1.DataSource = dv
                    GridView1.PageIndex = e.NewPageIndex
                    GridView1.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage

            cr.errorlog("Personnel.GridView1_PageIndexChanging", ex)
        End Try
    End Sub

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender
        Try
            'If Page.IsPostBack Then
            '    SearchClick()
            'End If
        Catch ex As Exception
            Dim cr As New ErrorPage

            cr.errorlog("Personnel.GridView1_PreRender", ex)
        End Try
        
    End Sub

     Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>check('" + GridView1.DataKeys(e.RowIndex).Value.ToString() + "')</script>")

        'Added By Varun Moota. 02/23/2010
        Try
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then

                txtVehId.Value = GridView1.DataKeys(e.RowIndex).Values.Item(1).ToString()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "check();", True)

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel.GridView1_RowDeleting", ex)
        End Try
       
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Try
            Response.Redirect("Personnel_New_Edit.aspx?PersID=" + GridView1.DataKeys(e.NewEditIndex).Values.Item(0).ToString(), False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel.GridView1_RowEditing", ex)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try

      
            If popup = "true" Then
                If Not e.Row.RowIndex = -1 Then
                    Dim script, script1 As String

                    script = """Javascript:window.opener.document.getElementById('" + Request.QueryString("txtpers").ToString() + "').value='"
                    script = script + GridView1.DataKeys(e.Row.RowIndex).Values.Item(2).ToString() + "," + GridView1.DataKeys(e.Row.RowIndex).Values.Item(3).ToString() + " " + GridView1.DataKeys(e.Row.RowIndex).Values.Item(4).ToString() + " : " + Left(GridView1.DataKeys(e.Row.RowIndex).Values.Item(1).ToString(), 2)
                    script = script + "';window.opener.document.getElementById('" + Request.QueryString("txtnum").ToString() + "').value='"
                    script = script + GridView1.DataKeys(e.Row.RowIndex).Values.Item(1).ToString()
                    script = script + "';window.opener.document.getElementById('" + Request.QueryString("txtPKey").ToString() + "').value='"
                    script = script + GridView1.DataKeys(e.Row.RowIndex).Values.Item(5).ToString()

                    script = script + "';window.close();"""

                    script1 = GridView1.DataKeys(e.Row.RowIndex).Values.Item(0).ToString()
                    GridView1.HeaderRow.Cells(0).Text = "Select"
                    GridView1.HeaderRow.Cells(1).Text = "Delete"

                    e.Row.Cells(0).Text = "<input type='button' onclick=" + script + " value='Select'/>"
                    e.Row.Cells(1).Text = "<input type='button' onclick=check('" + script1 + "') value='Delete'/>"
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Personnel.GridView1_RowDataBound", ex)
        End Try
    End Sub
    Public Function bindgrids() As DataView
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")
            Dim parcollection(5) As SqlParameter

            Dim ParPersID = New SqlParameter("@PersID", SqlDbType.VarChar, 10)
            Dim ParLName = New SqlParameter("@LName", SqlDbType.VarChar, 20)
            Dim ParAccountID = New SqlParameter("@AccountID", SqlDbType.VarChar, 11)
            Dim ParKeynumber = New SqlParameter("@Keynumber", SqlDbType.VarChar, 5)
            Dim ParCardNumber = New SqlParameter("@CardNumber", SqlDbType.VarChar, 7)
            Dim ParDepartment = New SqlParameter("@Department", SqlDbType.VarChar, 3)

            ParPersID.Direction = ParameterDirection.Input
            ParLName.Direction = ParameterDirection.Input
            ParAccountID.Direction = ParameterDirection.Input
            ParKeynumber.Direction = ParameterDirection.Input
            ParCardNumber.Direction = ParameterDirection.Input
            ParDepartment.Direction = ParameterDirection.Input

            If (txtPersonnelID.Text.Trim() <> "hidden" And txtPersonnelID.Text.Trim() <> "") Then
                gridflag = 1
                ParPersID.Value = txtPersonnelID.Text.Trim()
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            ElseIf (txtLastname.Text.Trim() <> "hidden" And txtLastname.Text.Trim() <> "") Then
                gridflag = 1
                ParPersID.Value = ""
                'Added By Pritam 04-Nov-2014
                Dim Lname As String = txtLastname.Text.Trim()
                If Not Lname = "" Then
                    Lname = Replace(Replace(Replace(Lname, ";", ""), "--", ""), "'", "").ToString()
                    ParLName.Value = Lname
                Else
                    ParLName.Value = ""
                End If

                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            ElseIf (txtAccountID.Text.Trim() <> "hidden" And txtAccountID.Text.Trim() <> "") Then
                gridflag = 2
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = txtAccountID.Text.Trim()
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            ElseIf (txtKeynumber.Text.Trim() <> "hidden" And txtKeynumber.Text.Trim() <> "") Then
                gridflag = 3
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = txtKeynumber.Text.Trim()
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            ElseIf (txtCardNumber.Text.Trim() <> "hidden" And txtCardNumber.Text.Trim() <> "") Then
                gridflag = 4
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = txtCardNumber.Text.Trim()
                ParDepartment.Value = ""
            ElseIf (txtDepartment.Text.Trim() <> "hidden" And txtDepartment.Text.Trim() <> "") Then
                gridflag = 5
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = txtDepartment.Text.Trim()
            Else
                gridflag = 1
                ParPersID.Value = ""
                ParLName.Value = ""
                ParAccountID.Value = ""
                ParKeynumber.Value = ""
                ParCardNumber.Value = ""
                ParDepartment.Value = ""
            End If

            parcollection(0) = ParPersID
            parcollection(1) = ParLName
            parcollection(2) = ParAccountID
            parcollection(3) = ParKeynumber
            parcollection(4) = ParCardNumber
            parcollection(5) = ParDepartment

            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PersList", parcollection)

            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))
                Select Case gridflag
                    Case 1
                        GridView1.Columns(5).Visible = False
                        GridView1.Columns(6).Visible = False
                        GridView1.Columns(7).Visible = False
                        GridView1.Columns(8).Visible = False
                    Case 2
                        GridView1.Columns(5).Visible = True
                        GridView1.Columns(6).Visible = False
                        GridView1.Columns(7).Visible = False
                        GridView1.Columns(8).Visible = False
                    Case 3
                        GridView1.Columns(5).Visible = False
                        GridView1.Columns(6).Visible = True
                        GridView1.Columns(7).Visible = False
                        GridView1.Columns(8).Visible = False
                    Case 4
                        GridView1.Columns(5).Visible = False
                        GridView1.Columns(6).Visible = False
                        GridView1.Columns(8).Visible = False
                        GridView1.Columns(7).Visible = True
                    Case 5
                        GridView1.Columns(8).Visible = True
                        GridView1.Columns(5).Visible = False
                        GridView1.Columns(6).Visible = False
                        GridView1.Columns(7).Visible = False
                End Select


            Else
                dv = ds.Tables(0).DefaultView
                GridView1.DataSource = dv
                GridView1.DataBind()

            End If
            ' ''If Not ds Is Nothing Then
            ' ''    If ds.Tables(0).Rows.Count > 0 Then
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()

            ' ''        Select Case gridflag
            ' ''            Case 1
            ' ''                GridView1.Columns(5).Visible = False
            ' ''                GridView1.Columns(6).Visible = False
            ' ''                GridView1.Columns(7).Visible = False
            ' ''                GridView1.Columns(8).Visible = False
            ' ''            Case 2
            ' ''                GridView1.Columns(5).Visible = True
            ' ''                GridView1.Columns(6).Visible = False
            ' ''                GridView1.Columns(7).Visible = False
            ' ''                GridView1.Columns(8).Visible = False
            ' ''            Case 3
            ' ''                GridView1.Columns(5).Visible = False
            ' ''                GridView1.Columns(6).Visible = True
            ' ''                GridView1.Columns(7).Visible = False
            ' ''                GridView1.Columns(8).Visible = False
            ' ''            Case 4
            ' ''                GridView1.Columns(5).Visible = False
            ' ''                GridView1.Columns(6).Visible = False
            ' ''                GridView1.Columns(8).Visible = False
            ' ''                GridView1.Columns(7).Visible = True
            ' ''            Case 5
            ' ''                GridView1.Columns(8).Visible = True
            ' ''                GridView1.Columns(5).Visible = False
            ' ''                GridView1.Columns(6).Visible = False
            ' ''                GridView1.Columns(7).Visible = False
            ' ''        End Select
            ' ''    Else
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''    End If
            ' ''End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel_btnSearch_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

        Return dv
    End Function
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        Dim sortExpression As String = e.SortExpression

        ViewState("sortExpr") = e.SortExpression


        Try

            If GridViewSortDirection = SortDirection.Ascending Then



                bindgrids()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "ASC"
                GridView1.DataSource = dv
                GridView1.DataBind()
                GridViewSortDirection = SortDirection.Descending


            Else


                bindgrids()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "DESC"
                GridView1.DataSource = dv
                GridView1.DataBind()
                GridViewSortDirection = SortDirection.Ascending

            End If

            Session("dvPersonnel") = dv


        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Personnel.GridView1_Sorting", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try


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
End Class
