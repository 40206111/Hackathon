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

namespace Hack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
        }

        private void btnBorder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            lstData.Visibility = Visibility.Hidden;
            Loading.Visibility = Visibility.Visible;
            ToggeEnable();
            Mouse.OverrideCursor = Cursors.Wait;
            int check = 0;

            if(!String.IsNullOrWhiteSpace(txtArtistName.Text))
            {
                txtArtistName.Text = "";
                check += 1;
            }
            if (!String.IsNullOrWhiteSpace(txtAlbum.Text))
            {
                txtAlbum.Text = "";
                check += 1;
            }
            if (!String.IsNullOrWhiteSpace(txtSong.Text))
            {
                txtSong.Text = "";
                check += 1;
            }
            if (check != 3)
            {
                //Search for data
                //Add data to listbox
            } else
            {
                txtbError.Text = "All fields are empty";
            }

            lstData.Visibility = Visibility.Visible;
            Loading.Visibility = Visibility.Hidden;
            ToggeEnable();
            Mouse.OverrideCursor = null;

        }

        private void ToggeEnable()
        {
            btnGo.IsEnabled = !btnGo.IsEnabled;
            txtArtistName.IsEnabled = !txtArtistName.IsEnabled;
            txtAlbum.IsEnabled = !txtAlbum.IsEnabled;
            txtSong.IsEnabled = !txtSong.IsEnabled;
            btnGenerate.IsEnabled = !btnGenerate.IsEnabled;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            Player p = new Player();
            Doots.Go(0.2f, 120, 0.2f, 0.29f);
        }

        private void VolumeChanged(object sender, RoutedEventArgs e)
        {
            lblVolume.Content = "Volume " + sldrVol.Value;
        }
    }
}
