using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Models.Dtos;
using MoviesAPI.Repository;
using MoviesAPI.Repository.IRepository;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public MoviesController(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovies()
        {
            var listMovies = _movieRepository.GetMovies();
            var listMoviesDto = new List<MovieDto>();

            foreach (var movie in listMovies)
            {
                listMoviesDto.Add(_mapper.Map<MovieDto>(movie));
            }

            return Ok(listMoviesDto);
        }

        [HttpGet("{movieId:int}", Name = "GetMovie")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMovie(int movieId)
        {
            var itemMovie = _movieRepository.GetMovie(movieId);

            if (itemMovie == null)
            {
                return NotFound();
            }

            var itemMovieDto = _mapper.Map<MovieDto>(itemMovie);

            return Ok(itemMovieDto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateMovie([FromBody] MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (movieDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_movieRepository.IsMovieExists(movieDto.Name))
            {
                ModelState.AddModelError("", "Movie already exists.");
                return StatusCode(404, ModelState);
            }

            var movie = _mapper.Map<Movie>(movieDto);

            if (!_movieRepository.CreateMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong while saving {movie.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetMovie", new { movieId = movie.Id }, movie);
        }

        [HttpPatch("{movieId:int}", Name = "UpdatePatchMovie")]
        [ProducesResponseType(204, Type = typeof(MovieDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePatchMovie(int movieId, [FromBody] MovieDto movieDto)
        {
            if (!ModelState.IsValid || movieDto == null || _movieRepository.IsMovieExists(movieDto.Name))
            {
                return BadRequest(ModelState);
            }

            if (!_movieRepository.IsMovieExists(movieId))
            {
                NotFound();
            }

            var movie = _mapper.Map<Movie>(movieDto);

            if (!_movieRepository.UpdateMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong while updating {movie.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{movieId:int}", Name = "DeleteMovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteMovie(int movieId)
        {
            if (!_movieRepository.IsMovieExists(movieId))
            {
                return NotFound();
            }

            var movie = _movieRepository.GetMovie(movieId);

            if (!_movieRepository.DeleteMovie(movie))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting {movie.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("GetMoviesByCategory/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMoviesByCategory(int categoryId)
        {
            var listMovies = _movieRepository.GetMoviesByCategory(categoryId);

            if (listMovies == null)
            {
                return NotFound();
            }
            var listMoviesDto = new List<MovieDto>();

            foreach (var movie in listMovies)
            {
                listMoviesDto.Add(_mapper.Map<MovieDto>(movie));
            }

            return Ok(listMoviesDto);
        }


        [HttpGet("SearchMovie")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SearchMovie(string name)
        {
            try
            {
                var listMovies = _movieRepository.SearchMovieByName(name.Trim());

                if (listMovies.Any())
                {
                    return Ok(listMovies);
                }

                return NotFound();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something went wrong while searching {name.Trim()}");
            }
        }
    }
}
