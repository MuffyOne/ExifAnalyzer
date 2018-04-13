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
        private ObservableCollection<PropertyViewer> _propertyList;
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

        public ObservableCollection<PropertyViewer> PropertyList
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
            PropertyList = new ObservableCollection<PropertyViewer>();
            foreach (Property prop in SelectedImage.properties)
            {
                PropertyViewer propertyViewer = new PropertyViewer();
                ExifProperties currentProp = (ExifProperties)prop.ExifCode;
                propertyViewer.PropertyName = EnumHelper.GetEnumDescription(currentProp);
                propertyViewer.PropertyValue = prop.Value;
                PropertyList.Add(propertyViewer);
            }
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
