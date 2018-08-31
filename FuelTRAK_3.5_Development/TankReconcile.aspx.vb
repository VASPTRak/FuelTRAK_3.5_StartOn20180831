Imports System.Data.SqlClient
Imports System.Data
Partial Class TankReconcile
    Inherits System.Web.UI.Page
    Dim sqlReader As SqlDataReader
    Dim sqlCmd As SqlCommand
    Dim str_Connection_string As String = IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString())
    Dim sqlAdapter As SqlDataAdapter
    Private SqlConn As SqlConnection
    Dim strDistrict As String
    Dim DAL As GeneralizedDAL

    Protected Sub ddlDistrict_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDistrict.SelectedIndexChanged
        Try

            'when this item changes, we want to refresh the list in ddlSentry
            'get the district number
            strDistrict = ddlDistrict.SelectedValue.ToString
            UpdateSentryList()
            lblError.Visible = False

        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.ddlDistrict_SelectedIndexChanged", ex)

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                'set the end date to today, begin date to a month ago
                If Not IsPostBack Then
                    calBeginDate.SelectedDate = Now().AddMonths(-1)
                    calEndDate.SelectedDate = Now()
                    calBeginDate.VisibleDate = calBeginDate.SelectedDate
                End If
                lblBeginDate.Text = calBeginDate.SelectedDate.ToLongDateString
                lblEndDate.Text = calEndDate.SelectedDate.ToLongDateString
                'UpdateSentryList()
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.Page_Load", ex)
        End Try
        
    End Sub

    Private Sub UpdateSentryList()
        Try
            Dim strQuery As String
            DAL = New GeneralizedDAL
            'update the list of sentries
            strQuery = "SELECT NUMBER,NAME, NUMBER + ':' + NAME AS NUMNAME FROM SENTRY WHERE pump1_tank > '000' order by number"
            Dim DS As DataSet = DAL.GetDataSet(strQuery)
            If (DS.Tables(0).Rows.Count > 0) Then
                ddlSentry.DataValueField = "NUMBER"
                ddlSentry.DataTextField = "NUMNAME"
                ddlSentry.DataSource = DS.Tables(0) 'sqlReader(0)
                ddlSentry.DataBind()
                UpdateTankList()
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.UpdateSentryList()", ex)
        End Try
       
    End Sub

    Private Sub UpdateTankList()
        Try

   
            Dim strQuery As String
            Dim i As Integer
            Dim strTankList As String
            DAL = New GeneralizedDAL
            'update the list of sentries
            strQuery = "SELECT NUMBER,NAME,PUMP1_TANK,PUMP2_TANK,PUMP3_TANK,PUMP4_TANK,PUMP5_TANK,PUMP6_TANK,PUMP7_TANK,PUMP8_TANK FROM SENTRY WHERE pump1_tank > '000' and number = " + ddlSentry.SelectedValue
            Dim DS As DataSet = DAL.GetDataSet(strQuery)

            'reinitialize the hidden pumps list
            hidPumpList.Text = ""

            strTankList = ""
            For i = 2 To 9
                If (DS.Tables(0).Rows(0).Item(i).ToString <> "000") Then
                    strTankList = strTankList + DS.Tables(0).Rows(0).Item(i).ToString
                    strTankList = strTankList + ","
                    'We need a list of which pumps are assigned to which tanks,
                    ' to send on to the next screen for the quantity query
                    hidPumpList.Text = hidPumpList.Text + DS.Tables(0).Rows(0).Item(i).ToString() + ","
                End If
            Next
            'remove the last "," 
            strTankList = strTankList.Substring(0, strTankList.Length - 1)
            hidPumpList.Text = hidPumpList.Text.Substring(0, hidPumpList.Text.Length - 1)

            'now retrieve tank info for  all of those tanks
            strQuery = "SELECT NUMBER,NUMBER + ':' + NAME AS NUMNAME FROM TANK WHERE NUMBER IN (" + strTankList + ")"
            Dim dsTanks As DataSet = DAL.GetDataSet(strQuery)
            ddlTank.DataValueField = "NUMBER"
            ddlTank.DataTextField = "NUMNAME"
            ddlTank.DataSource = dsTanks.Tables(0) 'sqlReader(0)
            ddlTank.DataBind()
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.UpdateTankList()", ex)
        End Try
    End Sub

    Protected Sub btnLaunch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLaunch.Click
        Try

 
            If (ddlSentry.SelectedIndex = -1) Then
                MsgBox("No Sentry Selected", MsgBoxStyle.OkOnly, "Notification")
            Else
                Dim arrPumps(12), strPumps As String

                'send the selected criteria to the next screen
                Session.Add("SentryNumber", ddlSentry.SelectedItem.Text)
                'I want to send a list of which pumps hold the  selected tank
                arrPumps = hidPumpList.Text.Split(","c)
                Dim i As Integer
                strPumps = ""
                For i = 0 To arrPumps.GetUpperBound(0)

                    If (arrPumps(i) = ddlTank.SelectedValue) Then
                        strPumps = strPumps + (i + 1).ToString.PadLeft(2, "0"c) + ","
                    End If
                Next
                'remove the last ","
                strPumps = strPumps.Remove(strPumps.Length - 1, 1)

                Session.Add("PumpNumber", strPumps)
                Session.Add("BeginDate", calBeginDate.SelectedDate.ToShortDateString)
                Session.Add("EndDate", calEndDate.SelectedDate.ToShortDateString)
                Session.Add("TankNumber", ddlTank.SelectedValue)
                Server.Transfer("TankReconcileDetail.aspx")
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.btnLaunch_Click", ex)
        End Try
    End Sub

    Protected Sub ddlSentry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSentry.SelectedIndexChanged

        Try
            UpdateTankList()
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.ddlSentry_SelectedIndexChanged", ex)
        End Try
    End Sub

    Protected Sub calBeginDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calBeginDate.SelectionChanged
        
        Try
            lblBeginDate.Text = calBeginDate.SelectedDate.ToShortDateString
            lblEndDate.Text = calEndDate.SelectedDate.ToLongDateString
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.calBeginDate_SelectionChanged", ex)
        End Try
    End Sub

    Protected Sub calEndDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calEndDate.SelectionChanged
        Try
            lblEndDate.Text = calEndDate.SelectedDate.ToLongDateString
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.calEndDate_SelectionChanged", ex)
        End Try
    End Sub

    Public Function Get_Company_Name() As String
        Dim strQry As String = ""
        Try

     
            SqlConn = New SqlConnection()
            SqlConn.ConnectionString = str_Connection_string
            SqlConn.Open()
            strQry = "select Owner from Status"
            sqlCmd = New SqlCommand(strQry, SqlConn)
            Dim obj As Object = New Object
            obj = sqlCmd.ExecuteScalar()
            If Not obj = Nothing Then
                Return obj.ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.Get_Company_Name()", ex)
        End Try
    End Function

    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Try

        
            If (ddlSentry.SelectedIndex = -1) Then
                lblError.Visible = True
            Else
                Dim arrPumps(12), strPumps As String

                'send the selected criteria to the next screen
                Session.Add("SentryNumber", ddlSentry.SelectedItem.Text)
                'I want to send a list of which pumps hold the  selected tank
                arrPumps = hidPumpList.Text.Split(","c)
                Dim i As Integer
                strPumps = ""
                For i = 0 To arrPumps.GetUpperBound(0)

                    If (arrPumps(i) = ddlTank.SelectedValue) Then
                        strPumps = strPumps + (i + 1).ToString.PadLeft(2, "0"c) + ","
                    End If
                Next
                'remove the last ","
                strPumps = strPumps.Remove(strPumps.Length - 1, 1)

                Session.Add("PumpNumber", strPumps)
                Session.Add("StartDate", calBeginDate.SelectedDate.ToShortDateString)
                Session.Add("EndDate", calEndDate.SelectedDate.ToShortDateString)
                Session.Add("TankNumber", ddlTank.SelectedValue)

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Run_Report('NoDataReportViewer.aspx');</script>")

                'Dim url As String = "ReportViewer_New.aspx?ReportID=" & "73" & _
                '                    "&ReportName=" & "" & "&Reportcomment=" & "" & "&ReportHeader=" & Get_Company_Name()
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>Run_Report('" & url & "');</script>")

            End If
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcile.btnReport_Click", ex)
        End Try
    End Sub
End Class
