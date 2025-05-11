using Tutorial9.Model.DTOs;
using Tutorial9.Repositories;

namespace Tutorial9.Services.Product_Warehouse;

public interface IProductWarehouseService
{

    Task<int> AddProductToWarehouse(ProductWarehouseInsertDTO dto);
    
    Task<int> ProcedureAddProductToWarehouse(ProductWarehouseInsertDTO dto);
    
}