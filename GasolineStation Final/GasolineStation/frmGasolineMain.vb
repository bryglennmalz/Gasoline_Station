Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmGasolineMain

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    'Dim pangalan, pname As String
    Dim Prodid, lCount As Integer
    Dim idProd As Integer

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"

    End Sub

    Public Sub load_table()
        load_Connection()
        Dim sda As New MySqlDataAdapter
        Dim dbDataSet As New DataTable
        Dim bSource As New BindingSource

        connection.Open()
        Dim query As String
        query = "select payment_details.id as 'Transaction ID', product.product_name as'Product Name', product.product_price as 'Product Price', payment_details.qnty as 'QNTY', payment_details.price as 'Sub Price' from inc.payment_details, inc.product where product.id = payment_details.prod_id order by payment_details.id asc"
        'query = "select distinct * from inc.transaction, inc.product where product.id = transaction.prod_id order by trans_id asc"
        command = New MySqlCommand(query, connection)
        sda.SelectCommand = command
        sda.Fill(dbDataSet)
        bSource.DataSource = dbDataSet
        DataGridView1.DataSource = bSource
        sda.Update(dbDataSet)

        'For Each newrow In dbDataSet.Rows
        '    ListView1.Items.Add(newrow.Item(0))
        '    ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(1))
        '    ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(2))
        '    ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(3))
        'Next
        connection.Close()
    End Sub

    Public Sub table_load()
        load_Connection()
        Dim sda As New MySqlDataAdapter
        Dim dbDataSet As New DataTable
        Dim bSource As New BindingSource

        connection.Open()
        Dim query As String
        query = "select payment_details.id, product.product_name, product.product_price, payment_details.qnty, payment_details.price, payment_details.paid from inc.payment_details, inc.product where product.id = payment_details.prod_id and payment_details.paid = 0 order by id asc"
        'query = "select distinct * from inc.transaction, inc.product where product.id = transaction.prod_id order by trans_id asc"
        command = New MySqlCommand(query, connection)
        sda.SelectCommand = command
        sda.Fill(dbDataSet)
        bSource.DataSource = dbDataSet

        Dim newrow As DataRow

        Me.ListView1.View = View.Details
        Me.ListView1.FullRowSelect = True
        Me.ListView1.LabelEdit = False
        Me.ListView1.LabelWrap = True

        ListView1.Items.Clear()


        For Each newrow In dbDataSet.Rows
            ListView1.Items.Add(newrow.Item(0))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(1))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(2))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(3))
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(newrow.Item(4))
        Next

        connection.Close()
    End Sub

    Private Sub frmGasolineMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        table_load()
        'load_table()
        Timer1.Start()
        connection.Open()
        Dim query As String
        query = "select * from inc.admin where admin_id='" & acc_id & "'"
        command = New MySqlCommand(query, connection)
        reader = command.ExecuteReader

        'Dim fName, lName As String
        While reader.Read
            fname = reader.GetString("fname")
            mi = reader.GetString("mi")
            lname = reader.GetString("lname")
        End While

        lblName.Text = fname & " " & mi & ". " & lname



    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        frmProducts.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        frmAdmin.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        frmLogs.Show()
        Me.Hide()
    End Sub

    Private Sub btnLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        ListView1.Items.Clear()
        table_load()
    End Sub

    Private Sub btnPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPay.Click
        'For i As Integer = 0 To DataGridView1.Rows.Count() - 1 Step +1
        '    Dim rowAlreadyExist As Boolean = False
        '    Dim check As Boolean = DataGridView1.Rows(i).Cells(1).Value
        '    Dim row As DataGridViewRow = DataGridView1.Rows(i)
        '    If check = True Then
        '        Dim cnt As Integer = frmPayment.ListView1.Items.Count - 1
        '        Dim item As New ListViewItem
        '        Dim x As Integer
        '        If cnt > 0 Then
        '            For j As Integer = 0 To cnt Step +1
        '                If row.Cells(0).Value.ToString() = frmPayment.ListView1.Items(j).SubItems(0).Text Then
        '                    rowAlreadyExist = True
        '                    Exit For
        '                End If
        '            Next

        '            If rowAlreadyExist = False Then
        '                With item
        '                    ListView1.Items.Clear()
        '                    'i = frmPayment.ListView1.CurrentRow.Index.ToString
        '                    item = frmPayment.ListView1.Items.Add(DataGridView1.Item(0, i).Value.ToString, 0)
        '                    item.SubItems.Add(DataGridView1.Item(1, i).Value.ToString)
        '                    item.SubItems.Add(DataGridView1.Item(2, i).Value.ToString)
        '                    item.SubItems.Add(DataGridView1.Item(3, i).Value.ToString)

        '                End With
        '                'frmPayment.ListView1.Items.Add(DataGridView1.SelectedCells(1).Value.ToString)
        '                'frmPayment.ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(DataGridView1.SelectedCells(2).Value.ToString)
        '                'frmPayment.ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(DataGridView1.SelectedCells(3).Value.ToString)
        '                'frmPayment.ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(DataGridView1.SelectedCells(4).Value.ToString)
        '                'frmPayment.ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(DataGridView1.SelectedCells(5).Value.ToString)
        '                'frmPayment.Show()
        '                Me.Hide()
        '            End If
        '        Else
        '            frmPayment.ListView1.Items.Add(DataGridView1.Item(0, x).Value.ToString)
        '            frmPayment.ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(DataGridView1.SelectedCells(2).Value.ToString)
        '            frmPayment.ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(DataGridView1.SelectedCells(3).Value.ToString)
        '            frmPayment.ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(DataGridView1.SelectedCells(4).Value.ToString)
        '            frmPayment.ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(DataGridView1.SelectedCells(5).Value.ToString)
        '            frmPayment.Show()
        '            Me.Hide()
        '        End If
        '    End If
        'Next


        '    For Each lvi As ListViewItem In ListView2.Items
        'Dim newLvi As ListViewItem = lvi.Clone()
        ' For Each lvsi As ListViewItem.ListViewSubItem In lvi.SubItems
        'newLvi.SubItems.Add(New ListViewItem.ListViewSubItem(newLvi, lvsi.Text, lvsi.ForeColor, lvsi.BackColor, lvsi.Font))
        'Next
        ' frmPayment.ListView1.Items.Add(newLvi)
        ListView2.Items.Clear()
        frmPayment.Show()
        Me.Hide()

        'Next
    End Sub

    'Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim lvi As New ListViewItem

    '    'Prodid = ListView1.SelectedItems(0).SubItems(0).Text
    '    If ListView1.SelectedItems.Count > 0 Then
    '        lvi = ListView1.SelectedItems(0)
    '        Dim lvi2 As New ListViewItem
    '        lvi2 = CType(lvi.Clone, ListViewItem)
    '        'f2.Show()
    '        ListView2.Items.Add(lvi2)
    '    End If
    'End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim lvi As New ListViewItem

        'Prodid = ListView1.SelectedItems(0).SubItems(0).Text
        'If ListView1.SelectedItems.Count > 0 Then
        'lvi = ListView1.SelectedItems(0)
        ' Dim lvi2 As New ListViewItem
        'lvi2 = CType(lvi.Clone, ListViewItem)
        'f2.Show()
        ' ListView2.Items.Add(lvi2)
        'End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ListView2.Items.Clear()
        frmPayment.ListView1.Items.Clear()
        frmPayment.ListView1.Refresh()
        ListView2.Refresh()
        'If ListView2.Items.Count = 0 Then
        '    MessageBox.Show("No item to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Else
        '    ListView2.FocusedItem.Remove()
        'End If

        'Try
        'If ListView1.Items.Count = 0 Then
        'MsgBox("No items to remove", MsgBoxStyle.Critical, "Error")
        'Else
        'Dim itmCnt, i, t As Integer
        'ListView2.FocusedItem.Remove()
        'itmCnt = ListView2.Items.Count
        't = 1
        'For i = 1 To itmCnt + 1
        'Dim lst1 As New ListViewItem(i)
        'ListView1.Items(i).SubItems(0).Text = t
        't = t + 1
        'Next
        'txtSubTotal.Text = subtot()
        'End If
        'btnDelete.Enabled = False
        'If ListView2.Items.Count = 0 Then
        'txtSubTotal.Text = ""
        'End If
        'Catch ex As Exception
        'MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub ListView2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'btnDelete.Enabled = True
        'Dim sql As String

        'load_Connection()

        'connection.Open()
        'idProd = ListView1.SelectedItems(0).SubItems(0).Text

        'sql = "select * from inc.transaction where id='" & idProd & "'"
        'command = New MySqlCommand(sql, connection)
        'reader = command.ExecuteReader

        'While reader.Read
        'pname = reader.GetString("product_name")
        'End While
        'table_load()
        'connection.Close()
    End Sub

    Private Sub ListView2_SelectedIndexChanged_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView2.SelectedIndexChanged

    End Sub

    Private Sub ListView1_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

        Dim p As Integer
        If Not ListView2.Items.Count = 0 Then
            For p = 0 To ListView2.Items.Count - 1
                If ListView2.Items.Item(p).Text = ListView1.Items.Item(ListView1.FocusedItem.Index).Text Then
                    MsgBox("Item already selected!", vbInformation, "Record Existed")
                    Exit Sub
                End If
            Next
        End If
        ListView2.Items.Add(ListView1.Items.Item(ListView1.FocusedItem.Index).Text.ToString)
        ListView2.Items.Item(ListView2.Items.Count - 1).SubItems.Add(ListView1.FocusedItem.SubItems(1))
        ListView2.Items.Item(ListView2.Items.Count - 1).SubItems.Add(ListView1.FocusedItem.SubItems(2))
        ListView2.Items.Item(ListView2.Items.Count - 1).SubItems.Add(ListView1.FocusedItem.SubItems(3))
        ListView2.Items.Item(ListView2.Items.Count - 1).SubItems.Add(ListView1.FocusedItem.SubItems(4))

        frmPayment.ListView1.Items.Add(ListView1.Items.Item(ListView1.FocusedItem.Index).Text.ToString)
        frmPayment.ListView1.Items.Item(ListView2.Items.Count - 1).SubItems.Add(ListView1.FocusedItem.SubItems(1))
        frmPayment.ListView1.Items.Item(ListView2.Items.Count - 1).SubItems.Add(ListView1.FocusedItem.SubItems(2))
        frmPayment.ListView1.Items.Item(ListView2.Items.Count - 1).SubItems.Add(ListView1.FocusedItem.SubItems(3))
        frmPayment.ListView1.Items.Item(ListView2.Items.Count - 1).SubItems.Add(ListView1.FocusedItem.SubItems(4))

        '----------------------------------------------
        'For i As Integer = 0 To ListView1.Items.Count - 1 Step +1
        'Dim rowAlreadyExist As Boolean = False
        'Dim row As ListViewItem = ListView1.Items(0)
        'If check = True Then
        'Dim cnt As Integer = ListView2.Items.Count
        'Dim item As New ListViewItem
        'Dim x As Integer
        'If cnt > 0 Then
        'For j As Integer = 0 To cnt Step +1
        'If ListView1.SelectedItems(0).SubItems(0).Text = ListView2.Items(j).SubItems(0).Text Then
        'rowAlreadyExist = True
        ' MsgBox("Already Exsisted.", vbInformation, "Exist")
        ' Exit For
        ' End If
        'Next

        '  If rowAlreadyExist = False Then
        'Dim lvi As New ListViewItem
        ' lvi = ListView1.SelectedItems(0)
        ' Dim lvi2 As New ListViewItem
        ' lvi2 = CType(lvi.Clone, ListViewItem)
        'f2.Show()
        ' ListView2.Items.Add(lvi2)
        ' End If
        'End If
        'End If
        'Next

        'Dim lvi As New ListViewItem

        ''Prodid = ListView1.SelectedItems(0).SubItems(0).Text
        'If ListView1.SelectedItems.Count > 0 Then
        '    lvi = ListView1.SelectedItems(0)
        '    Dim lvi2 As New ListViewItem
        '    lvi2 = CType(lvi.Clone, ListViewItem)
        '    'f2.Show()
        '    ListView2.Items.Add(lvi2)
        'End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        frmPayment.ListView1.Items.Item(ListView2.FocusedItem.Index).Remove()
        ListView2.Items.Item(ListView2.FocusedItem.Index).Remove()

        ListView2.Refresh()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If ListView2.Items.Count = 0 Then
            btnPay.Enabled = False
            btnClear.Enabled = False
        Else
            btnPay.Enabled = True
            btnClear.Enabled = True
        End If
    End Sub

    Private Sub lblName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    'Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
    '    If ListView2.SelectedItems.Item = True Then
    '        btnDelete.Enabled = True
    '    Else
    '        btnDelete.Enabled = False
    '    End If
    'End Sub

    Private Sub btnLogout_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        load_Connection()

        Dim s As StreamWriter
        Dim portfolioPath As String = My.Application.Info.DirectoryPath
        If Not Directory.Exists("C:\GasolineLogs") Then
            Directory.CreateDirectory("C:\GasolineLogs")
            File.Create("C:\GasolineLogs\logs.txt").Close()
            s = New StreamWriter("C:\GasolineLogs\logs.txt", True)
            s.WriteLine(lblName.Text & " has successfully logged out on " & Date.Now)
            s.Flush()
            s.Close()
        Else
            s = New StreamWriter("C:\GasolineLogs\logs.txt", True)
            s.WriteLine(lblName.Text & " has successfully logged out on " & Date.Now)
            s.Flush()
            s.Close()
        End If

        Me.Close()
        frmLogin.Show()
    End Sub
End Class