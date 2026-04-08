using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MikroSDN.Models;
using MikroSDN.Services;
using MikroSDN.Data;

namespace MikroSDN
{
    public partial class FormMain : Form
    {
        private RouterDevice _currentRouter;
        private MikrotikService _api;

        public FormMain()
        {
            InitializeComponent();
            comboRouters.SelectedIndexChanged += ComboRouters_SelectedIndexChanged;
            btnAddRouter.Click += BtnAddRouter_Click;
            btnAdd.Click += CrudButton_Click;
            btnEdit.Click += CrudButton_Click;
            btnDelete.Click += CrudButton_Click;
            btnRefresh.Click += CrudButton_Click;
        }
        private async void FormMain_Load(object sender, EventArgs e)
        {
            // Inicializa ComboBox com routers
            comboRouters.DataSource = RouterRepository.Routers;
            comboRouters.DisplayMember = "Name";

            if (comboRouters.Items.Count > 0)
                comboRouters.SelectedIndex = 0;
        }
        private void ComboRouters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentRouter = comboRouters.SelectedItem as RouterDevice;
            if (_currentRouter != null)
            {
                _api = new MikrotikService(_currentRouter);
                LoadCurrentTabData();
            }
        }

        private void BtnAddRouter_Click(object sender, EventArgs e)
        {
            using (var form = new AddRouterForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    comboRouters.DataSource = null;
                    comboRouters.DataSource = RouterRepository.Routers;
                    comboRouters.DisplayMember = "Name";
                }
            }
        }

        private void CrudButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            switch (btn.Text)
            {
                case "Add":
                    MessageBox.Show("Adicionar item");
                    break;
                case "Edit":
                    MessageBox.Show("Editar item");
                    break;
                case "Delete":
                    MessageBox.Show("Apagar item");
                    break;
                case "Refresh":
                    LoadCurrentTabData();
                    break;
            }
        }

        private async void LoadCurrentTabData()
        {
            if (_currentRouter == null) return;

            var ipService = new IpService(_api);
            try
            {
                var ips = await ipService.GetAll();
                dataGridViewMain.DataSource = ips;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }
    }
}
