using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nabu
{
    public partial class MainWindow : Window
    {
        //корректность логина
        private bool CheckLogin(TextBox login, Image image)
        {
            string regex = @"[0-9a-zA-Z]";
            bool valid = true;
            ToolTip toolTip = new ToolTip();

            for (int i = 0; i < login.Text.Length; i++)
            {
                if (Regex.IsMatch(login.Text[i].ToString(), regex)  == false)
                {
                    valid = false;
                    toolTip.Content = "Wrong symbol";
                }
            }  
            
            if(login.Text.Length == 0)
            {
                valid = false;
                toolTip.Content = "Can not be empty";
            }

             if(valid == false)
            {
                login.BorderBrush = new SolidColorBrush(Colors.Orange);              
                image.Visibility = Visibility.Visible;
                image.ToolTip = toolTip;
            }
            return valid;
        }

        //корректность пароля
        private bool CheckPassword(PasswordBox passwordBox, Image image)
        {
            string regex = @"[0-9a-zA-Z]";
            bool valid = true;
            ToolTip toolTip = new ToolTip();           

            for (int i = 0; i < passwordBox.Password.Length; i++)
            {
                if (Regex.IsMatch(passwordBox.Password[i].ToString(), regex) == false)
                {
                    valid = false;
                    toolTip.Content = "Wrong symbol";
                }
            }

            if (passwordBox.Password.Length == 0)
            {
                valid = false;
                toolTip.Content = "Can not be empty";
            }

            if (valid == false)
            {
                passwordBox.BorderBrush = new SolidColorBrush(Colors.Orange);                
                image.Visibility = Visibility.Visible;
                image.ToolTip = toolTip;
            }
            return valid;
        }
        
        //скрытие кнопки
        private void OnPasswordChanged_1(object sender, RoutedEventArgs e)
        {
            if (Password.Password.Length == 8)
            {
                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (NewPassword.Password.Length == 8)
            {
                registrationButton.IsEnabled = true;
            }
            else
            {
                registrationButton.IsEnabled = false;
            }
        }

        public bool CheckPassword(string s1, string s2)
        {
            bool isEqual = true;

            for(int  i=0;i< s1.Length; i++)
            {
                if (s1[i] != s2[i])
                    isEqual = false;
            }

            return isEqual;
        }

    }
}
