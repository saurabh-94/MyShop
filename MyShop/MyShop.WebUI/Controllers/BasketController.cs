using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketservice _basketService;

        public BasketController(IBasketservice basketService)
        {
            _basketService = basketService;
        }

        public ActionResult Index()
        {
            var viewModel = _basketService.GetBasketItems(this.HttpContext);

            return View(viewModel);
        }

        public ActionResult AddToBasket(string productId)
        {
            _basketService.AddProductToBasket(this.HttpContext, productId);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string basketItemId)
        {
            _basketService.RemoveItemFromBasket(this.HttpContext, basketItemId);

            return RedirectToAction("Index");
        }

        public PartialViewResult GetBasketSummary()
        {
            var viewModel = _basketService.GetBasketSummay(this.HttpContext);

            return PartialView(viewModel);
        }
    }
}