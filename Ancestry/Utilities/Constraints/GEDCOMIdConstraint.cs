using System.Text.RegularExpressions;

namespace Ancestry.Utilities.Constraints
{
    public class GEDCOMIdConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var value) && value != null)
            {
                string id = value.ToString();

                // Check and transform the ID to the GEDCOM format (@I...@)
                if (Regex.IsMatch(id, @"^I\d+$"))
                {
                    values[routeKey] = $"@I{id}@";
                    return true;
                }
            }

            return false;
        }
    }
}
