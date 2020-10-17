using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.ViewModels;
using TrelloApp.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrelloApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCard : ContentPage
    {
        public ViewCard()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<TrelloViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModelLocator.Instance.Resolve<TrelloViewModel>().CardCommand.Execute(null);
        }

        private void NewAttachment_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new ViewAddAttachment());
            
        }
    }
}