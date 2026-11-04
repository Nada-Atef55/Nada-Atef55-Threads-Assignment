
using OrderProcessing.Common;
using OrderProcessing.Data;
using OrderProcessing.models;

namespace OrderProcessing.Repositories;

public class ProductRepository
{
    private readonly JsonFileStore _json = new();
    public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
    {

        string path = DataPaths.GetFilePath(DataFiles.ProductsFile);
        Log.Info("ProductRepository", "Opening connection...");
        await Task.Delay(200, ct);
        List<Product> products = await _json.ReadAsync<Product>(path, ct);

        Log.Info("ProductRepository", $"Executing query for product #{id}");
        await Task.Delay(Random.Shared.Next(300, 600), ct);
        foreach (var product in products)
        {
            if (product.Id == id)
            {
                Log.Success("ProductRepository", $"Product #{id} found ({product.Name})");
                return product;
            }
        }

        return null;
    }


    public async Task<bool> ReserveStockAsync(int productId, int qty, CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.ProductsFile);

        Log.Info("ProductRepository", "Opening connection...");
        await Task.Delay(200, ct);

        List<Product> products = await _json.ReadAsync<Product>(path, ct);

        await Task.Delay(100, ct);
        foreach(var product in products)
        {
            if (product.Id == productId && product.StockQuantity >= qty)
            {
                Log.Success("ProductRepository", $"Quantitiy : {qty} is reserved from {product.Name}");
                product.StockQuantity -= qty;

                // update json file
                await Task.Delay(400, ct);
                await _json.WriteAsync(path, products, ct);

                return true;
            }
        }


        return false;
    }
}
