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
    public partial class ViewCreateCard : ContentPage
    {
        public ViewCreateCard()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<TrelloViewModel>();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModelLocator.Instance.Resolve<TrelloViewModel>().CreateCardCommand.Execute(null);
        }
    }
}