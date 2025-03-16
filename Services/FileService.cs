namespace Products_Management_API.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _uploadPath;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
            _uploadPath = Path.Combine(_environment.WebRootPath, "Images", "Products");

            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        public string SaveProductImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No file Uploaded");
            }

            // 1. Check file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Invalid file type. Only JPG, JPEG, and PNG are allowed");
            }

            // 2. Check file size (max 2 MB)
            const long maxFileSize = 2 * 1024 * 1024; // 2 MB

            if (file.Length > maxFileSize)
            {
                throw new InvalidOperationException("File size exceeds the maximum limit of 2 MB");
            }

            // 3. Generate unique file name
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_uploadPath, fileName);

            // 4. Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // 5. Return relative path
            return Path.Combine(_uploadPath, fileName);
        }

        public async Task DeleteFileAsync(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                throw new ArgumentException("File path cannot be null or empty");
            }

            var fullPath = Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                try
                {
                    // Delete the file asynchronously
                    await Task.Run(() => File.Delete(fullPath));
                }
                catch (Exception)
                {
                    throw new IOException($"Error occurred while deleting the file: {fullPath}");
                }
            }
            else
            {
                throw new FileNotFoundException($"File not found: {fullPath}");
            }
        }

        //public string GetProductFilePath(string fileName)
        //{
        //    return Path.Combine(_uploadPath, fileName);
        //}
    }
}
