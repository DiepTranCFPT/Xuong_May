using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay.Models.Entity;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Name { get; set; }
    [MaxLength(100)]
    public string? Description { get; set; }

    public bool IsDeleted { get; set; }

    public ICollection<Product> Products { get; set; }
}