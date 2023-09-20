using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebHost.Models;

namespace WebHost.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageTransferController : ControllerBase
{
    private const int OneHundredMegbytes = 104857600;

    private readonly ILogger<ImageTransferController> _logger;

    public ImageTransferController(ILogger<ImageTransferController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetHelloWorld")]
    public string Get()
    {
        _logger.LogInformation("Called Hello World endpoint");
        return "Hello World\n";
    }

    [HttpPost(Name = "PostFile")]
    [RequestSizeLimit(OneHundredMegbytes)]
    public string Post(SheetImage sheetImage)
    {
        if (sheetImage == null || string.IsNullOrEmpty(sheetImage.EncodedImage))
        {
            return "Failed\n";
        }
        _logger.LogInformation("Called PostFile endpoint");
        byte[] decodedImage = Convert.FromBase64String(sheetImage.EncodedImage);
        using FileStream uploadStream =
            new FileStream($"uploads/{Guid.NewGuid().ToString()}.{sheetImage.ImageFormat}",
                FileMode.Create);
        uploadStream.Write(decodedImage);
        return "Success\n";
    }
}