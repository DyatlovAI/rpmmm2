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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rpmmm
{
    /// <summary>
    /// Логика взаимодействия для topPanel.xaml
    /// </summary>
    public partial class topPanel : UserControl
    {
        public topPanel()
        {
            InitializeComponent();
        }
        private void CloseWindow()
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gl avtorizUser = new gl();
            avtorizUser.Show();
            this.CloseWindow();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Put avtorizUser = new Put();
            avtorizUser.Show();
            this.CloseWindow();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Users avtorizUser = new Users();
            avtorizUser.Show();
            this.CloseWindow();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            avtoriz avtorizUser = new avtoriz();
            avtorizUser.Show();
            this.CloseWindow();
        }
    }
}
