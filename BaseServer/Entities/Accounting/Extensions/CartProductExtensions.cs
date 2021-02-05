using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Accounting.Extensions
{
    public static class CartProductExtensions
    {
        public static InvoiceProduct ToInvoice(this CartProduct cartProduct)
        {
            InvoiceProduct product = new InvoiceProduct();

            if (cartProduct != null)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(cartProduct);

                product = Newtonsoft.Json.JsonConvert.DeserializeObject<InvoiceProduct>(json);
            }

            return product;
        }
    }
}
