using System.Linq;

namespace Entities.Accounting.Extensions
{
    public static class CartExtensions
    {
        public static Invoice ToInvoice(this Cart cart)
        {
            Invoice invoice = new Invoice();
            if (cart != null)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(cart);

                invoice = Newtonsoft.Json.JsonConvert.DeserializeObject<Invoice>(json);

                invoice.invoiceProducts = cart.cartProducts?.Select(s => s.ToInvoice());
            }

            return invoice;
        }
    }
}
