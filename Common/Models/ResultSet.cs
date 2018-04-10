using ExifAnalyzer.Common.EXIF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExifAnalyzer.Common.Models
{
    public class ResultSet : IResultSet
    {
        private List<ProcessedPhoto> _resultSet;

        private List<GrouppedProperty> _grouppedProperties { get; set; }


        public ResultSet()
        {
            _grouppedProperties = new List<GrouppedProperty>();
            _resultSet = new List<ProcessedPhoto>();
        }

        public void AddProcessedPhoto(ProcessedPhoto exif)
        {
            _resultSet.Add(exif);
        }

        public void ClearPreviousResults()
        {
            _resultSet.Clear();
        }

        public List<ProcessedPhoto> GetCollection()
        {
            return _resultSet;
        }

        private List<GrouppedProperty> CountProperty(int properyToCount)
        {
            List<GrouppedProperty> grouppedProperties = new List<GrouppedProperty>();
            List<Property> properties = new List<Property>();
            foreach (ProcessedPhoto photo in _resultSet)
            {
                var property = photo.properties.FirstOrDefault(i => i.ExifCode == properyToCount);
                properties.Add(property);
            }
            grouppedProperties = properties.GroupBy(i => i.Value).Select(group => new GrouppedProperty { Value = group.Key, Count = group.Count(), ExifCode = properyToCount }).ToList();
            return grouppedProperties;
        }

        public void CountProperties()
        {
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                var grouppedProperty = CountProperty((int)exifProperty);
                
            }
        }

        public List<GrouppedProperty> GetGrouppedCollection()
        {
            return _grouppedProperties;
        }
              

        public void GenerateGrouppedList()
        {
            _grouppedProperties.Clear();
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                var grouppedProperty = CountProperty((int)exifProperty);
                _grouppedProperties.AddRange(grouppedProperty);
            }
        }
              
        public List<GrouppedProperty> GetFilteredGrouppedCollection(int filter)
        {
            return _grouppedProperties.Where(i => i.ExifCode == filter).ToList();
        }

        public int GetNumberOfAnalyzedPictures()
        {
            return _resultSet.Count();
        }
    }
}
