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
    /// Interaction logic for Departments.xaml
    /// </summary>
    public partial class Departments : Window
    {
        public Departments()
        {
            InitializeComponent();
            initStores();

            Utility.initializeComboBox("SELECT DepartmentName FROM Departments", addDepartmentCombo);
        }
        private void initStores()
        {
            string query = "SELECT StoreName FROM dbo.Stores";
            Utility.initializeComboBox(query, storesCombo);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MainWindow().Show();
        }

        private void storesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            initDepartments();
            profit.Content = "";
        }

        private void initPurchases()
        {
            int storeId = DataBaseProcessor.extractCell<int>($"SELECT idStore FROM Stores WHERE StoreName = '{storesCombo.SelectedItem}'");
            int departmentId = DataBaseProcessor.extractCell<int>($"SELECT idDepartment FROM Departments WHERE Departments.DepartmentName = '{chosenDepartment.Content}'");

            string query = $"SELECT dbo.Products.ProductName, dbo.Store_Purchases.amount, dbo.Store_Purchases.price AS 'Total price' FROM dbo.Products RIGHT OUTER JOIN dbo.Store_Purchases ON dbo.Products.idProduct = dbo.Store_Purchases.idProduct WHERE Store_Purchases.idStore = {storeId} AND Store_Purchases.idDepartment = {departmentId}";

            DataBaseProcessor.retrieveAndFill(query, purchasesGrid);
        }

        private void initSales()
        {
            int storeId = DataBaseProcessor.extractCell<int>($"SELECT idStore FROM Stores WHERE StoreName = '{storesCombo.SelectedItem}'");
            int departmentId = DataBaseProcessor.extractCell<int>($"SELECT idDepartment FROM Departments WHERE Departments.DepartmentName = '{chosenDepartment.Content}'");

            string query = $"SELECT dbo.Products.ProductName, dbo.Store_Sales.amount, dbo.Store_Sales.price as 'Total price' FROM dbo.Store_Sales LEFT OUTER JOIN dbo.Products ON dbo.Store_Sales.idProduct = dbo.Products.idProduct WHERE Store_Sales.idStore = {storeId} AND Store_Sales.idDepartment = {departmentId}";

            DataBaseProcessor.retrieveAndFill(query, salesGrid);
        }

        private void initDepartments()
        {
            string query = $"SELECT dbo.Departments.DepartmentName AS Відділ, dbo.Store_Department.DirectorName AS Завідуючий FROM dbo.Store_Department INNER JOIN dbo.Stores ON dbo.Store_Department.idStore = dbo.Stores.idStore INNER JOIN dbo.Departments ON dbo.Store_Department.idDepartment = dbo.Departments.idDepartment WHERE(dbo.Stores.StoreName = '{storesCombo.SelectedItem}')";
            DataBaseProcessor.retrieveAndFill(query, departmentsGrid);
        }

        private void initProductsOfDepartments()
        {
            string selectedDepartment = (string)((DataRowView)departmentsGrid.SelectedItem)["Відділ"];
            int selectedDepartmentId = DataBaseProcessor.extractCell<int>($"SELECT idDepartment FROM Departments WHERE Departments.DepartmentName = '{selectedDepartment}'");

            int storeId = DataBaseProcessor.extractCell<int>($"SELECT idStore FROM Stores WHERE StoreName = '{storesCombo.SelectedItem}'");

            string query = $"SELECT dbo.Products.ProductName as 'Product name', Store_Product.amount as 'Amount' FROM dbo.Store_Product INNER JOIN dbo.Stores ON dbo.Store_Product.idStore = dbo.Stores.idStore INNER JOIN dbo.Products ON dbo.Store_Product.idProduct = dbo.Products.idProduct WHERE(dbo.Store_Product.idDepartment = {selectedDepartmentId}) AND(dbo.Store_Product.idStore = {storeId})";

            DataBaseProcessor.retrieveAndFill(query, productsGrid);
        }

        private void chooseDepartment_Click(object sender, RoutedEventArgs e)
        {
            initProductsOfDepartments();
            chosenProduct.Content = "";
            chosenDepartment.Content = (string)((DataRowView)departmentsGrid.SelectedItem)["Відділ"];
            initSales();
            initPurchases();
            calculateProfit();
        }

        private void chooseProduct_Click(object sender, RoutedEventArgs e)
        {
            chosenProduct.Content = (string)((DataRowView)productsGrid.SelectedItem)["Product name"];          
        }

        private void deleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            int departmentId = DataBaseProcessor.extractCell<int>($"SELECT idDepartment FROM Departments WHERE Departments.DepartmentName = '{chosenDepartment.Content}'");

            int storeId = DataBaseProcessor.extractCell<int>($"SELECT idStore FROM Stores WHERE StoreName = '{storesCombo.SelectedItem}'");

            DataBaseProcessor.executeQuery($"DELETE FROM Store_Department WHERE idStore = {storeId} AND idDepartment = {departmentId}");

            initDepartments();
            chosenDepartment.Content = "";
            chooseProduct.Content = "";
        }

        private void addDepartment_Click(object sender, RoutedEventArgs e)
        {
            string departmentName = addDepartmentCombo.SelectedItem.ToString();
            int departmentId = DataBaseProcessor.extractCell<int>($"SELECT idDepartment FROM Departments WHERE Departments.DepartmentName = '{departmentName}'");
            string director = directorTextbox.Text;
            int storeId = DataBaseProcessor.extractCell<int>($"SELECT idStore FROM Stores WHERE StoreName = '{storesCombo.SelectedItem}'");

            if(DataBaseProcessor.extractCell<int>($"SELECT COUNT(idStore) FROM Store_Department WHERE idStore = {storeId} AND idDepartment = {departmentId}") == 0)
            {
                DataBaseProcessor.executeQuery($"INSERT INTO Store_Department VALUES ({storeId}, {departmentId}, '{director}')");
            }
            initDepartments();
        }

        private void makeSale_Click(object sender, RoutedEventArgs e)
        {
            int departmentId = DataBaseProcessor.extractCell<int>($"SELECT idDepartment FROM Departments WHERE Departments.DepartmentName = '{chosenDepartment.Content}'");

            int storeId = DataBaseProcessor.extractCell<int>($"SELECT idStore FROM Stores WHERE StoreName = '{storesCombo.SelectedItem}'");

            int productId = DataBaseProcessor.extractCell<int>($"SELECT idProduct FROM Products WHERE ProductName = '{chosenProduct.Content}'");

            int temp = DataBaseProcessor.extractCell<int>($"SELECT COUNT(idStore) FROM Store_Sales WHERE idStore = {storeId} AND idDepartment = {departmentId} AND idProduct = {productId}");

            if(temp == 0)
                DataBaseProcessor.executeQuery($"INSERT INTO Store_Sales VALUES ({storeId}, {departmentId}, {productId}, {saleAmount.Text}, {int.Parse(saleAmount.Text) * int.Parse(salePrice.Text)})");
            else
            {
                int oldAmount = DataBaseProcessor.extractCell<int>($"SELECT amount FROM Store_Sales WHERE idStore = {storeId} AND idDepartment = {departmentId} AND idProduct = {productId}");
                int oldTotalPrice = DataBaseProcessor.extractCell<int>($"SELECT price FROM Store_Sales WHERE idStore = {storeId} AND idDepartment = {departmentId} AND idProduct = {productId}");

                DataBaseProcessor.executeQuery($"UPDATE Store_Sales SET amount = {oldAmount + int.Parse(saleAmount.Text)}, price = {oldTotalPrice + (int.Parse(saleAmount.Text) * int.Parse(salePrice.Text))} WHERE idStore = {storeId} AND idDepartment = {departmentId} AND idProduct = {productId}");
            }

            int oldInStockAmount = DataBaseProcessor.extractCell<int>($"SELECT amount FROM Store_Product WHERE idStore = {storeId} AND idDepartment = {departmentId} AND idProduct = {productId}");

            DataBaseProcessor.executeQuery($"UPDATE Store_Product SET amount = {oldInStockAmount - int.Parse(saleAmount.Text)} WHERE idStore = {storeId} AND idDepartment = {departmentId} AND idProduct = {productId}");

            initSales();
            initProductsOfDepartments();
            calculateProfit();
        }

        private void calculateProfit()
        {
            int storeId = DataBaseProcessor.extractCell<int>($"SELECT idStore FROM Stores WHERE StoreName = '{storesCombo.SelectedItem}'");
            int departmentId = DataBaseProcessor.extractCell<int>($"SELECT idDepartment FROM Departments WHERE Departments.DepartmentName = '{chosenDepartment.Content}'");

            string query = $"SELECT dbo.Products.ProductName, dbo.Store_Purchases.amount, dbo.Store_Purchases.price FROM dbo.Products RIGHT OUTER JOIN dbo.Store_Purchases ON dbo.Products.idProduct = dbo.Store_Purchases.idProduct WHERE Store_Purchases.idStore = {storeId} AND Store_Purchases.idDepartment = {departmentId}";

            var purchases = DataBaseProcessor.extractTable(query);

            query = $"SELECT dbo.Products.ProductName, dbo.Store_Sales.amount, dbo.Store_Sales.price FROM dbo.Store_Sales LEFT OUTER JOIN dbo.Products ON dbo.Store_Sales.idProduct = dbo.Products.idProduct WHERE Store_Sales.idStore = {storeId} AND Store_Sales.idDepartment = {departmentId}";

            var sales = DataBaseProcessor.extractTable(query);

            int totalSpent = 0;
            for(int i = 0; i < purchases.Rows.Count; i++)
            {
                totalSpent += (int)purchases.Rows[i]["price"];
            }

            int totalEarned = 0;
            for(int j = 0; j < sales.Rows.Count; j++)
            {
                totalEarned += (int)sales.Rows[j]["price"];
            }

            profit.Content = totalEarned - totalSpent;
        }
    }
}
