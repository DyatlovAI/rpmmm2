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
using MaterialDesignThemes.Wpf;

namespace rpmmm
{
    /// <summary>
    /// Логика взаимодействия для putAdd.xaml
    /// </summary>
    public partial class putAdd : Window
    {
        public putAdd()
        {
            InitializeComponent();
             Loaded += putAdds;
        }
        private void putAdds(object sender, RoutedEventArgs e)
        {
            InitializeComboBox();
            
        }
        private void InitializeComboBox()
        {
            using (trpoEntities db = new trpoEntities())
            {
                var zakazchikData = db.Admins.ToList();
                fio.ItemsSource = zakazchikData;
                fio.DisplayMemberPath = "SName";
                fio.SelectedValuePath = "Sname";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Admins selectedAdmin = (Admins)fio.SelectedItem;

                if (selectedAdmin == null)
                {
                    MessageBox.Show("Пожалуйста, выберите клиента.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string Strana = Passp.Text.Trim();
                string City = vozr.Text.Trim();
                string Datta = dat.Text.Trim();
                string Price = price.Text.Trim();

                StringBuilder errorMessage = new StringBuilder();
                bool hasError = false;

               
                if (!Regex.IsMatch(Strana, @"^[а-яА-Я]+$"))
                {
                    errorMessage.AppendLine("Пожалуйста, введите корректно (только буквы).");
                    hasError = true;
                }

           
                if (!Regex.IsMatch(City, @"^[а-яА-Я]+$"))
                {
                    errorMessage.AppendLine("Пожалуйста, введите корректно (только буквы).");
                    hasError = true;
                }

                if (!Regex.IsMatch(Datta, @"^[0-9-.]+$"))
                {
                    errorMessage.AppendLine("Пожалуйста, введите корректно дату (только буквы).");
                    hasError = true;
                }

                
                if (!Regex.IsMatch(Price, @"^[0-9_]+$"))
                {
                    errorMessage.AppendLine("Пожалуйста, введите корректно.");
                    hasError = true;
                }

                if (hasError)
                {
                    MessageBox.Show(errorMessage.ToString(), "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                using (trpoEntities db = new trpoEntities())
                {
                    put newEntity = new put
                    {
                        FIO = selectedAdmin.SName,
                        Strana = Strana,
                        City = City,
                        Datta = Datta,
                        Price = Price
                    };

                    db.put.Add(newEntity);
                    db.SaveChanges();
                    MessageBox.Show("Запись успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

