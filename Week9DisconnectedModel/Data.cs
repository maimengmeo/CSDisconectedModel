using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Week9DisconnectedModel
{
    public class Data
    {
        private static string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

        public static string ConnectionStr
        {
            get { return connStr; }
        }

        public DataTable GetAllProducts()
        {
            string query = "Select ProductID, ProductName, UnitPrice, UnitsInStock from Products";

            SqlConnection conn = new SqlConnection(Data.ConnectionStr);

            SqlDataAdapter adp = new SqlDataAdapter(query, conn);

            DataSet ds = new DataSet();

            adp.Fill(ds, "Products");

            DataTable tblProducts = ds.Tables["Products"];

            return tblProducts;
        }

    }
}
