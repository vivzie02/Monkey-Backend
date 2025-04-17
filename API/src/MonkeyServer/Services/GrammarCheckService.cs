using log4net;
using Microsoft.Extensions.Configuration;
using MonkeyServer.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonkeyServer.Services
{
    /// <summary>
    /// GrammarCheckService
    /// </summary>
    public class GrammarCheckService : IGrammarCheckService
    {
        private readonly ILog log = LogManager.GetLogger(typeof(Program));
        private readonly HttpClient client;
        private readonly string GrammarCheckBaseUrl;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="configuration"></param>
        public GrammarCheckService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            client = httpClientFactory.CreateClient();
            GrammarCheckBaseUrl = configuration["GrammarCheckService:BaseUrl"];
        }

        /// <summary>
        /// GrammarIsOk
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public async Task<bool> CheckGrammar(string sentence)
        {
            log.Info(">>> Starting Grammar check");

            var values = new Dictionary<string, string>
            {
                { "sentence", sentence }
            };

            var content = new FormUrlEncodedContent(values);

            try
            {
                var response = await client.PostAsync(GrammarCheckBaseUrl, content);

                var responseObject = JsonConvert.DeserializeObject<GrammarOutputDTO>(await response.Content.ReadAsStringAsync());

                //clean string of tabs, new lines and carriage returns
                responseString = Regex.Replace(responseString, @"\t|\n|\r", "");

                log.Info("<<< Finished Grammar check");
                return responseObject.IsCorrect;
            }
            catch (Exception ex)
            {
                log.Error("Error while checking grammar", ex);
                return false;
            }
        }
    }
}
