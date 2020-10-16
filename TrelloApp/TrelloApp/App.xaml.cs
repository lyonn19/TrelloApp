using System;
using TrelloApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrelloApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new ViewBoardList());
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
