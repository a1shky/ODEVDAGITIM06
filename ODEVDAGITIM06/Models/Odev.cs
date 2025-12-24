using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // ForeignKey için gerekli

namespace ODEVDAGITIM06.Models
{
    public class Odev
    {
        public int OdevId { get; set; }

        [Required(ErrorMessage = "Ödev Başlığı alanı zorunludur.")]
        [Display(Name = "Ödev Başlığı")]
        public string OdevBasligi { get; set; }

        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        public DateTime OlusturmaTarihi { get; set; }

        [Required(ErrorMessage = "Lütfen bir teslim tarihi seçin.")]
        [Display(Name = "Son Teslim Tarihi")]
        public DateTime TeslimTarihi { get; set; }

        [Required(ErrorMessage = "Lütfen bir ders seçin.")]
        [Display(Name = "Ders")]
        public int DersId { get; set; }
        public virtual Ders? Ders { get; set; }

        // --- YENİ EKLENEN KISIM: KİŞİYE ÖZEL ÖDEV ---

        [Display(Name = "Öğrenci (Kime Atanacak?)")]
        public string? OgrenciId { get; set; } // Hangi öğrenciye atandığı

        [ForeignKey("OgrenciId")]
        public virtual ApplicationUser? Ogrenci { get; set; }
    }
}