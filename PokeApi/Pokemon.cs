using System.Collections.Generic;
using System.Drawing;

namespace PokeApi
{
    public class Pokemon
    {
        public string ApiName { get; private set; }
        public string DisplayName { get; private set; }

        public int Id { get; private set; }
        public string Genus { get; private set; }
        public string Type1 { get; private set; }
        public string Type2 { get; private set; }
        public float Height { get; private set; }
        public float Weight { get; private set; }
        public Image FrontSprite { get; private set; }
        public IReadOnlyDictionary<string, string> PokedexEntries { get; private set; }
        public IReadOnlyDictionary<string, int> BaseStats { get; private set; }

        public Pokemon(string apiName, string displayName, int id, string genus,
            string type1, string type2, float height, float weight,
            Image frontSprite, IReadOnlyDictionary<string, string> pokedexEntries,
            IReadOnlyDictionary<string, int> baseStats)
        {
            ApiName = apiName;
            DisplayName = displayName;
            Id = id;
            Genus = genus;
            Type1 = type1;
            Type2 = type2;
            Height = height;
            Weight = weight;
            FrontSprite = frontSprite;
            PokedexEntries = pokedexEntries;
            BaseStats = baseStats;
        }

        public bool HasSecondType() => Type2 != null;

        public string GetPokedexEntry(string version) => PokedexEntries[version];
    }
}
