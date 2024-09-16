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
using AnyMusic.Domain.DTO;
using AnyMusic.Repository.Implementation;
using AnyMusic.Repository.Interface;
using ClosedXML.Excel;

namespace AnyMusic.Web.Controllers
{
    public class TracksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ITrackRepository trackRepository;

        public TracksController(ApplicationDbContext context, ITrackRepository trackRepository)
        {
            _context = context;
            this.trackRepository = trackRepository;
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


        [HttpGet]
        public FileContentResult ExportAllTracks()
        {
            string fileName = "Tracks.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

           
            var tracks = trackRepository.GetAll().ToList();

         
            var trackDtos = tracks.Select(t => new TrackDTO
            {
                TrackName = t.TrackName,
                Album = t.Album != null ? new AlbumDTO { AlbumName = t.Album.AlbumName } : null,
                Duration = t.Duration,
                Rating = t.Rating,
                Artists = t.Artists?.Select(a => new ArtistDTO { ArtistName = a.Artist.ArtistName }).ToList(),
                Playlists = t.TracksInPlaylists?.Select(tp => new PlaylistDTO
                {
                    Name = tp.Playlist?.Name,
                    UserEmail = tp.Playlist?.User?.Email
                }).ToList()
            }).ToList();

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Tracks");

              
                worksheet.Cell(1, 1).Value = "Track Name";
                worksheet.Cell(1, 2).Value = "Album Name";
                worksheet.Cell(1, 3).Value = "Duration";
                worksheet.Cell(1, 4).Value = "Rating";
                worksheet.Cell(1, 5).Value = "Artist Name";
                worksheet.Cell(1, 6).Value = "Playlist Name";
                worksheet.Cell(1, 7).Value = "User Email";

             
                for (int i = 0; i < trackDtos.Count; i++)
                {
                    var track = trackDtos[i];

                    worksheet.Cell(i + 2, 1).Value = track.TrackName;
                    worksheet.Cell(i + 2, 2).Value = track.Album?.AlbumName ?? "N/A";
                    worksheet.Cell(i + 2, 3).Value = track.Duration;
                    worksheet.Cell(i + 2, 4).Value = track.Rating;

                  
                    worksheet.Cell(i + 2, 5).Value = string.Join(", ", track.Artists?.Select(a => a.ArtistName) ?? new List<string> { "N/A" });
                    worksheet.Cell(i + 2, 6).Value = string.Join(", ", track.Playlists?.Select(p => p.Name) ?? new List<string> { "N/A" });
                    worksheet.Cell(i + 2, 7).Value = string.Join(", ", track.Playlists?.Select(p => p.UserEmail) ?? new List<string> { "N/A" });
                }

              
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }

    }
}
