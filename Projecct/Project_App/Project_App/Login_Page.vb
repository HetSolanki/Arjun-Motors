Imports System.Data.SqlClient

Public Class Login_Page


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim d As New DAO
        Dim obj As SqlClient.SqlDataReader
        Dim flag As Boolean = False
        Dim forgot_val As Boolean = forgot_Validation()

        If forgot_val = True Then
            Label2.Enabled = True
        End If
        Dim u_type As String = ""
        obj = d.getData("Select * from Admin where username = '" & TextBox1.Text & "' and password = '" & TextBox2.Text & "'")
        While obj.Read
            flag = True
            u_type = obj.Item("u_type")
        End While

        d.close_conn()

        If flag Then

            uname = TextBox1.Text
            utype = u_type

            'If TextBox1.Text = "Admin" And TextBox2.Text = "Admin" Then
            If u_type = "Admin" Then
                Main_Page.Show()
                Me.Hide()
            Else
                Emplyoee_Main_Page.Show()
                Me.Hide()
            End If
        Else
            MsgBox("Enter Valid Username And Password")
            If forgot_val = True Then
                'TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox2.Focus()
            Else
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox1.Focus()
            End If



        End If
    End Sub

    Public s As String = ""
    Private Function forgot_Validation() As Boolean 'Finding as Email-Id of an user
        Dim obj As SqlDataReader
        Dim d As New DAO
        obj = d.getData("Select Email from Admin where username = '" & TextBox1.Text & "'")

        If obj.Read Then
            s = obj.Item(0)
            Return True
        End If

        Return False
    End Function
    Private Sub Login_Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim d As New DAO
        PictureBox2.Visible = False
        TextBox2.UseSystemPasswordChar = True
        Label2.Enabled = False
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown, TextBox2.KeyDown, Button1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If sender.name = TextBox1.Name Then
                TextBox2.Focus()
            ElseIf sender.name = TextBox2.Name Then
                Button1_Click(sender, e)
            End If

        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        PictureBox2.Visible = True
        TextBox2.UseSystemPasswordChar = False
        PictureBox3.Visible = False
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        PictureBox2.Visible = False
        TextBox2.UseSystemPasswordChar = True
        PictureBox3.Visible = True
    End Sub


    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        forgot_password.Show()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        Dim d As Integer = MessageBox.Show("Are you Sure to Close", "Close
", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If d = 6 Then
            Me.Close()
        End If
    End Sub
End Class