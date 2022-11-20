Public Class Tr_Purchase


    Private Sub tr_purchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.WindowState = FormWindowState.Maximized

        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select distinct D_Name from Purchase_Summary")
        ComboBox1.DisplayMember = "D_Name"
        ComboBox1.ValueMember = "Id"
        ComboBox1.DataSource = ds.Tables(0)

        ds = d.loaddata("select D_Name, bill_Date, bill_No, Payment_Mode, bill_Total, GST_Total, Round_Off, L_SLIP from Purchase_summary")
        DataGridView1.DataSource = ds.Tables(0)
        count_total()

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
                ds = d.loaddata("select D_Name, bill_Date, bill_No, Payment_Mode, bill_Total, GST_Total, Round_Off, L_SLIP from Purchase_summary where bill_Date between '" & DateTimePicker1.Value.ToString("dd-mm-yyyy") & "' and '" & DateTimePicker2.Value.ToString("yyyy-mm-dd") & "' ")

            ElseIf ComboBox1.Text <> "" And ComboBox2.Text = "" Then
                'All Customer Transection Between Date
                ds = d.loaddata("select D_Name, bill_Date, bill_No, Payment_Mode, bill_Total, GST_Total, Round_Off, L_SLIP from Purchase_summary where bill_Date between '" & DateTimePicker1.Value.ToString("dd-mm-yyyy") & "' and '" & DateTimePicker2.Value.ToString("yyyy-mm-dd") & "' and D_name = '" & ComboBox1.Text & "' ")

            ElseIf ComboBox1.Text = "" And ComboBox2.Text <> "" Then
                'All Payment Mode Transection Between Date
                ds = d.loaddata("select D_Name, bill_Date, bill_No, Payment_Mode, bill_Total, GST_Total, Round_Off, L_SLIP from Purchase_summary where bill_Date between '" & DateTimePicker1.Value.ToString("dd-mm-yyyy") & "' and '" & DateTimePicker2.Value.ToString("yyyy-mm-dd") & "' and Payment_Mode = '" & ComboBox2.Text & "' ")

            Else
                'All Transection Customer With Payment Mode

                ds = d.loaddata("select D_Name, bill_Date, bill_No, Payment_Mode, bill_Total, GST_Total, Round_Off, L_SLIP from Purchase_summary where bill_Date between '" & DateTimePicker1.Value.ToString("dd-mm-yyyy") & "' and '" & DateTimePicker2.Value.ToString("yyyy-mm-dd") & "' and Payment_Mode = '" & ComboBox2.Text & "' and D_name = '" & ComboBox1.Text & "'")

            End If

            DataGridView1.DataSource = ds.Tables(0)
            ds = d.loaddata("Select d_name, Purchase_Summary.bill_no, Purchase_details.bill_No, bill_date, payment_mode, P_cat, P_Product, P_HSN, P_Qty, P_Rate, P_Unit, P_CGST, P_SGST, bill_tot, Round_Off, GST_Total, bill_total, id, p_id, id1, d_address, Description, L_SLIP from Purchase_Summary, Purchase_Details where purchase_summary.bill_no = purchase_details.bill_no")

            'ds = d.loaddata("Select * from Purchase_Summary, Purchase_Details")
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

        TextBox4.Text = Math.Round(tt, 2)
        TextBox5.Text = Math.Round(gtot, 2)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.Rows.Count > 0 Then
            r_type = "Purchase_Summary"


            Dim d As New View_rpt
            d.MdiParent = Me.MdiParent
            d.Show()
        End If
    End Sub
End Class