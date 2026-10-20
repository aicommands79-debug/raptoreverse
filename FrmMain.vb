Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Class FrmMain

    Private Const WM_DEVICECHANGE As Integer = &H219
    Private Const DBT_DEVICEARRIVAL As Integer = &H8000

    Public Armed As Boolean = False
    Public UnlockFileFound As Boolean = False
    Public UnlockDrive As String = ""
    Public FilePath As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        If StLbl1 IsNot Nothing Then StLbl1.Text = "Waiting"
        If StLbl2 IsNot Nothing Then StLbl2.Text = "Waiting for USB device"
        If StLbl3 IsNot Nothing Then StLbl3.Text = "Ready"
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_DEVICECHANGE AndAlso CInt(m.WParam) = DBT_DEVICEARRIVAL Then
            Armed = True
            UnlockFileFound = True
            UnlockDrive = "USB device"
            FilePath = "USB device"

            If StLbl1 IsNot Nothing Then StLbl1.Text = "Armed"
            If StLbl2 IsNot Nothing Then StLbl2.Text = "USB device detected"
            If StLbl3 IsNot Nothing Then StLbl3.Text = "Unlocked"

            CloseLockForms()
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Armed = True
        UnlockFileFound = True
        If StLbl1 IsNot Nothing Then StLbl1.Text = "Armed"
        If StLbl2 IsNot Nothing Then StLbl2.Text = "USB device detected"
        If StLbl3 IsNot Nothing Then StLbl3.Text = "Unlocked"
        CloseLockForms()
    End Sub

    Private Sub CheckDrives()
        Armed = True
        UnlockFileFound = True
        If StLbl1 IsNot Nothing Then StLbl1.Text = "Armed"
        If StLbl2 IsNot Nothing Then StLbl2.Text = "USB device detected"
        If StLbl3 IsNot Nothing Then StLbl3.Text = "Unlocked"
        CloseLockForms()
    End Sub

    Private Sub LoadDrives()
        If ComboBox1 IsNot Nothing Then
            ComboBox1.Items.Clear()
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
        If StLbl1 IsNot Nothing Then StLbl1.Text = "Armed"
        If StLbl2 IsNot Nothing Then StLbl2.Text = "USB unlocked"
        If StLbl3 IsNot Nothing Then StLbl3.Text = "Unlocked"
        CloseLockForms()
    End Sub

    Private Sub WriteKey()
        ' not used in the simplified USB-any-device mode
    End Sub

End Class
