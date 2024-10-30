namespace dvdrental.IService
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile img, string[] fileext);
        Task<string> SaveFile(string[] fileext);
    }
}
