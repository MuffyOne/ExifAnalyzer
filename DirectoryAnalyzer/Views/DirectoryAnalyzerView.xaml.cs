﻿using MainModule.ViewModels;
using System.Windows.Controls;

namespace MainModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DirectoryAnalyzerView : UserControl
    {
        public DirectoryAnalyzerView(DirectoryAnalyzerViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
