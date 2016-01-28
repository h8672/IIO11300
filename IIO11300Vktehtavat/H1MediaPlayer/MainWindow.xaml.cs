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
using Microsoft.Win32;

namespace H1MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isPlaying = false;
        public MainWindow() {
            InitializeComponent();
            isPlaying = false;
            SetMyButtons();
            btnStop.IsEnabled = false;
            txtFileName.Text = "";
        }
        private void SetMyButtons() {
            btnPlay.IsEnabled = isPlaying;
            btnPause.IsEnabled = isPlaying;
            btnStop.IsEnabled = !isPlaying;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            //Haetaan käyttäjän vaikodialogilla valitsema tiedosto textboxiin.
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "d:\\H8672";
            ofd.Filter = "Video-tiedostot|*.mp4|All files|*.*";
            ofd.Title = "test";
            Nullable<bool> result = ofd.ShowDialog();
            if (result == true)
            {
                txtFileName.Text = ofd.FileName;
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((txtFileName.Text.Length > 0) && (System.IO.File.Exists(txtFileName.Text))) {
                    if (mediaElement.IsEnabled)
                    {
                        mediaElement.Pause();
                    }
                    else {
                        mediaElement.Source = new Uri(txtFileName.Text);
                        mediaElement.Play();
                        //Napit käyttöön
                        SetMyButtons();
                    }
                }
                else MessageBox.Show("Tiedostoa " + txtFileName.Text + " ei löydy.");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying) {
                mediaElement.Pause();
                isPlaying = false;
                btnPause.Content = "Continue";
                SetMyButtons();
            }
            else {
                mediaElement.Play();
                isPlaying = true;
                btnPause.Content = "Pause";
                SetMyButtons();
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            SetMyButtons();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //Close(); //Sulkee ikkunan ja siten sovelluksen. Käy pieniin sovelluksiin, joissa koodi ikkunan koodin sisällä.
            Application.Current.Shutdown(); //Sulkee sovelluksen
        }
        
    }
}
