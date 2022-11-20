Public Class User_Master

    Dim flag As Integer = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.Text = ""
        flag = 0
        Button3.Enabled = False
        TextBox1.Focus()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text <> "" Then
            If TextBox2.Text <> "" Then
                If TextBox5.Text <> "" Then
                    If TextBox4.Text <> "" Then

                        If ComboBox1.Text <> "" Then
                            If flag = 0 Then
                                Dim dao As New DAO
                                Dim f As Integer = 0
                                f = dao.validate("Select Username from Admin where Username = '" & TextBox1.Text & "'")
                                If f = 1 Then
                                    MessageBox.Show("Unable To Create User, it's Already Exists")
                                    TextBox1.Focus()
                                Else
                                    dao.modifyData("insert into Admin (Username, Password, U_type, Email, Mobile_No) values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "','" & TextBox4.Text & "'," & TextBox5.Text & ")")
                                    loaddata()
                                    MessageBox.Show("User Created")
                                    Button1_Click(sender, e)
                                End If
                            Else
                                'Update
                            End If
                        Else
                            MessageBox.Show("Select Valid User Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            ComboBox1.Focus()

                        End If
                    Else
                        MessageBox.Show("Enter Valid Email-Id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        TextBox4.Focus()
                    End If
                Else
                    MessageBox.Show("Enter Valid Mobile Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TextBox5.Focus()
                End If
            Else
                MessageBox.Show("Enter Valid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox2.Focus()
            End If
        Else
            MessageBox.Show("Enter Valid Username", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Focus()
        End If
    End Sub


    Private Sub User_Master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        loaddata()
    End Sub

    Private Sub loaddata()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Admin")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Rows.Count > 0 Then
            TextBox3.Text = DataGridView1.Item("Id", DataGridView1.CurrentCell.RowIndex).Value
            TextBox1.Text = DataGridView1.Item("Username", DataGridView1.CurrentCell.RowIndex).Value
            TextBox2.Text = DataGridView1.Item("Password", DataGridView1.CurrentCell.RowIndex).Value
            TextBox4.Text = DataGridView1.Item("Email", DataGridView1.CurrentCell.RowIndex).Value
            ComboBox1.Text = DataGridView1.Item("U_Type", DataGridView1.CurrentCell.RowIndex).Value
            TextBox5.Text = DataGridView1.Item("Mobile_No", DataGridView1.CurrentCell.RowIndex).Value
            flag = 1
            Button3.Enabled = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim c As Integer = MessageBox.Show("Do You Want Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If c = 6 Then
            Dim d As New DAO
            d.modifyData("Delete from Admin where id = '" & TextBox3.Text & "'")
            loaddata()
            Button3.Enabled = False
            Button1_Click(sender, e)
        End If


    End Sub
End Class