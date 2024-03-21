
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography

Public Class IBSHasCode
    '----------------old start----------------------
    'Public Function CreateDoubleHas(ByVal sUserId As String, ByVal sPassword As String) As String
    '    'Dim convertedToBytes As Byte() = Encoding.UTF8.GetBytes(sUserId & sPassword)
    '    'Dim hashType As HashAlgorithm = New SHA512Managed()
    '    'Dim hashBytes As Byte() = hashType.ComputeHash(convertedToBytes)
    '    'Dim hashedResult As String = Convert.ToBase64String(hashBytes)
    '    'Return hashedResult

    '    Dim mnString, getString, HasCode As String, mLen As Integer
    '    HasCode = ""
    '    Dim k As String, n, i As Integer
    '    If Len(sUserId) + Len(sPassword) <= 3 Then
    '        MsgBox("User Id or Password should be increased")
    '        Return ""
    '        Exit Function
    '    End If
    '    mnString = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ/\@!$1234567890+=-"

    '    'mnString = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ/\@!$1234567890-2468013579+=-"
    '    'mnString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=][}{<>"
    '    getString = CalAsc(sUserId, sPassword)
    '    mLen = Len(getString)
    '    For n = 1 To mLen
    '        k = Int(Mid(getString, n, 2))
    '        Select Case k
    '            Case 0
    '                i = 1
    '            Case k > 70
    '                i = 100 - k
    '            Case Else
    '                i = k
    '        End Select
    '        HasCode = HasCode + Mid(mnString, Math.Abs(i), 1)
    '    Next
    '    CreateDoubleHas = Mid(HasCode, 1, 8) + Mid(HasCode, Int(Len(HasCode) / 2) - 8, 16) + Mid(HasCode, Int(Len(HasCode)) - 8, 8)
    'End Function
    'Private Function CalAsc(ByVal sVar As String, ByVal sPass As String) As String
    '    Dim f, g As String, sValue As String
    '    Dim k, n, m, max, min As Integer
    '    g = ""
    '    sValue = ""
    '    max = 0
    '    min = 0
    '    k = Len(sVar) : m = Len(sPass)
    '    For n = 1 To k
    '        f = Mid(sVar, n, 1)
    '        sValue = sValue + Right(CStr(Asc(f) * 5 / 7), 5)
    '    Next
    '    For n = 1 To m
    '        f = Mid(sPass, n, 1)
    '        sValue = sValue + Right(CStr(Asc(f) * 6 / 7), 5)
    '    Next
    '    For n = 1 To k
    '        f = Mid(sVar, n, 1)
    '        sValue = sValue + Right(CStr(Asc(f) * 4 / 7), 5)
    '    Next
    '    For n = 1 To m
    '        f = Mid(sPass, n, 1)
    '        sValue = sValue + Right(CStr(Asc(f) * 3 / 7), 5)
    '    Next

    '    CalAsc = sValue
    'End Function

    '----------------old end----------------------
    Public Function CreateDoubleHas(ByVal sUserId As String, ByVal sPassword As String) As String
        Dim mnString, getString, HasCode As String, mLen As Integer
        HasCode = ""
        Dim k As String, n, i As Integer
        If Len(sUserId) + Len(sPassword) <= 7 Then
            MsgBox("User Id or Password should be increased")
            Return ""
            Exit Function
        End If
        'mnString = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ/\@!$1234567890+=-"
        'mnString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=][}{<>"
        mnString = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/\@!$1234567890+=-!@#$%^&*()_+=][}{<>"
        getString = CalAsc(sUserId, sPassword)
        mLen = Len(getString)
        For n = 1 To mLen
            k = Int(Mid(getString, n, 2))
            Select Case k
                Case 0
                    i = 1
                Case k > 70
                    i = 100 - k
                Case Else
                    i = k
            End Select
            HasCode = HasCode + Mid(mnString, Math.Abs(i), 2)
        Next
        CreateDoubleHas = Mid(HasCode, 1, 8) + Mid(HasCode, Int(Len(HasCode) / 2) - 8, 16) + Mid(HasCode, Int(Len(HasCode)) - 8, 8)
    End Function
    Private Function CalAsc(ByVal sVar As String, ByVal sPass As String) As String
        Dim f, g As String, sValue As String
        Dim k, n, m, max, min As Integer
        g = ""
        sValue = ""
        max = 0
        min = 0
        k = Len(sVar) : m = Len(sPass)
        For n = 1 To k
            f = Mid(sVar, n, 1)
            sValue = sValue + Right(CStr(Asc(f) * 5 / 7), 6)
        Next
        For n = 1 To m
            f = Mid(sPass, n, 1)
            sValue = sValue + Right(CStr(Asc(f) * 6 / 7), 5)
        Next
        For n = 1 To k
            f = Mid(sVar, n, 1)
            sValue = sValue + Right(CStr(Asc(f) * 4 / 7), 4)
        Next
        For n = 1 To m
            f = Mid(sPass, n, 1)
            sValue = sValue + Right(CStr(Asc(f) * 3 / 7), 7)
        Next

        CalAsc = sValue
    End Function

End Class
