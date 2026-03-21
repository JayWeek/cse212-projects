using System.Text.Json;

// Represents the whole JSON response from the USGS feed
public class FeatureCollection
{
    // The collection of earthquake entries
    public List<Feature> Features { get; set; } = new();
}

// Represents a single earthquake entry
public class Feature
{
    // Holds the details we care about for each earthquake
    public FeatureDetails Properties { get; set; } = new();
}

// Represents the "properties" section inside each feature
public class FeatureDetails
{
    // Magnitude can sometimes be null, so make it nullable
    public decimal? Mag { get; set; }

    // Location description
    public string Place { get; set; } = string.Empty;
}

public static class EarthquakeData
{
    public static string[] EarthquakeDailySummary()
    {
        const string feedUrl = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var http = new HttpClient();

        // Download the raw JSON text from the earthquake feed
        var rawJson = http.GetStringAsync(feedUrl).Result;

        // Allow matching JSON names even if casing differs
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Convert JSON into C# objects
        var quakeData = JsonSerializer.Deserialize<FeatureCollection>(rawJson, jsonOptions);

        // If something went wrong or no data came back, return an empty array
        if (quakeData == null || quakeData.Features == null || quakeData.Features.Count == 0)
        {
            return Array.Empty<string>();
        }

        var summaryLines = new List<string>();

        foreach (var quake in quakeData.Features)
        {
            // Skip bad or incomplete entries safely
            if (quake?.Properties == null)
            {
                continue;
            }

            var locationText = string.IsNullOrWhiteSpace(quake.Properties.Place)
                ? "Unknown location"
                : quake.Properties.Place;

            var magnitudeText = quake.Properties.Mag.HasValue
                ? quake.Properties.Mag.Value.ToString()
                : "Unknown";

            // Build the final display string
            summaryLines.Add($"{locationText} - Mag {magnitudeText}");
        }

        return summaryLines.ToArray();
    }
}