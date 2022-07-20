namespace WarehouseManager
{
    public partial class ManageCategories : Form
    {
        //References database access layer
        db_access db = new db_access();

        public ManageCategories()
        {
            InitializeComponent();           
            Housekeeping();
        }

        //Initial setup for gridview
        //Resizes columns to fit entire page. Disabled user resize to prevent missclicks.
        //Hides label if nothing is selected
        public void Housekeeping()
        {
            try
            {
                UpdateDataTable();
                dgCategories.ClearSelection();
                dgCategories.RowHeadersVisible = false;
                dgCategories.Columns[0].Visible = false;
                dgCategories.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgCategories.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgCategories.AllowUserToResizeColumns = false;
                dgCategories.AllowUserToResizeRows = false;
                dgCategories.AllowUserToAddRows = false;
                dgCategories.MultiSelect = false;
                dgCategories.ReadOnly = true;
                lblCategoryID.Text = "";
            }
            catch (Exception e)
            {
                MessageBox.Show("An error has occurred: " + e.Message);
            }
        }

        //Add data to database
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (tbCategoryName.Text == "")
            {
                MessageBox.Show("Please insert data before attempting to add");
            }
            else
            {
                db.AddData(tbCategoryName.Text, rtbCategoryDescription.Text);
                UpdateDataTable();
                ClearData();
            }
            
        }

        //Update currently selected information
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbCategoryName.Text == "")
            {
                MessageBox.Show("There is nothing to update");
            } else
            {
                db.UpdateData(lblCategoryID.Text, tbCategoryName.Text, rtbCategoryDescription.Text);
                UpdateDataTable();
                ClearData();
            }
        }

        //Return back to main menu
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        //Delete currently selected row, with confirmation
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblCategoryID.Text == "")
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
                    db.DeleteData(lblCategoryID.Text);
                    ClearData();
                }
                else
                {

                }
            } 
        }

        //Update Gridview to show latest info from database
        public void UpdateDataTable()
        {
            dgCategories.DataSource = db.ShowAllData();
        }

        //Clear all data in texboxes
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        //Clear all data in texboxes
        public void ClearData()
        {
            UpdateDataTable();
            lblCategoryID.Text = "";
            tbCategoryName.Text = "";
            rtbCategoryDescription.Text = "";
        }

        //updates labels and textboxes whenever a new selection is made in the gridview
        private void dgCategories_SelectionChanged(object sender, EventArgs e)
        {
            lblCategoryID.Text = dgCategories.CurrentRow.Cells["CategoryID"].Value.ToString();
            tbCategoryName.Text = dgCategories.CurrentRow.Cells["CategoryName"].Value.ToString();
            rtbCategoryDescription.Text = dgCategories.CurrentRow.Cells["Description"].Value.ToString();
        }
    }
}
