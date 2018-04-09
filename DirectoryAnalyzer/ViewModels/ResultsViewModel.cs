using ExifAnalyzer.Common.EXIF;
using ExifAnalyzer.Common.Helpers;
using ExifAnalyzer.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MainModule.ViewModels
{
    public class ResultsViewModel : BindableBase
    {
        private ObservableCollection<Filter> _resultFilters;
        private IResultSet _resultSet;
        private ObservableCollection<GrouppedProperty> _grouppedProperties;

        public ICommand CheckedChangedCommand { get; set; }

        public ObservableCollection<Filter> ResultFilters
        {
            get { return _resultFilters; }
            set { SetProperty(ref _resultFilters, value); }
        }

        public ObservableCollection<GrouppedProperty> GrouppedProperties
        {
            get { return _grouppedProperties; }
            set { SetProperty(ref _grouppedProperties, value); }
        }

        public ResultsViewModel(IResultSet resultSet)
        {
            _grouppedProperties = new ObservableCollection<GrouppedProperty>();
            InitializeCommands();
            InitializeResultFilters();
            _resultSet = resultSet;
        }

        private void InitializeResultFilters()
        {
            ResultFilters = new ObservableCollection<Filter>();
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                Filter resultFilter = new Filter();
                resultFilter.Name = EnumHelper.GetEnumDescription(exifProperty);
                resultFilter.IsChecked = false;
                resultFilter.ExifCode = (int)exifProperty;
                resultFilter.PropertyChanged += ManageCheckBoxPropertyChanged;
                ResultFilters.Add(resultFilter);
            }
        }

        private void ManageCheckBoxPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Filter filter = (Filter)sender;
            if (filter.IsChecked == false)
            {
                List<GrouppedProperty> propertiesToRemove = new List<GrouppedProperty>();
                foreach (GrouppedProperty property in GrouppedProperties)
                {
                    if (property.ExifCode == filter.ExifCode)
                    {
                        propertiesToRemove.Add(property);
                    }
                }
                foreach (GrouppedProperty property in propertiesToRemove)
                {
                    GrouppedProperties.Remove(property);
                }
            }
            else
            {
                List<GrouppedProperty> propertiesToAdd = _resultSet.GetFilteredGrouppedCollection(filter.ExifCode);
                GrouppedProperties.AddRange(propertiesToAdd);
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
