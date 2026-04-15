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
                    // O form de cadastro deve adicionar o novo router à lista RouterRepository.Routers
                    // antes de retornar DialogResult.OK

                    // 2. Salvar a lista atualizada no ficheiro JSON
                    SessionManager.SaveSessions(RouterRepository.Routers);

                    // 3. Atualizar a ComboBox no FormMain
                    AtualizarCombo();

                    MessageBox.Show("Router guardado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // 2. Confirmação
            var confirm = MessageBox.Show($"Deseja apagar o router '{selected.Name}'?", "Confirmar",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                // 3. Remover da lista estática
                RouterRepository.Routers.Remove(selected);

                // 4. Salvar no ficheiro JSON para não voltar a aparecer ao reiniciar
                SessionManager.SaveSessions(RouterRepository.Routers);

                // 5. Atualizar a interface
                AtualizarCombo();

                // 6. Limpar o Grid se não houver mais routers
                if (RouterRepository.Routers.Count == 0)
                {
                    dataGridViewMain.DataSource = null;
                }

                MessageBox.Show("Router removido.");
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

        // Funçoes auxiliares
        private void AtualizarCombo()
        {
            comboRouters.DataSource = null; // Reset
            comboRouters.DataSource = RouterRepository.Routers;
            comboRouters.DisplayMember = "Name";

            // Opcional: Selecionar o último item se a lista não estiver vazia
            if (comboRouters.Items.Count > 0)
                comboRouters.SelectedIndex = comboRouters.Items.Count - 1;
        }
    }
}
