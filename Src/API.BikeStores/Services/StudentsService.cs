using API.Pitstop.Products.RefitClients;

namespace API.Pitstop.Products.Services
{
    public class StudentsService : IStudentsService

    {
        private readonly IStudentClient _studentsClient;
        public StudentsService(IStudentClient studentClient)
        {
            _studentsClient = studentClient;
        }
        public async Task<IEnumerable<Models.Student>> GetAllStudents()
        {
            var lstStudents = await _studentsClient.GetStudents();

            return lstStudents;
        }
    }
}
