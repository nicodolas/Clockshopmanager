using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminNhapHang.Model
{
    public class ReturnRequest
    {
        public string ReturnId { get; set; }
        public string UserId { get; set; }
        public string OrderId { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public List<ReturnItem> ReturnItems { get; set; }
        public int Amount { get; set; } // Tổng tiền trả hàng
    }

    public class ReturnItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string SelectedColor { get; set; }
        public string SelectedImageUrl { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Amount { get; set; }
    }

}
