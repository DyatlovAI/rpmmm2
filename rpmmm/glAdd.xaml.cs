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
using Xceed.Wpf.Toolkit;

namespace rpmmm
{
    /// <summary>
    /// Логика взаимодействия для glAdd.xaml
    /// </summary>
    public partial class glAdd : Window
    {
        public glAdd()
        {
            InitializeComponent();
            Loaded += glAdds;
        }
        private void glAdds(object sender, RoutedEventArgs e)
        {
            InitializeComboBox();
            InitializeComboBox2();
        }
        private void InitializeComboBox()
        {
            using (trpoEntities db = new trpoEntities())
            {
                var zakazchikData = db.users.ToList();
                firstt.ItemsSource = zakazchikData;
                firstt.DisplayMemberPath = "FIO";
                firstt.SelectedValuePath = "FIO";
            }
        }
        private void InitializeComboBox2()
        {
            using (trpoEntities db = new trpoEntities())
            {
                var putData = db.put.ToList();
                second.ItemsSource = putData;
                second.DisplayMemberPath = "Strana";
                second.SelectedValuePath = "Strana";
                // Добавляем обработчик события выбора элемента
                second.SelectionChanged += Second_SelectionChanged;
            }
        }

        private void Second_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var selectedPut = second.SelectedItem as put;
            if (selectedPut != null)
            {
                
                date.Text = selectedPut.Datta.ToString(); 
                price.Text = selectedPut.Price.ToString(); 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (trpoEntities db = new trpoEntities())
                {
                    var selectedPut = second.SelectedItem as put;
                    var selectedUser = firstt.SelectedItem as users;

                    string FIO = string.Empty;
                    if (selectedUser != null)
                    {
                        FIO = selectedUser.FIO;
                    }
                    string Strana = string.Empty;
                    if (selectedPut != null)
                    {
                        Strana = selectedPut.Strana;
                    }

                    if (!string.IsNullOrEmpty(FIO) && !string.IsNullOrEmpty(Strana))
                    {
                        // Получаем значения даты и цены из текстовых полей
                        string datta = date.Text;
                        string priceValue = price.Text;

                        // Создаем новый объект zakaz и заполняем его данными
                        zakaz newEntity = new zakaz
                        {
                            FIO = FIO,
                            Strana = Strana,
                            Datta = datta,
                            Price = priceValue
                        };

                        // Добавляем объект в контекст данных и сохраняем изменения
                        db.zakaz.Add(newEntity);
                        db.SaveChanges();

                        System.Windows.MessageBox.Show("Запись успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Не удалось получить данные из базы данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
