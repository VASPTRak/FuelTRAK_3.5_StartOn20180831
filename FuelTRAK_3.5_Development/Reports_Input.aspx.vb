Imports System.Data.DataSet
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Partial Class Reports_Input
    Inherits System.Web.UI.Page
    'Jatin Kshirsagar as on 26 Aug 2008
    Dim str_Qry As String
    Dim GenFun As GeneralFunctions
    Dim Uinfo As UserInfo
    Dim DAL As GeneralizedDAL
    '----------------------------------
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        GenFun = New GeneralFunctions
        Uinfo = New UserInfo
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                Me.Form.DefaultButton = "RunReportButton"
                StartDateTextBox.Attributes.Add("onkeyup", "KeyUpEvent_StartDateTextBox(event);")
                EndDateTextBox.Attributes.Add("onkeyup", "KeyUpEvent_EndDateTextBox(event);")
                StartDateTextBox.Attributes.Add("OnKeyPress", "AllowNumeric('StartDateTextBox');")
                EndDateTextBox.Attributes.Add("OnKeyPress", "AllowNumeric('EndDateTextBox');")

                StartTimeTextBox.Attributes.Add("onkeyup", "KeyUpEvent_txtTime(event,'StartTimeTextBox');")
                EndTimeTextBox.Attributes.Add("onkeyup", "KeyUpEvent_txtTime(event,'EndTimeTextBox');")
                StartTimeTextBox.Attributes.Add("OnKeyPress", "AllowNumeric('StartTimeTextBox');")
                EndTimeTextBox.Attributes.Add("OnKeyPress", "AllowNumeric('EndTimeTextBox');")
                Uinfo = Session("Uinfo")
                If Not IsPostBack Then
                    StartDateTextBox.Text = Format(Month(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Format(Year(DateAdd(DateInterval.Day, -1, Today)), "0000")
                    EndDateTextBox.Text = Format(Month(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Format(Day(DateAdd(DateInterval.Day, -1, Today)), "00") & "/" & Format(Year(DateAdd(DateInterval.Day, -1, Today)), "0000")
                    StartDateTextBox.Focus()
                End If
                txtReportID.Value = Uinfo.ReportID.ToString().Trim()
                If Not Uinfo.ReportID = 0 Then
                    If Not Uinfo.ReportID = 403 Then
                        RequiredFieldValidator3.Enabled = False
                    ElseIf Uinfo.ReportID = 403 Then
                        RequiredFieldValidator3.Enabled = True
                    End If
                    Select Case Uinfo.ReportID
                        Case 11, 104, 12, 13, 14, 15, 16, 17, 21, 22, 23, 31, 34, 35, 61, 62, 401, 402, 131
                            'dateTime,vehicle,VehicleType,VehicleKey,Sentry,Personnel,Tank,Dept,PersNum
                            Hide_Show_Controls(True, True, True, True, True, False, True, True, True, False, False)
                        Case 403
                            Hide_Show_Controls(True, True, True, True, True, False, True, True, True, False, True)
                        Case 41, 44, 45, 48, 49, 50
                            Hide_Show_Controls(False, True, True, True, False, False, False, True, False, False, False)
                        Case 91, 92
                            Hide_Show_Controls(True, True, True, True, False, False, False, True, False, False, False)
                        Case 46
                            Hide_Show_Controls(True, True, True, True, True, False, False, True, True, False, False)
                        Case 42, 43, 110, 304
                            Hide_Show_Controls(True, True, False, False, False, False, False, True, False, False, False)
                        Case 47
                            Hide_Show_Controls(True, True, True, True, True, True, True, True, True, False, False)
                        Case 51, 52, 53
                            Hide_Show_Controls(False, False, False, False, False, True, False, True, True, False, False)
                        Case 63, 64, 65, 66
                            Hide_Show_Controls(True, True, True, True, True, True, True, True, False, False, False)
                        Case 71, 73
                            Hide_Show_Controls(True, False, False, False, True, False, True, False, True, False, False)
                        Case 72, 88 'this report only requires one date
                            'this report only requires one date
                            StartDateTextBox.Text = "01/01/1999"
                            Hide_Show_Controls(True, False, False, False, False, False, False, False, False, False, False)
                            StartDateTextBox.Visible = False
                            StartdateLabel.Visible = False
                            StartTimeLabel.Visible = False
                            StartTimeTextBox.Visible = False
                            EndTimeTextBox.Visible = False
                            EndDateTextBox.Visible = True
                            StartDateImage.Visible = False
                            Label2.InnerText = "Balance as of "
                        Case 74
                            'dateTime,vehicle,VehicleType,VehicleKey,Sentry,Personnel,Tank,Dept,PersNum
                            Hide_Show_Controls(True, False, False, False, True, False, True, False, True, False, False)
                        Case 75, 76, 77, 78, 78
                            Hide_Show_Controls(True, False, False, False, False, False, True, False, True, False, False)

                            'Added By Varun Moota To Show Site Analaysis Report Dated:12/07/2009
                        Case 101
                            Hide_Show_Controls(True, False, False, False, True, False, False, False, False, False, False)

                        Case 302 'this report requires no Report Input.
                            'this report Runs without any Search Criteria.06/06/2011
                            StartDateTextBox.Visible = False
                            Label1.Visible = False
                            Label2.Visible = False
                            Hide_Show_Controls(False, False, False, False, False, False, False, False, False, False, False)
                            StartDateTextBox.Visible = False
                            StartdateLabel.Visible = False
                            StartTimeLabel.Visible = False
                            StartTimeTextBox.Visible = False
                            EndTimeTextBox.Visible = False
                            EndDateTextBox.Visible = False
                            StartDateImage.Visible = False
                        Case 303 ' This new Report for OBDII Trouble codes(Mostly J1939 Tag's).
                            Hide_Show_Controls(True, True, False, False, False, False, False, True, False, True, False)

                    End Select
                    CheckBoxState(Uinfo.ReportID)
                End If
                lblRepName.Text = GenFun.Get_Report_name(Uinfo.ReportID) + "                 "
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("Report_Input.aspx_Page_Load", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub

    Private Function CheckBoxState(ByVal ID As String)
        Try

       
            Select Case ID
                Case 11, 104, 12, 13, 14, 15, 16, 17, 21, 22, 23, 31, 32, 33, 61, 62, 63, 401, 131
                    CheckBox.Visible = True
                    CheckBox.Text = " Zero Quantity Transactions Only."
                Case 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 91, 92, 110, 303
                    CheckBox.Visible = True
                    CheckBox.Text = " Include Deleted Vehicles."
                Case 51, 52, 53
                    CheckBox.Visible = True
                    CheckBox.Text = " Include Deleted Personnel."
                Case Else
                    CheckBox.Visible = False
                    CheckBox.Text = ""
            End Select
            Return ""
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Report_Input.CheckBoxState", ex)
            Return ""
        End Try
    End Function

    Private Function Hide_Show_Controls(ByVal VisibleDateTime As Boolean, ByVal VisibleVehicle As Boolean, ByVal VisibleVehicleType As Boolean, ByVal VisibleVehicleKey As Boolean, ByVal VisibleSentry As Boolean, ByVal VisiblePersonnel As Boolean, ByVal VisibleTank As Boolean, ByVal VisibleDept As Boolean, ByVal blnPerNum As Boolean, ByVal blnSPN As Boolean, ByVal blnMPG As Boolean)
        Try


            Uinfo = New UserInfo
            Uinfo = Session("Uinfo")

            'HARDCODE PERSONNEL TO INVISIBLE; TAKE  OUT LATER TO REPLACE WITH NAME SEARCH
            ''Date and Time
            If VisibleDateTime Then DateTR.Visible = True : TimeTR.Visible = True Else DateTR.Visible = False : TimeTR.Visible = False
            ''Vehicle
            If VisibleVehicle Then Vehicle.Visible = True Else Vehicle.Visible = False
            ''Department
            If VisibleDept Then Department.Visible = True Else Department.Visible = False
            ''Personnel
            If VisiblePersonnel Then PersonnelLastName.Visible = True Else PersonnelLastName.Visible = False
            ''Tank
            If VisibleTank Then Tank.Visible = True Else Tank.Visible = False

            ''Vehicle Type
            If VisibleVehicleType Then VehicleType.Visible = True Else VehicleType.Visible = False
            ''Vehicle Key
            If VisibleVehicleKey Then VehicleKey.Visible = True Else VehicleKey.Visible = False
            ''Sentry
            If VisibleSentry Then Sentry.Visible = True Else Sentry.Visible = False
            If blnPerNum Then PerNum.Visible = True Else PerNum.Visible = False
            'New Search criteria for SPN'codes ,request from John.10/13/2011
            If blnSPN Then SPNNum.Visible = True Else SPNNum.Visible = False
            'New Search criteria for Transaction Detail – MPG out-of-range ,request from Katherine.05-Feb-2013
            If blnMPG Then trTransMpgRpt.Visible = True Else trTransMpgRpt.Visible = False
            'Wright Express Billing Report
            If Uinfo.ReportID.ToString().Trim() = "34" Or Uinfo.ReportID.ToString().Trim() = "35" Then
                Vehicle.Visible = False
                Department.Visible = False
                PersonnelLastName.Visible = False
                Tank.Visible = False
                VehicleType.Visible = False
                VehicleKey.Visible = False
                Sentry.Visible = False
            End If
            If Uinfo.ReportID.ToString().Trim() = "71" Or Uinfo.ReportID.ToString().Trim() = "73" Then
                Vehicle.Visible = False
                Department.Visible = False
                PersonnelLastName.Visible = False
                VehicleType.Visible = False
                VehicleKey.Visible = False
                PerNum.Visible = False
                Sentry.Visible = False
            End If
            Return ""
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Report_Input.Hide_Show_Controls", ex)
            Return ""
        End Try
    End Function

    Protected Sub RunReportButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RunReportButton.Click
        If ValidateCriteria() Then
            Try
                'Jatin Kshirsagar as on 26 Aug 2008
                GenFun = New GeneralFunctions

                Uinfo = New UserInfo()
                Uinfo = Session("Uinfo")
                '----------------------------------
                Dim stDate As String = ""
                Dim enDate As String = ""
                Dim strdt As String = DateTime.Now
                stDate = GenFun.ConvertDate(StartDateTextBox.Text.Trim())
                enDate = GenFun.ConvertDate(EndDateTextBox.Text.Trim())

                'if StartDateTextBox.Visible=False and EndDateTextBox.Visible=
                Dim i As Integer = DateDiff(DateInterval.Day, GenFun.ConvertDate(StartDateTextBox.Text.Trim()), GenFun.ConvertDate(EndDateTextBox.Text.Trim()))
                If i >= 0 Then
                    If Not StartTimeTextBox.Text.Trim() = "" Then
                        stDate += " " + CDate(StartTimeTextBox.Text.Trim()).ToString("HH:mm:ss")
                    Else
                        stDate += " 00:00:00"
                    End If

                    Session("StartDate") = stDate
                    If Not EndTimeTextBox.Text.Trim.Trim() = "" Then
                        enDate += " " + CDate(EndTimeTextBox.Text.Trim()).ToString("HH:mm:ss")
                    Else
                        enDate += " 23:59:59"
                    End If

                    Session("EndDate") = enDate

                    Uinfo.ReportHeader = GenFun.Get_Company_Name

                    Uinfo.StartDate = CDate(stDate)
                    Uinfo.EndDate = CDate(enDate)

                    Uinfo.StartVehicle = StartVehTextBox.Text.Trim()
                    Uinfo.EndVehicle = EndVehTextBox.Text.Trim()

                    Uinfo.StartDepartment = StartDeptTextBox.Text.Trim()
                    Uinfo.EndDepartment = EndDeptTextBox.Text.Trim()

                    Uinfo.StartSentry = StartSentryTextBox.Text.Trim()
                    Uinfo.EndSentry = EndSentryTextBox.Text.Trim()

                    Uinfo.StartVehType = StartVehTypeTextBox.Text.Trim()
                    Uinfo.EndVehicleType = EndVehTypeTextBox.Text.Trim()

                    Uinfo.StartTank = StartTankTextBox.Text.Trim()
                    Uinfo.EndTank = EndTankTextBox.Text.Trim()

                    Uinfo.StartVKey = StartVKeyTextBox.Text.Trim()
                    Uinfo.EndVKey = EndVKeyTextBox.Text.Trim()

                    Uinfo.StartPer = StartPerTextBox.Text.Trim()
                    Uinfo.EndPer = EndPerTextBox.Text.Trim()

                    Uinfo.StartPerID = txtStartPerNum.Text.Trim()
                    Uinfo.EndPerID = txtEndPerNum.Text.Trim()

                    Uinfo.StartSPN = txtSPNStart.Text.Trim()
                    Uinfo.EndSPN = txtSPNEnd.Text.Trim()

                    Uinfo.TransMPG = txtMPG.Text.Trim()

                    Uinfo.CheckBoxStatus = CheckBox.Checked

                    Session("Uinfo") = Uinfo

                    Dim strQuery As String = GenFun.Report(Convert.ToInt32(Uinfo.ReportID.ToString()), Uinfo)
                    Session("ReportInputs") = strQuery

                    'Response.Redirect("~/ReportViewer.aspx")
                    Dim url As String = "ReportViewer.aspx"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Run_Report('" & url & "');</script>")
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('From date should be less than To date.');</script>")
                End If
            Catch ex As Exception
                Dim cr As New ErrorPage
                Dim errmsg As String
                cr.errorlog("Report_Input_RunReportButton_Click", ex)
                If ex.Message.Contains(";") Then
                    errmsg = ex.Message.ToString()
                    errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
                Else
                    errmsg = ex.Message.ToString()
                End If
            End Try
        End If
    End Sub

    Private Function ValidateCriteria() As Boolean
        'check the data boxes for valid data
        Try

       
            ValidateCriteria = True
            lblValidateDigitString.Visible = False
            If (Not IsDigitString(StartSentryTextBox.Text) Or Not IsDigitString(EndSentryTextBox.Text)) Then
                lblValidateDigitString.Text = "Sentry must be a number"
                lblValidateDigitString.Visible = True
                ValidateCriteria = False
                Exit Function
            End If
            If (Not IsDigitString(StartTankTextBox.Text) Or Not IsDigitString(EndTankTextBox.Text)) Then
                lblValidateDigitString.Text = "Tank must be a number"
                lblValidateDigitString.Visible = True
                ValidateCriteria = False
                Exit Function
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Report_Input_RunReport.ValidateCriteria()", ex)
        End Try
    End Function

    Private Function IsDigitString(ByVal strCheck As String) As Boolean
        'check to make sure a string contains only digits
        Try

       
            Dim i As Integer
            IsDigitString = True
            For i = 0 To strCheck.Length - 1
                If (Not Char.IsDigit(strCheck(i))) Then
                    IsDigitString = False
                    i = strCheck.Length + 5
                End If
            Next
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Report_Input_RunReport.IsDigitString", ex)
        End Try
    End Function

    Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        Try
            Response.Redirect("Reports.aspx", False)
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Report_Input_RunReport.btncancel_Click", ex)
        End Try
    End Sub
End Class

