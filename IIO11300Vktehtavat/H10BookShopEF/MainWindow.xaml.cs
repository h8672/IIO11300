using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; //for OvservableCollection
using System.ComponentModel;
using System.Data.Entity;
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

namespace H10BookShopEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookShopEntities ctx;
        ObservableCollection<Book> localBooks;
        ICollectionView view; //filtteröintiä varten
        bool IsBooks;
        public MainWindow()
        {
            InitializeComponent();
            initMyStuff();
        }

        private void initMyStuff()
        {
            //tänne kaikki tarvittavat alustukset
            ctx = new BookShopEntities();
            ctx.Books.Load();
            localBooks = ctx.Books.Local;
            //comboboxin täyttäminen kirjojen eri mailla
            cbCountries.DataContext = localBooks.Select(n => n.country).Distinct();
            cbCountries.Visibility = Visibility.Visible;
            //view kirjojen filtterointia varten
            view = CollectionViewSource.GetDefaultView(localBooks);
        }

        //BUTTONS
        private void btnHaeAsiakkaat_Click(object sender, RoutedEventArgs e)
        {
            var customers = ctx.Customers.ToList();
            myGrid.DataContext = customers;
            IsBooks = false;
        }

        private void btnHaeKirjat_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            myGrid.DataContext = localBooks;
            IsBooks = true;
            //cbCountries.SelectedItem = -1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //tallennetaan kirjaan tehdyt muutokset
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //luodaan uusi kirja-olio ensiksi contextiin ja sitten tietokantaan
            Book newBook;
            if(btnNew.Content.ToString() == "Uusi")
            {
                //luodaan uusi Book-olio
                newBook = new Book();
                newBook.name = "Anna kirjan nimi";
                spBook.DataContext = newBook;
                btnNew.Content = "Tallenna kirja kantaan";
            }
            else
            {
                //lisätään uusi kirja ensin konstekstiin ja sieltä kantaan
                newBook = (Book)spBook.DataContext;
                ctx.Books.Add(newBook);
                ctx.SaveChanges();
                btnNew.Content = "Uusi";
                MessageBox.Show(string.Format("Kirja {0} lisätty kantaan onnistuneesti", newBook.name));
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //poistetaan valittu kirja-olio kontekstiksi ja sitten kannasta
            Book current = (Book)spBook.DataContext;
            var retval = MessageBox.Show("Haluatko varmasti poistaa kirjan " + current.DisplayName + "?", "Wanhat kirjat kysyy", MessageBoxButton.YesNo);
            if(retval == MessageBoxResult.Yes)
            {
                ctx.Books.Remove(current);
                ctx.SaveChanges();
            }
        }
        private void btnHaeTilaukset_Click(object sender, RoutedEventArgs e)
        {
            //haetaan valitun asiakkaan tilaukset navigation properties avulla
            string msg = "";
            Customer current = (Customer)spCustomer.DataContext;
            msg += string.Format("Asiakkaalla {0} on {1} tilausta:\n", current.DisplayName, current.OrderCount);
            foreach (var item in current.Orders)
            {
                msg += string.Format("Tilaus {0} sisältää {1} tilausriviä:\n", item.odate, item.Orderitems.Count);
                //kunkin tilauksen rivit ja sitä vastaava kirja
                Decimal summa = 0;
                foreach (var oitem in item.Orderitems)
                {
                    msg += string.Format("- kirja {0} kappaletta {1}\n", oitem.Book.name, oitem.count);
                    summa += oitem.count * oitem.Book.price.Value;
                }
                msg += string.Format("-- Tilaus yhteensä {0} €\n", summa);
            }
            MessageBox.Show(msg);
        }

        //SELECTION CHANGES
        private void myGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsBooks)
            {
                spBook.DataContext = myGrid.SelectedItem;
            }
            else
            {
                //valittu item(tässä tapauksessa Customer-olio) asetetaan stackpanelin datacontextiksi
                spCustomer.DataContext = myGrid.SelectedItem;
            }
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //suodatetaan kirjat käyttäjän valinnan mukaan
            //suodatus tehdään ns. predikaatti-funktiolla
            view.Filter = MyCountryFilter;
        }

        private bool MyCountryFilter(object item)
        {
            if(cbCountries.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return (item as Book).country.Contains(cbCountries.SelectedItem.ToString());
            }
        }
    }
}
