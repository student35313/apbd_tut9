using Microsoft.Data.SqlClient;

namespace Tutorial9.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly string? _connectionString;

    public WarehouseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<bool> WarehouseExistsAsync(int warehouseId)
    {
        
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            const string command = @"
            SELECT 1 FROM Warehouse
            WHERE IdWarehouse = @IdWarehouse";

            await using var cmd = new SqlCommand(command, conn);
            cmd.Parameters.AddWithValue("@IdWarehouse", warehouseId);

            await using var reader = await cmd.ExecuteReaderAsync();
            return await reader.ReadAsync();
        
    }
}