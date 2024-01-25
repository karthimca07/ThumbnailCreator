using SkiaSharp;
using System.IO;
using System.Reflection.Metadata;

namespace ThumbnailCreator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating thumbnail for image");
            Console.WriteLine("Place the image in the applicaiton root path with the name <input>.jpg and it will create the thumbnail<timestamp>.jpg");

            //Read the file as bytes and convert to memory stream
            MemoryStream stream = new MemoryStream(File.ReadAllBytes(Environment.CurrentDirectory + "/input.jpg"));

            //To write the file to disk
            //File.WriteAllBytes(Environment.CurrentDirectory + "/Thumbnail.jpg", stream.ToArray());

            using (var skImage = SKBitmap.Decode(stream))
            {
                int newHeight = 100;
                int newWidth = 150;

                using (var scaledBitmap = skImage.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.Low))
                {
                    using (var image = SKImage.FromBitmap(scaledBitmap))
                    {
                        using (var encodedImage = image.Encode(SKEncodedImageFormat.Jpeg, 100))         //set the quality to intended value
                        {
                            var stream1 = new MemoryStream();
                            encodedImage.SaveTo(stream1);
                            stream1.Seek(0, SeekOrigin.Begin);
                            File.WriteAllBytes(Environment.CurrentDirectory + "/Thumbnail.jpg", stream1.ToArray());
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}