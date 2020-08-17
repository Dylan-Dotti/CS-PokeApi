using StringExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi
{
    public class PokeApiNameConverter
    {
        private readonly Dictionary<string, string> apiToDisplay;
        private readonly Dictionary<string, string> displayToApi;

        public PokeApiNameConverter()
        {
            apiToDisplay = new Dictionary<string, string>();
            apiToDisplay["nidoran-f"] = "Nidoran♀";
            apiToDisplay["nidoran-m"] = "Nidoran♂";
            apiToDisplay["mr-mime"] = "Mr. Mime";
            displayToApi = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in apiToDisplay)
            {
                displayToApi[item.Value] = item.Key;
            }
        }

        public string GetApiName(string displayName)
        {
            return displayToApi.ContainsKey(displayName) ?
                displayToApi[displayName] : displayName.ToLower();
        }

        public string GetDisplayName(string apiName)
        {
            return apiToDisplay.ContainsKey(apiName) ?
                apiToDisplay[apiName] : apiName.Capitalize();
        }
    }
}
