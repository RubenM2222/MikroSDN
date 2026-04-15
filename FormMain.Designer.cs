namespace MikroSDN
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMenu = new Panel();
            comboRouters = new ComboBox();
            btnAddRouter = new Button();
            dataGridViewMain = new DataGridView();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).BeginInit();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.LightGray;
            panelMenu.Location = new Point(12, 12);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(140, 426);
            panelMenu.TabIndex = 0;
            // 
            // comboRouters
            // 
            comboRouters.DropDownStyle = ComboBoxStyle.DropDownList;
            comboRouters.Location = new Point(670, 12);
            comboRouters.Name = "comboRouters";
            comboRouters.Size = new Size(120, 23);
            comboRouters.TabIndex = 1;
            // 
            // btnAddRouter
            // 
            btnAddRouter.Location = new Point(635, 12);
            btnAddRouter.Name = "btnAddRouter";
            btnAddRouter.Size = new Size(29, 23);
            btnAddRouter.TabIndex = 2;
            btnAddRouter.Text = "+";
            btnAddRouter.UseVisualStyleBackColor = true;
            // 
            // dataGridViewMain
            // 
            dataGridViewMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMain.Location = new Point(160, 50);
            dataGridViewMain.Name = "dataGridViewMain";
            dataGridViewMain.ReadOnly = true;
            dataGridViewMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewMain.Size = new Size(630, 350);
            dataGridViewMain.TabIndex = 3;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(160, 410);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 25);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(250, 410);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 25);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(340, 410);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 25);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(430, 410);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 25);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(600, 12);
            button1.Name = "button1";
            button1.Size = new Size(29, 23);
            button1.TabIndex = 8;
            button1.Text = "-";
            button1.UseVisualStyleBackColor = true;
            button1.Click += BtnDelRouter_Click;
            // 
            // FormMain
            // 
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dataGridViewMain);
            Controls.Add(btnAddRouter);
            Controls.Add(comboRouters);
            Controls.Add(panelMenu);
            Name = "FormMain";
            Text = "MikroSDN Controller";
            Load += FormMain_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewMain).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.ComboBox comboRouters;
        private System.Windows.Forms.Button btnAddRouter;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private Button button1;
    }
}
