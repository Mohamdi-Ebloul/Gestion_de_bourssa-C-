
Imports System.Data.SqlClient
Public Class client

    Public dr As SqlDataReader
    Public cmd As SqlCommand

    Public dr2 As SqlDataReader
    Public cmd2 As SqlCommand
    Private Sub afficher()
        cn.Open()
        Dim listems As ListViewItem
        Dim str As String = "select*from clients"
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            listems = Me.ListView1.Items.Add(dr("Numero_client"))
            listems.SubItems.Add(dr("Nom"))
            listems.SubItems.Add(dr("Adresse"))
            listems.SubItems.Add(dr("Tel"))



        End While
        dr.Close()
        cn.Close()

    End Sub
    Private Sub client_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        afficher()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextNum.Text = Nothing
        TextNom.Text = Nothing

        TextAdresse.Text = Nothing
        TextTel.Text = Nothing


        TextNum.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextNum.Text = Nothing Or TextNom.Text = Nothing Or TextAdresse.Text = Nothing Or TextTel.Text = Nothing Then
            MsgBox("Remplir Tout Les Champs Pour Ajouter")
        Else


            If MsgBox("Vouler Vous Vraiment Ajouter ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "insert into clients values('" & TextNum.Text & "','" & TextNom.Text & "','" & TextAdresse.Text & "','" & TextTel.Text & "')"

                cmd.ExecuteNonQuery()
                MsgBox("Ajouter Avec Succee :)")
                cn.Close()
                afficher()
            End If

        End If
        cn.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextNum.Text = Nothing Or TextNom.Text = Nothing Or TextAdresse.Text = Nothing Or TextTel.Text = Nothing Then
            MsgBox("Remplir Tout Les Champs Pour Modifier")
        Else
            If MsgBox("Vouler Vous Vraiment Modifier ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "update clients set Nom='" & TextNom.Text & "', Adresse =' " & TextAdresse.Text & "',Tel='" & TextTel.Text & "'where Numero_client=(" & TextNum.Text & ")"

                cmd.ExecuteNonQuery()
                MsgBox("Modifier Avec Succee :)")
                cn.Close()
                afficher()

            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextNum.Text = Nothing Or TextNom.Text = Nothing Or TextAdresse.Text = Nothing Or TextTel.Text = Nothing Then
            MsgBox("Selectioner une vente Pour Suprimer")
        Else
            If MsgBox("Vouler Vous Vraiment Suprimer ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "delete from clients where Numero_client='" & TextNum.Text & "'"
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
            cmd.CommandText = "select * from clients where Numero_client='" & ListView1.SelectedItems(0).Text.ToString & "'"
            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()
            If dr.Read Then
                Me.TextNum.Text = dr.Item(0).ToString

                Me.TextNom.Text = dr.Item(1).ToString
                Me.TextAdresse.Text = dr.Item(2).ToString
                Me.TextTel.Text = dr.Item(3).ToString


                dr.Close()
            End If
            cn.Close()
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class