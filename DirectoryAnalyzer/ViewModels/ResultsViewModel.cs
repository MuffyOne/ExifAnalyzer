using ExifAnalyzer.Common.EXIF;
using ExifAnalyzer.Common.Helpers;
using MainModule.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MainModule.ViewModels
{
    public class ResultsViewModel : BindableBase
    {
        private ObservableCollection<ResultFilter> _resultFilters;
        public ICommand CheckedChangedCommand { get; set; }

        public ObservableCollection<ResultFilter> ResultFilters {
            get {return _resultFilters;}
            set { SetProperty(ref _resultFilters, value); }
        }

        public ResultsViewModel()
        {
            InitializeCommands();
            InitializeResultFilters();
        }

        private void InitializeResultFilters()
        {
            ResultFilters = new ObservableCollection<ResultFilter>();
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                ResultFilter resultFilter = new ResultFilter();
                resultFilter.Name = EnumHelper.GetEnumDescription(exifProperty);
                resultFilter.IsChecked = false;
                resultFilter.ExifCode = (int)exifProperty;
                resultFilter.PropertyChanged += ManageCheckBoxPropertyChanged;
                ResultFilters.Add(resultFilter);
            }
        }

        private static void ManageCheckBoxPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        private void InitializeCommands()
        {
            CheckedChangedCommand = new DelegateCommand<bool?>(OnCheckedChangedCommand);
        }

        private void OnCheckedChangedCommand(bool? isChecked)
        {
            bool? isCheckedlocal = isChecked;
        }
    }
}
