using System.Collections.Generic;

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
    }
}
