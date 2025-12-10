using System.ComponentModel.DataAnnotations;

namespace IotXBigData.Shared.Models;

public class OrdersDTO
{
    [Required] public string order_id { get; set; }
    [Required] public string customer_id { get; set; }
    [Required] public double ammount { get; set; }
    [Required] public DateTime order_ts { get; set; }
}
