using System;
using System.Collections.Generic;
using System.Text;
using TrelloApp.Services.API;
using TrelloApp.ViewModels.Base;

namespace TrelloApp.ViewModels
{
    public class TrelloViewModel : ViewModelBase
    {
        private readonly ITrelloService _trelloService;

        public TrelloViewModel(ITrelloService trelloService)
        {
            _trelloService = trelloService;
        }


    }
}
