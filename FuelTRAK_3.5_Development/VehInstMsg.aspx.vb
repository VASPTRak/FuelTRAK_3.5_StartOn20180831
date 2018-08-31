Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Partial Class vehInstMsg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Default
            PopUpVehMsg.Visible = False
            PopUpCustMsg.Visible = False


            If Session("User_name") Is Nothing Then 'check for session is null/not
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else

                If (Not IsPostBack) Then
                    Try
                        
                        Page.Title = "Vehicle Search"

                        tdsearch.Visible = True
                        SearchClick()

                    Catch ex As Exception
                        Dim cr As New ErrorPage
                        cr.errorlog("vehInstMsg.Page_Load", ex)
                    End Try
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("vehInstMsg.Page_Load", ex)
        End Try
    End Sub
    
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            tdsearch.Visible = True
            SearchClick()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("vehInstMsg.btnSearch_Click", ex)
        End Try

    End Sub

    Public Sub SearchClick()
        Dim gridflag As Integer = 0
        Dim dal = New GeneralizedDAL()



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


            gvVehRslts.DataSource = ds
            gvVehRslts.DataBind()
            tdsearch.Visible = False 'strSearch


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehInstMsg.Search_Click", ex)

        End Try
    End Sub

    Protected Sub gvVehRslts_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvVehRslts.PageIndexChanging

        Try
            gvVehRslts.PageIndex = e.NewPageIndex
            SearchClick()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehInstMsg.gvVehRslts_PageIndexChanging", ex)
        End Try
       
    End Sub

    Protected Sub gvVehRslts_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVehRslts.RowCommand
        Try
            txtPopUpVehiclelD.Text = ""
            Dim row As GridViewRow = DirectCast(DirectCast(e.CommandSource, Button).NamingContainer, GridViewRow)
            If (e.CommandName = "VehID") Then
                PopUpVehMsg.Visible = True
                Dim txtVehID As Label = DirectCast(row.FindControl("lblIdentity"), Label)
                txtPopUpVehiclelD.Text = txtVehID.Text.ToString()
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehInstMsg.gvVehRslts_PageIndexChanging", ex)
        End Try
    End Sub

    Protected Sub btnVehMsgSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVehMsgSave.Click
        Try
            UpdateInstVehMsgs(txtPopUpVehiclelD.Text.Trim(), txtInstmsg.Text)
            PopUpVehMsg.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehInstMsg.btnVehMsgSave_Click", ex)
        End Try
    End Sub

    Protected Sub btnVehMsgCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVehMsgCancel.Click
        Try

            PopUpVehMsg.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnVehMsgCancel_Click", ex)
        End Try
    End Sub
    'Added By Varun Moota to save Instant Vehicle Messages.06/08/2011
    Private Sub UpdateInstVehMsgs(ByVal VehID As String, ByVal Msg As String)
        Try
            Dim dal = New GeneralizedDAL()
            Dim blnFlag As Boolean


            
            Dim parcollection(2) As SqlParameter
            Dim ParVehicleId = New SqlParameter("@VehicleId", SqlDbType.VarChar, 10)
            Dim ParVehMsg = New SqlParameter("@VehMsg", SqlDbType.VarChar, 250)
            Dim ParPolled = New SqlParameter("@Polled", SqlDbType.Bit)

            ParVehicleId.Direction = ParameterDirection.Input
            ParVehMsg.Direction = ParameterDirection.Input
            ParPolled.Direction = ParameterDirection.Input

            ParVehicleId.value = VehID.ToString()
            ParVehMsg.Value = Msg.ToString()
            ParPolled.Value = "False"

            parcollection(0) = ParVehicleId
            parcollection(1) = ParVehMsg
            parcollection(2) = ParPolled

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("USP_TT_INSERT_VehicleInstMsgs", parcollection)
              
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehInstMsg.UpdateInstVehMsgs()", ex)
        End Try

    End Sub

    Protected Sub btnCustomMsg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCustomMsg.Click
        Try
            PopUpCustMsg.Visible = True

            If Not lstbox1.Items.Count > 0 Then
                Dim dal As New GeneralizedDAL()

                Dim qry As String = "SELECT * FROM vehicleCustMsgs"
                Dim dsvehCustMsgs As New DataSet
                dsvehCustMsgs = dal.GetDataSet(qry)
                lstbox1.DataSource = dsvehCustMsgs.Tables(0)
                lstbox1.DataTextField = "VehMessages"
                lstbox1.DataValueField = "VehMessages"
                lstbox1.DataBind()

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehInstMsg.btnCustomMsg_Click", ex)
        End Try
    End Sub

    Protected Sub btnCustClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCustClose.Click
        Try
            PopUpCustMsg.Visible = False
            PopUpVehMsg.Visible = True
            Dim strVehMsgs As New StringBuilder
            'Add Veh Custom Messages,And Append to the Existing String.
            For i As Integer = 0 To lstbox2.Items.Count - 1
                If (i = lstbox2.Items.Count - 1) Then
                    strVehMsgs.Append(lstbox2.Items(i).Value.ToString() & ".")
                Else
                    strVehMsgs.Append(lstbox2.Items(i).Value.ToString() & ",")
                End If
            Next
            txtInstmsg.Text = txtInstmsg.Text.ToString() + " " + strVehMsgs.ToString()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnCustClose_Click", ex)
        End Try
    End Sub

    Protected Sub btnAddItems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAddItems.Click
        Try

            'For Adding items in the list 
            For i As Integer = lstbox1.Items.Count - 1 To 0 Step -1
                If lstbox1.Items(i).Selected Then
                    lstbox2.Items.Add(lstbox1.Items(i).Value)
                    lstbox1.Items.Remove(lstbox1.Items(i).Value)
                End If

            Next
            PopUpCustMsg.Visible = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehInstMsg.btnAddItems_Click", ex)
        End Try
    End Sub

    Protected Sub btnRemoveItems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemoveItems.Click
        Try
            'For Removing items in the list 
            For i As Integer = lstbox2.Items.Count - 1 To 0 Step -1
                If lstbox2.Items(i).Selected Then
                    lstbox1.Items.Add(lstbox2.Items(i).Value)
                    lstbox2.Items.Remove(lstbox2.Items(i).Value)
                End If

            Next
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("VehInstMsg.btnRemoveItems_Click", ex)
        End Try
    End Sub

End Class
