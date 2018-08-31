Imports System.Data
Imports System.Data.SqlClient

Partial Class Key_Encoder
    Inherits System.Web.UI.Page

    Dim str_Connection_string As String = IIf(ConfigurationManager.AppSettings("ConnectionString").StartsWith(","), ConfigurationManager.AppSettings("ConnectionString").Substring(1, ConfigurationManager.AppSettings("ConnectionString").Length - 1), ConfigurationSettings.AppSettings("ConnectionString").ToString())


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim null As System.DBNull
            If Session("User_name") Is Nothing Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>chksesion()</script>")
            End If
            'Added By Varun Moota To get Comm Value. 02/05/2010
            GetCommValue()

            'Added by Harshada
            '29 Apr 09
            'Make all fields read only as in WinCC
            'Issue No #92 to #98


            'Added By Varun Moota to access Session Values
            ' ''If txt1.Text = "" Or String.Empty Then
            ' ''    Session("KeyType") = String.Empty

            ' ''    Session.Remove("KeyNumber")
            ' ''    Session.Remove("PID")
            ' ''    Session.Remove("KeyExp")
            ' ''    Session.Remove("SysNo")
            ' ''    Session.Remove("SecondKey")


            ' ''    Session.Remove("KeyNumber")
            ' ''    Session.Remove("VID")
            ' ''    Session.Remove("KeyExp")
            ' ''    Session.Remove("SysNo")
            ' ''    Session.Remove("Master")
            ' ''    Session.Remove("MileageReq")
            ' ''    Session.Remove("Option")
            ' ''    Session.Remove("FLimit")
            ' ''    Session.Remove("Mileage")


            ' ''    txt1.Text = ""
            ' ''    txt2.Text = ""
            ' ''    txt3.Text = ""
            ' ''    txt4.Text = ""
            ' ''    txt5.Text = ""
            ' ''    txt6.Text = ""
            ' ''    txt7.Text = ""
            ' ''    txt8.Text = ""
            ' ''    txt9.Text = ""
            ' ''End If





            txt1.Attributes.Add("Readonly", "Readonly")
            txt2.Attributes.Add("Readonly", "Readonly")
            txt3.Attributes.Add("Readonly", "Readonly")
            txt4.Attributes.Add("Readonly", "Readonly")
            txt5.Attributes.Add("Readonly", "Readonly")
            txt6.Attributes.Add("Readonly", "Readonly")
            txt7.Attributes.Add("Readonly", "Readonly")
            txt8.Attributes.Add("Readonly", "Readonly")
            txt9.Attributes.Add("Readonly", "Readonly")
            txt10.Attributes.Add("Readonly", "Readonly")

            If Not Session("KeyType") Is Nothing Then
                If Session("KeyType").ToString() = "0" Then 'VEHICLEKEY
                    Label8.Text = "Vehicle Key Encoder"
                    txt6.Visible = True
                    txt7.Visible = True
                    txt8.Visible = True
                    txt9.Visible = True
                    lbl6.Visible = True
                    lbl7.Visible = True
                    lbl8.Visible = True
                    lbl9.Visible = True
                    txt10.Text = "Vehicle"
                    txt1.Text = Session("KeyNumber")
                    txt2.Text = Session("VID")
                    txt3.Text = Session("KeyExp")
                    txt4.Text = Session("SysNo").ToString().PadLeft(4, "0")
                    txt5.Text = Session("Master")
                    txt6.Text = Session("MileageReq")
                    txt7.Text = Session("Option")
                    txt8.Text = Session("FLimit")
                    txt9.Text = Session("Mileage")

                    lbl1.Text = "Key Number :"
                    lbl2.Text = "Vehicle ID :"
                    lbl3.Text = "Key Expiration Date :"
                    lbl4.Text = "System Number :"
                    lbl5.Text = "Master field :"
                    lbl6.Text = "Mileage Required :"
                    lbl7.Text = "Option Required :"
                    lbl8.Text = "Fuel Limit :"
                    lbl9.Text = "Mileage :"
                    txtGKey.Value = BuildKeyString(0, txt4.Text.Substring(1), txt1.Text, txt2.Text, IIf(Session("Secondkey") = "Yes", True, False), False, IIf(txt7.Text = "True", True, False), txt8.Text.Trim, Session("Prods"), IIf(txt5.Text = "True", True, False))

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>BtnHide('1')</script>")



                ElseIf Session("KeyType").ToString() = "1" Then 'PERSONNELKEY
                    Label8.Text = "Personnel Key Encoder"

                    txt1.Text = Session("KeyNumber")
                    txt2.Text = Session("PID")
                    txt3.Text = Session("KeyExp")
                    txt4.Text = Session("SysNo").ToString().PadLeft(4, "0")
                    txt5.Text = Session("SecondKey")
                    txt10.Text = "Personnel"
                    lbl1.Text = "Key Number :"
                    lbl2.Text = "Personnel ID :"
                    lbl3.Text = "Key Expiration Date :"
                    lbl4.Text = "System Number :"
                    lbl5.Text = "Second Key :"
                    lbl6.Visible = False
                    lbl7.Visible = False
                    lbl8.Visible = False
                    lbl9.Visible = False
                    txt6.Visible = False
                    txt7.Visible = False
                    txt8.Visible = False
                    txt9.Visible = False
                    txtGKey.Value = BuildKeyString(1, txt4.Text.Substring(1), txt1.Text, txt2.Text, IIf(lbl5.Text = "Yes", True, False), False, False, "", "", False)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>BtnHide('1')</script>")
                    'BuildKeyString(0, txt4.Text.Substring(1), txt1.Text, txt2.Text, IIf(Session("Secondkey") = "Yes", True, False), False, IIf(txt7.Text = "True", True, False), txt8.Text.Trim, Session("Prods"), IIf(txt5.Text = "True", True, False))
                    'BuildKeyString(1, txt4.Text.Substring(1), txt1.Text, txt2.Text, IIf(lbl5.Text = "Yes", True, False), False, False, "", "", False)
                Else
                    lbl1.Text = "Key Number :"
                    lbl2.Text = "Identity :"
                    lbl3.Text = "Key Expires :"
                    lbl4.Text = "System Number :"
                    lbl5.Text = "Master field :"
                    lbl6.Text = "Require Mileage :"
                    lbl7.Text = "Ask For Option :"
                    lbl8.Text = "Fuel Limit :"
                    lbl9.Text = "Current Odometer :"
                    Label8.Text = "Read Key"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>BtnHide('2')</script>")
                End If
            Else

                lbl1.Text = "Key Number :"
                lbl2.Text = "Identity :"
                lbl3.Text = "Key Expires :"
                lbl4.Text = "System Number :"
                lbl5.Text = "Master field :"
                lbl6.Text = "Require Mileage :"
                lbl7.Text = "Ask For Option :"
                lbl8.Text = "Fuel Limit :"
                lbl9.Text = "Current Odometer :"
                'downLink.Attributes.CssStyle.Add("display", "none")

                Label8.Text = "Read Key"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>BtnHide('2')</script>")
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Key_Encoder.Page_load", ex)
        End Try
    End Sub
    Public Sub GetCommValue()
        Try
            Dim ds As New DataSet
            Dim dal = New GeneralizedDAL()
            Dim mcName As String
            mcName = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName 'Request.UserHostName.ToString()
            Session("MachineName") = mcName

            ds = dal.GetDataSet("Select COMMPORT FROM COMM WHERE MachineName = '" + mcName + "'")

            If ds.Tables(0).Rows.Count > 0 Then
                Session("COMM") = ds.Tables(0).Rows(0)("COMMPORT").ToString()

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('No COMM Port Value Exists.');location.href='Home.aspx';</script>")

            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Key_Encoder.GetCommValue()", ex)
        End Try

    End Sub

    Public Function BuildKeyString(ByVal keytype As Integer, ByVal SystemNumber As String, ByVal KeyNumber As String, ByVal identity As String, Optional ByVal SecondKey As Boolean = False, Optional ByVal ReqOdom As Boolean = False, Optional ByVal ReqOption As Boolean = False, Optional ByVal limit As String = "200", Optional ByVal fuels As String = "YYYYYYYYYYYYYYY", Optional ByVal Master As Boolean = False)
        Dim KeyString As String
        Try

      
            'This function will take in values from a vehicle record and return the string 
            ' that the keyreader needs to write the new data to a TRAK key.

            'the first few fields are the same in all keys
            'first 5 characters are the key number, padded left with zeros
            KeyString = KeyNumber.PadLeft(5, "0")
            'second 10 characters are the vehicle number, padded left with spaces
            KeyString = KeyString + identity.PadRight(10)
            'key type indicator is 1 character
            KeyString = KeyString + keytype.ToString

            'Added By Varun Moota to Have Mileage Set Properly.03/02/2010
            If txt6.Text = "Yes" Then
                ReqOdom = True
            Else
                ReqOdom = False
            End If

            Select Case keytype
                Case 0
                    'for vehicles, we can require a second key
                    KeyString = KeyString + IIf(SecondKey, "1", "0")
                    'Require Mileage?
                    KeyString = KeyString + IIf(ReqOdom, "1", "0") '
                    'Request option data?
                    KeyString = KeyString + IIf(ReqOption, "1", "0")
                    ' system number padded left with zeros to 3
                    KeyString = KeyString + SystemNumber.PadLeft(3, "0")
                    KeyString = KeyString + limit.PadLeft(3, "0")
                    KeyString = KeyString + "0000"
                    ' Is this a master key?
                    KeyString = KeyString + IIf(Master, "1", "0")
                    ' the fuel string is stored as Y's and N's. The key reader wants to see 0's and 1's. 
                    fuels = fuels.Replace("Y", "1")
                    fuels = fuels.Replace("N", "0")
                    KeyString = KeyString + fuels.PadRight(15, "0")
                    'one last zero for spaceholder
                    KeyString = KeyString + "0"
                Case 1
                    'personnel key can require the user to enter the personnel number. We can reuse
                    ' the secondkey option for this. 
                    KeyString = KeyString + IIf(SecondKey, "1", "0")
                    'The next two options are not applicable
                    KeyString = KeyString + "00"
                    ' system number padded left with zeros to 4
                    KeyString = KeyString + SystemNumber.PadLeft(3, "0")
                    'end with a string of 24 zeros to make the string the right length. 
                    KeyString = KeyString + Space(24)
            End Select
            BuildKeyString = "!1<E" + KeyString + ">"

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("Key_Encoder.BuildKeyString()", ex)
        End Try

    End Function

    Protected Sub btnReadKey1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReadKey1.Click
        Dim SqlConn As SqlConnection = New SqlConnection(str_Connection_string)
        Dim str_Query As String = ""
        Try

            If txt10.Text = "Vehicle" Then
                If txt6.Text = "No" Then 'hours
                    lbl9.Text = "Current Hours :"
                    'str_Query = "SELECT CURRHOURS FROM VEHS WHERE [IDENTITY]= '" + txt2.Text.Trim() + "'"

                    Dim ds As New DataSet
                    Dim da = New GeneralizedDAL()
                    ds = da.GetDataSet("SELECT CURRHOURS FROM VEHS WHERE [IDENTITY]= '" + txt2.Text.Trim() + "'")


                    If ds.Tables(0).Rows.Count > 0 Then
                        txt9.Text = ds.Tables(0).Rows(0).Item(0).ToString()
                    End If

                ElseIf txt6.Text = "Yes" Then 'mileage
                    lbl9.Text = "Current Odometer :"
                    'str_Query = "SELECT CURRMILES FROM VEHS WHERE [IDENTITY]= '" + txt2.Text.Trim() + "'"

                    Dim ds As New DataSet
                    Dim da = New GeneralizedDAL()
                    ds = da.GetDataSet("SELECT CURRMILES FROM VEHS WHERE [IDENTITY]= '" + txt2.Text.Trim() + "'")


                    If ds.Tables(0).Rows.Count > 0 Then
                        txt9.Text = ds.Tables(0).Rows(0).Item(0).ToString()
                    End If
                End If

                'Added By Varun Moota. 02/12/2010
                lbl6.Visible = True
                lbl7.Visible = True
                lbl8.Visible = True
                lbl9.Visible = True
                txt6.Visible = True
                txt7.Visible = True
                txt8.Visible = True
                txt9.Visible = True



                'Commented By Varun Moota.
                '' ''Dim sqlReader As SqlDataReader
                ' ''Dim sda As SqlDataAdapter
                ' ''Dim ds As New DataSet()
                '' ''Dim sqlReader As SqlDataReader
                ' ''Dim sqlCmd As SqlCommand = New SqlCommand(str_Query, SqlConn)
                '' ''SqlConn.Open()
                ' ''sda = New SqlDataAdapter(sqlCmd)
                ' ''sda.Fill(ds)
                ' ''If ds.Tables(0).Rows.Count > 0 Then
                ' ''    txt9.Text = ds.Tables(0).Rows(0).Item(0).ToString()
                ' ''End If


                'Commented By Varun
            Else
                lbl6.Visible = False
                lbl7.Visible = False
                lbl8.Visible = False
                lbl9.Visible = False
                txt6.Visible = False
                txt7.Visible = False
                txt8.Visible = False
                txt9.Visible = False
            End If


            'downLink.Attributes.CssStyle.Add("display", "")
        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String

            cr.errorlog("btnReadKey1_Click", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
            'Response.Redirect("Error1.aspx?Err=" + errmsg + "btnReadKey1_Click", False)

        Finally
            If (SqlConn.State = ConnectionState.Open = True) Then
                SqlConn.Close()
            End If
        End Try

    End Sub

    Protected Sub btnWriteKey_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWriteKey.ServerClick
        Try

            If Not textResult.Value.Trim() = "Error" Then
                Dim dal As New GeneralizedDAL()
                Dim parcollection(2) As SqlParameter
                Dim ParID = New SqlParameter("@ID", SqlDbType.Int)
                Dim ParKeyNo = New SqlParameter("@KEY", SqlDbType.VarChar, 5)
                Dim ParFlag = New SqlParameter("@Flag", SqlDbType.VarChar, 1)

                ParID.Direction = ParameterDirection.Input
                ParKeyNo.Direction = ParameterDirection.Input
                ParFlag.Direction = ParameterDirection.Input
                ParKeyNo.Value = txt1.Text.Trim()
                If Session("KeyType").ToString() = "0" Then
                    ParFlag.Value = "V"
                    ParID.Value = Session("VEHID")

                ElseIf Session("KeyType").ToString() = "1" Then
                    ParFlag.Value = "P"
                    ParID.Value = Session("PersID")

                End If
                parcollection(0) = ParID
                parcollection(1) = ParKeyNo
                parcollection(2) = ParFlag
                Dim blnKey As Boolean = dal.ExecuteStoredProcedureGetBoolean("usp_tt_VEHUpdateKeyIDENTITY", parcollection)

                If blnKey = True Then
                    If Session("KeyType").ToString() = "0" Then
                        Session.Remove("KeyType")
                        Response.Redirect("Vehicle.aspx", False)
                    ElseIf Session("KeyType").ToString() = "1" Then
                        Session.Remove("KeyType")
                        Response.Redirect("Personnel.aspx", False)
                    End If

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Key has been written successfully.');location.href='Key_Encoder.aspx?R=1';</script>")

                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "javascript", "<script>alert('Bad write.');</script>")

            End If


            'Added By Varun Moota to Clear Session Variables.02/09/2010

            Dim istring As String
            Dim iSession As Int32
            If Session("KeyType").ToString() = "0" Then
                iSession = CInt(Session("KeyType"))
                istring = Session("VEHID").ToString()
            ElseIf Session("KeyType").ToString() = "1" Then
                iSession = CInt(Session("KeyType"))
                istring = Session("PID").ToString()

            End If

            ClearSession()
            If iSession = "0" Then
                Response.Redirect("Vehicle.aspx", False)

            End If

            If iSession = "1" Then
                Response.Redirect("Personnel.aspx", False)

            End If



        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("btnWriteKey_ServerClick", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try
    End Sub
    Public Sub ClearSession()

        Try

            'Added By Varun to Clear once Successfully Written
            If Session("KeyType").ToString() = "0" Or "1" Then
                Session.Remove("KeyType")

            End If


            Session.Remove("KeyNumber")
            Session.Remove("PID")
            Session.Remove("KeyExp")
            Session.Remove("SysNo")
            Session.Remove("SecondKey")


            Session.Remove("KeyNumber")
            Session.Remove("VID")
            Session.Remove("KeyExp")
            Session.Remove("SysNo")
            Session.Remove("Master")
            Session.Remove("MileageReq")
            Session.Remove("Option")
            Session.Remove("FLimit")
            Session.Remove("Mileage")


            txt1.Text = ""
            txt2.Text = ""
            txt3.Text = ""
            txt4.Text = ""
            txt5.Text = ""
            txt6.Text = ""
            txt7.Text = ""
            txt8.Text = ""
            txt9.Text = ""
            txt10.Text = ""

        Catch ex As Exception
            Dim cr As New ErrorPage

            cr.errorlog("Key_encoder.ClearSession", ex)
        End Try
    End Sub
End Class
