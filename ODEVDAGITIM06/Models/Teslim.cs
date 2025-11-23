namespace ODEVDAGITIM06.Models // Proje adına göre güncellendi
{
    public class Teslim
    {
        public int TeslimId { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public string? DosyaYolu { get; set; }
        public int? Not { get; set; }

        public int OdevId { get; set; }
        public virtual Odev? Odev { get; set; }
    }
}