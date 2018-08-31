Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class MeterReadings
    Inherits System.Web.UI.Page
    Dim GenFun As New GeneralFunctions
    Dim DAL As GeneralizedDAL
    Dim Uinfo As UserInfo
    Dim ds As DataSet
    Dim dv As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                txtSentry.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtSentry');")
                txtDate.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtDate');")
                txtDate.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
                txtDate.Attributes.Add("onkeyup", "KeyUpEvent_txtDate(event,'txtDate');") 'to call javascript file in that chk for valid date.
                Session("visited") = False
                Session("currentrecord") = "0"
                ''Commented BY Varun To show Controls
                ' ''If (Not IsPostBack) Then
                ' ''    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(2);parent.scrollTo(0,0);</script>")
                ' ''    Session("HideControls2") = True
                ' ''End If

                'Added By Varun to Show Search Fields 12/03/2009
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")
                If Not Page.IsPostBack Then

                    'Added By Varun Moota to Load Page with Search Results.02/12/2010

                    BindGrid()
                End If

                If (Hidtxt.Value = "true") Then
                    If txtVehId.Value <> "" Then
                        DeleteRecord(Convert.ToInt32(txtVehId.Value))
                        Hidtxt.Value = ""
                        txtVehId.Value = ""
                        BindGrid()
                    End If
                ElseIf (Hidtxt.Value = "false") Then
                    Hidtxt.Value = ""
                    BindGrid()
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReadings.Page_Load", ex)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReadings.btnSearch_Click", ex)
        End Try

    End Sub


    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Response.Redirect("MeterReadings_New_Edit.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReadings.btnNew_Click", ex)
        End Try

    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Try
            Response.Redirect("~/MeterReadings_New_Edit.aspx?Record=" + GridView1.DataKeys(e.NewEditIndex).Value.ToString(), False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReadings.GridView1_RowEditing", ex)
        End Try

    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>check('" + GridView1.DataKeys(e.RowIndex).Value.ToString() + "')</script>")

        'Added By Varun Moota. 02/23/2010
        Try
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "check('" + GridView1.DataKeys(e.RowIndex).Values.Item(0).ToString() + "');", True)

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReadings.GridView1_RowDeleting", ex)
        End Try
        
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Try
            GridView1.PageIndex = e.NewPageIndex
            BindGrid()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("MeterReadings.GridView1_PageIndexChanging", ex)
        End Try
    End Sub

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender
        'If (Page.IsPostBack) Then
        '    SearchRecords()
        'End If
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
            blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_MeterDelete", parcollection)
            If blnflag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Meter deleted sucessfully.')</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("MeterSearch_deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
    Public Sub BindGrid()
        'Code modify Jatin Kshirsagar as on 01 Sept 2008
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Dim ds = New DataSet()
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(1)</script>")
            Dim parcollection(1) As SqlParameter

            Dim ParSENTRY = New SqlParameter("@SENTRY", SqlDbType.VarChar, 10)
            Dim Pardt = New SqlParameter("@dt", SqlDbType.DateTime)

            ParSENTRY.Direction = ParameterDirection.Input
            Pardt.Direction = ParameterDirection.Input
            If (txtSentry.Text.Trim() <> "") Then
                ParSENTRY.Value = txtSentry.Text.Trim()
            Else
                ParSENTRY.Value = System.DBNull.Value
            End If
            If (txtDate.Text.Trim() <> "") Then
                Pardt.Value = Convert.ToDateTime(GenFun.ConvertDate(txtDate.Text.Trim()))
            Else
                Pardt.Value = System.DBNull.Value
            End If

            parcollection(0) = ParSENTRY
            parcollection(1) = Pardt

            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_MeterSearchList", parcollection)
            

            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))
            Else
                dv = ds.Tables(0).DefaultView
                GridView1.DataSource = dv
                GridView1.DataBind()
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
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("MeterReading_btnSearch_Click", ex)

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

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        Dim sortExpression As String = e.SortExpression

        ViewState("sortExpr") = e.SortExpression


        Try

            If GridViewSortDirection = SortDirection.Ascending Then



                BindGrid()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "ASC"
                GridView1.DataSource = dv
                GridView1.DataBind()
                GridViewSortDirection = SortDirection.Descending


            Else


                BindGrid()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "DESC"
                GridView1.DataSource = dv
                GridView1.DataBind()
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
End Class
