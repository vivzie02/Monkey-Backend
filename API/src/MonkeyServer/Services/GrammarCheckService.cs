using log4net;
using log4net.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
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

                var responseString = await response.Content.ReadAsStringAsync();

                log.Info("<<< Finished Grammar check");
                return string.Equals(responseString, "true");
            }
            catch(Exception ex)
            {
                log.Error("Error while checking grammar", ex);
                return false;
            }       
        }
    }
}
