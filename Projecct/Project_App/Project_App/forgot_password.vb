Imports System.Net.Mail
Imports System.Net
'Imports System.Net.Mail
Public Class forgot_password

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox3.Text <> "" Then

            If TextBox4.Text <> "" Then
                If TextBox3.Text = TextBox4.Text Then
                    Dim d As New DAO
                    d.modifyData("Update admin set password = '" & TextBox3.Text & "' where email = '" & d1 & "'")
                    MessageBox.Show("Password Changed")
                Else
                    MsgBox("Invalid OTP")
                End If
            Else
                MsgBox("Enter Confirmation Password")
            End If
        Else
            MsgBox("Enter New Password")
        End If
    End Sub

    Dim val As String = ""
    Dim d1 As String = Login_Page.s
    Private Sub forgot_password_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "OTP Is sended to Email " + Login_Page.s

        Try
            Dim rnd As New Random
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("het.solanki721@gmail.com", "weeovkjyoazziimk")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"
            Smtp_Server.DeliveryMethod = SmtpDeliveryMethod.Network

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("het.solanki721@gmail.com")
            e_mail.To.Add(Login_Page.s)
            e_mail.Subject = "Email Sending"
            e_mail.IsBodyHtml = False
            Val = Convert.ToString(rnd.Next(90000, 99999))
            e_mail.Body = "OTP : " + val
            Smtp_Server.Send(e_mail)
            MsgBox("Mail Sent")

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text = val Then
            Panel2.Visible = True
            Panel1.Visible = False
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        Dim d As New DAO
        If sender.name = TextBox2.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
    End Sub
End Class