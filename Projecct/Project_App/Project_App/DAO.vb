Imports System.Data.SqlClient
Imports System.DateTime
Public Class DAO
    Private con As SqlClient.SqlConnection

    Public Sub New()
        con = New SqlConnection("Data Source=103.212.121.67;User ID=data_stu4;Password=Lovecoding@6750")

        Try
            con.Open()
            con.Close()
        Catch ex As Exception
            MessageBox.Show("Please check database connectivity..!")
        End Try
    End Sub

    Public Function getData(ByVal str As String) As SqlDataReader
        Dim obj As SqlDataReader
        Dim cmd As New SqlCommand(str, con)
        close_conn()
        con.Open()
        obj = cmd.ExecuteReader

        Return obj
        close_conn()
    End Function

    Public Sub close_conn()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Public Function validate(ByVal str As String) As Integer
        Dim f As Integer = 0
        Dim obj As SqlDataReader
        con.Open()
        obj = getData(str)
        While obj.Read
            f = 1
        End While
        close_conn()
        Return f
    End Function

    Public Sub modifyData(ByVal str As String)
        close_conn()
        con.Open()
        Dim cmd As New SqlCommand(str, con)
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub

    Public Function loaddata(ByVal str As String) As DataSet
        Dim ds As New DataSet
        Dim da As New SqlClient.SqlDataAdapter(str, con)
        con.Open()
        da.SelectCommand.ExecuteReader()
        con.Close()
        da.Fill(ds)
        Return ds
    End Function

    Public Function number_valided(ByVal c As Char, ByVal hc As Integer) As Boolean
        Dim f As Boolean = True
        If (c <> "" And IsNumeric(c)) Or hc = 524296 Or hc = 3014702 Or hc = 851981 Then
            f = False
        Else
            MessageBox.Show("Please Enter Only Number.....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Return f
    End Function
    Public Function ISALPHA_valided(ByVal c As Char, ByVal hc As Integer) As Boolean
        Dim f As Boolean = True
        If (UCase(c) >= "A" And UCase(c) <= "Z") Or IsNumeric(c) Or hc = 524296 Or hc = 3014702 Or c = " " Or hc = 851981 Then
            f = False
        Else
            MessageBox.Show("Please Enter Only Alphabets.....!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        Return f
    End Function


    Public Sub enable_design_grid_RGB(datagridview1 As DataGridView)

        datagridview1.AllowDrop = False
        datagridview1.AllowUserToAddRows = False
        datagridview1.AllowUserToDeleteRows = False
        datagridview1.AllowUserToOrderColumns = False
        datagridview1.AllowUserToResizeColumns = False
        datagridview1.AllowUserToResizeRows = False
        datagridview1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        datagridview1.BackColor = Color.FromArgb(127, 181, 255)
        datagridview1.BorderStyle = BorderStyle.FixedSingle
        datagridview1.CellBorderStyle = DataGridViewCellBorderStyle.Sunken
        datagridview1.ColumnHeadersBorderStyle = DataGridViewCellBorderStyle.Sunken

        datagridview1.DefaultCellStyle.Font = New Font("calibri", 14)
        datagridview1.DefaultCellStyle.BackColor = Color.FromArgb(196, 221, 255)
        datagridview1.EnableHeadersVisualStyles = False
        datagridview1.BorderStyle = BorderStyle.None
        datagridview1.ColumnHeadersDefaultCellStyle.Font = New Font("calibri", 16)
        datagridview1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(19, 59, 92)
        datagridview1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        datagridview1.AllowUserToAddRows = False
        datagridview1.AllowUserToDeleteRows = False
        datagridview1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub


End Class
