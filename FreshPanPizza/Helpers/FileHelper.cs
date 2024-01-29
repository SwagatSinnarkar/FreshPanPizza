using FreshPanPizza.Interfaces;

namespace FreshPanPizza.Helpers
{
    public class FileHelper : IFileHelper
    {
        //We need to find out this images folder location (wwwroot\assets\images) to save any images.
        //so we`ll find out using Hosting Enivornment Variable.

        IWebHostEnvironment _env;

        public FileHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        //Pass the file name in "fileName" parameter
        private string GenerateFileName(string fileName)
        {
            //Then I`ll extract it from the dot. Means I`m just removing the file extension(jpg, png).
            string[] strName = fileName.Split('.');
            //Creating fileName based upon the DateTime
            string strFileName = DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }

        public void DeleteFile(string imgUrl)
        {
            //delete existing file
            if (File.Exists(_env.WebRootPath + imgUrl))
            {               
                File.Delete(_env.WebRootPath + imgUrl);
            }
        }

        public string UploadFile(IFormFile file)
        {

            //here, we just finding out the complete path where the file is going to be upload.
            var uploads = Path.Combine(_env.WebRootPath, "adminImagesCustom");

            //If directory is not available it will created (CreateDirectory) & if directory is available it`ll not do anything
            //because if you will do for first time let say the "adminImagesCustom" folder is not there so first it`ll create it.
            //but it`ll be done only one time. Able to trying to upload first picture in this "adminImagesCustom" folder after that
            //the directory is already there then it will not be created.
            bool exists = Directory.Exists(uploads);
            if (!exists)
            {
                Directory.CreateDirectory(uploads);
            }

            //Saving File
            //There were two ways to save the file first with the same file/image name or the second way is with DateTime.
            //Second option is mostly using because sometime it`s happen that multiple files can have the same name but they
            ////mught be uploaded for the different purpose & thus they can create discrepancy.
            //So we`ve created the GenerateFileName() method to create the file name based upon DateTime.

            //In fileName: we are uploading the new FileName.
            var fileName = GenerateFileName(file.FileName);
            var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
            file.CopyToAsync(fileStream);
            fileStream.Close();
            return "/adminImagesCustom/" + fileName;
        }
    }
}
