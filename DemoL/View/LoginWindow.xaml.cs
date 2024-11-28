using DemoL.Model;
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

namespace DemoL.View
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private readonly Database _database = new Database();

        public LoginWindow()
        {
            InitializeComponent();
        }
        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UserTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                if (_database.CheckLogin(username, password))
                {
                    MessageBox.Show("Авторизация успешна", "Подтвердить", MessageBoxButton.OK, MessageBoxImage.Information);
                    Logger.Log($"Успешная авторизация: пользователь {username}");
                    ManageRequestWindow listWindow = new ManageRequestWindow();
                    listWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль", "Отказать", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logger.Log($"Неудачная попытка входа: пользователь {username}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Обнаружена ошибка: {ex.Message}\n смотреть логи для подробностей");
            }

        }
    }
}
