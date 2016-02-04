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
using JAMK.IT.IIO11300;

namespace H3MittausData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Luodaan lista mittaus-olioita varten
        List<MittausData> mitatut;

        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }
        private void IniMyStuff()
        {
            //omat ikkunaan liittyvät alustukset
            txtToday.Text = DateTime.Today.ToShortDateString();
            mitatut = new List<MittausData>();
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            //luodaan uusi mittausdata olio ja näytetään se käyttäjälle
            MittausData md = new MittausData(txtClock.Text, txtData.Text);
            //MittausData md2 = new MittausData();
            //lbData.Items.Add(md);
            mitatut.Add(md);
            ApplyChanges();
        }
        private void ApplyChanges()
        {
            lbData.ItemsSource = null;
            lbData.ItemsSource = mitatut;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Kirjoitetaan mitatut tiedostoon
            try
            {
                MittausData.SaveToFileV2(txtFileName.Text, mitatut);
                MessageBox.Show("Tiedot kirjoitettu onnistuneesti tiedostoon" + txtFileName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            //Haetaan käyttäjän antamasta tiedostosta mitatut arvot
            try
            {
                mitatut = MittausData.ReadFromFile(txtFileName.Text);
                ApplyChanges();
                MessageBox.Show("Tiedot haettu onnistuneesti " + txtFileName.Text + " tiedostosta");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSerialize_Click(object sender, RoutedEventArgs e)
        {
            //kutsutaan serialisointia
            try
            {
                JAMK.IT.IIO11300.Serialisointi.SerialisoiXml(txtFileName.Text, mitatut);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeSerialize_Click(object sender, RoutedEventArgs e)
        {
            //Kutsutaan deserialisointia
            try
            {
                mitatut = JAMK.IT.IIO11300.Serialisointi.DeSerialisoiXml(txtFileName.Text);
                ApplyChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSerializeBin_Click(object sender, RoutedEventArgs e)
        {
            //kutsutaan serialisointia
            try
            {
                JAMK.IT.IIO11300.Serialisointi.Serialisoi(txtFileName.Text, mitatut);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeSerializeBin_Click(object sender, RoutedEventArgs e)
        {
            //Kutsutaan deserialisointia, binäärideserialisoija metodi palauttaa objectin
            try
            {
                object obj = new object();
                JAMK.IT.IIO11300.Serialisointi.DeSerialisoi(txtFileName.Text, ref obj);
                mitatut = (List<MittausData>)obj;
                ApplyChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
