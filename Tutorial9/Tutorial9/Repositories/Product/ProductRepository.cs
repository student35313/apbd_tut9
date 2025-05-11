using Microsoft.Data.SqlClient;

namespace Tutorial9.Repositories.Product;

public class ProductRepository : IProductRepository
{
    private readonly string? _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<decimal> GetProductPriceAsync(int productId)
    {
        var price = decimal.MinValue;
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        const string command = @"
            SELECT Price FROM Product
            WHERE IdProduct = @IdProduct;
            ";
        
        await using var cmd = new SqlCommand(command, conn);
        cmd.Parameters.AddWithValue("@IdProduct", productId);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            price = reader.GetDecimal(0);
        }

        return price;
    }
    
}