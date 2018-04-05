using ExifAnalyzer.Common.CustomControls;
using Prism.Regions;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using ExifAnalyzer.Common;
using System.Linq;
using MainModule;
using Prism.Mvvm;
using ExifAnalyzer.Common.Extensions;

namespace MainMenu
{
    public class MainMenuModuleViewModel : BindableBase
    {

        private readonly IRegionManager _regionManager;

        #region Properties

        public ICommand ShowDirectoryPage { get; set; }
        public ICommand ShowImagePage { get; set; }
        public ICommand ShowResultPage { get; set; }
        private IRegion MainRegion { get { return _regionManager.Regions[RegionNames.MainRegion]; } }
        #endregion

        #region constructor
        public MainMenuModuleViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _toggleButtons = new ObservableCollection<SidebarToggleButton>();
            _toggleButtons.CollectionChanged += _toggleButtons_CollectionChanged;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ShowDirectoryPage = new DelegateCommand<bool?>(OnShowDirectoryPage);
            ShowImagePage = new DelegateCommand<bool?>(OnShowImagePage);
            ShowResultPage = new DelegateCommand<bool?>(OnShowResultPage);
        }

        private void OnShowResultPage(bool? isChecked)
        {
            if (MainRegion.ActiveViews.FirstOrDefault() is ResultsView)
            {
                return;
            }
            MainRegion.NavigateTo(isChecked == true ? typeof(ResultsView) : null);
        }

        private void OnShowImagePage(bool? isChecked)
        {
            if (MainRegion.ActiveViews.FirstOrDefault() is FileAnalyzerView)
            {
                return;
            }
            MainRegion.NavigateTo(isChecked == true ? typeof(FileAnalyzerView) : null);
        }

        private void OnShowDirectoryPage(bool? isChecked)
        {
            if (MainRegion.ActiveViews.FirstOrDefault() is DirectoryAnalyzerView)
            {
                return;
            }
            MainRegion.NavigateTo(isChecked == true ? typeof(DirectoryAnalyzerView) : null);
        }
        #endregion

        private ObservableCollection<SidebarToggleButton> _toggleButtons;

       
        private void _toggleButtons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            AddEventHandlersToButtons(e.NewItems);
            RemoveEventHandlersFromButtons(e.OldItems);
        }

        private void AddEventHandlersToButtons(IList newItems)
        {
            if (newItems == null)
            {
                return;
            }
            foreach (object item in newItems)
            {
                SidebarToggleButton toggleButton = (SidebarToggleButton)item;
                toggleButton.Checked += toggleButton_Checked;
                toggleButton.Unchecked += toggleButton_Unchecked;
            }
        }

        private void RemoveEventHandlersFromButtons(IList oldItems)
        {
            if (oldItems == null)
            {
                return;
            }
            foreach (object item in oldItems)
            {
                SidebarToggleButton toggleButton = (SidebarToggleButton)item;
                toggleButton.Checked -= toggleButton_Checked;
                toggleButton.Unchecked -= toggleButton_Unchecked;
            }
        }

        private void toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            SidebarToggleButton button = sender as SidebarToggleButton;
            if (button == null)
            {
                return;
            }
            button.IsChecked = true;
        }

        private void toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            foreach (SidebarToggleButton button in _toggleButtons)
            {
                SidebarToggleButton toggleButton = button as SidebarToggleButton;

                if (toggleButton != null && toggleButton != sender)
                {
                    toggleButton.InternalSetCheckedState(false, false);
                }

            }
        }

        public void RegisterToggleButton(SidebarToggleButton button)
        {
            _toggleButtons.Add(button);
        }
    }
}
