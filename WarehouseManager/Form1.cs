namespace WarehouseManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageCategories manageCategories = new ManageCategories();
            manageCategories.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}