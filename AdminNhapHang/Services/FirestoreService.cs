using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AdminNhapHang.Model;
namespace AdminNhapHang.Services
{
    public class FirestoreService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "https://firestore.googleapis.com/v1/projects/aht-shop/databases/(default)/documents/products";
        private readonly string _baseUrl_Brands = "https://firestore.googleapis.com/v1/projects/aht-shop/databases/(default)/documents/brands";
        private readonly string _baseUrl_Categories = "https://firestore.googleapis.com/v1/projects/aht-shop/databases/(default)/documents/categories";
        private readonly string _baseUrl_Return = "https://firestore.googleapis.com/v1/projects/aht-shop/databases/(default)/documents/returnRequests";
        private readonly string _baseUrl_Invoices = "https://firestore.googleapis.com/v1/projects/aht-shop/databases/(default)/documents/invoices";

        public FirestoreService() => _client = new HttpClient();

        //==================== PRODUCTS =======================//
        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _client.GetAsync(_baseUrl);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to load products.");

            var json = await response.Content.ReadAsStringAsync();
            var documentList = JsonSerializer.Deserialize<FirestoreDocuments>(json);

            return documentList?.documents?.Select(MapToProduct).ToList() ?? new List<Product>();
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                fields = MapProductToFields(product)
            }), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_baseUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            if (string.IsNullOrEmpty(product.ProductId)) return false;

            var url = $"{_baseUrl}/{product.ProductId}";
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                fields = MapProductToFields(product)
            }), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
            {
                Content = content
            };

            var response = await _client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(string documentId)
        {
            if (string.IsNullOrEmpty(documentId)) return false;

            var response = await _client.DeleteAsync($"{_baseUrl}/{documentId}");
            return response.IsSuccessStatusCode;
        }

        private Product MapToProduct(Document doc)
        {
            var fields = doc.fields;
            var colorOptions = fields.colorOptions?.arrayValue?.values?.Select(value =>
            {
                var item = value.mapValue.fields;
                return new ColorOption
                {
                    Color = item.color?.stringValue,
                    ImageUrl = item.imageUrl?.stringValue,
                    Stock = int.TryParse(item.stock?.integerValue, out var s) ? s : 0
                };
            }).ToList() ?? new List<ColorOption>();

            return new Product
            {
                ProductId = doc.name?.Split('/').Last(),
                BrandId = fields.brandId?.stringValue,
                CategoryId = fields.categoryId?.stringValue,
                ProductName = fields.productName?.stringValue,
                ProductDescription = fields.productDescription?.stringValue,
                ProductImageUrl = fields.productImageUrl?.stringValue,
                ProductPrice = GetProductPrice(fields.productPrice),
                ProductStatus = fields.productStatus?.stringValue,
                ColorOptions = colorOptions
            };
        }

        private object MapProductToFields(Product product)
        {
            var colorValues = product.ColorOptions?.Select(opt => new
            {
                mapValue = new
                {
                    fields = new
                    {
                        color = new { stringValue = opt.Color },
                        imageUrl = new { stringValue = opt.ImageUrl },
                        stock = new { integerValue = opt.Stock.ToString() }
                    }
                }
            }).ToList();

            return new Dictionary<string, object>
            {
                ["brandId"] = new { stringValue = product.BrandId },
                ["categoryId"] = new { stringValue = product.CategoryId },
                ["productName"] = new { stringValue = product.ProductName },
                ["productDescription"] = new { stringValue = product.ProductDescription },
                ["productImageUrl"] = new { stringValue = product.ProductImageUrl },
                ["productPrice"] = new { doubleValue = product.ProductPrice },
                ["productStatus"] = new { stringValue = product.ProductStatus },
                ["colorOptions"] = new { arrayValue = new { values = colorValues } }
            };
        }

        private double GetProductPrice(DoubleField field) =>
            field?.doubleValue ?? (double.TryParse(field?.integerValue, out var val) ? val : 0);

        public async Task<string> GetProductNameByIdAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId)) return null;

            var response = await _client.GetAsync($"{_baseUrl}/{productId}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonSerializer.Deserialize<Document>(json);

            return document?.fields?.productName?.stringValue;
        }
        public async Task<int> GetTotalInvoiceAmountAsync()
        {
            var response = await _client.GetAsync(_baseUrl_Invoices);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to load invoices.");

            var json = await response.Content.ReadAsStringAsync();

            var documentList = JsonSerializer.Deserialize<FirestoreDocumentsInvoice>(json);

            if (documentList?.documents == null)
                return 0;

            int totalAmount = 0;

            foreach (var doc in documentList.documents)
            {
                if (int.TryParse(doc.fields.invoiceTotalAmount?.integerValue, out int amount))
                {
                    totalAmount += amount;
                }
            }

            return totalAmount;
        }

        public async Task<List<ColorOption>> GetColorOptionsByProductIdAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId)) return new List<ColorOption>();

            var url = $"{_baseUrl}/{productId}";
            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to load product details.");

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonSerializer.Deserialize<Document>(json);
            var fields = document?.fields;

            if (fields?.colorOptions?.arrayValue?.values == null)
                return new List<ColorOption>();

            var colorOptions = fields.colorOptions.arrayValue.values.Select(value =>
            {
                var item = value.mapValue.fields;
                return new ColorOption
                {
                    Color = item.color?.stringValue,
                    ImageUrl = item.imageUrl?.stringValue,
                    Stock = int.TryParse(item.stock?.integerValue, out var s) ? s : 0
                };
            }).ToList();

            return colorOptions;
        }
        //===================
        public async Task<bool> UpdateProductStockByColorAsync(string productId, string colorName, int addedStock)
        {
            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(colorName))
                return false;

            // URL Firestore tài liệu + updateMask để chỉ cập nhật colorOptions
            var docUrl = $"{_baseUrl}/{productId}?updateMask.fieldPaths=colorOptions";

            // Bước 1: Lấy dữ liệu sản phẩm hiện tại từ Firestore (không gửi Authorization header)
            var getRequest = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{productId}");
            var getResponse = await _client.SendAsync(getRequest);
            if (!getResponse.IsSuccessStatusCode)
                return false;

            var json = await getResponse.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(json);
            var root = jsonDoc.RootElement;

            if (!root.TryGetProperty("fields", out var fields) ||
                !fields.TryGetProperty("colorOptions", out var colorOptions) ||
                !colorOptions.TryGetProperty("arrayValue", out var arrayValue) ||
                !arrayValue.TryGetProperty("values", out var valuesArray))
            {
                return false;
            }

            // Bước 2: Tìm đúng màu và cập nhật stock bằng cách cộng thêm
            var updatedColorOptions = new List<object>();

            foreach (var item in valuesArray.EnumerateArray())
            {
                var colorFields = item.GetProperty("mapValue").GetProperty("fields");

                var currentColor = colorFields.GetProperty("color").GetProperty("stringValue").GetString();
                var imageUrl = colorFields.GetProperty("imageUrl").GetProperty("stringValue").GetString();
                var currentStockStr = colorFields.GetProperty("stock").GetProperty("integerValue").GetString();
                int.TryParse(currentStockStr, out var currentStock);

                var finalStock = currentColor == colorName ? currentStock + addedStock : currentStock;

                updatedColorOptions.Add(new
                {
                    mapValue = new
                    {
                        fields = new Dictionary<string, object>
                {
                    { "color", new { stringValue = currentColor } },
                    { "imageUrl", new { stringValue = imageUrl } },
                    { "stock", new { integerValue = finalStock.ToString() } }
                }
                    }
                });
            }

            // Bước 3: Gửi PATCH để cập nhật lại colorOptions (không gửi Authorization header)
            var patchBody = new
            {
                fields = new Dictionary<string, object>
        {
            {
                "colorOptions", new
                {
                    arrayValue = new
                    {
                        values = updatedColorOptions
                    }
                }
            }
        }
            };

            var content = new StringContent(JsonSerializer.Serialize(patchBody), Encoding.UTF8, "application/json");
            var patchMethod = new HttpMethod("PATCH");
            var patchRequest = new HttpRequestMessage(patchMethod, new Uri(docUrl))
            {
                Content = content
            };

            var patchResponse = await _client.SendAsync(patchRequest);
            return patchResponse.IsSuccessStatusCode;
        }






        //==================== BRANDS =======================//
        public async Task<List<Brand>> GetBrandsAsync()
        {
            var response = await _client.GetAsync(_baseUrl_Brands);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to load brands.");

            var json = await response.Content.ReadAsStringAsync();
            var documentList = JsonSerializer.Deserialize<FirestoreDocuments>(json);

            return documentList?.documents?.Select(doc => new Brand
            {
                brandId = doc.fields.brandId?.stringValue,
                brandName = doc.fields.brandName?.stringValue
            }).ToList() ?? new List<Brand>();
        }

        //==================== CATEGORIES =======================//
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var response = await _client.GetAsync(_baseUrl_Categories);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to load categories.");

            var json = await response.Content.ReadAsStringAsync();
            var documentList = JsonSerializer.Deserialize<FirestoreDocuments>(json);

            return documentList?.documents?.Select(doc => new Category
            {
                categoryId = doc.fields.categoryId?.stringValue,
                categoryName = doc.fields.categoryName?.stringValue
            }).ToList() ?? new List<Category>();
        }

        //==================== RETURN REQUEST =======================//
        private object MapReturnRequestToFields(ReturnRequest request)
        {
            return new
            {
                returnId = new { stringValue = request.ReturnId },
                userId = new { stringValue = request.UserId },
                orderId = new { stringValue = request.OrderId },
                reason = new { stringValue = request.Reason },
                status = new { stringValue = request.Status },
                requestDate = new { timestampValue = request.RequestDate.ToString("yyyy-MM-ddTHH:mm:ssZ") },
                returnItems = new
                {
                    arrayValue = new
                    {
                        values = request.ReturnItems.Select(item => new
                        {
                            mapValue = new
                            {
                                fields = new
                                {
                                    productId = new { stringValue = item.ProductId },
                                    productName = new { stringValue = item.ProductName },
                                    selectedColor = new { stringValue = item.SelectedColor },
                                    selectedImageUrl = new { stringValue = item.SelectedImageUrl },
                                    price = new { integerValue = item.Price.ToString() },
                                    quantity = new { integerValue = item.Quantity.ToString() },
                                    amount = new { integerValue = item.Amount.ToString() }
                                }
                            }
                        }).ToArray()
                    }
                }
            };
        }

        public async Task<List<ReturnRequest>> GetReturnRequestsAsync()
        {
            var response = await _client.GetAsync(_baseUrl_Return);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to load return requests.");

            var json = await response.Content.ReadAsStringAsync();
            var documentList = JsonSerializer.Deserialize<FirestoreDocuments>(json);

            return documentList?.documents?.Select(MapToReturnRequest).ToList() ?? new List<ReturnRequest>();
        }

        private ReturnRequest MapToReturnRequest(Document doc)
        {
            var f = doc.fields;
            var returnItems = f.returnItems?.arrayValue?.values?.Select(item =>
            {
                var itemFields = item.mapValue.fields;
                return new ReturnItem
                {
                    ProductId = itemFields.productId?.stringValue,
                    ProductName = itemFields.productName?.stringValue,
                    SelectedColor = itemFields.selectedColor?.stringValue,
                    SelectedImageUrl = itemFields.selectedImageUrl?.stringValue,
                    Price = int.TryParse(itemFields.price?.integerValue, out var p) ? p : 0,
                    Quantity = int.TryParse(itemFields.quantity?.integerValue, out var q) ? q : 0,
                    Amount = int.TryParse(itemFields.amount?.integerValue, out var a) ? a : 0
                };
            }).ToList() ?? new List<ReturnItem>();

            return new ReturnRequest
            {
                ReturnId = f.returnId?.stringValue,
                UserId = f.userId?.stringValue,
                OrderId = f.orderId?.stringValue,
                Reason = f.reason?.stringValue,
                Status = f.status?.stringValue,
                RequestDate = DateTime.TryParse(f.requestDate?.timestampValue, out var date) ? date : DateTime.MinValue,
                ReturnItems = returnItems,
                Amount = returnItems.Sum(r => r.Amount)
            };
        }
        public async Task<bool> UpdateReturnRequestStatusAsync(string returnId, string newStatus)
        {

            var url = $"{_baseUrl_Return}/{returnId}?updateMask.fieldPaths=status";

            var content = new StringContent(JsonSerializer.Serialize(new
            {
                fields = new
                {
                    status = new { stringValue = newStatus }
                }
            }), Encoding.UTF8, "application/json");

            var requestMsg = new HttpRequestMessage(new HttpMethod("PATCH"), url)
            {
                Content = content
            };

            var response = await _client.SendAsync(requestMsg);
            return response.IsSuccessStatusCode;
        }












    }


    //==================== MODEL CHO FIRESTORE =======================//
    public class FirestoreDocuments
    {
        public List<Document> documents { get; set; }
    }

    public class Document
    {
        public string name { get; set; }
        public Fields fields { get; set; }
    }
    public class FirestoreDocumentsInvoice
    {
        public List<InvoiceDocument> documents { get; set; }
    }
    public class Fields
    {
        public StringField brandId { get; set; }
        public StringField categoryId { get; set; }
        public StringField categoryName { get; set; }
        public StringField brandName { get; set; }
        public StringField productId { get; set; }
        public StringField productName { get; set; }
        public StringField productDescription { get; set; }
        public StringField productImageUrl { get; set; }
        public StringField productStatus { get; set; }
        public DoubleField productPrice { get; set; }
        public ColorOptionsField colorOptions { get; set; }
        public StringField returnId { get; set; }
        public StringField userId { get; set; }
        public StringField orderId { get; set; }
        public StringField reason { get; set; }
        public StringField status { get; set; }
        public TimestampField requestDate { get; set; }
        public ArrayField returnItems { get; set; }
    }

    public class StringField
    {
        public string stringValue { get; set; }
    }

    public class DoubleField
    {
        public double doubleValue { get; set; }
        public string integerValue { get; set; }
    }

    public class IntegerField
    {
        public string integerValue { get; set; }
    }

    public class TimestampField
    {
        public string timestampValue { get; set; }
    }

    public class ColorOptionsField
    {
        public ArrayValue arrayValue { get; set; }
    }

    public class ArrayField
    {
        public ArrayValue arrayValue { get; set; }
    }


    public class ArrayValue
    {
        public List<ColorOptionMap> values { get; set; }
    }

    public class ColorOptionMap
    {
        public MapValue mapValue { get; set; }
    }

    public class MapValue
    {
        public ColorOptionFields fields { get; set; }
    }

    public class ColorOptionFields
    {
        public StringField color { get; set; }
        public StringField imageUrl { get; set; }
        public IntegerField stock { get; set; }

        // Dành cho return item
        public StringField productId { get; set; }
        public StringField productName { get; set; }
        public StringField selectedColor { get; set; }
        public StringField selectedImageUrl { get; set; }
        public IntegerField price { get; set; }
        public IntegerField quantity { get; set; }
        public IntegerField amount { get; set; }
    }
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent content)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = content
            };

            return await client.SendAsync(request);
        }
    }
    public class InvoiceDocument
    {
        public string name { get; set; }
        public InvoiceFields fields { get; set; }
    }

    public class InvoiceFields
    {
        public InvoiceShippingAddress invoiceShippingAddress { get; set; }
        public TimestampField invoiceDate { get; set; }
        public IntegerField invoiceTotalAmount { get; set; }
        public InvoiceItems invoiceItems { get; set; }
    }

    public class InvoiceShippingAddress
    {
        public MapValue mapValue { get; set; }
    }

    public class InvoiceItems
    {
        public ArrayValue arrayValue { get; set; }
    }

}
