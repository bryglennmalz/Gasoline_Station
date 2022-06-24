Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmGasoline

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim pangalan, first, mi, last As String

    Public isamount As Boolean

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"

    End Sub

    Public Sub load_type()
        load_Connection()

        connection.Open()

        Dim query As String
        query = "select distinct product_name from inc.product where available = 'Available'"
        command = New MySqlCommand(query, connection)
        reader = command.ExecuteReader

        While reader.Read
            cmbType.Items.Add(reader.GetString(0))
        End While
        connection.Close()
    End Sub

    Private Sub frmGasoline_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        load_Connection()
        connection.Open()
        load_type()
    End Sub

    Private Sub AdminCornerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminCornerToolStripMenuItem.Click
        frmLogin.Show()
        Me.Hide()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        frmAddEditAdmins.Close()
        frmAddEditProducts.Close()
        frmAdmin.Close()
        frmGasolineMain.Close()
        frmLogin.Close()
        frmLogs.Close()
        frmPayment.Close()
        frmProducts.Close()
        frmGasolineRefill.Close()
        Me.Close()
    End Sub

    Public Sub refill()
        load_Connection()
        connection.Open()

        Dim available As String
        Dim presyos As String
        Dim pangalan As String
        Dim availLiters As String

        Dim query As String
        query = "select * from inc.product where product_name = '" & cmbType.Text & "'"
        command = New MySqlCommand(query, connection)
        reader = command.ExecuteReader

        While reader.Read
            Gasid = reader.GetInt32("id")
            pangalan = reader.GetString("product_name")
            presyos = reader.GetString("product_price")
            availLiters = reader.GetString("content")
            available = reader.GetString("available")
        End While
        connection.Close()

        Dim AL As Double = availLiters
        Dim P As Double = presyos
        If available = "Available" Then
            frmGasolineRefill.txtPrice.Text = P.ToString("###,###.00")
            frmGasolineRefill.txtAvailable.Text = AL.ToString("###,###.00")

            frmGasolineRefill.btnAmount.Enabled = True
            frmGasolineRefill.btnLiter.Enabled = True
            'frmGasolineMain.Show()
            'Me.hide()
        ElseIf available = "Not available" Then
            MsgBox("Product Unavailable.", vbInformation, "Message")
            cmbType.Text = ""
        Else
            MsgBox("Incorrect Identifier.", vbInformation, "Message")
        End If
    End Sub

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
            first = reader.GetString("fname")
            mi = reader.GetString("mi")
            last = reader.GetString("lname")
            code = reader.GetString("usercode")
            txtUserCode.Text = ""

            connection.Close()

            Dim s As StreamWriter
            Dim portfolioPath As String = My.Application.Info.DirectoryPath
            If Not Directory.Exists("C:\GasolineLogs") Then
                Directory.CreateDirectory("C:\GasolineLogs")
                File.Create("C:\GasolineLogs\gasolinemanlogs.txt").Close()
                s = New StreamWriter("C:\GasolineLogs\gasolinemanlogs.txt", True)
                s.WriteLine(first & " " & mi & ". " & last & " has successfully logged in on " & Date.Now)
                s.Flush()
                s.Close()
            Else
                s = New StreamWriter("C:\GasolineLogs\gasolinemanlogs.txt", True)
                s.WriteLine(first & " " & mi & ". " & last & " has successfully logged in on " & Date.Now)
                s.Flush()
                s.Close()
            End If

            'frmGasolineMain.Label1.Text = pangalan
            frmGasolineRefill.lblUser.Text = first & " " & mi & ". " & last
            frmGasolineRefill.txtFuelType.Text = cmbType.Text
            frmGasolineRefill.Show()
            Me.Hide()

        Else
            MessageBox.Show("Usercode Error!" & vbNewLine & "Input a valid code.")
        End If
        connection.Close()
        refill()

    End Sub
End Class