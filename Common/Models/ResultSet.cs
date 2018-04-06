using System;
using System.Collections.Generic;
using System.Linq;

namespace ExifAnalyzer.Common.Models
{
    public class ResultSet
    {
        private List<ProcessedPhoto> _resultSet;
                
        public ResultSet()
        {
            _resultSet = new List<ProcessedPhoto>();
        }

        public void AddProcessedPhoto(ProcessedPhoto exif)
        {
            _resultSet.Add(exif);
        }

        public List<ProcessedPhoto> GetCollection()
        {
            return _resultSet;
        }

        public SortedList<string, int> CountProperty(int properyToCount)
        {
            SortedList<string, int> propertyCount = new SortedList<string, int>();
            foreach(ProcessedPhoto photo in _resultSet)
            {
                Tuple<int, string> property = photo.properties.FirstOrDefault(i => i.Item1 == properyToCount);

                if (!propertyCount.ContainsKey(property.Item2))
                {
                    propertyCount.Add(property.Item2, 1);
                }
                else
                {
                    propertyCount[property.Item2] = propertyCount.Values[propertyCount.IndexOfKey(property.Item2)] + 1;
                }
            }

            return propertyCount;
        }
    }
}
