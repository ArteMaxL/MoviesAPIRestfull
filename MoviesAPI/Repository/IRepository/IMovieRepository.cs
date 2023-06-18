using MoviesAPI.Models;

namespace MoviesAPI.Repository.IRepository
{
    public interface IMovieRepository
    {
        ICollection<Movie> GetMovies();
        Movie GetMovie(int id);
        bool IsMovieExists(string name);
        bool IsMovieExists(int id);
        bool CreateMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        bool DeleteMovie(Movie movie);

        // Methods to search for movies by categories and search for movies by name
        ICollection<Movie> GetMoviesByCategory(int id);
        ICollection<Movie> SearchMovieByName(string name);
        bool Save();
    }
}
