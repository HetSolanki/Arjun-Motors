Public Class Shop
    Private Sub Shop_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim obj As SqlClient.SqlDataReader
        Dim d As New DAO
        obj = d.getData("Select * from Shop")
        While obj.Read
            TextBox9.Text = obj.Item("Shop_Name")
            TextBox10.Text = obj.Item("Shop_Owner")
            TextBox1.Text = obj.Item("Address")
            TextBox2.Text = obj.Item("Mobile_Number")
            TextBox3.Text = obj.Item("GST_Number")

        End While

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If TextBox9.Text <> "" Then
            If TextBox10.Text <> "" Then
                If TextBox1.Text <> "" Then
                    If TextBox2.Text <> "" Then
                        If TextBox3.Text <> "" Then
                            Dim d As New DAO
                            d.modifyData("Insert into Shop (Shop_Name, Shop_Owner, Address, Mobile_Number, GST_Number) values ('" & TextBox9.Text & "','" & TextBox10.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')")
                            MessageBox.Show("Detais Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Enter Valid Shop GST Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                    Else
                        MessageBox.Show("Enter Valid Shop Mobile Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Enter Valid Shop Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Enter Valid Shop Owner Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Enter Valid Shop Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Panel8.Visible = True
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim d As Integer = MessageBox.Show("Are you Sure to Close", "Close
", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If d = 6 Then
            Panel8.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class