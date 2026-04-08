using MikroSDN.Data;
using MikroSDN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikroSDN
{
    public partial class AddRouterForm : Form
    {
        public AddRouterForm()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string ip = txtIP.Text.Trim();
            string user = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validação básica
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(ip) ||
                string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Todos os campos são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IPAddress.TryParse(ip, out _))
            {
                MessageBox.Show("IP inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Criar novo router
            RouterDevice router = new RouterDevice
            {
                Name = name,
                IP = ip,
                Username = user,
                Password = password
            };

            // Adicionar ao repositório
            RouterRepository.Routers.Add(router);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
