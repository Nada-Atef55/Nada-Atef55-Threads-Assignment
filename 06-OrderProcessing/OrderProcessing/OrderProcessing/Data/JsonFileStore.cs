
namespace OrderProcessing.Data;

public class JsonFileStore
{
    public async Task<string[]> ReadAsync(string path, CancellationToken ct = default) 
        => await File.ReadAllLinesAsync(path, ct);
    public string[] Read(string path) 
        => File.ReadAllLines(path);



    public async Task WriteAsync(string path, string text, CancellationToken ct = default)
        => await File.AppendAllTextAsync(path, $"\n{text}", ct);
    public void Write(string path, string text)
        => File.AppendAllText(path, $"\n{text}");

}
