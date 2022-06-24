Imports MySql.Data.MySqlClient
Imports System.IO
Public Class frmPayment

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim Prodid, lCount As Integer
    Dim idProd As Integer
    Public ispay As Boolean = False
    Public totalprices As Double

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"

    End Sub

    Public Function GetSubTotal() As Decimal
        Dim k As Integer = 0
        Try
            Dim j As Integer = ListView1.Items.Count
            Dim i As Integer
            For i = 0 To j - 1
                k += CType(ListView1.Items(i).SubItems(4).Text, Integer)
            Next
        Catch ex As Exception
        End Try
        Return k

        'Dim TotalValue As Decimal
        'Dim tmp As Decimal

        '' arrays and collections start at index(0) not (1)
        '' OP code would skip the first item 
        'For n As Integer = 0 To ListView1.Items.Count - 1
        '    ' ToDo: Not all items must have the same number of SubItems
        '    ' should also check SubItems Count >= 1 for each item
        '    ' try to get the value:
        '    If Decimal.TryParse(ListView1.Items(n).SubItems(4).Text, tmp) Then
        '        TotalValue = TotalValue + tmp
        '    End If
        'Next

        'Return TotalValue
    End Function

    Private Sub frmPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblTotalPrice.Text = CStr(GetSubTotal())
    End Sub

    Private Sub btnPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPay.Click
        ispay = True
        'frmLiterOrAmount.Text = "Set Amount"
        If txtCustname.Text = vbNullString Then
            MsgBox("Please input customer name first.", vbInformation, "Input Customer Name!")

            If txtCustname.Text = vbNullString Then
                Me.ErrorProvider1.SetError(Me.txtCustname, "Must Not Leave a Blank")
                Return
            Else
                Me.ErrorProvider1.Clear()
            End If
            'Return
        Else
            totalprices = CDbl(lblTotalPrice.Text)
            frmCashOnHand.ShowDialog()
        End If

            'Dim change As Decimal = 0
            'Dim payID, paid As Integer

            'If txtAmount.Text < lblTotalPrice.Text Then
            '    MsgBox("Insuficient amount to pay your transactions.", vbInformation, "Insuficient Amount")

            'ElseIf txtAmount.Text >= lblTotalPrice.Text Then
            '    change = CDec(txtAmount.Text) - CDec(lblTotalPrice.Text)
            '    lblChange.Text = CStr(change)
            '    MsgBox("--------- Reciept ----------------------" & vbLf & _
            '                "|Cash on Hand :  " & txtAmount.Text.ToString & vbLf & _
            '               "|Amount : ₱" & lblTotalPrice.Text.ToString & vbLf & _
            '              "|Change : " & lblChange.Text.ToString & vbLf & _
            '               "|Date : " & System.DateTime.Now.Date.ToShortDateString & vbLf & _
            '               "|End of Transaction!!!" & vbLf & _
            '               "--------- Reciept -----------------------", , "Summary")
            '    load_Connection()
            '    connection.Open()
            '    Dim query As String
            '    query = "insert into inc.payment (payment.total_amount, payment.csh_hand, payment.change, payment.date_time, payment.account_id) VALUES ('" & lblTotalPrice.Text & "','" & txtAmount.Text & "','" & CStr(change) & "'," & Now & ",'" & acc_id & "')"
            '    command = New MySqlCommand(query, connection)
            '    reader = command.ExecuteReader
            '    reader.Close()
            '    connection.Close()

            '    connection.Open()
            '    Dim query2 As String
            '    query2 = "select max(payment.id) from payment"
            '    command = New MySqlCommand(query, connection)
            '    reader = command.ExecuteReader

            '    While reader.Read
            '        payID = reader.GetInt32("id")
            '    End While
            '    reader.Close()
            '    connection.Close()

            '    If Not ListView1.Items.Count = 0 Then
            '        For p = 0 To ListView1.Items.Count - 1
            '            Dim id As Integer = CInt(ListView1.Items(p).SubItems(0).Text)
            '            connection.Open()
            '            Dim query3 As String
            '            query3 = "update inc.payment_details set payment_details.payment_id='" & payID & "',  payment_details.account_id='" & acc_id & "'  where id='" & id & "' LIMIT 1"
            '            command = New MySqlCommand(query, connection)
            '            reader = command.ExecuteReader
            '            reader.Close()
            '            connection.Close()
            '        Next
            '    End If
            'End If
            'btnBack.Visible = False
            'btnClose.Visible = True

    End Sub

    Private Sub btnBack_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If MsgBox("Are you sure you want to cancel operation?", vbYesNo, "Abort Proccess.") = vbYes Then
            ListView1.Items.Clear()
            frmGasolineMain.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        frmGasolineMain.ListView1.Items.Clear()
        frmGasolineMain.table_load()
        frmGasolineMain.ListView1.Refresh()
        Me.Close()
        frmGasolineMain.Show()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class