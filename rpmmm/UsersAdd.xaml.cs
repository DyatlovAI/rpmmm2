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
    /// Логика взаимодействия для UsersAdd.xaml
    /// </summary>
    public partial class UsersAdd : Window
    {
        public UsersAdd()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FIO = fio.Text;
                string Passport = Passp.Text;
                string age = vozr.Text;
                string birthday = dat.Text;
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

