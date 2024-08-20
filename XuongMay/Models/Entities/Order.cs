using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XuongMay.Models.Entity;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public double Amount { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerPhone { get; set; }

    public bool Status { get; set; }

    public ICollection<OrderDetails> OrderDetails { get; set; }
}