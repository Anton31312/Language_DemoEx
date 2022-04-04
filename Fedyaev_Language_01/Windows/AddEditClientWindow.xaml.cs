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
using Microsoft.Win32;
using System.IO;

namespace Fedyaev_Language_01.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditClientWindow.xaml
    /// </summary>
    public partial class AddEditClientWindow : Window
    {
        List<Tag> tags = new List<Tag>();
        VM_ClientList editClient = new VM_ClientList();
        bool isEdit = true; // изменяем или добавляем пользователя
        string pathPhoto = null; // Для сохранения пути к изображению

        public AddEditClientWindow()
        {
            InitializeComponent();

            cmbGender.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "NameGender";
            cmbGender.SelectedIndex = 0;

            isEdit = false;
        }

        public AddEditClientWindow(VM_ClientList client)
        {
            InitializeComponent();

            cmbGender.ItemsSource = AppData.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "NameGender";

            editClient = client;

            dpDateBirth.SelectedDate = editClient.DateBith;
            txtLastName.Text = editClient.LastName;
            txtFirstName.Text = editClient.FirstName;
            txtPatronymic.Text = editClient.Patronymic;
            txtEmail.Text = editClient.Email;
            txtPhone.Text = editClient.Phone;
            cmbGender.SelectedIndex = editClient.IDGender - 1;

            tags = AppData.Context.Tag.ToList();
            lvTagClient.ItemsSource = tags;
            //tags = tags.Where(i => i.NameTag).ToList();

            isEdit = true;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            #region

            //Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPatronymic.Text))
            {
                MessageBox.Show("Поле Отчество не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поле Телефон не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Поле Email не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //проверка на количество символов
            if (txtLastName.Text.Length > 100)
            {
                MessageBox.Show("Поле Фамилия не может содержать больше 100 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtFirstName.Text.Length > 100)
            {
                MessageBox.Show("Поле Имя не может содержать больше 100 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPatronymic.Text.Length > 100)
            {
                MessageBox.Show("Поле Отчество не может содержать больше 100 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhone.Text.Length > 20)
            {
                MessageBox.Show("Поле Телефон не может содержать больше 20 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtEmail.Text.Length > 100)
            {
                MessageBox.Show("Поле Email не может содержать больше 100 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion


            if (isEdit)
            {
                //Проверка на ошибки в БД
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите изменение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        //Изменения клиента
                        editClient.LastName = txtLastName.Text;
                        editClient.FirstName = txtFirstName.Text;
                        editClient.Patronymic = txtPatronymic.Text;
                        editClient.Email = txtEmail.Text;
                        editClient.Phone = txtPhone.Text;
                        editClient.IDGender = cmbGender.SelectedIndex + 1;
                        editClient.DateBith = (DateTime)dpDateBirth.SelectedDate;

                        AppData.Context.SaveChanges();
                        MessageBox.Show("Клиент успешно изменён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    throw;
                }
            }

            else
            {
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (resultClick == MessageBoxResult.Yes)
                    {
                        Client client = new Client();
                        client.LastName = txtLastName.Text;
                        client.FirstName = txtFirstName.Text;
                        client.Patronymic = txtPatronymic.Text;
                        client.Email = txtEmail.Text;
                        client.Phone = txtPhone.Text;
                        client.IDGender = cmbGender.SelectedIndex + 1;
                        client.Photo = pathPhoto;
                        client.DateBith = (DateTime)dpDateBirth.SelectedDate;

                        AppData.Context.Client.Add(client);
                        AppData.Context.SaveChanges();
                        MessageBox.Show("Клиент успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    throw;
                }
            }
        }

        private void BtnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imgClient.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                pathPhoto = openFileDialog.FileName;
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddTag_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
