Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class Vehicle
    Inherits System.Web.UI.Page
    Dim str_Connection_string As String = ConfigurationManager.AppSettings("str_Connection_string")
    Dim popup As String
    Dim dv As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       

        Try
            '******************* In this event we check for session is to be null or not****************************
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                If (Not IsPostBack) Then

                    popup = IIf(String.IsNullOrEmpty(Request.QueryString("popup")), "false", "true")
                    'Added By Varun to Show Search Fields 12/03/2009
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")


                    'Commeneted By Varun Moota to Test Controls(12/03/2009)
                    ' ''If popup = "true" Then
                    ' ''    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(0)</script>")
                    ' ''End If
                    txtKeyNo.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtYear.Attributes.Add("OnKeyPress", "KeyPressEvent_Year(event);")
                    'Commeneted BY Varun Moota to have DOT's,Dashes in some Controls.11/19/2010
                    'txtVehicleID.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtVehicleID');")
                    'txtLicenseNo.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtLicenseNo');")
                    'txtMake.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtMake');")
                    'txtModel.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtModel');")
                    'txtKeyNo.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtKeyNo');")
                    'txtCardNo.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtCardNo');")
                    'txtDepartment.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtDepartment');")
                    'txtDescription.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtDescription');")
                    'txtVinNumber.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtVinNumber');")
                    'txtYear.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtYear');")



                    Dim tbl As New Table
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Tbl();parent.scrollTo(0,0);</script>")
                    Page.Title = "Vehicle Search"


                    'Added By Varun Moota to show Search Results when it loads.02/12/2010
                    tdsearch.Visible = True
                    SearchClick()
                End If

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

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle.Page_Load", ex)
        End Try
    End Sub

     Public Sub deleteRecord(ByVal VehID As String)
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParVehID = New SqlParameter("@VehicleId", SqlDbType.VarChar)
            ParVehID.Direction = ParameterDirection.Input
            ParVehID.Value = VehID
            parcollection(0) = ParVehID
            Dim blnflag As Boolean
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_VehicleDelete", parcollection)
            If blnflag = False Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Vehicle deleted sucessfully.');</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehicle.deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
       
    'Added By varun Moota, to Lock Key or Card as Well Once, Vehilce Deleted.
    Private Sub SaveLockDetails(ByVal VehID As String, ByVal LockType As String)
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            Dim dsDeletedVehInfo As New DataSet
            Dim qry As String = "Select * FROM VEHS WHERE [VehicleId] = " + VehID.Trim + " AND NEWUPDT = 3"
            dsDeletedVehInfo = dal.GetDataSet(qry)
            If dsDeletedVehInfo.Tables(0).Rows.Count > 0 Then
                Dim parcollection(7) As SqlParameter
                Dim ParVehicleId = New SqlParameter("@VehicleId", SqlDbType.Int)
                Dim ParIDENTITY = New SqlParameter("@IDENTITY", SqlDbType.VarChar, 10)
                Dim ParACCT_ID = New SqlParameter("@ACCT_ID", SqlDbType.VarChar, 11)
                Dim ParKEY_NUMBER = New SqlParameter("@KEY_NUMBER", SqlDbType.VarChar, 5)
                Dim ParCARD_ID = New SqlParameter("@CARD_ID", SqlDbType.VarChar, 7)
                Dim ParKEY_EXP = New SqlParameter("@KEY_EXP", SqlDbType.VarChar, 50)
                Dim ParCARD_EXP = New SqlParameter("@CARD_EXP", SqlDbType.VarChar, 50)
                Dim ParFlag = New SqlParameter("@Flag", SqlDbType.VarChar, 5)
                ParVehicleId.Direction = ParameterDirection.Input
                ParIDENTITY.Direction = ParameterDirection.Input
                ParACCT_ID.Direction = ParameterDirection.Input
                ParKEY_NUMBER.Direction = ParameterDirection.Input
                ParCARD_ID.Direction = ParameterDirection.Input
                ParKEY_EXP.Direction = ParameterDirection.Input
                ParCARD_EXP.Direction = ParameterDirection.Input
                ParFlag.Direction = ParameterDirection.Input

                ParVehicleId.Value = Convert.ToInt32(VehID.Trim())
                ParFlag.Value = LockType
                ParIDENTITY.Value = dsDeletedVehInfo.Tables(0).Rows(0)("IDENTITY").ToString()
                ParACCT_ID.Value = dsDeletedVehInfo.Tables(0).Rows(0)("ACCT_ID").ToString()
                ParKEY_NUMBER.Value = dsDeletedVehInfo.Tables(0).Rows(0)("KEY_NUMBER").ToString()
                ParCARD_ID.Value = dsDeletedVehInfo.Tables(0).Rows(0)("CARD_ID").ToString()
                ParKEY_EXP.Value = dsDeletedVehInfo.Tables(0).Rows(0)("KEY_EXP").ToString()
                ParCARD_EXP.Value = dsDeletedVehInfo.Tables(0).Rows(0)("CARD_EXP").ToString()
                parcollection(0) = ParVehicleId
                parcollection(1) = ParIDENTITY
                parcollection(2) = ParACCT_ID
                parcollection(3) = ParKEY_NUMBER
                parcollection(4) = ParCARD_ID
                parcollection(5) = ParKEY_EXP
                parcollection(6) = ParCARD_EXP
                parcollection(7) = ParFlag

                blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_VeHLockCardKey", parcollection)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>location.href='Vehicle.aspx';</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehicle.SaveLockDetails", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            tdsearch.Visible = True
            SearchClick()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehivle.btnSearch_Click", ex)
        End Try
        
    End Sub

    Public Sub SearchClick()
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Dim strSearch As String = ""
        Try
            'Page.ClientScript.RegisterStartupScript(Me.GetType(),"javascript", "<script>HideControls(0)</script>")
            Dim parcollection(9) As SqlParameter
            Dim ParVehicleId = New SqlParameter("@VehicleId", SqlDbType.VarChar, 10)
            Dim ParLICNO = New SqlParameter("@LICNO", SqlDbType.VarChar, 9)
            Dim ParVEHMAKE = New SqlParameter("@VEHMAKE", SqlDbType.VarChar, 20)
            Dim ParVEHMODEL = New SqlParameter("@VEHMODEL", SqlDbType.VarChar, 20)
            Dim ParKEY_NUMBER = New SqlParameter("@KEY_NUMBER", SqlDbType.VarChar, 5)
            Dim ParCARD_ID = New SqlParameter("@CARD_ID", SqlDbType.VarChar, 7)
            Dim ParDEPT = New SqlParameter("@DEPT", SqlDbType.VarChar, 3)
            Dim ParEXTENSION = New SqlParameter("@EXTENSION", SqlDbType.VarChar, 50)
            Dim ParVEHVIN = New SqlParameter("@VEHVIN", SqlDbType.VarChar, 20)
            Dim ParVEHYEAR = New SqlParameter("@VEHYEAR", SqlDbType.VarChar, 4)
            ParVehicleId.Direction = ParameterDirection.Input
            ParLICNO.Direction = ParameterDirection.Input
            ParVEHMAKE.Direction = ParameterDirection.Input
            ParVEHMODEL.Direction = ParameterDirection.Input
            ParKEY_NUMBER.Direction = ParameterDirection.Input
            ParCARD_ID.Direction = ParameterDirection.Input
            ParDEPT.Direction = ParameterDirection.Input
            ParEXTENSION.Direction = ParameterDirection.Input
            ParVEHVIN.Direction = ParameterDirection.Input
            ParVEHYEAR.Direction = ParameterDirection.Input
            ParVehicleId.Value = ""
            ParLICNO.Value = ""
            ParVEHMAKE.Value = ""
            ParVEHMODEL.Value = ""
            ParKEY_NUMBER.Value = ""
            ParCARD_ID.Value = ""
            ParDEPT.Value = ""
            ParEXTENSION.Value = ""
            ParVEHVIN.Value = ""
            ParVEHYEAR.Value = ""
            If (txtVehicleID.Text.Trim() <> "hidden" And txtVehicleID.Text.Trim() <> "") Then
                ParVehicleId.Value = txtVehicleID.Text.Trim()
                strSearch = "by VehicleID"
                gridflag = 1
            ElseIf (txtLicenseNo.Text.Trim() <> "hidden" And txtLicenseNo.Text.Trim() <> "") Then
                ParLICNO.Value = txtLicenseNo.Text.Trim()
                strSearch = "by License No"
                gridflag = 2
            ElseIf (txtMake.Text.Trim() <> "hidden" And txtMake.Text.Trim() <> "") Then
                ' Added By Pritam 11-Nov-2014
                Dim VehMake As String = txtMake.Text.Trim()
                VehMake = Replace(Replace(Replace(VehMake, ";", ""), "--", ""), "'", "").ToString()
                ParVEHMAKE.Value = VehMake
                strSearch = "by Make"
                gridflag = 3
            ElseIf (txtModel.Text.Trim <> "hidden" And txtModel.Text.Trim() <> "") Then
                ParVEHMODEL.Value = txtModel.Text.Trim
                strSearch = "by Model"
                gridflag = 4
            ElseIf (txtKeyNo.Text.Trim() <> "hidden" And txtKeyNo.Text.Trim() <> "") Then
                ParKEY_NUMBER.Value = txtKeyNo.Text.Trim()
                strSearch = "by Key No"
                gridflag = 5
            ElseIf (txtCardNo.Text.Trim() <> "hidden" And txtCardNo.Text.Trim() <> "") Then
                ParCARD_ID.Value = txtCardNo.Text.Trim()
                strSearch = "by Card No"
                gridflag = 6
            ElseIf (txtDepartment.Text.Trim() <> "hidden" And txtDepartment.Text.Trim() <> "") Then
                ParDEPT.Value = txtDepartment.Text.Trim()
                strSearch = "by Department"
                gridflag = 1
            ElseIf (txtDescription.Text.Trim() <> "hidden" And txtDescription.Text.Trim() <> "") Then
                ' Added By Pritam 11-Nov-2014
                Dim Desc As String = txtDescription.Text.Trim()
                Desc = Replace(Replace(Replace(Desc, ";", ""), "--", ""), "'", "").ToString()
                ParEXTENSION.Value = Desc
                strSearch = "by Description"
                gridflag = 1
            ElseIf (txtVinNumber.Text.Trim() <> "hidden" And txtVinNumber.Text.Trim() <> "") Then
                ' Added By Pritam 11-Nov-2014
                Dim VehvinNumber As String = txtVinNumber.Text.Trim()
                VehvinNumber = Replace(Replace(Replace(VehvinNumber, ";", ""), "--", ""), "'", "").ToString()
                ParVEHMAKE.Value = VehvinNumber
                strSearch = "by Vin Number"
                gridflag = 7
            ElseIf (txtYear.Text.Trim() <> "hidden" And txtYear.Text.Trim() <> "") Then
                ParVEHYEAR.Value = txtYear.Text.Trim()
                strSearch = "by Year"
                gridflag = 8
            End If
            parcollection(0) = ParVehicleId
            parcollection(1) = ParLICNO
            parcollection(2) = ParVEHMAKE
            parcollection(3) = ParVEHMODEL
            parcollection(4) = ParKEY_NUMBER
            parcollection(5) = ParCARD_ID
            parcollection(6) = ParDEPT
            parcollection(7) = ParEXTENSION
            parcollection(8) = ParVEHVIN
            parcollection(9) = ParVEHYEAR
            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehicleList", parcollection)
            dv = New DataView(ds.Tables(0))
            Session("dvVehicle") = dv
            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))
                dv.Sort = CType(ViewState("sortExpr"), String)
                GridView1.DataSource = dv
                GridView1.DataBind()
                HideGridCols(gridflag)
                tdsearch.Visible = True
                lblresult.Text = "Search Result"
                lblresult.Text = lblresult.Text + " " + strSearch
            Else
                dv = ds.Tables(0).DefaultView
                GridView1.DataSource = dv
                GridView1.DataBind()
                tdsearch.Visible = False 'strSearch
            End If

            ' ''If Not ds Is Nothing Then
            ' ''    If ds.Tables(0).Rows.Count > 0 Then
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''        HideGridCols(gridflag)
            ' ''        tdsearch.Visible = True
            ' ''        lblresult.Text = "Search Result"
            ' ''        lblresult.Text = lblresult.Text + " " + strSearch
            ' ''    Else
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''        tdsearch.Visible = False 'strSearch
            ' ''    End If

            ' ''    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(0)</script>")
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
    End Sub
    Private Sub HideGridCols(ByVal gridflag As Integer)
        Try
            Select Case gridflag
                Case 1
                    GridView1.Columns(5).Visible = False
                    GridView1.Columns(6).Visible = False
                    GridView1.Columns(7).Visible = False
                    GridView1.Columns(8).Visible = False
                    GridView1.Columns(9).Visible = False
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                Case 2
                    GridView1.Columns(5).Visible = True
                    GridView1.Columns(6).Visible = False
                    GridView1.Columns(7).Visible = False
                    GridView1.Columns(8).Visible = False
                    GridView1.Columns(9).Visible = False
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                Case 3
                    GridView1.Columns(6).Visible = True
                    GridView1.Columns(5).Visible = False
                    GridView1.Columns(7).Visible = False
                    GridView1.Columns(8).Visible = False
                    GridView1.Columns(9).Visible = False
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                Case 4
                    GridView1.Columns(7).Visible = True
                    GridView1.Columns(5).Visible = False
                    GridView1.Columns(6).Visible = False
                    GridView1.Columns(8).Visible = False
                    GridView1.Columns(9).Visible = False
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                Case 5
                    GridView1.Columns(8).Visible = True
                    GridView1.Columns(5).Visible = False
                    GridView1.Columns(6).Visible = False
                    GridView1.Columns(7).Visible = False
                    GridView1.Columns(9).Visible = False
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                Case 6
                    GridView1.Columns(9).Visible = True
                    GridView1.Columns(5).Visible = False
                    GridView1.Columns(6).Visible = False
                    GridView1.Columns(7).Visible = False
                    GridView1.Columns(8).Visible = False
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(11).Visible = False
                Case 7
                    GridView1.Columns(10).Visible = False
                    GridView1.Columns(5).Visible = False
                    GridView1.Columns(6).Visible = False
                    GridView1.Columns(7).Visible = False
                    GridView1.Columns(8).Visible = False
                    GridView1.Columns(9).Visible = False
                    GridView1.Columns(11).Visible = True
                Case 8
                    GridView1.Columns(11).Visible = False
                    GridView1.Columns(5).Visible = False
                    GridView1.Columns(6).Visible = False
                    GridView1.Columns(7).Visible = False
                    GridView1.Columns(8).Visible = False
                    GridView1.Columns(9).Visible = False
                    GridView1.Columns(10).Visible = True
            End Select
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle.HideGridCols", ex)
        End Try
    End Sub
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("Vehicle_Edit.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle.btnNew_Click", ex)
        End Try
    End Sub
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            'GridView1.PageIndex = e.NewPageIndex
            'SearchClick()
            If ViewState("sortExpr") Is Nothing Then
                'SearchClick()
                dv = Session("dvVehicle")
                GridView1.DataSource = dv
                GridView1.PageIndex = e.NewPageIndex
                GridView1.DataBind()

            Else

                If (GridView1.PageCount < e.NewPageIndex) Then
                    GridView1.PageIndex = 0

                    dv = Session("dvVehicle")
                    GridView1.DataSource = dv
                    GridView1.DataBind()

                Else
                    dv = Session("dvVehicle")
                    GridView1.DataSource = dv
                    GridView1.PageIndex = e.NewPageIndex
                    GridView1.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle.GridView1_PageIndexChanging", ex)
        End Try
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Try
            Response.Redirect("Vehicle_Edit.aspx?VehicleNo=" + GridView1.DataKeys(e.NewEditIndex).Values.Item(0).ToString(), False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle.GridView1_RowEditing", ex)
        End Try
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Try

            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(0);check('" + GridView1.DataKeys(e.RowIndex).Values.Item(0).ToString() + "')</script>")

            'Added By Varun Moota. 02/23/2010
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "check('" + GridView1.DataKeys(e.RowIndex).Values.Item(2).ToString() + "');", True)

            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle.GridView1_RowDeleting", ex)
        End Try
       

    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try

        
            If popup = "true" Then
                If Not e.Row.RowIndex = -1 Then
                    Dim script, script1 As String
                    Dim str As String = GridView1.DataKeys(e.Row.RowIndex).Values.Item(3).ToString() + " | " + GridView1.DataKeys(e.Row.RowIndex).Values.Item(4).ToString()
                    script = """javascript:window.opener.document.getElementById('" + Request.QueryString("txtveh").ToString().Trim() + "').value='"
                    script = script + GridView1.DataKeys(e.Row.RowIndex).Values.Item(2).ToString()
                    script = script + "';window.opener.document.getElementById('" + Request.QueryString("txtdesc").ToString().Trim() + "').value='"
                    script = script + GridView1.DataKeys(e.Row.RowIndex).Values.Item(1).ToString()
                    script = script + "';window.opener.document.getElementById('" + Request.QueryString("txtHidDept").ToString().Trim() + "').value='"
                    script = script + GridView1.DataKeys(e.Row.RowIndex).Values.Item(3).ToString()
                    script = script + "';window.opener.document.getElementById('" + Request.QueryString("txtDept").ToString().Trim() + "').value='"
                    script = script + GridView1.DataKeys(e.Row.RowIndex).Values.Item(4).ToString()
                    script = script + "';window.opener.document.getElementById('" + Request.QueryString("txtVechKey").ToString().Trim() + "').value='"
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
            cr.errorlog("Vehicle.GridView1_RowDataBound", ex)
        End Try
    End Sub
    'Added By Varun Moota to Sort DataGrid. 01/12/2010
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




        ' ''bindgrids()
        ' ''ViewState("sortExpr") = e.SortExpression
        ' ''Dim newsortDirection As String = String.Empty
        ' ''If SortDirection.Ascending Then
        ' ''    newsortDirection = "ASC"
        ' ''Else
        ' ''    newsortDirection = "DESC"
        ' ''End If
        ' ''ViewState("sortExpr") = e.SortExpression + " " + newsortDirection

        ' ''GridView1.DataSource = dv
        ' ''GridView1.DataBind()


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
    Public Function bindgrids() As DataView
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Dim strSearch As String = ""
        Dim ds = New DataSet()
        Try
            'Page.ClientScript.RegisterStartupScript(Me.GetType(),"javascript", "<script>HideControls(0)</script>")

            Dim parcollection(9) As SqlParameter

            Dim ParVehicleId = New SqlParameter("@VehicleId", SqlDbType.VarChar, 10)
            Dim ParLICNO = New SqlParameter("@LICNO", SqlDbType.VarChar, 9)
            Dim ParVEHMAKE = New SqlParameter("@VEHMAKE", SqlDbType.VarChar, 20)
            Dim ParVEHMODEL = New SqlParameter("@VEHMODEL", SqlDbType.VarChar, 20)
            Dim ParKEY_NUMBER = New SqlParameter("@KEY_NUMBER", SqlDbType.VarChar, 5)
            Dim ParCARD_ID = New SqlParameter("@CARD_ID", SqlDbType.VarChar, 7)
            Dim ParDEPT = New SqlParameter("@DEPT", SqlDbType.VarChar, 3)
            Dim ParEXTENSION = New SqlParameter("@EXTENSION", SqlDbType.VarChar, 50)
            Dim ParVEHVIN = New SqlParameter("@VEHVIN", SqlDbType.VarChar, 20)
            Dim ParVEHYEAR = New SqlParameter("@VEHYEAR", SqlDbType.VarChar, 4)

            ParVehicleId.Direction = ParameterDirection.Input
            ParLICNO.Direction = ParameterDirection.Input
            ParVEHMAKE.Direction = ParameterDirection.Input
            ParVEHMODEL.Direction = ParameterDirection.Input
            ParKEY_NUMBER.Direction = ParameterDirection.Input
            ParCARD_ID.Direction = ParameterDirection.Input
            ParDEPT.Direction = ParameterDirection.Input
            ParEXTENSION.Direction = ParameterDirection.Input
            ParVEHVIN.Direction = ParameterDirection.Input
            ParVEHYEAR.Direction = ParameterDirection.Input
            ParVehicleId.Value = ""
            ParLICNO.Value = ""
            ParVEHMAKE.Value = ""
            ParVEHMODEL.Value = ""
            ParKEY_NUMBER.Value = ""
            ParCARD_ID.Value = ""
            ParDEPT.Value = ""
            ParEXTENSION.Value = ""
            ParVEHVIN.Value = ""
            ParVEHYEAR.Value = ""
            If (txtVehicleID.Text.Trim() <> "hidden" And txtVehicleID.Text.Trim() <> "") Then
                ParVehicleId.Value = txtVehicleID.Text.Trim()
                strSearch = "by VehicleID"
                gridflag = 1
            ElseIf (txtLicenseNo.Text.Trim() <> "hidden" And txtLicenseNo.Text.Trim() <> "") Then
                ParLICNO.Value = txtLicenseNo.Text.Trim()
                strSearch = "by License No"
                gridflag = 2
            ElseIf (txtMake.Text.Trim() <> "hidden" And txtMake.Text.Trim() <> "") Then
                ' Added By Pritam 11-Nov-2014
                Dim VehMake As String = txtMake.Text.Trim()
                VehMake = Replace(Replace(Replace(VehMake, ";", ""), "--", ""), "'", "").ToString()
                ParVEHMAKE.Value = VehMake
                strSearch = "by Make"
                gridflag = 3
            ElseIf (txtModel.Text.Trim <> "hidden" And txtModel.Text.Trim() <> "") Then
                ParVEHMODEL.Value = txtModel.Text.Trim
                strSearch = "by Model"
                gridflag = 4
            ElseIf (txtKeyNo.Text.Trim() <> "hidden" And txtKeyNo.Text.Trim() <> "") Then
                ParKEY_NUMBER.Value = txtKeyNo.Text.Trim()
                strSearch = "by Key No"
                gridflag = 5
            ElseIf (txtCardNo.Text.Trim() <> "hidden" And txtCardNo.Text.Trim() <> "") Then
                ParCARD_ID.Value = txtCardNo.Text.Trim()
                strSearch = "by Card No"
                gridflag = 6
            ElseIf (txtDepartment.Text.Trim() <> "hidden" And txtDepartment.Text.Trim() <> "") Then
                ParDEPT.Value = txtDepartment.Text.Trim()
                strSearch = "by Department"
                gridflag = 1
            ElseIf (txtDescription.Text.Trim() <> "hidden" And txtDescription.Text.Trim() <> "") Then
                ' Added By Pritam 11-Nov-2014
                Dim Desc As String = txtDescription.Text.Trim()
                Desc = Replace(Replace(Replace(Desc, ";", ""), "--", ""), "'", "").ToString()
                ParEXTENSION.Value = Desc
                strSearch = "by Description"
                gridflag = 1
            ElseIf (txtVinNumber.Text.Trim() <> "hidden" And txtVinNumber.Text.Trim() <> "") Then
                ' Added By Pritam 11-Nov-2014
                Dim VehvinNumber As String = txtVinNumber.Text.Trim()
                VehvinNumber = Replace(Replace(Replace(VehvinNumber, ";", ""), "--", ""), "'", "").ToString()
                ParVEHMAKE.Value = VehvinNumber
                strSearch = "by Vin Number"
                gridflag = 7
            ElseIf (txtYear.Text.Trim() <> "hidden" And txtYear.Text.Trim() <> "") Then
                ParVEHYEAR.Value = txtYear.Text.Trim()
                strSearch = "by Year"
                gridflag = 8
            End If

            parcollection(0) = ParVehicleId
            parcollection(1) = ParLICNO
            parcollection(2) = ParVEHMAKE
            parcollection(3) = ParVEHMODEL
            parcollection(4) = ParKEY_NUMBER
            parcollection(5) = ParCARD_ID
            parcollection(6) = ParDEPT
            parcollection(7) = ParEXTENSION
            parcollection(8) = ParVEHVIN
            parcollection(9) = ParVEHYEAR


            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehicleList", parcollection)

            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))
                
                HideGridCols(gridflag)
                tdsearch.Visible = True
                lblresult.Text = "Search Result"
                lblresult.Text = lblresult.Text + " " + strSearch
            Else
                dv = ds.Tables(0).DefaultView
                'GridView1.DataSource = dv
                'GridView1.DataBind()
                tdsearch.Visible = False 'strSearch
            End If

            ' ''If Not ds Is Nothing Then
            ' ''    If ds.Tables(0).Rows.Count > 0 Then
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''        HideGridCols(gridflag)
            ' ''        tdsearch.Visible = True
            ' ''        lblresult.Text = "Search Result"
            ' ''        lblresult.Text = lblresult.Text + " " + strSearch
            ' ''    Else
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''        tdsearch.Visible = False 'strSearch
            ' ''    End If

            ' ''    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(0)</script>")
            ' ''End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehcile.btnSearch_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try


        'If Not ViewState("sortExpr") Is Nothing Then
        '    dv = New DataView(ds.Tables(0))
        '    dv.Sort = CType(ViewState("sortExpr"), String)
        'Else
        '    dv = ds.Tables(0).DefaultView
        'End If
        Return dv
    End Function
    
    Protected Overrides Sub Finalize()

        MyBase.Finalize()
        
    End Sub






End Class
