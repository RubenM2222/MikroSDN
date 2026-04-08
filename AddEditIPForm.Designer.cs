namespace MikroSDN
{
    partial class AddEditIPForm
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
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblNetwork = new System.Windows.Forms.Label();
            this.lblInterface = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtNetwork = new System.Windows.Forms.TextBox();
            this.txtInterface = new System.Windows.Forms.TextBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Labels
            // 
            this.lblAddress.Text = "Address:";
            this.lblAddress.Location = new System.Drawing.Point(12, 15);
            this.lblNetwork.Text = "Network:";
            this.lblNetwork.Location = new System.Drawing.Point(12, 45);
            this.lblInterface.Text = "Interface:";
            this.lblInterface.Location = new System.Drawing.Point(12, 75);
            this.lblComment.Text = "Comment:";
            this.lblComment.Location = new System.Drawing.Point(12, 105);
            // 
            // TextBoxes
            // 
            this.txtAddress.Location = new System.Drawing.Point(100, 12);
            this.txtAddress.Width = 200;
            this.txtNetwork.Location = new System.Drawing.Point(100, 42);
            this.txtNetwork.Width = 200;
            this.txtInterface.Location = new System.Drawing.Point(100, 72);
            this.txtInterface.Width = 200;
            this.txtComment.Location = new System.Drawing.Point(100, 102);
            this.txtComment.Width = 200;
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
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblNetwork);
            this.Controls.Add(this.txtNetwork);
            this.Controls.Add(this.lblInterface);
            this.Controls.Add(this.txtInterface);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddEditIPForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblNetwork;
        private System.Windows.Forms.Label lblInterface;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtNetwork;
        private System.Windows.Forms.TextBox txtInterface;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}