namespace Tutorial9.Repositories.Product;

public interface IProductRepository
{
    Task<decimal> GetProductPriceAsync(int productId);
}