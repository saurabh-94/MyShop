using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService
    {
        private readonly IRepository<Product> _productRepository;

        private readonly IRepository<Basket> _basketRepository;

        public BasketService(IRepository<Product> productRepository , IRepository<Basket> basketRepository)
        {
            _productRepository = productRepository;
            _basketRepository = basketRepository;
        }

        private const string BasketSessionName = "eCommerceBasket";

        private Basket GetBasket(HttpContextBase httpContextBase , bool CreateIfNull)
        {
            Basket basket = new Basket();

            HttpCookie cookie = httpContextBase.Request.Cookies.Get(BasketSessionName);

            if(cookie != null)
            {
                var basketId = cookie.Value;

                if(!string.IsNullOrEmpty(basketId))
                {
                    basket = _basketRepository.Find(basketId);
                }

                else
                {
                    if(CreateIfNull)
                    {
                        basket = CreateNewBasket(httpContextBase);
                    }
                }
            }

            else
            {
                if(CreateIfNull)
                {
                    basket = CreateNewBasket(httpContextBase);
                }
            }

            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContextBase)
        {
            Basket basket = new Basket();

            HttpCookie cookie = new HttpCookie(BasketSessionName);

            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);

            httpContextBase.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddProductToBasket(HttpContextBase httpContextBase , string productId)
        {
            var basket = GetBasket(httpContextBase, true);

            var basketItem = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if(basketItem != null)
            {
                basketItem.Quantity += 1;
            }

            else
            {
                var item = new BasketItem()
                {
                    ProductId = productId,
                    BasketId = basket.Id,
                    Quantity = 1
                };

                basket.BasketItems.Add(item);
            }

            _basketRepository.Commit();
        }

        public void RemoveItemFromBasket(HttpContextBase httpContextBase, string basketItemId)
        {
            var basket = GetBasket(httpContextBase, true);

            var basketItem = basket.BasketItems.FirstOrDefault(i => i.Id == basketItemId);

            if(basketItem != null)
            {
                basket.BasketItems.Remove(basketItem);
                _basketRepository.Commit();
            }
        }
    }
}
