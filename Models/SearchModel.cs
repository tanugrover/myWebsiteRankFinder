using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InfoTrackSEOApp.Models
{
    public class SearchModel
    {
        [Display(Name = "Search Keywords")]
        [Required]
        public string SearchText { get; set; }

        [Required]
        [Display(Name = "Find rank for URL:")]
        public string SearchURL { get; set; }

        [Required]
        [Display(Name = "Search Engine:")]
        public SearchEngineOptions SearchEngine { get; set; }

        [Required]
        [DefaultValue(100)]
        [DisplayName("Search In Top")]
        public int SearchInTop { get; set; } = 100;
                     
    }

    public enum SearchEngineOptions
    {
       
        Google,
        Bing
    }
}