using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnyMusic.Domain.Domain;
using AnyMusic.Repository;
using AnyMusic.Domain.Domain.ViewModels;

namespace AnyMusic.Web.Controllers
{
    public class TracksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TracksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tracks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tracks.Include(t => t.Album)
                                                      .Include(t => t.Artists) 
                                                      .ThenInclude(at => at.Artist); 
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tracks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.Album)
                 .Include(t => t.Artists) // Include artists for the track
                    .ThenInclude(at => at.Artist) // Include actual artist entity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // GET: Tracks/Create
        public IActionResult Create()
        {
            var viewModel = new TrackViewModel
            {
                Track = new Track(),
                Artists = _context.Artists.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.ArtistName
                })
            };

            //ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "AlbumName");
            return View(viewModel);
        }

        // POST: Tracks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrackViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Track.Id = Guid.NewGuid();

                // Add the track to the database
                _context.Add(viewModel.Track);

                // Add the selected artists
                foreach (var artistId in viewModel.SelectedArtistIds)
                {
                    var artistInTrack = new ArtistInTrack
                    {
                        Id = Guid.NewGuid(),
                        TrackId = viewModel.Track.Id,
                        ArtistId = artistId
                    };
                    _context.Add(artistInTrack);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate SelectList if ModelState is invalid
            viewModel.Artists = _context.Artists.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.ArtistName
            });

            //ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "AlbumName", viewModel.Track.AlbumId);

            return View(viewModel);
        }


        // GET: Tracks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound();
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "AlbumCoverImage", track.AlbumId);
            return View(track);
        }

        // POST: Tracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TrackName,AlbumId,Duration,Rating,Id")] Track track)
        {
            if (id != track.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(track);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(track.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "AlbumCoverImage", track.AlbumId);
            return View(track);
        }

        // GET: Tracks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var track = await _context.Tracks
                .Include(t => t.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (track == null)
            {
                return NotFound();
            }

            return View(track);
        }

        // POST: Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track != null)
            {
                _context.Tracks.Remove(track);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackExists(Guid id)
        {
            return _context.Tracks.Any(e => e.Id == id);
        }
    }
}
