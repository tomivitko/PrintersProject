using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace PrintersProject
{
    class Database
    {
        //private static readonly string connectionString = "Server=127.0.0.1;Port=3307;Database=printers_db;Uid=root;Pwd=root;";
        static string connectionStringMDB = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=printers_db.mdb";
        /**
         *Dohvaća sve tipove printera iz baze podataka. 
        */
        public static DataTable getPrinterTypes()
        {
            DataTable dt = new DataTable();
            string query = @"select * from printer_type"; // set query to fetch data "Select * from  tabelname"; 
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                conn.Open();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    adapter.Fill(dt); 
                }
                
            }
            return dt;
        }

        /**
         *Dohvaća sve proizvođače printera iz baze podataka. 
        */
        public static DataTable getManufacturers()
        {
            DataTable dt = new DataTable();
            string query = @"select * from manufacturers"; // set query to fetch data "Select * from  tabelname"; 
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                conn.Open();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        /**
         * Dohvaća popis materijala u bazi podataka
         * 
         */
        public static List<Supplie> getSupplies()
        {
            List<Supplie> suppliesList = new List<Supplie>();
            string query = @"select * from supplies";
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        String name = reader.GetString(1);
                        Supplie supplie  =new Supplie(id, name);
                        suppliesList.Add(supplie);
                    }
                }
            }
            return suppliesList;
        }
        /**
         * Dohvaća popis materijala u bazi podataka za određeni printer
         * 
         */
        public static List<Supplie> getPrinterSupplies(int id_printer)
        {
            
            List<Supplie> suppliesList = new List<Supplie>();
            string query = @"SELECT supplies.id_supplie, supplies.supplie_name
                        FROM supplies INNER JOIN printer_supplies ON supplies.id_supplie = printer_supplies.id_supplie
                        WHERE printer_supplies.id_printer = @id_printer;";

            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_printer", id_printer);
                    conn.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        int id = reader.GetInt32(0);
                        String name = reader.GetString(1);
                        Supplie supplie = new Supplie(id, name);
                        suppliesList.Add(supplie);
                    }
                }
            }
            return suppliesList;
        }
        /*
         * Dohvaća sve printere iz baze podataka
         * @return List
         */
        public static List<Printer> getAllPrinters()
        {
            List<Printer> printersList = new List<Printer>();
            string query = @"SELECT printers.id_printer,
                            printers.printer_mark,
                            printer_type.printer_type,
                            manufacturers.manufacturer_name,
                            printers.model,
                            departments.department_name
                            FROM printer_type INNER JOIN (manufacturers INNER JOIN (departments INNER JOIN printers 
                            ON departments.id_department = printers.id_department) ON manufacturers.id_manufacturer = printers.id_manufacturer)
                            ON printer_type.id_printer_type = printers.id_printer_type;";
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        String mark = reader.GetString(1);
                        String type = reader.GetString(2);
                        String name = reader.GetString(3);
                        String model = reader.GetString(4);
                        String department = reader.GetString(5);
                        
                        Printer printer = new Printer(id, mark, type, name, model, department);
                        printersList.Add(printer);
                    }
                }
            }
            return printersList;
        }
        /*
         * Dohvaća sve odjele iz baze podataka
         * @return List
         */
        public static List<Department> getAllDepartments()
        {
            List<Department> departmentsList = new List<Department>();
            string query = @"select * from departments;";
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        String name = reader.GetString(1);

                        Department department = new Department(id, name);
                        departmentsList.Add(department);
                    }
                }
            }
            return departmentsList;
        }
        /*
         *Briše printer iz baze podataka 
         *
        */
        public static int deletePrinter(int id_printer)
        {
            int addedRecords = 0;
            string query = @"delete from printers where id_printer = @id_printer;";
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_printer", id_printer);
                    addedRecords = cmd.ExecuteNonQuery();
                }
            }
            return addedRecords;
        }
        /*
         *Zadužuje materijal i količinu printeru
        */
        public static int addSupplieToPrinter(Supplie supplie, int quantity, Printer printer)
        {
            int addedRecords = 0;
            string query = @"insert into supplies_distribution(id_printer, id_supplie, quantity, dis_date)
                        values(@id_printer, @id_supplie, @quantity, Date());";
            string query2 = @"insert into warehouse(id_supplie, quantity, dis_date)
                        values (@id_supplie, -@quantity, Date());";
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_printer", printer.id);
                    cmd.Parameters.AddWithValue("@id_supplie", supplie.id);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    //cmd.Parameters.AddWithValue("@date", DateTime.Parse("12/12/2009 11:34:55"));
                    addedRecords = cmd.ExecuteNonQuery();
                }
                using (OleDbCommand cmd = new OleDbCommand(query2, conn))
                {
                    cmd.Parameters.AddWithValue("@id_supplie", supplie.id);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            return addedRecords;
        }
        /*
         *Dodjeljuje printer odjelu 
         *
        */
        public static int addPrinterToDepartment(int id_printer, int id_department)
        {
            int addedRecords = 0;
            string query = @"update printers set id_department = @id_department where id_printer = @id_printer;";
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_department", id_department);
                    cmd.Parameters.AddWithValue("@id_printer", id_printer);
                    
                    addedRecords = cmd.ExecuteNonQuery();
                }
            }
            return addedRecords;
        }
        /*
         *Unosi novi materijal u skladištu u bazu podataka 
         *
         */
        public static int addSupplieToWarehouse(int id_supplie, int quantity)
        {
            int addedRecords = 0;
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                string sql = @"insert into warehouse(id_supplie, quantity, dis_date)
                        values (@id_supplie, @quantity, now());";
                try
                {
                    OleDbCommand sqlCommand = new OleDbCommand(sql, conn);
                    sqlCommand.Parameters.AddWithValue("@id_supplie", id_supplie);
                    sqlCommand.Parameters.AddWithValue("@quantity", quantity);
                    
                    conn.Open();

                    addedRecords = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return addedRecords;
        }
        /**
         * Upisuje novi printer u bazu podataka.
         * Prima id_proizvođača, naziv printera, id_tipa_printera, oznaku printera i listu dodanih materijala.
         */
        public static int addNewPrinter(int manufacturer_id, String name, int type_id, String mark, List<Supplie> addedSuppliesList)
        {
            int addedRecords = 1;
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                OleDbCommand cmdAddPrinter = new OleDbCommand();
                
                OleDbTransaction tr = null;


                string insertPrinter = @"insert into printers(model, id_printer_type, id_department, id_manufacturer, printer_mark)
                        values (@name, @type, @department, @manufacturer, @mark);";
                string insertSupplies = @"insert into printer_supplies(id_printer, id_supplie)
                        values (@id_printer, @id_supplie);";
                try
                {
                    
                    conn.Open();
                    tr = conn.BeginTransaction();

                    cmdAddPrinter.Connection = conn;
                    cmdAddPrinter.Transaction = tr;
                    

                    cmdAddPrinter.CommandText = insertPrinter;
                    cmdAddPrinter.Parameters.AddWithValue("@name", name);
                    cmdAddPrinter.Parameters.AddWithValue("@type", type_id);
                    cmdAddPrinter.Parameters.AddWithValue("@department", 4);
                    cmdAddPrinter.Parameters.AddWithValue("@manufacturer", manufacturer_id);
                    cmdAddPrinter.Parameters.AddWithValue("@mark", mark);
                    cmdAddPrinter.ExecuteNonQuery();
                    cmdAddPrinter.CommandText = "Select @@Identity";
                    int id = (int)cmdAddPrinter.ExecuteScalar();

                    foreach(Supplie s in addedSuppliesList)
                    {

                        OleDbCommand cmdAddSupplie = new OleDbCommand();

                        cmdAddSupplie.Connection = conn;
                        cmdAddSupplie.Transaction = tr;

                        cmdAddSupplie.CommandText = insertSupplies;
                        cmdAddSupplie.Parameters.AddWithValue("@id_printer", id);
                        cmdAddSupplie.Parameters.AddWithValue("@id_supplie", s.id);
                        cmdAddSupplie.ExecuteNonQuery();

                    }
                    
                    tr.Commit();
                    
                }
                catch(Exception ex)
                {
                    addedRecords = 0;
                    tr.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
            return addedRecords;
        }

        /**
         * Upisuje novi odjel, proizvođač ili potrošni materijal.
         * Ovisno o table parametru izvršava se potrebni sql upit.
         * Vraća 1 ili 0 ovisno o tome da li je uspješno upisano u bazu podataka.
        */
        public static int addNewOther(String name, int table)
        {

            int addedRecords = 0;
            string sql;
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                if (table == 1)
                    sql = @"insert into departments(department_name) values (@name);";
                else if (table == 2)
                    sql = @"insert into manufacturers(manufacturer_name) values (@name);";
                else
                    sql = @"insert into supplies(supplie_name) values (@name);";
                try
                {
                    OleDbCommand sqlCommand = new OleDbCommand(sql, conn);
                    sqlCommand.Parameters.AddWithValue("@name", name);
                    conn.Open();

                    addedRecords = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return addedRecords;
        }

        /**
         * 
         *Upisuje novi materijal u skladište 
         */
        public static int dodajMaterijal(Supplie supplie, int quantity)
        {
            int addedRecords = 0;
            string sql;
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                sql = @"insert into warehouse(id_supplie, quantity) values (@id_supplie, @quantity);";
                try
                {
                    OleDbCommand sqlCommand = new OleDbCommand(sql, conn);
                    sqlCommand.Parameters.AddWithValue("@id_supplie", supplie.getId());
                    sqlCommand.Parameters.AddWithValue("@quantity", quantity);
                    conn.Open();

                    addedRecords = sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return addedRecords;
        }

        /**
         * Dohvaća brojčano stanje materijala u skladištu
         * @return List
         */
        public static List<WarehouseItem> getWarehouseStatus()
        {
            List<WarehouseItem> warehouseItemList = new List<WarehouseItem>();
            string query = @"SELECT supplies.id_supplie, supplies.supplie_name, Sum(warehouse.quantity) AS suma
                            FROM supplies INNER JOIN warehouse ON supplies.id_supplie = warehouse.id_supplie
                            GROUP BY supplies.id_supplie, supplies.supplie_name;
";
            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        String name = reader.GetString(1);
                        int quantity = Convert.ToInt32(reader.GetDouble(2));

                        WarehouseItem whItem = new WarehouseItem(id, name, quantity);
                        warehouseItemList.Add(whItem);
                    }
                }
            }
            return warehouseItemList;
        }
        /*
         * Vraća tablicu dodijeljenog materijala za određeni printer u određenom vremenskom periodu
         */
        public static DataTable getPrinterSuppliesConsumption(int id_printer, String startDate, String endDate)
        {
            DataTable dt = new DataTable();
            String query = @"SELECT printers.printer_mark, printers.model, supplies.supplie_name, supplies_distribution.quantity
FROM supplies INNER JOIN (printers INNER JOIN supplies_distribution ON printers.id_printer = supplies_distribution.id_printer) ON supplies.id_supplie = supplies_distribution.id_supplie
                        where supplies_distribution.id_printer = @id_printer and supplies_distribution.dis_date between @startDate and @endDate";
            //@id_printer and supplies_distribution.dis_date between @startDate and @endDate;

            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                using (OleDbCommand sqlCommand = new OleDbCommand(query, conn))
                {
                    
                    sqlCommand.Parameters.AddWithValue("@id_printer", id_printer);
                    sqlCommand.Parameters.AddWithValue("@startDate", Convert.ToDateTime(startDate));
                    sqlCommand.Parameters.AddWithValue("@endDate", Convert.ToDateTime(endDate));
                    
                    conn.Open();

                    OleDbDataReader dr = sqlCommand.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }
        /*
         * Vraća tablicu svih dodijeljenih materijala 
         */
        public static DataTable getAllPrinterSuppliesConsumption()
        {
            DataTable dt = new DataTable();
            String query = @"SELECT printers.printer_mark, printers.model, supplies.supplie_name, supplies_distribution.quantity
FROM supplies INNER JOIN (printers INNER JOIN supplies_distribution ON printers.id_printer = supplies_distribution.id_printer) ON supplies.id_supplie = supplies_distribution.id_supplie
ORDER BY printers.printer_mark;";


            using (OleDbConnection conn = new OleDbConnection(connectionStringMDB))
            {
                using (OleDbCommand sqlCommand = new OleDbCommand(query, conn))
                {
                    conn.Open();

                    OleDbDataReader dr = sqlCommand.ExecuteReader();
                    dt.Load(dr);
                }
            }
            return dt;
        }
    }
}
