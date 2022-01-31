Imports System.Data.SqlClient
Public Class voitures
    Public dr As SqlDataReader
    Public cmd As SqlCommand

    Public dr2 As SqlDataReader
    Public cmd2 As SqlCommand

    Private Sub afficher()
        cn.Open()
        Dim listems As ListViewItem
        Dim str As String = "select*from produits"
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            listems = Me.ListView1.Items.Add(dr("Numero_produit"))
            listems.SubItems.Add(dr("Voiture"))
            listems.SubItems.Add(dr("prix_u"))
            listems.SubItems.Add(dr("Marque"))
            listems.SubItems.Add(dr("prix_vente"))
            listems.SubItems.Add(dr("Numero_sach"))

        End While
        dr.Close()

        Dim str2 As String = "select count(*) as Total,Max(Prix_u) as Prix_Max,Min(Prix_u) as Prix_Min from produits"
        cmd2 = New SqlCommand(str2, cn)
        dr2 = cmd2.ExecuteReader
        dr2.Read()
        Me.total.Text = "Nombre Voiture " & dr2.Item(0).ToString
        Me.Prix_Max.Text = "Prix Max " & dr2.Item(1).ToString
        Me.Prix_Min.Text = "Prix Min " & dr2.Item(2).ToString


        dr2.Close()

        cn.Close()
    End Sub
    Private Sub type()
        cn.Open()

        Dim str As String = "select distinct Marque from produits"
        cmd = New SqlCommand(str, cn)
        dr = cmd.ExecuteReader
        While (dr.Read)
            Me.Marque.Items.Add(dr("Marque")).ToString()
        End While
        dr.Close()
        cn.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        afficher()
        type()

    End Sub

    Private Sub ComboType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Marque.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Or Marque.Text = Nothing Or TextBox4.Text = Nothing Or TextBox5.Text = Nothing Then
            MsgBox("Remplir Tout Les Champs Pour Ajouter")
        Else


            If MsgBox("Vouler Vous Vraiment Ajouter ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "insert into produits values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & Marque.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')"

                cmd.ExecuteNonQuery()
                MsgBox("Ajouter Avec Succee :)")
                cn.Close()
                afficher()
            End If

        End If
    End Sub

    Private Sub ListView1_Click(sender As Object, e As EventArgs) Handles ListView1.Click
        If ListView1.SelectedItems.Count = 0 Then
            MsgBox("Selectionner Un Produit !")
        Else
            cn.Open()
            cmd.CommandText = "select * from produits where Numero_produit='" & ListView1.SelectedItems(0).Text.ToString & "'"
            cmd.ExecuteNonQuery()

            Dim dr As SqlDataReader = cmd.ExecuteReader()
            If dr.Read Then
                Me.TextBox1.Text = dr.Item(0).ToString
                Me.TextBox2.Text = dr.Item(1).ToString
                Me.TextBox3.Text = dr.Item(2).ToString
                Me.TextBox3.Text = Replace(TextBox3.Text, ",", ".")
                Me.Marque.Text = dr.Item(3).ToString
                Me.TextBox4.Text = dr.Item(4).ToString
                Me.TextBox4.Text = Replace(TextBox4.Text, ",", ".")

                Me.TextBox5.Text = dr.Item(5).ToString
                dr.Close()
            End If
            Dim str2 As String = "select coalesce( sum(coalesce(Qte,0)),0) from achats where Numero_achat='" & ListView1.SelectedItems(0).Text.ToString & "'"
            cmd2 = New SqlCommand(str2, cn)

            dr2 = cmd2.ExecuteReader()
            If dr2.Read Then
                Lab1.Text = dr2.Item(0).ToString
                dr2.Close()


            End If

            cn.Close()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Or Marque.Text = Nothing Or TextBox4.Text = Nothing Or TextBox5.Text = Nothing Then
            MsgBox("Remplir Tout Les Champs Pour Modifier")
        Else
            If MsgBox("Vouler Vous Vraiment Modifier ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "update produits set Voiture='" & TextBox2.Text & "', prix_u = " & TextBox3.Text & ",Marque='" & Marque.Text & "',prix_vente=" & TextBox4.Text & ",Numero_sach='" & TextBox5.Text & "' where Numero_produit=(" & TextBox1.Text & ")"
                cmd.ExecuteNonQuery()
                MsgBox("Modifier Avec Succee :)")
                cn.Close()
                afficher()

            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox1.Text = Nothing Or TextBox2.Text = Nothing Or TextBox3.Text = Nothing Or Marque.Text = Nothing Or TextBox4.Text = Nothing Or TextBox5.Text = Nothing Then
            MsgBox("Selectioner un Produit Pour Suprimer")
        Else
            If MsgBox("Vouler Vous Vraiment Suprimer ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                cn.Open()
                cmd.CommandText = "delete from produits where Numero_produit='" & TextBox1.Text & "'"
                cmd.ExecuteNonQuery()
                MsgBox("Supression Avec Succee :)")
                cn.Close()
                afficher()

            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = Nothing
        TextBox2.Text = Nothing
        TextBox3.Text = Nothing
        Marque.Text = Nothing
        TextBox4.Text = Nothing

        TextBox5.Text = Nothing
        TextBox1.Focus()

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextRecherche.TextChanged

    End Sub

    Private Sub TextBox6_KeyUp(sender As Object, e As KeyEventArgs) Handles TextRecherche.KeyUp
        cn.Open()
        Dim listems As ListViewItem
        Dim str As String = "select*from produits where " & Recherche.Text & " like '%' + @1 +  '%' "

        cmd = New SqlCommand(str, cn)
        cmd.Parameters.AddWithValue("@1", TextRecherche.Text)
        dr = cmd.ExecuteReader
        ListView1.Items.Clear()

        While (dr.Read)
            listems = Me.ListView1.Items.Add(dr("Numero_produit"))
            listems.SubItems.Add(dr("Voiture"))
            listems.SubItems.Add(dr("prix_u"))
            listems.SubItems.Add(dr("Marque"))
            listems.SubItems.Add(dr("prix_vente"))

            listems.SubItems.Add(dr("Numero_sach"))

        End While
        dr.Close()

        Dim str2 As String = "select count(*) as Total,Max(Prix_u) as Prix_Max,Min(Prix_u) as Prix_Min from produits where " & Recherche.Text & " like '%' + @1 +  '%' "
        cmd2 = New SqlCommand(str2, cn)
        cmd2.Parameters.AddWithValue("@1", TextRecherche.Text)
        dr2 = cmd2.ExecuteReader
        dr2.Read()
        Me.total.Text = "Nombre Voiture " & dr2.Item(0).ToString
        Me.Prix_Max.Text = "Prix Max " & dr2.Item(1).ToString
        Me.Prix_Min.Text = "Prix Min " & dr2.Item(2).ToString


        dr2.Close()

        cn.Close()
    End Sub

    Private Sub Recherche_TextChanged(sender As Object, e As EventArgs) Handles Recherche.TextChanged
        TextRecherche.Text = Nothing
        TextRecherche.Focus()
    End Sub

    Private Sub Lab3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class