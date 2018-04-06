using ExifAnalyzer.Common.EXIF;
using ExifAnalyzer.Common.Helpers;
using MainModule.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MainModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ResultsView : UserControl
    {
        ResultsViewModel _model;
        Binding _checkedChangedBinding;
        Binding _checkedBinding;

        public ResultsView(ResultsViewModel model)
        {
            InitializeComponent();   
            
            _model = model;
            CreateBindings();
            CreateDynamicFitlers();

            DataContext = model;
        }

        private void CreateBindings()
        {
            _checkedChangedBinding = new Binding();
            _checkedChangedBinding.Source = _model;
            _checkedChangedBinding.Path = new PropertyPath("CheckedChangedCommand");

            _checkedBinding = new Binding();
            _checkedBinding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
            _checkedBinding.Path = new PropertyPath(nameof(CheckBox.IsChecked));
        }

        private void CreateDynamicFitlers()
        {
            foreach (ExifProperties exifProperty in Enum.GetValues(typeof(ExifProperties)))
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Tag = exifProperty;
                var description = EnumHelper.GetEnumDescription(exifProperty);
                checkBox.Content = description;
                Thickness thickness = new Thickness(5);
                checkBox.Margin = thickness;
                checkBox.SetBinding(CheckBox.CommandProperty, _checkedChangedBinding);
                checkBox.SetBinding(CheckBox.CommandParameterProperty, _checkedBinding);
                FilterPanel.Children.Add(checkBox);
                
            }
        }

        
    }
}
