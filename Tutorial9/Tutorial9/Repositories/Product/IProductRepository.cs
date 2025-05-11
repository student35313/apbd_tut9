namespace Tutorial9.Repositories;

public interface IProductRepository
{
    Task<bool> ProductExistsAsync(int productId);
    Task<decimal> GetProductPriceAsync(int productId);
}