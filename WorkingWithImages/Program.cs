using System;
using System.IO;
using com.blogspot.jeanjmichel.util;
using System.Drawing;

namespace com.blogspot.jeanjmichel
{
    public class Program
    {
        static void Main(string[] args)
        {
            String imagesDirectoryPath = @"C:\Tests .Net\WorkingWithImages\WorkingWithImages\images\";

            String originalImageName = "Anita.jpg";
            String originalImageFullPath = imagesDirectoryPath + originalImageName;
            String originalImageExtension = ImageUtil.GetImageExtension(imagesDirectoryPath + originalImageName).Replace(".", "").ToLower();
            String originalImageOutTextFile = imagesDirectoryPath + originalImageName.Replace("." + originalImageExtension, ".txt");
            
            String resizedImageName = originalImageName.Replace("." + originalImageExtension, " - resized.jpg");
            String resizedImageFullPath = imagesDirectoryPath + resizedImageName;
            String resizedImageOutTextFile = imagesDirectoryPath + resizedImageName.Replace(".jpg", ".txt");
            
            Console.WriteLine("Generating HTML <img /> tag with embedded image data...");
            
            byte[] originalImageBytes = ImageUtil.ConvertImageToArrayOfBytes(originalImageFullPath);
            String originalImageInStringBase64 = ImageUtil.ConvertArrayOfBytesToStringBase64(originalImageBytes);
            StreamWriter txtFile = new StreamWriter(originalImageOutTextFile);
            txtFile.Write(ImageUtil.GenerateHTMLImgTag(originalImageExtension, originalImageInStringBase64));
            txtFile.Flush();
            txtFile.Dispose();
            
            Console.WriteLine("Done!");
            
            Console.WriteLine("Resizing the image to VGA resolution (640x480)...");
            
            if (ImageUtil.ResizeImageTo(originalImageFullPath, resizedImageFullPath, new Size(335, 180)))
            {
                Console.WriteLine("Done!");
                Console.WriteLine("Generating HTML <img /> tag with embedded resized image data ...");
                
                byte[] resizedImageBytes = ImageUtil.ConvertImageToArrayOfBytes(resizedImageFullPath);
                String resizedImageInStringBase64 = ImageUtil.ConvertArrayOfBytesToStringBase64(resizedImageBytes);
                StreamWriter txtFile2 = new StreamWriter(resizedImageOutTextFile);
                txtFile2.Write(ImageUtil.GenerateHTMLImgTag("jpg", resizedImageInStringBase64));
                txtFile2.Flush();
                txtFile2.Dispose();

                Console.WriteLine("Done!");
            }
            else
            {
                Console.WriteLine("The image is smaller than VGA resolution!");
            }
            
            Console.ReadKey();
        }
    }
}
