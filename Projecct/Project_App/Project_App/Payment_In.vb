Public Class Payment_In

    'Incomplete Code
    'Don't Touch it

    Private Sub Payment_In_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("select C_Name,In_Date,In_No,Payment_Mode, (In_Total+GST_Total) as 'Amount', Round_Off, Discount, L_SLIP, Balance_Due, 'Payment-In' as 'Type' from sales_summary")
        DataGridView1.DataSource = ds.Tables(0)
        dk = ds
        count_total()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
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

    Private Sub count_total()
        Dim d As Integer = DataGridView1.Rows.Count - 1
        Dim tt As Double = 0

        While d >= 0

            tt += Val(DataGridView1.Item("Balance_Due", d).Value)

            d -= 1
        End While

        TextBox1.Text = tt

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class