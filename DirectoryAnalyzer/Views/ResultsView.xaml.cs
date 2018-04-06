using ExifAnalyzer.Common.EXIF;
using ExifAnalyzer.Common.Helpers;
using MainModule.ViewModels;
using System;
using System.Windows.Controls;

namespace MainModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ResultsView : UserControl
    {
        ResultsViewModel _model;

        public ResultsView(ResultsViewModel model)
        {
            InitializeComponent();
            CreateDynamicFitlers();
            _model = model;
            DataContext = model;
        }

        private void CreateDynamicFitlers()
        {
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Tag = exifProperty;
                var description = EnumHelper.GetEnumDescription(exifProperty);
                checkBox.Content = description;
                System.Windows.Thickness thickness = new System.Windows.Thickness(5);
                checkBox.Margin = thickness;
                FilterPanel.Children.Add(checkBox);
                
            }
        }

        
    }
}
