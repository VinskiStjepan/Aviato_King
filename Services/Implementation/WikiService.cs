using Database.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Configuration;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class WikiService : IWikiService
    {
        private readonly IConfiguration _configuration;
        private readonly IIataRepository _iata;
        private readonly IGetResponse _response;
        private readonly WikiConfiguration _wikiConf; 
        //private readonly IEnumerable<string> _wikiPages;
        //private readonly string _wikiUrl1;
        //private readonly string _wikiUrl2;

        public WikiService(IConfiguration configuration, IIataRepository iata, IGetResponse response,
                            IOptions<WikiConfiguration> wikiConf)
        {
            _configuration = configuration;
            _iata = iata;
            _response = response;
            _wikiConf = wikiConf.Value;

            //_wikiPages = _configuration.GetSection("Wiki:Pages").GetChildren()
            //            .ToArray().Select(v => v.Value);

            //_wikiUrl1 = _configuration.GetValue<string>("Wiki:Url1");
            //_wikiUrl2 = _configuration.GetValue<string>("Wiki:Url2");
        }

        public async Task<List<Iata>> Import()
        {
            int TableTagCount = 6, ReturnInt = 1;
            string ResponseString;
            List<Iata> IataList = new List<Iata>();

            using (HttpClient client = new HttpClient())
            {
                foreach (string page in _wikiConf.Pages)
                {
                    if (page == "O")
                    {
                        TableTagCount = 4;
                    }

                    ResponseString = await _response.GetHttpResponse(_wikiConf.Url1 + page + _wikiConf.Url2);
                    InsertResponse(TableTagCount, ResponseString, IataList);
                }
            }
            return IataList;
        }

        private void InsertResponse(int TableTagCount, string ResponseString, List<Iata> iatas)
        {
            int iataCodeIndex = 0, iataTitleStartIndex, iataTitleEndIndex, nameIndex;
            string IataCode, iataName;

            while (iataCodeIndex >= 0)
            {
                iataCodeIndex = ResponseString.IndexOf("<td>");
                if (iataCodeIndex < 0) { continue; }
                IataCode = ResponseString.Substring((iataCodeIndex + 4), 3);

                iataTitleStartIndex = ResponseString.IndexOf("title=\\\"", (iataCodeIndex + 4)) + 8;
                iataTitleEndIndex = ResponseString.IndexOf("\">", iataTitleStartIndex) - 1;
                iataName = ResponseString.Substring(iataTitleStartIndex,
                    (iataTitleEndIndex - iataTitleStartIndex));
                nameIndex = iataName.IndexOf("(page does not exist)");

                if (nameIndex >= 0)
                {
                    iataName = iataName.Substring(0, (nameIndex - 1));
                }

                for (int n = 0; n < TableTagCount; n++)
                {
                    ResponseString = ResponseString.Substring(ResponseString.IndexOf("<td>") + 1);
                }

                if (IataCode == "<s>") { continue; };

                iatas.Add(new Iata { Code = IataCode, Name = iataName });
            }
        }
    }
}
