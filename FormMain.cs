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
            InitializeCustomComponents();
        }
        private async void FormMain_Load(object sender, EventArgs e)
        {
            // Inicialização extra (se quiseres carregar algo ao abrir)
            var combo = this.Controls["comboRouters"] as ComboBox;
            if (combo != null && combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0; // seleciona o primeiro router
            }
        }
        private void InitializeCustomComponents()
        {
            // Panel menu lateral
            var panelMenu = new Panel
            {
                Dock = DockStyle.Left,
                Width = 150,
                BackColor = System.Drawing.Color.LightGray
            };
            this.Controls.Add(panelMenu);

            string[] menuItems = { "Interfaces", "IP", "DHCP", "DNS", "Routes", "Wireless" };
            int btnTop = 10;
            foreach (var item in menuItems)
            {
                var btn = new Button
                {
                    Text = item,
                    Width = 130,
                    Height = 30,
                    Top = btnTop,
                    Left = 10
                };
                btn.Click += MenuButton_Click;
                panelMenu.Controls.Add(btn);
                btnTop += 40;
            }

            // Top-right ComboBox + button +
            var comboRouters = new ComboBox
            {
                Name = "comboRouters",
                Left = 160,
                Top = 10,
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            comboRouters.SelectedIndexChanged += ComboRouters_SelectedIndexChanged;
            this.Controls.Add(comboRouters);

            var btnAddRouter = new Button
            {
                Text = "+",
                Left = 370,
                Top = 10,
                Width = 30,
                Height = 25
            };
            btnAddRouter.Click += BtnAddRouter_Click;
            this.Controls.Add(btnAddRouter);

            // DataGridView para conteúdo
            var dgv = new DataGridView
            {
                Name = "dataGridViewMain",
                Top = 50,
                Left = 160,
                Width = 600,
                Height = 400,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgv);

            // Botões CRUD abaixo do DataGridView
            int btnCrudTop = dgv.Top + dgv.Height + 10;
            string[] actions = { "Add", "Edit", "Delete", "Refresh" };
            int btnLeft = 160;
            foreach (var act in actions)
            {
                var btn = new Button
                {
                    Text = act,
                    Left = btnLeft,
                    Top = btnCrudTop,
                    Width = 80
                };
                btn.Click += CrudButton_Click;
                this.Controls.Add(btn);
                btnLeft += 90;
            }

            // Carregar lista de routers no combo
            comboRouters.DataSource = RouterRepository.Routers;
            comboRouters.DisplayMember = "Name";
        }

        // Menu lateral clicado
        private void MenuButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            MessageBox.Show($"Menu {btn.Text} clicado");
            // Aqui depois podes alternar entre Interfaces, IP, DHCP etc.
        }

        // ComboBox mudou de router
        private void ComboRouters_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            _currentRouter = combo.SelectedItem as RouterDevice;
            if (_currentRouter != null)
            {
                _api = new MikrotikService(_currentRouter);
                LoadCurrentTabData(); // Atualiza DataGrid
            }
        }

        // Botão + para adicionar router
        private void BtnAddRouter_Click(object sender, EventArgs e)
        {
            //using (var form = new AddRouterForm())
            //{
            //    if (form.ShowDialog() == DialogResult.OK)
            //    {
            //        // Atualiza ComboBox
            //        var combo = this.Controls["comboRouters"] as ComboBox;
            //        combo.DataSource = null;
            //        combo.DataSource = RouterRepository.Routers;
            //        combo.DisplayMember = "Name";
            //    }
            //}
        }

        // Botões CRUD
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

        // Carregar dados no DataGridView
        private async void LoadCurrentTabData()
        {
            var dgv = this.Controls["dataGridViewMain"] as DataGridView;
            if (_currentRouter == null) return;

            // Exemplo: sempre mostrar IPs por enquanto
            var ipService = new IpService(_api);
            try
            {
                var ips = await ipService.GetAll();
                dgv.DataSource = ips;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }
    }
}
