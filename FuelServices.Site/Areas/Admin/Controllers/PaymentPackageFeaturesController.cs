using DBContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FuelServices.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PaymentPackageFeaturesController : Controller
    {
        private readonly AirportCoreContext _context;

        public PaymentPackageFeaturesController(AirportCoreContext context)
        {
            _context = context;
        }

        // GET: Admin/PaymentPackageFeatures
        public async Task<IActionResult> Index()
        {
            var airportCoreContext = _context.PaymentPackageFeature.Include(p => p.Feature).Include(p => p.PaymentPackage);
            return View(await airportCoreContext.ToListAsync());
        }

        // GET: Admin/PaymentPackageFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentPackageFeature = await _context.PaymentPackageFeature
                .Include(p => p.Feature)
                .Include(p => p.PaymentPackage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentPackageFeature == null)
            {
                return NotFound();
            }

            return View(paymentPackageFeature);
        }

        // GET: Admin/PaymentPackageFeatures/Create
        public IActionResult Create()
        {
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Name");
            ViewData["PaymentPackageId"] = new SelectList(_context.PaymentPackage, "Id", "DisplayName");
            return View();
        }

        // POST: Admin/PaymentPackageFeatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentPackageId,FeatureId,Value,Unit,Id,Created,Modified,IsDeleted,ItemOrder")] PaymentPackageFeature paymentPackageFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentPackageFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Name", paymentPackageFeature.FeatureId);
            ViewData["PaymentPackageId"] = new SelectList(_context.PaymentPackage, "Id", "DisplayName", paymentPackageFeature.PaymentPackageId);
            return View(paymentPackageFeature);
        }

        // GET: Admin/PaymentPackageFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentPackageFeature = await _context.PaymentPackageFeature.FindAsync(id);
            if (paymentPackageFeature == null)
            {
                return NotFound();
            }
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Name", paymentPackageFeature.FeatureId);
            ViewData["PaymentPackageId"] = new SelectList(_context.PaymentPackage, "Id", "DisplayName", paymentPackageFeature.PaymentPackageId);
            return View(paymentPackageFeature);
        }

        // POST: Admin/PaymentPackageFeatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentPackageId,FeatureId,Value,Unit,Id,Created,Modified,IsDeleted,ItemOrder")] PaymentPackageFeature paymentPackageFeature)
        {
            if (id != paymentPackageFeature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentPackageFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentPackageFeatureExists(paymentPackageFeature.Id))
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
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Name", paymentPackageFeature.FeatureId);
            ViewData["PaymentPackageId"] = new SelectList(_context.PaymentPackage, "Id", "DisplayName", paymentPackageFeature.PaymentPackageId);
            return View(paymentPackageFeature);
        }

        // GET: Admin/PaymentPackageFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentPackageFeature = await _context.PaymentPackageFeature
                .Include(p => p.Feature)
                .Include(p => p.PaymentPackage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentPackageFeature == null)
            {
                return NotFound();
            }

            return View(paymentPackageFeature);
        }

        // POST: Admin/PaymentPackageFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentPackageFeature = await _context.PaymentPackageFeature.FindAsync(id);
            _context.PaymentPackageFeature.Remove(paymentPackageFeature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentPackageFeatureExists(int id)
        {
            return _context.PaymentPackageFeature.Any(e => e.Id == id);
        }
    }
}