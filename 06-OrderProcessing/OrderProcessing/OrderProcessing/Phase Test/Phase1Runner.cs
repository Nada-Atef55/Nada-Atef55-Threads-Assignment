

using OrderProcessing.Common;
using OrderProcessing.Data;
using OrderProcessing.models;
using OrderProcessing.Repositories;
using System.Diagnostics;

namespace OrderProcessing.Phase_Test;

public class Phase1Runner
{
    private static readonly JsonFileStore _json = new();
    private static readonly OrderRepository _orderRepo = new();

    private static double _asyncTime;
    private static double _syncTime;

    public static async Task RunAsync()
    {

        // reset all files
        DataSeeder.ResetAsync().Wait();

        // load orders
        List<Order> orders = _json.Read<Order>(DataPaths.GetFilePath(DataFiles.IncomingOrdersFile));

        Stopwatch stopwatch = Stopwatch.StartNew();

        foreach (var order in orders)
        {
            SyncOrderValidator.ValidateOrder(order);
        }

        stopwatch.Stop();

        _syncTime = stopwatch.Elapsed.TotalSeconds;



        // reset
        await DataSeeder.ResetAsync();

        // load orders
        List<Order> ordersForAsync = await _json.ReadAsync<Order>(DataPaths.GetFilePath(DataFiles.IncomingOrdersFile));

        Stopwatch stopwatch2 = Stopwatch.StartNew();

        foreach (var order in ordersForAsync)
        {
            await OrderValidator.ValidateOrderAsync(order);
            await _orderRepo.SaveAsync(order);
        }

        stopwatch2.Stop();

        _asyncTime =  stopwatch2.Elapsed.TotalSeconds;

    }


    public static async Task PrintSummary()
    {
        List<Order> rejectedOrders = await _orderRepo.GetByStatusAsync(OrderStatus.Rejected);
        List<Order> validOrders = await _orderRepo.GetByStatusAsync(OrderStatus.Validated);
        List<Order> allOrders = await _orderRepo.GetAllAsync();

        Console.WriteLine("===== Validation Summary =====");
        Console.WriteLine($"Total Orders: {allOrders.Count}");
        Console.WriteLine($"Validated: {validOrders.Count}");
        Console.WriteLine($"Rejected: {rejectedOrders.Count}");

        foreach(var order in rejectedOrders)
        {
            Console.WriteLine($"\t Order #{order.Id}: {order.FailureReason}");
        }

        Console.WriteLine($"Sync Time: {_syncTime:F1} seconds");
        Console.WriteLine($"Async Time: {_asyncTime:F1} seconds");
        Console.WriteLine($"orders.json now contains {allOrders.Count} records.");

    }


}
