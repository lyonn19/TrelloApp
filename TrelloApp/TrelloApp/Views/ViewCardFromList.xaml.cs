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
    public partial class ViewCardFromList : ContentPage
    {
        public ViewCardFromList()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<TrelloViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModelLocator.Instance.Resolve<TrelloViewModel>().CardsListCommand.Execute(null);
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cards = ((ListView)sender).SelectedItem as Card;
            if (cards == null) return;
            ViewModelLocator.Instance.Resolve<TrelloViewModel>().SelectedCard = cards;
            ListViewCardList.SelectedItem = null;
            Navigation.PushAsync(new ViewCard());
        }

        private void NewCard_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewCreateCard());
        }
    }
}