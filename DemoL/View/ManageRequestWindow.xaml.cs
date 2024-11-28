using DemoL.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ManageRequestWindow.xaml
    /// </summary>
    public partial class ManageRequestWindow : Window
    {
        private Database _database = new Database();


        public ManageRequestWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DataTable requests = _database.GetRequests();
            DataGridRequests.ItemsSource = requests.DefaultView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            RequestDialogWindow dialog = new RequestDialogWindow();
            if (dialog.ShowDialog() == true)
            {
                var requestData = dialog.Request;

                // Логика добавления заявки в базу данных
                bool success = _database.AddRequest(
                    requestId: requestData.RequestId,
                    startDate: requestData.StartDate,
                    orgTechType: requestData.OrgTechType,
                    orgTechModel: requestData.OrgTechModel,
                    problemDescription: requestData.ProblemDescription,
                    clientFio: requestData.ClientFio,
                    clientPhone: requestData.ClientPhone,
                    requestStatus: requestData.RequestStatus,
                     masterId: requestData.MasterId

                );

                if (success)
                {
                    MessageBox.Show("Заявка успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при добавлении заявки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
