Imports System.Text
Imports System.IO

Module Module1
    Public Armed As Boolean = False

    ' Şifreleme kalktı, doğrudan boş bırakabilir veya başka bir şey yazabilirsin
    Public MasterUnlockText = ""

    ' USB takıldığında çalıştırılacak ana işlem
    Public Sub CalistirilacakIslem()
        ' Buraya USB takıldığında yapılmasını istediğin kodu yazabilirsin
        ' Örnek: MessageBox.Show("USB Takıldı!") veya başka bir komut
    End Sub

    ' Eski fonksiyonların yerini tutan sahte fonksiyonlar (Kodun diğer yerleri hata vermesin diye)
    Public Function MasterEncrypt() As String
        ' Eskiden şifreleme yapıyordu, artık doğrudan işlem tetikliyor
        CalistirilacakIslem()
        Return "USB_Aksiyonu_Baslatildi"
    End Function

    Public Function MasterDecrypt(ByVal cipherText As String) As String
        On Error Resume Next
        ' Eskiden şifre çözüyordu, artık USB takıldığında tetikleyici olarak kullanılabilir
        CalistirilacakIslem()
        Return cipherText
    End Function

End Module