using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Coworking.Application.Options;
using Coworking.Application.Services.ApiModels;
using Coworking.Domain.Exceptions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Coworking.Application.Services
{
    public class TeamupService
    {
        private readonly HttpClient _httpClient;
        private readonly TeamupOptions _options;

        public TeamupService(IHttpClientFactory httpClient, IOptions<TeamupOptions> options)
        {
            _httpClient = httpClient.CreateClient("teamup");
            _options = options.Value;
        }

        public async Task<HttpResponseMessage> AddRoomToCalendarAsync(string roomName)
        {
            //(1) Check if more than 8 
            var result = await _httpClient.GetAsync("subcalendars");

            if (!result.IsSuccessStatusCode)
                throw new BadRequestException("AddRoomToCalendar(string,string)"
                    , HttpMethod.Post, "status-code-was-not-200");

            var model = await ParseResponseMessage<SubCalendarCollection>(result);

            if (model.SubCalendars.Count > 8)
                throw new BadRequestException("AddRoomToCalendar(string,string)"
                    , HttpMethod.Post, "there are more than 8 subcalendars");


            var content = new CreateSubCalendarModel
            {
                active = true,
                color = 1,
                name = roomName
            };

            var serializeObject = JsonConvert.SerializeObject(content);

            var createdResult = await _httpClient.PostAsync("subcalendars",
                new StringContent(serializeObject, Encoding.UTF8, "application/json"));

            return createdResult;
        }


        public async Task<HttpResponseMessage> AddMeetingOnSubCalendarAsync(MeetingModel meetingModel)
        {

            var serializedContent = JsonConvert.SerializeObject(meetingModel,new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            });
            
            var result = await _httpClient.PostAsync("events",
                new StringContent(serializedContent, Encoding.UTF8, "application/json"));

            return result;
        }
        
        public async Task<int> GetSubcalendarIdAsync(string roomName)
        {
            var result = await _httpClient.GetAsync("subcalendars");

            if (!result.IsSuccessStatusCode)
                throw new BadRequestException("AddRoomToCalendar(string,string)"
                    , HttpMethod.Post, "status-code-was-not-200");

            var parseResult = await ParseResponseMessage<SubCalendarCollection>(result);

            return parseResult.SubCalendars.FirstOrDefault(x => x.Name == roomName).Id;
        }


        private static async Task<TResponse> ParseResponseMessage<TResponse>(HttpResponseMessage responseMessage)
            where TResponse : class
        {
            var response = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(response);
        }
    }
}