Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Partial Class Sentry
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim dv As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtSID.Attributes.Add("OnKeyPress", "KeyPressYear(event);")
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            Else
                If Not Page.IsPostBack Then

                    'Added By Varun Moota to Load Page with Search Results.02/12/2010
                    tblSearch.Visible = True
                    BindGrid()
                End If
            End If
            'Added By Varun to Show Controls 12/04/2009
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowControls();</script>")

           

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry.Page_Load", ex)
        End Try

    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("Sentry_New_Edit.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry.btnNew_Click", ex)
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            tblSearchsub.Visible = True
            btnNew.Visible = False
            btnSearch.Visible = False
            txtSID.Focus()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry.btnSearch_Click", ex)
        End Try
        
    End Sub

    Protected Sub btnSearchsub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchsub.Click
        Try
            tblSearch.Visible = True
            SearchClick()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry_btnSearchsub_Click", ex)
        End Try
    End Sub
    Public Sub SearchClick()
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(1) As SqlParameter

            Dim ParNumber = New SqlParameter("@Number", SqlDbType.NVarChar, 3)
            Dim ParName = New SqlParameter("@Name", SqlDbType.NVarChar, 25)

            ParNumber.Direction = ParameterDirection.Input
            ParName.Direction = ParameterDirection.Input

            If Not txtSID.Text = "" Then ParNumber.value = txtSID.Text.PadLeft(3, "0") Else ParNumber.value = ""
            If Not txtSName.Text = "" Then ParName.value = Replace(Replace(Replace(txtSName.Text.Trim, ";", ""), "--", ""), "'", "").ToString() Else ParName.value = ""


            parcollection(0) = ParNumber
            parcollection(1) = ParName

            ds = dal.ExecuteStoredProcedureGetDataSet("use_tt_getSentryDetails", parcollection)


            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))

                GridTM.DataSource = dv
                GridTM.DataBind()
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
            cr.errorlog("Sentry.SearchClick()", ex)
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

            If Not txtSID.Text = "" Then ParNumber.value = txtSID.Text.PadLeft(3, "0") Else ParNumber.value = ""
            If Not txtSName.Text = "" Then ParName.value = Replace(Replace(Replace(txtSName.Text.Trim, ";", ""), "--", ""), "'", "").ToString() Else ParName.value = ""

            parcollection(0) = ParNumber
            parcollection(1) = ParName

            ds = dal.ExecuteStoredProcedureGetDataSet("use_tt_getSentryDetails", parcollection)


            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then

                dv = New DataView(ds.Tables(0))
                dv.Sort = CType(ViewState("sortExpr"), String)



                GridTM.DataSource = dv
                GridTM.DataBind()
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
            cr.errorlog("Sentry.BindGrid", ex)
        End Try
    End Sub

    Protected Sub GridTM_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridTM.PageIndexChanging
        Try
            GridTM.PageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry.GridTM_PageIndexChanging", ex)
        End Try

    End Sub

    Protected Sub GridTM_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridTM.RowEditing
        Try
            Response.Redirect("Sentry_New_Edit.aspx?RowID=" + GridTM.DataKeys(e.NewEditIndex).Value.ToString(), False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Sentry.GridTM_RowEditing", ex)
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
            cr.errorlog("Sentry.GridTM_Sorting", ex)
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
