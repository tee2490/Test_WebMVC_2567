using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp5.Data;


namespace WebApp5.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductService(DataContext db,IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> AddUpdate(ProductDto data, IFormFile file)
        {

                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName);
                    var uploads = Path.Combine(wwwRootPath, @"images\products");

                    if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);


                    //กรณีมีรูปภาพเดิมต้องลบทิ้งก่อน
                    if (data.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, data.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //บันทึกรุปภาพใหม่
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    data.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                if (data.Product.Id == 0)
                {
                   await db.Products.AddAsync(data.Product);
                }
                else
                {
                    db.Products.Update(data.Product);
                }

               var success =  await db.SaveChangesAsync() > 0;

            return success;
        }

        public async Task<int> Delete(int id)
        {
            var data = await GetById(id);
            if (data == null) return 0;
            

            var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, data.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            db.Products.Remove(data);
            var success =  await db.SaveChangesAsync() > 0;
            var status = success ? 1 : 2;

            return status;

        }

        public async Task<List<Product>> GetAll()
        {
            return await db.Products.Include(px=>px.Category).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await db.Products.FindAsync(id);
        }

        public ProductDto ProductDto() 
        {
            ProductDto productDto = new()
            {
                Product = new() { Name = "TestProduct", Price = 1, Description = "Test Descript" },
                CategoryList = db.Categories.Select(
                   u => new SelectListItem
                   {
                       Text = u.Name,
                       Value = u.Id.ToString()
                   })
            };

            return productDto;
        }

    }
}
