using System.ComponentModel.DataAnnotations; // Hata mesajları için bu kütüphane ŞART!

namespace ODEVDAGITIM06.Models
{
    public class Odev
    {
        public int OdevId { get; set; }

        [Required(ErrorMessage = "Ödev Başlığı alanı zorunludur.")]
        [Display(Name = "Ödev Başlığı")]
        public string OdevBasligi { get; set; } // '?' işaretini kaldırdık

        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; } // Açıklama zorunlu değil, '?' kalabilir

        public DateTime OlusturmaTarihi { get; set; }

        [Required(ErrorMessage = "Lütfen bir teslim tarihi seçin.")]
        [Display(Name = "Son Teslim Tarihi")]
        public DateTime TeslimTarihi { get; set; }

        [Required(ErrorMessage = "Lütfen bir ders seçin.")] // Dropdown için
        [Display(Name = "Ders")]
        public int DersId { get; set; }
        public virtual Ders? Ders { get; set; }
    }
}