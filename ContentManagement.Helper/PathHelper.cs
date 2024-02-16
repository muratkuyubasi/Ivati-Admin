using Microsoft.Extensions.Configuration;

namespace ContentManagement.Helper
{
    public class PathHelper
    {
        public IConfiguration _configuration;

        public PathHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string DocumentPath
        {
            get
            {
                return _configuration["DocumentPath"];
            }
        }

        public string UserProfilePath
        {
            get
            {
                return _configuration["UserProfilePath"];
            }
        }
        public string CourseFilePath
        {
            get
            {
                return _configuration["CourseFilePath"];
            }
        }
        public string CourseCoverPath
        {
            get
            {
                return _configuration["CourseCoverPath"];
            }
        }

        public string LessonFilePath
        {
            get
            {
                return _configuration["LessonFilePath"];
            }
        }

        public string GalleryPath
        {
            get
            {
                return _configuration["Galleries"];
            }
        }

        public string ZoomApiKey
        {
            get
            {
                return _configuration["ZoomApiKey"];
            }
        }
        public string ZoomSecretKey
        {
            get
            {
                return _configuration["ZoomSecretKey"];
            }
        }
        public string wwwRootPath
        {
            get
            {
                return _configuration["wwwRootPath"];
            }
        }

        public string ImagePath
        {
            get
            {
                return _configuration["Images"];
            }
        }


    }
}
