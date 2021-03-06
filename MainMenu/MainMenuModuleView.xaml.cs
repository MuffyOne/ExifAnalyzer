﻿using System.Windows.Controls;

namespace MainMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainMenuModuleView : UserControl
    {
        public MainMenuModuleView(MainMenuModuleViewModel model)
        {
            InitializeComponent();
            DataContext = model;
            model.RegisterToggleButton(ResultsToggleButton);
            model.RegisterToggleButton(DirectoryToggleButton);
            model.RegisterToggleButton(ImageAnalyzer);
        }
    }
}
