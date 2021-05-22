using System;

namespace Service.Catalog.Dtos
{
    public class CourseDto
    {
     
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string UserId { get; set; }

        public string Picture { get; set; }

        public DateTime CreateTime { get; set; }

        public FeatureDto Feature { get; set; }

        public string CatagoryId { get; set; }
        
        public CategoryDto Catagory { get; set; }
    }
}