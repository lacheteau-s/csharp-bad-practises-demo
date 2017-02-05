using System;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace ConfigureAwaitDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            withoutCA.Click += WithoutConfigureAwait;
            withCA.Click += WithConfigureAwait;
            subsequentAwait.Click += ConfigureAwaitAndSubsequentAwait;
            nestedAwaitNoCA.Click += NestedAwaitWithoutConfigureAwait;
            completedTasksCA.Click += ConfigureAwaitOnCompletedTasks;
            CA_UI.Click += ConfigureAwaitAndUI;
        }

        private async void WithoutConfigureAwait(object sender, RoutedEventArgs e)
        {
            // Code here runs in the original context
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);

            // Code here runs in the original context
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);

            // Code here runs in the original context
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        private async void WithConfigureAwait(object sender, RoutedEventArgs e)
        {
            // Code here runs in the original context
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);

            // Code here runs in the original context
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000).ConfigureAwait(false);

            // Code here runs without the original context => ThreadPool
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        private async void ConfigureAwaitAndSubsequentAwait(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000).ConfigureAwait(false);

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        #region Nested await
        private async void NestedAwaitWithoutConfigureAwait(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Nested await before: " + Thread.CurrentThread.ManagedThreadId);
            await MyMethodAsync();
            Console.WriteLine("Nested await after: " + Thread.CurrentThread.ManagedThreadId);
        }

        private async Task MyMethodAsync()
        {
            Console.WriteLine("MyMethodAsync before: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000).ConfigureAwait(false);
            Console.WriteLine("MyMethodAsync after: " + Thread.CurrentThread.ManagedThreadId);
        }
        #endregion

        private async void ConfigureAwaitOnCompletedTasks(object sender, RoutedEventArgs e)
        {
            // Code here runs in the original context
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.FromResult(1);

            // Code here runs in the original context
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.FromResult(1).ConfigureAwait(false);

            // Code here runs in the original context
            var random = new Random();
            int delay = random.Next(2); // Delay is either 0 or 1

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(delay).ConfigureAwait(false);

            // Code might or might not run in the original context.
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        private async void ConfigureAwaitAndUI(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000).ConfigureAwait(false);

            // Crashes because we are accessing a UI element from a ThreadPool thread due to the use of ConfigureAwait
            Title = "Whatever";
        }
    }
}
