Partial Class Reports
    Inherits System.Web.UI.Page
    Dim Uinfo As UserInfo
    Dim GenFun As GeneralFunctions

    Protected Sub ReportOkButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReportOkButton.Click
        Try
            Dim i As Integer = 1
            GenFun = New GeneralFunctions
            Uinfo = New UserInfo

            If rdoSelectReport.Visible = True Then
                i = rdoSelectReport.SelectedItem.Value
            ElseIf rdoSelectReport1.Visible = True Then
                i = rdoSelectReport1.SelectedItem.Value
            End If
            'Added BY Varun Moota to Show Report selection.
            Session("SelectedRPT") = rdoSelectReport.SelectedItem.Value.ToString()

            Uinfo.ReportID = Convert.ToInt32(lstReport.Value)
            Uinfo.ReportHeader = GenFun.Get_Company_Name
            If (lstReport.Value.ToString() = "45") Then
                Uinfo.ReportTitle = "Vehicle List of PM Due Report"
            ElseIf (lstReport.Value.ToString() = "79") Then
                Uinfo.ReportTitle = "Tank Current Balance Report"
            ElseIf (lstReport.Value.ToString() = "81") Then
                Uinfo.ReportTitle = "Listing-Departments"
            ElseIf (lstReport.Value.ToString() = "82") Then
                Uinfo.ReportTitle = "Listing-Site Information"
            ElseIf (lstReport.Value.ToString() = "83") Then
                Uinfo.ReportTitle = "Listing-Tanks"
            ElseIf (lstReport.Value.ToString() = "84") Then
                Uinfo.ReportTitle = "Listing-Lockouts"
            ElseIf (lstReport.Value.ToString() = "85") Then
                Uinfo.ReportTitle = "Listing-Current Export File"
                'Added By Varun Moota 12/07/2009
            ElseIf (lstReport.Value.ToString() = "101") Then
                Uinfo.ReportTitle = "Site Analysis Summary Report"
                'Added By Varun Moota New Report(Fuel Use By Veh Type).11/19/2010
            ElseIf (lstReport.Value.ToString() = "64") Then
                Uinfo.ReportTitle = "Fuel Use Report by Type"
                'Added By Varun Moota New Report(Fuel Use By Veh Detail).01/27/2011
            ElseIf (lstReport.Value.ToString() = "65") Then
                Uinfo.ReportTitle = "Fuel Use by Vehicle-Detail"
                'Added By Varun Moota New Report(Fuel Use By Personnel/Dept).03/10/2011
            ElseIf (lstReport.Value.ToString() = "66") Then
                Uinfo.ReportTitle = "Fuel Use by Personnel/Dept"
            ElseIf (lstReport.Value.ToString() = "43") Then
                Uinfo.ReportTitle = "Vehicle Trouble Code"
            ElseIf (lstReport.Value.ToString() = "110") Then
                Uinfo.ReportTitle = "Vehicle DTC List"
                'Added By Varun Moota New Report(Vehicle OBDII Trouble Code report).03/10/2011
            ElseIf (lstReport.Value.ToString() = "303") Then
                Uinfo.ReportTitle = "Vehicle Trouble Code"
                'Added By Jatin New Reports(Transaction Detail).07-Feb-2013
            ElseIf (lstReport.Value.ToString() = "402") Then
                Uinfo.ReportTitle = "Transaction Detail – Exceeds Miles Window"
            ElseIf (lstReport.Value.ToString() = "403") Then
                Uinfo.ReportTitle = "Transaction Detail – MPG out-of-range"
            End If
            Session("Uinfo") = Uinfo
            Select Case CStr(i)
                Case 1, 2, 3, 4, 5, 6, 7, 8
                    Select Case Convert.ToInt32(lstReport.Value)
                        'Case 73
                        '    Response.Redirect("TankReconcile.aspx?ReportID=" & lstReport.Value.ToString(), False)

                        Case 45, 79, 81, 82, 83, 84, 85, 50
                            Dim url As String = "ReportViewer.aspx"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Run_Report('" & url & "');</script>")
                        Case Else
                            Response.Redirect("Reports_Input.aspx?ReportID=" & lstReport.Value.ToString(), False)
                    End Select
                Case Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Please select report from list')</script>")
            End Select
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Reports.ReportOkButton_Click", ex)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion();</script>")
                Exit Sub
            End If
            ''Added BY Varun Moota to Show Report selection.
            'If Not (Session("SelectedRPT") Is Nothing) Then
            '    rdoSelectReport.SelectedItem.Value = Convert.ToInt16(Session("SelectedRPT").ToString())
            'Else
            '    Session("SelectedRPT") = Nothing
            'End If
            If Not (Session("User_name") Is Nothing) Then
                If (Session("User_Level").ToString() = "1") Then
                    rdoSelectReport.Visible = True
                    rdoSelectReport1.Visible = False
                Else
                    rdoSelectReport.Visible = False
                    rdoSelectReport1.Visible = True
                End If
                If Not IsPostBack Then
                    rdoSelectReport_SelectedIndexChanged(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Reports.Page_Load", ex)
        End Try
    End Sub

    Protected Sub rdoSelectReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSelectReport.SelectedIndexChanged
        Try
            If (rdoSelectReport.SelectedItem.Value = "1") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Transaction List in Date/Time Order", "11"))
                lstReport.Items.Add(New ListItem("Transaction Customized List - Sheboygan County", "104"))
                lstReport.Items.Add(New ListItem("Transaction List in Sentry Order", "12"))
                lstReport.Items.Add(New ListItem("Transaction List in Personnel Order", "13"))
                lstReport.Items.Add(New ListItem("Transaction List in Vehicle Order", "14"))
                lstReport.Items.Add(New ListItem("Transaction List of Errors", "15"))
                lstReport.Items.Add(New ListItem("Transaction List - Master Key Usage", "16"))
                lstReport.Items.Add(New ListItem("Transaction List - By Vehicle Type", "17"))
                lstReport.Items.Add(New ListItem("Transaction List - By Dept", "401"))
                lstReport.Items.Add(New ListItem("Transaction Detail – Exceeds Miles Window", "402"))
                lstReport.Items.Add(New ListItem("Transaction Detail – MPG out-of-range", "403"))
            ElseIf (rdoSelectReport.SelectedItem.Value = "2") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Sentry Report in Date/Time Order", "21"))
                lstReport.Items.Add(New ListItem("Sentry Report - Totals Only", "22"))
                lstReport.Items.Add(New ListItem("Sentry Report in Vehicle Order", "23"))
                'Added By Varun Moota To Show Site Analaysis Report Dated:12/07/2009
                lstReport.Items.Add(New ListItem("Site Analysis Summary Report", "101"))

            ElseIf (rdoSelectReport.SelectedItem.Value = "3") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Dept Billing Report - Detail", 31))
                lstReport.Items.Add(New ListItem("Detail Billing Report – Personnel", 131))
                lstReport.Items.Add(New ListItem("Personnel Dept Billing Report  - Detail", 70))
                lstReport.Items.Add(New ListItem("Dept Billing Report - Vehicle Summary", 32))
                lstReport.Items.Add(New ListItem("Dept Billing Report - Department Summary", 33))
                lstReport.Items.Add(New ListItem("Wright Express Billing Report", 34))
                lstReport.Items.Add(New ListItem("Wright Express Billing Report - Sentry Datails", 35))
            ElseIf (rdoSelectReport.SelectedItem.Value = "4") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Vehicle List by Department", 41))
                lstReport.Items.Add(New ListItem("Fuel Use by Dept/Vehicle Summary", 42))
                lstReport.Items.Add(New ListItem("Vehicle List with Trouble Codes", 43))
                lstReport.Items.Add(New ListItem("Vehicle List with OBDII Trouble Codes", 303))
                'New Report Vehicle DTC-List.04/13/2011
                lstReport.Items.Add(New ListItem("Vehicle DTC List", 110))
                lstReport.Items.Add(New ListItem("Vehicle Engine DTC List", 304))
                lstReport.Items.Add(New ListItem("Vehicle List in Identity Order", 44))
                lstReport.Items.Add(New ListItem("Vehicle List of PM Due", 45))
                lstReport.Items.Add(New ListItem("Vehicle Performance Report", 46))
                lstReport.Items.Add(New ListItem("Vehicle Mileage Override Report", 47))
                lstReport.Items.Add(New ListItem("Vehicle List by Type", 48))
                lstReport.Items.Add(New ListItem("Vehicle List-FA Calibration", 49))
                lstReport.Items.Add(New ListItem("MPG Deviation Report", 50))
                lstReport.Items.Add(New ListItem("Vehicle History Summary", 91))
                lstReport.Items.Add(New ListItem("Vehicle that have not Fueled", 92))
            ElseIf (rdoSelectReport.SelectedItem.Value = 5) Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Personnel List in Dept/Name Order", 51))
                lstReport.Items.Add(New ListItem("Personnel List in ID Order", 52))
                lstReport.Items.Add(New ListItem("Personnel List in Last Name Order", 53))
            ElseIf (rdoSelectReport.SelectedItem.Value = "6") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Fuel Use by Personnel", 61))
                'lstReport.Items.Add(New ListItem("Fuel Use by Last Name", 61))
                lstReport.Items.Add(New ListItem("Fuel Use by Vehicle - Summary", 62))
                lstReport.Items.Add(New ListItem("Fuel Use by Department", 63))
                'Added By Varun Moota, New report for Vallecitos.11/19/2010 
                lstReport.Items.Add(New ListItem("Fuel Use Report by Type", 64))
                'Added By Varun Moota, New report for Vallecitos.11/19/2010 
                lstReport.Items.Add(New ListItem("Fuel Use by Vehicle - Detail", 65))
                lstReport.Items.Add(New ListItem("Fuel Use by Personnel/Dept", 66))
            ElseIf (rdoSelectReport.SelectedItem.Value = "7") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Inventory Activity", 71))
                lstReport.Items.Add(New ListItem("Tank Balance", 72))
                'Commented By Varun Moota, as it need's More Understanding. 03/16/2010
                lstReport.Items.Add(New ListItem("Tank Reconciliation", 73))
                lstReport.Items.Add(New ListItem("Pump Totalizer Report", 74))
                lstReport.Items.Add(New ListItem("Percent Usage Report", 75))
                'Commented By Varun Moota As Per Katherine's Request. 12/16/2009
                'lstReport.Items.Add(New ListItem("Tank Reconciliation, No Dippings", 76))
                lstReport.Items.Add(New ListItem("Tank Balance-FIFO", 77))
                lstReport.Items.Add(New ListItem("Inventory Information-No Fuel TXTN", 78))
                'Commented By Varun Moota As Per Katherine's Request. 12/16/2009
                'lstReport.Items.Add(New ListItem("Tank Current Balance Report", 79))
            ElseIf (rdoSelectReport.SelectedItem.Value = "8") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Listing - Departments", 81))
                lstReport.Items.Add(New ListItem("Listing - Site Information", 82))
                lstReport.Items.Add(New ListItem("Listing - Tanks", 83))
                lstReport.Items.Add(New ListItem("Listing - Lockouts", 84))
                lstReport.Items.Add(New ListItem("Listing - Current Export File", 85))
                'lstReport.Items.Add(New ListItem("Polling Results", 86))
                'lstReport.Items.Add(New ListItem("List of Overrides", 87))
                'Commented By Varun Moota As it fails.03/16/2010
                'lstReport.Items.Add(New ListItem("Export Summary Report", 88))
                'Added By Varun Moota , Polling Results Report.06/06/2011
                lstReport.Items.Add(New ListItem("Polling Results Report", 302))
            Else
                lstReport.Items.Clear()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Reports.rdoSelectReport_SelectedIndexChanged", ex)
        End Try
    End Sub

    Protected Sub rdoSelectReport1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSelectReport1.SelectedIndexChanged
        Try

      
            If (rdoSelectReport1.SelectedItem.Value = "1") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Transaction List in Date/Time Order", "11"))
                lstReport.Items.Add(New ListItem("Transaction Customized List - Sheboygan County", "104"))
                lstReport.Items.Add(New ListItem("Transaction List in Sentry Order", "12"))
                lstReport.Items.Add(New ListItem("Transaction List in Personnel Order", "13"))
                lstReport.Items.Add(New ListItem("Transaction List in Vehicle Order", "14"))
                lstReport.Items.Add(New ListItem("Transaction List of Errors", "15"))
                lstReport.Items.Add(New ListItem("Transaction List - Master Key Usage", "16"))
                lstReport.Items.Add(New ListItem("Transaction List - By Vehicle Type", "17"))
                lstReport.Items.Add(New ListItem("Transaction List - By Dept", "401"))
                lstReport.Items.Add(New ListItem("Transaction Detail – Exceeds Miles Window", "402"))
                lstReport.Items.Add(New ListItem("Transaction Detail – MPG out-of-range", "403"))
            ElseIf (rdoSelectReport1.SelectedItem.Value = "2") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Sentry Report in Date/Time Order", "21"))
                lstReport.Items.Add(New ListItem("Sentry Report - Totals Only", "22"))
                lstReport.Items.Add(New ListItem("Sentry Report in Vehicle Order", "23"))

                'Added By Varun Moota To Show Site Analaysis Report Dated:12/07/2009
                lstReport.Items.Add(New ListItem("Site Analysis Summary Report", "101"))

            ElseIf (rdoSelectReport1.SelectedItem.Value = "3") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Dept Billing Report - Detail", 31))
                lstReport.Items.Add(New ListItem("Detail Billing Report – Personnel", 131))
                lstReport.Items.Add(New ListItem("Personnel Dept Billing Report  - Detail", 70))
                lstReport.Items.Add(New ListItem("Dept Billing Report - Vehicle Summary", 32))
                lstReport.Items.Add(New ListItem("Dept Billing Report - Department Summary", 33))
                lstReport.Items.Add(New ListItem("Wright Express Billing Report", 34))
                'lstReport.Items.Add(New ListItem("Wright Express Billing Report - Sentry Datails", 35))
            ElseIf (rdoSelectReport1.SelectedItem.Value = "4") Then
                'lstReport.Items.Clear()
                'lstReport.Items.Add(New ListItem("Vehicle List by Department", 41))
                'lstReport.Items.Add(New ListItem("Fuel Use by Dept/Vehicle Summary", 42))
                'lstReport.Items.Add(New ListItem("Vehicle List with Trouble Codes", 43))
                'lstReport.Items.Add(New ListItem("Vehicle List with OBDII Trouble Codes", 303))
                ''New Report Vehicle DTC-List.04/13/2011
                'lstReport.Items.Add(New ListItem("Vehicle List with Trouble Codes", 110))
                'lstReport.Items.Add(New ListItem("Vehicle List in Identity Order", 44))
                'lstReport.Items.Add(New ListItem("Vehicle List of PM Due", 45))
                'lstReport.Items.Add(New ListItem("Vehicle Performance Report", 46))
                ''lstReport.Items.Add(New ListItem("Vehicle Mileage Override Report", 47))
                'lstReport.Items.Add(New ListItem("Vehicle List by Type", 48))
                'lstReport.Items.Add(New ListItem("Vehicle List-FA Calibration", 49))
                'lstReport.Items.Add(New ListItem("MPG Deviation Report", 50))
                'lstReport.Items.Add(New ListItem("Vehicle History Summary", 91))
                'lstReport.Items.Add(New ListItem("Vehicle that have not Fueled", 92))
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Vehicle List by Department", 41))
                lstReport.Items.Add(New ListItem("Fuel Use by Dept/Vehicle Summary", 42))
                lstReport.Items.Add(New ListItem("Vehicle List with Trouble Codes", 43))
                lstReport.Items.Add(New ListItem("Vehicle List with OBDII Trouble Codes", 303))
                'New Report Vehicle DTC-List.04/13/2011
                lstReport.Items.Add(New ListItem("Vehicle DTC List", 110))
                lstReport.Items.Add(New ListItem("Vehicle Engine DTC List", 304))
                lstReport.Items.Add(New ListItem("Vehicle List in Identity Order", 44))
                lstReport.Items.Add(New ListItem("Vehicle List of PM Due", 45))
                lstReport.Items.Add(New ListItem("Vehicle Performance Report", 46))
                'lstReport.Items.Add(New ListItem("Vehicle Mileage Override Report", 47))
                lstReport.Items.Add(New ListItem("Vehicle List by Type", 48))
                lstReport.Items.Add(New ListItem("Vehicle List-FA Calibration", 49))
                lstReport.Items.Add(New ListItem("MPG Deviation Report", 50))
                lstReport.Items.Add(New ListItem("Vehicle History Summary", 91))
                lstReport.Items.Add(New ListItem("Vehicle that have not Fueled", 92))
            ElseIf (rdoSelectReport1.SelectedItem.Value = 5) Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Personnel List in Dept/Name Order", 51))
                lstReport.Items.Add(New ListItem("Personnel List in ID Order", 52))
                lstReport.Items.Add(New ListItem("Personnel List in Last Name Order", 53))
            ElseIf (rdoSelectReport1.SelectedItem.Value = "6") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Fuel Use by Personnel", 61))
                'lstReport.Items.Add(New ListItem("Fuel Use by Last Name", 61))
                'lstReport.Items.Add(New ListItem("Fuel Use by Vehicle - Summary", 62))
                lstReport.Items.Add(New ListItem("Fuel Use by Department", 63))
                'Added By Varun Moota, New report for Vallecitos.11/19/2010 
                'lstReport.Items.Add(New ListItem("Fuel Use Report by Type", 64))
                'Added By Varun Moota, New Report for ISD.03/10/2011
                'lstReport.Items.Add(New ListItem("Fuel Use by Personnel/Dept", 66))
            ElseIf (rdoSelectReport1.SelectedItem.Value = "7") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Inventory Activity", 71))
                lstReport.Items.Add(New ListItem("Tank Balance", 72))
                lstReport.Items.Add(New ListItem("Tank Reconciliation", 73))
                lstReport.Items.Add(New ListItem("Pump Totalizer Report", 74))
                lstReport.Items.Add(New ListItem("Percent Usage Report", 75))
                lstReport.Items.Add(New ListItem("Tank Reconciliation, No Dippings", 76))
                lstReport.Items.Add(New ListItem("Tank Balance-FIFO", 77))
                lstReport.Items.Add(New ListItem("Inventory Information-No Fuel TXTN", 78))
                lstReport.Items.Add(New ListItem("Tank Current Balance Report", 79))
            ElseIf (rdoSelectReport1.SelectedItem.Value = "8") Then
                lstReport.Items.Clear()
                lstReport.Items.Add(New ListItem("Listing - Departments", 81))
                lstReport.Items.Add(New ListItem("Listing - Site Information", 82))
                lstReport.Items.Add(New ListItem("Listing - Tanks", 83))
                lstReport.Items.Add(New ListItem("Listing - Lockouts", 84))
                lstReport.Items.Add(New ListItem("Listing - Current Export File", 85))
                'lstReport.Items.Add(New ListItem("Polling Results", 86))
                'lstReport.Items.Add(New ListItem("List of Overrides", 87))
                lstReport.Items.Add(New ListItem("Export Summary Report", 88))
                'Added By Varun Moota , Polling Results Report.06/06/2011
                lstReport.Items.Add(New ListItem("Polling Results Report", 302))
            Else
                lstReport.Items.Clear()
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Reports.rdoSelectReport1_SelectedIndexChanged", ex)
        End Try
    End Sub

End Class
