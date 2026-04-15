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
                    // O form de cadastro deve adicionar o novo router � lista RouterRepository.Routers
                    // antes de retornar DialogResult.OK

                    // 2. Salvar a lista atualizada no ficheiro JSON
                    SessionManager.SaveSessions(RouterRepository.Routers);

                    // 3. Atualizar a ComboBox no FormMain
                    AtualizarCombo();

                    MessageBox.Show("Router guardado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshRoutersDropdown();
                    comboRouters.SelectedIndex = RouterRepository.Routers.Count - 1; // Seleciona novo router
                }
            }
        }
        private void BtnDelRouter_Click(object sender, EventArgs e)
        {
            // 1. Verificar se existe algo selecionado
            var selected = comboRouters.SelectedItem as RouterDevice;

            if (selected == null)
            {
                MessageBox.Show("Selecione um router para apagar.");
                return;
            }

            // 2. Confirma��o
            var confirm = MessageBox.Show($"Deseja apagar o router '{selected.Name}'?", "Confirmar",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                // 3. Remover da lista est�tica
                RouterRepository.Routers.Remove(selected);

                // 4. Salvar no ficheiro JSON para n�o voltar a aparecer ao reiniciar
                SessionManager.SaveSessions(RouterRepository.Routers);

                // 5. Atualizar a interface
                AtualizarCombo();

                // 6. Limpar o Grid se n�o houver mais routers
                if (RouterRepository.Routers.Count == 0)
                {
                    dataGridViewMain.DataSource = null;
                }

                MessageBox.Show("Router removido.");
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
        // M�todos Async para CRUD IPs
        // -----------------------------
        private async Task AddItemAsync()
        {
            if (_api == null) return;

            using (var form = new AddEditIPForm()) // Criar formul�rio para Add IP
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

        // Fun�oes auxiliares
        private void AtualizarCombo()
        {
            comboRouters.DataSource = null; // Reset
            comboRouters.DataSource = RouterRepository.Routers;
            comboRouters.DisplayMember = "Name";

            // Opcional: Selecionar o �ltimo item se a lista n�o estiver vazia
            if (comboRouters.Items.Count > 0)
                comboRouters.SelectedIndex = comboRouters.Items.Count - 1;
        }
    }
}
