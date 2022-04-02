using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HeySavings.Services;
using HeySavings.Views;
using SQLite;
using HeySavings.SQL_Lite;

[assembly: ExportFont("Lato-Regular.ttf", Alias = "LocalFont1")]

[assembly: ExportFont("Helvetica.ttf", Alias = "LocalFont")]
namespace HeySavings
{
    public partial class App : Application
    {
        public static SqlHelper Database;
        public static LoginTable login;

        public App()
        {
            InitializeComponent();

           
            Database = new SqlHelper();
            login = new LoginTable();
            
            DependencyService.Register<MockDataStore>();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
