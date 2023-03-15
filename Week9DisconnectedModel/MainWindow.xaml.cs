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
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;

namespace Week9DisconnectedModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Data data = new Data();
        private CrudOp crud = new CrudOp();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadAllProducts_Click(object sender, RoutedEventArgs e)
        {
            //grdProducts.ItemsSource = data.GetAllProducts().DefaultView;
            grdProducts.ItemsSource = crud.GetAllProducts().DefaultView;
        }

        private void btnShowWindow2_Click(object sender, RoutedEventArgs e)
        {
            DataSetWithMulTables win2 = new DataSetWithMulTables();
            win2.Show();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);

            DataRow row = crud.GetProductById(id);

            if(row != null)
            {
                txtName.Text = row["ProductName"].ToString();
                txtPrice.Text = row["UnitPrice"].ToString();
                txtQuantity.Text = row["UnitsInStock"].ToString();
            } else
            {
                MessageBox.Show("Invalid ID. Please Try Again!");
                txtName.Text = txtPrice.Text = txtQuantity.Text = "";
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            short qty = short.Parse(txtQuantity.Text);

            crud.InsertProduct(name, price, qty);

            grdProducts.ItemsSource = crud.GetAllProducts().DefaultView;

            MessageBox.Show("Product is Added!");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            short qty = short.Parse(txtQuantity.Text);

            crud.UpdateProduct(id, name, price, qty);

            grdProducts.ItemsSource = crud.GetAllProducts().DefaultView;

            MessageBox.Show("Product is Updated!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);

            crud.DeleteProduct(id);

            grdProducts.ItemsSource = crud.GetAllProducts().DefaultView;

            MessageBox.Show("Product is Deleted!");

            //if delete original data, will get constrain, have to delete from DB first then in local row
        }
    }
}
