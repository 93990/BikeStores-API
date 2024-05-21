namespace API.Pitstop.Products.Services
{
    public interface IStudentsService
    {
        Task<IEnumerable<Models.Student>> GetAllStudents();
    }
}
