using System.ComponentModel.DataAnnotations;

namespace Tutorial9.Model.DTOs;

public class ProductWarehouseInsertDTO
{
    [Required]
    public int? IdProduct { get; set; }
    [Required]
    public int? IdWarehouse { get; set; }
    [Required]
    public int? Amount { get; set; }
    [Required, DataType(DataType.Date)]
    public DateTime? CreatedAt { get; set; }
    
    
}