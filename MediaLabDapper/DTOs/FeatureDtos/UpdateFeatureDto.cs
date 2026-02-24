using System.ComponentModel.DataAnnotations;
namespace MediaLabDapper.DTOs.FeatureDtos
{
    public class UpdateFeatureDto
    {
        public int FeatureId { get; set; }
        [Required(ErrorMessage = "Başlık boş bırakılamaz.")]
        [MaxLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
        [MaxLength(300, ErrorMessage = "Açıklama en fazla 300 karakter olabilir.")]
        public string Description { get; set; }
        [MaxLength(100, ErrorMessage = "Ikon bilgisi en fazla 100 karakter olabilir.")]
        public string? Icon { get; set; }
    }
}