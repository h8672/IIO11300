using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H9BookshopORM
{
    public class Book
    {
        #region PROPERTIES
        private int id;
        private string name;
        private string author;
        private string country;
        private int year;

        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        #endregion
        #region CONSTRUCTORS
        public Book(int ID)
        {
            this.id = ID;
        }
        public Book(int id, string name, string author, string country, int year)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.country = country;
            this.year = year;
        }
        #endregion
        #region METHODS
        public override string ToString()
        {
            return name + " written by " + author;
        }
        #endregion
    }
    public static class Bookshop
    {
        private static string cs = H9BookshopORM.Properties.Settings.Default.Kirjakauppa;
        public static List<Book> GetTestBooks()
        {
            List<Book> temp = new List<Book>();
            temp.Add(new Book(1, "Sota ja rauha", "Leo Tolstoi", "Venäjä", 1867));
            temp.Add(new Book(2, "Anna Karenina", "Leo Tolstoi", "Venäjä", 1877));
            return temp;
        }
        public static List<Book> GetBooks(bool useDB)
        {
            try
            {
                DataTable dt = new DataTable();
                List<Book> books = new List<Book>();
                if (useDB)
                {
                    //Pyydetään DB-kerrokselta kirjojen tiedot
                    dt = DBBookshop.GetBooks(cs);
                }
                else
                {
                    dt = DBBookshop.GetTestData();
                }
                //ORM = muutetaan datatablen rivit olioiksi
                Book book;
                foreach (DataRow row in dt.Rows)
                {
                    book = new Book((int)row[0]);
                    book.Name = row["name"].ToString();
                    book.Author = row["author"].ToString();
                    book.Country = row["country"].ToString();
                    book.Year = (int)row["year"];
                    books.Add(book);
                }
                //palautus
                return books;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int UpdateBook(Book book)
        {
            try
            {
                int lkm = DBBookshop.UpdateBook(cs, book.Id, book.Name, book.Author, book.Country, book.Year);
                return lkm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool InsertBook(Book book)
        {
            try
            {
                int lkm = DBBookshop.InsertBook(cs, book.Name, book.Author, book.Country, book.Year);
                if (lkm > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool DeleteBook(Book book)
        {
            try
            {
                int lkm = DBBookshop.DeleteBook(cs, book.Id);
                if (lkm > 0) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
