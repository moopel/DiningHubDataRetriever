namespace DiningHubDataRetriever;

public static class ConstantValues
{
    public const string DiningHubBaseEndpoint =
        "https://api.elevate-dxp.com/api/mesh/c087f756-cc72-4649-a36f-3a41b700c519/graphql";

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
