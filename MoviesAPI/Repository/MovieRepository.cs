using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Repository.IRepository;
using System.Data.Common;
using System.Text.Json.Nodes;

namespace MoviesAPI.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _bd;

        public MovieRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }
        public bool CreateMovie(Movie movie)
        {
            movie.InitialDate = DateTime.Now;
            _bd.Movies.Add(movie);
            return Save();
        }

        public bool DeleteMovie(Movie movie)
        {
            _bd.Movies.Remove(movie);
            return Save();
        }

        public ICollection<Movie> GetMovies()
        {
            return _bd.Movies.OrderBy(c => c.Name).ToList();
        }

        public Movie GetMovie(int id)
        {
            return _bd.Movies.FirstOrDefault(c => c.Id == id);
        }

        public bool IsMovieExists(string name)
        {
            bool result = _bd.Movies.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
            return result;
        }

        public bool IsMovieExists(int id)
        {
            return _bd.Movies.Any(c => c.Id == id);
        }

        public bool Save()
        {
            try
            {
                _bd.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public bool UpdateMovie(Movie Movie)
        {
            Movie.InitialDate = DateTime.Now;
            _bd.Movies.Update(Movie);
            return Save();
        }

        public ICollection<Movie> GetMoviesByCategory(int id)
        {
            return _bd.Movies.Include(cat => cat.Category).Where(cat => cat.CategoryId == id).ToList();
        }

        public ICollection<Movie> SearchMovieByName(string name)
        {
            IQueryable<Movie> query = _bd.Movies;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(mov => mov.Name.Contains(name) || mov.Description.Contains(name));
            }

            return query.ToList();
        }
    }
}
