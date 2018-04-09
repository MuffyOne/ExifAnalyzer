using System;
using System.ComponentModel;

namespace MainModule.Models
{
    public class ResultFilter : INotifyPropertyChanged
    {
        private bool _isChecked;

        public bool IsChecked { get { return _isChecked; }
            set { _isChecked = value; OnPropertyChanged("IsChecked");  } }
        public string Name { get; set; }
        public int ExifCode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

    }
}
