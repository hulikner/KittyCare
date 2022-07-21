using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using KittyCare.Models;
using KittyCare.Models.ViewModels;
using KittyCare.Repositories;
using System.Security.Claims;
using System;

namespace KittyCare.Controllers
{
    public class ProvisionsController : Controller
    {
        private readonly IProvisionRepository _provisionRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly ICatRepository _catRepo;
        private readonly IProviderRepository _providerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        public ProvisionsController(
            IProvisionRepository provisionRepository,
            IOwnerRepository ownerRepository,
            ICatRepository catRepository,
            IProviderRepository providerRepository,
            INeighborhoodRepository neighborhoodRepository)
        {
            _provisionRepo = provisionRepository;
            _ownerRepo = ownerRepository;
            _catRepo = catRepository;
            _providerRepo = providerRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }
        // GET: ProvisionsController
        public ActionResult Index()
        {
            List<Provision> provisions = _provisionRepo.GetAllProvisions();

            return View(provisions);
        }

        // GET: ProvisionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProvisionsController/Create
        public ActionResult Create()
        {
            List<Provider> providers = _providerRepo.GetAllProviders();
            List<Cat> cats = _catRepo.GetAllCats();
            ProvisionFormViewModel pfvm = new ProvisionFormViewModel
            {
                Providers = providers,
                Cats = cats,
                Provision = new Provision(),
                CatIds = new List<int>()
            };
            return View(pfvm);
        }

        // POST: ProvisionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProvisionFormViewModel pfvm)
        {
            try
            {
                foreach (int catId in pfvm.CatIds)
                {
                    Provision provision = new Provision
                    {
                        Date = pfvm.Provision.Date,
                        Duration = pfvm.Provision.Duration,
                        ProviderId = pfvm.Provision.ProviderId,
                        CatId = catId
                    };
                    _provisionRepo.AddProvision(provision);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: ProvisionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProvisionsController/Edit/5
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

        // GET: ProvisionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProvisionsController/Delete/5
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
    }
}
