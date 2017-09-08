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


        public void Go(float Fade_in, float tempo, float fade_out, float duration)
        {
            foreach(Ellipse d in doots)
            {
                d.Height = 100;
                d.Opacity = 1.0f;
            }

            dots.Visibility = Visibility.Visible;

            FadeIn(Fade_in, fade_out, duration);
            Bounce(tempo);
        }

        private void FadeIn(float Fade_in, float Fade_out, float duration)
        {
            float tempfade = (Fade_in / 5.0f);
            float currentfade = Fade_in + tempfade;
            DoubleAnimation fade = new DoubleAnimation {From = 0, To = 1, Duration = new Duration(TimeSpan.FromMinutes(currentfade)), BeginTime = TimeSpan.FromMinutes(0)};
            Storyboard sb = new Storyboard { Duration = new Duration(TimeSpan.FromMinutes(duration))};

            sb.Completed += new EventHandler(animDone);

            float tempf = ((duration - Fade_out) / 5.0f);
            float currentf = (duration - Fade_out) + tempf;
            DoubleAnimation fadeOut = new DoubleAnimation { To = 0, Duration = new Duration(TimeSpan.FromMinutes(currentf)), BeginTime = TimeSpan.FromMinutes(Fade_out) };

            doots.Reverse();
            foreach (Ellipse d in doots)
            {
                currentfade -= tempfade;
                fade.Duration = new Duration(TimeSpan.FromMinutes(currentfade));
                Storyboard.SetTarget(fade, d);
                Storyboard.SetTargetProperty(fade, new PropertyPath(Ellipse.OpacityProperty));
                sb.Children.Add(fade);
                currentf -= tempf;
                fadeOut.Duration = new Duration(TimeSpan.FromMinutes(currentf));
                Storyboard.SetTarget(fadeOut, d);
                Storyboard.SetTargetProperty(fadeOut, new PropertyPath(Ellipse.OpacityProperty));
                sb.Children.Add(fadeOut);
                d.BeginStoryboard(sb);
            }

            doots.Reverse();
        }

        private void animDone(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
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
