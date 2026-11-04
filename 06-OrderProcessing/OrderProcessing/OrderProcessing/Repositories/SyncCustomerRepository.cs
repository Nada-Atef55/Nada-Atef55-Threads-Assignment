
using OrderProcessing.Common;
using OrderProcessing.Data;
using OrderProcessing.models;

namespace OrderProcessing.Repositories;


public class SyncCustomerRepository
{
    private readonly JsonFileStore _json = new();

    public Customer? GetById(int id, CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.CustomersFile);

        Log.Info("CustomerRepository", "Opening connection...");
        Thread.Sleep(200);
        List<Customer> customers = _json.Read<Customer>(path);

        Log.Info("CustomerRepository", $"Executing query for customer #{id}");
        Thread.Sleep(Random.Shared.Next(300, 600));
        foreach (var customer in customers)
        {
            if (customer.Id == id)
            {
                Log.Success("CustomerRepository", $"Customer #{id} found ({customer.Name})");
                return customer;
            }
        }

        return null;
    }
}

