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
        string connectionString = "Data Source=hulamin.database.windows.net;Initial Catalog=hulaminTest;Persist Security Info=True;User ID=ZanderG;Password=GphkhMQtjwtnTB7";
        SqlConnection? con;
        SqlCommand? cmd;

        public DataTable ShowAllData()
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

        public void AddData(string CategoryName, string Description)
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

        public void DeleteData(string CategoryID)
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

        public void UpdateData(string CategoryID, string CategoryName, string Description)
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
    }
}


