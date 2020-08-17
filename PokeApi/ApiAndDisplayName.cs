
namespace PokeApi
{
    struct ApiAndDisplayName
    {
        public string ApiName { get; private set; }
        public string DisplayName { get; private set; }

        public ApiAndDisplayName(string apiName, string displayName)
        {
            ApiName = apiName;
            DisplayName = displayName;
        }
    }
}
