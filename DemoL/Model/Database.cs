using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoL.Model
{
    public class Database
    {
        private readonly string _connectionString = "Server = (local); Database = Demo; Trusted_Connection = True";


      
            public bool CheckLogin(string username, string password)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        string query = "SELECT COUNT(*) FROM ПользователиБД WHERE login = @Username  AND  password = @Password";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password);

                            int count = (int)command.ExecuteScalar();
                            return count > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log($"Ошибка проверки логина: {ex.Message}");
                    return false;
                }
            }

            public DataTable GetRequests()
        {
            DataTable _dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                { 
                connection.Open();

                    string query = "SELECT * FROM ЗаявкиБД";

                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(_dataTable);
                    }
                }
            }
            catch(Exception ex) 
            {
                Logger.Log($"Ошибка при загрузке заявок: {ex.Message}");
            }
            return _dataTable;
        }

        public bool AddRequest(int requestId, DateTime startDate, string orgTechType, string orgTechModel, string problemDescription,
       string clientFio, long clientPhone, string requestStatus, int masterId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    // Проверяем, существует ли клиент с указанным телефоном
                    int clientId;
                    string checkClientQuery = @"SELECT userID FROM [ПользователиБД] WHERE phone = @Phone";
                    using (SqlCommand checkClientCommand = new SqlCommand(checkClientQuery, connection))
                    {
                        checkClientCommand.Parameters.AddWithValue("@Phone", clientPhone);
                        object result = checkClientCommand.ExecuteScalar();
                        if (result == null)
                        {
                            // Если клиента нет, добавляем его в базу данных
                            string insertClientQuery = @"INSERT INTO [ПользователиБД] (fio, phone) 
                                                     OUTPUT INSERTED.userID 
                                                     VALUES (@Fio, @Phone)";
                            using (SqlCommand insertClientCommand = new SqlCommand(insertClientQuery, connection))
                            {
                                insertClientCommand.Parameters.AddWithValue("@Fio", clientFio);
                                insertClientCommand.Parameters.AddWithValue("@Phone", clientPhone);
                                clientId = (int)insertClientCommand.ExecuteScalar();
                            }
                        }
                        else
                        {
                            clientId = (int)result;
                        }
                    }

                    // Добавляем заявку
                    string insertRequestQuery = @"INSERT INTO [ЗаявкиБД] 
                                              (requestID, startDate, orgTechType, orgTechModel, problemDescryption, 
                                               requestStatus, clientID, masterID) 
                                              VALUES 
                                              (@RequestID, @StartDate, @OrgTechType, @OrgTechModel, @ProblemDescription, 
                                               @RequestStatus, @ClientID, @MasterID)";
                    using (SqlCommand insertRequestCommand = new SqlCommand(insertRequestQuery, connection))
                    {
                        insertRequestCommand.Parameters.AddWithValue("@RequestID", requestId);
                        insertRequestCommand.Parameters.AddWithValue("@StartDate", startDate);
                        insertRequestCommand.Parameters.AddWithValue("@OrgTechType", orgTechType);
                        insertRequestCommand.Parameters.AddWithValue("@OrgTechModel", orgTechModel);
                        insertRequestCommand.Parameters.AddWithValue("@ProblemDescription", problemDescription);
                        insertRequestCommand.Parameters.AddWithValue("@RequestStatus", requestStatus);
                        insertRequestCommand.Parameters.AddWithValue("@ClientID", clientId);
                        insertRequestCommand.Parameters.AddWithValue("@MasterID", 0); // Пример: 0, если мастер не назначен
                        insertRequestCommand.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении заявки: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
