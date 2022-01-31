Imports System.Data.SqlClient
Public Class log

    Public dr As SqlDataReader
    Public cmd As SqlCommand
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If login.Text = Nothing Then
            MessageBox.Show("Remplir Login !")
        ElseIf pw.Text = Nothing Then
            MessageBox.Show("Remplir Mot de Passe !")
        Else

            cn.Open()
            Dim str As String = "select*from users where Login='" & login.Text & "' and Password='" & pw.Text & "'"
            cmd = New SqlCommand(str, cn)
            dr = cmd.ExecuteReader
            If dr.Read Then
                connection.N = dr.Item("Numero_user").ToString
                connection.nom = dr.Item("Nom").ToString
                connection.prenom = dr.Item("Prenom").ToString
                acceil.Show()
                dr.Close()
                cn.Close()
                Me.Hide()
            Else
                MessageBox.Show("Mot de Passe Incorect ")
                dr.Close()
                cn.Close()

            End If
            dr.Close()
            cn.Close()
        End If
    End Sub

    Private Sub log_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub login_TextChanged(sender As Object, e As EventArgs) Handles login.TextChanged

    End Sub
End Class