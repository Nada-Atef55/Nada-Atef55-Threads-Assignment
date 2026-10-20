
namespace OrderProcessing.Data;

public static class DataPaths
{
    public static readonly string DataDirectory = Path.Combine(AppContext.BaseDirectory, "Data");

    public static string GetFilePAth(string fileName) => Path.Combine(DataDirectory, fileName);
}
