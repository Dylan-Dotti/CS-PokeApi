using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokeApi
{
    class SpriteLoader
    {
        private static readonly HttpClient client;

        static SpriteLoader()
        {
            client = new HttpClient();
        }

        public async Task<Image> LoadSprite(string url)
        {
            return Image.FromStream(await client.GetStreamAsync(url));
        }
    }
}
