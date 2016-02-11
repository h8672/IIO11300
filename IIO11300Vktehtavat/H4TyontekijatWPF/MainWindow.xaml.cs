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
using System.Xml;
using System.Xml.Linq;

namespace H4TyontekijatWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XElement xe;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReadXML_Click(object sender, RoutedEventArgs e)
        {
            //Luetaan XML-tiedostosta työntekijä elementit ja sidotaan ne DataGridiin
            try
            {
                string filename = "d:\\Työntekijät.xml";
                xe = XElement.Load(filename);
                dgData.DataContext = xe.Elements("tyontekija");
                //tyontekijat = XElement.Parse("vakituinen");
                //tyontekijat = xe
                tbMessage.Text = string.Format("Vakituisia työntekijöitä {0} ja palkat yhteensä {1}", CountWorkers("vakituinen"), CalculateSalarySum("vakituinen"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int CountWorkers()
        {
            int lkm = 0;
            //TODO
            //lkm = tyontekijat.Count;
            lkm = dgData.Columns.Count; //Columnien määrä...
            //lkm = dgData...; //Muuta?
            return lkm;
        }
        private int CountWorkers(string tyosuhde)
        {
            int lkm = 0;
            //Lasketaan LINQ-kyselyllä tietyntyyppiset työntekijät
            var temp = from ele in xe.Elements()
                       where ele.Element("tyosuhde").Value == tyosuhde
                       select ele.Element("Sukunimi");
            lkm = temp.Count();
            return lkm;
        }
        private decimal CalculateSalarySum()
        {
            decimal sum = 0;
            //TODO
            XmlNode xn;
            //for (int i = 0; i < tyontekijat.Count; i++)
            //{
            //    xn = tyontekijat.Item(i);
            //    sum += decimal.Parse(xn.LastChild.InnerText);
            //}
            return sum;
        }
        private decimal CalculateSalarySum(string tyosuhde)
        {
            decimal sum = 0;
            //TODO
            var palkat = from ele in xe.Elements()
                       where ele.Element("tyosuhde").Value == tyosuhde
                       select ele.Element("palkka");
            foreach (var item in palkat)
            {
                //System.Diagnostics.Debug.Print(item.ToString());
                sum += Convert.ToDecimal(item.Value);
            }
            return sum;
        }
    }
}
