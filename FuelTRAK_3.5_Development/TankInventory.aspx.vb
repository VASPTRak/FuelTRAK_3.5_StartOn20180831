Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class InventoryEntry
    Inherits System.Web.UI.Page
    Dim visited As Boolean
    Dim GenFun As New GeneralFunctions
    Dim ds As DataSet
    Dim dv As New DataView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

     
            txtTank.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
            txtDate.Attributes.Add("OnKeyPress", "KeyPressEvent_notAllowDot(event);")
            txtTankName.Attributes.Add("OnKeyPress", "DotCommaNotAllow('txtTankName');")
            '**************** Check for session is null/not*********************
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else

                Session("visited") = False

                If Not Page.IsPostBack Then

                    'Added By Varun Moota to Load Page with Search Results.02/12/2010
                    BindGrid()
                End If

                'Commented By Varun Moota to show Search Control. 12/17/2009
                ' ''If (Not IsPostBack) Then
                ' ''    ShowPopup(btnNew)
                ' ''    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(1);parent.scrollTo(0,0);</script>")
                ' ''    Session("HideControls2") = True
                ' ''End If
                'Added By Varun Moota to Show Search Fields 12/03/2009
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>ShowSearch()</script>")


                txtDate.Attributes.Add("onkeyup", "KeyUpEvent_txtDate(event,'txtDate');")
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
            Dim exLog As New ErrorPage
            exLog.errorlog("TankInventory.Page_Load", ex)
        End Try


    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            BindGrid()
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankInventory.BindGrid()", ex)
        End Try

    End Sub
    Public Sub BindGrid()
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>HideControls(4)</script>")

            Dim parcollection(3) As SqlParameter

            Dim ParENTRY_TYPE = New SqlParameter("@ENTRY_TYPE", SqlDbType.VarChar, 1)
            Dim Pardt = New SqlParameter("@dt", SqlDbType.VarChar, 20)
            Dim ParTANK_NBR = New SqlParameter("@TANK_NBR", SqlDbType.VarChar, 3)
            Dim ParTankName = New SqlParameter("@TankName", SqlDbType.VarChar, 25)

            ParENTRY_TYPE.Direction = ParameterDirection.Input
            Pardt.Direction = ParameterDirection.Input
            ParTANK_NBR.Direction = ParameterDirection.Input
            ParTankName.Direction = ParameterDirection.Input

            ParENTRY_TYPE.Value = DDLstType.SelectedItem.Value
            If (txtDate.Text.Trim() <> "") Then
                Pardt.Value = Convert.ToDateTime(GenFun.ConvertDate(txtDate.Text.Trim()))
            Else
                Pardt.Value = ""
            End If
            ParTANK_NBR.value = txtTank.Text.Trim()
            If Not txtTankName.Text.Trim() = "" Then
                ParTankName.value = Replace(Replace(Replace(txtTankName.Text.Trim(), ";", ""), "--", ""), "'", "").ToString()
            Else
                ParTankName.value = ""
            End If

            parcollection(0) = ParENTRY_TYPE
            parcollection(1) = Pardt
            parcollection(2) = ParTANK_NBR
            parcollection(3) = ParTankName

            Dim ds = New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_TankInvList", parcollection)

            'Added By Varun Moota to Sort Datagrid. 01/12/2010
            If Not ViewState("sortExpr") Is Nothing Then
                dv = New DataView(ds.Tables(0))

                GridView1.DataSource = dv
                GridView1.DataBind()
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
            cr.errorlog("TankInventory.btnSearch_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Public Shared Sub ShowPopup(ByVal opener As System.Web.UI.WebControls.WebControl)
        Try
            '*********************Call the helper function to set the calender***********************
            OpenPopUp(opener, "InventoryPopup.aspx", "Popup", 300, 200)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory.ShowPopup", ex)
        End Try
       
    End Sub

    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String)
        Dim clientScript As String
        Try
            '***********************Building the client script- window.open****************
            clientScript = "window.open('" & PagePath & "')"
            'regiter the script to the clientside click event of the 'opener' control
            opener.Attributes.Add("onClick", clientScript)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory.OpenPopUp", ex)
        End Try

      
    End Sub

    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String, ByVal windowName As String, ByVal width As Integer, ByVal height As Integer)
        Dim clientScript As String
        Dim windowAttribs As String
        Try

   
            'Building Client side window attributes with width and height.
            'Also the the window will be positioned to the middle of the screen
            windowAttribs = "width=" & width & "px," & _
                            "height=" & height & "px," & _
                            "titlebar=no," & _
                            "left='+((screen.width -" & width & ") / 2)+'," & _
                            "top='+ (screen.height - " & height & ") / 2+'"
            'Building the client script- window.open, with additional parameters
            clientScript = "window.open('" & PagePath & "','" & windowName & "','" & windowAttribs & "');return false;"
            'regiter the script to the clientside click event of the 'opener' control
            opener.Attributes.Add("onClick", clientScript)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory.OpenPopUp", ex)
        End Try

    End Sub

    Public Sub deleteRecord(ByVal RECORDID As Integer)
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParRECORDID = New SqlParameter("@RECORD", SqlDbType.Int)
            ParRECORDID.Direction = ParameterDirection.Input
            ParRECORDID.Value = RECORDID
            parcollection(0) = ParRECORDID
            Dim iCnt As Integer
            iCnt = dal.ExecuteStoredProcedureGetInteger("usp_tt_TankInvDelete", parcollection)

            If iCnt > 0 Then
                BindGrid()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Tank Inventory deleted sucessfully.')</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankInventory.deleteRecord", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Protected Sub DDLstType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLstType.SelectedIndexChanged
        Try
            txtType.Text = DDLstType.SelectedItem.Value
            txtType.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory.DDLstType_SelectedIndexChanged", ex)
        End Try
        
    End Sub

    Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        Dim Tanktype As String = String.Empty
        ' ''If GridView1.Rows(e.NewEditIndex).Cells(3).Text.Equals("Delivery") Then
        ' ''    Tanktype = "R"
        ' ''ElseIf GridView1.Rows(e.NewEditIndex).Cells(3).Text.Equals("Setting") Then
        ' ''    Tanktype = "S"
        ' ''ElseIf GridView1.Rows(e.NewEditIndex).Cells(3).Text.Equals("Dipping") Then
        ' ''    Tanktype = "D"
        ' ''ElseIf GridView1.Rows(e.NewEditIndex).Cells(3).Text.Equals("Level") Then
        ' ''    Tanktype = "L"
        ' ''End If
        'Added By Varun Moota, since the Above code Returns "".08/13/2010
        Try
            Dim EntryType As Label = GridView1.Rows(e.NewEditIndex).Cells(3).FindControl("lbltype")


            Response.Redirect("~/TankInventory_New_Edit.aspx?TankType=" + EntryType.Text.ToString().Trim() + "&Record=" + GridView1.DataKeys(e.NewEditIndex).Value.ToString(), False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("TankInventory_New_Edit.GridView1_RowEditing", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
        
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>check('" + GridView1.DataKeys(e.RowIndex).Value.ToString() + "')</script>")
        'Added By Varun Moota. 02/23/2010
        Try

      
            If (Not ClientScript.IsStartupScriptRegistered("alert")) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "check('" + GridView1.DataKeys(e.RowIndex).Values.Item(0).ToString() + "');", True)
                txtEntryID.Value = GridView1.DataKeys(e.RowIndex).Values.Item(0).ToString()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory.GridView1_RowDeleting", ex)
        End Try
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        'GridView1.PageIndex = e.NewPageIndex
        'SearchClick()
        Try
            GridView1.PageIndex = e.NewPageIndex
            BindGrid()

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory.GridView1_PageIndexChanging", ex)
        End Try
    End Sub

    Protected Sub GridView1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.PreRender
        
        Try
            'If (Page.IsPostBack) Then
            '    'SearchRecord()
            '    SearchClick()
            'End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory.GridView1_PreRender", ex)
        End Try
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If (Not e.Row.RowIndex = -1) Then
                If e.Row.Cells(3).Text.Equals("R") Then
                    e.Row.Cells(3).Text = "Delivery"
                ElseIf e.Row.Cells(3).Text.Equals("S") Then
                    e.Row.Cells(3).Text = "Setting"
                ElseIf e.Row.Cells(3).Text.Equals("D") Then
                    e.Row.Cells(3).Text = "Dipping"
                ElseIf e.Row.Cells(3).Text.Equals("L") Then
                    e.Row.Cells(3).Text = "Level"
                End If
            End If

            'Added By Varun Moota. 02/25/2010



            'If (Not e.Row.RowIndex = -1) Then
            '    Dim lbl As Label = CType(e.Row.FindControl("lblType"), Label)
            '    If lbl.Text = "R" Then
            '        e.Row.Cells(3).Text = "Delivery"
            '    ElseIf lbl.Text.Equals("S") Then
            '        e.Row.Cells(3).Text = "Setting"
            '    ElseIf lbl.Text.Equals("D") Then
            '        e.Row.Cells(3).Text = "Dipping"
            '    ElseIf lbl.Text.Equals("L") Then
            '        e.Row.Cells(3).Text = "Level"
            '    End If
            'End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankInventory.GridView1_RowDataBound", ex)
        End Try

    End Sub

    
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
            cr.errorlog("TankInventory.GridTM_Sorting", ex)
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
