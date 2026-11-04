

using OrderProcessing.Common;
using OrderProcessing.Data;
using OrderProcessing.models;

namespace OrderProcessing.Repositories;

public class OrderRepository
{
    private readonly JsonFileStore _json = new();
    public async Task SaveAsync(Order order, CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.OrdersFile);

        Log.Info("OrderRepository", "Opening connection...");
        await Task.Delay(200, ct);
        List<Order> orders = await _json.ReadAsync<Order>(path, ct);

        await Task.Delay(100, ct);
        int index = -1;
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i].Id == order.Id)
            {
                index = i;
                break;
            }
        }


        if (index != -1)
        {
            Log.Success("OrderRepository", $"Order #{order.Id} is updated");
            orders[index] = order;
        }
        else
        {
            Log.Success("OrderRepository", $"Order #{order.Id} is added");
            orders.Add(order);
        }

        await Task.Delay(400, ct);
        
        await _json.WriteAsync(path, orders, ct);

    }

    public async Task<List<Order>> GetAllAsync(CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.OrdersFile);
        Log.Info("OrderRepository", "Opening connection...");
        await Task.Delay(200, ct);

        await Task.Delay(Random.Shared.Next(300, 600), ct);
        List<Order> orders = await _json.ReadAsync<Order>(path, ct);

        Log.Success("OrderRepository", $"All Orders are retrieved");
        return orders;
    }

    public async Task<List<Order>> GetByStatusAsync(OrderStatus s, CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.OrdersFile);

        Log.Info("OrderRepository", "Opening connection...");
        await Task.Delay(200, ct);
        List<Order> orders = await _json.ReadAsync<Order>(path, ct);

        await Task.Delay(Random.Shared.Next(300, 600), ct);
        List<Order> filteredOrders = new();
        foreach (var order in orders)
        {
            if(order.Status == s)
            {
                filteredOrders.Add(order); 
            }
        }

        Log.Success("OrderRepository", $"Orders are filtered by status : {s}");
        return filteredOrders;
    }


}
