namespace MediaLabDapper.DTOs.FeatureDtos
{
    public class GetByIdFeatureDto
    {
        public int FeatureId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}