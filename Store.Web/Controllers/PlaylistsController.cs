using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnyMusic.Domain.Domain;
using AnyMusic.Repository;
using AnyMusic.Service;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AnyMusic.Domain.Domain.ViewModels;
using AnyMusic.Service.Interface;

namespace AnyMusic.Web.Controllers
{

   
    public class PlaylistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IPlaylistService _playlistService;
        private readonly ITrackService _trackService;
        private readonly ITrackInPlaylistService _trackInPlaylistService;

        public PlaylistsController(ApplicationDbContext context, IPlaylistService playlistService, ITrackService trackService, ITrackInPlaylistService trackInPlaylistService)
        {
            _context = context;
            _playlistService = playlistService;
            _trackService = trackService;
            _trackInPlaylistService = trackInPlaylistService;
        }


        [Authorize]
        public async Task<IActionResult> AddToPlaylist(Guid trackId)
        {
            var track = _trackService.GetDetailsForTrack(trackId);
            if (track == null)
            {
                return NotFound();
            }
            // Get all playlists for the current user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            var playlists = _playlistService.GetAllUserPlaylists(userId)
                                                     
                                                  .Where(pl => !pl.TracksInPlaylists.Any(tp => tp.TrackId == trackId)) // Exclude playlists that already contain the track
                                                  .ToList();


            var model = new AddToPlaylistViewModel
            {
                TrackId = trackId,
                TrackName = track.TrackName,
                Playlists = playlists.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToPlaylist(AddToPlaylistViewModel model)
        {
            if (ModelState.IsValid)
            {
                var track = _trackService.GetDetailsForTrack(model.TrackId);
                var playlist = _playlistService.GetDetailsForUserPlaylist(model.PlaylistId);

                if (track != null && playlist != null)
                {
                    _trackService.AddTrackToUserPlaylist(playlist, track);
                    return RedirectToAction("Index", "Tracks");
                }

                ModelState.AddModelError("", "Invalid track or playlist.");
            }

            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.Playlists = _playlistService.GetAllUserPlaylists(userId)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToList();

            return View(model);
        }


        public async Task<IActionResult> RemoveTrackFromPlaylist(Guid playlistId, Guid trackId)
        {
            var trackInPlaylist = await _context.TrackInPlaylists
         .FirstOrDefaultAsync(tip => tip.PlaylistId == playlistId && tip.TrackId == trackId);

            if (trackInPlaylist == null)
            {
                return NotFound();
            }

            _context.TrackInPlaylists.Remove(trackInPlaylist);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = playlistId });
        }



        // GET: Playlists
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Filter playlists based on the current user's ID
            var userPlaylists = await _context.Playlists
                .Where(p => p.UserId == userId)
                .Include(p => p.TracksInPlaylists)
                .ToListAsync();

            return View(userPlaylists);
        }

        // GET: Playlists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playlist = await _context.Playlists
          .Include(p => p.TracksInPlaylists)
          .ThenInclude(tp => tp.Track) // Include the Track details
          .FirstOrDefaultAsync(p => p.Id == id);
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
