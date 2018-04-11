using ExifAnalyzer.Common.EXIF;
using ExifAnalyzer.Common.Helpers;
using ExifAnalyzer.Common.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using ExifAnalyzer.Common;
using Prism.Regions;

namespace MainModule.ViewModels
{
    public class ResultsViewModel : BindableBase, INavigationAware
    {
        private ObservableCollection<Filter> _resultFilters;
        private IResultSet _resultSet;
        private ObservableCollection<GrouppedProperty> _grouppedProperties;
        private string[] _graphTypes;
        private string _selectedGraphType;
        public ICommand CheckedChangedCommand { get; set; }

        public ObservableCollection<Filter> ResultFilters
        {
            get { return _resultFilters; }
            set { SetProperty(ref _resultFilters, value); }
        }

        public string[] GraphTypes
        {
            get { return _graphTypes; }
            set { SetProperty(ref _graphTypes, value); }
        }

        public ObservableCollection<GrouppedProperty> GrouppedProperties
        {
            get { return _grouppedProperties; }
            set { SetProperty(ref _grouppedProperties, value); }
        }

        public string SelectedGraphType
        {
            get { return _selectedGraphType; }
            set
            {
                SetProperty(ref _selectedGraphType, value);
                RedeisgnGraphs();
            }
        }

        private void RedeisgnGraphs()
        {
            if (_resultSet == null)
            {
                return;
            }
            switch (SelectedGraphType)
            {
                case "Pie Chart":
                    DesignCakeGraph();
                    break;
                case "Bar Graph":
                    DesignBarGraph();
                    break;
            }

        }


        public PlotModel ResultsPlot { get; private set; }

        public ResultsViewModel(IResultSet resultSet)
        {
            ResultsPlot = InitialiseGraph();
            _grouppedProperties = new ObservableCollection<GrouppedProperty>();
            InitializeCommands();
            InitializeResultFilters();
            GraphTypes = new string[] { "Pie Chart", "Bar Graph" };
            SelectedGraphType = "Pie Chart";
            _resultSet = resultSet;
        }

        private PlotModel InitialiseGraph()
        {
            var plotModel = new PlotModel();
            plotModel.LegendPlacement = LegendPlacement.Outside;
            plotModel.LegendPosition = LegendPosition.BottomCenter;
            plotModel.LegendOrientation = LegendOrientation.Horizontal;
            plotModel.LegendBorderThickness = 0;
            return plotModel;
        }

        private void InitializeResultFilters()
        {
            Guid guid = new Guid();
            ResultFilters = new ObservableCollection<Filter>();
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                Filter resultFilter = new Filter();
                resultFilter.Name = EnumHelper.GetEnumDescription(exifProperty);
                if (exifProperty == ExifProperties.CameraModel)
                    resultFilter.IsChecked = false;
                resultFilter.ExifCode = (int)exifProperty;
                resultFilter.PropertyChanged += ManageCheckBoxPropertyChanged;
                resultFilter.Guid = guid.ToString();
                ResultFilters.Add(resultFilter);
            }
        }

        private void ManageCheckBoxPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Filter filter = (Filter)sender;
            if (filter.IsChecked == false)
            {
                return;
            }
            GrouppedProperties.Clear();
            List<GrouppedProperty> propertiesToAdd = _resultSet.GetFilteredGrouppedCollection(filter.ExifCode);
            GrouppedProperties.AddRange(propertiesToAdd);
            RedeisgnGraphs();
            
        }

        private void ForceGraphToRedisgn()
        {
            GrouppedProperties.Clear();
            Filter filter = _resultFilters.FirstOrDefault(i => i.IsChecked);
            if(filter == null)
            {
                return;
            }
            List<GrouppedProperty> propertiesToAdd = _resultSet.GetFilteredGrouppedCollection(filter.ExifCode);
            GrouppedProperties.AddRange(propertiesToAdd);
            RedeisgnGraphs();
        }

        private void DesignBarGraph()
        {

            ResultsPlot.Series.Clear();
            ResultsPlot.Axes.Clear();
            ResultsPlot.PlotMargins = new OxyThickness(55, 15, 15, 15);
            int numberOfPictures = _resultSet.GetNumberOfAnalyzedPictures();
            var regrouppedProp = GrouppedProperties.GroupBy(i => i.ExifCode);
            foreach (var exifGroup in regrouppedProp)
            {
                BarSeries series = new BarSeries();
                series.StrokeColor = OxyColors.Black;
                series.LabelPlacement = LabelPlacement.Inside;
                CategoryAxis categoryAxis = new CategoryAxis();
                categoryAxis.Position = AxisPosition.Left;
                categoryAxis.Angle = -45;
                categoryAxis.FontSize = 11;
                foreach (GrouppedProperty property in exifGroup)
                {
                    BarItem item = new BarItem();
                    item.Value = property.Count;
                    series.Items.Add(item);
                    categoryAxis.Labels.Add(property.Value);
                }
                var valueAxis = new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    MinimumPadding = 0,
                    MaximumPadding = 2.5,
                    AbsoluteMinimum = 0,
                    Maximum = numberOfPictures,
                    Unit = "Photos",
                    MinimumMajorStep = 1,
                    MinimumMinorStep = 1
                };
                ResultsPlot.Series.Add(series);
                ResultsPlot.Axes.Add(categoryAxis);
                ResultsPlot.Axes.Add(valueAxis);
            }
            ResultsPlot.InvalidatePlot(true);
        }

        private void DesignLineGraph()
        {
            ResultsPlot.Series.Clear();
            ResultsPlot.Axes.Clear();
            int numberOfPictures = _resultSet.GetNumberOfAnalyzedPictures();
            var regrouppedProp = GrouppedProperties.GroupBy(i => i.ExifCode);
            foreach (var exifGroup in regrouppedProp)
            {
                LineSeries series = new LineSeries();
                CategoryAxis categoryAxis = new CategoryAxis();
                categoryAxis.Position = AxisPosition.Bottom;
                int i = 0;

                foreach (GrouppedProperty property in exifGroup)
                {
                    series.Points.Add(new DataPoint(Convert.ToDouble(i), Convert.ToDouble(property.Count)));
                    categoryAxis.Labels.Add(property.Value);
                    i++;
                }
                var valueAxis = new LinearAxis
                {
                    Position = AxisPosition.Left,
                    MinimumPadding = 0,
                    MaximumPadding = 2.5,
                    AbsoluteMinimum = 0,
                    Minimum = 0,
                    Maximum = numberOfPictures,
                    Unit = "Photos",
                    MinimumMajorStep = 1,
                    MinimumMinorStep = 1,
                    MajorGridlineThickness = 1

                };
                ResultsPlot.Series.Add(series);
                ResultsPlot.Axes.Add(categoryAxis);
                ResultsPlot.Axes.Add(valueAxis);
            }
            ResultsPlot.InvalidatePlot(true);
        }

        private void DesignCakeGraph()
        {

            ResultsPlot.Title = "Distribution of values over number of pictures";
            ResultsPlot.TitleFontSize = 20;
            ResultsPlot.TitleFont = "Segoe UI";
            ResultsPlot.TitleColor = OxyColor.FromRgb(ColorsHelper.RedMainGreen, ColorsHelper.GreenMainGreen, ColorsHelper.BlueMainGreen);
            ResultsPlot.PlotMargins = new OxyThickness(15);
            ResultsPlot.Series.Clear();
            ResultsPlot.Axes.Clear();
            int numberOfPictures = _resultSet.GetNumberOfAnalyzedPictures();
            var regrouppedProp = GrouppedProperties.GroupBy(i => i.ExifCode);
            foreach (var exifGroup in regrouppedProp)
            {
                PieSeries series = new PieSeries();
                foreach (GrouppedProperty property in exifGroup)
                {
                    series.Slices.Add(new PieSlice(property.Value, property.Count) { IsExploded = true });
                }
                ResultsPlot.Series.Add(series);
            }
            ResultsPlot.InvalidatePlot(true);
        }

        private void InitializeCommands()
        {
            CheckedChangedCommand = new DelegateCommand<bool?>(OnCheckedChangedCommand);
        }

        private void OnCheckedChangedCommand(bool? isChecked)
        {
            bool? isCheckedlocal = isChecked;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (ResultFilters.All(i => i.IsChecked == false))
            {
                ResultFilters[0].IsChecked = true;
            }
            ForceGraphToRedisgn();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
