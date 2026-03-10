using System;
using System.Collections.Generic;
using System.Text;

namespace DiningHubDataRetriever.Models;

public class DiningHubItem
{
    public required string Name { get; init; }
    public required string SkuId { get; init; }

    public required string Ingredients { get; init; }
    public required string Allergens { get; init; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine($"Item[{Name}] - SkuId[{SkuId}]");
        sb.AppendLine($"Ingredients[{Ingredients}]");
        sb.AppendLine($"Allergens[{Allergens}]");

        return sb.ToString();
    }
}
