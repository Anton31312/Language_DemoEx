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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        List<VM_ClientList> clientList = new List<VM_ClientList>();

        private List<string> listSort = new List<string>() // Формарование листа для сортировки
        {
            "По фамилии (от А до Я)",
            "По дате последнего посещения (от новых к старым)",
            "Количеству посещений (от большего к меньшему)"
        };

        private List<string> listFilter = new List<string>()
        {
            "Все",
            "Мужской", 
            "Женский"
        };

        private List<string> listSelectCountPage = new List<string>()
        {
            "10",
            "50",
            "200",
            "Все"
        };

        bool checkBirthday = false;
        int numberPage = 0;
        int countClient;

        private void Filter() // Поиск, сортировка, постраничный вывод, фильтрация
        {
            if (checkBirthday)
            {
                clientList = AppData.Context.VM_ClientList.ToList(); // получение всех клиентов из БД
                lvClient.ItemsSource = clientList;
                // Поиск
                clientList = clientList.
                    Where(i => i.LastName.ToLower().Contains(txtSearch.Text.ToLower()) || i.FirstName.ToLower().Contains(txtSearch.Text.ToLower())
                        || i.Patronymic.ToLower().Contains(txtSearch.Text.ToLower()) || i.Email.ToLower().Contains(txtSearch.Text.ToLower())
                        || i.Phone.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

                // Фильтрация
                switch (cmbFilter.SelectedIndex)
                {
                    case 0:
                        clientList = clientList.OrderBy(i => i.ID).ToList(); // фильтрация по возрастанию
                        break;

                    case 1:
                        clientList = clientList.Where(i => i.Gender == "мужской").ToList(); // фильтрация по мужскому полу
                        break;

                    case 2:
                        clientList = clientList.Where(i => i.Gender == "женский").ToList(); // фильтрация по женскому полу
                        break;

                    default:
                        clientList = clientList.OrderBy(i => i.ID).ToList();
                        break;
                }

                // Сортировка
                switch (cmbSort.SelectedIndex)
                {
                    case 0:
                        clientList = clientList.OrderBy(i => i.LastName).ToList(); // сортировка по возрастанию
                        break;

                    case 1:
                        clientList = clientList.OrderByDescending(i => i.DateLastVisit).ToList(); // сортировка по убыванию
                        break;

                    case 2:
                        clientList = clientList.OrderByDescending(i => i.CountVisit).ToList();
                        break;

                    default:
                        clientList = clientList.OrderByDescending(i => i.LastName).ToList();
                        break;
                }

                countClient = clientList.Count;

                // Постраничный вывод
                switch (cmbSelectCountPage.SelectedIndex)
                {
                    case 0:
                        lvClient.ItemsSource = clientList.Skip(numberPage * 10).Take(10).ToList();
                        break;

                    case 1:
                        lvClient.ItemsSource = clientList.Skip(numberPage * 50).Take(50).ToList();
                        break;

                    case 2:
                        lvClient.ItemsSource = clientList.Skip(numberPage * 200).Take(200).ToList();
                        break;
                    case 3:
                        lvClient.ItemsSource = clientList.ToList();
                        break;

                    default:
                        lvClient.ItemsSource = clientList.Skip(numberPage * 10).Take(10).ToList();
                        break;
                }
            }
            else
            {
                clientList = AppData.Context.VM_ClientList.ToList(); // получение всех клиентов из БД
                lvClient.ItemsSource = clientList;
                // Поиск
                clientList = clientList.
                    Where(i => i.LastName.ToLower().Contains(txtSearch.Text.ToLower()) || i.FirstName.ToLower().Contains(txtSearch.Text.ToLower())
                        || i.Patronymic.ToLower().Contains(txtSearch.Text.ToLower()) || i.Email.ToLower().Contains(txtSearch.Text.ToLower())
                        || i.Phone.ToLower().Contains(txtSearch.Text.ToLower()) && i.DateBith.Month == DateTime.Now.Month).ToList();

                // Фильтрация
                switch (cmbFilter.SelectedIndex)
                {
                    case 0:
                        clientList = clientList.OrderBy(i => i.ID).ToList(); // фильтрация по возрастанию
                        break;

                    case 1:
                        clientList = clientList.Where(i => i.Gender == "мужской").ToList(); // фильтрация по мужскому полу
                        break;

                    case 2:
                        clientList = clientList.Where(i => i.Gender == "женский").ToList(); // фильтрация по женскому полу
                        break;

                    default:
                        clientList = clientList.OrderBy(i => i.ID).ToList();
                        break;
                }

                // Сортировка
                switch (cmbSort.SelectedIndex)
                {
                    case 0:
                        clientList = clientList.OrderBy(i => i.LastName).ToList(); // сортировка по возрастанию
                        break;

                    case 1:
                        clientList = clientList.OrderByDescending(i => i.DateLastVisit).ToList(); // сортировка по убыванию
                        break;

                    case 2:
                        clientList = clientList.OrderByDescending(i => i.CountVisit).ToList();
                        break;

                    default:
                        clientList = clientList.OrderByDescending(i => i.LastName).ToList();
                        break;
                }

                countClient = clientList.Count;

                // Постраничный вывод
                switch (cmbSelectCountPage.SelectedIndex)
                {
                    case 0:
                        lvClient.ItemsSource = clientList.Skip(numberPage * 10).Take(10).ToList();
                        break;

                    case 1:
                        lvClient.ItemsSource = clientList.Skip(numberPage * 50).Take(50).ToList();
                        break;

                    case 2:
                        lvClient.ItemsSource = clientList.Skip(numberPage * 200).Take(200).ToList();
                        break;
                    case 3:
                        lvClient.ItemsSource = clientList.ToList();
                        break;

                    default:
                        lvClient.ItemsSource = clientList.Skip(numberPage * 10).Take(10).ToList();
                        break;
                }
            }
        }

        public ClientWindow()
        {
            InitializeComponent();

            cmbFilter.ItemsSource = listFilter;  // заполнеие ComboBox для фильтрации
            cmbFilter.SelectedIndex = 0;

            cmbSort.ItemsSource = listSort; // заполнеие ComboBox для сортировки
            cmbSort.SelectedIndex = 0;

            cmbSelectCountPage.ItemsSource = listSelectCountPage;  // заполнеие ComboBox для постраничного вывода (выбор кол-во записей)
            cmbSelectCountPage.SelectedIndex = 0;

            Filter();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditClientWindow addClient = new AddEditClientWindow();
            this.Opacity = 0.2;
            addClient.ShowDialog();
            this.Opacity = 1;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            cmbSort.SelectedIndex = 0;
            cmbFilter.SelectedIndex = 0;
            Filter();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            numberPage = 0;
            tbCountPage.Text = "1";
            Filter();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numberPage = 0;
            tbCountPage.Text = "1";
            Filter();
        }

        private void BtnLastPage_Click(object sender, RoutedEventArgs e)
        {
            if (numberPage > 0)
            {
                numberPage--;

                tbCountPage.Text = (numberPage + 1).ToString();

                Filter();
            }
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {

            if (((countClient / 10) > numberPage && cmbSelectCountPage.SelectedIndex == 0) || ((countClient / 50) > numberPage && cmbSelectCountPage.SelectedIndex == 1) || ((countClient / 200) > numberPage && cmbSelectCountPage.SelectedIndex == 2))
            {
                numberPage++;

                tbCountPage.Text = (numberPage + 1).ToString();

                Filter();

            }

        }

        private void CmbSelectCountPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numberPage = 0;
            if (cmbSelectCountPage.SelectedIndex == 0)
            {
                tbIs.Visibility = Visibility.Visible;
                tbCountPage.Visibility = Visibility.Visible;
                tbAllCountPage.Visibility = Visibility.Visible;
                btnLastPage.Visibility = Visibility.Visible;
                btnNextPage.Visibility = Visibility.Visible;
                tbAllCountPage.Text = Convert.ToString((countClient / 10));
                tbCountPage.Text = "1";
            }
            if (cmbSelectCountPage.SelectedIndex == 1)
            {
                tbIs.Visibility = Visibility.Visible;
                tbCountPage.Visibility = Visibility.Visible;
                tbAllCountPage.Visibility = Visibility.Visible;
                btnLastPage.Visibility = Visibility.Visible;
                btnNextPage.Visibility = Visibility.Visible;
                tbAllCountPage.Text = Convert.ToString((countClient / 50));
                tbCountPage.Text = "1";
            }
            if (cmbSelectCountPage.SelectedIndex == 2)
            {
                tbIs.Visibility = Visibility.Visible;
                tbCountPage.Visibility = Visibility.Visible;
                tbAllCountPage.Visibility = Visibility.Visible;
                btnLastPage.Visibility = Visibility.Visible;
                btnNextPage.Visibility = Visibility.Visible;
                tbAllCountPage.Text = Convert.ToString((countClient / 200) + 1);
                tbCountPage.Text = "1";
            }
            if (cmbSelectCountPage.SelectedIndex == 3)
            {
                tbCountPage.Text = Convert.ToString(countClient);
                tbAllCountPage.Text = Convert.ToString(countClient);
                btnLastPage.Visibility = Visibility.Hidden;
                btnNextPage.Visibility = Visibility.Hidden;
            }

            Filter();
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numberPage = 0;
            tbCountPage.Text = "1";
            Filter();
        }

        private void CbBithNow_Checked(object sender, RoutedEventArgs e)
        {
            numberPage = 0;
            tbCountPage.Text = "1";
            checkBirthday = true;
            Filter();
        }

        private void CbBithNow_Unchecked(object sender, RoutedEventArgs e)
        {
            numberPage = 0;
            tbCountPage.Text = "1";
            checkBirthday = false;
            Filter();
        }

        private void LvClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editClient = new EF.VM_ClientList();

            if (lvClient.SelectedItem is EF.VM_ClientList)
            { 
                editClient = lvClient.SelectedItem as EF.VM_ClientList;
            }
            AddEditClientWindow editClientWindow = new AddEditClientWindow(editClient);
            this.Opacity = 0.2;
            editClientWindow.ShowDialog();
            this.Opacity = 1;
            Filter();
        }
    }
}
