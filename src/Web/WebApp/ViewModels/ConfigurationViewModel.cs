using System;
using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class ConfigurationViewModel
    {
        public ConfigurationViewModel()
        {
            this.Configuration = new List<KeyValuePair<string, string>>();
        }

        public ICollection<KeyValuePair<string,string>> Configuration { get; set; }
    }
}
