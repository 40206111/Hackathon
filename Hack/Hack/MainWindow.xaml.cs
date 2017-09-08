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
            int dataCollect = 0;

            if(!String.IsNullOrWhiteSpace(txtArtistName.Text))
            {
                dataCollect += 2;
            }
            if (!String.IsNullOrWhiteSpace(txtAlbum.Text))
            {
                dataCollect += 3;
            }
            if (!String.IsNullOrWhiteSpace(txtSong.Text))
            {
                dataCollect += 4;
            }

            /*Artist = 2
             * Album = 3
             * Song = 4
             * All = 9
             * Artist + Album = 5
             * Album + song = 7
             * Artist + song = 6
            */
            switch(dataCollect)
            {
                case 9:
                    //search for data based on Artist, Album and Song
                    txtbError.Text = "";
                    //DEBUG
                    Console.WriteLine(txtArtistName.Text + " " + txtAlbum.Text + " " + txtSong.Text + " " + dataCollect);
                    break;
                case 7:
                    //Search for data based on Album and Song
                    txtbError.Text = "";
                    //DEBUG
                    Console.WriteLine(txtAlbum.Text + " " + txtSong.Text + " " + dataCollect);
                    break;
                case 6:
                    //Search for data based on Artist and Song
                    txtbError.Text = "";
                    //DEBUG
                    Console.WriteLine(txtArtistName.Text + " " + txtSong.Text + " " + dataCollect);
                    break;
                case 5:
                    //Search for data based on Artist and Album
                    txtbError.Text = "";
                    //DEBUG
                    Console.WriteLine(txtArtistName.Text + " " + txtAlbum.Text + " " + dataCollect);
                    break;
                case 4:
                    //Search for data based on Song
                    txtbError.Text = "";
                    //DEBUG
                    Console.WriteLine(txtSong.Text + " " + dataCollect);
                    break;
                case 3:
                    //Search for data based on Album
                    txtbError.Text = "";
                    //DEBUG
                    Console.WriteLine(txtAlbum.Text + " " + dataCollect);
                    break;
                case 2:
                    //Search for data based on Artist
                    txtbError.Text = "";
                    //DEBUG
                    Console.WriteLine(txtArtistName.Text + " " + dataCollect);
                    break;
                default:
                    txtbError.Text = "All fields are empty";
                    break;
            }

            //Add data to listbox

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
        }
    }
}
