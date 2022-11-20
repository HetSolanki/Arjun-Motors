Public Class Product_Master
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Panel4.Visible = True
    End Sub

    Private Sub product_Click(sender As Object, e As EventArgs) Handles Me.Click
        Panel4.Visible = False
        Panel1.Visible = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel5.Visible = True
        ComboBox2.Focus()
    End Sub


    Private Sub loaddata()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Product")
        DataGridView3.DataSource = ds.Tables(0)
    End Sub
    Private Sub product_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Panel5.Visible = False
        loaddata()
        Load_Product_Data()
    End Sub

    Public Sub Load_Product_Data()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select Product_Name, Sum(Quantity) from Product group by Product_Name")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub


    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim d As Integer = MessageBox.Show("Are you Sure to Close", "Close
", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If d = 6 Then
            Panel5.Visible = False
        End If
    End Sub



    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Dim flag As Integer = 0
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ComboBox2.Text <> "" Then
            If TextBox5.Text <> "" Then
                If TextBox2.Text <> "" Then
                    If TextBox3.Text <> "" Then
                        If TextBox8.Text <> "" Then
                            If TextBox10.Text <> "" Then
                                If ComboBox1.Text <> "" Then
                                    If TextBox4.Text <> "" Then
                                        If TextBox6.Text <> "" Then
                                            If TextBox7.Text <> "" Then
                                                If flag = 0 Then
                                                    Dim f As Integer = 0
                                                    Dim d As New DAO
                                                    f = d.validate("Select id from Product where id = '" & TextBox9.Text & "'")
                                                    If f = 0 Then
                                                        'INSERT
                                                        d.modifyData("Insert into Product (Product_Name, HSN_Code, Sale_Price, Purchase_Price, Vehicle_Name, S_GST, C_GST, Quantity, Product_Category, Product_Unit) Values ('" & TextBox5.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox6.Text & "','" & TextBox8.Text & "','" & TextBox10.Text & "','" & TextBox7.Text & "','" & ComboBox2.Text & "','" & ComboBox1.Text & "')")
                                                        loaddata()
                                                        flag = 1
                                                        MessageBox.Show("Product Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                    Else
                                                        d.modifyData("Update Product set Product_Name = '" & TextBox5.Text & "', HSN_Code = '" & TextBox2.Text & "', Sale_Price = '" & TextBox3.Text & "', Purchase_Price = '" & TextBox4.Text & "', Vehicle_Name = '" & TextBox6.Text & "', S_GST = '" & TextBox8.Text & "', C_GST = '" & TextBox10.Text & "', Quantity = '" & TextBox7.Text & "', Product_Category = '" & ComboBox2.Text & "', Product_Unit = '" & ComboBox1.Text & "' where id = '" & TextBox9.Text & "'")
                                                        loaddata()
                                                        resetfields()
                                                        MessageBox.Show("Record Updated")
                                                        flag = 0
                                                    End If
                                                Else
                                                    'UPDATE
                                                    Dim d As New DAO
                                                    d.modifyData("Update Product set Product_Name = '" & TextBox5.Text & "', HSN_Code = '" & TextBox2.Text & "', Sale_Price = '" & TextBox3.Text & "', Purchase_Price = '" & TextBox4.Text & "', Vehicle_Name = '" & TextBox6.Text & "', S_GST = '" & TextBox8.Text & "', C_GST = '" & TextBox10.Text & "', Quantity = '" & TextBox7.Text & "', Product_Category = '" & ComboBox2.Text & "', Product_Unit = '" & ComboBox1.Text & "' where id = '" & TextBox9.Text & "'")
                                                    loaddata()
                                                    resetfields()
                                                    MessageBox.Show("Record Updated")
                                                    flag = 0
                                                End If
                                            Else
                                                MessageBox.Show("Enter Valid Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                TextBox7.Focus()
                                            End If
                                        Else
                                            MessageBox.Show("Enter Valid Vehicle Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            TextBox6.Focus()
                                        End If
                                    Else
                                        MessageBox.Show("Enter Valid Purchase Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        TextBox4.Focus()
                                    End If
                                Else
                                    MessageBox.Show("Select Valid Product Unit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    ComboBox1.Focus()
                                End If
                            Else
                                MessageBox.Show("Enter Valid C_GST%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                ComboBox1.Focus()
                            End If

                        Else
                            MessageBox.Show("Enter Valid S_GST%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            TextBox8.Focus()
                        End If
                    Else
                        MessageBox.Show("Enter Valid Sale Price", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        TextBox3.Focus()
                    End If
                Else
                    MessageBox.Show("Enter Valid HSN Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TextBox2.Focus()
                End If
            Else
                MessageBox.Show("Enter Valid Product Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox5.Focus()
            End If
        Else
            MessageBox.Show("Select Valid Product Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox2.Focus()
        End If
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If DataGridView3.Rows.Count > 0 Then
            TextBox9.Text = DataGridView3.Item("Id", DataGridView3.CurrentCell.RowIndex).Value
            TextBox5.Text = DataGridView3.Item("Product_Name", DataGridView3.CurrentCell.RowIndex).Value
            TextBox2.Text = DataGridView3.Item("HSN_Code", DataGridView3.CurrentCell.RowIndex).Value
            TextBox3.Text = DataGridView3.Item("Sale_Price", DataGridView3.CurrentCell.RowIndex).Value
            TextBox4.Text = DataGridView3.Item("Purchase_Price", DataGridView3.CurrentCell.RowIndex).Value
            TextBox6.Text = DataGridView3.Item("Vehicle_Name", DataGridView3.CurrentCell.RowIndex).Value
            TextBox7.Text = DataGridView3.Item("Quantity", DataGridView3.CurrentCell.RowIndex).Value
            TextBox8.Text = DataGridView3.Item("S_GST", DataGridView3.CurrentCell.RowIndex).Value
            ComboBox2.Text = DataGridView3.Item("Product_Category", DataGridView3.CurrentCell.RowIndex).Value
            ComboBox1.Text = DataGridView3.Item("Product_Unit", DataGridView3.CurrentCell.RowIndex).Value
            TextBox10.Text = DataGridView3.Item("C_GST", DataGridView3.CurrentCell.RowIndex).Value
            Button5.Enabled = True
        End If

    End Sub

    Private Sub resetfields()
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        ComboBox2.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Focus()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim c As Integer = MessageBox.Show("Do You Want to Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If c = 6 Then
            Dim d As New DAO
            d.modifyData("delete from Product where id = '" & TextBox9.Text & "'")
            loaddata()
            resetfields()
            Button5.Enabled = False
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        resetfields()
    End Sub



    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox8.KeyPress, ComboBox2.KeyPress, ComboBox1.KeyPress
        'Validation

        Dim d As New DAO
        If sender.name = TextBox5.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = TextBox6.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = TextBox2.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = TextBox3.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = TextBox4.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = TextBox7.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = TextBox8.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If

        'Enter Press Tab

        If e.KeyChar.GetHashCode = 851981 Then
            If sender.name = ComboBox2.Name Then
                ComboBox1.Focus()
            ElseIf sender.name = ComboBox1.Name Then
                TextBox5.Focus()
            ElseIf sender.name = TextBox5.Name Then
                TextBox2.Focus()
            ElseIf sender.name = TextBox2.Name Then
                TextBox3.Focus()
            ElseIf sender.name = TextBox3.Name Then
                TextBox8.Focus()
            ElseIf sender.name = TextBox8.Name Then
                TextBox4.Focus()
            ElseIf sender.name = TextBox4.Name Then
                TextBox6.Focus()
            ElseIf sender.name = TextBox6.Name Then
                TextBox7.Focus()
            ElseIf sender.name = TextBox7.Name Then
                Button4_Click(sender, e)
            End If
        End If


    End Sub

    Private Sub DataGridView3_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Product where Product_Name = '" & DataGridView1.Item("P_Name", DataGridView1.CurrentCell.RowIndex).Value & "'")
        DataGridView2.DataSource = ds.Tables(0)

    End Sub


End Class