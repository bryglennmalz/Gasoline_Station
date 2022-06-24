Imports MySql.Data.MySqlClient

Public Class frmAddEditAdmins

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim pangalan As String

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        Try
            If txtFname.Text = vbNullString Or txtMI.Text = vbNullString Or txtLname.Text = vbNullString Or txtUsername.Text = vbNullString Or txtPassword.Text = vbNullString Then
                MsgBox("Must Not Leave a blank", vbCritical, "message ")

                If txtFname.Text = vbNullString Then
                    Me.ErrorProvider1.SetError(Me.txtFname, "Must Not Leave a Blank")
                    Return
                Else
                    Me.ErrorProvider1.Clear()
                End If
                If txtMI.Text = vbNullString Then
                    Me.ErrorProvider1.SetError(Me.txtMI, "Must Not Leave a Blank")
                    Return
                Else
                    Me.ErrorProvider1.Clear()
                End If
                If txtLname.Text = vbNullString Then
                    Me.ErrorProvider1.SetError(Me.txtLname, "Must Not Leave a Blank")
                    Return
                Else
                    Me.ErrorProvider1.Clear()
                End If
                If txtUsername.Text = vbNullString Then
                    Me.ErrorProvider1.SetError(Me.txtUsername, "Must Not Leave a Blank")
                    Return
                Else
                    Me.ErrorProvider1.Clear()
                End If
                If txtPassword.Text = vbNullString Then
                    Me.ErrorProvider1.SetError(Me.txtPassword, "Must Not Leave a Blank")
                    Return
                Else
                    Me.ErrorProvider1.Clear()
                End If
            Else
                Dim p As Integer
                Dim cnt As Integer = frmAdmin.ListView1.Items.Count
                If Not cnt = 0 Then
                    For p = 0 To cnt - 1
                        If frmAdmin.ListView1.Items.Item(p).SubItems(2).Text = txtUsername.Text Then
                            MsgBox("Item already Existed!", vbInformation, "Record Existed")
                            Return
                            Exit For
                            Exit Sub
                        End If
                    Next
                End If
                'f frmAdmin.ListView1.Items.Item(p).SubItems(1).Text <> txtFullname.Text Then
                load_Connection()
                connection.Open()
                Dim query As String
                query = "insert into inc.admin (fname, mi, lname, username, pword)values ('" & txtFname.Text & "', '" & txtMI.Text & "', '" & txtLname.Text & "', '" & txtUsername.Text & "', '" & txtPassword.Text & "')"
                command = New MySqlCommand(query, connection)
                reader = command.ExecuteReader

                'MessageBox.Show("New Account Added!")
                MsgBox("New Admin Added! ", vbInformation, "")
                frmAdmin.ListView1.Items.Clear()
                frmAdmin.table_load()

                Me.Close()
                connection.Close()
                frmAdmin.Show()
                'End If
            End If

        Catch ex As MySqlException
            MsgBox(ex.Message, vbCritical, ex.Message)
        Finally
            connection.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        load_Connection()


        connection.Open()
        Dim query As String
        query = "update inc.admin set fname='" & txtFname.Text & "', mi='" & txtMI.Text & "', lname='" & txtLname.Text & "', username='" & txtUsername.Text & "', pword='" & txtPassword.Text & "'  where admin_id='" & txtID.Text & "' LIMIT 1"
        command = New MySqlCommand(query, connection)
        reader = command.ExecuteReader

        connection.Close()
        MessageBox.Show("Update Successfully!")
        connection.Close()

        frmProducts.table_load()

        txtID.Text = ""
        txtFname.Text = ""
        txtMI.Text = ""
        txtLname.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""

        Me.Close()
        frmAdmin.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If MsgBox("Cancel Operation?", vbYesNo, "Cancel Operation") = vbYes Then
            txtID.Text = ""
            txtFname.Text = ""
            txtMI.Text = ""
            txtLname.Text = ""
            txtUsername.Text = ""
            txtPassword.Text = ""
            frmAdmin.Show()
            Me.Hide()
        End If
    End Sub
End Class