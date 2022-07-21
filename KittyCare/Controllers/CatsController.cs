using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using KittyCare.Models;
using KittyCare.Repositories;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;
using KittyCare.Models.ViewModels;

namespace KittyCare.Controllers
{
    public class CatsController : Controller
    {
        private readonly ICatRepository _catRepo;
        private readonly IOwnerRepository _ownerRepo;
        public CatsController(ICatRepository catRepository, IOwnerRepository ownerRepository)
        {
            _catRepo = catRepository;
            _ownerRepo = ownerRepository;
        }
        // GET: CatsController
        [Authorize]
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Cat> cats = _catRepo.GetCatsByOwnerId(ownerId);

            return View(cats);
        }

        // GET: CatsController/Details/5
        public ActionResult Details(int id)
        {
            Cat cat = _catRepo.GetCatById(id);
            int ownerId = GetCurrentUserId();
            Owner owner = _ownerRepo.GetOwnerById(ownerId);
            CatOwnerViewModel covm = new CatOwnerViewModel();
            covm.Cat = cat;
            covm.Owner = owner;

            if (cat == null)
            {
                return NotFound();
            }

            return View(covm);
        }

        // GET: CatsController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cat cat)
        {
            try
            {
                // update the cats OwnerId to the current user's Id
                cat.OwnerId = GetCurrentUserId();
                Console.WriteLine(cat.OwnerId);

                _catRepo.AddCat(cat);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(cat);
            }
        }

        // GET: CatsController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Cat cat = _catRepo.GetCatById(id);

            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        // POST: CatsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, Cat cat)
        {
            try
            {
                _catRepo.UpdateCat(cat);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(cat);
            }
        }

        // GET: CatsController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Cat cat = _catRepo.GetCatById(id);

            return View(cat);
        }

        // POST: CatsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id, Cat cat)
        {
            try
            {
                _catRepo.DeleteCat(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(cat);
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
