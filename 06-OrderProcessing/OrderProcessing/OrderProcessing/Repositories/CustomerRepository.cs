using OrderProcessing.Common;
using OrderProcessing.Data;
using OrderProcessing.models;


namespace OrderProcessing.Repositories;

public class CustomerRepository
{
    private readonly JsonFileStore _json = new();
    public async Task<Customer?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        
        string path = DataPaths.GetFilePath(DataFiles.CustomersFile);
        Log.Info("CustomerRepository", "Opening connection...");
        await Task.Delay(200, ct);
        List<Customer> customers = await _json.ReadAsync<Customer>(path, ct);

        Log.Info("CustomerRepository", $"Executing query for customer #{id}");
        await Task.Delay(Random.Shared.Next(300, 600), ct);
        foreach(var customer in customers)
        {
            if(customer.Id == id)
            {
                Log.Success("CustomerRepository", $"Customer #{id} found ({customer.Name})");
                return customer;
            }
        }

        return null;
    }




}
