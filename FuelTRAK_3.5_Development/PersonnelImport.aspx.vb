Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Data.SqlClient

Partial Class PersonnelImport
    Inherits System.Web.UI.Page

    Dim ds As DataSet
    Dim i As Integer
    Dim HtmChk As HtmlInputCheckBox
    Dim Txt As TextBox

    Public Sub HideDisplay(ByVal strName As String)
        Try

       
            If strName = "CSV" Then
                TDFixed1.Visible = False
                TDFixed2.Visible = False
                TDFixed3.Visible = False
                TDFixed4.Visible = False
                TDCSV1.Visible = True
                TDCSV2.Visible = True
                TDCSV3.Visible = True
            Else
                TDCSV1.Visible = False
                TDCSV2.Visible = False
                TDCSV3.Visible = False
                TDFixed1.Visible = True
                TDFixed2.Visible = True
                TDFixed3.Visible = True
                TDFixed4.Visible = True
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PersonnelImport.HideDisplay", ex)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        
            HideDisplay(DropFileType.SelectedValue)

            ds = BindControl(DropFileType.SelectedValue)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        If (ds.Tables(0).Rows(i)("Type").Equals("CSV")) And DropFileType.SelectedValue = "CSV" Then
                            If Not ds.Tables(0).Rows(i)("FieldName") Is Nothing Then
                                If Not ds.Tables(0).Rows(i)("FieldName").ToString() = "" Then
                                    HtmChk = CType(Page.FindControl("Chk" & ds.Tables(0).Rows(i)("FieldName")), HtmlInputCheckBox)
                                    HtmChk.Checked = True
                                    Txt = CType(Page.FindControl("CSV" & ds.Tables(0).Rows(i)("FieldName")), TextBox)
                                    Txt.Text = ds.Tables(0).Rows(i)("Fieldpos")
                                End If
                                If i = 0 Then
                                    CheckFlag(ds.Tables(0).Rows(i)("CheckFlag").ToString())
                                End If
                            End If
                        ElseIf (ds.Tables(0).Rows(i)("Type").Equals("Fixed")) And DropFileType.SelectedValue = "FIXED" Then
                            If Not ds.Tables(0).Rows(i)("startpos") Is Nothing Then
                                If Not ds.Tables(0).Rows(i)("startpos").ToString() = "" Then
                                    HtmChk = CType(Page.FindControl("Chk" & ds.Tables(0).Rows(i)("FieldName")), HtmlInputCheckBox)
                                    HtmChk.Checked = True
                                    Txt = CType(Page.FindControl("FixedS" & ds.Tables(0).Rows(i)("FieldName")), TextBox)
                                    Txt.Text = ds.Tables(0).Rows(i)("startpos")
                                    Txt = CType(Page.FindControl("FixedL" & ds.Tables(0).Rows(i)("FieldName")), TextBox)
                                    Txt.Text = ds.Tables(0).Rows(i)("Length")
                                End If
                                If i = 0 Then
                                    CheckFlag(ds.Tables(0).Rows(i)("CheckFlag").ToString())
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PersonnelImport.Page_Load", ex)
        End Try

    End Sub

    Public Sub CheckFlag(ByVal strcnt As String)
        Try
            If strcnt = "1" Then
                chkDel1.Checked = True
                chkDel2.Checked = False
            ElseIf strcnt = "2" Then
                chkDel2.Checked = True
                chkDel1.Checked = False
            ElseIf strcnt = "3" Then
                chkDel2.Checked = False
                chkDel1.Checked = False
            ElseIf strcnt = "4" Then
                chkDel2.Checked = True
                chkDel1.Checked = True
            End If
        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("PersonnelImport.CheckFlag()", ex)
        End Try
       
    End Sub

    Public Function BindControl(ByVal strType As String) As DataSet
        Try
            Dim dal As New GeneralizedDAL()
            Dim parcollection(0) As SqlParameter
            Dim ParType = New SqlParameter("@Type", SqlDbType.VarChar)
            ParType.Direction = ParameterDirection.Input
            ParType.Value = strType
            parcollection(0) = ParType
            Return dal.ExecuteStoredProcedureGetDataSet("use_tt_GETImportData", parcollection)

        Catch ex As Exception
            Dim cr As New ErrorPage
            cr.errorlog("BindControl", ex)
            Return Nothing
        End Try

    End Function
End Class
