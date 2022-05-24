using CreateMovieApi.Data;
using CreateMovieApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreateMovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly SearchMovieApiDbContext _context;

        public MovieController(SearchMovieApiDbContext context)
        {
            _context = context;
        }

        // RQ1 - Get a list of all movies
        // GET: api/Movie
        [HttpGet("GetMovieList")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovieList()
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }
            return await _context.Movie.ToListAsync();
        }

        // RQ2 - Get a list of all movies in a specific category
        // GET: api/Movie
        // Learn more about - When do I use path params vs. query params in a RESTful API?
        [HttpGet("GetMovieListByCategory/{category}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovieListByCategory(string category)
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }

            return await _context.Movie.Where(m => m.Category.Contains(category)).ToListAsync();
        }

        //RQ3 - Get a random movie pick
        [HttpGet("GetRandomMovie")]
        public async Task<ActionResult<Movie>> GetRandomMovie()
        {
            Random rand = new Random();
            var movieCt = _context.Movie.Count();
            int toSkip = rand.Next(0, _context.Movie.Count());

            if (_context.Movie == null)
            {
                return NotFound();
            }

            return _context.Movie.Skip(toSkip).Take(1).First();
        }

        //RQ4 - Get a random movie pick from a specific category
        [HttpGet("GetRandomMovieByCategory/{category}")]
        public async Task<ActionResult<Movie>> GetRandomMovieByCategory(string category)
        {
            Random rand = new Random();
            int toSkip = rand.Next(1, _context.Movie.Count());

           var randMovie = _context.Movie.Skip(toSkip).Where(r => r.Category.Contains(category)).Take(1).SingleOrDefault();

            return randMovie;
        }

        //RQ5- Get a random movie pick from a specific category
        [HttpGet("GetMultipleRandomMovies/{quantity}")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMultipleRandomMovies(int qty)
        {
            Random rand = new Random();
            int toSkip = rand.Next(1, _context.Movie.Count());

            List<Movie> movieList = _context.Movie.Skip(toSkip).Take(qty).ToList();

            return movieList;
        }

        //RQ5- Get a random movie pick from a specific category
        // test - hmmm ... doesn't always return 5???
        [HttpGet("GetMultipleRandomMoviesByCatagory/{quantity}/{category}")]
        public async Task<ActionResult<List<Movie>>> GetMultipleRandMoviesByCatagory(int qty, string category)
        {
            Random rand = new Random();
            int toSkip = rand.Next(1, _context.Movie.Count());

            List<Movie> randMovieList = _context.Movie.Skip(toSkip).Where(r => r.Category.Contains(category)).Take(qty).ToList();

            return randMovieList;
        }

        // GET: api/Movie/5
        // Not a RQ
        [HttpGet("GetMovieById/{id}")]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            if (_context.Movie == null)
            {
                return NotFound();
            }
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movie/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMovie(int id, Movie movie)
        //{
        //    if (id != movie.MovieId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(movie).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MovieExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Movie
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        //{
        //  if (_context.Movie == null)
        //  {
        //      return Problem("Entity set 'SearchMovieApiDbContext.Movie'  is null.");
        //  }
        //    _context.Movie.Add(movie);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        //}

        // DELETE: api/Movie/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMovie(int id)
        //{
        //    if (_context.Movie == null)
        //    {
        //        return NotFound();
        //    }
        //    var movie = await _context.Movie.FindAsync(id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Movie.Remove(movie);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MovieExists(int id)
        //{
        //    return (_context.Movie?.Any(e => e.MovieId == id)).GetValueOrDefault();
        //}
    }
}