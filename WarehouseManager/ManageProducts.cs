using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseManager
{
    public partial class ManageProducts : Form
    {
        //Gradient Background
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                               Color.DarkCyan,
                                                               Color.Black,
                                                               45F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        //References database access layer
        db_access db = new db_access();
        public ManageProducts()
        {
            InitializeComponent();
            Housekeeping();
        }

        /// <summary>
        /// Sets up gridview data, column width, formatting and restrictions
        /// </summary>
        public void Housekeeping()
        {
            try
            {
                UpdateDataTable();
                dgProducts.ClearSelection();
                dgProducts.RowHeadersVisible = false;
                dgProducts.Columns[0].Visible = false;
                for (int i = 0; i <= 9; i++)
                {
                    dgProducts.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgProducts.Columns["UnitPrice"].DefaultCellStyle.Format = "0.00##";
                dgProducts.AllowUserToResizeColumns = false;
                dgProducts.AllowUserToResizeRows = false;
                dgProducts.AllowUserToAddRows = false;
                dgProducts.MultiSelect = false;
                dgProducts.ReadOnly = true;
                dgProducts.AllowUserToDeleteRows = false;

                lblProductID.Text = "";
            }
            catch (Exception e)
            {
                MessageBox.Show("An error has occurred: " + e.Message);
            }
        }

        //Refreshes data in datagridview
        public void UpdateDataTable()
        {
            dgProducts.DataSource = db.ShowAllData_Products();
        }

        //Changes textboxes and labels whenever a new row is selected in the datagridview
        private void dgProducts_SelectionChanged(object sender, EventArgs e)
        {
            
            if (dgProducts.CurrentRow != null)
            {
                float UnitPrice = float.Parse(dgProducts.CurrentRow.Cells["UnitPrice"].Value.ToString());
                String.Format(UnitPrice % 1 == 0 ? "{0:0}" : "{0:0.00}", UnitPrice);

                lblProductID.Text = dgProducts.CurrentRow.Cells["ProductID"].Value.ToString();
                tbProductName.Text = dgProducts.CurrentRow.Cells["ProductName"].Value.ToString();
                tbQuantity.Text = dgProducts.CurrentRow.Cells["QuantityPerUnit"].Value.ToString();
                tbSupplierID.Text = dgProducts.CurrentRow.Cells["SupplierID"].Value.ToString();
                tbCategoryID.Text = dgProducts.CurrentRow.Cells["CategoryID"].Value.ToString();
                tbReorderLevel.Text = dgProducts.CurrentRow.Cells["ReorderLevel"].Value.ToString();
                string test = dgProducts.CurrentRow.Cells["Discontinued"].Value.ToString() == "True" ? cbDiscontinued.Text = "Yes" : cbDiscontinued.Text = "No";
                tbUnitPrice.Text = UnitPrice.ToString();
                tbInStock.Text = dgProducts.CurrentRow.Cells["UnitsInStock"].Value.ToString();
                tbOnOrder.Text = dgProducts.CurrentRow.Cells["UnitsOnOrder"].Value.ToString();
            } else
            {

            }
        }

        //Tests if any number is above a datatype maximum,
        //and checks if CategoryID and SupplierID exists before you are allowed to enter/edit data
        public int Test(bool fulltest, int SupplierID, int CategoryID, Int16 InStock, Int16 OnOrder, Int16 ReorderLevel, float UnitPrice)
        {
            
            if (fulltest)
            {
                bool[] test = new bool[6];
                
                test[0] = SupplierID != 0 ? test[0] : !test[0];
                test[1] = CategoryID != 0 ? test[1] : !test[1];
                test[2] = UnitPrice != 0 ? test[2] : !test[2];
                test[3] = InStock != 0 ? test[3] : !test[3];
                test[4] = OnOrder != 0 ? test[4] : !test[4];
                test[5] = ReorderLevel != 0 ? test[5] : !test[5];

                for (int i = 0; i < 2; i++)
                {
                    if (test[i])
                    {
                        return 0;
                    }
                }
                for (int i = 2; i < 6; i++)
                {
                    if (test[i])
                    {
                        return 1;
                    }
                }
            }
            

            DataTable dtCategory = new DataTable();
            DataTable dtSupplier = new DataTable();
            dtCategory = db.CompareCategoryID_Products(CategoryID);
            dtSupplier = db.CompareSupplierID_Products(SupplierID);

            if (dtCategory.Rows.Count == 0)
            {
                return 2;
            } else if (dtSupplier.Rows.Count == 0)
            {
                return 3;
            }
            return 4;
        }

        //Adds data to database if no errors occur
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int SupplierID, CategoryID;
            Int16 InStock, OnOrder, ReorderLevel;
            float UnitPrice = 0;
            bool tryparseSupplierID = int.TryParse(tbSupplierID.Text, out SupplierID);
            bool tryparseCategoryID = int.TryParse(tbCategoryID.Text, out CategoryID);
            bool tryParseUnitPrice = float.TryParse(tbUnitPrice.Text, out UnitPrice);
            bool tryParseInStock = Int16.TryParse(tbInStock.Text, out InStock);
            bool tryParseOnOrder = Int16.TryParse(tbOnOrder.Text, out OnOrder);
            bool tryParseReorderLevel = Int16.TryParse(tbReorderLevel.Text, out ReorderLevel);

            UnitPrice = (float)Math.Round(UnitPrice * 100f) / 100f;
            int success;
            bool parseSuccess = false;
            if (tryparseSupplierID && tryparseCategoryID && tryParseUnitPrice && tryParseInStock && tryParseUnitPrice && tryParseOnOrder && tryParseReorderLevel)
            {
                parseSuccess = true;
                success = 4;
            } else
            {
                success = Test(true, SupplierID, CategoryID, InStock, OnOrder, ReorderLevel, UnitPrice);
            }

            if (tbSupplierID.Text == "0" | tbCategoryID.Text == "0")
            {

                success = 0;
            } else if (!parseSuccess)
            {
                success = Test(false, SupplierID, CategoryID, InStock, OnOrder, ReorderLevel, UnitPrice);
            }

            if (success == 0)
            {
                MessageBox.Show("Supplier ID and Category ID must be between 1 and 2,147,483,647");
                
            } else if (success == 1)
            {
                MessageBox.Show("Units In Stock, On Order and Reorder Level must be between 0 and 32,767");
            }
            else if (success == 2)
            {
                MessageBox.Show("Category ID " + CategoryID + " does not exist");
            }
            else if (success == 3)
            {
                MessageBox.Show("Supplier ID " + SupplierID + " does not exist");
            }
            else
            {            
                db.AddData_Products(tbProductName.Text, SupplierID, 
                    CategoryID, tbQuantity.Text, 
                    UnitPrice, InStock, 
                    OnOrder, ReorderLevel, 
                    cbDiscontinued.Text.ToString());
                UpdateDataTable();
            }
        }

        //Close this form and return to main menu
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu form1 = new MainMenu();
            form1.Show();
        }

        //Updates data in database for selected row if no errors occur
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int SupplierID, CategoryID;
            Int16 InStock, OnOrder, ReorderLevel;
            float UnitPrice = 0;
            bool tryparseSupplierID = int.TryParse(tbSupplierID.Text, out SupplierID);
            bool tryparseCategoryID = int.TryParse(tbCategoryID.Text, out CategoryID);
            bool tryParseUnitPrice = float.TryParse(tbUnitPrice.Text, out UnitPrice);
            bool tryParseInStock = Int16.TryParse(tbInStock.Text, out InStock);
            bool tryParseOnOrder = Int16.TryParse(tbOnOrder.Text, out OnOrder);
            bool tryParseReorderLevel = Int16.TryParse(tbReorderLevel.Text, out ReorderLevel);

            UnitPrice = (float)Math.Round(UnitPrice * 100f) / 100f;
            int success;
            if (tryparseSupplierID && tryparseCategoryID && tryParseUnitPrice && tryParseInStock && tryParseUnitPrice && tryParseOnOrder && tryParseReorderLevel)
            {
                success = 4;
            }

            if (tbSupplierID.Text == "0" | tbCategoryID.Text == "0")
            {

                success = 0;
            }
            else
            {
                success = Test(false, SupplierID, CategoryID, InStock, OnOrder, ReorderLevel, UnitPrice);
            }

            if (success == 0)
            {
                MessageBox.Show("Supplier ID and Category ID must be between 1 and 2,147,483,647");

            }
            else if (success == 1)
            {
                MessageBox.Show("Units In Stock, On Order and Reorder Level must be between 0 and 32,767");
            }
            else if (success == 2)
            {
                MessageBox.Show("Category ID " + CategoryID + " does not exist");
            }
            else if (success == 3)
            {
                MessageBox.Show("Supplier ID " + SupplierID + " does not exist");
            }
            else
            {
                db.UpdateData_Products(int.Parse(lblProductID.Text), tbProductName.Text, SupplierID, 
                    CategoryID, tbQuantity.Text, 
                    UnitPrice, InStock, 
                    OnOrder, ReorderLevel, 
                    cbDiscontinued.Text.ToString());
                UpdateDataTable();
            }
        }

        //Delete selected row from database, with confirmation
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblProductID.Text == "")
            {
                MessageBox.Show("Please select something to delete");
            }
            else
            {
                var confirmResult = MessageBox.Show("Are you sure you want to delete this item?",
                                     "Confirm Delete!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    db.DeleteData_Products(lblProductID.Text);
                    UpdateDataTable();
                }
                else
                {

                }
            }
        }

        //Limits characters use is able to input into textboxes
        private void tbSupplierID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbCategoryID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbReorderLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbInStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar !='.')
                e.Handled = true;

            // only allow one decimal point
            if ((e.KeyChar == '.') && (((TextBox)sender).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void tbOnOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
