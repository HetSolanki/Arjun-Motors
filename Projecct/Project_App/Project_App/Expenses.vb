Public Class Expenses
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Add_expenses.Show()
    End Sub

    Public Sub loaddata()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select item_name, Sum(price) from expenses group by Item_Name")

        DataGridView1.DataSource = ds.Tables(0)
    End Sub

    Private Sub Expenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loaddata()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Expenses where Item_Name = '" & DataGridView1.Item("Item_Name", DataGridView1.CurrentCell.RowIndex).Value & "'")
        DataGridView2.DataSource = ds.Tables(0)
    End Sub
End Class