using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H9BookshopORM
{
    public class DBBookshop
    {
        public static DataTable GetTestData()
        {
            DataTable dt = new DataTable();
            //sarakkeiden määrittely
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Author", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("Year", typeof(int));
            //rivit eli tietueet
            dt.Rows.Add(11, "Pekka Lipposen seikkaulut", "Outsider", "Suomi", 1950);
            dt.Rows.Add(12, "Lucky Luke", "René Coscinny", "Belgia", 1946);
            return dt;
        }
        public static DataTable GetBooks(string connStr)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT id, name, author, country, year FROM books";
                    //conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Books");
                    da.Fill(dt);
                    conn.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int UpdateBook(string connStr, int id, string name, string author, string country, int year)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //HUOM! käytetään SQL-kyselyn parametrejä kahdesta syystä:
                    //1: heittomerkki engl. aposrophe esim nimessä O'Hara
                    //2: tietoturva paramertisoidut kuselyt ovat tietoturvallisempia
                    string sql = String.Format("UPDATE books SET name=@Nimi, author=@Kirjailija, country=@Maa, year={1} WHERE id={0}",
                        id, year);
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //lisätään komennolla parametrit
                    SqlParameter sp;
                    sp = new SqlParameter("Nimi", SqlDbType.NVarChar);
                    sp.Value = name;
                    cmd.Parameters.Add(sp);
                    sp = new SqlParameter("Kirjailija", SqlDbType.NVarChar);
                    sp.Value = author;
                    cmd.Parameters.Add(sp);
                    sp = new SqlParameter("Maa", SqlDbType.NVarChar);
                    sp.Value = country;
                    cmd.Parameters.Add(sp);
                    int lkm = cmd.ExecuteNonQuery();
                    conn.Close();
                    return lkm;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int InsertBook(string connStr, string name, string author, string country, int year)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    //HUOM! käytetään SQL-kyselyn parametrejä kahdesta syystä:
                    //1: heittomerkki engl. aposrophe esim nimessä O'Hara
                    //2: tietoturva paramertisoidut kuselyt ovat tietoturvallisempia
                    string sql = String.Format("INSERT INTO books (name, author, country, year) VALUES (@Nimi, @Kirjailija, @Maa, @Vuosi)");
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //lisätään komennolla parametrit
                    cmd.Parameters.AddWithValue("Nimi", name);
                    cmd.Parameters.AddWithValue("Kirjailija", author);
                    cmd.Parameters.AddWithValue("Maa", country);
                    cmd.Parameters.AddWithValue("Vuosi", year);
                    int lkm = cmd.ExecuteNonQuery();
                    conn.Close();
                    return lkm;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int DeleteBook(string connStr, int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("DELETE FROM books WHERE id={0}", id), conn);
                    int lkm = cmd.ExecuteNonQuery();
                    conn.Close();
                    return lkm;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
