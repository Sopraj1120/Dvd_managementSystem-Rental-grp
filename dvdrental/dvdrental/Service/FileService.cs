using dvdrental.IService;

namespace dvdrental.Service
{
    public class FileService(IWebHostEnvironment Env):IFileService
    {
        public async Task<string> SaveFile(IFormFile img, string[] fileext)
        {
            if (img == null)
            {

                throw new ArgumentNullException(nameof(img));

            }
            var cpath = Env.ContentRootPath;
            var path = Path.Combine(cpath, "Uploads");
            if (!Directory.Exists(path))
            {

                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(img.FileName);
            if (!fileext.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", fileext)} are allowed.");

            }
            var fileName = $"{Guid.NewGuid().ToString()}{ext}";
            var fileNmPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNmPath, FileMode.Create);
            await img.CopyToAsync(stream);

            return fileNmPath;
        }

        public void Deleteimg(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }
            var cpath = Env.ContentRootPath;
            var path = Path.Combine(cpath, "Uploads", filename);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Invalid File path");
            }
            File.Delete(path);
        }

        public Task<string> SaveFile(string[] fileext)
        {
            throw new NotImplementedException();
        }
    }
}
   
