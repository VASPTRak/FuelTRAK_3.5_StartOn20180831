<%@ WebHandler Language="VB" Class="PollingInfo" %>

Imports System
Imports System.Web
Imports System.Collections.Generic
Imports System.Data.SqlClient

'Edit by Omar -- this handler is used by the ajax polling code, return json formatted data 
Public Class PollingInfo : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        Dim pollingKey As String = context.Request.Params("pollingKey")
        If String.IsNullOrEmpty(pollingKey) Then
            context.Response.End()
        End If
        
        Dim status As String = String.Empty
        Dim messages As New List(Of String)()
        Dim objSql As New GeneralizedDAL()
        Using conn As SqlConnection = objSql.GetsqlConn()

            Using cmd As SqlCommand = conn.CreateCommand

                Dim sql As String = _
                "SELECT q.DeviceID, q.Status, l.Message, l.Id " & _
                "FROM PollingQueue AS q " & _
                "LEFT OUTER JOIN PollingLog AS l " & _
                "ON q.TimePollingStarted <= l.Datetime " & _
                "AND q.TimePollingCompleted >= l.Datetime " & _
                "WHERE     q.Id = @Id " & _
                "ORDER BY l.Datetime "

                cmd.CommandText = sql
                cmd.Parameters.Add(New SqlParameter("@Id", pollingKey))

                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While (reader.Read)
                        status = reader("status").ToString().ToLower()
                        If Not reader("Message") Is DBNull.Value Then
                            messages.Add(" { ""text"": """ & reader("Message").ToString().Replace(Environment.NewLine, "") & """, ""id"": """ & reader("Id").ToString() & """ } ")
                        End If
                    End While
                End Using
            End Using
        End Using

        Dim jsonResponse As New StringBuilder
        jsonResponse.Append("{ ")
        jsonResponse.Append("""status"": """)
        jsonResponse.Append(status)
        jsonResponse.Append(""",")
        
        jsonResponse.Append("""messages"": [")
        jsonResponse.Append(String.Join(",", messages.ToArray()))        
        jsonResponse.Append(" ]")
        jsonResponse.Append(" }")
        
        context.Response.Clear()
        context.Response.ContentType = "application/json"
        context.Response.Write(jsonResponse.ToString())
        context.Response.Flush()
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class