using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
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
        private ComboBox comboResources;

        private enum ResourceType
        {
            IPs,
            Interfaces,
            Wireless,
            Bridges,
            Routes,
            DHCP,
            DNS,
            SecurityProfiles
        }

        private ResourceType _currentResource = ResourceType.IPs;

        public FormMain()
        {
            InitializeComponent();

            // Create a runtime ComboBox to choose the resource to display
            comboResources = new ComboBox
            {
                Name = "comboResources",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 180
            };

            // Add resource options (adjust names as you prefer)
            comboResources.Items.AddRange(new object[]
            {
                "IPs",
                "Interfaces",
                "Wireless",
                "Bridges",
                "Routes",
                "DHCP",
                "DNS",
                "Security Profiles"
            });

            // Position the combo next to comboRouters when possible
            try
            {
                // If comboRouters exists (from designer), position relative to it
                comboResources.Left = comboRouters.Right + 8;
                comboResources.Top = comboRouters.Top;
            }
            catch
            {
                // Fallback position
                comboResources.Left = 10;
                comboResources.Top = 10;
            }

            // Default selection
            comboResources.SelectedIndex = 0;
            comboResources.SelectedIndexChanged += (s, e) => LoadCurrentTabData();

            // Add to form controls so it's visible
            this.Controls.Add(comboResources);

            // Wire resource buttons to set resource type
            WireResourceButtons();

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
                    // O form de cadastro deve adicionar o novo router à lista RouterRepository.Routers
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

        private async void LoadCurrentTabData()
        {
            if (_currentRouter == null) return;

            try
            {
                switch (_currentResource)
                {
                    case ResourceType.Interfaces:
                        var ifaceService = new InterfaceService(_api);
                        dataGridViewMain.DataSource = await ifaceService.GetAll();
                        break;
                    case ResourceType.Wireless:
                        var wirelessService = new WirelessService(_api);
                        dataGridViewMain.DataSource = await wirelessService.GetAllInterfaces();
                        break;
                    case ResourceType.Bridges:
                        var bridgeService = new BridgeService(_api);
                        dataGridViewMain.DataSource = await bridgeService.GetAll();
                        break;
                    case ResourceType.Routes:
                        var routeService = new RouteService(_api);
                        dataGridViewMain.DataSource = await routeService.GetAll();
                        break;
                    case ResourceType.DHCP:
                        var dhcpService = new DhcpService(_api);
                        dataGridViewMain.DataSource = await dhcpService.GetAllServers();
                        break;
                    case ResourceType.DNS:
                        var dnsService = new DnsService(_api);
                        var dnsSettings = await dnsService.GetSettings();
                        dataGridViewMain.DataSource = new[] { dnsSettings };
                        break;
                    case ResourceType.SecurityProfiles:
                        var spService = new SecurityProfileService(_api);
                        dataGridViewMain.DataSource = await spService.GetAll();
                        break;
                    case ResourceType.IPs:
                    default:
                        var ipService = new IpService(_api);
                        dataGridViewMain.DataSource = await ipService.GetAll();
                        break;
                }
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
        // Métodos Async para CRUD IPs (keeps existing behavior for IPs)
        // -----------------------------
        private async Task AddItemAsync()
        {
            if (_api == null) return;

            // Behavior remains targeted at IPs; adapt per-resource later
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

        // Funções auxiliares
        private void AtualizarCombo()
        {
            comboRouters.DataSource = null; // Reset
            comboRouters.DataSource = RouterRepository.Routers;
            comboRouters.DisplayMember = "Name";

            // Opcional: Selecionar o último item se a lista não estiver vazia
            if (comboRouters.Items.Count > 0)
                comboRouters.SelectedIndex = comboRouters.Items.Count - 1;
        }

        // add this helper method to set resource from button clicks
        private void SetResource(ResourceType resource)
        {
            _currentResource = resource;

            // Update visual state of buttons (adjust names to match your Designer)
            // Example: highlight the selected button and reset others
            Action<Button, bool> setSelected = (btn, sel) =>
            {
                if (btn == null) return;
                btn.BackColor = sel ? System.Drawing.SystemColors.Highlight : System.Drawing.SystemColors.Control;
                btn.ForeColor = sel ? System.Drawing.SystemColors.HighlightText : System.Drawing.SystemColors.ControlText;
            };

            setSelected(btnIPs, resource == ResourceType.IPs);
            setSelected(btnInterfaces, resource == ResourceType.Interfaces);
            setSelected(btnWireless, resource == ResourceType.Wireless);
            setSelected(btnBridges, resource == ResourceType.Bridges);
            setSelected(btnRoutes, resource == ResourceType.Routes);
            setSelected(btnDhcp, resource == ResourceType.DHCP);
            setSelected(btnDns, resource == ResourceType.DNS);
            setSelected(btnSecurityProfiles, resource == ResourceType.SecurityProfiles);

            // Optionally enable/disable CRUD buttons depending on resource
            // e.g., some resources are read-only or need different verbs
            btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = true;
            switch (resource)
            {
                case ResourceType.DNS:
                    // DNS settings may be a single record — keep Add disabled if appropriate
                    // btnAdd.Enabled = false;
                    break;
            }

            LoadCurrentTabData();
        }

        // In your constructor (after InitializeComponent) wire buttons to call SetResource:
        // Example:
        private void WireResourceButtons()
        {
            btnIPs.Click += (s, e) => SetResource(ResourceType.IPs);
            btnInterfaces.Click += (s, e) => SetResource(ResourceType.Interfaces);
            btnWireless.Click += (s, e) => SetResource(ResourceType.Wireless);
            btnBridges.Click += (s, e) => SetResource(ResourceType.Bridges);
            btnRoutes.Click += (s, e) => SetResource(ResourceType.Routes);
            btnDhcp.Click += (s, e) => SetResource(ResourceType.DHCP);
            btnDns.Click += (s, e) => SetResource(ResourceType.DNS);
            btnSecurityProfiles.Click += (s, e) => SetResource(ResourceType.SecurityProfiles);
        }
    }
}
