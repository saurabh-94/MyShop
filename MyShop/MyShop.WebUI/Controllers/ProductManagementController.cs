using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagementController : Controller
    {
        private readonly IRepository<Product> context;

        private readonly IRepository<ProductCategory> productCategoryRepository;


        public ProductManagementController(IRepository<Product> context , IRepository<ProductCategory> productCategoryRepository)
        {
            this.context = context;

            this.productCategoryRepository = productCategoryRepository;
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
        public ActionResult Create(Product product , HttpPostedFileBase file)
        {
            if(!ModelState.IsValid)
            {
                return View(product);
            }

            else
            {
                if(file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }

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
        public ActionResult Edit(Product product , string Id , HttpPostedFileBase file)
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
                    if (file != null)
                    {
                        productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                        file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                    }

                    productToEdit.Category = product.Category;
                    productToEdit.Description = product.Description;
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