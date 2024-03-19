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
    /// Логика взаимодействия для avtoriz.xaml
    /// </summary>
    public partial class avtoriz : Window
    {
        public avtoriz()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            register avtorizUser = new register();
            avtorizUser.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (log.Text != "" && log.Text != "Логин" && pas.Password != "")
            {
                using (trpoEntities db = new trpoEntities())
                {
                    bool isUserFound = false;

                    foreach (Admins user in db.Admins)
                    {
                        if (user.logg == log.Text && user.pass == pas.Password)
                        {
                            MessageBox.Show("Вход успешен");
                            Put gl = new Put();
                            gl.Show();
                            this.Close();
                            log.Text = "";
                            pas.Password = "";
                            isUserFound = true;
                            break;
                        }
                    }

                    if (!isUserFound)
                    {
                        MessageBox.Show("Пользователь не найден");
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            email avtorizUser = new email();
            avtorizUser.Show();
            this.Close();
        }
    }
}
