using System;
using System.Net.Http;
using System.Threading.Tasks;

class TestRateLimiter
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7XXX/"); // Replace XXX with your port

        Console.WriteLine("Testing Rate Limiter...\n");

        // Test API endpoint (60 requests/minute)
        Console.WriteLine("Testing WeatherForecast API (Limit: 60/min):");
        for (int i = 1; i <= 65; i++)
        {
            try
            {
                var response = await client.GetAsync("WeatherForecast");
                Console.WriteLine($"Request {i}: {(int)response.StatusCode} - {response.StatusCode}");

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Rate limit hit! Message: {content}");
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Request {i} failed: {ex.Message}");
            }

            await Task.Delay(100); // Small delay between requests
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}