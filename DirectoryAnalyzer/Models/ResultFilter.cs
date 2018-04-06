using System.ComponentModel;

namespace MainModule.Models
{
    public class ResultFilter : INotifyPropertyChanged
    {
        public bool IsChecked { get; set; }
        public string Name { get; set; }
        public int ExifCode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
