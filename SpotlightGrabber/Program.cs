using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace SpotlightGrabber
{
    class Program
    {
        static int _savedCounter;
        static void Main(string[] args)
        {
            string destinationDirectory = string.Empty;
            int minimumImageSizeInKB = 200;
            bool changeWallpaper = true;

            if (args.Length > 0)
            {
                destinationDirectory = args[0];
            }

            if (args.Length > 1)
            {
                try
                {
                    minimumImageSizeInKB = Convert.ToInt32(args[1].Trim());
                }
                catch
                {
                    Console.WriteLine($"Invalid file size provided in second argument. Using the default minimum image size of {minimumImageSizeInKB} KB.");
                }
            }

            if (args.Length > 2)
            {
                try
                {
                    changeWallpaper = args[2].Trim().ToLowerInvariant() == "yes";
                }
                catch
                {
                    Console.WriteLine("Invalid third argument. Please specify Yes or No to enable or disable automatic wallpaper change.");
                }
            }

            if (string.IsNullOrWhiteSpace(destinationDirectory))
            {
                destinationDirectory = Path.Combine(Path.GetTempPath(), "SpotlightGrabber");
                Console.WriteLine("Since no target directory was provided, falling back to directory : " + destinationDirectory);
            }

            var largestImage = CheckFilesAndStoreImages(GetSpotlightFilesDirectory(), destinationDirectory, minimumImageSizeInKB);

            if (_savedCounter == 0)
            {
                Console.WriteLine("No new files found.");
                GetImageFromLoremPicsumAndSetAsWallpaper("https://picsum.photos/1920/1080");
            }
            else
            {
                Console.WriteLine($"{_savedCounter} new file{(_savedCounter > 1 ? "s" : String.Empty)} found and saved to {destinationDirectory} directory.");
            }

            if (!string.IsNullOrWhiteSpace(largestImage) && changeWallpaper && _savedCounter > 0)
            {
                SetImageAsWallpaper(largestImage);
                Console.WriteLine("Wallpaper changed successfully.");
            }

            Console.WriteLine("Operation completed.");


            Console.ReadLine();
        }

        private static void GetImageFromLoremPicsumAndSetAsWallpaper(string url)
        {
            try
            {
                Console.WriteLine("Since no new files were found, falling back to https://picsum.photos/");
                SetImageAsWallpaper(url);
                Console.WriteLine("Wallpaper changed successfully.");
            }
            //since this is a remote url, connection error might occur
            catch (Exception)
            {
                Console.WriteLine("unable to get resource from https://picsum.photos/");
            }
        }

        public static string GetSpotlightFilesDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="destinationDirectory"></param>
        /// <param name="minimumFileSizeInKb"></param>
        /// <returns>Path to largest file found in the search. Null if no file matches the search.</returns>
        public static string CheckFilesAndStoreImages(string sourceDirectory, string destinationDirectory, int minimumFileSizeInKb)
        {
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            string largestImagePath = string.Empty;
            int largestFileSize = 0;
            string[] filesInDestinationDirectory = Directory.GetFiles(destinationDirectory);
            _savedCounter = 0;
            foreach (var file in Directory.GetFiles(sourceDirectory))
            {
                string destinationFilePath = Path.Combine(destinationDirectory, Path.GetFileName(file) + ".jpg");

                bool alreadyExists = filesInDestinationDirectory.Contains(destinationFilePath);

                if (IsImage(file))
                {
                    var imgBytes = File.ReadAllBytes(file);
                    if ((imgBytes.Length / 1024) > minimumFileSizeInKb)
                    {
                        if (!alreadyExists)
                        {
                            File.WriteAllBytes(destinationFilePath, imgBytes);
                            _savedCounter++;
                        }

                        if (imgBytes.Length > largestFileSize)
                        {
                            largestImagePath = file;
                            largestFileSize = imgBytes.Length;
                        }
                    }
                }
            }

            return largestImagePath;
        }

        public static void SetImageAsWallpaper(string imagePath)
        {
            Wallpaper.Set(imagePath, Wallpaper.Style.Stretched);
        }

        public static bool IsImage(string imagePath)
        {
            try
            {
                using (Image img = Image.FromFile(imagePath)) { }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*
         * 
         * Service:
 Retrieve files from directory :
  C:\Users\username\AppData\Local\Packages\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\LocalState\Assets

 Check if file is an image, store in temp folder.

 Order by file size

 Make copy of largest 3

 Store to user defined directory with datetime stamp

 Set the largest as wallpaper
         * 
         * */
    }
}