Public Class UserInfo

#Region "Private Members"
    Private _userid As Integer
    Private _username As String
    Private _password As String
    Private _Edituserid As Integer
#End Region

#Region "Private Members for Reports"
    'Reports
    Private _reportID As Integer
    Private _startDate As DateTime
    Private _endDate As DateTime
    Private _startVehicle As String
    Private _endVehicle As String
    Private _startDept As String
    Private _endDept As String
    Private _startSentry As String
    Private _endSentry As String
    Private _startVehType As String
    Private _endVehType As String
    Private _startTank As String
    Private _endTank As String
    Private _startVKey As String
    Private _endVKey As String
    Private _startPer As String
    Private _endPer As String
    Private _startPerID As String
    Private _endPerID As String
    Private _reportTitle As String
    Private _chkStatus As Boolean
    Private _reportHeader As String
    Private _startSPN As String
    Private _endSPN As String
    Private _TransMPG As String
#End Region

#Region "DataMembers"

    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal Value As String)
            _password = Value
        End Set
    End Property

    Public Property Username() As String
        Get
            Return _username
        End Get
        Set(ByVal Value As String)
            _username = Value
        End Set
    End Property

    Public Property UserID() As Integer
        Get
            Return _userid
        End Get
        Set(ByVal Value As Integer)
            _userid = Value
        End Set
    End Property

    Public Property EditUserID() As Integer
        Get
            Return _Edituserid
        End Get
        Set(ByVal Value As Integer)
            _Edituserid = Value
        End Set
    End Property

#End Region

#Region "Report Data Members"
    Public Property ReportHeader() As String
        Set(ByVal value As String)
            _reportHeader = value
        End Set
        Get
            Return _reportHeader
        End Get
    End Property

    Public Property ReportTitle() As String
        Set(ByVal value As String)
            _reportTitle = value
        End Set
        Get
            Return _reportTitle
        End Get
    End Property

    Public Property ReportID() As Integer
        Set(ByVal value As Integer)
            _reportID = value
        End Set
        Get
            Return _reportID
        End Get
    End Property

    Public ReadOnly Property ReportComment() As String
        Get
            Dim dtObject As Object = _startDate
            Dim comment As String = "FROM " & Format(dtObject, "MM/dd/yyyy HH:mm")
            dtObject = _endDate
            comment += " TO " & Format(dtObject, "MM/dd/yyyy HH:mm")
            Return comment
        End Get
    End Property

    Public Property StartDate() As Date
        Set(ByVal value As Date)
            _startDate = value
        End Set
        Get
            Return _startDate
        End Get
    End Property

    Public Property EndDate() As Date
        Set(ByVal value As Date)
            _endDate = value
        End Set
        Get
            Return _endDate
        End Get
    End Property

    Public Property StartVehicle() As String
        Set(ByVal value As String)
            _startVehicle = value
        End Set
        Get
            Return _startVehicle
        End Get
    End Property

    Public Property EndVehicle() As String
        Set(ByVal value As String)
            _endVehicle = value
        End Set
        Get
            Return _endVehicle
        End Get
    End Property

    Public Property StartDepartment() As String
        Set(ByVal value As String)
            _startDept = value
        End Set
        Get
            Return _startDept
        End Get
    End Property

    Public Property EndDepartment() As String
        Set(ByVal value As String)
            _endDept = value
        End Set
        Get
            Return _endDept
        End Get
    End Property

    Public Property StartSentry() As String
        Set(ByVal value As String)
            _startSentry = value
        End Set
        Get
            Return _startSentry
        End Get
    End Property

    Public Property EndSentry() As String
        Set(ByVal value As String)
            _endSentry = value
        End Set
        Get
            Return _endSentry
        End Get
    End Property

    Public Property StartVehType() As String
        Set(ByVal value As String)
            _startVehType = value
        End Set
        Get
            Return _startVehType
        End Get
    End Property

    Public Property EndVehicleType() As String
        Set(ByVal value As String)
            _endVehType = value
        End Set
        Get
            Return _endVehType
        End Get
    End Property

    Public Property StartTank() As String
        Set(ByVal value As String)
            _startTank = value
        End Set
        Get
            Return _startTank
        End Get
    End Property

    Public Property EndTank() As String
        Set(ByVal value As String)
            _endTank = value
        End Set
        Get
            Return _endTank
        End Get
    End Property

    Public Property StartVKey() As String
        Set(ByVal value As String)
            _startVKey = value
        End Set
        Get
            Return _startVKey
        End Get
    End Property

    Public Property EndVKey() As String
        Set(ByVal value As String)
            _endVKey = value
        End Set
        Get
            Return _endVKey
        End Get
    End Property

    Public Property StartPer() As String
        Set(ByVal value As String)
            _startPer = value
        End Set
        Get
            Return _startPer
        End Get
    End Property

    Public Property EndPer() As String
        Set(ByVal value As String)
            _endPer = value
        End Set
        Get
            Return _endPer
        End Get
    End Property


    Public Property StartPerID() As String
        Set(ByVal value As String)
            _startPerID = value
        End Set
        Get
            Return _startPerID
        End Get
    End Property

    Public Property EndPerID() As String
        Set(ByVal value As String)
            _endPerID = value
        End Set
        Get
            Return _endPerID
        End Get
    End Property
    Public Property StartSPN() As String
        Set(ByVal value As String)
            _startSPN = value
        End Set
        Get
            Return _startSPN
        End Get
    End Property

    Public Property EndSPN() As String
        Set(ByVal value As String)
            _endSPN = value
        End Set
        Get
            Return _endSPN
        End Get
    End Property

    Public Property TransMPG() As String
        Set(ByVal value As String)
            _TransMPG = value
        End Set
        Get
            Return _TransMPG
        End Get
    End Property

    Public Property CheckBoxStatus() As Boolean
        Set(ByVal value As Boolean)
            _chkStatus = value
        End Set
        Get
            Return _chkStatus
        End Get
    End Property
#End Region
End Class
