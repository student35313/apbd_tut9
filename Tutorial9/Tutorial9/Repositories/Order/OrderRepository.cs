using Microsoft.Data.SqlClient;

namespace Tutorial9.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string? _connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    /*
    public async Task<bool> OrderExistsAsync(int orderId)
    {
        {
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            const string command = @"
            SELECT 1 FROM Order
            WHERE IdOrder = @IdOrder";

            await using var cmd = new SqlCommand(command, conn);
            cmd.Parameters.AddWithValue("@IdOrder", orderId);

            await using var reader = await cmd.ExecuteReaderAsync();
            return await reader.ReadAsync();
        }
    }
    */

    public async Task<int> FindAvailableOrderAsync(int ammount, DateTime createdAt)
    {
        var orderId = -1;
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        const string command = @"
            SELECT TOP 1 o.IdOrder
            FROM [Order] o
            LEFT JOIN Product_Warehouse pw ON pw.IdOrder = o.IdOrder
            WHERE o.IdProduct = @IdProduct
            AND o.Amount = @Amount
            AND o.CreatedAt < @CreatedAt
            AND pw.IdOrder IS NULL
            ORDER BY o.CreatedAt ASC;";

        await using var cmd = new SqlCommand(command, conn);
        cmd.Parameters.AddWithValue("@Amount", ammount);
        cmd.Parameters.AddWithValue("@CreatedAt", createdAt);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            orderId = reader.GetInt32(0);
        }

        return orderId;
    }
    
    public async Task<bool> FullfillOrderAsync(int orderId , SqlConnection connection, SqlTransaction transaction)
    {
        const string command = @"
            UPDATE [Order]
            SET FulfilledAt = @Date
            WHERE IdOrder = @IdOrder;
            ";
        
        await using var cmd = new SqlCommand(command, connection, transaction);
        cmd.Parameters.AddWithValue("@IdOrder", orderId);
        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
        var rowsAffected = await cmd.ExecuteNonQueryAsync();
        if (rowsAffected == 0) return false;
        return true;
    }
}