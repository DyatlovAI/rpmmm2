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
    /// Логика взаимодействия для userEdd.xaml
    /// </summary>
    public partial class userEdd : Window
    {
        private users _originalData;
        private int _userId;
        public userEdd(users dataToEdit, int userId)
        {
            InitializeComponent();
            _originalData = dataToEdit;
            _userId = userId; 
            id.Text = userId.ToString();
            fio.Text = dataToEdit.FIO;
            Passp.Text = dataToEdit.Passport;
            vozr.Text = dataToEdit.age;
            dat.Text = dataToEdit.birthday;
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
        public users GetEditedData()
        {
            var editedData = new users();
            if (int.TryParse(id.Text, out int userId))
            {
                editedData.Id_user = userId;
            }
            else
            {
                MessageBox.Show("Некорректный формат идентификатора пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            editedData.FIO = fio.Text;
            editedData.Passport = Passp.Text;
            editedData.age = vozr.Text;
            editedData.birthday = dat.Text;

            return editedData;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsDataChanged())
            {
                MessageBox.Show("Данные не были изменены.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var editedData = GetEditedData();

            
            if (!IsValidData(editedData))
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (var dbContext = new trpoEntities())
            {

                var existingData = dbContext.users.Find(_userId);

                if (existingData != null)
                {
                    existingData.FIO = editedData.FIO;
                    existingData.Passport = editedData.Passport;
                    existingData.age = editedData.age;
                    existingData.birthday = editedData.birthday;

                    dbContext.SaveChanges();
                }
            }

            DialogResult = true;
            Close();
        }
        private bool IsValidData(users data)
        {
            
            if (!IsStranaValid(data.FIO))
                return false;

            if (!IsCityValid(data.Passport))
                return false;

            

            return true;
        }

        private bool IsStranaValid(string FIO)
        {
           
            return Regex.IsMatch(FIO, @"^[а-яА-Я]+$");
        }

        private bool IsCityValid(string Passport)
        {
            
            return Regex.IsMatch(Passport, @"^[0-9 _ы]+$");
        }

       

        private bool IsDataChanged()
        {
            var editedData = GetEditedData();

            if (editedData == null)
            {
                return false;
            }

            return
                editedData.Id_user != _originalData.Id_user ||
                editedData.FIO != _originalData.FIO ||
                editedData.Passport != _originalData.Passport ||
                editedData.age != _originalData.age ||
                editedData.birthday != _originalData.birthday;
        }
    }
}
