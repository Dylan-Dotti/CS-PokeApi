
namespace PokeApi
{
    class PokedexEntryFormatter
    {
        public string FormatEntry(string entry)
        {
            entry = entry.Replace("\n", " ");
            entry = entry.Replace("POKéMON", "Pokémon");
            entry = entry.Replace("\f", " ");
            return entry;
        }
    }
}
