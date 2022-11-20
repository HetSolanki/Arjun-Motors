Public Class Add_expenses
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim d As Integer = MessageBox.Show("Are you Sure to Close", "Close
", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If d = 6 Then
            Me.Close()
        End If

    End Sub

    Private Sub Add_expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Dim d As New DAO
        d.enable_design_grid_RGB(DataGridView2)
        Expenses_NO_load()
        load_Expenses_data()
        DateTimePicker3.Value = Date.Now
        'resetfild()
    End Sub

    Private Sub load_Expenses_data()
        Try
            Dim d As New DAO
            Dim dd As New Data.DataSet
            dd = d.loaddata("SELECT e_date,item_name, price, mode_payment,description FROM expenses WHERE e_no = " & TextBox1.Text & "")
            If dd.Tables(0).Rows.Count > 0 Then
                TextBox4.Text = dd.Tables(0).Rows(0).Item("Item_name")
                TextBox2.Text = dd.Tables(0).Rows(0).Item("Description")
                TextBox10.Text = dd.Tables(0).Rows(0).Item("price")
                DateTimePicker3.Value = dd.Tables(0).Rows(0).Item("E_date")
                ComboBox2.Text = dd.Tables(0).Rows(0).Item("mode_payment")
                'TextBox22.Text = dd.Tables(0).Rows(0).Item("In_Total")
                MsgBox("hii")
            End If
            DataGridView2.DataSource = dd.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Dim c As Integer = DataGridView2.Rows.Count - 1
        Dim Total As Double = 0

        While c >= 0
            'MsgBox(SGST_Total)

            Total += DataGridView2.Item("Item_price", DataGridView2.CurrentCell.RowIndex).Value

            c -= 1
        End While

        TextBox11.Text = Total
    End Sub

    Private Sub Expenses_NO_load()
        resetfild()
        Try
            Dim d As New DAO
            Dim ds As New Data.DataSet
            Dim c_year As Integer = Now.Year
            ds = d.loaddata("select e_no from configure_master")
            If ds.Tables(0).Rows.Count > 0 Then
                TextBox1.Text = Set_zero(ds.Tables(0).Rows(0).Item(0))
            End If
            load_Expenses_data()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub resetfild()
        TextBox2.ResetText()
        TextBox4.ResetText()
        TextBox10.ResetText()
        ComboBox2.Text = ""
    End Sub

    Private Function Set_zero(v As String) As String
        Dim d As Integer = v.Length
        Dim s As String = ""
        For i = d To 3
            s &= "0"
        Next
        s &= v
        Return s
    End Function

    Dim flag As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If TextBox4.Text <> "" Then
                If TextBox10.Text <> "" Then
                    If ComboBox2.Text <> "" Then
                        If flag = 0 Then
                            Dim d As New DAO

                            d.modifyData("Insert into Expenses( Item_name, Price, Mode_payment, description, e_no,E_date) values ('" & TextBox4.Text & "'," & TextBox10.Text & ", '" & ComboBox2.Text & "', '" & TextBox2.Text & "', " & TextBox1.Text & ", '" & DateTimePicker3.Value.ToString() & "')")
                            load_Expenses_data()
                            MessageBox.Show("Item Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Expenses.loaddata()
                            'resetfild()
                            TextBox4.Focus()
                        Else
                            Dim d As New DAO
                            d.modifyData("Update expenses set e_catogery = item_name = '" & TextBox4.Text & "', price = '" & TextBox10.Text & "', mode_payment = '" & ComboBox2.Text & "', description = '" & TextBox2.Text & "' where id = " & TextBox3.Text & "")
                            MessageBox.Show("Record Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'resetfild()
                            flag = 1
                        End If
                    Else
                        MessageBox.Show("Please Select Mode Of Payment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ComboBox2.Focus()
                    End If
                Else
                    MessageBox.Show("Enter Rate Of Item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TextBox10.Focus()
                End If
            Else
                MessageBox.Show("Enter Name OF Item ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox4.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        TextBox4.Text = DataGridView2.Item("item_name", DataGridView2.CurrentCell.RowIndex).Value
        TextBox10.Text = DataGridView2.Item("Item_price", DataGridView2.CurrentCell.RowIndex).Value
        ComboBox2.Text = DataGridView2.Item("modeofpayment", DataGridView2.CurrentCell.RowIndex).Value
        TextBox2.Text = DataGridView2.Item("description", DataGridView2.CurrentCell.RowIndex).Value
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ComboBox2.Enabled = True
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox4.Enabled = True
        TextBox10.Enabled = True
        DateTimePicker3.Enabled = True
        TextBox11.Enabled = True
        Button1.Enabled = True
        Expenses_NO_load()
        Dim d As New DAO
        Dim flag As Integer = d.validate("select e_no from expenses where e_no=" & TextBox1.Text & "")
        MsgBox(flag)
        If flag = 1 Then
            'invoice is already created update invoice no and get new invoice no
            Dim invno As Integer = 0
            Dim obj As SqlClient.SqlDataReader
            obj = d.getData("select id from expenses")
            While obj.Read
                invno = obj.Item(0)
            End While
            d.close_conn()
            invno += 1
            d.modifyData("update configure_master set e_no = " & invno)
            Expenses_NO_load()
            TextBox4.Focus()
        Else
            'invoice is not created 
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim In_No As String = InputBox("Enter Invoice Number : ", "Search Invoice", "000")
        TextBox1.Text = In_No
        resetfild()
        load_Expenses_data()
        ComboBox2.Enabled = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox4.Enabled = False
        TextBox10.Enabled = False
        DateTimePicker3.Enabled = False
        TextBox11.Enabled = False
        Button1.Enabled = False


    End Sub

    Private Sub TextBox2_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress, TextBox2.KeyPress, TextBox10.KeyPress, ComboBox2.KeyPress


        'Validation

        Dim d As New DAO
        If sender.name = TextBox2.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = TextBox4.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = TextBox10.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If



        'Enter Press Tab

        If e.KeyChar.GetHashCode = 851981 Then
            If sender.name = TextBox4.Name Then
                TextBox10.Focus()
            ElseIf sender.name = TextBox10.Name Then
                ComboBox2.Focus()
            ElseIf sender.name = ComboBox2.Name Then
                TextBox2.Focus()
            ElseIf sender.name = textBox2.Name Then
                Button1_Click(sender, e)
            End If
        End If


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim d As Integer = MessageBox.Show("Are you Want to save", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        Dim obj As New DAO
        If d = 6 Then
            obj.modifyData("Update expenses set e_catogery = item_name = '" & TextBox4.Text & "', price = '" & TextBox10.Text & "', mode_payment = '" & ComboBox2.Text & "', description = '" & TextBox2.Text & "' where id = " & TextBox3.Text & "")
            MessageBox.Show("Expenses Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Hide()
    End Sub
End Class