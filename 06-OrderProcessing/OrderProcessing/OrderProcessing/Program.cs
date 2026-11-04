using OrderProcessing.Phase_Test;

namespace OrderProcessing;

internal class Program
{
    static async Task Main(string[] args)
    {
        await Phase1Runner.RunAsync();
        await Phase1Runner.PrintSummary();


        

    }
}
