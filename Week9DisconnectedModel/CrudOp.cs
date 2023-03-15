using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Week9DisconnectedModel
{
    //only need to select, the others will be auto generated
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
            //ds = new DataSet();

            FillDataSet();
        }

        private void FillDataSet() //refresh dataset after crud
        {
            ds = new DataSet();

            adp.Fill(ds, "Products");
            tblProducts = ds.Tables[0];

            //define PK
            DataColumn[] pk = new DataColumn[1];
            pk[0] = tblProducts.Columns["ProductID"];
            pk[0].AutoIncrement = true; //set this up for local copy
            tblProducts.PrimaryKey = pk;
        }

        public DataTable GetAllProducts()
        {
            FillDataSet();
            return tblProducts;
        }

        public DataRow GetProductById(int id)
        {
            DataRow row = tblProducts.Rows.Find(id);
            return row;

        }

        public void InsertProduct(string name, decimal price, short quantity) //these type should match with the DB. This case it's money & smallint == decimal & short in c#
        {
            DataRow newRow = tblProducts.NewRow();
            newRow["ProductName"] = name;
            newRow["UnitPrice"] = price;
            newRow["UnitsInStock"] = quantity;

            tblProducts.Rows.Add(newRow);

            adp.InsertCommand = cmdBuilder.GetInsertCommand();
            
            adp.Update(tblProducts);
        }

        public void UpdateProduct(int id,string name, decimal price, short quantity) //these type should match with the DB. This case it's money & smallint == decimal & short in c#
        {
            DataRow row = tblProducts.Rows.Find(id);
            row["ProductName"] = name;
            row["UnitPrice"] = price;
            row["UnitsInStock"] = quantity;

            adp.UpdateCommand = cmdBuilder.GetUpdateCommand();

            adp.Update(tblProducts);
        }

        public void DeleteProduct(int id) //these type should match with the DB. This case it's money & smallint == decimal & short in c#
        {
            DataRow row = tblProducts.Rows.Find(id);

            row.Delete();

            adp.DeleteCommand = cmdBuilder.GetDeleteCommand();

            adp.Update(tblProducts);
        }

    }
}
