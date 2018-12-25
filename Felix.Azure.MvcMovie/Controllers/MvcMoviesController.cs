using Felix.Azure.MvcMovie.Repositories;
using Felix.Azure.MvcMovie.Entity;
using Felix.Azure.MvcMovie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Felix.Azure.MvcMovie.DataAccess;

namespace Felix.Azure.MvcMovie.Controllers
{
    public class MvcMoviesController : Controller
    {
        private readonly IMovieDataAccess _dataAccess;

        public MvcMoviesController(IMovieDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // GET: MvcMovies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.Genres = new SelectList(await _dataAccess.GetGenreAsync());
            movieGenreVM.Movies = await _dataAccess.GetMovies(movieGenre, searchString);
            movieGenreVM.SearchString = searchString;

            return View(movieGenreVM);
        }

        // GET: MvcMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var movie = await _dataAccess.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: MvcMovies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MvcMovies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _dataAccess.SaveAsync(movie);
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // GET: MvcMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var movie = await _dataAccess.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: MvcMovies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _dataAccess.SaveAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: MvcMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var movie = await _dataAccess.GetMovieByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: MvcMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _dataAccess.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
