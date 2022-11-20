Public Class Add_Product
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Dim d As Integer = MessageBox.Show("Are you Sure to Close", "Close
", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If d = 6 Then
            Me.Close()
            product.Focus()
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Add_Product_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MessageBox.Show("Hello World..!")
    End Sub
End Class