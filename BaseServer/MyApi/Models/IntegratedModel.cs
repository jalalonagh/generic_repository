using Entities.Accounting;
using Entities.Base;
using Entities.Content;
using Entities.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moradi.Models
{
    public class IntegratedModel
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<ShopProduct> stores { get; set; }
        public IEnumerable<Option> options { get; set; }
        public IEnumerable<Shop> shops { get; set; }
        public IEnumerable<Menu> menus { get; set; }
        public IEnumerable<Content> contents { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Warrenty> warrenties { get; set; }
    }
}
