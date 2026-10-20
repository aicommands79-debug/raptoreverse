Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class FrmMain

    Private Const WM_DEVICECHANGE As Integer = &H219
    Private Const DBT_DEVICEARRIVAL As Integer = &H8000
    Private Const DBT_DEVTYP_VOLUME As Integer = &H2

    Public Structure DEV_BROADCAST_HDR
        Public dbch_size As Int32
        Public dbch_devicetype As Int32
        Public dbch_reserved As Int32
    End Structure

    Private Structure DEV_BROADCAST_VOLUME
        Public dbcv_size As Int32
        Public dbcv_devicetype As Int32
        Public dbcv_reserved As Int32
        Public dbcv_unitmask As Int32
        Public dbcv_flags As Int16
    End Structure

    Public Armed As Boolean = False
    Public UnlockFileFound As Boolean = False
    Public UnlockDrive As String = ""
    Public FilePath As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDrives()
        Timer1.Enabled = True
        StLbl1.Text = "Waiting"
        StLbl2.Text = "Waiting for USB"
        StLbl3.Text = "Ready"
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_DEVICECHANGE AndAlso CInt(m.WParam) = DBT_DEVICEARRIVAL Then
            Dim deviceInfo As DEV_BROADCAST_HDR = DirectCast(Marshal.PtrToStructure(m.LParam, GetType(DEV_BROADCAST_HDR)), DEV_BROADCAST_HDR)
            If deviceInfo.dbch_devicetype = DBT_DEVTYP_VOLUME Then
                Dim volume As DEV_BROADCAST_VOLUME = DirectCast(Marshal.PtrToStructure(m.LParam, GetType(DEV_BROADCAST_VOLUME)), DEV_BROADCAST_VOLUME)
                Dim driveLetter As String = (GetDriveLetterFromMask(volume.dbcv_unitmask) & ":\")
                Dim drive As DriveInfo = New DriveInfo(driveLetter)
                If drive.DriveType = DriveType.Removable AndAlso drive.IsReady Then
                    UnlockDrive = driveLetter
                    FilePath = driveLetter
                    Armed = True
                    UnlockFileFound = True
                    StLbl1.Text = "Armed"
                    StLbl2.Text = "USB detected"
                    StLbl3.Text = "Unlocked"
                    CloseLockForms()
                End If
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Private Shared Function GetDriveLetterFromMask(ByRef unit As Integer) As Char
        Dim i As Integer
        For i = 0 To 25
            If (unit And 1) <> 0 Then Exit For
            unit = unit >> 1
        Next
        Return Chr(i + Asc("A"))
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        CheckDrives()
    End Sub

    Private Sub CheckDrives()
        For Each drive As DriveInfo In DriveInfo.GetDrives()
            If drive.DriveType = DriveType.Removable AndAlso drive.IsReady Then
                UnlockDrive = drive.Name
                FilePath = drive.Name
                Armed = True
                UnlockFileFound = True
                StLbl1.Text = "Armed"
                StLbl2.Text = "USB detected: " & drive.Name
                StLbl3.Text = "Unlocked"
                CloseLockForms()
                Exit Sub
            End If
        Next

        Armed = False
        UnlockFileFound = False
        StLbl1.Text = "Disarmed"
        StLbl2.Text = "No USB detected"
        StLbl3.Text = "Ready"
    End Sub

    Private Sub LoadDrives()
        If ComboBox1 IsNot Nothing Then
            ComboBox1.Items.Clear()
            For Each drive As DriveInfo In DriveInfo.GetDrives()
                If drive.DriveType = DriveType.Removable Then
                    ComboBox1.Items.Add(drive.Name)
                End If
            Next
        End If
    End Sub

    Private Sub CloseLockForms()
        Try
            If FrmMonitor1 IsNot Nothing AndAlso FrmMonitor1.Visible Then FrmMonitor1.Close()
            If FrmMonitor2 IsNot Nothing AndAlso FrmMonitor2.Visible Then FrmMonitor2.Close()
            If FrmMonitor3 IsNot Nothing AndAlso FrmMonitor3.Visible Then FrmMonitor3.Close()
        Catch
        End Try
    End Sub

    Public Function Encrypt(ByVal plainText As String) As String
        Return plainText
    End Function

    Public Function Decrypt(ByVal cipherText As String) As String
        Return cipherText
    End Function

    Private Sub ReadFile()
        Armed = True
        UnlockFileFound = True
        StLbl1.Text = "Armed"
        StLbl2.Text = "USB unlocked"
        StLbl3.Text = "Unlocked"
        CloseLockForms()
    End Sub

    Private Sub WriteKey()
        If ComboBox1 IsNot Nothing AndAlso ComboBox1.Text <> "" Then
            Dim fileName As String = ComboBox1.Text & "unlock.k3y"
            File.WriteAllText(fileName, "USB_UNLOCK")
        End If
    End Sub

End Class
