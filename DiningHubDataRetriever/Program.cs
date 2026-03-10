using System.Text;
using DiningHubDataRetriever;
using DiningHubDataRetriever.Models;

Console.WriteLine("Hello, World!");

GraphApiHandler handler = new();

Dictionary<string, List<DiningHubItem>> diningHubDictionary = [];


foreach (string diningHub in ConstantValues.DiningHubs)
{
    List<DiningHubItem>? items = await handler.GetDiningHubItems(diningHub, 10);
    diningHubDictionary[diningHub] = items ?? [];
}

StringBuilder sb = new();
sb.AppendLine();
foreach (var hubKvp in diningHubDictionary)
{
    sb.AppendLine($"{hubKvp.Key} item count: {hubKvp.Value.Count} ");
    if (hubKvp.Value.Count > 5)
    {
        sb.AppendLine("Top 5 Items:");
        for (int i = 0; i <= 5; i++)
        {
            sb.AppendLine($"{hubKvp.Value[i]}");
        }
    }
    sb.AppendLine("");
}

Console.WriteLine(sb.ToString());