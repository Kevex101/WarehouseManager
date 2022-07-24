using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseManager
{
    public partial class ManageSuppliers : Form
    {

        //References database access layer
        db_access db = new db_access();
        public ManageSuppliers()
        {
            InitializeComponent();
            Housekeeping();
        }

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
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu form1 = new MainMenu();
            form1.Show();
        }

        /// <summary>
        /// Sets up gridview data, column width, formatting and restrictions
        /// </summary>
        public void Housekeeping()
        {
            try
            {
                UpdateDataTable();
                dgSuppliers.ClearSelection();
                dgSuppliers.RowHeadersVisible = false;
                dgSuppliers.Columns[0].Visible = false;
                for (int i = 0; i <= 10; i++)
                {
                    dgSuppliers.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dgSuppliers.AllowUserToResizeColumns = false;
                dgSuppliers.AllowUserToResizeRows = false;
                dgSuppliers.AllowUserToAddRows = false;
                dgSuppliers.MultiSelect = false;
                dgSuppliers.ReadOnly = true;
                dgSuppliers.AllowUserToDeleteRows = false;

                lblSupplierID.Text = "";
            }
            catch (Exception e)
            {
                MessageBox.Show("An error has occurred: " + e.Message);
            }
        }

        //Refreshes data in datagridview
        public void UpdateDataTable()
        {
            dgSuppliers.DataSource = db.ShowAllData_Suppliers();
        }

        //Adds data to database if no errors occur
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbCompanyName.Text == "")
            {
                MessageBox.Show("Please insert data before attempting to add");
            }
            else
            {
                db.AddData_Suppliers(tbCompanyName.Text, tbContactName.Text,
                    tbContactTitle.Text, tbAddress.Text, tbCity.Text, tbRegion.Text, tbPostalCode.Text, tbCountry.Text,
                    tbPhone.Text, tbFax.Text);
                UpdateDataTable();
            }
        }

        //Changes textboxes and labels whenever a new row is selected in the datagridview
        private void dgSuppliers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgSuppliers.CurrentRow != null)
            {
                lblSupplierID.Text = dgSuppliers.CurrentRow.Cells["SupplierID"].Value.ToString();
                tbCompanyName.Text = dgSuppliers.CurrentRow.Cells["CompanyName"].Value.ToString();
                tbContactName.Text = dgSuppliers.CurrentRow.Cells["ContactName"].Value.ToString();
                tbContactTitle.Text = dgSuppliers.CurrentRow.Cells["ContactTitle"].Value.ToString();
                tbAddress.Text = dgSuppliers.CurrentRow.Cells["Address"].Value.ToString();
                tbCity.Text = dgSuppliers.CurrentRow.Cells["City"].Value.ToString();
                tbRegion.Text = dgSuppliers.CurrentRow.Cells["Region"].Value.ToString();
                tbPostalCode.Text = dgSuppliers.CurrentRow.Cells["PostalCode"].Value.ToString();
                tbCountry.Text = dgSuppliers.CurrentRow.Cells["Country"].Value.ToString();
                tbPhone.Text = dgSuppliers.CurrentRow.Cells["Phone"].Value.ToString();
                tbFax.Text = dgSuppliers.CurrentRow.Cells["Fax"].Value.ToString();
            }
            else
            {

            }
        }

        //Updates data in database for selected row if no errors occur
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbCompanyName.Text == "")
            {
                MessageBox.Show("There is nothing to update");
            }
            else
            {
                db.UpdateData_Suppliers(lblSupplierID.Text, tbCompanyName.Text, tbContactName.Text,
                    tbContactTitle.Text, tbAddress.Text, tbCity.Text, tbRegion.Text, tbPostalCode.Text, tbCountry.Text,
                    tbPhone.Text, tbFax.Text);
                UpdateDataTable();
            }
        }

        //Delete selected row from database, with confirmation
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblSupplierID.Text == "")
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
                    db.DeleteData_Suppliers(lblSupplierID.Text);
                    UpdateDataTable();
                }
                else
                {

                }
            }
        }

        //limits allowed characters use is allowed to use
        private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9^\-^\/^\(^\)^\\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }

        }

        private void tbFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Regex.IsMatch(e.KeyChar.ToString(), @"[^0-9^\-^\/^\(^\)^\\b]"))
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }
        }
    }
}
