using System.ComponentModel.DataAnnotations.Schema; // ForeignKey için gerekli

namespace ODEVDAGITIM06.Models
{
    public class Teslim
    {
        public int TeslimId { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public string? DosyaYolu { get; set; }
        public int? Not { get; set; }

        public int OdevId { get; set; }
        public virtual Odev? Odev { get; set; }

        // YENİ EKLENDİ: Öğrenci İlişkisi
        // String çünkü IdentityUser'ın ID'si varsayılan olarak string'dir (GUID)
        public string? OgrenciId { get; set; }

        [ForeignKey("OgrenciId")]
        public virtual ApplicationUser? Ogrenci { get; set; }
    }
}