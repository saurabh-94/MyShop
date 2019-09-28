using MyShop.Core.Contracts;
using MyShop.Core.Models;
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
        private readonly IOrderService _orderService;

        public BasketController(IBasketservice basketService , IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
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

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            var basketItems = _basketService.GetBasketItems(this.HttpContext);

            order.OrderStatus = "Order Created";

            // payment process

            order.OrderStatus = "Payment Processed";

            _orderService.CreateOrder(order, basketItems);

            _basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Thankyou", new { OrderId = order.Id });
        }

        public ActionResult Thankyou(string OrderId)
        {
            ViewBag.OrderId = OrderId;

            return View();
        }
    }
}