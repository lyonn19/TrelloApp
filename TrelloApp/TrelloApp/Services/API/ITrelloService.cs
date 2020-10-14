using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.Models;

namespace TrelloApp.Services.API
{
    public interface ITrelloService
    {
        Task<IEnumerable<Board>> GetBoardList();
        Task<IEnumerable<Card>> GetCardsFromList(string cardListId);
        Task<Card> GetCard(string cardId);
        Task<object> InviteMember(string boardId, string email);
        Task<object> CreateCard(string cardIdList);
        Task<object> AddAttachmentToCard(string cardId);

    }
}
