using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace rpmmm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static AppData AppData { get; private set; } = new AppData();
    }
    public class AppData
    {
        public int ID_Ispolnitel { get; set; }
        public Nullable<int> IdZakaz { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Reyt { get; set; }
        public string Rekvezit { get; set; }
        public string Loginad { get; set; }
        public string passwordd { get; set; }

    }
}
