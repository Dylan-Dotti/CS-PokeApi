using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi
{
    class PokeApiCategoryIndex
    {
        public List<CategoryIndexResult> Results { get; private set; }

        public PokeApiCategoryIndex(List<CategoryIndexResult> results)
        {
            Results = results;
        }
    }
}
