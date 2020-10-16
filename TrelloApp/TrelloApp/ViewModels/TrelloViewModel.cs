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
            CardsList = new ObservableCollection<Cards>();

            SelectedBoardList = new Board();
            SelectedCardsList = new Cards();
            SelectedCard = new Card();
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

        private Cards _selectedCardsList;
        public Cards SelectedCardsList
        {
            get { return _selectedCardsList; }
            set
            {
                _selectedCardsList = value;
                OnPropertyChanged();
            }
        }

        private Card _selectedCard;
        public Card SelectedCard
        {
            get { return _selectedCard; }
            set
            {
                _selectedCard = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string NewMemeberEmail
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _cardName;
        public string Cardname
        {
            get { return _cardName; }
            set
            {
                _cardName = value;
                OnPropertyChanged();
            }
        }

        private string _cardDescription;
        public string CardDescription
        {
            get { return _cardDescription; }
            set
            {
                _cardDescription = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Board> BoardList { get; set; }
        public ObservableCollection<Cards> CardsList { get; set; }

        // Functions
        // Board 
        private async Task GetBoardListAsync()
        {
            try
            {
                var boards = await _trelloService.GetBoardList(AppSettings.BoardId);

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
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error interno en el servidor", "Aceptar");
            }
        }

        // Card list
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
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error interno en el servidor", "Aceptar");
            }
        }

        // New member to board
        private async Task PostNewBoardMemberAsync()
        {
            try
            {
                await _trelloService.PostNewBoardMember(AppSettings.BoardId, NewMemeberEmail);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error interno en el servidor", "Aceptar");
            }
        }

        // View Card
        private async Task GetCardAsync()
        {
            try
            {
                await _trelloService.GetCard(SelectedCard.Id);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error interno en el servidor", "Aceptar");
            }
        }

        // Create Card
        private async Task CreateNewCardAsync()
        {
            try
            {
                await _trelloService.CreateCard(SelectedCard.Id);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error interno en el servidor", "Aceptar");
            }
        }

        // Add Attachment to Card
        private async Task CreateAttachmentToCardAsync()
        {
            try
            {
                await _trelloService.AddAttachmentToCard(SelectedCard.Id);
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error interno en el servidor", "Aceptar");
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

        Command _newBoardMemberCommand;
        public Command NewBoardMemberCommand
        {
            get
            {
                return _newBoardMemberCommand ?? (_newBoardMemberCommand = new Command(async () => await NewBoardMemberAsync(), () => !IsBusy));
            }
        }
        public async Task NewBoardMemberAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await PostNewBoardMemberAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command _cardCommand;
        public Command CardCommand
        {
            get
            {
                return _cardCommand ?? (_cardCommand = new Command(async () => await CardAsync(), () => !IsBusy));
            }
        }
        public async Task CardAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await GetCardAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command _createCardCommand;
        public Command CreateCardCommand
        {
            get
            {
                return _createCardCommand ?? (_createCardCommand = new Command(async () => await AddCardAsync(), () => !IsBusy));
            }
        }
        public async Task AddCardAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await CreateNewCardAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command _addAttachmentToCardCommand;
        public Command AttachmentToCardCommand
        {
            get
            {
                return _addAttachmentToCardCommand ?? (_addAttachmentToCardCommand = new Command(async () => await AddAttachmentCardAsync(), () => !IsBusy));
            }
        }
        public async Task AddAttachmentCardAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await CreateAttachmentToCardAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
