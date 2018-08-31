Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class TankCharts
    Inherits System.Web.UI.Page
    Dim GenFun As New GeneralFunctions
    Dim ds As DataSet
    Dim dv As New DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'txtChartID.Attributes.Add("OnKeyPress", "KeyPressYear(event);")
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            Else
                If Not Page.IsPostBack Then
                    tblSearch.Visible = True
                    BindGrid()
                End If

                If (Hidtxt.Value = "true") Then
                    If txtEntryID.Value <> "" Then
                        deleteRecord(Convert.ToInt32(txtEntryID.Value))
                        BindGrid()
                        Hidtxt.Value = ""
                        txtEntryID.Value = ""
                    End If
                ElseIf (Hidtxt.Value = "false") Then
                    Hidtxt.Value = ""
                    BindGrid()
                End If
            End If
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Page_Load_TankCharts", ex)
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            tblSearch.Visible = True
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts.btnSearch_Click", ex)
        End Try
    End Sub
    Public Sub SearchResults()
        'Dim gridflag As Integer = 0
        'Dim dal = New GeneralizedDAL()
        Try
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(4)</script>")

            '    Dim parcollection(3) As SqlParameter

            '    Dim ParENTRY_TYPE = New SqlParameter("@ENTRY_TYPE", SqlDbType.VarChar, 1)
            '    Dim Pardt = New SqlParameter("@dt", SqlDbType.VarChar, 20)
            '    Dim ParTANK_NBR = New SqlParameter("@TANK_NBR", SqlDbType.VarChar, 3)
            '    Dim ParTankName = New SqlParameter("@TankName", SqlDbType.VarChar, 25)

            '    ParENTRY_TYPE.Direction = ParameterDirection.Input
            '    Pardt.Direction = ParameterDirection.Input
            '    ParTANK_NBR.Direction = ParameterDirection.Input
            '    ParTankName.Direction = ParameterDirection.Input

            '    ParENTRY_TYPE.Value = DDLstType.SelectedItem.Value
            '    If (txtDate.Text.Trim() <> "") Then
            '        Pardt.Value = Convert.ToDateTime(GenFun.ConvertDate(txtDate.Text.Trim()))
            '    Else
            '        Pardt.Value = ""
            '    End If
            '    ParTANK_NBR.value = txtTank.Text.Trim()
            '    ParTankName.value = txtTankName.Text.Trim()

            '    parcollection(0) = ParENTRY_TYPE
            '    parcollection(1) = Pardt
            '    parcollection(2) = ParTANK_NBR
            '    parcollection(3) = ParTankName

            '    Dim ds = New DataSet()
            '    ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TankInvList", parcollection)
            '    If Not ds Is Nothing Then
            '        If ds.Tables(0).Rows.Count > 0 Then
            '            GridTankCharts.DataSource = ds.Tables(0)
            '            GridTankCharts.DataBind()
            '        Else
            '            GridView1.DataSource = ds.Tables(0)
            '            GridView1.DataBind()
            '        End If
            '    End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankCharts_btnSearch_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
    Public Sub BindGrid()
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(1) As SqlParameter

            Dim ParNumber = New SqlParameter("@ChartNumber", SqlDbType.NVarChar, 3)
            Dim ParName = New SqlParameter("@ChartName", SqlDbType.NVarChar, 25)

            ParNumber.Direction = ParameterDirection.Input
            ParName.Direction = ParameterDirection.Input

            If Not txtChartID.Text = "" Then ParNumber.value = txtChartID.Text Else ParNumber.value = ""
            'Added By Pritam to avoid SQL injection Date : 12-Nov-2014
            If Not txtChartName.Text = "" Then ParName.value = Replace(Replace(Replace(txtChartName.Text, ";", ""), "--", ""), "'", "").ToString() Else ParName.value = ""

            parcollection(0) = ParNumber
            parcollection(1) = ParName

            ds = dal.ExecuteStoredProcedureGetDataSet("use_TT_GetTankCharts", parcollection)

            'Added By Varun Moota to Sort Datagrid. 01/05/2011
            If Not ViewState("sortExpr") Is Nothing Then

                dv = New DataView(ds.Tables(0))
                dv.Sort = CType(ViewState("sortExpr"), String)

                GridTankCharts.DataSource = dv
                GridTankCharts.DataBind()
            Else
                dv = ds.Tables(0).DefaultView
                GridTankCharts.DataSource = dv
                GridTankCharts.DataBind()
            End If

            'If Not ds Is Nothing Then
            '    GridTankCharts.DataSource = ds.Tables(0)
            '    GridTankCharts.DataBind()
            'End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("BindGrid_TankCharts", ex)
        End Try
    End Sub
    Protected Sub btnSearchSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchSub.Click
        Try
            tblSearch.Visible = True
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("BindControl_TankCharts", ex)
        End Try
    End Sub
    Protected Sub GridTankCharts_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridTankCharts.PageIndexChanging
        Try
            GridTankCharts.PageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GridTankCharts_PageIndexChanging_TankMonitor", ex)
        End Try
    End Sub

    Protected Sub GridTankCharts_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridTankCharts.RowEditing
        Try
            'Response.Redirect("TankCharts_New_Edit.aspx?RowID=" + (GridTankCharts.DataKeys(e.NewEditIndex).Value).ToString(), False)
            Dim txtGVChartNum As Label = CType(GridTankCharts.Rows(e.NewEditIndex).FindControl("lblNumber"), Label)
            'Response.Redirect("TankChartsPopUp.aspx?RowID=" + txtGVChartNum.Text.ToString(), False)
            Response.Redirect("TankCharts_New_Edit.aspx?RowID=" + txtGVChartNum.Text.ToString(), False)



        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("GridTankCharts_RowEditing", ex)
        End Try
    End Sub

    Protected Sub GridTankCharts_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridTankCharts.Sorting
        Dim sortExpression As String = e.SortExpression
        ViewState("sortExpr") = e.SortExpression

        Try

            If GridViewSortDirection = SortDirection.Ascending Then
                BindGrid()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "ASC"
                GridTankCharts.DataSource = dv
                GridTankCharts.DataBind()
                GridViewSortDirection = SortDirection.Descending
            Else
                BindGrid()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "DESC"
                GridTankCharts.DataSource = dv
                GridTankCharts.DataBind()
                GridViewSortDirection = SortDirection.Ascending

            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankCharts.GridTankCharts_Sorting", ex)
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

    Public Sub deleteRecord(ByVal RECORDID As Integer)
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParRECORDID = New SqlParameter("@RECORD", SqlDbType.Int)
            ParRECORDID.Direction = ParameterDirection.Input
            ParRECORDID.Value = RECORDID
            parcollection(0) = ParRECORDID
            Dim iCnt As Integer
            iCnt = dal.ExecuteStoredProcedureGetInteger("usp_tt_TankChartsDelete", parcollection)

            If iCnt > 0 Then
                BindGrid()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Tank charts deleted sucessfully.')</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankCharts.deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Protected Sub GridTankCharts_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridTankCharts.RowDeleting
        Try
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "check('" + GridTankCharts.DataKeys(e.RowIndex).Values.Item(0).ToString() + "');", True)
                txtEntryID.Value = GridTankCharts.DataKeys(e.RowIndex).Values.Item(0).ToString()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankCharts.GridTankCharts_RowDeleting", ex)
        End Try
    End Sub
End Class
