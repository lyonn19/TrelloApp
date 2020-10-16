using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.Services.API;
using Xamarin.Forms;

namespace TrelloApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

           
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var trelloService = new TrelloService();
            await trelloService.GetBoardList(AppSettings.BoardId);
            //await trelloService.PostNewBoardMember(AppSettings.BoardId, "lyonn@gmail.com");
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var trelloService = new TrelloService();
            await trelloService.GetCardsFromList("5c11cc68dc16a31d7eb52f33");
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            var trelloService = new TrelloService();
            await trelloService.GetCard("5c1ab760cf80b9032af79741");
        }
    }
}
