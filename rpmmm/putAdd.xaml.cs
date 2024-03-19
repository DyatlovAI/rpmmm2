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
    /// Логика взаимодействия для putAdd.xaml
    /// </summary>
    public partial class putAdd : Window
    {
        public putAdd()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string FIO = fio.Text;
                string Strana = Passp.Text;
                string City = vozr.Text;
                string Datta = dat.Text;
                string Price = price.Text;
                using (trpoEntities db = new trpoEntities())
                {

                    put newEntity = new put
                    {

                        FIO = FIO,
                        Strana = Strana,
                        City = City,
                        Datta = Datta,
                        Price = Price

                    };

                    db.put.Add(newEntity);
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

