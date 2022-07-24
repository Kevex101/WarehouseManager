using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WarehouseManager
{
    public class db_access 
    {
        //Variables
        string connectionString = "Data Source=hulamin.database.windows.net;Initial Catalog=hulaminTest;Persist Security Info=True;User ID=ZanderG;Password=GphkhMQtjwtnTB7";
        SqlConnection? con;
        SqlCommand? cmd;

        /// <summary>
        /// All sql queries are done in the database via Stored procedures, 
        /// not a single query is done in this app directly
        /// </summary>

        //Retrieves all data from Categories table in database
        public DataTable ShowAllData_Categories()
        {
            using (con = new SqlConnection(connectionString))
            {
                con.Open();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("spCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "FetchData");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }

        //Adds data to Categories table in database
        public void AddData_Categories(string CategoryName, string Description, byte[] Image)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    cmd = new SqlCommand("spCategories", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "SaveData");
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Picture", Image);
                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Data saved successfully");
                    } else
                    {
                        MessageBox.Show("Please try again");
                    }
                }
            }catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        //Deletes selected row from Categories table in database
        public void DeleteData_Categories(string CategoryID)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("spCategories", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "DeleteData");
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);

                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Data deleted successfully");
                    } else
                    {
                        MessageBox.Show("Please try again");
                    }
                }
            }catch(Exception e)
            {
            MessageBox.Show("Error: " + e.Message);
            }
        }

        //Updates selected data to Categories table in database
        public void UpdateData_Categories(string CategoryID, string CategoryName, string Description, byte[] Image)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    cmd = new SqlCommand("spCategories", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "UpdateData");
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Picture", Image);
                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Data updated successfully");
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        //Fetch only image from database from selected row
        public Image? FetchImage_Categories(string CategoryID)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    cmd = new SqlCommand("spCategories", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "FetchImage");
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    int numRes = cmd.ExecuteNonQuery();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(ds, "Categories");
                    

                    if (ds.Tables["Categories"].Rows.Count > 0)
                    {
                        Byte[] byteBLOBData = new Byte[0];
                        byteBLOBData = (Byte[])ds.Tables["Categories"].Rows[0]["Picture"];
                        return GetDataToImage(byteBLOBData);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                return null;
            }
        }

        //Retrieves all data from Products table in database
        public DataTable ShowAllData_Products()
        {
            using (con = new SqlConnection(connectionString))
            {
                con.Open();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("spProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "FetchData");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }

        //Adds data to Products table in database
        public void AddData_Products(string ProductName, int SupplierID, int CategoryID, 
            string QuantityPerUnit, float UnitPrice, 
            int UnitsInStock, int UnitsOnOrder, 
            int ReorderLevel, string Discontinued)
        {
            try
            {
                int bit = Discontinued.ToString() == "Yes" ? 1 : 0;

                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    cmd = new SqlCommand("spProducts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "SaveData");
                    cmd.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    cmd.Parameters.AddWithValue("@QuantityPerUnit", QuantityPerUnit);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    cmd.Parameters.AddWithValue("@UnitsInStock", UnitsInStock);
                    cmd.Parameters.AddWithValue("@UnitsOnOrder", UnitsOnOrder);
                    cmd.Parameters.AddWithValue("@ReorderLevel", ReorderLevel);
                    cmd.Parameters.AddWithValue("@Discontinued", bit);
                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Data added successfully");
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        //Deletes selected row from Categories table in database
        public void DeleteData_Products(string ProductID)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("spProducts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "DeleteData");
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);

                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Data deleted successfully");
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        //Updates selected data to Categories table in database
        public void UpdateData_Products(int ProductID, string ProductName, int SupplierID, int CategoryID,
            string QuantityPerUnit, float UnitPrice,
            int UnitsInStock, int UnitsOnOrder, 
            int ReorderLevel, string Discontinued)
        {
            try
            {
                int bit = Discontinued.ToString() == "Yes" ? 1 : 0;
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    cmd = new SqlCommand("spProducts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "UpdateData");
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", ProductName);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    cmd.Parameters.AddWithValue("@QuantityPerUnit", QuantityPerUnit);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    cmd.Parameters.AddWithValue("@UnitsInStock", UnitsInStock);
                    cmd.Parameters.AddWithValue("@UnitsOnOrder", UnitsOnOrder);
                    cmd.Parameters.AddWithValue("@ReorderLevel", ReorderLevel);
                    cmd.Parameters.AddWithValue("@Discontinued", bit);

                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Data updated successfully");
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        //Fetches only CategoryID from Category table
        //To be used for error checking in ManageProducts Form

        public DataTable CompareCategoryID_Products(int CategoryID)
        {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    cmd = new SqlCommand("spProducts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "CompareCategoryID");
                    cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
                }
        }

        //Fetches only Supplier from Suppliers table
        //To be used for error checking in ManageProducts Form
        public DataTable CompareSupplierID_Products(int SupplierID)
        {
            using (con = new SqlConnection(connectionString))
            {
                con.Open();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("spProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "CompareSupplierID");
                cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }

        //Retrieves all data from Suppliers table in database
        public DataTable ShowAllData_Suppliers()
        {
            using (con = new SqlConnection(connectionString))
            {
                con.Open();
                DataTable dt = new DataTable();
                cmd = new SqlCommand("spSuppliers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "FetchData");
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                return dt;
            }
        }

        //Adds data to Suppliers table in database
        public void AddData_Suppliers(string CompanyName, string ContactName, string ContactTitle, 
            string Address, string City, string Region, string PostalCode, string Country, 
            string Phone, string FAX)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    cmd = new SqlCommand("spSuppliers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "SaveData");
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                    cmd.Parameters.AddWithValue("@ContactName", ContactName);
                    cmd.Parameters.AddWithValue("@ContactTitle", ContactTitle);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@City", City);
                    cmd.Parameters.AddWithValue("@Region", Region);
                    cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@FAX", FAX);

                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Data added successfully");
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        //Deletes selected row from Suppliers table in database
        public void DeleteData_Suppliers(string SupplierID)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("spSuppliers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "DeleteData");
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);

                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Data deleted successfully");
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        //Updates selected data to Suppliers table in database
        public void UpdateData_Suppliers(string SupplierID, string CompanyName, string ContactName, string ContactTitle,
            string Address, string City, string Region, string PostalCode, string Country,
            string Phone, string FAX)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    cmd = new SqlCommand("spSuppliers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "UpdateData");
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                    cmd.Parameters.AddWithValue("@ContactName", ContactName);
                    cmd.Parameters.AddWithValue("@ContactTitle", ContactTitle);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@City", City);
                    cmd.Parameters.AddWithValue("@Region", Region);
                    cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
                    cmd.Parameters.AddWithValue("@Country", Country);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@FAX", FAX);

                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Data updated successfully");
                    }
                    else
                    {
                        MessageBox.Show("Please try again");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }
 
        //Converts blob from database to image datatype
        public Image? GetDataToImage(byte[] pData)
        {
            try
            {
                ImageConverter imgConverter = new ImageConverter();
                return imgConverter.ConvertFrom(pData) as Image;
            }
            catch (Exception f)
            {
                MessageBox.Show("Error: " + f.Message);
                return null;
            }
        }
    }
}


