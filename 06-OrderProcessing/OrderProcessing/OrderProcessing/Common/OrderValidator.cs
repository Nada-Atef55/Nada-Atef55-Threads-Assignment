
using OrderProcessing.Data;
using OrderProcessing.models;
using OrderProcessing.Repositories;


namespace OrderProcessing.Common;

public class OrderValidator
{
    public readonly JsonFileStore _json = new();
    private static readonly CustomerRepository _customerRepository = new();
    private static readonly ProductRepository _productRepository = new();
    private static readonly CouponRepository _couponRepository = new();
    private static readonly OrderRepository _orderRepository = new();

    public static async Task<bool> ValidateOrderAsync(Order order)
    {
        Customer? customer = await _customerRepository.GetByIdAsync(order.CustomerId);


        if (customer == null)
        {
            order.FailureReason = $"Customer #{order.CustomerId} not found";
            order.Status = OrderStatus.Rejected;

            //await _orderRepository.SaveAsync(order);

            return false;
        }

        if (!customer.IsActive)
        {
            order.FailureReason = "Customer is inactive";
            order.Status = OrderStatus.Rejected;

            //await _orderRepository.SaveAsync(order);

            return false;
        }

        foreach(var item in order.Items)
        {
            Product? product = await _productRepository.GetByIdAsync(item.ProductId);

            if (product?.StockQuantity == 0)
            {
                order.FailureReason = $"Product #{product.Id} not found";
                order.Status = OrderStatus.Rejected;

                //await _orderRepository.SaveAsync(order);

                return false;
            }

            if(product?.StockQuantity < item.Quantity)
            {
                order.FailureReason = $"Insufficient stock for '{product.Name}'";
                order.Status = OrderStatus.Rejected;

                //await _orderRepository.SaveAsync(order);

                return false;
            }
        }


        if(order.CouponCode != null)
        {
            Coupon? coupon = await _couponRepository.GetByCodeAsync(order.CouponCode);
            if(coupon == null)
            {
                order.FailureReason = $"Coupon '{order.CouponCode}' does not exist";
                order.Status = OrderStatus.Rejected;

                //await _orderRepository.SaveAsync(order);

                return false;
            }

            if (coupon.ExpiryDate < DateTime.Today)
            {
                order.FailureReason = $"Coupon '{order.CouponCode}' has expired";
                order.Status = OrderStatus.Rejected;

                //await _orderRepository.SaveAsync(order);

                return false;
            }
        }

        // save if the order is valid and the doesn't execute any return 
        //await _orderRepository.SaveAsync(order);

        order.Status = OrderStatus.Validated;


        return true;
    }

}
