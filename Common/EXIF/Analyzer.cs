using ExifAnalyzer.Common.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace ExifAnalyzer.Common.EXIF
{
    public static class Analyzer
    {
        private static string ParseExifTag(int property, Image img)
        {
            try
            {

                PropertyItem propertiItem;
                string returnProp;
                propertiItem = img.GetPropertyItem(property);
                switch (property)
                {
                    case 34850:
                        returnProp = PropertyHelper.GetExposureDescription(propertiItem);
                        break;
                    case 33434:
                        returnProp = PropertyHelper.GetExposureTime(propertiItem);
                        break;
                    default:
                        returnProp = PropertyHelper.GetProperty(propertiItem).Replace("\0", "");
                        break;
                }
                return returnProp;
            }
            catch (Exception)
            {
                return "Not Found";
            }
        }

        public static ProcessedPhoto ProcesImage(string filePath, bool filterNullValues)
        {
            Image img = Image.FromFile(filePath);
            string filename = GetFilename(filePath);
            ProcessedPhoto processedPhoto = new ProcessedPhoto();
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                int exifCode = Convert.ToInt32(exifProperty);
                string propertyValue = ParseExifTag(exifCode, img);
                if(filterNullValues && propertyValue== "Not Found")
                {
                    continue;
                }
                processedPhoto.AddProperty(exifCode, propertyValue);
            }
            return processedPhoto;
        }

        private static string GetFilename(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            return fi.Name;
        }


    }
}
