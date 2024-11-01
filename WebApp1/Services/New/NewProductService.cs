
using Microsoft.AspNetCore.Hosting;
using WebApp1.Data;

namespace WebApp1.Services.New
{
    public class NewProductService : INewProductService
    {
        private readonly DataContext db;
        private readonly IWebHostEnvironment webEnv;

        public NewProductService(DataContext db,IWebHostEnvironment webEnv)
        {
            this.db = db;
            this.webEnv = webEnv;
        }

        public List<Product> GetAll(string keyword)
        {
            keyword = keyword?.ToUpper();

            var products = db.Products.OrderByDescending(px => px.Id).ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
                products = products.Where(px => px.Name.ToUpper().Contains(keyword) ||
                px.Price.ToString().Contains(keyword)).ToList();
            }

            return products;
        }

        public void AddData(Product product, IFormFile file)
        {
            string wwwRootPath = webEnv.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString(); 
                var extension = Path.GetExtension(file.FileName); //ดึงนามสกุลไฟล์
                var uploads = Path.Combine(wwwRootPath, "images"); //wwwroot/images
              
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                //แบบที่ 1 บันทึกรุปภาพใหม่(เป็นไฟล์ภายนอก database)
                //wwwRootPath/images/skldjfdslkjfsdk.jpg
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                product.Image = @"\images\" + fileName + extension;
            }


            //แบบที่ 2 บันทึกเนื้อไฟล์แบบ Base64
            using (var memoryStream = new MemoryStream())
            {
                var format = "image/png";
                file.CopyTo(memoryStream);
                product.ImageBase64 = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
            }

            db.Products.Add(product);
            db.SaveChanges();
        }

        public Product SearchData(int id)
        {
            return db.Products.Find(id);
        }

        public void UpdateData(Product product, IFormFile file)
        {
            string wwwRootPath = webEnv.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);
                var uploads = Path.Combine(wwwRootPath, "images");

                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                //กรณีมีรูปภาพเดิมต้องลบทิ้งก่อน
                if (product.Image != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, product.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                //แบบที่ 1 บันทึกรุปภาพใหม่(เป็นไฟล์ภายนอก database)
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                product.Image = @"\images\" + fileName + extension;

                //แบบที่ 2 บันทึกเนื้อไฟล์แบบ Base64 (ภายใน database)
                using (var memoryStream = new MemoryStream())
                {
                    var format = "image/png";
                    file.CopyTo(memoryStream);
                    product.ImageBase64 = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
                }
            }

            db.Products.Update(product);
            db.SaveChanges();
        }

        public void DeleteData(int id)
        {
            var product = SearchData(id);

            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
