using System.Collections.Generic;

namespace ExifAnalyzer.Common.Models
{
    public interface IResultSet
    {
        List<ProcessedPhoto> GetCollection();
        void GenerateGrouppedList();
        List<GrouppedProperty> GetGrouppedCollection();
        void AddProcessedPhoto(ProcessedPhoto exif);
        List<GrouppedProperty> GetFilteredGrouppedCollection(int filter);
        void ClearPreviousResults();
    }
}
