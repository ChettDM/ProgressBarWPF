using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace PrgressBarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();

        // Super important that the percentage is stored up
        // here as the casting to an int, will keep the
        // entire thing at 0% for extremely large processes.
        private double _progressPercentage;

        public MainWindow()
        {
            InitializeComponent();
            Progress.Value = 0;

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.ProgressChanged += (sender, args) => Progress.Value = args.ProgressPercentage; 
            _backgroundWorker.RunWorkerCompleted += (sender, args) => Progress.Value = 100;
            _backgroundWorker.DoWork += (sender, args) =>
            {
                for (var i = 0; i < 100; i++)
                {
                    if (((BackgroundWorker) sender).CancellationPending)
                    {
                        args.Cancel = true;
                        break;
                    }
                    
                    _backgroundWorker.ReportProgress((int)(_progressPercentage =+ i));
                    Thread.Sleep(500);
                }
            };
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _backgroundWorker.RunWorkerAsync();
        }


        private void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
        {
            _backgroundWorker.CancelAsync(); // <= Doesn't work
        }

        
    }
}
