using Microsoft.Data.SqlClient;
using Tutorial9.Model.DTOs;

namespace Tutorial9.Repositories.Order;

public interface IOrderRepository
{
    Task<int> FindAvailableOrderAsync(ProductWarehouseInsertDTO dto);
    Task<bool> FullfillOrderAsync(int orderId , SqlConnection connection,
        SqlTransaction transaction);
}