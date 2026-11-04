
using OrderProcessing.Common;
using OrderProcessing.Data;
using OrderProcessing.models;

namespace OrderProcessing.Repositories;


public class SyncProductRepository
{
    private readonly JsonFileStore _json = new();

    public Product? GetById(int id, CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.ProductsFile);

        Log.Info("ProductRepository", "Opening connection...");
        Thread.Sleep(200);
        List<Product> products = _json.Read<Product>(path);

        Log.Info("ProductRepository", $"Executing query for product #{id}");
        Thread.Sleep(Random.Shared.Next(300, 600));
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

    public bool ReserveStock(int productId, int qty, CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.ProductsFile);

        Log.Info("ProductRepository", "Opening connection...");
        Thread.Sleep(200);

        List<Product> products = _json.Read<Product>(path);

        Thread.Sleep(100);
        foreach (var product in products)
        {
            if (product.Id == productId && product.StockQuantity >= qty)
            {
                Log.Success("ProductRepository", $"Quantitiy : {qty} is reserved from {product.Name}");
                product.StockQuantity -= qty;

                // update json file
                Thread.Sleep(400);
                _json.Write(path, products);

                return true;
            }
        }

        return false;
    }
}

