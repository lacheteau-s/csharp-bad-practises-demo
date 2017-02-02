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

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            button1.Click += Button_Click1;
        }

        #region Async void exception
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                ThrowExceptionAsync();
            }
            catch (Exception)
            {
                // Exception is never caught
                MessageBox.Show("Caught exception");
            }
        }

        private async void ThrowExceptionAsync()
        {
            throw new InvalidOperationException();
        }
        #endregion
    }
}
