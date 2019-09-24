using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        private readonly IRepository<ProductCategory> _productCategoryRepository;

        public HomeController(IRepository<Product> productRepository , IRepository<ProductCategory> productCategoryRepository)
        {
            _productRepository = productRepository;

            _productCategoryRepository = productCategoryRepository;
        }

        public ActionResult Index(string Category = null)
        {
            ProductListViewModel viewModel = new ProductListViewModel();

            viewModel.ProductCategories = _productCategoryRepository.Collection().ToList();

            List<Product> products;

            if(Category == null)
            {
                products = _productRepository.Collection().ToList();
            }

            else
            {
                products = _productRepository.Collection().Where(c => c.Category == Category).ToList();
            }

            viewModel.Products = products;

            return View(viewModel);
        }

        public ActionResult Details(string Id)
        {
            var product = _productRepository.Find(Id);

            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}