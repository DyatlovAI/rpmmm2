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
    /// Логика взаимодействия для putEdd.xaml
    /// </summary>
    public partial class putEdd : Window
    {
        private put _originalData;
        private int _userId;
        public putEdd(put dataToEdit)
        {
            InitializeComponent();
            _originalData = dataToEdit;
            _userId = dataToEdit.Id_put;


            fio.Text = dataToEdit.FIO;
            Passp.Text = dataToEdit.Strana;
            vozr.Text = dataToEdit.City;
            dat.Text = dataToEdit.Datta;
            price.Text = dataToEdit.Price;
        }
        public put GetEditedData()
        {

            var editedData = new put
            {
                Id_put = int.Parse(id.Text),
                FIO = fio.Text,
                Strana = Passp.Text,
                City = vozr.Text,
                Datta = dat.Text,
                Price = price.Text,

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
                var existingData = dbContext.put.Find(_userId);

                if (existingData != null)
                {
                    existingData.Id_put = editedData.Id_put;
                    existingData.FIO = editedData.FIO;
                    existingData.Strana = editedData.Strana;
                    existingData.City = editedData.City;
                    existingData.Datta = editedData.Datta;
                    existingData.Price = editedData.Price;
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
                    editedData.Id_put != _originalData.Id_put ||
                   editedData.FIO != _originalData.FIO ||
                   editedData.Strana != _originalData.Strana ||
                   editedData.City != _originalData.City ||
                   editedData.Datta != _originalData.Datta ||
                   editedData.Price != _originalData.Price;
        }
    }
}
