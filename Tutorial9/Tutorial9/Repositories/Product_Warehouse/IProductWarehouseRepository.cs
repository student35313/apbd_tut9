using Microsoft.Data.SqlClient;
using Tutorial9.Model.DTOs;

namespace Tutorial9.Repositories;

public interface IProductWarehouseRepository
{
    Task<int> AddOrderAsync(ProductWarehouseInsertDTO dto, int orderId,
        decimal price, SqlConnection connection, SqlTransaction transaction);
}