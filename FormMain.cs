using MikroSDN.Models;
using MikroSDN.Services;

namespace MikroSDN
{
    public partial class FormMain : Form
    {
        private MikrotikService _api;
        public FormMain()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var router = new RouterDevice
            {
                Name = "Router1",
                IP = "192.168.88.1",
                Username = "api_user",
                Password = "123456"
            };

            var api = new MikrotikService(router);
            var interfaceService = new InterfaceService(api);

            try
            {
                var interfaces = await interfaceService.GetAllInterfaces();

                foreach (var i in interfaces)
                {
                    Console.WriteLine(i.name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}
