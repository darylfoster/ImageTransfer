using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.RequestDecompression;
using Microsoft.Extensions.Logging;

namespace WebHost.Providers;

public class GZipDecompressionProvider : IDecompressionProvider
{
    private readonly ILogger<GZipDecompressionProvider> _logger;

    public GZipDecompressionProvider(ILogger<GZipDecompressionProvider> logger)
    {
        _logger = logger;
    }

    public Stream GetDecompressionStream(Stream stream)
    {
        _logger.LogInformation("Decompressing stream");
        return new GZipStream(stream, CompressionMode.Decompress);
    }
}