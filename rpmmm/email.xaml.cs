using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Логика взаимодействия для email.xaml
    /// </summary>
    public partial class email : Window
    {
        public email()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (trpoEntities db = new trpoEntities())
                {
                    
                    string password = db.Admins.Where(a => a.email == second.Text).Select(a => a.pass).FirstOrDefault();

                    
                    if (!string.IsNullOrEmpty(password))
                    {
                       
                        MailMessage m = new MailMessage();
                        m.From = new MailAddress("artem.dyatlov.2023@mail.ru", "TravelB");
                        m.To.Add(new MailAddress(second.Text));
                        m.Subject = "ПАРОЛЬ";
                        m.Body = "<h1>Пароль: " + password + "</h1>";
                        m.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                        smtp.Credentials = new NetworkCredential("artem.dyatlov.2023@mail.ru", "WmkwQSPgvPg9KV4hBVZY");
                        smtp.EnableSsl = true;
                        smtp.Send(m);

                        MessageBox.Show("Пароль отправлен на почту.");
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким email не найден.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            avtoriz avtorizUser = new avtoriz();
            avtorizUser.Show();
            this.Close();
        }
    }
}

