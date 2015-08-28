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
    /// Interaction logic for PushButton.xaml
    /// </summary>
    public partial class PushButton : UserControl
    {
        public PushButton()
        {
            InitializeComponent();
            this.thumbCircle.TouchDown += ThumbCircle_TouchDown;
            this.thumbCircle.TouchUp += ThumbCircle_TouchUp;
        }

        public event Action StateChanged;

        public bool IsPressed
        {
            get; set;
        }

        public void Reset()
        {
            this.thumbCircle.Fill = Brushes.Yellow;
            IsPressed = false;
        }

        private void ThumbCircle_TouchUp(object sender, TouchEventArgs e)
        {
            IsPressed = false;
            this.thumbCircle.Fill = Brushes.Yellow;
            FireStateChanged();
        }

        private void ThumbCircle_TouchDown(object sender, TouchEventArgs e)
        {
            IsPressed = true;
            this.thumbCircle.Fill = Brushes.Lime;
            FireStateChanged();
        }

        private void FireStateChanged()
        {
            if (StateChanged != null)
            {
                StateChanged();
            }
        }
    }
}
