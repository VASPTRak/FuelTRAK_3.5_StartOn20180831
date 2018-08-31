Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Partial Class Tank
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim dv As New DataView
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            tblGrid.Visible = True
            btnNew.Visible = False
            btnSearch.Visible = True
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("Tank.btnSearch_Click", ex)
        End Try
       
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("Tank_New_Edit.aspx", False)
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("Tank.btnNew_Click", ex)
        End Try

    End Sub
    Public Sub BindGrid()
        Try
            Dim dal As New GeneralizedDAL()
            ds = dal.GetDataSet("use_tt_GetTankDetails")

            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))
            Else
                dv = ds.Tables(0).DefaultView
                GridTank.DataSource = dv
                GridTank.DataBind()
            End If

            ' ''If Not ds Is Nothing Then
            ' ''    GridTank.DataSource = ds.Tables(0)
            ' ''    GridTank.DataBind()
            ' ''End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank.BindGrid()", ex)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then 'check for session is null/not
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            ElseIf Not Page.IsPostBack Then

                'Added By Varun Moota to Load Page with Search Results.02/12/2010
                BindGrid()
                tblGrid.Visible = True
                btnNew.Visible = False
                btnSearch.Visible = True
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank.Page_Load", ex)
        End Try
       
    End Sub

    Protected Sub GridTank_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridTank.PageIndexChanging
        Try
            GridTank.PageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank.GridTank_PageIndexChanging", ex)
        End Try

    End Sub

    Protected Sub GridTank_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridTank.RowEditing
        Try
            Response.Redirect("Tank_New_Edit.aspx?RowID=" + GridTank.DataKeys(e.NewEditIndex).Value.ToString(), False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Tank.GridTank_RowEditing", ex)
        End Try
    End Sub

    Protected Sub GridTank_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridTank.Sorting
        Dim sortExpression As String = e.SortExpression

        ViewState("sortExpr") = e.SortExpression


        Try

            If GridViewSortDirection = SortDirection.Ascending Then



                BindGrid()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "ASC"
                GridTank.DataSource = dv
                GridTank.DataBind()
                GridViewSortDirection = SortDirection.Descending


            Else


                BindGrid()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "DESC"
                GridTank.DataSource = dv
                GridTank.DataBind()
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
