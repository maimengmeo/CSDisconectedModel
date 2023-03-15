using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Week9DisconnectedModel
{
    class CrudOp
    {
        private SqlConnection conn;
        private SqlDataAdapter adp; //connection btw app & database
        private SqlCommandBuilder cmdBuilder;
        private DataSet ds;
        private DataTable tblProducts;

        public CrudOp()
        {
            string query = "Select ProductID, ProductName, UnitPrice, UnitsInStock from Products";

            conn = new SqlConnection(Data.ConnectionStr);
            adp = new SqlDataAdapter(query, conn);
            cmdBuilder = new SqlCommandBuilder(adp);
            ds = new DataSet();
        }

        private void FillDataSet() //refresh dataset after crud
        {
            adp.Fill(ds, "Products");
            tblProducts = ds.Tables[0];
        }
    }
}
