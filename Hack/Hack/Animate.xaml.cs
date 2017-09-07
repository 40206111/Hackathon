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
using System.Windows.Media.Animation;

namespace Hack
{
    /// <summary>
    /// Interaction logic for Animate.xaml
    /// </summary>
    public partial class Animate : UserControl
    {
        public Animate()
        {
            InitializeComponent();
        }

        public void Go(float duration, float Fade_in)
        {
            Storyboard sb = (this.FindResource("Test")as Storyboard);
            sb.Duration = new Duration(TimeSpan.FromMinutes(duration));
            DoubleAnimation da = new DoubleAnimation(247, new Duration(TimeSpan.FromMinutes(Fade_in)));
            //sb.RepeatBehavior = new RepeatBehavior();
            MessageBox.Show(Convert.ToString(Fade_in));
            //sb.BeginTime = TimeSpan.FromMinutes(Fade_in);
            da.Duration = new Duration(TimeSpan.FromMinutes(Fade_in));
            sb.Begin();
        }
    }
}
