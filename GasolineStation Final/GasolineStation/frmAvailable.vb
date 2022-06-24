Imports MySql.Data.MySqlClient

Public Class frmAvailable

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim pangalan As String
    Dim idAcc As Integer

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        load_Connection()

        If vbYes = MsgBox("Update this Product?", vbYesNo, "Update") Then

            connection.Open()
            Dim query As String
            query = "update inc.product set content='" & txtLiters.Text & "' available='" & cmbStatus.Text & "'  where id='" & txtProdID.Text & "' LIMIT 1"
            command = New MySqlCommand(query, connection)
            reader = command.ExecuteReader

            connection.Close()
            MessageBox.Show("Update Successfully!")
            connection.Close()

            frmProducts.table_load()

            Me.Close()
        End If
    End Sub
End Class