using Microsoft.Data.SqlClient;

namespace Tutorial9.Repositories.Product;

public class ProductRepository : IProductRepository
{
    private readonly string? _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<decimal> GetProductPriceAsync(int? productId, SqlConnection connection)
    {
        var price = decimal.MinValue;
        
        const string command = @"
            SELECT Price FROM Product
            WHERE IdProduct = @IdProduct;
            ";
        
        await using var cmd = new SqlCommand(command, connection);
        cmd.Parameters.AddWithValue("@IdProduct", productId);

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            price = reader.GetDecimal(0);
        }

        return price;
    }
    
}