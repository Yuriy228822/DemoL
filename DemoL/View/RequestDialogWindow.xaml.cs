using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DemoL.Model;

namespace DemoL.View
{
    /// <summary>
    /// Логика взаимодействия для RequestDialogWindow.xaml
    /// </summary>
    public partial class RequestDialogWindow : Window
    {
        public RequestData Request { get; private set; } // Данные заявки

        public RequestDialogWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение данных из полей
                Request = new RequestData
                {
                    RequestId = int.Parse(RequestIdTextBox.Text),
                    StartDate = StartDatePicker.SelectedDate ?? DateTime.Now,
                    OrgTechType = OrgTechTypeTextBox.Text,
                    OrgTechModel = OrgTechModelTextBox.Text,
                    ProblemDescription = ProblemDescriptionTextBox.Text,
                    ClientFio = ClientFioTextBox.Text,
                    ClientPhone = long.Parse(ClientPhoneTextBox.Text),
                    RequestStatus = ((ComboBoxItem)RequestStatusComboBox.SelectedItem).Content.ToString(),
                    MasterId = int.Parse(MasterIdTextBox.Text)
                };

                DialogResult = true; // Устанавливаем успешное завершение
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Отмена
            Close();
        }
    }

 
}
