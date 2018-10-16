Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml
Imports System.Management
Imports System.Net
Imports System.Runtime.CompilerServices

Module Helper

    Function GetHex(filename As String, offset As Integer, requiredBytes As Integer) As Byte()
        Dim value(0 To requiredBytes - 1) As Byte
        Using reader As New BinaryReader(File.Open(filename, FileMode.Open))
            ' Loop through length of file.
            Dim fileLength As Long = reader.BaseStream.Length
            Dim byteCount As Integer = 0
            reader.BaseStream.Seek(offset, SeekOrigin.Begin)
            While offset < fileLength And byteCount < requiredBytes
                value(byteCount) = reader.ReadByte()
                offset += 1
                byteCount += 1
            End While
        End Using

        Return value
    End Function

    Function Neg60(offset As Integer) As Integer
        Return (offset - 60)
    End Function

    Function Plus60(offset As Integer) As Integer
        Return (offset + 60)
    End Function

    Sub SetHex(filename As String, offset As Long, value As Byte())
        Try
            Dim fs As New FileStream(filename, FileMode.Open)
            Dim reader As New BinaryReader(fs)
            fs.Position = offset
            For Each num As Byte In value
                If num.ToString() = String.Empty Then
                    Exit For
                End If
                reader.BaseStream.WriteByte(num)
            Next
            reader.Close()
        Catch ex As Exception
            NSMessageBox.ShowOk(ex.Message, MessageBoxIcon.Error, "Error")
            Logger.Log(ex.Message & ex.StackTrace)
        End Try
    End Sub

    Function Neg3C(offset As Long) As Long
        Return (offset - &H3C)
    End Function

    Function Plus3C(offset As Long) As Long
        Return (offset + &H3C)
    End Function

    Function GetHexStringFromBinary(hex As Byte()) As String
        Return BitConverter.ToString(hex).Replace("-", "")
    End Function

    Function SetValue(val As Integer) As Byte()
        Return HexStringToBinary(val.ToString("X2"))
    End Function

    Function SetValue4(val As Integer) As Byte()
        Return HexStringToBinary(val.ToString("X4"))
    End Function

    Function IsURLValid(url As String) As Boolean
        Try
            Dim Client As WebClientEx = New WebClientEx() With {.Timeout = 5000}

            Dim reader As StreamReader = New StreamReader(Client.OpenRead(Convert.ToString(url)))
            Dim Source As String = reader.ReadToEnd
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Function GetSeatName(hex As Byte()) As String
        Dim result As String = Nothing
        Select Case BitConverter.ToString(hex).Replace("-", "")
            Case "01"
                result = "A1"
            Case "02"
                result = "A2"
            Case "03"
                result = "B1"
            Case "04"
                result = "B2"
            Case "05"
                result = "C1"
            Case "06"
                result = "C2"
            Case "07"
                result = "D1"
            Case "08"
                result = "D2"
            Case "00"
                result = "Single"
        End Select

        Return result ' & " | " & BitConverter.ToString(hex)
    End Function

    Sub SetSeatName(str As String, e2promFile As String)
        Dim _042A As String = BitConverter.ToString(GetHex(e2promFile, &H4, 39)).Replace("-", "")
        Dim _2C73 As String = BitConverter.ToString(GetHex(e2promFile, &H2C, 72)).Replace("-", "")
        Dim _2B As String = Nothing

        Select Case str
            Case "A1"
                _2B = "01"
            Case "A2"
                _2B = "02"
            Case "B1"
                _2B = "03"
            Case "B2"
                _2B = "04"
            Case "C1"
                _2B = "05"
            Case "C2"
                _2B = "06"
            Case "D1"
                _2B = "07"
            Case "D2"
                _2B = "08"
            Case "Single"
                _2B = "00"
        End Select

        Dim result As String = CalculateCRC32(_042A & _2B & _2C73)
        Dim whole As String = result & _042A & _2B & _2C73 & result & _042A & _2B & _2C73

        SetHex(e2promFile, &H0, HexStringToBinary(whole))
    End Sub

    Function CalculateCRC32(hexString As String) As String
        Dim crc As New Crc32()
        Dim rbytes As Byte() = crc.ComputeHash(HexStringToBinary(hexString))
        Dim result As String = BitConverter.ToUInt32(rbytes, 0).ToString("X8")
        Dim F4 = result.Substring(0, 4)
        Dim L4 = result.Substring(4)
        Dim F2 = F4.Substring(0, 2)
        Dim L2 = F4.Substring(2)
        Dim _F2 = L4.Substring(0, 2)
        Dim _L2 = L4.Substring(2)
        Dim _0003 As String = _L2 & _F2 & L2 & F2
        Return _0003
    End Function

    Function HexStringToBinary(ByVal hexString As String) As Byte()
        Return Enumerable.Range(0, hexString.Length).Where(Function(x) x Mod 2 = 0).[Select](Function(x) Convert.ToByte(hexString.Substring(x, 2), 16)).ToArray()
    End Function

    Function SafeImageFromFile(path As String) As Image
        Dim bytes = File.ReadAllBytes(path)
        Using ms As New MemoryStream(bytes)
            Dim img = Image.FromStream(ms)
            Return img
        End Using
    End Function

    Function Md5Sum(strToEncrypt As String) As String
        Dim ue As New System.Text.UTF8Encoding()
        Dim bytes As Byte() = ue.GetBytes(strToEncrypt)
        Dim md5 As New MD5CryptoServiceProvider()
        Dim hashBytes As Byte() = md5.ComputeHash(bytes)
        Dim hashString As String = ""
        For i As Integer = 0 To hashBytes.Length - 1
            hashString += Convert.ToString(hashBytes(i), 16).PadLeft(2, "0"c)
        Next
        Return hashString.PadLeft(32, "0"c)
    End Function

    Function GetProcessorId() As String
        Dim strProcessorId As String = String.Empty
        Dim query As New SelectQuery("Win32_processor")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()
            strProcessorId = info("processorId").ToString()
        Next
        Return strProcessorId
    End Function

    Function GetMACAddress() As String
        Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
        Dim moc As ManagementObjectCollection = mc.GetInstances()
        Dim MACAddress As String = String.Empty
        For Each mo As ManagementObject In moc
            If (MACAddress.Equals(String.Empty)) Then
                If CBool(mo("IPEnabled")) Then MACAddress = mo("MacAddress").ToString()
                mo.Dispose()
            End If
            MACAddress = MACAddress.Replace(":", String.Empty)
        Next
        Return MACAddress
    End Function

    Function GetVolumeSerial(Optional ByVal strDriveLetter As String = "C") As String
        Dim disk As ManagementObject = New ManagementObject(String.Format("win32_logicaldisk.deviceid=""{0}:""", strDriveLetter))
        disk.Get()
        Return disk("VolumeSerialNumber").ToString()
    End Function

    Function GetMotherBoardID() As String

        Dim strMotherBoardID As String = String.Empty
        Dim query As New SelectQuery("Win32_BaseBoard")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()
            strMotherBoardID = info("SerialNumber").ToString()
        Next
        Return strMotherBoardID
    End Function

    Function getMD5Hash(ByVal strToHash As String) As String
        Dim md5Obj As New MD5CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)
        bytesToHash = md5Obj.ComputeHash(bytesToHash)
        Dim strResult As String = ""
        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next
        Return strResult
    End Function

    Function getNewCPUID() As String
        Return getMD5Hash(GetMotherBoardID() & GetVolumeSerial() & GetMACAddress() & GetProcessorId()).Substring(0, 14).ToUpper
    End Function

    Function EncryptSHA256Managed(ByVal ClearString As String) As String
        Dim sha256 As SHA256 = SHA256Managed.Create()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(ClearString)
        Dim hash As Byte() = sha256.ComputeHash(bytes)
        Dim stringBuilder As New StringBuilder()

        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next

        Return stringBuilder.ToString()
    End Function

    Sub wait(ByVal interval As Integer)
        Dim stopW As New Stopwatch
        stopW.Start()
        Do While stopW.ElapsedMilliseconds < interval
            ' Allows your UI to remain responsive
            Application.DoEvents()
        Loop
        stopW.Stop()
    End Sub

    Function HexToBinary(hexString As String) As String
        Dim result As String = Nothing
        Dim num As Integer = CInt("&H" & hexString)
        Do While num > 0
            result = (num Mod 2).ToString & result
            num = num \ 2
        Loop
        Return result
    End Function

    Function BinaryToHex(binaryString As String) As String
        Return Convert.ToString(Convert.ToInt32(binaryString, 2), 16).ToUpper
    End Function

    Private sjis As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift_JIS")
    <Extension()>
    Public Function IsWideEastAsianWidth_SJIS(ByVal c As Char) As Boolean
        Dim byteCount As Integer = sjis.GetByteCount(c.ToString())
        Return byteCount = 2
    End Function

    Public Function FindFocussedControl(ByVal ctr As Control) As Control
        Dim container As ContainerControl = TryCast(ctr, ContainerControl)
        Do While (container IsNot Nothing)
            ctr = container.ActiveControl
            container = TryCast(ctr, ContainerControl)
        Loop
        Return ctr
    End Function

    <Extension()>
    Public Sub ChangeBackgroundImage(picturebox As PictureBox, image As Bitmap)
        If picturebox.BackgroundImage IsNot Nothing Then picturebox.BackgroundImage.Dispose()
        picturebox.BackgroundImage = Nothing
        picturebox.BackgroundImage = image
    End Sub
End Module
