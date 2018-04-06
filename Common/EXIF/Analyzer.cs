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
            PropertyItem prI;
            string returnProp;
            prI = img.GetPropertyItem(property);
            switch (property)
            {
                case 34850:
                    returnProp = Exposure.GetExposureDescription(prI);
                    break;
                default:
                    returnProp = ParseProperty(prI.Type, prI);
                    break;
            }
            return returnProp;
        }

        public static ProcessedPhoto ProcesImage(string filePath)
        {
            Image img = Image.FromFile(filePath);
            string filename = GetFilename(filePath);
            ProcessedPhoto processedPhoto = new ProcessedPhoto();
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                int exifCode = Convert.ToInt32(exifProperty);
                string propertyValue = ParseExifTag(exifCode, img);
                processedPhoto.AddProperty(exifCode, propertyValue);
            }
            return processedPhoto;
        }

        private static string GetFilename(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            return fi.Name;
        }

        private static string ParseProperty(int ID, PropertyItem propCorr)
        {
            int temp;
            StringBuilder stBuild = new StringBuilder("");
            switch (ID)
            {
                case 2:
                    {
                        stBuild = new StringBuilder("");
                        for (temp = 0; temp < propCorr.Len; temp++)
                        {
                            stBuild.Append(Convert.ToChar(propCorr.Value[temp]));
                        }
                        return stBuild.ToString();
                    }
                case 5:
                    {
                        int i = 0;
                        int enumerator = 0;
                        int denominator = 0;
                        double result;
                        for (temp = 0; temp < propCorr.Len; temp++)
                        {
                            if (propCorr.Value[temp] != 0)
                            {
                                if (i == 0)
                                    enumerator = propCorr.Value[temp];
                                if (i == 1)
                                    denominator = propCorr.Value[temp];
                                i++;
                            }
                        }
                        result = (double)enumerator / denominator;
                        return result.ToString();
                    }
                case 3:
                    {
                        stBuild = new StringBuilder("");
                        for (temp = 0; temp < propCorr.Len; temp++)
                        {
                            if (propCorr.Value[temp] != 0)
                            {
                                stBuild.Append(propCorr.Value[temp].ToString());
                            }
                        }
                        return stBuild.ToString();
                    }
                default:
                    return "";

            }
        }
    }
}
