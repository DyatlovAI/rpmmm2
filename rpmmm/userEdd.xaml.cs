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
        public userEdd(users dataToEdit)
        {
            InitializeComponent();
            _originalData = dataToEdit;
            _userId = dataToEdit.Id_user;


            fio.Text = dataToEdit.FIO;
            Passp.Text = dataToEdit.Passport;
            vozr.Text = dataToEdit.age;
            dat.Text = dataToEdit.birthday;
            
        }
        public users GetEditedData()
        {

            var editedData = new users
            {
                Id_user = int.Parse(id.Text),
                FIO = fio.Text,
                Passport = Passp.Text,
                age = vozr.Text,
                birthday = dat.Text,

            };


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
                    existingData.Id_user = editedData.Id_user;
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

            return
                    editedData.Id_user != _originalData.Id_user ||
                   editedData.FIO != _originalData.FIO ||
                   editedData.Passport != _originalData.Passport ||
                   editedData.age != _originalData.age ||
                   editedData.birthday != _originalData.birthday;
        }
    }
}
