using System.Data;
using Microsoft.Data.SqlClient;
using Tutorial9.Model.DTOs;

namespace Tutorial9.Repositories.Product_Warehouse;

public class ProductWarehouseRepository : IProductWarehouseRepository
{
    private readonly string? _connectionString;

    public ProductWarehouseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<int> AddProductToWarehouse(ProductWarehouseInsertDTO dto, int orderId,
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
        var result = await cmd.ExecuteScalarAsync();
        var id = Convert.ToInt32(result);
        return id;

    }

    public async Task<int> ProcedureAddProductToWarehouse(ProductWarehouseInsertDTO dto)
    {
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        
        using var cmd = new SqlCommand("AddProductToWarehouse", conn)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
        cmd.Parameters.AddWithValue("@IdWarehouse", dto.IdWarehouse);
        cmd.Parameters.AddWithValue("@Amount", dto.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);
        var result = await cmd.ExecuteScalarAsync();
        return result == null? -1 : Convert.ToInt32(result);
         
    }
    
}