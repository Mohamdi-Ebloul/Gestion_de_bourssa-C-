

Imports System.Data.SqlClient
Public Class user

    Public dr As SqlDataReader
    Public cmd As SqlCommand

    Public dr2 As SqlDataReader
    Public cmd2 As SqlCommand
    Private Sub afficher()
        cn.Open()
        Dim listems As ListViewItem
        Dim str As String = "select*from users"
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            listems = Me.ListView1.Items.Add(dr("Numero_user"))
            listems.SubItems.Add(dr("login"))

            listems.SubItems.Add(dr("Nom"))
            listems.SubItems.Add(dr("Prenom"))
            listems.SubItems.Add(dr("Tel"))
            listems.SubItems.Add(dr("Type"))



        End While
        dr.Close()
        cn.Close()

    End Sub
    Private Sub user_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        afficher()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextNum.Text = Nothing
        TextNom.Text = Nothing

        TextPrenom.Text = Nothing
        TextNom.Text = Nothing
        TextPW.Text = Nothing
        TextPrenom.Text = Nothing
        ComboBox1.Text = Nothing
        Textlogin.Text = Nothing
        TextTel.Text = Nothing
        
        TextNum.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextPrenom.Text = Nothing Or TextNom.Text = Nothing Or TextPW.Text = Nothing Or TextPrenom.Text = Nothing Or ComboBox1.Text = Nothing Or Textlogin.Text = Nothing Or TextTel.Text = Nothing Then
            MsgBox("Remplir Tout Les Champs Pour Ajouter")
        Else


            If MsgBox("Vouler Vous Vraiment Ajouter ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "insert into users values('" & TextNum.Text & "','" & Textlogin.Text & "','" & TextPW.Text & "','" & TextNom.Text & "','" & TextPrenom.Text & "','" & TextTel.Text & "','" & ComboBox1.Text & "')"

                cmd.ExecuteNonQuery()
                MsgBox("Ajouter Avec Succee :)")
                cn.Close()
                afficher()
            End If

        End If
        cn.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextPrenom.Text = Nothing Or TextNom.Text = Nothing Or TextPW.Text = Nothing Or TextPrenom.Text = Nothing Or ComboBox1.Text = Nothing Or Textlogin.Text = Nothing Or TextTel.Text = Nothing Then
            MsgBox("Remplir Tout Les Champs Pour Modifier")
        Else
            If MsgBox("Vouler Vous Vraiment Modifier ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "update users set Nom='" & TextNom.Text & "', Prenom =' " & TextPrenom.Text & "',Tel='" & TextTel.Text & "',Login='" & Textlogin.Text & "',Password='" & TextPW.Text & "',Type='" & ComboBox1.Text & "'  where Numero_user=(" & TextNum.Text & ")"

                cmd.ExecuteNonQuery()
                MsgBox("Modifier Avec Succee :)")
                cn.Close()
                afficher()

            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextPrenom.Text = Nothing Or TextNom.Text = Nothing Or TextPW.Text = Nothing Or TextPrenom.Text = Nothing Or ComboBox1.Text = Nothing Or Textlogin.Text = Nothing Or TextTel.Text = Nothing Then
            MsgBox("Selectioner une vente Pour Suprimer")
        Else
            If MsgBox("Vouler Vous Vraiment Suprimer ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "delete from users where Numero_user='" & TextNum.Text & "'"
                cmd.ExecuteNonQuery()
                MsgBox("Supression Avec Succee :)")
                cn.Close()
                afficher()

            End If
        End If
    End Sub

    Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click
        If ListView1.SelectedItems.Count = 0 Then
            MsgBox("Selectionner Un client!")
        Else
            cn.Open()
            cmd.CommandText = "select * from users where Numero_user='" & ListView1.SelectedItems(0).Text.ToString & "'"
            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()
            If dr.Read Then
                Me.TextNum.Text = dr.Item(0).ToString
                Me.Textlogin.Text = dr.Item(1).ToString
                Me.TextPW.Text = dr.Item(2).ToString
                Me.TextNom.Text = dr.Item(3).ToString
                Me.TextPrenom.Text = dr.Item(4).ToString
                Me.TextTel.Text = dr.Item(5).ToString
                Me.ComboBox1.Text = dr.Item(6).ToString


                dr.Close()
            End If
            cn.Close()
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class