Imports MySql.Data.MySqlClient
Imports System.IO
Public Class frmCancel
    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim pangalan, first, mi, last, codes As String

    Public isamount As Boolean

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"

    End Sub
    Function delete()
        load_Connection()
        connection.Open()

        Dim query As String
        query = "DELETE FROM inc.payment_details WHERE id='" & ids & "' LIMIT 1"
        command = New MySqlCommand(query, connection)
        reader = command.ExecuteReader
        connection.Close()
        MsgBox("Transaction has been canceled.", vbInformation, "Transaction Canceled")
        Dim count As Integer
        'While reader.Read
        '    count = count + 1
        'End While
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        load_Connection()
        connection.Open()

        Dim query As String
        query = "select * from inc.gasolineman where usercode='" & txtUserCode.Text & "'"
        command = New MySqlCommand(query, connection)
        reader = command.ExecuteReader
        Dim count As Integer
        While reader.Read
            count = count + 1
        End While
        If count = 1 Then
            gasman_id = reader.GetInt32("man_id")
            'first = reader.GetString("fname")
            'mi = reader.GetString("mi")
            'last = reader.GetString("lname")
            'codes = reader.GetString("usercode")
            'txtUserCode.Text = ""
            connection.Close()

            If txtUserCode.Text = code Then
                'delete()
                connection.Open()

                Dim query3 As String
                query3 = "DELETE FROM inc.payment_details WHERE payment_details.id='" & ids & "'"
                command = New MySqlCommand(query3, connection)
                reader = command.ExecuteReader
                connection.Close()
                MsgBox("Transaction has been canceled.", vbInformation, "Transaction Canceled")

                If frmGasolineRefill.btnAmount.Enabled = True And frmGasolineRefill.btnLiter.Enabled = False Then
                    connection.Open()
                    Dim lit As Double
                    lit = CDbl(frmGasolineRefill.txtAvailable.Text) + CDbl(frmGasolineRefill.txtLiterOrPrice.Text)
                    Dim query2 As String
                    query2 = "update inc.product set content='" & CStr(lit) & "'  where id='" & Gasid & "' LIMIT 1"
                    command = New MySqlCommand(query2, connection)
                    reader = command.ExecuteReader

                    connection.Close()
                ElseIf frmGasolineRefill.btnAmount.Enabled = False And frmGasolineRefill.btnLiter.Enabled = True Then
                    connection.Open()
                    Dim lit As Double
                    lit = CDbl(frmGasolineRefill.txtAvailable.Text) + CDbl(frmGasolineRefill.txtAmountOrLiter.Text)
                    Dim query2 As String
                    query2 = "update inc.product set content='" & CStr(lit) & "'  where id='" & Gasid & "' LIMIT 1"
                    command = New MySqlCommand(query2, connection)
                    reader = command.ExecuteReader
                End If
                    frmGasolineMain.load_table()
                    Me.Close()
                    frmGasolineRefill.Show()

            Else
                MessageBox.Show("Usercode Error!" & vbNewLine & "Input a valid code.")
                Return
            End If
        End If
    End Sub
End Class