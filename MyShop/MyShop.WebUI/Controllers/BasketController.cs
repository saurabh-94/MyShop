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
        private readonly IRepository<Customer> _customerRepository;

        public BasketController(IBasketservice basketService , IOrderService orderService , IRepository<Customer> customerRepository)
        {
            _basketService = basketService;
            _orderService = orderService;
            _customerRepository = customerRepository;
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

        [Authorize]
        public ActionResult Checkout()
        {
            var customer = _customerRepository.Collection().FirstOrDefault(c=>c.Email ==  this.User.Identity.Name);

            if(customer != null)
            {
                Order order = new Order();

                order.FirstName = customer.FirstName;
                order.LastName = customer.LastName;
                order.State = customer.State;
                order.Street = customer.Street;
                order.City = customer.City;
                order.ZipCode = customer.ZipCode;
                order.Email = customer.Email;

                return View(order);
            }

            return View("Error");
            
        }

        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order)
        {
            var basketItems = _basketService.GetBasketItems(this.HttpContext);

            order.OrderStatus = "Order Created";
            order.Email = this.User.Identity.Name;

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