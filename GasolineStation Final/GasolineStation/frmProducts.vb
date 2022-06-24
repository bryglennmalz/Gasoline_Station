Imports MySql.Data.MySqlClient

Public Class frmProducts

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim Pname As String
    Dim idProd As Integer
    Dim lCount As Integer

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"
    End Sub

    Public Sub table_load()
        load_Connection()
        Dim sda As New MySqlDataAdapter
        Dim dbDataSet As New DataTable
        Dim bSource As New BindingSource

        connection.Open()
        Dim query As String
        query = "select * from inc.product order by id asc"
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

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        frmAddEditProducts.Show()
        frmAddEditProducts.lblCaption.Text = "Add Products"
        frmAddEditProducts.btnAdd.Visible = True
        frmAddEditProducts.btnUpdate.Visible = False
        Me.Hide()
    End Sub

    Private Sub frmProducts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        table_load()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim query, fname, qnty, price, available As String
        Dim id As Integer

        If idProd = vbEmpty Then
            MsgBox("Select an item first.", vbInformation, "Can't edit")
        Else
            load_Connection()
            connection.Open()

            query = "select * from inc.product where id='" & idProd & "'"
            command = New MySqlCommand(query, connection)
            reader = command.ExecuteReader

            While reader.Read
                id = reader.GetInt32("id")
                fname = reader.GetString("product_name")
                qnty = reader.GetString("content")
                price = reader.GetString("product_price")
                available = reader.GetString("available")
            End While

            Gasid = vbEmpty
            frmAddEditProducts.Show()
            frmAddEditProducts.btnUpdate.Visible = True
            frmAddEditProducts.btnAdd.Visible = False
            frmAddEditProducts.lblCaption.Text = "Update Product's Information"
            frmAddEditProducts.Label1.Visible = True
            frmAddEditProducts.txtProdID.Visible = True

            frmAddEditProducts.txtProdID.Text = CStr(id)
            frmAddEditProducts.txtName.Text = fname
            frmAddEditProducts.txtPrice.Text = price
            frmAddEditProducts.txtLiters.Text = qnty
            frmAddEditProducts.cmbStatus.Text = available

            connection.Close()
        End If
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Dim sql As String

        load_Connection()

        connection.Open()
        idProd = ListView1.SelectedItems(0).SubItems(0).Text

        sql = "select * from inc.product where id='" & idProd & "'"
        command = New MySqlCommand(sql, connection)
        reader = command.ExecuteReader

        While reader.Read
            Pname = reader.GetString("product_name")
        End While
        table_load()
        connection.Close()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If idProd = vbEmpty Then
            MsgBox("Select an item first.", vbInformation, "Can't delete")
        Else
            If vbYes = MsgBox("Remove '" & Pname & "' Product?", vbYesNo, "Delete") Then

                load_Connection()
                connection.Open()
                Dim sql As String
                sql = "Delete from inc.product where id='" & idProd & "' Limit 1"
                command = New MySqlCommand(sql, connection)
                reader = command.ExecuteReader
                connection.Close()

                ListView1.Items.Clear()
                MsgBox(Pname & "has been deeted", vbInformation, "Delete")
                table_load()
            End If
        End If
    End Sub

    Private Sub btnAvailable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAvailable.Click
        Dim query, fname, qnty As String
        Dim id As Integer
        load_Connection()
        connection.Open()

        query = "select * from inc.product where id='" & idProd & "'"
        command = New MySqlCommand(query, connection)
        reader = command.ExecuteReader

        While reader.Read
            id = reader.GetInt32("id")
            fname = reader.GetString("product_name")
            qnty = reader.GetString("content")
        End While
        frmAddEditProducts.Show()
        frmAddEditProducts.btnUpdate.Visible = True
        frmAddEditProducts.lblCaption.Text = "Update Product's Information"
        frmAddEditProducts.Label1.Visible = True
        frmAddEditProducts.txtProdID.Visible = True

        frmAvailable.txtProdID.Text = CStr(id)
        frmAvailable.txtName.Text = fname
        frmAvailable.txtLiters.Text = qnty

        connection.Close()
    End Sub

    Private Sub btnBack_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        frmGasolineMain.Show()
        Me.Hide()
    End Sub
End Class