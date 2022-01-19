using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Woonnet.Models;

namespace Woonnet
{
    public class HouseFinder
    {
        private readonly HttpClient _httpClient;
        private readonly string _requestBody;
        public HouseFinder(HttpClient httpClient)
        {
            _httpClient = httpClient;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Woonnet.Request.json"))
            {
                _requestBody = new StreamReader(stream).ReadToEnd(); ;
            }
        }

        public async Task<IEnumerable<Advertentie>> GetMatches()
        {
            var response = await _httpClient.PostAsync("wsWoonnetRijnmond/Woonwensen/wsWoonwensen.asmx/GetWoonwensResultatenVoorPagina",
                new StringContent(_requestBody, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var doc = JsonObject.Parse(responseContent);
            var resultaten = doc["d"]["resultaten"].AsArray();
            var res = JsonSerializer.Deserialize<IEnumerable<Advertentie>>(resultaten);
            return res;
        }

        public async Task<Aanbod> GetDetails(long frontendAdvertentieId)
        {
            var request = new DetailRequest
            {
                Id = frontendAdvertentieId.ToString(),
                //VolgendeId = null, // -1
            };
            var response = await _httpClient.PostAsync("wsWoonnetRijnmond/WoningenModule/Service.asmx/getAanbodEnVolgendeViaId",
                new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var doc = JsonObject.Parse(responseContent);
            var resultaat = doc["d"]["Aanbod"];
            var res = JsonSerializer.Deserialize<Aanbod>(resultaat);
            return res;
        }

    }
}
