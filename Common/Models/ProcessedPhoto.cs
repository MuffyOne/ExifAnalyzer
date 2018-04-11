using System;
using System.Collections.Generic;
using System.Drawing;

namespace ExifAnalyzer.Common.Models
{
    public class ProcessedPhoto
    {
        public List<Property> properties;

        public Image Thumbnail { get; set; }
        public ProcessedPhoto()
        {
            properties = new List<Property>();
        }

        public void AddProperty(int exif, string propertyValue)
        {
            Property prop = new Property();
            prop.ExifCode = exif;
            prop.Value = propertyValue;
            properties.Add(prop);
        }

        
    }
}
