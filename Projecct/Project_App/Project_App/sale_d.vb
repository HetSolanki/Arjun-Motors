Public Class sale_d
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sale.Show()
    End Sub

    Private Sub sale_d_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select distinct C_Name from Sales_Summary")
        ComboBox1.DisplayMember = "C_Name"
        ComboBox1.ValueMember = "Id"
        ComboBox1.DataSource = ds.Tables(0)

        ds = d.loaddata("select C_Name,In_Date,In_No,Payment_Mode,In_Total,GST_Total,Round_Off,Discount,L_SLIP from Sales_Summary")
        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ''RETRIVING CURRENT WEEK'S START AND END DATE
        'Dim startOfWeek As Date = DateTime.Today.AddDays(-1 * Now.DayOfWeek)
        'Dim endOfWeek As Date = DateTime.Today.AddDays(7 - Now.DayOfWeek)


    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress, DateTimePicker1.KeyPress, ComboBox2.KeyPress, ComboBox1.KeyPress
        If e.KeyChar.GetHashCode = 852582 Then
            If sender.name = ComboBox1.Name Then
                ComboBox2.Focus()
            ElseIf sender.name = ComboBox2.Name Then
                DateTimePicker1.Focus()
            ElseIf sender.name = DateTimePicker1.Name Then
                DateTimePicker2.Focus()
            End If
        End If

        If sender.name = ComboBox1.Name Or sender.name = ComboBox2.Name Then
            AutoSearch(sender, e, True)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim d As New DAO
        Dim ds As New DataSet
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

            If ds.Tables(0).Rows.Count > 0 Then
                DataGridView1.DataSource = ds.Tables(0)
                ds = d.loaddata("Select Sales_Summary.*, sale_Details.* from Sales_Summary, sale_details where Sales_Summary.in_no = Sale_details.in_No")
                dk = ds

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub
End Class