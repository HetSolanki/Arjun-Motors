Imports System.Data.SqlClient
Public Class customer_master
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Panel4.Visible = True
    End Sub

    Private Sub product_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Panel4.Visible = False
        Panel1.Visible = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel5.Visible = True
        TextBox5.Focus()
    End Sub
    Private Sub load_vehicle()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Vehicle where id = '" & TextBox8.Text & "'")
        DataGridView4.DataSource = ds.Tables(0)
    End Sub
    Private Sub loaddata()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Customer")
        Dim ds2 As DataSet = d.loaddata("Select * from Vehicle")
        DataGridView3.DataSource = ds.Tables(0)
        DataGridView4.DataSource = ds2.Tables(0)
    End Sub

    Private Sub Load_Customer()
        Try
            Dim d As New DAO
            Dim ds As DataSet = d.loaddata("Select Customer_Name From Customer")
            ComboBox1.DisplayMember = "Customer_Name"
            ComboBox1.ValueMember = "id"
            ComboBox1.DataSource = ds.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub customer_master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Panel7.Visible = False
        Dim d As New DAO
        d.enable_design_grid_RGB(DataGridView2)
        loaddata()
        Load_Customer()
        loadValue()
        'Customer()
        Load_Customer_Data()
    End Sub


    Public Sub Load_Customer_Data()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select C_Name, sum(In_Total + GST_Total + Round_Off) as 'Amount' from Sales_Summary group by C_Name")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim d As Integer = MessageBox.Show("Are you Sure to Close", "Close
", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If d = 6 Then
            Panel5.Visible = False
        End If
    End Sub

    Dim flag As Integer = 0

    Private Sub resetfields()
        TextBox2.Text = ""
        TextBox3.Text = ""
        MaskedTextBox1.Text = ""
        TextBox5.Text = ""
        TextBox5.Focus()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox5.Text <> "" Then
            If TextBox2.Text <> "" Then
                If TextBox3.Text <> "" Then
                    If MaskedTextBox1.Text <> "" Then
                        If TextBox2.Text.Length >= 10 Then
                            Dim d As New DAO
                            Dim flag As Integer = d.validate("Select * from Customer where Customer_Name = '" & TextBox5.Text & "'")

                            If flag = 1 Then
                                MessageBox.Show("Customer Already Exits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                TextBox5.Focus()
                            Else
                                d.modifyData("Insert into Customer(Customer_Name, Address, Mobile_No, Adharcard_No) values('" & TextBox5.Text & "','" & TextBox3.Text & "','" & TextBox2.Text & "', '" & MaskedTextBox1.Text & "')")
                                'd.modifyData("Insert into Customer_Details (C_Name, Total) values ('" & TextBox5.Text & "', " & 0 & ")")
                                MsgBox("Customer Added")
                                Load_Customer_Data()
                                loaddata()
                                resetfields()
                                TextBox5.Focus()
                            End If

                        Else
                            MessageBox.Show("Enter 10 Digit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            TextBox2.Focus()
                        End If
                    Else
                        MessageBox.Show("Enter Valid AdharCard Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        MaskedTextBox1.Focus()
                    End If
                Else
                    MessageBox.Show("Enter Valid Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TextBox3.Focus()
                End If
            Else
                MessageBox.Show("Enter Valid Mobile Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox2.Focus()
            End If
        Else
            MessageBox.Show("Enter Valid Customer Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox5.Focus()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim d As New DAO

        d.modifyData("update Customer set Customer_Name='" & TextBox5.Text & "', Address='" & TextBox3.Text & "', Mobile_No='" & TextBox2.Text & "', Adharcard_No='" & MaskedTextBox1.Text & "' where id = " & TextBox7.Text & "")

        MsgBox("Customer Updated")
        loaddata()
        resetfields()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Customer where Customer_Name = '" & DataGridView1.Item("C_Name", DataGridView1.CurrentCell.RowIndex).Value & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            Label12.Text = UCase(ds.Tables(0).Rows(0).Item("Customer_Name"))
            Label13.Text = UCase(ds.Tables(0).Rows(0).Item("Mobile_No"))
            Label15.Text = UCase(ds.Tables(0).Rows(0).Item("Address"))
            Label16.Text = UCase(ds.Tables(0).Rows(0).Item("Adharcard_No"))
        End If

        ds = d.loaddata("Select in_no,in_date,payment_mode,in_total, gst_total from Sales_Summary where C_Name = '" & DataGridView1.Item("C_Name", DataGridView1.CurrentCell.RowIndex).Value & "'")
        DataGridView2.DataSource = ds.Tables(0)
    End Sub

    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If DataGridView3.Rows.Count > 0 Then
            TextBox7.Text = DataGridView3.Item("Index", DataGridView3.CurrentCell.RowIndex).Value
            TextBox5.Text = DataGridView3.Item("Cts_Name", DataGridView3.CurrentCell.RowIndex).Value
            TextBox2.Text = DataGridView3.Item("Mobile_Number", DataGridView3.CurrentCell.RowIndex).Value
            TextBox3.Text = DataGridView3.Item("Address_Val", DataGridView3.CurrentCell.RowIndex).Value
            MaskedTextBox1.Text = DataGridView3.Item("Adharcard", DataGridView3.CurrentCell.RowIndex).Value
            Button5.Enabled = True
        End If
    End Sub

    Private Sub resetfield2()
        TextBox10.Text = ""
        TextBox8.Text = ""
        MaskedTextBox3.Text = ""
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim c As Integer = MessageBox.Show("Do You Want to Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If c = 6 Then
            Dim d As New DAO
            d.modifyData("delete from Customer where id = '" & TextBox7.Text & "'")
            loaddata()
            resetfields()
            Button5.Enabled = False
        End If
    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Load_Customer()
        loadValue()
        Panel7.Visible = True
        Panel7.BackColor = Color.FromArgb(33, 41, 52)
        'load_vehicle()
        'loaddata()
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim c As Integer = MessageBox.Show("Do You Want to Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If c = 6 Then
            Dim d As New DAO
            d.modifyData("delete from Vehicle where id = '" & TextBox8.Text & "'")
            MessageBox.Show("Record Deleted", "Infromation", MessageBoxButtons.OK, MessageBoxIcon.Information)
            loaddata()
            loadValue()
            resetfields()
            Button7.Enabled = False
        End If
    End Sub

    Private Sub DataGridView4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellClick
        If DataGridView1.Rows.Count > 0 Then
            TextBox8.Text = DataGridView4.Item("Id1", DataGridView4.CurrentCell.RowIndex).Value
            MaskedTextBox3.Text = DataGridView4.Item("Vehicle_Number2", DataGridView4.CurrentCell.RowIndex).Value
            TextBox10.Text = DataGridView4.Item("Vehicle_Name", DataGridView4.CurrentCell.RowIndex).Value
            Button7.Enabled = True
        End If
    End Sub

    'Dim flag As Integer = 0
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If MaskedTextBox3.Text <> "" Then
            If TextBox10.Text <> "" Then
                If flag = 0 Then
                    Dim d As New DAO
                    Dim f As Integer = d.validate("Select id from Vehicle where id = '" & TextBox8.Text & "'")
                    If f = 0 Then
                        d.modifyData("Insert into Vehicle (Vehicle_Number, Vehicle_Name, Customer_Name) values ('" & MaskedTextBox3.Text & "','" & TextBox10.Text & "','" & ComboBox1.Text & "')")
                        MaskedTextBox3.Focus()
                        MessageBox.Show("Vehicle Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'loaddata()
                        loadValue()
                    Else
                        d.modifyData("Update Vehicle set Vehicle_Name = '" & TextBox10.Text & "', Vehicle_Number = '" & MaskedTextBox3.Text & "', Customer_Name = '" & ComboBox1.Text & "' where id = '" & TextBox8.Text & "'")
                        MessageBox.Show("Record Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'loaddata()
                        loadValue()
                        resetfield2()
                        flag = 1
                    End If
                Else
                    Dim d As New DAO
                    d.modifyData("Update Vehicle set Vehicle_Name = '" & TextBox10.Text & "', Vehicle_Number = '" & MaskedTextBox3.Text & "', Customer_Name = '" & ComboBox1.Text & "' where id = '" & TextBox8.Text & "'")
                    MessageBox.Show("Record Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    resetfield2()
                    'loaddata()
                    loadValue()
                    flag = 1
                End If
            Else
                MessageBox.Show("Enter Valid Vehicle Number")
            End If
        Else
            MessageBox.Show("Enter Valid Vehicle Number")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        resetfield2()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim d As Integer = MessageBox.Show("Are you Sure to Close", "Close
", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If d = 6 Then
            Panel7.Visible = False
            Panel7.BackColor = Color.White

        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub loadValue()
        Dim ds As DataSet
        Dim d As New DAO
        Dim validate As Integer
        validate = d.validate("Select * from Vehicle Where Customer_Name = '" & ComboBox1.Text & "'")

        If validate = 1 Then
            ds = d.loaddata("Select * from Vehicle Where Customer_Name = '" & ComboBox1.Text & "'")
            DataGridView4.DataSource = ds.Tables(0)
        Else
            'DataGridView4.DataSource = Nothing
            'DataGridView4.Rows.Clear()
            'DataGridView4.Refresh()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            Dim d As New DAO
            Dim obj As SqlDataReader
            obj = d.getData("Select * from Vehicle where Customer_Name = '" & ComboBox1.Text & "'")
            If obj.Read() Then
                MaskedTextBox3.Text = obj.Item("vehicle_Number")
                TextBox10.Text = obj.Item("Vehicle_Name")
                TextBox8.Text = obj.Item("id")
                loadValue()
            Else
                Dim dt = TryCast(DataGridView4.DataSource, DataTable)
                dt.Rows.Clear()
                DataGridView4.DataSource = dt
                resetfield2()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub TextBox5_KeyPress_2(sender As Object, e As KeyPressEventArgs) Handles TextBox10.KeyPress, TextBox5.KeyPress, TextBox3.KeyPress, TextBox2.KeyPress, ComboBox1.KeyPress, MaskedTextBox1.KeyPress
        If e.KeyChar.GetHashCode = 851981 Then
            If sender.name = TextBox5.Name Then
                TextBox2.Focus()
            ElseIf sender.name = TextBox2.Name Then
                TextBox3.Focus()
            ElseIf sender.name = TextBox3.Name Then
                MaskedTextBox1.Focus()
            ElseIf sender.name = MaskedTextBox1.Name Then
                Button4_Click(sender, e)
            End If
        End If


        Dim d As New DAO
        If sender.name = TextBox2.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = MaskedTextBox1.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If

        If sender.name = TextBox3.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If

        If sender.name = TextBox5.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If


        If sender.name = ComboBox1.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If

        If sender.name = TextBox10.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
    End Sub


    Private Sub DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        If DataGridView2.Rows.Count > 0 Then
            invno = DataGridView2.Item("in_no1", DataGridView2.CurrentCell.RowIndex).Value
            If invno <> "" Then
                Dim d As New sale
                d.Show()
            End If
        End If

    End Sub



    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.Hide()
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class