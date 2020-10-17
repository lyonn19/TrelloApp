using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TrelloApp.Models;

namespace TrelloApp.Services.API
{
    public class TrelloService : RestService, ITrelloService
    {

        const string ENDPOINT_BASE = "https://api.trello.com";

        public TrelloService(): base(ENDPOINT_BASE) { }

        public async Task<IEnumerable<Board>> GetBoardList(string boardId)
        {
            IEnumerable<Board> result = new List<Board>();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", AppSettings.key },
                    { "token", AppSettings.token }
                };

                var remoteResponse = await GetAsync($"/1/boards/{boardId}/lists", parameters);
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
                    { "key", AppSettings.key },
                    { "token", AppSettings.token }
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
                    { "key", AppSettings.key },
                    { "token", AppSettings.token }
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

        public async Task<IEnumerable<Attachment>> GetAttachmentsOnCard(string cardId)
        {
            IEnumerable<Attachment> result = new List<Attachment>();
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", AppSettings.key },
                    { "token", AppSettings.token }
                };

                var remoteResponse = await GetAsync($"/1/cards/{cardId}/attachments", parameters);
                var content = await remoteResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<IEnumerable<Attachment>>(content);

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



        public async Task<bool> PostNewBoardMember(string boardId, string email)
        {
            bool result = false;
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", AppSettings.key },
                    { "token", AppSettings.token },
                    { "email", email },
                };

                var remoteResponse = await PutAsync($"/1/boards/{boardId}/members", parameters);
                var content = await remoteResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<BoardMember>(content);

                if (response != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }
            return result;
        }

        public async Task<bool> CreateCard(string cardIdList, Card card)
        {
            bool result = false;
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", AppSettings.key },
                    { "token", AppSettings.token },
                    { "name", card.Name },
                    { "desc", card.Desc },
                    { "idList", cardIdList },
                };

                var remoteResponse = await PostAsync($"/1/cards", JsonConvert.SerializeObject(parameters));
                var content = await remoteResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Card>(content);

                if (response != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }
            return result;
        }

        public async Task<bool> AddAttachmentToCard(string cardId, byte[] file)
        {
            bool result = false;
            try
            {
                var parameters = new Dictionary<string, string>
                {
                    { "key", AppSettings.key },
                    { "token", AppSettings.token }
                };

                var uri = new Uri(BuidlQueryString($"{ENDPOINT_BASE}/1/cards/{cardId}/attachments", parameters));

                var client = new RestClient(uri)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddFile("file", file ,"fileAttached");
                IRestResponse response = await client.ExecuteAsync(request);

                if (response != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }
            return result;
        }

        private string BuidlQueryString(string endPoint, Dictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                return string.Format("{0}?{1}", endPoint, string.Join("&", parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));
            }
            else
            {
                return endPoint;
            }
        }

    }
}
