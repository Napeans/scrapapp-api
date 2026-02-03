using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrapapp_api.Models
{
    public class Mst_Scrap_Type
    {
        public int ScrapTypeId { get; set; }
        public string ScrapType { get; set; }
        public string ScrapTypeIcon { get; set; }
    }


    public class ProductDetailsModel
    {
        public int ProductId { get; set; }
        public int ScrapTypeId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int OrderBy { get; set; }

        public decimal MarketPrice { get; set; }

        public decimal OurPrice { get; set; }
        public string QuantityPerPrice { get; set; }
    }
}