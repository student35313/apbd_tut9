namespace Tutorial9.Repositories.Warehouse;

public interface IWarehouseRepository
{
    Task<bool> WarehouseExistsAsync(int warehouseId);
}