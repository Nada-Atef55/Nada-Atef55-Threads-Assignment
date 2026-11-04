
using System.Diagnostics;

namespace InvoiceExport;

public sealed class InvoiceExportService
{
    readonly OrderDatabase _db;
    readonly CustomerFileStore _customers;
    readonly InvoiceFileWriter _invoices;

    public InvoiceExportService(OrderDatabase db, CustomerFileStore customers, InvoiceFileWriter invoices)
    {
        _db = db;
        _customers = customers;
        _invoices = invoices;
    }

    public async Task ExportAllPending()
    {
        var watch = Stopwatch.StartNew();
        List<Order> orders = await _db.GetPendingOrders(); // 800 ms

        foreach (Order order in orders)
        {
            Order fresh = await _db.GetOrderById(order.Id) // 400 ms
                ?? throw new InvalidOperationException($"Order {order.Id} missing");

            Task<CustomerProfile> customerTask =  _customers.ReadProfile(fresh.CustomerId);
            CustomerProfile customer = await customerTask; // 300 ms

            Task<string> pathTask = _invoices.WriteInvoice(fresh, customer);
            string path = await pathTask; // 300 ms

            Console.WriteLine($"Wrote {path}");
        }

        Console.WriteLine($"Export finished in {watch.Elapsed.TotalSeconds:F1} s");
    }
}
