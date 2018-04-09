using System;
using System.Collections.Generic;

namespace ExifAnalyzer.Common.Models
{
    public class ProcessedPhoto
    {
        public List<Property> properties;


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
