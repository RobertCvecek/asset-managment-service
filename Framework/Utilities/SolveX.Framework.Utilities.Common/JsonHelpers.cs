using Newtonsoft.Json.Linq;

namespace SolveX.Framework.Utilities.Common;
public static class JsonHelpers
{
    public static bool PropertyExistsWithValue(string jsonString, string propertyName, string expectedValue)
    {
        JObject json = JObject.Parse(jsonString);
        return PropertyExistsWithValue(json, propertyName, expectedValue);
    }

    private static bool PropertyExistsWithValue(JToken token, string propertyName, string expectedValue)
    {
        if (token.Type == JTokenType.Object)
        {
            JObject obj = (JObject)token;
            foreach (var property in obj.Properties())
            {
                if (property.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase)
                    && property.Value.ToString().Equals(expectedValue, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (PropertyExistsWithValue(property.Value, propertyName, expectedValue))
                {
                    return true;
                }
            }
        }
        else if (token.Type == JTokenType.Array)
        {
            JArray array = (JArray)token;
            foreach (var item in array.Children())
            {
                if (PropertyExistsWithValue(item, propertyName, expectedValue))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static string GetPropertyValue(string jsonString, string propertyName)
    {
        JObject json = JObject.Parse(jsonString);

        // Split the property name by ":" to handle nested properties
        string[] propertyNames = propertyName.Split(':');

        JToken propertyValue = json;
        foreach (string propName in propertyNames)
        {
            propertyValue = propertyValue[propName];

            // If any property is not found, return null
            if (propertyValue == null)
            {
                return null;
            }
        }

        return propertyValue.ToString();
    }
}

