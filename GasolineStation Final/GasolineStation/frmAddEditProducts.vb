Imports MySql.Data.MySqlClient

Public Class frmAddEditProducts


    Dim pangalan As String
    Dim existss As Boolean

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Hide()
        txtProdID.Text = ""
        txtName.Text = ""
        txtLiters.Text = ""
        txtPrice.Text = ""
        cmbStatus.Text = ""
        frmProducts.Show()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim ll As Double
        Dim pp As Double
        txtPrice.Text = pp.ToString("###,###.00")
        txtLiters.Text = ll.ToString("###,###.00")
        Try
            If txtLiters.Text = vbNullString Or txtName.Text = vbNullString Or txtPrice.Text = vbNullString Or cmbStatus.Text = vbNullString Then
                MsgBox("Must Not Leave a blank", vbCritical, "message ")

                If txtName.Text = vbNullString Then
                    Me.ErrorProvider1.SetError(Me.txtName, "Must Not Leave a Blank")
                    Return
                Else
                    Me.ErrorProvider1.Clear()
                End If
                If txtPrice.Text = vbNullString Then
                    Me.ErrorProvider1.SetError(Me.txtPrice, "Must Not Leave a Blank")
                    Return
                Else
                    Me.ErrorProvider1.Clear()
                End If
                If txtLiters.Text = vbNullString Then
                    Me.ErrorProvider1.SetError(Me.txtLiters, "Must Not Leave a Blank")
                    Return
                Else
                    Me.ErrorProvider1.Clear()
                End If
                If cmbStatus.Text = vbNullString Then
                    Me.ErrorProvider1.SetError(Me.cmbStatus, "Must Not Leave a Blank")
                    Return
                Else
                    Me.ErrorProvider1.Clear()
                End If


            Else
                ' MsgBox("Must Not Leave a blank", vbCritical, "message ")
                If Not IsNumeric(txtLiters.Text) Or Not IsNumeric(txtPrice.Text) Then
                    ' MsgBox("not")
                    If txtLiters.Text = vbNullString Then
                        Me.ErrorProvider1.SetError(Me.txtLiters, "Must Be Numeric")
                        Return
                    Else
                        Me.ErrorProvider1.Clear()
                    End If
                    If txtPrice.Text = vbNullString Then
                        Me.ErrorProvider1.SetError(Me.txtPrice, "Must Be Numeric")
                        Return
                    Else
                        Me.ErrorProvider1.Clear()
                    End If
                Else
                    'If txtLiters.Text <= 5 And cmbStatus.Text = "Available" Then
                    '    MsgBox("The amount of Liters you entered is good for personal use,", vbInformation, "")
                    'End If
                    connection.Open()
                    query = "SELECT * FROM product where product_name='" & txtName.Text & "'"
                    command = New MySqlCommand(query, connection)
                    reader = command.ExecuteReader
                    counts = 0
                    While reader.Read
                        counts = counts + 1
                    End While

                    connection.Close()
                    If counts = 1 Then
                        MsgBox("Product Name Already Exist", vbInformation, "Message")
                        Return
                    Else
                        connection.Open()
                        query = "insert into product (product_name,product_price,content,`available`) values ('" & txtName.Text & "','" & CDbl(txtPrice.Text) & "','" & txtLiters.Text & "','" & cmbStatus.Text & "')"
                        command = New MySqlCommand(query, connection)
                        command.ExecuteNonQuery()

                        connection.Close()
                        frmProducts.ListView1.Items.Clear()
                        frmProducts.table_load()
                        frmGasoline.load_type()

                        MsgBox("Product Saved", vbInformation, "Message")
                        Me.Close()
                        connection.Close()
                        frmProducts.Show()
                    End If
                End If
                frmProducts.Show()
            End If
        Catch ex As MySqlException
            MsgBox(ex.Message, vbCritical, ex.Message)
        Finally
            connection.Dispose()
        End Try

        
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        load_Connection()

        If vbYes = MsgBox("Update this Product?", vbYesNo, "Update") Then

            connection.Open()
            Dim query As String
            query = "update inc.product set product_name='" & txtName.Text & "', product_price='" & txtPrice.Text & "', content='" & txtLiters.Text & "'  where id='" & txtProdID.Text & "' LIMIT 1"
            command = New MySqlCommand(query, connection)
            reader = command.ExecuteReader

            connection.Close()
            MessageBox.Show("Update Successfully!")
            connection.Close()

            frmProducts.table_load()
            frmGasoline.load_type()

            Me.Close()
        End If
    End Sub

    Private Sub frmAddEditProducts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        load_Connection()
        existss = True
    End Sub

    Private Sub txtPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice.TextChanged

    End Sub
End Class