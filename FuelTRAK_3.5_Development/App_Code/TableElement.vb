Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Drawing

Public Class TableElement

    Public Sub TableElement()
    End Sub

    ' TODO: Add constructor logic here

    Public Function GetTableRow() As TableRow
        Dim Row As TableRow = New TableRow()
        Row.Width = Unit.Percentage(100)
        Row.BorderWidth = Unit.Pixel(1)
        Row.BorderColor = Color.SteelBlue
        Return Row
    End Function

    Public Function GetTableCell(ByVal Align As String, ByVal Text As String, ByVal IsHeaderCell As Boolean) As TableCell

        Dim Cell As TableCell = New TableCell()
        If Text.Trim() = "" Or Text.Trim() = " " Then
            Text = "-"
        End If
        Cell.Text = Text
        Cell.Font.Name = "Verdana"
        Cell.Font.Size = FontUnit.Small
        Cell.ForeColor = Color.Black
        If (IsHeaderCell) Then
            Cell.Font.Bold = True
            Cell.Font.Size = FontUnit.Small
            Cell.Width = Unit.Percentage(50)
        End If
        If (Align = "Left") Then
            Cell.HorizontalAlign = HorizontalAlign.Left
        ElseIf (Align = "Right") Then
            Cell.HorizontalAlign = HorizontalAlign.Right
        ElseIf (Align = "Center") Then
            Cell.HorizontalAlign = HorizontalAlign.Center
        Else
            Cell.HorizontalAlign = HorizontalAlign.Justify
        End If

        Cell.BorderStyle = BorderStyle.Solid
        Cell.BorderWidth = Unit.Pixel(1)
        Cell.BorderColor = Color.SteelBlue

        Return Cell
    End Function

End Class
