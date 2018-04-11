using ExifAnalyzer.Common.Models;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using ExifAnalyzer.Common.Helpers;
using ExifAnalyzer.Common.EXIF;

namespace MainModule.ViewModels
{
    public class FileAnalyzerViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<ProcessedPhoto> _processedPhotos;
        private readonly IResultSet _resultSet;
        private readonly IRegionManager _regionManager;
        private ProcessedPhoto _selectedImage;
        private ObservableCollection<string> _propertyList;
        public ObservableCollection<ProcessedPhoto> ProcessedPhotos
        {
            get { return _processedPhotos; }
            set { SetProperty(ref _processedPhotos, value); }
        }

        public FileAnalyzerViewModel(IResultSet resultSet, IRegionManager regionManager)
        {
            _resultSet = resultSet;
            _regionManager = regionManager;
        }

        public ObservableCollection<string> PropertyList
        {
            get { return _propertyList; }
            set { SetProperty(ref _propertyList, value); }
        }

        public ProcessedPhoto SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                SetProperty(ref _selectedImage, value);
                UpdateImageProperty();
            }
        }

        private void UpdateImageProperty()
        {
            List<string> propList = new List<string>();
            foreach (Property prop in SelectedImage.properties)
            {
                ExifProperties currentProp =(ExifProperties)prop.ExifCode;
                var enumDesc = EnumHelper.GetEnumDescription(currentProp);
                propList.Add(enumDesc + ": " + prop.Value);
            }
            PropertyList = new ObservableCollection<string>(propList);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ProcessedPhotos = new ObservableCollection<ProcessedPhoto>(_resultSet.GetCollection());
        }
    }
}
