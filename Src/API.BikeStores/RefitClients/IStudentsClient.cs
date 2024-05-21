using Refit;

namespace API.Pitstop.Products.RefitClients
{
    [Headers("Content-Type: application/json")]
    public interface IStudentClient
    {
        [Get(path:"/v1/student")]
        Task<IEnumerable<Models.Student>> GetStudents();
    }
}
