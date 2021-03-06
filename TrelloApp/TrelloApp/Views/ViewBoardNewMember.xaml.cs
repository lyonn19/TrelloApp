﻿using System;
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
    public partial class ViewBoardNewMember : ContentPage
    {
        public ViewBoardNewMember()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Instance.Resolve<TrelloViewModel>();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;
            ViewModelLocator.Instance.Resolve<TrelloViewModel>().NewBoardMemberCommand.Execute(null);
        }

        private bool ValidateFields()
        {
            bool result = true;
            EmailValidator.Validate();
            if (!EmailValidator.IsValid)
            {
                result = false;
            }

            return result;
        }
    }
}