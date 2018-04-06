using ExifAnalyzer.Common.Localisation;
using System;
using System.Drawing.Imaging;

namespace ExifAnalyzer.Common.EXIF
{
    public static class Exposure
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
    }
}
