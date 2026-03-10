using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiningHubDataRetriever.Models;

public class GraphMenuResponse
{
    [JsonPropertyName("data")]
    public GraphMenuData? Data { get; set; }
}

public class GraphMenuData
{
    [JsonPropertyName("getLocationRecipes")]
    public GraphMenuRecipes? Recipes { get; set; }
}

public class GraphMenuRecipes
{
    [JsonPropertyName("locationRecipesMap")]
    public GraphMenuSkuMap? SkuMap { get; set; }

    [JsonPropertyName("products")]
    public GraphMenuProducts? Products { get; set; }
}

public class GraphMenuSkuMap
{
    [JsonPropertyName("skus")]
    public List<string>? Skus { get; set; }
}

public class GraphMenuProducts
{
    [JsonPropertyName("items")]
    public List<GraphMenuItem>? Items { get; set; }
}

public class GraphMenuItem
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    [JsonPropertyName("attributes")]
    public List<GraphMenuAttribute>? Attributes { get; set; }
}

public class GraphMenuAttribute
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    public JsonElement Value { get; set; }
}
