
using OrderProcessing.Exceptions;
using System.Text.Json;
namespace OrderProcessing.Data;

public class JsonFileStore
{
    public async Task<List<T>> ReadAsync<T>(string path, CancellationToken ct = default)
    {
        SemaphoreSlim semaphore = new(1, 1);
        await semaphore.WaitAsync(ct);

        try
        {
            string content = await File.ReadAllTextAsync(path, ct);
            List<T> result = JsonSerializer.Deserialize<List<T>>(content, JsonConfigurations.Options) ?? new List<T>();
            return result;
        }
        catch(JsonException )
        {
            throw new DataFileCorruptedException(path);
        }
        finally
        {
            semaphore.Release();
        }



    }

    public List<T> Read<T>(string path)
    {
        SemaphoreSlim semaphore = new(1, 1);
        semaphore.Wait();
        try
        {
            string content = File.ReadAllText(path);
            List<T> result = JsonSerializer.Deserialize<List<T>>(content, JsonConfigurations.Options) ?? new List<T>();
            return result;
        }
        catch (JsonException)
        {
            throw new DataFileCorruptedException(path);
        }
        finally
        {
            semaphore.Release();
        }
    }




    public async Task WriteAsync<T>(string path, List<T> objects, CancellationToken ct = default)
    {
        SemaphoreSlim semaphore = new(1, 1);
        await semaphore.WaitAsync(ct);
        try
        {
            string json = JsonSerializer.Serialize(objects, JsonConfigurations.Options);
            await File.WriteAllTextAsync(path, json, ct);
        }
        finally
        {
            semaphore.Release();
        }
        
    }
    public void Write<T>(string path, List<T> objects)
    {
        SemaphoreSlim semaphore = new(1, 1);
        semaphore.Wait();
        try
        {
            string json = JsonSerializer.Serialize(objects, JsonConfigurations.Options);
            File.WriteAllText(path, json);
        }
        finally
        {
            semaphore.Release();
        }

    }

}
