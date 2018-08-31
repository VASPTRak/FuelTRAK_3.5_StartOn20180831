Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Partial Class Product
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim i As Integer
    Dim txtctl As TextBox
    Dim txtctl1 As TextBox
    Dim Chkctl As CheckBox
    Dim lblctl As Label
    Dim txtPrl As TextBox  ' By Soham Gangavane Sep 14, 2017
    Dim TxtStartDateCheck As TextBox ' By Soham Gangavane Sep 14, 2017
    Dim TxtEndDateCheck As TextBox ' By Soham Gangavane Sep 14, 2017

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            Dim checkValue As Boolean = True
            'For i = 1 To 15
            '    txtPrl = CType(Page.FindControl("txtPr" & i), TextBox) ' By Soham Gangavane Sep 14, 2017
            '    'txtDate = CType(Page.FindControl("txtDate" & i), TextBox) ' By Soham Gangavane Sep 14, 2017
            '    Dim result As Double = 0.0

            '    If Double.TryParse(txtPrl.Text, result) Then
            '        checkValue = True
            '        'Dim newDate As DateTime
            '        'Try
            '        '    newDate = DateTime.Parse(txtDate.Text)
            '        'Catch ex As Exception
            '        '    checkValue = False
            '        '    Exit For
            '        'End Try
            '    Else
            '        checkValue = False
            '        Exit For
            '    End If
            'Next


            For i = 1 To 15
                lblctl = CType(Page.FindControl("L" & i), Label)
                txtctl = CType(Page.FindControl("txtN" & i), TextBox)
                Chkctl = CType(Page.FindControl("Chk" & i), CheckBox)
                txtctl1 = CType(Page.FindControl("txtE" & i), TextBox)
                'txtPrl = CType(Page.FindControl("txtPr" & i), TextBox) ' By Soham Gangavane Sep 14, 2017
                'txtDate = CType(Page.FindControl("txtDate" & i), TextBox) ' By Soham Gangavane Sep 14, 2017
                If Chkctl.Checked = True Then
                    InsertData(lblctl.Text, txtctl.Text, True, txtctl1.Text) ' By Soham Gangavane Sep 14, 2017
                Else
                    InsertData(lblctl.Text, txtctl.Text, False, txtctl1.Text) ' By Soham Gangavane Sep 14, 2017
                End If
            Next
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record has been saved successfully !!');parent.location.href='Mainmenu.aspx';</script>")
           
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product.btnOk_Click", ex)
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            addattribute()
            If Session("User_name") Is Nothing Then 'check for session is null/not
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
            ElseIf Not Page.IsPostBack Then
                Session("productNumber") = 0
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
            Parval.value = "0"
            parcollection(0) = Parval
            ds = dal.ExecuteStoredProcedureGetDataSet("use_tt_Productlist", parcollection)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To 14

                        txtctl = CType(Page.FindControl("txtN" & (i + 1)), TextBox)
                        txtctl.Text = ds.Tables(0).Rows(i)(0).ToString()

                        Chkctl = CType(Page.FindControl("chk" & (i + 1)), CheckBox)
                        If ds.Tables(0).Rows(i)(1).ToString() = "True" Then
                            Chkctl.Checked = True
                        Else
                            Chkctl.Checked = False
                        End If

                        txtctl = CType(Page.FindControl("txtE" & (i + 1)), TextBox)
                        txtctl.Text = ds.Tables(0).Rows(i)(2).ToString()

                        'txtPrl = CType(Page.FindControl("txtPr" & (i + 1)), TextBox) ' By Soham Gangavane Sep 14, 2017
                        'If ds.Tables(0).Rows(i)(4).ToString() = "" Then
                        '    txtPrl.Text = "0.0"
                        'Else
                        '    txtPrl.Text = ds.Tables(0).Rows(i)(4).ToString()
                        'End If

                        'txtDate = CType(Page.FindControl("txtDate" & (i + 1)), TextBox) ' By Soham Gangavane Sep 14, 2017
                        'If ds.Tables(0).Rows(i)(5).ToString() = "" Then
                        '    Dim strToday As String
                        '    strToday = Date.Today.Month & "/" & Date.Today.Day & "/" & Date.Today.Year
                        '    txtDate.Text = strToday
                        'Else
                        '    txtDate.Text = ds.Tables(0).Rows(i)(5).ToString()
                        'End If

                    Next
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product.BindControl", ex)
        End Try
    End Sub

    Public Function InsertData(ByVal Number As String, ByVal name As String, ByVal Primary As Boolean, ByVal code As String) As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim parcollection(5) As SqlParameter
            Dim ParName = New SqlParameter("@NAME", SqlDbType.NVarChar, 20)
            Dim ParPrimary = New SqlParameter("@Primary", SqlDbType.Bit)
            Dim ParCode = New SqlParameter("@code", SqlDbType.NVarChar, 50)
            Dim ParNumber = New SqlParameter("@Number", SqlDbType.NVarChar, 2)
            Dim ParPrice = New SqlParameter("@Price", SqlDbType.Decimal) ' By Soham Gangavane Sep 14, 2017
            Dim ParModifiedDate = New SqlParameter("@ModifiedDate", SqlDbType.NVarChar, 10) ' By Soham Gangavane Sep 14, 2017

            ParName.Direction = ParameterDirection.Input
            ParPrimary.Direction = ParameterDirection.Input
            ParCode.Direction = ParameterDirection.Input
            ParNumber.Direction = ParameterDirection.Input
            ParPrice.Direction = ParameterDirection.Input ' By Soham Gangavane Sep 14, 2017
            ParModifiedDate.Direction = ParameterDirection.Input ' By Soham Gangavane Sep 14, 2017

            ParName.Value = name
            ParPrimary.Value = Primary
            ParCode.Value = code
            ParNumber.Value = Number
            ParPrice.Value = 0.0 'By Soham Gangavane Sep 14, 2017
            ParModifiedDate.Value = DateTime.Now ' By Soham Gangavane Sep 14, 2017

            parcollection(0) = ParName
            parcollection(1) = ParPrimary
            parcollection(2) = ParCode
            parcollection(3) = ParNumber
            parcollection(4) = ParPrice ' By Soham Gangavane Sep 14, 2017
            parcollection(5) = ParModifiedDate ' By Soham Gangavane Sep 14, 2017

            Return dal.ExecuteSQLStoredProcedureGetBoolean("use_tt_InsertProduct", parcollection)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product.BindControl", ex)
        End Try
    End Function

    Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Try
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>parent.location.href='Mainmenu.aspx'</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product.btncancel_Click", ex)
        End Try

    End Sub

    Public Sub addattribute()
        Try
            For i = 0 To 14
                txtctl = CType(Page.FindControl("txtN" & (i + 1)), TextBox)
                txtctl.Attributes.Add("OnKeyPress", "KeyPress(event);")
                txtctl1 = CType(Page.FindControl("txtE" & (i + 1)), TextBox)
                txtctl1.Attributes.Add("OnKeyPress", "KeyPress(event);")
                txtPrl = CType(Page.FindControl("TxtPr" & (i + 1)), TextBox)
                txtPrl.Attributes.Add("OnKeyPress", "return KeyPressProduct(event);")

                TxtStartDateCheck = CType(Page.FindControl("txtStartDate" & (i + 1)), TextBox)
                TxtStartDateCheck.Attributes.Add("OnKeyPress", "KeyUpEvent_DateTextBox(event,'txtStartDate" & (i + 1) & "')")
                TxtEndDateCheck = CType(Page.FindControl("txtEndDate" & (i + 1)), TextBox)
                TxtEndDateCheck.Attributes.Add("OnKeyPress", "KeyUpEvent_DateTextBox(event,'txtEndDate" & (i + 1) & "')")
                'txtDate = CType(Page.FindControl("txtDate" & (i + 1)), TextBox)
                'txtDate.Attributes.Add("onkeyup", "KeyUpEvent_DateTextBox(event,'txtDate" & (i + 1) & "')")
                'txtDate.Attributes.Add("OnKeyPress", "return event.charCode >= 48 && event.charCode <= 57")
            Next
            'txtDate = CType(Page.FindControl("txtDate1"), TextBox)
            'txtDate.Attributes.Add("onkeyup", "KeyUpEvent_DateTextBox(event,'txtDate1')")
            'txtDate.Attributes.Add("OnKeyPress", "AllowNumeric('txtDate1');")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Product.addattribute", ex)
        End Try

    End Sub

    Private Sub saveValues()
        Try
            Dim newDate As DateTime
            Dim checkValue As Integer = 0
            TxtStartDateCheck = CType(Page.FindControl("txtStartDate" & Session("productNumber").ToString()), TextBox)
            TxtEndDateCheck = CType(Page.FindControl("txtEndDate" & Session("productNumber").ToString()), TextBox)
            txtPrl = CType(Page.FindControl("TxtPr" & Session("productNumber").ToString()), TextBox)

            Try
                If Not txtPrl.Text <> "" Then
                    checkValue = 2
                End If
            Catch ex As Exception
                checkValue = 2
            End Try

            Try
                newDate = DateTime.Parse(TxtStartDateCheck.Text)
                newDate = DateTime.Parse(TxtEndDateCheck.Text)
            Catch ex As Exception
                checkValue = 1
            End Try

            If checkValue = 0 Then

                Dim dal = New GeneralizedDAL()
                Dim parcollection(6) As SqlParameter

                Dim ParUserName = New SqlParameter("@UserName", SqlDbType.NVarChar, 250)
                Dim ParProductNumber = New SqlParameter("@ProductNumber", SqlDbType.NVarChar, 3)
                Dim ParResetPrice = New SqlParameter("@ResetPrice", SqlDbType.Decimal)
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

                Dim Prod As String = ""
                If Session("productNumber").ToString.TrimStart.TrimEnd.Length = 1 Then
                    Prod = "0" + Session("productNumber").ToString.TrimStart.TrimEnd
                Else
                    Prod = Session("productNumber").ToString.TrimStart.TrimEnd
                End If

                ParUserName.Value = Session("User_name").ToString
                ParProductNumber.Value = Prod
                ParResetPrice.Value = Convert.ToDecimal(txtPrl.Text)
                ParFromDate.Value = TxtStartDateCheck.Text
                ParToDate.Value = TxtEndDateCheck.Text
                ParDateAdded.Value = DateTime.Today.Month & "/" & DateTime.Today.Day & "/" & DateTime.Today.Year
                Parflag.Value = 1

                parcollection(0) = ParUserName
                parcollection(1) = ParProductNumber
                parcollection(2) = ParResetPrice
                parcollection(3) = ParFromDate
                parcollection(4) = ParToDate
                parcollection(5) = ParDateAdded
                parcollection(6) = Parflag

                Dim ds As DataSet
                ds = dal.ExecuteStoredProcedureGetDataSet("SP_Update_TXTN_Price_Cost_History", parcollection)

                TxtStartDateCheck.Text = ""
                TxtEndDateCheck.Text = ""
                txtPrl.Text = ""

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('New price updated in transaction records for selected products!!');</script>")
            ElseIf checkValue = 1 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Date format are not valid. Please enter valid date and try again !!');</script>")
            ElseIf checkValue = 2 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Price field is empty. Please enter Price and try again !!');</script>")

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPrice1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice1.Click
        Session("productNumber") = 1
        saveValues()
    End Sub

    Protected Sub btnPrice2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice2.Click
        Session("productNumber") = 2
        saveValues()
    End Sub

    Protected Sub btnPrice3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice3.Click
        Session("productNumber") = 3
        saveValues()
    End Sub

    Protected Sub btnPrice4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice4.Click
        Session("productNumber") = 4
        saveValues()
    End Sub

    Protected Sub btnPrice5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice5.Click
        Session("productNumber") = 5
        saveValues()
    End Sub

    Protected Sub btnPrice6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice6.Click
        Session("productNumber") = 6
        saveValues()
    End Sub

    Protected Sub btnPrice7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice7.Click
        Session("productNumber") = 7
        saveValues()
    End Sub

    Protected Sub btnPrice8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice8.Click
        Session("productNumber") = 8
        saveValues()
    End Sub

    Protected Sub btnPrice9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice9.Click
        Session("productNumber") = 9
        saveValues()
    End Sub

    Protected Sub btnPrice10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice10.Click
        Session("productNumber") = 10
        saveValues()
    End Sub

    Protected Sub btnPrice11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice11.Click
        Session("productNumber") = 11
        saveValues()
    End Sub

    Protected Sub btnPrice12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice12.Click
        Session("productNumber") = 12
        saveValues()
    End Sub

    Protected Sub btnPrice13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice13.Click
        Session("productNumber") = 13
        saveValues()
    End Sub

    Protected Sub btnPrice14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice14.Click
        Session("productNumber") = 14
        saveValues()
    End Sub

    Protected Sub btnPrice15_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrice15.Click
        Session("productNumber") = 15
        saveValues()
    End Sub

    Protected Sub btnHistory1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory1.Click
        Session("productNumber") = 1
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory2.Click
        Session("productNumber") = 2
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory3.Click
        Session("productNumber") = 3
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory4.Click
        Session("productNumber") = 4
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory5.Click
        Session("productNumber") = 5
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory6.Click
        Session("productNumber") = 6
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory7.Click
        Session("productNumber") = 7
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory8.Click
        Session("productNumber") = 8
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory9.Click
        Session("productNumber") = 9
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory10.Click
        Session("productNumber") = 10
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory11.Click
        Session("productNumber") = 11
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory12.Click
        Session("productNumber") = 12
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory13.Click
        Session("productNumber") = 13
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory14.Click
        Session("productNumber") = 14
        bindgrids()
        PopUpHistory.Visible = True
    End Sub

    Protected Sub btnHistory15_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory15.Click
        Session("productNumber") = 15
        bindgrids()
        PopUpHistory.Visible = True
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
