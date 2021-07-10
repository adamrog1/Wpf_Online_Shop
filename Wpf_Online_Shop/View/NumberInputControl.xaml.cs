using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_Online_Shop.View
{
    /// <summary>
    /// Logika interakcji dla klasy NumberInputControl.xaml
    /// </summary>
    public partial class NumberInputControl : UserControl
    {
        public NumberInputControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty =
                DependencyProperty.Register(
                    "Text",
                    typeof(string),
                    typeof(NumberInputControl),
                    new FrameworkPropertyMetadata(null)
                );
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly RoutedEvent NumberChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("TabItemSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumberInputControl));
        
        public event RoutedEventHandler NumberChanged
        {
            add { AddHandler(NumberChangedRoutedEvent, value); }
            remove { RemoveHandler(NumberChangedRoutedEvent, value); }
        }

        void NumberChanged_Raise()
        {
            RoutedEventArgs eventArgs = new RoutedEventArgs(NumberInputControl.NumberChangedRoutedEvent);
            RaiseEvent(eventArgs);
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(int.TryParse(e.Text, out _)))
            {
                e.Handled = true;
                return;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NumberChanged_Raise();
        }
    }
}
