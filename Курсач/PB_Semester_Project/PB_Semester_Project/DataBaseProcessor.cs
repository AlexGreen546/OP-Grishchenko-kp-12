using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace PB_Semester_Project
{
    public static class DataBaseProcessor
    {
        public static string connectionString = "Data Source=DESKTOP-1VDI1RQ;Initial Catalog=PB_Semester;Integrated Security=True";
        static SqlConnection connection;
        static DataBaseProcessor()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public static DataTable extractTable(String query)
        {
            DataTable table = new DataTable();
            new SqlDataAdapter(new SqlCommand(query, connection)).Fill(table);
            return table;
        }
        public static T extractCell<T>(String query)
        {
            DataTable table = new DataTable();
            new SqlDataAdapter(new SqlCommand(query, connection)).Fill(table);
            return (T)table.Rows[0][0];
        }
        public static void retrieveAndFill(string SQLQuery, DataGrid dataGrid)
        {
            var Table = DataBaseProcessor.extractTable(SQLQuery);

            dataGrid.ItemsSource = Table.DefaultView;
        }
        public static void executeQuery(String query)
        {
            new SqlCommand(query, connection).ExecuteNonQuery();
        }
    }
}
