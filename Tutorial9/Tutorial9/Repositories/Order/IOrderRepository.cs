using Microsoft.Data.SqlClient;

namespace Tutorial9.Repositories;

public interface IOrderRepository
{
    //Task<bool> OrderExistsAsync(int orderId);
    Task<int> FindAvailableOrderAsync(int ammount, DateTime createdAt);
    Task<bool> FullfillOrderAsync(int orderId , SqlConnection connection,
        SqlTransaction transaction);
}