using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminNhapHang.Model
{
    public class Product
    {
        public string ProductId { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductStatus { get; set; }
        public double ProductPrice { get; set; }

        public List<ColorOption> ColorOptions { get; set; } = new List<ColorOption>();
    }

    public class ColorOption
    {
        public string Color { get; set; }
        public string ImageUrl { get; set; }
        public int Stock { get; set; }
    }

}
