using System.ComponentModel.DataAnnotations; // Hata mesajları için bu kütüphane ŞART!

namespace ODEVDAGITIM06.Models
{
    public class Ders
    {
        public int DersId { get; set; }

        [Required(ErrorMessage = "Ders Adı alanı zorunludur.")] // Hata mesajı eklendi
        [Display(Name = "Ders Adı")] // Güzel görünsün diye
        public string DersAdi { get; set; } // '?' işaretini kaldırdık

        [Required(ErrorMessage = "Ders Kodu alanı zorunludur.")] // Hata mesajı eklendi
        [Display(Name = "Ders Kodu")]
        public string DersKodu { get; set; } // '?' işaretini kaldırdık

        [Display(Name = "Açıklama")]
        public string? DersAciklamasi { get; set; } // Açıklama zorunlu değil, '?' kalabilir
    }
}