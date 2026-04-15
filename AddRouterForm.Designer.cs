namespace MikroSDN
{
    partial class AddRouterForm
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
            lblName = new Label();
            lblIP = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            txtName = new TextBox();
            txtIP = new TextBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.Location = new Point(12, 15);
            lblName.Name = "lblName";
            lblName.Size = new Size(82, 23);
            lblName.TabIndex = 0;
            lblName.Text = "Nome:";
            // 
            // lblIP
            // 
            lblIP.Location = new Point(12, 45);
            lblIP.Name = "lblIP";
            lblIP.Size = new Size(68, 23);
            lblIP.TabIndex = 2;
            lblIP.Text = "IP:";
            // 
            // lblUsername
            // 
            lblUsername.Location = new Point(12, 75);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(82, 23);
            lblUsername.TabIndex = 4;
            lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(12, 105);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(82, 23);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password:";
            // 
            // txtName
            // 
            txtName.Location = new Point(100, 12);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 23);
            txtName.TabIndex = 1;
            // 
            // txtIP
            // 
            txtIP.Location = new Point(100, 42);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(200, 23);
            txtIP.TabIndex = 3;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(100, 72);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 23);
            txtUsername.TabIndex = 5;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(100, 102);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(200, 23);
            txtPassword.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(100, 140);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 8;
            btnSave.Text = "Salvar";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(200, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancelar";
            btnCancel.Click += btnCancel_Click;
            // 
            // AddRouterForm
            // 
            ClientSize = new Size(320, 180);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblIP);
            Controls.Add(txtIP);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AddRouterForm";
            Text = "Adicionar Router";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}