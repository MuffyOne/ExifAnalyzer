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
using OxyPlot.Annotations;

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

        public PlotModel ResultsPlot { get; private set; }

        public ResultsViewModel(IResultSet resultSet)
        {
            ResultsPlot = InitialiseGraph();
            _grouppedProperties = new ObservableCollection<GrouppedProperty>();
            InitializeCommands();
            InitializeResultFilters();
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
            //DesignBarGraph();
            DesignCakeGraph();
        }

        private void DesignBarGraph()
        {

            ResultsPlot.Series.Clear();
            ResultsPlot.Axes.Clear();
            int numberOfPictures = _resultSet.GetNumberOfAnalyzedPictures();
            var regrouppedProp = GrouppedProperties.GroupBy(i => i.ExifCode);
            foreach (var exifGroup in regrouppedProp)
            {
                BarSeries series = new BarSeries();
                series.StrokeColor = OxyColors.Black;
                series.LabelPlacement = LabelPlacement.Inside;
                CategoryAxis categoryAxis = new CategoryAxis();
                categoryAxis.Position = AxisPosition.Left;
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
            ResultsPlot.TitleColor = OxyColor.FromRgb(56,120,8);
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
    }
}
