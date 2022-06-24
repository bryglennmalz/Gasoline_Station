Imports MySql.Data.MySqlClient
Public Class frmLogs

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader

    Dim qtrans As String
    Dim count As Integer

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"

    End Sub

    Public Sub loadTransactionTable()
        load_Connection()

        Dim sda As New MySqlDataAdapter
        Dim dbDataSet As New DataTable
        Dim bSource As New BindingSource
        Dim a, b As String



        If txtEmpName.Text <> "" Then
            b = "and admin.fname like '%" & txtEmpName.Text & "%'"
            ', admin.mi like '%" & txtEmpName.Text & "%', admin.lname like '%" & txtEmpName.Text & "%'"
        End If

        '    If cmbByDate.Text = "All" Or cmbByDate.Text = vbNullString Then
        'qtrans = ""
        'ElseIf cmbByDate.Text = "Daily" Then
        'qtrans = "WHERE payment.date LIKE '" & DateTimePicker1.Text & "%'"
        'ElseIf cmbByDate.Text = "Monthly" Then
        'Dim memonth As String = Microsoft.VisualBasic.Left(DateTimePicker1.Text, 2)
        'qtrans = "WHERE payment.date LIKE '" & memonth & "%'"
        'ElseIf cmbByDate.Text = "Annually" Then
        'Dim memonth As String = Microsoft.VisualBasic.Right(DateTimePicker1.Text, 4)
        'qtrans = "WHERE payment.date LIKE '%" & memonth & "%'"
        'End If

        connection.Open()
        Dim query As String
        If qtrans = "" Then
            qtrans = "WHERE payment.date <>''"
        End If
        'query = "select payment.payment_id, payment.total_amount, payment.csh_hand, payment.`change`, concat(payment.date,' ', payment.time) as Date,  CONCAT(admin.fname, ' ', admin.mi, '. ', admin.lname) as EmpName FROM inc.payment INNER JOIN inc.admin ON admin.admin_id = payment.account_id " & qtrans & b
        query = "select payment.payment_id, payment.cust_name, payment.total_amount, payment.csh_hand, payment.`change`, CONCAT(admin.fname, ' ', admin.mi, '. ', admin.lname) as EmpName,concat(payment.date,' ', payment.time) as Dates FROM inc.payment, inc.admin where admin.admin_id = payment.account_id and  `date` ='" & Format(Date.Now, "yyyy-MM-dd") & "'"
        command = New MySqlCommand(query, connection)
        sda.SelectCommand = command
        sda.Fill(dbDataSet)
        bSource.DataSource = dbDataSet


        Dim newrow As DataRow

        Me.ListView1.FullRowSelect = True
        Me.ListView1.LabelEdit = False
        Me.ListView1.LabelWrap = True

        ListView1.Items.Clear()

        For Each newrow In dbDataSet.Rows
            count = count + 1
            ListView1.Items.Add(newrow.Item(0))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(1))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(2))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(3))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(4))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item("EmpName"))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item("Dates"))
            'ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(6))


        Next

        'lblCount.Text = count

        count = 0
        connection.Close()
    End Sub

    Private Sub frmLogs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        load_Connection()
        loadTransactionTable()
        cmbByDate.Items.Add("Date")
        cmbByDate.Items.Add("Payment #")
        cmbByDate.Items.Add("Time")
        
    End Sub
    'Private Sub datas()
    '    Dim sda2 As New MySqlDataAdapter
    '    Dim dbDataSet2 As New DataTable
    '    Dim bSource2 As New BindingSource
    '    Dim a, b As String
    '    'Dim tempdate, temptime As String
    '    Try
    '        connection.Open()
    '        query = "SELECT payment.payment_id, payment.cust_name, payment.total_amount, payment.csh_hand, payment.`change`, payment.date, payment.time,  CONCAT(admin.fname, ' ', admin.mi, '. ', admin.lname) as Fullname FROM payment INNER JOIN admin ON admin.admin_id = payment.account_id where `date`='" & Format(Date.Now, "yyyy-MM-dd") & "'"
    '        command = New MySqlCommand(query, connection)
    '        reader = command.ExecuteReader
    '        ListView1.Items.Clear()

    '        sda2.SelectCommand = command
    '        sda2.Fill(dbDataSet2)
    '        bSource2.DataSource = dbDataSet2

    '        Dim newrow As DataRow

    '        Me.ListView1.FullRowSelect = True
    '        Me.ListView1.LabelEdit = False
    '        Me.ListView1.LabelWrap = True

    '        ListView1.Items.Clear()

    '        For Each newrow In dbDataSet2.Rows
    '            count = count + 1
    '            ListView1.Items.Add(newrow.Item(0))
    '            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(1))
    '            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(2))
    '            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(3))
    '            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(5))
    '            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(4))
    '        Next
    '        'While reader.Read
    '        '    With ListView1
    '        '        .Items.Add(reader(0))
    '        '        With .Items(.Items.Count - 1).SubItems
    '        '            .Add(reader(1))
    '        '            .Add(reader(2))
    '        '            .Add(reader(3))
    '        '            tempdate = CStr(reader(4))
    '        '            temptime = CStr(reader(5))

    '        '            .Add(tempdate & "" & temptime)
    '        '            .Add(reader(6))
    '        '        End With
    '        '    End With
    '        'End While
    '        connection.Close()
    '    Catch ex As MySqlException
    '        MsgBox(ex.Message, vbCritical, ex.Message)
    '    Finally
    '        connection.Dispose()
    '    End Try
    'End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        Dim tota As Double = ListView1.FocusedItem.SubItems(2).Text
        Dim coh As Double = ListView1.FocusedItem.SubItems(3).Text
        Dim ch As Double = ListView1.FocusedItem.SubItems(4).Text
        lblDate.Text = ListView1.FocusedItem.SubItems(6).Text
        lblNo.Text = ListView1.FocusedItem.SubItems(0).Text
        lblCust.Text = ListView1.FocusedItem.SubItems(1).Text
        lblTotA.Text = tota.ToString("###,###.00")
        lblCoH.Text = coh.ToString("###,###.00")
        lblChange.Text = ch.ToString("###,###.00")
        lblEmp.Text = ListView1.FocusedItem.SubItems(5).Text

        lblDate.Visible = True
        lblNo.Visible = True
        lblCust.Visible = True
        lblTotA.Visible = True
        lblCoH.Visible = True
        lblChange.Visible = True
        lblEmp.Visible = True

        load_Connection()

        Dim sda2 As New MySqlDataAdapter
        Dim dbDataSet2 As New DataTable
        Dim bSource2 As New BindingSource

        connection.Open()
        Dim query As String

        query = "select payment_details.id, product.product_name, product.product_price, payment_details.qnty, payment_details.price, payment_details.paid from inc.payment_details, inc.product where product.id = payment_details.prod_id and payment_details.payment_id = '" & ListView1.FocusedItem.SubItems(0).Text & "'"
        'query = "select * from payment_details where payment_details.payment_id = '" & ListView1.FocusedItem.SubItems(0).Text & "'"

        command = New MySqlCommand(query, connection)
        sda2.SelectCommand = command
        sda2.Fill(dbDataSet2)
        bSource2.DataSource = dbDataSet2


        Dim newrow As DataRow

        Me.ListView3.FullRowSelect = True
        Me.ListView3.LabelEdit = False
        Me.ListView3.LabelWrap = True

        ListView3.Items.Clear()

        For Each newrow In dbDataSet2.Rows
            ListView3.Items.Add(newrow.Item(0))
            ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(newrow.Item(1))
            ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(newrow.Item(2))
            ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(newrow.Item(3))
            ListView3.Items(ListView3.Items.Count - 1).SubItems.Add(newrow.Item(4))
        Next

        'lblCount.Text = count

        count = 0
        connection.Close()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub txtEmpName_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmpName.KeyDown
        'loadTransactionTable()
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmpName.TextChanged
        'loadTransactionTable()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        'loadTransactionTable()
    End Sub

    Private Sub cmbByDate_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbByDate.SelectedIndexChanged
        'loadTransactionTable()
    End Sub

    Private Sub btnBack_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        lblDate.Text = ""
        lblNo.Text = ""
        lblCust.Text = ""
        lblTotA.Text = ""
        lblCoH.Text = ""
        lblChange.Text = ""
        lblEmp.Text = ""

        ListView3.Items.Clear()

        frmGasolineMain.Show()
        Me.Hide()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dates1, dates2 As String
        dates1 = DateTimePicker1.Text 'Format(DateTimePicker1.Value, "yyyy-MM-dd")
        dates2 = DateTimePicker2.Text 'Format(DateTimePicker2.Value, "yyyy-MM-dd")

        Try
            If DateTimePicker1.Text > DateTimePicker2.Text Then
                MsgBox("Invalid Date Range", vbCritical, "Message")
            Else
                If cmbByDate.Text = "Date" Or cmbByDate.Text = vbNullString Then
                    ListView1.Items.Clear()
                    connection.Open()
                    query = "SELECT payment.payment_id, payment.cust_name, payment.total_amount, payment.csh_hand, payment.`change`, CONCAT(admin.fname, ' ', admin.mi, '. ', admin.lname) as EmpName, CONCAT(payment.date , ' ', payment.time) as date FROM payment INNER JOIN admin ON payment.account_id = admin.admin_id WHERE (admin.fname LIKE '%" & txtEmpName.Text & "%' or admin.mi LIKE '%" & txtEmpName.Text & "%' or admin.lname LIKE '%" & txtEmpName.Text & "%') AND (payment.date BETWEEN '" & dates1 & "' AND '" & dates2 & "') ORDER BY payment.date DESC"
                    command = New MySqlCommand(query, connection)
                    reader = command.ExecuteReader
                    While reader.Read
                        With ListView1
                            .Items.Add(reader(0))
                            With .Items(.Items.Count - 1).SubItems
                                .Add(reader(1))
                                .Add(reader(2))
                                .Add(reader(3))
                                .Add(reader(4))
                                .Add(reader(5))
                                .Add(reader(6))
                            End With
                        End With
                    End While
                    connection.Close()
                ElseIf cmbByDate.Text = "Payment #" Then
                    ListView1.Items.Clear()
                    connection.Open()
                    query = "SELECT payment.payment_id, payment.cust_name, payment.total_amount, payment.csh_hand, payment.`change`, CONCAT(admin.fname, ' ', admin.mi, '. ', admin.lname) as EmpName, CONCAT(payment.date , ' ', payment.time) as date FROM payment INNER JOIN admin ON payment.account_id = admin.admin_id WHERE (admin.fname LIKE '%" & txtEmpName.Text & "%' or admin.mi LIKE '%" & txtEmpName.Text & "%' or admin.lname LIKE '%" & txtEmpName.Text & "%') AND (payment.date BETWEEN '" & dates1 & "' AND '" & dates2 & "') ORDER BY payment.payment_id "
                    command = New MySqlCommand(query, connection)
                    reader = command.ExecuteReader
                    While reader.Read
                        With ListView1
                            .Items.Add(reader(0))
                            With .Items(.Items.Count - 1).SubItems
                                .Add(reader(1))
                                .Add(reader(2))
                                .Add(reader(3))
                                .Add(reader(4))
                                .Add(reader(5))
                                .Add(reader(6))
                            End With
                        End With
                    End While
                    connection.Close()
                ElseIf cmbByDate.Text = "Time" Then
                    ListView1.Items.Clear()
                    connection.Open()
                    query = "SELECT payment.payment_id, payment.cust_name, payment.total_amount, payment.csh_hand, payment.`change`, CONCAT(admin.fname, ' ', admin.mi, '. ', admin.lname) as EmpName, CONCAT(payment.date , ' ', payment.time) as date FROM payment INNER JOIN admin ON payment.account_id = admin.admin_id WHERE (admin.fname LIKE '%" & txtEmpName.Text & "%' or admin.mi LIKE '%" & txtEmpName.Text & "%' or admin.lname LIKE '%" & txtEmpName.Text & "%') AND (payment.date BETWEEN '" & dates1 & "' AND '" & dates2 & "') ORDER BY payment.time desc"
                    command = New MySqlCommand(query, connection)
                    reader = command.ExecuteReader
                    While reader.Read
                        With ListView1
                            .Items.Add(reader(0))
                            With .Items(.Items.Count - 1).SubItems
                                .Add(reader(1))
                                .Add(reader(2))
                                .Add(reader(3))
                                .Add(reader(4))
                                .Add(reader(5))
                                .Add(reader(6))
                            End With
                        End With
                    End While
                    connection.Close()
                End If
            End If

        Catch ex As MySqlException
            MsgBox(ex.Message, vbCritical, ex.Message)
        Finally
            connection.Dispose()
        End Try
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class