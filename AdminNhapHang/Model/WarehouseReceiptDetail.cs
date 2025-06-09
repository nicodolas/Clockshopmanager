using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminNhapHang.Model
{
    public class WarehouseReceiptDetail
    {
        public string ReceiptId { get; set; }         // Khóa chính + ngoại đến phiếu nhập
        public string ProductId { get; set; }         // Mã sản phẩm
        public string Color { get; set; }
        private int _quantity;
        private decimal _unitPrice;
        private decimal _total;

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                UpdateTotal();
            }
        }

        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                _unitPrice = value;
                UpdateTotal();
            }
        }

        public decimal Total
        {
            get => _total;
            set => _total = value; // Cho phép gán thủ công nếu cần
        }

        private void UpdateTotal()
        {
            _total = _quantity * _unitPrice;
        }

    }
    public class ReceiptItemSummary
    {
        public string ProductId { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
    }
}
