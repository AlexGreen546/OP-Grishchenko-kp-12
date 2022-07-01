using System;
using System.Collections.Generic;
using System.Data;
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

namespace PB_Semester_Project
{
    /// <summary>
    /// Interaction logic for Bases.xaml
    /// </summary>
    public partial class Bases : Window
    {
        public Bases()
        {
            InitializeComponent();
            initBases();
            initStores();
        }

        private void initStores()
        {
            string query = "SELECT StoreName FROM dbo.Stores";
            Utility.initializeComboBox(query, storesCombo);
        }

        private void initBases()
        {
            String query = "SELECT idBase as ID, BaseName as [Назва бази] FROM dbo.Bases";
            DataBaseProcessor.retrieveAndFill(query, basesGrid);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show(); 
        }

        private void chooseBase_Click(object sender, RoutedEventArgs e)
        {
            int selected = (int)((DataRowView)basesGrid.SelectedItem)["ID"];
            string query = $"SELECT dbo.Products.idProduct, dbo.Products.ProductName FROM dbo.Base_Product INNER JOIN                  dbo.Bases ON dbo.Base_Product.idBase = dbo.Bases.idBase INNER JOIN                  dbo.Products ON dbo.Base_Product.idProduct = dbo.Products.idProduct WHERE(dbo.Bases.idBase = {selected})";
            DataBaseProcessor.retrieveAndFill(query, baseProductsGrid);
        }

        private void storesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            storeDepartmentCombo.Items.Clear();
            string selectedStore = storesCombo.SelectedItem.ToString();
            string query = $"SELECT dbo.Departments.DepartmentName FROM dbo.Departments RIGHT OUTER JOIN                   dbo.Store_Department ON dbo.Departments.idDepartment = dbo.Store_Department.idDepartment RIGHT OUTER JOIN dbo.Stores ON dbo.Store_Department.idStore = dbo.Stores.idStore WHERE(dbo.Stores.StoreName = '{selectedStore}')";
            Utility.initializeComboBox(query, storeDepartmentCombo);
        }

        private void makeOrder_Click(object sender, RoutedEventArgs e)
        {
            string selectedStore = storesCombo.SelectedItem.ToString();
            int selectedStoreId = DataBaseProcessor.extractCell<int>($"SELECT idStore FROM Stores WHERE Stores.StoreName = '{selectedStore}'");

            string selectedDepartment = storeDepartmentCombo.SelectedItem.ToString();
            int selectedDepartmentId = DataBaseProcessor.extractCell<int>($"SELECT idDepartment FROM Departments WHERE DepartmentName = '{selectedDepartment}';");

            int selectedProductId = (int)((DataRowView)baseProductsGrid.SelectedItem)["idProduct"];

            int amount = int.Parse(amountOfProduct.Text);

            int totalPrice = amount * int.Parse(pricePerUnit.Text);

            int temp = DataBaseProcessor.extractCell<int>($"SELECT COUNT(Store_Product.amount) FROM Store_Product WHERE idStore = {selectedStoreId} AND idDepartment = {selectedDepartmentId} AND idProduct = {selectedProductId};");

            if(temp == 0)
            {
                DataBaseProcessor.executeQuery($"INSERT INTO Store_Product VALUES ({selectedStoreId}, {selectedDepartmentId}, {selectedProductId}, {amount});");
            }
            else
            {
                int prevAmount = DataBaseProcessor.extractCell<int>($"SELECT amount FROM Store_Product WHERE idStore = '{selectedStoreId}' AND idDepartment = {selectedDepartmentId} AND idProduct = {selectedProductId}");

                DataBaseProcessor.executeQuery($"UPDATE Store_Product SET idStore = {selectedStoreId}, idDepartment = {selectedDepartmentId}, idProduct = {selectedProductId}, amount = {prevAmount + amount} WHERE idStore = {selectedStoreId} AND idDepartment = {selectedDepartmentId} AND idProduct = {selectedProductId}");
            }

            int purchases = DataBaseProcessor.extractCell<int>($"SELECT COUNT(amount) FROM Store_Purchases WHERE idStore = '{selectedStoreId}' AND idDepartment = '{selectedDepartmentId}' AND idProduct = '{selectedProductId}';");

            if(purchases == 0)
            {
                DataBaseProcessor.executeQuery($"INSERT INTO Store_Purchases VALUES ({selectedStoreId}, {selectedDepartmentId}, {selectedProductId}, {amount}, {totalPrice});");
            }
            else
            {
                int prevAmount = DataBaseProcessor.extractCell<int>($"SELECT amount FROM Store_Purchases WHERE idStore = {selectedStoreId} AND idDepartment = {selectedDepartmentId} AND idProduct = {selectedProductId}");
                int prevPrice = DataBaseProcessor.extractCell<int>($"SELECT price FROM Store_Purchases WHERE idStore = {selectedStoreId} AND idDepartment = {selectedDepartmentId} AND idProduct = {selectedProductId}");

                DataBaseProcessor.executeQuery($"UPDATE Store_Purchases SET amount = {prevAmount + amount}, price = {prevPrice + totalPrice} WHERE idStore = {selectedStoreId} AND idDepartment = {selectedDepartmentId} AND idProduct = {selectedProductId};");
            }
        }
    }
}
