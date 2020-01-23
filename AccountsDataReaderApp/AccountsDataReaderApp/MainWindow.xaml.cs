using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //                        GetListOfTables();
            //            ExportToExcel();
            GetFieldNamesFromTable();

            //            CreateDynamicDataTable();

        }

        private void CreateDynamicDataTable()
        {
            //            string constr1 = @"Provider=vfpoledb;Data Source=\\172.20.10.25\Server VFP Static and Dynamic\data;Extended Properties=dBASE III;";
            string constr1 = @"Provider=vfpoledb;Data Source=C:\Globeweigh\OperaData;Extended Properties=dBASE III;";
            using (OleDbConnection con = new OleDbConnection(constr1))
            {
                con.Open();


                //Cust Pricing
                //                OleDbCommand command = new OleDbCommand("Select * from  1_CDETL.DBF", con);
                //Products
                //                                OleDbCommand command = new OleDbCommand("Select * from 1_CNAME.DBF", con);
                //Customers
                //                                OleDbCommand command =new OleDbCommand("Select * from 1_SNAME.DBF WHERE sn_dormant = 0", con);
                //
                OleDbCommand command = new OleDbCommand("Select * from  1_STERMS.DBF", con);



                OleDbDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                DataGrid.ItemsSource = dataTable.DefaultView;

            }


        }

        private void GetListOfTables()
        {

            // Microsoft Access provider factory
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");

            DataTable userTables = null;
            using (DbConnection connection = factory.CreateConnection())
            {
                // c:\test\test.mdb
                connection.ConnectionString = @"Provider=vfpoledb;Data Source=c:\Globeweigh\OperaData;Extended Properties=dBASE III;";
                // We only want user tables, not system tables
                string[] restrictions = new string[4];
                restrictions[3] = "Table";

                connection.Open();

                // Get list of user tables
                userTables = connection.GetSchema("Tables", restrictions);
            }

            List<string> tableNames = new List<string>();
            for (int i = 0; i < userTables.Rows.Count; i++)
                tableNames.Add(userTables.Rows[i][2].ToString());

        }

        private void GetFieldNamesFromTable()
        {
            OleDbConnection cn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            DataTable schemaTable;
            OleDbDataReader myReader;

            //            cn.ConnectionString = @"Provider=vfpoledb;Data Source=C:\Globeweigh\Clonakilty\OperaData;Extended Properties=dBASE III;";
            cn.ConnectionString = @"Provider=vfpoledb;Data Source=D:\Github\McCormacks\trunk\Documents\IntactData;Extended Properties=dBASE III;";
            cn.Open();

            cmd.Connection = cn;
            cmd.CommandText = @"Select * from SORDITEM.DBF";
            myReader = cmd.ExecuteReader(CommandBehavior.KeyInfo);

            //Retrieve column schema into a DataTable.
            schemaTable = myReader.GetSchemaTable();


            //For each field in the table...
            foreach (DataRow myField in schemaTable.Rows)
            {
                //For each property of the field...
                foreach (DataColumn myProperty in schemaTable.Columns)
                {
                    //Display the field name and value.
                    if (myProperty.ColumnName == "DataType" || myProperty.ColumnName == "ColumnName")
                    {
                        Console.WriteLine(myProperty.ColumnName + " = " + myField[myProperty].ToString());
                    }

                }
                Console.WriteLine();

                //Pause.
                Console.ReadLine();
            }

            //Always close the DataReader and connection.
            myReader.Close();
            cn.Close();
        }

        public void ExportToExcel()
        {

            string constr1 = @"Provider=vfpoledb;Data Source=C:\Globeweigh\OperaData;Extended Properties=dBASE III;";
            using (OleDbConnection con = new OleDbConnection(constr1))
            {
                con.Open();

                OleDbCommand command = new OleDbCommand("Select * from 1_SNAME.DBF WHERE sn_dormant = 0", con);
                OleDbDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                //                var item = reader.GetString(reader.GetOrdinal("Location"));


                var lines = new List<string>();

                string[] columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();

                var header = string.Join(",", columnNames);
                lines.Add(header);

                var valueLines = dataTable.AsEnumerable()
                                   .Select(row => string.Join(",", row.ItemArray));
                lines.AddRange(valueLines);

                File.WriteAllLines(@"C:\Globeweigh\Clonakilty\Customers.csv", lines);

            }
        }


        public void ExportClonakiltyDataToExcel()
        {

            string constr1 = @"Provider = Microsoft.JET.OLEDB.4.0; " + @"data source = C:\Globeweigh\OperaData.mdb;";
            using (OleDbConnection con = new OleDbConnection(constr1))
            {
                con.Open();

                OleDbCommand command = new OleDbCommand("Select * from tblInvoices", con);
                OleDbDataReader reader = command.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                //                var item = reader.GetString(reader.GetOrdinal("Location"));


                var lines = new List<string>();

                string[] columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();

                var header = string.Join(",", columnNames);
                lines.Add(header);

                var valueLines = dataTable.AsEnumerable()
                                   .Select(row => string.Join(",", row.ItemArray));
                lines.AddRange(valueLines);

                File.WriteAllLines(@"c:\Neil\tblInvoices.csv", lines);

            }
        }


    }
}
