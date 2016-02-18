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
using System.Windows.Shapes;
using System.Xml;

namespace H5Movies
{
    /// <summary>
    /// Interaction logic for Movies2.xaml
    /// </summary>
    public partial class Movies2 : Window
    {
        public Movies2()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Haetaan XmlDataProviderin XML-tiedoston nimi
            string file = xdpMovies.Source.LocalPath;
            //Tallennetaan raakasti XmlDocument olemassaolevan XML-tiedoston päälle!
            xdpMovies.Document.Save(file);
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //Asetetaan textboxit viittaamaan pois datasta eli lista ei ole valittuna
            if(lbMovies.SelectedIndex >= 0)
            {
                lbMovies.SelectedIndex = -1;
            }
            else
            {
                try
                {
                    //Lisätään uusi noodi XML-tiedostoon
                    string file = xdpMovies.Source.LocalPath;
                    //Viittaus XmlDocumenttiin ja sen juurielementtiin
                    XmlDocument doc = xdpMovies.Document;
                    XmlNode root = doc.SelectSingleNode("/Movies");
                    //Luodaan uusi node
                    XmlNode newMovie = doc.CreateElement("Movie");
                    //Lisätään nodelle tarvittavat attribuutit
                    XmlAttribute xa1 = doc.CreateAttribute("Name");
                    xa1.Value = txtName.Text;
                    newMovie.Attributes.Append(xa1);
                    XmlAttribute xa2 = doc.CreateAttribute("Director");
                    xa2.Value = txtDirector.Text;
                    newMovie.Attributes.Append(xa2);
                    XmlAttribute xa3 = doc.CreateAttribute("Country");
                    xa3.Value = txtCountry.Text;
                    newMovie.Attributes.Append(xa3);
                    XmlAttribute xa4 = doc.CreateAttribute("Checked");
                    xa4.Value = chkChecked.IsChecked.ToString();
                    newMovie.Attributes.Append(xa4);
                    //Lisätään noodi juureen
                    root.AppendChild(newMovie);
                    xdpMovies.Document.Save(file);
                    MessageBox.Show("Uusi elokuva lisätty!");
                    //Done

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    btnCreate.Content = "Lisää uusi";
                }
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Poistetaan käyttäjän valitsema elokuva --> Poistettava node haetaan Name-attribuutin avulla
            try
            {
                string file = xdpMovies.Source.LocalPath;
                XmlDocument doc = xdpMovies.Document;
                XmlNode root = doc.SelectSingleNode("/Movies");
                XmlNode poistettava = null;
                //Haetaan XPathilla poistettava node
                var item = doc.SelectSingleNode(string.Format("/Movies/Movie[@Name='{0}']", txtName.Text));
                if((item != null) && (MessageBox.Show("Poistetaanko " + txtName.Text, "Elokuva", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    //MessageBox.Show("Poistettu!");
                    poistettava = item;
                }
                if(poistettava != null)
                {
                    //Poistetaan noodi juuresta
                    root.RemoveChild(poistettava);
                    xdpMovies.Document.Save(file);
                    //Listboxin osoitin
                    lbMovies.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
