Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmLiterOrAmount

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim pangalan As String
    Public isamount As Boolean

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"
        txtAmount.Text = vbNullString
    End Sub

    Private Sub frmLiterOrAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Me.Close()
        End If
    End Sub

    Private Sub frmLiterOrAmount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        load_Connection()
    End Sub

    Private Sub txtAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown

        If txtAmount.Text <> vbNullString Then
            If IsNumeric(txtAmount.Text) Then
                If e.KeyCode = Keys.Enter Then
                    If frmGasolineRefill.isamount = True Then
                        If txtAmount.Text <= 0 Then
                            MsgBox("Cannot input value 0 and below.", vbInformation, "Wrong amount.")
                            Return
                        Else
                            'frmGasolineMain.btnLiter.Enabled = False
                            frmGasolineRefill.Label6.Text = "Amount:"
                            frmGasolineRefill.Label6.Visible = True
                            'frmGasolineMain.txtAmountOrLiter.Enabled = True
                            frmGasolineRefill.txtAmountOrLiter.Visible = True
                            frmGasolineRefill.Label2.Text = "No. of Liters:"
                            frmGasolineRefill.Label2.Visible = True
                            frmGasolineRefill.txtLiterOrPrice.Visible = True
                            frmGasolineRefill.btnLiter.Enabled = False
                            Dim am As Double = txtAmount.Text
                            frmGasolineRefill.txtAmountOrLiter.Text = am.ToString("###,###.00")
                            Dim lp As Double = txtAmount.Text / frmGasolineRefill.txtPrice.Text
                            frmGasolineRefill.txtLiterOrPrice.Text = lp.ToString("###,###.00")
                        End If

                    Else
                        If txtAmount.Text <= 0 Then
                            MsgBox("Cannot input value 0 and below.", vbInformation, "Wrong amount.")
                            Return
                        Else
                            ' frmGasolineMain.btnAmount.Enabled = False
                            frmGasolineRefill.Label6.Text = "No. of Liters:"
                            frmGasolineRefill.Label6.Visible = True
                            '   frmGasolineMain.txtAmountOrLiter.Enabled = True
                            frmGasolineRefill.txtAmountOrLiter.Visible = True
                            frmGasolineRefill.Label2.Text = "Amount:"
                            frmGasolineRefill.Label2.Visible = True
                            frmGasolineRefill.txtLiterOrPrice.Visible = True
                            frmGasolineRefill.btnAmount.Enabled = False
                            Dim ami As Double = txtAmount.Text
                            Dim lpi As Double = txtAmount.Text * frmGasolineRefill.txtPrice.Text

                            frmGasolineRefill.txtAmountOrLiter.Text = ami.ToString("###,###.00")
                            frmGasolineRefill.txtLiterOrPrice.Text = lpi.ToString("###,###.00")
                        End If
                    End If
                    frmGasolineRefill.btnStart.Enabled = True
                    txtAmount.Text = ""
                    Me.Close()
                ElseIf e.KeyCode = Keys.Escape Then
                    txtAmount.Text = ""
                    Me.Close()
                End If
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            txtAmount.Text = ""
            Me.Close()
        End If
    End Sub

    Private Sub txtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub
End Class