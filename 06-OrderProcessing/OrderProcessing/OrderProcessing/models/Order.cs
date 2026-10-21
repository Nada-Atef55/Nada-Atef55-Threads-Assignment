using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.models;

public enum OrderStatus { 
    Pending, 
    Validated, 
    Rejected, 
    PaymentFailed, 
    Completed, 
    Failed 
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }

    public List<OrderItem> Items { get; set; } = [];

    public string? CouponCode { get; set; }

    public decimal SubTotal { get; set; }
    public decimal Discount { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string? FailureReason { get; set; } = null;
    public string? TransactionId { get; set; }
}
