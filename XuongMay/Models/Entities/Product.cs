using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay.Models.Entity;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductID { get; set; }
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public double UnitPrice { get; set; }
    public bool Status { get; set; }
    public Category Category { get; set; }
    public ICollection<OrderDetails> OrderDetails { get; set; }
}