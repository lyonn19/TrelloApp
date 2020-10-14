﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.Models;

namespace TrelloApp.Services.API
{
    public class TrelloService : RestService, ITrelloService
    {

        const string ENDPOINT_BASE = "https://api.trello.com";

        public TrelloService(): base(ENDPOINT_BASE) { }

        public async Task<IEnumerable<Board>> GetBoardList()
        {
            IEnumerable<Board> result = new List<Board>();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", "a9f2cc2bbe00cfc3e93460eb0de6e361" },
                    { "token", "97af0bfb10fec01fd940a129dbcf32b3a46570a15d07206c068cb201ff4abfd7" }
                };

                var remoteResponse = await GetAsync("/1/boards/lr4DfEjt/lists", parameters);
                var content = await remoteResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<IEnumerable<Board>>(content);

                if (response != null)
                {
                    result = response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }
            return result;
        }

        public async Task<IEnumerable<Card>> GetCardsFromList(string cardListId)
        {
            IEnumerable<Card> result = new List<Card>();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", "a9f2cc2bbe00cfc3e93460eb0de6e361" },
                    { "token", "97af0bfb10fec01fd940a129dbcf32b3a46570a15d07206c068cb201ff4abfd7" }
                };

                var remoteResponse = await GetAsync($"/1/lists/{cardListId}/cards", parameters);
                var content = await remoteResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<IEnumerable<Card>>(content);

                if (response != null)
                {
                    result = response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }
            return result;
        }

        public async Task<Card> GetCard(string cardId)
        {
            Card result = new Card();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", "a9f2cc2bbe00cfc3e93460eb0de6e361" },
                    { "token", "97af0bfb10fec01fd940a129dbcf32b3a46570a15d07206c068cb201ff4abfd7" }
                };

                var remoteResponse = await GetAsync($"/1/cards/{cardId}", parameters);
                var content = await remoteResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Card>(content);

                if (response != null)
                {
                    result = response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }
            return result;
        }

        public async Task<object> InviteMember(string boardId, string email)
        {
            Object result = new object();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", "a9f2cc2bbe00cfc3e93460eb0de6e361" },
                    { "token", "97af0bfb10fec01fd940a129dbcf32b3a46570a15d07206c068cb201ff4abfd7" },
                    { "email", email },
                };

                var remoteResponse = await PutAsync($"/1/boards/{boardId}/members", parameters);
                var content = await remoteResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<object>(content);

                if (response != null)
                {
                    result = response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }
            return result;
        }

        public async Task<object> CreateCard(string cardIdList)
        {
            Object result = new object();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", "a9f2cc2bbe00cfc3e93460eb0de6e361" },
                    { "token", "97af0bfb10fec01fd940a129dbcf32b3a46570a15d07206c068cb201ff4abfd7" },
                    { "idList", cardIdList },
                };

                var remoteResponse = await PostAsync($"/1/cards", JsonConvert.SerializeObject(parameters));
                var content = await remoteResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<object>(content);

                if (response != null)
                {
                    result = response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }
            return result;
        }

        public async Task<object> AddAttachmentToCard(string cardId)
        {
            Object result = new object();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", "a9f2cc2bbe00cfc3e93460eb0de6e361" },
                    { "token", "97af0bfb10fec01fd940a129dbcf32b3a46570a15d07206c068cb201ff4abfd7" },
                };

                var remoteResponse = await PostAsync($"/1/cards/{cardId}/attachments", JsonConvert.SerializeObject(parameters));
                var content = await remoteResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<object>(content);

                if (response != null)
                {
                    result = response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }
            return result;
        }

    }
}
