using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelPlanner.Data;
using TravelPlanner.Models;
using TravelPlanner.Models.ViewModels;

namespace TravelPlanner.Controllers
{
    public class TripsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public TripsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Trips
        [Authorize]
        public async Task<IActionResult> Index(string searchString)
        {
            var user = await GetCurrentUserAsync();
            if (searchString != null)
            {
                var trips = _context.Trip
                    .Include(t => t.Client)
                    .Where(t => user == t.User && t.StartDate > DateTime.Now && t.Location.Contains(searchString))
                    .OrderBy(t => t.StartDate);
                    return View(await trips.ToListAsync());
            } else {
                var trips = _context.Trip
                  .Include(t => t.Client)
                  .Where(t => user == t.User && t.StartDate > DateTime.Now)
                  .OrderBy(t => t.StartDate);
                return View(await trips.ToListAsync());
            }
        }
        [Authorize]
        public async Task<IActionResult> List()
        {
                var user = await GetCurrentUserAsync();
                var trips = _context.Trip
                    .Include(t => t.Client)
                    .Where(t => user == t.User && t.StartDate < DateTime.Now)
                    .OrderBy(t => t.StartDate);
                return View(trips);
        }

        // GET: Trips/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            List<Client> clients = await _context.Client.ToListAsync();

            var viewModel = new CreateTripViewModel()
            {
                ClientOptions = clients.Where(c => c.ApplicationUserId == user.Id).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.FullName
                }).ToList()
            };
            return View(viewModel);
        }

        // POST: Trips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTripViewModel tripModel)
        {
            var user = await GetCurrentUserAsync();
            if (ModelState.IsValid)
            {
                _context.Add(tripModel); 
                tripModel.user = user;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                List<Client> clients = await _context.Client.ToListAsync();
                var viewModel = new CreateTripViewModel()
                {
                    ClientOptions = clients.Where(c => c.ApplicationUserId == user.Id).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.FullName
                    }).ToList()
                };
                return View(viewModel);
            }
        }

        // GET: Trips/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,Location, ClientId")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
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
            return View(trip);
        }

        // GET: Trips/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trip.FindAsync(id);
            _context.Trip.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trip.Any(e => e.Id == id);
        }
    }
}
