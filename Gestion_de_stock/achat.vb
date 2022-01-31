Imports System.Data.SqlClient
Public Class achat
    Public dr As SqlDataReader
    Public cmd As SqlCommand

    Public dr2 As SqlDataReader
    Public cmd2 As SqlCommand


    Private Sub afficher()
        cn.Open()
        Dim listems As ListViewItem
        Dim str As String = "select*from achats"
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            listems = Me.ListView1.Items.Add(dr("Numero_achat"))
            listems.SubItems.Add(dr("Date"))
            listems.SubItems.Add(dr("Qte"))
            listems.SubItems.Add(dr("Numero_produit"))
            listems.SubItems.Add(dr("Numero_client"))
            listems.SubItems.Add(dr("Prix"))
        End While
        dr.Close()

        Dim str2 As String = "select count(*),sum(Qte),count (distinct Numero_achat)  from achats "
        cmd2 = New SqlCommand(str2, cn)
        dr2 = cmd2.ExecuteReader
        dr2.Read()
        Me.total.Text = "Nombre Achat " & dr2.Item(0).ToString
        Me.Qte.Text = "Somme Quantite  " & dr2.Item(1).ToString
        Me.Montent.Text = " Montent " & dr2.Item(2).ToString


        dr2.Close()


        cn.Close()

    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextNum.TextChanged


    End Sub

    Private Sub achat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        afficher()

        cn.Open()

        Dim str As String = "select Voiture from produits"
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        Combov.Items.Clear()
        While (dr.Read)
            Me.Combov.Items.Add(dr("Voiture"))


        End While
        dr.Close()
        cn.Close()

    End Sub

   

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextNum.Text = Nothing Or TextQte.Text = Nothing Or TextN_produit.Text = Nothing Or TextN_client.Text = Nothing Or DateTimePicker1.Text = Nothing Then
            MsgBox("Remplir Tout Les Champs Pour Ajouter")
        Else


            If MsgBox("Vouler Vous Vraiment Ajouter ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "insert into achats values('" & TextNum.Text & "','" & DateTimePicker1.Text & "','" & TextQte.Text & "','" & TextN_produit.Text & "','" & TextN_client.Text & "','" & textprix.Text & "')"

                cmd.ExecuteNonQuery()
                MsgBox("Ajouter Avec Succee :)")
                cn.Close()
                afficher()
            End If

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextNum.Text = Nothing
        TextQte.Text = Nothing
        DateTimePicker1.Text = Date.Now
        TextN_produit.Text = Nothing

        TextN_client.Text = Nothing
        TextNum.Focus()
    End Sub

    Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click
        If ListView1.SelectedItems.Count = 0 Then
            MsgBox("Selectionner Un Produit !")
        Else
            cn.Open()
            cmd.CommandText = "select * from achats where Numero_achat='" & ListView1.SelectedItems(0).Text.ToString & "'"
            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()
            If dr.Read Then
                Me.TextNum.Text = dr.Item(0).ToString
                Me.DateTimePicker1.Text = dr.Item(1).ToString
                Me.TextQte.Text = dr.Item(2).ToString
                Me.TextN_produit.Text = dr.Item(3).ToString
                Me.TextN_client.Text = dr.Item(4).ToString
                Me.textprix.Text = dr.Item(5).ToString

                dr.Close()
            End If
            cn.Close()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextNum.Text = Nothing Or TextQte.Text = Nothing Or TextN_produit.Text = Nothing Or TextN_client.Text = Nothing Or DateTimePicker1.Text = Nothing Then
            MsgBox("Remplir Tout Les Champs Pour Modifier")
        Else
            If MsgBox("Vouler Vous Vraiment Modifier ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "update achats set Date='" & DateTimePicker1.Text & "', Qte = " & TextQte.Text & ",Numero_produit='" & TextN_produit.Text & "',Numero_client='" & TextN_client.Text & "',Prix='" & textprix.Text & "' where Numero_achat=(" & TextNum.Text & ")"

                cmd.ExecuteNonQuery()
                MsgBox("Modifier Avec Succee :)")
                cn.Close()
                afficher()

            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextNum.Text = Nothing Or TextQte.Text = Nothing Or TextN_produit.Text = Nothing Or TextN_client.Text = Nothing Or DateTimePicker1.Text = Nothing Then
            MsgBox("Selectioner un achat Pour Suprimer")
        Else
            If MsgBox("Vouler Vous Vraiment Suprimer ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "delete from achats where Numero_achat='" & TextNum.Text & "'"
                cmd.ExecuteNonQuery()
                MsgBox("Supression Avec Succee :)")
                cn.Close()
                afficher()

            End If
        End If
    End Sub

    Private Sub TextRecherche_KeyUp(sender As Object, e As KeyEventArgs) Handles TextRecherche.KeyUp
        cn.Open()
        Dim listems As ListViewItem
        Dim str As String = "select*from achats where " & Recherche.Text & " like '%' + @1 +  '%' "

        cmd = New SqlCommand(str, cn)
        cmd.Parameters.AddWithValue("@1", TextRecherche.Text)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            listems = Me.ListView1.Items.Add(dr("Numero_achat"))
            listems.SubItems.Add(dr("Date"))

            listems.SubItems.Add(dr("Numero_Produit"))

            listems.SubItems.Add(dr("Numero_client"))
            listems.SubItems.Add(dr("Prix"))

        End While
        dr.Close()
        Dim str2 As String = "select count(*),sum(Qte),count (distinct Numero_produit)  from achats where " & Recherche.Text & " like '%' + @1 +  '%' "
        cmd2 = New SqlCommand(str2, cn)
        cmd2.Parameters.AddWithValue("@1", TextRecherche.Text)
        dr2 = cmd2.ExecuteReader
        dr2.Read()
        Me.total.Text = "Nombre Achat " & dr2.Item(0).ToString
        Me.Qte.Text = "Somme Quantite  " & dr2.Item(1).ToString
        Me.Montent.Text = " Total Produit " & dr2.Item(2).ToString


        dr2.Close()
        cn.Close()

    End Sub

    Private Sub Recherche_TextChanged(sender As Object, e As EventArgs) Handles Recherche.TextChanged
        TextRecherche.Text = Nothing
        TextRecherche.Focus()
    End Sub



    Private Sub Combov_TextChanged(sender As Object, e As EventArgs) Handles Combov.TextChanged

        cn.Open()

        Dim str As String = "select Numero_produit,prix_vente from produits where voiture='" & Combov.Text & "'"
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        TextN_produit.Text = Nothing
        textprix.Text = Nothing
        While (dr.Read)
            TextN_produit.Text = dr.Item("Numero_produit").ToString
            textprix.Text = dr.Item("prix_vente").ToString
        End While
        dr.Close()
        cn.Close()

    End Sub

    Private Sub Combov_Click(sender As Object, e As EventArgs) Handles Combov.Click

    End Sub

    Private Sub TextN_client_TextChanged(sender As Object, e As EventArgs) Handles TextN_client.TextChanged

    End Sub
End Class