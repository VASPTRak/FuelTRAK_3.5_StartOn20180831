Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient
Partial Class TankRecon
    Inherits System.Web.UI.Page
    Dim GenFun As New GeneralFunctions
    Dim dal = New GeneralizedDAL()
    Dim ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            Else
                If (Not Page.IsPostBack) Then
                    'Clear Session Variables
                    ClearSessionValues()
                    InsertTanksInTot()
                    'Dual Dispenser
                    DualDispInsertTankInTot()
                End If
                End If
                'End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankRecon.PageLoad", ex)
        End Try
    End Sub

    Protected Sub ddltank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltank.SelectedIndexChanged
        Try

            Session("TNKNBR") = ddltank.SelectedValue.ToString()
            Session("StartDTRecon") = ddlMonth.SelectedValue.ToString() + "/" + "01" + "/" + ddlYear.SelectedValue.ToString()


            If CheckRecordsExists() Then
                If Not CheckDualDispExists() Then
                    Response.Redirect("ReconciliationForm.aspx", False)
                Else
                    Response.Redirect("ReconciliationForm2.aspx", False)
                End If

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No Records found!');</script>")
                ddltank.SelectedIndex = 0
            End If

           

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankRecon.ddltank_SelectedIndexChanged", ex)
        End Try
    End Sub
    
    Public Function CheckRecordsExists() As Boolean
        Try

     
            Dim dal = New GeneralizedDAL()
            Dim qry As String = "Select  * from TankTot where datepart(mm,[datetime]) = datepart(mm,'" + Session("StartDTRecon").ToString() + "')" & _
                    " AND TANK_NBR='" + Session("TNKNBR").ToString() + "'"

            Dim dsTankTotRecords As New DataSet
            dsTankTotRecords = dal.GetDataSet(qry)
            If dsTankTotRecords.Tables(0).Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankRecon.CheckRecordsExists()", ex)
        End Try
    End Function

    Public Sub InsertTanksInTot()
        Try
            Dim tankNo As String = ""
            Dim dtString As String = ""
            Dim dal = New GeneralizedDAL()
            Dim qry As String = ""
            qry = " select tank_nbr,Convert(varchar,[datetime],101) as [datetime] from FRTD t1 WHERE t1.tank_nbr NOT IN " & _
                            "(select t2.tank_nbr from TankTot t2 where t2.tank_nbr=t1.tank_nbr " & _
                            " AND datepart(mm,t2.[datetime]) = datepart(mm,t1.[datetime]) And datepart(yy,t2.[datetime]) = datepart(yy,t1.[datetime]) ) AND tank_NBR  != ''"
            '"AND datepart(mm,t1.[datetime]) NOT IN (select datepart(mm,t3.[datetime]) from TankTot t3)  " & _
            '"AND datepart(yy,t1.[datetime]) NOT IN (select datepart(mm,t4.[datetime]) from TankTot t4)"
            Dim dsFRTD As New DataSet
            dsFRTD = dal.GetDataSet(qry)
            If dsFRTD.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                For i = 0 To dsFRTD.Tables(0).Rows.Count - 1
                    tankNo = dsFRTD.Tables(0).Rows(i)("Tank_nbr").ToString()
                    dtString = dsFRTD.Tables(0).Rows(i)("datetime").ToString()
                    Dim parcollection(1) As SqlParameter
                    Dim parTankNo = New SqlParameter("@TankNo", SqlDbType.NVarChar, 3)
                    Dim parDT = New SqlParameter("@Datetime", SqlDbType.NVarChar, 15)

                    parTankNo.Direction = ParameterDirection.Input
                    parDT.Direction = ParameterDirection.Input

                    parTankNo.value = tankNo
                    parDT.value = dtString.ToString().Replace("#", "")

                    parcollection(0) = parTankNo
                    parcollection(1) = parDT
                    Dim blnInsertFlag As Boolean = dal.ExecuteStoredProcedureGetBoolean("usp_tt_GetDateTime_DFW", parcollection)
                Next
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankRecon.InsertTanksInTot()", ex)
        End Try
    End Sub
    Public Sub DualDispInsertTankInTot()
        Try
            Dim tankNo As String = ""
            Dim dtString As String = ""
            Dim dal = New GeneralizedDAL()

            Dim qry As String = " select distinct [Tank] from TXTN where Pump = '02' AND tank !='' AND TANK IN (SELECT [TANK_NBR] FROM FRTD)"
            Dim dsDisp2 As New DataSet
            dsDisp2 = dal.GetDataSet(qry)
            If dsDisp2.Tables(0).Rows.Count > 0 Then

                Dim i As Integer
                For i = 0 To dsDisp2.Tables(0).Rows.Count - 1
                    tankNo = dsDisp2.Tables(0).Rows(i)("Tank").ToString()
                    'dtString = dsDisp2.Tables(0).Rows(i)("DateTime").ToString()
                    dtString = DateTime.Now.ToString("MM/dd/yyyy")

                    Dim parcollection(1) As SqlParameter
                    Dim parTankNo = New SqlParameter("@TankNo", SqlDbType.NVarChar, 3)
                    Dim parDT = New SqlParameter("@Datetime", SqlDbType.NVarChar, 15)

                    parTankNo.Direction = ParameterDirection.Input
                    parDT.Direction = ParameterDirection.Input

                    parTankNo.Value = tankNo
                    parDT.Value = dtString.ToString().Replace("#", "")

                    parcollection(0) = parTankNo
                    parcollection(1) = parDT
                    Dim blnInsertFlag As Boolean = dal.ExecuteStoredProcedureGetBoolean("usp_tt_GetDateTime_DFW_2", parcollection)
                Next
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankRecon.CheckDualDisp()", ex)
        End Try
    End Sub
    Public Function CheckDualDispExists() As Boolean
        Try
            Dim dal = New GeneralizedDAL()
            Dim qry As String = " select Tank,Convert(varchar,[datetime],101) as [datetime] from TXTN where Pump = '02' AND tank ='" + Session("TNKNBR").ToString() + "'"
            Dim dsDisp2 As New DataSet
            dsDisp2 = dal.GetDataSet(qry)
            If dsDisp2.Tables(0).Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankRecon.CheckDualDispExists()", ex)
        End Try
    End Function
    Private Sub ClearSessionValues()
        Try
            Session.Remove("TNKNBR")
            Session.Remove("StartDTRecon")
            Session.Remove("EndDTRecon")
            Session.Remove("TankTotLoc")
            Session.Remove("TankTotYear")
            Session.Remove("TankTotMonth")
            Session.Remove("strPrevMntLevel")
            Session.Remove("strPrevMntTot")
            Session.Remove("CurrTotalizer")
            Session.Remove("CurrTotalizer2")
            Session.Remove("strPrevMntTot2")
            Session.Remove("CntCurrentTotalizerAB")
            Session.Remove("strCurrentMntTot")
            Session.Remove("strCurrentMntDel")
            Session.Remove("strCurrentMntLevels")
            Session.Remove("cntLine1")
            Session.Remove("cntLine2")
            Session.Remove("cntLine3")
            Session.Remove("cntLine4")
            Session.Remove("cntLine5")
            Session.Remove("cntLine6")
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankRecon.ClearSessionValues()", ex)
        End Try

    End Sub

    Protected Sub ddlMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMonth.SelectedIndexChanged
        Try
            If ddlMonth.SelectedIndex > 0 Then
                ddlYear.Visible = True
            Else
                ddlYear.Visible = False
                ddltank.Visible = False
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankRecon.ddlMonth_SelectedIndexChanged", ex)
        End Try
       
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            Dim dal As New GeneralizedDAL
            If ddlYear.SelectedIndex > 0 Then
                ddltank.Visible = True

                Dim qry As String = " Select  [NUMBER] from Tank "
                Dim i As Integer = 0
                ds = dal.GetDataSet(qry)
                If Not ds.Tables(0).Rows(0)("NUMBER") Is DBNull.Value Then

                    ddltank.DataSource = ds.Tables(0).DefaultView
                    ddltank.DataTextField = "number"
                    ddltank.DataValueField = "Number"
                    ddltank.DataBind()

                    ddltank.Items.Insert(0, New ListItem("Please Select Tank", "0"))

                End If
            Else
                ddltank.Visible = False
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("TankRecon.ddlYear_SelectedIndexChanged", ex)
        End Try
    End Sub
End Class
