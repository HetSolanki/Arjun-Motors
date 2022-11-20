Public Class Tr_Sale
    Public Sub Tr_sale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select distinct C_Name from Sales_Summary")
        ComboBox1.DisplayMember = "C_Name"
        ComboBox1.ValueMember = "Id"
        ComboBox1.DataSource = ds.Tables(0)
        ds = d.loaddata("select C_Name,In_Date,In_No,Payment_Mode, (In_Total+GST_Total) as 'Amount', In_Total, GST_Total, Round_Off,Discount,L_SLIP, Balance_Due from sales_summary")
        DataGridView1.DataSource = ds.Tables(0)
        dk = ds
        count_total()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress, DateTimePicker1.KeyPress, ComboBox2.KeyPress, ComboBox1.KeyPress

        If e.KeyChar.GetHashCode = 851981 Then
            If sender.name = ComboBox1.Name Then
                ComboBox2.Focus()
            ElseIf sender.name = ComboBox2.Name Then
                DateTimePicker1.Focus()
            ElseIf sender.name = DateTimePicker1.Name Then
                DateTimePicker2.Focus()
            ElseIf sender.name = DateTimePicker2.Name Then
                Button2.Focus()
            End If
        End If

        If sender.name = ComboBox1.Name Or sender.name = ComboBox2.Name Then
            AutoSearch(sender, e, True)
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Dim d As New DAO
        Dim ds As New Data.DataSet
        Try
            If ComboBox1.Text = "" And ComboBox2.Text = "" Then
                'All Transection Between Date
                ds = d.loaddata("select C_Name,In_Date,In_No,Payment_Mode,In_Total,GST_Total,Round_Off,Discount,L_SLIP from sales_summary where In_Date between '" & DateTimePicker1.Value.ToString("dd-mm-yyyy") & "' and '" & DateTimePicker2.Value.ToString("dd-mm-yyyy") & "' ")

            ElseIf ComboBox1.Text <> "" And ComboBox2.Text = "" Then
                'All Customer Transection Between Date
                ds = d.loaddata("select C_Name,In_Date,In_No,Payment_Mode,In_Total,GST_Total,Round_Off,Discount,L_SLIP from sales_summary where In_Date between '" & DateTimePicker1.Value.ToString("dd-mm-yyyy") & "' and '" & DateTimePicker2.Value.ToString("dd-mm-yyyy") & "' and C_name = '" & ComboBox1.Text & "' ")

            ElseIf ComboBox1.Text = "" And ComboBox2.Text <> "" Then
                'All Payment Mode Transection Between Date
                ds = d.loaddata("select C_Name,In_Date,In_No,Payment_Mode,In_Total,GST_Total,Round_Off,Discount,L_SLIP from sales_summary where In_Date between '" & DateTimePicker1.Value.ToString("dd-mm-yyyy") & "' and '" & DateTimePicker2.Value.ToString("dd-mm-yyyy") & "' and  Payment_Mode= '" & ComboBox2.Text & "' ")

            Else
                'All Transection Customer With Payment Mode

                ds = d.loaddata("select C_Name,In_Date,In_No,Payment_Mode,In_Total,GST_Total,Round_Off,Discount,L_SLIP from sales_summary where In_Date between '" & DateTimePicker1.Value.ToString("dd-mm-yyyy") & "' and '" & DateTimePicker2.Value.ToString("dd-mm-yyyy") & "' and  Payment_Mode= '" & ComboBox2.Text & "' and C_name = '" & ComboBox1.Text & "'")

            End If

            ds = d.loaddata("Select sales_Summary.*, sale_details.* from sales_Summary, sale_Details where sales_summary.in_no = sale_details.in_no")
            DataGridView1.DataSource = ds.Tables(0)
            dk = ds
            count_total()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub count_total()
        Dim d As Integer = DataGridView1.Rows.Count - 1
        Dim gtot As Double = 0
        Dim tt As Double = 0

        While d >= 0

            gtot += Val(DataGridView1.Item("gst_total", d).Value)
            tt += Val(DataGridView1.Item("in_total", d).Value)

            d -= 1
        End While

        TextBox4.Text = tt
        TextBox1.Text = gtot

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If DataGridView1.Rows.Count > 0 Then
            invno = DataGridView1.Item("in_no", DataGridView1.CurrentCell.RowIndex).Value
            If invno <> "" Then
                Dim d As New sale
                d.MdiParent = Me.MdiParent
                d.Show()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.Rows.Count > 0 Then
            r_type = "sales_summary"
            Dim d As New view_rpt
            d.MdiParent = Me.MdiParent
            d.Show()
        End If
    End Sub

    Private Sub ViewEditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewEditToolStripMenuItem.Click
        If DataGridView1.Rows.Count > 0 Then
            invno = DataGridView1.Item("in_no", DataGridView1.CurrentCell.RowIndex).Value
            If invno <> "" Then
                Dim d As New sale
                d.MdiParent = Me.MdiParent
                d.Show()
            End If
        End If
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        'Invoice Print
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Panel2.Visible = True
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select distinct C_Name from Sales_Summary")
        ComboBox3.DisplayMember = "C_Name"
        ComboBox3.ValueMember = "Id"
        ComboBox3.DataSource = ds.Tables(0)
        TextBox2.Text = DataGridView1.Item("In_No", DataGridView1.CurrentCell.RowIndex).Value
        TextBox5.Text = DataGridView1.Item("Balance_Due", DataGridView1.CurrentCell.RowIndex).Value
        TextBox6.Text = DataGridView1.Item("Balance_Due", DataGridView1.CurrentCell.RowIndex).Value
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Panel2.Visible = False
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        If Val(TextBox6.Text) <= Val(TextBox5.Text) Then
            TextBox7.Text = Val(TextBox5.Text) - Val(TextBox6.Text)
        Else
            TextBox6.Text = 0
            MessageBox.Show("Enter Valid Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox4.Text <> "" Then
            Dim d As New DAO
            d.modifyData("Update Sales_Summary set Balance_Due = " & TextBox7.Text & " where in_no = '" & TextBox2.Text & "'")
            If TextBox3.Text <> "" Then
                d.modifyData("Insert into Payment_In (Customer_Name, In_Date, In_No, Payment_Mode, Amount, Balance_Due, Type, Description) values ('" & ComboBox3.Text & "', '" & DateTimePicker2.Value & "', '" & TextBox2.Text & "','" & ComboBox4.Text & "','" & TextBox5.Text & "', '" & TextBox7.Text & "', Payment-In, '" & TextBox3.Text & "')")
            Else
                d.modifyData("Insert into Payment_In (Customer_Name, In_Date, In_No, Payment_Mode, Amount, Balance_Due, Type) values ('" & ComboBox3.Text & "', '" & DateTimePicker2.Value.ToString("dd-MM-yyyy") & "', '" & TextBox2.Text & "','" & ComboBox4.Text & "','" & DataGridView1.Item("Amount", DataGridView1.CurrentCell.RowIndex).Value & "', '" & TextBox7.Text & "', 'Payment-In')")

            End If
            MsgBox("Record Inserted..!")
            Tr_sale_Load(sender, e)
        Else
            MessageBox.Show("Select Valid Payment-Mode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox4.Focus()
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView1.Item("Balance_Due", DataGridView1.CurrentCell.RowIndex).Value = 0 Then
            DataGridView1.CurrentRow.ContextMenuStrip = ContextMenuStrip2
        End If
    End Sub
End Class