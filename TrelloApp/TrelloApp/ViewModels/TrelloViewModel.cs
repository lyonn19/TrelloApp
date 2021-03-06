﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.Models;
using TrelloApp.Services.API;
using TrelloApp.Utils;
using TrelloApp.ViewModels.Base;
using Xamarin.Essentials;
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
            SelectedCard = new Card();
            DetailCard = new Card();
        }

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

        private Card _detailCard;
        public Card DetailCard
        {
            get { return _detailCard; }
            set
            {
                _detailCard = value;
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
        public string CardName
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

        private byte[] _imagenToUpload;
        public byte[] ImagenToUpload
        {
            get { return _imagenToUpload; }
            set 
            {
                _imagenToUpload = value;
                OnPropertyChanged();
            }
        }

        private int _attachmentsCount;
        public int AttachmentCount
        {
            get { return _attachmentsCount; }
            set
            {
                _attachmentsCount = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Board> BoardList { get; set; }
        public ObservableCollection<Card> CardsList { get; set; }

        private async Task GetBoardListAsync()
        {
            try
            {
                var boards = await _trelloService.GetBoardList(AppSettings.BoardId);

                if (boards.Any())
                {
                    foreach (var item in boards)
                    {

                        var result = await _trelloService.GetCardsFromList(item.Id);
                       
                        BoardList.Add(new Board()
                        {
                            Id = item.Id,
                            Closed = item.Closed,
                            IdBoard = item.IdBoard,
                            Name = item.Name,
                            Pos = item.Pos,
                            SoftLimit = item.SoftLimit,
                            Subscribed = item.Subscribed,
                            CardsCount = result.Count()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error interno en el servidor", "Aceptar");
            }
        }

        private async Task GetCardsListAsync()
        {
            try
            {
                var boards = await _trelloService.GetCardsFromList(SelectedBoardList.Id);

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

        private async Task GetAttachmentOnCard()
        {
            try
            {
                var result = await _trelloService.GetAttachmentsOnCard(SelectedCard.Id);

                AttachmentCount = result.Count();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ocurrio un error interno en el servidor", "Aceptar");
            }
        }

        private async Task PostNewBoardMemberAsync()
        {
            try
            {
                var result = await _trelloService.PostNewBoardMember(AppSettings.BoardId, NewMemeberEmail);
                if(!result)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "an error occurred creating the card", "Accept");
                }

                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An internal server error occurred", "Accept");
            }
        }

        private async Task GetCardAsync()
        {
            try
            {
                DetailCard =  await _trelloService.GetCard(SelectedCard.Id);
                
                await GetAttachmentOnCard();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An internal server error occurred", "Accept");
            }
        }

        private async Task CreateNewCardAsync()
        {
            try
            {
                var result = await _trelloService.CreateCard(SelectedBoardList.Id, new Card() { Name = CardName, Desc = CardDescription });
                if (!result)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "an error occurred creating the card", "Accept");
                }
                
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An internal server error occurred", "Accept");
            }
        }

        private async Task CreateAttachmentToCardAsync()
        {
            try
            {
                var result = await _trelloService.AddAttachmentToCard(SelectedCard.Id, ImagenToUpload);
                if (!result)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "an error occurred attaching file", "Accept");
                }

                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An internal server error occurred", "Accept");
            }
        }

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
                BoardList.Clear();
                await GetBoardListAsync();
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
                CardsListCommand.Execute(null);
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
                CardCommand.Execute(null);
            }
        }
                       
    }
}
