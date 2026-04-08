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

            // Eventos
            this.Load += FormMain_Load;
            comboRouters.SelectedIndexChanged += ComboRouters_SelectedIndexChanged;
            btnAddRouter.Click += BtnAddRouter_Click;

            btnAdd.Click += CrudButton_Click;
            btnEdit.Click += CrudButton_Click;
            btnDelete.Click += CrudButton_Click;
            btnRefresh.Click += CrudButton_Click;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            RefreshRoutersDropdown();
        }

        private void RefreshRoutersDropdown()
        {
            comboRouters.DataSource = null;
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
                    RefreshRoutersDropdown();
                    comboRouters.SelectedIndex = RouterRepository.Routers.Count - 1; // Seleciona novo router
                }
            }
        }

        private async void LoadCurrentTabData()
        {
            if (_currentRouter == null) return;

            try
            {
                // Exemplo com IPs, expandir para Interfaces, DHCP, etc.
                var ipService = new IpService(_api);
                var ips = await ipService.GetAll();

                dataGridViewMain.DataSource = ips;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CrudButton_Click(object sender, EventArgs e)
        {
            if (_currentRouter == null) return;

            var btn = sender as Button;

            switch (btn.Text)
            {
                case "Add":
                    await AddItemAsync();
                    break;
                case "Edit":
                    await EditItemAsync();
                    break;
                case "Delete":
                    await DeleteItemAsync();
                    break;
                case "Refresh":
                    LoadCurrentTabData();
                    break;
            }
        }

        // -----------------------------
        // Métodos Async para CRUD IPs
        // -----------------------------
        private async Task AddItemAsync()
        {
            if (_api == null) return;

            using (var form = new AddEditIPForm()) // Criar formulário para Add IP
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var ipService = new IpService(_api);
                    await ipService.Create(form.IpModel.address, form.IpModel.@interface);
                    LoadCurrentTabData();
                }
            }
        }

        private async Task EditItemAsync()
        {
            if (_api == null || dataGridViewMain.CurrentRow == null) return;

            var selected = dataGridViewMain.CurrentRow.DataBoundItem as IpAddressModel;
            if (selected == null) return;

            using (var form = new AddEditIPForm(selected)) // Form para editar
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var ipService = new IpService(_api);
                    await ipService.Update(form.IpModel.address, form.IpModel.disabled == "true");
                    LoadCurrentTabData();
                }
            }
        }

        private async Task DeleteItemAsync()
        {
            if (_api == null || dataGridViewMain.CurrentRow == null) return;

            var selected = dataGridViewMain.CurrentRow.DataBoundItem as InterfaceModel; // ou IpModel
            if (selected == null) return;

            if (MessageBox.Show("Deseja realmente apagar este item?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var ipService = new IpService(_api);
                await ipService.Delete(selected.name); // ou selected.Name
                LoadCurrentTabData();
            }
        }
    }
}
