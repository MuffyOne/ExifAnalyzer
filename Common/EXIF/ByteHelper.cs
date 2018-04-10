using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExifAnalyzer.Common.EXIF
{
    public class ByteHelper
    {
        public UInt32 ReadInt(byte[] data, int offset)
        {
            return BitConverter.ToUInt32(data, offset);
        }
    }
}
