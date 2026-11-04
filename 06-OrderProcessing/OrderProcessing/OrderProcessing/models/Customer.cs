using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OrderProcessing.models;

public enum LoyaltyTier
{
    Bronze,
    Silver,
    Gold
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email => $"{Name.Split()[0].ToLower()}.{Name.Split()[1].ToLower()}@example.com";

    [JsonPropertyName("Active")]
    public bool IsActive { get; set; }

    [JsonPropertyName("Tier")]
    public LoyaltyTier LoyaltyTier { get; set; }
}
