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
        Task<IEnumerable<Cards>> GetCardsFromList(string cardListId);
        Task<Cards> GetCard(string cardId);
        Task<bool> PostNewBoardMember(string boardId, string email);
        Task<object> CreateCard(string cardIdList);
        Task<object> AddAttachmentToCard(string cardId);
    }
}
