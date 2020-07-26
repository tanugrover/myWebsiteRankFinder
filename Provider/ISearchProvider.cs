using InfoTrackSEOApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrackSEOApp.Provider
{
    public interface ISearchProvider
    {
         List<ResultsModel> searchService(SearchModel searchModel);
    }
}
