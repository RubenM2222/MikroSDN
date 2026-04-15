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
            try
            {
                // 1. Extração e Sanitização
                string name = txtName.Text.Trim();
                string ip = txtIP.Text.Trim();
                string user = txtUsername.Text.Trim();
                string password = txtPassword.Text; // Senhas geralmente não levam Trim() pois espaços podem fazer parte delas

                // 2. Validação de Campos Vazios
                if (new[] { name, ip, user, password }.Any(string.IsNullOrWhiteSpace))
                {
                    MessageBox.Show("Preencha todos os campos corretamente.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Validação de Formato de IP
                if (!IPAddress.TryParse(ip, out _))
                {
                    MessageBox.Show("O endereço IP informado não é válido.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIP.Focus();
                    return;
                }

                // 4. Verificação de Duplicidade (Opcional, mas recomendado)
                if (RouterRepository.Routers.Any(r => r.IP == ip))
                {
                    MessageBox.Show("Já existe um dispositivo cadastrado com este IP.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 5. Criação do Objeto
                var newRouter = new RouterDevice
                {
                    Name = name,
                    IP = ip,
                    Username = user,
                    Password = password // Considere usar uma função de criptografia aqui: Encrypt(password)
                };

                // 6. Persistência
                RouterRepository.Routers.Add(newRouter);
                RouterRepository.Save();

                // Se houver um banco de dados ou arquivo JSON:
                // RouterRepository.SaveToFile(); 

                // 7. Finalização
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar o dispositivo: {ex.Message}", "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
