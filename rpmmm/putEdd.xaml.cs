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
    /// Логика взаимодействия для putEdd.xaml
    /// </summary>
    public partial class putEdd : Window
    {
        private put _originalData;
        private int _userId;
        private int _putId;
        public putEdd(put dataToEdit,  int putId)
        {
            InitializeComponent();
            _originalData = dataToEdit;
            //_userId = userId;
            _putId = putId;
            
            id.Text = putId.ToString();
            fio.Text = dataToEdit.FIO;
            Passp.Text = dataToEdit.Strana;
            vozr.Text = dataToEdit.City;
            dat.Text = dataToEdit.Datta;
            price.Text = dataToEdit.Price;
        }
        public put GetEditedData()
        {
            var editedData = new put();   
            if (int.TryParse(id.Text, out int putId))
            {
               

                editedData.Id_put = putId;
                editedData.FIO = fio.Text;
                editedData.Strana = Passp.Text;
                editedData.City = vozr.Text;
                editedData.Datta = dat.Text;
                editedData.Price = price.Text;

                return editedData;
            }
            else
            {
                MessageBox.Show("Некорректный формат идентификатора пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        
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
                var existingData = dbContext.put.Find(_putId);

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

        private bool IsValidData(put data)
        {
           
            if (!IsStranaValid(data.Strana))
                return false;

            if (!IsCityValid(data.City))
                return false;

            if (!IsDattaValid(data.Datta))
                return false;

            if (!IsPriceValid(data.Price))
                return false;

            return true;
        }

        private bool IsStranaValid(string strana)
        {
            
            return Regex.IsMatch(strana, @"^[а-яА-Я]+$");
        }

        private bool IsCityValid(string city)
        {
            
            return Regex.IsMatch(city, @"^[а-яА-Я]+$");
        }

        private bool IsDattaValid(string datta)
        {
            
            return Regex.IsMatch(datta, @"^[0-9-.]+$");
        }

        private bool IsPriceValid(string price)
        {
            
            return Regex.IsMatch(price, @"^[0-9_]+$");
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
