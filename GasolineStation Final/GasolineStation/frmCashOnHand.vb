Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Drawing.Printing

Public Class frmCashOnHand
    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim reader3 As MySqlDataReader

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"

    End Sub

    'Fun print_receipt()
    'Dim printDialog = New PrintDialog
    'Dim print_doc = New PrintDocument

    '    printDialog.Document = print_doc
    'End Function

    Private Sub txtAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
        Dim myDate As Date = Date.Now()
        ' MsgBox("" & myDate)
        If txtAmount.Text <> vbNullString Then
            If IsNumeric(txtAmount.Text) Then
                If e.KeyCode = Keys.Enter Then
                    If frmPayment.ispay = True Then
                        If CDbl(txtAmount.Text) < frmPayment.totalprices Then
                            MsgBox("Insuficient amount to pay your transactions.", vbInformation, "Insuficient Amount")
                        Else
                            Dim change As Double = 0
                            Dim payID As Integer
                            change = CDbl(txtAmount.Text) - CDbl(frmPayment.lblTotalPrice.Text)
                            frmPayment.lblChange.Text = CStr(change)
                            frmPayment.txtAmount.Text = txtAmount.Text
                            'MsgBox("--------- Reciept ----------------------" & vbLf & _
                            '            "|Customer Name :  " & frmPayment.txtCustname.Text.ToString & vbLf & _
                            '            "|Cash on Hand :  " & txtAmount.Text.ToString & vbLf & _
                            '           "|Amount : ₱" & frmPayment.lblTotalPrice.Text.ToString & vbLf & _
                            '          "|Change : " & frmPayment.lblChange.Text.ToString & vbLf & _
                            '           "|Date : " & System.DateTime.Now.Date.ToShortDateString & vbLf & _
                            '           "|End of Transaction!!!" & vbLf & _
                            '           "--------- Reciept -----------------------", , "Summary")
                            load_Connection()
                            connection.Open()
                            Dim query As String

                            query = "insert into inc.payment (payment.cust_name, payment.total_amount, payment.csh_hand, payment.change, payment.date, payment.time, payment.account_id) VALUES ('" & frmPayment.txtCustname.Text & "','" & frmPayment.lblTotalPrice.Text & "','" & CStr(txtAmount.Text) & "','" & CStr(change) & "','" & DateTime.Now.ToString("yyyy/MM/dd") & "','" & Date.Now.ToString("HH:mm") & "','" & acc_id & "')"
                            command = New MySqlCommand(query, connection)
                            reader = command.ExecuteReader
                            reader.Close()
                            connection.Close()

                            connection.Open()
                            Dim query2 As String
                            query2 = "select max(payment.payment_id) from inc.payment"
                            command = New MySqlCommand(query2, connection)
                            reader = command.ExecuteReader

                            While reader.Read
                                payID = reader.GetInt32("max(payment.payment_id)")
                            End While
                            reader.Close()
                            connection.Close()

                            MsgBox("--------- Reciept " & payID & "-------------------" & vbLf & _
                                       "|Customer Name :  " & frmPayment.txtCustname.Text.ToString & vbLf & _
                                       "|Cash on Hand :  " & txtAmount.Text.ToString & vbLf & _
                                      "|Amount : ₱" & frmPayment.lblTotalPrice.Text.ToString & vbLf & _
                                     "|Change : " & frmPayment.lblChange.Text.ToString & vbLf & _
                                      "|Date : " & System.DateTime.Now.Date.ToShortDateString & vbLf & _
                                      "|End of Transaction!!!" & vbLf & _
                                      "--------- Reciept -----------------------", , "Summary")


                            If Not frmPayment.ListView1.Items.Count = 0 Then
                                For p = 0 To frmPayment.ListView1.Items.Count - 1
                                    Dim id As Integer = CInt(frmPayment.ListView1.Items(p).SubItems(0).Text)
                                    connection.Open()
                                    Dim query3 As String
                                    query3 = "update inc.payment_details set payment_details.payment_id='" & payID & "',  payment_details.paid='1'  where id='" & id & "' LIMIT 1"
                                    command = New MySqlCommand(query3, connection)
                                    reader3 = command.ExecuteReader
                                    reader.Close()
                                    connection.Close()
                                Next
                            End If
                            frmPayment.btnPay.Enabled = False
                            frmPayment.btnBack.Visible = False
                            frmPayment.btnClose.Visible = True
                            frmPayment.ListView1.Items.Clear()
                            frmPayment.lblTotalPrice.Text = ""
                            frmPayment.txtCustname.Text = ""
                            frmPayment.txtAmount.Text = ""
                            frmPayment.lblChange.Text = ""

                            txtAmount.Text = ""
                            frmGasolineMain.ListView1.Items.Clear()
                            frmGasolineMain.table_load()
                            frmGasolineMain.ListView1.Refresh()
                            Me.Close()

                            frmPayment.Close()
                            frmGasolineMain.Show()
                            'Me.Close()
                        End If
                        '  frmPayment.btnPay.Enabled = False
                        ' frmPayment.btnBack.Visible = False
                        'frmPayment.btnClose.Visible = True
                    Else

                    End If
                    txtAmount.Text = ""
                    Me.Close()
                    'frmGasolineMain.Show()
                    'ElseIf e.KeyCode = Keys.Escape Then
                    '    frmPayment.ispay = False
                    '    txtAmount.Text = ""
                    '    Me.Close()
                End If
            End If
        End If
        'If e.KeyCode = Keys.Escape Then
        '    txtAmount.Text = ""
        '    Me.Close()
        'End If
    End Sub

    Private Sub txtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged

    End Sub

    Private Sub frmCashOnHand_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Me.Close()
        End If
    End Sub

    Private Sub frmCashOnHand_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class