Public Class dealer_master

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Panel4.Visible = True
    End Sub

    Private Sub product_Click(sender As Object, e As EventArgs) Handles Me.Click
        Panel4.Visible = False
        Panel1.Visible = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel5.Visible = True

    End Sub



    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim d As Integer = MessageBox.Show("Are you Sure to Close", "Close
", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If d = 6 Then
            Panel5.Visible = False
        End If
    End Sub

    Private Sub resetfields()
        TextBox6.Text = ""
        TextBox5.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Focus()
    End Sub

    Dim flag As Integer = 0
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox5.Text <> "" Then
            If TextBox2.Text <> "" Then
                If TextBox3.Text <> "" Then
                    If TextBox4.Text <> "" Then
                        If flag = 0 Then 'INSERT
                            Dim dao As New DAO
                            Dim f As Integer = 0
                            f = dao.validate("Select id from Dealer where id = '" & TextBox6.Text & "'")
                            If f = 0 Then
                                Dim d As New DAO
                                d.modifyData("Insert into Dealer (Dealer_Name, Mobile_No, Address, GST_Number) values ('" & TextBox5.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')")
                                loaddata()
                                MessageBox.Show("Dealer Added")
                                flag = 1
                            Else
                                Dim d As New DAO
                                d.modifyData("Update Dealer Set Dealer_Name = '" & TextBox5.Text & "', GST_Number = '" & TextBox4.Text & "', Mobile_No = " & TextBox2.Text & ", Address = '" & TextBox3.Text & "' where id = '" & TextBox6.Text & "'")
                                loaddata()
                                resetfields()
                                MessageBox.Show("Record Updated")
                                flag = 1
                            End If
                        Else
                            'Update

                            Dim d As New DAO
                            d.modifyData("Update Dealer Set Dealer_Name = '" & TextBox5.Text & "', GST_Number = '" & TextBox4.Text & "', Mobile_No = " & TextBox2.Text & ", Address = '" & TextBox3.Text & "'  where id = '" & TextBox6.Text & "'")
                            loaddata()
                            resetfields()
                            MessageBox.Show("Record Updated")
                            flag = 1
                        End If
                    Else
                        MessageBox.Show("Enter Valid GST Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Else
                    MessageBox.Show("Enter Valid Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Enter Valid Mobile Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Enter Valid Dealer Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub loaddata()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Dealer")
        DataGridView3.DataSource = ds.Tables(0)
    End Sub
    Private Sub dealer_master_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Panel5.Visible = False
        loaddata()
        Load_Dealer_Data()
    End Sub

    Public Sub Load_Dealer_Data()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select distinct D_Name from Purchase_Summary")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub


    'Private Sub Dealer()
    '    Dim d As New DAO
    '    Dim ds2 As DataSet = d.loaddata("Select distinct C_Name from Sales_Summary")
    '    Dim i As Integer = 0
    '    Dim total As Double
    '    While i < ds2.Tables(0).Rows.Count
    '        MsgBox(ds2.Tables(0).Rows(i).Item("C_Name"))
    '        Dim ds1 As DataSet = d.loaddata("Select SUM(In_Total + GST_Total + Round_Off) as 'In_Total' from Sales_Summary where C_Name = '" & ds2.Tables(0).Rows(i).Item("C_Name") & "'")
    '        total = ds1.Tables(0).Rows(0).Item("In_Total")
    '        d.modifyData("Update Customer_Details set Total = " & total & " where C_Name = '" & ds2.Tables(0).Rows(i).Item("C_Name") & "'")
    '        i += 1
    '    End While
    'End Sub


    Private Sub DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If DataGridView3.Rows.Count > 0 Then
            TextBox6.Text = DataGridView3.Item("Id", DataGridView3.CurrentCell.RowIndex).Value
            TextBox5.Text = DataGridView3.Item("Dealer_Name", DataGridView3.CurrentCell.RowIndex).Value
            TextBox2.Text = DataGridView3.Item("Mobile_No", DataGridView3.CurrentCell.RowIndex).Value
            TextBox3.Text = DataGridView3.Item("Address", DataGridView3.CurrentCell.RowIndex).Value
            TextBox4.Text = DataGridView3.Item("GST_Number", DataGridView3.CurrentCell.RowIndex).Value
            Button3.Enabled = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim c As Integer = MessageBox.Show("Do You Want to Delete", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If c = 6 Then
            Dim d As New DAO
            d.modifyData("delete from dealer where id = '" & TextBox6.Text & "'")
            loaddata()
            resetfields()
            Button3.Enabled = False
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Dealer where Dealer_Name = '" & DataGridView1.Item("D_Name", DataGridView1.CurrentCell.RowIndex).Value & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            'Label12.Text = UCase(ds.Tables(0).Rows(0).Item("Dealer_Name"))
            'Label13.Text = UCase(ds.Tables(0).Rows(0).Item("Mobile_No"))
            'Label15.Text = UCase(ds.Tables(0).Rows(0).Item("Address"))
            'Label16.Text = UCase(ds.Tables(0).Rows(0).Item("GST_Number"))
        End If

        ds = d.loaddata("Select * from Purchase_Summary where D_Name = '" & DataGridView1.Item("D_Name", DataGridView1.CurrentCell.RowIndex).Value & "'")
        DataGridView2.DataSource = ds.Tables(0)
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        resetfields()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress, TextBox3.KeyPress
        Dim d As New DAO
        If sender.name = TextBox3.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
        If sender.name = TextBox5.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim d As New DAO

        If sender.name = TextBox5.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
    End Sub
    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress, TextBox4.KeyPress, TextBox3.KeyPress, TextBox2.KeyPress

        'Enter_Button Code

        If e.KeyChar.GetHashCode = 851981 Then
            If sender.name = TextBox5.Name Then
                TextBox2.Focus()
            ElseIf sender.name = TextBox2.Name Then
                TextBox3.Focus()
            ElseIf sender.name = TextBox3.Name Then
                TextBox4.Focus()
            ElseIf sender.name = TextBox4.Name Then
                Button4_Click(sender, e)
            End If
        End If

        'Validation

        Dim d As New DAO
        If sender.name = TextBox5.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If

        If sender.name = TextBox4.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If

        If sender.name = TextBox3.Name Then
            e.Handled = d.ISALPHA_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If

        If sender.name = TextBox2.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class