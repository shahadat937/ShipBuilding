Public Class ClaInWord
    Public Function InWordLocal(ByVal lngVar As Double) As String
        Dim txtvar, txtvar1 As String, txtNeg As String
        Dim intVal As Double, intVal2 As Integer, intVal3 As Integer
        Dim intCore, intLac, intThousand, intHundred, intRest, intDes As String
        lngVar = System.Math.Abs(lngVar)
        intVal = Int(lngVar)
        intVal3 = ((lngVar - Int(lngVar)) * 100)
        If intVal3 <> 0 Then
            intVal2 = Format(intVal3, "##")
        Else
            intVal2 = 0
        End If
        'MsgBox intVal2
        intCore = CalHund(Int(intVal / 10000000))
        intVal = Int(intVal - Int(intVal / 10000000) * 10000000)

        intLac = CalHund(Int(intVal / 100000))
        intVal = Int(intVal - Int(intVal / 100000) * 100000)

        intThousand = CalHund(Int(intVal / 1000))
        intVal = Int(intVal - Int(intVal / 1000) * 1000)

        intHundred = CalHund(Int(intVal / 100))
        intVal = Int(intVal - Int(intVal / 100) * 100)

        intRest = CalHund(Int(intVal / 1))
        intDes = CalHund(Int(intVal2 / 1))

        txtvar1 = intCore + IIf(Len(Trim(intCore)) = 0, "", " Core")
        txtvar1 = txtvar1 + intLac + IIf(Len(Trim(intLac)) = 0, "", " Lac")
        txtvar1 = txtvar1 + intThousand + IIf(Len(Trim(intThousand)) = 0, "", " Thousand")
        txtvar1 = txtvar1 + intHundred + IIf(Len(Trim(intHundred)) = 0, "", " Hundred")
        txtvar1 = txtvar1 + intRest + " Taka"
        txtvar1 = txtvar1 + IIf(Len(Trim(intDes)) = 0, "", " and" + intDes + " Paisa")
        InWordLocal = txtvar1
    End Function

    Public Function CalHund(ByVal intVar As Integer) As String
        Dim ss, hund As Double
        Dim txtWord(36) As String
        Dim txtvar, txtvar1 As String
        txtWord(0) = ""
        txtWord(1) = "One"
        txtWord(2) = "Two"
        txtWord(3) = "Three"
        txtWord(4) = "Four"
        txtWord(5) = "Five"
        txtWord(6) = "Six"
        txtWord(7) = "Seven"
        txtWord(8) = "Eight"
        txtWord(9) = "Nine"
        txtWord(10) = "Ten"
        txtWord(11) = "Eleven"
        txtWord(12) = "Twelve"
        txtWord(13) = "Thirteen"
        txtWord(14) = "Fourteen"
        txtWord(15) = "Fifteen"
        txtWord(16) = "Sixteen"
        txtWord(17) = "Seventeen"
        txtWord(18) = "Eighteen"
        txtWord(19) = "Nineteen"
        txtWord(20) = "Twenty"
        txtWord(21) = "Thirty"
        txtWord(22) = "Forty"
        txtWord(23) = "Fifty"
        txtWord(24) = "Sixty"
        txtWord(25) = "Seventy"
        txtWord(26) = "Eighty"
        txtWord(27) = "Ninety"
        txtWord(28) = " One Hundred"
        txtWord(29) = "Two Hundred"
        txtWord(30) = "Three Hundred"
        txtWord(31) = " Four Hundred"
        txtWord(32) = "Five Hundred"
        txtWord(33) = "Six Hundred"
        txtWord(34) = " Seven Hundred"
        txtWord(35) = "Eight Hundred"
        txtWord(36) = "Nine Hundred"
        txtWord(36) = "One Thousend"

        Dim TwentyandLess, NintyNineandLess, NineHundredNintynineandLess, ThousendUp, RemTwodigit, RoundTwodigit, Doshk, Acok As Double


        Select Case intVar

            Case 0 To 20
                txtvar = txtWord(intVar)
            Case Else
                If intVar <= 99 Then
                    txtvar = txtWord(Int(intVar / 10) + 18) + " " + txtWord(intVar - Int(intVar / 10) * 10)

                ElseIf intVar <= 999 Then
                    If Mid(intVar, 2, 1) > 0 Then

                        If Mid(intVar, 2, 1) = 1 Then  'Only for when last two digit are  below TwentyOne
                            txtvar = txtWord(Int(intVar / 100)) + "  Hundred " + txtWord(intVar - Int(intVar / 100) * 100)

                        Else

                            RemTwodigit = Int(intVar - ((Int(intVar / 100)) * 100))
                            RoundTwodigit = (Int((intVar - ((Int(intVar / 100)) * 100)) / 10)) * 10
                            txtvar = txtWord(Int(intVar / 100)) + "  Hundred " + txtWord(Int(Int(intVar - Int(intVar / 100) * 100) / 10) + 18) + " " + txtWord(RemTwodigit - RoundTwodigit)
                        End If

                        RemTwodigit = 0
                        RoundTwodigit = 0
                    Else
                        'No Problem if Not exist Under this Portion of txtword function
                        txtvar = txtWord(Int(intVar / 100)) + "  Hundred " + txtWord(Int(intVar - Int(intVar / 100) * 100)) + " " + txtWord(((intVar - ((Int(intVar / 100)) * 100)) / 10))

                    End If

                ElseIf intVar >= 1000 Then

                    If Mid(intVar, 2, 2) = "00" Then
                        RemTwodigit = Int(intVar - ((Int(intVar / 1000)) * 1000))
                        txtvar = txtWord(Int(intVar / 1000)) + " Thousend " + txtWord(RemTwodigit)

                    ElseIf Mid(intVar, 2, 1) = 0 And intVar < 10000 Then
                        RemTwodigit = Int(intVar - ((Int(intVar / 1000)) * 1000))
                        'RoundTwodigit = (Int((intVar - ((Int(intVar / 1000)) * 1000)) / 100)) * 100
                        'Doshk = RemTwodigit - RoundTwodigit
                        Acok = RemTwodigit - (Int(RemTwodigit / 10) * 10)
                        txtvar = txtWord(Int(intVar / 1000)) + " Thousend " + txtWord(RemTwodigit / 10 + 18) + "" + txtWord(Acok)

                    ElseIf Mid(intVar, 3, 2) > 21 And intVar < 10000 Then

                        RemTwodigit = intVar - ((Int(intVar / 1000)) * 1000)
                        RoundTwodigit = (Int((intVar - ((Int(intVar / 1000)) * 1000)) / 100)) * 100
                        Doshk = RemTwodigit - RoundTwodigit
                        Acok = Doshk - (Int(Doshk / 10) * 10)
                        txtvar = txtWord(Int(intVar / 1000)) + " Thousend " + txtWord(Int((intVar - Int(intVar / 1000) * 1000) / 100)) + " Hundred " + txtWord(Int(Doshk / 10) + 18) + " " + txtWord(Acok)
                        RemTwodigit = 0
                        RoundTwodigit = 0


                        '12415 core Problem
                    ElseIf Mid(intVar, 3, 2) < 21 And intVar < 10000 Then      'Only for when last two digit are  below TwentyOne

                        RemTwodigit = Int(intVar - ((Int(intVar / 1000)) * 1000))
                        RoundTwodigit = (Int((intVar - ((Int(intVar / 1000)) * 1000)) / 100)) * 100
                        Doshk = RemTwodigit - RoundTwodigit
                        txtvar = txtWord(Int(intVar / 1000)) + " Thousend " + txtWord(Int((intVar - Int(intVar / 1000) * 1000) / 100)) + " Hundred " + txtWord(Doshk)
                        RemTwodigit = 0
                        RoundTwodigit = 0

                    ElseIf Mid(intVar, 3, 1) = 0 And intVar >= 10000 Then  'No Shatak

                        RemTwodigit = Int(intVar - ((Int(intVar / 1000)) * 1000))
                        RoundTwodigit = (Int((intVar - ((Int(intVar / 1000)) * 1000)) / 100)) * 100
                        Doshk = RemTwodigit - RoundTwodigit
                        txtvar = txtWord(Int(intVar / 1000)) + " Thousend " + " " + txtWord(Int(Doshk)) + txtWord(Acok)
                        RemTwodigit = 0
                        RoundTwodigit = 0


                    ElseIf Mid(intVar, 4, 2) < 20 And intVar >= 10000 Then   'No Dashk

                        RemTwodigit = Int(intVar - ((Int(intVar / 1000)) * 1000))
                        RoundTwodigit = (Int((intVar - ((Int(intVar / 1000)) * 1000)) / 100)) * 100
                        Doshk = RemTwodigit - RoundTwodigit
                        txtvar = txtWord(Int(intVar / 1000)) + " Thousend " + txtWord(Int((intVar - Int(intVar / 1000) * 1000) / 100)) + " Hundred " + " " + txtWord(Doshk)
                        RemTwodigit = 0
                        RoundTwodigit = 0


                    ElseIf Mid(intVar, 4, 2) > 20 And intVar >= 10000 Then

                        RemTwodigit = Int(intVar - ((Int(intVar / 1000)) * 1000))
                        RoundTwodigit = (Int((intVar - ((Int(intVar / 1000)) * 1000)) / 100)) * 100
                        Doshk = RemTwodigit - RoundTwodigit
                        Acok = Doshk - (Int(Doshk / 10) * 10)
                        txtvar = txtWord(Int(intVar / 1000)) + " Thousend " + txtWord(Int((intVar - Int(intVar / 1000) * 1000) / 100)) + " Hundred " + " " + txtWord(Int(Doshk) / 10 + 18) + txtWord(Acok)
                        RemTwodigit = 0
                        RoundTwodigit = 0



                    End If

                End If

        End Select

        CalHund = " " + txtvar

    End Function
    Function Compare(ByVal str1 As String, ByVal str2 As String) As Double
        Dim count As Integer = If(str1.Length > str2.Length, str1.Length, str2.Length)
        Dim hits As Integer = 0
        Dim i, j As Integer : i = 0 : j = 0
        For i = 0 To str1.Length - 1
            If str1.Chars(i) = " " Then i += 1 : j = str2.IndexOf(" "c, j) + 1 : hits += 1
            While j < str2.Length AndAlso str2.Chars(j) <> " "c
                If str1.Chars(i) = str2.Chars(j) Then
                    hits += 1
                    j += 1
                    Exit While
                Else
                    j += 1
                End If
            End While
            If Not (j < str2.Length AndAlso str2.Chars(j) <> " "c) Then
                j -= 1
            End If
        Next
        Return Math.Round((hits / count), 2)
    End Function
End Class
