using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.Models;
using TrelloApp.ViewModels;
using TrelloApp.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrelloApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardList : ContentPage
    {
        public BoardList()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<TrelloViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModelLocator.Instance.Resolve<TrelloViewModel>().BoardListCommand.Execute(null);
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var boardList = ((ListView)sender).SelectedItem as Board;
            if (boardList == null) return;
            ViewModelLocator.Instance.Resolve<TrelloViewModel>().SelectedBoardList = boardList;
            ListViewBoardListMenu.SelectedItem = null;
            Navigation.PushAsync(new ViewCardFromList());
        }


        private void NewMember_Clicked(object sender, EventArgs e)
        {

        }
    }
}