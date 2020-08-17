using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi
{
    public class CategoryIndexResult
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public CategoryIndexResult(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
