using System;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string? Name { get; set; }

        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Price must be positive")]
        public int? Price { get; set; }

        [Required]
        public string? Description { get; set; }
        
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        public bool IsPublished { get; set; }
        
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Stock must be positive")]
        public int? Stock { get; set; }

        [Required] public string Category { get; set; } = "";

        public DateTime ExpireDate { get; set; } = DateTime.Today.AddDays(9);
    }
}

