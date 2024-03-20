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
