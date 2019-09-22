using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagementController : Controller
    {
    //    private readonly ProductRepository context;

    //    private readonly ProductCategoryRepository productCategoryRepository;

        private readonly InMemoryRepository<Product> context;

        private readonly InMemoryRepository<ProductCategory> productCategoryRepository;


        public ProductManagementController()
        {
            context = new InMemoryRepository<Product>();

            productCategoryRepository = new InMemoryRepository<ProductCategory>();
        }
        
        public ActionResult Index()
        {
            var products = context.Collection();

            return View(products);
        }

        // Get : Create View
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.ProductCategories = productCategoryRepository.Collection();
            viewModel.Product = new Product();

            return View(viewModel);
        }


        // Post : Add product to in memory cache
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }

            else
            {
                context.Insert(product);
                context.Commit();


                return RedirectToAction("Index");
            }
        }

        // Get : Edit View
        public ActionResult Edit(string Id)
        {
            var product = context.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }

            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategoryRepository.Collection();

                return View(viewModel);
            }
        }

        // post : Update the product in memory cache

        [HttpPost]
        public ActionResult Edit(Product product , string Id)
        {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }

            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {
                    productToEdit.Category = product.Category;
                    productToEdit.Description = product.Description;
                    productToEdit.Image = product.Image;
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price;

                        
                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if(productToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }

            else
            {
                context.Delete(productToDelete);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

    }
}