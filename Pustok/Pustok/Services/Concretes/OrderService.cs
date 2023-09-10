using Pustok.Database;


namespace Pustok.Services.Concretes;

public class OrderService
{
    private readonly PustokDbContext _dbContext;

    public OrderService(PustokDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string GenerateUniqueOrderCode()
    {
        string orderCode;
        do
        {
            orderCode = GenerateOrderCode();
        }
        while (_dbContext.Orders.Any(o => o.OrderCode == orderCode));
        
        return orderCode;
    }

    private string GenerateOrderCode()
    {
        string prefix = "OR";
        string randomDigits = GenerateRandomDigits(5);
        return prefix + randomDigits;
    }

    private string GenerateRandomDigits(int length)
    {
        Random random = new Random();
        string digits = "";
        for (int i = 0; i < length; i++)
        {
            digits += random.Next(0, 10);
        }
        return digits;
    }
}
