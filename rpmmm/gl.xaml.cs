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
    /// Логика взаимодействия для gl.xaml
    /// </summary>
    public partial class gl : Window
    {
        private trpoEntities db;
        private List<zakaz> allData;
        public gl()
        {
            InitializeComponent();
            db = new trpoEntities();
            LoadData();
        }
        private void LoadData()
        {
            allData = db.zakaz?.ToList();
            dataGrid.ItemsSource = allData;
        }
        private zakaz GetSelectedData()
        {
            var selectedRow = dataGrid.SelectedItem as zakaz;
            return selectedRow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            glAdd avtorizUser = new glAdd();
            avtorizUser.Show();
            
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is zakaz selectedItem)
            {
                var existingItem = db.zakaz.Find(selectedItem.Id_zakaz);
                if (existingItem != null)
                {
                    db.zakaz.Remove(existingItem);
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Объект не найден в базе данных.");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            gl avtorizUser = new gl();
            avtorizUser.Show();
            Close();
        }
    }
}
