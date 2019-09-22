using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagementController : Controller
    {
        private readonly InMemoryRepository<ProductCategory> productCategoryRepository;

        public ProductCategoryManagementController()
        {
            productCategoryRepository = new InMemoryRepository<ProductCategory>();
        }

        // Get - All Categories

        public ActionResult Index()
        {
            var categories = productCategoryRepository.Collection();

            return View(categories);
        }

        // GET - Edit View
        public ActionResult Edit(string Id)
        {
            var category = productCategoryRepository.Find(Id);

            if(category == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(category);
            }
            
        }

        // POST - Update the category
        [HttpPost]
        public ActionResult Edit(ProductCategory category , string Id)
        {
            var categoryToUpdate = productCategoryRepository.Find(Id);

            if(categoryToUpdate == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }

                else
                {
                    productCategoryRepository.Update(category , Id);
                    productCategoryRepository.Commit();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if(!ModelState.IsValid)
            {
                return View(productCategory);
            }

            else
            {
                productCategoryRepository.Insert(productCategory);
                productCategoryRepository.Commit();

                return RedirectToAction("Index");
            }
        }


        public ActionResult Delete(string Id)
        {
            var category = productCategoryRepository.Find(Id);

            if(category == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(category);
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            var category = productCategoryRepository.Find(Id);

            productCategoryRepository.Delete(category);
            productCategoryRepository.Commit();

            return RedirectToAction("Index");
        }
    }
}