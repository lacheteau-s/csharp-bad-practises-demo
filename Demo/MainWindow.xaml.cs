using System.Windows;
using Demo.Samples;

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

            var asyncVoid = new AsyncVoidSample();
            var asyncTask = new AsyncTaskSample();
            var awaitTask = new AwaitAsyncTaskSample();

            button1.Click += asyncVoid.EntryPoint;
            button2.Click += asyncTask.EntryPoint;
            button3.Click += awaitTask.EntryPoint;
        }
    }
}
