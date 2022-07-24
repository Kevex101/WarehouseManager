using System.Drawing.Drawing2D;
using System;
using System.Text;
using System.Buffers.Text;
using Microsoft.Data.SqlClient;
using System.Data;

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

        /// <summary>
        /// Sets up gridview data, column width, formatting and restrictions
        /// </summary>
        public void Housekeeping()
        {
            try
            {
                UpdateDataTable();
                dgCategories.ClearSelection();
                dgCategories.RowHeadersVisible = false;
                dgCategories.Columns[0].Visible = false;
                dgCategories.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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

        //Adds data to database if no errors occur
        private void btnAdd_Click(object sender, EventArgs e)
        {

            //this.Hide();
            if (tbCategoryName.Text == "")
            {
                MessageBox.Show("Please insert a Category Name before attempting to add");
            }
            else
            {
                Image img = pbPicture.Image;
                byte[] imgbytes = ImageToByteArray(img);
                db.AddData_Categories(tbCategoryName.Text, rtbCategoryDescription.Text, imgbytes);
            }
            UpdateDataTable();
            ClearData();
        }

        //Updates data in database for selected row if no errors occur
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tbCategoryName.Text == "")
            {
                MessageBox.Show("There is nothing to update");
            }
            else
            {
                Image img = pbPicture.Image;
                byte[] imgbytes = ImageToByteArray(img);
                db.UpdateData_Categories(lblCategoryID.Text, tbCategoryName.Text, rtbCategoryDescription.Text,  imgbytes);
                UpdateDataTable();
                ClearData();
            }
        }

        //Close this form and return to main menu
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu form1 = new MainMenu();
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
                    db.DeleteData_Categories(lblCategoryID.Text);
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
            dgCategories.DataSource = db.ShowAllData_Categories();
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
            pbPicture.Image = null;
        }

        //updates labels and textboxes whenever a new selection is made in the gridview
        private void dgCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (dgCategories.CurrentRow != null)
            {
                lblCategoryID.Text = dgCategories.CurrentRow.Cells["CategoryID"].Value.ToString();
                tbCategoryName.Text = dgCategories.CurrentRow.Cells["CategoryName"].Value.ToString();
                rtbCategoryDescription.Text = dgCategories.CurrentRow.Cells["Description"].Value.ToString();
                pbPicture.Image = db.FetchImage_Categories(lblCategoryID.Text);
            }
        }

        //Allows user to upload image to application into the picturebox
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFD = new OpenFileDialog();
            OpenFD.Title = "Select Filed";
            OpenFD.Filter = "Jpg|*.jpg|Jpge|*.jpge|Gif|*.gif";

            if (OpenFD.ShowDialog() == DialogResult.OK)
            {
                pbPicture.Image = Image.FromFile(OpenFD.FileName);
            }
            OpenFD.Dispose();

        }

        //Converts image from blob into an image for the 
        public static byte[]? ImageToByteArray(Image image)
        {
            if (image != null)
            {
                var ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            } else
            {
                return null;
            }
               
            
        }

        //Clears Picturebox on click
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            pbPicture.Image = null;
        }
    }
}
