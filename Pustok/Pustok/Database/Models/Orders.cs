namespace Pustok.Database.Models;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } 
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public int StatusId { get; set; } 
    public Status Status { get; set; }
}
