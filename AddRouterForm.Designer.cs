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
            this.lblName = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Labels
            // 
            this.lblName.Text = "Nome:";
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblIP.Text = "IP:";
            this.lblIP.Location = new System.Drawing.Point(12, 45);
            this.lblUsername.Text = "Username:";
            this.lblUsername.Location = new System.Drawing.Point(12, 75);
            this.lblPassword.Text = "Password:";
            this.lblPassword.Location = new System.Drawing.Point(12, 105);
            // 
            // TextBoxes
            // 
            this.txtName.Location = new System.Drawing.Point(100, 12);
            this.txtName.Width = 200;
            this.txtIP.Location = new System.Drawing.Point(100, 42);
            this.txtIP.Width = 200;
            this.txtUsername.Location = new System.Drawing.Point(100, 72);
            this.txtUsername.Width = 200;
            this.txtPassword.Location = new System.Drawing.Point(100, 102);
            this.txtPassword.Width = 200;
            this.txtPassword.PasswordChar = '*';
            // 
            // Buttons
            // 
            this.btnSave.Text = "Salvar";
            this.btnSave.Location = new System.Drawing.Point(100, 140);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Location = new System.Drawing.Point(200, 140);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Form
            // 
            this.ClientSize = new System.Drawing.Size(320, 180);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddRouterForm";
            this.Text = "Adicionar Router";
            this.ResumeLayout(false);
            this.PerformLayout();
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