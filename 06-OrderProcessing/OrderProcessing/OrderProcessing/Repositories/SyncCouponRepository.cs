
using OrderProcessing.Common;
using OrderProcessing.Data;
using OrderProcessing.models;

namespace OrderProcessing.Repositories;


public class SyncCouponRepository
{
    private readonly JsonFileStore _json = new();

    public Coupon? GetByCode(string code, CancellationToken ct = default)
    {
        string path = DataPaths.GetFilePath(DataFiles.CouponsFile);

        Log.Info("CouponRepository", "Opening connection...");
        Thread.Sleep(200);
        List<Coupon> coupons = _json.Read<Coupon>(path);

        Thread.Sleep(Random.Shared.Next(300, 600));
        foreach (var coupon in coupons)
        {
            if (coupon.Code == code)
            {
                Log.Success("CouponRepository", $"Coupon code : {code} found");
                return coupon;
            }
        }

        return null;
    }
}

