using Microsoft.Data.SqlClient;
using Tutorial9.Exceptions;
using Tutorial9.Model.DTOs;
using Tutorial9.Repositories.Order;
using Tutorial9.Repositories.Product_Warehouse;
using Tutorial9.Repositories.Product;
using Tutorial9.Repositories.Warehouse;

namespace Tutorial9.Services.Product_Warehouse;

public class ProductWarehouseService : IProductWarehouseService
{
    private readonly IProductWarehouseRepository _productWarehouseRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly string? _connectionString;

    
    public ProductWarehouseService(IProductWarehouseRepository productWarehouseRepository,
        IWarehouseRepository warehouseRepository, IProductRepository productRepository,
        IOrderRepository orderRepository,
        IConfiguration configuration)
    {
        _productWarehouseRepository = productWarehouseRepository;
        _warehouseRepository = warehouseRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<int> AddProductToWarehouse(ProductWarehouseInsertDTO dto)
    {
        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        
        if (!await _warehouseRepository.WarehouseExistsAsync(dto.IdWarehouse, conn))
            throw new NotFoundException("Warehouse not found");
            
        var price = await _productRepository.GetProductPriceAsync(dto.IdProduct, conn);
        if (price == decimal.MinValue)
            throw new NotFoundException("Product not found");
            
        var orderId = await _orderRepository.FindAvailableOrderAsync(dto, conn);
        if (orderId == -1)
            throw new ConflictException("No available orders to fullfill");
        
        await using var transaction = conn.BeginTransaction();
        
        try
        {
            
            var fulfilled = await _orderRepository.FullfillOrderAsync(orderId, conn, transaction);
            if (!fulfilled)
                throw new ConflictException("Problem with fullfilling order");
            
            var resultId = await _productWarehouseRepository.AddProductToWarehouse(dto, orderId, price, conn, transaction);

            await transaction.CommitAsync();
            return resultId;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<int> ProcedureAddProductToWarehouse(ProductWarehouseInsertDTO dto)
    {
        var id = await _productWarehouseRepository.ProcedureAddProductToWarehouse(dto);

        if (id == -1)
            throw new Exception("Insertion failed via stored procedure.");

        return id;
    }
}