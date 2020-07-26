using InfoTrackSEOApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace InfoTrackSEOApp.Provider
{
    public class SearchProvider : ISearchProvider
    {
        public List<ResultsModel> searchService(SearchModel searchModel)
        {
            string searchString = searchModel.SearchText;
            if(searchString.IndexOf(' ') > -1)
            {
                searchString = Regex.Replace(searchString, @"\s+", "+");
            }
            WebClient client = new WebClient();

            Stream data = client.OpenRead(searchHttpURL(searchModel.SearchInTop,searchModel.SearchEngine)+ HttpUtility.UrlEncode(searchString));

            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            HtmlAgilityPack.HtmlDocument doc23 = new HtmlAgilityPack.HtmlDocument();
            doc23.LoadHtml(s);
            HtmlNode[] toftitle = doc23.DocumentNode.Descendants().
                Where(
                x=>x.Name=="div"  
                && x.ChildNodes.Count>0 
                && x.ChildNodes["a"]!=null 
                && x.ChildNodes["a"].Attributes["href"]!=null
                && x.ChildNodes["a"].Attributes["href"].Value.Contains("www."))
                //&& x.Attributes["class"]!=null)// && x.Attributes["class"].Value.Contains("rc")).
            //    . Where(x =>
            //      (x.Name == "a") &&
            //      (x.Attributes["href"] != null)
            //      &&
            //      (x.Attributes["href"].Value.Contains("www."))
            //)
                .ToArray();
            //  toftitle.FindAll(x => x.Attributes["href"].Value.Contains("infotrack.com.au"));
            List<ResultsModel> results = new List<ResultsModel>();
            for (int i = 0; i < toftitle.Length; i++)
            {
                if (toftitle[i].ChildNodes["a"].Attributes["href"].Value.Contains(searchModel.SearchURL))
                {
                    results.Add(new ResultsModel() {Index=(i+1).ToString(),link= toftitle[i].ChildNodes["a"].Attributes["href"].Value });
                }
            }
            
            data.Close();
            reader.Close();



            return results;
        }

        string searchHttpURL(int SearchInTop, SearchEngineOptions SearchEngineSelect)
        {
            if (SearchEngineOptions.Bing == SearchEngineSelect)
            {
                return "https://www.bing.com/search?count=" + SearchInTop + "&q=";

            }
            else
            {
                return "https://www.google.com/search?num=" +SearchInTop+"&q=";
            }
        }
    }
}