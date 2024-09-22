using System;
using System.Collections.Generic;

class TinyUrlService
{
    private Dictionary<string, UrlEntry> shortUrlToLongUrlMap;
    private Dictionary<string, int> clickCountMap;

    public TinyUrlService()
    {
        shortUrlToLongUrlMap = new Dictionary<string, UrlEntry>();
        clickCountMap = new Dictionary<string, int>();
    }

    public void CreateShortUrl(string longUrl, string customShortUrl = null)
    {
        string shortUrl = customShortUrl ?? GenerateRandomShortUrl();

        if (shortUrlToLongUrlMap.ContainsKey(shortUrl))
        {
            Console.WriteLine("Error: Short URL already exists.");
            return;
        }

        shortUrlToLongUrlMap[shortUrl] = new UrlEntry(longUrl);
        clickCountMap[shortUrl] = 0;

        Console.WriteLine($"Short URL created: {shortUrl} -> {longUrl}");
    }

    public void DeleteShortUrl(string shortUrl)
    {
        if (shortUrlToLongUrlMap.ContainsKey(shortUrl))
        {
            shortUrlToLongUrlMap.Remove(shortUrl);
            clickCountMap.Remove(shortUrl);
            Console.WriteLine($"Short URL deleted: {shortUrl}");
        }
        else
        {
            Console.WriteLine("Error: Short URL not found.");
        }
    }

    public string GetLongUrl(string shortUrl)
    {
        if (shortUrlToLongUrlMap.ContainsKey(shortUrl))
        {
            clickCountMap[shortUrl]++;
            return shortUrlToLongUrlMap[shortUrl].LongUrl;
        }

        return null;
    }

    public int GetClickCount(string shortUrl)
    {
        return clickCountMap.ContainsKey(shortUrl) ? clickCountMap[shortUrl] : 0;
    }

    private string GenerateRandomShortUrl()
    {
        // This is a simple example; you might want to implement a more robust algorithm for generating short URLs
        return Guid.NewGuid().ToString().Substring(0, 8);
    }
}

class UrlEntry
{
    public string LongUrl { get; }

    public UrlEntry(string longUrl)
    {
        LongUrl = longUrl;
    }
}

class Program
{
    static void Main()
    {
        TinyUrlService tinyUrlService = new TinyUrlService();

        tinyUrlService.CreateShortUrl("https://something.com");
        tinyUrlService.CreateShortUrl("google.com", "custom123");
        tinyUrlService.DeleteShortUrl("google.com");

        string shortUrl = "custom123";
        Console.WriteLine($"Long URL for {shortUrl}: {tinyUrlService.GetLongUrl(shortUrl)}");
        Console.WriteLine($"Click count for {shortUrl}: {tinyUrlService.GetClickCount(shortUrl)}");
    }
}