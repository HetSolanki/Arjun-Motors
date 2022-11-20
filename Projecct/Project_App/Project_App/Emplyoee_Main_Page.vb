Imports System.ComponentModel
Public Class Emplyoee_Main_Page

    Private Sub Main_Page_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
            End
        End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs)
        closeall()
        Dim productd As New Product_Master
        productd.MdiParent = Me
        productd.Show()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        closeall()
        mainpage.Visible = False
        settings.Visible = True
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        closeall()
        settings.Visible = False
        mainpage.Visible = True
        '   d.Hide()
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs)
        closeall()
        Dim d As New User_Master
        d.MdiParent = Me
        d.Show()
    End Sub
    Private Sub closeall()
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub CustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerToolStripMenuItem.Click
        closeall()
        Dim customerd As New customer_master
        customerd.MdiParent = Me
        customerd.Show()
    End Sub


    Private Sub DealerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DealerToolStripMenuItem.Click
        closeall()
        Dim dealerd As New dealer_master
        dealerd.MdiParent = Me
        dealerd.Show()
    End Sub

    Private Sub ToolStripButton12_Click(sender As Object, e As EventArgs) Handles ToolStripButton12.Click
        Dim d As New DAO
        Dim obj As SqlClient.SqlDataReader
        Dim s As String = Date.Now
        obj = d.getData("Select U_type from Admin where Username = '" & Login_Page.TextBox1.Text & "'")

        Dim da As Integer = MessageBox.Show("Are you Sure to Logout", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If da = 6 Then
            If obj.Read Then
                d.modifyData("Insert into Activity_Log (Date, Name, Category, Event) values ('" & Date.Now & "','" & Login_Page.TextBox1.Text & "','" & obj.Item(0) & "','Logged_Out')")
            End If
            Application.Restart()
        End If
    End Sub

    Private Sub settings_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles settings.ItemClicked

    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        closeall()
        mainpage.Visible = False
        Reports.Visible = True
    End Sub

    Private Sub ToolStripButton14_Click(sender As Object, e As EventArgs) Handles ToolStripButton14.Click
        closeall()
        Dim d As New Tr_Sale
        d.MdiParent = Me
        d.Show()
    End Sub

    Private Sub ToolStripButton13_Click(sender As Object, e As EventArgs) Handles ToolStripButton13.Click
        Reports.Visible = False
        mainpage.Visible = True

    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        closeall()
        Dim d As New Shop
        d.MdiParent = Me
        d.Show()
    End Sub

    Private Sub ToolStripDropDownButton3_Click(sender As Object, e As EventArgs)
        closeall()
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        closeall()
    End Sub

    Private Sub ToolStripDropDownButton1_Click(sender As Object, e As EventArgs)
        closeall()
    End Sub

    Private Sub SaleInvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaleInvoiceToolStripMenuItem.Click
        closeall()
        Dim d As New sale_d
        d.MdiParent = Me
        d.Show()
    End Sub

    Private Sub PaymentInToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentInToolStripMenuItem.Click
        closeall()
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs)
        closeall()
    End Sub

    Private Sub ToolStripLabel1_Click(sender As Object, e As EventArgs)
        closeall()
        mainpage.Visible = False
        settings.Visible = True
        Dim d As New Shop
        d.MdiParent = Me
        d.Show()
    End Sub

    Private Sub Main_Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim d As New DAO
        Dim obj As SqlClient.SqlDataReader
        obj = d.getData("Select U_type from Admin where Username = '" & Login_Page.TextBox1.Text & "'")
        If obj.Read Then
            d.modifyData("Insert into Activity_Log (Date, Name, Category, Event) values ('" & Date.Now & "','" & Login_Page.TextBox1.Text & "','" & obj.Item(0) & "','Logged_In')")
            MsgBox("Welcome.....")
        End If


    End Sub

    Private Sub Main_Page_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim d As New DAO
        Dim obj As SqlClient.SqlDataReader
        Dim s As String = Date.Now
        obj = d.getData("Select U_type from Admin where Username = '" & Login_Page.TextBox1.Text & "'")

        Dim da As Integer = MessageBox.Show("Are you Sure to Close", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If da = 6 Then
            If obj.Read Then
                d.modifyData("Insert into Activity_Log (Date, Name, Category, Event) values ('" & Date.Now & "','" & Login_Page.TextBox1.Text & "','" & obj.Item(0) & "','Logged_Out')")
            End If
            MsgBox("Thank You For Visit Our Software....!")
            Dispose()
        Else
            e.Cancel = True
        End If
    End Sub



    Private Sub ToolStripButton23_Click_1(sender As Object, e As EventArgs)
        closeall()
        Dim d As New Activity_Log
        d.MdiParent = Me
        d.Show()
    End Sub

    Private Sub ToolStripButton17_Click(sender As Object, e As EventArgs) Handles ToolStripButton17.Click
            closeall()
            Dim d As New Tr_Purchase
        'd.MdiParent = Me
        d.Show()
        End Sub
    End Class