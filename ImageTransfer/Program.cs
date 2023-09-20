using System;
using System.IO;
using System.IO.Compression;
using Google.Cloud.Vision.V1;

namespace ImageTransfer;

public class ImageTransferExample
{
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: ImageTransferExample <sourceFilePath> <destinationFilePath>");
            return;
        }
        string sourceFilePath = args[0];
        string destinationFilePath = args[1];
        string compressedFilePath = destinationFilePath + ".gz";
        CompressFile(sourceFilePath, compressedFilePath);
        string encodedFilePath = destinationFilePath + ".b64";
        EncodeFile(sourceFilePath, encodedFilePath);
        string compressedEncodedFilePath = encodedFilePath + ".gz";
        CompressFile(encodedFilePath, compressedEncodedFilePath);
        string encodedCompressedFilePath = compressedFilePath + ".b64";
        EncodeFile(compressedFilePath, encodedCompressedFilePath);
    }

    private static void CompressFile(string sourceFilePath, string destinationFilePath)
    {
        using FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open);
        using FileStream destinationStream = new FileStream(destinationFilePath, FileMode.OpenOrCreate);
        using GZipStream compressionStream = new GZipStream(destinationStream, CompressionMode.Compress);
        sourceStream.CopyTo(compressionStream);
    }

    private static void DecompressFile(string sourceFilePath, string destinationFilePath)
    {
        using FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open);
        using FileStream destinationStream = new FileStream(destinationFilePath, FileMode.OpenOrCreate);
        using GZipStream compressionStream = new GZipStream(sourceStream, CompressionMode.Decompress);
        compressionStream.CopyTo(destinationStream);
    }

    private static void EncodeFile(string sourceFilePath, string destinationFilePath)
    {
        byte[] buffer = File.ReadAllBytes(sourceFilePath);
        string encoded = Convert.ToBase64String(buffer);
        File.WriteAllText(destinationFilePath, encoded);
    }

    private static void DecodeFile(string sourceFilePath, string destinationFilePath)
    {
        string encoded = File.ReadAllText(sourceFilePath);
        byte[] buffer = Convert.FromBase64String(encoded);
        File.WriteAllBytes(destinationFilePath, buffer);
    }

    private static void FooBar()
    {
        ImageAnnotatorClient client = ImageAnnotatorClient.Create();
        ImageContext context = new ImageContext();
        TextDetectionParams parameters = new TextDetectionParams();
        context.TextDetectionParams = parameters;
        
        client.DetectText(new Image(), null);
    }
}