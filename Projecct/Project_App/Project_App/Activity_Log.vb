Public Class Activity_Log
    Private Sub Activity_Log_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Activity_Log order by id desc")
        DataGridView1.DataSource = ds.Tables(0)

        Me.WindowState = FormWindowState.Maximized
    End Sub

End Class