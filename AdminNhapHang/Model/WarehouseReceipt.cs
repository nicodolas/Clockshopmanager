using System;
using System.Collections.Generic;
using System.Linq;

namespace AdminNhapHang.Model
{
    public class WarehouseReceipt
    {
        public string ReceiptId { get; set; }         // Mã phiếu nhập
        public DateTime Date { get; set; }            // Ngày nhập  
        public int Status { get; set; }

        private decimal _grandTotal;
        public decimal GrandTotal
        {
            get { return _grandTotal; }
            set { _grandTotal = value; }
        }

        private List<WarehouseReceiptDetail> _details = new List<WarehouseReceiptDetail>();
        public List<WarehouseReceiptDetail> Details
        {
            get => _details;
            set
            {
                _details = value;
                _grandTotal = _details.Sum(t => t.Total); // Cập nhật tự động GrandTotal
            }
        }
    }
}
