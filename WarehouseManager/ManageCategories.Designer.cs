namespace WarehouseManager
{
    partial class ManageCategories
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCategoryName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbCategoryDescription = new System.Windows.Forms.RichTextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCategoryID = new System.Windows.Forms.Label();
            this.dgCategories = new System.Windows.Forms.DataGridView();
            this.dbaccessBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbaccessBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Category Name";
            // 
            // tbCategoryName
            // 
            this.tbCategoryName.Location = new System.Drawing.Point(559, 96);
            this.tbCategoryName.Name = "tbCategoryName";
            this.tbCategoryName.Size = new System.Drawing.Size(229, 23);
            this.tbCategoryName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(435, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Category Description";
            // 
            // rtbCategoryDescription
            // 
            this.rtbCategoryDescription.Location = new System.Drawing.Point(559, 161);
            this.rtbCategoryDescription.Name = "rtbCategoryDescription";
            this.rtbCategoryDescription.Size = new System.Drawing.Size(229, 48);
            this.rtbCategoryDescription.TabIndex = 4;
            this.rtbCategoryDescription.Text = "";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(496, 317);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(591, 318);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(685, 317);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(59, 374);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 10;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(435, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Category ID";
            // 
            // lblCategoryID
            // 
            this.lblCategoryID.AutoSize = true;
            this.lblCategoryID.Location = new System.Drawing.Point(559, 42);
            this.lblCategoryID.Name = "lblCategoryID";
            this.lblCategoryID.Size = new System.Drawing.Size(57, 15);
            this.lblCategoryID.TabIndex = 12;
            this.lblCategoryID.Text = "{number}";
            // 
            // dgCategories
            // 
            this.dgCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCategories.Location = new System.Drawing.Point(12, 42);
            this.dgCategories.Name = "dgCategories";
            this.dgCategories.RowTemplate.Height = 25;
            this.dgCategories.Size = new System.Drawing.Size(387, 313);
            this.dgCategories.TabIndex = 15;
            this.dgCategories.SelectionChanged += new System.EventHandler(this.dgCategories_SelectionChanged);
            // 
            // dbaccessBindingSource
            // 
            this.dbaccessBindingSource.DataSource = typeof(WarehouseManager.db_access);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(496, 374);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Cancel";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ManageCategories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgCategories);
            this.Controls.Add(this.lblCategoryID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.rtbCategoryDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbCategoryName);
            this.Controls.Add(this.label1);
            this.Name = "ManageCategories";
            this.Text = "ManageCategories";
            ((System.ComponentModel.ISupportInitialize)(this.dgCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbaccessBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox tbCategoryName;
        private Label label2;
        private RichTextBox rtbCategoryDescription;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnReturn;
        private Label label3;
        private Label lblCategoryID;
        public DataGridView dgCategories;
        private DataGridViewTextBoxColumn categoryNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn categoryDescriptionDataGridViewTextBoxColumn;
        private BindingSource dbaccessBindingSource;
        private Button btnClear;
    }
}