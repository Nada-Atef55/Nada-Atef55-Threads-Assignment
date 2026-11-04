

using OrderProcessing.Common;
using OrderProcessing.Data;
using OrderProcessing.models;

namespace OrderProcessing.Repositories;

public class IncomingOrderRepository
{
    private readonly JsonFileStore _json = new();
    public async Task<List<Order>> GetAllAsync(CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.IncomingOrdersFile);
        Log.Info("IncomingOrderRepository", "Opening connection...");
        await Task.Delay(200, ct);

        await Task.Delay(Random.Shared.Next(300, 600), ct);
        List<Order> incommingOrders = await _json.ReadAsync<Order>(path, ct);

        Log.Success("IncomingOrderRepository", $"All incoming_orders are retrieved");
        return incommingOrders;
    }
}
