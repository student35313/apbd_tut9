using Microsoft.Data.SqlClient;
using Tutorial9.Model.DTOs;

namespace Tutorial9.Repositories;

public class ProductWarehouseRepository : IProductWarehouseRepository
{
    public async Task<int> AddOrderAsync(ProductWarehouseInsertDTO dto, int orderId,
        decimal price, SqlConnection connection, SqlTransaction transaction)
    {
        const string command = @"
            INSERT INTO Product_Warehouse 
                (IdProduct, IdWarehouse , IdOrder, Amount, CreatedAt , Price)
            VALUES (@IdProduct, @IdWarehouse , @IdOrder, @Amount, @CreatedAt , @Price);
            SELECT SCOPE_IDENTITY();
            ";
        await using var cmd = new SqlCommand(command, connection, transaction);
        cmd.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
        cmd.Parameters.AddWithValue("@IdWarehouse", dto.IdWarehouse);
        cmd.Parameters.AddWithValue("@IdOrder", orderId);
        cmd.Parameters.AddWithValue("@Amount", dto.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
        cmd.Parameters.AddWithValue("@Price", price);
        var id = await cmd.ExecuteScalarAsync();
        if (id == null) return -1;
        return (int)id;
        
    }
}