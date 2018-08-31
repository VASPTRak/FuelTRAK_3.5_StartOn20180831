Imports System.Data.SqlClient
Imports System.Data
Partial Class TankReconcileDetail
    Inherits System.Web.UI.Page
    Dim sqlReader As SqlDataReader
    Dim sqlCmd As SqlCommand
    Dim str_Connection_string As String = ConfigurationManager.AppSettings("str_Connection_string")
    Dim sqlAdapter As SqlDataAdapter
    Private SqlConn As SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strQuery As String
        Dim strSentry, strPumpNumber As String
        Dim strBeginDate, strEndDate As String
        Dim strTank, strTankName, strTankProduct As String
        Dim intBeginGallons, intEndGallons As String
        Dim intDelivered, intDispensed, intCalculated As Integer

        Try

      
            'Pull parameters from session
            strSentry = Session("SentryNumber")
            strPumpNumber = Session("PumpNumber")
            strBeginDate = Session("BeginDate") + " 00:00"
            strEndDate = Session("EndDate") + " 23:59"
            strTank = Session("TankNumber")


            'Load the sentry information


            'display the name and number of the sentry
            lblSentryName.Text = "SENTRY #:" + strSentry


            strQuery = "SELECT tank.number,tank.name,tank.product,product.name as prodname from tank left outer join product on tank.product = product.number WHERE tank.NUMBER = " + strTank

            SqlConn = New SqlConnection(str_Connection_string)

            Dim DA As SqlDataAdapter = New SqlDataAdapter(strQuery, SqlConn)
            Dim DS As DataSet = New DataSet()
            DA.Fill(DS)
            strTankName = DS.Tables(0).Rows(0).Item(1)
            strTankProduct = DS.Tables(0).Rows(0).Item(3)
            lblTankInfo.Text = "TANK #" + strTank + ":" + strTankName + "-" + strTankProduct

            'Gather information about the closest FRTD record the start date
            Dim daFRTD = New SqlDataAdapter("Select top 1 datetime ,qty_meas,height from FRTD where tank_nbr = " + strTank + " and datetime < '" + strBeginDate + "' and (entry_type = 'D' or entry_type = 'L') order by datetime desc", SqlConn)
            Dim dsFRTD As DataSet = New DataSet
            daFRTD.fill(dsFRTD)
            'display the results on the screen
            lblStartDate.Text = dsFRTD.Tables(0).Rows(0).Item(0).ToString
            lblBeginGallons.Text = dsFRTD.Tables(0).Rows(0).Item(1).ToString
            'store beginning gallons as integer for later. 
            intBeginGallons = dsFRTD.Tables(0).Rows(0).Item(1)
            lblBeginHeight.Text = "(" + dsFRTD.Tables(0).Rows(0).Item(2).ToString + " Inches)"

            'Gather information about the closest FRTD record to the end date
            Dim daFRTDend = New SqlDataAdapter("Select top 1 datetime ,qty_meas,height from FRTD where tank_nbr = " + strTank + " and datetime < '" + strEndDate + "' and (entry_type = 'D' or entry_type = 'L') order by datetime desc", SqlConn)
            Dim dsFRTDend = New DataSet
            daFRTDend.fill(dsFRTDend)
            'display the results on the screen
            lblEndDate.Text = dsFRTDend.Tables(0).Rows(0).Item(0).ToString
            lblEndGallons.Text = dsFRTDend.Tables(0).Rows(0).Item(1).ToString
            intEndGallons = dsFRTDend.Tables(0).Rows(0).Item(1)
            lblEndHeight.Text = "(" + dsFRTDend.Tables(0).Rows(0).Item(2).ToString + " Inches)"

            'Get the sum of any deliveries, making sure to use isnull to make sure we don't get a null in 
            ' the return dateset
            Dim daFRTDDel = New SqlDataAdapter("Select isnull(sum(qty_added),0) from FRTD where tank_nbr = " + strTank + "and datetime >= '" + lblStartDate.Text + "' and datetime <= '" + lblEndDate.Text + "' and entry_type = 'R'", SqlConn)
            Dim dsFRTDDel = New DataSet
            daFRTDDel.fill(dsFRTDDel)
            'display the results on the screen

            lblDelivery.Text = dsFRTDDel.Tables(0).Rows(0).Item(0).ToString
            If (lblDelivery.Text = "") Then
                lblDelivery.Text = "0"
            End If
            'store delivery as integer for later
            intDelivered = dsFRTDDel.Tables(0).Rows(0).Item(0)
            lblSubTotal.Text = (intDelivered + intBeginGallons).ToString

            'Get the sum of transaction data between the start and end dates retrieved from the
            'FRTD table
            Dim daTXTN = New SqlDataAdapter("Select isnull(sum(quantity),0) from txtn where sentry = " + strSentry.Substring(0, 3) + " and pump in (" + strPumpNumber + ") and datetime >= '" + lblStartDate.Text + "' and datetime <= '" + lblEndDate.Text + "'", SqlConn)
            Dim dsTXTN = New DataSet
            daTXTN.fill(dsTXTN)
            'display the results on the screen
            intDispensed = dsTXTN.Tables(0).Rows(0).Item(0)
            'intDispensed = System.Math.Round(intDispensed)

            lblDispensed.Text = intDispensed.ToString

            intCalculated = intBeginGallons + intDelivered - intDispensed
            lblCalculated.Text = intCalculated.ToString

            lblDifference.Text = (intCalculated - intEndGallons).ToString
        Catch ex As Exception
            Dim exLog As New ErrorPage
            exLog.errorlog("TankReconcileDetail.Page_load", ex)
        End Try

    End Sub
    

End Class
