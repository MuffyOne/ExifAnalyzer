using MainModule.ViewModels;
using System.Windows.Controls;

namespace MainModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class FileAnalyzerView : UserControl
    {
        public FileAnalyzerView(FileAnalyzerViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
