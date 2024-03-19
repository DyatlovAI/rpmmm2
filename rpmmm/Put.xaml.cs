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
    /// Логика взаимодействия для Put.xaml
    /// </summary>
    public partial class Put : Window
    {
        private trpoEntities db;
        private List<put> allData2;
        public Put()
        {
            InitializeComponent();
            db = new trpoEntities();
            LoadData();
        }
        private void LoadData()
        {
            allData2 = db.put?.ToList();
            dataGrid.ItemsSource = allData2;
        }
        private put GetSelectedData()
        {
            var selectedRow = dataGrid.SelectedItem as put;
            return selectedRow;
        }
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            var selectedData = GetSelectedData();
            if (selectedData is put)
            {
                putEdd editWindow = new putEdd(selectedData);
                editWindow.ShowDialog();
                if (editWindow.DialogResult == true)
                {
                    var editedData = editWindow.GetEditedData();
                   
                }
            }
            else
            {
                MessageBox.Show("Выбранный элемент не является типом put.");
            }
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem is put selectedItem)
            {
                var existingItem = db.put.Find(selectedItem.Id_put);
                if (existingItem != null)
                {
                    db.put.Remove(existingItem); 
                    db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Объект не найден в базе данных.");
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            putAdd avtorizUser = new putAdd();
            avtorizUser.Show();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Put avtorizUser = new Put();
            avtorizUser.Show();
            Close();
        }
    }
}
