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

namespace OlafTherapy.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            pbLeft.StateChanged += OnStateChanged;
            pbRight.StateChanged += OnStateChanged;
        }

        bool _isPlaying = false;
        private void OnStateChanged()
        {
            if (_isPlaying) { return; }
            if (pbLeft.IsPressed && pbRight.IsPressed)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(DoReward);
            }
        }

        private void DoReward(object state)
        {
            _isPlaying = true;

            this.Dispatcher.Invoke(new Action(() => {
                pbLeft.Visibility = Visibility.Hidden;
                pbRight.Visibility = Visibility.Hidden;
                olaf.Visibility = Visibility.Visible;
            }));

            MediaPlayer mp = new MediaPlayer();
            mp.Open(new Uri(new System.IO.FileInfo(".\\OlafWarmHugs.mp3").FullName));
            mp.Play();
            System.Threading.Thread.Sleep(6000);

            this.Dispatcher.Invoke(new Action(() => {
                Reset();
            }));

            _isPlaying = false;
        }

        Random _r = new Random(DateTime.Now.Millisecond);
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            pbLeft.Visibility = Visibility.Visible;
            pbRight.Visibility = Visibility.Visible;
            pbLeft.Reset();
            pbRight.Reset();
            Canvas.SetTop(pbLeft, _r.Next(0, Convert.ToInt32(canvasLeft.ActualHeight - pbLeft.ActualHeight)));
            Canvas.SetLeft(pbLeft, _r.Next(0, Convert.ToInt32(canvasLeft.ActualWidth - pbLeft.ActualWidth)));
            Canvas.SetTop(pbRight, _r.Next(0, Convert.ToInt32(canvasRight.ActualHeight - pbRight.ActualHeight)));
            Canvas.SetLeft(pbRight, _r.Next(0, Convert.ToInt32(canvasRight.ActualWidth - pbRight.ActualWidth)));
            
            this.olaf.Visibility = Visibility.Hidden;
        }

        protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            //base.OnPreviewMouseRightButtonDown(e);
        }

        protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
        {
            e.Handled = true;
            base.OnPreviewMouseRightButtonUp(e);
        }
    }
}
