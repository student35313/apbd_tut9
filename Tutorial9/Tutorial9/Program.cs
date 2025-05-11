
using Tutorial9.Repositories.Order;
using Tutorial9.Repositories.Product_Warehouse;
using Tutorial9.Repositories.Product;
using Tutorial9.Repositories.Warehouse;

using Tutorial9.Services.Product_Warehouse;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductWarehouseRepository, ProductWarehouseRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductWarehouseService, ProductWarehouseService>();


builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();