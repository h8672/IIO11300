using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JAMK.ICT.Data
{
  public static class DBPlacebo
  {
    public static DataTable GetTestCustomers()
	{
    //create
    DataTable dt = new DataTable();
    //columns
    dt.Columns.Add("asioid",typeof(string));
    dt.Columns.Add("LastName",typeof(string));
    dt.Columns.Add("FirstName", typeof(string));
    //rows
    dt.Rows.Add("A3581", "Waltari","Mika");
	  dt.Rows.Add("B3553", "King", "Stephen");
	  dt.Rows.Add("C1238", "Neruda", "Pablo");
	  dt.Rows.Add("D9876", "Oksanen", "Sofi");
	return dt;
	}
    public static DataTable GetAllCustomersFromSQLServer(string connectionStr, string taulu, string cityFilter, out string viesti)
    {
        // basic principle: connect - execute query - disconnect
        try
        {
            SqlConnection myConn = new SqlConnection(connectionStr);
            myConn.Open();
            string sql = "";
            if( cityFilter != "")
            {
                sql = string.Format("SELECT * FROM {0} WHERE city like '{1}'", taulu, cityFilter);
            }
            else
            {
                sql = "SELECT * FROM " + taulu;
            }
            SqlCommand cmd = new SqlCommand(sql, myConn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, taulu);
            viesti = "Tiedot haettu onnistuneesti tietokannasta " + myConn.DataSource;
            return ds.Tables[taulu];
        }
        catch (Exception ex)
        {
            viesti = ex.Message;
            throw;
        }
    }
    public static DataTable GetCitiesFromSQLServer(string connectionStr, string taulu)
    {
        // basic principle: connect - execute query - disconnect
        try
        {
            SqlConnection myConn = new SqlConnection(connectionStr);
            myConn.Open();
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT city FROM " + taulu, myConn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, taulu);
            return ds.Tables[taulu];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
  }
}
