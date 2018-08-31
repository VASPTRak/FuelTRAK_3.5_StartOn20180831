Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO

<WebService(Namespace:="http://trakeng.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class KeyEncoderService
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly vehicleKeyLockObject As New Object()
    Private Shared ReadOnly personnelKeyLockObject As New Object()

    <WebMethod()> _
    Public Function Ping() As Boolean
        Return True
    End Function

    Public Enum KeyType
        Personnel
        Verhicle
    End Enum

    <WebMethod()> _
    Public Function GetVehicleInformation(ByVal vehicleId As String) As VehicleInformation

        'TODO: This work needs to be complede by adding the logic to generate a new key if necessary.
        'I attempted to derive teh required logic from eth existing logic for encoding a vehicle key
        'but the existing logic is very very broken

        Try

            'Dim sw As StreamWriter
            'Dim str1 As String = ""
            'Dim path As String = HttpContext.Current.Server.MapPath("TrakLogError.txt")
            'str1 = DateTime.Now()
            'If Not File.Exists(path) Then
            '    File.Create(path)
            'End If

            'sw = New StreamWriter(path, True)
            'sw.WriteLine("Date/Time----- " + str1 + "Error Message:---- Error Occured in:-")
            'sw.Flush()
            'sw.Close()

            Dim data As New VehicleInformation()
            Dim Id As String = vehicleId.ToString()

            Dim objSql As New GeneralizedDAL()
            Using conn As SqlConnection = objSql.GetsqlConn()

                Using cmd As SqlCommand = conn.CreateCommand



                    Dim sql As String = _
                    "SELECT [IDENTITY], LIMIT, MASTER, REQODOM, FUELS, SECOND_KEY, CURRMILES, MILES_WIND " & _
                    "FROM Vehs AS v WHERE v.[IDENTITY] = '" + Id + "'"

                    cmd.CommandText = sql
                    cmd.Parameters.Add(New SqlParameter("@Id", Id.Trim.ToString()))

                    conn.Open()
                    Dim ds As New DataSet
                    ds = objSql.GetDataSet(sql)
                    If (ds IsNot Nothing And ds.Tables(0).Rows.Count > 0) Then
                        With data
                            .Id = CStr(ds.Tables(0).Rows(0)("IDENTITY"))

                            If Not IsDBNull(ds.Tables(0).Rows(0)("LIMIT")) Then
                                Dim limit As String
                                limit = CStr(ds.Tables(0).Rows(0)("LIMIT"))
                                .FuelLimit = If(limit.Trim().ToUpper() = "NO", "000", limit)
                            Else
                                .FuelLimit = "000"
                            End If

                            .IsMaster = CBool(ds.Tables(0).Rows(0)("MASTER"))
                            .MileageEntryRequired = CBool(ds.Tables(0).Rows(0)("REQODOM"))
                            .FuelTypes = CStr(ds.Tables(0).Rows(0)("FUELS"))
                            .RequireSecondKey = CBool(ds.Tables(0).Rows(0)("SECOND_KEY"))
                            .Mileage = CDec(ds.Tables(0).Rows(0)("CURRMILES"))

                            If Not IsDBNull(ds.Tables(0).Rows(0)("MILES_WIND")) Then
                                .MileageWindow = CStr(ds.Tables(0).Rows(0)("MILES_WIND"))
                            Else
                                .MileageWindow = "0300"
                            End If
                        End With
                    End If
                    'Using reader As SqlDataReader = cmd.ExecuteReader()
                    '    If reader.HasRows Then
                    '        reader.Read() 'iterate forward to first (and only) record in reader
                    '        With data
                    '            .Id = CStr(reader.GetString(reader.GetOrdinal("IDENTITY")))
                    '            Dim limit As String = reader.GetString(reader.GetOrdinal("LIMIT"))
                    '            .FuelLimit = IIf(limit.Trim().ToUpper() = "NO", "000", limit)
                    '            .IsMaster = reader.GetBoolean(reader.GetOrdinal("MASTER"))
                    '            .MileageEntryRequired = reader.GetBoolean(reader.GetOrdinal("REQODOM"))
                    '            .FuelTypes = reader.GetString(reader.GetOrdinal("FUELS"))
                    '            .RequireSecondKey = reader.GetBoolean(reader.GetOrdinal("SECOND_KEY"))
                    '            ' RSmith 9/27/10
                    '            .Mileage = reader.GetDecimal(reader.GetOrdinal("CURRMILES")).ToString
                    '            .MileageWindow = reader.GetString(reader.GetOrdinal("MILES_WIND"))
                    '        End With
                    '    Else
                    '        Return Nothing
                    '    End If
                    'End Using
                End Using
            End Using

            data.SystemId = GetSystemNumber()

            Return data


        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("App_Code.KeyEncoderService.GetVehicleInformation()", ex)
            Return Nothing
        End Try

    End Function

    <WebMethod()> _
    Public Function GetPersonnelInformation(ByVal personnelId As String) As PersonnelInformation
        Try

            Dim Id As String = personnelId.ToString()


            Dim data As New PersonnelInformation()

            Dim objSql As New GeneralizedDAL()
            Using conn As SqlConnection = objSql.GetsqlConn()

                Using cmd As SqlCommand = conn.CreateCommand

                    Dim sql As String = _
                    "SELECT [IDENTITY], REQIDENTRY, FIRST_NAME, LAST_NAME " & _
                    "FROM Pers AS p " & _
                    "WHERE p.[IDENTITY] = '" + Id + "'"

                    cmd.CommandText = sql
                    cmd.Parameters.Add(New SqlParameter("@Id", personnelId))

                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read() 'iterate forward to first (and only) record in reader
                            With data
                                .Id = CStr(reader.GetString(reader.GetOrdinal("IDENTITY")))
                                .RequireSecondKey = reader.GetBoolean(reader.GetOrdinal("REQIDENTRY"))
                                .FirstName = reader.GetString(reader.GetOrdinal("FIRST_NAME"))
                                .LastName = reader.GetString(reader.GetOrdinal("LAST_NAME"))
                            End With
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using
            End Using

            data.SystemId = GetSystemNumber()

            Return data
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("App_Code.KeyEncoderService.GetPersonnelInformation()", ex)
        End Try

    End Function

    <WebMethod()> _
    Public Function GetVehicleKeyId(ByVal vehicleId As String) As String

        Try

            Dim Id As String = vehicleId.ToString()

            Dim objSql As New GeneralizedDAL()

            Dim parcollection(0) As SqlParameter
            parcollection(0) = New SqlParameter("@id", vehicleId)

            Dim sql As String = _
                  "SELECT key_number FROM Vehs WHERE [IDENTITY] = '" + Id + "'"

            Dim existingKey As String = objSql.ExecuteScalarGetString(sql, parcollection)


            If String.IsNullOrEmpty(existingKey) OrElse existingKey = "*NEW*" OrElse existingKey = "00000" OrElse IsKeyLocked(existingKey, True) Then
                Return GenerateNewVehicleKeyAndAssign(vehicleId)
            Else
                Return existingKey
            End If

        Catch ex As Exception
            Dim cr As New ErrorPage
            Dim errmsg As String
            cr.errorlog("KeyEncoderService.GetvehicleKeyID", ex)
            If ex.Message.Contains(";") Then
                errmsg = ex.Message.ToString()
                errmsg = errmsg.Substring(0, errmsg.IndexOf(";"))
            Else
                errmsg = ex.Message.ToString()
            End If
        End Try

    End Function

    Public Function IsKeyLocked(ByVal key As String, ByVal IsVehicleKey As Boolean) As Boolean
        Try

            Dim keyId As String = key.ToString()
            Dim IsVehicleKeyID As Boolean = IsVehicleKey
            Dim type As String = If(IsVehicleKey, "V", "P")

            Dim parcollection(1) As SqlParameter
            parcollection(0) = New SqlParameter("@keyNumber", key)
            parcollection(1) = New SqlParameter("@type", If(IsVehicleKey, "V", "P"))

            Dim sql As String = "Select COUNT(*) FROM KeyLock WHERE Key_Number ='" + keyId + "' AND [type] = '" + type + "'"

            Return New GeneralizedDAL().ExecuteScalarGetInteger(sql, parcollection) > 0
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("App_Code.KeyEncoderService.IsKeyLocked()", ex)
        End Try
    End Function

    <WebMethod()> _
    Public Function GetPersonnelKeyId(ByVal personnelId As String) As String
        Try

            Dim Id As String = personnelId.ToString()
            Dim objSql As New GeneralizedDAL()

            Dim parcollection(0) As SqlParameter
            parcollection(0) = New SqlParameter("@id", personnelId)

            Dim sql As String = _
                 "SELECT key_number FROM Pers WHERE [IDENTITY] = '" + Id + "'"

            Dim existingKey As String = objSql.ExecuteScalarGetString(sql, parcollection)

            If String.IsNullOrEmpty(existingKey) OrElse existingKey = "*NEW*" OrElse existingKey = "00000" OrElse IsKeyLocked(existingKey, False) Then
                Return GenerateNewPersonnelKeyAndAssign(personnelId)
            Else
                Return existingKey
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("App_Code.KeyEncoderService.GetPersonnelKeyId()", ex)
        End Try
    End Function

    Private Function GetSystemNumber() As String
        Try


            ' This code is taken almost directly from Vehicle_Edit.aspx.vb 
            ' Why is a substring needed to chop off the first character? is it really not ever used?
            Dim sysno As String
            sysno = New GeneralizedDAL().ExecuteScalarGetString("select sysno from status")
            'Return sysno.Substring(1)'It's a bug, we don't need to substring Sysno.VMoota(06/06/2012)
            Return sysno
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("App_Code.KeyEncoderService.GetSystemNumber()", ex)
        End Try
    End Function

    Private Function GenerateNewVehicleKeyAndAssign(ByVal vehicleId As String) As String
        Try

            Dim Id As String = vehicleId.ToString()

            'locking on this operation to ensure 2 threads do not end up assigning the same id to 2 vehicles
            SyncLock vehicleKeyLockObject
                Dim objSql As New GeneralizedDAL()

                Dim parcollection(0) As SqlParameter
                parcollection(0) = New SqlParameter("@id", vehicleId)


                Dim sql As String = "UPDATE Vehs SET key_number =(SELECT RIGHT('00000' + LTRIM(STR((SELECT Max((CASE KEY_NUMBER WHEN '*NEW*' then '00000' else KEY_NUMBER end)  + 1)  FROM Vehs))), 5)) WHERE [identity] = '" + Id + "' ; SELECT key_number FROM Vehs WHERE [IDENTITY] = '" + Id + "'"

                'Return objSql.ExecuteScalarGetString("UPDATE Vehs SET key_number =(SELECT RIGHT('00000' + LTRIM(STR((SELECT MAX(KEY_NUMBER) + 1 FROM Vehs))), 5)) WHERE [identity] = @id;SELECT key_number FROM Vehs WHERE [IDENTITY] = @id", parcollection)

                'Added By Varun Moota  Modified above SQL Statement. 04/07/2010

                Return objSql.ExecuteScalarGetString(sql, parcollection)
            End SyncLock
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("App_Code.KeyEncoderService.GenerateNewVehicleKeyAndAssign()", ex)
        End Try
    End Function

    Private Function GenerateNewPersonnelKeyAndAssign(ByVal personnelId As String) As String
        Try

            Dim Id As String = personnelId.ToString()
            'locking on this operation to ensure 2 threads do not end up assigning the same id to 2 vehicles
            SyncLock personnelKeyLockObject
                Dim objSql As New GeneralizedDAL()

                Dim parcollection(0) As SqlParameter
                parcollection(0) = New SqlParameter("@id", personnelId)

                Dim sql As String = "UPDATE Pers SET key_number =(SELECT RIGHT('00000' + LTRIM(STR((SELECT MAX((CASE KEY_NUMBER WHEN '*NEW*' then '00000'else KEY_NUMBER end))+ 1 FROM Pers))), 5)) WHERE [identity] = '" + Id + "' ; SELECT key_number FROM Pers WHERE [IDENTITY] = '" + Id + "'"

                Return objSql.ExecuteScalarGetString(sql, parcollection)
            End SyncLock
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("App_Code.KeyEncoderService.GenerateNewPersonnelKeyAndAssign()", ex)
        End Try
    End Function

End Class

<Serializable()> _
Public Class VehicleInformation

    Private _vehicleId As String
    Private _systemId As String
    Private _fuelLimit As String

    Private _master As Boolean
    Private _mileageRequired As Boolean
    Private _secondKey As Boolean

    Private _fuels As String

    ' RSmith 9/27/10
    Private _mileage As String
    Private _mileageWindow As String

    Public Property Id() As String
        Get
            Return _vehicleId
        End Get
        Set(ByVal value As String)
            _vehicleId = value
        End Set
    End Property

    Public Property SystemId() As String
        Get
            Return _systemId
        End Get
        Set(ByVal value As String)
            _systemId = value
        End Set
    End Property

    Public Property FuelLimit() As String
        Get
            Return _fuelLimit
        End Get
        Set(ByVal value As String)
            _fuelLimit = value
        End Set
    End Property

    Public Property IsMaster() As Boolean
        Get
            Return _master
        End Get
        Set(ByVal value As Boolean)
            _master = value
        End Set
    End Property

    Public Property RequireSecondKey() As Boolean
        Get
            Return _secondKey
        End Get
        Set(ByVal value As Boolean)
            _secondKey = value
        End Set
    End Property

    Public Property MileageEntryRequired() As Boolean
        Get
            Return _mileageRequired
        End Get
        Set(ByVal value As Boolean)
            _mileageRequired = value
        End Set
    End Property

    Public Property FuelTypes() As String
        Get
            Return _fuels
        End Get
        Set(ByVal value As String)
            _fuels = value
        End Set
    End Property

    Public Property Mileage() As String
        Get
            Return _mileage
        End Get
        Set(ByVal value As String)
            _mileage = value
        End Set
    End Property

    Public Property MileageWindow() As String
        Get
            Return _mileageWindow
        End Get
        Set(ByVal value As String)
            _mileageWindow = value
        End Set
    End Property
End Class


<Serializable()> _
Public Class PersonnelInformation

    Private _personnelId As String
    Private _systemId As String
    Private _secondKey As Boolean
    Private _firstName As String
    Private _lastName As String

    Public Property Id() As String
        Get
            Return _personnelId
        End Get
        Set(ByVal value As String)
            _personnelId = value
        End Set
    End Property

    Public Property SystemId() As String
        Get
            Return _systemId
        End Get
        Set(ByVal value As String)
            _systemId = value
        End Set
    End Property

    Public Property RequireSecondKey() As Boolean
        Get
            Return _secondKey
        End Get
        Set(ByVal value As Boolean)
            _secondKey = value
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return _firstName
        End Get
        Set(ByVal value As String)
            _firstName = value
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return _lastName
        End Get
        Set(ByVal value As String)
            _lastName = value
        End Set
    End Property

End Class
