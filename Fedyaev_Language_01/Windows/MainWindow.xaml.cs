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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fedyaev_Language_01.Windows;

namespace Fedyaev_Language_01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();

            clientWindow.ShowDialog();
            this.Close();
        }

        private void BtnTag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnClientService_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
