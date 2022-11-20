Module Module1
    Public uname As String = ""
    Public utype As String = ""

    Public invno As String = ""
    Public dk As New DataSet
    Public r_type As String = ""
    Public dk1 As New Data.DataSet


    Public Sub AutoSearch(ByRef xcb As ComboBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal blnLimitToList As Boolean)

        Dim strFindStr As String = ""

        If e.KeyChar = ChrW(8) Then

            If (xcb.SelectionStart <= 1) Then

                xcb.Text = ""
                Exit Sub
            End If

            If (xcb.SelectionLength = 0) Then

                strFindStr = xcb.Text.Substring(0, xcb.Text.Length - 1)

            Else

                strFindStr = xcb.Text.Substring(0, xcb.SelectionStart - 1)
            End If

        Else

            If (xcb.SelectionLength = 0) Then

                strFindStr = xcb.Text + e.KeyChar

            Else

                strFindStr = xcb.Text.Substring(0, xcb.SelectionStart) + e.KeyChar
            End If

            Dim intIdx As Integer = -1
            ' Search the string in the ComboBox list.  

            intIdx = xcb.FindString(strFindStr)
            If (intIdx <> -1) Then

                xcb.SelectedText = ""
                xcb.SelectedIndex = intIdx
                xcb.SelectionStart = strFindStr.Length
                xcb.SelectionLength = xcb.Text.Length
                e.Handled = True

            Else

                e.Handled = blnLimitToList

            End If
        End If
    End Sub

    Public Function NumberToText(ByVal n As Integer) As String

        Select Case n
            Case 0
                Return ""

            Case 1 To 19
                Dim arr() As String = {"One", "Two", "Three", "Four", "Five", "Six", "Seven",
    "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen",
      "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
                Return arr(n - 1) & " "

            Case 20 To 99
                Dim arr() As String = {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
                Return arr(n \ 10 - 2) & " " & NumberToText(n Mod 10)

            Case 100 To 199
                Return "One Hundred " & NumberToText(n Mod 100)

            Case 200 To 999
                Return NumberToText(n \ 100) & "Hundreds " & NumberToText(n Mod 100)

            Case 1000 To 1999
                Return "One Thousand " & NumberToText(n Mod 1000)

            Case 2000 To 999999
                Return NumberToText(n \ 1000) & "Thousands " & NumberToText(n Mod 1000)

            Case 1000000 To 1999999
                Return "One Million " & NumberToText(n Mod 1000000)

            Case 1000000 To 999999999
                Return NumberToText(n \ 1000000) & "Millions " & NumberToText(n Mod 1000000)

            Case 1000000000 To 1999999999
                Return "One Billion " & NumberToText(n Mod 1000000000)

            Case Else
                Return NumberToText(n \ 1000000000) & "Billion " _
    & NumberToText(n Mod 1000000000)
        End Select
    End Function

End Module