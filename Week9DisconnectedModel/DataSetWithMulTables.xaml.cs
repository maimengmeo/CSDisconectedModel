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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Week9DisconnectedModel
{
    /// <summary>
    /// Interaction logic for DataSetWithMulTables.xaml
    /// </summary>
    public partial class DataSetWithMulTables : Window
    {
        public DataSetWithMulTables()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string query = "Select * from Categories; " +
                            "Select * from Products"; //mul select queries can be done

            SqlConnection conn = new SqlConnection(Data.ConnectionStr);
            SqlDataAdapter adp = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();

            adp.Fill(ds); //cant set table name here bc mul select queries, 
                          //no opt to provide second table name

            ds.Tables[0].TableName = "Categories";
            ds.Tables[1].TableName = "Products";

            DataTable tblCategories = ds.Tables[0];//untype data set, default name, have to set manually using tablename as above
            DataTable tblProducts = ds.Tables[1];

            grdCategories.ItemsSource = tblCategories.DefaultView;
            grdProducts.ItemsSource = tblProducts.DefaultView;
        }
    }
}
