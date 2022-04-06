using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Fedyaev_Language_01.EF;
using Fedyaev_Language_01.ClassHelper;

namespace Fedyaev_Language_01.Windows
{
    /// <summary>
    /// Логика взаимодействия для VisitClientWindow.xaml
    /// </summary>
    public partial class VisitClientWindow : Window
    {
        Client client = new Client();
        List<ClientService> clientServices = new List<ClientService>();
        public VisitClientWindow(Client clientVisit)
        {
            InitializeComponent();
            client = clientVisit;

            clientServices = AppData.Context.ClientService.ToList();
            lvVisitClient.ItemsSource = clientServices.Where(i => i.IDClient == clientVisit.ID);
        }
    }
}
