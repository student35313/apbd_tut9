using Microsoft.Data.SqlClient;
using Tutorial9.Model.DTOs;

namespace Tutorial9.Repositories.Product_Warehouse;

public interface IProductWarehouseRepository
{
    Task<int> AddProductToWarehouse(ProductWarehouseInsertDTO dto, int orderId,
        decimal price, SqlConnection connection, SqlTransaction transaction);

    Task<int> ProcedureAddProductToWarehouse(ProductWarehouseInsertDTO dto);
}