using ImageMagick;

namespace Crop2.Models
{
    public class FileUploader
    {
        public  bool Resize(int width, int height, string source_path)
        {
            if (!File.Exists(source_path))
            {
                return false;
            }
            var local_image_dir = $"{source_path}";
            var file_path = Path.GetDirectoryName(source_path) ;
            if (!Directory.Exists(Path.Combine(file_path)))
                Directory.CreateDirectory(Path.Combine(file_path));


            using (var image = new MagickImage(source_path))
            {
                image.Resize(width, height);
                image.Write(source_path);
            }

            return true;
        }
    }
}
