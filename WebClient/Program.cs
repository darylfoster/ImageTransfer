using System.IO.Compression;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WebClient.Models;

namespace WebClient;

public static class ImageTransferWebClient
{
    public static async Task Main(string[] args)
    {
        string imageFile = args[0];
        string url = args[1];
        var client = new HttpClient();
        // var response = await client.GetAsync(url);
        // Console.WriteLine(await response.Content.ReadAsStringAsync());
        var image = new SheetImage
        {
            ImageFormat = Path.GetExtension(imageFile).Replace(".", ""),
            EncodedImage = Convert.ToBase64String(await File.ReadAllBytesAsync(imageFile))
        };
        // var httpContent = JsonContent.Create(image);
        // var httpContent = new StringContent(JsonSerializer.Serialize(image));
        // httpContent.Headers.ContentEncoding.Add("gzip");
        var httpContent = CompressRequestContent(image);
        var postResponse = await client.PostAsync(url, httpContent);
        Console.WriteLine(await postResponse.Content.ReadAsStringAsync());
    }

    private static HttpContent CompressRequestContent(SheetImage image)
    {
        var compressedStream = new MemoryStream();
        using (var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(image))))
        {
            using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                contentStream.CopyTo(gzipStream);
            }
        }

        var httpContent = new ByteArrayContent(compressedStream.ToArray());
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        httpContent.Headers.ContentEncoding.Add("gzip");
        return httpContent;
    }
}