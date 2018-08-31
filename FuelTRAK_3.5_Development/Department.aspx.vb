Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class Department
    Inherits System.Web.UI.Page
    Dim dv As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '**************** check for session is null*********************
        Try

      
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                Session("visited") = False
                Session("currentrecord") = "0"
                If (Not IsPostBack) Then
                    ' ''Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(2);parent.scrollTo(0,0)</script>")
                    ' ''Session("HideControls2") = True


                End If
                txtDepartmentName.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtDepartmentName');")
                txtDepartmentNumber.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtDepartmentNumber');")
                'Added By Varun to Show Search Fields 12/03/2009
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowControls()</script>")

                'Added By Varun Moota to Show Search Results whne it Loads. 02/12/2010
                FillDeptGrid()

                If (Hidtxt.Value = "true") Then
                    If txtVehId.Value <> "" Then
                        deleteRecord(Convert.ToInt32(txtVehId.Value))
                        FillDeptGrid()
                        Hidtxt.Value = ""
                        txtVehId.Value = ""
                    End If
                ElseIf (Hidtxt.Value = "false") Then
                    Hidtxt.Value = ""
                    If (IsPostBack) Then
                        FillDeptGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Deparment.Page_Load", ex)

        End Try
    End Sub

    Public Sub FillDeptGrid()
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(1) As SqlParameter
            Dim DeptName = New SqlParameter("@DeptName", SqlDbType.VarChar, 25)
            Dim DeptNumber = New SqlParameter("@NUMBER", SqlDbType.VarChar, 3)
            DeptName.Direction = ParameterDirection.Input
            If (txtDepartmentName.Text.Trim() <> "hidden" And txtDepartmentName.Text.Trim() <> "") Then
                DeptName.Value = Replace(Replace(Replace(txtDepartmentName.Text.Trim(), ";", ""), "--", ""), "'", "").ToString()
                DeptNumber.Value = ""
            ElseIf (txtDepartmentNumber.Text.Trim() <> "hidden" And txtDepartmentNumber.Text.Trim() <> "") Then
                DeptName.Value = ""
                DeptNumber.Value = txtDepartmentNumber.Text.Trim()
            Else
                DeptName.Value = ""
                DeptNumber.Value = ""
            End If

            parcollection(0) = DeptName
            parcollection(1) = DeptNumber
            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_DeptList", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    GridView1.DataSource = ds.Tables(0)
                    GridView1.DataBind()
                Else
                    GridView1.DataSource = ds.Tables(0)
                    GridView1.DataBind()
                End If
            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(4)</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("DepartmentSearch_FillDeptGrid", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        Finally
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            '******************This is use for search the perticular record according to enter data***********************
            tdsearch.Visible = True
            FillDeptGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Department.btnSearch_Click", ex)
        End Try
      
    End Sub

    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Session("visited") = False
            Response.Redirect("Department_New_Edit.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Department.btnNew_Click", ex)
        End Try
       
    End Sub

    Public Sub deleteRecord(ByVal DeptID As Integer)
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParDeptID = New SqlParameter("@DeptID", SqlDbType.Int)
            ParDeptID.Direction = ParameterDirection.Input
            ParDeptID.Value = DeptID
            parcollection(0) = ParDeptID
            Dim blnflag As Boolean
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_Dept_Delete", parcollection)
            If blnflag = False Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Department deleted sucessfully.');HideControls(4);</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("DepartmentSearch_deleteRecord", ex)
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
            FillDeptGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Department.GridView1_PageIndexChanging", ex)
        End Try
       
    End Sub

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Response.Redirect("Department_New_Edit.aspx?DeptNo=" + GridView1.DataKeys(e.NewEditIndex).Value.ToString(), False)
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Try

       
            ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>check('" + GridView1.DataKeys(e.RowIndex).Value.ToString() + "')</script>")

            'Added By Varun Moota. 02/23/2010
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "check('" + GridView1.DataKeys(e.RowIndex).Values.Item(0).ToString() + "');", True)

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Department.GridView1_RowDeleting", ex)
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
    Public Function bindgrids() As DataView
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(1) As SqlParameter
            Dim DeptName = New SqlParameter("@DeptName", SqlDbType.VarChar, 25)
            Dim DeptNumber = New SqlParameter("@NUMBER", SqlDbType.VarChar, 3)
            DeptName.Direction = ParameterDirection.Input
            If (txtDepartmentName.Text.Trim() <> "hidden" And txtDepartmentName.Text.Trim() <> "") Then
                DeptName.Value = Replace(Replace(Replace(txtDepartmentName.Text.Trim(), ";", ""), "--", ""), "'", "").ToString()
                DeptNumber.Value = ""
            ElseIf (txtDepartmentNumber.Text.Trim() <> "hidden" And txtDepartmentNumber.Text.Trim() <> "") Then
                DeptName.Value = ""
                DeptNumber.Value = txtDepartmentNumber.Text.Trim()
            Else
                DeptName.Value = ""
                DeptNumber.Value = ""
            End If

            parcollection(0) = DeptName
            parcollection(1) = DeptNumber
            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_DeptList", parcollection)

            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))
                dv.Sort = CType(ViewState("sortExpr"), String)

            Else
                dv = ds.Tables(0).DefaultView
               
            End If
            ' ''If Not ds Is Nothing Then
            ' ''    If ds.Tables(0).Rows.Count > 0 Then
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''    Else
            ' ''        GridView1.DataSource = ds.Tables(0)
            ' ''        GridView1.DataBind()
            ' ''    End If
            ' ''End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(4)</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("DepartmentSearch_FillDeptGrid", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        Finally
        End Try
        Return dv
    End Function
    'Added By Varun Moota to Sort DataGrid. 01/25/2010
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
            cr.errorlog("Department.GridView1_Sorting", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try
    End Sub
End Class
