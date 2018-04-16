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
using System.Data;
using System.Data.SqlClient; //пределяет функциональность провайдера для баз данных MS SQL Server
using System.Configuration;

namespace Nabu
{

    public partial class MainWindow : Window
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        //РЕГИСТРАЦИЯ
        private void OnRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            bool isLoginCorrect = CheckLogin(NewLogin, Alert_NewLogin);
            bool isPasswordCorrect = CheckPassword(NewPassword, Alert_NewPassword);

            if (isLoginCorrect && isPasswordCorrect)
            {
                try
                {
                    // Создание подключения                  
                    string sqlExpression = $"INSERT USERS VALUES('{NewLogin.Text}', '{ NewPassword.Password}','{UserType.Text}')";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                    }
               
                switch (UserType.SelectedIndex)
                {
                    case 0:
                        UserMenu userMenu = new UserMenu();
                        userMenu.Show();
                        this.Close();
                        break;
                    case 1:
                        CreaterMenu createrMenu = new CreaterMenu();
                        createrMenu.Show();
                        this.Close();
                        break;
                }
                }                
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        NewLogin.BorderBrush = new SolidColorBrush(Colors.Orange);
                        Alert_NewLogin.Visibility = Visibility.Visible;
                        Alert_NewLogin.ToolTip = "Пользователь с таким именем уже существует";
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                   
                }               
            }
        }


        //ВХОД
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            bool isLoginCorrect = CheckLogin(Login, Alert_Login);
            bool isPasswordCorrect =  CheckPassword(Password, Alert_Password);

            if (isLoginCorrect && isPasswordCorrect)
            {
                try
                {
                    // Создание подключения                  
                    string sqlExpression = $"SELECT * FROM USERS WHERE ([LOGIN] = '{Login.Text}')";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows) // если есть данные
                        {
                            if (reader.Read())
                            {
                                if ( CheckPassword(Password.Password, reader.GetValue(2).ToString()))
                                {
                                    if (reader.GetValue(3).ToString() == "User")
                                    {
                                        UserMenu userMenu = new UserMenu();
                                        userMenu.Show();
                                        this.Close();
                                    }
                                    else
                                    {
                                        CreaterMenu createrMenu = new CreaterMenu();
                                        createrMenu.Show();
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(Password.Password+" vs "+reader.GetValue(2).ToString());
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("пользователь не существует");
                        }
                    }                  
                }
                catch (SqlException ex)
                {                    
                    MessageBox.Show(ex.Message);                    
                }               
            }
        }
    }
}
