using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PokeApi.Tests
{
    public class PokemonFactoryTests
    {
        private readonly PokemonFactory factory;

        public PokemonFactoryTests()
        {
            factory = new PokemonFactory();
        }

        [Theory]
        [InlineData("pikachu")][InlineData("mr-mime")]
        [InlineData("nidoran-m")][InlineData("nidoran-f")]
        public async void TestApiName(string apiName)
        {
            Pokemon pokemonResult = await factory.CreatePokemon(apiName);
            string actualName = pokemonResult.ApiName;
            Assert.Equal(apiName, actualName);
        }

        [Theory]
        [InlineData("pikachu", "Pikachu")][InlineData("mr-mime", "Mr. Mime")]
        [InlineData("nidoran-m", "Nidoran♂")][InlineData("nidoran-f", "Nidoran♀")]
        public async void TestDisplayName(string apiName, string displayName)
        {
            Pokemon pokemonResult = await factory.CreatePokemon(apiName);
            string actualName = pokemonResult.DisplayName;
            Assert.Equal(displayName, actualName);
        }
    }
}
