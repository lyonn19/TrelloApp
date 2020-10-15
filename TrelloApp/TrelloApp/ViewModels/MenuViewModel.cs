using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.Models;
using TrelloApp.ViewModels.Base;
using Xamarin.Forms;

namespace TrelloApp.ViewModels
{
    public class MenuBoardViewModel : ViewModelBase
    {
        public MenuBoardViewModel()
        {
            MenuBoardItems = new ObservableCollection<MenuOptions>()
            {
                new MenuOptions
                {
                    Title = "optionx",
                    Icon = "ic_doctors.png",
                    // MenuItemType = typeof(DoctorBenefODAView),
                },
                new MenuOptions
                {
                    Title = "optionx",
                    Icon = "ic_doctors.png",
                    // MenuItemType = typeof(MedicalAppointBenfView),
                },
                new MenuOptions
                {
                    Title = "optionx",
                    Icon = "ic_img_exam.png",
                    // MenuItemType = typeof(ExamsSearchLabImg),
                },
                new MenuOptions
                {
                    Title = "optionx",
                    Icon = "ic_labs_exam.png",
                    // MenuItemType = typeof(ExamsSearchLabClinic),
                },
                new MenuOptions
                {
                    Title = "optionx",
                    Icon = "ic_hospital.png",
                    //MenuItemType = typeof(ClinicCenterSearchView),
                },
            };
        }

        public ObservableCollection<MenuOptions> MenuBoardItems { get; set; }

        private Type _pageName;
        public Type PageName
        {
            get { return _pageName; }
            set
            {
                _pageName = value;
                OnPropertyChanged();
            }
        }

        public async Task NavigateToPageAsyc()
        {
            await Application.Current.MainPage.Navigation.PushAsync((Page)Activator.CreateInstance(PageName));
        }

        Command _navigateToCommand;
        public Command NavigateToCommand
        {
            get
            {
                return _navigateToCommand ?? (_navigateToCommand = new Command(async() => await NavigateToPage(), () => !IsBusy));
            }
        }

        public async Task NavigateToPage()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await NavigateToPageAsyc();
            }
            finally
            {
                IsBusy = false;
            }
        }
    
    }
}
