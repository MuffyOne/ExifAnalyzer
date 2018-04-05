using ExifAnalyzer.Common.CustomControls;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace MainMenu
{
    public class MainMenuModuleViewModel
    {
        private ObservableCollection<SidebarToggleButton> _toggleButtons;

        public MainMenuModuleViewModel()
        {
            _toggleButtons = new ObservableCollection<SidebarToggleButton>();
            _toggleButtons.CollectionChanged += _toggleButtons_CollectionChanged;
        }

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
