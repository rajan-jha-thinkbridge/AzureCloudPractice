internal class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();
        var functionUrl = "http://localhost:7071/api/MyHttpTrigger";

        Console.WriteLine("Enter your name:");
        var name = Console.ReadLine();

        var response = await httpClient.GetAsync($"{functionUrl}?name={name}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine($"Failed to call the function. Status code: {response.StatusCode}");
        }

        httpClient.Dispose();
    }
}