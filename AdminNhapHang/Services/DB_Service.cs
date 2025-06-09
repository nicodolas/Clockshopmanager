using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AdminNhapHang.Model;

namespace AdminNhapHang.Services
{
    public class DB_Service
    {
        private static Random _random = new Random();
        private static string connectionString = @"Server=localhost;Database=QL_SHOPDONGHO;Integrated Security=True;";

        // Tạo mã phiếu nhập duy nhất
        public string TaoMaPhieuNhap()
        {
            string ngayThang = DateTime.Now.ToString("ddMMyyyy");
            string maPN;

            var maDaTonTai = LayTatCaMaPhieuNhap();

            do
            {
                string randomPart = _random.Next(0, 10000).ToString("D4");
                maPN = "PN" + ngayThang + randomPart;
            }
            while (maDaTonTai.Contains(maPN));

            return maPN;
        }

        private HashSet<string> LayTatCaMaPhieuNhap()
        {
            var result = new HashSet<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT receiptId FROM WarehouseReceipt";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0));
                    }
                }
            }

            return result;
        }

        // Lưu phiếu nhập vào DB
        public bool SaveWarehouseReceipt(WarehouseReceipt receipt)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string insertReceipt = @"INSERT INTO WarehouseReceipt(receiptId, receiptDate, grandTotal, status)
                                         VALUES (@receiptId, @receiptDate, @grandTotal, @status)";
                SqlCommand cmd = new SqlCommand(insertReceipt, conn);

                cmd.Parameters.AddWithValue("@receiptId", receipt.ReceiptId);
                cmd.Parameters.AddWithValue("@receiptDate", receipt.Date);
                cmd.Parameters.AddWithValue("@grandTotal", receipt.GrandTotal);
                cmd.Parameters.AddWithValue("@status", receipt.Status);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        // Lưu chi tiết phiếu nhập vào DB
        public bool SaveWarehouseReceiptDetails(List<WarehouseReceiptDetail> details)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string insertDetail = @"INSERT INTO WarehouseReceiptDetail(receiptId, productId,color, quantity, unitPrice, total)
                                    VALUES (@receiptId, @productId,@color, @quantity, @unitPrice, @total)";

                    int rs = 0;
                    foreach (var detail in details)
                    {
                        using (SqlCommand cmd = new SqlCommand(insertDetail, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@receiptId", detail.ReceiptId);
                            cmd.Parameters.AddWithValue("@productId", detail.ProductId);
                            cmd.Parameters.AddWithValue("@color", detail.Color);
                            cmd.Parameters.AddWithValue("@quantity", detail.Quantity);
                            cmd.Parameters.AddWithValue("@unitPrice", detail.UnitPrice);
                            cmd.Parameters.AddWithValue("@total", detail.Total);

                            rs += cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return rs > 0; 
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    //throw;
                    return false;
                }
            }
        }
        public List<WarehouseReceipt> GetAllWarehouseReceipts(int? status = 0)
        {
            List<WarehouseReceipt> receipts = new List<WarehouseReceipt>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT receiptId, receiptDate, grandTotal, status
                         FROM WarehouseReceipt";
                if (status != null)
                {
                    query += " WHERE status = @status";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (status != null)
                    cmd.Parameters.AddWithValue("@status", status);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        receipts.Add(new WarehouseReceipt
                        {
                            ReceiptId = reader.GetString(0),
                            Date = reader.GetDateTime(1),
                            GrandTotal = reader.GetDecimal(2),
                            Status = reader.GetInt32(3)
                        });
                    }
                }
            }

            return receipts;
        }
        // Lấy chi tiết theo receiptId
public List<WarehouseReceiptDetail> GetReceiptDetails(string receiptId)
{
    var list = new List<WarehouseReceiptDetail>();
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        string sql = @"SELECT receiptId, productId,color, quantity, unitPrice, total
                       FROM WarehouseReceiptDetail
                       WHERE receiptId = @id";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", receiptId);

        conn.Open();
        using (SqlDataReader rd = cmd.ExecuteReader())
        {
            while (rd.Read())
            {
                list.Add(new WarehouseReceiptDetail
                {
                    ReceiptId  = rd.GetString(0),
                    ProductId  = rd.GetString(1),
                    Color=rd.GetString(2),
                    Quantity   = rd.GetInt32(3),
                    UnitPrice  = rd.GetDecimal(4),
                    Total      = rd.GetDecimal(5)
                });
            }
        }
    }
    return list;
}
        public bool UpdateWarehouseReceiptStatus(string receiptId, int status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE WarehouseReceipt 
                         SET status = @status 
                         WHERE receiptId = @receiptId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@receiptId", receiptId);
                cmd.Parameters.AddWithValue("@status", status);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public decimal TinhTongTienPhieuNhap(int? status = null)
        {
            decimal tongTien = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT SUM(grandTotal) FROM WarehouseReceipt";
                if (status != null)
                {
                    query += " WHERE status = @status";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (status != null)
                    cmd.Parameters.AddWithValue("@status", status);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    tongTien = Convert.ToDecimal(result);
                }
            }

            return tongTien;
        }

        public List<ReceiptItemSummary> GetReceiptItemSummaries(string receiptId)
        {
            var list = new List<ReceiptItemSummary>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT productId, color, quantity
                       FROM WarehouseReceiptDetail
                       WHERE receiptId = @receiptId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@receiptId", receiptId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new ReceiptItemSummary
                        {
                            ProductId = reader.GetString(0),
                            Color = reader.GetString(1),
                            Quantity = reader.GetInt32(2)
                        });
                    }
                }
            }

            return list;
        }



    }
}
