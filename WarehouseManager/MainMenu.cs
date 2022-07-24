using System.Drawing.Drawing2D;

namespace WarehouseManager
{
    public partial class MainMenu : Form
    {
        private int BorderSize = 1;
        public MainMenu()
        {
            InitializeComponent();
            HouseKeeping();
        }

        public void HouseKeeping()
        {
            btnSuppliers.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnCategories.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnProducts.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnExit.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageProducts manageProducts = new ManageProducts();
            manageProducts.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageCategories manageCategories = new ManageCategories();
            manageCategories.Show();
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {

            btnExit.FlatAppearance.BorderSize = BorderSize;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.FlatAppearance.BorderSize = 0;
        }

        private void btnSuppliers_MouseEnter(object sender, EventArgs e)
        {
            btnSuppliers.FlatAppearance.BorderSize = BorderSize;
        }

        private void btnSuppliers_MouseLeave(object sender, EventArgs e)
        {
            btnSuppliers.FlatAppearance.BorderSize = 0;
        }

        private void btnCategories_MouseEnter(object sender, EventArgs e)
        {
            btnCategories.FlatAppearance.BorderSize = BorderSize;
        }

        private void btnCategories_MouseLeave(object sender, EventArgs e)
        {
            btnCategories.FlatAppearance.BorderSize = 0;
        }

        private void btnProducts_MouseEnter(object sender, EventArgs e)
        {
            btnProducts.FlatAppearance.BorderSize = BorderSize;
        }

        private void btnProducts_MouseLeave(object sender, EventArgs e)
        {
            btnProducts.FlatAppearance.BorderSize = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageSuppliers manageSuppliers = new ManageSuppliers();
            manageSuppliers.Show();
        }
    }
}