using System.ComponentModel.DataAnnotations;
namespace MediaLabDapper.DTOs.TestimonialDto
{
    public class UpdateTestimonialDto
    {
        public int TestimonialId { get; set; }
        [Required(ErrorMessage = "Ad soyad boş bırakılamaz.")]
        [MaxLength(150, ErrorMessage = "Ad soyad en fazla 150 karakter olabilir.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Yorum boş bırakılamaz.")]
        [MaxLength(500, ErrorMessage = "Yorum en fazla 500 karakter olabilir.")]
        public string Comment { get; set; }
        [MaxLength(50, ErrorMessage = "Puan en fazla 50 karakter olabilir.")]
        public string? Review { get; set; }
        [MaxLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string? Title { get; set; }
        [MaxLength(500, ErrorMessage = "Resim url en fazla 500 karakter olabilir.")]
        public string? ImageUrl { get; set; }
    }
}