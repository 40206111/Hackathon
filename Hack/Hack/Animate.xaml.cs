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
        private List<Ellipse> doots = new List<Ellipse>();
        public Animate()
        {
            InitializeComponent();
            doots.Add(_1);
            doots.Add(_2);
            doots.Add(_3);
            doots.Add(_4);
            doots.Add(_5);
        }


        public void Go(float Fade_in, float tempo)
        {
            FadeIn(Fade_in);
            Bounce(tempo);
        }

        private void FadeIn(float Fade_in)
        {
            Hide();
            float tempfade = (Fade_in / 5.0f);
            float currentfade = Fade_in + tempfade;
            DoubleAnimation fade = new DoubleAnimation { From = 0, To = 1, Duration = new Duration(TimeSpan.FromMinutes(currentfade)) };

            doots.Reverse();

            foreach (Ellipse d in doots)
            {
                currentfade -= tempfade;
                fade.Duration = new Duration(TimeSpan.FromMinutes(currentfade));
                d.BeginAnimation(Ellipse.OpacityProperty, fade);
            }

            doots.Reverse();
        }

        private void Hide()
        {
            _1.Opacity = 0.0f;
            _2.Opacity = 0.0f;
            _3.Opacity = 0.0f;
            _4.Opacity = 0.0f;
            _5.Opacity = 0.0f;
        }

        private void Bounce(float tempo)
        {
            DoubleAnimation bounce = new DoubleAnimation { To = 150 };
            bounce.RepeatBehavior = RepeatBehavior.Forever;
            bounce.AutoReverse = true;
            bounce.Duration = new Duration(TimeSpan.FromSeconds(60/tempo));

            foreach (Ellipse d in doots)
            {
                d.BeginAnimation(Ellipse.HeightProperty, bounce);
            }
        }
    }

}
