using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemon.Where(pokemon => pokemon.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemon.Where(pokemon =>pokemon.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int id)
        {
            var review = _context.Reviews.Where(pokemon => pokemon.Id != id);

            if(review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(review => review.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemon.OrderBy(pokemon => pokemon.Id).ToList();
        }

        public bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(pokemon => pokemon.Id == id);
        }
    }
}
