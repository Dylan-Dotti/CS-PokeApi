using Newtonsoft.Json.Linq;
using PokeApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace PokeApi
{
    public class PokemonFactory
    {
        private readonly PokemonSpeciesService speciesService;
        private readonly PokemonService pokemonService;

        private readonly SpriteLoader spriteLoader;
        private readonly PokeApiNameConverter converter;

        public PokemonFactory()
        {
            speciesService = new PokemonSpeciesService();
            pokemonService = new PokemonService();
            spriteLoader = new SpriteLoader();
            converter = new PokeApiNameConverter();
        }

        public async Task<Pokemon> CreatePokemon(string nameOrId)
        {
            JObject speciesData = await speciesService.GetById(nameOrId);
            JObject pokemonData = await pokemonService.GetById(nameOrId);

            string apiName = pokemonData.Value<string>("name");
            string displayName = converter.GetDisplayName(apiName);

            int pkmId = pokemonData.Value<int>("id");
            string genus = GetGenus(speciesData);
            (string t1, string t2) = GetTypes(pokemonData);
            float height = GetHeightMeters(pokemonData);
            float weight = GetWeightKilograms(pokemonData);

            var pokedexEntries = GetPokedexEntries(speciesData);
            var baseStats = GetBaseStats(pokemonData);

            JObject spriteUrls = pokemonData.Value<JObject>("sprites");
            string frontSpriteUrl = spriteUrls.Value<string>("front_default");
            Image frontSprite = await spriteLoader.LoadSprite(frontSpriteUrl);
            
            return new Pokemon(apiName, displayName, pkmId, genus, t1, t2,
                height, weight, frontSprite, pokedexEntries, baseStats);
        }

        private string GetGenus(JObject speciesData)
        {
            JArray genera = speciesData.Value<JArray>("genera");
            JToken generaObject = genera.Where(g =>
            {
                JObject language = g.Value<JObject>("language");
                string lname = language.Value<string>("name");
                return lname == "en";
            }).First();
            return generaObject.Value<string>("genus");
        }

        private (string, string) GetTypes(JObject pkmData)
        {
            JArray types = pkmData.Value<JArray>("types");
            string[] typeNames = types.Select(
                t => t.Value<JObject>("type").Value<string>("name")).ToArray();
            string type1 = typeNames[0];
            string type2 = typeNames.Length == 2 ? typeNames[1] : null;
            return (type1, type2);
        }

        private float GetHeightMeters(JObject pkmData)
        {
            float heightDecimeters = pkmData.Value<float>("height");
            return (float)Math.Round(heightDecimeters / 10f, 2);
        }

        private float GetWeightKilograms(JObject pkmdata)
        {
            float weightHectograms = pkmdata.Value<float>("weight");
            return (float)Math.Round(weightHectograms / 10f, 2);
        }

        private Dictionary<string, string> GetPokedexEntries(JObject speciesData)
        {
            Dictionary<string, string> versionDescriptions = new Dictionary<string, string>();
            JArray flavorEntries = speciesData.Value<JArray>("flavor_text_entries");
            foreach (JObject entry in flavorEntries)
            {
                string language = GetLanguageName(entry);
                if (language == "en")
                {
                    string versionName = GetVersionName(entry);
                    string text = entry.Value<string>("flavor_text");
                    text = new PokedexEntryFormatter().FormatEntry(text);
                    versionDescriptions.Add(versionName, text);
                }
            }
            return versionDescriptions;
        }

        private Dictionary<string, int> GetBaseStats(JObject pokemonData)
        {
            Dictionary<string, int> baseStats = new Dictionary<string, int>();
            JArray stats = pokemonData.Value<JArray>("stats");
            foreach (JObject stat in stats)
            {
                string statName = stat.Value<JObject>("stat").Value<string>("name");
                int statValue = stat.Value<int>("base_stat");
                baseStats.Add(statName, statValue);
            }
            return baseStats;
        }

        private string GetVersionName(JObject obj)
        {
            return obj.Value<JObject>("version").Value<string>("name");
        }

        private string GetLanguageName(JObject obj)
        {
            return obj.Value<JObject>("language").Value<string>("name");
        }
    }
}
