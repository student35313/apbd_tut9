using Microsoft.Data.SqlClient;

namespace Tutorial9.Repositories.Product;

public interface IProductRepository
{
    Task<decimal> GetProductPriceAsync(int? productId , SqlConnection connection);
}