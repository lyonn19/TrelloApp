using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.Models;
using TrelloApp.Services.API;
using TrelloApp.ViewModels.Base;
using Xamarin.Forms;

namespace TrelloApp.ViewModels
{
    public class TrelloViewModel : ViewModelBase
    {
        private readonly ITrelloService _trelloService;

        public TrelloViewModel(ITrelloService trelloService)
        {
            _trelloService = trelloService;
            
            BoardList = new ObservableCollection<Board>();
            CardsList = new ObservableCollection<Card>();

            SelectedBoardList = new Board();
            SelectedCardsList = new Card();
        }

        // Properties
        private Board _selectedBoardList;
        public Board SelectedBoardList
        {
            get { return _selectedBoardList; }
            set
            {
                _selectedBoardList = value;
                OnPropertyChanged();
            }
        }

        private Card _selectedCardsList;
        public Card SelectedCardsList
        {
            get { return _selectedCardsList; }
            set
            {
                _selectedCardsList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Board> BoardList { get; set; }
        public ObservableCollection<Card> CardsList { get; set; }

        // Functions
        private async Task GetBoardListAsync()
        {
            try
            {
                var boards = await _trelloService.GetBoardList();

                if (boards.Any())
                {
                    foreach (var item in boards)
                    {
                        BoardList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                /// await Application.Current.MainPage.DisplayAlert(AppResources.Information, AppResources.ErrorServerResponse, AppResources.Accept);
            }
        }


        private async Task GetCardsListAsync()
        {
            try
            {
                var boards = await _trelloService.GetCardsFromList(SelectedBoardList.id);

                if (boards.Any())
                {
                    foreach (var item in boards)
                    {
                        CardsList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                /// await Application.Current.MainPage.DisplayAlert(AppResources.Information, AppResources.ErrorServerResponse, AppResources.Accept);
            }
        }

        // Commands
        Command _boardListCommand;
        public Command BoardListCommand
        {
            get
            {
                return _boardListCommand ?? (_boardListCommand = new Command(async () => await BoardListAsync(), () => !IsBusy));
            }
        }

        public async Task BoardListAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                if(BoardList.Count == 0) await GetBoardListAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command _cardsListCommand;
        public Command CardsListCommand
        {
            get
            {
                return _cardsListCommand ?? (_cardsListCommand = new Command(async () => await CardsListAsync(), () => !IsBusy));
            }
        }

        public async Task CardsListAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                CardsList.Clear();
                await GetCardsListAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
