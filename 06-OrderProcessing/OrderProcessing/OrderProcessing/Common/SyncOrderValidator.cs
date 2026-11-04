using OrderProcessing.models;
using OrderProcessing.Repositories;


namespace OrderProcessing.Common;


public class SyncOrderValidator
{
    private static readonly SyncCustomerRepository _customerRepository = new();
    private static readonly SyncProductRepository _productRepository = new();
    private static readonly SyncCouponRepository _couponRepository = new();

    public static bool ValidateOrder(Order order)
    {
        Customer? customer = _customerRepository.GetById(order.CustomerId);

        if (customer == null)
        {
            order.FailureReason = $"Customer #{order.CustomerId} not found";
            return false;
        }

        if (!customer.IsActive)
        {
            order.FailureReason = "Customer is inactive";
            return false;
        }

        foreach (var item in order.Items)
        {
            Product? product = _productRepository.GetById(item.ProductId);

            if (product?.StockQuantity == 0)
            {
                order.FailureReason = $"Product #{product.Id} not found";
                return false;
            }

            if (product?.StockQuantity < item.Quantity)
            {
                order.FailureReason = $"Insufficient stock for '{product.Name}'";
                return false;
            }
        }

        if (order.CouponCode != null)
        {
            Coupon? coupon = _couponRepository.GetByCode(order.CouponCode);
            if (coupon == null)
            {
                order.FailureReason = $"Coupon '{order.CouponCode}' does not exist";
                return false;
            }

            if (coupon.ExpiryDate >= DateTime.Today)
            {
                order.FailureReason = $"Coupon '{order.CouponCode}' has expired";
                return false;
            }
        }

        return true;
    }
}

