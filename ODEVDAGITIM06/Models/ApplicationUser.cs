using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ODEVDAGITIM06.Models
{
    // IdentityUser sınıfından miras alıyoruz.
    // Bu sayede Username, Email, PasswordHash gibi standart alanlara sahip oluyoruz.
    // Ekstra olarak Ad, Soyad ve Numara ekliyoruz.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Required]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Display(Name = "Öğrenci Numarası")]
        public string? OgrenciNo { get; set; } // Öğretmenler için boş olabilir
    }
}