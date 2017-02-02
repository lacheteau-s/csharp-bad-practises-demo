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

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
        }

        #region Async void exception
        private async void Button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThrowExceptionAsync();
            }
            catch (Exception ex)
            {
                // Exception is never caught
                MessageBox.Show(ex.Message);
            }
        }

        private async void ThrowExceptionAsync()
        {
            throw new InvalidOperationException("Exception thrown from an async void returning method");
        }
        #endregion

        #region Async Task Exception
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ThrowTaskExceptionAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await ThrowTaskExceptionAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task ThrowTaskExceptionAsync()
        {
            throw new InvalidOperationException("Exception thrown from an async Task returning method");
        }
        #endregion
    }
}
