Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Partial Class TankMonitor
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim dv As New DataView
    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("TankMonitor_New_Edit.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor.btnNew_Click", ex)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            tblSearchsub.Visible = True
            btnNew.Visible = False
            btnSearch.Visible = True
            txtTMID.Focus()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor.btnSearch_Click", ex)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtTMID.Attributes.Add("OnKeyPress", "KeyPressYear(event);")
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            Else
                If Not Page.IsPostBack Then

                    'Added By Varun Moota to Load Page with Search Results.02/12/2010

                    BindGrid()
                    tblSearch.Visible = True
                End If
            End If
            'Added By Varun to Show Controls 12/04/2009
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowControls();</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Page_Load_TankMonitor", ex)
        End Try

    End Sub
    Public Sub BindGrid()
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(1) As SqlParameter

            Dim ParNumber = New SqlParameter("@Number", SqlDbType.NVarChar, 3)
            Dim ParName = New SqlParameter("@Name", SqlDbType.NVarChar, 25)

            ParNumber.Direction = ParameterDirection.Input
            ParName.Direction = ParameterDirection.Input

            If Not txtTMID.Text = "" Then ParNumber.value = txtTMID.Text.PadLeft(3, "0") Else ParNumber.value = ""
            'Added by Pritam to avoid SQL injection with replacing ; and -- with "" 
            If Not txtTMName.Text = "" Then ParName.value = Replace(Replace(Replace(txtTMName.Text.Trim(), ";", ""), "--", ""), "'", "").ToString() Else ParName.value = ""

            parcollection(0) = ParNumber
            parcollection(1) = ParName

            ds = dal.ExecuteStoredProcedureGetDataSet("use_tt_getTMDetails", parcollection)


            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))
            Else
                dv = ds.Tables(0).DefaultView
                GridTM.DataSource = dv
                GridTM.DataBind()
            End If


            ' ''If Not ds Is Nothing Then
            ' ''    GridTM.DataSource = ds.Tables(0)
            ' ''    GridTM.DataBind()
            ' ''End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("BindGrid_TankMonitor", ex)
        End Try
    End Sub

    Protected Sub btnSearchsub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchsub.Click
        Try
            tblSearch.Visible = True
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("BindControl_TankMonitor", ex)
        End Try
        
    End Sub

    Protected Sub GridTM_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridTM.PageIndexChanging
        Try
            GridTM.PageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor.GridTM_PageIndexChanging", ex)
        End Try
    End Sub

    Protected Sub GridTM_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridTM.RowEditing

        Try
            Response.Redirect("TankMonitor_New_Edit.aspx?RowID=" + GridTM.DataKeys(e.NewEditIndex).Value, False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankMonitor.GridTM_RowEditing", ex)
        End Try
    End Sub

    Protected Sub GridTM_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridTM.Sorting
        Dim sortExpression As String = e.SortExpression

        ViewState("sortExpr") = e.SortExpression


        Try

            If GridViewSortDirection = SortDirection.Ascending Then



                BindGrid()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "ASC"
                GridTM.DataSource = dv
                GridTM.DataBind()
                GridViewSortDirection = SortDirection.Descending


            Else


                BindGrid()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "DESC"
                GridTM.DataSource = dv
                GridTM.DataBind()
                GridViewSortDirection = SortDirection.Ascending

            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Tank.GridTank_Sorting", ex)
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
