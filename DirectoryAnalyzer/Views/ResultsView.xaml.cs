using MainModule.ViewModels;
using System.Windows.Controls;

namespace MainModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ResultsView : UserControl
    {
        public ResultsView(ResultsViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}
