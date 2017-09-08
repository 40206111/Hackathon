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

        List<Song> list;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            ImportData.SongsToDisplay(1000000);
        }

        private void btnBorder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            int check = 0;

            if (String.IsNullOrWhiteSpace(txtArtistName.Text))
            {
                txtArtistName.Text = "";
                check += 1;
            }
            if (String.IsNullOrWhiteSpace(txtAlbum.Text))
            {
                txtAlbum.Text = "";
                check += 1;
            }
            if (String.IsNullOrWhiteSpace(txtSong.Text))
            {
                txtSong.Text = "";
                check += 1;
            }

            if (check != 3)
            {
                lstData.Visibility = Visibility.Hidden;
                Loading.Visibility = Visibility.Visible;
                ToggeEnable();
                Mouse.OverrideCursor = Cursors.Wait;
                lstData.Items.Clear();

                //Search for data
                list = SearchSong.Search(txtArtistName.Text, txtAlbum.Text, txtSong.Text);
                //Add data to listbox
                for (int i = 0; i < list.Count; i++)
                {
                    string content = "Artist Name: " + list[i].ArtistName + "\nAlbum Name: " + list[i].Release + "\nSong Name: " + list[i].Title;
                    lstData.Items.Insert(i, content);
                    Console.WriteLine(content);
                }

                lstData.Visibility = Visibility.Visible;
                Loading.Visibility = Visibility.Hidden;
                ToggeEnable();
                Mouse.OverrideCursor = null;
            }
            else
            {
                txtbError.Text = "All fields are empty";
            }


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
            try
            {
                txtbError.Text = "Please select a song";
                int i = lstData.SelectedIndex;
                Doots.Go(0.2f, list[i].Tempo, 0.2f, list[i].Duration);
                Player k = new Player();
              }
            catch
            {
                txtbError.Text = "Please select a song";
            }
        }

        private void VolumeChanged(object sender, RoutedEventArgs e)
        {
            lblVolume.Content = "Volume " + sldrVol.Value;
        }
    }
}
