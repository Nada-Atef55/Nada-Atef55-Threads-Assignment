
using System.Collections.Concurrent;

namespace ViewsAndEmails;

public sealed class AnalyticsAndEmailJobs
{
    public readonly ConcurrentDictionary<string, int> Views = new();
    public readonly ConcurrentQueue<string> EmailJobs = new();
    public readonly List<string> SentEmails = new();

    readonly object _sentSync = new();
    public volatile bool ProducersDone;

    public void RecordView(string product)
    {
        // no lost updates, no exceptions
        Views.AddOrUpdate(product, 1, (_, currnetValue) => currnetValue + 1); 
    }

    public void PlaceOrder(int orderId)
    {
        EmailJobs.Enqueue($"Email: order {orderId}"); // no overwrite 
    }

    public void EmailWorker()
    {
        while (true)
        {
            try
            {
                string? job = null;
                if (EmailJobs.Count > 0)
                    EmailJobs.TryDequeue(out job);// no exceptions 

                if (job is not null)
                {
                    lock (_sentSync)
                        SentEmails.Add(job);
                    continue;
                }

                if (ProducersDone && EmailJobs.Count == 0)
                    return;

                Thread.Sleep(1);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
                if (ProducersDone)
                    return;
            }
        }
    }
}
