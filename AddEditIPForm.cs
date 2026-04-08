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
using System.Xml.Linq;

namespace MikroSDN
{
    public partial class AddEditIPForm : Form
    {
        public IpAddressModel IpModel { get; private set; }

        public AddEditIPForm()
        {
            InitializeComponent();
            this.Text = "Adicionar IP";
            IpModel = new IpAddressModel();
        }

        public AddEditIPForm(IpAddressModel model) : this()
        {
            this.Text = "Editar IP";
            IpModel = model;

            txtAddress.Text = model.address;
            txtNetwork.Text = model.network;
            txtInterface.Text = model.@interface;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text.Trim();
            string network = txtNetwork.Text.Trim();
            string iface = txtInterface.Text.Trim();
            string comment = txtComment.Text.Trim();

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(network) || string.IsNullOrEmpty(iface))
            {
                MessageBox.Show("Address, Network e Interface são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validação IP simples
            if (!IPAddress.TryParse(address.Split('/')[0], out _))
            {
                MessageBox.Show("Address inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // CORREÇÃO: usar a instância IpModel e não a classe IpAddressModel
            IpModel.address = address;
            IpModel.network = network;
            IpModel.@interface = iface;   // usar @interface por ser palavra reservada
            IpModel.disabled = "false";   // opcional, podes ajustar conforme API

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
