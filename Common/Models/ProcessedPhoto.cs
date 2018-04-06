using System;
using System.Collections.Generic;

namespace ExifAnalyzer.Common.Models
{
    public class ProcessedPhoto
    {
        public List<Tuple<int, string>> properties;
        public ProcessedPhoto()
        {
            properties = new List<Tuple<int, string>>();
        }

        public void AddProperty(int property, string propertyValue)
        {
            properties.Add(new Tuple<int, string>(property, propertyValue));
        }
    }
}
