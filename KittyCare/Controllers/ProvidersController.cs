using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KittyCare.Models.ViewModels;
using System.Collections.Generic;
using KittyCare.Models;
using KittyCare.Repositories;
using System.Security.Claims;

namespace KittyCare.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly IProviderRepository _providerRepo;
        private readonly IProvisionRepository _provisionRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        // ASP.NET will give us an instance of our Provider Repository. This is called "Dependency Injection"
        public ProvidersController(
            IProviderRepository providerRepository,
            IProvisionRepository provisionRepository,
            INeighborhoodRepository neighborhoodRepo)
        {
            _providerRepo = providerRepository;
            _provisionRepo = provisionRepository;
            _neighborhoodRepo = neighborhoodRepo;
        }
        // GET: Providers
        public ActionResult Index()
        {
            if (GetCurrentUserId() != -1)
            {
                Provider provider = _providerRepo.GetProviderById(GetCurrentUserId());
                List<Provider> providers = _providerRepo.GetProvidersInNeighborhood(provider.NeighborhoodId);
                return View(providers);
            }
            else
            {
                List<Provider> providers = _providerRepo.GetAllProviders();
                return View(providers);
            }

        }

        // GET: Providers/Details/5
        public ActionResult Details(int id)
        {
            Provider provider = _providerRepo.GetProviderById(id);
            List<Provision> provisions = _provisionRepo.GetProvisionsByProviderId(provider.Id);

            if (provider == null)
            {
                return NotFound();
            }

            ProviderProfileViewModel wpvm = new ProviderProfileViewModel()
            {
                Provider = provider,
                Provisions = provisions
            };

            return View(wpvm);
        }

        // GET: ProvidersController/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            ProviderFormViewModel vm = new ProviderFormViewModel()
            {
                Provider = new Provider(),
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        // POST: ProvidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Provider provider)
        {
            try
            {
                _providerRepo.AddProvider(provider);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(provider);
            }
        }

        // GET: ProvidersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProvidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProvidersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProvidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return -1;
            }
            else
            {
                return int.Parse(id);
            }
        }
    }
}
