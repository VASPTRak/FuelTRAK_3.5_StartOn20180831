Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Partial Class Vehicle_Edit
    Inherits System.Web.UI.Page
    Dim mhflag = ""
    'Code modified by Jatin Kshirsagar 30 Aug 2008
    Dim VEHTcount As Integer
    Dim Dal = New GeneralizedDAL()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dal = New GeneralizedDAL()
        Dim ds = New DataSet()
        PopPM.Visible = False
        PopUpProduct.Visible = False
        PopUpVehMsg.Visible = False
        PopUpCustMsg.Visible = False
        'DDLstDepartment.Attributes.Add("size", "10")

        If Session("User_name") Is Nothing Then 'check for session is null/not
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
        Else
            If (Not IsPostBack) Then
                Try
                    txtCOdometer.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    'By Soham Gangavane Aug 28, 2017
                    txtPOdometer.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtCHours.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    'By Soham Gangavane Aug 28, 2017
                    txtPHours.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtMileage.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtKeyexp.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtCardexp.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtYear.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtFuellimit.Attributes.Add("OnKeyPress", "KeyPressEvent_txtFuellimit(event);")

                    txtKeyexp.Attributes.Add("onkeyup", "KeyUpEvent_txtKeyexp(event);")
                    txtCard.Attributes.Add("onkeyup", "KeyUpEvent_txtCard(event);")
                    txtCardexp.Attributes.Add("onkeyup", "KeyUpEvent_txtCardexp(event);")
                    txtYear.Attributes.Add("onkeyup", "KeyUpEvent_txtYear(event);")
                    txtType.Attributes.Add("onkeyup", "KeyUpEvent_txtType(event);")
                    txtSubdepartment.Attributes.Add("onkeyup", "KeyUpEvent_txtSubdepartment(event);")
                    txtFuellimit.Attributes.Add("onkeyup", "KeyUpEvent_txtFuellimit(event);")

                    txtCurrOdomtr.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtNxtPmOdo.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtPmIntr.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")
                    txtPmTxtn.Attributes.Add("OnKeyPress", "KeyPressEvent(event);")

                    txtVehicleID.Attributes.Add("OnKeyPress", "KeyPressEvent_RemoveLessThan(event);")

                    'Me.txtNxtPmOdo.Attributes.Add("onKeyUp", "javascript:CheckNextPM();")
                    Me.txtNxtPmOdo.Attributes.Add("onBlur", "javascript:CheckNextPM();")
                    'Me.btnPMOK.Attributes.Add("onKeyUp", "javascript:CheckNextPM();")
                    'Added By Varun Moota, to handle Client-Side Actions. 05/23/2011
                    'btnAddItems.Attributes.Add("OnClick", "getResult();return false;")
                    'btnRemoveItems.Attributes.Add("OnClick", "getResult();return false;")


                    If Not Session("visited") Or Not IsPostBack Then
                        'Populate Department Information
                        DDLstDepartment.Items.Clear()
                        ds = New DataSet()
                        ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_PerPopulateDeptList")
                        If Not ds Is Nothing Then
                            If ds.Tables(0).Rows.Count > 0 Then
                                DDLstDepartment.DataSource = ds.Tables(0)
                                DDLstDepartment.DataTextField = "DeptNoName"
                                DDLstDepartment.DataValueField = "NUMBER"
                                DDLstDepartment.DataBind()
                            End If
                        End If
                        'Edit Vehicle Information
                        If (Request.QueryString.Count = 1 And Not IsPostBack) Then
                            lblNew_Edit.Text = "Edit Vehicle Information"
                            btnOk.Enabled = False
                            btnDeleteVehicle.Enabled = True
                            'txtVehicleID.Enabled = False
                            CboxDisabled.Disabled = False
                            'Get Vehicle list and TotalCount
                            ds = New DataSet
                            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehListAndCount")
                            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("VehicleId")}
                            Dim cnt As Long = 0
                            If Not ds Is Nothing Then
                                If ds.Tables(0).Rows.Count > 0 Then
                                    Dim dr As DataRow = ds.Tables(0).Rows.Find(Request.QueryString(0).ToString())
                                    If Not IsDBNull(dr) Then
                                        cnt = ds.Tables(0).Rows.IndexOf(dr)
                                    End If
                                    Session("VEHDS") = ds
                                    Session("currentrecord") = cnt
                                    Session("VEHTCnt") = ds.Tables(1).Rows(0)(0).ToString()
                                    FillTextbox(cnt)

                                    'Added By Varun Moota. 02/10/2010
                                    txtVehicleID.Attributes.Add("onblur", "All('" + txtVehicleID.Text + "');")

                                End If
                                If ds.Tables(1).Rows.Count > 0 Then
                                    Session("VEHTCnt") = ds.Tables(1).Rows(0)(0).ToString()
                                    lblof.Text = cnt + 1 & " " & "of" & " " & ds.Tables(1).Rows(0)(0).ToString()
                                End If
                                txtVId.Value = "True"
                            End If

                            'Added By Varun to Hide the EncodeKey Button.02/09/2010
                            btnEncodeKey.Visible = False
                            encodeKeyLink.Visible = True
                            btnVehPath.Visible = True

                            'Added By Varun Moota to Have Active Troubled Codes fro the Vehicle.04/21/2011
                            Dim dsTCodes As DataSet
                            Dim parcollection(0) As SqlParameter
                            Dim ParVehID = New SqlParameter("@VehicleID", SqlDbType.NVarChar)
                            ParVehID.Direction = ParameterDirection.Input
                            ParVehID.Value = txtVehicleID.Text.ToString()
                            parcollection(0) = ParVehID
                            dsTCodes = dal.ExecuteStoredProcedureGetDataSet("SP_GET_VEHICLE_TROUBLECODE_DATA", parcollection)
                            If dsTCodes.Tables(0).Rows.Count > 0 Then
                                Dim dtRecords As DataTable
                                dtRecords = RemoveDuplicateRows(dsTCodes.Tables(0), "CODES")
                                txtActCodes.Text = dsTCodes.Tables(1).Rows(0)(0).ToString()
                                ddlCodes.DataSource = dtRecords
                                ddlCodes.DataTextField = "CODES"
                                ddlCodes.DataValueField = "CODES"
                                ddlCodes.DataBind()
                            Else
                                txtActCodes.Text = "0"
                                ddlCodes.Items.Insert(0, New ListItem("---------NONE---------", ""))
                            End If
                        ElseIf (Not IsPostBack) Then
                            txtKey.Text = "0000"
                            lblNew_Edit.Text = "New Vehicle Information"
                            Session("VehIDCheck") = ""
                            txtVehicleID.Focus()
                            CboxNolimit.Checked = True
                            txtDailylimit.Enabled = False
                            txtDailylimit.Text = ""
                            txtDailylimit.BackColor = Drawing.Color.LightGray
                            txtLastfueler.Text = "No Info"
                            txtFueldatetime.Text = DateTime.Now
                            CboxDisabled.Disabled = True
                            btnVehicleUses.Text = "Mileage"
                            'txtCHours.Visible = False
                            'Label2.Visible = False
                            'By Soham Gangavane Aug 28, 2017
                            divHours.Visible = False
                            Session("Product") = ""

                            lblof.Visible = False
                            btnFirst.Visible = False
                            btnLast.Visible = False
                            btnNext.Visible = False
                            txtKeyexp.Enabled = True
                            txtKey.Enabled = True
                            txtKey.Text = "*NEW*"
                            btnprevious.Visible = False
                            txtVId.Value = "True"

                            'Added By Varun to Hide the EncodeKey Button.02/09/2010
                            btnEncodeKey.Visible = False
                            encodeKeyLink.Visible = False
                            btnVehPath.Visible = False
                        End If
                    End If

                Catch ex As Exception
                    Dim cr As New ErrorPage
                    Dim errmsg As String
                    cr.errorlog("Vehicle_Edit.Page_Load", ex)
                    If ex.Message.Contains(";") Then
                        errmsg = ex.Message.ToString()
                        errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
                    Else
                        errmsg = ex.Message.ToString()
                    End If
                Finally
                End Try
            End If
        End If

    End Sub

    Public Sub FillTextbox(ByVal cnt As Integer)
        Try
            Dim ds = New DataSet()
            Dim fuel As String

            'Changed by Sudhanshu:06/26/2015 - Refresh the DS on click of NEXT and Previous Button
            'ds = CType(Session("VEHDS"), DataSet)
            ds = Dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehListAndCount")
            '---------------------------------------------

            VEHTcount = Session("VEHTCnt")
            txtVehicleID.Text = ds.Tables(0).Rows(cnt).Item("IDENTITY").ToString()
            encodeKeyLink.NavigateUrl = "fueltrakencode:v" + ds.Tables(0).Rows(cnt).Item("IDENTITY").ToString()

            'Added By Varun Moota.03/08/2010
            Session("VehIDCheck") = txtVehicleID.Text.Trim()
            txtGPSId.Text = GetVehGPSID(txtVehicleID.Text)

            txtvehicleIdHide.Text = ds.Tables(0).Rows(cnt).Item("VehicleID").ToString()
            txtLicense.Text = ds.Tables(0).Rows(cnt).Item("LICNO").ToString()
            txtDescription.Text = ds.Tables(0).Rows(cnt).Item("EXTENSION").ToString()
            txtKey.Text = ds.Tables(0).Rows(cnt).Item("KEY_NUMBER").ToString()
            If txtKey.Text = "00000" Then btnLostKey.Disabled = True Else btnLostKey.Disabled = False

            'Added By Varun to Disable the LostKey Button, if the Key# Exists in KeyLock Table.
            Dim dsLostKeyNumber As New DataSet
            'Dim dal As New GeneralizedDAL
            dsLostKeyNumber = dal.GetDataSet("DELETE FROM KeyLock WHERE Key_Number LIKE '%NEW%' or Key_Number ='00000'")
            dsLostKeyNumber = dal.GetDataSet("Select isnull(Key_Number,'00000') FROM KeyLock WHERE Key_Number = '" + txtKey.Text + "'")
            If dsLostKeyNumber.Tables(0).Rows.Count > 0 Then
                If dsLostKeyNumber.Tables(0).Rows(0)(0).ToString() <> "" Then
                    txtKey.BackColor = Drawing.Color.DarkRed
                    lblLoskKey.Visible = True
                    btnLostKey.Disabled = True
                End If
            Else
                txtKey.BackColor = System.Drawing.Color.White 'System.Drawing.ColorTranslator.FromHtml("#C0C0C0")
                lblLoskKey.Visible = False
                btnLostKey.Disabled = False
            End If


            txtKeyexp.Text = ds.Tables(0).Rows(cnt).Item("KEY_EXP").ToString()
            txtCard.Text = ds.Tables(0).Rows(cnt).Item("CARD_ID").ToString()

            'Added By Varun Moota to Check Whether VEHICLE ID is being Deleted or Not.10/12/2010
            Dim dsDeletedVeh As New DataSet
            Session("delVehValue") = "0"
            dsDeletedVeh = dal.GetDataSet("Select * FROM VEHS WHERE [IDENTITY] = '" + txtVehicleID.Text + "' AND NEWUPDT = 3")
            If dsDeletedVeh.Tables(0).Rows.Count > 0 Then
                btnDeleteVehicle.BackColor = Drawing.Color.Red
                btnDeleteVehicle.Text = "UnDelete"
                lblDelVeh.Visible = True
                Session("delVehValue") = "1"
                'And also Add Key to the KeyLock Table
                SaveLockDetails("K")

                'Added By Pritam to disable Save button for deleted vehicle Date : 24-July-2014
                btnSave.Enabled = False
                btnPM.Enabled = False
                btnMessage.Enabled = False
                btnEncodeKey.Enabled = False
                btnLostCard.Disabled = True
                btnLostKey.Disabled = True
                encodeKeyLink.Enabled = False
                btnVehPath.Enabled = False
            Else
                btnDeleteVehicle.Text = "Delete"
                btnDeleteVehicle.BackColor = Drawing.Color.LightGray
                lblDelVeh.Visible = False

                'Added By Pritam to Enable Save button for non-deleted vehicle Date : 24-July-2014
                btnSave.Enabled = True
                btnPM.Enabled = True
                btnMessage.Enabled = True
                btnEncodeKey.Enabled = True
                btnLostCard.Disabled = False
                btnLostKey.Disabled = False
                encodeKeyLink.Enabled = True
                btnVehPath.Enabled = True

            End If

            'Added By Varun Moota.03/08/2010
            Session("CardIDCheck") = txtCard.Text.Trim()

            If txtCard.Text = "" Then btnLostCard.Disabled = True Else btnLostCard.Disabled = False
            txtCardexp.Text = ds.Tables(0).Rows(cnt).Item("CARD_EXP").ToString()
            txtYear.Text = ds.Tables(0).Rows(cnt).Item("VEHYEAR").ToString()
            txtMake.Text = ds.Tables(0).Rows(cnt).Item("VEHMAKE").ToString()
            txtModel.Text = ds.Tables(0).Rows(cnt).Item("VEHMODEL").ToString()
            txtType.Text = ds.Tables(0).Rows(cnt).Item("TYPE").ToString()
            txtVinNumber.Text = ds.Tables(0).Rows(cnt).Item("VEHVIN").ToString()
            txtTagID.Text = ds.Tables(0).Rows(cnt).Item("Tag_ID").ToString()
            Try
                DDLstDepartment.SelectedValue = ds.Tables(0).Rows(cnt).Item("DEPT").ToString()
            Catch ex As Exception
                DDLstDepartment.SelectedIndex = 0
            End Try

            txtSubdepartment.Text = ds.Tables(0).Rows(cnt).Item("SUBDEPT").ToString()
            If (Convert.ToBoolean(ds.Tables(0).Rows(cnt).Item("MILEAGE").ToString()) = True) Then
                btnVehicleUses.Text = "Mileage"
                txtMileage.Text = ds.Tables(0).Rows(cnt).Item("MILES_WIND").ToString()
                txtCOdometer.Text = ds.Tables(0).Rows(cnt).Item("CURRMILES").ToString() '16
                txtCHours.Text = ds.Tables(0).Rows(cnt).Item("CURRHOURS").ToString()

                'By Soham Gangavane Aug 28, 2017
                txtPOdometer.Text = ds.Tables(0).Rows(cnt).Item("PREVMILES").ToString()
                txtPHours.Text = ds.Tables(0).Rows(cnt).Item("PREVHOURS").ToString()

                btnVehicleUses.BackColor = Drawing.Color.FromName("#A5BBC5")
            Else
                btnVehicleUses.Text = "Hours"
                txtCHours.Text = ds.Tables(0).Rows(cnt).Item("CURRHOURS").ToString() '17
                txtCOdometer.Text = ds.Tables(0).Rows(cnt).Item("CURRMILES").ToString()

                'By Soham Gangavane Aug 28, 2017
                txtPOdometer.Text = ds.Tables(0).Rows(cnt).Item("PREVMILES").ToString()
                txtPHours.Text = ds.Tables(0).Rows(cnt).Item("PREVHOURS").ToString()

                txtMileage.Text = ds.Tables(0).Rows(cnt).Item("MILES_WIND").ToString()
                btnVehicleUses.BackColor = Drawing.Color.FromName("#A5BBC5")
            End If
            DisableControls()
            txtFuellimit.Text = ds.Tables(0).Rows(cnt).Item("LIMIT").ToString() '18
            If ds.Tables(0).Rows(cnt).Item("LIMIT").ToString() = "" Then DDLFuelLimit.Text = "NO" Else DDLFuelLimit.Text = ds.Tables(0).Rows(cnt).Item("LIMIT").ToString() '18
            txtDailylimit.Text = ds.Tables(0).Rows(cnt).Item("TXTNLIMIT").ToString() 'sqlReader(19).ToString().Trim()
            If (txtDailylimit.Text <> "") And (txtDailylimit.Text <> "0") Then
                txtDailylimit.Enabled = True
                txtDailylimit.BackColor = Drawing.Color.White
                CboxNolimit.Checked = False
            Else
                CboxNolimit.Checked = True
                txtDailylimit.Enabled = False
                txtDailylimit.BackColor = Drawing.Color.LightGray
            End If
            txtAccid.Text = ds.Tables(0).Rows(cnt).Item("ACCT_ID").ToString() 'sqlReader(20).ToString()
            If (ds.Tables(0).Rows(cnt).Item("LASTFUELER").ToString() = "") Then 'sqlReader(28).ToString() = "") Then
                txtLastfueler.Text = "No Info"
            Else
                txtLastfueler.Text = ds.Tables(0).Rows(cnt).Item("LASTFUELER").ToString()
            End If
            'CboxFuelercard
            txtFueldatetime.Text = ds.Tables(0).Rows(cnt).Item("LASTFUELDT").ToString() 'sqlReader(29).ToString().Trim()
            txtExportcode.Text = ds.Tables(0).Rows(cnt).Item("CODE").ToString() 'sqlReader(21).ToString().Trim()
            CboxSecondkey.Checked = Convert.ToBoolean(ds.Tables(0).Rows(cnt).Item("SECOND_KEY").ToString())
            CboxCheckmiles.Checked = Convert.ToBoolean(ds.Tables(0).Rows(cnt).Item("REQODOM").ToString())
            CboxFuelercard.Checked = Convert.ToBoolean(ds.Tables(0).Rows(cnt).Item("MASTER").ToString()) 'MASTER
            CboxDisabled.Checked = Convert.ToBoolean(ds.Tables(0).Rows(cnt).Item("LOCKED").ToString())
            'CboxFacilitycard.Checked = Convert.ToBoolean(If(ds.Tables(0).Rows(cnt).Item("OPTION1").ToString() = "", False, ds.Tables(0).Rows(cnt).Item("OPTION1").ToString())) 'Option 1
            CboxFacilitycard.Checked = Convert.ToBoolean(If(ds.Tables(0).Rows(cnt).Item("OPTION1").ToString() = "", "False", ds.Tables(0).Rows(cnt).Item("OPTION1").ToString()))
            fuel = ds.Tables(0).Rows(cnt).Item("FUELS").ToString()
            txtComments.Text = ds.Tables(0).Rows(cnt).Item("TEXT").ToString()
            txtNxtPmOdo.Text = ds.Tables(0).Rows(cnt).Item("NEXTPMMILE").ToString()
            txtPmIntr.Text = ds.Tables(0).Rows(cnt).Item("PM_INCREM").ToString()
            txtPmTxtn.Text = ds.Tables(0).Rows(cnt).Item("PMTXTNCNT").ToString()
            Dim i As Integer
            Dim s As String = System.String.Empty
            Session("Product") = ""
            For i = 0 To fuel.Length() - 1
                If fuel(i) = "Y" Then
                    s = s + (i + 1).ToString().Trim()
                    Session("Product") += (i + 1).ToString() + ","
                Else
                    s = s + " "
                End If
            Next
            btnProduct.Text = s
            txtprodval1.Value = Session("Product").ToString().Trim()

            If cnt + 1 = 1 Then
                lblof.Visible = True
                btnprevious.Enabled = False
                btnNext.Enabled = True
                btnFirst.Enabled = False
                btnLast.Enabled = True
            ElseIf cnt + 1 = VEHTcount Then
                lblof.Visible = True
                btnprevious.Enabled = True
                btnNext.Enabled = False
                btnFirst.Enabled = True
                btnLast.Enabled = False
            ElseIf cnt + 1 < VEHTcount Then
                lblof.Visible = True
                btnprevious.Enabled = True
                btnNext.Enabled = True
                btnFirst.Enabled = True
                btnLast.Enabled = True
            End If

            'Added By Varun Moota to Get Vehicle Messages if Any.05/23/2011
            Dim dsVehMsg = New DataSet()
            dsVehMsg = dal.GetDataSet("SELECT * FROM VEHS WHERE [IDENTITY] = '" + txtVehicleID.Text.ToString() + "'")
            If Not ds Is Nothing Then
                txtVehMsg1.Text = dsVehMsg.Tables(0).Rows(0).Item("MESSAGELINE1").ToString()
                txtVehMsg2.Text = dsVehMsg.Tables(0).Rows(0).Item("MESSAGELINE2").ToString()
                txtVehMsgCnt.Text = dsVehMsg.Tables(0).Rows(0).Item("MESS_COUNT").ToString()
                Dim strDate As String = dsVehMsg.Tables(0).Rows(0).Item("MESSLASTDT").ToString()
                Dim strExpDate As String = dsVehMsg.Tables(0).Rows(0).Item("MESSAGEEXPDT").ToString()
                If Not strDate = "" Then
                    txtVMLastDT.Text = CDate(strDate).ToShortDateString()
                End If
                If Not strExpDate = "" Then
                    txtVMExpDT.Text = CDate(strExpDate).ToShortDateString()
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Veh_Edit.FillTextBox", ex.InnerException)
        End Try

    End Sub

    Public Sub DisableControls()
        Try
            If btnVehicleUses.Text = "Hours" Then
                'By Soham Gangavane Aug 28, 2017
                divHours.Visible = True
                divmile.Visible = False

                'txtCOdometer.Visible = False
                'Label1.Visible = False
                'txtCHours.Visible = True
                'Label2.Visible = True
                'txtMileage.Visible = True
                'Label3.Visible = True
            ElseIf btnVehicleUses.Text = "Mileage" Then
                'By Soham Gangavane Aug 28, 2017
                divHours.Visible = False
                divmile.Visible = True


                'txtCOdometer.Visible = True
                'Label1.Visible = True
                'txtMileage.Visible = True
                'Label3.Visible = True
                'txtCHours.Visible = False
                'Label2.Visible = False
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Veh_Edit.DisableControls()", ex.InnerException)
        End Try

    End Sub

    Protected Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        'Code modified by Jatin Kshirsagar 30 Aug 2008
        Try
            If DDLstDepartment.Text = "" Then
                'If Not DDLstDepartment.SelectedIndex > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please select department.');</script>")
                Exit Sub
            End If

            If lblNew_Edit.Text = "New Vehicle Information" Then
                If VehicleExists(txtVehicleID.Text.Trim()) = False Then
                    If UCase(txtVId.Value) = "TRUE" Then

                        Session("CondionToRedirect") = "Add Another"
                        SaveUpdateRecords()
                        ClearText()
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Vehicle Identity already exist.')</script>")
                End If
            ElseIf (lblNew_Edit.Text = "Edit Vehicle Information") Then
                If UCase(txtVId.Value) = "TRUE" Then
                    SaveUpdateRecords()
                    ClearText()
                End If
            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehicle_Edit_btnOk_Click", ex)

            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Public Sub ClearText()
        Try


            txtVehicleID.Text = ""
            txtGPSId.Text = ""
            txtVehicleID.Focus()
            txtLicense.Text = ""
            txtDescription.Text = ""
            txtKeyexp.Text = ""
            txtCard.Text = ""
            txtCardexp.Text = ""
            txtYear.Text = ""
            txtMake.Text = ""
            txtModel.Text = ""
            txtType.Text = ""
            txtVinNumber.Text = ""
            txtSubdepartment.Text = ""
            txtFuellimit.Text = ""
            DDLFuelLimit.Text = "NO"
            txtDailylimit.Text = ""
            txtAccid.Text = ""
            txtExportcode.Text = ""
            txtCOdometer.Text = ""
            txtCHours.Text = ""
            'By Soham Gangavane Aug 28, 2017
            txtPOdometer.Text = ""
            txtPHours.Text = ""

            txtMileage.Text = ""
            txtComments.Text = ""
            DDLstDepartment.SelectedIndex = 0

            CboxDisabled.Checked = False
            CboxFuelercard.Checked = False
            CboxSecondkey.Checked = False
            CboxCheckmiles.Checked = False
            CboxFacilitycard.Checked = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.ClearText()", ex)

        End Try
    End Sub

    Protected Sub btnDeleteVehicle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteVehicle.Click
        Dim dal = New GeneralizedDAL()
        Try
            'Changed By Varun Moota, to have Un Delete Funcitionality as Per customers request.10/12/2010

            If Session("delVehValue").ToString = "0" Then
                Dim parcollection(0) As SqlParameter
                Dim ParVehID = New SqlParameter("@VehicleId", SqlDbType.VarChar, 10)
                ParVehID.Direction = ParameterDirection.Input
                'Changed By Sudhanshu 06/26/2015: comment the below line and store the Identity Field for update because we are unable to delete and undelete the records while we use next and previous button.
                'ParVehID.Value = Convert.ToInt32(Request.QueryString.Get("VehicleNo").Trim())
                ParVehID.Value = txtVehicleID.Text
                '----------------------------------------------------------
                parcollection(0) = ParVehID
                Dim blnflag As Boolean
                blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_VehicleDelete", parcollection)
                If blnflag = False Then
                    'Added By Varun Moota, to Lock Key As well if its Deleted.11/12/2010
                    'SaveLockDetails("K")
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Vehicle deleted sucessfully.');location.href='Vehicle.aspx';</script>")
                End If

            ElseIf Session("delVehValue").ToString = "1" Then
                Dim parcollection(0) As SqlParameter
                Dim ParVehID = New SqlParameter("@VehicleId", SqlDbType.VarChar, 10)
                ParVehID.Direction = ParameterDirection.Input
                'Changed By Sudhanshu 06/26/2015: : comment the below line and store the Identity Field for update because we are unable to delete and undelete the records while we use next and previous button.
                'ParVehID.Value = Convert.ToInt32(Request.QueryString.Get("VehicleNo").Trim())
                ParVehID.Value = txtVehicleID.Text
                '----------------------------------------------------------
                parcollection(0) = ParVehID
                Dim blnflag As Boolean
                blnflag = dal.ExecuteStoredProcedureGetBoolean("usp_tt_Vehicle_UnDelete", parcollection)
                If blnflag = False Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Vehicle Un Deleted sucessfully.');location.href='Vehicle.aspx';</script>")
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehicle_Edit.btnDeleteVehicle_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
        '*********************** This event use for delete the record from database**********************************
        'Response.Redirect("Vehicle.aspx", False)
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Try
            Response.Redirect("~/Vehicle.aspx", False)
            Session("Visited") = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.btnCancel_Click", ex)
        End Try
    End Sub

    Protected Sub btnVehicleUses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVehicleUses.Click
        Try
            If btnVehicleUses.Text = "Mileage" Then
                btnVehicleUses.Text = "Hours"
                btnVehicleUses.BackColor = Drawing.Color.FromName("#A5BBC5")
            ElseIf btnVehicleUses.Text = "Hours" Then
                btnVehicleUses.Text = "Mileage"
                btnVehicleUses.BackColor = Drawing.Color.FromName("#A5BBC5")
            End If
            DisableControls()
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.btnVehicleUses_Click", ex)
        End Try
    End Sub

    Protected Sub CboxNolimit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboxNolimit.CheckedChanged
        Try
            If (CboxNolimit.Checked = True) Then
                txtDailylimit.Text = "0"
                txtDailylimit.BackColor = Drawing.Color.LightGray
                txtDailylimit.Enabled = False
            ElseIf (CboxNolimit.Checked = False) Then
                If (txtDailylimit.Text = "") Then
                    txtDailylimit.Text = "0"
                End If
                txtDailylimit.Enabled = True
                txtDailylimit.BackColor = Drawing.Color.White
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.CboxNolimit_CheckedChanged", ex)
        End Try
    End Sub

    Public Shared Sub ShowPopup(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal Parameter As String, ByVal Width As Integer, ByVal Height As Integer)
        Try
            OpenPopUp(opener, Parameter, "Popup", Width, Height)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.ShowPopup()", ex)
        End Try
    End Sub

    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String)
        Try
            Dim clientScript As String
            clientScript = "window.open('" & PagePath & "')"
            opener.Attributes.Add("onClick", clientScript)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.OpenPopUp()", ex)
        End Try
    End Sub

    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String, ByVal windowName As String, ByVal width As Integer, ByVal height As Integer)
        Dim clientScript As String
        Dim windowAttribs As String
        Try
            windowAttribs = "width=" & width & "px," & _
                        "height=" & height & "px," & _
                        "left='+((screen.width -" & width & ") / 2)+'," & _
                        "top='+ (screen.height - " & height & ") / 2+'"
            clientScript = "window.open('" & PagePath & "','" & windowName & "','" & windowAttribs & "');return false;"
            opener.Attributes.Add("onClick", clientScript)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.OpenPopUp()", ex)
        End Try
    End Sub

    Protected Sub btnFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFirst.Click
        Try
            Dim ds = New DataSet()
            Dim dal As New GeneralizedDAL
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehListAndCount")
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("VehicleId")}
            Session("VEHDS") = ds
            Dim cnt As Integer = 0
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = 0
            If (cnt < ds.Tables(0).Rows.Count) Then
                FillTextbox(cnt)
            End If
            Session("currentrecord") = "0"
            lblof.Text = "1 of " & Session("VEHTCnt").ToString()
            btnprevious.Enabled = False
            btnFirst.Enabled = False
            btnNext.Enabled = True
            btnLast.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("VehicleNewEdit_btnFirst_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLast.Click
        Try
            Dim ds = New DataSet()
            Dim dal As New GeneralizedDAL
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehListAndCount")
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("VehicleId")}
            Session("VEHDS") = ds

            Dim cnt As Integer = 0
            cnt = Convert.ToInt32(Session("VEHTCnt").ToString() - 1)
            If (cnt < ds.Tables(0).Rows.Count) Then
                FillTextbox(cnt)
            End If
            lblof.Text = Session("VEHTCnt").ToString() & " of " & Session("VEHTCnt").ToString()
            Session("currentrecord") = cnt
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnprevious.Enabled = True
            btnFirst.Enabled = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("VehicleNewEdit_btnLast_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            Dim ds = New DataSet()
            Dim dal As New GeneralizedDAL
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehListAndCount")
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("VehicleId")}
            Session("VEHDS") = ds
            Dim cnt As Integer = 0
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt + 1
            If (cnt < ds.Tables(0).Rows.Count) Then
                FillTextbox(cnt)
                Session("currentrecord") = cnt
            End If
            lblof.Text = cnt + 1 & " of " & Session("VEHTCnt").ToString()
            If Not btnFirst.Enabled Then
                btnFirst.Enabled = True
                btnprevious.Enabled = True
            End If
            If (cnt + 1 = Convert.ToInt32(Session("VEHTCnt").ToString())) Then
                btnLast.Enabled = False
                btnNext.Enabled = False
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("VehicleNewEdit_btnNext_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnprevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnprevious.Click
        Try
            Dim ds = New DataSet()
            Dim dal As New GeneralizedDAL
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehListAndCount")
            ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("VehicleId")}
            Session("VEHDS") = ds

            Dim cnt As Integer = 0
            cnt = Integer.Parse(Session("currentrecord").ToString())
            cnt = cnt - 1
            If (cnt < ds.Tables(0).Rows.Count And cnt >= 0) Then
                FillTextbox(cnt)
                Session("currentrecord") = cnt
            End If
            lblof.Text = (cnt + 1) & " of " & Session("VEHTCnt").ToString()

            If Not btnLast.Enabled Then
                btnLast.Enabled = True
                btnNext.Enabled = True
            End If
            If (cnt + 1 = 1) Then
                btnFirst.Enabled = False
                btnprevious.Enabled = False
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("VehicleNewEdit_btnprevious_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If lblNew_Edit.Text = "New Vehicle Information" Then
                If txtVehicleID.Text.Trim() <> "" Then
                    If DDLstDepartment.Text <> "" Then
                        If VehicleExists(txtVehicleID.Text.Trim()) = False Then
                            If UCase(txtVId.Value) = "TRUE" Then

                                'Added By Varun Moota to Check CardID. 03/08/2010
                                If Not txtCard.Text = "" Or Nothing Then
                                    If Not CardIDExists(txtCard.Text.Trim()) Then
                                        Session("CondionToRedirect") = "Save"
                                        SaveUpdateRecords()
                                        ClearText()
                                    Else
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Card ID Already Exists.');</script>")
                                    End If
                                Else
                                    Session("CondionToRedirect") = "Save"
                                    SaveUpdateRecords()
                                    ClearText()

                                End If

                            End If
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Vehicle Identity already exist.')</script>")
                        End If
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please select department.');</script>")
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please enter Vehicle Identity.')</script>")
                End If

            ElseIf (lblNew_Edit.Text = "Edit Vehicle Information") Then
                If DDLstDepartment.Text <> "" Then
                    Dim VID As String = Session("VehIDCheck").ToString()
                    If VID = txtVehicleID.Text.Trim() Then
                        EditVehicle()
                    Else
                        If VehicleExists(txtVehicleID.Text.Trim()) = False Then
                            EditVehicle()
                        Else
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Vehicle Identity already exist.')</script>")
                        End If
                    End If

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please select department.');</script>")
                End If
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.btnSave_Click", ex)
        End Try
    End Sub

    Private Sub EditVehicle()
        Try
            'Added By Varun Moota to Check CardID. 03/08/2010
            If Not txtCard.Text = "" Or Nothing Then
                If txtCard.Text.Trim() = Session("CardIDCheck").ToString() Then
                    SaveUpdateRecords()
                Else
                    If Not CardIDExists(txtCard.Text.Trim()) Then
                        SaveUpdateRecords()
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Card ID Already Exists.');</script>")
                    End If
                End If
            Else
                SaveUpdateRecords()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.btnSave_Click", ex)
        End Try
    End Sub

    Private Function VehicleExists(ByVal VehID As String) As Boolean
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParVehID = New SqlParameter("@VehIdentity", SqlDbType.NVarChar, 10)
            ParVehID.Direction = ParameterDirection.Input
            ParVehID.Value = VehID
            parcollection(0) = ParVehID
            'Dim blnflg As Boolean
            'Added By Varun Moota to Check Duplicate VEHICLE ID. 03/08/2010
            Dim strVehIDCheck As String = Nothing
            If Not Session("VehIDCheck") = Nothing Then
                strVehIDCheck = Session("VehIDCheck").ToString.Trim()

                'ClearSession
                'Session("VehIDCheck") = Nothing
            End If

            If txtVehicleID.Text.Trim = strVehIDCheck Then
                Return False
            Else
                VehicleExists = dal.ExecuteStoredProcedureGetBoolean("usp_tt_VehCheckExist", parcollection)
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("VehicleEdit_VehicleExists", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If


        End Try
    End Function

    'Added By Varun Moota to CHECK CARD ID EXISTS. 03/05/2010.
    Private Function CardIDExists(ByVal CardID As String) As Boolean
        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParCardID = New SqlParameter("@CardID", SqlDbType.NVarChar, 10)
            ParCardID.Direction = ParameterDirection.Input
            ParCardID.Value = CardID
            parcollection(0) = ParCardID
            'Dim blnflg As Boolean
            'Added By Varun Moota to Check Duplicate CARD ID. 03/08/2010
            Dim strCardIDCheck As String = Nothing
            If Not Session("CardIDCheck") = Nothing Then
                strCardIDCheck = Session("CardIDCheck").ToString.Trim()

                'ClearSession
                Session("CardIDCheck") = Nothing
            End If
            If txtCard.Text.Trim() = strCardIDCheck Then

                Return False
            Else
                CardIDExists = dal.ExecuteStoredProcedureGetBoolean("usp_tt_VehCardExists", parcollection)
            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("VehicleEdit_VehicleExists", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If


        End Try
    End Function

    Protected Sub btnProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProduct.Click
        Try
            Dim ds = New DataSet()
            Dim dal As New GeneralizedDAL
            Dim i As Integer = 0
            PopUpProduct.Visible = True
            ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehFuelCount")
            If Not ds Is Nothing Then
                If ds.Tables(1).Rows.Count > 0 Then
                    For i = 1 To 15 'ds.Tables(1).Rows.Count - 1
                        Dim btn As CheckBox = CType(FindControl("CheckProd" & (i).ToString), CheckBox)
                        btn.Text = ds.Tables(1).Rows(i - 1)(0).ToString()
                    Next
                End If
            End If
            selectUnCheckProduct()
            If Not btnProduct.Text = "- None -" Then
                Dim strSelected_Product As String = Session("Product").ToString() 'btnProduct.Text.ToString()
                Dim temp As String = ""
                For i = 0 To strSelected_Product.Length - 1 Step 1
                    If (strSelected_Product(i) = ",") Then
                        selectProduct(Convert.ToInt32(temp))
                        temp = ""
                    Else
                        temp += strSelected_Product(i)
                    End If
                Next i
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("btnProduct_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Public Sub selectUnCheckProduct()
        Try


            CheckProd1.Checked = False
            CheckProd2.Checked = False
            CheckProd3.Checked = False
            CheckProd4.Checked = False
            CheckProd5.Checked = False
            CheckProd6.Checked = False
            CheckProd7.Checked = False
            CheckProd8.Checked = False
            CheckProd9.Checked = False
            CheckProd10.Checked = False
            CheckProd11.Checked = False
            CheckProd12.Checked = False
            CheckProd13.Checked = False
            CheckProd14.Checked = False
            CheckProd15.Checked = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.selectUnCheckProduct()", ex)
        End Try
    End Sub

    Public Sub selectProduct(ByVal j As Integer)
        Try


            Select Case j
                Case 1
                    CheckProd1.Checked = True
                Case 2
                    CheckProd2.Checked = True
                Case 3
                    CheckProd3.Checked = True
                Case 4
                    CheckProd4.Checked = True
                Case 5
                    CheckProd5.Checked = True
                Case 6
                    CheckProd6.Checked = True
                Case 7
                    CheckProd7.Checked = True
                Case 8
                    CheckProd8.Checked = True
                Case 9
                    CheckProd9.Checked = True
                Case 10
                    CheckProd10.Checked = True
                Case 11
                    CheckProd11.Checked = True
                Case 12
                    CheckProd12.Checked = True
                Case 13
                    CheckProd13.Checked = True
                Case 14
                    CheckProd14.Checked = True
                Case 15
                    CheckProd15.Checked = True
            End Select
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.selectProduct", ex)
        End Try
    End Sub

    Protected Sub btnEncodeKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEncodeKey.Click
        Try
            EncodeKey()
            Response.Redirect("Key_Encoder.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehicle_Edit.btnEncodeKey_Click", ex)
        End Try
    End Sub

    Public Sub EncodeKey()

        Try
            'Added By Varun Moota. 03/02/2010
            If VehicleExists(txtVehicleID.Text.Trim()) Then


                SaveUpdateRecords()


                Dim ds = New DataSet()
                Dim dal As New GeneralizedDAL
                Dim strKNo As String = ""

                ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehFuelCount")
                If Not ds Is Nothing Then
                    If ds.Tables(2).Rows.Count > 0 Then
                        If Not IsDBNull(ds.Tables(2).Rows(0)("KeyNo")) Then

                            If txtKey.Text = "*NEW*" Then

                                'Added By Varun Moota to Assign a New Unique Key#.02/22/2010

                                Dim dsKeyNum As New DataSet


                                dsKeyNum = dal.GetDataSet("Select MAX(Key_Number) + 1 AS [KEY] FROM VEHS")
                                strKNo = dsKeyNum.Tables(0).Rows(0).Item("KEY").ToString()
                                If strKNo = "" Or Nothing Then
                                    strKNo = "00001"
                                Else

                                    Session("KeyNumber") = strKNo.PadLeft(5, "0")
                                End If
                            Else

                                strKNo = txtKey.Text
                                Session("KeyNumber") = strKNo.PadLeft(5, "0")
                            End If
                        Else

                            strKNo = (Val(ds.Tables(2).Rows(0)("KeyNo")) + 1).ToString()
                            Session("KeyNumber") = strKNo.PadLeft(5, "0")
                        End If

                        'Added By Varun Moota for Lost Key Need to Have New Key#.02/22/2010
                        Dim dsLostKeyNumber As New DataSet

                        dsLostKeyNumber = dal.GetDataSet("Select Key_Number FROM KeyLock WHERE Key_Number = '" + txtKey.Text + "'")

                        If dsLostKeyNumber.Tables(0).Rows.Count > 0 Then
                            dsLostKeyNumber = dal.GetDataSet("Select MAX(Key_Number) + 1 AS [KEY] FROM VEHS")
                            strKNo = dsLostKeyNumber.Tables(0).Rows(0).Item("KEY").ToString()
                            Session("KeyNumber") = strKNo.PadLeft(5, "0")
                        Else
                        End If
                    End If
                Else
                    strKNo = (1).ToString()
                    Session("KeyNumber") = strKNo.PadLeft(5, "0")
                End If
                Session("VEHID") = txtvehicleIdHide.Text.Trim()
                Session("VID") = txtVehicleID.Text.Trim
                'Session("KeyExp") = txtKeyexp.Text.Trim
                'Changed by Harshada
                'To match with WinCC
                '29 Apr 09
                Session("KeyExp") = "NEVER"
                'Session("SysNo") = "1"
                'Changed by Harshada
                'To get system no from status table
                '08 May 09
                Dim sysno As String
                sysno = dal.ExecuteScalarGetString("select sysno from status")
                Session("SysNo") = sysno.Substring(1)

                'Commented By Varun Moota because we are having KEY# issues. 02/18/2010
                ' ''If Not ds Is Nothing Then
                ' ''    If ds.Tables(2).Rows.Count > 0 Then
                ' ''        If Not IsDBNull(ds.Tables(2).Rows(0)("KeyNo")) Then
                ' ''            strKNo = (Val(ds.Tables(2).Rows(0)("KeyNo")) + 1).ToString()
                ' ''            Session("KeyNumber") = strKNo.PadLeft(5, "0")
                ' ''        Else
                ' ''            strKNo = (1).ToString()
                ' ''            Session("KeyNumber") = strKNo.PadLeft(5, "0")
                ' ''        End If
                ' ''    End If
                ' ''Else
                ' ''    strKNo = (1).ToString()
                ' ''    Session("KeyNumber") = strKNo.PadLeft(5, "0")
                ' ''End If

                If CboxFuelercard.Checked = True Then
                    Session("Master") = "Yes"
                ElseIf CboxFuelercard.Checked = False Then
                    Session("Master") = "No"
                End If
                If CboxCheckmiles.Checked = True Then
                    Session("MileageReq") = "Yes"
                ElseIf CboxCheckmiles.Checked = False Then
                    Session("MileageReq") = "No"
                End If
                If CboxFacilitycard.Checked = True Then
                    Session("Option") = "Yes"
                ElseIf CboxFacilitycard.Checked = False Then
                    Session("Option") = "No"
                End If
                If CboxSecondkey.Checked = True Then
                    Session("Secondkey") = "Yes"
                ElseIf CboxSecondkey.Checked = False Then
                    Session("Secondkey") = "No"
                End If
                'Session("FLimit") = txtFuellimit.Text.Trim 'DDLFuelLimit.Text =
                Session("FLimit") = DDLFuelLimit.Text.Trim
                Session("Mileage") = txtCOdometer.Text.Trim 'txtMileage.Text.Trim
                Session("KeyType") = 0
                Dim p As Array
                Dim fuel As String = ""
                Dim i, j As Integer
                p = txtprodval1.Value.Split(",")
                j = 0
                For i = 1 To 15
                    If p(j).ToString() = i.ToString() Then
                        fuel = fuel + "1"
                        j = j + 1
                    Else
                        fuel = fuel + "0"
                    End If
                Next

                Session("Prods") = fuel

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Vehicle Identity already exist.')</script>")
            End If


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Veh_Edit.EncodeKey", ex.InnerException)
        End Try

    End Sub

    Public Sub SaveUpdateRecords()
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            Dim parcollection(48) As SqlParameter

            Dim ParVehicleId = New SqlParameter("@VehicleId", SqlDbType.Int)
            Dim ParIDENTITY = New SqlParameter("@IDENTITY", SqlDbType.VarChar, 10)
            Dim ParLICNO = New SqlParameter("@LICNO", SqlDbType.VarChar, 9)
            Dim ParEXTENSION = New SqlParameter("@EXTENSION", SqlDbType.VarChar, 50)
            'Edit by Omar to fix parameter name from KEY to KEY_NUMBER as expected by the stored procedure
            'Dim ParKEY = New SqlParameter("@KEY", SqlDbType.VarChar, 5)
            Dim ParKEY = New SqlParameter("@KEY_NUMBER", SqlDbType.VarChar, 5)
            Dim ParKEYEXP = New SqlParameter("@KEYEXP", SqlDbType.VarChar, 7)
            Dim ParCARD = New SqlParameter("@CARD", SqlDbType.Char, 20)
            Dim ParCARDEXP = New SqlParameter("@CARDEXP", SqlDbType.VarChar, 7)
            Dim ParYEAR = New SqlParameter("@YEAR", SqlDbType.VarChar, 4)
            Dim ParMAKE = New SqlParameter("@MAKE", SqlDbType.VarChar, 20)
            Dim ParMODEL = New SqlParameter("@MODEL", SqlDbType.VarChar, 20)
            Dim ParTYPE = New SqlParameter("@TYPE", SqlDbType.VarChar, 20)
            Dim ParVEHVIN = New SqlParameter("@VEHVIN", SqlDbType.VarChar, 20)
            Dim ParDEPT = New SqlParameter("@DEPT", SqlDbType.VarChar, 3)
            Dim ParSUBDEPT = New SqlParameter("@SUBDEPT", SqlDbType.VarChar, 20)
            Dim ParCURRHRS = New SqlParameter("@CURRHRS", SqlDbType.Float)
            Dim ParLIMIT = New SqlParameter("@LIMIT", SqlDbType.VarChar, 3)
            Dim ParTXTLIMIT = New SqlParameter("@TXTLIMIT", SqlDbType.VarChar, 50)
            Dim ParACCTID = New SqlParameter("@ACCTID", SqlDbType.VarChar, 20)
            Dim ParCODE = New SqlParameter("@CODE", SqlDbType.VarChar, 25)
            Dim ParSECOND_KEY = New SqlParameter("@SECOND_KEY", SqlDbType.Bit)
            Dim ParREQODOM = New SqlParameter("@REQODOM", SqlDbType.Bit)
            Dim ParMASTER = New SqlParameter("@MASTER", SqlDbType.Bit)
            Dim ParMHFLAG = New SqlParameter("@MHFLAG", SqlDbType.VarChar, 50)
            Dim ParLOCKED = New SqlParameter("@LOCKED", SqlDbType.Bit)
            Dim ParOPTION1 = New SqlParameter("@OPTION1", SqlDbType.Bit)
            Dim ParOPTION2 = New SqlParameter("@OPTION2", SqlDbType.Bit)
            Dim ParINVENTORY = New SqlParameter("@INVENTORY", SqlDbType.Bit)
            Dim ParBULK = New SqlParameter("@BULK", SqlDbType.Bit)
            Dim ParBUS = New SqlParameter("@BUS", SqlDbType.Bit)
            Dim ParLASTFUELER = New SqlParameter("@LASTFUELER", SqlDbType.VarChar, 10)
            Dim ParFUELS = New SqlParameter("@FUELS", SqlDbType.VarChar, 15)
            Dim ParCOMENT = New SqlParameter("@COMENT", SqlDbType.Text)
            Dim ParLASTUPDT = New SqlParameter("@LASTUPDT", SqlDbType.DateTime)
            Dim ParNEWUPDT = New SqlParameter("@NEWUPDT", SqlDbType.Int)
            Dim ParMILEAGE = New SqlParameter("@MILEAGE", SqlDbType.Bit)
            Dim ParMILESWIND = New SqlParameter("@MILESWIND", SqlDbType.NVarChar, 10)
            Dim ParCURRMILES = New SqlParameter("@CURRMILES", SqlDbType.Float)
            Dim ParAddEdit = New SqlParameter("@AddEdit", SqlDbType.VarChar, 5)
            Dim ParNxtPmOdo = New SqlParameter("@NxtPmOdo", SqlDbType.Int)
            Dim ParPmIntr = New SqlParameter("@PmIntr", SqlDbType.Int)
            Dim ParPmTxtn = New SqlParameter("@PmTxtn", SqlDbType.Int)
            Dim ParTagId = New SqlParameter("@TagID", SqlDbType.VarChar, 16)

            'Added By Varun Moota, new Items Vehicle Messages.05/17/2011
            Dim ParVehMsgCnt = New SqlParameter("@VehMsgCnt", SqlDbType.Int)
            Dim ParVehMsg1 = New SqlParameter("@VehMsg1", SqlDbType.NVarChar, 25)
            Dim ParVehMsg2 = New SqlParameter("@VehMsg2", SqlDbType.NVarChar, 25)
            Dim ParVehMsgExpDT = New SqlParameter("@VehMsgExpDT", SqlDbType.DateTime)

            'By Soham Gangavane Aug 28, 2017
            Dim ParPREVMILES = New SqlParameter("@PREVMILES", SqlDbType.Float)
            Dim ParPREVHOURS = New SqlParameter("@PREVHOURS", SqlDbType.Float)

            ParVehicleId.Direction = ParameterDirection.Input
            ParIDENTITY.Direction = ParameterDirection.Input
            ParLICNO.Direction = ParameterDirection.Input
            ParEXTENSION.Direction = ParameterDirection.Input
            ParKEY.Direction = ParameterDirection.Input
            ParKEYEXP.Direction = ParameterDirection.Input
            ParCARD.Direction = ParameterDirection.Input
            ParCARDEXP.Direction = ParameterDirection.Input
            ParYEAR.Direction = ParameterDirection.Input
            ParMAKE.Direction = ParameterDirection.Input
            ParMODEL.Direction = ParameterDirection.Input
            ParTYPE.Direction = ParameterDirection.Input
            ParVEHVIN.Direction = ParameterDirection.Input
            ParDEPT.Direction = ParameterDirection.Input
            ParSUBDEPT.Direction = ParameterDirection.Input
            ParCURRHRS.Direction = ParameterDirection.Input
            ParLIMIT.Direction = ParameterDirection.Input
            ParTXTLIMIT.Direction = ParameterDirection.Input
            ParACCTID.Direction = ParameterDirection.Input
            ParCODE.Direction = ParameterDirection.Input
            ParSECOND_KEY.Direction = ParameterDirection.Input
            ParREQODOM.Direction = ParameterDirection.Input
            ParMASTER.Direction = ParameterDirection.Input
            ParMHFLAG.Direction = ParameterDirection.Input
            ParLOCKED.Direction = ParameterDirection.Input
            ParOPTION1.Direction = ParameterDirection.Input
            ParOPTION2.Direction = ParameterDirection.Input
            ParINVENTORY.Direction = ParameterDirection.Input
            ParBULK.Direction = ParameterDirection.Input
            ParBUS.Direction = ParameterDirection.Input
            ParLASTFUELER.Direction = ParameterDirection.Input
            ParFUELS.Direction = ParameterDirection.Input
            ParCOMENT.Direction = ParameterDirection.Input
            ParLASTUPDT.Direction = ParameterDirection.Input
            ParNEWUPDT.Direction = ParameterDirection.Input
            ParAddEdit.Direction = ParameterDirection.Input
            ParMILEAGE.Direction = ParameterDirection.Input
            ParMILESWIND.Direction = ParameterDirection.Input
            ParCURRMILES.Direction = ParameterDirection.Input
            ParNxtPmOdo.Direction = ParameterDirection.Input
            ParPmIntr.Direction = ParameterDirection.Input
            ParPmTxtn.Direction = ParameterDirection.Input
            ParTagId.Direction = ParameterDirection.Input

            ParVehMsgCnt.Direction = ParameterDirection.Input
            ParVehMsg1.Direction = ParameterDirection.Input
            ParVehMsg2.Direction = ParameterDirection.Input
            ParVehMsgExpDT.Direction = ParameterDirection.Input

            'By Soham Gangavane Aug 28, 2017
            ParPREVMILES.Direction = ParameterDirection.Input
            ParPREVHOURS.Direction = ParameterDirection.Input


            If (lblNew_Edit.Text = "Edit Vehicle Information") Then
                ParAddEdit.Value = "Edit"
                ParVehicleId.Value = Convert.ToInt32(txtvehicleIdHide.Text.Trim())
                ParNEWUPDT.value = 2
            ElseIf lblNew_Edit.Text = "New Vehicle Information" Then
                ParAddEdit.Value = "ADD"
                ParVehicleId.Value = 0
                ParNEWUPDT.value = 1
            End If

            ParIDENTITY.value = txtVehicleID.Text.Trim()
            Session("VehIDCheck") = txtVehicleID.Text.Trim()
            ParLICNO.Value = txtLicense.Text.Trim()
            ParEXTENSION.Value = txtDescription.Text.Trim()
            ParKEY.Value = txtKey.Text.Trim()
            ParKEYEXP.Value = txtKeyexp.Text.Trim()
            ParCARD.Value = txtCard.Text.Trim()
            ParCARDEXP.Value = txtCardexp.Text.Trim()
            ParYEAR.Value = txtYear.Text.Trim()
            ParMAKE.Value = txtMake.Text.Trim()
            ParMODEL.Value = txtModel.Text.Trim()
            ParTYPE.Value = txtType.Text.Trim()
            ParVEHVIN.Value = txtVinNumber.Text.Trim()
            ParDEPT.Value = DDLstDepartment.SelectedItem.Value.Trim()
            ParSUBDEPT.Value = txtSubdepartment.Text.Trim()
            ParCOMENT.Value = txtComments.Text.Trim()
            If (btnVehicleUses.Text = "Hours") Then
                ParMILEAGE.Value = False
            ElseIf (btnVehicleUses.Text = "Mileage") Then
                ParMILEAGE.Value = True
            End If
            ParMILESWIND.Value = txtMileage.Text
            ParCURRMILES.Value = Convert.ToInt32(Val(txtCOdometer.Text))
            ParCURRHRS.Value = Val(txtCHours.Text)

            'By Soham Gangavane Aug 28, 2017
            ParPREVMILES.Value = Convert.ToInt32(Val(txtPOdometer.Text))
            ParPREVHOURS.Value = Convert.ToInt32(Val(txtPHours.Text))


            'ParLIMIT.Value = txtFuellimit.Text 'DDLFuelLimit
            ParLIMIT.Value = DDLFuelLimit.Text
            If ((CboxNolimit.Checked = True) And (txtDailylimit.Text = "" Or txtDailylimit.Text = "0")) Then
                ParTXTLIMIT.Value = ""
            ElseIf (CboxNolimit.Checked = False) And (txtDailylimit.Text <> "") Then
                ParTXTLIMIT.Value = txtDailylimit.Text
            End If
            ParACCTID.Value = txtAccid.Text
            ParCODE.Value = txtExportcode.Text
            ParSECOND_KEY.Value = CboxSecondkey.Checked.ToString()
            ParREQODOM.Value = CboxCheckmiles.Checked.ToString()
            If CboxFuelercard.Checked = True Then mhflag = "M"
            ParMASTER.Value = CboxFuelercard.Checked
            ParMHFLAG.Value = mhflag
            ParLOCKED.Value = CboxDisabled.Checked
            ParOPTION1.Value = CboxFacilitycard.Checked
            If lblNew_Edit.Text = "New Vehicle Information" Then
                ParOPTION2.Value = False
                ParINVENTORY.Value = False
                ParBULK.Value = False
                ParBUS.Value = False
            Else
                ParOPTION2.Value = True
                ParINVENTORY.Value = True
                ParBULK.Value = True
                ParBUS.Value = True
            End If
            If (txtLastfueler.Text = "No Info") Then ParLASTFUELER.Value = "" Else ParLASTFUELER.Value = txtLastfueler.Text
            ParLASTUPDT.Value = DateTime.Now

            Dim p As Array
            Dim fuel As String = ""
            Dim i, j As Integer
            p = txtprodval1.Value.Split(",")
            j = 0
            For i = 1 To 15
                If p(j).ToString() = i.ToString() Then
                    fuel = fuel + "Y"
                    j = j + 1
                Else
                    fuel = fuel + "N"
                End If
            Next i
            ParFUELS.Value = fuel
            ParNxtPmOdo.Value = Val(txtNxtPmOdo.Text)
            ParPmIntr.Value = Val(txtPmIntr.Text)
            ParPmTxtn.Value = Val(txtPmTxtn.Text)
            ParTagId.Value = txtTagID.Text.Trim()

            If (txtVehMsgCnt.Text <> "") Then
                ParVehMsgCnt.value = CInt(txtVehMsgCnt.Text.ToString)
            Else
                ParVehMsgCnt.value = 0
            End If
            ParVehMsg1.value = txtVehMsg1.Text.ToString()
            ParVehMsg2.value = txtVehMsg2.Text.ToString()
            If txtVMExpDT.Text.ToString() = "" Then
                ParVehMsgExpDT.value = DBNull.Value
            Else
                ParVehMsgExpDT.value = txtVMExpDT.Text.ToString()
            End If
            parcollection(0) = ParVehicleId
            parcollection(1) = ParIDENTITY
            parcollection(2) = ParLICNO
            parcollection(3) = ParEXTENSION
            parcollection(4) = ParKEY
            parcollection(5) = ParKEYEXP
            parcollection(6) = ParCARD
            parcollection(7) = ParCARDEXP
            parcollection(8) = ParYEAR
            parcollection(9) = ParMAKE
            parcollection(10) = ParMODEL
            parcollection(11) = ParTYPE
            parcollection(12) = ParVEHVIN
            parcollection(13) = ParDEPT
            parcollection(14) = ParSUBDEPT
            parcollection(15) = ParCURRHRS 'numeric(6, 0)
            parcollection(16) = ParLIMIT
            parcollection(17) = ParTXTLIMIT
            parcollection(18) = ParACCTID
            parcollection(19) = ParCODE
            parcollection(20) = ParSECOND_KEY
            parcollection(21) = ParREQODOM
            parcollection(22) = ParMASTER
            parcollection(23) = ParMHFLAG
            parcollection(24) = ParLOCKED
            parcollection(25) = ParOPTION1
            parcollection(26) = ParOPTION2
            parcollection(27) = ParINVENTORY
            parcollection(28) = ParBULK
            parcollection(29) = ParBUS
            parcollection(30) = ParLASTFUELER
            parcollection(31) = ParFUELS
            parcollection(32) = ParCOMENT
            parcollection(33) = ParLASTUPDT
            parcollection(34) = ParNEWUPDT 'numeric(6, 0)
            parcollection(35) = ParMILEAGE
            parcollection(36) = ParMILESWIND
            parcollection(37) = ParCURRMILES 'numeric(6, 0)
            parcollection(38) = ParAddEdit
            parcollection(39) = ParNxtPmOdo
            parcollection(40) = ParPmIntr
            parcollection(41) = ParPmTxtn
            parcollection(42) = ParTagId
            parcollection(43) = ParVehMsgCnt
            parcollection(44) = ParVehMsg1
            parcollection(45) = ParVehMsg2
            parcollection(46) = ParVehMsgExpDT
            'By Soham Gangavane Aug 28, 2017
            parcollection(47) = ParPREVMILES
            parcollection(48) = ParPREVHOURS

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_VehInsertUpdate", parcollection)
            If blnFlag = True Then
                If (lblNew_Edit.Text = "Edit Vehicle Information") Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record updated successfully');</script>")
                ElseIf lblNew_Edit.Text = "New Vehicle Information" Then
                    If (Session("CondionToRedirect").ToString() = "Add Another") Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record saved successfully');location.href='Vehicle_Edit.aspx';</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record saved successfully');location.href='Vehicle.aspx';</script>")
                    End If
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record save successfully');location.href='Vehicle_Edit.aspx';</script>")
                    End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record not updated successfully');location.href='Vehicle.aspx';</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehicle_new_edit.SaveUpdateRecords", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Record not Saved.');</script>")
        End Try
    End Sub

    Protected Sub btnPM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPM.Click
        Try
            ' ''Dim ds = New DataSet()
            ' ''Dim dal As New GeneralizedDAL
            ' ''Dim parcollection(0) As SqlParameter

            ' ''Dim ParIDENTITY = New SqlParameter("@VehIDENTITY", SqlDbType.VarChar, 10)
            ' ''ParIDENTITY.Direction = ParameterDirection.Input
            ' ''ParIDENTITY.value = txtVehicleID.Text
            ' ''parcollection(0) = ParIDENTITY
            ' ''Dim i As Integer = 0
            ' ''ds = dal.ExecuteStoredProcedureGetDataSet("usp_tt_VehPMPopup", parcollection)
            ' ''If Not ds Is Nothing Then
            ' ''    If ds.Tables(0).Rows.Count > 0 Then
            ' ''        txtCurrOdomtr.Text = txtCOdometer.Text 'ds.Tables(0).Rows(i)(0).ToString()
            ' ''    Else
            ' ''        txtCurrOdomtr.Text = txtCOdometer.Text
            ' ''    End If
            ' ''    If ds.Tables(1).Rows.Count > 0 Then
            ' ''        For i = 0 To ds.Tables(1).Rows.Count - 1
            ' ''            txtNxtPmOdo.Text = ds.Tables(1).Rows(i)(0).ToString()
            ' ''            txtPmIntr.Text = ds.Tables(1).Rows(i)(1).ToString()
            ' ''            txtPmTxtn.Text = ds.Tables(1).Rows(i)(2).ToString()
            ' ''        Next
            ' ''    End If
            ' ''End If

            'Added By Varun Moota, since it needs a static Current odometer.
            txtCurrOdomtr.Text = txtCOdometer.Text.ToString()
            PopPM.Visible = True



        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehcile_edit.btnPM_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click

        Try
            PopPM.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnClose_Click", ex)
        End Try
    End Sub

    Protected Sub btnDis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDis.Click

        Try
            txtPmTxtn.Text = "0"
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnDis_Click", ex)
        End Try
    End Sub

    Protected Sub btnPMOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPMOK.Click

        Try
            PopPM.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnPMOK_Click", ex)
        End Try
    End Sub

    Protected Sub btnProdClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProdClose.Click
        Try
            PopUpProduct.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnProdClose_Click", ex)
        End Try
    End Sub

    Protected Sub btnProdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProdOK.Click
        Try


            Dim strProduct As String = ""
            Session("Product") = ""
            If CheckProd1.Checked Then
                strProduct = strProduct & "1"
                Session("Product") = "1,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd2.Checked Then
                strProduct = strProduct & "2"
                Session("Product") += "2,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd3.Checked Then
                strProduct = strProduct & "3"
                Session("Product") += "3,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd4.Checked Then
                strProduct = strProduct & "4"
                Session("Product") += "4,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd5.Checked Then
                strProduct = strProduct & "5"
                Session("Product") += "5,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd6.Checked Then
                strProduct = strProduct & "6"
                Session("Product") += "6,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd7.Checked Then
                strProduct = strProduct & "7"
                Session("Product") += "7,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd8.Checked Then
                strProduct = strProduct & "8"
                Session("Product") += "8,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd9.Checked Then
                strProduct = strProduct & "9"
                Session("Product") += "9,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd10.Checked Then
                strProduct = strProduct & "10"
                Session("Product") += "10,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd11.Checked Then
                strProduct = strProduct & "11"
                Session("Product") += "11,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd12.Checked Then
                strProduct = strProduct & "12"
                Session("Product") += "12,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd13.Checked Then
                strProduct = strProduct & "13"
                Session("Product") += "13,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd14.Checked Then
                strProduct = strProduct & "14"
                Session("Product") += "14,"
            Else
                strProduct = strProduct & " "
            End If

            If CheckProd15.Checked Then
                strProduct = strProduct & "15"
                Session("Product") += "15,"
            Else
                strProduct = strProduct & " "
            End If
            btnProduct.Text = strProduct
            txtprodval1.Value = Session("Product")
            PopUpProduct.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnProdOK_Click", ex)
        End Try
    End Sub

    Protected Sub btnLostKey_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLostKey.ServerClick
        Try
            If txtLost.Value = "true" Then

                SaveLockDetails("K")
                'LostKeyUpdate()

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnLostKey_ServerClick", ex)
        End Try

    End Sub

    Private Sub SaveLockDetails(ByVal LockType As String)
        Try
            Dim blnFlag As Boolean
            Dim dal = New GeneralizedDAL()
            Dim parcollection(7) As SqlParameter
            Dim ParVehicleId = New SqlParameter("@VehicleId", SqlDbType.Int)
            Dim ParIDENTITY = New SqlParameter("@IDENTITY", SqlDbType.VarChar, 10)
            Dim ParACCT_ID = New SqlParameter("@ACCT_ID", SqlDbType.VarChar, 11)
            Dim ParKEY_NUMBER = New SqlParameter("@KEY_NUMBER", SqlDbType.VarChar, 5)
            Dim ParCARD_ID = New SqlParameter("@CARD_ID", SqlDbType.VarChar, 7)
            Dim ParKEY_EXP = New SqlParameter("@KEY_EXP", SqlDbType.VarChar, 50)
            Dim ParCARD_EXP = New SqlParameter("@CARD_EXP", SqlDbType.VarChar, 50)
            Dim ParFlag = New SqlParameter("@Flag", SqlDbType.VarChar, 5)

            ParVehicleId.Direction = ParameterDirection.Input
            ParIDENTITY.Direction = ParameterDirection.Input
            ParACCT_ID.Direction = ParameterDirection.Input
            ParKEY_NUMBER.Direction = ParameterDirection.Input
            ParCARD_ID.Direction = ParameterDirection.Input
            ParKEY_EXP.Direction = ParameterDirection.Input
            ParCARD_EXP.Direction = ParameterDirection.Input
            ParFlag.Direction = ParameterDirection.Input

            ParVehicleId.Value = Convert.ToInt32(txtvehicleIdHide.Text.Trim())
            ParFlag.Value = LockType
            ParIDENTITY.Value = txtVehicleID.Text.Trim()
            ParACCT_ID.Value = txtAccid.Text.Trim()
            ParKEY_NUMBER.Value = txtKey.Text.Trim()
            ParCARD_ID.Value = txtCard.Text.Trim()
            ParKEY_EXP.Value = txtKeyexp.Text.Trim()
            ParCARD_EXP.Value = txtCardexp.Text.Trim()
            parcollection(0) = ParVehicleId
            parcollection(1) = ParIDENTITY
            parcollection(2) = ParACCT_ID
            parcollection(3) = ParKEY_NUMBER
            parcollection(4) = ParCARD_ID
            parcollection(5) = ParKEY_EXP
            parcollection(6) = ParCARD_EXP
            parcollection(7) = ParFlag

            blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("usp_tt_VeHLockCardKey", parcollection)
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>location.href='Vehicle.aspx';</script>")
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehcile_Edit.SaveLockDetails", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Protected Sub btnLostCard_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLostCard.ServerClick
        Try
            If txtLost.Value = "true" Then
                SaveLockDetails("C")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnLostCard_ServerClick", ex)
        End Try

    End Sub

    Public Shared Function ConvertToUnixTimestamp(ByVal datestamp As DateTime) As Double
        Try
            Dim origin As DateTime = New DateTime(1970, 1, 1, 0, 0, 0, 0)
            Dim diff As TimeSpan = datestamp - origin
            Return Math.Floor(diff.TotalSeconds)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.ConvertToUnixTimestamp", ex)
            Return 0.0
        End Try

    End Function

    Public Function Encrypt() As String

        Try
            ' Convert our plaintext into a byte array.
            ' Let us assume that plaintext contains UTF8-encoded characters.

            Dim timeStamp As String = Convert.ToString(ConvertToUnixTimestamp(DateTime.Today))

            Dim vehID As String = GetVehPathSerial(txtVehicleID.Text)
            Dim plainText As String = "serial=" + vehID + "&time=" + timeStamp '"serial=6002276&time=" + timeStamp
            Dim plainTextBytes() As Byte = Encoding.UTF8.GetBytes(plainText)


            Dim key As String = "ee893f723abbd539"
            Dim keyBytes() As Byte = ASCIIEncoding.UTF8.GetBytes(key)

            Dim IV As String = "37978ace827d2f18"
            Dim IVBytes() As Byte = ASCIIEncoding.UTF8.GetBytes(IV)
            ' Create uninitialized Rijndael encryption object.
            'RijndaelManaged symmetricKey = new RijndaelManaged();

            Dim symmetricKey As Rijndael = Rijndael.Create()

            symmetricKey.Padding = PaddingMode.Zeros

            ' It is reasonable to set encryption mode to Cipher Block Chaining
            ' (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC

            ' Generate encryptor from the existing key bytes and initialization 
            ' vector. Key size will be defined based on the number of the key 
            ' bytes.
            Dim encryptor As ICryptoTransform

            encryptor = symmetricKey.CreateEncryptor(keyBytes, IVBytes)

            ' Define memory stream which will be used to hold encrypted data.
            Dim memoryStream As MemoryStream = New MemoryStream()

            ' Define cryptographic stream (always use Write mode for encryption).
            Dim cryptoStream As CryptoStream = New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
            ' Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

            ' Finish encrypting.
            cryptoStream.FlushFinalBlock()

            ' Convert our encrypted data from a memory stream into a byte array.
            Dim cipherTextBytes() As Byte = memoryStream.ToArray()

            ' Close both streams.
            memoryStream.Close()
            cryptoStream.Close()

            ' Convert encrypted data into a base64-encoded string.
            'string cipherText = Convert.ToBase64String(cipherTextBytes);
            BitConverter.ToString(cipherTextBytes)
            Dim cipherText As String = BitConverter.ToString(cipherTextBytes)

            ' Return encrypted string.
            Return cipherText

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehicle.Encrypt", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return String.Empty
        End Try
    End Function


    'Protected Sub btnVehPath_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVehPath.Click
    '    Try
    '        Dim lcPostData As String = Encrypt()
    '        lcPostData = lcPostData.ToLower().Replace("-", "").Trim()


    '        Dim lcUrl As String = "http://map4.gpsservicenetwork.com/remote_access.api?trackeng=" + lcPostData

    '        Dim sb As StringBuilder = New StringBuilder()
    '        sb.Append("<script>")
    '        sb.Append("window.open('" + lcUrl + "', '', '');")
    '        sb.Append("</script>")

    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "test", sb.ToString())

    '    Catch ex As Exception
    '        Dim cr As New ErrorPage
    '        Dim errmsg As String
    '        cr.errorlog("Vehicle_Edit.btnVehPath_Click", ex)
    '        If ex.Message.Contains(";") Then
    '            errmsg = ex.Message.ToString()
    '            errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
    '        Else
    '            errmsg = ex.Message.ToString()
    '        End If
    '    End Try


    'End Sub

    Public Function GetVehPathSerial(ByVal VehicleID As String) As String

        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParVehicleID = New SqlParameter("@VehicleID", SqlDbType.NVarChar, 10)
            ParVehicleID.Direction = ParameterDirection.Input
            ParVehicleID.Value = VehicleID
            parcollection(0) = ParVehicleID

            Dim ds As New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("use_tt_getVehPathSerial", parcollection)

            If ds.Tables(0).Rows.Count > 0 Then
                VehicleID = ds.Tables(0).Rows(0)("SERIALNUMBER").ToString()
                Return VehicleID

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Not Assigned to Vehicle Path GPS System.');location.href='Vehicle.aspx';</script>")
                Return String.Empty

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("GetVehPathSerial", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return String.Empty
        End Try

    End Function

    Public Function GetVehGPSID(ByVal VehicleID As String) As String

        Dim dal = New GeneralizedDAL()
        Try
            Dim parcollection(0) As SqlParameter
            Dim ParVehicleID = New SqlParameter("@VehicleID", SqlDbType.NVarChar, 10)
            ParVehicleID.Direction = ParameterDirection.Input
            ParVehicleID.Value = VehicleID
            parcollection(0) = ParVehicleID

            Dim ds As New DataSet()
            ds = dal.ExecuteStoredProcedureGetDataSet("use_tt_getVehPathSerial", parcollection)

            If ds.Tables(0).Rows.Count > 0 Then
                VehicleID = ds.Tables(0).Rows(0)("SERIALNUMBER").ToString()
                Return VehicleID

            Else
                Return String.Empty

            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehcile_Edit.GetVehGPSID", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            Return String.Empty
        End Try

    End Function

    Protected Sub btnMessage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMessage.Click
        Try
            PopUpVehMsg.Visible = True
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnMessage_Click", ex)
        End Try
    End Sub

    Protected Sub btnVehMsgSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVehMsgSave.Click
        Try
            PopUpVehMsg.Visible = False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnVehMsgSave_Click", ex)
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

    Protected Sub btnVehMsgDis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVehMsgDis.Click
        Try
            txtVehMsgCnt.Text = "0"
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.btnVehMsgDis_Click", ex)
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
            cr.errorlog("Vehcile_Edit.btnCustomMsg_Click", ex)
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
            txtVehMsg1.Text = txtVehMsg1.Text.ToString() + " " + strVehMsgs.ToString()
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
            cr.errorlog("Vehcile_Edit.btnAddItems_Click", ex)
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
            cr.errorlog("Vehcile_Edit.btnRemoveItems_Click", ex)
        End Try
    End Sub
    'Added By Varun Moota, to remove Dupes in Datatable.10/17/2011
    Public Function RemoveDuplicateRows(ByVal dTable As DataTable, ByVal colName As String) As DataTable
        Try
            Dim hTable As Hashtable = New Hashtable()
            Dim duplicateList As ArrayList = New ArrayList()

            Dim drow As DataRow
            For Each drow In dTable.Rows
                If hTable.Contains(drow(colName)) Then
                    duplicateList.Add(drow)
                Else
                    hTable.Add(drow(colName), String.Empty)
                End If
            Next


            For Each drow In duplicateList
                dTable.Rows.Remove(drow)
            Next

            Return dTable
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Vehcile_Edit.RemoveDuplicateRows", ex)
            Return Nothing
        End Try

    End Function

    ' ''Added By varun to Send Inst veh Messages.
    ' ''Private Sub UpdateInstVehMsgs(ByVal VehID As String, ByVal Msg As String)
    ' ''    Try
    ' ''        Dim dal = New GeneralizedDAL()
    ' ''        Dim blnFlag As Boolean


    ' ''        Dim qry As String = "SELECT ISNULL(MESS_COUNT,0) AS CNT FROM VEHS WHERE [IDENTITY]= '" + VehID.ToString + "'"
    ' ''        Dim dsVehMsgCounter As New DataSet
    ' ''        dsVehMsgCounter = dal.GetDataSet(qry)
    ' ''        If Not IsDBNull(dsVehMsgCounter) Then
    ' ''            If CInt(dsVehMsgCounter.Tables(0).Rows(0)(0).ToString) > 0 Then
    ' ''                Dim parcollection(2) As SqlParameter
    ' ''                Dim ParVehicleId = New SqlParameter("@VehicleId", SqlDbType.VarChar, 10)
    ' ''                Dim ParVehMsg = New SqlParameter("@VehMsg", SqlDbType.VarChar, 250)
    ' ''                Dim ParPolled = New SqlParameter("@Polled", SqlDbType.Bit)

    ' ''                ParVehicleId.Direction = ParameterDirection.Input
    ' ''                ParVehMsg.Direction = ParameterDirection.Input
    ' ''                ParPolled.Direction = ParameterDirection.Input

    ' ''                ParVehicleId.value = VehID.ToString()
    ' ''                ParVehMsg.Value = Msg.ToString()
    ' ''                ParPolled.Value = "False"

    ' ''                parcollection(0) = ParVehicleId
    ' ''                parcollection(1) = ParVehMsg
    ' ''                parcollection(2) = ParPolled

    ' ''                blnFlag = dal.ExecuteSQLStoredProcedureGetBoolean("USP_TT_INSERT_VehicleInstMsgs", parcollection)
    ' ''            End If

    ' ''        End If
    ' ''    Catch ex As Exception
    ' ''        Dim cr As New ErrorPage
    ' ''        cr.errorlog("Vehcile_Edit.UpdateInstVehMsgs()", ex)
    ' ''    End Try

    ' ''End Sub

    Protected Sub btnVehPath_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVehPath.Click
        Try
            Dim lcPostData As String = Encrypt()
            lcPostData = lcPostData.ToLower().Replace("-", "").Trim()


            Dim lcUrl As String = "http://map4.gpsservicenetwork.com/remote_access.api?trackeng=" + lcPostData

            Dim sb As StringBuilder = New StringBuilder()
            sb.Append("<script>")
            sb.Append("window.open('" + lcUrl + "', '', '');")
            sb.Append("</script>")

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "test", sb.ToString())

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Vehicle_Edit.btnVehPath_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Sub

    Protected Sub txtLicense_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLicense.TextChanged

    End Sub

End Class
