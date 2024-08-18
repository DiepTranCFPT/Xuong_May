using System.ComponentModel.DataAnnotations;

namespace XuongMay.Models
{
    public class AddCategoryDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
