using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contracts
{
    public interface IBasketservice
    {
        void AddProductToBasket(HttpContextBase httpContextBase, string productId);

        void RemoveItemFromBasket(HttpContextBase httpContextBase, string basketItemId);

        List<BasketListViewModel> GetBasketItems(HttpContextBase httpContextBase);

        BasketSummaryViewModel GetBasketSummay(HttpContextBase httpContextBase);

        void ClearBasket(HttpContextBase httpContextBas);

    }
}
