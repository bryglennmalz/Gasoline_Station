Imports MySql.Data.MySqlClient
Imports System.IO

Public Class frmLogin

    Dim connection As MySqlConnection
    Dim command As MySqlCommand
    Dim reader As MySqlDataReader
    Dim first, middle, last As String

    Private Sub load_Connection()
        connection = New MySqlConnection
        connection.ConnectionString = "server=localhost; port=3306; userid=root; password=''; database=inc"
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        txtPassword.Text = ""
        txtUsername.Text = ""
        frmGasoline.Show()
        Me.Hide()

    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        load_Connection()

        connection.Open()
        Dim query As String
        query = "select * from inc.admin where username='" & txtUsername.Text & "' and pword='" & txtPassword.Text & "'"
        command = New MySqlCommand(query, connection)
        reader = command.ExecuteReader
        Dim count As Integer
        While reader.Read
            count = count + 1
        End While

        If count = 1 Then
            acc_id = reader.GetInt32("admin_id")
            first = reader.GetString("fname")
            middle = reader.GetString("mi")
            last = reader.GetString("lname")
            txtPassword.Text = ""
            txtUsername.Text = ""

            connection.Close()

            Dim s As StreamWriter
            Dim portfolioPath As String = My.Application.Info.DirectoryPath
            If Not Directory.Exists("C:\GasolineLogs") Then
                Directory.CreateDirectory("C:\GasolineLogs")
                File.Create("C:\GasolineLogs\logs.txt").Close()
                s = New StreamWriter("C:\GasolineLogs\logs.txt", True)
                s.WriteLine(first & " " & mi & ". " & last & " has successfully logged in on " & Date.Now)
                s.Flush()
                s.Close()
            Else
                s = New StreamWriter("C:\GasolineLogs\logs.txt", True)
                s.WriteLine(first & " " & mi & ". " & last & " has successfully logged in on " & Date.Now)
                s.Flush()
                s.Close()
            End If

            'frmGasolineMain.Label1.Text = pangalan
            frmGasolineMain.Show()
            Me.Hide()

        Else
            MessageBox.Show("Username or Password Error!" & vbNewLine & "Or account is currently used")
        End If
    End Sub

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class