using ExifAnalyzer.Common.Localisation;
using System;
using System.Drawing.Imaging;

namespace ExifAnalyzer.Common.EXIF
{
    public static class PropertyHelper
    {
        public static string GetExposureDescription(PropertyItem propertyItem)
        {
            int value = GetCurrentExposureValue(propertyItem);
            switch (value)
            {
                case 0:
                    return Resources.ExposureNotDefined;
                case 1:
                    return Resources.ExposureManual;
                case 2:
                    return Resources.ExposureNormalProgram;
                case 3:
                    return Resources.ExposureAperturePriority;
                case 4:
                    return Resources.ExposureShutterPriority;
                case 5:
                    return Resources.ExposureCreativeProgram;
                case 6:
                    return Resources.ExposureActionProgram;
                case 7:
                    return Resources.ExposurePortraitMode;
                case 8:
                    return Resources.ExposureLandscapeMode;
                default:
                    return Resources.ExposureNotDefined;
            }


        }

        private static int GetCurrentExposureValue(PropertyItem propertyItem)
        {
            int value = -1;
            for (int temp = 0; temp < propertyItem.Len; temp++)
            {
                if (propertyItem.Value[temp] != 0)
                {
                    value = propertyItem.Value[temp];
                }
            }
            return value;
        }

        public static string GetExposureTime(PropertyItem propertyItem)
        {
            var enumerator = BitConverter.ToUInt32(propertyItem.Value, 0);
            var denominator = BitConverter.ToUInt32(propertyItem.Value, 4);
            var value = "";
            if (enumerator > 1)
            {
                value = ((double)enumerator / (double)denominator).ToString();
            }
            else
            {
                value = string.Format("{0}/{1}",enumerator,denominator);
            }
            return value + " sec.";
        }

        public static string GetProperty(PropertyItem propertyItem)
        {
            switch(propertyItem.Type)
            {
                case 2:
                    {
                        return System.Text.Encoding.ASCII.GetString(propertyItem.Value);
                    }
                case 3:
                    {
                        return BitConverter.ToUInt16(propertyItem.Value, 0).ToString();
                    }
                case 5:
                    {

                        int i = 0;
                        UInt32 enumerator = BitConverter.ToUInt32(propertyItem.Value, 0);
                        UInt32 denominator = BitConverter.ToUInt32(propertyItem.Value, 4);
                        double result;
                        result = (double)enumerator / denominator;
                        return result.ToString();
                    }

                default:
                    return "";

            }
        }
    }
}
