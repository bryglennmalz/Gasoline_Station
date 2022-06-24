Imports MySql.Data.MySqlClient
Public Class frmAdmin

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim pangalan As String
    Dim idAcc As Integer

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
        query = "select * from inc.admin order by admin_id asc"
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
        frmAddEditAdmins.Show()
        frmAddEditAdmins.lblCaption.Text = "Add New Employee"
        frmAddEditAdmins.btnAdd.Visible = True
        frmAddEditAdmins.btnUpdate.Visible = False
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim query, fname, mi, lname, uname, pword As String
        Dim id As Integer
        load_Connection()
        connection.Open()

        query = "select * from inc.admin where admin_id='" & idAcc & "'"
        command = New MySqlCommand(query, connection)
        reader = command.ExecuteReader

        While reader.Read
            id = reader.GetInt32("admin_id")
            mi = reader.GetString("mi")
            lname = reader.GetString("lname")
            fname = reader.GetString("fname")
            uname = reader.GetString("username")
            pword = reader.GetString("pword")
        End While
        frmAddEditAdmins.btnUpdate.Visible = True
        frmAddEditAdmins.btnAdd.Visible = False
        frmAddEditAdmins.lblCaption.Text = "Update Product's Information"

        frmAddEditAdmins.txtID.Text = CStr(id)
        frmAddEditAdmins.txtFname.Text = fname
        frmAddEditAdmins.txtMI.Text = mi
        frmAddEditAdmins.txtLname.Text = lname
        frmAddEditAdmins.txtUsername.Text = uname
        frmAddEditAdmins.txtPassword.Text = pword

        frmAddEditAdmins.Show()

        connection.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If vbYes = MsgBox("Remove this User?", vbYesNo, "Delete") Then

            load_Connection()
            connection.Open()
            Dim sql As String
            sql = "Delete from inc.admin where admin_id='" & idAcc & "' Limit 1"
            command = New MySqlCommand(sql, connection)
            reader = command.ExecuteReader
            connection.Close()

            ListView1.Items.Clear()
            table_load()
        End If
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Dim sql As String

        load_Connection()

        connection.Open()
        idAcc = ListView1.SelectedItems(0).SubItems(0).Text

        sql = "select * from inc.admin where admin_id='" & idAcc & "'"
        command = New MySqlCommand(sql, connection)
        reader = command.ExecuteReader

        table_load()
        ListView1.SelectedItems.Clear()
        ListView1.Refresh()
        connection.Close()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        
    End Sub

    Private Sub frmAdmin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        table_load()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnBack_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        frmGasolineMain.Show()
        Me.Hide()
    End Sub
End Class