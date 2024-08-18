using System.ComponentModel.DataAnnotations;

namespace XuongMay.Models
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "ProductID is required")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "CategoryID is required")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount must be at least 1")]
        public int Amount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "UnitPrice must be greater than 0")]
        public double UnitPrice { get; set; }

        public bool Status { get; set; }
    }
}
