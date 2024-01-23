namespace FreshPanPizza.Interfaces
{
  //Manage all type of file (images).
    public interface IFileHelper
    {
        void DeleteFile (string imgUrl);
        string UploadFile (IFormFile file);
    }
}
