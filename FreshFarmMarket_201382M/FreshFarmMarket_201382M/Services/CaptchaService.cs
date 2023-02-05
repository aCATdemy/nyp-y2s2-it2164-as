using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace FreshFarmMarket_201382M.Services
{
    public class CaptchaService
    {
        private readonly IOptionsMonitor<CaptchaConfig> _config;

        public CaptchaService(IOptionsMonitor<CaptchaConfig> config)
        {
            _config = config;
        }

        public async Task<bool> VerifyToken(string token)
        {
            try
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.CurrentValue.SecretKey}&response={token}";

                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    if (httpResult.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var responseString = await httpResult.Content.ReadAsStringAsync();

                    var googleResult = JsonConvert.DeserializeObject<CaptchaResponse>(responseString);

                    return googleResult.success && googleResult.score >= 0.5;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
