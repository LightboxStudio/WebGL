using System;
using System.Collections.Generic;
using UnityEngine;
public static class QueryParameters
{
    [Header("Data")]
    public static Dictionary<string, string> values = new Dictionary<string, string>();
    public static void CollectQueryParameters()
    {
        string textureUrl = Application.absoluteURL;

        // Check if the URL contains any query parameters
        if (textureUrl.Contains("?"))
        {
            // Parse the URL using System.Uri class
            Uri uri = new Uri(textureUrl);

            // Get the query parameters as a NameValueCollection
            System.Collections.Specialized.NameValueCollection queryParams =
                System.Web.HttpUtility.ParseQueryString(uri.Query);

            // Iterate through each parameter and add it to the dictionary
            foreach (string key in queryParams.AllKeys)
            {
                string value = queryParams.Get(key);
                values[key] = value;
            }
        }
    }

    public static string GetBaseUrl()
    {
        try
        {
            Uri uri = new Uri(Application.absoluteURL);
            string baseUrl = uri.GetLeftPart(UriPartial.Authority);
            return baseUrl;
        }
        catch (UriFormatException ex)
        {
            Debug.LogError("Invalid URL format: " + ex.Message);
            return null;
        }
    }
}