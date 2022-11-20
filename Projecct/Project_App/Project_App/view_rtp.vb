Imports CrystalDecisions.CrystalReports.Engine


Public Class View_rpt
    Private Sub View_Purchase_rpt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If r_type = "sales_summary" Then
            DataGridView1.DataSource = dk.Tables(0)
            Dim d As New Sale_Summary_rpt
            d.Load()
            Dim orpt As New ReportDocument
            orpt.Load(d.FileName)
            orpt.SetDataSource(dk.Tables(0))
            CrystalReportViewer1.ReportSource = orpt
            d.Dispose()
        ElseIf r_type = "Purchase_Summary" Then

            Dim d As New purchase_summary_rpt
            d.Load()
            Dim orpt As New ReportDocument
            orpt.Load(d.FileName)
            orpt.SetDataSource(dk.Tables(0))
            CrystalReportViewer1.ReportSource = orpt
            d.Dispose()

        End If
    End Sub
End Class