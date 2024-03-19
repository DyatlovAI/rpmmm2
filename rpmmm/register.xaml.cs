using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace rpmmm
{
    /// <summary>
    /// Логика взаимодействия для register.xaml
    /// </summary>
    public partial class register : Window
    {
        public register()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            avtoriz avtorizUser = new avtoriz();
            avtorizUser.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string login = log.Text;
            string firstName = firstt.Text;
            string secondName = second.Text;
            string patronymic = third.Text;
            string email = em.Text;
            string password = pas1.Password;
            string repeatPassword = pas2.Password;
            

            StringBuilder errorMessage = new StringBuilder();
            bool hasError = false;

            if (!Regex.IsMatch(firstName, @"^[а-яА-Яa-zA-Z]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректное имя (только буквы).");
                hasError = true;
            }

            if (!Regex.IsMatch(secondName, @"^[а-яА-Яa-zA-Z]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректную фамилию (только буквы).");
                hasError = true;
            }
            if (!Regex.IsMatch(patronymic, @"^[а-яА-Яa-zA-Z]+$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректное отчество (только буквы).");
                hasError = true;
            }

            if (!Regex.IsMatch(login, @"^[a-zA-Z0-9_]{4,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный логин (только буквы, цифры и _).");
                hasError = true;
            }
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9_\.]+@[a-zA-Z0-9_]+\.[a-zA-Z]{2,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный email.");
                hasError = true;
            }


            if (!Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*[0-9!()-_]).{8,}$"))
            {
                errorMessage.AppendLine("Пожалуйста, введите корректный пароль (минимум 8 символов, хотя бы 1 буква, цифра или символ !()-_).");
                hasError = true;
            }

            if (hasError)
            {
                MessageBox.Show(errorMessage.ToString());
                return;
            }

            using (trpoEntities db = new trpoEntities())
            {
                Admins newUser = new Admins
                {
                    FName = firstName,
                    SName = secondName,
                    TName = patronymic,
                    logg = login,
                    pass = password,
                    email = email // предполагая, что email соответствует реквизитам
                };

                db.Admins.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("Регистрация успешна.");
            }

        }
    }
}
