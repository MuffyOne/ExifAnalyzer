using System.Collections.Generic;
using System.ComponentModel;

namespace ExifAnalyzer.Common.EXIF
{
    public enum ExifProperties
    {
        [Description("Camera model")]
        CameraModel = 0x0110,

        [Description("Exposure program")]
        ExposureProgram = 0x8822,

        [Description("Exposure Time")]
        ExposureTime = 0x829A,

        [Description("Aperture f.")]
        Aperture = 0x829D,

        [Description("Focal length mm.")]
        FocalLength = 0x920A,

        [Description("ISO")]
        Iso = 0x8827

    }

}
