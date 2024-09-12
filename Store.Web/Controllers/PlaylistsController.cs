using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnyMusic.Domain.Domain;
using AnyMusic.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AnyMusic.Domain.Domain.ViewModels;

namespace AnyMusic.Web.Controllers
{

   
    public class PlaylistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaylistsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        public async Task<IActionResult> AddToPlaylist(Guid trackId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
            var playlists = await _context.Playlists
                                          .Where(p => p.UserId == userId)
                                          .ToListAsync();

            ViewBag.PlaylistId = new SelectList(playlists, "Id", "Name");
            var track = await _context.Tracks.FindAsync(trackId);

            if (track == null)
            {
                return NotFound();
            }

            var model = new AddToPlaylistViewModel
            {
                TrackId = trackId,
                TrackName = track.TrackName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToPlaylist(AddToPlaylistViewModel model)
        {
            if (ModelState.IsValid)
            {
                var trackInPlaylist = new TrackInPlaylist
                {
                    Id = Guid.NewGuid(),
                    PlaylistId = model.PlaylistId,
                    TrackId = model.TrackId
                };

                _context.TrackInPlaylists.Add(trackInPlaylist);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Tracks");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var playlists = await _context.Playlists.Where(p => p.UserId == userId).ToListAsync();
            ViewBag.PlaylistId = new SelectList(playlists, "Id", "Name", model.PlaylistId);

            return View(model);
        }



        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Playlists.ToListAsync());
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // GET: Playlists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                playlist.Id = Guid.NewGuid();
                playlist.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(playlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] Playlist playlist)
        {
            if (id != playlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playlist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaylistExists(playlist.Id))
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
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaylistExists(Guid id)
        {
            return _context.Playlists.Any(e => e.Id == id);
        }
    }
}
