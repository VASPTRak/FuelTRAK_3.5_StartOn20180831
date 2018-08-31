Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class TransactionCostUpdate
    Inherits System.Web.UI.Page

    Dim ds As DataSet
    Dim i As Integer
    Dim txtctl As Label
    Dim txtctl1 As TextBox
    Dim Chkctl As CheckBox
    Dim lblctl As Label

    Dim txtPrl As Label  ' By Soham Gangavane Sep 14, 2017


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            addattribute()
            If Session("User_name") Is Nothing Then 'check for session is null/not
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            ElseIf Not Page.IsPostBack Then
                Session("productNumber") = 0
                'PopUpProduct.Visible = False
                PopUpHistory.Visible = False
                BindAllControls()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product.Page_Load", ex)
        End Try

    End Sub

    Public Sub BindAllControls()
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim Parval = New SqlParameter("@val", SqlDbType.NVarChar, 1)
            Parval.Direction = ParameterDirection.Input
            Parval.Value = "0"
            parcollection(0) = Parval
            ds = dal.ExecuteStoredProcedureGetDataSet("use_tt_Productlist", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To 14


                        txtctl = CType(Page.FindControl("txtN" & (i + 1)), Label)
                        txtctl.Text = ds.Tables(0).Rows(i)(0).ToString()

                        txtPrl = CType(Page.FindControl("txtPr" & (i + 1)), Label) ' By Soham Gangavane Sep 14, 2017
                        If ds.Tables(0).Rows(i)(4).ToString() = "" Then
                            txtPrl.Text = "0.0"
                        Else
                            txtPrl.Text = ds.Tables(0).Rows(i)(4).ToString()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product.BindControl", ex)
        End Try
    End Sub

    Public Sub addattribute()
        Try
            For i = 0 To 14
                txtctl = CType(Page.FindControl("txtN" & (i + 1)), Label)
                txtctl.Attributes.Add("OnKeyPress", "KeyPress(event);")
                txtctl1 = CType(Page.FindControl("txtE" & (i + 1)), TextBox)
                txtctl1.Attributes.Add("OnKeyPress", "KeyPress(event);")
            Next
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product.addattribute", ex)
        End Try

    End Sub

    Protected Sub btnPrice1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice1.Click
        Session("productNumber") = 1
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice2.Click
        Session("productNumber") = 2
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice3.Click
        Session("productNumber") = 3
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice4.Click
        Session("productNumber") = 4
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice5.Click
        Session("productNumber") = 5
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice6.Click
        Session("productNumber") = 6
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice7.Click
        Session("productNumber") = 7
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice8.Click
        Session("productNumber") = 8
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice9.Click
        Session("productNumber") = 9
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice10.Click
        Session("productNumber") = 10
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice11.Click
        Session("productNumber") = 11
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice12.Click
        Session("productNumber") = 12
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice13.Click
        Session("productNumber") = 13
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice14.Click
        Session("productNumber") = 14
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnPrice15_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice15.Click
        Session("productNumber") = 15
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnProdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProdClose.Click
        Try
            txtStartDate.Text = ""
            txtEndDate.Text = ""
            Session("productNumber") = 0
            Response.Redirect("~/Product.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product_Edit.btnProdClose_Click", ex)
        End Try
    End Sub

    Protected Sub btnProdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProdOK.Click
        Try

            Dim result As Double = 0.0
            Dim checkValue As Boolean = True

            Dim strProd As String = ""
            For i = 1 To 15
                Chkctl = CType(Page.FindControl("chk" & (i)), CheckBox)
                If Chkctl.Checked Then
                    Dim Prod As String = ""
                    If i.ToString.Length = 1 Then
                        Prod = "0" + i.ToString
                    Else
                        Prod = i.ToString
                    End If
                    strProd = strProd + Prod + ","
                End If
            Next

            If strProd = "" Then
                checkValue = False
            Else
                checkValue = True
            End If

            If checkValue = True Then
                Dim newDate As DateTime
                Try
                    newDate = DateTime.Parse(txtStartDate.Text)
                    newDate = DateTime.Parse(txtEndDate.Text)
                Catch ex As Exception
                    checkValue = False
                End Try
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No product seleted to repost price. Please select product and try again!!');</script>")
                Exit Sub
            End If

            If checkValue = True Then
                Dim strProdList() As String = strProd.TrimEnd(",").Split(",")

                Dim dal = New GeneralizedDAL()
                Dim parcollection(5) As SqlParameter

                Dim ParUserName = New SqlParameter("@UserName", SqlDbType.NVarChar, 250)
                Dim ParProductNumber = New SqlParameter("@ProductNumber", SqlDbType.NVarChar, 3)
                Dim ParResetPrice = New SqlParameter("@ResetPrice", SqlDbType.NVarChar, 50)
                Dim ParFromDate = New SqlParameter("@FromDate", SqlDbType.NVarChar, 25)
                Dim ParToDate = New SqlParameter("@ToDate", SqlDbType.NVarChar, 25)
                Dim ParDateAdded = New SqlParameter("@DateAdded", SqlDbType.NVarChar, 25)
                Dim Parflag = New SqlParameter("@flag", SqlDbType.Int)

                ParUserName.Direction = ParameterDirection.Input
                ParProductNumber.Direction = ParameterDirection.Input
                ParResetPrice.Direction = ParameterDirection.Input
                ParFromDate.Direction = ParameterDirection.Input
                ParToDate.Direction = ParameterDirection.Input
                ParDateAdded.Direction = ParameterDirection.Input
                Parflag.Direction = ParameterDirection.Input

                For i = 0 To strProdList.Length - 1
                    ParUserName.Value = Session("User_name").ToString
                    ParProductNumber.Value = strProdList(i).ToString.TrimStart.TrimEnd
                    ParFromDate.Value = txtStartDate.Text
                    ParToDate.Value = txtEndDate.Text
                    ParDateAdded.Value = DateTime.Today.Month & "/" & DateTime.Today.Day & "/" & DateTime.Today.Year
                    Parflag.Value = 1

                    parcollection(0) = ParUserName
                    parcollection(1) = ParProductNumber
                    parcollection(2) = ParFromDate
                    parcollection(3) = ParToDate
                    parcollection(4) = ParDateAdded
                    parcollection(5) = Parflag

                    Dim ds As DataSet
                    ds = dal.ExecuteStoredProcedureGetDataSet("SP_Update_TXTN_Price_Cost_History", parcollection)
                Next
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('New price updated in transaction records for selected products!!');</script>")
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Date format are not valid. Please enter valid date and try again !!');</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product_Edit_Popup.btnProdOK_Click", ex)
        End Try
    End Sub

    'Protected Sub btnHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory.Click
    '    Try
    '        PopUpHistory.Visible = True
    '        bindgrids()

    '    Catch ex As Exception
    '        Dim cr As New ErrorPage
    '        cr.errorlog("Product_Edit_Popup.btnHistory_Click", ex)
    '    End Try
    'End Sub

    Protected Sub btnCancelHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelHistory.Click
        Try
            PopUpHistory.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product_Edit.btnCancelHistory", ex)
        End Try
    End Sub

    Protected Sub grdHistory_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdHistory.Sorting

        Dim sortExpression As String = e.SortExpression

        ViewState("sortExpr") = e.SortExpression
        Dim dv As New DataView

        Try

            If GridViewSortDirection = SortDirection.Ascending Then
                dv = bindgrids()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "ASC"
                grdHistory.DataSource = dv
                grdHistory.DataBind()
                GridViewSortDirection = SortDirection.Descending

            Else
                bindgrids()
                dv.Sort = CType(ViewState("sortExpr"), String) + " " + "DESC"
                grdHistory.DataSource = dv
                grdHistory.DataBind()
                GridViewSortDirection = SortDirection.Ascending
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Product.grdHistory_Sorting", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If

        End Try
    End Sub

    Public Function bindgrids() As DataView
        Dim dv As New DataView
        Try

            Dim prod As String = Session("productNumber").ToString
            If prod.Length = 1 Then
                prod = "0" + prod.TrimEnd.TrimStart
            End If

            Dim dal = New GeneralizedDAL()
            Dim parcollection(6) As SqlParameter

            Dim ParUserName = New SqlParameter("@UserName", SqlDbType.NVarChar, 250)
            Dim ParProductNumber = New SqlParameter("@ProductNumber", SqlDbType.NVarChar, 3)
            Dim ParResetPrice = New SqlParameter("@ResetPrice", SqlDbType.NVarChar, 50)
            Dim ParFromDate = New SqlParameter("@FromDate", SqlDbType.NVarChar, 25)
            Dim ParToDate = New SqlParameter("@ToDate", SqlDbType.NVarChar, 25)
            Dim ParDateAdded = New SqlParameter("@DateAdded", SqlDbType.NVarChar, 25)
            Dim Parflag = New SqlParameter("@flag", SqlDbType.Int)

            ParUserName.Direction = ParameterDirection.Input
            ParProductNumber.Direction = ParameterDirection.Input
            ParResetPrice.Direction = ParameterDirection.Input
            ParFromDate.Direction = ParameterDirection.Input
            ParToDate.Direction = ParameterDirection.Input
            ParDateAdded.Direction = ParameterDirection.Input
            Parflag.Direction = ParameterDirection.Input

            ParUserName.Value = ""
            ParProductNumber.Value = prod
            ParResetPrice.Value = 0.0
            ParFromDate.Value = ""
            ParToDate.Value = ""
            ParDateAdded.Value = ""
            Parflag.Value = 2

            parcollection(0) = ParUserName
            parcollection(1) = ParProductNumber
            parcollection(2) = ParResetPrice
            parcollection(3) = ParFromDate
            parcollection(4) = ParToDate
            parcollection(5) = ParDateAdded
            parcollection(6) = Parflag

            Dim ds As DataSet
            ds = dal.ExecuteStoredProcedureGetDataSet("SP_Update_TXTN_Price_Cost_History", parcollection)

            If ds IsNot Nothing Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        grdHistory.DataSource = ds.Tables(0)
                        dv = New DataView(ds.Tables(0))
                        grdHistory.DataBind()
                    Else
                        grdHistory.DataSource = Nothing
                        dv = Nothing
                        grdHistory.DataBind()
                    End If
                Else
                    grdHistory.DataSource = Nothing
                    dv = Nothing
                    grdHistory.DataBind()
                End If
            Else
                grdHistory.DataSource = Nothing
                dv = Nothing
                grdHistory.DataBind()
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Product.bindgrids", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
        Return dv
    End Function


    Public Property GridViewSortDirection() As SortDirection
        Get

            If ViewState("sortDirection") Is Nothing Then

                ViewState("sortDirection") = SortDirection.Ascending
            End If

            Return CType(ViewState("sortDirection"), SortDirection)

        End Get
        Set(ByVal Value As SortDirection)
            ViewState("sortDirection") = Value
        End Set
    End Property

End Class
