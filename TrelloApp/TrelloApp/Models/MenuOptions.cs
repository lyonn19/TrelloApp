using System;
using System.Collections.Generic;
using System.Text;
using TrelloApp.ViewModels;
using TrelloApp.ViewModels.Base;
using Xamarin.Forms;

namespace TrelloApp.Models
{
    public class MenuOptions : BindableObject
    {
        #region Propidedades
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _icon;
        public string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private bool _switch;
        public bool Switch
        {
            get { return _switch; }
            set
            {
                _switch = value;
                OnPropertyChanged();
            }
        }

        private Type _menuItemType;
        public Type MenuItemType
        {
            get { return _menuItemType; }
            set
            {
                _menuItemType = value;
                OnPropertyChanged();
            }
        }

        #endregion 

    }
}
