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

namespace H9BookshopORM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string name;

        public MainWindow()
        {
            InitializeComponent();
            InitInterface();
        }

        private void InitInterface()
        {

        }

        private void btnHaeTesti_Click(object sender, RoutedEventArgs e)
        {
            myGrid.DataContext = Bookshop.GetTestBooks();
        }

        private void btnHaeSQL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myGrid.DataContext = Bookshop.GetBooks(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Book current = (Book)spBook.DataContext;
                if(Bookshop.UpdateBook(current) > 0)
                {
                    MessageBox.Show(string.Format("Kirja {0} päivitetty tietokantaa onnustuneesti", current.ToString()));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(btnNew.Content.ToString() == "Uusi")
                {
                    Book newBook = new Book(0);
                    newBook.Name = "Anna kirjan nimi";
                    spBook.DataContext = newBook;
                    btnNew.Content = "Tallenna uusi kantaan";
                }
                else
                {
                    //Tallennetaan
                    Book current = (Book)spBook.DataContext;
                    Bookshop.InsertBook(current);
                    myGrid.DataContext = Bookshop.GetBooks(true);
                    MessageBox.Show(string.Format("Kirja {0} tallennettu kantaan onnistuneesti", current.ToString()));
                    btnNew.Content = "Uusi";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Poistetaan valittu kirja
                Book current = (Book)spBook.DataContext;
                var retval = MessageBox.Show("Haluatko varmasti poistaa kirjan " + current.ToString(), "Wanhat kirjat kysyy", MessageBoxButton.YesNo);
                if(retval == MessageBoxResult.Yes)
                {
                    Bookshop.DeleteBook(current);
                    myGrid.DataContext = Bookshop.GetBooks(true);
                    MessageBox.Show(string.Format("Kirja {0} poistettu kannasta onnistuneesti", current.ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void myGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spBook.DataContext = myGrid.CurrentItem;// (myGrid.SelectedIndex);
        }
    }
}
