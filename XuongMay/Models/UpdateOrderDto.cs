namespace XuongMay.Models
{
    public class UpdateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public double Amount { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }

        public bool Status { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
