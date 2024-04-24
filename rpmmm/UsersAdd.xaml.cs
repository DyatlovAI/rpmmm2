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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace rpmmm
{
    /// <summary>
    /// Логика взаимодействия для UsersAdd.xaml
    /// </summary>
    public partial class UsersAdd : Window
    {
        public UsersAdd()
        {
            InitializeComponent();
        }
        private void dat_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dat.SelectedDate.HasValue)
            {
                DateTime birthdate = dat.SelectedDate.Value;
                int age = CalculateAge(birthdate);
                vozr.Text = age.ToString();
            }
        }

        private int CalculateAge(DateTime birthdate)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthdate.Year;
            if (birthdate > now.AddYears(-age))
                age--;
            return age;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FIO = fio.Text;
                string Passport = Passp.Text;
                string age = vozr.Text;
                string birthday = dat.Text;
                StringBuilder errorMessage = new StringBuilder();
                bool hasError = false;


                if (!Regex.IsMatch(FIO, @"^[а-яА-Я]+$"))
                {
                    errorMessage.AppendLine("Пожалуйста, введите корректно (только буквы).");
                    hasError = true;
                }


                if (!Regex.IsMatch(Passport, @"^[0-9 _]+$"))
                {
                    errorMessage.AppendLine("Пожалуйста, введите корректно пасспорт.");
                    hasError = true;
                }

                if (!Regex.IsMatch(birthday, @"^[0-9-.]+$"))
                {
                    errorMessage.AppendLine("Пожалуйста, введите корректно дату рождения");
                    hasError = true;
                }


                if (!Regex.IsMatch(age, @"^[0-9_]+$"))
                {
                    errorMessage.AppendLine("Пожалуйста, введите корректно возраст.");
                    hasError = true;
                }

                if (hasError)
                {
                    MessageBox.Show(errorMessage.ToString(), "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                using (trpoEntities db = new trpoEntities())
                {
                   
                        users newEntity = new users
                        {

                            FIO = FIO,
                            Passport = Passport,
                            age = age,
                            birthday = birthday

                        };

                        db.users.Add(newEntity);
                        db.SaveChanges();
                        System.Windows.MessageBox.Show("Запись успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                   
                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}

