

using OrderProcessing.Common;
using OrderProcessing.Data;
using OrderProcessing.models;

namespace OrderProcessing.Repositories;

public class CouponRepository
{
    private readonly JsonFileStore _json = new();
    public async Task<Coupon?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.CouponsFile);

        Log.Info("CouponRepository", "Opening connection...");
        await Task.Delay(200, ct);
        List<Coupon> coupons = await _json.ReadAsync<Coupon>(path, ct);

        await Task.Delay(Random.Shared.Next(300, 600), ct);
        foreach (var coupon in coupons)
        {
            if(coupon.Code == code)
            {
                Log.Success("CouponRepository", $"Coupon code : {code} found");
                return coupon;
            }
        }

        return null;
    }

}
