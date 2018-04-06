using System.Collections.Generic;
using System.ComponentModel;

namespace ExifAnalyzer.Common.EXIF
{
    public enum ExifProperties
    {
        [Description("Camera model")]
        CameraModel = 0x0110,

        [Description("Exposure program")]
        ExposureProgram = 0x8822
    }

}
