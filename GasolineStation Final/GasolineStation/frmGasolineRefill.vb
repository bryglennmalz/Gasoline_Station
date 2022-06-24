Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmGasolineRefill

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim pangalan As String

    Public isamount As Boolean

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"

    End Sub
    Function max_trans()
        load_Connection()
        connection.Open()

        Dim query2 As String
        query2 = "select max(payment_details.id) from inc.payment_details"
        command = New MySqlCommand(query2, connection)
        reader = command.ExecuteReader

        While reader.Read
            ids = reader.GetInt32("max(payment_details.id)")
        End While
        'lblUser.Text = lblUser.Text & " " & ids
        connection.Close()
    End Function

    Private Sub AdminCornerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmLogin.Show()
        Me.Hide()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If MsgBox("Do you want to exit?", vbYesNo, "Confirm Message") = vbYes Then
            txtAmountOrLiter.Text = ""
            txtAvailable.Text = ""
            txtFuelType.Text = ""
            txtLiterOrPrice.Text = ""
            txtPrice.Text = ""
            frmGasoline.Show()
            Me.Hide()
        End If
        'frmAddEditAdmins.Close()
        'frmAddEditProducts.Close()
        'frmAdmin.Close()
        'frmGasolineMain.Close()
        'frmLogin.Close()
        'frmLogs.Close()
        'frmPayment.Close()
        'frmProducts.Close()
        'Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtAmountOrLiter.Text = ""
        txtAvailable.Text = ""
        txtLiterOrPrice.Text = ""
        txtPrice.Text = ""
        cmbType.Text = ""
        btnLiter.Enabled = False
        btnAmount.Enabled = False
        Label6.Text = ""
        Label6.Visible = False
        Label2.Visible = False
        Label2.Text = ""
    End Sub

    'Public Sub load_type()
    '    load_Connection()

    '    connection.Open()

    '    Dim query As String
    '    query = "select distinct product_name from inc.product where available = 'Available'"
    '    command = New MySqlCommand(query, connection)
    '    reader = command.ExecuteReader

    '    While reader.Read
    '        cmbType.Items.Add(reader.GetString(0))
    '    End While
    '    connection.Close()
    'End Sub

    Private Sub frmGasolineRefill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        load_Connection()

        connection.Open()

        'load_type()

        'Dim query As String
        'query = "select distinct product_name from inc.product where available = 'Available'"
        'command = New MySqlCommand(query, connection)
        'reader = command.ExecuteReader

        'While reader.Read
        '    cmbType.Items.Add(reader.GetString(0))
        'End While
        'connection.Close()
    End Sub

    'Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

    '        load_Connection()
    '        connection.Open()

    '        Dim available As String
    '        Dim presyos As String
    '        Dim pangalan As String
    '        Dim availLiters As String

    '        Dim query As String
    '        query = "select * from inc.product where product_name = '" & cmbType.Text & "'"
    '        command = New MySqlCommand(query, connection)
    '        reader = command.ExecuteReader

    '        While reader.Read
    '            Gasid = reader.GetInt32("id")
    '            pangalan = reader.GetString("product_name")
    '            presyos = reader.GetString("product_price")
    '            availLiters = reader.GetString("content")
    '            available = reader.GetString("available")
    '        End While
    '    connection.Close()

    '    Dim AL As Double = availLiters
    '    Dim P As Double = presyos
    '        If available = "Available" Then
    '        txtPrice.Text = P.ToString("###,###.00")
    '        txtAvailable.Text = AL.ToString("###,###.00")

    '            btnAmount.Enabled = True
    '            btnLiter.Enabled = True
    '            'frmGasolineMain.Show()
    '            'Me.hide()
    '        ElseIf available = "Not available" Then
    '            MsgBox("Product Unavailable.", vbInformation, "Message")
    '            cmbType.Text = ""
    '        Else
    '            MsgBox("Incorrect Identifier.", vbInformation, "Message")
    '        End If
    'End Sub

    Private Sub btnAmount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmount.Click
        isamount = True
        frmLiterOrAmount.Text = "Set Amount"
        frmLiterOrAmount.lblP.Visible = True
        frmLiterOrAmount.ShowDialog()
    End Sub

    Private Sub btnLiter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLiter.Click
        isamount = False
        frmLiterOrAmount.Text = "Set Liter"
        frmLiterOrAmount.lblP.Visible = False
        frmLiterOrAmount.lblL.Visible = True
        frmLiterOrAmount.ShowDialog()
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        load_Connection()
        Dim x As String
        Dim y As Decimal

        If btnAmount.Enabled = True And btnLiter.Enabled = False Then
            x = txtAvailable.Text
            txtAvailable.Text = ""
            y = CDec(x) - CDec(txtLiterOrPrice.Text)
            txtAvailable.Text = CStr(y)

            connection.Open()
            Dim query As String
            query = "insert into inc.payment_details ( prod_id, qnty, price, paid)values ('" & Gasid & "', '" & txtLiterOrPrice.Text & "', '" & txtAmountOrLiter.Text & "', '0')"
            command = New MySqlCommand(query, connection)
            reader = command.ExecuteReader

            'MessageBox.Show("New Account Added!")
            frmGasolineMain.table_load()
            connection.Close()
            If txtAvailable.Text >= 1 Then
                connection.Open()
                Dim query2 As String
                query2 = "update inc.product set content='" & txtAvailable.Text & "'  where id='" & Gasid & "' LIMIT 1"
                command = New MySqlCommand(query2, connection)
                reader = command.ExecuteReader

                connection.Close()
                MsgBox("successfully refilled.", vbInformation, "Success")
                frmProducts.table_load()
                frmGasolineMain.table_load()

                max_trans()
            Else
                connection.Open()
                Dim query2 As String
                query2 = "update inc.product set content = '" & txtAvailable.Text & "' , available = 'Not available' where id='" & Gasid & "' LIMIT 1"
                command = New MySqlCommand(query2, connection)
                reader = command.ExecuteReader

                connection.Close()
                MsgBox("Tank is empty.", vbCritical, "Message")
                frmProducts.table_load()
                frmGasolineMain.table_load()
            End If
            
        ElseIf btnAmount.Enabled = False And btnLiter.Enabled = True Then
            x = txtAvailable.Text
            txtAvailable.Text = ""
            y = CDec(x) - CDec(txtAmountOrLiter.Text)
            txtAvailable.Text = CStr(y)

            connection.Open()
            Dim query As String
            query = "insert into inc.payment_details ( prod_id, qnty, price)values ('" & Gasid & "', '" & txtAmountOrLiter.Text & "' , '" & txtLiterOrPrice.Text & "')"
            command = New MySqlCommand(query, connection)
            reader = command.ExecuteReader

            'MessageBox.Show("New Account Added!")
            frmGasolineMain.table_load()
            connection.Close()

            If txtAvailable.Text >= 1 Then
                connection.Open()
                Dim query2 As String
                query2 = "update inc.product set content='" & txtAvailable.Text & "'  where id='" & Gasid & "' LIMIT 1"
                command = New MySqlCommand(query2, connection)
                reader = command.ExecuteReader

                connection.Close()
                MsgBox("successfully refilled.", vbInformation, "Success")
                frmProducts.table_load()
                frmGasolineMain.table_load()

                max_trans()
                
            Else
                connection.Open()
                Dim query2 As String
                query2 = "update inc.product set content = '" & txtAvailable.Text & "' , available = 'Not available' where id='" & Gasid & "' LIMIT 1"
                command = New MySqlCommand(query2, connection)
                reader = command.ExecuteReader

                connection.Close()
                MsgBox("Tank is empty.", vbCritical, "Message")


                frmProducts.table_load()
                frmGasolineMain.table_load()
            End If
            'connection.Open()

            'Dim query3 As String
            'query3 = "select max(payment_details.id) from payment_details"
            'command = New MySqlCommand(query3, connection)
            'reader = command.ExecuteReader

            'While reader.Read
            '    ids = reader.GetInt32("id")
            'End While
            'connection.Close()

        End If
        btnCancel.Enabled = True
        'txtAmountOrLiter.Text = ""
        'txtAvailable.Text = ""
        'txtLiterOrPrice.Text = ""
        'txtPrice.Text = ""
        'cmbType.Text = ""
        'btnLiter.Enabled = False
        'btnAmount.Enabled = False
        'Label6.Text = ""
        'Label6.Visible = False
        'Label2.Visible = False
        'Label2.Text = ""
        'btnStart.Enabled = False
    End Sub

    Private Sub cmbType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbType.SelectedIndexChanged

    End Sub

    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        If MsgBox("Do you want to exit?", vbYesNo, "Confirm Message") = vbYes Then
            txtAmountOrLiter.Text = ""
            txtAvailable.Text = ""
            txtFuelType.Text = ""
            txtLiterOrPrice.Text = ""
            txtPrice.Text = ""
            frmGasoline.Show()
            Me.Hide()
        End If
        'frmAddEditAdmins.Close()
        'frmAddEditProducts.Close()
        'frmAdmin.Close()
        'frmGasolineMain.Close()
        'frmLogin.Close()
        'frmLogs.Close()
        'frmPayment.Close()
        'frmProducts.Close()
        'Me.Close()
    End Sub

    'Private Sub cmbType_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbType.TextChanged
    '    load_Connection()
    '    connection.Open()

    '    Dim available As String
    '    Dim presyos As String
    '    Dim pangalan As String
    '    Dim availLiters As String

    '    Dim query As String
    '    query = "select * from inc.product where product_name = '" & cmbType.Text & "'"
    '    command = New MySqlCommand(query, connection)
    '    reader = command.ExecuteReader

    '    While reader.Read
    '        Gasid = reader.GetInt32("id")
    '        pangalan = reader.GetString("product_name")
    '        presyos = reader.GetString("product_price")
    '        availLiters = reader.GetString("content")
    '        available = reader.GetString("available")
    '    End While
    '    connection.Close()

    '    Dim AL As Double = availLiters
    '    Dim P As Double = presyos
    '    If available = "Available" Then
    '        txtPrice.Text = P.ToString("###,###.00")
    '        txtAvailable.Text = AL.ToString("###,###.00")

    '        btnAmount.Enabled = True
    '        btnLiter.Enabled = True
    '        'frmGasolineMain.Show()
    '        'Me.hide()
    '    ElseIf available = "Not available" Then
    '        MsgBox("Product Unavailable.", vbInformation, "Message")
    '        cmbType.Text = ""
    '    Else
    '        MsgBox("Incorrect Identifier.", vbInformation, "Message")
    '    End If
    'End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Do you want to cancel this transaction?", vbYesNo, "Cancel Transaction") = vbYes Then
            frmCancel.Show()
            Me.Hide()
        End If
    End Sub
End Class
