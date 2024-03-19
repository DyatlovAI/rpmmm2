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
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        private trpoEntities db;
        private List<users> allData;

        public Users()
        {
            InitializeComponent();
            db = new trpoEntities();
            LoadData();
        }

        private void LoadData()
        {
            allData = db.users?.ToList();
            dataGrid.ItemsSource = allData;
        }

        private users GetSelectedData()
        {
            var selectedRow = dataGrid.SelectedItem as users;
            return selectedRow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UsersAdd avtorizUser = new UsersAdd();
            avtorizUser.Show();

        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            var selectedData = GetSelectedData();
            userEdd editWindow = new userEdd(selectedData);
            editWindow.ShowDialog();
            if (editWindow.DialogResult == true)
            {
                var editedData = editWindow.GetEditedData();

            }
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is users selectedItem)
            {
                var existingItem = db.users.Find(selectedItem.Id_user); 
                if (existingItem != null)
                {
                    db.users.Remove(existingItem); 
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Объект не найден в базе данных.");
                }
            }
        }

        private void Button_Click_Reload(object sender, RoutedEventArgs e)
        {
            Users avtorizUser = new Users();
            avtorizUser.Show();
            Close();
        }
    }
}
