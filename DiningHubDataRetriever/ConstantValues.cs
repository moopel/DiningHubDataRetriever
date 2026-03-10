namespace DiningHubDataRetriever;

public static class ConstantValues
{
    public const string DiningHubBaseEndpoint =
        "https://api.elevate-dxp.com/api/mesh/c087f756-cc72-4649-a36f-3a41b700c519/graphql";

    public static readonly List<string> DiningHubs = ["community-hub", "the-dish-at-mcalister", "schilletter-dining-hall"];

    public const string MenuItemQuery = @"
    query getLocationRecipes(
      $campusUrlKey:String!,
      $locationUrlKey:String!,
      $date:String!,
      $mealPeriod:Int,
      $viewType:Commerce_MenuViewType!
    ){
      getLocationRecipes(
        campusUrlKey:$campusUrlKey
        locationUrlKey:$locationUrlKey
        date:$date
        mealPeriod:$mealPeriod
        viewType:$viewType
      ){
        locationRecipesMap {
          skus
        }
        products {
          items {
            name
            sku
            attributes {
              name
              value
            }
          }
        }
      }
    }
    ";
}
