using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.Models;

namespace TrelloApp.Services.API
{
    public interface ITrelloService
    {
        Task<IEnumerable<Board>> GetBoardList(string boardId);
        Task<IEnumerable<Card>> GetCardsFromList(string cardListId);
        Task<Card> GetCard(string cardId);
        Task<bool> PostNewBoardMember(string boardId, string email);
        Task<bool> CreateCard(string cardListId, Card card);
        Task<bool> AddAttachmentToCard(string cardId, byte[] imagen);
    }
}
