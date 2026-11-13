using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Globalization;

public static class SetsAndMaps
{
    /// <summary>
    /// Find all symmetric pairs of two-character words using sets in O(n).
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var wordSet = new HashSet<string>(words);
        var pairs = new List<string>();

        foreach (var word in words)
        {
            // Skip words that have already been processed.
            if (!wordSet.Remove(word))
            {
                continue;
            }

            // Reverse the two-character word.
            string reverse = $"{word[1]}{word[0]}";

            // Find and remove its matching reverse.
            if (word != reverse && wordSet.Remove(reverse))
            {
                pairs.Add($"{word} & {reverse}");
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees earned.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");

            if (fields.Length < 4)
            {
                continue;
            }

            string degree = fields[3].Trim().Trim('"');

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if two words are anagrams using a dictionary.
    /// Ignore spaces and letter case.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        var letterCounts = new Dictionary<char, int>();

        foreach (char letter in word1.ToUpperInvariant())
        {
            if (char.IsWhiteSpace(letter))
            {
                continue;
            }

            if (letterCounts.ContainsKey(letter))
            {
                letterCounts[letter]++;
            }
            else
            {
                letterCounts[letter] = 1;
            }
        }

        foreach (char letter in word2.ToUpperInvariant())
        {
            if (char.IsWhiteSpace(letter))
            {
                continue;
            }

            if (!letterCounts.ContainsKey(letter))
            {
                return false;
            }

            letterCounts[letter]--;

            if (letterCounts[letter] == 0)
            {
                letterCounts.Remove(letter);
            }
        }

        return letterCounts.Count == 0;
    }

    /// <summary>
    /// Retrieve daily earthquake information from USGS.
    /// Return earthquake locations and magnitudes.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();

        using var response = client.GetAsync(uri).GetAwaiter().GetResult();

        response.EnsureSuccessStatusCode();

        string json = response.Content.ReadAsStringAsync()
            .GetAwaiter().GetResult();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var featureCollection =
            JsonSerializer.Deserialize<FeatureCollection>(json, options);

        if (featureCollection?.Features == null)
        {
            return [];
        }

        var earthquakes = new List<string>();

        foreach (var feature in featureCollection.Features)
        {
            if (feature.Properties == null ||
                feature.Properties.Place == null ||
                feature.Properties.Mag == null)
            {
                continue;
            }

            string place = feature.Properties.Place;

            string magnitude = feature.Properties.Mag.Value
                .ToString("0.##", CultureInfo.InvariantCulture);

            earthquakes.Add($"{place} - Mag {magnitude}");
        }

        return earthquakes.ToArray();
    }
}