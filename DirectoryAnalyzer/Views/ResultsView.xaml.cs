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

            DataContext = model;
        }
    }
}
