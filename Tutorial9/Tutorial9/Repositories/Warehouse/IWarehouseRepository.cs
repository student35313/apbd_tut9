namespace Tutorial9.Repositories;

public interface IWarehouseRepository
{
    Task<bool> WarehouseExistsAsync(int warehouseId);
}