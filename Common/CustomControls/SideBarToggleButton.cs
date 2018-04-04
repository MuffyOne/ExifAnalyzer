using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ExifAnalyzer.Common.CustomControls
{
    public class SidebarToggleButton : ToggleButton
    {
        static SidebarToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SidebarToggleButton), new FrameworkPropertyMetadata(typeof(SidebarToggleButton)));
        }

        #region DependencyProperties
        /// <summary>
        /// Represents the <see cref="Text"/> property as a DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text", typeof(string), typeof(SidebarToggleButton), new FrameworkPropertyMetadata("Text"));

        /// <summary>
        /// Represents the <see cref="ImageSource"/> property as a DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.RegisterAttached("ImageSource", typeof(ImageSource), typeof(SidebarToggleButton), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Represents the <see cref="MouseOverBackground"/> property as a DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(SidebarToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(49, 0, 0, 0))));


        /// <summary>
        /// Represents the <see cref="IsChecked"/> property as a DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached("IsChecked", typeof(bool?), typeof(SidebarToggleButton), new PropertyMetadata(null));

        /// <summary>
        /// Represents the <see cref="ToggledBackground"/> property as a DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ToggledBackgroundProperty = DependencyProperty.RegisterAttached("ToggledBackground", typeof(Brush), typeof(SidebarToggleButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(49, 0, 0, 0))));

        #endregion

       
      
        #region Events
        /// <summary>
        /// Raised when the toggle button value is "indeterminate".
        /// </summary>
        private RoutedEventHandler _indeterminateEvent;
        /// <summary>
        /// Raised when the toggle button value is "checked".
        /// </summary>
        private RoutedEventHandler _checkedEvent;
        /// <summary>
        /// Raised when the toggle button value is "unchecked".
        /// </summary>
        private RoutedEventHandler _uncheckedEvent;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new <see cref="SidebarToggleButton"/>.
        /// </summary>
        public SidebarToggleButton()
        {
            Click += SidebarToggleButton_Click;
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Occurs when a System.Windows.Controls.Button is clicked.
        /// </summary>
        /// <param name="sender">The object where the event handler is attached.</param>
        /// <param name="e">The event data.</param>
        private void SidebarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            IsChecked = IsChecked == null || IsChecked == false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Raises the <see cref="_checkedEvent"/>.
        /// </summary>
        /// <param name="e">The event data.</param>
        private void OnChecked(RoutedEventArgs e)
        {
            if (_checkedEvent != null)
            {
                _checkedEvent(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="_uncheckedEvent"/>.
        /// </summary>
        /// <param name="e">The event data.</param>
        private void OnUnchecked(RoutedEventArgs e)
        {
            if (_uncheckedEvent != null)
            {
                _uncheckedEvent(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="_indeterminateEvent"/>.
        /// </summary>
        /// <param name="e">The event data.</param>
        private void OnIndeterminate(RoutedEventArgs e)
        {
            if (_indeterminateEvent != null)
            {
                _indeterminateEvent(this, e);
            }
        }

        /// <summary>
        /// Provides class handling for the System.Windows.Controls.Primitives.ButtonBase.ClickMode routed event that occurs when the mouse enters this control.
        /// </summary>
        /// <param name="e">The event data for the System.Windows.Input.Mouse.MouseEnter event.</param>
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the text for the button.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets an that can be shown at the edge of the button with the <see cref="Text"/>.
        /// </summary>
        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
        /// <summary>
        /// Gets or sets the background of the button when the mouse moves over it.
        /// </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }
        /// <summary>
        /// Adds or removes an event handler for the Indeterminate event.
        /// </summary>
        public event RoutedEventHandler Indeterminate
        {
            add { _indeterminateEvent += value; }
            // ReSharper disable once DelegateSubtraction
            remove { _indeterminateEvent -= value; }
        }
        /// <summary>
        /// Adds or removes an event handler for the Checked event.
        /// </summary>
        public event RoutedEventHandler Checked
        {
            add { _checkedEvent += value; }
            // ReSharper disable once DelegateSubtraction
            remove { _checkedEvent -= value; }
        }
        /// <summary>
        /// Adds or removes an event handler for the Unchecked event.
        /// </summary>
        public event RoutedEventHandler Unchecked
        {
            add { _uncheckedEvent += value; }
            // ReSharper disable once DelegateSubtraction
            remove { _uncheckedEvent -= value; }
        }

        /// <summary>
        /// Gets or sets the checked state for the button.
        /// </summary>
        /// <remarks>A value of null is treated as "indeterminate".</remarks>
        public bool? IsChecked
        {
            get { return (bool?)GetValue(IsCheckedProperty); }
            set
            {
                if (IsChecked != value)
                {
                    InternalSetCheckedState(value, true);
                }
            }
        }

        #region Fields
        /// <summary>
        /// By default, when the mouse is not hovering over the button, the "background" is transparent (i.e. it's not shown).
        /// </summary>
        protected Brush UnMousedBackground = new SolidColorBrush(Colors.Transparent);
        #endregion

        /// <summary>
        /// Gets or sets the background to apply to the control when it's toggled.
        /// </summary>
        public Brush ToggledBackground
        {
            get { return (Brush)GetValue(ToggledBackgroundProperty); }
            set { SetValue(ToggledBackgroundProperty, value); }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Background = IsChecked == true ? ToggledBackground : UnMousedBackground;
        }

        #endregion

        /// <summary>
        /// Internally checks the new checked state, and if desired raises changed events.
        /// </summary>
        /// <param name="newState">The new checked state. A value of null is treated as "indeterminate".</param>
        /// <param name="raiseEvents">true to raise events. false to not raise events.</param>
        internal void InternalSetCheckedState(bool? newState, bool raiseEvents)
        {
            SetValue(IsCheckedProperty, newState);

            var e = new RoutedEventArgs();
            switch (newState)
            {
                case true:
                    Background = ToggledBackground;
                    if (raiseEvents)
                    {
                        OnChecked(e);
                    }
                    break;

                case false:
                    Background = UnMousedBackground;
                    if (raiseEvents)
                    {
                        OnUnchecked(e);
                    }
                    break;

                default:
                    Background = UnMousedBackground;
                    if (raiseEvents)
                    {
                        OnIndeterminate(e);
                    }
                    break;
            }
        }
    }
}
