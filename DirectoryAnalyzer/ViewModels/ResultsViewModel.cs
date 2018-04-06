using ExifAnalyzer.Common.EXIF;
using ExifAnalyzer.Common.Helpers;
using MainModule.Models;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MainModule.ViewModels
{
    public class ResultsViewModel
    {
        private ObservableCollection<ResultFilter> _resultFilters;

        public ICommand CheckedChangedCommand { get; set; }

        ObservableCollection<ResultFilter> ResultFilters {
            get
            {
                return _resultFilters;
            }
            set
            {
                _resultFilters = value;
            }
        }

        public ResultsViewModel()
        {
            InitializeCommands();
            InitializeResultFilters();
        }

        private void InitializeResultFilters()
        {
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                ResultFilter resultFilter = new ResultFilter();
                resultFilter.Name = EnumHelper.GetEnumDescription(exifProperty);
                resultFilter.IsChecked = false;
                resultFilter.ExifCode = (int)exifProperty;
                ResultFilters.Add(resultFilter);
            }
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
