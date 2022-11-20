Imports System.Data.SqlClient

Public Class Purchase
    Private Sub Purchase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        load_Dealer()
        Load_Category()
        If invno <> "" Then
            TextBox1.Text = invno
            invno = ""
            load_invoice_data()
        End If
        DateTimePicker3.Value = Date.Now
        TextBox14.Text = NumberToText(Val(TextBox22.Text))
    End Sub

    Private Function load_Dealer()
        Try
            Dim d As New DAO
            Dim ds As New Data.DataSet
            ds = d.loaddata("select distinct Dealer_Name from Dealer")
            ComboBox1.DisplayMember = "Dealer_Name"
            ComboBox1.ValueMember = "id"
            ComboBox1.DataSource = ds.Tables(0)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function



    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick

        If DataGridView2.Rows.Count > 0 Then
            Button1.Enabled = True
            TextBox7.Text = DataGridView2.Item("p_id", DataGridView2.CurrentCell.RowIndex).Value
            Dim d As New DAO
            Dim obj As SqlClient.SqlDataReader
            obj = d.getData("select Quantity from Product where id =" & TextBox7.Text)
            While obj.Read
                TextBox8.Text = obj.Item(0)
            End While
            d.close_conn()
            NumericUpDown1.Value = DataGridView2.Item("P_Qty", DataGridView2.CurrentCell.RowIndex).Value
            ComboBox2.Text = DataGridView2.Item("P_Category", DataGridView2.CurrentCell.RowIndex).Value
            ComboBox3.Text = DataGridView2.Item("P_Name", DataGridView2.CurrentCell.RowIndex).Value
        Else
            Button1.Enabled = False
        End If

    End Sub
    'Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
    '    TextBox13.Text = DataGridView2.Item("P_HSN", DataGridView2.CurrentCell.RowIndex).Value 'HSN
    '    ComboBox2.Text = DataGridView2.Item("P_Category", DataGridView2.CurrentCell.RowIndex).Value 'Category
    '    ComboBox3.Text = DataGridView2.Item("P_Name", DataGridView2.CurrentCell.RowIndex).Value 'Product
    '    NumericUpDown1.Value = DataGridView2.Item("P_Qty", DataGridView2.CurrentCell.RowIndex).Value 'Quantity
    '    TextBox9.Text = DataGridView2.Item("P_Unit", DataGridView2.CurrentCell.RowIndex).Value 'Unit
    '    TextBox10.Text = DataGridView2.Item("P_Rate", DataGridView2.CurrentCell.RowIndex).Value 'Rate
    '    TextBox3.Text = DataGridView2.Item("P_CGST", DataGridView2.CurrentCell.RowIndex).Value 'CGST
    '    TextBox2.Text = DataGridView2.Item("P_SGST", DataGridView2.CurrentCell.RowIndex).Value 'SGST
    '    TextBox11.Text = DataGridView2.Item("P_Total", DataGridView2.CurrentCell.RowIndex).Value 'Total


    '    If DataGridView2.Rows.Count > 0 Then
    '        Button1.Enabled = True
    '    Else
    '        Button1.Enabled = False
    '    End If
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim d As Integer = MessageBox.Show("Do you want to delete " & DataGridView2.Item("P_Name", DataGridView2.CurrentCell.RowIndex).Value & "?", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        If d = 6 Then
            Dim id As Integer = DataGridView2.Item("id", DataGridView2.CurrentCell.RowIndex).Value
            Dim dp As New DAO
            dp.modifyData("delete from Purchase_details where id = " & id)
            Update_Inventory(1)
            load_invoice_data()
            Update_Creaditor_Accounts(1)

            If DataGridView2.Rows.Count = 0 Then
                dp.modifyData("delete from Purchase_summary where bill_no ='" & TextBox1.Text & "'")
                'dp.modifyData("update Customer_Details set total = " & 0)

            Else
                'UPDATE TOTALS
                dp.modifyData("update Purchase_summary set bill_Total =" & TextBox20.Text & ", GST_Total =" & TextBox16.Text & ",Round_Off =" & TextBox19.Text & " where bill_no ='" & TextBox1.Text & "'")
            End If
            Dim d1 As New DAO
            'd1.modifyData("Update Customer_Details set Total = " & TextBox22.Text & " where C_Name = '" & ComboBox1.Text & "'")
            Button1.Enabled = False
        End If
    End Sub



    Dim count As Integer = 0

    Private Function Set_zero(v As String) As String
        Dim d As Integer = v.Length
        Dim s As String = ""
        For i = d To 3
            s &= "0"
        Next
        s &= v
        Return s
    End Function









    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    'invoice_load()
    '    Dim d As New DAO
    '    Dim flag As Integer = d.validate("select id from sales_summary where in_no='" & TextBox1.Text & "'")
    '    'MsgBox(flag)
    '    If flag = 1 Then
    '        'invoice is already created update invoice no and get new invoice no
    '        Dim invno As Integer = 0
    '        Dim obj As SqlClient.SqlDataReader
    '        obj = d.getData("select id from configure_master")
    '        While obj.Read
    '            invno = obj.Item(0)
    '        End While
    '        d.close_conn()
    '        invno += 1
    '        d.modifyData("update configure_master set id = " & invno)
    '        'invoice_load()
    '    Else
    '        'invoice is not created 
    '    End If
    'End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        Dim d As New DAO
        If sender.name = TextBox5.Name Then
            e.Handled = d.number_valided(e.KeyChar, e.KeyChar.GetHashCode)
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If sender.name = ComboBox1.Name Then
            AutoSearch(sender, e, False)
        End If
    End Sub

    'Private Sub sale_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
    '    Dim d As New DAO
    '    'd.modifyData("Insert into Invoice (invoice_no) values (" & count & ")")
    'End Sub

    Private Function Load_Category()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select distinct(Product_Category) from Product")
        ComboBox2.DisplayMember = "Product_Category"
        ComboBox2.ValueMember = "id"
        ComboBox2.DataSource = ds.Tables(0)
    End Function

    Private Function Load_Product()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select distinct Product_Name from Product where Product_Category = '" & ComboBox2.Text & "'")
        ComboBox3.DisplayMember = "Product_Name"
        ComboBox3.ValueMember = "id"
        ComboBox3.DataSource = ds.Tables(0)

    End Function
    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'Load_Vehicle()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select * from Dealer where Dealer_Name = '" & ComboBox1.Text & "'")

        If ds.Tables(0).Rows.Count > 0 Then
            TextBox5.Text = ds.Tables(0).Rows(0).Item("Mobile_No")
            TextBox4.Text = ds.Tables(0).Rows(0).Item("Address")
            TextBox24.Text = ds.Tables(0).Rows(0).Item("GST_Number")
        End If



    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Load_Product()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Load_Product_data()
    End Sub

    Private Sub Load_Product_data()
        Dim d As New DAO
        Dim ds As DataSet = d.loaddata("Select Quantity, Product_Unit, Sale_Price, S_GST, C_GST, HSN_Code, id from Product where Product_Name = '" & ComboBox3.Text & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            TextBox9.Text = ds.Tables(0).Rows(0).Item("Product_Unit")
            TextBox8.Text = ds.Tables(0).Rows(0).Item("Quantity")
            TextBox10.Text = ds.Tables(0).Rows(0).Item("Sale_Price")
            TextBox3.Text = ds.Tables(0).Rows(0).Item("C_GST")
            TextBox2.Text = ds.Tables(0).Rows(0).Item("S_GST")
            TextBox13.Text = ds.Tables(0).Rows(0).Item("HSN_Code")
            TextBox7.Text = ds.Tables(0).Rows(0).Item("id")
        End If
        Dim stock As Integer = Val(TextBox8.Text)
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged, NumericUpDown1.ValueChanged
        If sender.text <> "" And IsNumeric(sender.text) Then
            TextBox6.Text = NumericUpDown1.Value * Val(TextBox10.Text)
            Dim cgst_amt As Double = Math.Round((Val(TextBox6.Text) * Val(TextBox2.Text) / 100), 2)
            Dim sgst_amt As Double = Math.Round((Val(TextBox6.Text) * Val(TextBox3.Text) / 100), 2)
            TextBox12.Text = cgst_amt + sgst_amt
            TextBox11.Text = Val(TextBox12.Text) + Val(TextBox6.Text)
        Else
            sender.text = 0
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Dim d As Integer = MessageBox.Show("Are you Sure to Close", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If d = 6 Then
            Me.Close()
        End If
    End Sub

    Dim total As Integer = 0

    Dim flag As Integer = 0

    Private Sub load_invoice_data()
        Try
            'customer_master.Load_Customer_Data()
            Dim d As New DAO
            Dim dd As New Data.DataSet
            dd = d.loaddata("SELECT * FROM Purchase_SUMMARY WHERE bill_NO = '" & TextBox1.Text & "'")
            If dd.Tables(0).Rows.Count > 0 Then
                ComboBox1.Text = dd.Tables(0).Rows(0).Item("D_Name")
                DateTimePicker3.Value = dd.Tables(0).Rows(0).Item("bill_Date")
                ComboBox5.Text = dd.Tables(0).Rows(0).Item("Payment_Mode")
                'TextBox21.Text = dd.Tables(0).Rows(0).Item("Discount")
                TextBox19.Text = dd.Tables(0).Rows(0).Item("Round_Off")
                TextBox23.Text = dd.Tables(0).Rows(0).Item("L_SLIP")
            End If

            dd = d.loaddata("Select * from Purchase_Summary")
            sale_d.DataGridView1.DataSource = dd.Tables(0)


            Dim ds As New Data.DataSet
            ds = d.loaddata("select * from Purchase_details where bill_no='" & TextBox1.Text & "'")
            DataGridView2.DataSource = ds.Tables(0)

            'calculate totals 

            Dim c As Integer = DataGridView2.Rows.Count - 1
            Dim Total As Double = 0
            Dim CGST_Total As Double = 0
            Dim SGST_Total As Double = 0
            Dim GST_Total As Double = 0
            While c >= 0

                CGST_Total += Math.Round(((Val(DataGridView2.Rows(c).Cells("P_Qty").Value) * Val(DataGridView2.Rows(c).Cells("P_Rate").Value)) * Val(DataGridView2.Rows(c).Cells("P_CGST").Value)) / 100, 2)
                'MsgBox(CGST_Total)
                SGST_Total += Math.Round(((Val(DataGridView2.Rows(c).Cells("P_Qty").Value) * Val(DataGridView2.Rows(c).Cells("P_Rate").Value)) * Val(DataGridView2.Rows(c).Cells("P_SGST").Value)) / 100, 2)

                'MsgBox(SGST_Total)
                GST_Total += Math.Round(Val(DataGridView2.Rows(c).Cells("P_Total").Value), 2)
                Total += Math.Round((Val(DataGridView2.Rows(c).Cells("P_Qty").Value) * Val(DataGridView2.Rows(c).Cells("P_Rate").Value)), 2)
                c -= 1
            End While

            'MsgBox(CGST_Total)
            TextBox20.Text = Total
            TextBox16.Text = Math.Round(GST_Total - Total, 2)
            TextBox17.Text = CGST_Total
            TextBox18.Text = SGST_Total
            TextBox22.Text = GST_Total - Val(TextBox21.Text) + Val(TextBox19.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ComboBox1.Text <> "" Then
            If TextBox5.Text <> "" Then
                'If ComboBox4.Text <> "" Then
                If TextBox4.Text <> "" Then
                    If ComboBox2.Text <> "" Then
                        If ComboBox3.Text <> "" Then
                            If NumericUpDown1.Value <> 0 Then
                                If TextBox9.Text <> "" Then
                                    If TextBox10.Text <> "" Then
                                        If TextBox13.Text <> "" Then
                                            If TextBox1.Text <> "" Then
                                                If Val(TextBox8.Text) >= NumericUpDown1.Value Then

                                                    Dim d As New DAO


                                                    'PRODUCT ENTERY

                                                    Dim CGST As Double = Val(TextBox12.Text) / 2
                                                    d.modifyData("Insert into Purchase_details (bill_No, P_Cat, P_Product, P_Qty, P_Unit, P_Rate, P_CGST, P_SGST, P_HSN, bill_Tot, P_id) values ('" & TextBox1.Text & "','" & ComboBox2.Text & "','" & ComboBox3.Text & "'," & NumericUpDown1.Value & ",'" & TextBox9.Text & "','" & TextBox10.Text & "'," & TextBox3.Text & "," & TextBox2.Text & "," & TextBox13.Text & "," & TextBox11.Text & ", " & TextBox7.Text & ")")
                                                    MsgBox("Record Inserted")

                                                    ' LOAD DATA & CALCULATE TOTALS
                                                    load_invoice_data()

                                                    'UPDATE INVENTORY

                                                    Update_Inventory(0)


                                                    'UPDATE CREADITOR ACCOUNTS

                                                    Update_Creaditor_Accounts(0)

                                                    'ONE ENTRY INTO SALES SUMMERY

                                                    If DataGridView2.Rows.Count = 1 Then
                                                        d.modifyData("Insert into Purchase_Summary (D_Name, D_Address, bill_Date, bill_No, Payment_Mode, bill_Total, GST_Total, Round_off, L_SLIP) values ('" & ComboBox1.Text & "','" & TextBox4.Text & "','" & DateTimePicker3.Value.ToString("dd-MM-yyyy") & "','" & TextBox1.Text & "','" & ComboBox5.Text & "'," & TextBox20.Text & "," & TextBox16.Text & "," & TextBox19.Text & "," & TextBox23.Text & ")")
                                                        MsgBox("Record Summary")
                                                        TextBox14.Text = NumberToText(Val(TextBox22.Text))
                                                    Else
                                                        'UPDATE TOTALS

                                                        d.modifyData("Update Purchase_Summary Set GST_Total = " & TextBox16.Text & ", bill_Total = " & TextBox20.Text & ", Round_Off = " & TextBox19.Text & " where bill_No = '" & TextBox1.Text & "'")
                                                        TextBox14.Text = NumberToText(Val(TextBox22.Text))

                                                    End If



                                                    'UPDATE TOTALS IN CUSTOMER_DETAILS
                                                    'd.modifyData("Update Customer_Details set Total += " & Val(TextBox22.Text) & " where C_Name = '" & ComboBox1.Text & "'")
                                                    'customer_master.Load_Customer_Data()

                                                Else
                                                    MsgBox("Enter Valid Quantity")
                                                    NumericUpDown1.Focus()

                                                End If
                                            Else
                                                MessageBox.Show("Enter Valid Bill No.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                TextBox1.Focus()
                                            End If

                                        Else
                                            MessageBox.Show("Enter Valid HSN Code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                            TextBox13.Focus()
                                        End If
                                    Else
                                        MessageBox.Show("Enter Valid Product Rate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        TextBox10.Focus()
                                    End If
                                Else
                                    MessageBox.Show("Enter Valid Unit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    TextBox9.Focus()
                                End If
                            Else
                                MessageBox.Show("Enter Valid Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                NumericUpDown1.Focus()
                            End If
                        Else
                            MessageBox.Show("Select Valid Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            ComboBox3.Focus()
                        End If
                    Else
                        MessageBox.Show("Select Valid Category", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ComboBox2.Focus()
                    End If
                Else
                    MessageBox.Show("Enter Valid Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TextBox4.Focus()
                End If
                'Else
                '    MessageBox.Show("Select Valid Vehicle_No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    ComboBox4.Focus()
                'End If
            Else
                MessageBox.Show("Enter Valid Mobile_No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TextBox5.Focus()
            End If
        Else
            MessageBox.Show("Select Valid Dealer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.Focus()
        End If
    End Sub

    Private Sub Update_Creaditor_Accounts(ByVal v As Integer)
        'Try


        ' v = 0 --> Called From Insert
        ' v = 1 --> Called From Delete
        If ComboBox5.Text = "Credit" Then
                If DataGridView2.Rows.Count = 1 And v = 0 Then
                    'INSERT

                    '(1). Load Vouchar Number
                    Dim v_no As Integer = 0
                    Dim d As New DAO
                    Dim obj As SqlDataReader
                    obj = d.getData("Select v_no from configure_master")
                    While obj.Read
                        v_no = obj.Item("v_no")
                    End While
                    d.close_conn()
                    TextBox23.Text = v_no
                '(2) Insert Vouchar Number to Account
                d.modifyData("Insert into Account_Master (L_Name, L_id, L_Amount, L_Type, L_desc, L_SLIP) values ('" & ComboBox1.Text & "'," & ComboBox1.SelectedIndex & "," & TextBox22.Text & ",'Cr','" & TextBox1.Text & "', " & v_no & ")")

                '(3) Update Vouchar Number 
                d.modifyData("Update configure_master Set v_no = " & (v_no + 1))

                ElseIf DataGridView2.Rows.Count = 0 Then
                    ' No Record In Invoice
                    Dim d As New DAO
                    d.modifyData("Delete from Account_Master where L_SLIP = " & TextBox23.Text)
                Else

                    'UPDATE Account_Master

                    Dim d As New DAO
                    d.modifyData("Update Account_Master Set L_Amount = " & TextBox22.Text & " where L_SLIP = " & TextBox23.Text)

                End If
            End If

            'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub

    Private Sub Update_Inventory(v As Integer)
        Try
            If v = 0 Then
                'Inventory Plus
                Dim new_Qty As Double = Val(TextBox8.Text) + NumericUpDown1.Value
                MsgBox(new_Qty)
                Dim d As New DAO
                d.modifyData("Update Product Set Quantity = " & new_Qty & " where id = " & TextBox7.Text)

            Else
                'Inventory Minus
                Dim new_Qty As Double = Val(TextBox8.Text) - NumericUpDown1.Value
                MsgBox(new_Qty)
                Dim d As New DAO
                d.modifyData("Update Product Set Quantity = " & new_Qty & " where id = " & TextBox7.Text)


            End If

            TextBox8.Text = ""
            NumericUpDown1.Value = 0
            TextBox7.Text = ""
            Load_Product_data()
            dealer_master.Load_Dealer_Data()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub TextBox21_TextChanged(sender As Object, e As EventArgs) Handles TextBox21.TextChanged
        Dim discount As Double = (Val(TextBox20.Text) * Val(TextBox21.Text) / 100) 'Discount
        Dim total As Double = Val(TextBox20.Text) - discount
        TextBox19.Text = Math.Round(Math.Ceiling(total) - total, 2)
        TextBox22.Text = Math.Round(total, 0)
    End Sub

    Private Sub TextBox20_TextChanged(sender As Object, e As EventArgs) Handles TextBox20.TextChanged, TextBox16.TextChanged
        Dim total As Double = Val(TextBox16.Text) + Val(TextBox20.Text)
        TextBox19.Text = Math.Round(Math.Ceiling(total) - total, 2)
    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim bill_No As String = InputBox("Enter Invoice Number : ", "Search Invoice", "HS/2022/")
        TextBox1.Text = bill_No
        load_invoice_data()
        ComboBox1.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim c As Integer = MessageBox.Show("Do you want to Save Invoice?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If c = 6 Then
            Dim d As New DAO
            'UPDATE TOTALS
            d.modifyData("update Dealer_Summary set bill_Total =" & TextBox20.Text & ", GST_Total =" & TextBox16.Text & ", Round_Off=" & TextBox19.Text & " where bill_No ='" & TextBox1.Text & "'")
            Update_Creaditor_Accounts(1)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        resetfield()
    End Sub

    Public Function resetfield()
        ComboBox2.ResetText()
        ComboBox3.ResetText()
        NumericUpDown1.Value = 0
        TextBox9.ResetText()
        TextBox10.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox14.Text = ""
        TextBox8.Text = 0
        TextBox15.Text = ""
    End Function
End Class