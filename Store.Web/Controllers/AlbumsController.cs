﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnyMusic.Domain.Domain;
using AnyMusic.Repository;
using AnyMusic.Service.Interface;
using AnyMusic.Domain.Domain.ViewModels;

namespace AnyMusic.Web.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAlbumService albumService;

        public AlbumsController(ApplicationDbContext context,IAlbumService albumService)
        {
            _context = context;
            this.albumService = albumService;
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            return View(await _context.Albums.ToListAsync());
        }

       
        public async Task<IActionResult> AddTrackToAlbum(Guid albumId)
        {
            // Fetch all tracks that have no associated album
            var tracksWithoutAlbum = await _context.Tracks
                                         .Where(t => t.AlbumId == null)
                                       .ToListAsync();


            var model = new AddToAlbum
            {
                AlbumId = albumId,
                Tracks = tracksWithoutAlbum.Select(track => new SelectListItem
                {
                    Value = track.Id.ToString(),
                    Text = $"{track.TrackName} (Duration: {track.Duration}, Rating: {track.Rating})"
                }).ToList()
            };

            return View(model);
        }

       
        [HttpPost]
        public async Task<IActionResult> AddTrackToAlbum(Guid albumId, Guid trackId)
        {
            var track = await _context.Tracks.FindAsync(trackId);
            var album = await _context.Albums.FindAsync(albumId);

            if (track == null || album == null)
            {
                return RedirectToAction("Index");
            }

            //  track with the album
            track.AlbumId = albumId;
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = albumId });
        }

        public async Task<IActionResult> RemoveTrackFromAlbum(Guid albumId, Guid trackId)
        {
            var track = await _context.Tracks.FindAsync(trackId);

            if (track == null || track.AlbumId != albumId)
            {
                return NotFound();
            }
            track.AlbumId = null;

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = albumId });
        }


        // GET: Albums/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
          
            return View(albumService.GetDetailsForAlbum(id));
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlbumName,AlbumDescription,AlbumCoverImage,Genre,Id")] Album album)
        {
            if (ModelState.IsValid)
            {
                album.Id = Guid.NewGuid();
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AlbumName,AlbumDescription,AlbumCoverImage,Genre,Id")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
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
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var album = albumService.GetDetailsForAlbum(id);
            if (album != null)
            {
                var idk = album.Tracks;
                for(var i = 0;i< idk.Count;i++)
                {
                    idk.ElementAt(i).AlbumId = null;
                }
                _context.Albums.Remove(album);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(Guid id)
        {
            return _context.Albums.Any(e => e.Id == id);
        }
    }
}
